namespace ProyectoCS.Formularios.Actividades
{
    partial class HorActividades
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
            this.label1 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.NomAct = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DiaAct = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CupAct = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HorAct = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LblRegr = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(201, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(241, 29);
            this.label1.TabIndex = 33;
            this.label1.Text = "Cursos Disponibles";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NomAct,
            this.DiaAct,
            this.CupAct,
            this.HorAct});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(24, 69);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(567, 301);
            this.listView1.TabIndex = 32;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // NomAct
            // 
            this.NomAct.Text = "Nombre";
            this.NomAct.Width = 200;
            // 
            // DiaAct
            // 
            this.DiaAct.Text = "Dias";
            this.DiaAct.Width = 200;
            // 
            // CupAct
            // 
            this.CupAct.Text = "Cupos";
            this.CupAct.Width = 100;
            // 
            // HorAct
            // 
            this.HorAct.Text = "Hora";
            // 
            // LblRegr
            // 
            this.LblRegr.AutoSize = true;
            this.LblRegr.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblRegr.Location = new System.Drawing.Point(535, 21);
            this.LblRegr.Name = "LblRegr";
            this.LblRegr.Size = new System.Drawing.Size(72, 16);
            this.LblRegr.TabIndex = 35;
            this.LblRegr.Text = "Regresar";
            this.LblRegr.Click += new System.EventHandler(this.LblRegr_Click);
            // 
            // HorActividades
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(640, 432);
            this.ControlBox = false;
            this.Controls.Add(this.LblRegr);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView1);
            this.Name = "HorActividades";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HorActividades";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader NomAct;
        private System.Windows.Forms.ColumnHeader DiaAct;
        private System.Windows.Forms.ColumnHeader CupAct;
        private System.Windows.Forms.ColumnHeader HorAct;
        private System.Windows.Forms.Label LblRegr;
    }
}