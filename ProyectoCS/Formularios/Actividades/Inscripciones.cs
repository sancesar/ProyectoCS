using ProyectoCS.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoCS.Formularios.Actividades
{
    public partial class Inscripciones : Form
    {
        Actividad Act = new Actividad();
        Recluso Re = new Recluso();
        public Inscripciones()
        {
            InitializeComponent();
            TxtNom.Enabled = false;
            TxtApe.Enabled = false;
            Txtdias.Enabled = false;
            Txthora.Enabled = false;
            GrbPena.Visible = false;
        }

        private void LblRegr_Click(object sender, EventArgs e)
        {
            FrmActividades FrmAct = new FrmActividades();
            FrmAct.Show();
            this.Close();
        }

        private void Cmbtip_Click(object sender, EventArgs e)
        {
            Cmbtip.Items.Clear();
            Act.llenarComboActividad(Cmbtip);
        }

        private void Cmbtip_SelectedIndexChanged(object sender, EventArgs e)
        {
            Act.llenarHorario(Cmbtip, Txtdias, Txthora);
        }

        private void BtnFuga_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(this, "El recluso cometió alguna infracción?", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                Cmbtip.Text = "";
                Txtdias.Text = "";
                Txthora.Text = "";
                Cmbtip.Enabled = false;
                GrbPena.Visible = true;
            }
            else if(dr == DialogResult.No){
                Cmbtip.Enabled = true;
                Cmbtip.Text = "";
                GrbPena.Visible = false;
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (GrbPena.Visible == true)
            {
                String fecha = DateTime.Now.ToLongDateString();
                Act.Fuga(Txtced, TxtNom, TxtApe, fecha, TxtMot);
                Re.Fuga(Txtced, TxtValor);
            }
            else
            {
                Act.BuscCupos(Cmbtip);
                Act.AgreRecAct(TxtNom, TxtApe, Txtced, Cmbtip, Txtdias, Txthora);
            }
            Visualización();
        }

            private void Txtced_TextChanged(object sender, EventArgs e)
        {
            if (Txtced.Text.Length.ToString()== "10")
            {
                Re.BuscarRec(Txtced, TxtNom, TxtApe);
            }
            
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            FrmActividades FrmAct = new FrmActividades();
            FrmAct.Show();
            this.Close();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Visualización();
        }

        private void Visualización()
        {
            Cmbtip.Enabled = true;
            Cmbtip.Text = "";
            TxtNom.Text = "";
            TxtApe.Text = "";
            Txtced.Text = "";
            Txtdias.Text = "";
            Txthora.Text = "";
            TxtValor.Text = "";
            TxtMot.Text = "";
            GrbPena.Visible = false;
        }
    }
}
