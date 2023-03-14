using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using ProyectoCS.Excepciones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoCS.Datos
{
    public class Recluso : DBContext
    {
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
            }
            finally
            {
                Command.Parameters.Clear();
                connectionBD.Close();
            }
        }

        //buscar recluso
        internal void BuscarEdiRecl(TextBox txtBusc, TextBox txtnom, TextBox txtape, TextBox txtfech, TextBox txtcond, TextBox txtexp)
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
                    throw new ExceptionRecluso();
                    
                    
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);

            }
            catch (ExceptionRecluso)
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

        internal void BuscarRec(TextBox txtced, TextBox txtNom, TextBox txtApe)
        {
            try
            {
                connectionBD.Open();
                string busc = txtced.Text;
                //Buscamos los datos del recluso con la condicion de la cedula
                Command.Connection = connectionBD;
                Command.CommandText = "RECCED";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@bus", busc);
                reader = Command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //Se llenan los Txt para poder modificarlos
                        txtNom.Text = reader.GetString(0);
                        txtApe.Text = reader.GetString(1);
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
            catch (ExceptionRecluso)
            {
                txtced.Enabled = true;
            }
            finally
            {
                Command.Parameters.Clear();
                reader.Close();
                connectionBD.Close();
            }
        }

        internal void Fuga(TextBox txtced, TextBox txtvalor)
        {
            int dia = 0;
            int valor = Int32.Parse(txtvalor.Text);
            try
            {
                connectionBD.Open();
                string busc = txtced.Text;
                //Buscamos los datos del recluso con la condicion de la cedula
                Command.Connection = connectionBD;
                Command.CommandText = "TabRecluso";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@bus", busc);
                reader = Command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dia = Int32.Parse(reader.GetString(4)); 
                    }
                }
                else
                {
                    throw new ExceptionRecluso();


                }

                ActFuga(dia, busc, valor);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);

            }
            catch (ExceptionRecluso)
            {
                txtced.Enabled = true;
            }
            finally
            {
                reader.Close();
                Command.Parameters.Clear();
            }
        }

        private void ActFuga(int dia, string busc, int valor)
        {
            dia += valor;
            string Dia = dia.ToString();
            try
            {
                connectionBD.Close();
                connectionBD.Open();
                Command.Parameters.Clear();
                //Mandamamos los datos modificados a la base de datos para que se pueda actualizar con la condicion que le mandamos
                Command.Connection = connectionBD;
                Command.CommandText = "ACTFUGA";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@bus", busc);
                Command.Parameters.AddWithValue("@dia", Dia);
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
    }
}
