using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CapaPresentacionTienda.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registrar()
        {
            return View();
        }
        public ActionResult Reestablecer()
        {
            return View();
        }

        public ActionResult CambiarPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Cliente objeto)
        {
            int resultado;
            string mensaje = string.Empty;
            ViewData["nombre"] = string.IsNullOrEmpty(objeto.nombre) ? "" : objeto.nombre;
            ViewData["apellido"] = string.IsNullOrEmpty(objeto.apellido) ? "" : objeto.apellido;
            ViewData["email"] = string.IsNullOrEmpty(objeto.email) ? "" : objeto.email;

            if (objeto.password != objeto.confirmarPassword)
            {
                ViewBag.Error = "Los passwords no coinciden";
                return View();
            }
            resultado = new CN_Cliente().Registrar(objeto, out mensaje);
            if (resultado > 0)
            {
                ViewBag.Error = null;
                return RedirectToAction("Index", "Acceso");
            }
            else
            {
                ViewBag.Error = mensaje;
                return View();

            }
        }

        [HttpPost]


        public ActionResult Index(string email, string password)
        {
            Cliente oCliente = null;

            // comprobando 
            var clientes = new CN_Cliente().Listar();
            Console.WriteLine("Total de clientes: " + clientes.Count);
            var passwordEncriptado = CN_Recursos.ConvertirSha256(password);
            Console.WriteLine("Email recibido: " + email);
            Console.WriteLine("Password encriptado: " + passwordEncriptado);

            // sigue codigo

            oCliente = new CN_Cliente().Listar().Where(item => item.email == email && item.password == CN_Recursos.ConvertirSha256(password)).FirstOrDefault();


            if (oCliente == null)
            {
                ViewBag.Error = "Email o Password invalidos";
                return View();
            }
            else
            {
                if (oCliente.reestablecer)
                {
                    TempData["idCliente"] = oCliente.idCliente;
                    return RedirectToAction("CambiarPassword", "Acceso");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(oCliente.email, false);
                    Session["Cliente"] = oCliente;
                    ViewBag.Error = null;
                    return RedirectToAction("Index", "Tienda");
                }
            }

        }
        //restablecer clave
        [HttpPost]
        public ActionResult Reestablecer(string email)
        {
            Cliente cliente = new Cliente();
            cliente = new CN_Cliente().Listar().Where(item => item.email == email).FirstOrDefault();

            if (cliente == null)
            {
                ViewBag.Error = "No se encotnro un Cliente con ese email";
                return View();
            }

            string mensaje = string.Empty;
            bool respuesta = new CN_Cliente().ReestablecerPassword(cliente.idCliente, email, out mensaje);
            if (respuesta)
            {
                ViewBag.Error = null;
                return RedirectToAction("Index", "Acceso");
            }
            else
            {
                ViewBag.Error = mensaje;
                return View();
            }
        }

        //cambiar password
        [HttpPost]
        public ActionResult CambiarPassword(string idCliente, string password, string newPassword, string confirmarPassword)
        {
            Cliente oCliente = new Cliente();
            oCliente = new CN_Cliente().Listar().Where(u => u.idCliente == int.Parse(idCliente)).FirstOrDefault();

            if (oCliente.password != CN_Recursos.ConvertirSha256(password))
            {
                TempData["idCliente"] = idCliente;
                ViewData["password"] = "";
                ViewBag.Error = "El password actual no es correcto";
                return View();

            }
            else if (newPassword != confirmarPassword)
            {
                TempData["idCliente"] = idCliente;
                ViewData["password"] = password;
                ViewBag.Error = "Los password no coinciden";
                return View();

            }

            ViewData["password"] = "";
            newPassword = CN_Recursos.ConvertirSha256(newPassword);
            string mensaje = "";
            bool respuesta = new CN_Cliente().CambiarPassword(int.Parse(idCliente), newPassword, out mensaje);
            if (respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["idCliente"] = idCliente;
                ViewBag.Error = mensaje;
                return View();
            }
        }

        public ActionResult CerrarSesion()
        {
            //FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Acceso");
        }

    }
}