using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;
using System.IO;

namespace CapaPresentacionTienda.Controllers
{
    public class TiendaController : Controller
    {
        // GET: Tienda
        public ActionResult Index()
        {
            return View();
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


    }
}