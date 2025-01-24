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
    public class T_genm_justificaciones_ODA
    {
        BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWork = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings["cnnSQL"].ConnectionString));

        public IEnumerable<Justificaciones_Registro> ListarJustificacionTrabajador(JustificacionesTrabajador_Request request)
        {
            List<Justificaciones_Registro> lista = new List<Justificaciones_Registro>();
            Justificaciones_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paJustificacionTrabajadorConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodigoDependencia", request.iCodigoDependencia));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", request.iCodTrabajador));
                if (request.fecha == "")
                    cmd.Parameters.Add(new SqlParameter("@fecha", DBNull.Value));
                else 
                    cmd.Parameters.Add(new SqlParameter("@fecha", Convert.ToDateTime(request.fecha)));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoJustificacion", request.iCodTipoJustificacion));
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
                            item = new Justificaciones_Registro();

                            item.Grilla = new Grilla_Response();
                            item.iCodJustificaciones = Types.CheckDefaultValue<Int32>(dr["iCodJustificaciones"]);
                            item.iCodTrabajador = Types.CheckDefaultValue<Int32>(dr["iCodTrabajador"]);
                            item.iCodigoDependencia = Types.CheckDefaultValue<Int32>(dr["iCodigoDependencia"]);

                            item.iCodTipoJustificacion = Types.CheckDefaultValue<Int32>(dr["iCodTipoJustificacion"]);
                            item.vTipoJustificacion = Types.CheckDefaultValue<string>(dr["vTipoJustificacion"]);

                            item.iCodEstadoProceso= Types.CheckDefaultValue<Int32>(dr["iCodEstadoProceso"]);
                            item.vEstadoProceso = Types.CheckDefaultValue<string>(dr["vEstadoProceso"]);
                            
                            item.dtFechaHoraInicio = Types.CheckDefaultValue<DateTime>(dr["dtFechaHoraInicio"]);
                            item.dtFechaHoraFin = Types.CheckDefaultValue<DateTime>(dr["dtFechaHoraFin"]);
                            
                            item.vDescripcion = Types.CheckDefaultValue<string>(dr["vDescripcion"]);

                            item.vMotivoJustificacion = Types.CheckDefaultValue<string>(dr["vMotivoJustificacion"]);

                            item.iCodTipGoce = Types.CheckDefaultValue<Int32>(dr["iCodTipGoce"]);
                            item.vTipoGoce = Types.CheckDefaultValue<string>(dr["vTipoGoce"]);

                            item.vDependencia = Types.CheckDefaultValue<String>(dr["vDependencia"]);
                            item.vNombreTrabajador = Types.CheckDefaultValue<String>(dr["vNombreTrabajador"]);
                            item.vNumeroDocumento = Types.CheckDefaultValue<String>(dr["vNumeroDocumento"]);
                            item.vUrlArchivo = Types.CheckDefaultValue<String>(dr["vUrlArchivo"]);
                            item.vAuditCreacion = Types.CheckDefaultValue<String>(dr["vAuditCreacion"]);
                            item.dtAuditCreacion = Types.CheckDefaultValue<DateTime>(dr["dtAuditCreacion"]);


                            item.vAprobadoJeje = Types.CheckDefaultValue<String>(dr["vAprobadoJeje"]);
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

        public JustificacionesTrabajador_Registro Insertar(JustificacionesTrabajador_Registro request)
        {
            using (SqlConnection conn = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion().Connection)
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Insertar en la tabla Justificaciones
                    var proceso = new JustificacionProceso();
                    proceso.iCodEstadoProceso = (int)EnumMaeEstadoProceso.PENDIENTE;
                    request.iCodEstadoProceso = proceso.iCodEstadoProceso;
                    int codJustificacion = InsertarJustificacion(conn, transaction, request);

                    // Insertar los archivos relacionados
                    if (request.Archivos.Count > 0)
                    {
                        foreach (var archivo in request.Archivos)
                        {
                            archivo.iCodJustificaciones = codJustificacion; // Relacionar el archivo con la justificación insertada
                            InsertarJustificacionArchivo(conn, transaction, archivo);
                        }
                    }

                    // Insertar los procesos relacionados                    
                    proceso.bEstado = true;
                    proceso.vAuditCreacion = request.vAuditCreacion;
                    proceso.iCodJustificaciones = codJustificacion; // Relacionar el proceso con la justificación insertada
                    proceso.vComentario = request.vDescripcion;
                    proceso.vAuditCreacion = request.vAuditCreacion;
                    InsertarJustificacionProceso(conn, transaction, proceso);

                    // Confirmar la transacción si todo salió bien
                    transaction.Commit();
                    if (codJustificacion > 0)
                    {
                        request.iCodJustificaciones = codJustificacion;
                        return request;
                    }
                    else {
                        transaction.Rollback();
                        throw new Exception("Error al insertar los datos: ");
                    }
                }
                catch (Exception ex)
                {
                    // Hacer rollback si hubo un error
                    transaction.Rollback();
                    throw new Exception("Error al insertar los datos: ", ex);
                }
            }
        }

        private int InsertarJustificacion(SqlConnection conn, SqlTransaction transaction, JustificacionesTrabajador_Registro justificacion)
        {
            using (SqlCommand cmd = new SqlCommand("dbo.InsertarJustificacion", conn, transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@iCodTrabajador", justificacion.iCodTrabajador);
                cmd.Parameters.AddWithValue("@iCodigoDependencia", justificacion.iCodigoDependencia);
                cmd.Parameters.AddWithValue("@iCodMotivoJustificacion", justificacion.iCodMotivoJustificacion);
                cmd.Parameters.AddWithValue("@iCodTipoJustificacion", justificacion.iCodTipoJustificacion);
                cmd.Parameters.AddWithValue("@iCodTipGoce", justificacion.iCodTipGoce);
                cmd.Parameters.AddWithValue("@iCodEstadoProceso", justificacion.iCodEstadoProceso);
                cmd.Parameters.AddWithValue("@bMadrugada", justificacion.bMadrugada);
                cmd.Parameters.AddWithValue("@dtFechaHoraInicio", justificacion.dtFechaHoraInicio);
                cmd.Parameters.AddWithValue("@dtFechaHoraFin", justificacion.dtFechaHoraFin);
                cmd.Parameters.AddWithValue("@vDescripcion", justificacion.vDescripcion);
                cmd.Parameters.AddWithValue("@bEstado", justificacion.bEstado);
                cmd.Parameters.AddWithValue("@vAuditCreacion", justificacion.vAuditCreacion);

                SqlParameter outputParam = new SqlParameter("@iCodJustificaciones", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputParam);

                cmd.ExecuteNonQuery();

                // Retornar el ID generado
                return (int)outputParam.Value;
            }
        }
        private void InsertarJustificacionArchivo(SqlConnection conn, SqlTransaction transaction, JustificacionArchivo archivo)
        {
            using (SqlCommand cmd = new SqlCommand("dbo.InsertarJustificacionesArchivos", conn, transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@iCodJustificaciones", archivo.iCodJustificaciones);
                cmd.Parameters.AddWithValue("@vObservaciones", archivo.vObservaciones);
                cmd.Parameters.AddWithValue("@vUrlArchivo", archivo.vUrlArchivo);
                cmd.Parameters.AddWithValue("@bEstado", archivo.bEstado);
                //cmd.Parameters.AddWithValue("@dtAuditCreacion", archivo.dtAuditCreacion);
                cmd.Parameters.AddWithValue("@vAuditCreacion", archivo.vAuditCreacion);
                //cmd.Parameters.AddWithValue("@dtAuditModificacion", archivo.dtAuditModificacion);
                //cmd.Parameters.AddWithValue("@vAuditModificacion", archivo.vAuditModificacion);

                SqlParameter outputParam = new SqlParameter("@iCodJustificacionesArchivos", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputParam);

                cmd.ExecuteNonQuery();
                archivo.iCodJustificacionesArchivos = (int)outputParam.Value;
            }
        }
        private void InsertarJustificacionProceso(SqlConnection conn, SqlTransaction transaction, JustificacionProceso proceso)
        {
            using (SqlCommand cmd = new SqlCommand("dbo.InsertarJustificacionesProceso", conn, transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@iCodJustificaciones", proceso.iCodJustificaciones);
                cmd.Parameters.AddWithValue("@iCodEstadoProceso", proceso.iCodEstadoProceso);
                cmd.Parameters.AddWithValue("@bEstado", proceso.bEstado);
                cmd.Parameters.AddWithValue("@vComentario", proceso.vComentario);
                cmd.Parameters.AddWithValue("@vAuditCreacion", proceso.vAuditCreacion);
                //cmd.Parameters.AddWithValue("@dtAuditModificacion", proceso.dtAuditModificacion);
                //cmd.Parameters.AddWithValue("@vAuditModificacion", proceso.vAuditModificacion);

                SqlParameter outputParam = new SqlParameter("@iCodJustificacionesProceso", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputParam);

                cmd.ExecuteNonQuery();
                proceso.iCodJustificacionesProceso = (int)outputParam.Value;
            }
        }


        public JustificacionesTrabajador_Registro ActualizarJustificacion(JustificacionesTrabajador_Registro request)
        {
            using (SqlConnection connection = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion().Connection)
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Actualizar Justificaciones
                    ActualizarJustificacion(request, connection, transaction);

                    // Actualizar Archivos relacionados
                    ActualizarJustificacionArchivo(request, connection, transaction);

                    // Actualizar Procesos relacionados
                    var proceso = new JustificacionProceso();
                    proceso.iCodEstadoProceso = (int)request.iCodEstadoProceso;
                    proceso.bEstado = true;
                    proceso.vAuditModificacion = request.vAuditModificacion;
                    proceso.vAuditCreacion = request.vAuditModificacion;
                    proceso.iCodJustificaciones = request.iCodJustificaciones; // Relacionar el proceso con la justificación insertada
                    proceso.vComentario = request.vDescripcion;
                    ActualizarJustificacionProceso(proceso, connection, transaction);

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

        private static void ActualizarJustificacionProceso(JustificacionProceso proceso, SqlConnection connection, SqlTransaction transaction)
        {
            if (proceso.iCodJustificacionesProceso == 0)
            {
                using (SqlCommand cmd = new SqlCommand("dbo.InsertarJustificacionesProceso", connection, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@iCodJustificaciones", proceso.iCodJustificaciones);
                    cmd.Parameters.AddWithValue("@iCodEstadoProceso", proceso.iCodEstadoProceso);
                    cmd.Parameters.AddWithValue("@bEstado", proceso.bEstado);
                    cmd.Parameters.AddWithValue("@vComentario", proceso.vComentario);
                    cmd.Parameters.AddWithValue("@vAuditCreacion", proceso.vAuditCreacion);
                    SqlParameter outputParam = new SqlParameter("@iCodJustificacionesProceso", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    cmd.ExecuteNonQuery();
                    proceso.iCodJustificacionesProceso = (int)outputParam.Value;
                }
            }
            else
            {
                using (SqlCommand cmd = new SqlCommand("dbo.pa_ActualizarJustificacionProceso", connection, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@iCodJustificacionesProceso", proceso.iCodJustificacionesProceso);
                    cmd.Parameters.AddWithValue("@iCodJustificaciones", proceso.iCodJustificaciones);
                    cmd.Parameters.AddWithValue("@iCodEstadoProceso", proceso.iCodEstadoProceso);
                    cmd.Parameters.AddWithValue("@bEstado", proceso.bEstado);
                    cmd.Parameters.AddWithValue("@vComentario", proceso.vComentario);
                    cmd.Parameters.AddWithValue("@vAuditModificacion", proceso.vAuditModificacion);

                    cmd.ExecuteNonQuery();
                }
            }

        }
        private static void ActualizarJustificacionArchivo(JustificacionesTrabajador_Registro justificacion, SqlConnection connection, SqlTransaction transaction)
        {
            foreach (var archivo in justificacion.Archivos)
            {
                if (archivo.iCodJustificacionesArchivos == 0)
                {
                    using (SqlCommand cmd = new SqlCommand("dbo.InsertarJustificacionesArchivos", connection, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@iCodJustificaciones", justificacion.iCodJustificaciones);
                        cmd.Parameters.AddWithValue("@vObservaciones", archivo.vObservaciones);
                        cmd.Parameters.AddWithValue("@vUrlArchivo", archivo.vUrlArchivo);
                        cmd.Parameters.AddWithValue("@bEstado", archivo.bEstado);
                        cmd.Parameters.AddWithValue("@vAuditCreacion", archivo.vAuditCreacion);

                        SqlParameter outputParam = new SqlParameter("@iCodJustificacionesArchivos", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);
                        cmd.ExecuteNonQuery();
                        archivo.iCodJustificacionesArchivos = (int)outputParam.Value;
                    }
                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand("pa_ActualizarJustificacionArchivo", connection, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@iCodJustificacionesArchivos", archivo.iCodJustificacionesArchivos);
                        cmd.Parameters.AddWithValue("@iCodJustificaciones", archivo.iCodJustificaciones);
                        cmd.Parameters.AddWithValue("@vObservaciones", archivo.vObservaciones);
                        cmd.Parameters.AddWithValue("@vUrlArchivo", archivo.vUrlArchivo);
                        cmd.Parameters.AddWithValue("@bEstado", archivo.bEstado);
                        cmd.Parameters.AddWithValue("@vAuditModificacion", justificacion.vAuditModificacion);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        private static void ActualizarJustificacion(JustificacionesTrabajador_Registro justificacion, SqlConnection connection, SqlTransaction transaction)
        {
            using (SqlCommand cmd = new SqlCommand("pa_ActualizarJustificacion", connection, transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iCodJustificaciones", justificacion.iCodJustificaciones);
                cmd.Parameters.AddWithValue("@iCodTrabajador", justificacion.iCodTrabajador);
                cmd.Parameters.AddWithValue("@iCodigoDependencia", justificacion.iCodigoDependencia);
                cmd.Parameters.AddWithValue("@iCodTipoJustificacion", justificacion.iCodTipoJustificacion);
                cmd.Parameters.AddWithValue("@iCodTipGoce", justificacion.iCodTipGoce);
                cmd.Parameters.AddWithValue("@iCodEstadoProceso", justificacion.iCodEstadoProceso);
                cmd.Parameters.AddWithValue("@bMadrugada", justificacion.bMadrugada);
                cmd.Parameters.AddWithValue("@dtFechaHoraInicio", justificacion.dtFechaHoraInicio);
                cmd.Parameters.AddWithValue("@dtFechaHoraFin", justificacion.dtFechaHoraFin);
                cmd.Parameters.AddWithValue("@vDescripcion", justificacion.vDescripcion);
                cmd.Parameters.AddWithValue("@bEstado", justificacion.bEstado);
                cmd.Parameters.AddWithValue("@vAuditModificacion", justificacion.vAuditModificacion);

                cmd.ExecuteNonQuery();
            }
        }
        private static void ActualizarAprobarDenegarJustificacion(JustificacionesTrabajador_Registro justificacion, SqlConnection connection, SqlTransaction transaction)
        {
            using (SqlCommand cmd = new SqlCommand("pa_ActualizarAprobarDenegarJustificacion", connection, transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iCodJustificaciones", justificacion.iCodJustificaciones);
                cmd.Parameters.AddWithValue("@iCodEstadoProceso", justificacion.iCodEstadoProceso);
                cmd.Parameters.AddWithValue("@vDescripcion", justificacion.vDescripcion);
                cmd.Parameters.AddWithValue("@vAuditModificacion", justificacion.vAuditModificacion);

                cmd.ExecuteNonQuery();
            }
        }

        public JustificacionesTrabajador_Registro ObtenerJustificacionPorId(int iCodJustificaciones)
        {
            JustificacionesTrabajador_Registro justificacion = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

                // Obtener Justificación
                cmd.CommandText = "[dbo].[pa_JustificacionTrabajadorConsultarporId]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@iCodJustificaciones", iCodJustificaciones));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        dr.Read();
                        justificacion = new JustificacionesTrabajador_Registro
                        {
                            iCodJustificaciones = Types.CheckDefaultValue<int>(dr["iCodJustificaciones"]),
                            iCodTrabajador = Types.CheckDefaultValue<int?>(dr["iCodTrabajador"]),
                            iCodigoDependencia = Types.CheckDefaultValue<int?>(dr["iCodigoDependencia"]),
                            iCodMotivoJustificacion = Types.CheckDefaultValue<int?>(dr["iCodMotivoJustificacion"]),
                            iCodTipoJustificacion = Types.CheckDefaultValue<int?>(dr["iCodTipoJustificacion"]),
                            bMadrugada = Types.CheckDefaultValue<bool?>(dr["bMadrugada"]),
                            dtFechaHoraInicio = Types.CheckDefaultValue<DateTime?>(dr["dtFechaHoraInicio"]),
                            dtFechaHoraFin = Types.CheckDefaultValue<DateTime?>(dr["dtFechaHoraFin"]),
                            iCodTipGoce = Types.CheckDefaultValue<int?>(dr["iCodTipGoce"]),
                            vDescripcion = Types.CheckDefaultValue<string>(dr["vDescripcion"]),
                            iCodEstadoProceso = Types.CheckDefaultValue<int?>(dr["iCodEstadoProceso"]),
                        };
                    }
                }

                // Obtener Archivos de la Justificación
                cmd.CommandText = "[dbo].[pa_JustificacionTrabajadorArchivosporIdJustificacion]";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@iCodJustificaciones", iCodJustificaciones));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        justificacion.Archivos = new List<JustificacionArchivo>();
                        while (dr.Read())
                        {
                            justificacion.Archivos.Add(new JustificacionArchivo
                            {
                                iCodJustificacionesArchivos = Types.CheckDefaultValue<int>(dr["iCodJustificacionesArchivos"]),
                                iCodJustificaciones = Types.CheckDefaultValue<int>(dr["iCodJustificaciones"]),
                                vObservaciones = Types.CheckDefaultValue<string>(dr["vObservaciones"]),
                                vUrlArchivo = Types.CheckDefaultValue<string>(dr["vUrlArchivo"]),
                                bEstado = Types.CheckDefaultValue<bool>(dr["bEstado"]),
                            });
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return justificacion;
        }
        public JustificacionesTrabajador_Registro AprobarJustificacion(JustificacionesTrabajador_Registro request)
        {
            using (SqlConnection connection = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion().Connection)
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    request.iCodEstadoProceso = request.adminJefe ? (int)EnumMaeEstadoProceso.APROBADO_POR_ADMINISTRADOR : (int)EnumMaeEstadoProceso.APROBADO_POR_JEFE;
                    // Actualizar Justificaciones
                    ActualizarAprobarDenegarJustificacion(request, connection, transaction);

                    // Actualizar Procesos relacionados
                    var proceso = new JustificacionProceso();
                    proceso.iCodEstadoProceso = request.adminJefe ? (int)EnumMaeEstadoProceso.APROBADO_POR_ADMINISTRADOR : (int)EnumMaeEstadoProceso.APROBADO_POR_JEFE;
                    proceso.bEstado = true;
                    proceso.vAuditModificacion = request.vAuditModificacion;
                    proceso.iCodJustificaciones = request.iCodJustificaciones; // Relacionar el proceso con la justificación insertada
                    proceso.vComentario = request.vDescripcion;
                    proceso.vAuditCreacion = request.vAuditModificacion;
                    InsertarJustificacionProceso(connection, transaction, proceso);

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
        public JustificacionesTrabajador_Registro DenegarJustificacion(JustificacionesTrabajador_Registro request)
        {
            using (SqlConnection connection = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion().Connection)
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    request.iCodEstadoProceso = request.adminJefe ? (int)EnumMaeEstadoProceso.RECHAZADO_POR_ADMINISTRADOR : (int)EnumMaeEstadoProceso.RECHAZADO_POR_JEFE;
                    // Actualizar Justificaciones
                    ActualizarAprobarDenegarJustificacion(request, connection, transaction);

                    // Actualizar Procesos relacionados
                    var proceso = new JustificacionProceso();
                    proceso.iCodEstadoProceso = request.adminJefe ? (int)EnumMaeEstadoProceso.RECHAZADO_POR_ADMINISTRADOR : (int)EnumMaeEstadoProceso.RECHAZADO_POR_JEFE;
                    proceso.bEstado = true;
                    proceso.vAuditModificacion = request.vAuditModificacion;
                    proceso.iCodJustificaciones = request.iCodJustificaciones; // Relacionar el proceso con la justificación insertada
                    proceso.vComentario = request.vDescripcion;
                    proceso.vAuditCreacion = request.vAuditModificacion;
                    InsertarJustificacionProceso(connection, transaction, proceso);

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

        /*
         CREATE PROCEDURE pa_ActualizarAprobarDenegarJustificacionMas
    @iCodEstadoProceso INT,
    @vDescripcion VARCHAR(MAX),
    @vAuditModificacion VARCHAR(50),
	@vIds VARCHAR(MAX)
         */
        public JustificacionesTrabajador_Registro AprobarJustificacionMas(JustificacionesTrabajador_Registro request)
        {
            using (SqlConnection connection = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion().Connection)
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    request.iCodEstadoProceso = request.adminJefe ? (int)EnumMaeEstadoProceso.APROBADO_POR_ADMINISTRADOR : (int)EnumMaeEstadoProceso.APROBADO_POR_JEFE;

                    using (SqlCommand cmd = new SqlCommand("pa_ActualizarAprobarDenegarJustificacionMas", connection, transaction))
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
        public JustificacionesTrabajador_Registro DenegarJustificacionMas(JustificacionesTrabajador_Registro request)
        {
            using (SqlConnection connection = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion().Connection)
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    request.iCodEstadoProceso = request.adminJefe ? (int)EnumMaeEstadoProceso.RECHAZADO_POR_ADMINISTRADOR : (int)EnumMaeEstadoProceso.RECHAZADO_POR_JEFE;
                    using (SqlCommand cmd = new SqlCommand("pa_ActualizarAprobarDenegarJustificacionMas", connection, transaction))
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
    }
}
