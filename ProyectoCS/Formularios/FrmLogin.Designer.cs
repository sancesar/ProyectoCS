namespace ProyectoCS
{
    partial class FrmLogin
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Txtusu = new System.Windows.Forms.TextBox();
            this.Txtcontr = new System.Windows.Forms.TextBox();
            this.Btnacep = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(49, 41);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(405, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sistema de Gestion de una carcel";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ProyectoCS.Properties.Resources.Captura_de_pantalla_2022_12_30_210510;
            this.pictureBox1.Location = new System.Drawing.Point(350, 89);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(444, 193);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Usuario:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(66, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Contraseña:";
            // 
            // Txtusu
            // 
            this.Txtusu.Location = new System.Drawing.Point(69, 121);
            this.Txtusu.Name = "Txtusu";
            this.Txtusu.Size = new System.Drawing.Size(218, 22);
            this.Txtusu.TabIndex = 4;
            // 
            // Txtcontr
            // 
            this.Txtcontr.Location = new System.Drawing.Point(69, 190);
            this.Txtcontr.Name = "Txtcontr";
            this.Txtcontr.PasswordChar = '*';
            this.Txtcontr.Size = new System.Drawing.Size(218, 22);
            this.Txtcontr.TabIndex = 5;
            // 
            // Btnacep
            // 
            this.Btnacep.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Btnacep.Location = new System.Drawing.Point(129, 233);
            this.Btnacep.Name = "Btnacep";
            this.Btnacep.Size = new System.Drawing.Size(158, 38);
            this.Btnacep.TabIndex = 6;
            this.Btnacep.Text = "Iniciar Sesión";
            this.Btnacep.UseVisualStyleBackColor = false;
            this.Btnacep.Click += new System.EventHandler(this.Btnacep_Click);
            // 
            // FrmLogin
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(836, 351);
            this.Controls.Add(this.Btnacep);
            this.Controls.Add(this.Txtcontr);
            this.Controls.Add(this.Txtusu);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmLogin_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Txtusu;
        private System.Windows.Forms.TextBox Txtcontr;
        private System.Windows.Forms.Button Btnacep;
    }
}

