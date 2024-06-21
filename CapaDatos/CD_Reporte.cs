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
    public class CD_Reporte
    {
        public DashBoard VerDashBoard()
        {
            DashBoard objeto = new DashBoard();

            try
            {
                using (SqlConnection oConn = new SqlConnection(Conexion.conn))
                {
                    

                    SqlCommand cmd = new SqlCommand("Reporte_Dashboard", oConn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objeto = new DashBoard()
                            {
                                totalCliente = Convert.ToInt32(dr["TotalCliente"]),
                                totalVenta = Convert.ToInt32(dr["TotalVenta"]),
                                totalProducto = Convert.ToInt32(dr["TotalProducto"]),
                            };
                        }
                    }
                }
            }
            catch
            {
                objeto = new DashBoard();

            }

            return objeto;
        }


        public List<Reporte> Ventas(string fechaInicio, string fechaFin, string idTransaccion)
        {
            List<Reporte> lista = new List<Reporte>();

            try
            {
                using (SqlConnection oConn = new SqlConnection(Conexion.conn))
                {
                    

                    SqlCommand cmd = new SqlCommand("Reporte_Ventas", oConn);

                    cmd.Parameters.AddWithValue("fechaVenta", fechaInicio);
                    cmd.Parameters.AddWithValue("fechaFin", fechaFin);
                    cmd.Parameters.AddWithValue("idTransaccion", idTransaccion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Reporte()
                                {
                                    fechaVenta =dr["fechaVenta"].ToString(),
                                    cliente = dr["cliente"].ToString(),
                                    producto = dr["producto"].ToString(),
                                    precio =Convert.ToDecimal( dr["precio"], new CultureInfo("es-AR")),
                                    cantidad = Convert.ToInt32( dr["cantidad"]),
                                    total = Convert.ToDecimal(dr["total"], new CultureInfo("es-AR")),
                                    idTransaccion = dr["idTransaccion"].ToString(),
                                }
                            );
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Reporte>();
            }

            return lista;
        }


    }
}
