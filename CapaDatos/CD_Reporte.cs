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
    }
}
