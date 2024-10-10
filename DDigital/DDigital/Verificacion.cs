using DDigital.Interfaz;
using DDigital.Utilidades;
using DPUruNet;
using ProyectoDIGITALPERSONA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace DDigital
{
    public partial class Verificacion : Form
    {
        public Main_Menu _sender;
        CREDENCIALES _CRED;
        int count = 0;
        Bitmap ultima_imagen_huella;
        UTILIDADES UT;
        PERMISOS _permisos;
        bool cancelar=false;

        DATA_PERSONA persona;
        HUELLA huella;
        public Verificacion()
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
            // UT = new UTILIDADES();
            try
            {
                using (Tracer tracer = new Tracer("Verificacion::CheckCaptureResult"))
                {
                    if (captureResult.Data == null || captureResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
                    {
                        if (captureResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
                        {
                            //CancelarEnrrol();
                            //Task.Run(() => CancelarEnrrol());
                            try
                            {
                                while (!EstadoLector())
                                {
                                    lbl_principal.ForeColor = Color.Orange;
                                 
                                    _sender._reader.CancelCapture();
                                    _sender._reader.Dispose();
                                }
                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message);
                            }
                         


                           // this.Invoke(new Action(() => lbl_principal.Text = "Debe poner el dedo en el lector de huellas"));
                            Task.Run(() => lbl_principal.ForeColor = Color.Black);
                            bool activa_cancel = _sender.OpenReader();
                            if (activa_cancel)
                            {
                                _sender.StartCaptureAsync(this.OnCaptured);
                            }
                        }
                        else
                        {
                            //lbl_estado.Text = "Lector conectado";
                        }

                        // Send message if quality shows fake finger
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


            UTILIDADES UT = new UTILIDADES();
            try
            {
             
                try
                {
                    if (!this.CheckCaptureResult(captureResult)) return;
                }
                catch (Exception ex)
                {

                    MessageBox.Show(UT.HAS_ERROS("VE00001") + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
  
                count++;
                DataResult<Fmd> resultConversion = FeatureExtraction.CreateFmdFromFid(captureResult.Data, Constants.Formats.Fmd.ANSI);

                foreach (Fid.Fiv fiv in captureResult.Data.Views)
                {
                    ultima_imagen_huella = UT.CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height);
                    v_pictureBox1.Image = ultima_imagen_huella;
                }


                if (resultConversion.ResultCode != Constants.ResultCode.DP_SUCCESS)
                {            
                    throw new Exception(resultConversion.ResultCode.ToString());
                }
                // verifica2();
                bool existe = _sender.VerificarExistenciaHuella(resultConversion.Data);
                if (existe)
                {
                     persona = new DATA_PERSONA();
                     huella = new HUELLA();
                     work_flow wf = new work_flow();


                    huella = _sender._HUELLA;
                    persona = wf.InformacionVerificacion(huella._HUE_IDENTIDAD, 1);

                    this.Invoke(new Action(() => txt_codigo.Text = persona.CODIGO));
                    this.Invoke(new Action(() => txt_dedo.Text = huella._DEDO));
                    this.Invoke(new Action(() => txt_identidad.Text = persona.IDENTIDAD));
                    this.Invoke(new Action(() => txt_nombre.Text = persona.NOMBRE));
                    this.Invoke(new Action(() => txt_observacion.Text = huella._HUE_OBSERVACION));
                    this.Invoke(new Action(() => txt_tipo.Text = persona.TIPO));
                    this.Invoke(new Action(() => lbl_principal.Text = "Debe poner el dedo en el lector de huellas"));
                    this.Invoke(new Action(() => lbl_principal.ForeColor = Color.Black));

                    if (_CRED.cta!="ver")
                    {
                        VerificarFirma(huella);
                    }
                    else
                    {
                        this.Invoke((Action)(() =>
                        {
                            btn_confirmar.Enabled = _permisos.VerificarHuellas;
                        }));
                    }




                }
                else {
                    try
                    {
                        this.Invoke((Action)(() =>
                        {
                            btn_confirmar.Enabled = false;
                        }));
                            resetVentana();
                        this.Invoke(new Action(() => lbl_principal.Text = "No encontrado. Favor vuelva a escanear el dedo"));
                        Task.Run(() => lbl_principal.ForeColor = Color.Red);

                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("al reset " + ex.Message);
                    }
                  
                    // lbl_principal.Text = "No encontrado. Favor vuelva a escanear el dedo";
                    //  lbl_principal.ForeColor = Color.Red;

                }
          


              
            }
            catch (Exception ex)
            {

                MessageBox.Show(UT.HAS_ERROS("VE00001") + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
        }
        private void resetVentana()
        {
            try
            {
                this.Invoke(new Action(() => txt_codigo.Clear()));
                this.Invoke(new Action(() => txt_dedo.Clear()));
                this.Invoke(new Action(() => txt_identidad.Clear()));
                this.Invoke(new Action(() => txt_nombre.Clear()));
                this.Invoke(new Action(() => txt_observacion.Clear()));
                this.Invoke(new Action(() => txt_tipo.Clear()));
                this.Invoke(new Action(() => lbl_principal.Text = "Debe poner el dedo en el lector de huellas"));
                this.Invoke(new Action(() => lbl_principal.ForeColor = Color.Black));
            }
            catch (Exception ex)
            {

                MessageBox.Show("al reset " + ex.Message);
            }
      

            //txt_codigo.Clear();
            //txt_dedo.Clear();
            //txt_identidad.Clear();
            //txt_nombre.Clear();
            //txt_observacion.Clear();
            //txt_tipo.Clear();
            //v_pictureBox1.Image = null;
            //btn_confirmar.Enabled = false;
            //lbl_principal.Text = "Debe poner el dedo en el lector de huellas";
            //lbl_verifique.Text = "Verifique";


        }
        public void VerificarFirma(HUELLA huella)
        {
            work_flow wf = new work_flow();
         
            int ESTADO = 0;
            try
            {              

                if (huella._HUE_IDENTIDAD != null)
                {
                    //para huella propia
               
                    ESTADO = wf.Verifica_firmas(huella, _CRED, 0);
                    if (ESTADO == 1)
                    {
                        lbl_principal.Invoke(new Action(() => lbl_principal.Text = "La huella pertenece al afiliado"));
                        lbl_principal.Invoke(new Action(() => lbl_principal.ForeColor = Color.Green));

                    }
                    else if (ESTADO == 0)
                    {
                        //para personas autorizadas
                        ESTADO = wf.Verifica_firmas(huella, _CRED, 1);
                        var mensaje = ESTADO == 1 ? "La huella pertenece a una firma autorizada" : "La huella NO coincide como persona o firma autorizada";
                        var colorTexto = ESTADO == 1 ? Color.Green : Color.Red;
                        lbl_principal.Invoke(new Action(() => lbl_principal.Text = mensaje));
                        lbl_principal.Invoke(new Action(() => lbl_principal.ForeColor = colorTexto));
                    }
                    else if (ESTADO < 0)
                    {
                        lbl_principal.Invoke(new Action(() => lbl_principal.Text = $"Esta persona mancomuna ésta cuenta ({_CRED.cta})"));
                        lbl_principal.Invoke(new Action(() => lbl_principal.ForeColor = Color.SteelBlue));
                    }

                }
            }
            finally
            {
                this.Invoke(new Action(() => {
                    // pictureBox1.Visible = ESTADO == 1;
                    lbl_verifique.Text = ESTADO == 1 ? "VERIFICADO POR: " + _CRED.usr_logged.ToString() : "Huella NO Verificada";
                    lbl_verifique.ForeColor = ESTADO == 1 ? Color.Green : Color.Red;
                    btn_confirmar.Enabled = ESTADO == 1 ? _permisos.VerificarHuellas : false;
                }));
            }


        }
        private void Verificacion_Load(object sender, EventArgs e)
        {
            _permisos = _sender.PERMISSIONS_;
            _sender.AplicarPermisos(this, _permisos);
            _CRED = _sender.CRED_;
            UT = new UTILIDADES();

            if (!_permisos.VerificarHuellas)
            {
                lbl_verifique.Text = "Usted no puede verificar Huellas";
            }

            _sender.CancelCaptureAndCloseReader(this.OnCaptured);
            if (!_sender.OpenReader())
            {
                this.Close();
            }
            else
            {
                _sender.StartCaptureAsync(this.OnCaptured);
            }

            //if (!_sender.StartCaptureAsync(this.OnCaptured))
            //{
            //    this.Close();
            //}
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            cancelar=true;
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
            link.TIPO_PER = "0";
            link.USR_VERIFICO = _CRED.usr_logged;
            return link;
        }
        private DATA_INTERSEPTOR Verificacion_aceptada()
        {
            DATA_INTERSEPTOR link = new DATA_INTERSEPTOR();
            link.FLAG = "1";
            link.NOMBRE_VERIFICA = persona.NOMBRE;
            link.HUE_IDENTIDAD = persona.IDENTIDAD;
            link.HUE_CODIGO = persona.CODIGO;
            link.TIPO_PER = persona.TIPO;
            link.USR_VERIFICO = _CRED.usr_logged;
            return link;
        }

        private void btn_confirmar_Click(object sender, EventArgs e)
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

        private void Verificacion_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_CRED.fromAction == "1")
            {
                Application.Exit();

            }
          

        }

        private void btn_lista_hue_Click(object sender, EventArgs e)
        {
            DATA_PERSONA dp = new DATA_PERSONA();
            dp = persona;

            listado_huellas lh = new listado_huellas(false, dp);
            lh._sender = this._sender;
            lh.ShowDialog();
            lh.Dispose();
            lh = null;
        }
    }
}
