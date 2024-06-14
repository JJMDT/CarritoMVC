using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class DetalleVenta
    {
        public int idDetalleVenta { get; set; }
        public int idVenta { get; set; }
        public Producto oProducto { get; set; }
        public int cantidad { get; set; }
        public decimal total { get; set; }
        public string idTransaccion { get; set; }

    }
}
