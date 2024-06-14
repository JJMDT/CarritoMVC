using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Usuarios
    {
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();

            try
            {
                using (SqlConnection oConn = new SqlConnection(Conexion.conn))
                {
                    string query = "select idUsuario, nombre, apellido, email, password, reestablecer,activo from usuario";

                    SqlCommand cmd = new SqlCommand(query,oConn);
                    cmd.CommandType = CommandType.Text;

                    oConn.Open();
                    using (SqlDataReader dr=cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Usuario()
                                {
                                    idUsuario= Convert.ToInt32(dr["idUsuario"]),
                                    nombre = dr["nombre"].ToString(),
                                    apellido = dr["apellido"].ToString(),
                                    email = dr["email"].ToString(),
                                    password = dr["password"].ToString(),
                                    reestablecer = Convert.ToBoolean(dr["reestablecer"]),
                                    activo = Convert.ToBoolean(dr["activo"])
                                }
                            );
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Usuario>();
            }

            return lista;
        }
    }
}
