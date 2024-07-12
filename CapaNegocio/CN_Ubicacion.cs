using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Ubicacion
    {
        private CD_Ubicacion objCapaDato = new CD_Ubicacion();

        public List<Departamento> ObtenerDepartamento()
        {
            return objCapaDato.ObtenerDepartamento();
        }

        public List<Provincia> ObtenerProvincia(string iddep)
        {
            return objCapaDato.ObtenerProvincia(iddep);
        }

        public List<Distrito> ObtenerDistrito(string iddepartamento, string idprovincia)
        {
            return objCapaDato.ObtenerDistrito(iddepartamento, idprovincia);
        }

    }
}
