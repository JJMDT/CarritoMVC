using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Distrito
    {
        public int idDistrito { get; set; }
        public string descripcion { get; set; }

        public int idProvincia { get; set; }
        public int idDepartamento { get; set; }
    }
}
