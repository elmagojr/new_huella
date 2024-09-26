using DDigital.Interfaz;
using DDigital.Utilidades;
using ProyectoDIGITALPERSONA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DDigital
{
    public partial class listado_huellas : Form
    {
        public Main_Menu _sender = new Main_Menu();
       
        DataTable data_huellas;
        work_flow wf;
        CREDENCIALES _CRED;
        UTILIDADES UT;
        bool usarDNI_;
        DATA_PERSONA dp_from_verificacion;
        List<HISTO_HUELLAS> idis;
        PERMISOS _permisos;
        public listado_huellas(bool usarDNI, DATA_PERSONA persona = null)
        {

            usarDNI_ = usarDNI;
            InitializeComponent();
            if (persona != null)
            {
                dp_from_verificacion = persona;
            }
         
        }

        private void button3_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("SE ELIMINARÁN DE LOS REGISTROS, ¿ESTÁ SEGURO QUE DESEA CONTINUAR?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                int borrados = 0;
                if (txt_eliminacion.Text != "")
                {      
                        BorrarRegistros();                      
                }
                else
                {
                    MessageBox.Show("Debe ingresar un motivo del por cual está eliminando la huella", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                if (borrados > 0)
                {
                    MessageBox.Show("HUELLAS ELIMINADAS", "ELIMINADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.Rows.Clear();
                    pictureBox1.Image = null;   
                    txt_eliminacion.Clear();
                    cargarData(txt_identidad.Text);
                }
            }
         
        }

        private void listado_huellas_Load(object sender, EventArgs e)
        {

            _permisos = _sender.PERMISSIONS_;
            _sender.AplicarPermisos(this, _permisos);
            _CRED = _sender.CRED_;
            if (usarDNI_)
            {
                cargarData(_CRED.identidad);
            }

            if (dp_from_verificacion!= null && !usarDNI_)
            {
                cargarData(dp_from_verificacion.IDENTIDAD);
            }
        }


        private void cargarData(string identidad)
        {
            wf = new work_flow();
            UT = new UTILIDADES();
            try
            {
                data_huellas = new DataTable();
                          
                data_huellas = wf.Lsitado_huellas(identidad);
                if (data_huellas.Rows.Count ==0)
                {
                    MessageBox.Show("No se encontro datos para esta persona", "NO ENCONTRADO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                btn_eliminar.Enabled = false;
                txt_eliminacion.Enabled = false;

                dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged;
                // dataGridView1.DataSource = data_huellas;
                //dataGridView1.Rows.Clear();
                //dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged;

                //dataGridView1.ClearSelection();
                dataGridView1.Rows.Clear();

                foreach (DataRow row in data_huellas.Rows)
                {
                    txt_identidad.Text = row[1].ToString();
                    txt_nombre.Text = row[0].ToString();
                    txt_codigo.Text = row[7].ToString();

                    int rowIndex = dataGridView1.Rows.Add();
                    dataGridView1[7, rowIndex].Value = row[1].ToString();

                    dataGridView1[0, rowIndex].Value = UT.identificaDedo(row[2].ToString()); //DEDO
                    dataGridView1[1, rowIndex].Value = row[3].ToString(); //OBSERVACION
                    dataGridView1[2, rowIndex].Value = row[4].ToString();//FECHA
                    dataGridView1[3, rowIndex].Value = row[8].ToString();//TIPO
                    dataGridView1[4, rowIndex].Value = row[9].ToString();//AGREGO
                    dataGridView1[5, rowIndex].Value = row[5];//IMAGEN
                    dataGridView1[6, rowIndex].Value = row[6].ToString(); //ID

                    dataGridView1.Columns[6].Visible = false; dataGridView1.Columns[7].Visible = false;
                    dataGridView1.ClearSelection();
                    dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
        
            cargarData(txt_identidad.Text);
        }

        public void BorrarRegistros()
        {
            wf = new work_flow();
            int borrado = 0;
            foreach (var item in idis)
            {
                item.HistoObservacion = txt_eliminacion.Text;
                borrado =    wf.EliminarHuella(item);
            }
            if (borrado>0)
            {
                MessageBox.Show("HUELLAS ELIMINADAS", "ELIMINADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.Rows.Clear();
                cargarData(txt_identidad.Text);
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //dataGridView1.ClearSelection();

            List<HISTO_HUELLAS> selectedID = new List<HISTO_HUELLAS>();
            idis = new List<HISTO_HUELLAS>();
            if (dataGridView1.SelectedRows.Count > 0)
            {

                btn_eliminar.Enabled = _permisos.EliminarHuellas;
                txt_eliminacion.Enabled =_permisos.EliminarHuellas;

                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    HISTO_HUELLAS BorraHuella = new HISTO_HUELLAS();
                    int rowIndex = row.Index;
                    BorraHuella.HistoId = data_huellas.Rows[rowIndex][6].ToString();
                    BorraHuella.HistoTabla = "HUELLAS_FIGERS";
                    BorraHuella.HistoCampo = "HUELLA";
                    BorraHuella.HistoVAnterior ="(NO_DATA)";
                    BorraHuella.HistoVActual = "(NO_DATA)";
                    BorraHuella.HistoUsrAccion = _CRED.usr_logged;
                    BorraHuella.HistoInfoAdicional ="";
                    BorraHuella.HistoObservacion = "";
                   

                    selectedID.Add(BorraHuella);

                }
                idis = selectedID;
            }

        }

        private void btn_salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {      
                    byte[] sample = row.Cells[5].Value as byte[];
                   
                    using (var ms = new MemoryStream(sample))
                    {
                        pictureBox1.Image = Image.FromStream(ms);
                    }
              
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
