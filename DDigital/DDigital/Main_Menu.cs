using DDigital.Interfaz;
using DDigital.Utilidades;
using DPUruNet;
using DPXUru;
using ProyectoDIGITALPERSONA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DDigital
{
    public partial class Main_Menu : Form
    {
        Bitmap ultima_imagen_huella;
        public Reader _reader;
        List<Fmd> preenrollmentFmds;
        int count;
        bool cancel_enrol = false;
        MANO mano_;
        UTILIDADES UT;
        HUELLA _HUELLA;
        QUERIES sql;

        public bool InitializeReader()
        {
            _reader = ReaderCollection.GetReaders()[0]; // Obtén el primer lector disponible
            if (_reader!= null)
            {
                Constants.ResultCode resultCode = _reader.Open(Constants.CapturePriority.DP_PRIORITY_COOPERATIVE);

                if (resultCode != Constants.ResultCode.DP_SUCCESS)
                {
                    MessageBox.Show("No hay lector conectado");
                    return false;
                }

                return true;
            }
            else
            {
                MessageBox.Show("No hay lector conectado");
                return false;
            }
           
        }

        public bool Reset
        {
            get { return reset; }
            set { reset = value; }
        }
        private bool reset;


        public void GetStatus()
        {
            using (Tracer tracer = new Tracer("Main_Menu::GetStatus"))
            {
                Constants.ResultCode result = _reader.GetStatus();

                if ((result != Constants.ResultCode.DP_SUCCESS))
                {
                    //reset = true;
                    throw new Exception("" + result);
                }

                if ((_reader.Status.Status == Constants.ReaderStatuses.DP_STATUS_BUSY))
                {
                    Thread.Sleep(50);
                }
                else if ((_reader.Status.Status == Constants.ReaderStatuses.DP_STATUS_NEED_CALIBRATION))
                {
                    _reader.Calibrate();
                }
                else if ((_reader.Status.Status != Constants.ReaderStatuses.DP_STATUS_READY))
                {
                    throw new Exception("Reader Status - " + _reader.Status.Status);
                }
            }
        }
        public bool OpenReader()
        {
            //_reader = new Reader();
            _reader = ReaderCollection.GetReaders()[0];

            using (Tracer tracer = new Tracer("Main_Menu::OpenReader"))
            {
                //reset = false;
                Constants.ResultCode result = Constants.ResultCode.DP_DEVICE_FAILURE;

                // Open reader
                result = _reader.Open(Constants.CapturePriority.DP_PRIORITY_COOPERATIVE);
          
                if (result != Constants.ResultCode.DP_SUCCESS)
                {
                    MessageBox.Show("Error:  Lector no conectado" + result);
                    //reset = true;
                    return false;
                }

                return true;
            }
        }

        public bool StartCaptureAsync(Reader.CaptureCallback OnCaptured)
        {
            using (Tracer tracer = new Tracer("Main_Menu::StartCaptureAsync"))
            {
                // Activate capture handler
                _reader.On_Captured += new Reader.CaptureCallback(OnCaptured);

                // Call capture
                if (!CaptureFingerAsync())
                {
                    return false;
                }

                return true;
            }
        }

        public void CancelCaptureAndCloseReader(Reader.CaptureCallback OnCaptured)
        {
            using (Tracer tracer = new Tracer("Main_Menu::CancelCaptureAndCloseReader"))
            {
                if (_reader != null)
                {
                     //Task.Run(() => _reader.CancelCapture());
                    _reader.CancelCapture();

                    // Dispose of reader handle and unhook reader events.
                    //Task.Run(()=> _reader.Dispose())  ;
                    _reader.Dispose();
                     count = 0;

                    //if (reset)
                    //{
                    //    _reader = null;
                    //}
                }
            }
        }
        public bool CaptureFingerAsync()
        {
            using (Tracer tracer = new Tracer("Main_Menu::CaptureFingerAsync"))
            {
                try
                {
                    GetStatus();

                    Constants.ResultCode captureResult = _reader.CaptureAsync(Constants.Formats.Fid.ANSI, Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT, _reader.Capabilities.Resolutions[0]);
                    if (captureResult != Constants.ResultCode.DP_SUCCESS)
                    {
                        //reset = true;
                        throw new Exception("" + captureResult);
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:  " + ex.Message);
                    return false;
                }
            }
        }
        public bool CheckCaptureResult(CaptureResult captureResult)
        {
            using (Tracer tracer = new Tracer("Main_Menu::CheckCaptureResult"))
            {
                if (captureResult.Data == null || captureResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
                {
                    if (captureResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
                    {
                        //reset = true;
                        throw new Exception(captureResult.ResultCode.ToString());
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

        private void IniciarNuevaLectura(bool isCheck, MANO dataMano)
        {
            CancelCaptureAndCloseReader(this.OnCaptured);
            //cancel_enrol = false;
            if (isCheck)
            {
                bool activa_cancel = this.OpenReader();
                if (activa_cancel)
                {
                    btn_cancel_enrol.Visible = true;
                }
                this.StartCaptureAsync(this.OnCaptured);
                label1.Text = "Escaneado " + count + " de 4 muestras";
            }
            else
            {
                CancelCaptureAndCloseReader(this.OnCaptured);
            }
           
          
       
        }


      
        public void OnCaptured(CaptureResult captureResult) 
        {
           
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    OnCaptured(captureResult);
                }));
                return;
            }
           
            UTILIDADES UT = new UTILIDADES();
            try
                {
                // Check capture quality and throw an error if bad.
                if (!this.CheckCaptureResult(captureResult)) return;

                count++;
                label1.Text = "Escaneado " + count + " de 4 muestras";
                DataResult<Fmd> resultConversion = FeatureExtraction.CreateFmdFromFid(captureResult.Data, Constants.Formats.Fmd.ANSI);
            
        

                firstFinger = resultConversion.Data;

                foreach (Fid.Fiv fiv in captureResult.Data.Views)
                {          
                   ultima_imagen_huella  =  UT.CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height);
                    pic_huella.Image = ultima_imagen_huella;
                }
 

                if (resultConversion.ResultCode != Constants.ResultCode.DP_SUCCESS)
                {
                    //this.Reset = true;
                    throw new Exception(resultConversion.ResultCode.ToString());
                }
               // verifica2();
                bool existe =  VerificarExistenciaHuella(resultConversion.Data);
                if (existe)
                {
                    MessageBox.Show("La huella que intenta registrar ya existe.", "Existente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //btn_cancel_enrol.PerformClick();
                }

                preenrollmentFmds.Add(resultConversion.Data);
                
                
                if (count == 4)
                {
                    label1.Text = "Escaneado " + count + " de 4 muestras";
                    DataResult<Fmd> resultEnrollment = DPUruNet.Enrollment.CreateEnrollmentFmd(Constants.Formats.Fmd.ANSI, preenrollmentFmds);

                   
                    if (resultEnrollment.ResultCode == Constants.ResultCode.DP_SUCCESS)
                    {
                        MessageBox.Show("Registro exitoso "+mano_.QUE_DEDO + mano_.QUE_MANO);         
                        preenrollmentFmds.Clear();
                        count = 0;
                        label1.Text = "Escaneado " + count + " de 4 muestras";
                        btn_cancel_enrol.Visible = false;

                        _HUELLA = ObtenerHuella(resultEnrollment.Data, ultima_imagen_huella);

                        return;
                    }
                    else if (resultEnrollment.ResultCode == Constants.ResultCode.DP_ENROLLMENT_INVALID_SET)
                    {
                     
                        MessageBox.Show("Todas las huellas escaneadas no coiciden. Intente de nuevo.","Aviso", MessageBoxButtons.OK,MessageBoxIcon.Hand);
                        preenrollmentFmds.Clear();
                        count = 0;
                        label1.Text = "Escaneado " + count + " de 4 muestras";
                        return;
                    }
                    else if (resultEnrollment.ResultCode == Constants.ResultCode.DP_ENROLLMENT_NOT_READY)
                    {
                        label1.Text = "La muestra es incorrecta. Favor intente de nuevo.";
                        count = 0;
                        return;
                    }

                }
    
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: OnCapture: "+ex.Message.ToString(),"Error",MessageBoxButtons.OK,MessageBoxIcon.Information);

            }
        }


        private HUELLA ObtenerHuella(Fmd data_fmd, Bitmap IMG)
        {
            UT = new UTILIDADES();
                 string x = Fmd.SerializeXml(data_fmd);
            byte[] tobytes = Encoding.UTF8.GetBytes(x);

            byte[] jpg_bytes = UT.ConveritirBitmap_tojpeg(IMG);

            HUELLA nueva_huella = new HUELLA();
            nueva_huella._HUE_CODIGO = "110003815";
            nueva_huella._HUE_IDENTIDAD = "0101198400304";
            nueva_huella._HUE_TIPO_PER = "prueba";
            nueva_huella._HUELLA = tobytes;
            nueva_huella._HUE_COMPANIA = "1";
            nueva_huella._HUE_OBSERVACION = "Observacion nomas";
            nueva_huella._DEDO = mano_.QUE_MANO+mano_.QUE_DEDO ;
            nueva_huella._HUELLA_SAMPLE = jpg_bytes;


            nueva_huella._USR_AGREGO = "elmago";

            work_flow wf = new work_flow();
            wf.InformacionVerificacion("0101198400304", 2);
            


            return nueva_huella;
        }


        private const int PROBABILITY_ONE = 0x7fffffff;
        private Fmd firstFinger;
       // int count = 0;
        DataResult<Fmd> resultEnrollment;
        //List<Fmd> preenrollmentFmds;
        
        private bool VerificarExistenciaHuella(Fmd fmd1) 
        {
            work_flow wf = new work_flow();
           return wf.verificacion_huella(fmd1 );
        
        }
        private void CancelarEnrrol()
        {
            btn_cancel_enrol.Visible = false;
            pic_huella.Image = null;
            // _reader.CancelCapture();
            //_reader.Dispose();
            CancelCaptureAndCloseReader(this.OnCaptured);
            count = 0;
            label1.Text = "Escaneado " + count + " de 4 muestras";
            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                // Recorrer cada control dentro de la TabPage
                foreach (Control control in tabPage.Controls)
                {
                    // Si el control es un RadioButton, deseleccionarlo
                    if (control is RadioButton radioButton)
                    {
                        radioButton.Checked = false;
                    }

                }

            }
        }
        public Main_Menu()
        {
            InitializeComponent();
           
        }

        private void Main_Menu_Load(object sender, EventArgs e)
        {
            //lector = OpenReader();
     
            pic_huella.Image=null;
            preenrollmentFmds = new List<Fmd>();
            count = 0;
            label1.Text = "Escaneado "+count+" de 4 muestras";
         
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
        #region radios
        private void radio_I1_CheckedChanged(object sender, EventArgs e)
        {
            mano_ = new MANO() { QUE_DEDO = "1", QUE_MANO = "DI" };
            IniciarNuevaLectura(radio_I1.Checked,mano_);         
           
        }

        private void radio_I2_CheckedChanged(object sender, EventArgs e)
        {
            mano_ = new MANO() { QUE_DEDO = "2", QUE_MANO = "DI" };
            IniciarNuevaLectura(radio_I2.Checked, mano_);           
         
        }

        private void radio_I3_CheckedChanged(object sender, EventArgs e)
        {
            mano_ = new MANO() { QUE_DEDO = "3", QUE_MANO = "DI" };
            IniciarNuevaLectura(radio_I3.Checked, mano_);
            //IniciarNuevaLectura(mano_);
        }

        private void radio_I4_CheckedChanged(object sender, EventArgs e)
        {
            mano_ = new MANO() { QUE_DEDO = "4", QUE_MANO = "DI" };
            IniciarNuevaLectura(radio_I4.Checked, mano_);
            // IniciarNuevaLectura(mano_);
        }

        private void radio_I5_CheckedChanged(object sender, EventArgs e)
        {
            mano_ = new MANO() { QUE_DEDO = "5", QUE_MANO = "DI" };
            IniciarNuevaLectura(radio_I5.Checked, mano_);
            // IniciarNuevaLectura(mano_);
        }

        private void radio_D1_CheckedChanged(object sender, EventArgs e)
        {
            mano_ = new MANO() { QUE_DEDO = "1", QUE_MANO = "DD" };
            IniciarNuevaLectura(radio_D1.Checked, mano_);
            //IniciarNuevaLectura(mano_);
        }
            
        private void radio_D2_CheckedChanged(object sender, EventArgs e)
        {

            mano_ = new MANO() { QUE_DEDO = "2", QUE_MANO = "DD" };
            IniciarNuevaLectura(radio_D2.Checked, mano_);
            //IniciarNuevaLectura(mano_);
        }

        private void radio_D3_CheckedChanged(object sender, EventArgs e)
        {
            mano_ = new MANO() { QUE_DEDO = "3", QUE_MANO = "DD" };
            IniciarNuevaLectura(radio_D3.Checked, mano_);
            //IniciarNuevaLectura(mano_);
        }

        private void radio_D4_CheckedChanged(object sender, EventArgs e)
        {
            mano_ = new MANO() { QUE_DEDO = "4", QUE_MANO = "DD" };
            IniciarNuevaLectura(radio_D4.Checked, mano_);
            //IniciarNuevaLectura(mano_);
        }

        private void radio_D5_CheckedChanged(object sender, EventArgs e)
        {
            mano_ = new MANO() { QUE_DEDO = "5", QUE_MANO = "DD" };
            IniciarNuevaLectura(radio_D5.Checked, mano_);
            // IniciarNuevaLectura(mano_);
        }
        #endregion

        private void btn_cancel_enrol_Click(object sender, EventArgs e)
        {
            CancelarEnrrol();
           
        }



        private void btn_registrar_Click(object sender, EventArgs e)
        {
            work_flow wf = new work_flow();

            if (wf.registro_huella(_HUELLA))
            {
                MessageBox.Show("Huella guardada con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se pudo guardar la huella", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CancelarEnrrol();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Verificacion vr = new Verificacion();
            vr.ShowDialog();
        }
    }
}
