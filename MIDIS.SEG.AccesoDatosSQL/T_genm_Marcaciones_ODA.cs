using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using MIDIS.ORI.Entidades;
using MIDIS.SEG.AccesoDatosSQL.Helpers;

namespace MIDIS.SEG.AccesoDatosSQL
{
    public partial class T_genm_Marcaciones_ODA
    {
        //BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWork = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnOracleM"]].ConnectionString));
        //BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWork = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSQLAnt"]].ConnectionString));
        BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWork = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings["cnnSQL"].ConnectionString));

        public IEnumerable<TipoMarcaciones_Registro> ListarTipoMarcaciones()
        {
            List<TipoMarcaciones_Registro> lista = new List<TipoMarcaciones_Registro>();
            TipoMarcaciones_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaeTipoMarcacionConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                //cmd.Parameters.Add(new SqlParameter("@ID_BANCO", peticion.IdBanco));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new TipoMarcaciones_Registro();
                            //item.Grilla = new Grilla_Response();

                            item.iCodTipoMarcacion = dr.GetInt32(dr.GetOrdinal("iCodTipoMarcacion"));
                            item.vDescripcion = dr.GetString(dr.GetOrdinal("vDescripcion"));
                            item.bEstado = dr.GetBoolean(dr.GetOrdinal("bEstado"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<Marcaciones_Registro> ListarMarcaciones(Marcaciones_Request request)
        {
            List<Marcaciones_Registro> lista = new List<Marcaciones_Registro>();
            Marcaciones_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMarcacionConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodigoDependencia", request.iCodigoDependencia));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", request.iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@dtFechaMarcacionIni", request.dtFechaMarcacionIni));
                cmd.Parameters.Add(new SqlParameter("@dtFechaMarcacionFin", request.dtFechaMarcacionFin));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoMarcacion", request.iCodTipoMarcacion));


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            //iCodMarcaciones	iCodTrabajador	iCodigoDependencia	iCodTipoMarcacion	
                            //dtFechaMarcacion	vLongitud	vLatitud	vIpCliente	dtAuditCreacion	vAuditCreacion	
                            //dtAuditModificacion	vAuditModificacion	vDependencia	vNombreTrabajador	vTipoMarcacion
                            item = new Marcaciones_Registro();

                            item.Grilla = new Grilla_Response();
                            item.iCodMarcaciones = Types.CheckDefaultValue<Int32>(dr["iCodMarcaciones"]);
                            item.iCodTrabajador = Types.CheckDefaultValue<Int32>(dr["iCodTrabajador"]);
                            item.iCodigoDependencia = Types.CheckDefaultValue<Int32>(dr["iCodigoDependencia"]);
                            item.iCodTipoMarcacion = Types.CheckDefaultValue<Int32>(dr["iCodTipoMarcacion"]);
                            item.dtFechaMarcacion = Types.CheckDefaultValue<DateTime>(dr["dtFechaMarcacion"]);
                            item.vLongitud = Types.CheckDefaultValue<String>(dr["vLongitud"]);
                            item.vLatitud = Types.CheckDefaultValue<String>(dr["vLatitud"]);
                            item.vIpCliente = Types.CheckDefaultValue<String>(dr["vIpCliente"]);
                            item.dtAuditCreacion = dr.GetDateTime(dr.GetOrdinal("dtAuditCreacion"));
                            item.vAuditCreacion = Types.CheckDefaultValue<String>(dr["vAuditCreacion"]);
                            item.dtAuditModificacion = Types.CheckDefaultValue<DateTime>(dr["dtAuditModificacion"]); // Convert.ToDateTime(dr["dtAuditModificacion"]); //  dr.IsDBNull(dr.GetOrdinal("dtAuditModificacion")) ? DateTime.MinValue : dr.GetDateTime(dr.GetOrdinal("dtAuditModificacion"));
                            item.vAuditModificacion = Types.CheckDefaultValue<String>(dr["vAuditModificacion"]);
                            item.vDependencia = Types.CheckDefaultValue<String>(dr["vDependencia"]);
                            item.vNombreTrabajador = Types.CheckDefaultValue<String>(dr["vNombreTrabajador"]);
                            item.vTipoMarcacion = Types.CheckDefaultValue<String>(dr["vTipoMarcacion"]);
                            item.vNumeroDocumento = Types.CheckDefaultValue<String>(dr["vNumeroDocumento"]);
                            item.bEstado = Types.CheckDefaultValue<bool>(dr["bEstado"]);

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public Marcaciones_Registro Insertar(Marcaciones_Registro request)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMarcacionInsertar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", request.iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iCodigoDependencia", request.iCodigoDependencia));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoMarcacion", request.iCodTipoMarcacion));
                cmd.Parameters.Add(new SqlParameter("@dtFechaMarcacion", request.dtFechaMarcacion));
                cmd.Parameters.Add(new SqlParameter("@vLongitud", request.vLongitud));
                cmd.Parameters.Add(new SqlParameter("@vLatitud", request.vLatitud));
                cmd.Parameters.Add(new SqlParameter("@vIpCliente", request.vIpCliente));
                cmd.Parameters.Add(new SqlParameter("@bEstado", request.bEstado));
                cmd.Parameters.Add(new SqlParameter("@vAuditCreacion", request.vAuditCreacion));

                SqlParameter IdPropuestaParameter = new SqlParameter("@iCodMarcaciones", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(IdPropuestaParameter);

                cmd.ExecuteNonQuery();
                request.iCodMarcaciones = Int32.Parse(IdPropuestaParameter.Value.ToString());

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return request;
        }
    }
}
