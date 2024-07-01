using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Cliente
    {

        public int Registrar(Cliente obj, out string mensaje)
        {
            int idAutogenerado = 0;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oConn = new SqlConnection(Conexion.conn))
                {
                    SqlCommand cmd = new SqlCommand("Registrar_Cliente", oConn);
                    cmd.Parameters.AddWithValue("nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("apellido", obj.apellido);
                    cmd.Parameters.AddWithValue("email", obj.email);
                    cmd.Parameters.AddWithValue("password", obj.password);
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
        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();

            try
            {
                using (SqlConnection oConn = new SqlConnection(Conexion.conn))
                {
                    Console.WriteLine("Abriendo conexión...");
                    oConn.Open();
                    Console.WriteLine("Conexión abierta.");

                    string query = "SELECT idCliente, nombre, apellido, email, password, restablecer FROM cliente";
                    using (SqlCommand cmd = new SqlCommand(query, oConn))
                    {
                        cmd.CommandType = CommandType.Text;

                        Console.WriteLine("Ejecutando comando SQL...");
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                Console.WriteLine("Datos obtenidos de la base de datos:");
                                while (dr.Read())
                                {
                                    Console.WriteLine($"idCliente: {dr["idCliente"]}, email: {dr["email"]}");

                                    lista.Add(new Cliente()
                                    {
                                        idCliente = Convert.ToInt32(dr["idCliente"]),
                                        nombre = dr["nombre"].ToString(),
                                        apellido = dr["apellido"].ToString(),
                                        email = dr["email"].ToString(),
                                        password = dr["password"].ToString(),
                                        reestablecer = Convert.ToBoolean(dr["restablecer"]),
                                    });
                                }
                            }
                            else
                            {
                                Console.WriteLine("No se encontraron filas.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar clientes: " + ex.Message);
                lista = new List<Cliente>();
            }

            return lista;
        }
        //cambiar password 

        public bool CambiarPassword(int idCliente, string newPassword, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.conn))
                {
                    SqlCommand cmd = new SqlCommand("update cliente set password = @newPassword, restablecer = 0 where idCliente = @id", conn);
                    cmd.Parameters.AddWithValue("@id", idCliente);
                    cmd.Parameters.AddWithValue("@newPassword", newPassword);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;

                }

            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }
            return resultado;
        }

        //reestablecer clave 

        public bool ReestablecerPassword(int idCliente, string newPassword, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.conn))
                {
                    SqlCommand cmd = new SqlCommand("update cliente set password = @newPassword, restablecer = 1 where idCliente = @id", conn);
                    cmd.Parameters.AddWithValue("@id", idCliente);
                    cmd.Parameters.AddWithValue("@newPassword", newPassword);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;

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
