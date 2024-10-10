namespace DDigital
{
    partial class Admin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Admin));
            this.tabControl_adminRol = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_guardar = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.btn_quitar = new System.Windows.Forms.Button();
            this.btn_asignar_rol = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.BTN_CAMBIAR_ACCESO = new System.Windows.Forms.Button();
            this.BT_CAMBIAR_ROL = new System.Windows.Forms.Button();
            this.lb_acceso_usuario = new System.Windows.Forms.Label();
            this.lb_rol_asig = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_busqueda = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_save_roll = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GB_PERMISO_DES = new System.Windows.Forms.GroupBox();
            this.lb_per_des = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_nombre_rol = new System.Windows.Forms.TextBox();
            this.btn_eliminar_rol = new System.Windows.Forms.Button();
            this.btn_editar_rol = new System.Windows.Forms.Button();
            this.btn_agregar_rol = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.groupbox_adminRol = new System.Windows.Forms.GroupBox();
            this.tabControl_adminRol.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.GB_PERMISO_DES.SuspendLayout();
            this.groupbox_adminRol.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl_adminRol
            // 
            this.tabControl_adminRol.Controls.Add(this.tabPage1);
            this.tabControl_adminRol.Controls.Add(this.tabPage2);
            this.tabControl_adminRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl_adminRol.Location = new System.Drawing.Point(9, 44);
            this.tabControl_adminRol.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabControl_adminRol.Name = "tabControl_adminRol";
            this.tabControl_adminRol.SelectedIndex = 0;
            this.tabControl_adminRol.Size = new System.Drawing.Size(627, 453);
            this.tabControl_adminRol.TabIndex = 0;
            this.tabControl_adminRol.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btn_cancel);
            this.tabPage1.Controls.Add(this.btn_guardar);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.listBox2);
            this.tabPage1.Controls.Add(this.btn_quitar);
            this.tabPage1.Controls.Add(this.btn_asignar_rol);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.txt_busqueda);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage1.Size = new System.Drawing.Size(619, 427);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Permisos";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(448, 389);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 35;
            this.btn_cancel.Text = "Cancelar";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_guardar
            // 
            this.btn_guardar.Enabled = false;
            this.btn_guardar.Location = new System.Drawing.Point(530, 389);
            this.btn_guardar.Name = "btn_guardar";
            this.btn_guardar.Size = new System.Drawing.Size(75, 23);
            this.btn_guardar.TabIndex = 34;
            this.btn_guardar.Text = "Guardar";
            this.btn_guardar.UseVisualStyleBackColor = true;
            this.btn_guardar.Click += new System.EventHandler(this.btn_guardar_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(370, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(155, 13);
            this.label8.TabIndex = 33;
            this.label8.Text = "Lista de Roles disponibles";
            // 
            // listBox2
            // 
            this.listBox2.Enabled = false;
            this.listBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox2.FormattingEnabled = true;
            this.listBox2.HorizontalScrollbar = true;
            this.listBox2.Location = new System.Drawing.Point(370, 50);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(230, 134);
            this.listBox2.TabIndex = 32;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // btn_quitar
            // 
            this.btn_quitar.Enabled = false;
            this.btn_quitar.Location = new System.Drawing.Point(514, 247);
            this.btn_quitar.Name = "btn_quitar";
            this.btn_quitar.Size = new System.Drawing.Size(91, 23);
            this.btn_quitar.TabIndex = 31;
            this.btn_quitar.Text = "Quitar Rol";
            this.btn_quitar.UseVisualStyleBackColor = true;
            this.btn_quitar.Click += new System.EventHandler(this.btn_quitar_Click);
            // 
            // btn_asignar_rol
            // 
            this.btn_asignar_rol.Enabled = false;
            this.btn_asignar_rol.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_asignar_rol.Location = new System.Drawing.Point(514, 218);
            this.btn_asignar_rol.Name = "btn_asignar_rol";
            this.btn_asignar_rol.Size = new System.Drawing.Size(91, 23);
            this.btn_asignar_rol.TabIndex = 30;
            this.btn_asignar_rol.Text = "Asignar Rol";
            this.btn_asignar_rol.UseVisualStyleBackColor = true;
            this.btn_asignar_rol.Click += new System.EventHandler(this.btn_asignar_rol_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.BTN_CAMBIAR_ACCESO);
            this.groupBox3.Controls.Add(this.BT_CAMBIAR_ROL);
            this.groupBox3.Controls.Add(this.lb_acceso_usuario);
            this.groupBox3.Controls.Add(this.lb_rol_asig);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Enabled = false;
            this.groupBox3.Location = new System.Drawing.Point(5, 312);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(359, 100);
            this.groupBox3.TabIndex = 29;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Valores Asignados";
            // 
            // BTN_CAMBIAR_ACCESO
            // 
            this.BTN_CAMBIAR_ACCESO.Location = new System.Drawing.Point(253, 54);
            this.BTN_CAMBIAR_ACCESO.Name = "BTN_CAMBIAR_ACCESO";
            this.BTN_CAMBIAR_ACCESO.Size = new System.Drawing.Size(75, 23);
            this.BTN_CAMBIAR_ACCESO.TabIndex = 7;
            this.BTN_CAMBIAR_ACCESO.Text = "Cambiar";
            this.BTN_CAMBIAR_ACCESO.UseVisualStyleBackColor = true;
            this.BTN_CAMBIAR_ACCESO.Click += new System.EventHandler(this.BTN_CAMBIAR_ACCESO_Click);
            // 
            // BT_CAMBIAR_ROL
            // 
            this.BT_CAMBIAR_ROL.Location = new System.Drawing.Point(253, 25);
            this.BT_CAMBIAR_ROL.Name = "BT_CAMBIAR_ROL";
            this.BT_CAMBIAR_ROL.Size = new System.Drawing.Size(75, 23);
            this.BT_CAMBIAR_ROL.TabIndex = 6;
            this.BT_CAMBIAR_ROL.Text = "Cambiar";
            this.BT_CAMBIAR_ROL.UseVisualStyleBackColor = true;
            this.BT_CAMBIAR_ROL.Click += new System.EventHandler(this.BT_CAMBIAR_ROL_Click);
            // 
            // lb_acceso_usuario
            // 
            this.lb_acceso_usuario.AutoSize = true;
            this.lb_acceso_usuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_acceso_usuario.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lb_acceso_usuario.Location = new System.Drawing.Point(102, 59);
            this.lb_acceso_usuario.Name = "lb_acceso_usuario";
            this.lb_acceso_usuario.Size = new System.Drawing.Size(145, 13);
            this.lb_acceso_usuario.TabIndex = 3;
            this.lb_acceso_usuario.Text = "Seleccione un usuario...";
            // 
            // lb_rol_asig
            // 
            this.lb_rol_asig.AutoSize = true;
            this.lb_rol_asig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_rol_asig.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lb_rol_asig.Location = new System.Drawing.Point(102, 30);
            this.lb_rol_asig.Name = "lb_rol_asig";
            this.lb_rol_asig.Size = new System.Drawing.Size(145, 13);
            this.lb_rol_asig.TabIndex = 2;
            this.lb_rol_asig.Text = "Seleccione un usuario...";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Acceso al Addon:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Rol Asignado:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(370, 207);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(137, 91);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Acceso al Add-on";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.Location = new System.Drawing.Point(24, 53);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(83, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Denegado";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.Location = new System.Drawing.Point(24, 29);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(77, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Permitido";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(2, 29);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "Filtro de Usuarios:";
            // 
            // txt_busqueda
            // 
            this.txt_busqueda.Location = new System.Drawing.Point(97, 26);
            this.txt_busqueda.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_busqueda.Name = "txt_busqueda";
            this.txt_busqueda.Size = new System.Drawing.Size(269, 19);
            this.txt_busqueda.TabIndex = 26;
            this.txt_busqueda.TextChanged += new System.EventHandler(this.txt_busqueda_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(5, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(151, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Lista de Usuarios Activos";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Thistle;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Location = new System.Drawing.Point(5, 50);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(359, 249);
            this.dataGridView1.TabIndex = 24;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupbox_adminRol);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.btn_save_roll);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.checkedListBox1);
            this.tabPage2.Controls.Add(this.listBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage2.Size = new System.Drawing.Size(619, 427);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Roles";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Brown;
            this.label10.Location = new System.Drawing.Point(5, 324);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(19, 13);
            this.label10.TabIndex = 35;
            this.label10.Text = "...";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(447, 388);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 34;
            this.button1.Text = "Cancelar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_save_roll
            // 
            this.btn_save_roll.Enabled = false;
            this.btn_save_roll.Location = new System.Drawing.Point(526, 388);
            this.btn_save_roll.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_save_roll.Name = "btn_save_roll";
            this.btn_save_roll.Size = new System.Drawing.Size(75, 23);
            this.btn_save_roll.TabIndex = 33;
            this.btn_save_roll.Text = "Guardar";
            this.btn_save_roll.UseVisualStyleBackColor = true;
            this.btn_save_roll.Click += new System.EventHandler(this.btn_save_roll_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.GB_PERMISO_DES);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_nombre_rol);
            this.groupBox1.Location = new System.Drawing.Point(5, 208);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(505, 104);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información Rol";
            // 
            // GB_PERMISO_DES
            // 
            this.GB_PERMISO_DES.Controls.Add(this.lb_per_des);
            this.GB_PERMISO_DES.Location = new System.Drawing.Point(249, 32);
            this.GB_PERMISO_DES.Name = "GB_PERMISO_DES";
            this.GB_PERMISO_DES.Size = new System.Drawing.Size(241, 56);
            this.GB_PERMISO_DES.TabIndex = 27;
            this.GB_PERMISO_DES.TabStop = false;
            this.GB_PERMISO_DES.Text = "Descripción Permiso";
            // 
            // lb_per_des
            // 
            this.lb_per_des.Location = new System.Drawing.Point(6, 16);
            this.lb_per_des.Name = "lb_per_des";
            this.lb_per_des.Size = new System.Drawing.Size(229, 54);
            this.lb_per_des.TabIndex = 0;
            this.lb_per_des.Text = "Seleccione un permiso para ver su descripción";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 34);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Nombre Rol";
            // 
            // txt_nombre_rol
            // 
            this.txt_nombre_rol.Location = new System.Drawing.Point(77, 32);
            this.txt_nombre_rol.Name = "txt_nombre_rol";
            this.txt_nombre_rol.ReadOnly = true;
            this.txt_nombre_rol.Size = new System.Drawing.Size(166, 19);
            this.txt_nombre_rol.TabIndex = 25;
            // 
            // btn_eliminar_rol
            // 
            this.btn_eliminar_rol.Enabled = false;
            this.btn_eliminar_rol.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_eliminar_rol.Location = new System.Drawing.Point(197, 17);
            this.btn_eliminar_rol.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_eliminar_rol.Name = "btn_eliminar_rol";
            this.btn_eliminar_rol.Size = new System.Drawing.Size(75, 23);
            this.btn_eliminar_rol.TabIndex = 31;
            this.btn_eliminar_rol.Text = "Eliminar Rol";
            this.btn_eliminar_rol.UseVisualStyleBackColor = true;
            this.btn_eliminar_rol.Click += new System.EventHandler(this.btn_eliminar_Click);
            // 
            // btn_editar_rol
            // 
            this.btn_editar_rol.Enabled = false;
            this.btn_editar_rol.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_editar_rol.Location = new System.Drawing.Point(101, 17);
            this.btn_editar_rol.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_editar_rol.Name = "btn_editar_rol";
            this.btn_editar_rol.Size = new System.Drawing.Size(75, 23);
            this.btn_editar_rol.TabIndex = 30;
            this.btn_editar_rol.Text = "Editar Rol";
            this.btn_editar_rol.UseVisualStyleBackColor = true;
            this.btn_editar_rol.Click += new System.EventHandler(this.btn_editar_Click);
            // 
            // btn_agregar_rol
            // 
            this.btn_agregar_rol.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_agregar_rol.Location = new System.Drawing.Point(5, 17);
            this.btn_agregar_rol.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_agregar_rol.Name = "btn_agregar_rol";
            this.btn_agregar_rol.Size = new System.Drawing.Size(75, 23);
            this.btn_agregar_rol.TabIndex = 29;
            this.btn_agregar_rol.Text = "Agregar Rol";
            this.btn_agregar_rol.UseVisualStyleBackColor = true;
            this.btn_agregar_rol.Click += new System.EventHandler(this.btn_agregar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(254, 12);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Listado de Permisos";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Listado de Roles";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.Enabled = false;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "Registrar Huellas",
            "Verificar Huellas",
            "Verifica Mancomunas",
            "Seleccionar Mano",
            "Seleccionar Dedos",
            "Ver Huellas",
            "Eliminar Huellas",
            "Admon Usuarios",
            "Quitar Acceso a Usuarios",
            "Admon Roles"});
            this.checkedListBox1.Location = new System.Drawing.Point(256, 31);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.ScrollAlwaysVisible = true;
            this.checkedListBox1.Size = new System.Drawing.Size(257, 144);
            this.checkedListBox1.TabIndex = 23;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.Location = new System.Drawing.Point(5, 31);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(230, 147);
            this.listBox1.TabIndex = 22;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(145, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(381, 20);
            this.label9.TabIndex = 1;
            this.label9.Text = "Administración de Usuarios para Huella Digital";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(542, 501);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Salir";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupbox_adminRol
            // 
            this.groupbox_adminRol.Controls.Add(this.btn_eliminar_rol);
            this.groupbox_adminRol.Controls.Add(this.btn_agregar_rol);
            this.groupbox_adminRol.Controls.Add(this.btn_editar_rol);
            this.groupbox_adminRol.Location = new System.Drawing.Point(23, 371);
            this.groupbox_adminRol.Name = "groupbox_adminRol";
            this.groupbox_adminRol.Size = new System.Drawing.Size(282, 51);
            this.groupbox_adminRol.TabIndex = 36;
            this.groupbox_adminRol.TabStop = false;
            // 
            // Admin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 531);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tabControl_adminRol);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Admin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Administración de Usuarios y Roles";
            this.Activated += new System.EventHandler(this.Admin_Activated);
            this.Load += new System.EventHandler(this.Admin_Load);
            this.tabControl_adminRol.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.GB_PERMISO_DES.ResumeLayout(false);
            this.groupbox_adminRol.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl_adminRol;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_eliminar_rol;
        private System.Windows.Forms.Button btn_editar_rol;
        private System.Windows.Forms.Button btn_agregar_rol;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_nombre_rol;
        private System.Windows.Forms.GroupBox GB_PERMISO_DES;
        private System.Windows.Forms.Label lb_per_des;
        private System.Windows.Forms.Button btn_save_roll;
        private System.Windows.Forms.Button btn_quitar;
        private System.Windows.Forms.Button btn_asignar_rol;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button BTN_CAMBIAR_ACCESO;
        private System.Windows.Forms.Button BT_CAMBIAR_ROL;
        private System.Windows.Forms.Label lb_acceso_usuario;
        private System.Windows.Forms.Label lb_rol_asig;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_busqueda;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_guardar;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupbox_adminRol;
    }
}