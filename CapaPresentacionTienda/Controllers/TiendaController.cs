using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;
using System.IO;
using System.Web.Mvc.Routing.Constraints;

namespace CapaPresentacionTienda.Controllers
{
    public class TiendaController : Controller
    {
        // GET: Tienda
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DetalleProducto(int idproducto = 0)
        {
            Producto oProducto = new Producto();
            bool conversion;

            oProducto = new CN_Productos().Listar().Where(p => p.idProducto == idproducto).FirstOrDefault();

            if (oProducto != null)
            {
                oProducto.base64 = CN_Recursos.ConvertirBase64(Path.Combine(oProducto.rutaImagen, oProducto.nombreImagen), out conversion);
                oProducto.extension = Path.GetExtension(oProducto.nombreImagen);
            }
            return View(oProducto);
        }



        [HttpGet]
        public JsonResult ListarCategorias()
        {
            List<Categoria> lista = new List<Categoria>();

            lista = new CN_Categoria().Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult ListarMarcaXCategoria(int idcategoria)
        {
            List<Marca> lista = new List<Marca>();
            lista = new CN_Marcas().ListarMarcaXCategoria(idcategoria);
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult ListarProductos(int idcategoria, int idmarca)
        {
            List<Producto> lista = new List<Producto>();
            bool conversion;

            lista = new CN_Productos().Listar().Select(p => new Producto()
            {
                idProducto = p.idProducto,
                nombre = p.nombre,
                descripcion = p.descripcion,
                oMarca = p.oMarca,
                oCategoria = p.oCategoria,
                precio = p.precio,
                stock = p.stock,
                rutaImagen = p.rutaImagen,
                base64 = CN_Recursos.ConvertirBase64(Path.Combine(p.rutaImagen, p.nombreImagen), out conversion),
                extension = Path.GetExtension(p.nombreImagen),
                activo = p.activo
            }).Where(p => p.oCategoria.idCategoria == (idcategoria == 0 ? p.oCategoria.idCategoria : idcategoria) && p.oMarca.idMarca == (idmarca == 0 ? p.oMarca.idMarca : idmarca) && p.stock > 0 && p.activo == true).ToList();

            var jsonresult = Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;


        }

        //agregar al carrito

        [HttpPost]
        public JsonResult AgregarCarrito(int idproducto)
        {
            int idcliente = ((Cliente)Session["Cliente"]).idCliente;
            bool existe = new CN_Carrito().ExisteCarrito(idcliente, idproducto);
            bool respuesta = false;
            string mensaje = string.Empty;

            if (existe)
            {
                mensaje = "El producto ya existe en el carrito";

            }
            else
            {
                respuesta = new CN_Carrito().OperacionCarrito(idcliente, idproducto, true, out mensaje);
            }
            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]

        public JsonResult CantidadEnCarrito()
        {
            int idcliente = ((Cliente)Session["Cliente"]).idCliente;
            int cantidad = new CN_Carrito().CantidadEnCarrito(idcliente);

            return Json(new { cantidad = cantidad }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult ListarProductosCarrito()
        {
            int idcliente = ((Cliente)Session["Cliente"]).idCliente;
            List<Carrito> oLista = new List<Carrito>();
            bool conversion;

            oLista = new CN_Carrito().ListarProducto(idcliente).Select(oc => new Carrito()
            {
                oProducto = new Producto()
                {
                    idProducto = oc.oProducto.idProducto,
                    nombre = oc.oProducto.nombre,
                    oMarca = oc.oProducto.oMarca,
                    precio = oc.oProducto.precio,
                    rutaImagen = oc.oProducto.rutaImagen,
                    base64 = CN_Recursos.ConvertirBase64(Path.Combine(oc.oProducto.rutaImagen, oc.oProducto.nombreImagen), out conversion),
                    extension = Path.GetExtension(oc.oProducto.nombreImagen),
                },
                cantidad = oc.cantidad
            }).ToList();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult OperacionCarrito(int idproducto, bool sumar)
        {
            int idcliente = ((Cliente)Session["Cliente"]).idCliente;
            bool respuesta = false;
            string mensaje = string.Empty;

            
            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarCarrito(int idproducto)
        {
            int idcliente = ((Cliente)Session["Cliente"]).idCliente;
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Carrito().EliminarCarrito(idcliente, idproducto);
            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult ObtenerProvincia()
        {
            List<Provincia> oLista = new List<Provincia>();

            oLista = new CN_Ubicacion().ObtenerProvincia();
            return Json(new { lista = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ObtenerLocalidad(string idprov)
        {
            List<Localidad> oLista = new List<Localidad>();

            oLista = new CN_Ubicacion().ObtenerLocalidad(idprov);
            return Json(new { lista = oLista }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Carrito() {

            return View();
        }

    }
}