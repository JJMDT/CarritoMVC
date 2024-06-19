using CapaEntidad;
using CapaNegocio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;


namespace CapaPresentacionAdmin.Controllers
{
    public class MantenimientoController : Controller
    {
        // GET: Mantenimiento
        public ActionResult Categorias()
        {
            return View();
        }
        public ActionResult Marcas()
        {
            return View();
        }
        public ActionResult Productos()
        {
            return View();
        }
        //categoria
        #region CATEGORIA
        public JsonResult ListarCategorias()
        {
            List<Categoria> oLista = new List<Categoria>();
            oLista = new CN_Categoria().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]

        public JsonResult GuardarCategoria(Categoria objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.idCategoria == 0)
            {
                resultado = new CN_Categoria().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Categoria().Editar(objeto, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarCategoria(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Categoria().Eliminar(id, out mensaje);
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
#endregion
        // marcas
        #region MARCAS

        public JsonResult ListarMarcas()
        {
            List<Marca> oLista = new List<Marca>();
            oLista = new CN_Marcas().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]

        public JsonResult GuardarMarca(Marca objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.idMarca == 0)
            {
                resultado = new CN_Marcas().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Marcas().Editar(objeto, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarMarca(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Marcas().Eliminar(id, out mensaje);
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        // productos
        #region PRODUCTOS
        public JsonResult ListarProductos()
        {
            List<Producto> oLista = new List<Producto>();
            oLista = new CN_Productos().Listar();
            

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]

        public JsonResult GuardarProducto(string objeto,HttpPostedFileBase arcivoImagen)
        {
            object resultado;
            string mensaje = string.Empty;
            bool operacionExitosa = true;
            bool guardarImgExito=true;
           

            Producto oProducto=new Producto();
            oProducto=JsonConvert.DeserializeObject<Producto>(objeto);

            decimal precio;

            if (decimal.TryParse(oProducto.precioTexto,System.Globalization.NumberStyles.AllowDecimalPoint,new CultureInfo("es-AR"),out precio))
            {
                oProducto.precio = precio;
                
            }else
            {
                return Json(new { operacionExitosa = false, mensaje = "El formato del precio debe ser ##.##" },JsonRequestBehavior.AllowGet);
            }



            if (oProducto.idProducto == 0)
            {
                int idProductoGenerado = new CN_Productos().Registrar(oProducto, out mensaje);
                if(idProductoGenerado != 0)
                {
                    oProducto.idProducto = idProductoGenerado;
                }else
                {
                    operacionExitosa=false;
                }

            }
            else
            {
                operacionExitosa = new CN_Productos().Editar(oProducto, out mensaje);
            }

            if (operacionExitosa)
            {
                if(arcivoImagen != null)
                {
                    string rutaGuardar = ConfigurationManager.AppSettings["ServidorFotos"];
                    string extencion = Path.GetExtension(arcivoImagen.FileName);
                    string nombreImagen = string.Concat(oProducto.idProducto.ToString(), extencion);

                    try
                    {
                        arcivoImagen.SaveAs(Path.Combine(rutaGuardar,nombreImagen));
                    }
                    catch(Exception ex)
                    {
                        string msg = ex.Message;
                        guardarImgExito = false;
                    }
                    if (guardarImgExito)
                    {
                        oProducto.rutaImagen = rutaGuardar;
                        oProducto.nombreImagen= nombreImagen;
                        bool resp = new CN_Productos().GuardarDatosImagen(oProducto, out mensaje);
                    }
                    else
                    {
                        mensaje = "Se guardo el producto, pero huno problemas con la imagen";
                    }
                }
            }
            return Json(new { operacionExitosa = operacionExitosa, idGenerado = oProducto.idProducto ,mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        //img producto

        [HttpPost]
        public JsonResult ImagenProducto(int id)
        {
            bool conversion;
            Producto oProducto = new CN_Productos().Listar().Where(p => p.idProducto == id).FirstOrDefault();
            string textoBase64 = CN_Recursos.ConvertirBase64(Path.Combine(oProducto.rutaImagen, oProducto.nombreImagen), out conversion);

            return Json(new
            {
                conversion= conversion,
                textoBase64 = textoBase64,
                extension = Path.GetExtension(oProducto.nombreImagen),

            },
            JsonRequestBehavior.AllowGet);
        }


        //eliminar
        [HttpPost]
        public JsonResult EliminarProducto(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Productos().Eliminar(id, out mensaje);
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}