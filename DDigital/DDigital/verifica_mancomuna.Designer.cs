namespace DDigital.Utilidades
{
    partial class verifica_mancomuna
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(verifica_mancomuna));
            this.lbl_advertencia = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbl_count = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_nombre = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_identidad = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_aceptar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.pic_denied = new System.Windows.Forms.PictureBox();
            this.pic_huella = new System.Windows.Forms.PictureBox();
            this.pic_check = new System.Windows.Forms.PictureBox();
            this.LBL_ESTADO = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_denied)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_huella)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_check)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_advertencia
            // 
            this.lbl_advertencia.AutoSize = true;
            this.lbl_advertencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_advertencia.ForeColor = System.Drawing.Color.DarkCyan;
            this.lbl_advertencia.Location = new System.Drawing.Point(11, 43);
            this.lbl_advertencia.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_advertencia.Name = "lbl_advertencia";
            this.lbl_advertencia.Size = new System.Drawing.Size(486, 17);
            this.lbl_advertencia.TabIndex = 29;
            this.lbl_advertencia.Text = "Advertencia: Hay personas dentro de la lista que no tienen huella";
            this.lbl_advertencia.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 80);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 16);
            this.label3.TabIndex = 15;
            this.label3.Text = "Huellas";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lbl_count);
            this.groupBox2.Location = new System.Drawing.Point(629, 286);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(132, 103);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Por Verificar";
            // 
            // lbl_count
            // 
            this.lbl_count.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_count.Location = new System.Drawing.Point(8, 43);
            this.lbl_count.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_count.Name = "lbl_count";
            this.lbl_count.Size = new System.Drawing.Size(116, 28);
            this.lbl_count.TabIndex = 14;
            this.lbl_count.Text = "0";
            this.lbl_count.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(15, 100);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(399, 28);
            this.label7.TabIndex = 13;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txt_nombre);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txt_identidad);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(160, 266);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(431, 137);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Persona que puso la huella";
            // 
            // txt_nombre
            // 
            this.txt_nombre.Location = new System.Drawing.Point(81, 23);
            this.txt_nombre.Margin = new System.Windows.Forms.Padding(4);
            this.txt_nombre.Name = "txt_nombre";
            this.txt_nombre.ReadOnly = true;
            this.txt_nombre.Size = new System.Drawing.Size(331, 22);
            this.txt_nombre.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 63);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "Identidad:";
            // 
            // txt_identidad
            // 
            this.txt_identidad.Location = new System.Drawing.Point(81, 55);
            this.txt_identidad.Margin = new System.Windows.Forms.Padding(4);
            this.txt_identidad.Name = "txt_identidad";
            this.txt_identidad.ReadOnly = true;
            this.txt_identidad.Size = new System.Drawing.Size(331, 22);
            this.txt_identidad.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 31);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Nombre:";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(13, 421);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(146, 20);
            this.checkBox1.TabIndex = 28;
            this.checkBox1.Text = "Verificación Manual";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(605, 414);
            this.btn_cancel.Margin = new System.Windows.Forms.Padding(4);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(100, 28);
            this.btn_cancel.TabIndex = 24;
            this.btn_cancel.Text = "Cancelar";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_aceptar
            // 
            this.btn_aceptar.Enabled = false;
            this.btn_aceptar.Location = new System.Drawing.Point(713, 414);
            this.btn_aceptar.Margin = new System.Windows.Forms.Padding(4);
            this.btn_aceptar.Name = "btn_aceptar";
            this.btn_aceptar.Size = new System.Drawing.Size(100, 28);
            this.btn_aceptar.TabIndex = 23;
            this.btn_aceptar.Text = "Confirmar";
            this.btn_aceptar.UseVisualStyleBackColor = true;
            this.btn_aceptar.Click += new System.EventHandler(this.btn_aceptar_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(680, 165);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 17);
            this.label2.TabIndex = 21;
            this.label2.Text = "Verifique";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeight = 29;
            this.dataGridView1.Location = new System.Drawing.Point(13, 62);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(659, 197);
            this.dataGridView1.TabIndex = 19;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(620, 16);
            this.label1.TabIndex = 18;
            this.label1.Text = "La cuenta se trata de una Mancomunada, Debe Verificar la huella de todas las pers" +
    "onas a continuación";
            // 
            // pic_denied
            // 
            this.pic_denied.Image = global::DDigital.Properties.Resources.denied;
            this.pic_denied.Location = new System.Drawing.Point(681, 62);
            this.pic_denied.Margin = new System.Windows.Forms.Padding(4);
            this.pic_denied.Name = "pic_denied";
            this.pic_denied.Size = new System.Drawing.Size(104, 100);
            this.pic_denied.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_denied.TabIndex = 25;
            this.pic_denied.TabStop = false;
            this.pic_denied.Visible = false;
            // 
            // pic_huella
            // 
            this.pic_huella.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_huella.InitialImage = null;
            this.pic_huella.Location = new System.Drawing.Point(13, 266);
            this.pic_huella.Margin = new System.Windows.Forms.Padding(4);
            this.pic_huella.Name = "pic_huella";
            this.pic_huella.Size = new System.Drawing.Size(103, 99);
            this.pic_huella.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_huella.TabIndex = 22;
            this.pic_huella.TabStop = false;
            // 
            // pic_check
            // 
            this.pic_check.Image = global::DDigital.Properties.Resources.check;
            this.pic_check.Location = new System.Drawing.Point(680, 62);
            this.pic_check.Margin = new System.Windows.Forms.Padding(4);
            this.pic_check.Name = "pic_check";
            this.pic_check.Size = new System.Drawing.Size(104, 100);
            this.pic_check.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_check.TabIndex = 20;
            this.pic_check.TabStop = false;
            this.pic_check.Visible = false;
            // 
            // LBL_ESTADO
            // 
            this.LBL_ESTADO.AutoSize = true;
            this.LBL_ESTADO.Location = new System.Drawing.Point(179, 426);
            this.LBL_ESTADO.Name = "LBL_ESTADO";
            this.LBL_ESTADO.Size = new System.Drawing.Size(16, 16);
            this.LBL_ESTADO.TabIndex = 30;
            this.LBL_ESTADO.Text = "...";
            // 
            // verifica_mancomuna
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 454);
            this.Controls.Add(this.LBL_ESTADO);
            this.Controls.Add(this.lbl_advertencia);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.pic_denied);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_aceptar);
            this.Controls.Add(this.pic_huella);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pic_check);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "verifica_mancomuna";
            this.Text = "Verificación de cuenta Mancomunada";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.verifica_mancomuna_FormClosing);
            this.Load += new System.EventHandler(this.verifica_mancomuna_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_denied)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_huella)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_check)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_advertencia;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbl_count;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_nombre;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_identidad;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.PictureBox pic_denied;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_aceptar;
        private System.Windows.Forms.PictureBox pic_huella;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pic_check;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LBL_ESTADO;
    }
}