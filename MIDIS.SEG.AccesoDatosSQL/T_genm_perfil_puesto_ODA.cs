﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using MIDIS.ORI.Entidades;

namespace MIDIS.SEG.AccesoDatosSQL
{
    public partial class T_genm_perfil_puesto_ODA
    {
        //BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWork = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSQLAnt"]].ConnectionString));
        BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWork = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnOracle"]].ConnectionString));
        public Int32 InsertarCab(PerfilPuestoRegistro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paSISGESRRHH_MaePerfil_Registrar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", registro.iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iCodOrgano", registro.iCodOrgano));
                cmd.Parameters.Add(new SqlParameter("@iCodUnidadOrganica", registro.iCodUnidadOrganica));
                cmd.Parameters.Add(new SqlParameter("@iTipoPerfil", registro.iTipoPerfil));
                cmd.Parameters.Add(new SqlParameter("@vPuestoEstructural", registro.strPuestoEstructural));
                cmd.Parameters.Add(new SqlParameter("@vNombrePuesto", registro.strNombrePuesto));
                cmd.Parameters.Add(new SqlParameter("@vDependenciaJerárquicaLineal", registro.strDependenciaJerarquicaLineal));
                cmd.Parameters.Add(new SqlParameter("@vDependenciaFuncional", registro.strDependenciaFuncional));
                cmd.Parameters.Add(new SqlParameter("@vPuestos_a_su_Cargo", registro.strPuestos_a_su_Cargo));
                cmd.Parameters.Add(new SqlParameter("@vMision", registro.strMision));
                
