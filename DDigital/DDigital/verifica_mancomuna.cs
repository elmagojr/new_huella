using DDigital.Interfaz;
using DPUruNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DDigital.Utilidades
{
    public partial class verifica_mancomuna : Form
    {
        List<string> listaYaVerificado;
        public Main_Menu _sender;
        DataTable data_huellas;
        CREDENCIALES _CRED;
        int CountMancomunados = 0;
        Bitmap ultima_imagen_huella;
        UTILIDADES UT;
        bool cancelar = false;
        DATA_PERSONA persona;
        DATA_PERSONA persona_delGrid;
        HUELLA huella;
        work_flow wf; 
        public verifica_mancomuna()
        {
            InitializeComponent();
        }
        public bool EstadoLector()
        {
            // UT = new UTILIDADES();
            Reader temp = ReaderCollection.GetReaders()[0];
            if (temp == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool CheckCaptureResult(CaptureResult captureResult)
        {

            try
            {
                using (Tracer tracer = new Tracer("verifica_mancomuna::CheckCaptureResult"))
                {
                    if (captureResult.Data == null || captureResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
                    {
                        if (captureResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
                        {
                           
                            try
                            {
                                while (!EstadoLector())
                                {     
                                    _sender._reader.CancelCapture();
                                    _sender._reader.Dispose();
                                    LBL_ESTADO.Invoke(new Action (() => { LBL_ESTADO.Text = "Lector desconectado"; }) );
                                }

                                LBL_ESTADO.Invoke(new Action(() => { LBL_ESTADO.Text = "..."; }));
                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message);
                            }


                       
                            bool activa_cancel = _sender.OpenReader();
                            if (activa_cancel)
                            {
                                _sender.StartCaptureAsync(this.OnCaptured);
                            }
                        }
                        else
                        {

                        }

           
                        if ((captureResult.Quality != Constants.CaptureQuality.DP_QUALITY_CANCELED))
                        {
                            throw new Exception("Calidad de fig - " + captureResult.Quality);
                        }
                        return false;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public void OnCaptured(CaptureResult captureResult)
        {
            wf = new work_flow();
            try
            {
                if (!this.CheckCaptureResult(captureResult)) return;
            }
            catch (Exception ex)
            {

                MessageBox.Show(UT.HAS_ERROS("VE00001") + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }      

    
            DataResult<Fmd> resultConversion = FeatureExtraction.CreateFmdFromFid(captureResult.Data, Constants.Formats.Fmd.ANSI);

            foreach (Fid.Fiv fiv in captureResult.Data.Views)
            {
                ultima_imagen_huella = UT.CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height);
                pic_huella.Image = ultima_imagen_huella;
            }


            if (resultConversion.ResultCode != Constants.ResultCode.DP_SUCCESS)
            {
                throw new Exception(resultConversion.ResultCode.ToString());
            }

            //verifica la huella existe
            bool existe = _sender.VerificarExistenciaHuella(resultConversion.Data);
            if (existe)
            {
                persona = new DATA_PERSONA();
                huella = new HUELLA();
                work_flow wf = new work_flow();
                huella = _sender._HUELLA;//del que puso el dedo en el elector
                persona = wf.InformacionVerificacion(huella._HUE_IDENTIDAD, 1); //de la persona que puso el dedo
                this.Invoke(new Action(() => txt_identidad.Text = persona.IDENTIDAD));
                this.Invoke(new Action(() => txt_nombre.Text = persona.NOMBRE));
                if (!checkBox1.Checked) //automatico
                {
                    if (CountMancomunados <= data_huellas.Rows.Count)
                    {

                        comparar_data(persona, huella);

                    }
                    if (CountMancomunados == data_huellas.Rows.Count)
                    {
                        Task.Run(() => { _sender.CancelCaptureAndCloseReader(this.OnCaptured); });
                        int todo_correcto = VerificaMancomuna(listaYaVerificado); //se verifica el paquete 
                        if (data_huellas.Rows.Count == todo_correcto)
                        {
                            this.Invoke(new Action(() => btn_aceptar.Enabled = true));
                            this.Invoke(new Action(() => pic_check.Visible = true));
                        }
                        else
                        {
                            this.Invoke(new Action(() => btn_aceptar.Enabled = false));
                            this.Invoke(new Action(() => pic_denied.Visible = true));
                        }

                    }
                }
                else //manual
                {
                    comparar_data(persona, huella); //para sacar el listado de verificados 
                    if (CountMancomunados == data_huellas.Rows.Count)
                    {
                        Task.Run(() => { _sender.CancelCaptureAndCloseReader(this.OnCaptured); });
                        int todo_correcto = VerificaMancomuna(listaYaVerificado); //se verifica el paquete 
                        if (data_huellas.Rows.Count == todo_correcto)
                        {
                            this.Invoke(new Action(() => btn_aceptar.Enabled = true));
                            //this.Invoke(new Action(() => pic_check.Visible = true));
                        }
                        else
                        {
                            this.Invoke(new Action(() => btn_aceptar.Enabled = false));
                            //this.Invoke(new Action(() => pic_denied.Visible = true));
                        }
                       

                    }

                }




            }
            else
            {
                this.Invoke(new Action(() => txt_identidad.Clear()));
                this.Invoke(new Action(() => txt_nombre.Clear()));
                //this.Invoke(new Action(() => pic_huella.Image = null)); 
                
            }


        }

        private int VerificaMancomuna(List<string> ya_verificados)
        {
            int conta_man = 0;
            wf= new work_flow();
            foreach (var item in ya_verificados)
            {
                if (wf.Verifica_mancomuna(_CRED, item, 2)==1)
                {
                    conta_man++;
               
                }

            }
            return conta_man;

        }
        private void comparar_data(DATA_PERSONA persona, HUELLA huella)
        {    
            //ACA UNICAMENTE SE ASEGURA DE QUE LAS PERSONAS HAN PUESTO EL DEDO Y QUE ENE EFECTO SON LAS PERSONAS DE LA LISTA DE FIRMAS AUTORIZADAS
            try
            {
                //automatico
                if (checkBox1.Checked==false)
                {
                    foreach (DataRow item in data_huellas.Rows)
                    {
                        if (item[0].ToString() == huella._HUE_TIPO_PER.ToString() && item[0].ToString() == persona.TIPO.ToString() && item[3].ToString() == huella._HUE_IDENTIDAD.ToString() && item[3].ToString() == persona.IDENTIDAD.ToString())
                        {
                            if (!listaYaVerificado.Contains(persona.IDENTIDAD))
                            {
                                listaYaVerificado.Add(persona.IDENTIDAD);                                
                                CountMancomunados++;
                                this.Invoke(new Action(() => lbl_count.Text = (data_huellas.Rows.Count - listaYaVerificado.Count).ToString()));

                            }
                        }
                    }

                }
                else //manual
                {
                    if ( persona_delGrid.IDENTIDAD == huella._HUE_IDENTIDAD.ToString() && persona_delGrid.IDENTIDAD == persona.IDENTIDAD.ToString())
                    {
                        if (!listaYaVerificado.Contains(persona.IDENTIDAD))
                        {
                            listaYaVerificado.Add(persona.IDENTIDAD);
                            CountMancomunados++;
                            this.Invoke(new Action(() => lbl_count.Text = (data_huellas.Rows.Count - listaYaVerificado.Count).ToString()));
                            this.Invoke(new Action(() => pic_check.Visible = true));
                            this.Invoke(new Action(() => pic_denied.Visible = false));

                        

                        }

                    }
                    else
                    {
                        this.Invoke(new Action(() => pic_check.Visible = false));
                        this.Invoke(new Action(() => pic_denied.Visible = true));
                        MessageBox.Show("La persona que puso el dedo, no corresponde con la persona seleccionada", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        private void IniciarNuevaLectura(bool isCheck, MANO dataMano)
        {
            
            //CancelCaptureAndCloseReader(this.OnCaptured);
         
            //if (isCheck)
            //{
            //    bool activa_cancel = this.OpenReader();
            //    if (activa_cancel)
            //    {
            //        btn_cancel_enrol.Visible = true;
            //        this.StartCaptureAsync(this.OnCaptured);
            //        label1.Text = "Escaneado " + count + " de 4 muestras";
            //        label1.ForeColor = Color.Black;
            //    }

            //}
            //else
            //{
            //    CancelCaptureAndCloseReader(this.OnCaptured);
            //}


        }
        private void CargarDatos()
        {
            wf = new work_flow();
            UT = new UTILIDADES();
            try
            {
                data_huellas = new DataTable();
                int sin_huellas = 0;
                data_huellas = wf.Listado_mancomunadas(_CRED,3, out sin_huellas);
                if (data_huellas.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontro datos para esta persona", "NO ENCONTRADO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (sin_huellas > 0)
                {
                    lbl_advertencia.Visible = true;    
                }
                dataGridView1.DataSource = data_huellas;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.ClearSelection();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void verifica_mancomuna_Load(object sender, EventArgs e)
        {
            _CRED = _sender.CRED_;
            UT = new UTILIDADES();
            listaYaVerificado = new List<string>();
            CargarDatos();
            lbl_count.Text = (data_huellas.Rows.Count-listaYaVerificado.Count).ToString();

        
            checkBox1.Checked = true;


            _sender.CancelCaptureAndCloseReader(this.OnCaptured);
     
        }
        private void resetVentana()
        {
            pic_huella.Image = null;
            listaYaVerificado.Clear();
            pic_check.Visible = false;
            pic_denied.Visible = false;
            CountMancomunados = 0;
            this.Invoke(new Action(() => btn_aceptar.Enabled = false));


            lbl_count.Text = (data_huellas.Rows.Count - listaYaVerificado.Count).ToString();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            label7.Text = "";
            resetVentana();
            _sender.CancelCaptureAndCloseReader(this.OnCaptured);
            if (!checkBox1.Checked)
            {
                dataGridView1.Enabled = false;
                dataGridView1.ClearSelection();
                if (_sender.OpenReader())
                {
                    _sender.StartCaptureAsync(this.OnCaptured);
                }
            }
            else
            {
                dataGridView1.Enabled = true;

                _sender.CancelCaptureAndCloseReader(this.OnCaptured);
            }

           
        }

        private void verifica_mancomuna_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_CRED.fromAction == "1")
            {
                Application.Exit();
            }
            else
            {
                this.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            work_flow wf = new work_flow();
            _sender.CancelCaptureAndCloseReader(this.OnCaptured);
            if (_sender.OpenReader())
            {
                _sender.StartCaptureAsync(this.OnCaptured);
            }
            persona_delGrid = new DATA_PERSONA();



            label7.Text = "";
            if (e.RowIndex >= 0)
            {
               
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {

                        persona_delGrid.NOMBRE=row.Cells[2].Value.ToString();
                        persona_delGrid.CODIGO= row.Cells[1].Value.ToString();
                        persona_delGrid.IDENTIDAD=row.Cells[3].Value.ToString();
                        persona_delGrid.TIPO=row.Cells[0].Value.ToString();
                        if (wf.NO_Existe_huella(persona_delGrid.IDENTIDAD) == 0)
                        {
                            label7.Text = "Esta persona no cuenta con huella.";
                        }
                        else
                        {
                            label7.Text = "";
                        }
                        label7.ForeColor = Color.OrangeRed;
                        //label7.Text = (wf.NO_Existe_huella(persona_delGrid.IDENTIDAD) > 0) ? "Esta persona no cuenta con huella." : "";

                        if (listaYaVerificado.Contains(persona_delGrid.IDENTIDAD))
                        { 
                            this.Invoke(new Action(() => pic_check.Visible = true));
                            this.Invoke(new Action(() => pic_denied.Visible = false));
                        }
                        else
                        {
                            this.Invoke(new Action(() => pic_check.Visible = false));
                            this.Invoke(new Action(() => pic_denied.Visible = true));
                        }

                    }
                }
               
            }
           
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            cancelar = true;
            _sender.CancelCaptureAndCloseReader(this.OnCaptured);


            if (_CRED.fromAction == "1")
            {
                Application.Exit();
            }
            else
            {
                this.Close();
            }
        }

        private DATA_INTERSEPTOR Verificacion_cancelada()
        {
            DATA_INTERSEPTOR link = new DATA_INTERSEPTOR();
            link.FLAG = "0";
            link.NOMBRE_VERIFICA = "";
            link.HUE_IDENTIDAD = "0";
            link.HUE_CODIGO = "0";
            link.TIPO_PER = "MAN";
            link.USR_VERIFICO = _CRED.usr_logged;
            return link;
        }
        private DATA_INTERSEPTOR Verificacion_aceptada()
        {
            DATA_INTERSEPTOR link = new DATA_INTERSEPTOR();
            link.FLAG = "1";
            link.NOMBRE_VERIFICA = "";
            link.HUE_IDENTIDAD = "";
            link.HUE_CODIGO = "";
            link.TIPO_PER = "MAN";
            link.USR_VERIFICO = _CRED.usr_logged;
            return link;
        }
        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            cancelar = false;
            UT = new UTILIDADES();
            if (UT.ExportarTXT((cancelar ? Verificacion_cancelada() : Verificacion_aceptada())))
            {
                if (_CRED.fromAction == "1")
                {
                    Application.Exit();

                }
                else
                {
                    this.Close();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
