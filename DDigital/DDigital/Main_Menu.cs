using DDigital.Utilidades;
using DPUruNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DDigital
{
    public partial class Main_Menu : Form
    {
        private Reader _reader;
        List<Fmd> preenrollmentFmds;
        int count;

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
                    reset = true;
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
            // _reader = new Reader();
            _reader = ReaderCollection.GetReaders()[0];

            using (Tracer tracer = new Tracer("Main_Menu::OpenReader"))
            {
                reset = false;
                Constants.ResultCode result = Constants.ResultCode.DP_DEVICE_FAILURE;

                // Open reader
                result = _reader.Open(Constants.CapturePriority.DP_PRIORITY_COOPERATIVE);
          
                if (result != Constants.ResultCode.DP_SUCCESS)
                {
                    MessageBox.Show("Error:  Lector no conectado" + result);
                    reset = true;
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
                    _reader.CancelCapture();

                    // Dispose of reader handle and unhook reader events.
                    _reader.Dispose();

                    if (reset)
                    {
                        _reader = null;
                    }
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
                        reset = true;
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
                        reset = true;
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

        //public void OnCaptured(CaptureResult captureResult)
        //{
        //    if (this.InvokeRequired)
        //    {
        //        this.Invoke(new Action(() =>
        //        {
        //            OnCaptured(captureResult);
        //        })); 
        //        return;
        //    }
        //    UTILIDADES UT = new UTILIDADES();
        //    try
        //    {
        //        // Check capture quality and throw an error if bad.
        //        if (!this.CheckCaptureResult(captureResult)) return;

        //        // Create bitmap
        //        foreach (Fid.Fiv fiv in captureResult.Data.Views)
        //        {
        //            pic_huella.Image = UT.CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height);
        //            pic_huella.Refresh();   

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

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

                
               // MessageBox.Show("A finger was captured "+(count));
               // SendMessage(Action.SendMessage, "A finger was captured.  \r\nCount:  " + (count));

                if (resultConversion.ResultCode != Constants.ResultCode.DP_SUCCESS)
                {
                    this.Reset = true;
                    throw new Exception(resultConversion.ResultCode.ToString());
                }

                preenrollmentFmds.Add(resultConversion.Data);
                
                
                if (count == 4)
                {
                    label1.Text = "Escaneado " + count + " de 4 muestras";
                    DataResult<Fmd> resultEnrollment = DPUruNet.Enrollment.CreateEnrollmentFmd(Constants.Formats.Fmd.ANSI, preenrollmentFmds);

                    if (resultEnrollment.ResultCode == Constants.ResultCode.DP_SUCCESS)
                    {
                        MessageBox.Show("An enrollment FMD was successfully created");
                        // SendMessage(Action.SendMessage, "An enrollment FMD was successfully created.");
                        // SendMessage(Action.SendMessage, "Place a finger on the reader.");
                        preenrollmentFmds.Clear();
                        count = 0;
                        label1.Text = "Escaneado " + count + " de 4 muestras";
                        return;
                    }
                    else if (resultEnrollment.ResultCode == Constants.ResultCode.DP_ENROLLMENT_INVALID_SET)
                    {
                        // SendMessage(Action.SendMessage, "Enrollment was unsuccessful.  Please try again.");
                        // SendMessage(Action.SendMessage, "Place a finger on the reader.");
                        MessageBox.Show("Una");
                        preenrollmentFmds.Clear();
                        count = 0;
                        label1.Text = "Escaneado " + count + " de 4 muestras";
                        return;
                    }
                    else if (resultEnrollment.ResultCode == Constants.ResultCode.DP_ENROLLMENT_NOT_READY)
                    {
                        label1.Text = "Muestras correctas son insuficientes, intente de nuevo";
                        count = 0;
                        return;
                    }

                }
            

                // SendMessage(Action.SendMessage, "Now place the same finger on the reader.");
            }
            catch (Exception ex)
            {
                // Send error message, then close form
               // SendMessage(Action.SendMessage, "Error:  " + ex.Message);
            }
        }




        public Main_Menu()
        {
            InitializeComponent();
           
            
            
         

        }

        private void Main_Menu_Load(object sender, EventArgs e)
        {
           bool lector = OpenReader();
     
            pic_huella.Image=null;
            preenrollmentFmds = new List<Fmd>();
            count = 0;
            label1.Text = "Escaneado "+count+" de 4 muestras";
            if (lector)
            {
                if (!this.StartCaptureAsync(this.OnCaptured))
                {
                    this.Close();
                }
            }



        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
