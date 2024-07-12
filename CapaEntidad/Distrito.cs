using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Distrito
    {
        public string idDistrito { get; set; }
        public string descripcion { get; set; }

        public string idProvincia { get; set; }
        public string idDepartamento { get; set; }
    }
}
