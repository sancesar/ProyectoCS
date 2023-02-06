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
                //Mandamos esta linea a la aplicacion de MySQL para traer los datos que estamos requiriendo 
                MySqlCommand cmd = new MySqlCommand("SELECT Nombre_de_usu, Clave, Rol FROM btxxzyr0ildyylyibkf2.Usuarios WHERE Nombre_de_usu = @usu AND " +
                    "Clave = @cla", connectionBD);
                cmd.Parameters.AddWithValue("@usu", usu);
                cmd.Parameters.AddWithValue("@cla", cla);
                cmd.ExecuteNonQuery();
                //Verifica si con la coincidencia se encontro 
                reader = cmd.ExecuteReader();
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
                MySqlCommand cmd = new MySqlCommand("INSERT INTO `btxxzyr0ildyylyibkf2`.`Recluso` (`Nombre`, `Apellido`, `Cedula`, `Fecha_Nacimiento` , `Condena`, `N_Expediente`) " +
                    "VALUES ('" + nombre + "','" + apellido + "','" + cedula + "','" + fechaNac + "','" + condena + "','" + expediente + "');", connectionBD);
                cmd.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("" + ex.ToString());
            }finally
            {
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
                MySqlCommand cmd = new MySqlCommand("INSERT INTO `btxxzyr0ildyylyibkf2`.`Representante` (`Nombre`, `Apellido`, `Cedula`, `Fecha_Nacimiento`, `Especialidad` ) " +
                    "VALUES ('" + nombre + "','" + apellido + "','" + cedula + "','" + fechaNac + "','" + especialidad + "');", connectionBD);
                cmd.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("" + ex.ToString());
            }finally
            {
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
                MySqlCommand cmd = new MySqlCommand("SELECT Nombre, Apellido, Fecha_Nacimiento, Condena, N_Expediente FROM btxxzyr0ildyylyibkf2.Recluso WHERE Cedula = @busc ;", connectionBD);
                cmd.Parameters.AddWithValue("@busc", busc);
                reader = cmd.ExecuteReader();
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
                connectionBD.Close();
            }
        }
        //Permite actualizar/modificar los datos del recluso en la base de datos en la nube
        internal void ActulizarRecl(TextBox txtbusc, TextBox txtnom, TextBox txtape, TextBox txtfech, TextBox txtcond, TextBox txtexp)
        {
            string busc = txtbusc.Text;
            string nom = txtnom.Text;
            string ape = txtape.Text;
            string fech = txtfech.Text;
            string cond = txtcond.Text;
            string exp = txtexp.Text;

            try
            {
                //Mandamamos los datos modificados a la base de datos para que se pueda actualizar con la condicion que le mandamos
                MySqlCommand cmd = new MySqlCommand("UPDATE `btxxzyr0ildyylyibkf2`.`Recluso` SET `Nombre` = '" + nom + "', " +
                    "`Apellido` = '" + ape + "', `Fecha_Nacimiento` = '" + fech + "', `Condena` = '" + cond + "', `N_Expediente` = '" + exp +
                    "' WHERE `Cedula` = '" + busc+ "';", connectionBD);
                connectionBD.Open();
                cmd.ExecuteNonQuery();


                MessageBox.Show("Recluso Actualizado...");

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("" + ex.ToString());
            }
            finally
            {
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
                    MySqlCommand cmd = new MySqlCommand("SELECT Nombre, Apellido, Cedula, Fecha_Nacimiento FROM btxxzyr0ildyylyibkf2.Recluso;", connectionBD);
                    MySqlDataAdapter adap = new MySqlDataAdapter();
                    adap.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    DataTable tabla = new DataTable();
                    adap.Fill(ds);
                    tabla = ds.Tables[0];
                    listper.Items.Clear();
                    for (int i = 0; i < tabla.Rows.Count; i++)
                    {
                        llenarlist(i, tabla, listper);
                    }
                }
                else
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT Nombre, Apellido, Cedula, Fecha_Nacimiento FROM btxxzyr0ildyylyibkf2.Recluso WHERE Cedula= @ced;", connectionBD);
                    cmd.Parameters.AddWithValue("@ced", cedula);
                    MySqlDataAdapter adap = new MySqlDataAdapter();
                    adap.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    DataTable tabla = new DataTable();
                    adap.Fill(ds);
                    tabla = ds.Tables[0];
                    listper.Items.Clear();
                    for (int i = 0; i < tabla.Rows.Count; i++)
                    {
                        llenarlist(i, tabla, listper);
                    }
                }
            }
            //Buscamos la coincidencia que el usuario sea representante
            else if (usuario == "Representante")
            {
                if (cedula == "")
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT Nombre, Apellido, Cedula, Fecha_Nacimiento FROM btxxzyr0ildyylyibkf2.Representante;", connectionBD);
                    MySqlDataAdapter adap = new MySqlDataAdapter();
                    adap.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    DataTable tabla = new DataTable();
                    adap.Fill(ds);
                    tabla = ds.Tables[0];
                    listper.Items.Clear();
                    for (int i = 0; i < tabla.Rows.Count; i++)
                    {
                        llenarlist(i, tabla, listper);
                    }
                }
                else
                {                   
                    MySqlCommand cmd = new MySqlCommand("SELECT Nombre, Apellido, Cedula, Fecha_Nacimiento FROM btxxzyr0ildyylyibkf2.Representante WHERE Cedula= @ced;", connectionBD);
                    cmd.Parameters.AddWithValue("@ced", cedula);
                    MySqlDataAdapter adap = new MySqlDataAdapter();
                    adap.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    DataTable tabla = new DataTable();
                    adap.Fill(ds);
                    tabla = ds.Tables[0];
                    listper.Items.Clear();
                    for (int i = 0; i < tabla.Rows.Count; i++)
                    {
                        llenarlist(i, tabla, listper);
                    }
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
                //Buscamos los datos del recluso con la condicion de la cedula
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM btxxzyr0ildyylyibkf2.Representante WHERE Cedula = @busc ;", connectionBD);
                cmd.Parameters.AddWithValue("@busc", busc);
                reader = cmd.ExecuteReader();
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

                //Mandamamos los datos modificados a la base de datos para que se pueda actualizar con la condicion que le mandamos
                MySqlCommand cmd = new MySqlCommand("UPDATE `btxxzyr0ildyylyibkf2`.`Representante` SET `Nombre` = '" + nom + "', `Apellido` = '" + ape +
                   "', `Fecha_Nacimiento` = '" + fech + "', `Especialidad` = '" + esp + "' WHERE `Cedula` = '" + busc + "';", connectionBD);
                connectionBD.Open();
                cmd.ExecuteNonQuery();


                MessageBox.Show("Representante Actualizado...");

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("" + ex.ToString());
            }
            finally
            {
                connectionBD.Close();
            }
        }

        internal void Creacion_Actividad(string id, string nombre, string valor, string tipo, string dias, string representante, string hora, string cupos)
        {
            try
            {
                connectionBD.Open();
                //Se manda los datos del recluso que fueron ingresados a la base de datos para que puedan ser guardados
                MySqlCommand cmd = new MySqlCommand("INSERT INTO `btxxzyr0ildyylyibkf2`.`Actividad` (`Id_Actividad`, `Nombre`, `Valor`, `Tipo` , `Representante`, `Dia`, `Hora`, `Cupo`) " +
                    "VALUES ('" + id + "','" + nombre + "','" + valor + "','" + tipo + "','" + representante + "','" + dias + "','" + hora + "','" + cupos + "');", connectionBD);
                cmd.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("" + ex.ToString());
            }
            finally
            {
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
                //Buscamos los datos del recluso con la condicion de la cedula
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM btxxzyr0ildyylyibkf2.Actividad WHERE Id_Actividad = @busc ;", connectionBD);
                cmd.Parameters.AddWithValue("@busc", busc);
                reader = cmd.ExecuteReader();
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

                //Mandamamos los datos modificados a la base de datos para que se pueda actualizar con la condicion que le mandamos
                MySqlCommand cmd = new MySqlCommand("UPDATE `btxxzyr0ildyylyibkf2`.`Actividad` SET `Nombre` = '" + nom + "', `Valor` = '" + val +
                   "', `Tipo` = '" + tip + "', `Representante` = '" + repre + "', `Dia` = '" + dias + "'" +
                   ", `Hora` = '" + hora + "', `Cupo` = '" + cup + "' WHERE `Id_Actividad` = '" + busc + "';", connectionBD);
                connectionBD.Open();
                cmd.ExecuteNonQuery();


                MessageBox.Show("Actividad Actualizada...");

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("" + ex.ToString());
            }
            finally
            {
                connectionBD.Close();
            }
        }

        internal void Llenar_horario(ListView lstvHorario)
        {
            MySqlCommand cmd = new MySqlCommand("SELECT Nombre, Dia, Cupo, Hora FROM btxxzyr0ildyylyibkf2.Actividad;", connectionBD);
            MySqlDataAdapter adap = new MySqlDataAdapter();
            adap.SelectCommand = cmd;
            DataSet ds = new DataSet();
            DataTable tabla = new DataTable();
            adap.Fill(ds);
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
        }

        internal void Buscar_Actividades(string id, ListView lstvActividades)
        {
            if (string.IsNullOrEmpty(id))
            {
                llenarlist2_1(lstvActividades);
            }
            else { 
                MySqlCommand cmd = new MySqlCommand("SELECT Id_Actividad, Nombre, Valor, Tipo, Representante  FROM btxxzyr0ildyylyibkf2.Actividad WHERE Id_Actividad= @id;", connectionBD);
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataAdapter adap = new MySqlDataAdapter();
                adap.SelectCommand = cmd;
                DataSet ds = new DataSet();
                DataTable tabla = new DataTable();
                adap.Fill(ds);
                lstvActividades.Items.Clear();
                tabla = ds.Tables[0];
                for (int i = 0; i < tabla.Rows.Count; i++)
                {
                    llenarlist2(i, tabla, lstvActividades);
                }
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
            MySqlCommand cmd = new MySqlCommand("SELECT Id_Actividad, Nombre, Valor, Tipo, Representante  FROM btxxzyr0ildyylyibkf2.Actividad", connectionBD);
            MySqlDataAdapter adap = new MySqlDataAdapter();
            adap.SelectCommand = cmd;
            DataSet ds = new DataSet();
            DataTable tabla = new DataTable();
            adap.Fill(ds);
            lstvActividades.Items.Clear();
            tabla = ds.Tables[0];
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                llenarlist2(i, tabla, lstvActividades);
            }
        }
    }
}
