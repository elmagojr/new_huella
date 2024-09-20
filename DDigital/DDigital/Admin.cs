using DDigital.Interfaz;
using DDigital.Utilidades;
using DPUruNet;
using ProyectoDIGITALPERSONA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DDigital
{
    public partial class Admin : Form
    {
        public Main_Menu _sender = new Main_Menu();
        CREDENCIALES _CRED;
        work_flow wf;
        Security secu;
        DataTable data_usuarios;
        DataTable dt_ROLES = new DataTable();
        DataTable dt_rol_usr = new DataTable();
        ROLES rol_edit = new ROLES();
        ROLES rol_add = new ROLES();
        Token_Payload token_pay;
        PERMISOS _permisos;

        int contro_accion = 0;
        public Admin()
        {
            InitializeComponent();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            _permisos = _sender.PERMISSIONS_;
            _sender.AplicarPermisos(this, _permisos);

            token_pay = new Token_Payload();
            secu = new Security();
            _CRED = _sender.CRED_;
            wf = new work_flow();
            data_usuarios = new DataTable();
            CargarData();
        }
        private void FiltrarRegistros()
        {
            // Obtén el término de búsqueda desde el TextBox
            string terminoBusqueda = txt_busqueda.Text;

            // Aplica el filtro al DataTable
            data_usuarios.DefaultView.RowFilter = $"USUARIO LIKE '%{terminoBusqueda}%'";

            // Refresca el DataGridView para reflejar los cambios
            dataGridView1.Refresh();
        }
        private void CargarData()
        {
            data_usuarios = new DataTable();
            wf = new work_flow();
            data_usuarios = wf.listaUsuarios();

            dt_ROLES = wf.ListaRoles();

            dataGridView1.DataSource = data_usuarios;
            dataGridView1.ClearSelection();


            listBox1.Items.Clear();
            listBox2.Items.Clear();
            using (dt_ROLES)
            {
                foreach (DataRow rows in dt_ROLES.Rows)
                {
                    string itemLsit = $"{rows["nombre_rolhue"]}"; //- {rows["permisos_rolhue"]}<----permisos
                    listBox1.Items.Add(itemLsit);
                    listBox2.Items.Add(itemLsit);
                }
            }

        }

        private void txt_busqueda_TextChanged(object sender, EventArgs e)
        {
            FiltrarRegistros();
            dataGridView1.ClearSelection();
        }
        private void resetVentanaUsr()
        {
            btn_quitar.Enabled = false;
            btn_asignar_rol.Enabled = false;
            listBox2.Enabled = false;
            listBox2.ClearSelected();
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            btn_guardar.Enabled = false;
            //dataGridView1.ClearSelection();
            lb_rol_asig.Text = "Seleccione un usuario...";
            lb_acceso_usuario.Text = "Seleccione un usuario...";
        }
        private void resetVentanaRol()
        {
            button1.Visible = false;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
            checkedListBox1.Enabled = false;
            txt_nombre_rol.ReadOnly = true;
            txt_nombre_rol.Clear();
            listBox1.Enabled = true;
            listBox1.ClearSelected();
            btn_save_roll.Enabled = false;
            btn_eliminar.Enabled = false;
            btn_editar.Enabled = false;
        }
        private void ActivaxSelccion()
        {

        }

        private string Add_permisos(CheckedListBox cheked_list)
        {
            if (cheked_list.CheckedItems.Count == 0)
            {
                return string.Empty;
            }
            else
            {
                List<int> indices = new List<int>();
                for (int i = 0; i < cheked_list.Items.Count; i++)
                {
                    if (cheked_list.GetItemChecked(i))
                    {
                        indices.Add(i);
                    }
                }


                string cadena = string.Join(",", indices.Select(selector: index => $"{index}:true"));
                return cadena;
            }
        }
        private void GuardarRol()
        {
            try
            {
                secu = new Security();
                wf = new work_flow();


                Token_Payload TP = new Token_Payload();
                string CEDENA_PERMISOS = Add_permisos(checkedListBox1);
                if (string.IsNullOrEmpty(txt_nombre_rol.Text))
                {
                    MessageBox.Show("Debe asignar un nombre para este rol.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(CEDENA_PERMISOS))
                {
                    MessageBox.Show("Por favor, seleccione al menos una opción antes de continuar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    TP.ListaPermisos = CEDENA_PERMISOS;
                    TP.ROL = txt_nombre_rol.Text;

                };
                ROLES ROL = new ROLES
                {
                    NOMBRE_ROLHUE = txt_nombre_rol.Text,
                    PERMISOS_ROLHUE = secu.GenerarToken("createRol", TP),
                    USR_AGREGO_ROLHUE = _CRED.usr_logged
                };

                int rowsaffetted = wf.roles_Admin(ROL, 0);
                if (rowsaffetted > 0)
                {
                    MessageBox.Show($"Rol ha sido Creado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    resetVentanaRol();
                    CargarData();
                }
                else
                {
                    MessageBox.Show($"El nombre del rol ya existe", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception EX)
            {

                MessageBox.Show($"Algo Ocurrio {EX.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void EditarRol()
        {
            try
            {
                secu = new Security();
                wf = new work_flow();


                Token_Payload TP = new Token_Payload();
                string CEDENA_PERMISOS = Add_permisos(checkedListBox1);
                if (string.IsNullOrEmpty(txt_nombre_rol.Text))
                {
                    MessageBox.Show("Debe asignar un nombre para este rol.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(CEDENA_PERMISOS))
                {
                    MessageBox.Show("Por favor, seleccione al menos una opción antes de continuar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    TP.ListaPermisos = CEDENA_PERMISOS;
                    TP.ROL = txt_nombre_rol.Text;

                };
                ROLES ROL = new ROLES
                {
                    NOMBRE_ROLHUE = txt_nombre_rol.Text,
                    PERMISOS_ROLHUE = secu.GenerarToken("createRol", TP),
                    FECHA_MODI_ROLHUE = DateTime.Now,
                    USR_MODI_ROLHUE = _CRED.usr_logged,
                    ID_ROLHUE = rol_edit.ID_ROLHUE
                };

                int rowsaffetted = wf.roles_Admin(ROL, 1);
                if (rowsaffetted > 0)
                {
                    MessageBox.Show($"Rol ha sido actualizado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    resetVentanaRol();
                    CargarData();
                    // valores_inicales();
                    // showRoles();
                }
                else
                {
                    MessageBox.Show($"Rol no se ha sido actualizado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // valores_inicales();
                }

            }
            catch (Exception EX)
            {

                MessageBox.Show($"Algo Ocurrio {EX.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            resetVentanaUsr();
            if (e.RowIndex >= 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    token_pay.NOMBRE_USR = row.Cells[0].Value.ToString().Trim();
                    listBox2.ClearSelected();
                    btn_asignar_rol.Enabled = _permisos.AdmonUsuarios;
                    btn_quitar.Enabled = _permisos.AdmonUsuarios;

                    BTN_CAMBIAR_ACCESO.Enabled = _permisos.AdmonUsuarios ? _permisos.AdmonUsuarios : _permisos.QuitarAccesoUsr;
                    BT_CAMBIAR_ROL.Enabled = _permisos.AdmonUsuarios;

                    Token_Payload prove_payload = new Token_Payload();
                    ROLES prove_rol = verificar_acceso(token_pay.NOMBRE_USR, out prove_payload);
                    if (prove_payload != null && prove_rol != null)
                    {
                        rol_add = prove_rol;
                        token_pay = prove_payload;
                        token_pay.ROL = rol_add.ID_ROLHUE;

                    }
                }
            }
        }

        private void btn_asignar_rol_Click(object sender, EventArgs e)
        {
            listBox2.Enabled = true;
            contro_accion = 1;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {



            if (listBox2.SelectedItems != null && listBox2.SelectedIndex != -1)
            {
                foreach (DataRow rows in dt_ROLES.Rows)
                {
                    if (rows["nombre_rolhue"].ToString() == listBox2.SelectedItem.ToString())
                    {

                        rol_add.NOMBRE_ROLHUE = rows["nombre_rolhue"].ToString();
                        rol_add.ID_ROLHUE = rows["ID_ROLHUE"].ToString();
                        rol_add.USR_AGREGO_ROLHUE = _CRED.usr_logged;
                        string usuario = token_pay.NOMBRE_USR;
                        token_pay = secu.VerificarToken(rows["permisos_rolhue"].ToString());
                        token_pay.NOMBRE_USR = usuario;
                        token_pay.ROL = rol_add.ID_ROLHUE;
                        token_pay.ROL = rol_add.ID_ROLHUE;
                        if (token_pay != null)
                        {
                            rol_add.PERMISOS_ROLHUE = token_pay.ListaPermisos;
                        }
                    }
                }
                groupBox2.Enabled = _permisos.AdmonUsuarios ? _permisos.AdmonUsuarios : _permisos.QuitarAccesoUsr;
                btn_guardar.Enabled = true;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Token_Payload tp = new Token_Payload();
            ROLES rol = new ROLES();
            try
            {
                if (listBox1.SelectedItems != null && listBox1.SelectedIndex != -1)
                {
                    foreach (DataRow rows in dt_ROLES.Rows)
                    {
                        if (rows["nombre_rolhue"].ToString() == listBox1.SelectedItem.ToString())
                        {
                            rol.NOMBRE_ROLHUE = rows["nombre_rolhue"].ToString();
                            if (rol.NOMBRE_ROLHUE == _permisos.nombre_rol)
                            {
                                label10.Text = "Aviso: Ha seleccionado el mismo rol que su usuario tiene asignado. Precaución en caso de Editarlo.";
                            }
                            else
                            {
                                label10.Text = "...";
                            }
                            rol.ID_ROLHUE = rows["ID_ROLHUE"].ToString();
                            tp = secu.VerificarToken(rows["permisos_rolhue"].ToString());
                            if (tp != null)
                            {
                                rol.PERMISOS_ROLHUE = tp.ListaPermisos;
                                ActivarChecked(tp.ListaPermisos, checkedListBox1);
                            }

                            // permisos_ = secu.PERMISOS(rows["permisos_rolhue"].ToString(), out tp);
                        }
                    }

                    //permisos_ = secu.PERMISOS()
                    btn_editar.Enabled = true;
                    btn_eliminar.Enabled = true;
                    txt_nombre_rol.Text = listBox1.SelectedItem.ToString();
                    // txt_nombre_rol.ReadOnly=false;
                    // 
                    rol_edit = rol;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void ActivarChecked(string cadena, CheckedListBox cheklist)
        {
            Dictionary<int, bool> listachecked = secu.convertirCadenaDiccionario(cadena);//valor de la cadena desifrada del token
            for (int i = 0; i < cheklist.Items.Count; i++)
            {
                bool estado;
                if (listachecked.TryGetValue(i, out estado))
                {
                    cheklist.SetItemChecked(i, estado);
                }
                else
                {
                    cheklist.SetItemChecked(i, false);
                }
            }
        }
        private void btn_editar_Click(object sender, EventArgs e)
        {
            button1.Visible = true;
            checkedListBox1.Enabled = true;
            txt_nombre_rol.ReadOnly = false;
            btn_save_roll.Enabled = true;
            contro_accion = 2;
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex != -1)
            {
                lb_per_des.Text = Des_permiso(checkedListBox1.SelectedIndex);
                btn_save_roll.Enabled = true;

            }
        }
        private string Des_permiso(int elemento)
        {

            switch (elemento)
            {
                case 0:
                    return "[1] Permite el registro / enrolamiento de huellas digitales";
                case 1:
                    return "[2] Permite la verificación normal de una huella previamente registrada. ";
                case 2:
                    return "[3] Permite realizar la verificación de las huellas mancomunadas de una cuenta";
                case 3:
                    return "[4] Permite poder cambiar / seleccionnar la mano a utilizar en el proceso de enrolamiento";
                case 4:
                    return "[5] Permite seleccionar que dedos estaran habilitados en la mano seleccionada.";
                case 5:
                    return "[6] Permite visualizar el listado de huellas de una persona";
                case 6:
                    return "[7] Permite Permite eliminar huellas del listado de huellas de una persona";
                case 7:
                    return "[8] Permite el acceso a la ventana de Administración de usuarios del Add-on";
                case 8:
                    return "[9] Permite quitar o proveer acceso a un usuario al que se le ha asignado un rol";
                case 9:
                    return "[10] Permite acceso a la pestaña de administracion de roles (Agregar, Editar, Eliminar)";
                default:
                    return "";

            }

        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            button1.Visible = true;
            btn_editar.Enabled = false;
            btn_eliminar.Enabled = false;
            contro_accion = 1;
            checkedListBox1.Enabled = true;
            listBox1.Enabled = false;
            txt_nombre_rol.Clear();
            txt_nombre_rol.ReadOnly = false;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
        }

        private void btn_save_roll_Click(object sender, EventArgs e)
        {
            switch (contro_accion)
            {
                case 1: //guardar nuevo rol
                    GuardarRol();
                    break;
                case 2:
                    EditarRol();
                    break;
                case 3:
                    break;
                default:
                    break;
            }


        }

        private void btn_eliminar_Click(object sender, EventArgs e)
        {

            secu = new Security();
            wf = new work_flow();
            contro_accion = 3;

            DialogResult result = MessageBox.Show($" Eliminar este rol hará que los usuarios con este rol NO TENGAN ACCESO, ¿Está seguro de eliminarlo?", $"¿Desea Eliminar el rol {txt_nombre_rol.Text}", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                ODBC_CONN cn1 = new ODBC_CONN();
                int ROWSAFFETED = wf.roles_Admin(rol_edit, 2);
                if (ROWSAFFETED > 0)
                {
                    MessageBox.Show($"Rol eliminado", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    resetVentanaRol();
                    CargarData();
                }
                else
                {
                    MessageBox.Show($"Rol NO eliminado", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    resetVentanaRol();
                    CargarData();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            resetVentanaRol();
            CargarData();
        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            switch (contro_accion)
            {
                case 1: //guardar nuevo usuario
                    RegisitroUsuario();
                    break;
                case 2:
                    ActualizarUsuario();
                    break;
                case 3:
                    break;
                default:
                    break;
            }
        }
        private ROLES verificar_acceso(string usuario, out Token_Payload existente_rol)
        {
            Security sc = new Security();
            Token_Payload tp = new Token_Payload();
            ROLES rOLES = new ROLES();
            wf = new work_flow();
            DataTable data_Accceso = wf.informacion_acceso(usuario);

            if (data_Accceso.Rows.Count > 0)
            {


                string tokenId = data_Accceso.Rows[0].ItemArray[4].ToString();//token de usurario
                string token_permisos = data_Accceso.Rows[0].ItemArray[3].ToString();
                string NombreRol = data_Accceso.Rows[0].ItemArray[2].ToString();
                string Nombre_usuario = data_Accceso.Rows[0].ItemArray[1].ToString();
                string id_rol = data_Accceso.Rows[0].ItemArray[0].ToString();

                Token_Payload payload_acceso = secu.VerificarToken(tokenId);
                Token_Payload payload_permisos = secu.VerificarToken(token_permisos);

                if (id_rol != payload_acceso.ROL || Nombre_usuario != payload_acceso.NOMBRE_USR)
                {
                    MessageBox.Show("Atención, los datos de acceso no coinciden. ", "RESTRINGIDO", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    rOLES = null;
                    existente_rol = null;
                }
                else
                {
                    groupBox3.Enabled = true;
                    if (payload_acceso.EsPermitido == "SI")
                    {
                        radioButton1.Checked = true;
                    }
                    if (payload_acceso.EsPermitido == "NO")
                    {
                        radioButton2.Checked = true;
                    }
                    lb_rol_asig.Text = payload_permisos.ROL;
                    lb_acceso_usuario.Text = payload_acceso.EsPermitido == "SI" ? radioButton1.Text : radioButton2.Text;
                    btn_asignar_rol.Enabled = false;

                    tp.ROL = NombreRol;
                    tp.EsPermitido = payload_acceso.EsPermitido;
                    tp.ListaPermisos = payload_permisos.ListaPermisos;
                    tp.NOMBRE_USR = Nombre_usuario;

                    rOLES.NOMBRE_ROLHUE = NombreRol;
                    rOLES.ID_ROLHUE = id_rol;
                    rOLES.PERMISOS_ROLHUE = payload_permisos.ListaPermisos;

                    //for (int i = 0; i < listBox2.Items.Count; i++)
                    //{
                    //    if (listBox2.Items[i].ToString()==NombreRol)
                    //    {
                    //        listBox2.SetSelected(i,true);
                    //    }
                    //}
                    btn_quitar.Enabled = _permisos.AdmonUsuarios;
                    existente_rol = tp;
                }



            }
            else
            {
                btn_quitar.Enabled = false;
                existente_rol = null;
            }


            return rOLES;

        }
        private void RegisitroUsuario()
        {
            try
            {
                wf = new work_flow();

                if (!radioButton1.Checked && !radioButton2.Checked)
                {
                    token_pay.EsPermitido = "NO";
                }


                int afecctedRows = wf.registraAcceso(token_pay, rol_add, 3);

                if (afecctedRows > 0)
                {
                    MessageBox.Show($"Rol Asignado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //setearValores();
                    dataGridView1.ClearSelection();
                    resetVentanaUsr();
                    // lb_rol_asig.Text = txt_add_rol.Text;
                    btn_asignar_rol.Enabled = false;
                }
                else if (afecctedRows < 0)
                {
                    MessageBox.Show($"Este usuario ya tiene un rol asignado. Favor, quitar rol si desea reasignar otro rol a este usuario", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // resetVentanaUsr();
                }
                else
                {
                    MessageBox.Show($"No se pudo guardar el acceso a este usuario", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    resetVentanaUsr();
                }
            }
            catch (Exception EX)
            {

                MessageBox.Show($"Algo Ocurrio {EX.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ActualizarUsuario()
        {
            try
            {
                wf = new work_flow();

                if (!radioButton1.Checked && !radioButton2.Checked)
                {
                    token_pay.EsPermitido = "NO";
                }
                else
                {
                    token_pay.EsPermitido = radioButton1.Checked ? "SI" : "NO";
                }



                int afecctedRows = wf.registraAcceso(token_pay, rol_add, 4);

                if (afecctedRows > 0)
                {
                    MessageBox.Show($"Rol actualizado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //setearValores();
                    dataGridView1.ClearSelection();
                    resetVentanaUsr();
                    // lb_rol_asig.Text = txt_add_rol.Text;
                    btn_asignar_rol.Enabled = false;
                }
                else
                {
                    MessageBox.Show($"No se pudo guardar el acceso a este usuario", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    resetVentanaUsr();
                }
            }
            catch (Exception EX)
            {

                MessageBox.Show($"Algo Ocurrio {EX.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void QuitarRol(string nombre)
        {
            wf = new work_flow();

            DialogResult result = MessageBox.Show($"¿Desea revocarle el rol a este usuario: {nombre} ", $"Quitar rol", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {

                int rowsaffetted = wf.Quitar_Accso_rol(nombre);
                if (rowsaffetted > 0)
                {
                    MessageBox.Show($"Rol ha sido quitado a este usuario", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    resetVentanaUsr();
                }
                else
                {
                    MessageBox.Show($"Rol no ha sido quitado a este usuario", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    resetVentanaUsr();
                }
            }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            btn_agregar.Enabled = true;
            if (radioButton1.Checked && rol_add.ID_ROLHUE != "")
            {
                token_pay.EsPermitido = "SI";
            }

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            btn_agregar.Enabled = true;
            if (radioButton2.Checked && rol_add.ID_ROLHUE != "")
            {

                token_pay.EsPermitido = "NO";
            }
        }

        private void BT_CAMBIAR_ROL_Click(object sender, EventArgs e)
        {
            contro_accion = 2;
            listBox2.Enabled = true;
            btn_asignar_rol.Enabled = false;
        }

        private void BTN_CAMBIAR_ACCESO_Click(object sender, EventArgs e)
        {
            contro_accion = 2;
            groupBox2.Enabled = true;
            btn_asignar_rol.Enabled = false;
            btn_guardar.Enabled = true;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)//limpia ventana permisos
            {
                resetVentanaUsr();
            }
            else // limpia ventana roles
            {
                resetVentanaRol();
            }
            dataGridView1.ClearSelection();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {

            this.Close();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_quitar_Click(object sender, EventArgs e)
        {
            QuitarRol(token_pay.NOMBRE_USR);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_sender.primera_vez == 0 && _CRED.usr_logged.ToUpper() == "HID")
            {
                DialogResult result = MessageBox.Show("Primera vez: ¿Cerrar esta ventana de Administración?", "Primera Configuración", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Properties.Settings.Default.primerVez = 1;
                    Properties.Settings.Default.Save();
                    this.Close();
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }

                // Application.Exit();    
            }
            else
            {
                this.Close();
            }

        }

        private void Admin_Activated(object sender, EventArgs e)
        {
            UTILIDADES UT = new UTILIDADES();
            Security secu = new Security();
            _sender.PERMISSIONS_ = secu.ObtenerPermisos(_sender.CRED_.usr_logged);
            if (_sender.CRED_.usr_logged.ToUpper() == "HID")
            {
                _sender.PERMISSIONS_ = UT.permisosSuperUS;
            }
            if (_sender.PERMISSIONS_ != null)
            {
                _sender.AplicarPermisos(this, _sender.PERMISSIONS_);

            }
        }
    }
}
