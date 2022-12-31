using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoCS.Formularios.Actividades;
using System.Windows.Forms;

namespace ProyectoCS.Metodos
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
    }
}
