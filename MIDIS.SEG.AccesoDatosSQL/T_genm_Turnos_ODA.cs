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
    public partial class T_genm_Turnos_ODA
    {
        //BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWork = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnOracleM"]].ConnectionString));
        //BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWork = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSQLAnt"]].ConnectionString));
        BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWork = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings["cnnSQL"].ConnectionString));

        public IEnumerable<Turno_Registro> ListarTurnos()
        {
            List<Turno_Registro> lista = new List<Turno_Registro>();
            Turno_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paTurnoConsultar]";
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
                            item = new Turno_Registro();
                            //item.Grilla = new Grilla_Response();
                            //iCodTurno, vDescripcion, bEstado, dtAuditCreacion, vAuditCreacion, dtAuditModificacion, vAuditModificacion
                            item.iCodTurno = dr.GetInt32(dr.GetOrdinal("iCodTurno"));
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

        public IEnumerable<TurnoTrabajador_Registro> ListarTurnosTrabajador(TurnoTrabajador_Request request)
        {
            List<TurnoTrabajador_Registro> lista = new List<TurnoTrabajador_Registro>();
            TurnoTrabajador_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paTurnoTrabajadorConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodigoDependencia", request.iCodigoDependencia));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", request.iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iCodTurno", request.iCodTurno));
                cmd.Parameters.Add(new SqlParameter("@bEstado", request.bEstado));
                cmd.Parameters.Add(new SqlParameter("@bVigente", request.bVigente));


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            //iCodTurnoTrabajador	iCodTurno	iCodTrabajador	iCodigoDependencia	bVigente	
                            //dtVigenciaInicio	dtVigenciaFin	bEstado	dtAuditCreacion	vAuditCreacion	
                            //dtAuditModificacion	vAuditModificacion	vDependencia	vNombreTrabajador	
                            //vNumeroDocumento	Turno
                            item = new TurnoTrabajador_Registro();

                            item.Grilla = new Grilla_Response();
                            item.iCodTurnoTrabajador = Types.CheckDefaultValue<Int32>(dr["iCodTurnoTrabajador"]);
                            item.iCodTurno = Types.CheckDefaultValue<Int32>(dr["iCodTurno"]);
                            item.iCodTrabajador = Types.CheckDefaultValue<Int32>(dr["iCodTrabajador"]);
                            item.iCodigoDependencia = Types.CheckDefaultValue<Int32>(dr["iCodigoDependencia"]);
                            item.bVigente = Types.CheckDefaultValue<bool>(dr["bVigente"]);
                            item.dtVigenciaInicio = Types.CheckDefaultValue<DateTime>(dr["dtVigenciaInicio"]);
                            item.dtVigenciaFin = Types.CheckDefaultValue<DateTime>(dr["dtVigenciaFin"]);
                            item.bEstado = Types.CheckDefaultValue<bool>(dr["bEstado"]);
                            item.dtAuditCreacion = Types.CheckDefaultValue<DateTime>(dr["dtAuditCreacion"]);
                            item.vAuditCreacion = Types.CheckDefaultValue<String>(dr["vAuditCreacion"]);
                            item.dtAuditModificacion = Types.CheckDefaultValue<DateTime>(dr["dtAuditModificacion"]); 
                            item.vAuditModificacion = Types.CheckDefaultValue<String>(dr["vAuditModificacion"]);
                            item.vDependencia = Types.CheckDefaultValue<String>(dr["vDependencia"]);
                            item.vNombreTrabajador = Types.CheckDefaultValue<String>(dr["vNombreTrabajador"]);
                            item.vNumeroDocumento = Types.CheckDefaultValue<String>(dr["vNumeroDocumento"]);
                            item.vTurno = Types.CheckDefaultValue<String>(dr["vTurno"]);
                            
                           

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public TurnoTrabajador_Registro Insertar(TurnoTrabajador_Registro request)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paTurnoTrabajadorInsertar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodTurno", request.iCodTurno));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", request.iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iCodigoDependencia", request.iCodigoDependencia));
                cmd.Parameters.Add(new SqlParameter("@bVigente", request.bVigente));
                cmd.Parameters.Add(new SqlParameter("@bEstado", request.bEstado));
                cmd.Parameters.Add(new SqlParameter("@vAuditCreacion", request.vAuditCreacion));

                SqlParameter IdParameter = new SqlParameter("@iCodTurnoTrabajador", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(IdParameter);

                cmd.ExecuteNonQuery();
                request.iCodTurnoTrabajador = Int32.Parse(IdParameter.Value.ToString());

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return request;
        }

        public IEnumerable<TurnoDiaSemana_Registro> ObtenerTurnoDiaSenamaTrabajadorVigente(TurnoDiaSemana_Request request)
        {
            List<TurnoDiaSemana_Registro> lista = new List<TurnoDiaSemana_Registro>();
            TurnoDiaSemana_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "pa_TrabajadorTurnoDiaSemanaConsultar";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                //cmd.Parameters.Add(new SqlParameter("@iCodigoDependencia", request.iCodigoDependencia));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", request.iCodTrabajador));
                //cmd.Parameters.Add(new SqlParameter("@iCodTurno", request.iCodTurno));
                //cmd.Parameters.Add(new SqlParameter("@bEstado", request.bEstado));
                //cmd.Parameters.Add(new SqlParameter("@bVigente", request.bVigente));


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            //TDS.iCodTurnoDiaSemana, TDS.iCodTurno, TDS.iCodDiaSemana, TDS.tHoraEntrada, TDS.dToleranciaEntrada, TDS.dtRangoMarcaEntradaInicio, 
                            //TDS.dtRangoMarcaEntradaFin, TDS.tHoraSalida, TDS.dToleranciaSalida, TDS.dtRangoMarcaSalidaInicio, TDS.dtRangoMarcaSalidaFin, 
                            //TDS.bRefrigerioFlexible, TDS.dTiempoRefrigerio, TDS.dtRangoMarcaRefrigerioInicio, TDS.dtRangoMarcaRefrigerioFin, 
                            //TDS.bEstado, TDS.dtAuditCreacion, TDS.vAuditCreacion, TDS.dtAuditModificacion, TDS.vAuditModificacion
                            item = new TurnoDiaSemana_Registro();

                            item.Grilla = new Grilla_Response();
                            item.iCodTurnoDiaSemana = Types.CheckDefaultValue<Int32>(dr["iCodTurnoDiaSemana"]);
                            item.iCodTurno = Types.CheckDefaultValue<Int32>(dr["iCodTurno"]);
                            item.iCodDiaSemana = Types.CheckDefaultValue<Int32>(dr["iCodDiaSemana"]);
                            item.tHoraEntrada = Types.CheckDefaultValue<DateTime>(dr["tHoraEntrada"]);
                            item.dToleranciaEntrada = Types.CheckDefaultValue<decimal>(dr["dToleranciaEntrada"]);
                            item.dtRangoMarcaEntradaInicio = Types.CheckDefaultValue<DateTime>(dr["dtRangoMarcaEntradaInicio"]);
                            item.dtRangoMarcaEntradaFin = Types.CheckDefaultValue<DateTime>(dr["dtRangoMarcaEntradaFin"]);
                            item.tHoraSalida = Types.CheckDefaultValue<DateTime>(dr["tHoraSalida"]);
                            item.dToleranciaSalida = Types.CheckDefaultValue<decimal>(dr["dToleranciaSalida"]);
                            item.dtRangoMarcaSalidaInicio = Types.CheckDefaultValue<DateTime>(dr["dtRangoMarcaSalidaInicio"]);
                            item.dtRangoMarcaSalidaFin = Types.CheckDefaultValue<DateTime>(dr["dtRangoMarcaSalidaFin"]);
                            item.bRefrigerioFlexible = Types.CheckDefaultValue<bool>(dr["bRefrigerioFlexible"]);
                            item.dTiempoRefrigerio = Types.CheckDefaultValue<decimal>(dr["dTiempoRefrigerio"]);
                            item.dtRangoMarcaRefrigerioInicio = Types.CheckDefaultValue<DateTime>(dr["dtRangoMarcaRefrigerioInicio"]);
                            item.dtRangoMarcaRefrigerioFin = Types.CheckDefaultValue<DateTime>(dr["dtRangoMarcaRefrigerioFin"]);
                            item.bEstado = Types.CheckDefaultValue<bool>(dr["bEstado"]);
                            item.dtAuditCreacion = Types.CheckDefaultValue<DateTime>(dr["dtAuditCreacion"]);
                            item.vAuditCreacion = Types.CheckDefaultValue<String>(dr["vAuditCreacion"]);
                            item.dtAuditModificacion = Types.CheckDefaultValue<DateTime>(dr["dtAuditModificacion"]);
                            item.vAuditModificacion = Types.CheckDefaultValue<String>(dr["vAuditModificacion"]);
                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
    }
}
