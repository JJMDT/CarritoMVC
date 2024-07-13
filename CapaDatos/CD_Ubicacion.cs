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
    public class CD_Ubicacion
    {

        public List<Provincia> ObtenerProvincia()
        {
            List<Provincia> lista = new List<Provincia>();

            try
            {
                using (SqlConnection oConn = new SqlConnection(Conexion.conn))
                {
                    string query = "select * from provincia ";
                    SqlCommand cmd = new SqlCommand(query, oConn);
                    cmd.CommandType = CommandType.Text;
                    oConn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Provincia()
                                {
                                    idProvincia = dr["idProvincia"].ToString(),
                                    descripcion = dr["descripcion"].ToString(),
                                }
                            );
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Provincia>();
            }
            return lista;
        }

        public List<Localidad> ObtenerLocalidad(string idprov)
        {
            List<Localidad> lista = new List<Localidad>();

            try
            {
                using (SqlConnection oConn = new SqlConnection(Conexion.conn))
                {
                    string query = "SELECT * FROM localidad AS loc INNER JOIN provincia p ON p.idProvincia = loc.idProvincia WHERE loc.idProvincia = @idprov";
                    SqlCommand cmd = new SqlCommand(query, oConn);
                    cmd.Parameters.AddWithValue("@idprov", idprov);
                    cmd.CommandType = CommandType.Text;
                    oConn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Localidad()
                                {
                                    idLocalidad = dr["idLocalidad"].ToString(),
                                    descripcion = dr["descripcion"].ToString(),

                                }
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener las localidades: " + ex.Message);

                lista = new List<Localidad>();
            }
            return lista;
        }

       

       
    }

    
}

