using DDigital.Interfaz;
using DDigital.Utilidades;
using DPUruNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
                                  //  this.Invoke(new Action(() => lbl_principal.Text = "LECTOR DE HUELLAS DESCONECTADO"));
                                    //Task.Run(() => lbl_principal.ForeColor = Color.Orange);

                                    //this.Invoke(new Action(() => lbl_principal.Text = "LECTOR DE HUELLAS DESCONECTADO"));
                                    //
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

            //if (this.InvokeRequired)
            //{
            //    this.Invoke(new Action(() =>
            //    {
            //        OnCaptured(captureResult);
            //    }));
            //    return;
            //}

            UTILIDADES UT = new UTILIDADES();
            try
            {
                // Check capture quality and throw an error if bad.
                //try
                //{
                //    if (!_sender.CheckCaptureResult(captureResult)) return;
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("Error en CheckCaptureResult: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);            
                //    return;
                //}
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
                    DATA_PERSONA persona = new DATA_PERSONA();
                    HUELLA huella = new HUELLA();
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
                    //this.Invoke(new Action(() =>
                    //{
                    //    txt_codigo.Text = persona.CODIGO;
                    //    txt_dedo.Text = huella._DEDO;
                    //    txt_identidad.Text = persona.IDENTIDAD;
                    //    txt_nombre.Text = persona.NOMBRE;
                    //    txt_observacion.Text = huella._HUE_OBSERVACION;
                    //    txt_tipo.Text = persona.TIPO;
                    //    lbl_principal.Text = "Debe poner el dedo en el lector de huellas";
                    //    lbl_principal.ForeColor = Color.Black;
                    //}));

               

                }
                else {
                    try
                    {
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

        private void Verificacion_Load(object sender, EventArgs e)
        {
            _CRED = _sender.CRED_;
            UT = new UTILIDADES();


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
            if (_CRED.fromAction=="1")
            {
                Application.Exit();
            }
            else
            {
                this.Close();
            }
            
        }

        private void btn_confirmar_Click(object sender, EventArgs e)
        {

        }

        private void Verificacion_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_CRED.fromAction == "1")
            {
                Application.Exit();
            }
           
        }
    }
}
