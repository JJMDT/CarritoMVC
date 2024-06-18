using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;


namespace CapaDatos
{
    public class CD_Productos
    {
        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();

            try
            {
                using (SqlConnection oConn = new SqlConnection(Conexion.conn))
                {
                   
                    string query = "select p.nombre,p.descripcion, m.descripcion [DescMarca] , c.descripcion [DescCategoria],p.precio, p.stock, p.activo from producto as p inner join marca as m on m.idMarca = p.idMarca inner join categoria as c on c.idCategoria=p.idCategoria";

                    SqlCommand cmd = new SqlCommand(query, oConn);
                    cmd.CommandType = CommandType.Text;

                    oConn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Producto()
                                {
                                    idProducto = Convert.ToInt32(dr["idProducto"]),
                                    nombre = Convert.ToString(dr["nombre"]),
                                    descripcion = dr["descripcion"].ToString(),
                                    oMarca =new Marca() { descripcion = Convert.ToString(dr["DescMarca"]) },
                                    oCategoria =new Categoria() { descripcion = Convert.ToString(dr["DescCategoria"]) },
                                    precio = Convert.ToDecimal(dr["precio"],new CultureInfo("es-AR")),
                                    stock = Convert.ToInt32(dr["stock"]),
                                    rutaImagen = dr["RutaImagen"].ToString(),
                                    nombreImagen = dr["NombreImagen"].ToString(),
                                    activo = Convert.ToBoolean(dr["activo"])


                                }
                            );
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Producto>();
            }

            return lista;
        }

        public int Registrar(Producto obj, out string mensaje)
        {
            int idAutogenerado = 0;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oConn = new SqlConnection(Conexion.conn))
                {
                    SqlCommand cmd = new SqlCommand("Registrar_Producto", oConn);
                    cmd.Parameters.AddWithValue("nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("descripcion", obj.descripcion);
                    cmd.Parameters.AddWithValue("idMarca", obj.oMarca.idMarca);
                    cmd.Parameters.AddWithValue("idCategoria", obj.oCategoria.idCategoria);
                    cmd.Parameters.AddWithValue("precio", obj.precio);
                    cmd.Parameters.AddWithValue("stock", obj.stock);
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

        public bool Editar(Producto obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oConn = new SqlConnection(Conexion.conn))
                {
                    SqlCommand cmd = new SqlCommand("Editar_Producto", oConn);

                    cmd.Parameters.AddWithValue("idProducto", obj.idProducto);
                    cmd.Parameters.AddWithValue("nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("descripcion", obj.descripcion);
                    cmd.Parameters.AddWithValue("idMarca", obj.oMarca.idMarca);
                    cmd.Parameters.AddWithValue("idCategoria", obj.oCategoria.idCategoria);
                    cmd.Parameters.AddWithValue("precio", obj.precio);
                    cmd.Parameters.AddWithValue("stock", obj.stock);
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


        public bool GuardarDatosImagen(Producto obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oConn = new SqlConnection(Conexion.conn))
                {
                    string query = "update producto set RutaImagen = @rutaImagen, NombreImagen = @nombreImagen where idProducto = @idProducto";

                    SqlCommand cmd = new SqlCommand("Registrar_Producto", oConn);
                    cmd.Parameters.AddWithValue("RutaImagen", obj.rutaImagen);
                    cmd.Parameters.AddWithValue("NombreImagen", obj.nombreImagen);
                    cmd.Parameters.AddWithValue("idProducto", obj.idProducto);
                   
                    cmd.CommandType = CommandType.Text;
                    oConn.Open();
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        resultado = true;
                    }
                    else
                    {
                        resultado= false;
                        mensaje = "No se pudo almacenar la imagen";
                    }


                }
            }
            catch (Exception ex)
            {
                resultado= false;
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
                    SqlCommand cmd = new SqlCommand("Eliminar_Producto", oConn);

                    cmd.Parameters.AddWithValue("idProducto", id);
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


    }
}

