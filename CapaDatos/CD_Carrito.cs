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
    public class CD_Carrito
    {
        public bool ExisteCarrito(int idcliente, int idproducto)
        {
            bool resultado = true;

            try
            {
                using (SqlConnection oConn = new SqlConnection(Conexion.conn))
                {
                    SqlCommand cmd = new SqlCommand("ExisteCarrito", oConn);
                    cmd.Parameters.AddWithValue("idCliente", idcliente);
                    cmd.Parameters.AddWithValue("idProducto", idproducto);
                    cmd.Parameters.Add("resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;
                    oConn.Open();
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);


                }
            }
            catch (Exception ex)
            {
                resultado = false;

            }
            return resultado;
            ;


        }



        public bool OperacionCarrito(int idcliente, int idproducto, bool sumar, out string mensaje)
        {
            bool resultado = true;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oConn = new SqlConnection(Conexion.conn))
                {
                    SqlCommand cmd = new SqlCommand("OperacionCarrito", oConn);
                    cmd.Parameters.AddWithValue("idCliente", idcliente);
                    cmd.Parameters.AddWithValue("idProducto", idproducto);
                    cmd.Parameters.AddWithValue("sumar", sumar);
                    cmd.Parameters.Add("resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;
                    oConn.Open();
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    mensaje = cmd.Parameters["Resultado"].Value.ToString();


                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;

            }
            return resultado;
            ;


        }

        //select count(*) from carrito where idCliente = 1
        public int CantidadEnCarrito(int idcliente)
        {
            int resultado = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.conn))
                {
                    SqlCommand cmd = new SqlCommand(" select count(*) from carrito where idCliente =@idcliente ", conn);
                    cmd.Parameters.AddWithValue("@idcliente", idcliente);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    resultado = Convert.ToInt32(cmd.ExecuteScalar());

                }

            }
            catch (Exception ex)
            {
                resultado = 0;
            }
            return resultado;
        }

    }
}
