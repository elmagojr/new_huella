namespace DDigital
{
    partial class Verificacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Verificacion));
            this.lbl_verifique = new System.Windows.Forms.Label();
            this.btn_lista_hue = new System.Windows.Forms.Button();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.txt_tipo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_observacion = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_dedo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_confirmar = new System.Windows.Forms.Button();
            this.v_pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txt_codigo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_principal = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_identidad = new System.Windows.Forms.TextBox();
            this.txt_nombre = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.v_pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_verifique
            // 
            this.lbl_verifique.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_verifique.Location = new System.Drawing.Point(20, 191);
            this.lbl_verifique.Name = "lbl_verifique";
            this.lbl_verifique.Size = new System.Drawing.Size(158, 34);
            this.lbl_verifique.TabIndex = 17;
            this.lbl_verifique.Text = "Verifique";
            this.lbl_verifique.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_lista_hue
            // 
            this.btn_lista_hue.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_lista_hue.Location = new System.Drawing.Point(16, 332);
            this.btn_lista_hue.Margin = new System.Windows.Forms.Padding(4);
            this.btn_lista_hue.Name = "btn_lista_hue";
            this.btn_lista_hue.Size = new System.Drawing.Size(172, 28);
            this.btn_lista_hue.TabIndex = 15;
            this.btn_lista_hue.Text = "Ver Todas las Huellas";
            this.btn_lista_hue.UseVisualStyleBackColor = true;
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Location = new System.Drawing.Point(487, 332);
            this.btn_cancelar.Margin = new System.Windows.Forms.Padding(4);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(100, 28);
            this.btn_cancelar.TabIndex = 14;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = true;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // txt_tipo
            // 
            this.txt_tipo.Location = new System.Drawing.Point(122, 174);
            this.txt_tipo.Margin = new System.Windows.Forms.Padding(4);
            this.txt_tipo.Name = "txt_tipo";
            this.txt_tipo.ReadOnly = true;
            this.txt_tipo.Size = new System.Drawing.Size(355, 22);
            this.txt_tipo.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 178);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 16);
            this.label8.TabIndex = 11;
            this.label8.Text = "Pertenece A:";
            // 
            // txt_observacion
            // 
            this.txt_observacion.Location = new System.Drawing.Point(123, 210);
            this.txt_observacion.Margin = new System.Windows.Forms.Padding(4);
            this.txt_observacion.Multiline = true;
            this.txt_observacion.Name = "txt_observacion";
            this.txt_observacion.ReadOnly = true;
            this.txt_observacion.Size = new System.Drawing.Size(355, 61);
            this.txt_observacion.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 214);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 16);
            this.label6.TabIndex = 9;
            this.label6.Text = "Observacón:";
            // 
            // txt_dedo
            // 
            this.txt_dedo.Location = new System.Drawing.Point(123, 124);
            this.txt_dedo.Margin = new System.Windows.Forms.Padding(4);
            this.txt_dedo.Name = "txt_dedo";
            this.txt_dedo.ReadOnly = true;
            this.txt_dedo.Size = new System.Drawing.Size(355, 22);
            this.txt_dedo.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 128);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "Dedo:";
            // 
            // btn_confirmar
            // 
            this.btn_confirmar.Enabled = false;
            this.btn_confirmar.Location = new System.Drawing.Point(595, 332);
            this.btn_confirmar.Margin = new System.Windows.Forms.Padding(4);
            this.btn_confirmar.Name = "btn_confirmar";
            this.btn_confirmar.Size = new System.Drawing.Size(100, 28);
            this.btn_confirmar.TabIndex = 13;
            this.btn_confirmar.Text = "Confirmar";
            this.btn_confirmar.UseVisualStyleBackColor = true;
            this.btn_confirmar.Click += new System.EventHandler(this.btn_confirmar_Click);
            // 
            // v_pictureBox1
            // 
            this.v_pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.v_pictureBox1.Location = new System.Drawing.Point(16, 45);
            this.v_pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.v_pictureBox1.Name = "v_pictureBox1";
            this.v_pictureBox1.Size = new System.Drawing.Size(162, 142);
            this.v_pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.v_pictureBox1.TabIndex = 12;
            this.v_pictureBox1.TabStop = false;
            // 
            // txt_codigo
            // 
            this.txt_codigo.Location = new System.Drawing.Point(122, 87);
            this.txt_codigo.Margin = new System.Windows.Forms.Padding(4);
            this.txt_codigo.Name = "txt_codigo";
            this.txt_codigo.ReadOnly = true;
            this.txt_codigo.Size = new System.Drawing.Size(355, 22);
            this.txt_codigo.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 91);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Código:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 59);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Identidad:";
            // 
            // lbl_principal
            // 
            this.lbl_principal.Font = new System.Drawing.Font("MS Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_principal.ForeColor = System.Drawing.Color.Black;
            this.lbl_principal.Location = new System.Drawing.Point(13, 9);
            this.lbl_principal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_principal.Name = "lbl_principal";
            this.lbl_principal.Size = new System.Drawing.Size(681, 30);
            this.lbl_principal.TabIndex = 16;
            this.lbl_principal.Text = "Debe poner el dedo en el lector de huellas";
            this.lbl_principal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_tipo);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txt_observacion);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txt_dedo);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txt_codigo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_identidad);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_nombre);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(200, 45);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(492, 279);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de la persona";
            // 
            // txt_identidad
            // 
            this.txt_identidad.Location = new System.Drawing.Point(122, 55);
            this.txt_identidad.Margin = new System.Windows.Forms.Padding(4);
            this.txt_identidad.Name = "txt_identidad";
            this.txt_identidad.ReadOnly = true;
            this.txt_identidad.Size = new System.Drawing.Size(355, 22);
            this.txt_identidad.TabIndex = 2;
            // 
            // txt_nombre
            // 
            this.txt_nombre.Location = new System.Drawing.Point(122, 23);
            this.txt_nombre.Margin = new System.Windows.Forms.Padding(4);
            this.txt_nombre.Name = "txt_nombre";
            this.txt_nombre.ReadOnly = true;
            this.txt_nombre.Size = new System.Drawing.Size(355, 22);
            this.txt_nombre.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nombre:";
            // 
            // Verificacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 370);
            this.Controls.Add(this.lbl_verifique);
            this.Controls.Add(this.btn_lista_hue);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.btn_confirmar);
            this.Controls.Add(this.v_pictureBox1);
            this.Controls.Add(this.lbl_principal);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Verificacion";
            this.Text = "Verificacion";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Verificacion_FormClosing);
            this.Load += new System.EventHandler(this.Verificacion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.v_pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_verifique;
        private System.Windows.Forms.Button btn_lista_hue;
        private System.Windows.Forms.Button btn_cancelar;
        public System.Windows.Forms.TextBox txt_tipo;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox txt_observacion;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox txt_dedo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_confirmar;
        private System.Windows.Forms.PictureBox v_pictureBox1;
        public System.Windows.Forms.TextBox txt_codigo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_principal;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.TextBox txt_identidad;
        public System.Windows.Forms.TextBox txt_nombre;
        private System.Windows.Forms.Label label1;
    }
}