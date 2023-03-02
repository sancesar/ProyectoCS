using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoCS.Formularios.Actividades;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.Xml.Linq;
using Microsoft.Reporting.WinForms;
using Microsoft.ReportingServices.Interfaces;
using Google.Protobuf.WellKnownTypes;

namespace ProyectoCS.Datos
{
    public class ConnectBDD
    {
        //Para la conceccion de la base de datos
        MySqlConnection connectionBD = new MySqlConnection("server= btxxzyr0ildyylyibkf2-mysql.services.clever-cloud.com; " +
           "port= 3306; user id=uiw3felwq3nefzn6; password=byRQJsOZPoqRUBP6Gomr; database=btxxzyr0ildyylyibkf2;");
        protected MySqlCommand Command = new MySqlCommand();
        protected MySqlDataReader Reader;
        MySqlDataReader reader = null;
        
        
        public Boolean login(string usuario, string clave)
        {

            string usu = usuario;
            string cla = clave;
            FrmActividades frmAct = new FrmActividades();
            FrmLogin frmLog = new FrmLogin();
            try
            {
                connectionBD.Close();
                connectionBD.Open();
                MySqlDataReader reader = null;
                Command.Connection = connectionBD;
                Command.CommandText = "INICIAR";
                Command.CommandType = CommandType.StoredProcedure;
                //Mandamos esta linea a la aplicacion de MySQL para traer los datos que estamos requiriendo 
                Command.Parameters.AddWithValue("@usu", usu);
                Command.Parameters.AddWithValue("@cla", cla);
                Command.ExecuteNonQuery();
                //Verifica si con la coincidencia se encontro 
                reader = Command.ExecuteReader();
                while (reader.Read())
                {

                    if (Convert.ToString(reader["Nombre_de_usu"]) == usu && Convert.ToString(reader["Clave"]) == cla)
                    {
                        MessageBox.Show("Inicio de sesión excitosa...", "Conectado");
                        //Se abre el formulario 
                        frmAct.Show();
                        return true;
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("" + ex.ToString());
                return false;
            }
            return false;
        }
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


            //agregar nuevo recluso
        internal void DatosRecluso(string nombre, string apellido, string cedula, string fechaNac, string condena, string expediente)
        {
            try
            {
                connectionBD.Open();
                //Se manda los datos del recluso que fueron ingresados a la base de datos para que puedan ser guardados
                Command.Connection = connectionBD;
                Command.CommandText = "DRECLUSO";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@nom", nombre);
                Command.Parameters.AddWithValue("@ape", apellido);
                Command.Parameters.AddWithValue("@ced", cedula);
                Command.Parameters.AddWithValue("@fec", fechaNac);
                Command.Parameters.AddWithValue("@con", condena);
                Command.Parameters.AddWithValue("@exp", expediente);
                Command.ExecuteNonQuery();
                MessageBox.Show("Recluso Registrado...");

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("" + ex.ToString());
            }finally
            {
                Command.Parameters.Clear();
                connectionBD.Close();
            }
}
        //Agregar representante
        internal void DatosRepresentante(string nombre, string apellido, string cedula, string fechaNac, string especialidad)
        {

            try
            {
                connectionBD.Open();
                //Se manda los datos del representante que fueron ingresados a la base de datos para que puedan ser guardados
                Command.Connection = connectionBD;
                Command.CommandText = "DREPRESENTANTE";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@nom", nombre);
                Command.Parameters.AddWithValue("@ape", apellido);
                Command.Parameters.AddWithValue("@ced", cedula);
                Command.Parameters.AddWithValue("@fec", fechaNac);
                Command.Parameters.AddWithValue("@esp", especialidad);
                Command.ExecuteNonQuery();
                MessageBox.Show("Representante Registrado...");

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("" + ex.ToString());
            }finally
            {
                Command.Parameters.Clear();
                connectionBD.Close();
            }
}
        //buscar recluso
        internal void BuscarEdiRecl(TextBox txtBusc,TextBox txtnom, TextBox txtape, TextBox txtfech, TextBox txtcond, TextBox txtexp)
        {
            try
            {
                connectionBD.Open();
                string busc = txtBusc.Text;
                //Buscamos los datos del recluso con la condicion de la cedula
                Command.Connection = connectionBD;
                Command.CommandText = "BUSCRECLUSO";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@busc", busc);
                reader = Command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //Se llenan los Txt para poder modificarlos
                        txtnom.Text = reader.GetString(0);
                        txtape.Text = reader.GetString(1);
                        txtfech.Text = reader.GetString(2);
                        txtcond.Text = reader.GetString(3);
                        txtexp.Text = reader.GetString(4);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontro recluso...");

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
        //Permite actualizar/modificar los datos del recluso en la base de datos en la nube
        internal void ActulizarRecl(TextBox txtbusc, TextBox txtnom, TextBox txtape, TextBox txtfech, TextBox txtcond, TextBox txtexp)
        {
            string bus = txtbusc.Text;
            string nom = txtnom.Text;
            string ape = txtape.Text;
            string fec = txtfech.Text;
            string con = txtcond.Text;
            string exp = txtexp.Text;

            try
            {
                //Mandamamos los datos modificados a la base de datos para que se pueda actualizar con la condicion que le mandamos
                Command.Connection = connectionBD;
                Command.CommandText = "ACTURECLUSO";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@bus", bus);
                Command.Parameters.AddWithValue("@nom", nom);
                Command.Parameters.AddWithValue("@ape", ape);
                Command.Parameters.AddWithValue("@fec", fec);
                Command.Parameters.AddWithValue("@con", con);
                Command.Parameters.AddWithValue("@exp", exp);
                connectionBD.Open();
                Command.ExecuteNonQuery();


                MessageBox.Show("Recluso Actualizado...");

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
        //Permite buscar a un usuario en la base de datos por medio del parametro cedula
        internal void BuscarUsuario(string usuario, string cedula, ListView listper)
        {
            //Buscamos la coincidencia que el usuario sea Recluso
            if (usuario == "Recluso")
            {
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

        internal void BuscarEdiRepr(TextBox txtBusc, TextBox txtnom, TextBox txtape, TextBox txtfech, TextBox txtesp)
        {
            try
            {
                connectionBD.Open();
                string busc = txtBusc.Text;
                Command.Connection = connectionBD;
                Command.CommandText = "BUSCREPRESENTANTE";
                Command.CommandType = CommandType.StoredProcedure;
                //Buscamos los datos del recluso con la condicion de la cedula
                Command.Parameters.AddWithValue("@busc", busc);
                reader = Command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //Se llenan los Txt para poder modificarlos
                        txtnom.Text = reader.GetString(0);
                        txtape.Text = reader.GetString(1);
                        txtfech.Text = reader.GetString(3);
                        txtesp.Text = reader.GetString(4);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontro representante ...");

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

        internal void ActulizarRepr(TextBox txtBusc, TextBox txtnom, TextBox txtape, TextBox txtfech, TextBox txtesp)
        {
            string busc = txtBusc.Text;
            string nom = txtnom.Text;
            string ape = txtape.Text;
            string fech = txtfech.Text;
            string esp = txtesp.Text;

            try
            {
                Command.Connection = connectionBD;
                Command.CommandText = "ACTUREPRESENTANTE";
                Command.CommandType = CommandType.StoredProcedure;
                //Mandamamos los datos modificados a la base de datos para que se pueda actualizar con la condicion que le mandamos
                Command.Parameters.AddWithValue("@bus", busc);
                Command.Parameters.AddWithValue("@nom", nom);
                Command.Parameters.AddWithValue("@ape", ape);
                Command.Parameters.AddWithValue("@fech", fech);
                Command.Parameters.AddWithValue("@esp", esp);
                connectionBD.Open();
                Command.ExecuteNonQuery();


                MessageBox.Show("Representante Actualizado...");

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
                    MessageBox.Show("No se encontro Actividad ...");

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
                //Se manda los datos del recluso que fueron ingresados a la base de datos para que puedan ser guardados
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

        internal void Llenar_horario(ListView lstvHorario)
        {
            connectionBD.Open();
            Command.Connection = connectionBD;
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

        internal void Buscar_Actividades(string id, ListView lstvActividades)
        {
            if (string.IsNullOrEmpty(id))
            {
                llenarlist2_1(lstvActividades);
            }
            else {
                Command.Connection = connectionBD;
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
        private void Reporte(DataTable tabla, ReportViewer reporte, string datos)
        {
            reporte.LocalReport.DataSources.Clear();
            ReportDataSource report = new ReportDataSource(datos, tabla);
            reporte.LocalReport.DataSources.Add(report);
            reporte.RefreshReport();
        }

        internal void ReportRepresentante(ReportViewer reportRepres)
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
    }
}
