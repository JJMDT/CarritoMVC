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
    public class CN_Usuarios
    {
        private CD_Usuarios objCapaDato = new CD_Usuarios();

        public List<Usuario> Listar()
        {
            return objCapaDato.Listar();
        }
        public int Registrar(Usuario obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre)){
                mensaje = "El nombre de usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.apellido) || string.IsNullOrWhiteSpace(obj.apellido)){
                mensaje = "El apellido de usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.email) || string.IsNullOrWhiteSpace(obj.email)){
                mensaje = "El email de usuario no puede ser vacio";
            }
            if (string.IsNullOrEmpty(obj.password) || string.IsNullOrWhiteSpace(obj.password))
            {
               // obj.password = "usuario123"; // Contraseña por defecto
               //si la clave es varia , genera una clave automatica aleatoria
                obj.password = CN_Recursos.GenerarClave();
            }

            // Convertir la contraseña a SHA-256
           // obj.password = CN_Recursos.ConvertirSha256(obj.password);

            // Proceder con el registro del usuario
            if (string.IsNullOrEmpty(mensaje))
            {

                string clave = obj.password;
                //string clave = CN_Recursos.GenerarClave();
                string asunto = "Creacion de cuenta";
                string mensaje_email = " Su cuenta fue creada correctamente, su password es: !clave!";
                mensaje_email = mensaje_email.Replace("!clave!", clave);

                bool respuesta = CN_Recursos.EnviarEmail(obj.email, asunto, mensaje_email);
                if (respuesta)
                {
                    obj.password = CN_Recursos.ConvertirSha256(clave);
                    return objCapaDato.Registrar(obj, out mensaje);

                }
                else
                {
                    mensaje = "No se puede enviar el email";
                    return 0;
                }

            }
            else
            {
                return 0;
            }
        }
        public bool Editar(Usuario obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = "El nombre de usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.apellido) || string.IsNullOrWhiteSpace(obj.apellido))
            {
                mensaje = "El apellido de usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.email) || string.IsNullOrWhiteSpace(obj.email))
            {
                mensaje = "El email de usuario no puede ser vacio";
            }
            if (string.IsNullOrEmpty(obj.password) || string.IsNullOrWhiteSpace(obj.password))
            {
                obj.password = "9"; // Contraseña por defecto
            }

            // Convertir la contraseña a SHA-256
            obj.password = CN_Recursos.ConvertirSha256(obj.password);

            // Proceder con el registro del usuario
            if (string.IsNullOrEmpty(mensaje))
            {
                return objCapaDato.Editar(obj, out mensaje);
            }
            else
            {
                return false;
            }

        }

        public bool Eliminar(int id, out string mensaje)
        {
            return objCapaDato.Eliminar(id, out mensaje);
        }

        //cambiar clave

        public bool CambiarPassword(int idUsuario, string newPassword, out string mensaje)
        {
            return objCapaDato.CambiarPassword(idUsuario,newPassword, out mensaje);
        }

        //reestablcer password
        public bool ReestablecerPassword(int idUsuario, string email, out string mensaje)
        {
            mensaje = string.Empty;
            string newPassword = CN_Recursos.GenerarClave();
            bool result = objCapaDato.ReestablecerPassword(idUsuario, CN_Recursos.ConvertirSha256(newPassword), out mensaje);

            if (result)
            {
                string asunto = "Password reestablecido";
                string mensaje_email = " Su cuenta fue reestablecida correctamente, su password es: !clave!";
                mensaje_email = mensaje_email.Replace("!clave!", newPassword);
                bool respuesta = CN_Recursos.EnviarEmail(email, asunto, mensaje_email);

                if (respuesta)
                {
                    return true;

                }
                else
                {
                    mensaje = "No se pudo enviar el email";
                    return false;
                }
            }

            else
            {
                mensaje = "No se pudo reestablecer el password";

                return false;
            }

        }
    }
}
