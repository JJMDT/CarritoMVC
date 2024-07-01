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
    public class CN_Cliente
    {
        private CD_Cliente objCapaDato = new CD_Cliente();



        //registrar

        public int Registrar(Cliente obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = "El nombre de cliente no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.apellido) || string.IsNullOrWhiteSpace(obj.apellido))
            {
                mensaje = "El apellido de cliente no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.email) || string.IsNullOrWhiteSpace(obj.email))
            {
                mensaje = "El email de cliente no puede ser vacio";
            }
           


            // Proceder con el registro del Cliente
            if (string.IsNullOrEmpty(mensaje))
            {

                obj.password = CN_Recursos.ConvertirSha256(obj.password);
                return objCapaDato.Registrar(obj, out mensaje);



            }
            else
            {
                return 0;
            }
        }

        //listar

        public List<Cliente> Listar()
        {
            return objCapaDato.Listar();
        }

        //cambiar clave

        public bool CambiarPassword(int idCliente, string newPassword, out string mensaje)
        {
            return objCapaDato.CambiarPassword(idCliente, newPassword, out mensaje);
        }
        //restablecer la clave 

        public bool ReestablecerPassword(int idCliente, string email, out string mensaje)
        {
            mensaje = string.Empty;
            string newPassword = CN_Recursos.GenerarClave();
            bool result = objCapaDato.ReestablecerPassword(idCliente, CN_Recursos.ConvertirSha256(newPassword), out mensaje);

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
