using MySql.Data.MySqlClient;
using ProyectoCS.Formularios.Actividades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoCS.Datos
{
    public class Login : DBContext
    {
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
    }
}
