using MySql.Data.MySqlClient;
using ProyectoCS.Excepciones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoCS.Datos
{
    public class Representante : DBContext
    {
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
            }
            finally
            {
                Command.Parameters.Clear();
                connectionBD.Close();
            }
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
                    throw new ExceptionRepresentante();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);

            }
            catch (ExceptionRepresentante)
            {
                txtBusc.Enabled = true;
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
    }
}
