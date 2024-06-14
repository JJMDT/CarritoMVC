using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Venta
    {
        public int idVenta;
        public Cliente oCliente { get; set; }
        public int totalProducto { get; set; }
        public decimal montoTotal { get; set; }
        public string contacto { get; set; }
        public string idDistrito { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
        public string IdTransaccion { get; set; }

        public List<DetalleVenta> detalleVentaList { get; set; }
    }
}
