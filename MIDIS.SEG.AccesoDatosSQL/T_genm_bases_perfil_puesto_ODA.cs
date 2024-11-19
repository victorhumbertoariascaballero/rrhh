using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using MIDIS.ORI.Entidades;

namespace MIDIS.SEG.AccesoDatosSQL
{
    public partial class T_genm_bases_perfil_puesto_ODA
    {
        BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWork = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnOracle"]].ConnectionString));

        public Int32 InsertarBasesPerfilPuesto(BasesPerfilPuestoRegistro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paSISGESRRHH_MaeBasesPerfil_Registrar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajadorReg", registro.iCodTrabajadorReg));
                cmd.Parameters.Add(new SqlParameter("@iCantPersonalRequerido", registro.iCantPersonalRequerido));
                cmd.Parameters.Add(new SqlParameter("@vDuracionContrato", registro.strDuracionContrato));
                cmd.Parameters.Add(new SqlParameter("@bDuracionContrato31Diciembre", registro.bDuracionContrato31Diciembre));
                cmd.Parameters.Add(new SqlParameter("@fRemuneracion", registro.decRemuneracion));
                cmd.Parameters.Add(new SqlParameter("@iTipoBase", registro.iTipoBase));
                //cmd.Parameters.Add(new SqlParameter("@dFechaAprobConv", registro.dFechaAprobConv));
                //cmd.Parameters.Add(new SqlParameter("@dFechaDesdePubSNE_MTPE", registro.dFechaDesdePubSNE_MTPE));
                //cmd.Parameters.Add(new SqlParameter("@dFechaHastaPubSNE_MTPE", registro.dFechaHastaPubSNE_MTPE));
                cmd.Parameters.Add(new SqlParameter("@dFechaDesdePubMIDIS", registro.dFechaDesdePubMIDIS));
                cmd.Parameters.Add(new SqlParameter("@dFechaHastaPubMIDIS", registro.dFechaHastaPubMIDIS));
                cmd.Parameters.Add(new SqlParameter("@dFechaRegCVPostulante", registro.dFechaRegCVPostulante));
                cmd.Parameters.Add(new SqlParameter("@dFechaDesdeEvaCV", registro.dFechaDesdeEvaCV));
                cmd.Parameters.Add(new SqlParameter("@dFechaHastaEvaCV", registro.dFechaHastaEvaCV));
                cmd.Parameters.Add(new SqlParameter("@dFechaPubResultadoMIDIS", registro.dFechaPubResultadoMIDIS));
                cmd.Parameters.Add(new SqlParameter("@dFechaDesdeEntrevista", registro.dFechaDesdeEntrevista));
                cmd.Parameters.Add(new SqlParameter("@dFechaHastaEntrevista", registro.dFechaHastaEntrevista));
                cmd.Parameters.Add(new SqlParameter("@dFechaPubResultadoFinalMIDIS", registro.dFechaPubResultadoFinalMIDIS));
                cmd.Parameters.Add(new SqlParameter("@dFechaDesdeSuscripcionContrato", registro.dFechaDesdeSuscripcionContrato));
                cmd.Parameters.Add(new SqlParameter("@dFechaHastaSuscripcionContrato", registro.dFechaHastaSuscripcionContrato));
                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", registro.iCodPerfil));
                cmd.Parameters.Add(new SqlParameter("@iExamenConocimiento", registro.IdExamenConocimiento));

                if (registro.dFechaConocimiento == DateTime.MinValue)
                    cmd.Parameters.Add(new SqlParameter("@dFechaConocimiento", null));
                else
                    cmd.Parameters.Add(new SqlParameter("@dFechaConocimiento", registro.dFechaConocimiento));

                if (registro.dFechaPubConocimiento == DateTime.MinValue)
                    cmd.Parameters.Add(new SqlParameter("@dFechaPubConocimiento", null));
                else
                    cmd.Parameters.Add(new SqlParameter("@dFechaPubConocimiento", registro.dFechaPubConocimiento));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.iCodPerfil;
        }

