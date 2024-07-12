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
        public List<Departamento> ObtenerDepartamento()
        {
            List<Departamento> lista = new List<Departamento>();

            try
            {
                using (SqlConnection oConn = new SqlConnection(Conexion.conn))
                {
                    string query = "select * from departamento";
                    SqlCommand cmd = new SqlCommand(query, oConn);
                    cmd.CommandType = CommandType.Text;
                    oConn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Departamento()
                                {
                                    idDepartamento =dr["idDepartamento"].ToString(),
                                    descripcion = dr["descripcion"].ToString(),
                                }
                            );
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Departamento>();
            }
            return lista;
        }

        public List<Provincia> ObtenerProvincia(string iddep)
        {
            List<Provincia> lista = new List<Provincia>();

            try
            {
                using (SqlConnection oConn = new SqlConnection(Conexion.conn))
                {
                    string query = "select * from provincia where idDepartamento = @iddepartamento";
                    SqlCommand cmd = new SqlCommand(query, oConn);
                    cmd.Parameters.AddWithValue("@iddepartamento", iddep);
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

        public List<Distrito> ObtenerDistrito(string iddepartamento, string idprovincia)
        {
            List<Distrito> lista = new List<Distrito>();

            try
            {
                using (SqlConnection oConn = new SqlConnection(Conexion.conn))
                {
                    string query = "select * from distrito where idProvincia = @idprovincia and idDepartamento = @iddepartamento";
                    SqlCommand cmd = new SqlCommand(query, oConn);
                    cmd.Parameters.AddWithValue("@iddepartamento", iddepartamento);
                    cmd.Parameters.AddWithValue("@idprovincia", idprovincia);
                    cmd.CommandType = CommandType.Text;
                    oConn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Distrito()
                                {
                                    idDistrito = dr["idDistrito"].ToString(),
                                    descripcion = dr["descripcion"].ToString(),
                                }
                            );
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Distrito>();
            }
            return lista;
        }
    }

    
}

