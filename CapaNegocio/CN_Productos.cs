using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Productos
    {
        private CD_Productos objCapaDato = new CD_Productos();

        public List<Producto> Listar()
        {
            return objCapaDato.Listar();
        }
        // registrar 
        public int Registrar(Producto obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = "El nombre del producto no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.descripcion) || string.IsNullOrWhiteSpace(obj.descripcion))
            {
                mensaje = "La descripcion  del producto no puede ser vacio";
            }
            else if (obj.oMarca.idMarca == 0)
            {
                mensaje = "Debe seleccionar una marca";
            }
            else if (obj.oCategoria.idCategoria == 0)
            {
                mensaje = "Debe seleccionar una categoria";
            }
            else if (obj.precio == 0)
            {
                mensaje = "Debe ingresar el precio del producto";
            }
            else if (obj.stock == 0)
            {
                mensaje = "Debe ingresar el stock del producto";
            }

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

        //editar 

        public bool Editar(Producto obj, out string mensaje)
        {
            mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = "El nombre del producto no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.descripcion) || string.IsNullOrWhiteSpace(obj.descripcion))
            {
                mensaje = "La descripcion  del producto no puede ser vacio";
            }
            else if (obj.oMarca.idMarca == 0)
            {
                mensaje = "Debe seleccionar una marca";
            }
            else if (obj.oCategoria.idCategoria == 0)
            {
                mensaje = "Debe seleccionar una categoria";
            }
            else if (obj.precio == 0)
            {
                mensaje = "Debe ingresar el precio del producto";
            }
            else if (obj.stock == 0)
            {
                mensaje = "Debe ingresar el stock del producto";
            }


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
        //guardar datos imagenes
        public bool GuardarDatosImagen(Producto obj, out string mensaje)
        {
            return objCapaDato.GuardarDatosImagen(obj,out mensaje);
        }



        //eliminar 

        public bool Eliminar(int id, out string mensaje)
        {
            return objCapaDato.Eliminar(id, out mensaje);
        }
    }
}


