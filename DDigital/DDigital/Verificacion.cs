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
        int count = 0;
        Bitmap ultima_imagen_huella;
        public Verificacion()
        {
            InitializeComponent();
     
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
                try
                {
                    if (!_sender.CheckCaptureResult(captureResult)) return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error en CheckCaptureResult: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);            
                    return;
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
                    //this.Reset = true;
                    throw new Exception(resultConversion.ResultCode.ToString());
                }
                // verifica2();
                bool existe = _sender.VerificarExistenciaHuella(resultConversion.Data);
                if (existe)
                {
                    DATA_PERSONA persona = new DATA_PERSONA();
                    HUELLA huella = new HUELLA();
                    work_flow wf = new work_flow();

                    var OBJETOS = wf.InformacionVerificacion(_sender.IDENTIDAD_FROM_OUT, 2);
                    persona = OBJETOS.Persona;
                    huella = OBJETOS.hue_data;


                    this.Invoke(new Action(() =>
                    {
                        txt_codigo.Text = persona.CODIGO;
                        txt_dedo.Text = huella._DEDO;
                        txt_identidad.Text = persona.IDENTIDAD;
                        txt_nombre.Text = persona.NOMBRE;
                        txt_observacion.Text = huella._HUE_OBSERVACION;
                        txt_tipo.Text = persona.TIPO;
                        lbl_principal.Text = "Debe poner el dedo en el lector de huellas";
                        lbl_principal.ForeColor = Color.Black;
                    }));

               

                }
                else {

                    this.Invoke(new Action(() =>
                    {
                        resetVentana();
                        lbl_principal.Text = "No encontrado. Favor vuelva a escanear el dedo";
                        lbl_principal.ForeColor = Color.Red;
                    }));
                 
                }
          


              
            }
            catch (Exception ex)
            {
                // Task.Run(() => _sender._reader.Dispose());
                // Task.Run(() => _sender._reader.CancelCapture());
               
               // Task.Run(()=> this.Close());
                MessageBox.Show("Error: OnCapture: " + ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Invoke(new Action(() =>
                {
                    this.Close();
                }));

            }
        }
        private void resetVentana()
        {
            txt_codigo.Clear();
            txt_dedo.Clear();
            txt_identidad.Clear();
            txt_nombre.Clear();
            txt_observacion.Clear();
            txt_tipo.Clear();
            v_pictureBox1.Image = null;
            btn_confirmar.Enabled = false;
            lbl_principal.Text = "Debe poner el dedo en el lector de huellas";
            lbl_verifique.Text = "Verifique";


        }

        private void Verificacion_Load(object sender, EventArgs e)
        {
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
            this.Close();
        }

        private void btn_confirmar_Click(object sender, EventArgs e)
        {

        }
    }
}
