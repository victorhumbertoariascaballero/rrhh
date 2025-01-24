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
    public class T_genm_vacaciones_ODA
    {

        BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWork = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings["cnnSQL"].ConnectionString));




        public IEnumerable<Vacaciones_Registro> ListaVacacionesTrabajador(VacacionesTrabajador_Request request)
        {
            List<Vacaciones_Registro> lista = new List<Vacaciones_Registro>();
            Vacaciones_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[pa_VacacionesTrabajadorConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodigoDependencia", request.iCodigoDependencia));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", request.iCodTrabajador));
                if (request.fecha == "")
                    cmd.Parameters.Add(new SqlParameter("@fecha", DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@fecha", Convert.ToDateTime(request.fecha)));
                cmd.Parameters.Add(new SqlParameter("@iCodEstadoProceso", request.iCodEstadoProceso));


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            //J.iCodJustificaciones, J.iCodTrabajador, J.iCodigoDependencia, J.iCodTrabajador, J.iCodTipoJustificacion, TJ.vDescripcion vTipoJustificacion,
                            //J.iCodEstadoProceso, EP.vDescripcion EstadoProceso, CONVERT(NVARCHAR(10), J.dtFechaHoraInicio, 103) dtFechaHoraInicio, CONVERT(NVARCHAR(10), J.dtFechaHoraFin, 103) dtFechaHoraFin,J.vDescripcion, 
                            //D.vDependencia, (T.vNombres + ' ' + T.vApellidoPaterno + ' ' + T.vApellidoMaterno) AS vNombreTrabajador, T.vNumeroDocumento
                            item = new Vacaciones_Registro();

                            item.Grilla = new Grilla_Response();
                            item.iCodVacaciones = Types.CheckDefaultValue<Int32>(dr["iCodVacaciones"]);
                            item.iCodTipoVacaciones = Types.CheckDefaultValue<Int32>(dr["iCodTipoVacaciones"]);
                            item.iCodTrabajador = Types.CheckDefaultValue<Int32>(dr["iCodTrabajador"]);
                            item.iCodigoDependencia = Types.CheckDefaultValue<Int32>(dr["iCodigoDependencia"]);

                            item.iDiasRestantes = Types.CheckDefaultValue<Int32>(dr["iDiasRestantes"]);
                            item.iAsignados = Types.CheckDefaultValue<Int32>(dr["iAsignados"]);
                            item.iDisponibles = Types.CheckDefaultValue<Int32>(dr["iDisponibles"]);

                            item.iCodEstadoProceso = Types.CheckDefaultValue<Int32>(dr["iCodEstadoProceso"]);
                            item.vEstadoProceso = Types.CheckDefaultValue<string>(dr["vEstadoProceso"]);
                            item.vAuditCreacion = Types.CheckDefaultValue<string>(dr["vAuditCreacion"]);
                            item.vUrlArchivo = Types.CheckDefaultValue<string>(dr["vUrlArchivo"]);

                            item.dtAuditCreacion = Types.CheckDefaultValue<DateTime>(dr["dtAuditCreacion"]);
                            item.dtFechaInicio = Types.CheckDefaultValue<DateTime>(dr["dtFechaInicio"]);
                            item.dtFechaFin = Types.CheckDefaultValue<DateTime>(dr["dtFechaFin"]);

                            item.vDescripcion = Types.CheckDefaultValue<string>(dr["vEstadoProceso"]);
                            item.iPeriodo = Types.CheckDefaultValue<int>(dr["iPeriodo"]);
                            item.bFraccionamientoVacacionalMediaJornada = Types.CheckDefaultValue<bool>(dr["bFraccionamientoVacacionalMediaJornada"]);


                            item.vDependencia = Types.CheckDefaultValue<String>(dr["vDependencia"]);
                            item.vNombreTrabajador = Types.CheckDefaultValue<String>(dr["vNombreTrabajador"]);
                            item.vNumeroDocumento = Types.CheckDefaultValue<String>(dr["vNumeroDocumento"]);
                            item.vTipoVacaciones = Types.CheckDefaultValue<String>(dr["vTipoVacaciones"]);


                            item.vAprovadoJeje = Types.CheckDefaultValue<String>(dr["vAprovadoJeje"]);
                            item.vFechaAprobadoJefe = Types.CheckDefaultValue<String>(dr["vFechaAprobadoJefe"]);
                            item.vAprobadoAdmin = Types.CheckDefaultValue<String>(dr["vAprobadoAdmin"]);
                            item.vFechaAprobadoAdmin = Types.CheckDefaultValue<String>(dr["vFechaAprobadoAdmin"]);

                            item.dFechaAprobadoJefe = Types.CheckDefaultValue<DateTime>(dr["dFechaAprobadoJefe"]);
                            item.dFechaAprobadoAdmin = Types.CheckDefaultValue<DateTime>(dr["dFechaAprobadoAdmin"]);
                            item.dFechaDenegadoAdmin = Types.CheckDefaultValue<DateTime>(dr["dFechaDenegadoAdmin"]);


                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public VacacionesTrabajador_Registro ObtenerVacacionesPorId(int iCodVacaciones)
        {
            VacacionesTrabajador_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[pa_VacacionesTrabajadorXId]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodVacaciones", iCodVacaciones));


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            //J.iCodJustificaciones, J.iCodTrabajador, J.iCodigoDependencia, J.iCodTrabajador, J.iCodTipoJustificacion, TJ.vDescripcion vTipoJustificacion,
                            //J.iCodEstadoProceso, EP.vDescripcion EstadoProceso, CONVERT(NVARCHAR(10), J.dtFechaHoraInicio, 103) dtFechaHoraInicio, CONVERT(NVARCHAR(10), J.dtFechaHoraFin, 103) dtFechaHoraFin,J.vDescripcion, 
                            //D.vDependencia, (T.vNombres + ' ' + T.vApellidoPaterno + ' ' + T.vApellidoMaterno) AS vNombreTrabajador, T.vNumeroDocumento
                            item = new VacacionesTrabajador_Registro();

                            item.iCodVacaciones = Types.CheckDefaultValue<Int32>(dr["iCodVacaciones"]);
                            item.iCodTipoVacaciones = Types.CheckDefaultValue<Int32>(dr["iCodTipoVacaciones"]);
                            item.iCodTrabajador = Types.CheckDefaultValue<Int32>(dr["iCodTrabajador"]);
                            item.iCodigoDependencia = Types.CheckDefaultValue<Int32>(dr["iCodigoDependencia"]);

                            item.iDiasRestantes = Types.CheckDefaultValue<Int32>(dr["iDiasRestantes"]);
                            item.iAsignados = Types.CheckDefaultValue<Int32>(dr["iAsignados"]);
                            item.iDisponibles = Types.CheckDefaultValue<Int32>(dr["iDisponibles"]);

                            item.iCodEstadoProceso = Types.CheckDefaultValue<Int32>(dr["iCodEstadoProceso"]);
                            item.vAuditCreacion = Types.CheckDefaultValue<string>(dr["vAuditCreacion"]);

                            item.dtFechaInicio = Types.CheckDefaultValue<DateTime>(dr["dtFechaInicio"]);
                            item.dtFechaFin = Types.CheckDefaultValue<DateTime>(dr["dtFechaFin"]);

                            item.vDescripcion = Types.CheckDefaultValue<string>(dr["vDescripcion"]);
                            item.iPeriodo = Types.CheckDefaultValue<int>(dr["iPeriodo"]);

                            //lista.Add(item);
                        }
                    }
                }

                // Obtener Archivos de la Justificación
                cmd.CommandText = "[dbo].[pa_VacacionesTrabajadorArchivosporId]";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@iCodVacaciones", iCodVacaciones));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        item.Archivos = new List<VacacionesArchivo>();
                        while (dr.Read())
                        {
                            item.Archivos.Add(new VacacionesArchivo
                            {
                                iCodVacacionesArchivos = Types.CheckDefaultValue<int>(dr["iCodVacacionesArchivos"]),
                                iCodTipoVacacionesFormato = Types.CheckDefaultValue<int>(dr["iCodTipoVacacionesFormato"]),
                                iCodVacaciones = Types.CheckDefaultValue<int>(dr["iCodVacaciones"]),
                                vObservaciones = Types.CheckDefaultValue<string>(dr["vObservaciones"]),
                                vUrlArchivo = Types.CheckDefaultValue<string>(dr["vUrlArchivo"]),
                                bEstado = Types.CheckDefaultValue<bool>(dr["bEstado"]),
                            });
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return item;
        }

        public VacacionesTrabajador_Registro InsertarVacaciones(VacacionesTrabajador_Registro vacaciones)
        {
            using (SqlConnection connection = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion().Connection)
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Insertar en Vacaciones
                    SqlCommand cmd = new SqlCommand("pa_InsertarVacaciones", connection, transaction)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@iCodTipoVacaciones", vacaciones.iCodTipoVacaciones);
                    cmd.Parameters.AddWithValue("@iCodTrabajador", vacaciones.iCodTrabajador);
                    cmd.Parameters.AddWithValue("@iCodigoDependencia", vacaciones.iCodigoDependencia);
                    cmd.Parameters.AddWithValue("@iCodEstadoProceso", vacaciones.iCodEstadoProceso);
                    cmd.Parameters.AddWithValue("@bFraccionamientoVacacionalMediaJornada", vacaciones.bFraccionamientoVacacionalMediaJornada);
                    cmd.Parameters.AddWithValue("@dtFechaInicio", vacaciones.dtFechaInicio);
                    cmd.Parameters.AddWithValue("@dtFechaFin", vacaciones.dtFechaFin);
                    cmd.Parameters.AddWithValue("@vDescripcion", vacaciones.vDescripcion);
                    cmd.Parameters.AddWithValue("@iAsignados", vacaciones.iAsignados);
                    cmd.Parameters.AddWithValue("@iDisponibles", vacaciones.iDisponibles);
                    cmd.Parameters.AddWithValue("@iPeriodo", vacaciones.iPeriodo);
                    cmd.Parameters.AddWithValue("@bEstado", vacaciones.bEstado);
                    cmd.Parameters.AddWithValue("@vAuditCreacion", vacaciones.vAuditCreacion);

                    SqlParameter outputParam = new SqlParameter("@iCodVacaciones", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    cmd.ExecuteNonQuery();

                    // Retornar el ID generado
                    vacaciones.iCodVacaciones = (int)outputParam.Value;

                    // Insertar archivos si existen
                    foreach (var archivo in vacaciones.Archivos)
                    {
                        SqlCommand cmdArchivo = new SqlCommand("pa_InsertarVacacionesArchivo", connection, transaction)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmdArchivo.Parameters.AddWithValue("@iCodVacaciones", vacaciones.iCodVacaciones);
                        cmdArchivo.Parameters.AddWithValue("@iCodTipoVacacionesFormato", archivo.iCodTipoVacacionesFormato);
                        cmdArchivo.Parameters.AddWithValue("@vObservaciones", archivo.vObservaciones);
                        cmdArchivo.Parameters.AddWithValue("@vUrlArchivo", archivo.vUrlArchivo);
                        cmdArchivo.Parameters.AddWithValue("@bEstado", archivo.bEstado);
                        cmdArchivo.Parameters.AddWithValue("@vAuditCreacion", archivo.vAuditCreacion);
                        cmdArchivo.ExecuteNonQuery();
                    }

                    // Insertar proceso si existe (en lugar de un foreach, ahora solo se usa un único registro)
                    var proceso = new VacacionesProceso();
                    proceso.iCodEstadoProceso = (int)EnumMaeEstadoProceso.PENDIENTE;
                    vacaciones.iCodEstadoProceso = proceso.iCodEstadoProceso;
                    proceso.bEstado = true;
                    proceso.vAuditCreacion = vacaciones.vAuditCreacion;
                    proceso.iCodVacacionesProceso = vacaciones.iCodVacaciones; // Relacionar el proceso con la justificación insertada
                    proceso.vComentario = vacaciones.vDescripcion;
                    proceso.vAuditCreacion = vacaciones.vAuditCreacion;
                    if (proceso != null)
                    {
                        SqlCommand cmdProceso = new SqlCommand("pa_InsertarVacacionesProceso", connection, transaction)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmdProceso.Parameters.AddWithValue("@iCodVacaciones", vacaciones.iCodVacaciones);
                        cmdProceso.Parameters.AddWithValue("@iCodEstadoProceso", proceso.iCodEstadoProceso);
                        cmdProceso.Parameters.AddWithValue("@vComentario", proceso.vComentario);
                        cmdProceso.Parameters.AddWithValue("@bEstado", proceso.bEstado);
                        cmdProceso.Parameters.AddWithValue("@vAuditCreacion", proceso.vAuditCreacion);
                        cmdProceso.ExecuteNonQuery();
                    }

                    // Commit de la transacción
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Si ocurre un error, deshacer los cambios realizados
                    transaction.Rollback();
                    throw;
                }
                return vacaciones;
            }
        }

        // Método para Actualizar Vacaciones
        public VacacionesTrabajador_Registro ActualizarVacaciones(VacacionesTrabajador_Registro vacaciones)
        {
            using (SqlConnection connection = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion().Connection)
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Actualizar en Vacaciones
                    SqlCommand cmd = new SqlCommand("pa_ActualizarVacaciones", connection, transaction)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@iCodVacaciones", vacaciones.iCodVacaciones);
                    cmd.Parameters.AddWithValue("@iCodTipoVacaciones", vacaciones.iCodTipoVacaciones);
                    cmd.Parameters.AddWithValue("@iCodTrabajador", vacaciones.iCodTrabajador);
                    cmd.Parameters.AddWithValue("@iCodigoDependencia", vacaciones.iCodigoDependencia);
                    cmd.Parameters.AddWithValue("@iCodEstadoProceso", vacaciones.iCodEstadoProceso);
                    cmd.Parameters.AddWithValue("@bFraccionamientoVacacionalMediaJornada", vacaciones.bFraccionamientoVacacionalMediaJornada);
                    cmd.Parameters.AddWithValue("@dtFechaInicio", vacaciones.dtFechaInicio);
                    cmd.Parameters.AddWithValue("@dtFechaFin", vacaciones.dtFechaFin);
                    cmd.Parameters.AddWithValue("@vDescripcion", vacaciones.vDescripcion);
                    cmd.Parameters.AddWithValue("@iDisponibles", vacaciones.iDisponibles);
                    cmd.Parameters.AddWithValue("@iAsignados", vacaciones.iAsignados);
                    cmd.Parameters.AddWithValue("@iPeriodo", vacaciones.iPeriodo);
                    cmd.Parameters.AddWithValue("@bEstado", vacaciones.bEstado);
                    cmd.Parameters.AddWithValue("@vAuditModificacion", vacaciones.vAuditModificacion);

                    cmd.ExecuteNonQuery();

                    // Actualizar archivos si existen
                    foreach (var archivo in vacaciones.Archivos)
                    {
                        if (archivo.iCodVacacionesArchivos == 0)
                        {
                            SqlCommand cmdArchivo = new SqlCommand("pa_InsertarVacacionesArchivo", connection, transaction)
                            {
                                CommandType = CommandType.StoredProcedure
                            };
                            cmdArchivo.Parameters.AddWithValue("@iCodVacaciones", vacaciones.iCodVacaciones);
                            cmdArchivo.Parameters.AddWithValue("@iCodTipoVacacionesFormato", archivo.iCodTipoVacacionesFormato);
                            cmdArchivo.Parameters.AddWithValue("@vObservaciones", archivo.vObservaciones);
                            cmdArchivo.Parameters.AddWithValue("@vUrlArchivo", archivo.vUrlArchivo);
                            cmdArchivo.Parameters.AddWithValue("@bEstado", archivo.bEstado);
                            cmdArchivo.Parameters.AddWithValue("@vAuditCreacion", archivo.vAuditCreacion);
                            cmdArchivo.ExecuteNonQuery();
                        }
                        else
                        {
                            SqlCommand cmdArchivo = new SqlCommand("pa_ActualizarVacacionesArchivo", connection, transaction)
                            {
                                CommandType = CommandType.StoredProcedure
                            };
                            cmdArchivo.Parameters.AddWithValue("@iCodVacacionesArchivos", archivo.iCodVacacionesArchivos);
                            cmdArchivo.Parameters.AddWithValue("@iCodVacaciones", vacaciones.iCodVacaciones);
                            cmdArchivo.Parameters.AddWithValue("@iCodTipoVacacionesFormato", archivo.iCodTipoVacacionesFormato);
                            cmdArchivo.Parameters.AddWithValue("@vObservaciones", archivo.vObservaciones);
                            cmdArchivo.Parameters.AddWithValue("@vUrlArchivo", archivo.vUrlArchivo);
                            cmdArchivo.Parameters.AddWithValue("@bEstado", archivo.bEstado);
                            cmdArchivo.Parameters.AddWithValue("@vAuditModificacion", vacaciones.vAuditModificacion);

                            cmdArchivo.ExecuteNonQuery();
                        }
                    }

                    // Actualizar proceso si existe (en lugar de un foreach, ahora solo se usa un único registro)
                    var proceso = new VacacionesProceso();
                    proceso.iCodEstadoProceso = (int)vacaciones.iCodEstadoProceso;
                    proceso.bEstado = true;
                    proceso.vAuditModificacion = vacaciones.vAuditModificacion;
                    proceso.iCodVacaciones = vacaciones.iCodVacaciones; // Relacionar el proceso con la justificación insertada
                    proceso.vComentario = vacaciones.vDescripcion;
                    if (proceso != null)
                    {
                        SqlCommand cmdProceso = new SqlCommand("pa_InsertarVacacionesProceso", connection, transaction)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmdProceso.Parameters.AddWithValue("@iCodVacaciones", vacaciones.iCodVacaciones);
                        cmdProceso.Parameters.AddWithValue("@iCodEstadoProceso", proceso.iCodEstadoProceso);
                        cmdProceso.Parameters.AddWithValue("@vComentario", proceso.vComentario);
                        cmdProceso.Parameters.AddWithValue("@bEstado", proceso.bEstado);
                        cmdProceso.Parameters.AddWithValue("@vAuditCreacion", vacaciones.vAuditCreacion);
                        cmdProceso.ExecuteNonQuery();
                    }

                    // Commit de la transacción
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Si ocurre un error, deshacer los cambios realizados
                    transaction.Rollback();
                    throw ex;
                }
                return vacaciones;
            }
        }

        public IEnumerable<VacacionesPeriodo_Registro> ListarVacacionesPeriodo(VacacionesPeriodo_Registro request)
        {
            List<VacacionesPeriodo_Registro> lista = new List<VacacionesPeriodo_Registro>();
            VacacionesPeriodo_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[pa_VacacionesPeriodoTrabajadorConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodigoDependencia", request.iCodigoDependencia));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", request.iCodTrabajador));


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            /*
                             * iCodVacacionesPeriodo, iCodTrabajador, iCodigoDependencia, 
		iPeriodo, dtFechaInicio, dtFechaFin, iProgramados, 
		iAsignados, iDisponibles, iFraccionamiento, dtAuditCreacion, 
		vAuditCreacion, dtAuditModificacion, vAuditModificacion
                            */
                            item = new VacacionesPeriodo_Registro();

                            item.Grilla = new Grilla_Response();
                            item.iCodVacacionesPeriodo = Types.CheckDefaultValue<Int32>(dr["iCodVacacionesPeriodo"]);
                            item.iCodTrabajador = Types.CheckDefaultValue<Int32>(dr["iCodTrabajador"]);
                            item.iCodigoDependencia = Types.CheckDefaultValue<Int32>(dr["iCodigoDependencia"]);
                            item.iPeriodo = Types.CheckDefaultValue<int>(dr["iPeriodo"]);
                            item.dtFechaInicio = Types.CheckDefaultValue<DateTime>(dr["dtFechaInicio"]);
                            item.dtFechaFin = Types.CheckDefaultValue<DateTime>(dr["dtFechaFin"]);


                            item.iProgramados = Types.CheckDefaultValue<decimal>(dr["iProgramados"]);
                            item.iAsignados = Types.CheckDefaultValue<decimal>(dr["iAsignados"]);

                            item.iDisponibles = Types.CheckDefaultValue<decimal>(dr["iDisponibles"]);
                            item.iFraccionamiento = Types.CheckDefaultValue<decimal>(dr["iFraccionamiento"]);
                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public VacacionesTrabajador_Registro AprobarVacaciones(VacacionesTrabajador_Registro request)
        {
            using (SqlConnection connection = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion().Connection)
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    request.iCodEstadoProceso = request.adminJefe ? (int)EnumMaeEstadoProceso.APROBADO_POR_ADMINISTRADOR : (int)EnumMaeEstadoProceso.APROBADO_POR_JEFE;
                    // Actualizar Justificaciones
                    ActualizarAprobarDenegarVacaciones(request, connection, transaction);

                    // Actualizar Procesos relacionados
                    var proceso = new VacacionesProceso();
                    proceso.iCodEstadoProceso = request.adminJefe ? (int)EnumMaeEstadoProceso.APROBADO_POR_ADMINISTRADOR : (int)EnumMaeEstadoProceso.APROBADO_POR_JEFE;
                    proceso.bEstado = true;
                    proceso.vAuditModificacion = request.vAuditModificacion;
                    proceso.iCodVacaciones = request.iCodVacaciones; // Relacionar el proceso con la justificación insertada
                    proceso.vComentario = request.vDescripcion;
                    proceso.vAuditCreacion = request.vAuditModificacion;
                    InsertarVacacionesProceso(connection, transaction, proceso);

                    // Si todo fue exitoso, commit de la transacción
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Si ocurre algún error, hacer rollback
                    transaction.Rollback();
                    throw new Exception("Error al actualizar la justificación", ex);
                }
                return request;
            }
        }
        public VacacionesTrabajador_Registro DenegarVacaciones(VacacionesTrabajador_Registro request)
        {
            using (SqlConnection connection = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion().Connection)
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    request.iCodEstadoProceso = request.adminJefe ? (int)EnumMaeEstadoProceso.RECHAZADO_POR_ADMINISTRADOR : (int)EnumMaeEstadoProceso.RECHAZADO_POR_JEFE;
                    // Actualizar Justificaciones
                    ActualizarAprobarDenegarVacaciones(request, connection, transaction);

                    // Actualizar Procesos relacionados
                    var proceso = new VacacionesProceso();
                    proceso.iCodEstadoProceso = request.adminJefe ? (int)EnumMaeEstadoProceso.RECHAZADO_POR_ADMINISTRADOR : (int)EnumMaeEstadoProceso.RECHAZADO_POR_JEFE;
                    proceso.bEstado = true;
                    proceso.vAuditModificacion = request.vAuditModificacion;
                    proceso.iCodVacaciones = request.iCodVacaciones; // Relacionar el proceso con la justificación insertada
                    proceso.vComentario = request.vDescripcion;
                    proceso.vAuditCreacion = request.vAuditModificacion;
                    InsertarVacacionesProceso(connection, transaction, proceso);

                    // Si todo fue exitoso, commit de la transacción
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Si ocurre algún error, hacer rollback
                    transaction.Rollback();
                    throw new Exception("Error al actualizar la justificación", ex);
                }
                return request;
            }
        }

        public VacacionesTrabajador_Registro AprobarVacacionesMas(VacacionesTrabajador_Registro request)
        {
            using (SqlConnection connection = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion().Connection)
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    request.iCodEstadoProceso = request.adminJefe ? (int)EnumMaeEstadoProceso.APROBADO_POR_ADMINISTRADOR : (int)EnumMaeEstadoProceso.APROBADO_POR_JEFE;
                    // Actualizar Justificaciones
                    using (SqlCommand cmd = new SqlCommand("pa_ActualizarAprobarDenegarVacacionesMas", connection, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@iCodEstadoProceso", request.iCodEstadoProceso);
                        cmd.Parameters.AddWithValue("@vDescripcion", request.vDescripcion);
                        cmd.Parameters.AddWithValue("@vAuditModificacion", request.vAuditModificacion);
                        cmd.Parameters.AddWithValue("@vIds", request.vIds);

                        cmd.ExecuteNonQuery();
                    }

                    request.resMasivo = true;

                    // Si todo fue exitoso, commit de la transacción
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Si ocurre algún error, hacer rollback
                    transaction.Rollback();
                    throw new Exception("Error al actualizar la justificación", ex);
                }
                return request;
            }
        }
        public VacacionesTrabajador_Registro DenegarVacacionesMas(VacacionesTrabajador_Registro request)
        {
            using (SqlConnection connection = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion().Connection)
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    request.iCodEstadoProceso = request.adminJefe ? (int)EnumMaeEstadoProceso.RECHAZADO_POR_ADMINISTRADOR : (int)EnumMaeEstadoProceso.RECHAZADO_POR_JEFE;
                    // Actualizar Justificaciones
                    using (SqlCommand cmd = new SqlCommand("pa_ActualizarAprobarDenegarVacacionesMas", connection, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@iCodEstadoProceso", request.iCodEstadoProceso);
                        cmd.Parameters.AddWithValue("@vDescripcion", request.vDescripcion);
                        cmd.Parameters.AddWithValue("@vAuditModificacion", request.vAuditModificacion);
                        cmd.Parameters.AddWithValue("@vIds", request.vIds);

                        cmd.ExecuteNonQuery();
                    }

                    request.resMasivo = true;

                    // Si todo fue exitoso, commit de la transacción
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Si ocurre algún error, hacer rollback
                    transaction.Rollback();
                    throw new Exception("Error al actualizar la justificación", ex);
                }
                return request;
            }
        }
        private static void ActualizarAprobarDenegarVacaciones(VacacionesTrabajador_Registro vacaciones, SqlConnection connection, SqlTransaction transaction)
        {
            using (SqlCommand cmd = new SqlCommand("pa_ActualizarAprobarDenegarVacaciones", connection, transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iCodVacaciones", vacaciones.iCodVacaciones);
                cmd.Parameters.AddWithValue("@iCodEstadoProceso", vacaciones.iCodEstadoProceso);
                cmd.Parameters.AddWithValue("@vDescripcion", vacaciones.vDescripcion);
                cmd.Parameters.AddWithValue("@vAuditModificacion", vacaciones.vAuditModificacion);

                cmd.ExecuteNonQuery();
            }
        }

        private void InsertarVacacionesProceso(SqlConnection conn, SqlTransaction transaction, VacacionesProceso proceso)
        {
            using (SqlCommand cmd = new SqlCommand("dbo.InsertarVacacionesProceso", conn, transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@iCodVacaciones", proceso.iCodVacaciones);
                cmd.Parameters.AddWithValue("@iCodEstadoProceso", proceso.iCodEstadoProceso);
                cmd.Parameters.AddWithValue("@bEstado", proceso.bEstado);
                cmd.Parameters.AddWithValue("@vComentario", proceso.vComentario);
                cmd.Parameters.AddWithValue("@vAuditCreacion", proceso.vAuditCreacion);
                //cmd.Parameters.AddWithValue("@dtAuditModificacion", proceso.dtAuditModificacion);
                //cmd.Parameters.AddWithValue("@vAuditModificacion", proceso.vAuditModificacion);

                SqlParameter outputParam = new SqlParameter("@iCodVacacionesProceso", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputParam);

                cmd.ExecuteNonQuery();
                proceso.iCodVacacionesProceso = (int)outputParam.Value;
            }
        }

        public IEnumerable<VacacionesProceso> ListarVacacionesProcesoHistorial(int iCodVacaciones)
        {
            List<VacacionesProceso> lista = new List<VacacionesProceso>();
            VacacionesProceso item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[pa_VacacionesProcesoHistorialporId]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodVacaciones", iCodVacaciones));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new VacacionesProceso();

                            item.vComentario = Types.CheckDefaultValue<string>(dr["vComentario"]);
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
