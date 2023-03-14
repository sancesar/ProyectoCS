﻿using ProyectoCS.Datos;
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
    public partial class LisInscrip : Form
    {
        Actividad Act = new Actividad();
        public LisInscrip()
        {
            InitializeComponent();
            Act.LLenarAct1(Listact);
        }

        private void LblRegr_Click(object sender, EventArgs e)
        {
            FrmActividades FrmAct = new FrmActividades();
            FrmAct.Show();
            this.Close();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            String Tipo = Cmbtipo.Text;
            Act.LLenarAct2(Listact, Tipo);
        }

        private void Cmbtipo_Click(object sender, EventArgs e)
        {
            Cmbtipo.Items.Clear();
            Act.llenarComboActividad(Cmbtipo);
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Cmbtipo.Text = " ";
            Act.LLenarAct1(Listact);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            FrmActividades FrmAct = new FrmActividades();
            FrmAct.Show();
            this.Close();
        }
    }
}
