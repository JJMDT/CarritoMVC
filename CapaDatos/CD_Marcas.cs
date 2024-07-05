using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;


namespace CapaDatos
{
    public class CD_Marcas
    {
        public List<Marca> Listar()
        {
            List<Marca> lista = new List<Marca>();

            try
            {
                using (SqlConnection oConn = new SqlConnection(Conexion.conn))
                {
                    string query = "select idMarca,descripcion,activo from marca";

                    SqlCommand cmd = new SqlCommand(query, oConn);
                    cmd.CommandType = CommandType.Text;

                    oConn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Marca()
                                {
                                    idMarca = Convert.ToInt32(dr["idMarca"]),
                                    descripcion = dr["descripcion"].ToString(),
                                    activo = Convert.ToBoolean(dr["activo"])
                                }
                            );
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Marca>();
            }

            return lista;
        }

        public int Registrar(Marca obj, out string mensaje)
        {
            int idAutogenerado = 0;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oConn = new SqlConnection(Conexion.conn))
                {
                    SqlCommand cmd = new SqlCommand("Registrar_Marca", oConn);
                    cmd.Parameters.AddWithValue("descripcion", obj.descripcion);
                    cmd.Parameters.AddWithValue("activo", obj.activo);
                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oConn.Open();
                    cmd.ExecuteNonQuery();

                    idAutogenerado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();


                }
            }
            catch (Exception ex)
            {
                idAutogenerado = 0;
                mensaje = ex.Message;

            }
            return idAutogenerado;
        }

        public bool Editar(Marca obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oConn = new SqlConnection(Conexion.conn))
                {
                    SqlCommand cmd = new SqlCommand("Editar_Marca", oConn);

                    cmd.Parameters.AddWithValue("idMarca", obj.idMarca);
                    cmd.Parameters.AddWithValue("descripcion", obj.descripcion);
                    cmd.Parameters.AddWithValue("activo", obj.activo);
                    cmd.Parameters.Add("resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConn.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }
            return resultado;
        }

        public bool Eliminar(int id, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oConn = new SqlConnection(Conexion.conn))
                {
                    SqlCommand cmd = new SqlCommand("Eliminar_Marca", oConn);

                    cmd.Parameters.AddWithValue("idMarca", id);
                    cmd.Parameters.Add("resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConn.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }
            return resultado;
        }



        public List<Marca> ListarMarcaXCategoria(int idcategoria)
        {
            List<Marca> lista = new List<Marca>();

            try
            {
                using (SqlConnection oConn = new SqlConnection(Conexion.conn))
                {
                    //string query = "select distinct m.idMarca,m.descripcion from producto as p inner join categoria as c on c.idCategoria = p.idCategoria inner join marca as m on m.idMarca = p.idMarca and m.activo = 1 where c.idCategoria = iif(@idcategoria = 0, c.idCategoria, @idcategoria)";

                    StringBuilder sb=new StringBuilder();
                    sb.AppendLine("select distinct m.idMarca,m.descripcion from producto as p");
                    sb.AppendLine("inner join categoria as c on c.idCategoria = p.idCategoria");
                    sb.AppendLine("inner join marca as m on m.idMarca = p.idMarca and m.activo = 1");
                    sb.AppendLine("where c.idCategoria = iif(@idcategoria = 0, c.idCategoria, @idcategoria)");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oConn);
                    cmd.Parameters.AddWithValue("@idcategoria", idcategoria);
                    cmd.CommandType = CommandType.Text;

                    oConn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Marca()
                                {
                                    idMarca = Convert.ToInt32(dr["idMarca"]),
                                    descripcion = dr["descripcion"].ToString(),
                                }
                            );
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Marca>();
            }

            return lista;
        }
    }
}

