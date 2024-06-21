using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;


namespace CapaPresentacionAdmin.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CambiarPassword()
        {
            return View();
        }
        public ActionResult Reestablecer()
        {
            return View();
        }


        // POST: Acceso
        [HttpPost]
        public ActionResult Index(string email, string password)
        {
            Usuario oUsuario = new Usuario();
            oUsuario = new CN_Usuarios().Listar().Where(u => u.email == email ).FirstOrDefault();

            if (oUsuario == null)
            {
                ViewBag.Error = "Email incorrecto";
                return View();
            }
            else
            {
                if (oUsuario.password != CN_Recursos.ConvertirSha256(password))
                {
                    ViewData["email"] = email;
                    ViewBag.Error = "Password incorrecto";
                    return View();
                }
                else
                {
                    if (oUsuario.reestablecer)
                    {
                        TempData["idUsuario"]= oUsuario.idUsuario;
                        return RedirectToAction("CambiarPassword");
                    }
                    ViewBag.Error = null;
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        [HttpPost]
        public ActionResult CambiarPassword(string idUsuario, string password, string newPassword , string confirmarPassword)
        {
            Usuario oUsuario = new Usuario();
            oUsuario = new CN_Usuarios().Listar().Where(u => u.idUsuario == int.Parse(idUsuario)).FirstOrDefault();

            if(oUsuario.password != CN_Recursos.ConvertirSha256(password))
            {
                TempData["idUsuario"] = idUsuario;
                ViewData["password"] = "";
                ViewBag.Error = "El password actual no es correcto";
                return View();

            }
            else if (newPassword != confirmarPassword)
            {
                TempData["idUsuario"] = idUsuario;
                ViewData["password"] = password;
                ViewBag.Error = "Los password no coinciden";
                return View();

            }

            ViewData["password"] = "";
            newPassword = CN_Recursos.ConvertirSha256(newPassword);
            string mensaje = "";
            bool respuesta = new CN_Usuarios().CambiarPassword(int.Parse(idUsuario), newPassword, out mensaje);
            if(respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["idUsuario"] = idUsuario;
                ViewBag.Error = mensaje;
                return View();
            }
        }

    }
}