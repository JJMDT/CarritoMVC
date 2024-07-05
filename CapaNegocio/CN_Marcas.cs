using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public  class CN_Marcas
    {
        private CD_Marcas objCapaDato = new CD_Marcas();

        public List<Marca> Listar()
        {
            return objCapaDato.Listar();
        }
        // registrar 
        public int Registrar(Marca obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.descripcion) || string.IsNullOrWhiteSpace(obj.descripcion))
            {
                mensaje = "La descripcion de la catetegoia no puede ser vacio";
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

        public bool Editar(Marca obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.descripcion) || string.IsNullOrWhiteSpace(obj.descripcion))
            {
                mensaje = "La descripcion de la catetegoia no puede ser vacio";
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

        //eliminar 

        public bool Eliminar(int id, out string mensaje)
        {
            return objCapaDato.Eliminar(id, out mensaje);
        }
        public List<Marca> ListarMarcaXCategoria(int idcategoria)
        {
            return objCapaDato.ListarMarcaXCategoria(idcategoria);
        }

    }
}

