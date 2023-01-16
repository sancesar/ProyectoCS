using ProyectoCS.Datos;
using ProyectoCS.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoCS.Formularios.Usuarios
{
    public partial class RegisRecluso : Form
    {
        Validaciones val = new Validaciones();
        ConnectBDD Bd = new ConnectBDD();
        public RegisRecluso()
        {
            InitializeComponent();
        }
        //Regresamos al formulatio anterior
        private void LblRegr_Click(object sender, EventArgs e)
        {
            FrmUsuarios FrmUsu = new FrmUsuarios();
            FrmUsu.Show();
            this.Close();
        }


        //Guarda la información del formulario
        private void Btnguardar_Click(object sender, EventArgs e)
        {
            string Nombre = Txtnom.Text;
            string Apellido = Txtape.Text;
            string Cedula = Txtced.Text;
            string FechaNac = Dtpnac.Text;
            string Condena = Txtcond.Text;
            string Expediente = Txtexp.Text;
            if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Apellido) || string.IsNullOrEmpty(Cedula) || string.IsNullOrEmpty(Condena) || string.IsNullOrEmpty(Expediente))
            {
                MessageBox.Show("Por favor llenar todos los campos....", "Sistema");

            }
            else
            {
                Bd.DatosRecluso(Nombre, Apellido, Cedula, FechaNac, Condena, Expediente);

            }
            limpiar();
        }

        //Cancela la acción
        private void Btncancelar_Click(object sender, EventArgs e)
        {
            limpiar();
        }
        private void limpiar()
        {
            Txtnom.Clear();
            Txtape.Clear();
            Txtced.Clear();
            Txtcond.Clear();
            Txtexp.Clear();
        }
        //Solo acepta numeros
        private void Txtced_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.soloNumeros(e);
        }
        //Solo acepta letras
        private void Txtnom_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.soloTexto(e);
        }
    }
}
