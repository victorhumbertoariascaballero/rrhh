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
    public class T_genm_compensaciones_ODA
    {
        BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWork = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings["cnnSQL"].ConnectionString));
        public List<Compensaciones_Registro> ListarCompensaciones(CompensacionesTrabajador_Request request)
        {
            var compensaciones = new List<Compensaciones_Registro>();

            using (var connection = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion().Connection)
            {
                connection.Open();
                using (var cmd = new SqlCommand("pa_CompensacionTrabajadorConsultar", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Añadimos los parámetros con valores
                    cmd.Parameters.AddWithValue("@iCodTrabajador", request.iCodTrabajador);
                    cmd.Parameters.AddWithValue("@iCodigoDependencia", request.iCodigoDependencia);

                    if (request.fecha == "")
                        cmd.Parameters.Add(new SqlParameter("@fecha", DBNull.Value));
                    else
                        cmd.Parameters.Add(new SqlParameter("@fecha", Convert.ToDateTime(request.fecha)));

                    cmd.Parameters.AddWithValue("@iCodEstadoProceso", request.iCodEstadoProceso);

                    // Ejecutamos la consulta y leemos los resultados
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var compensacion = new Compensaciones_Registro
                            {
                                ICodCompensaciones = reader.GetInt32(reader.GetOrdinal("iCodCompensaciones")),
                                ICodTrabajador = reader.GetInt32(reader.GetOrdinal("iCodTrabajador")),
                                ICodigoDependencia = reader.GetInt32(reader.GetOrdinal("iCodigoDependencia")),
                                ICodEstadoProceso = reader.GetInt32(reader.GetOrdinal("iCodEstadoProceso")),
                                Horas = reader.GetInt32(reader.GetOrdinal("horas")),
                                Exacto = reader.GetBoolean(reader.GetOrdinal("exacto")),
                                DtFechaCompensacion = reader.GetDateTime(reader.GetOrdinal("dtFechaCompensacion")),
                                VDescripcion = reader.IsDBNull(reader.GetOrdinal("vDescripcion")) ? null : reader.GetString(reader.GetOrdinal("vDescripcion")),
                                BEstado = reader.GetBoolean(reader.GetOrdinal("bEstado")),
                                DtAuditCreacion = reader.GetDateTime(reader.GetOrdinal("dtAuditCreacion")),
                                VAuditCreacion = reader.IsDBNull(reader.GetOrdinal("vAuditCreacion")) ? null : reader.GetString(reader.GetOrdinal("vAuditCreacion")),
                                DtAuditModificacion = reader.IsDBNull(reader.GetOrdinal("dtAuditModificacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtAuditModificacion")),
                                VAuditModificacion = reader.IsDBNull(reader.GetOrdinal("vAuditModificacion")) ? null : reader.GetString(reader.GetOrdinal("vAuditModificacion")),
                                vDependencia = reader.IsDBNull(reader.GetOrdinal("vDependencia")) ? null : reader.GetString(reader.GetOrdinal("vDependencia")),
                                vNombreTrabajador = reader.GetString(reader.GetOrdinal("vNombreTrabajador")),
                                vNumeroDocumento = reader.GetString(reader.GetOrdinal("vNumeroDocumento")),
                                vEstadoProceso = reader.GetString(reader.GetOrdinal("vEstadoProceso")),

                                dFechaAprobadoJefe = reader.IsDBNull(reader.GetOrdinal("dFechaAprobadoJefe")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dFechaAprobadoJefe")),
                                dFechaAprobadoAdmin = reader.IsDBNull(reader.GetOrdinal("dFechaAprobadoAdmin")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dFechaAprobadoAdmin")),
                                dFechaDenegadoAdmin = reader.IsDBNull(reader.GetOrdinal("dFechaDenegadoAdmin")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dFechaDenegadoAdmin")),

                            };
                            if ((int)EnumMaeEstadoProceso.APROBADO_POR_ADMINISTRADOR == compensacion.ICodEstadoProceso)
                                compensacion.aprobadoAdmin = true;
                            compensaciones.Add(compensacion);
                        }
                    }
                }
            }

            return compensaciones;
        }

        public List<Compensaciones_Registro> ListarCompensacionesAptas(CompensacionesTrabajador_Request request)
        {
            var compensaciones = new List<Compensaciones_Registro>();

            using (var connection = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion().Connection)
            {
                connection.Open();
                using (var cmd = new SqlCommand("pa_CompensacionTrabajadorCompensarConsultar", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Añadimos los parámetros con valores
                    cmd.Parameters.AddWithValue("@iCodTrabajador", request.iCodTrabajador);
                    cmd.Parameters.AddWithValue("@iMes", request.iMes);
                    cmd.Parameters.AddWithValue("@iAnio", request.iAnio);

                    // Ejecutamos la consulta y leemos los resultados
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var compensacion = new Compensaciones_Registro
                            {
                                ICodCompensaciones = reader.GetInt32(reader.GetOrdinal("iCodCompensaciones")),
                                ICodTrabajador = reader.GetInt32(reader.GetOrdinal("iCodTrabajador")),
                                ICodigoDependencia = reader.GetInt32(reader.GetOrdinal("iCodigoDependencia")),
                                ICodEstadoProceso = reader.GetInt32(reader.GetOrdinal("iCodEstadoProceso")),
                                Horas = reader.GetInt32(reader.GetOrdinal("horas")),
                                Exacto = reader.GetBoolean(reader.GetOrdinal("exacto")),
                                DtFechaCompensacion = reader.GetDateTime(reader.GetOrdinal("dtFechaCompensacion")),
                                VDescripcion = reader.IsDBNull(reader.GetOrdinal("vDescripcion")) ? null : reader.GetString(reader.GetOrdinal("vDescripcion")),
                                BEstado = reader.GetBoolean(reader.GetOrdinal("bEstado")),
                                DtAuditCreacion = reader.GetDateTime(reader.GetOrdinal("dtAuditCreacion")),
                                VAuditCreacion = reader.IsDBNull(reader.GetOrdinal("vAuditCreacion")) ? null : reader.GetString(reader.GetOrdinal("vAuditCreacion")),
                                DtAuditModificacion = reader.IsDBNull(reader.GetOrdinal("dtAuditModificacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtAuditModificacion")),
                                VAuditModificacion = reader.IsDBNull(reader.GetOrdinal("vAuditModificacion")) ? null : reader.GetString(reader.GetOrdinal("vAuditModificacion")),
                                vDependencia = reader.IsDBNull(reader.GetOrdinal("vDependencia")) ? null : reader.GetString(reader.GetOrdinal("vDependencia")),
                                vNombreTrabajador = reader.GetString(reader.GetOrdinal("vNombreTrabajador")),
                                vNumeroDocumento = reader.GetString(reader.GetOrdinal("vNumeroDocumento")),
                                vEstadoProceso = reader.GetString(reader.GetOrdinal("vEstadoProceso")),

                                dFechaAprobadoJefe = reader.IsDBNull(reader.GetOrdinal("dFechaAprobadoJefe")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dFechaAprobadoJefe")),
                                dFechaAprobadoAdmin = reader.IsDBNull(reader.GetOrdinal("dFechaAprobadoAdmin")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dFechaAprobadoAdmin")),

                            };
                            if ((int)EnumMaeEstadoProceso.APROBADO_POR_ADMINISTRADOR == compensacion.ICodEstadoProceso)
                                compensacion.aprobadoAdmin = true;
                            compensaciones.Add(compensacion);
                        }
                    }
                }
            }

            return compensaciones;
        }

        public List<CompensacionesCalendarioTrabajador_Registro> ListarCompensacionesCalendarAptas(CompensacionesTrabajador_Request request)
        {
            var compensaciones = new List<CompensacionesCalendarioTrabajador_Registro>();

            using (var connection = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion().Connection)
            {
                connection.Open();
                using (var cmd = new SqlCommand("pa_CompensacionCalendarioTrabajador", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Añadimos los parámetros con valores
                    cmd.Parameters.AddWithValue("@iCodTrabajador", request.iCodTrabajador);
                    cmd.Parameters.AddWithValue("@iMes", request.iMes);
                    cmd.Parameters.AddWithValue("@iAnio", request.iAnio);

                    // Ejecutamos la consulta y leemos los resultados
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            /*
                             * iCodCompensacionesCalendario, iCodTrabajador, iCodigoDependencia, dtFecha, 
		dMinCompDisponibles, bEstado, dtAuditCreacion, vAuditCreacion, 
		dtAuditModificacion, vAuditModificacion
                             * */
                            var compensacion = new CompensacionesCalendarioTrabajador_Registro
                            {
                                iCodCompensacionesCalendario = reader.GetInt32(reader.GetOrdinal("iCodCompensacionesCalendario")),
                                iCodTrabajador = reader.GetInt32(reader.GetOrdinal("iCodTrabajador")),
                                iCodigoDependencia = reader.GetInt32(reader.GetOrdinal("iCodigoDependencia")),

                                dtFecha = reader.IsDBNull(reader.GetOrdinal("dtFecha")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtFecha")),
                                dMinCompDisponibles = reader.GetDecimal(reader.GetOrdinal("dMinCompDisponibles")),
                                bEstado = reader.GetBoolean(reader.GetOrdinal("bEstado")),
                                dtAuditCreacion = reader.GetDateTime(reader.GetOrdinal("dtAuditCreacion")),
                                vAuditCreacion = reader.IsDBNull(reader.GetOrdinal("vAuditCreacion")) ? null : reader.GetString(reader.GetOrdinal("vAuditCreacion")),
                                dtAuditModificacion = reader.IsDBNull(reader.GetOrdinal("dtAuditModificacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtAuditModificacion")),
                                vAuditModificacion = reader.IsDBNull(reader.GetOrdinal("vAuditModificacion")) ? null : reader.GetString(reader.GetOrdinal("vAuditModificacion")),
                            };
                            compensaciones.Add(compensacion);
                        }
                    }
                }
            }

            return compensaciones;
        }
        public object ValidarHorasRegistradasEnCompensacionDetalle(int iCodCompensaciones)
        {
            throw new NotImplementedException();
        }

        public CompensacionesTrabajador_Registro ObtenerCompensacionPorId(int iCodCompensaciones)
        {
            CompensacionesTrabajador_Registro justificacion = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

                // Obtener Justificación
                cmd.CommandText = "[dbo].[pa_CompensacionTrabajadorConsultarporId]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@iCodCompensaciones", iCodCompensaciones));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        dr.Read();
                        justificacion = new CompensacionesTrabajador_Registro
                        {
                            ICodCompensaciones = Types.CheckDefaultValue<int>(dr["iCodCompensaciones"]),
                            ICodTrabajador = Types.CheckDefaultValue<int?>(dr["iCodTrabajador"]),
                            Exacto = Types.CheckDefaultValue<bool?>(dr["exacto"]),
                            Horas = Types.CheckDefaultValue<int?>(dr["horas"]),
                            DtFechaCompensacion = Types.CheckDefaultValue<DateTime?>(dr["dtFechaCompensacion"]),
                            VDescripcion = Types.CheckDefaultValue<string>(dr["vDescripcion"]),
                            ICodEstadoProceso = Types.CheckDefaultValue<int?>(dr["iCodEstadoProceso"]),
                            IHoras = Types.CheckDouble(dr["iHoras"]),
                        };
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return justificacion;
        }

        public CompensacionesTrabajador_Registro InsertarCompensacion(CompensacionesTrabajador_Registro request)
        {
            using (var connection = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion().Connection)
            {
                connection.Open();

                // Iniciamos la transacción
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var proceso = new CompensacionesProceso();
                        proceso.ICodEstadoProceso = (int)EnumMaeEstadoProceso.PENDIENTE;
                        request.ICodEstadoProceso = proceso.ICodEstadoProceso;
                        // Inserción de la compensación
                        var iCodCompensaciones = InsertarCompensacionCore(request, connection, transaction);

                        // Ahora insertamos los procesos relacionados

                        // Insertar los procesos relacionados                    
                        proceso.BEstado = true;
                        proceso.VAuditCreacion = request.VAuditCreacion;
                        proceso.ICodCompensaciones = iCodCompensaciones; // Relacionar el proceso con la justificación insertada
                        proceso.VComentario = request.VDescripcion;
                        proceso.VAuditCreacion = request.VAuditCreacion;
                        InsertarCompensacionesProcesos(proceso, iCodCompensaciones, connection, transaction);

                        // Si todo ha ido bien, confirmamos la transacción
                        transaction.Commit();

                        if (iCodCompensaciones > 0)
                        {
                            request.ICodCompensaciones = iCodCompensaciones;
                            return request;
                        }
                        else
                        {
                            transaction.Rollback();
                            throw new Exception("Error al insertar los datos: ");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Si hay algún error, hacemos un rollback
                        transaction.Rollback();
                        throw new Exception("Error al insertar la compensación y sus procesos.", ex);
                    }
                }
            }
        }

        // Método auxiliar para insertar la compensación
        private int InsertarCompensacionCore(CompensacionesTrabajador_Registro compensacion, SqlConnection connection, SqlTransaction transaction)
        {
            using (var command = new SqlCommand("pa_InsertarCompensacion", connection, transaction))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ICodTrabajador", compensacion.ICodTrabajador ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ICodigoDependencia", compensacion.ICodigoDependencia ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ICodEstadoProceso", compensacion.ICodEstadoProceso ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Horas", compensacion.Horas ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Exacto", compensacion.Exacto ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@DtFechaCompensacion", compensacion.DtFechaCompensacion ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@VDescripcion", compensacion.VDescripcion ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@BEstado", compensacion.BEstado ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@VAuditCreacion", compensacion.VAuditCreacion ?? (object)DBNull.Value);

                // Ejecutar el procedimiento y obtener el ID generado
                int iCodCompensaciones = Convert.ToInt32(command.ExecuteScalar());

                return iCodCompensaciones;
            }
        }

        // Método auxiliar para insertar los procesos de compensación
        private void InsertarCompensacionesProcesos(CompensacionesProceso proceso, int codCompensacion, SqlConnection connection, SqlTransaction transaction)
        {
            using (var command = new SqlCommand("pa_InsertarCompensacionesProceso", connection, transaction))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ICodCompensaciones", codCompensacion);
                command.Parameters.AddWithValue("@ICodEstadoProceso", proceso.ICodEstadoProceso ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@VComentario", proceso.VComentario ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@BEstado", proceso.BEstado ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@VAuditCreacion", proceso.VAuditCreacion ?? (object)DBNull.Value);

                command.ExecuteNonQuery();
            }
        }

        // Método para actualizar una compensación dentro de una transacción
        public CompensacionesTrabajador_Registro ActualizarCompensacion(CompensacionesTrabajador_Registro request)
        {
            using (var connection = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion().Connection)
            {
                connection.Open();

                // Iniciamos la transacción
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Actualizamos la compensación
                        ActualizarCompensacionCore(request, connection, transaction);


                        // Actualizar Procesos relacionados
                        var proceso = new CompensacionesProceso();
                        proceso.ICodEstadoProceso = (int)request.ICodEstadoProceso;
                        proceso.BEstado = true;
                        proceso.VAuditModificacion = request.VAuditModificacion;
                        proceso.VAuditCreacion = request.VAuditCreacion;
                        proceso.ICodCompensaciones = request.ICodCompensaciones; // Relacionar el proceso con la justificación insertada
                        proceso.VComentario = request.VDescripcion;
                        // Ahora actualizamos los procesos relacionados
                        ActualizarCompensacionesProcesos(proceso, request.ICodCompensaciones, connection, transaction);

                        // Si todo ha ido bien, confirmamos la transacción
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // Si hay algún error, hacemos un rollback
                        transaction.Rollback();
                        throw new Exception("Error al actualizar la compensación y sus procesos.", ex);
                    }
                }
                return request;
            }
        }

        // Método auxiliar para actualizar la compensación
        private void ActualizarCompensacionCore(CompensacionesTrabajador_Registro compensacion, SqlConnection connection, SqlTransaction transaction)
        {
            using (var command = new SqlCommand("pa_ActualizarCompensacion", connection, transaction))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ICodCompensaciones", compensacion.ICodCompensaciones);
                command.Parameters.AddWithValue("@ICodTrabajador", compensacion.ICodTrabajador ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ICodigoDependencia", compensacion.ICodigoDependencia ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ICodEstadoProceso", compensacion.ICodEstadoProceso ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Horas", compensacion.Horas ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Exacto", compensacion.Exacto ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@DtFechaCompensacion", compensacion.DtFechaCompensacion ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@VDescripcion", compensacion.VDescripcion ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@BEstado", compensacion.BEstado ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@VAuditModificacion", compensacion.VAuditModificacion ?? (object)DBNull.Value);

                command.ExecuteNonQuery();
            }
        }

        // Método auxiliar para actualizar los procesos de compensación
        private void ActualizarCompensacionesProcesos(CompensacionesProceso proceso, int codCompensacion, SqlConnection connection, SqlTransaction transaction)
        {
            if (proceso.ICodCompensacionesProceso == 0)
            {
                using (var command = new SqlCommand("pa_InsertarCompensacionesProceso", connection, transaction))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ICodCompensaciones", codCompensacion);
                    command.Parameters.AddWithValue("@ICodEstadoProceso", proceso.ICodEstadoProceso ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@VComentario", proceso.VComentario ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@BEstado", proceso.BEstado ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@VAuditCreacion", proceso.VAuditCreacion ?? (object)DBNull.Value);

                    command.ExecuteNonQuery();
                }
            }
            else
            {
                using (var command = new SqlCommand("pa_ActualizarCompensacionesProceso", connection, transaction))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ICodCompensacionesProceso", proceso.ICodCompensacionesProceso);
                    command.Parameters.AddWithValue("@ICodCompensaciones", codCompensacion);
                    command.Parameters.AddWithValue("@ICodEstadoProceso", proceso.ICodEstadoProceso ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@VComentario", proceso.VComentario ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@BEstado", proceso.BEstado ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@VAuditModificacion", proceso.VAuditModificacion ?? (object)DBNull.Value);

                    command.ExecuteNonQuery();
                }
            }
        }


        private static void ActualizarAprobarDenegarCompensacion(CompensacionesTrabajador_Registro compensacion, SqlConnection connection, SqlTransaction transaction)
        {
            using (SqlCommand cmd = new SqlCommand("pa_ActualizarAprobarDenegarCompensacion", connection, transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iCodCompensaciones", compensacion.ICodCompensaciones);
                cmd.Parameters.AddWithValue("@iCodEstadoProceso", compensacion.ICodEstadoProceso);
                cmd.Parameters.AddWithValue("@vDescripcion", compensacion.VDescripcion);
                cmd.Parameters.AddWithValue("@vAuditModificacion", compensacion.VAuditModificacion);

                cmd.ExecuteNonQuery();
            }
        }

        public CompensacionesTrabajador_Registro AprobarCompensacion(CompensacionesTrabajador_Registro request)
        {
            using (SqlConnection connection = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion().Connection)
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    request.ICodEstadoProceso = request.adminJefe ? (int)EnumMaeEstadoProceso.APROBADO_POR_ADMINISTRADOR : (int)EnumMaeEstadoProceso.APROBADO_POR_JEFE;
                    // Actualizar Justificaciones
                    ActualizarAprobarDenegarCompensacion(request, connection, transaction);

                    // Actualizar Procesos relacionados
                    var proceso = new CompensacionesProceso();
                    proceso.ICodEstadoProceso = request.adminJefe ? (int)EnumMaeEstadoProceso.APROBADO_POR_ADMINISTRADOR : (int)EnumMaeEstadoProceso.APROBADO_POR_JEFE;
                    proceso.BEstado = true;
                    proceso.VAuditModificacion = request.VAuditModificacion;
                    proceso.ICodCompensaciones = request.ICodCompensaciones; // Relacionar el proceso con la justificación insertada
                    proceso.VComentario = request.VDescripcion;
                    proceso.VAuditCreacion = request.VAuditModificacion;
                    InsertarCompensacionesProcesos(proceso, request.ICodCompensaciones, connection, transaction);

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
        public CompensacionesTrabajador_Registro DenegarCompensacion(CompensacionesTrabajador_Registro request)
        {
            using (SqlConnection connection = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion().Connection)
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    request.ICodEstadoProceso = request.adminJefe ? (int)EnumMaeEstadoProceso.RECHAZADO_POR_ADMINISTRADOR : (int)EnumMaeEstadoProceso.RECHAZADO_POR_JEFE;
                    // Actualizar Justificaciones
                    ActualizarAprobarDenegarCompensacion(request, connection, transaction);

                    // Actualizar Procesos relacionados
                    var proceso = new CompensacionesProceso();
                    proceso.ICodEstadoProceso = request.adminJefe ? (int)EnumMaeEstadoProceso.RECHAZADO_POR_ADMINISTRADOR : (int)EnumMaeEstadoProceso.RECHAZADO_POR_JEFE;
                    proceso.BEstado = true;
                    proceso.VAuditModificacion = request.VAuditModificacion;
                    proceso.ICodCompensaciones = request.ICodCompensaciones; // Relacionar el proceso con la justificación insertada
                    proceso.VComentario = request.VDescripcion;
                    proceso.VAuditCreacion = request.VAuditModificacion;
                    InsertarCompensacionesProcesos(proceso, request.ICodCompensaciones, connection, transaction);

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

        public CompensacionesTrabajador_Registro AprobarCompensacionMas(CompensacionesTrabajador_Registro request)
        {
            using (SqlConnection connection = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion().Connection)
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    request.ICodEstadoProceso = request.adminJefe ? (int)EnumMaeEstadoProceso.APROBADO_POR_ADMINISTRADOR : (int)EnumMaeEstadoProceso.APROBADO_POR_JEFE;
                    //me quede haciendo el aprobar y denegar masivo

                    using (SqlCommand cmd = new SqlCommand("pa_ActualizarAprobarDenegarCompensacionMas", connection, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@iCodEstadoProceso", request.ICodEstadoProceso);
                        cmd.Parameters.AddWithValue("@vDescripcion", request.VDescripcion);
                        cmd.Parameters.AddWithValue("@vAuditModificacion", request.VAuditModificacion);
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
        public CompensacionesTrabajador_Registro DenegarCompensacionMas(CompensacionesTrabajador_Registro request)
        {
            using (SqlConnection connection = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion().Connection)
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    request.ICodEstadoProceso = request.adminJefe ? (int)EnumMaeEstadoProceso.RECHAZADO_POR_ADMINISTRADOR : (int)EnumMaeEstadoProceso.RECHAZADO_POR_JEFE;
                    // Actualizar Justificaciones
                    using (SqlCommand cmd = new SqlCommand("pa_ActualizarAprobarDenegarCompensacionMas", connection, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@iCodEstadoProceso", request.ICodEstadoProceso);
                        cmd.Parameters.AddWithValue("@vDescripcion", request.VDescripcion);
                        cmd.Parameters.AddWithValue("@vAuditModificacion", request.VAuditModificacion);
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


        public bool InsertarCompensacionDetalle(List<CompensacionesDetalleTrabajador_Request> lista)
        {
            bool bolSuccess = false;
            using (SqlConnection connection = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion().Connection)
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    foreach (var compensacion in lista)
                    {
                        using (SqlCommand command = new SqlCommand("pa_InsertarCompensacionDetalle", connection, transaction))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@iCodCompensaciones", compensacion.ICodCompensaciones ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@iCodEstadoProceso", compensacion.iCodEstadoProceso ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@iCodigoDependencia", compensacion.iCodigoDependencia ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@iCodTrabajador", compensacion.iCodTrabajador ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@iNroDia", compensacion.INroDia ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@bDiaCompleto", compensacion.BDiaCompleto ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@iMinutos", compensacion.IMinutos ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@iHoras", compensacion.IHoras ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@dtFechaHorasExtra", compensacion.DtFechaHorasExtra ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@dtFecha", compensacion.DtFecha ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@dtFechaHoraIni", compensacion.DtFechaHoraIni ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@dtFechaHoraFin", compensacion.DtFechaHoraFin ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@vComentario", compensacion.VComentario ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@bEstado", compensacion.BEstado ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@vAuditCreacion", compensacion.VAuditCreacion ?? (object)DBNull.Value);

                            int ICodCompensacionesDetalle = Convert.ToInt32(command.ExecuteScalar());
                            foreach (var item in compensacion.DetalleFechas)
                            {                               
                                item.iCodCompensacionesDetalle = ICodCompensacionesDetalle;
                                item.vAuditCreacion = compensacion.VAuditCreacion;
                                item.bEstado = true;

                                InsertarCompensacionesDetalleFechas(item, connection, transaction);
                            }


                            CompensacionesDetalleProceso proceso = new CompensacionesDetalleProceso()
                            {
                                iCodCompensacionesDetalle = ICodCompensacionesDetalle,
                                ICodEstadoProceso = compensacion.iCodEstadoProceso,
                                VComentario = compensacion.VComentario,
                                VAuditCreacion = compensacion.VAuditCreacion,
                                BEstado = true
                            };
                            InsertarCompensacionesDetalleProcesos(proceso, connection, transaction);
                            bolSuccess = true;
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    bolSuccess = false;
                    throw new Exception("Error al actualizar la compensacion", ex);

                }

            }
            return bolSuccess;
        }

        private void InsertarCompensacionesDetalleProcesos(CompensacionesDetalleProceso proceso, SqlConnection connection, SqlTransaction transaction)
        {
            using (var command = new SqlCommand("pa_InsertarCompensacionesDetalleProceso", connection, transaction))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@iCodCompensacionesDetalle", proceso.iCodCompensacionesDetalle);
                command.Parameters.AddWithValue("@ICodEstadoProceso", proceso.ICodEstadoProceso ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@VComentario", proceso.VComentario ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@BEstado", proceso.BEstado ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@VAuditCreacion", proceso.VAuditCreacion ?? (object)DBNull.Value);

                command.ExecuteNonQuery();
            }
        }

        private void InsertarCompensacionesDetalleFechas(CompensacionesDetalleFechasTrabajador_Registro compensacionesDetalleFecha, SqlConnection connection, SqlTransaction transaction)
        {
            using (var command = new SqlCommand("pa_CompensacionesDetalleFechaInsertar", connection, transaction))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@iCodCompensacionesDetalle", compensacionesDetalleFecha.iCodCompensacionesDetalle);
                command.Parameters.AddWithValue("@iCodCompensacionesCalendario", compensacionesDetalleFecha.iCodCompensacionesCalendario);
                command.Parameters.AddWithValue("@iCodTrabajador", compensacionesDetalleFecha.iCodTrabajador);
                command.Parameters.AddWithValue("@iCodigoDependencia", compensacionesDetalleFecha.iCodigoDependencia);                
                command.Parameters.AddWithValue("@dtFecha", compensacionesDetalleFecha.dtFecha ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@dMinConsumidos", compensacionesDetalleFecha.dMinConsumidos);
                command.Parameters.AddWithValue("@dMinRestantes", compensacionesDetalleFecha.dMinRestantes);
                command.Parameters.AddWithValue("@bEstado", compensacionesDetalleFecha.bEstado ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@vAuditCreacion", compensacionesDetalleFecha.vAuditCreacion ?? (object)DBNull.Value);

                command.ExecuteNonQuery();
            }
        }
        public decimal ObtenerTotalHorasPorCompensacion(int iCodCompensaciones)
        {
            decimal totalHoras = 0;

            // Crear la conexión con la base de datos
            using (SqlConnection connection = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion().Connection)
            {
                try
                {
                    // Crear el comando para ejecutar el procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("pa_ObtenerTotalHorasPorCompensacion", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Agregar el parámetro al procedimiento almacenado
                        command.Parameters.AddWithValue("@iCodCompensaciones", iCodCompensaciones);

                        // Abrir la conexión
                        connection.Open();

                        // Ejecutar el procedimiento y obtener el resultado
                        var result = command.ExecuteScalar();

                        // Si el resultado no es null, asignarlo a totalHoras
                        if (result != DBNull.Value)
                        {
                            totalHoras = Convert.ToDecimal(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de errores
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return totalHoras;
        }

        public IEnumerable<CompensacionesProceso> ListarCompensacionesProcesoHistorial(int iCodCompensaciones)
        {
            List<CompensacionesProceso> lista = new List<CompensacionesProceso>();
            CompensacionesProceso item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[pa_CompensacionesProcesoHistorialporId]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodCompensaciones", iCodCompensaciones));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new CompensacionesProceso();

                            item.Grilla = new Grilla_Response();
                            item.VComentario = Types.CheckDefaultValue<string>(dr["vComentario"]);
                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public decimal GetMinutosXFecha(CompensacionesTrabajador_Request request)
        {
            decimal totalHoras = 0;
            using (SqlConnection connection = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion().Connection)
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("pa_CompensacionTrabajadorMinutosConsultar", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@iCodTrabajador", request.iCodTrabajador);
                        command.Parameters.AddWithValue("@dFecha", request.dFecha);
                        connection.Open();
                        var result = command.ExecuteScalar();

                        if (result != DBNull.Value)
                        {
                            totalHoras = Convert.ToDecimal(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de errores
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return totalHoras;
        }

        public List<CompensacionesDetalleTrabajador_Request> ListarCompensacionesDetalle(CompensacionesDetalleTrabajador_Request request)
        {
            var compensaciones = new List<CompensacionesDetalleTrabajador_Request>();

            using (var connection = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion().Connection)
            {
                connection.Open();
                using (var cmd = new SqlCommand("pa_CompensacionDetalleTrabajadorConsultar", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Añadimos los parámetros con valores
                    cmd.Parameters.AddWithValue("@iCodTrabajador", request.iCodTrabajador);
                    cmd.Parameters.AddWithValue("@iCodigoDependencia", request.iCodigoDependencia);

                    if (request.DtFecha == null)
                        cmd.Parameters.Add(new SqlParameter("@fecha", DBNull.Value));
                    else
                        cmd.Parameters.Add(new SqlParameter("@fecha", Convert.ToDateTime(request.DtFecha)));

                    cmd.Parameters.AddWithValue("@iCodEstadoProceso", request.iCodEstadoProceso);

                    // Ejecutamos la consulta y leemos los resultados
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //iCodCompensacionesDetalle	iCodCompensaciones	iNroDia	bDiaCompleto	iMinutos	iHoras	dtFechaHoraIni	dtFechaHoraFin
                            var compensacion = new CompensacionesDetalleTrabajador_Request
                            {
                                ICodCompensacionesDetalle = reader.GetInt32(reader.GetOrdinal("iCodCompensacionesDetalle")),
                                ICodCompensaciones = reader.GetInt32(reader.GetOrdinal("iCodCompensaciones")),
                                iCodTrabajador = reader.GetInt32(reader.GetOrdinal("iCodTrabajador")),
                                iCodigoDependencia = reader.GetInt32(reader.GetOrdinal("iCodigoDependencia")),
                                iCodEstadoProceso = reader.GetInt32(reader.GetOrdinal("iCodEstadoProceso")),
                                INroDia = reader.GetInt32(reader.GetOrdinal("iNroDia")),
                                IMinutos = reader.GetInt32(reader.GetOrdinal("iMinutos")),
                                IHoras = reader.GetDecimal(reader.GetOrdinal("iHoras")),
                                BDiaCompleto = reader.GetBoolean(reader.GetOrdinal("bDiaCompleto")),
                                DtFecha = reader.IsDBNull(reader.GetOrdinal("dtFecha")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtFecha")),
                                DtFechaHoraIni = reader.IsDBNull(reader.GetOrdinal("dtFechaHoraIni")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtFechaHoraIni")),
                                DtFechaHoraFin = reader.IsDBNull(reader.GetOrdinal("dtFechaHoraFin")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtFechaHoraFin")),
                                DtFechaHorasExtra = reader.IsDBNull(reader.GetOrdinal("dtFechaHorasExtra")) ? null : reader.GetString(reader.GetOrdinal("dtFechaHorasExtra")),

                                VComentario = reader.IsDBNull(reader.GetOrdinal("vComentario")) ? null : reader.GetString(reader.GetOrdinal("vComentario")),
                                BEstado = reader.GetBoolean(reader.GetOrdinal("bEstado")),
                                DtAuditCreacion = reader.GetDateTime(reader.GetOrdinal("dtAuditCreacion")),
                                VAuditCreacion = reader.IsDBNull(reader.GetOrdinal("vAuditCreacion")) ? null : reader.GetString(reader.GetOrdinal("vAuditCreacion")),
                                DtAuditModificacion = reader.IsDBNull(reader.GetOrdinal("dtAuditModificacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dtAuditModificacion")),
                                VAuditModificacion = reader.IsDBNull(reader.GetOrdinal("vAuditModificacion")) ? null : reader.GetString(reader.GetOrdinal("vAuditModificacion")),
                                vDependencia = reader.IsDBNull(reader.GetOrdinal("vDependencia")) ? null : reader.GetString(reader.GetOrdinal("vDependencia")),
                                vNombreTrabajador = reader.GetString(reader.GetOrdinal("vNombreTrabajador")),
                                vNumeroDocumento = reader.GetString(reader.GetOrdinal("vNumeroDocumento")),
                                vEstadoProceso = reader.GetString(reader.GetOrdinal("vEstadoProceso")),

                                dFechaAprobadoJefe = reader.IsDBNull(reader.GetOrdinal("dFechaAprobadoJefe")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dFechaAprobadoJefe")),
                                dFechaAprobadoAdmin = reader.IsDBNull(reader.GetOrdinal("dFechaAprobadoAdmin")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dFechaAprobadoAdmin")),
                                dFechaDenegadoAdmin = reader.IsDBNull(reader.GetOrdinal("dFechaDenegadoAdmin")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("dFechaDenegadoAdmin")),

                            };
                            compensaciones.Add(compensacion);
                        }
                    }
                }
            }

            return compensaciones;
        }

        public IEnumerable<CompensacionesDetalleProceso> ListarCompensacionesDetalleProcesoHistorial(int iCodCompensacionesDetalle)
        {
            List<CompensacionesDetalleProceso> lista = new List<CompensacionesDetalleProceso>();
            CompensacionesDetalleProceso item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[pa_CompensacionesDetalleProcesoHistorialporId]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodCompensacionesDetalle", iCodCompensacionesDetalle));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new CompensacionesDetalleProceso();

                            item.Grilla = new Grilla_Response();
                            item.VComentario = Types.CheckDefaultValue<string>(dr["vComentario"]);
                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        private static void ActualizarAprobarDenegarCompensacionDetalle(CompensacionesDetalleTrabajador_Request compensacion, SqlConnection connection, SqlTransaction transaction)
        {
            using (SqlCommand cmd = new SqlCommand("pa_ActualizarAprobarDenegarCompensacionDetalle", connection, transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iCodCompensacionesDetalle", compensacion.ICodCompensacionesDetalle);
                cmd.Parameters.AddWithValue("@iCodEstadoProceso", compensacion.iCodEstadoProceso);
                cmd.Parameters.AddWithValue("@vDescripcion", compensacion.VComentario);
                cmd.Parameters.AddWithValue("@vAuditModificacion", compensacion.VAuditModificacion);

                cmd.ExecuteNonQuery();
            }
        }

        public CompensacionesDetalleTrabajador_Request AprobarCompensacionDetalle(CompensacionesDetalleTrabajador_Request request)
        {
            using (SqlConnection connection = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion().Connection)
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    request.iCodEstadoProceso = request.adminJefe ? (int)EnumMaeEstadoProceso.APROBADO_POR_ADMINISTRADOR : (int)EnumMaeEstadoProceso.APROBADO_POR_JEFE;
                    // Actualizar Justificaciones
                    ActualizarAprobarDenegarCompensacionDetalle(request, connection, transaction);

                    // Actualizar Procesos relacionados
                    var proceso = new CompensacionesDetalleProceso();
                    proceso.iCodCompensacionesDetalle = request.ICodCompensacionesDetalle;
                    proceso.ICodEstadoProceso = request.adminJefe ? (int)EnumMaeEstadoProceso.APROBADO_POR_ADMINISTRADOR : (int)EnumMaeEstadoProceso.APROBADO_POR_JEFE;
                    proceso.BEstado = true;
                    proceso.VAuditModificacion = request.VAuditModificacion;
                    proceso.VComentario = request.VComentario;
                    proceso.VAuditCreacion = request.VAuditModificacion;
                    InsertarCompensacionesDetalleProcesos(proceso, connection, transaction);

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
        public CompensacionesDetalleTrabajador_Request DenegarCompensacionDetalle(CompensacionesDetalleTrabajador_Request request)
        {
            using (SqlConnection connection = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion().Connection)
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    request.iCodEstadoProceso = request.adminJefe ? (int)EnumMaeEstadoProceso.RECHAZADO_POR_ADMINISTRADOR : (int)EnumMaeEstadoProceso.RECHAZADO_POR_JEFE;
                    // Actualizar Justificaciones
                    ActualizarAprobarDenegarCompensacionDetalle(request, connection, transaction);

                    // Actualizar Procesos relacionados
                    var proceso = new CompensacionesDetalleProceso();
                    proceso.ICodEstadoProceso = request.adminJefe ? (int)EnumMaeEstadoProceso.RECHAZADO_POR_ADMINISTRADOR : (int)EnumMaeEstadoProceso.RECHAZADO_POR_JEFE;
                    proceso.iCodCompensacionesDetalle = request.ICodCompensacionesDetalle;
                    proceso.BEstado = true;
                    proceso.VAuditModificacion = request.VAuditModificacion;
                    proceso.VComentario = request.VComentario;
                    proceso.VAuditCreacion = request.VAuditModificacion;
                    InsertarCompensacionesDetalleProcesos(proceso, connection, transaction);

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

        public CompensacionesDetalleTrabajador_Request AprobarCompensacionDetalleMas(CompensacionesDetalleTrabajador_Request request)
        {
            using (SqlConnection connection = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion().Connection)
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    request.iCodEstadoProceso = request.adminJefe ? (int)EnumMaeEstadoProceso.APROBADO_POR_ADMINISTRADOR : (int)EnumMaeEstadoProceso.APROBADO_POR_JEFE;
                    // Actualizar Justificaciones
                    using (SqlCommand cmd = new SqlCommand("pa_ActualizarAprobarDenegarCompensacionDetalleMas", connection, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@iCodEstadoProceso", request.iCodEstadoProceso);
                        cmd.Parameters.AddWithValue("@vDescripcion", request.VComentario);
                        cmd.Parameters.AddWithValue("@vAuditModificacion", request.VAuditModificacion);
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
        public CompensacionesDetalleTrabajador_Request DenegarCompensacionDetalleMas(CompensacionesDetalleTrabajador_Request request)
        {
            using (SqlConnection connection = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion().Connection)
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    request.iCodEstadoProceso = request.adminJefe ? (int)EnumMaeEstadoProceso.RECHAZADO_POR_ADMINISTRADOR : (int)EnumMaeEstadoProceso.RECHAZADO_POR_JEFE;
                    // Actualizar Justificaciones
                    using (SqlCommand cmd = new SqlCommand("pa_ActualizarAprobarDenegarCompensacionDetalleMas", connection, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@iCodEstadoProceso", request.iCodEstadoProceso);
                        cmd.Parameters.AddWithValue("@vDescripcion", request.VComentario);
                        cmd.Parameters.AddWithValue("@vAuditModificacion", request.VAuditModificacion);
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
    }
}
