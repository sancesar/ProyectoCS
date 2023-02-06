using MySqlX.XDevAPI.Relational;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace ProyectoCS.Formularios.Actividades
{
    public partial class CreacionActividad : Form
    {
        ConnectBDD Bd = new ConnectBDD();
        public CreacionActividad()
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

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            string Id = txtidact.Text;
            string Nombre = Txtnomact.Text;
            string Valor = Txtval.Text;
            string Tipo = Cmbtip.Text;
            string Dias = Txtdias.Text;
            string Representante = CmbRepr.Text;
            string Hora = Txthora.Text;
            string Cupos = Txtcup.Text;
            if (string.IsNullOrEmpty(Id) || string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Valor) || string.IsNullOrEmpty(Tipo) || string.IsNullOrEmpty(Dias) || 
                string.IsNullOrEmpty(Representante) || string.IsNullOrEmpty(Hora) || string.IsNullOrEmpty(Cupos))
            {
                MessageBox.Show("Por favor llenar todos los campos....", "Sistema");

            }
            else
            {
                Bd.Creacion_Actividad(Id, Nombre, Valor, Tipo, Dias, Representante, Hora, Cupos);
                limpiar();

            }
            
        }
        private void limpiar()
        {
            txtidact.Clear();
            Txtnomact.Clear();
            Txtval.Clear();
            Txtdias.Clear();
            Txthora.Clear();
            Txtcup.Clear();
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
    }
}
