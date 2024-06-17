using System;
using System.Collections.Generic;
using System.Linq;
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
                obj.password = "9"; // Contraseña por defecto
            }

            // Convertir la contraseña a SHA-256
            obj.password = CN_Recursos.ConvertirSha256(obj.password);

            // Proceder con el registro del usuario
            if (string.IsNullOrEmpty(mensaje))
            {
                return objCapaDato.Registrar(obj, out mensaje);
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

      
    }
}
