using MIDIS.ORI.Entidades;
using MIDIS.ORI.Entidades.Core;
using MIDIS.SEG.AccesoDatosSQL.Helpers;
using MIDIS.Utiles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.SEG.AccesoDatosSQL
{
    public class T_genm_controlasistencia_ODA
    {
        BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWork = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings["cnnSQL"].ConnectionString));
        public List<ReporteHorizontal_Registro> GetReporteHorizontal(ReporteHorizontal_Request request)
        {
            var lista = new List<ReporteHorizontal_Registro>();

            using (var connection = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion().Connection)
            {
                connection.Open();
                using (var cmd = new SqlCommand("pa_ReporteHorizontal", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Añadimos los parámetros con valores
                    cmd.Parameters.AddWithValue("@iCodTrabajador", request.iCodTrabajador);
                    cmd.Parameters.AddWithValue("@iCodigoDependencia", request.iCodigoDependencia);
                    cmd.Parameters.AddWithValue("@iMes", request.iMes);

                    // Ejecutamos la consulta y leemos los resultados
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var entidad = new ReporteHorizontal_Registro
                            {
                                vDependencia = reader.IsDBNull(reader.GetOrdinal("vDependencia")) ? null : reader.GetString(reader.GetOrdinal("vDependencia")),
                                vNombreTrabajador = reader.GetString(reader.GetOrdinal("vNombreTrabajador")),
                                vNumeroDocumento = reader.GetString(reader.GetOrdinal("vNumeroDocumento")),
                               
                            };
                            lista.Add(entidad);
                        }
                    }
                }
            }

            return lista;
        }

        public List<ReportePlanilla_Registro> GetReportePlanilla(ReportePlanilla_Request request)
        {
            var lista = new List<ReportePlanilla_Registro>();

            using (var connection = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion().Connection)
            {
                connection.Open();
                using (var cmd = new SqlCommand("pa_ReportePlanillas", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Añadimos los parámetros con valores
                    cmd.Parameters.AddWithValue("@iCodTrabajador", request.iCodTrabajador);
                    cmd.Parameters.AddWithValue("@iCodigoDependencia", request.iCodigoDependencia);
                    cmd.Parameters.AddWithValue("@iMes", request.iMes);

                    // Ejecutamos la consulta y leemos los resultados
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var entidad = new ReportePlanilla_Registro
                            {
                                vDependencia = reader.IsDBNull(reader.GetOrdinal("vDependencia")) ? null : reader.GetString(reader.GetOrdinal("vDependencia")),
                                vNombreTrabajador = reader.GetString(reader.GetOrdinal("vNombreTrabajador")),
                                vNumeroDocumento = reader.GetString(reader.GetOrdinal("vNumeroDocumento")),

                            };
                            lista.Add(entidad);
                        }
                    }
                }
            }

            return lista;
        }

        public List<ReporteSobretiempo_Registro> GetReporteSobretiempo(ReporteSobretiempo_Request request)
        {
            var lista = new List<ReporteSobretiempo_Registro>();

            using (var connection = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion().Connection)
            {
                connection.Open();
                using (var cmd = new SqlCommand("pa_ReporteSobretiempo", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Añadimos los parámetros con valores
                    cmd.Parameters.AddWithValue("@iCodTrabajador", request.iCodTrabajador);
                    cmd.Parameters.AddWithValue("@iCodigoDependencia", request.iCodigoDependencia);
                    cmd.Parameters.AddWithValue("@iMes", request.iMes);

                    // Ejecutamos la consulta y leemos los resultados
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var entidad = new ReporteSobretiempo_Registro
                            {
                                vDependencia = reader.IsDBNull(reader.GetOrdinal("vDependencia")) ? null : reader.GetString(reader.GetOrdinal("vDependencia")),
                                vNombreTrabajador = reader.GetString(reader.GetOrdinal("vNombreTrabajador")),
                                vNumeroDocumento = reader.GetString(reader.GetOrdinal("vNumeroDocumento")),

                            };
                            lista.Add(entidad);
                        }
                    }
                }
            }

            return lista;
        }
    }
}
