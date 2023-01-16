using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using ProyectoCS.Control;
using static Org.BouncyCastle.Bcpg.Attr.ImageAttrib;


namespace PruebaLogin
{
    [TestClass]
    public class UnitTest1
    {
        Validaciones val = new Validaciones();

        [TestMethod]
        public void TestSesion()
        {
            //Arrange
            string usu = "Cesar";
            string cla = "123";
            bool result = false;
            
            //Act
            MySqlConnection connectionBD = new MySqlConnection("server= btxxzyr0ildyylyibkf2-mysql.services.clever-cloud.com; " +
           "port= 3306; user id=uiw3felwq3nefzn6; password=byRQJsOZPoqRUBP6Gomr; database=btxxzyr0ildyylyibkf2;");
            MySqlDataReader reader = null;
            try { 
            connectionBD.Open();
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
                    result = true;
                }
            }
            }
            catch (MySqlException)
            {
                result = false;
             }
          
            //Assert
            Assert.AreEqual(result, true);           
        }

        [TestMethod]
        public void TestBuscarRecluso()
        {
            //Arrange

            //Act

            //Assert

        }
    }
}