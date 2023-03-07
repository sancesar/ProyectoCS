using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoCS.Excepciones
{
    public class ExceptionRepresentante : Exception
    {
        public ExceptionRepresentante()
        {
            MessageBox.Show("No se encontro tal representante.", "Sistema");
        }
    }
}