        public Int32 ActualizarBasesPerfilPuesto(BasesPerfilPuestoRegistro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paSISGESRRHH_MaeBasesPerfil_Actualizar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;
                cmd.Parameters.Add(new SqlParameter("@iCodBasePerfil", registro.iCodBasePerfil));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabajadorMod", registro.iCodTrabajadorMod));
                cmd.Parameters.Add(new SqlParameter("@iCantPersonalRequerido", registro.iCantPersonalRequerido));
                cmd.Parameters.Add(new SqlParameter("@vDuracionContrato", registro.strDuracionContrato));
                cmd.Parameters.Add(new SqlParameter("@bDuracionContrato31Diciembre", registro.bDuracionContrato31Diciembre));
                cmd.Parameters.Add(new SqlParameter("@fRemuneracion", registro.decRemuneracion));
                //cmd.Parameters.Add(new SqlParameter("@dFechaAprobConv", registro.dFechaAprobConv));
                //cmd.Parameters.Add(new SqlParameter("@dFechaDesdePubSNE_MTPE", registro.dFechaDesdePubSNE_MTPE));
                //cmd.Parameters.Add(new SqlParameter("@dFechaHastaPubSNE_MTPE", registro.dFechaHastaPubSNE_MTPE));
                cmd.Parameters.Add(new SqlParameter("@dFechaDesdePubMIDIS", registro.dFechaDesdePubMIDIS));
                cmd.Parameters.Add(new SqlParameter("@dFechaHastaPubMIDIS", registro.dFechaHastaPubMIDIS));
                cmd.Parameters.Add(new SqlParameter("@dFechaRegCVPostulante", registro.dFechaRegCVPostulante));
                cmd.Parameters.Add(new SqlParameter("@dFechaDesdeEvaCV", registro.dFechaDesdeEvaCV));
                cmd.Parameters.Add(new SqlParameter("@dFechaHastaEvaCV", registro.dFechaHastaEvaCV));
                cmd.Parameters.Add(new SqlParameter("@dFechaPubResultadoMIDIS", registro.dFechaPubResultadoMIDIS));
                cmd.Parameters.Add(new SqlParameter("@dFechaDesdeEntrevista", registro.dFechaDesdeEntrevista));
                cmd.Parameters.Add(new SqlParameter("@dFechaHastaEntrevista", registro.dFechaHastaEntrevista));
                cmd.Parameters.Add(new SqlParameter("@dFechaPubResultadoFinalMIDIS", registro.dFechaPubResultadoFinalMIDIS));
                cmd.Parameters.Add(new SqlParameter("@dFechaDesdeSuscripcionContrato", registro.dFechaDesdeSuscripcionContrato));
                cmd.Parameters.Add(new SqlParameter("@dFechaHastaSuscripcionContrato", registro.dFechaHastaSuscripcionContrato));
                cmd.Parameters.Add(new SqlParameter("@iCodPerfil", registro.iCodPerfil));
                cmd.Parameters.Add(new SqlParameter("@iExamenConocimiento", registro.IdExamenConocimiento));
                if (registro.dFechaConocimiento == DateTime.MinValue)
                    cmd.Parameters.Add(new SqlParameter("@dFechaConocimiento", null));
                else
                    cmd.Parameters.Add(new SqlParameter("@dFechaConocimiento", registro.dFechaConocimiento));

                if (registro.dFechaPubConocimiento == DateTime.MinValue)
                    cmd.Parameters.Add(new SqlParameter("@dFechaPubConocimiento", null));
                else
                    cmd.Parameters.Add(new SqlParameter("@dFechaPubConocimiento", registro.dFechaPubConocimiento));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.iCodBasePerfil;
        }

        public Int32 AprobarBasesPerfilPuesto(BasesPerfilPuestoRegistro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paSISGESRRHH_MaeBasesPerfil_Aprobar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;
                cmd.Parameters.Add(new SqlParameter("@iCodBasePerfil", registro.iCodBasePerfil));                
                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.iCodBasePerfil;
        }

        public IEnumerable<BasesPerfilPuesto_Request> ObtenerBasesPerfilesPuestoConvocatoria(Int32 idDependencia, Int32 iTipo)
        {
            List<BasesPerfilPuesto_Request> lista = new List<BasesPerfilPuesto_Request>();
            BasesPerfilPuesto_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paBasesPerfilesPuestoConsultarXConvocatoria]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idDependencia", idDependencia));
                cmd.Parameters.Add(new SqlParameter("@iTipo", iTipo));

                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new BasesPerfilPuesto_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();

