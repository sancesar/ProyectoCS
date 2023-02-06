using Google.Protobuf.WellKnownTypes;
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
    public partial class ModificarActividad : Form
    {
        ConnectBDD Bd = new ConnectBDD();
        public ModificarActividad()
        {
            InitializeComponent();
        }
        //Regresamos al formulatio anterior
        private void LblRegr_Click(object sender, EventArgs e)
        {
            FrmActividades FrmAct = new FrmActividades();
            FrmAct.Show();
            this.Close();
        }

        private void CmbRepr_Click(object sender, EventArgs e)
        {
            CmbRepr.Items.Clear();
            Bd.llenarCombo(CmbRepr);
        }
        private void limpiar()
        {
            Txtbusc.Clear();
            Txtnomact.Clear();
            Txtval.Clear();
            Txtdias.Clear();
            Txthora.Clear();
            Txtcup.Clear();
            Txtbusc.Enabled = true;
            CmbRepr.SelectedIndex = -1;
            Cmbtip.SelectedIndex = -1;
        }

        private void Cmbtip_Click(object sender, EventArgs e)
        {
            Cmbtip.Items.Clear();
            string[] items = { "Manualidades", "Agricultura", "Deportivo", "Cocinero", "Conserje", "Lavanderia", "Taller de escritura", "Clases de teatro" };
            Cmbtip.Items.AddRange(items);
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            Txtbusc.Enabled = false;
            Bd.Buscar_Actividad(Txtbusc, Txtnomact, Txtval, Cmbtip, Txtdias, CmbRepr, Txthora, Txtcup);
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            Bd.Modificar_Actividad(Txtbusc, Txtnomact, Txtval, Cmbtip, Txtdias, CmbRepr, Txthora, Txtcup);
            limpiar();
        }
    }
}
