
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
    internal class ConnectBDD2
    {
        public MySqlConnection conexion;

        public MySqlConnection Conexion
        {
            get { return conexion; }
            set { conexion = value; }
        }

        public ConnectBDD2()
        {
            Initialize();
        }

        public void Initialize()
        {
            string connectionString = null;
            connectionString = "server= btxxzyr0ildyylyibkf2-mysql.services.clever-cloud.com; " +
           "port= 3306; user id=uiw3felwq3nefzn6; password=byRQJsOZPoqRUBP6Gomr; database=btxxzyr0ildyylyibkf2;";
            conexion = new MySqlConnection(connectionString);
        }

        public Boolean login(string usuario, string clave)
        {

            string usu = usuario;
            string cla = clave;
            FrmActividades frmAct = new FrmActividades();
            FrmLogin frmLog = new FrmLogin();

            bool Conectar()
            {
                try
                {
                    conexion.Open();
                    return true;
                }
                catch (MySqlException ex)
                {
                    switch (ex.Number)
                    {
                        case 0:
                            Console.WriteLine("No se pudo conectar al servidor");
                            break;

                        case 1045:
                            Console.WriteLine("Usuario/Contraseña incorrecto");
                            break;
                    }
                    return false;
                }
            }

                    
            bool Desconectar()
            {
                try
                {
                    conexion.Close();
                    return true;
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            return false;
        }
    }
}