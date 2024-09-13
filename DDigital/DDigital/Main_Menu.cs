﻿using DDigital.Interfaz;
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
using System.Timers;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DDigital
{
    public partial class Main_Menu : Form
    {
        work_flow wf; 
        public CREDENCIALES CRED_;
        Bitmap ultima_imagen_huella;
        public Reader _reader;
        private Fmd fmd_Registro;
        List<Fmd> preenrollmentFmds;
        int count;
        bool cancel_enrol = false;
        MANO mano_;
        UTILIDADES UT;
        public HUELLA _HUELLA;
        QUERIES sql;
        public DATA_PERSONA d_persona;
        public string IDENTIDAD_FROM_OUT = "";

        
      











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


        private int DeshabilitarRadios(string IDENTIDAD)
        {
            List<Control> controlsToHide = new List<Control> { p_1, p_2, p_3, p_4, p_5, p_6, p_7, p_8, p_9, p_10 };

            foreach (Control ctrl in controlsToHide)
            {
                ctrl.Visible = true;
            }

            List<Control> radios_enables = new List<Control> { radio_I1, radio_I2, radio_I3, radio_I4, radio_I5, radio_D1, radio_D2, radio_D3, radio_D4, radio_D5 };

            foreach (Control ctrl in radios_enables)
            {
                ctrl.Enabled = false;
            }

            List<Control> checks = new List<Control> { PI1, PI2, PI3, PI4, PI5,PD1,PD2,PD3,PD4,PD5 };

            foreach (Control ctrl in checks)
            {
                ctrl.Visible = false;
            }

            work_flow wf = new work_flow();
            int conteo = 0;
            List<string> list = new List<string>();
            //para desactivar los que no estan permitidos
            list = wf.Deshabilitar_Checks();
            

            string dedo_select = list[0];
            if (dedo_select =="I" | dedo_select == "DI")
            {
                
                if (list.Contains("DI1"))
                {
                    p_10.Visible = false;
                    radio_I1.Enabled = true;
                }
                if (list.Contains("DI2"))
                {
                    p_9.Visible = false;
                    radio_I2.Enabled = true;
                }
                if (list.Contains("DI3"))
                {
                    p_8.Visible = false;
                    radio_I3.Enabled = true;
                }
                if (list.Contains("DI4"))
                {
                    p_7.Visible = false;
                    radio_I4.Enabled = true;
                }
                if (list.Contains("DI5"))
                {
                    p_6.Visible = false;
                    radio_I5.Enabled = true;
                }
            } 






            if (dedo_select == "D" | dedo_select == "DI")
            {
                //tabControl1.SelectedTab = tabPage2;
                if (list.Contains("DD1"))
                {
                    p_1.Visible = false;      
                    radio_D1.Enabled = true; 
                }
                if (list.Contains("DD2"))
                {
                    p_2.Visible = false;
                    radio_D2.Enabled = true;
                }
                if (list.Contains("DD3"))
                {
                    p_3.Visible = false;
                    radio_D3.Enabled = true;
                }
                if (list.Contains("DD4"))
                {
                    p_4.Visible = false;
                    radio_D4.Enabled = true;
                }
                if (list.Contains("DD5"))
                {
                    p_5.Visible = false;
                    radio_D5.Enabled = true;
                }
            }


            tabControl1.SelectedTab = (dedo_select == "I") ? tabPage1 : tabPage2;

            DataTable resultado = wf.Deshabilitar_Radios(IDENTIDAD); //trae todas las huellas
            using (DataTableReader reader = resultado.CreateDataReader())
            {

                while (reader.Read())
                {
                    string DEDO = reader["DEDO"].ToString();
                    switch (DEDO)
                    {
                        case "DI1":
                            radio_I1.Enabled = false;
                            PI1.Visible = true;
                            conteo++;
                            break;
                        case "DI2":
                            radio_I2.Enabled = false;
                            PI2.Visible = true;
                            conteo++;
                            break;
                        case "DI3":
                            radio_I3.Enabled = false;
                            PI3.Visible = true;
                            conteo++;
                            break;
                        case "DI4":
                            radio_I4.Enabled = false;
                            PI4.Visible = true;
                            conteo++;
                            break;
                        case "DI5":
                            radio_I5.Enabled = false;
                            PI5.Visible = true;
                            conteo++;
                            break;
                        case "DD1":
                            radio_D1.Enabled = false;
                            PD1.Visible = true;
                            conteo++;
                            break;
                        case "DD2":
                            PD2.Visible = true;
                            radio_D2.Enabled = false;
                            conteo++;
                            break;
                        case "DD3":
                            PD3.Visible = true;
                            radio_D3.Enabled = false;
                            conteo++;
                            break;
                        case "DD4":
                            PD4.Visible = true;
                            radio_D4.Enabled = false;
                            conteo++;
                            break;
                        case "DD5":
                            PD5.Visible = true;
                            radio_D5.Enabled = false;
                            conteo++;
                            break;
                    }
                }
            }
            


            return conteo;

        }


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
            UT = new UTILIDADES();  
            //_reader = new Reader();
            _reader = ReaderCollection.GetReaders()[0];

            
            if (_reader==null)
            {
                MessageBox.Show(UT.HAS_ERROS("LE00001"));
                Task.Run(() => CancelarEnrrol());
                return false;
            }
            using (Tracer tracer = new Tracer("Main_Menu::OpenReader"))
            {
                //reset = false;
                Constants.ResultCode result = Constants.ResultCode.DP_DEVICE_FAILURE;

                // Open reader
                result = _reader.Open(Constants.CapturePriority.DP_PRIORITY_COOPERATIVE);
          
                if (result != Constants.ResultCode.DP_SUCCESS)
                {
                    MessageBox.Show(UT.HAS_ERROS("LE00001") + result);
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
                //if (_reader!=null)
                //{
                //    lbl_estado.Text = "Lector de huellas no esta conectado";
                //   // return false;   
                //}
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
                    // Task.Run(() => _reader.CancelCapture());
                    _reader.CancelCapture();

                    // Dispose of reader handle and unhook reader events.
                    //Task.Run(()=> _reader.Dispose())  ;
                    _reader.Dispose();
                     count = 0;
                    _reader = null;
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
            UT = new UTILIDADES();
            using (Tracer tracer = new Tracer("Main_Menu::CheckCaptureResult"))
            {
                if (captureResult.Data == null || captureResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
                {
                    if (captureResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
                    {
                        //CancelarEnrrol();
                        //Task.Run(() => CancelarEnrrol());
                  
                        while (!EstadoLector())
                        {
                            _reader.Dispose();
                            _reader.CancelCapture();
                            //Task.Run(() => _reader.Dispose());
                            //Task.Run(() => _reader.CancelCapture());
                            //MessageBox.Show("Error en CheckCaptureResult: Descocncetado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        bool activa_cancel = this.OpenReader();
                        if (activa_cancel)
                        {
                         
                            this.StartCaptureAsync(this.OnCaptured);                      
                        }
                        //this.Invoke(new Action(() =>
                        //{
                        //    this.Close();
                        //}));
                        // throw new Exception(UT.HAS_ERROS("LE00002") + captureResult.ResultCode.ToString());

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

        private void IniciarNuevaLectura(bool isCheck, MANO dataMano)
        {
            btn_registrar.Enabled = false;
            txt_observacion.Enabled = false;
            CancelCaptureAndCloseReader(this.OnCaptured);
            //cancel_enrol = false;
            if (isCheck)
            {
                bool activa_cancel = this.OpenReader();
                if (activa_cancel)
                {
                    btn_cancel_enrol.Visible = true;
                    this.StartCaptureAsync(this.OnCaptured);
                    label1.Text = "Escaneado " + count + " de 4 muestras";
                    label1.ForeColor = Color.Black;
                }
              
            }
            else
            {
                CancelCaptureAndCloseReader(this.OnCaptured);
            }
           
       
        }
            
        public bool EstadoLector()
        {
            UT = new UTILIDADES();
            Reader temp = ReaderCollection.GetReaders()[0];
            if (temp == null)
            {
                this.Invoke(new Action(() =>
                {
                    lbl_estado.Text = "Lector Desconectado";
                }));
            
                return false;
            }
            else
            {
                this.Invoke(new Action(() =>
                {
                    lbl_estado.Text = "Lector conectado";
                }));
           
                return true;
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
                label1.ForeColor = Color.Black;
                DataResult<Fmd> resultConversion = FeatureExtraction.CreateFmdFromFid(captureResult.Data, Constants.Formats.Fmd.ANSI);
            
        

                //firstFinger = resultConversion.Data;

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
                    MessageBox.Show(UT.HAS_ERROS("HD00001"), "Existente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Task.Run(() => _reader.Dispose());
                    //Task.Run(() => _reader.CancelCapture());
                    Task.Run(() => CancelarEnrrol());
                    //return;


                }

                preenrollmentFmds.Add(resultConversion.Data);
                
                
                if (count == 4)
                {
                    label1.Text = "Escaneado " + count + " de 4 muestras";
                    DataResult<Fmd> resultEnrollment = DPUruNet.Enrollment.CreateEnrollmentFmd(Constants.Formats.Fmd.ANSI, preenrollmentFmds);

                   
                    if (resultEnrollment.ResultCode == Constants.ResultCode.DP_SUCCESS)
                    {
                        //MessageBox.Show("Registro exitoso "+mano_.QUE_DEDO + mano_.QUE_MANO);         
                        preenrollmentFmds.Clear();                       
                        label1.Text = "Escaneado " + count + " de 4 muestras";
                        label1.ForeColor = Color.DarkGreen;
                        count = 0;
                        

                       // btn_cancel_enrol.Visible = false;

                        fmd_Registro = resultEnrollment.Data;
                        btn_registrar.Enabled = true;
                        txt_observacion.Enabled = true;
                       
                        Task.Run(() => CancelCaptureAndCloseReader(this.OnCaptured));
                        return;
                    }
                    else if (resultEnrollment.ResultCode == Constants.ResultCode.DP_ENROLLMENT_INVALID_SET)
                    {
                     
                        MessageBox.Show("Todas las huellas escaneadas no coiciden. Intente de nuevo.","Aviso", MessageBoxButtons.OK,MessageBoxIcon.Hand);
                        preenrollmentFmds.Clear();
                        count = 0;
                        label1.Text = "Escaneado " + count + " de 4 muestras";
                        label1.ForeColor = Color.Black;
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
               // CancelCaptureAndCloseReader(this.OnCaptured);
                Task.Run(() => _reader.Dispose());
                Task.Run(() => _reader.CancelCapture());
                //CancelarEnrrol();
                MessageBox.Show(ex.Message.ToString(),"Error",MessageBoxButtons.OK,MessageBoxIcon.Information);

            }
            //finally { Task.Run(() => CancelarEnrrol()); _HUELLA = null; }
        }


        private HUELLA ObtenerHuella(Fmd data_fmd, Bitmap IMG)
        {
            UT = new UTILIDADES();
                 string x = Fmd.SerializeXml(data_fmd);
            byte[] tobytes = Encoding.UTF8.GetBytes(x);

            byte[] jpg_bytes = UT.ConveritirBitmap_tojpeg(IMG);

            HUELLA nueva_huella = new HUELLA();

            nueva_huella._USR_AGREGO = CRED_.usr_logged;
            DATA_PERSONA DP = new DATA_PERSONA();
            work_flow wf = new work_flow();
        
            DP = wf.InformacionVerificacion(CRED_.identidad, 1);

            nueva_huella._HUE_CODIGO = CRED_.codigo;
            nueva_huella._HUE_IDENTIDAD = CRED_.identidad;
            nueva_huella._HUE_TIPO_PER = DP.TIPO;
            nueva_huella._HUELLA = tobytes;
            nueva_huella._HUE_COMPANIA = CRED_.cia;
            nueva_huella._HUE_OBSERVACION = txt_observacion.Text;
            nueva_huella._DEDO = mano_.QUE_MANO+mano_.QUE_DEDO ;
            nueva_huella._HUELLA_SAMPLE = jpg_bytes;

            return nueva_huella;
        }


        private const int PROBABILITY_ONE = 0x7fffffff;

       // int count = 0;
        DataResult<Fmd> resultEnrollment;
        //List<Fmd> preenrollmentFmds;
        
        public bool VerificarExistenciaHuella(Fmd fmd1) 
        {
           
           work_flow wf = new work_flow();
           return wf.verificacion_huella(fmd1, out _HUELLA);
        
        }
        private void CancelarEnrrol()
        {

            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    CancelarEnrrol();
                }));
                return;
            }
            btn_registrar.Enabled = false;
            txt_observacion.Enabled = false;
            btn_cancel_enrol.Visible = false;
            pic_huella.Image = null;
            // _reader.CancelCapture();
            //_reader.Dispose();
            CancelCaptureAndCloseReader(this.OnCaptured);
            count = 0;
            label1.Text = "Escaneado " + count + " de 4 muestras";
            label1.ForeColor = Color.Black;
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
            timer1.Start();
            lbl_mano.Text = tabControl1.SelectedTab.Text;
 
            UT = new UTILIDADES();
            wf = new work_flow();
            d_persona = new DATA_PERSONA();
            CRED_ =  UT.LEER_CREDENCIALES();
            try
            {
                d_persona = wf.InformacionVerificacion(CRED_.identidad, 1);
                txt_nombre.Text = d_persona.NOMBRE;
                txt_identidad.Text = d_persona.IDENTIDAD;
                txt_tipo_per.Text = d_persona.TIPO;
                txt_codigo.Text = d_persona.CODIGO;

                //lbl_estado.Text = "";
                pic_huella.Image = null;
                preenrollmentFmds = new List<Fmd>();
                count = 0;
                label1.Text = "Escaneado " + count + " de 4 muestras";
                if (CRED_ != null)
                {
                    int CONTEO_HUELLAS = 0;
                   
                        DeshabilitarRadios(CRED_.identidad);
                  
                    lbl_count_hue.Text = CONTEO_HUELLAS.ToString();
                    switch (CRED_.fromAction)
                    {
                        case "1":
                            if (wf.Es_Mancomunada(CRED_) > 1 && CRED_.cta != "ver")
                            {
                                verifica_mancomuna vm = new verifica_mancomuna();
                                vm._sender = this;
                                vm.ShowDialog();
                                vm.Dispose();
                                vm = null;
                            }
                            else
                            {
                                Verificacion vr = new Verificacion();
                                vr._sender = this;
                                vr.ShowDialog();
                                vr.Dispose();
                                vr = null;
                            }

                            break;
                        case "":
                        default:
                            break;
                    }

                }
                else
                {
                    MessageBox.Show("ERROR IO0002: " + UT.HAS_ERROS("IO0002"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Application.Exit();
                    return;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show( ex.Message);
                Application.Exit();
                return;
            }

           

         


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
            _HUELLA = ObtenerHuella(fmd_Registro, ultima_imagen_huella);
            work_flow wf = new work_flow();
            UT = new UTILIDADES();
            if (wf.registro_huella(_HUELLA))
            {
                txt_observacion.Clear();
                MessageBox.Show("Huella guardada con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Task.Run(() => CancelarEnrrol());
            }
            else
            {
                MessageBox.Show("HD00002 No filas afectadas: "+UT.HAS_ERROS("HD00002"), "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            int CONTEO_HUELLAS = DeshabilitarRadios(CRED_.identidad);
            lbl_count_hue.Text = CONTEO_HUELLAS.ToString();

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name== "tabPage1")
            {
                lbl_mano.Text = "Mano Izquierda";
            }
            else
            {
                lbl_mano.Text = "Mano Derecha";
            }
            CancelarEnrrol();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Verificacion vr = new Verificacion();
            vr._sender = this;
            vr.ShowDialog();
            vr.Dispose();
            vr = null;
            //int CONTEO_HUELLAS = DeshabilitarRadios(CRED_.identidad);
            //lbl_count_hue.Text = CONTEO_HUELLAS.ToString();
        }


      
        private void timer1_Tick(object sender, EventArgs e)
        {
            EstadoLector();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void lbl_estado_Click(object sender, EventArgs e)
        {

        }

        private void seccionarManoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_reader!=null)
            {
                MessageBox.Show("Debe cancelar el enrolamiento que inicio", "ADVERTENCIA",MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                return; 
            }
            Select_mano select_Mano = new Select_mano();
            select_Mano.ShowDialog();
            select_Mano.Dispose();
            select_Mano = null;
            //int CONTEO_HUELLAS = DeshabilitarRadios(CRED_.identidad);
            //lbl_count_hue.Text = CONTEO_HUELLAS.ToString();

        }

        private void Main_Menu_Activated(object sender, EventArgs e)
        {
            int CONTEO_HUELLAS = DeshabilitarRadios(CRED_.identidad);

            lbl_count_hue.Text = CONTEO_HUELLAS.ToString();
            //CancelarEnrrol();
        }

        private void listadoDeHuellasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool usardni = false;
            DialogResult result = MessageBox.Show("¿Desea usar el DNI de la persona Actual?", "Usar DNI", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                usardni = true;
            } else if (result == DialogResult.Cancel)
            {
                return;
            }

            listado_huellas lh = new listado_huellas(usardni);
            lh._sender = this;
            lh.ShowDialog();
            lh.Dispose();
            lh = null;
            //int CONTEO_HUELLAS = DeshabilitarRadios(CRED_.identidad);
            //lbl_count_hue.Text = CONTEO_HUELLAS.ToString();

        }
    }
}
