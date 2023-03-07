using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoCS.Excepciones
{
    public class ExceptionRecluso : Exception
    {
        public ExceptionRecluso()
        {
            MessageBox.Show("No se encontro tal recluso.", "Sistema");
        }
    }
}
