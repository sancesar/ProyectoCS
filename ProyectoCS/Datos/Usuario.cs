using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoCS.Datos
{
    public class Usuario : DBContext
    {
        internal void BuscarUsuario(string usuario, string cedula, ListView listper)
        {
            //Buscamos la coincidencia que el usuario sea Recluso
            if (usuario == "Recluso")
            {
                //Muestra todos los reclusos
                if (cedula == "")
                {
                    Command.Connection = connectionBD;
                    Command.CommandText = "USURECLUSO";
                    Command.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter adap = new MySqlDataAdapter();
                    adap.SelectCommand = Command;
                    DataSet ds = new DataSet();
                    DataTable tabla = new DataTable();
                    adap.Fill(ds);
                    connectionBD.Open();
                    Reader = Command.ExecuteReader();
                    tabla = ds.Tables[0];
                    listper.Items.Clear();
                    for (int i = 0; i < tabla.Rows.Count; i++)
                    {
                        llenarlist(i, tabla, listper);
                    }
                    Command.Parameters.Clear();
                    Reader.Close();
                    connectionBD.Close();
                }
                else
                {
                    //Muestra al recluso buscado por cedula
                    Command.Connection = connectionBD;
                    Command.CommandText = "USUCEDRECLUSO";
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@ced", cedula);
                    MySqlDataAdapter adap = new MySqlDataAdapter();
                    adap.SelectCommand = Command;
                    DataSet ds = new DataSet();
                    DataTable tabla = new DataTable();
                    adap.Fill(ds);
                    connectionBD.Open();
                    Reader = Command.ExecuteReader();
                    tabla = ds.Tables[0];
                    listper.Items.Clear();
                    for (int i = 0; i < tabla.Rows.Count; i++)
                    {
                        llenarlist(i, tabla, listper);
                    }
                    Command.Parameters.Clear();
                    Reader.Close();
                    connectionBD.Close();
                }
            }
            //Buscamos la coincidencia que el usuario sea representante
            else if (usuario == "Representante")
            {
                //Muestra todos los representantes
                if (cedula == "")
                {

                    Command.Connection = connectionBD;
                    Command.CommandText = "USUREPRESENTANTE";
                    Command.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter adap = new MySqlDataAdapter();
                    adap.SelectCommand = Command;
                    DataSet ds = new DataSet();
                    DataTable tabla = new DataTable();
                    adap.Fill(ds);
                    connectionBD.Open();
                    Reader = Command.ExecuteReader();
                    tabla = ds.Tables[0];
                    listper.Items.Clear();
                    for (int i = 0; i < tabla.Rows.Count; i++)
                    {
                        llenarlist(i, tabla, listper);
                    }
                    Command.Parameters.Clear();
                    Reader.Close();
                    connectionBD.Close();
                }
                else
                {
                    //Muestra al representante buscado por cedula
                    Command.Connection = connectionBD;
                    Command.CommandText = "USUCEDREPRESENTANTE";
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@ced", cedula);
                    MySqlDataAdapter adap = new MySqlDataAdapter();
                    adap.SelectCommand = Command;
                    DataSet ds = new DataSet();
                    DataTable tabla = new DataTable();
                    adap.Fill(ds);
                    connectionBD.Open();
                    Reader = Command.ExecuteReader();
                    tabla = ds.Tables[0];
                    listper.Items.Clear();
                    for (int i = 0; i < tabla.Rows.Count; i++)
                    {
                        llenarlist(i, tabla, listper);
                    }
                    Command.Parameters.Clear();
                    Reader.Close();
                    connectionBD.Close();
                }
            }
        }
        //Metodo que permite llenar las listas con lo que se requiera
        private void llenarlist(int i, DataTable tabla, ListView listper)
        {
            DataRow filas = tabla.Rows[i];
            ListViewItem elementos = new ListViewItem(filas["Nombre"].ToString());
            elementos.SubItems.Add(filas["Apellido"].ToString());
            elementos.SubItems.Add(filas["Cedula"].ToString());
            elementos.SubItems.Add(filas["Fecha_Nacimiento"].ToString());
            listper.Items.Add(elementos);
        }
    }
}
