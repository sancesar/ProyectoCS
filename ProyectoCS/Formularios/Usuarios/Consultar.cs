﻿using Google.Protobuf.WellKnownTypes;
using ProyectoCS.Metodos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoCS.Formularios.Usuarios
{
    public partial class Consultar : Form
    {
        ConnectBDD Bd = new ConnectBDD();
        public Consultar()
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

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            string Usuario = Cmbusuario.Text;
            string Cedula = txtced.Text;

            Bd.BuscarUsuario(Usuario, Cedula, Listper);
        }
    }
}