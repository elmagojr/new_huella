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
        public carga_primeravez()
        {
            InitializeComponent();
            progressBar1.Minimum = 0;
            listObjetos  = new List<string>();
            listObjetos.AddRange(new string[] {
                "comprobar_aditamentos",
                "HISTO_HUELLAS",
                "HUELLAS_FIGERS",
                "FIRMAS_X_CUENTA",
                "tgr_secuencial_fxc",
                "after_actauliza_identidad_firmas",
                "ROL_HUELLA",
                "USUARIOS_HUELLAS",
                "SP_BUSCAR_PERSONA",
                "sp_select_mano",
                "VERIFICA_TRANSACCION_HUELLA",
                "SP_USRS_HUELLAS",
                "HID"});

            progressBar1.Maximum=listObjetos.Count;
            progressBar1.Step=1;
        }
        public async Task COmprobaciones()
        {
            secu = new Security();
            wf = new work_flow();


           


            foreach (var item in listObjetos)
            {
                await Task.Delay(1000);  
                ExisteObj(wf.comprobarExistenciObjetos(item),item);
                progressBar1.PerformStep();
            }
            label2.Text = "Finalizó la comprobación";
            if (listBox1.Items.Count>0)
            {
                label2.Text = "Revise el listado, hay Aditamentos pendientes"; 
            }
            else
            {
                this.Close();     
            }

        }

        private void ExisteObj(int valor, string item) 
        {
            if (valor ==1)
            {
               // listBox1.Items.Add(item + " (✔️)");
            }
            else
            {
                listBox1.Visible = true;
                label4.Visible = true;
                listBox1.Items.Add(item + " (❌)");
            }
            listBox1.Refresh();
            //return "";       
        
        }
        private async void carga_primeravez_Load(object sender, EventArgs e)
        {
           await COmprobaciones();
        }
    }
}
