using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCS.Datos
{
    public class DBContext
    {
        //Conexion con mi base de datos
        protected MySqlConnection connectionBD = new MySqlConnection("server= btxxzyr0ildyylyibkf2-mysql.services.clever-cloud.com; " +
           "port= 3306; user id=uiw3felwq3nefzn6; password=byRQJsOZPoqRUBP6Gomr; database=btxxzyr0ildyylyibkf2;");
        //Reader
        protected MySqlDataReader Reader;
        protected MySqlDataReader reader = null;
        //Comandos
        protected MySqlCommand Command = new MySqlCommand();
    }
}
