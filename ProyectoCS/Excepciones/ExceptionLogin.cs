using Org.BouncyCastle.Crypto.Prng.Drbg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoCS.Excepciones
{
    public class ExceptionLogin : Exception
    {
        public ExceptionLogin()
        {
            MessageBox.Show("Usuario/Clave incorrectas....", "Sistema");
        }
    }
}
