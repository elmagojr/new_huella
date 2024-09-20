using DDigital.Interfaz;
using DDigital.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DDigital
{

    public partial class Select_mano : Form
    {
        public Main_Menu _sender = new Main_Menu();
        MANO mano_seleccionada;
        List<string> Lcheks;
        string PA_VALOR = "";
        PERMISOS _permisos;
        public Select_mano()
        {
            InitializeComponent();
        }

        private void Select_mano_Load(object sender, EventArgs e)
        {
            _permisos = _sender.PERMISSIONS_;
            _sender.AplicarPermisos(this, _permisos);
            Deshabilitar_checks();
            label2.Text = (!ch_activarManoI.Checked && !ch_activarManoD.Checked) ? "Ambas Manos" : tabControlSelectMano.SelectedTab.Text;
        }

        private void Deshabilitar_checks()
        {
            Lcheks = new List<string>();

            work_flow wf = new work_flow();
            Lcheks = wf.Deshabilitar_Checks();
            
            switch (Lcheks[0].Trim())
            {
                case "D":
                    tabControlSelectMano.SelectedTab = tabPage2;
                    ch_activarManoD.Visible = true;
                    ch_activarManoD.Checked = true;
                    ch_activarManoI.Checked=false;
                    ch_activarManoI.Visible = false;

                    p_derecha1.Visible = true;
                    p_izquierda2.Visible = true;
                    break;
                case "I":
                    tabControlSelectMano.SelectedTab = tabPage1;
                    ch_activarManoI.Visible = true;
                    ch_activarManoI.Checked = true;
                    ch_activarManoD.Checked=false;  
                    ch_activarManoD.Visible = false;

                    
                    p_izquierda1.Visible = true;
                    p_derecha2.Visible = true;
                    break;
                case "DI":
                   // ch_activarManoD.Checked = false;
                   
                    p_izquierda1.Visible = true;
                    p_izquierda2.Visible = false;
                    p_derecha1.Visible = true;
                    p_derecha2.Visible = false;
                    break;
                default:
                    break;
            }

            foreach (var item in Lcheks)
            {
                switch (item)
                {
                    case "DI1":
                       ci1.Checked = true;
                        break;   
                    case "DI2":
                        ci2.Checked = true;
                        break;
                    case "DI3":
                        ci3.Checked = true;
                        break ;
                    case "DI4":
                        ci4.Checked = true;
                        break;
                    case "DI5":
                        ci5.Checked = true;
                        break;
                    case "DD1":
                        cd1.Checked = true;
                        break;
                    case "DD2":
                        cd2.Checked = true;
                        break;
                    case "DD3":
                        cd3.Checked = true;
                        break;  
                    case "DD4":
                        cd4.Checked = true;
                        break;
                    case "DD5":
                        cd5.Checked = true;
                        break;
                    default: 
                        //cd1.Checked= true;
                        break;
                }
            }


        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            
            work_flow wf = new work_flow();
            if (ch_activarManoD.Checked)
            {
                PA_VALOR = "mano:D;";
            }
            else if (ch_activarManoI.Checked)
            {
                PA_VALOR = "mano:I;";
            }
            else
            {
                PA_VALOR = "mano:DI;";
            }

            //PA_VALOR = string.Concat(PA_VALOR, "dedos:");

            List<string> list = new List<string>();
   
            foreach (TabPage tabPage in tabControlSelectMano.TabPages)
            {   
                foreach (Control control in tabPage.Controls)
                {  
                    if (control is CheckBox checks && checks.Checked)
                    {                        
                        
                            switch (checks.Name)
                            {
                                case "ci1":
                                    list.Add("DI1");                                   
                                    break;
                                case "ci2":
                                    list.Add("DI2");
                                    break;
                                case "ci3":
                                    list.Add("DI3");
                                    break;
                                case "ci4":
                                    list.Add("DI4");
                                    break;
                                case "ci5":
                                    list.Add("DI5");
                                    break;
                                case "cd1":
                                    list.Add("DD1");
                                    break;
                                case "cd2":
                                    list.Add("DD2");
                                    break;
                                case "cd3":
                                    list.Add("DD3");
                                    break;
                                case "cd4":
                                    list.Add("DD4");    
                                    break;
                                case "cd5":
                                    list.Add("DD5");    
                                    break;
                                default:
                                    list.Add("DD1");
                                break;
                            }
                        
                        
                    }

                }

            }
            if (list.Count == 0 )
            {
                list.Add("DD2");
            }

            PA_VALOR = string.Concat(PA_VALOR,"dedos:", string.Join(",", list));
            try
            {
                if (wf.actualiza_mano(PA_VALOR))
                {
                    this.Close();
                }
                else {
                    MessageBox.Show("ERROR: No se pudo actulizar la mano", "ERROR");
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //label2.Text = tabControl1.SelectedTab.Text;

            if (tabControlSelectMano.SelectedIndex == 0)
            {
                ch_activarManoI.Visible = true;
                //ch_activarManoI.Checked = true;
                ch_activarManoD.Visible = false;
            }
            else if (tabControlSelectMano.SelectedIndex == 1)
            {
                ch_activarManoD.Visible = true;
                //ch_activarManoD.Checked = true;
                ch_activarManoI.Visible = false;
            }


        }

      

        private void ch_activarManoI_CheckedChanged(object sender, EventArgs e)
        {
            // ch_activarManoI.Visible = true;
            //ch_activarManoD.Checked = false;
            //ch_activarManoD.Visible = false;
        }

        private void ch_activarManoD_CheckedChanged(object sender, EventArgs e)
        {
            // ch_activarManoD.Visible = true;
            //ch_activarManoI.Checked = false;
            //ch_activarManoI.Visible = false;
        }

        private void ch_activarManoI_Click(object sender, EventArgs e)
        {
            ch_activarManoD.Checked = false;
            p_izquierda1.Visible = true; p_izquierda2.Visible = false; p_derecha2.Visible = true; p_derecha1.Visible = false;
            label2.Text = (!ch_activarManoI.Checked && !ch_activarManoD.Checked) ? "Ambas Manos" : tabControlSelectMano.SelectedTab.Text;
        }

        private void ch_activarManoD_Click(object sender, EventArgs e)
        {
            p_izquierda1.Visible = false; p_izquierda2.Visible = true; p_derecha2.Visible = false; p_derecha1.Visible = true;
            ch_activarManoI.Checked = false;
            label2.Text = (!ch_activarManoI.Checked && !ch_activarManoD.Checked) ? "Ambas Manos" : tabControlSelectMano.SelectedTab.Text;
        }
    }
}
