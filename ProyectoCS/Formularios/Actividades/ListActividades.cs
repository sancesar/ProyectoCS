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
    public partial class ListActividades : Form
    {
        ConnectBDD Bd = new ConnectBDD();
        public ListActividades()
        {
            InitializeComponent();
            Bd.llenarlist2_1(LstvActividades);
        }
        //Regresamos al formulatio anterior
        private void LblRegr_Click(object sender, EventArgs e)
        {
            FrmActividades FrmAct = new FrmActividades();
            FrmAct.Show();
            this.Close();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            string Id = Txtid.Text;
            Bd.Buscar_Actividades(Id, LstvActividades);
        }
    }
}
