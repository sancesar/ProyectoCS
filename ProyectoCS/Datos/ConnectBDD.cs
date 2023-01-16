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

namespace ProyectoCS.Datos
{
    public class ConnectBDD
    {
        MySqlConnection connectionBD = new MySqlConnection("server= btxxzyr0ildyylyibkf2-mysql.services.clever-cloud.com; " +
           "port= 3306; user id=uiw3felwq3nefzn6; password=byRQJsOZPoqRUBP6Gomr; database=btxxzyr0ildyylyibkf2;");
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
                MySqlCommand cmd = new MySqlCommand("SELECT Nombre_de_usu, Clave, Rol FROM btxxzyr0ildyylyibkf2.Usuarios WHERE Nombre_de_usu = @usu AND " +
                    "Clave = @cla", connectionBD);
                cmd.Parameters.AddWithValue("@usu", usu);
                cmd.Parameters.AddWithValue("@cla", cla);
                cmd.ExecuteNonQuery();

                reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    if (Convert.ToString(reader["Nombre_de_usu"]) == usu && Convert.ToString(reader["Clave"]) == cla)
                    {
                        MessageBox.Show("Inicio de sesión excitosa...", "Conectado");
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
        public void llenarCombo(ComboBox combo1)
        {
            MySqlCommand cm = new MySqlCommand("REPRESEN", connectionBD);
            cm.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter da = new MySqlDataAdapter(cm);
            DataTable dt = new DataTable();
            da.Fill(dt);
            combo1.DisplayMember = "Especialidad";
            combo1.DataSource = dt;
        }

        internal void DatosRecluso(string nombre, string apellido, string cedula, string fechaNac, string condena, string expediente)
        {
            try
            {
                //connectionBD.Close();
                connectionBD.Open();
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

        internal void DatosRepresentante(string nombre, string apellido, string cedula, string fechaNac, string especialidad)
        {

            try
            {
                //connectionBD.Close();
                connectionBD.Open();
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

        internal void BuscarEdiRecl(TextBox txtBusc,TextBox txtnom, TextBox txtape, TextBox txtfech, TextBox txtcond, TextBox txtexp)
        {
            try
            {
                connectionBD.Open();
                string busc = txtBusc.Text;
                MySqlCommand cmd = new MySqlCommand("SELECT Nombre, Apellido, Fecha_Nacimiento, Condena, N_Expediente FROM btxxzyr0ildyylyibkf2.Recluso WHERE Cedula = @busc ;", connectionBD);
                cmd.Parameters.AddWithValue("@busc", busc);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtnom.Text = reader.GetString(0);
                        txtape.Text = reader.GetString(1);
                        txtfech.Text = reader.GetString(2);
                        txtcond.Text = reader.GetString(3);
                        txtexp.Text = reader.GetString(4);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontro lote...");

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

        internal void BuscarUsuario(string usuario, string cedula, ListView listper)
        {
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
            else if(usuario == "Representante")
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
