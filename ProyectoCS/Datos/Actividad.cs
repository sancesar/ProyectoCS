using MySql.Data.MySqlClient;
using ProyectoCS.Excepciones;
using ProyectoCS.Formularios.Actividades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoCS.Datos
{
    public class Actividad : DBContext
    {
        //llena el combo de representantes en actividades
        public void llenarCombo(ComboBox mycombo)
        {
            try
            {
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
                DataSet clientds = new DataSet();
                DataTable clientsTable = new DataTable();

                Command.Connection = connectionBD;
                Command.CommandText = "REPRESEN";
                Command.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand = Command;
                dataAdapter.Fill(clientds);
                connectionBD.Open();
                Reader = Command.ExecuteReader();
                clientsTable = clientds.Tables[0];

                try
                {

                    for (int i = 0; i < clientsTable.Rows.Count; i++)
                    {
                        DataRow rowClient = clientsTable.Rows[i];
                        mycombo.Items.Add(rowClient["Representante"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex);
                }

                Command.Parameters.Clear();
                Reader.Close();
                connectionBD.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(" " + ex);
            }
        }

        internal void Creacion_Actividad(string id, string nombre, string valor, string tipo, string dias, string representante, string hora, string cupos)
        {
            try
            {
                Command.Connection = connectionBD;
                Command.CommandText = "CREACTIVIDAD";
                Command.CommandType = CommandType.StoredProcedure;
                //Se manda los datos de la actividad que fueron ingresados a la base de datos para que puedan ser guardados
                Command.Parameters.AddWithValue("@id", id);
                Command.Parameters.AddWithValue("@nom", nombre);
                Command.Parameters.AddWithValue("@val", valor);
                Command.Parameters.AddWithValue("@tip", tipo);
                Command.Parameters.AddWithValue("@dias", dias);
                Command.Parameters.AddWithValue("@rep", representante);
                Command.Parameters.AddWithValue("@hor", hora);
                Command.Parameters.AddWithValue("@cup", cupos);
                connectionBD.Open();
                Command.ExecuteNonQuery();
                MessageBox.Show("Actividad Creada...");

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("" + ex.ToString());
            }
            finally
            {
                Command.Parameters.Clear();
                connectionBD.Close();
            }
        }
        //Buscamos la actividad
        internal void Buscar_Actividad(TextBox txtbusc, TextBox txtnomact, TextBox txtval, ComboBox cmbtip, TextBox txtdias, ComboBox cmbRepr, TextBox txthora, TextBox txtcup)
        {
            try
            {
                connectionBD.Open();
                string busc = txtbusc.Text;
                //Buscamos los datos de la actividad
                Command.Connection = connectionBD;
                Command.CommandText = "BUSCACTIVIDAD";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@busc", busc);
                reader = Command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //Se llenan los Txt para poder modificarlos
                        txtnomact.Text = reader.GetString(1);
                        txtval.Text = reader.GetString(2);
                        cmbtip.Text = reader.GetString(3);
                        cmbRepr.Text = reader.GetString(4);
                        txtdias.Text = reader.GetString(5);
                        txthora.Text = reader.GetString(6);
                        txtcup.Text = reader.GetString(7);
                    }
                }
                else
                {
                    throw new ExceptionActividad();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);

            }
            catch (ExceptionActividad)
            {
                txtbusc.Enabled = true;
            }
            finally
            {
                Command.Parameters.Clear();
                reader.Close();
                connectionBD.Close();
            }
        }

        internal void Modificar_Actividad(TextBox txtbusc, TextBox txtnomact, TextBox txtval, ComboBox cmbtip, TextBox txtdias, ComboBox cmbRepr, TextBox txthora, TextBox txtcup)
        {
            string busc = txtbusc.Text;
            string nom = txtnomact.Text;
            string val = txtval.Text;
            string tip = cmbtip.Text;
            string dias = txtdias.Text;
            string repre = cmbRepr.Text;
            string hora = txthora.Text;
            string cup = txtcup.Text;

            try
            {
                Command.Connection = connectionBD;
                Command.CommandText = "MODACTIVIDAD";
                Command.CommandType = CommandType.StoredProcedure;
                //Se manda los datos de la actividad que fueron ingresados a la base de datos para que puedan ser guardados
                Command.Parameters.AddWithValue("@bus", busc);
                Command.Parameters.AddWithValue("@nom", nom);
                Command.Parameters.AddWithValue("@val", val);
                Command.Parameters.AddWithValue("@tip", tip);
                Command.Parameters.AddWithValue("@dias", dias);
                Command.Parameters.AddWithValue("@rep", repre);
                Command.Parameters.AddWithValue("@hor", hora);
                Command.Parameters.AddWithValue("@cup", cup);
                connectionBD.Open();
                Command.ExecuteNonQuery();
                MessageBox.Show("Actividad Actualizada...");

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("" + ex.ToString());
            }
            finally
            {
                Command.Parameters.Clear();
                connectionBD.Close();
            }
        }
        //llena los horarios de las actividades
        internal void Llenar_horario(ListView lstvHorario)
        {
            connectionBD.Open();
            Command.Connection = connectionBD;
            //Llamamos al procedimiento almacenado
            Command.CommandText = "LLENARHORARIOS";
            Command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adap = new MySqlDataAdapter();
            adap.SelectCommand = Command;
            DataSet ds = new DataSet();
            DataTable tabla = new DataTable();
            adap.Fill(ds);
            Command.ExecuteNonQuery();
            tabla = ds.Tables[0];
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                DataRow filas = tabla.Rows[i];
                ListViewItem elementos = new ListViewItem(filas["Nombre"].ToString());
                elementos.SubItems.Add(filas["Dia"].ToString());
                elementos.SubItems.Add(filas["Cupo"].ToString());
                elementos.SubItems.Add(filas["Hora"].ToString());
                lstvHorario.Items.Add(elementos);
            }
            Command.Parameters.Clear();
            connectionBD.Close();
        }
        //Busca las actividades
        internal void Buscar_Actividades(string id, ListView lstvActividades)
        {
            //Si no busca una actividad en especifico
            if (string.IsNullOrEmpty(id))
            {
                llenarlist2_1(lstvActividades);
            }
            else
            {
                //Si busca una actividad en especifico
                Command.Connection = connectionBD;
                //Llamamos al procedimiento almacenado
                Command.CommandText = "TBUSCACTIVIDAD";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@bus", id);
                MySqlDataAdapter adap = new MySqlDataAdapter();
                adap.SelectCommand = Command;
                DataSet ds = new DataSet();
                DataTable tabla = new DataTable();
                adap.Fill(ds);
                connectionBD.Open();
                Command.ExecuteNonQuery();
                lstvActividades.Items.Clear();
                tabla = ds.Tables[0];
                for (int i = 0; i < tabla.Rows.Count; i++)
                {
                    llenarlist2(i, tabla, lstvActividades);
                }
                Command.Parameters.Clear();
                connectionBD.Close();
            }
        }

        private void llenarlist2(int i, DataTable tabla, ListView lstvActividades)
        {
            DataRow filas = tabla.Rows[i];
            ListViewItem elementos = new ListViewItem(filas["Id_Actividad"].ToString());
            elementos.SubItems.Add(filas["Nombre"].ToString());
            elementos.SubItems.Add(filas["Valor"].ToString());
            elementos.SubItems.Add(filas["Tipo"].ToString());
            elementos.SubItems.Add(filas["Representante"].ToString());
            lstvActividades.Items.Add(elementos);
        }

        internal void llenarlist2_1(ListView lstvActividades)
        {
            Command.Connection = connectionBD;
            Command.CommandText = "TBUSCACTIVIDAD2";
            Command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adap = new MySqlDataAdapter();
            adap.SelectCommand = Command;
            DataSet ds = new DataSet();
            DataTable tabla = new DataTable();
            adap.Fill(ds);
            connectionBD.Open();
            Command.ExecuteNonQuery();
            lstvActividades.Items.Clear();
            tabla = ds.Tables[0];
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                llenarlist2(i, tabla, lstvActividades);
            }
            Command.Parameters.Clear();
            connectionBD.Close();
        }

        internal void llenarComboActividad(ComboBox mycombo)
        {
            try
            {
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
                DataSet clientds = new DataSet();
                DataTable clientsTable = new DataTable();

                Command.Connection = connectionBD;
                Command.CommandText = "ACTIV";
                Command.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand = Command;
                dataAdapter.Fill(clientds);
                connectionBD.Open();
                Reader = Command.ExecuteReader();
                clientsTable = clientds.Tables[0];

                try
                {

                    for (int i = 0; i < clientsTable.Rows.Count; i++)
                    {
                        DataRow rowClient = clientsTable.Rows[i];
                        mycombo.Items.Add(rowClient["Nombre"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex);
                }

                Command.Parameters.Clear();
                Reader.Close();
                connectionBD.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(" " + ex);
            }
        }

        internal void llenarHorario(ComboBox cmbtip, TextBox txtdias, TextBox txthora)
        {
            try
            {
                connectionBD.Open();
                string busc = cmbtip.Text;
                //Buscamos los datos del recluso con la condicion de la cedula
                Command.Connection = connectionBD;
                Command.CommandText = "LLENHORA";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@bus", busc);
                reader = Command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //Se llenan los Txt para poder modificarlos
                        txtdias.Text = reader.GetString(0);
                        txthora.Text = reader.GetString(1);
                    }
                }
                else
                {
                    throw new ExceptionRecluso();


                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                Command.Parameters.Clear();
                reader.Close();
                connectionBD.Close();
            }
        }
        internal void Fuga(TextBox txtced, TextBox txtNom, TextBox txtApe, String fecha, TextBox txtMot)
        {
            string ced = txtced.Text;
            string nom = txtNom.Text;
            string ape = txtApe.Text;
            string mot = txtMot.Text;
            try
            {
                Command.Connection = connectionBD;
                Command.CommandText = "PROFUGA";
                Command.CommandType = CommandType.StoredProcedure;
                //Se manda los datos de la actividad que fueron ingresados a la base de datos para que puedan ser guardados
                Command.Parameters.AddWithValue("@ced", ced);
                Command.Parameters.AddWithValue("@nom", nom);
                Command.Parameters.AddWithValue("@ape", ape);
                Command.Parameters.AddWithValue("@mot", mot);
                Command.Parameters.AddWithValue("@dia", fecha);
                connectionBD.Open();
                Command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("" + ex.ToString());
            }
            finally
            {
                Command.Parameters.Clear();
                connectionBD.Close();
            }
        }

        internal void AgreRecAct(TextBox txtNom, TextBox txtApe, TextBox txtced, ComboBox cmbtip, TextBox txtdias, TextBox txthora)
        {
            string nom = txtNom.Text;
            string ape = txtApe.Text;
            string ced = txtced.Text;
            string tip = cmbtip.Text;
            string dias = txtdias.Text;
            string hora = txthora.Text;

            try
            {
                connectionBD.Close();
                Command.Connection = connectionBD;
                Command.CommandText = "AGRERECACT";
                Command.CommandType = CommandType.StoredProcedure;
                //Se manda los datos de la actividad y del recluso  que fueron ingresados a la base de datos para que puedan ser guardados
                Command.Parameters.AddWithValue("@nom", nom);
                Command.Parameters.AddWithValue("@ape", ape);
                Command.Parameters.AddWithValue("@ced", ced);
                Command.Parameters.AddWithValue("@tip", tip);
                Command.Parameters.AddWithValue("@dia", dias);
                Command.Parameters.AddWithValue("@hor", hora);
                connectionBD.Open();
                Command.ExecuteNonQuery();
                MessageBox.Show("Recluso agregado a la actividad...");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("" + ex.ToString());
            }
            finally
            {
                Command.Parameters.Clear();
                connectionBD.Close();
            }
        }

        internal void BuscCupos( ComboBox cmbtip)
        {
            int cup = 0;
            try
            {
                connectionBD.Open();
                string busc = cmbtip.Text;
                //Buscamos los datos del recluso con la condicion de la cedula
                Command.Connection = connectionBD;
                Command.CommandText = "TABACTIVIDAD";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@bus", busc);
                reader = Command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cup = Int32.Parse(reader.GetString(7));
                    }
                }
                else
                {
                    throw new ExceptionRecluso();


                }

                ActCupo(cup, busc);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                reader.Close();
                Command.Parameters.Clear();
            }
        }

        private void ActCupo(int cup, string busc)
        {
            cup = cup - 1;
            string Cup = cup.ToString();
            try
            {
                connectionBD.Close();
                connectionBD.Open();
                Command.Parameters.Clear();
                //Mandamamos los datos modificados a la base de datos para que se pueda actualizar con la condicion que le mandamos
                Command.Connection = connectionBD;
                Command.CommandText = "ACTCUPO";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@bus", busc);
                Command.Parameters.AddWithValue("@cup", Cup);
                Command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("" + ex.ToString());
            }
            finally
            {
                Command.Parameters.Clear();
                connectionBD.Close();
            }
        }

        internal void LLenarAct1(ListView listact)
        {
                //Buscamos los datos del recluso con la condicion de la cedula
                Command.Connection = connectionBD;
                Command.CommandText = "BUSCACTRECLU";
                Command.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter adap = new MySqlDataAdapter();
                adap.SelectCommand = Command;
                DataSet ds = new DataSet();
                DataTable tabla = new DataTable();
                adap.Fill(ds);
                connectionBD.Open();
                Command.ExecuteNonQuery();
                listact.Items.Clear();
                tabla = ds.Tables[0];
                for (int i = 0; i < tabla.Rows.Count; i++)
                {
                    llenartabla(i, tabla, listact);
                }
                Command.Parameters.Clear();
                connectionBD.Close();
        }

        private void llenartabla(int i, DataTable tabla, ListView listact)
        {
                DataRow filas = tabla.Rows[i];
                ListViewItem elementos = new ListViewItem(filas["Nombre"].ToString());
                elementos.SubItems.Add(filas["Apellido"].ToString());
                elementos.SubItems.Add(filas["Cedula"].ToString());
                elementos.SubItems.Add(filas["Tipo"].ToString());
            listact.Items.Add(elementos);
        }

        internal void LLenarAct2(ListView listact, string tipo)
        {
            //Si busca una actividad en especifico
            Command.Connection = connectionBD;
            //Llamamos al procedimiento almacenado
            Command.CommandText = "BUSCACTRECLU2";
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddWithValue("@busc", tipo);
            MySqlDataAdapter adap = new MySqlDataAdapter();
            adap.SelectCommand = Command;
            DataSet ds = new DataSet();
            DataTable tabla = new DataTable();
            adap.Fill(ds);
            connectionBD.Open();
            Command.ExecuteNonQuery();
            listact.Items.Clear();
            tabla = ds.Tables[0];
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                llenartabla(i, tabla, listact);
            }
            Command.Parameters.Clear();
            connectionBD.Close();
        }
    }
}
