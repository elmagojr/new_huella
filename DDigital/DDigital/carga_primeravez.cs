using DDigital.Interfaz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DDigital
{
    public partial class carga_primeravez : Form
    {
        Security secu;
        work_flow wf;
        List<string> listObjetos;
        public int valida_comprobacion = 0;
        public carga_primeravez()
        {
            InitializeComponent();
            progressBar1.Minimum = 0;
            listObjetos  = new List<string>();
            listObjetos.AddRange(new string[] {
                "func_trae_info_sys",
                "comprobar_aditamentos",

                "FIGERS",
                "FILIALES_HUELLA",
                "DEDO_MANO_ACTIVO",

                "SP_BUSCAR_PERSONA",
                "sp_firmas_digitales",
                "FIRMAS_X_CUENTA",
                "sp_select_mano",
                "SP_USRS_HUELLAS",
                "VERIFICA_TRANSACCION_HUELLA",

                "FIRMAS_X_CUENTA",
                "after_actauliza_identidad_firmas",
                "tgr_secuencial_fxc",

                "HISTO_HUELLAS",

                "HUELLAS_FIGERS",
                "trg_tablas_parame",

                "ROL_HUELLA",
                "TGR_INSERTED",
                "TGR_ONDELETE",
                "TRG_ONUPDATE",

                "USUARIOS_HUELLAS",
                "TRG_DELETE_USR",
                "TRG_UPDATE_USR",

                "HID"

            });

            progressBar1.Maximum=listObjetos.Count;
            progressBar1.Step=1;
        }
        public async Task COmprobaciones()
        {
            secu = new Security();
            wf = new work_flow();

            foreach (var item in listObjetos)
            {
                await Task.Delay(600);
               
                int valor = wf.comprobarExistenciObjetos(item);
                ExisteObj(valor,item);
                progressBar1.PerformStep();
            }
            label2.Text = "Finalizó la comprobación";
            if (listBox1.Items.Count>0)
            {
                listBox1.Visible = true;
                button1.Visible = true;
                label4.Visible = true;
                label2.Text = "Revise el listado, hay Aditamentos pendientes"; 
                valida_comprobacion = 0;
            }
            else
            {
                valida_comprobacion = 1;
                this.Close();     
            }
        }

        private void ExisteObj(int valor, string item) 
        {
            if (valor ==1)
            {
                //listBox1.Items.Add(item + " (✔️)");
            }
            else
            {                
                listBox1.Items.Add(item + " (❌)");              
               
            }
            listBox1.Refresh();
            //return "";       
        
        }
        private async void carga_primeravez_Load(object sender, EventArgs e)
        {
           await COmprobaciones();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            this.Close();
            //Application.Exit();
        }
    }
}
