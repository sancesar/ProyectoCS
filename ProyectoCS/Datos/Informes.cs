﻿using Microsoft.Reporting.WinForms;
using MySql.Data.MySqlClient;
using ProyectoCS.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace ProyectoCS.Datos
{
    public class Informes : DBContext
    {
        internal void Reporte(DataTable tabla, ReportViewer reporte, string datos)
        {
            reporte.LocalReport.DataSources.Clear();
            ReportDataSource report = new ReportDataSource(datos, tabla);
            reporte.LocalReport.DataSources.Add(report);
            reporte.RefreshReport();
        }

        public void ReportRepresentante(ReportViewer reportRepres)
        {
            string Reportes = "Representante";
            DataTable tabla = new DataTable();
            MySqlDataAdapter adap = new MySqlDataAdapter();
            Command.Connection = connectionBD;
            Command.CommandText = "REPORREPRESENTANTE";
            Command.CommandType = CommandType.StoredProcedure;
            connectionBD.Open();
            Command.ExecuteNonQuery();
            adap.SelectCommand = Command;
            adap.Fill(tabla);
            Reporte(tabla, reportRepres, Reportes);
            Command.Parameters.Clear();
            connectionBD.Close();
        }

        public void ReportRecluso(ReportViewer reportRecluso)
        {
            string Reportes = "Reclusos";
            DataTable tabla = new DataTable();
            MySqlDataAdapter adap = new MySqlDataAdapter();
            Command.Connection = connectionBD;
            Command.CommandText = "REPORRECLUSO";
            Command.CommandType = CommandType.StoredProcedure;
            connectionBD.Open();
            Command.ExecuteNonQuery();
            adap.SelectCommand = Command;
            adap.Fill(tabla);
            Reporte(tabla, reportRecluso, Reportes);
            Command.Parameters.Clear();
            connectionBD.Close();
        }

        public void ReportActividades(ReportViewer reportActiv)
        {
            string Reportes = "Actividades";
            DataTable tabla = new DataTable();
            MySqlDataAdapter adap = new MySqlDataAdapter();
            Command.Connection = connectionBD;
            Command.CommandText = "REPORACTIVIDADES";
            Command.CommandType = CommandType.StoredProcedure;
            connectionBD.Open();
            Command.ExecuteNonQuery();
            adap.SelectCommand = Command;
            adap.Fill(tabla);
            Reporte(tabla, reportActiv, Reportes);
            Command.Parameters.Clear();
            connectionBD.Close();
        }
    }
}