                id = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                registro.iCodPerfil = id;

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.iCodPerfil;
        }

        public Int32 ActualizarCab(PerfilPuestoRegistro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paSISGESRRHH_MaePerfil_Actualizar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;
                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", registro.iCodPerfil));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", registro.iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iCodOrgano", registro.iCodOrgano));
                cmd.Parameters.Add(new SqlParameter("@iCodUnidadOrganica", registro.iCodUnidadOrganica));
                cmd.Parameters.Add(new SqlParameter("@vNombrePuesto", registro.strNombrePuesto));
                cmd.Parameters.Add(new SqlParameter("@vMision", registro.strMision));
                
                cmd.Parameters.Add(new SqlParameter("@iAnioExpGeneral", registro.iAnioExpGeneral));
                cmd.Parameters.Add(new SqlParameter("@iAnioExpEspecifica", registro.iAnioExpEspecifica));
                cmd.Parameters.Add(new SqlParameter("@vDesExpEspecifica", registro.strDesExpEspecifica));
                cmd.Parameters.Add(new SqlParameter("@iAnioExpSectorPublico", registro.iAnioExpSectorPublico));
                cmd.Parameters.Add(new SqlParameter("@iCodNivelMinimo", registro.iCodNivelMinimo));

                cmd.Parameters.Add(new SqlParameter("@vCondiciones", registro.strCondiciones));
                cmd.Parameters.Add(new SqlParameter("@vPeriodicidad", registro.strPeriodicidad));
                cmd.Parameters.Add(new SqlParameter("@iCodPeriodicidad", registro.iPeriodicidad));
                
                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.iCodPerfil;
        }
        public Int32 ActualizarAnexo1(PerfilPuestoRegistro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paSISGESRRHH_MaePerfilAnexo1_Actualizar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;
                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", registro.iCodPerfil));
                cmd.Parameters.Add(new SqlParameter("@iCodOrgano", registro.iCodOrgano));
                cmd.Parameters.Add(new SqlParameter("@iCodUnidadOrganica", registro.iCodUnidadOrganica));
                cmd.Parameters.Add(new SqlParameter("@iTipoReq", registro.iTipoReq));
                cmd.Parameters.Add(new SqlParameter("@dtFechaCese", registro.datFechaCese));
                cmd.Parameters.Add(new SqlParameter("@vTrabajadorCese", registro.strTrabajadorCese));
                cmd.Parameters.Add(new SqlParameter("@vNombrePuesto", registro.strNombrePuesto));
                cmd.Parameters.Add(new SqlParameter("@vRemuneracion", registro.strRemuneracion));
                cmd.Parameters.Add(new SqlParameter("@iPosiciones", registro.iPosiciones));
                cmd.Parameters.Add(new SqlParameter("@vPeriodo", registro.strPeriodo));
                cmd.Parameters.Add(new SqlParameter("@vMeta", registro.strMeta));
                cmd.Parameters.Add(new SqlParameter("@iTipoServicio", registro.iTipoServicio));
                cmd.Parameters.Add(new SqlParameter("@vJustificacion", registro.strJustificacion));
                cmd.Parameters.Add(new SqlParameter("@iConocimiento", registro.iConocimiento));
                cmd.Parameters.Add(new SqlParameter("@iPsicologico", registro.iPsicologico));
                cmd.Parameters.Add(new SqlParameter("@bEstado", registro.bEstado));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", registro.iCodTrabajador));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.iCodPerfil;
        }
        public Int32 InsertarMision(PerfilPuestoRegistro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paSISGESRRHH_MaePerfil_Reg_Mision]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", registro.iCodPerfil));
                cmd.Parameters.Add(new SqlParameter("@vMision", registro.strMision));
                

                //if (registro.archivo != null) cmd.Parameters.Add(new SqlParameter("@ARCHIVO", registro.archivo));
                //SqlParameter IdPropuestaParameter = new SqlParameter("@AUX_EMPLEADO", SqlDbType.Int)
                //{
                //    Direction = ParameterDirection.Output
                //};
                //cmd.Parameters.Add(IdPropuestaParameter);

                cmd.ExecuteNonQuery();                

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.iCodPerfil;
        }

        public Int32 InsertarExperiencia(PerfilPuestoRegistro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paSISGESRRHH_MaePerfil_Reg_Experiencia]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                //cmd.Parameters.Add(new SqlParameter("@iCodPerfil", registro.iCodPerfil));
                //cmd.Parameters.Add(new SqlParameter("@iAnioExpGeneral", registro.iAnioExpGeneral));
                //cmd.Parameters.Add(new SqlParameter("@iAnioExpEspecifica", registro.iAnioExpEspecifica));
                //cmd.Parameters.Add(new SqlParameter("@iAnioExpSectorPublico", registro.iAnioExpSectorPublico));
                //cmd.Parameters.Add(new SqlParameter("@iCodNivelMinimo", registro.iCodNivelMinimo));

                
                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.iCodPerfil;
        }
        public IEnumerable<PerfilPuesto_Request> ObtenerPerfilesPuesto(string id, string fechaIni, string fechaFin)
        {
            List<PerfilPuesto_Request> lista = new List<PerfilPuesto_Request>();
            PerfilPuesto_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPerfilesPuestoPorUUOOConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodigoUUOO", Convert.ToInt32(id)));
                if (fechaIni != null && fechaIni != string.Empty && fechaFin != null && fechaFin != string.Empty)
                {
                    cmd.Parameters.Add(new SqlParameter("@vFechaInicio", Convert.ToDateTime(fechaIni).ToString("dd/MM/yyyy")));
                    cmd.Parameters.Add(new SqlParameter("@vFechaFin", Convert.ToDateTime(fechaFin).ToString("dd/MM/yyyy")));
                }                
                
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PerfilPuesto_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();
                            be.iCodPerfil = dr.GetInt32(dr.GetOrdinal("iCodPerfil"));
                            be.iCodUnidadOrganica = dr.GetInt32(dr.GetOrdinal("iCodigoDependencia"));
                            be.strUnidadOrganica = dr.GetString(dr.GetOrdinal("vUnidadOrganica"));
                            be.strNombrePuesto = dr.GetString(dr.GetOrdinal("vNombrePuesto"));
                            be.datFechaReg = dr.GetDateTime(dr.GetOrdinal("dFechaReg"));
                            be.bEstadoCompletado = dr.GetBoolean(dr.GetOrdinal("bEstadoCompletado"));
                            be.strEstadoCompletado = dr.GetString(dr.GetOrdinal("vEstadoCompletado"));
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<PerfilPuestoRegistro> ObtenerPerfilesPuestoUserRRHH(String strOrgano, String strUO, String strEstado, string strNombre)
        {
            List<PerfilPuestoRegistro> lista = new List<PerfilPuestoRegistro>();
            PerfilPuestoRegistro be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPerfilesPuestoPorUUOO_UserRRHHConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodigoOrgano", strOrgano));
                cmd.Parameters.Add(new SqlParameter("@iCodigoUUOO", strUO));
                cmd.Parameters.Add(new SqlParameter("@vEstado", strEstado.ToUpper()));

                //cmd.Parameters.Add(new SqlParameter("@iCodigoUUOO", Convert.ToInt32(id)));
                //if (fechaIni != null && fechaIni != string.Empty && fechaFin != null && fechaFin != string.Empty)
                //{
                //    cmd.Parameters.Add(new SqlParameter("@vFechaInicio", Convert.ToDateTime(fechaIni).ToString("dd/MM/yyyy")));
                //    cmd.Parameters.Add(new SqlParameter("@vFechaFin", Convert.ToDateTime(fechaFin).ToString("dd/MM/yyyy")));
                //}

                cmd.Parameters.Add(new SqlParameter("@vNombrePuesto", strNombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PerfilPuestoRegistro();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();
                            be.iCodPerfil = dr.GetInt32(dr.GetOrdinal("iCodPerfil"));
                            be.iCodUnidadOrganica = dr.GetInt32(dr.GetOrdinal("iCodigoDependencia"));
                            be.strUnidadOrganica = dr.GetString(dr.GetOrdinal("vUnidadOrganica"));
                            be.strOrgano = dr.GetString(dr.GetOrdinal("vOrgano"));
                            be.iTipoPerfil = dr.GetInt32(dr.GetOrdinal("iTipoPerfil"));
                            be.strNombrePuesto = dr.GetString(dr.GetOrdinal("vNombrePuesto"));
                            be.datFechaReg = dr.GetDateTime(dr.GetOrdinal("dFechaReg"));
                            be.bEstadoCompletado = dr.GetBoolean(dr.GetOrdinal("bEstadoCompletado"));
                            be.strEstadoCompletado = dr.GetString(dr.GetOrdinal("vEstadoCompletado"));
                            //be.strEstadoMovimiento = dr.GetString(dr.GetOrdinal("vEstadoMovimiento"));
                            be.IdTieneArchivo = dr.GetInt32(dr.GetOrdinal("TieneArchivo"));
                            
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<PerfilPuesto_Request> ObtenerPerfilesPuestoUser(string id, string fechaIni, string fechaFin)
        {
            List<PerfilPuesto_Request> lista = new List<PerfilPuesto_Request>();
            PerfilPuesto_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPerfilesPuestoPorUUOO_UserConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodigoUUOO", Convert.ToInt32(id)));
                if (fechaIni != null && fechaIni != string.Empty && fechaFin != null && fechaFin != string.Empty)
                {
                    cmd.Parameters.Add(new SqlParameter("@vFechaInicio", Convert.ToDateTime(fechaIni).ToString("dd/MM/yyyy")));
                    cmd.Parameters.Add(new SqlParameter("@vFechaFin", Convert.ToDateTime(fechaFin).ToString("dd/MM/yyyy")));
                }

                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PerfilPuesto_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();
                            be.iCodPerfil = dr.GetInt32(dr.GetOrdinal("iCodPerfil"));
                            be.iCodUnidadOrganica = dr.GetInt32(dr.GetOrdinal("iCodigoDependencia"));
                            be.strUnidadOrganica = dr.GetString(dr.GetOrdinal("vUnidadOrganica"));
                            be.strNombrePuesto = dr.GetString(dr.GetOrdinal("vNombrePuesto"));
                            be.datFechaReg = dr.GetDateTime(dr.GetOrdinal("dFechaReg"));
                            be.bEstadoCompletado = dr.GetBoolean(dr.GetOrdinal("bEstadoCompletado"));
                            be.strEstadoCompletado = dr.GetString(dr.GetOrdinal("vEstadoCompletado"));
                            be.strEstadoDerivado = dr.GetString(dr.GetOrdinal("cEstadoDerivado"));
                            be.strEstadoMovimiento = dr.GetString(dr.GetOrdinal("vEstadoMovimiento"));
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PerfilPuesto_Request> ObtenerPerfilesPuestoJefe(string IdDependencia, string iCodUnidadOrganica, string fechaIni, string fechaFin)
        {
            List<PerfilPuesto_Request> lista = new List<PerfilPuesto_Request>();
            PerfilPuesto_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPerfilesPuestoPorUUOO_JefeConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;
                cmd.Parameters.Add(new SqlParameter("@iIdDependencia", Convert.ToInt32(IdDependencia)));
                cmd.Parameters.Add(new SqlParameter("@iCodUnidadOrganica", Convert.ToInt32(iCodUnidadOrganica)));
                
                if (fechaIni != null && fechaIni != string.Empty && fechaFin != null && fechaFin != string.Empty)
                {
                    cmd.Parameters.Add(new SqlParameter("@vFechaInicio", Convert.ToDateTime(fechaIni).ToString("dd/MM/yyyy")));
                    cmd.Parameters.Add(new SqlParameter("@vFechaFin", Convert.ToDateTime(fechaFin).ToString("dd/MM/yyyy")));
                }

                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PerfilPuesto_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();
                            be.iCodPerfil = dr.GetInt32(dr.GetOrdinal("iCodPerfil"));
                            be.iCodUnidadOrganica = dr.GetInt32(dr.GetOrdinal("iCodigoDependencia"));
                            be.strUnidadOrganica = dr.GetString(dr.GetOrdinal("vUnidadOrganica"));
                            be.strNombrePuesto = dr.GetString(dr.GetOrdinal("vNombrePuesto"));
                            be.datFechaReg = dr.GetDateTime(dr.GetOrdinal("dFechaReg"));
                            be.bEstadoCompletado = dr.GetBoolean(dr.GetOrdinal("bEstadoCompletado"));
                            be.strEstadoCompletado = dr.GetString(dr.GetOrdinal("vEstadoCompletado"));
                            be.strEstadoMovimiento = dr.GetString(dr.GetOrdinal("vEstadoMovimiento"));
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PerfilPuesto_Request> ObtenerPerfilesPuestoJefeRRHH(string id, string fechaIni, string fechaFin)
        {
            List<PerfilPuesto_Request> lista = new List<PerfilPuesto_Request>();
            PerfilPuesto_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPerfilesPuestoPorUUOO_JefeRRHHConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;
                //if (id !=null && id!=string.Empty)
                //{
                //    cmd.Parameters.Add(new SqlParameter("@iCodigoUUOO", Convert.ToInt32(id)));    
                //}                
                if (fechaIni != null && fechaIni != string.Empty && fechaFin != null && fechaFin != string.Empty)
                {
                    cmd.Parameters.Add(new SqlParameter("@vFechaInicio", Convert.ToDateTime(fechaIni).ToString("dd/MM/yyyy")));
                    cmd.Parameters.Add(new SqlParameter("@vFechaFin", Convert.ToDateTime(fechaFin).ToString("dd/MM/yyyy")));
                }

                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PerfilPuesto_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();
                            be.iCodPerfil = dr.GetInt32(dr.GetOrdinal("iCodPerfil"));
                            be.iCodUnidadOrganica = dr.GetInt32(dr.GetOrdinal("iCodigoDependencia"));
                            be.strUnidadOrganica = dr.GetString(dr.GetOrdinal("vUnidadOrganica"));
                            be.strNombrePuesto = dr.GetString(dr.GetOrdinal("vNombrePuesto"));
                            be.datFechaReg = dr.GetDateTime(dr.GetOrdinal("dFechaReg"));
                            be.bEstadoCompletado = dr.GetBoolean(dr.GetOrdinal("bEstadoCompletado"));
                            be.strEstadoCompletado = dr.GetString(dr.GetOrdinal("vEstadoCompletado"));
                            be.strEstadoMovimiento = dr.GetString(dr.GetOrdinal("vEstadoMovimiento"));
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<Dependencia_Request> ObtenerDependenciasPorUUOO(string id)
        {
            List<Dependencia_Request> lista = new List<Dependencia_Request>();
            Dependencia_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paDependenciasPorUUOOConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodigoUUOO", Convert.ToInt32(id)));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new Dependencia_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Request();                            
                            be.IdDependencia = dr.GetInt32(dr.GetOrdinal("iCodigoDependencia"));
                            be.Nombre = dr.GetString(dr.GetOrdinal("vDependencia"));                            
                            be.RegistroUsuarioCreacion = dr.GetInt32(dr.GetOrdinal("iCantPendientes"));  // Cantidad de Solicitudes Pendientes de Aprobacion
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PerfilPuestoRegistro> ObtenerPerfilesPuestoPorID(string id)
        {
            List<PerfilPuestoRegistro> lista = new List<PerfilPuestoRegistro>();
            PerfilPuestoRegistro be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePerfilConsultarPorID]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", Convert.ToInt32(id)));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PerfilPuestoRegistro();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();
                            be.iCodPerfil = dr.GetInt32(dr.GetOrdinal("iCodPerfil"));
                            be.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            be.iCodOrgano = dr.GetInt32(dr.GetOrdinal("iCodOrgano"));
                            be.strOrgano = dr.GetString(dr.GetOrdinal("vOrgano"));
                            be.iCodUnidadOrganica = dr.GetInt32(dr.GetOrdinal("iCodUnidadOrganica"));
                            be.strUnidadOrganica = dr.GetString(dr.GetOrdinal("vUnidadOrganica"));
                            be.strPuestoEstructural = dr.GetString(dr.GetOrdinal("vPuestoEstructural"));
                            be.strNombrePuesto = dr.GetString(dr.GetOrdinal("vNombrePuesto"));
                            be.strDependenciaJerarquicaLineal = dr.GetString(dr.GetOrdinal("vDependenciaJerarquicaLineal"));
                            
                            if (!Convert.IsDBNull(dr["vMision"])) { be.strMision = dr.GetString(dr.GetOrdinal("vMision")); }
                            if (!Convert.IsDBNull(dr["iAnioExpGeneral"])) { be.iAnioExpGeneral = dr.GetInt32(dr.GetOrdinal("iAnioExpGeneral")); }
                            if (!Convert.IsDBNull(dr["iAnioExpEspecifica"])) { be.iAnioExpEspecifica = dr.GetInt32(dr.GetOrdinal("iAnioExpEspecifica")); }
                            if (!Convert.IsDBNull(dr["IAnioExpSectorPublico"])) { be.iAnioExpSectorPublico = dr.GetInt32(dr.GetOrdinal("IAnioExpSectorPublico")); }
                            if (!Convert.IsDBNull(dr["iCodNivelMinimo"])) { be.iCodNivelMinimo = dr.GetInt32(dr.GetOrdinal("iCodNivelMinimo")); }
                            if (!Convert.IsDBNull(dr["vDependenciaFuncional"])) { be.strDependenciaFuncional = dr.GetString(dr.GetOrdinal("vDependenciaFuncional")); }
                            if (!Convert.IsDBNull(dr["vPuestos_a_su_Cargo"])) { be.strPuestos_a_su_Cargo = dr.GetString(dr.GetOrdinal("vPuestos_a_su_Cargo")); }
                            if (!Convert.IsDBNull(dr["vDesExpEspecifica"])) { be.strDesExpEspecifica = dr.GetString(dr.GetOrdinal("vDesExpEspecifica")); }

                            if (!Convert.IsDBNull(dr["vCondiciones"])) { be.strCondiciones = dr.GetString(dr.GetOrdinal("vCondiciones")); }
                            if (!Convert.IsDBNull(dr["vPeriodicidad"])) { be.strPeriodicidad = dr.GetString(dr.GetOrdinal("vPeriodicidad")); }
                            if (!Convert.IsDBNull(dr["iCodPeriodicidad"])) { be.iPeriodicidad = dr.GetInt32(dr.GetOrdinal("iCodPeriodicidad")); }

                            if (!Convert.IsDBNull(dr["archivo"]))
                            {
                                be.IdTieneArchivo = 1;
                                be.archivo = (byte[])(dr["archivo"]);
                            }
                            be.datFechaReg = dr.GetDateTime(dr.GetOrdinal("dFechaReg"));
                            be.bEstadoCompletado = dr.GetBoolean(dr.GetOrdinal("bEstadoCompletado"));
                            be.strEstadoCompletado = dr.GetString(dr.GetOrdinal("vEstadoCompletado"));
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<PerfilPuestoRegistro> ObtenerPerfilesAnexo1PorID(string id)
        {
            List<PerfilPuestoRegistro> lista = new List<PerfilPuestoRegistro>();
            PerfilPuestoRegistro be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePerfilConsultarAnexo1PorID]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", Convert.ToInt32(id)));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PerfilPuestoRegistro();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();
                            be.iCodPerfilAnexo = dr.GetInt32(dr.GetOrdinal("iCodPerfilAnexo"));
                            be.iCodPerfil = dr.GetInt32(dr.GetOrdinal("iCodPerfil"));
                            be.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            be.iCodOrgano = dr.GetInt32(dr.GetOrdinal("iCodOrgano"));
                            be.strOrgano = dr.GetString(dr.GetOrdinal("vOrgano"));
                            be.iCodUnidadOrganica = dr.GetInt32(dr.GetOrdinal("iCodUnidadOrganica"));
                            be.iTipoReq = dr.GetInt32(dr.GetOrdinal("iTipoReq"));
                            be.iTipoServicio = dr.GetInt32(dr.GetOrdinal("iTipoServicio"));
                            be.iPosiciones = dr.GetInt32(dr.GetOrdinal("iPosiciones"));
                            be.strPeriodo = dr.GetString(dr.GetOrdinal("vPeriodo"));
                            be.strTrabajadorCese = dr.GetString(dr.GetOrdinal("vTrabajadorCese"));
                            be.strRemuneracion = dr.GetString(dr.GetOrdinal("vRemuneracion"));
                            be.strNombrePuesto = dr.GetString(dr.GetOrdinal("vNombrePuesto"));
                            be.strMeta = dr.GetString(dr.GetOrdinal("vMeta"));
                            be.strJustificacion = dr.GetString(dr.GetOrdinal("vJustificacion"));
                            be.iConocimiento = dr.GetInt32(dr.GetOrdinal("iConocimiento"));
                            be.iPsicologico = dr.GetInt32(dr.GetOrdinal("iPsicologico"));
                            //be.estado = dr.GetInt32(dr.GetOrdinal("iPsicologico"));

                            //if (!Convert.IsDBNull(dr["vMision"])) { be.strMision = dr.GetString(dr.GetOrdinal("vMision")); }
                            //if (!Convert.IsDBNull(dr["iAnioExpGeneral"])) { be.iAnioExpGeneral = dr.GetInt32(dr.GetOrdinal("iAnioExpGeneral")); }

                            if (!Convert.IsDBNull(dr["archivo"]))
                            {
                                be.IdTieneArchivo = 1;
                                be.archivo = (byte[])(dr["archivo"]);
                            }
                            if (!Convert.IsDBNull(dr["dtFechaCese"])) be.datFechaCese = dr.GetDateTime(dr.GetOrdinal("dtFechaCese"));
                            
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        #region Fuciones
        public IEnumerable<PerfilFunciones_Request> ListarPerfilDetFunciones(PerfilFunciones_Request peticion)
        {
            List<PerfilFunciones_Request> lista = new List<PerfilFunciones_Request>();
            PerfilFunciones_Request item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paDetFuncionesConsultarPorID]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));                

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PerfilFunciones_Request();

                            //item.Grilla = new Grilla_Response();
                            item.iCodPerfil = dr.GetInt32(dr.GetOrdinal("iCodPerfil"));
                            item.iSecuencia = dr.GetInt32(dr.GetOrdinal("iSecuencia"));
                            item.Verbo = new Verbo_Registro
                            {
                                iCodVerbo = dr.GetInt32(dr.GetOrdinal("iCodVerbo")),
                                strDescripcion = dr.GetString(dr.GetOrdinal("vVerbo"))
                            };                            
                            item.Objetivo = dr.GetString(dr.GetOrdinal("vObjeto"));
                            item.Funcion = dr.GetString(dr.GetOrdinal("vDescripcion"));                           
                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public bool InsertarFunciones(PerfilFunciones_Request peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paDetFunciones_Registrar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));
                cmd.Parameters.Add(new SqlParameter("@iCodVerbo", peticion.iCodVerbo));
                cmd.Parameters.Add(new SqlParameter("@vObjeto", peticion.Objetivo));
                cmd.Parameters.Add(new SqlParameter("@vDescripcion", peticion.Funcion));
                                
                //if (registro.archivo != null) cmd.Parameters.Add(new SqlParameter("@ARCHIVO", registro.archivo));
                //SqlParameter IdPropuestaParameter = new SqlParameter("@AUX_EMPLEADO", SqlDbType.Int)
                //{
                //    Direction = ParameterDirection.Output
                //};
                //cmd.Parameters.Add(IdPropuestaParameter);

                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        public bool ActualizarFunciones(PerfilFunciones_Request peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paDetFunciones_Actualizar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));
                cmd.Parameters.Add(new SqlParameter("@iSecuencia", peticion.iSecuencia));
                cmd.Parameters.Add(new SqlParameter("@iCodVerbo", peticion.iCodVerbo));
                cmd.Parameters.Add(new SqlParameter("@vObjeto", peticion.Objetivo));
                cmd.Parameters.Add(new SqlParameter("@vDescripcion", peticion.Funcion));

                //if (registro.archivo != null) cmd.Parameters.Add(new SqlParameter("@ARCHIVO", registro.archivo));
                //SqlParameter IdPropuestaParameter = new SqlParameter("@AUX_EMPLEADO", SqlDbType.Int)
                //{
                //    Direction = ParameterDirection.Output
                //};
                //cmd.Parameters.Add(IdPropuestaParameter);

                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        public bool EliminarFunciones(PerfilFunciones_Request peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paDetFunciones_Eliminar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));
                cmd.Parameters.Add(new SqlParameter("@iSecuencia", peticion.iSecuencia));                

                //if (registro.archivo != null) cmd.Parameters.Add(new SqlParameter("@ARCHIVO", registro.archivo));
                //SqlParameter IdPropuestaParameter = new SqlParameter("@AUX_EMPLEADO", SqlDbType.Int)
                //{
                //    Direction = ParameterDirection.Output
                //};
                //cmd.Parameters.Add(IdPropuestaParameter);

                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        #endregion


        #region Requisitos Adicionales
        public IEnumerable<RequisitosAdicionales_Registro> ListarPerfilDetRequisitosAdicionales(RequisitosAdicionales_Registro peticion)
        {
            List<RequisitosAdicionales_Registro> lista = new List<RequisitosAdicionales_Registro>();
            RequisitosAdicionales_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paDetRequisitosAdicionalesConsultarPorID]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new RequisitosAdicionales_Registro();

                            //item.Grilla = new Grilla_Response();
                            item.iCodPerfil = dr.GetInt32(dr.GetOrdinal("iCodPerfil"));
                            item.iSecuencia = dr.GetInt32(dr.GetOrdinal("iSecuencia"));
                            item.Requisito = dr.GetString(dr.GetOrdinal("vDescripcion"));
                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }


        public bool InsertarDetRequisitosAdicionales(RequisitosAdicionales_Registro peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paDetRequisitosAdicionales_Registrar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));
                cmd.Parameters.Add(new SqlParameter("@vDescripcion", peticion.Requisito));

                //if (registro.archivo != null) cmd.Parameters.Add(new SqlParameter("@ARCHIVO", registro.archivo));
                //SqlParameter IdPropuestaParameter = new SqlParameter("@AUX_EMPLEADO", SqlDbType.Int)
                //{
                //    Direction = ParameterDirection.Output
                //};
                //cmd.Parameters.Add(IdPropuestaParameter);

                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        public bool ActualizarDetRequisitosAdicionales(RequisitosAdicionales_Registro peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paDetRequisitosAdicionales_Actualizar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));
                cmd.Parameters.Add(new SqlParameter("@iSecuencia", peticion.iSecuencia));
                cmd.Parameters.Add(new SqlParameter("@vDescripcion", peticion.Requisito));

                //if (registro.archivo != null) cmd.Parameters.Add(new SqlParameter("@ARCHIVO", registro.archivo));
                //SqlParameter IdPropuestaParameter = new SqlParameter("@AUX_EMPLEADO", SqlDbType.Int)
                //{
                //    Direction = ParameterDirection.Output
                //};
                //cmd.Parameters.Add(IdPropuestaParameter);

                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        public bool EliminarDetRequisitosAdicionales(RequisitosAdicionales_Registro peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paDetRequisitosAdicionales_Eliminar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));
                cmd.Parameters.Add(new SqlParameter("@iSecuencia", peticion.iSecuencia));

                //if (registro.archivo != null) cmd.Parameters.Add(new SqlParameter("@ARCHIVO", registro.archivo));
                //SqlParameter IdPropuestaParameter = new SqlParameter("@AUX_EMPLEADO", SqlDbType.Int)
                //{
                //    Direction = ParameterDirection.Output
                //};
                //cmd.Parameters.Add(IdPropuestaParameter);

                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }
    
        #endregion

        #region Habilidades y Competencias
        public IEnumerable<Habilidad_Competencias_Registro> ListarPerfilDetHabilidades_Competencias(Habilidad_Competencias_Registro peticion)
        {
            List<Habilidad_Competencias_Registro> lista = new List<Habilidad_Competencias_Registro>();
            Habilidad_Competencias_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paDetHabilidades_CompetenciasConsultarPorID]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Habilidad_Competencias_Registro();

                            //item.Grilla = new Grilla_Response();
                            item.iCodPerfil = dr.GetInt32(dr.GetOrdinal("iCodPerfil"));
                            item.iCodCualidad = dr.GetInt32(dr.GetOrdinal("iCodCualidad"));
                            item.TipoCualidad =  new  Tipo_Cualidad_Response()
                            {
                                iCodTipoCualidad = dr.GetInt32(dr.GetOrdinal("iCodTipoCualidad")),
                                strDescripcion = dr.GetString(dr.GetOrdinal("vTipoCualidad"))
                            };
                            item.Cualidad = new Cualidad_Response()
                            {
                                iCodCualidad = dr.GetInt32(dr.GetOrdinal("iCodCualidad")),
                                strNombre = dr.GetString(dr.GetOrdinal("vCualidad")),
                                strDescripcion = dr.GetString(dr.GetOrdinal("vDescripcion"))
                            };                            
                            
                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<Habilidad_Competencias_Registro> ListarPerfilDetHabilidades(Habilidad_Competencias_Registro peticion)
        {
            List<Habilidad_Competencias_Registro> lista = new List<Habilidad_Competencias_Registro>();
            Habilidad_Competencias_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paDetHabilidadesConsultarPorID]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Habilidad_Competencias_Registro();

                            //item.Grilla = new Grilla_Response();
                            item.iCodPerfil = dr.GetInt32(dr.GetOrdinal("iCodPerfil"));
                            item.iCodCualidad = dr.GetInt32(dr.GetOrdinal("iCodCualidad"));
                            item.iCodCualidadAnt = dr.GetInt32(dr.GetOrdinal("iCodCualidad"));
                            //item.TipoCualidad = new Tipo_Cualidad_Response()
                            //{
                            //    iCodTipoCualidad = dr.GetInt32(dr.GetOrdinal("iCodTipoCualidad")),
                            //    strDescripcion = dr.GetString(dr.GetOrdinal("vTipoCualidad"))
                            //};
                            item.Cualidad = new Cualidad_Response()
                            {
                                iCodCualidad = dr.GetInt32(dr.GetOrdinal("iCodCualidad")),
                                strNombre = dr.GetString(dr.GetOrdinal("vCualidad")),
                                strDescripcion = dr.GetString(dr.GetOrdinal("vDescripcion"))
                            };

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<Habilidad_Competencias_Registro> ListarPerfilDetCompetencias(Habilidad_Competencias_Registro peticion)
        {
            List<Habilidad_Competencias_Registro> lista = new List<Habilidad_Competencias_Registro>();
            Habilidad_Competencias_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paDetCompetenciasConsultarPorID]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Habilidad_Competencias_Registro();

                            //item.Grilla = new Grilla_Response();
                            item.iCodPerfil = dr.GetInt32(dr.GetOrdinal("iCodPerfil"));
                            item.iCodCualidad = dr.GetInt32(dr.GetOrdinal("iCodCualidad"));
                            item.iCodCualidadAnt = dr.GetInt32(dr.GetOrdinal("iCodCualidad"));
                            //item.TipoCualidad = new Tipo_Cualidad_Response()
                            //{
                            //    iCodTipoCualidad = dr.GetInt32(dr.GetOrdinal("iCodTipoCualidad")),
                            //    strDescripcion = dr.GetString(dr.GetOrdinal("vTipoCualidad"))
                            //};
                            item.Cualidad = new Cualidad_Response()
                            {
                                iCodCualidad = dr.GetInt32(dr.GetOrdinal("iCodCualidad")),
                                strNombre = dr.GetString(dr.GetOrdinal("vCualidad")),
                                strDescripcion = dr.GetString(dr.GetOrdinal("vDescripcion"))
                            };

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public bool InsertarDetHabilidades_Competencias(Habilidad_Competencias_Registro peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {

                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paDetHabilidades_Competencias_Registrar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));
                cmd.Parameters.Add(new SqlParameter("@iCodCualidad", peticion.iCodCualidad));

                //if (registro.archivo != null) cmd.Parameters.Add(new SqlParameter("@ARCHIVO", registro.archivo));
                //SqlParameter IdPropuestaParameter = new SqlParameter("@AUX_EMPLEADO", SqlDbType.Int)
                //{
                //    Direction = ParameterDirection.Output
                //};
                //cmd.Parameters.Add(IdPropuestaParameter);

                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        public bool ActualizarDetHabilidades_Competencias(Habilidad_Competencias_Registro peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paDetHabilidades_Competencias_Actualizar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));
                cmd.Parameters.Add(new SqlParameter("@iCodCualidad", peticion.iCodCualidad));
                cmd.Parameters.Add(new SqlParameter("@iCodCualidadAnt", peticion.iCodCualidadAnt));

                //if (registro.archivo != null) cmd.Parameters.Add(new SqlParameter("@ARCHIVO", registro.archivo));
                //SqlParameter IdPropuestaParameter = new SqlParameter("@AUX_EMPLEADO", SqlDbType.Int)
                //{
                //    Direction = ParameterDirection.Output
                //};
                //cmd.Parameters.Add(IdPropuestaParameter);

                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        public bool EliminarDetHabilidades_Competencias(Habilidad_Competencias_Registro peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paDetHabilidades_Competencias_Eliminar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));
                cmd.Parameters.Add(new SqlParameter("@iCodCualidad", peticion.iCodCualidad));

                //if (registro.archivo != null) cmd.Parameters.Add(new SqlParameter("@ARCHIVO", registro.archivo));
                //SqlParameter IdPropuestaParameter = new SqlParameter("@AUX_EMPLEADO", SqlDbType.Int)
                //{
                //    Direction = ParameterDirection.Output
                //};
                //cmd.Parameters.Add(IdPropuestaParameter);

                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        #endregion

        public IEnumerable<NivelMinimo_Registro> ListarNivelMimino()
        {
            List<NivelMinimo_Registro> lista = new List<NivelMinimo_Registro>();
            NivelMinimo_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePerfilNivelMinConsultar]";
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
                            item = new NivelMinimo_Registro();
                            //item.Grilla = new Grilla_Response();

                            item.iCodNivelMinimo = dr.GetInt32(dr.GetOrdinal("iCodNivelMinimo"));
                            item.strDescripcion = dr.GetString(dr.GetOrdinal("vDescripcion"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PerfilNivelMateria_Response> ListarMaePerfilNivelMateria()
        {
            List<PerfilNivelMateria_Response> lista = new List<PerfilNivelMateria_Response>();
            PerfilNivelMateria_Response item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePerfilNivelMateriaConsultar]";
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
                            item = new PerfilNivelMateria_Response();
                            //item.Grilla = new Grilla_Response();

                            item.iCodTipoNivelMateria = dr.GetInt32(dr.GetOrdinal("iCodTipoNivelMateria"));
                            item.strDescripcion = dr.GetString(dr.GetOrdinal("vDescripcion"));
                            item.bEstado = dr.GetBoolean(dr.GetOrdinal("bEstado"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PerfilTipoMateria_Response> ListarMaePerfilTipoMateria()
        {
            List<PerfilTipoMateria_Response> lista = new List<PerfilTipoMateria_Response>();
            PerfilTipoMateria_Response item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePerfilTipoMateriaConsultar]";
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
                            item = new PerfilTipoMateria_Response();
                            //item.Grilla = new Grilla_Response();

                            item.iCodTipoMateria = dr.GetInt32(dr.GetOrdinal("iCodTipoMateria"));
                            item.strDescripcion = dr.GetString(dr.GetOrdinal("vDescripcion"));
                            item.bEstado = dr.GetBoolean(dr.GetOrdinal("bEstado"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PerfilTipoMateriaOtros_Response> ListarMaePerfilTipoMateriaOtros()
        {
            List<PerfilTipoMateriaOtros_Response> lista = new List<PerfilTipoMateriaOtros_Response>();
            PerfilTipoMateriaOtros_Response item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePerfilTipoMateriaOtrosConsultar]";
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
                            item = new PerfilTipoMateriaOtros_Response();
                            //item.Grilla = new Grilla_Response();

                            item.iCodTipoMateriaOtros = dr.GetInt32(dr.GetOrdinal("iCodTipoMateriaOtros"));
                            item.strDescripcion = dr.GetString(dr.GetOrdinal("vDescripcion"));
                            item.bEstado = dr.GetBoolean(dr.GetOrdinal("bEstado"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PerfilTipoSubMateriaOtros_Response> ListarMaePerfilTipoSubMateriaOtros(int iCodTipoMateriaOtros)
        {
            List<PerfilTipoSubMateriaOtros_Response> lista = new List<PerfilTipoSubMateriaOtros_Response>();
            PerfilTipoSubMateriaOtros_Response item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePerfilTipoSubMateriaOtrosConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@p_iCodTipoMateriaOtros", iCodTipoMateriaOtros));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PerfilTipoSubMateriaOtros_Response();
                            //item.Grilla = new Grilla_Response();

                            item.iCodTipoSubMateriaOtros = dr.GetInt32(dr.GetOrdinal("iCodTipoSubMateriaOtros"));
                            item.strDescripcion = dr.GetString(dr.GetOrdinal("vDescripcion"));
                            item.iCodTipoMateriaOtros = dr.GetInt32(dr.GetOrdinal("iCodTipoMateriaOtros"));
                            item.bEstado = dr.GetBoolean(dr.GetOrdinal("bEstado"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PerfillNivelEducativo_Response> ListarNivelEducativo()
        {
            List<PerfillNivelEducativo_Response> lista = new List<PerfillNivelEducativo_Response>();
            PerfillNivelEducativo_Response item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePerfilNivelConsultar]";
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
                            item = new PerfillNivelEducativo_Response();
                            //item.Grilla = new Grilla_Response();

                            item.iCodNivel = dr.GetInt32(dr.GetOrdinal("iCodNivel"));
                            item.strDescripcion = dr.GetString(dr.GetOrdinal("vDescripcion"));
                            item.bEstado = dr.GetBoolean(dr.GetOrdinal("bEstado"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PerfilGrados_Response> ListarGradosBasico()
        {
            List<PerfilGrados_Response> lista = new List<PerfilGrados_Response>();
            PerfilGrados_Response item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePerfilGradosBasicoConsultar]";
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
                            item = new PerfilGrados_Response();
                            //item.Grilla = new Grilla_Response();

                            item.iCodGrado = dr.GetInt32(dr.GetOrdinal("iCodGrado"));
                            item.strDescripcion = dr.GetString(dr.GetOrdinal("vDescripcion"));
                            item.bEstado = dr.GetBoolean(dr.GetOrdinal("bEstado"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<PerfilGrados_Response> ListarGradosTodos()
        {
            List<PerfilGrados_Response> lista = new List<PerfilGrados_Response>();
            PerfilGrados_Response item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePerfilGrados]";
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
                            item = new PerfilGrados_Response();
                            //item.Grilla = new Grilla_Response();

                            item.iCodGrado = dr.GetInt32(dr.GetOrdinal("iCodGrado"));
                            item.strDescripcion = dr.GetString(dr.GetOrdinal("vDescripcion"));
                            item.bEstado = dr.GetBoolean(dr.GetOrdinal("bEstado"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PerfilGrados_Response> ListarGrados()
        {
            List<PerfilGrados_Response> lista = new List<PerfilGrados_Response>();
            PerfilGrados_Response item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePerfilGradosConsultar]";
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
                            item = new PerfilGrados_Response();
                            //item.Grilla = new Grilla_Response();

                            item.iCodGrado = dr.GetInt32(dr.GetOrdinal("iCodGrado"));
                            item.strDescripcion = dr.GetString(dr.GetOrdinal("vDescripcion"));
                            item.bEstado = dr.GetBoolean(dr.GetOrdinal("bEstado"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PerfilCarrera_Response> ListarCarreraNivel1(int iCodTipoCarrera)
        {
            List<PerfilCarrera_Response> lista = new List<PerfilCarrera_Response>();
            PerfilCarrera_Response item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePerfilCarreraConsultarNivel1]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@p_iCodSubTipoCarrera", iCodTipoCarrera));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PerfilCarrera_Response();
                            //item.Grilla = new Grilla_Response();

                            item.strCodCarrera = dr.GetString(dr.GetOrdinal("vCodCarrera"));
                            item.strDescripcion = dr.GetString(dr.GetOrdinal("vDescripcion"));
                            item.iCodTipoCarrera = dr.GetInt32(dr.GetOrdinal("iCodTipoCarrera"));
                            item.bEstado = dr.GetBoolean(dr.GetOrdinal("bEstado"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PerfilCarrera_Response> ListarCarreraNivel2(string vCodCarrera)
        {
            List<PerfilCarrera_Response> lista = new List<PerfilCarrera_Response>();
            PerfilCarrera_Response item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePerfilCarreraConsultarNivel2]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@p_vCodCarrera", vCodCarrera));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PerfilCarrera_Response();
                            //item.Grilla = new Grilla_Response();

                            item.strCodCarrera = dr.GetString(dr.GetOrdinal("vCodCarrera"));
                            item.strDescripcion = dr.GetString(dr.GetOrdinal("vDescripcion"));
                            item.iCodTipoCarrera = dr.GetInt32(dr.GetOrdinal("iCodTipoCarrera"));
                            item.bEstado = dr.GetBoolean(dr.GetOrdinal("bEstado"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PerfilCarrera_Response> ListarCarreraNivel3(string vCodCarrera)
        {
            List<PerfilCarrera_Response> lista = new List<PerfilCarrera_Response>();
            PerfilCarrera_Response item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePerfilCarreraConsultarNivel3]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@p_vCodCarrera", vCodCarrera));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PerfilCarrera_Response();
                            //item.Grilla = new Grilla_Response();

                            item.strCodCarrera = dr.GetString(dr.GetOrdinal("vCodCarrera"));
                            item.strDescripcion = dr.GetString(dr.GetOrdinal("vDescripcion"));
                            item.iCodTipoCarrera = dr.GetInt32(dr.GetOrdinal("iCodTipoCarrera"));
                            item.bEstado = dr.GetBoolean(dr.GetOrdinal("bEstado"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PerfilCarrera_Response> ListarCarreraNivel4(string vCodCarrera, string vDescripcion)
        {
            List<PerfilCarrera_Response> lista = new List<PerfilCarrera_Response>();
            PerfilCarrera_Response item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePerfilCarreraConsultarNivel4]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@p_vCodCarrera", vCodCarrera));
                cmd.Parameters.Add(new SqlParameter("@p_nombre", vDescripcion));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PerfilCarrera_Response();
                            //item.Grilla = new Grilla_Response();

                            item.strCodCarrera = dr.GetString(dr.GetOrdinal("vCodCarrera"));
                            item.strDescripcion = dr.GetString(dr.GetOrdinal("vDescripcion"));
                            item.iCodTipoCarrera = dr.GetInt32(dr.GetOrdinal("iCodTipoCarrera"));
                            item.bEstado = dr.GetBoolean(dr.GetOrdinal("bEstado"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PerfilCarrera_Response> ListarCarreraNivel1_Mae(int iCodTipoCarrera)
        {
            List<PerfilCarrera_Response> lista = new List<PerfilCarrera_Response>();
            PerfilCarrera_Response item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePerfilCarreraConsultarNivel1_Mae]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@p_iCodSubTipoCarrera", iCodTipoCarrera));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PerfilCarrera_Response();
                            //item.Grilla = new Grilla_Response();

                            item.strCodCarrera = dr.GetString(dr.GetOrdinal("vCodCarrera"));
                            item.strDescripcion = dr.GetString(dr.GetOrdinal("vDescripcion"));
                            item.iCodTipoCarrera = dr.GetInt32(dr.GetOrdinal("iCodTipoCarrera"));
                            item.bEstado = dr.GetBoolean(dr.GetOrdinal("bEstado"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PerfilCarrera_Response> ListarCarreraNivel2_Mae(string vCodCarrera)
        {
            List<PerfilCarrera_Response> lista = new List<PerfilCarrera_Response>();
            PerfilCarrera_Response item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePerfilCarreraConsultarNivel2_Mae]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@p_vCodCarrera", vCodCarrera));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PerfilCarrera_Response();
                            //item.Grilla = new Grilla_Response();

                            item.strCodCarrera = dr.GetString(dr.GetOrdinal("vCodCarrera"));
                            item.strDescripcion = dr.GetString(dr.GetOrdinal("vDescripcion"));
                            item.iCodTipoCarrera = dr.GetInt32(dr.GetOrdinal("iCodTipoCarrera"));
                            item.bEstado = dr.GetBoolean(dr.GetOrdinal("bEstado"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PerfilCarrera_Response> ListarCarreraNivel3_Mae(string vCodCarrera)
        {
            List<PerfilCarrera_Response> lista = new List<PerfilCarrera_Response>();
            PerfilCarrera_Response item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePerfilCarreraConsultarNivel3_Mae]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@p_vCodCarrera", vCodCarrera));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PerfilCarrera_Response();
                            //item.Grilla = new Grilla_Response();

                            item.strCodCarrera = dr.GetString(dr.GetOrdinal("vCodCarrera"));
                            item.strDescripcion = dr.GetString(dr.GetOrdinal("vDescripcion"));
                            item.iCodTipoCarrera = dr.GetInt32(dr.GetOrdinal("iCodTipoCarrera"));
                            item.bEstado = dr.GetBoolean(dr.GetOrdinal("bEstado"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PerfilCarrera_Response> ListarCarreraNivel4_Mae(string vCodCarrera, string vDescripcion)
        {
            List<PerfilCarrera_Response> lista = new List<PerfilCarrera_Response>();
            PerfilCarrera_Response item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePerfilCarreraConsultarNivel4_Mae]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@p_vCodCarrera", vCodCarrera));
                cmd.Parameters.Add(new SqlParameter("@p_nombre", vDescripcion));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PerfilCarrera_Response();
                            //item.Grilla = new Grilla_Response();

                            item.strCodCarrera = dr.GetString(dr.GetOrdinal("vCodCarrera"));
                            item.strDescripcion = dr.GetString(dr.GetOrdinal("vDescripcion"));
                            item.iCodTipoCarrera = dr.GetInt32(dr.GetOrdinal("iCodTipoCarrera"));
                            item.bEstado = dr.GetBoolean(dr.GetOrdinal("bEstado"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PerfilCarrera_Response> ListarCarreraNivel1_Doc(int iCodTipoCarrera)
        {
            List<PerfilCarrera_Response> lista = new List<PerfilCarrera_Response>();
            PerfilCarrera_Response item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePerfilCarreraConsultarNivel1_Doc]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@p_iCodSubTipoCarrera", iCodTipoCarrera));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PerfilCarrera_Response();
                            //item.Grilla = new Grilla_Response();

                            item.strCodCarrera = dr.GetString(dr.GetOrdinal("vCodCarrera"));
                            item.strDescripcion = dr.GetString(dr.GetOrdinal("vDescripcion"));
                            item.iCodTipoCarrera = dr.GetInt32(dr.GetOrdinal("iCodTipoCarrera"));
                            item.bEstado = dr.GetBoolean(dr.GetOrdinal("bEstado"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PerfilCarrera_Response> ListarCarreraNivel2_Doc(string vCodCarrera)
        {
            List<PerfilCarrera_Response> lista = new List<PerfilCarrera_Response>();
            PerfilCarrera_Response item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePerfilCarreraConsultarNivel2_Doc]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@p_vCodCarrera", vCodCarrera));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PerfilCarrera_Response();
                            //item.Grilla = new Grilla_Response();

                            item.strCodCarrera = dr.GetString(dr.GetOrdinal("vCodCarrera"));
                            item.strDescripcion = dr.GetString(dr.GetOrdinal("vDescripcion"));
                            item.iCodTipoCarrera = dr.GetInt32(dr.GetOrdinal("iCodTipoCarrera"));
                            item.bEstado = dr.GetBoolean(dr.GetOrdinal("bEstado"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PerfilCarrera_Response> ListarCarreraNivel3_Doc(string vCodCarrera)
        {
            List<PerfilCarrera_Response> lista = new List<PerfilCarrera_Response>();
            PerfilCarrera_Response item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePerfilCarreraConsultarNivel3_Doc]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@p_vCodCarrera", vCodCarrera));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PerfilCarrera_Response();
                            //item.Grilla = new Grilla_Response();

                            item.strCodCarrera = dr.GetString(dr.GetOrdinal("vCodCarrera"));
                            item.strDescripcion = dr.GetString(dr.GetOrdinal("vDescripcion"));
                            item.iCodTipoCarrera = dr.GetInt32(dr.GetOrdinal("iCodTipoCarrera"));
                            item.bEstado = dr.GetBoolean(dr.GetOrdinal("bEstado"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PerfilCarrera_Response> ListarCarreraNivel4_Doc(string vCodCarrera, string vDescripcion)
        {
            List<PerfilCarrera_Response> lista = new List<PerfilCarrera_Response>();
            PerfilCarrera_Response item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePerfilCarreraConsultarNivel4_Doc]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@p_vCodCarrera", vCodCarrera));
                cmd.Parameters.Add(new SqlParameter("@p_nombre", vDescripcion));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PerfilCarrera_Response();
                            //item.Grilla = new Grilla_Response();

                            item.strCodCarrera = dr.GetString(dr.GetOrdinal("vCodCarrera"));
                            item.strDescripcion = dr.GetString(dr.GetOrdinal("vDescripcion"));
                            item.iCodTipoCarrera = dr.GetInt32(dr.GetOrdinal("iCodTipoCarrera"));
                            item.bEstado = dr.GetBoolean(dr.GetOrdinal("bEstado"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        #region Coordinaciones Internas y Externas
        
        public IEnumerable<PerfilCoordinaciones_Registro> ListarPerfilDetCoordinacionInterna(PerfilCoordinaciones_Registro peticion)
        {
            List<PerfilCoordinaciones_Registro> lista = new List<PerfilCoordinaciones_Registro>();
            PerfilCoordinaciones_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paDetCoordInternaConsultarPorID]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PerfilCoordinaciones_Registro();

                            //item.Grilla = new Grilla_Response();
                            item.iCodPerfil = dr.GetInt32(dr.GetOrdinal("iCodPerfil"));
                            item.iSecuencia = dr.GetInt32(dr.GetOrdinal("iSecuencia"));
                            item.Coordinacion = dr.GetString(dr.GetOrdinal("vDescripcion"));                            

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PerfilCoordinaciones_Registro> ListarPerfilDetCoordinacionExterna(PerfilCoordinaciones_Registro peticion)
        {
            List<PerfilCoordinaciones_Registro> lista = new List<PerfilCoordinaciones_Registro>();
            PerfilCoordinaciones_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paDetCoordExternaConsultarPorID]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PerfilCoordinaciones_Registro();

                            item.iCodPerfil = dr.GetInt32(dr.GetOrdinal("iCodPerfil"));
                            item.iSecuencia = dr.GetInt32(dr.GetOrdinal("iSecuencia"));
                            item.Coordinacion = dr.GetString(dr.GetOrdinal("vDescripcion"));   

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public bool InsertarDetCoordinacionesInt_Ext(PerfilCoordinaciones_Registro peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {

                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paDetCoordinaciones_Registrar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));
                cmd.Parameters.Add(new SqlParameter("@vDescripcion", peticion.Coordinacion));
                cmd.Parameters.Add(new SqlParameter("@iTipoCoordinacion", peticion.iTipoCoordinacion));               

                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        public bool ActualizarDetCoordinacionesInt_Ext(PerfilCoordinaciones_Registro peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paDetCoordinaciones_Actualizar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));
                cmd.Parameters.Add(new SqlParameter("@iSecuencia", peticion.iSecuencia));
                cmd.Parameters.Add(new SqlParameter("@vDescripcion", peticion.Coordinacion));
                
                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        public bool EliminarDetCoordinacionesInt_Ext(PerfilCoordinaciones_Registro peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paDetCoordinaciones_Eliminar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));
                cmd.Parameters.Add(new SqlParameter("@iSecuencia", peticion.iSecuencia));                

                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        #endregion


        #region Conocimientos

        public IEnumerable<PerfilConocimientos_Registro> ListarPerfilDetConocimientosTecnicos(PerfilConocimientos_Registro peticion)
        {
            List<PerfilConocimientos_Registro> lista = new List<PerfilConocimientos_Registro>();
            PerfilConocimientos_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paDetConocTecnicosConsultarPorID]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PerfilConocimientos_Registro();

                            //item.Grilla = new Grilla_Response();
                            item.iCodPerfil = dr.GetInt32(dr.GetOrdinal("iCodPerfil"));
                            item.iSecuencia = dr.GetInt32(dr.GetOrdinal("iSecuencia"));
                            item.Conocimientos = dr.GetString(dr.GetOrdinal("vConocimientos"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PerfilConocimientos_Registro> ListarPerfilDetConocimientosCursosProgramas(PerfilConocimientos_Registro peticion)
        {
            List<PerfilConocimientos_Registro> lista = new List<PerfilConocimientos_Registro>();
            PerfilConocimientos_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paDetConocCurProgConsultarPorID]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PerfilConocimientos_Registro();

                            item.iCodPerfil = dr.GetInt32(dr.GetOrdinal("iCodPerfil"));
                            item.iSecuencia = dr.GetInt32(dr.GetOrdinal("iSecuencia"));
                            item.Conocimientos = dr.GetString(dr.GetOrdinal("vConocimientos"));
                            item.bConDocumento = dr.GetBoolean(dr.GetOrdinal("bConDocumento"));
                            item.PerfilTipoMateria = new PerfilTipoMateria_Response() 
                            {
                                iCodTipoMateria = dr.GetInt32(dr.GetOrdinal("iCodTipoMateria")),
                                strDescripcion = dr.GetString(dr.GetOrdinal("vTipoMateria"))
                            };
                            item.PerfilNivelMateria = new PerfilNivelMateria_Response() 
                            {
                                iCodTipoNivelMateria = dr.GetInt32(dr.GetOrdinal("iCodTipoNivelMateria")),
                                strDescripcion = dr.GetString(dr.GetOrdinal("vTipoNivelMateria"))
                            };                            

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PerfilConocimientos_Registro> ListarPerfilDetConocimientosOfficeIdiomas(PerfilConocimientos_Registro peticion)
        {
            List<PerfilConocimientos_Registro> lista = new List<PerfilConocimientos_Registro>();
            PerfilConocimientos_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paDetConocOfiIdioConsultarPorID]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PerfilConocimientos_Registro();

                            item.iCodPerfil = dr.GetInt32(dr.GetOrdinal("iCodPerfil"));
                            item.iSecuencia = dr.GetInt32(dr.GetOrdinal("iSecuencia"));                            
                            item.bConDocumento = dr.GetBoolean(dr.GetOrdinal("bConDocumento"));
                            item.PerfilTipoMateriaOtros = new PerfilTipoMateriaOtros_Response()
                            {
                                iCodTipoMateriaOtros = dr.GetInt32(dr.GetOrdinal("iCodTipoMateriaOtros")),
                                strDescripcion = dr.GetString(dr.GetOrdinal("vTipoMateriaOtros"))
                            };
                            //item.iCodTipoMateriaOtros = dr.GetInt32(dr.GetOrdinal("iCodTipoMateriaOtros"));
                            //item.strTipoMateriaOtros = dr.GetString(dr.GetOrdinal("vTipoMateriaOtros"));
                            item.PerfilTipoSubMateriaOtros = new PerfilTipoSubMateriaOtros_Response()
                            {
                                iCodTipoSubMateriaOtros = dr.GetInt32(dr.GetOrdinal("iCodTipoSubMateriaOtros")),
                                iCodTipoMateriaOtros = dr.GetInt32(dr.GetOrdinal("iCodTipoMateriaOtros")),
                                strDescripcion = dr.GetString(dr.GetOrdinal("vConocimientos"))
                            };
                            //item.iCodTipoSubMateriaOtros = dr.GetInt32(dr.GetOrdinal("iCodTipoSubMateriaOtros"));
                            //item.strConocimientos = dr.GetString(dr.GetOrdinal("vConocimientos"));
                            item.PerfilNivelMateria = new PerfilNivelMateria_Response()
                            {
                                iCodTipoNivelMateria = dr.GetInt32(dr.GetOrdinal("iCodTipoNivelMateria")),
                                strDescripcion = dr.GetString(dr.GetOrdinal("vTipoNivelMateria"))
                            };  

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public bool InsertarDetConocimientos(PerfilConocimientos_Registro peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {

                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paDetConocimientos_Registrar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));
                if (peticion.Conocimientos != string.Empty)
                {
                    cmd.Parameters.Add(new SqlParameter("@vDescripcion", peticion.Conocimientos));
                }
                if (peticion.bConDocumento != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@bConDocumento", peticion.bConDocumento));
                }
                if (peticion.iCodTipoMateria != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@iCodTipoMateria", peticion.iCodTipoMateria));
                }
                if (peticion.iCodTipoMateriaOtros != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@iCodTipoMateriaOtros", peticion.iCodTipoMateriaOtros));
                }
                if (peticion.iCodTipoSubMateriaOtros != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@iCodTipoSubMateriaOtros", peticion.iCodTipoSubMateriaOtros));
                }
                if (peticion.iTipoNivelMateria != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@iTipoNivelMateria", peticion.iTipoNivelMateria));
                }

                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        public bool ActualizarDetConocimientos(PerfilConocimientos_Registro peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paDetConocimientos_Actualizar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));
                cmd.Parameters.Add(new SqlParameter("@iSecuencia", peticion.iSecuencia));
                if (peticion.Conocimientos != string.Empty)
                {
                    cmd.Parameters.Add(new SqlParameter("@vDescripcion", peticion.Conocimientos));    
                }                
                if (peticion.bConDocumento != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@bConDocumento", peticion.bConDocumento));
                }
                if (peticion.iCodTipoMateria != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@iCodTipoMateria", peticion.iCodTipoMateria));
                }
                if (peticion.iCodTipoMateriaOtros != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@iCodTipoMateriaOtros", peticion.iCodTipoMateriaOtros));
                }
                if (peticion.iCodTipoSubMateriaOtros != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@iCodTipoSubMateriaOtros", peticion.iCodTipoSubMateriaOtros));
                }
                if (peticion.iTipoNivelMateria != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@iTipoNivelMateria", peticion.iTipoNivelMateria));
                }

                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        public bool EliminarDetConocimientos(PerfilConocimientos_Registro peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paDetConocimientos_Eliminar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));
                cmd.Parameters.Add(new SqlParameter("@iSecuencia", peticion.iSecuencia));

                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        #endregion


        #region Formacion Academica

        public IEnumerable<PerfilFormacionAcademica_Registro> ListarPerfilDetFormAcaNivelBasico(PerfilFormacionAcademica_Registro peticion)
        {
            List<PerfilFormacionAcademica_Registro> lista = new List<PerfilFormacionAcademica_Registro>();
            PerfilFormacionAcademica_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paDetFormacionAcademicaBasicaConsultarPorID]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PerfilFormacionAcademica_Registro();

                            //item.Grilla = new Grilla_Response();
                            item.iCodPerfil = dr.GetInt32(dr.GetOrdinal("iCodPerfil"));
                            item.iSecuencia = dr.GetInt32(dr.GetOrdinal("iSecuencia"));

                            if (!Convert.IsDBNull(dr["iCodGrado"]))
                            {
                                item.iCodGrado = dr.GetInt32(dr.GetOrdinal("iCodGrado"));
                                item.strGrado = dr.GetString(dr.GetOrdinal("vGrado"));
                            }
                            //if (!Convert.IsDBNull(dr["iCodNivel"]))
                            //{
                            //    item.iCodNivel = dr.GetInt32(dr.GetOrdinal("iCodNivel"));
                            //    item.strNivel = dr.GetString(dr.GetOrdinal("vNivel"));
                            //}

                            //item.bColegiatura = dr.GetBoolean(dr.GetOrdinal("bColegiatura"));
                            //item.vColegiatura = dr.GetBoolean(dr.GetOrdinal("bColegiatura")) ? "SI" : "NO";
                            //item.bHabilitado = dr.GetBoolean(dr.GetOrdinal("bHabilitado"));
                            //item.vHabilitado = dr.GetBoolean(dr.GetOrdinal("bHabilitado")) ? "SI" : "NO";
                            item.bCompleto = dr.GetBoolean(dr.GetOrdinal("bCompleto"));
                            item.vCompleto = dr.GetBoolean(dr.GetOrdinal("bCompleto")) ? "SI" : "NO";

                            //if (!Convert.IsDBNull(dr["vCodCarrera"])) { item.strCodCarrera = dr.GetString(dr.GetOrdinal("vCodCarrera")); }

                            //if (!Convert.IsDBNull(dr["vCodCarreraN1"]))
                            //{
                            //    item.strCodCarreraN1 = dr.GetString(dr.GetOrdinal("vCodCarreraN1"));
                            //    item.strNivel1 = dr.GetString(dr.GetOrdinal("vNivel1"));
                            //}

                            //if (!Convert.IsDBNull(dr["vCodCarreraN2"]))
                            //{
                            //    item.strCodCarreraN2 = dr.GetString(dr.GetOrdinal("vCodCarreraN2"));
                            //    item.strNivel2 = dr.GetString(dr.GetOrdinal("vNivel2"));
                            //}

                            //if (!Convert.IsDBNull(dr["vCodCarreraN3"]))
                            //{
                            //    item.strCodCarreraN3 = dr.GetString(dr.GetOrdinal("vCodCarreraN3"));
                            //    item.strNivel3 = dr.GetString(dr.GetOrdinal("vNivel3"));
                            //}

                            //if (!Convert.IsDBNull(dr["vCodCarreraN4"]))
                            //{
                            //    item.strCodCarreraN4 = dr.GetString(dr.GetOrdinal("vCodCarreraN4"));
                            //    item.strNivel4 = dr.GetString(dr.GetOrdinal("vNivel4"));
                            //}

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        
        public IEnumerable<PerfilFormacionAcademica_Registro> ListarPerfilDetFormAcaNivelEducativo(PerfilFormacionAcademica_Registro peticion)
        {
            List<PerfilFormacionAcademica_Registro> lista = new List<PerfilFormacionAcademica_Registro>();
            PerfilFormacionAcademica_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paDetFormacionAcademicaPreGradoConsultarPorID]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));                

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PerfilFormacionAcademica_Registro();

                            //item.Grilla = new Grilla_Response();
                            item.iCodPerfil = dr.GetInt32(dr.GetOrdinal("iCodPerfil"));
                            item.iSecuencia = dr.GetInt32(dr.GetOrdinal("iSecuencia"));
                            
                            if (!Convert.IsDBNull(dr["iCodGrado"])) 
                            { 
                                item.iCodGrado = dr.GetInt32(dr.GetOrdinal("iCodGrado"));
                                item.strGrado = dr.GetString(dr.GetOrdinal("vGrado"));
                            }
                            if (!Convert.IsDBNull(dr["iCodNivel"]))
                            {
                                item.iCodNivel = dr.GetInt32(dr.GetOrdinal("iCodNivel"));
                                item.strNivel = dr.GetString(dr.GetOrdinal("vNivel"));
                            }                           
                                                       
                            item.bColegiatura = dr.GetBoolean(dr.GetOrdinal("bColegiatura"));
                            item.vColegiatura = dr.GetBoolean(dr.GetOrdinal("bColegiatura")) ? "SI" : "NO";
                            item.bHabilitado = dr.GetBoolean(dr.GetOrdinal("bHabilitado"));
                            item.vHabilitado = dr.GetBoolean(dr.GetOrdinal("bHabilitado")) ? "SI" : "NO";
                            item.bCompleto = dr.GetBoolean(dr.GetOrdinal("bCompleto"));
                            item.vCompleto = dr.GetBoolean(dr.GetOrdinal("bCompleto")) ? "SI" : "NO";

                            if (!Convert.IsDBNull(dr["vCodCarrera"])) { item.strCodCarrera = dr.GetString(dr.GetOrdinal("vCodCarrera")); }

                            if (!Convert.IsDBNull(dr["vCodCarreraN1"])) 
                            {
                                item.strCodCarreraN1 = dr.GetString(dr.GetOrdinal("vCodCarreraN1"));
                                item.strNivel1 = dr.GetString(dr.GetOrdinal("vNivel1"));
                            }

                            if (!Convert.IsDBNull(dr["vCodCarreraN2"])) 
                            {
                                item.strCodCarreraN2 = dr.GetString(dr.GetOrdinal("vCodCarreraN2"));
                                item.strNivel2 = dr.GetString(dr.GetOrdinal("vNivel2"));
                            }

                            if (!Convert.IsDBNull(dr["vCodCarreraN3"])) 
                            {
                                item.strCodCarreraN3 = dr.GetString(dr.GetOrdinal("vCodCarreraN3"));
                                item.strNivel3 = dr.GetString(dr.GetOrdinal("vNivel3"));
                            }

                            if (!Convert.IsDBNull(dr["vCodCarreraN4"])) 
                            {
                                item.strCodCarreraN4 = dr.GetString(dr.GetOrdinal("vCodCarreraN4"));
                                item.strNivel4 = dr.GetString(dr.GetOrdinal("vNivel4"));
                            }

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PerfilFormacionAcademica_Registro> ListarPerfilDetFormAcaMaestria(PerfilFormacionAcademica_Registro peticion)
        {
            List<PerfilFormacionAcademica_Registro> lista = new List<PerfilFormacionAcademica_Registro>();
            PerfilFormacionAcademica_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paDetFormacionAcademicaMaestriaConsultarPorID]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PerfilFormacionAcademica_Registro();

                            //item.Grilla = new Grilla_Response();
                            item.iCodPerfil = dr.GetInt32(dr.GetOrdinal("iCodPerfil"));
                            item.iSecuencia = dr.GetInt32(dr.GetOrdinal("iSecuencia"));

                            if (!Convert.IsDBNull(dr["iCodGrado"]))
                            {
                                item.iCodGrado = dr.GetInt32(dr.GetOrdinal("iCodGrado"));
                                item.strGrado = dr.GetString(dr.GetOrdinal("vGrado"));
                            }
                            if (!Convert.IsDBNull(dr["iCodNivel"]))
                            {
                                item.iCodNivel = dr.GetInt32(dr.GetOrdinal("iCodNivel"));
                                item.strNivel = dr.GetString(dr.GetOrdinal("vNivel"));
                            }

                            item.bColegiatura = dr.GetBoolean(dr.GetOrdinal("bColegiatura"));
                            item.vColegiatura = dr.GetBoolean(dr.GetOrdinal("bColegiatura")) ? "SI" : "NO";
                            item.bHabilitado = dr.GetBoolean(dr.GetOrdinal("bHabilitado"));
                            item.vHabilitado = dr.GetBoolean(dr.GetOrdinal("bHabilitado")) ? "SI" : "NO";
                            item.bCompleto = dr.GetBoolean(dr.GetOrdinal("bCompleto"));
                            item.vCompleto = dr.GetBoolean(dr.GetOrdinal("bCompleto")) ? "SI" : "NO";

                            if (!Convert.IsDBNull(dr["vCodCarrera"])) { item.strCodCarrera = dr.GetString(dr.GetOrdinal("vCodCarrera")); }

                            if (!Convert.IsDBNull(dr["vCodCarreraN1"]))
                            {
                                item.strCodCarreraN1 = dr.GetString(dr.GetOrdinal("vCodCarreraN1"));
                                item.strNivel1 = dr.GetString(dr.GetOrdinal("vNivel1"));
                            }

                            if (!Convert.IsDBNull(dr["vCodCarreraN2"]))
                            {
                                item.strCodCarreraN2 = dr.GetString(dr.GetOrdinal("vCodCarreraN2"));
                                item.strNivel2 = dr.GetString(dr.GetOrdinal("vNivel2"));
                            }

                            if (!Convert.IsDBNull(dr["vCodCarreraN3"]))
                            {
                                item.strCodCarreraN3 = dr.GetString(dr.GetOrdinal("vCodCarreraN3"));
                                item.strNivel3 = dr.GetString(dr.GetOrdinal("vNivel3"));
                            }

                            if (!Convert.IsDBNull(dr["vCodCarreraN4"]))
                            {
                                item.strCodCarreraN4 = dr.GetString(dr.GetOrdinal("vCodCarreraN4"));
                                item.strNivel4 = dr.GetString(dr.GetOrdinal("vNivel4"));
                            }

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PerfilFormacionAcademica_Registro> ListarPerfilDetFormAcaDoctorado(PerfilFormacionAcademica_Registro peticion)
        {
            List<PerfilFormacionAcademica_Registro> lista = new List<PerfilFormacionAcademica_Registro>();
            PerfilFormacionAcademica_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paDetFormacionAcademicaDoctoradoConsultarPorID]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PerfilFormacionAcademica_Registro();

                            //item.Grilla = new Grilla_Response();
                            item.iCodPerfil = dr.GetInt32(dr.GetOrdinal("iCodPerfil"));
                            item.iSecuencia = dr.GetInt32(dr.GetOrdinal("iSecuencia"));

                            if (!Convert.IsDBNull(dr["iCodGrado"]))
                            {
                                item.iCodGrado = dr.GetInt32(dr.GetOrdinal("iCodGrado"));
                                item.strGrado = dr.GetString(dr.GetOrdinal("vGrado"));
                            }
                            if (!Convert.IsDBNull(dr["iCodNivel"]))
                            {
                                item.iCodNivel = dr.GetInt32(dr.GetOrdinal("iCodNivel"));
                                item.strNivel = dr.GetString(dr.GetOrdinal("vNivel"));
                            }

                            item.bColegiatura = dr.GetBoolean(dr.GetOrdinal("bColegiatura"));
                            item.vColegiatura = dr.GetBoolean(dr.GetOrdinal("bColegiatura")) ? "SI" : "NO";
                            item.bHabilitado = dr.GetBoolean(dr.GetOrdinal("bHabilitado"));
                            item.vHabilitado = dr.GetBoolean(dr.GetOrdinal("bHabilitado")) ? "SI" : "NO";
                            item.bCompleto = dr.GetBoolean(dr.GetOrdinal("bCompleto"));
                            item.vCompleto = dr.GetBoolean(dr.GetOrdinal("bCompleto")) ? "SI" : "NO";

                            if (!Convert.IsDBNull(dr["vCodCarrera"])) { item.strCodCarrera = dr.GetString(dr.GetOrdinal("vCodCarrera")); }

                            if (!Convert.IsDBNull(dr["vCodCarreraN1"]))
                            {
                                item.strCodCarreraN1 = dr.GetString(dr.GetOrdinal("vCodCarreraN1"));
                                item.strNivel1 = dr.GetString(dr.GetOrdinal("vNivel1"));
                            }

                            if (!Convert.IsDBNull(dr["vCodCarreraN2"]))
                            {
                                item.strCodCarreraN2 = dr.GetString(dr.GetOrdinal("vCodCarreraN2"));
                                item.strNivel2 = dr.GetString(dr.GetOrdinal("vNivel2"));
                            }

                            if (!Convert.IsDBNull(dr["vCodCarreraN3"]))
                            {
                                item.strCodCarreraN3 = dr.GetString(dr.GetOrdinal("vCodCarreraN3"));
                                item.strNivel3 = dr.GetString(dr.GetOrdinal("vNivel3"));
                            }

                            if (!Convert.IsDBNull(dr["vCodCarreraN4"]))
                            {
                                item.strCodCarreraN4 = dr.GetString(dr.GetOrdinal("vCodCarreraN4"));
                                item.strNivel4 = dr.GetString(dr.GetOrdinal("vNivel4"));
                            }

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        
        public bool InsertarDetFormacionAcademica(PerfilFormacionAcademica_Registro peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {

                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paDetFormacionAcademica_Registrar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));
                if (peticion.strCodCarrera != string.Empty)
                {
                    cmd.Parameters.Add(new SqlParameter("@vCodCarrera", peticion.strCodCarrera));
                }
                if (peticion.iCodGrado != null && peticion.iCodGrado > 0)
                {
                    cmd.Parameters.Add(new SqlParameter("@iCodGrado", peticion.iCodGrado));
                }
                if (peticion.iCodNivel != null && peticion.iCodNivel > 0)
                {
                    cmd.Parameters.Add(new SqlParameter("@iCodNivel", peticion.iCodNivel));
                }
                if (peticion.bColegiatura != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@bColegiatura", peticion.bColegiatura));
                }
                if (peticion.bHabilitado != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@bHabilitado", peticion.bHabilitado));
                }
                if (peticion.bCompleto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@bCompleto", peticion.bCompleto));
                }
                if (peticion.iCodSubTipoCarrera != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@iCodSubTipoCarrera", peticion.iCodSubTipoCarrera));
                }


                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }
        public bool InsertarCarreraProfesional(PerfilFormacionAcademica_Registro peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {

                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paInsFormacionAcademica]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@p_vDescripcion", peticion.strNivel4));
                
                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }
        public bool InsertarMaestria(PerfilFormacionAcademica_Registro peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {

                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paInsFormacionAcademicaMaestria]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@p_vDescripcion", peticion.strNivel4));

                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }
        public bool InsertarDoctorado(PerfilFormacionAcademica_Registro peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {

                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paInsFormacionAcademicaDoctorado]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@p_vDescripcion", peticion.strNivel4));

                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        public bool ActualizarDetFormacionAcademica(PerfilFormacionAcademica_Registro peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paDetFormacionAcademica_Actualizar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));
                cmd.Parameters.Add(new SqlParameter("@iSecuencia", peticion.iSecuencia));
                if (peticion.strCodCarrera != string.Empty)
                {
                    cmd.Parameters.Add(new SqlParameter("@vCodCarrera", peticion.strCodCarrera));
                }
                if (peticion.iCodGrado != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@iCodGrado", peticion.iCodGrado));
                }
                if (peticion.iCodNivel != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@iCodNivel", peticion.iCodNivel));
                }
                if (peticion.bColegiatura != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@bColegiatura", peticion.bColegiatura));
                }
                if (peticion.bHabilitado != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@bHabilitado", peticion.bHabilitado));
                }
                if (peticion.bCompleto != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@bCompleto", peticion.bCompleto));
                }
                if (peticion.iCodSubTipoCarrera != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@iCodSubTipoCarrera", peticion.iCodSubTipoCarrera));
                }

                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        public bool EliminarDetFormacionAcademica(PerfilFormacionAcademica_Registro peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paDetFormacionAcademica_Eliminar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));
                cmd.Parameters.Add(new SqlParameter("@iSecuencia", peticion.iSecuencia));

                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        #endregion

        public bool PerfilFinalizar(PerfilPuestoRegistro peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paSISGESRRHH_MaePerfil_Finalizar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));             

                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }
        public bool PerfilEliminar(PerfilPuestoRegistro peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paSISGESRRHH_MaePerfil_Eliminar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));

                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        public bool PerfilDerivarUser(PerfilPuestoRegistro peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePerfilMovimientoAprobarUser]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabajadorEnvia", peticion.iCodTrabajador));
                if (peticion.strObservacion != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@vObservacion", peticion.strObservacion));
                }
                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }
        public bool PerfilDerivarJefe(PerfilPuestoRegistro peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePerfilMovimientoAprobarJefe]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabajadorEnvia", peticion.iCodTrabajador));
                if (peticion.strObservacion != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@vObservacion", peticion.strObservacion));
                }
                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        public bool PerfilDerivarJefeRRHH(PerfilPuestoRegistro peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePerfilMovimientoJefeAprobarRRHH]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabajadorEnvia", peticion.iCodTrabajador));
                if (peticion.strObservacion != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@vObservacion", peticion.strObservacion));
                }
                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        public bool PerfilDerivarUserRRHH(PerfilPuestoRegistro peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePerfilMovimientoAprobarRRHH]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabajadorEnvia", peticion.iCodTrabajador));
                if (peticion.strObservacion != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@vObservacion", peticion.strObservacion));
                }
                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }
        public bool PerfilDesaprobar(PerfilPuestoRegistro peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePerfilMovimientoDesaprobar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", peticion.iCodPerfil));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabajadorEnvia", peticion.iCodTrabajador));
                if (peticion.strObservacion != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@vObservacion", peticion.strObservacion));
                }
                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        public IEnumerable<PerfilPuestoRegistro> ListarPerfilHistorico(string id)
        {
            List<PerfilPuestoRegistro> lista = new List<PerfilPuestoRegistro>();
            PerfilPuestoRegistro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePerfilHistorico]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", Convert.ToInt32(id)));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PerfilPuestoRegistro();

                            item.datFechaReg = dr.GetDateTime(dr.GetOrdinal("dFechaReg"));
                            item.strFechaReg = Convert.ToDateTime(dr.GetDateTime(dr.GetOrdinal("dFechaReg"))).ToString("dd/MM/yyyy");
                            item.strUnidadOrganica_Envia = dr.GetString(dr.GetOrdinal("vUnidadOrganica_Envia"));
                            item.strUnidadOrganica_Recibe = dr.GetString(dr.GetOrdinal("vUnidadOrganica_Recibe"));
                            if (!Convert.IsDBNull(dr["vObservacion"]))
                            {
                                item.strObservacion = dr.GetString(dr.GetOrdinal("vObservacion"));
                            }
                            
                            item.strEstadoAprobacion = dr.GetString(dr.GetOrdinal("vEstadoAprobacion"));
                            
                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public Int32 RegistrarPerfilArchivo(PerfilPuestoRegistro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_Perfil_file_ins]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_PERFIL", registro.iCodPerfil));
                if (registro.archivo != null) cmd.Parameters.Add(new SqlParameter("@ARCHIVO", registro.archivo));
                cmd.Parameters.Add(new SqlParameter("@USUARIO_MODIFICACION", registro.IdUsuarioModificacion));
                cmd.Parameters.Add(new SqlParameter("@FECHA_MODIFICACION", registro.FechaModificacion));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.iCodPerfil;
        }
    }
}
