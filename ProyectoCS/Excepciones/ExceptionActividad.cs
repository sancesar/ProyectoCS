using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoCS.Excepciones
{
    public class ExceptionActividad : Exception
        {
          public ExceptionActividad()
          {
                MessageBox.Show("No existe tal actividad", "Sistema");
          }
    }
}