                            be.iCodBasePerfil = dr.GetInt32(dr.GetOrdinal("iCodBasePerfil"));
                            be.strNroCAS = dr.GetString(dr.GetOrdinal("vNroCAS"));
                            be.dFechaReg = dr.GetDateTime(dr.GetOrdinal("dFechaReg"));
                            be.iCodTrabajadorReg = dr.GetInt32(dr.GetOrdinal("iCodTrabajadorReg"));
                            be.iCantPersonalRequerido = dr.GetInt32(dr.GetOrdinal("iCantPersonalRequerido"));
                            be.strDuracionContrato = dr.GetString(dr.GetOrdinal("vDuracionContrato"));
                            be.decRemuneracion = dr.GetDecimal(dr.GetOrdinal("fRemuneracion"));
                            //be.dFechaAprobConv = dr.GetDateTime(dr.GetOrdinal("dFechaAprobConv"));
                            //be.dFechaDesdePubSNE_MTPE = dr.GetDateTime(dr.GetOrdinal("dFechaDesdePubSNE_MTPE"));
                            //be.dFechaHastaPubSNE_MTPE = dr.GetDateTime(dr.GetOrdinal("dFechaHastaPubSNE_MTPE"));
                            be.dFechaDesdePubMIDIS = dr.GetDateTime(dr.GetOrdinal("dFechaDesdePubMIDIS"));
                            be.dFechaHastaPubMIDIS = dr.GetDateTime(dr.GetOrdinal("dFechaHastaPubMIDIS"));
                            be.dFechaRegCVPostulante = dr.GetDateTime(dr.GetOrdinal("dFechaRegCVPostulante"));
                            be.dFechaDesdeEvaCV = dr.GetDateTime(dr.GetOrdinal("dFechaDesdeEvaCV"));
                            be.dFechaHastaEvaCV = dr.GetDateTime(dr.GetOrdinal("dFechaHastaEvaCV"));
                            be.dFechaPubResultadoMIDIS = dr.GetDateTime(dr.GetOrdinal("dFechaPubResultadoMIDIS"));
                            be.dFechaDesdeEntrevista = dr.GetDateTime(dr.GetOrdinal("dFechaDesdeEntrevista"));
                            be.dFechaHastaEntrevista = dr.GetDateTime(dr.GetOrdinal("dFechaHastaEntrevista"));
                            be.dFechaPubResultadoFinalMIDIS = dr.GetDateTime(dr.GetOrdinal("dFechaPubResultadoFinalMIDIS"));
                            be.dFechaDesdeSuscripcionContrato = dr.GetDateTime(dr.GetOrdinal("dFechaDesdeSuscripcionContrato"));
                            be.dFechaHastaSuscripcionContrato = dr.GetDateTime(dr.GetOrdinal("dFechaHastaSuscripcionContrato"));
                            be.iCodPerfil = dr.GetInt32(dr.GetOrdinal("iCodPerfil"));
                            be.bEstado = dr.GetBoolean(dr.GetOrdinal("bEstado"));
                            be.strOrgano = dr.GetString(dr.GetOrdinal("vOrgano"));
                            be.strUnidadOrganica = dr.GetString(dr.GetOrdinal("vUnidadOrganica"));
                            be.strNombrePuesto = dr.GetString(dr.GetOrdinal("vNombrePuesto"));
                            be.cEstadoAprobado = dr.GetString(dr.GetOrdinal("cEstadoAprobado"));
                            be.strEstadoAprobado = dr.GetString(dr.GetOrdinal("strEstadoAprobado"));
                            be.cEstadoPublicacion = dr.GetString(dr.GetOrdinal("cEstadoPublicacion"));
                            be.strEstadoPublicacion = dr.GetString(dr.GetOrdinal("strEstadoPublicacion"));
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<BasesPerfilPuestoRegistro> ObtenerBasesPerfilesPuesto(String strOrgano, String strUO, String strEstado, string fechaIni, string fechaFin)
        {
            List<BasesPerfilPuestoRegistro> lista = new List<BasesPerfilPuestoRegistro>();
            BasesPerfilPuestoRegistro be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paBasesPerfilesPuestoConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;
                
                cmd.Parameters.Add(new SqlParameter("@iCodigoOrgano", strOrgano));
                cmd.Parameters.Add(new SqlParameter("@iCodigoUUOO", strUO));
                cmd.Parameters.Add(new SqlParameter("@vEstado", strEstado.ToUpper()));

                //if (fechaIni != null && fechaIni != string.Empty && fechaFin != null && fechaFin != string.Empty)
                //{
                //    cmd.Parameters.Add(new SqlParameter("@vFechaInicio", Convert.ToDateTime(fechaIni).ToString("dd/MM/yyyy")));
                //    cmd.Parameters.Add(new SqlParameter("@vFechaFin", Convert.ToDateTime(fechaFin).ToString("dd/MM/yyyy")));
                //}

                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new BasesPerfilPuestoRegistro();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();

                            be.iCodBasePerfil = dr.GetInt32(dr.GetOrdinal("iCodBasePerfil"));
                            //be.iCodUnidadOrganica = dr.GetInt32(dr.GetOrdinal("iCodigoDependencia"));
                            be.strUnidadOrganica = dr.GetString(dr.GetOrdinal("vUnidadOrganica"));
                            be.strOrgano = dr.GetString(dr.GetOrdinal("vOrgano"));
                            be.strNroCAS = dr.GetString(dr.GetOrdinal("vNroCAS"));
                            be.dFechaReg = dr.GetDateTime(dr.GetOrdinal("dFechaReg"));
                            be.iCodTrabajadorReg = dr.GetInt32(dr.GetOrdinal("iCodTrabajadorReg"));
                            be.iCantPersonalRequerido = dr.GetInt32(dr.GetOrdinal("iCantPersonalRequerido"));
                            be.strDuracionContrato = dr.GetString(dr.GetOrdinal("vDuracionContrato"));
                            be.decRemuneracion = dr.GetDecimal(dr.GetOrdinal("fRemuneracion"));
                            //be.dFechaAprobConv = dr.GetDateTime(dr.GetOrdinal("dFechaAprobConv"));
                            //be.dFechaDesdePubSNE_MTPE = dr.GetDateTime(dr.GetOrdinal("dFechaDesdePubSNE_MTPE"));
                            //be.dFechaHastaPubSNE_MTPE = dr.GetDateTime(dr.GetOrdinal("dFechaHastaPubSNE_MTPE"));
                            be.dFechaDesdePubMIDIS = dr.GetDateTime(dr.GetOrdinal("dFechaDesdePubMIDIS"));
                            be.dFechaHastaPubMIDIS = dr.GetDateTime(dr.GetOrdinal("dFechaHastaPubMIDIS"));
                            be.dFechaRegCVPostulante = dr.GetDateTime(dr.GetOrdinal("dFechaRegCVPostulante"));
                            be.dFechaDesdeEvaCV = dr.GetDateTime(dr.GetOrdinal("dFechaDesdeEvaCV"));
                            be.dFechaHastaEvaCV = dr.GetDateTime(dr.GetOrdinal("dFechaHastaEvaCV"));
                            be.dFechaPubResultadoMIDIS = dr.GetDateTime(dr.GetOrdinal("dFechaPubResultadoMIDIS"));
                            be.dFechaDesdeEntrevista = dr.GetDateTime(dr.GetOrdinal("dFechaDesdeEntrevista"));
                            be.dFechaHastaEntrevista = dr.GetDateTime(dr.GetOrdinal("dFechaHastaEntrevista"));
                            be.dFechaPubResultadoFinalMIDIS = dr.GetDateTime(dr.GetOrdinal("dFechaPubResultadoFinalMIDIS"));
                            be.dFechaDesdeSuscripcionContrato = dr.GetDateTime(dr.GetOrdinal("dFechaDesdeSuscripcionContrato"));
                            be.dFechaHastaSuscripcionContrato = dr.GetDateTime(dr.GetOrdinal("dFechaHastaSuscripcionContrato"));
                            be.iCodPerfil = dr.GetInt32(dr.GetOrdinal("iCodPerfil"));
                            be.bEstado = dr.GetBoolean(dr.GetOrdinal("bEstado"));
                            be.strNombrePuesto = dr.GetString(dr.GetOrdinal("vNombrePuesto"));
                            be.cEstadoAprobado = dr.GetString(dr.GetOrdinal("cEstadoAprobado"));
                            be.strEstadoAprobado = dr.GetString(dr.GetOrdinal("strEstadoAprobado"));
                            be.cEstadoPublicacion = dr.GetString(dr.GetOrdinal("cEstadoPublicacion"));
                            be.strEstadoPublicacion = dr.GetString(dr.GetOrdinal("strEstadoPublicacion"));
                            be.IdTieneArchivo = dr.GetInt32(dr.GetOrdinal("TieneArchivo"));
                            
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<BasesPerfilPuestoRegistro> ObtenerBasesPerfilesPuestoPorID(string id)
        {
            List<BasesPerfilPuestoRegistro> lista = new List<BasesPerfilPuestoRegistro>();
            BasesPerfilPuestoRegistro be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paBasesPerfilesPuestoConsultarPorID]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodBasePerfil", Convert.ToInt32(id)));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new BasesPerfilPuestoRegistro();
                            be.Grilla = new Grilla_Response();

                            be.iCodBasePerfil = dr.GetInt32(dr.GetOrdinal("iCodBasePerfil"));
                            be.strNroCAS = dr.GetString(dr.GetOrdinal("vNroCAS"));
                            be.dFechaReg = dr.GetDateTime(dr.GetOrdinal("dFechaReg"));
                            be.iCodTrabajadorReg = dr.GetInt32(dr.GetOrdinal("iCodTrabajadorReg"));
                            be.iCantPersonalRequerido = dr.GetInt32(dr.GetOrdinal("iCantPersonalRequerido"));
                            be.strDuracionContrato = dr.GetString(dr.GetOrdinal("vDuracionContrato"));
                            be.bDuracionContrato31Diciembre = dr.GetBoolean(dr.GetOrdinal("bDuracionContrato31Diciembre"));
                            be.decRemuneracion = dr.GetDecimal(dr.GetOrdinal("fRemuneracion"));
                            //be.dFechaAprobConv = dr.GetDateTime(dr.GetOrdinal("dFechaAprobConv"));
                            //be.dFechaDesdePubSNE_MTPE = dr.GetDateTime(dr.GetOrdinal("dFechaDesdePubSNE_MTPE"));
                            //be.dFechaHastaPubSNE_MTPE = dr.GetDateTime(dr.GetOrdinal("dFechaHastaPubSNE_MTPE"));
                            be.dFechaDesdePubMIDIS = dr.GetDateTime(dr.GetOrdinal("dFechaDesdePubMIDIS"));
                            be.dFechaHastaPubMIDIS = dr.GetDateTime(dr.GetOrdinal("dFechaHastaPubMIDIS"));
                            be.dFechaRegCVPostulante = dr.GetDateTime(dr.GetOrdinal("dFechaRegCVPostulante"));
                            be.dFechaDesdeEvaCV = dr.GetDateTime(dr.GetOrdinal("dFechaDesdeEvaCV"));
                            be.dFechaHastaEvaCV = dr.GetDateTime(dr.GetOrdinal("dFechaHastaEvaCV"));
                            be.dFechaPubResultadoMIDIS = dr.GetDateTime(dr.GetOrdinal("dFechaPubResultadoMIDIS"));
                            be.dFechaDesdeEntrevista = dr.GetDateTime(dr.GetOrdinal("dFechaDesdeEntrevista"));
                            be.dFechaHastaEntrevista = dr.GetDateTime(dr.GetOrdinal("dFechaHastaEntrevista"));
                            be.dFechaPubResultadoFinalMIDIS = dr.GetDateTime(dr.GetOrdinal("dFechaPubResultadoFinalMIDIS"));
                            be.dFechaDesdeSuscripcionContrato = dr.GetDateTime(dr.GetOrdinal("dFechaDesdeSuscripcionContrato"));
                            be.dFechaHastaSuscripcionContrato = dr.GetDateTime(dr.GetOrdinal("dFechaHastaSuscripcionContrato"));
                            be.iCodPerfil = dr.GetInt32(dr.GetOrdinal("iCodPerfil"));
                            be.bEstado = dr.GetBoolean(dr.GetOrdinal("bEstado"));
                            be.strUnidadOrganica = dr.GetString(dr.GetOrdinal("vUnidadOrganica"));
                            be.strNombrePuesto = dr.GetString(dr.GetOrdinal("vNombrePuesto"));
                            be.strMeta = dr.GetString(dr.GetOrdinal("Meta"));

                            be.IdExamenConocimiento = dr.GetInt32(dr.GetOrdinal("bTieneExamenConoc"));
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaExamenConoc"))) be.dFechaConocimiento = dr.GetDateTime(dr.GetOrdinal("dFechaExamenConoc"));
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaPubResultadoConoc"))) be.dFechaPubConocimiento = dr.GetDateTime(dr.GetOrdinal("dFechaPubResultadoConoc"));

                            if (!Convert.IsDBNull(dr["archivo"]))
                            {
                                be.IdTieneArchivo = 1;
                                be.archivo = (byte[])(dr["archivo"]);
                            }

                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PerfilPuesto_Request> ListarPerfilPuesto(string strPerfilPuesto, int tipo)
        {
            List<PerfilPuesto_Request> lista = new List<PerfilPuesto_Request>();
            PerfilPuesto_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPerfilesPuestoConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;
                //if (strPerfilPuesto != null && strPerfilPuesto != string.Empty)
                //{
                    cmd.Parameters.Add(new SqlParameter("@vNombrePuesto", strPerfilPuesto));
                cmd.Parameters.Add(new SqlParameter("@iTipoPerfil", tipo));
                //}                                
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
                            be.strOrgano = dr.GetString(dr.GetOrdinal("vOrgano"));
                            be.strUnidadOrganica = dr.GetString(dr.GetOrdinal("vUnidadOrganica"));
                            be.strNombrePuesto = dr.GetString(dr.GetOrdinal("vNombrePuesto"));
                            be.datFechaReg = dr.GetDateTime(dr.GetOrdinal("dFechaReg"));
                            //be.bEstadoCompletado = dr.GetBoolean(dr.GetOrdinal("bEstadoCompletado"));
                            //be.strEstadoCompletado = dr.GetString(dr.GetOrdinal("vEstadoCompletado"));
                            //be.strEstadoDerivado = dr.GetString(dr.GetOrdinal("cEstadoDerivado"));
                            //be.strEstadoMovimiento = dr.GetString(dr.GetOrdinal("vEstadoMovimiento"));
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public string ObtenerRutaBasesConvocatorias(string strParametro)
        {
            string ruta = string.Empty; 

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paObtenerParametro]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;
                if (strParametro != null && strParametro != string.Empty)
                {
                    cmd.Parameters.Add(new SqlParameter("@vParametro", strParametro));
                }
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        if (dr.Read())
                        {
                            ruta = dr.GetString(dr.GetOrdinal("vValor"));                            
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return ruta;
        }

        public bool LiberarBases(BasesPerfilPuestoObservacionRegistro peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaeBasesPerfilObservacion_Registrar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodBasePerfil", peticion.iCodBasePerfil));                
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

        public IEnumerable<BasesPerfilPuestoObservacionRequest> ListarBasesPerfilPuestoObservacion(string id)
        {
            List<BasesPerfilPuestoObservacionRequest> lista = new List<BasesPerfilPuestoObservacionRequest>();
            BasesPerfilPuestoObservacionRequest item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaeBasesPerfilObservacionHistorico]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodBasePerfil", Convert.ToInt32(id)));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new BasesPerfilPuestoObservacionRequest();

                            item.datFechaReg = dr.GetDateTime(dr.GetOrdinal("dtFechaReg"));
                            item.strFechaReg = Convert.ToDateTime(dr.GetDateTime(dr.GetOrdinal("dtFechaReg"))).ToString("dd/MM/yyyy");
                            item.datFechaAprobacionAnterior = dr.GetDateTime(dr.GetOrdinal("dtFechaAprobacionAnterior"));
                            item.strFechaAprobacionAnterior = Convert.ToDateTime(dr.GetDateTime(dr.GetOrdinal("dtFechaAprobacionAnterior"))).ToString("dd/MM/yyyy");                            
                            if (!Convert.IsDBNull(dr["vObservacion"]))
                            {
                                item.strObservacion = dr.GetString(dr.GetOrdinal("vObservacion"));
                            }
                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public Int32 PublicarBasesPerfilPuesto(BasesPerfilPuestoRegistro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paSISGESRRHH_Convocatoria_Registrar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;
                cmd.Parameters.Add(new SqlParameter("@iCodBasePerfil", registro.iCodBasePerfil));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabajadorReg", registro.iCodTrabajadorReg));
                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.iCodBasePerfil;
        }

        public Int32 RegistrarBasesArchivo(BasesPerfilPuestoRegistro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_Bases_file_ins]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_BASES", registro.iCodBasePerfil));
                if (registro.archivo != null) cmd.Parameters.Add(new SqlParameter("@ARCHIVO", registro.archivo));
                cmd.Parameters.Add(new SqlParameter("@USUARIO_MODIFICACION", registro.IdUsuarioModificacion));
                cmd.Parameters.Add(new SqlParameter("@FECHA_MODIFICACION", registro.FechaModificacion));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.iCodBasePerfil;
        }
    }
}
