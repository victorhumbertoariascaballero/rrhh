/*----------------------------------------------------------------------------------------
ARCHIVO CLASE   : T_genm_usuario_ODA

Objetivo: Clase referida a los métodos de Acceso a datos de la clase T_genm_usuario
Autor: Miguel Angel Salvador Paucar (MASP)
Fecha Creacion: 2015-09-03
Metodos: 
        Insertar_T_genm_usuario
        Actualizar_T_genm_usuario
        Listar_T_genm_usuario
        Anular_T_genm_usuario_PorCodigo
        Recuperar_T_genm_usuario_PorCodigo
        ListarPaginado_T_genm_usuario

----------------------------------------------------------------------------------------*/
#region Espacio de Nombres
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Configuration;
using System.Data;
using MIDIS.ORI.Entidades;
using MIDIS.ORI.Entidades.Core;
#endregion

namespace MIDIS.SEG.AccesoDatosSQL
{
    public partial class T_genm_convocatoria_ODA
    {
        BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWork = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnOracle"]].ConnectionString));
        BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWorkM = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnOracleM"]].ConnectionString));
        BaseOracleAdoUnitOfWork _iBasesOracleAdoUnitOfWork = new BaseOracleAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnOracle2"]].ConnectionString));

        BaseSqlOfWorkOld _iBaseSqlOfWorkOld = new BaseSqlOfWorkOld(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSQLRHant"]].ConnectionString));

        #region Metodos Generales

        string vSqlString = new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnOracleM"]].ConnectionString);

        public String Validar(BoletaCarga_Registro registro)
        {
            string strMensaje = String.Empty;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[Usp_Empleado_Boleta_validar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@NRO_DOCUMENTO", registro.NroDocumento));
                cmd.Parameters.Add(new SqlParameter("@ANIO", registro.Anio));
                cmd.Parameters.Add(new SqlParameter("@MES", registro.Mes));
                cmd.Parameters.Add(new SqlParameter("@GUID", "XXXXXXXX"));
                
                using (SqlDataReader dr = cmd.ExecuteReader()) {
                    if (dr.HasRows) {
                        while (dr.Read())
                            strMensaje = dr.GetString(dr.GetOrdinal("MENSAJE"));
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return strMensaje;
        }

        public IEnumerable<Convocatoria_Registro> ListarConvocatoria(Convocatoria_Request peticion)
        {
            List<Convocatoria_Registro> lista = new List<Convocatoria_Registro>();
            Convocatoria_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[usp_Convocatoria_listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@idConvocatoria", peticion.IdConvocatoria));
                cmd.Parameters.Add(new SqlParameter("@idDependencia", peticion.IdOrgano));
                cmd.Parameters.Add(new SqlParameter("@nombre", peticion.NroCAS));
                cmd.Parameters.Add(new SqlParameter("@cargo", peticion.NombreCargo));
                cmd.Parameters.Add(new SqlParameter("@estado", peticion.Estado));
                cmd.Parameters.Add(new SqlParameter("@tipo", peticion.IdTipo));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Convocatoria_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoria"));
                            item.IdBase = dr.GetInt32(dr.GetOrdinal("iCodBase"));
                            item.IdPerfil = dr.GetInt32(dr.GetOrdinal("iCodPerfil"));
                            item.IdDependencia = dr.GetInt32(dr.GetOrdinal("iCodDependencia"));
                            item.CantidadVacantes = dr.GetInt32(dr.GetOrdinal("iCantPersonalRequerido"));
                            item.NroConvocatoria = dr.GetString(dr.GetOrdinal("vNroCAS"));
                            item.Organo = dr.GetString(dr.GetOrdinal("vOrgano"));
                            item.Dependencia = dr.GetString(dr.GetOrdinal("vDependencia"));
                            item.NombreCargo = dr.GetString(dr.GetOrdinal("vNombrePuesto"));
                            item.NroAIRHSP = dr.GetString(dr.GetOrdinal("vAirhsp"));
                            item.Meta = dr.GetString(dr.GetOrdinal("vMeta"));
                            //item.IdResponsableCurricular = dr.GetInt32(dr.GetOrdinal("IdResponsableCurri"));
                            //item.ResponsableCurricular = dr.GetString(dr.GetOrdinal("ResponsableCurri"));
                            item.Estado = dr.GetInt32(dr.GetOrdinal("bEstado"));
                            item.EstadoNombre = dr.GetString(dr.GetOrdinal("vEstadoNombre"));
                            item.IdTieneRequerimiento = dr.GetInt32(dr.GetOrdinal("tieneRequerimiento"));
                            item.IdTieneCertificacion = dr.GetInt32(dr.GetOrdinal("tieneCertificacion"));
                            item.IdTieneComite = dr.GetInt32(dr.GetOrdinal("tieneComite"));
                            item.IdTieneExamenConoc = dr.GetInt32(dr.GetOrdinal("bTieneExamenConoc"));
                            item.IdTieneExamenPsico = dr.GetInt32(dr.GetOrdinal("bTieneExamenPsico"));
                            item.IdTipo = dr.GetInt32(dr.GetOrdinal("iTipoConvocatoria"));

                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaDesdePubMIDIS"))) item.FechaPublicacionDesde = dr.GetDateTime(dr.GetOrdinal("dFechaDesdePubMIDIS")).ToString("dd/MM/yyyy");
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaHastaPubMIDIS"))) item.FechaPublicacionHasta = dr.GetDateTime(dr.GetOrdinal("dFechaHastaPubMIDIS")).ToString("dd/MM/yyyy");
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaRegCVPostulante"))) item.FechaPostulacion = dr.GetDateTime(dr.GetOrdinal("dFechaRegCVPostulante")).ToString("dd/MM/yyyy");
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaDesdeEvaCV"))) item.FechaCurricularDesde = dr.GetDateTime(dr.GetOrdinal("dFechaDesdeEvaCV")).ToString("dd/MM/yyyy");
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaHastaEvaCV"))) item.FechaCurricularHasta = dr.GetDateTime(dr.GetOrdinal("dFechaHastaEvaCV")).ToString("dd/MM/yyyy");
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaExamenConoc"))) item.FechaConocimientos = dr.GetDateTime(dr.GetOrdinal("dFechaExamenConoc")).ToString("dd/MM/yyyy");
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaExamenPsico"))) item.FechaPsicologico = dr.GetDateTime(dr.GetOrdinal("dFechaExamenPsico")).ToString("dd/MM/yyyy");
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaPubResultadoConoc"))) item.FechaResultadosConoc = dr.GetDateTime(dr.GetOrdinal("dFechaPubResultadoConoc")).ToString("dd/MM/yyyy");
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaPubResultadoPsico"))) item.FechaResultadosPsico = dr.GetDateTime(dr.GetOrdinal("dFechaPubResultadoPsico")).ToString("dd/MM/yyyy");

                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaPubResultadoMIDIS"))) item.FechaResultadosCurri = dr.GetDateTime(dr.GetOrdinal("dFechaPubResultadoMIDIS")).ToString("dd/MM/yyyy");
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaDesdeEntrevista"))) item.FechaEntrevistaDesde = dr.GetDateTime(dr.GetOrdinal("dFechaDesdeEntrevista")).ToString("dd/MM/yyyy");
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaHastaEntrevista"))) item.FechaEntrevistaHasta = dr.GetDateTime(dr.GetOrdinal("dFechaHastaEntrevista")).ToString("dd/MM/yyyy");
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaPubResultadoFinalMIDIS"))) item.FechaResultadoFinal = dr.GetDateTime(dr.GetOrdinal("dFechaPubResultadoFinalMIDIS")).ToString("dd/MM/yyyy");
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaDesdeSuscripcionContrato"))) item.FechaContratoDesde = dr.GetDateTime(dr.GetOrdinal("dFechaDesdeSuscripcionContrato")).ToString("dd/MM/yyyy");
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaHastaSuscripcionContrato"))) item.FechaContratoHasta = dr.GetDateTime(dr.GetOrdinal("dFechaHastaSuscripcionContrato")).ToString("dd/MM/yyyy");

                            if (!dr.IsDBNull(dr.GetOrdinal("IdTipoApertura"))) item.IdTipoApertura = dr.GetInt32(dr.GetOrdinal("IdTipoApertura"));
                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PostulacionPostulante_Registro> ListarPostulantes(Convocatoria_Request peticion)
        {
            List<PostulacionPostulante_Registro> lista = new List<PostulacionPostulante_Registro>();
            PostulacionPostulante_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulacionPostulantes_listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", peticion.IdConvocatoria));
                
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulacionPostulante_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
                            item.IdPostulacion = dr.GetInt32(dr.GetOrdinal("iCodPostulacion"));
                            item.TipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            item.NroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            item.Paterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            item.Materno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            item.Foto = dr.GetString(dr.GetOrdinal("vFoto"));
                            item.Sexo = dr.GetString(dr.GetOrdinal("iCodigoGenero"));
                            item.Telefono = dr.GetString(dr.GetOrdinal("vTelefono"));
                            item.Celular = dr.GetString(dr.GetOrdinal("vCelular"));
                            item.CorreoElectronico = dr.GetString(dr.GetOrdinal("vCorreoElectronico"));
                            item.FFAA = dr.GetInt32(dr.GetOrdinal("iFFAA"));
                            item.Discapacidad = dr.GetInt32(dr.GetOrdinal("iDiscapacidad"));
                            //item.Nacionalidad = dr.GetString(dr.GetOrdinal("vNacionalidad"));
                            item.Estado = 1; // dr.GetInt32(dr.GetOrdinal("bEstado"));

                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaModificacion"))) item.FechaModificacion = dr.GetDateTime(dr.GetOrdinal("dFechaModificacion")).ToString("dd/MM/yyyy HH:mm");

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<PostulacionPostulante_Registro> ListarPostulantesServir(Convocatoria_Request peticion)
        {
            List<PostulacionPostulante_Registro> lista = new List<PostulacionPostulante_Registro>();
            PostulacionPostulante_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulacionPostulantesServir_listar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", peticion.IdConvocatoria));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulacionPostulante_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
                            item.IdPostulacion = dr.GetInt32(dr.GetOrdinal("iCodPostulacion"));
                            item.TipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            item.NroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            item.Paterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            item.Materno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            item.Foto = dr.GetString(dr.GetOrdinal("vFoto"));
                            item.Sexo = dr.GetString(dr.GetOrdinal("iCodigoGenero"));
                            item.Telefono = dr.GetString(dr.GetOrdinal("vTelefono"));
                            item.Celular = dr.GetString(dr.GetOrdinal("vCelular"));
                            item.CorreoElectronico = dr.GetString(dr.GetOrdinal("vCorreoElectronico"));
                            item.FFAA = dr.GetInt32(dr.GetOrdinal("iFFAA"));
                            item.Discapacidad = dr.GetInt32(dr.GetOrdinal("iDiscapacidad"));
                            //item.Nacionalidad = dr.GetString(dr.GetOrdinal("vNacionalidad"));
                            item.Estado = 1; // dr.GetInt32(dr.GetOrdinal("bEstado"));

                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaModificacion"))) item.FechaModificacion = dr.GetDateTime(dr.GetOrdinal("dFechaModificacion")).ToString("dd/MM/yyyy HH:mm");

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<PostulacionPostulante_Registro> ListarPostulantesPractica(Convocatoria_Request peticion)
        {
            List<PostulacionPostulante_Registro> lista = new List<PostulacionPostulante_Registro>();
            PostulacionPostulante_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulacionPostulantesPractica_listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", peticion.IdConvocatoria));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulacionPostulante_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
                            item.IdPostulacion = dr.GetInt32(dr.GetOrdinal("iCodPostulacionPrac"));
                            item.TipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            item.NroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            item.Paterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            item.Materno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            item.Foto = dr.GetString(dr.GetOrdinal("vFoto"));
                            item.Sexo = dr.GetString(dr.GetOrdinal("iCodigoGenero"));
                            item.Telefono = dr.GetString(dr.GetOrdinal("vTelefono"));
                            item.Celular = dr.GetString(dr.GetOrdinal("vCelular"));
                            item.CorreoElectronico = dr.GetString(dr.GetOrdinal("vCorreoElectronico"));
                            item.FFAA = dr.GetInt32(dr.GetOrdinal("iFFAA"));
                            item.Discapacidad = dr.GetInt32(dr.GetOrdinal("iDiscapacidad"));
                            //item.Nacionalidad = dr.GetString(dr.GetOrdinal("vNacionalidad"));
                            item.Estado = 1; // dr.GetInt32(dr.GetOrdinal("bEstado"));

                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaModificacion"))) item.FechaModificacion = dr.GetDateTime(dr.GetOrdinal("dFechaModificacion")).ToString("dd/MM/yyyy HH:mm");

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PostulacionEvaluacionCurricular_Registro> ListarPostulantesEvaluacionCurri(Convocatoria_Request peticion)
        {
            List<PostulacionEvaluacionCurricular_Registro> lista = new List<PostulacionEvaluacionCurricular_Registro>();
            PostulacionEvaluacionCurricular_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulantesEvaluacionCurri_listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", peticion.IdConvocatoria));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulacionEvaluacionCurricular_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdEvaluacion = dr.GetInt32(dr.GetOrdinal("iCodConvocatoriaEvalCurri"));
                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoria"));
                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
                            item.IdPostulacion = dr.GetInt32(dr.GetOrdinal("iCodPostulacion"));
                            item.TipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            item.NroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            item.Paterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            item.Materno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            item.Foto = dr.GetString(dr.GetOrdinal("vFoto"));
                            item.Sexo = dr.GetString(dr.GetOrdinal("iCodigoGenero"));
                            item.Telefono = dr.GetString(dr.GetOrdinal("vTelefono"));
                            item.Celular = dr.GetString(dr.GetOrdinal("vCelular"));
                            item.CorreoElectronico = dr.GetString(dr.GetOrdinal("vCorreoElectronico"));
                            item.FFAA = dr.GetInt32(dr.GetOrdinal("iFFAA"));
                            item.Discapacidad = dr.GetInt32(dr.GetOrdinal("iDiscapacidad"));
                            item.Estado = 1; // dr.GetInt32(dr.GetOrdinal("bEstado"));
                            item.Observacion = dr.GetString(dr.GetOrdinal("vObservacion"));

                            item.AptoTotal = dr.GetInt32(dr.GetOrdinal("iAptoTotal"));
                            item.PuntajeTotal = dr.GetDecimal(dr.GetOrdinal("iPuntajeTotal")); 
                            item.AptoDDJJ = dr.GetInt32(dr.GetOrdinal("iAptoDDJJ"));
                            item.AptoSanciones = dr.GetInt32(dr.GetOrdinal("iAptoSanciones"));
                            item.AptoFormacion = dr.GetInt32(dr.GetOrdinal("iAptoFormacion"));
                            item.AptoCapacitacion = dr.GetInt32(dr.GetOrdinal("iAptoCapacitacion"));
                            item.AptoExperienciaGen = dr.GetInt32(dr.GetOrdinal("iAptoExperienciaGen"));					        
                            item.AptoExperienciaEsp = dr.GetInt32(dr.GetOrdinal("iAptoExperienciaEsp"));
                            item.PuntajeFormacion = dr.GetInt32(dr.GetOrdinal("iPuntajeFormacion"));
                            item.PuntajeExperienciaGen = dr.GetInt32(dr.GetOrdinal("iPuntajeExperienciaGen"));
                            item.PuntajeExperienciaEsp = dr.GetInt32(dr.GetOrdinal("iPuntajeExperienciaEsp"));
                            item.PuntajeBonifFormacion = dr.GetInt32(dr.GetOrdinal("iPuntajeBonifFormacion"));
                            item.PuntajeBonifExperienciaGen = dr.GetInt32(dr.GetOrdinal("iPuntajeBonifExperienciaGen"));
					        item.PuntajeBonifExperienciaEsp = dr.GetInt32(dr.GetOrdinal("iPuntajeBonifExperienciaEsp"));
                            item.BonifDiscapacidad = dr.GetInt32(dr.GetOrdinal("iBonifDiscapacidad"));
                            item.BonifFFAA = dr.GetInt32(dr.GetOrdinal("iBonifFFAA"));
                            item.BonifDeporte = dr.GetInt32(dr.GetOrdinal("iBonifDeporte"));

                            item.CumpleFormacion = new Estado_Response()
                            {
                                Codigo = item.AptoFormacion.ToString(),
                                Nombre = (item.AptoFormacion == -1 ? "--" : (item.AptoFormacion == 1 ? "SI" : (item.AptoFormacion == 0 ? "NO" : "")))
                            };
                            item.CumpleCapacitacion = new Estado_Response()
                            {
                                Codigo = item.AptoCapacitacion.ToString(),
                                Nombre = (item.AptoCapacitacion == -1 ? "--" : (item.AptoCapacitacion == 1 ? "SI" : (item.AptoCapacitacion == 0 ? "NO" : "")))
                            };
                            item.CumpleExperienciaGen = new Estado_Response()
                            {
                                Codigo = item.AptoExperienciaGen.ToString(),
                                Nombre = (item.AptoExperienciaGen == -1 ? "--" : (item.AptoExperienciaGen == 1 ? "SI" : (item.AptoExperienciaGen == 0 ? "NO" : "")))
                            };
                            item.CumpleExperienciaEsp = new Estado_Response()
                            {
                                Codigo = item.AptoExperienciaEsp.ToString(),
                                Nombre = (item.AptoExperienciaEsp == -1 ? "--" : (item.AptoExperienciaEsp == 1 ? "SI" : (item.AptoExperienciaEsp == 0 ? "NO" : "")))
                            };
                            item.CumpleBonifica3 = new Estado_Response()
                            {
                                Codigo = item.PuntajeBonifFormacion.ToString(),
                                Nombre = item.PuntajeBonifFormacion.ToString()
                            };
                            item.CumpleBonifica2 = new Estado_Response()
                            {
                                Codigo = item.PuntajeBonifExperienciaEsp.ToString(),
                                Nombre = item.PuntajeBonifExperienciaEsp.ToString()
                            };
                            item.CumpleDDJJ = new Estado_Response()
                            {
                                Codigo = item.AptoDDJJ.ToString(),
                                Nombre = (item.AptoDDJJ == -1 ? "--" : (item.AptoDDJJ == 1 ? "SI" : (item.AptoDDJJ == 0 ? "NO" : "")))
                            };
                            item.CumpleHabilitacion = new Estado_Response()
                            {
                                Codigo = item.AptoSanciones.ToString(),
                                Nombre = (item.AptoSanciones == -1 ? "--" : (item.AptoSanciones == 1 ? "SI" : (item.AptoSanciones == 0 ? "NO" : "")))
                            };
                            item.CumpleFFAA = new Estado_Response()
                            {
                                Codigo = item.BonifFFAA.ToString(),
                                Nombre = (item.BonifFFAA == -1 ? "--" : (item.BonifFFAA == 1 ? "SI" : (item.BonifFFAA == 0 ? "NO" : "")))
                            };
                            item.CumpleDiscapacidad = new Estado_Response()
                            {
                                Codigo = item.BonifDiscapacidad.ToString(),
                                Nombre = (item.BonifDiscapacidad == -1 ? "--" : (item.BonifDiscapacidad == 1 ? "SI" : (item.BonifDiscapacidad == 0 ? "NO" : "")))
                            };
                            item.CumpleDeportista = new Estado_Response()
                            {
                                Codigo = item.BonifDeporte.ToString(),
                                Nombre = (item.BonifDeporte == 0 ? "--" : (item.BonifDeporte == 20 ? "BONIF 20%" : (item.BonifDeporte == 16 ? "BONIF 16%" : (item.BonifDeporte == 12 ? "BONIF 12%" : (item.BonifDeporte == 8 ? "BONIF 8%" : (item.BonifDeporte == 4 ? "BONIF 4%" : ""))))))
                            };
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaConocimiento"))) item.FechaConocimiento = dr.GetDateTime(dr.GetOrdinal("dFechaConocimiento")).ToString("dd/MM/yyyy");
                            if (!dr.IsDBNull(dr.GetOrdinal("dHoraConocimiento"))) item.HoraConocimiento = dr.GetTimeSpan(dr.GetOrdinal("dHoraConocimiento")).ToString(@"hh\:mm");
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaEntrevista"))) item.FechaEntrevista = dr.GetDateTime(dr.GetOrdinal("dFechaEntrevista")).ToString("dd/MM/yyyy");
                            if (!dr.IsDBNull(dr.GetOrdinal("dHoraEntrevista"))) item.HoraEntrevista = dr.GetTimeSpan(dr.GetOrdinal("dHoraEntrevista")).ToString(@"hh\:mm");
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaModificacion"))) item.FechaModificacion = dr.GetDateTime(dr.GetOrdinal("dFechaModificacion")).ToString("dd/MM/yyyy HH:mm");

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<PostulacionEvaluacionCurricular_Registro> ListarPostulantesServirEvaluacionCurri(Convocatoria_Request peticion)
        {
            List<PostulacionEvaluacionCurricular_Registro> lista = new List<PostulacionEvaluacionCurricular_Registro>();
            PostulacionEvaluacionCurricular_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulantesServirEvaluacionCurri_listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", peticion.IdConvocatoria));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulacionEvaluacionCurricular_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdEvaluacion = dr.GetInt32(dr.GetOrdinal("iCodConvocatoriaEvalCurri"));
                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoria"));
                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
                            item.IdPostulacion = dr.GetInt32(dr.GetOrdinal("iCodPostulacion"));
                            item.TipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            item.NroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            item.Paterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            item.Materno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            item.Foto = dr.GetString(dr.GetOrdinal("vFoto"));
                            item.Sexo = dr.GetString(dr.GetOrdinal("iCodigoGenero"));
                            item.Telefono = dr.GetString(dr.GetOrdinal("vTelefono"));
                            item.Celular = dr.GetString(dr.GetOrdinal("vCelular"));
                            item.CorreoElectronico = dr.GetString(dr.GetOrdinal("vCorreoElectronico"));
                            item.FFAA = dr.GetInt32(dr.GetOrdinal("iFFAA"));
                            item.Discapacidad = dr.GetInt32(dr.GetOrdinal("iDiscapacidad"));
                            item.Estado = 1; // dr.GetInt32(dr.GetOrdinal("bEstado"));
                            item.Observacion = dr.GetString(dr.GetOrdinal("vObservacion"));

                            item.AptoTotal = dr.GetInt32(dr.GetOrdinal("iAptoTotal"));
                            item.PuntajeTotal = dr.GetDecimal(dr.GetOrdinal("iPuntajeTotal"));
                            item.AptoDDJJ = dr.GetInt32(dr.GetOrdinal("iAptoDDJJ"));
                            item.AptoSanciones = dr.GetInt32(dr.GetOrdinal("iAptoSanciones"));
                            item.AptoFormacion = dr.GetInt32(dr.GetOrdinal("iAptoFormacion"));
                            item.AptoCapacitacion = dr.GetInt32(dr.GetOrdinal("iAptoCapacitacion"));
                            item.AptoExperienciaGen = dr.GetInt32(dr.GetOrdinal("iAptoExperienciaGen"));
                            item.AptoExperienciaEsp = dr.GetInt32(dr.GetOrdinal("iAptoExperienciaEsp"));
                            item.PuntajeFormacion = dr.GetInt32(dr.GetOrdinal("iPuntajeFormacion"));
                            item.PuntajeExperienciaGen = dr.GetInt32(dr.GetOrdinal("iPuntajeExperienciaGen"));
                            item.PuntajeExperienciaEsp = dr.GetInt32(dr.GetOrdinal("iPuntajeExperienciaEsp"));
                            item.PuntajeBonifFormacion = dr.GetInt32(dr.GetOrdinal("iPuntajeBonifFormacion"));
                            item.PuntajeBonifExperienciaGen = dr.GetInt32(dr.GetOrdinal("iPuntajeBonifExperienciaGen"));
                            item.PuntajeBonifExperienciaEsp = dr.GetInt32(dr.GetOrdinal("iPuntajeBonifExperienciaEsp"));
                            item.BonifDiscapacidad = dr.GetInt32(dr.GetOrdinal("iBonifDiscapacidad"));
                            item.BonifFFAA = dr.GetInt32(dr.GetOrdinal("iBonifFFAA"));
                            item.BonifDeporte = dr.GetInt32(dr.GetOrdinal("iBonifDeporte"));

                            item.CumpleFormacion = new Estado_Response()
                            {
                                Codigo = item.AptoFormacion.ToString(),
                                Nombre = (item.AptoFormacion == -1 ? "--" : (item.AptoFormacion == 1 ? "SI" : (item.AptoFormacion == 0 ? "NO" : "")))
                            };
                            item.CumpleCapacitacion = new Estado_Response()
                            {
                                Codigo = item.AptoCapacitacion.ToString(),
                                Nombre = (item.AptoCapacitacion == -1 ? "--" : (item.AptoCapacitacion == 1 ? "SI" : (item.AptoCapacitacion == 0 ? "NO" : "")))
                            };
                            item.CumpleExperienciaGen = new Estado_Response()
                            {
                                Codigo = item.AptoExperienciaGen.ToString(),
                                Nombre = (item.AptoExperienciaGen == -1 ? "--" : (item.AptoExperienciaGen == 1 ? "SI" : (item.AptoExperienciaGen == 0 ? "NO" : "")))
                            };
                            item.CumpleExperienciaEsp = new Estado_Response()
                            {
                                Codigo = item.AptoExperienciaEsp.ToString(),
                                Nombre = (item.AptoExperienciaEsp == -1 ? "--" : (item.AptoExperienciaEsp == 1 ? "SI" : (item.AptoExperienciaEsp == 0 ? "NO" : "")))
                            };
                            item.CumpleBonifica3 = new Estado_Response()
                            {
                                Codigo = item.PuntajeBonifFormacion.ToString(),
                                Nombre = item.PuntajeBonifFormacion.ToString()
                            };
                            item.CumpleBonifica2 = new Estado_Response()
                            {
                                Codigo = item.PuntajeBonifExperienciaEsp.ToString(),
                                Nombre = item.PuntajeBonifExperienciaEsp.ToString()
                            };
                            item.CumpleDDJJ = new Estado_Response()
                            {
                                Codigo = item.AptoDDJJ.ToString(),
                                Nombre = (item.AptoDDJJ == -1 ? "--" : (item.AptoDDJJ == 1 ? "SI" : (item.AptoDDJJ == 0 ? "NO" : "")))
                            };
                            item.CumpleHabilitacion = new Estado_Response()
                            {
                                Codigo = item.AptoSanciones.ToString(),
                                Nombre = (item.AptoSanciones == -1 ? "--" : (item.AptoSanciones == 1 ? "SI" : (item.AptoSanciones == 0 ? "NO" : "")))
                            };
                            item.CumpleFFAA = new Estado_Response()
                            {
                                Codigo = item.BonifFFAA.ToString(),
                                Nombre = (item.BonifFFAA == -1 ? "--" : (item.BonifFFAA == 1 ? "SI" : (item.BonifFFAA == 0 ? "NO" : "")))
                            };
                            item.CumpleDiscapacidad = new Estado_Response()
                            {
                                Codigo = item.BonifDiscapacidad.ToString(),
                                Nombre = (item.BonifDiscapacidad == -1 ? "--" : (item.BonifDiscapacidad == 1 ? "SI" : (item.BonifDiscapacidad == 0 ? "NO" : "")))
                            };
                            item.CumpleDeportista = new Estado_Response()
                            {
                                Codigo = item.BonifDeporte.ToString(),
                                Nombre = (item.BonifDeporte == 0 ? "--" : (item.BonifDeporte == 20 ? "BONIF 20%" : (item.BonifDeporte == 16 ? "BONIF 16%" : (item.BonifDeporte == 12 ? "BONIF 12%" : (item.BonifDeporte == 8 ? "BONIF 8%" : (item.BonifDeporte == 4 ? "BONIF 4%" : ""))))))
                            };
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaConocimiento"))) item.FechaConocimiento = dr.GetDateTime(dr.GetOrdinal("dFechaConocimiento")).ToString("dd/MM/yyyy");
                            if (!dr.IsDBNull(dr.GetOrdinal("dHoraConocimiento"))) item.HoraConocimiento = dr.GetTimeSpan(dr.GetOrdinal("dHoraConocimiento")).ToString(@"hh\:mm");
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaEntrevista"))) item.FechaEntrevista = dr.GetDateTime(dr.GetOrdinal("dFechaEntrevista")).ToString("dd/MM/yyyy");
                            if (!dr.IsDBNull(dr.GetOrdinal("dHoraEntrevista"))) item.HoraEntrevista = dr.GetTimeSpan(dr.GetOrdinal("dHoraEntrevista")).ToString(@"hh\:mm");
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaModificacion"))) item.FechaModificacion = dr.GetDateTime(dr.GetOrdinal("dFechaModificacion")).ToString("dd/MM/yyyy HH:mm");

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<PostulacionEvaluacionCurricular_Registro> ListarPostulantesPracticaEvaluacionCurri(Convocatoria_Request peticion)
        {
            List<PostulacionEvaluacionCurricular_Registro> lista = new List<PostulacionEvaluacionCurricular_Registro>();
            PostulacionEvaluacionCurricular_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulantesPracEvaluacionCurri_listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", peticion.IdConvocatoria));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulacionEvaluacionCurricular_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdEvaluacion = dr.GetInt32(dr.GetOrdinal("iCodConvocatoriaPracEvalCurri"));
                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoriaPrac"));
                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
                            item.IdPostulacion = dr.GetInt32(dr.GetOrdinal("iCodPostulacion"));
                            item.TipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            item.NroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            item.Paterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            item.Materno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            item.Foto = dr.GetString(dr.GetOrdinal("vFoto"));
                            item.Sexo = dr.GetString(dr.GetOrdinal("iCodigoGenero"));
                            item.Telefono = dr.GetString(dr.GetOrdinal("vTelefono"));
                            item.Celular = dr.GetString(dr.GetOrdinal("vCelular"));
                            item.CorreoElectronico = dr.GetString(dr.GetOrdinal("vCorreoElectronico"));
                            item.FFAA = dr.GetInt32(dr.GetOrdinal("iFFAA"));
                            item.Discapacidad = dr.GetInt32(dr.GetOrdinal("iDiscapacidad"));
                            item.Estado = 1; // dr.GetInt32(dr.GetOrdinal("bEstado"));
                            item.Observacion = dr.GetString(dr.GetOrdinal("vObservacion"));

                            item.AptoTotal = dr.GetInt32(dr.GetOrdinal("iAptoTotal"));
                            item.PuntajeTotal = dr.GetDecimal(dr.GetOrdinal("iPuntajeTotal"));
                            item.AptoDDJJ = dr.GetInt32(dr.GetOrdinal("iAptoDDJJ"));
                            item.AptoSanciones = dr.GetInt32(dr.GetOrdinal("iAptoSanciones"));
                            item.AptoFormacion = dr.GetInt32(dr.GetOrdinal("iAptoFormacion"));
                            item.AptoCapacitacion = dr.GetInt32(dr.GetOrdinal("iAptoCapacitacion"));
                            item.AptoExperienciaGen = dr.GetInt32(dr.GetOrdinal("iAptoExperienciaGen"));
                            item.AptoExperienciaEsp = dr.GetInt32(dr.GetOrdinal("iAptoExperienciaEsp"));
                            item.PuntajeFormacion = dr.GetInt32(dr.GetOrdinal("iPuntajeFormacion"));
                            item.PuntajeExperienciaGen = dr.GetInt32(dr.GetOrdinal("iPuntajeExperienciaGen"));
                            item.PuntajeExperienciaEsp = dr.GetInt32(dr.GetOrdinal("iPuntajeExperienciaEsp"));
                            item.PuntajeBonifFormacion = dr.GetInt32(dr.GetOrdinal("iPuntajeBonifFormacion"));
                            item.PuntajeBonifExperienciaGen = dr.GetInt32(dr.GetOrdinal("iPuntajeBonifExperienciaGen"));
                            item.PuntajeBonifExperienciaEsp = dr.GetInt32(dr.GetOrdinal("iPuntajeBonifExperienciaEsp"));
                            item.BonifDiscapacidad = dr.GetInt32(dr.GetOrdinal("iBonifDiscapacidad"));
                            item.BonifFFAA = dr.GetInt32(dr.GetOrdinal("iBonifFFAA"));
                            item.BonifDeporte = dr.GetInt32(dr.GetOrdinal("iBonifDeporte"));

                            item.CumpleFormacion = new Estado_Response()
                            {
                                Codigo = item.AptoFormacion.ToString(),
                                Nombre = (item.AptoFormacion == -1 ? "--" : (item.AptoFormacion == 1 ? "SI" : (item.AptoFormacion == 0 ? "NO" : "")))
                            };
                            item.CumpleCapacitacion = new Estado_Response()
                            {
                                Codigo = item.AptoCapacitacion.ToString(),
                                Nombre = (item.AptoCapacitacion == -1 ? "--" : (item.AptoCapacitacion == 1 ? "SI" : (item.AptoCapacitacion == 0 ? "NO" : "")))
                            };
                            item.CumpleExperienciaGen = new Estado_Response()
                            {
                                Codigo = item.AptoExperienciaGen.ToString(),
                                Nombre = (item.AptoExperienciaGen == -1 ? "--" : (item.AptoExperienciaGen == 1 ? "SI" : (item.AptoExperienciaGen == 0 ? "NO" : "")))
                            };
                            item.CumpleExperienciaEsp = new Estado_Response()
                            {
                                Codigo = item.AptoExperienciaEsp.ToString(),
                                Nombre = (item.AptoExperienciaEsp == -1 ? "--" : (item.AptoExperienciaEsp == 1 ? "SI" : (item.AptoExperienciaEsp == 0 ? "NO" : "")))
                            };
                            item.CumpleBonifica3 = new Estado_Response()
                            {
                                Codigo = item.PuntajeBonifFormacion.ToString(),
                                Nombre = item.PuntajeBonifFormacion.ToString()
                            };
                            item.CumpleBonifica2 = new Estado_Response()
                            {
                                Codigo = item.PuntajeBonifExperienciaEsp.ToString(),
                                Nombre = item.PuntajeBonifExperienciaEsp.ToString()
                            };
                            item.CumpleDDJJ = new Estado_Response()
                            {
                                Codigo = item.AptoDDJJ.ToString(),
                                Nombre = (item.AptoDDJJ == -1 ? "--" : (item.AptoDDJJ == 1 ? "SI" : (item.AptoDDJJ == 0 ? "NO" : "")))
                            };
                            item.CumpleHabilitacion = new Estado_Response()
                            {
                                Codigo = item.AptoSanciones.ToString(),
                                Nombre = (item.AptoSanciones == -1 ? "--" : (item.AptoSanciones == 1 ? "SI" : (item.AptoSanciones == 0 ? "NO" : "")))
                            };
                            item.CumpleFFAA = new Estado_Response()
                            {
                                Codigo = item.BonifFFAA.ToString(),
                                Nombre = (item.BonifFFAA == -1 ? "--" : (item.BonifFFAA == 1 ? "SI" : (item.BonifFFAA == 0 ? "NO" : "")))
                            };
                            item.CumpleDiscapacidad = new Estado_Response()
                            {
                                Codigo = item.BonifDiscapacidad.ToString(),
                                Nombre = (item.BonifDiscapacidad == -1 ? "--" : (item.BonifDiscapacidad == 1 ? "SI" : (item.BonifDiscapacidad == 0 ? "NO" : "")))
                            };
                            item.CumpleDeportista = new Estado_Response()
                            {
                                Codigo = item.BonifDeporte.ToString(),
                                Nombre = (item.BonifDeporte == 0 ? "--" : (item.BonifDeporte == 20 ? "BONIF 20%" : (item.BonifDeporte == 16 ? "BONIF 16%" : (item.BonifDeporte == 12 ? "BONIF 12%" : (item.BonifDeporte == 8 ? "BONIF 8%" : (item.BonifDeporte == 4 ? "BONIF 4%" : ""))))))
                            };
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaConocimiento"))) item.FechaConocimiento = dr.GetDateTime(dr.GetOrdinal("dFechaConocimiento")).ToString("dd/MM/yyyy");
                            if (!dr.IsDBNull(dr.GetOrdinal("dHoraConocimiento"))) item.HoraConocimiento = dr.GetTimeSpan(dr.GetOrdinal("dHoraConocimiento")).ToString(@"hh\:mm");
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaEntrevista"))) item.FechaEntrevista = dr.GetDateTime(dr.GetOrdinal("dFechaEntrevista")).ToString("dd/MM/yyyy");
                            if (!dr.IsDBNull(dr.GetOrdinal("dHoraEntrevista"))) item.HoraEntrevista = dr.GetTimeSpan(dr.GetOrdinal("dHoraEntrevista")).ToString(@"hh\:mm");
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaModificacion"))) item.FechaModificacion = dr.GetDateTime(dr.GetOrdinal("dFechaModificacion")).ToString("dd/MM/yyyy HH:mm");

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<PostulacionEvaluacionConocimiento_Registro> ListarPostulantesEvaluacionConocimiento(Convocatoria_Request peticion)
        {
            List<PostulacionEvaluacionConocimiento_Registro> lista = new List<PostulacionEvaluacionConocimiento_Registro>();
            PostulacionEvaluacionConocimiento_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulantesEvaluacionConoc_listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", peticion.IdConvocatoria));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulacionEvaluacionConocimiento_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdEvaluacion = dr.GetInt32(dr.GetOrdinal("iCodConvocatoriaEvalConoc"));
                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoria"));
                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
                            item.IdPostulacion = dr.GetInt32(dr.GetOrdinal("iCodPostulacion"));
                            item.TipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            item.NroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            item.Paterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            item.Materno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            item.Foto = dr.GetString(dr.GetOrdinal("vFoto"));
                            item.Sexo = dr.GetString(dr.GetOrdinal("iCodigoGenero"));
                            item.Telefono = dr.GetString(dr.GetOrdinal("vTelefono"));
                            item.Celular = dr.GetString(dr.GetOrdinal("vCelular"));
                            item.CorreoElectronico = dr.GetString(dr.GetOrdinal("vCorreoElectronico"));
                            //item.Nacionalidad = dr.GetString(dr.GetOrdinal("vNacionalidad"));
                            item.Estado = 1; // dr.GetInt32(dr.GetOrdinal("bEstado"));
                            item.Observacion = dr.GetString(dr.GetOrdinal("vObservacion"));

                            item.AptoTotal = dr.GetInt32(dr.GetOrdinal("iAptoTotal"));
                            item.PuntajeTotal = dr.GetDecimal(dr.GetOrdinal("iPuntajeTotal"));
                            
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaEntrevista"))) item.FechaEntrevista = dr.GetDateTime(dr.GetOrdinal("dFechaEntrevista")).ToString("dd/MM/yyyy");
                            if (!dr.IsDBNull(dr.GetOrdinal("dHoraEntrevista"))) item.HoraEntrevista = dr.GetTimeSpan(dr.GetOrdinal("dHoraEntrevista")).ToString(@"hh\:mm");
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaModificacion"))) item.FechaModificacion = dr.GetDateTime(dr.GetOrdinal("dFechaModificacion")).ToString("dd/MM/yyyy HH:mm");

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<PostulacionEvaluacionConocimiento_Registro> ListarPostulantesServirEvaluacionConocimiento(Convocatoria_Request peticion)
        {
            List<PostulacionEvaluacionConocimiento_Registro> lista = new List<PostulacionEvaluacionConocimiento_Registro>();
            PostulacionEvaluacionConocimiento_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulantesServirEvaluacionConoc_listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", peticion.IdConvocatoria));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulacionEvaluacionConocimiento_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdEvaluacion = dr.GetInt32(dr.GetOrdinal("iCodConvocatoriaEvalConoc"));
                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoria"));
                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
                            item.IdPostulacion = dr.GetInt32(dr.GetOrdinal("iCodPostulacion"));
                            item.TipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            item.NroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            item.Paterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            item.Materno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            item.Foto = dr.GetString(dr.GetOrdinal("vFoto"));
                            item.Sexo = dr.GetString(dr.GetOrdinal("iCodigoGenero"));
                            item.Telefono = dr.GetString(dr.GetOrdinal("vTelefono"));
                            item.Celular = dr.GetString(dr.GetOrdinal("vCelular"));
                            item.CorreoElectronico = dr.GetString(dr.GetOrdinal("vCorreoElectronico"));
                            //item.Nacionalidad = dr.GetString(dr.GetOrdinal("vNacionalidad"));
                            item.Estado = 1; // dr.GetInt32(dr.GetOrdinal("bEstado"));
                            item.Observacion = dr.GetString(dr.GetOrdinal("vObservacion"));

                            item.AptoTotal = dr.GetInt32(dr.GetOrdinal("iAptoTotal"));
                            item.PuntajeTotal = dr.GetDecimal(dr.GetOrdinal("iPuntajeTotal"));

                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaEntrevista"))) item.FechaEntrevista = dr.GetDateTime(dr.GetOrdinal("dFechaEntrevista")).ToString("dd/MM/yyyy");
                            if (!dr.IsDBNull(dr.GetOrdinal("dHoraEntrevista"))) item.HoraEntrevista = dr.GetTimeSpan(dr.GetOrdinal("dHoraEntrevista")).ToString(@"hh\:mm");
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaModificacion"))) item.FechaModificacion = dr.GetDateTime(dr.GetOrdinal("dFechaModificacion")).ToString("dd/MM/yyyy HH:mm");

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<PostulacionEntrevistaPersonal_Registro> ListarPostulantesEntrevistaPersonal(Convocatoria_Request peticion)
        {
            List<PostulacionEntrevistaPersonal_Registro> lista = new List<PostulacionEntrevistaPersonal_Registro>();
            PostulacionEntrevistaPersonal_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulantesEntrevistaPersonal_listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", peticion.IdConvocatoria));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulacionEntrevistaPersonal_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdEvaluacion = dr.GetInt32(dr.GetOrdinal("iCodConvocatoriaEntrevista"));
                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoria"));
                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
                            item.IdPostulacion = dr.GetInt32(dr.GetOrdinal("iCodPostulacion"));
                            item.IdDependencia = dr.GetInt32(dr.GetOrdinal("iCodDependencia"));
                            item.IdTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            item.TipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            item.NroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            item.Paterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            item.Materno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            item.Foto = dr.GetString(dr.GetOrdinal("vFoto"));
                            item.Sexo = dr.GetString(dr.GetOrdinal("iCodigoGenero"));
                            item.Telefono = dr.GetString(dr.GetOrdinal("vTelefono"));
                            item.Celular = dr.GetString(dr.GetOrdinal("vCelular"));
                            item.CorreoElectronico = dr.GetString(dr.GetOrdinal("vCorreoElectronico"));
                            //item.Nacionalidad = dr.GetString(dr.GetOrdinal("vNacionalidad"));
                            item.Estado = 1; // dr.GetInt32(dr.GetOrdinal("bEstado"));
                            item.Observacion = dr.GetString(dr.GetOrdinal("vObservacion"));

                            item.AptoTotal = dr.GetInt32(dr.GetOrdinal("iAptoTotal"));
                            item.PuntajeTotal = dr.GetDecimal(dr.GetOrdinal("iPuntajeTotal"));
                            item.IdPresento = dr.GetInt32(dr.GetOrdinal("iPresento"));
                            item.IdTieneActa = dr.GetInt32(dr.GetOrdinal("iTieneActa"));
                            item.IdTieneExamen = dr.GetInt32(dr.GetOrdinal("iTieneExamen"));
                            item.Observacion = dr.GetString(dr.GetOrdinal("vObservacion"));
                            item.NombreEvaluador = dr.GetString(dr.GetOrdinal("Evaluador"));

                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaEntrevista"))) item.FechaEntrevista = dr.GetDateTime(dr.GetOrdinal("dFechaEntrevista")).ToString("dd/MM/yyyy");
                            if (!dr.IsDBNull(dr.GetOrdinal("dHoraEntrevista"))) item.HoraEntrevista = dr.GetTimeSpan(dr.GetOrdinal("dHoraEntrevista")).ToString(@"hh\:mm");
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaModificacion"))) item.FechaModificacion = dr.GetDateTime(dr.GetOrdinal("dFechaModificacion")).ToString("dd/MM/yyyy HH:mm");

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<PostulacionEntrevistaPersonal_Registro> ListarPostulantesPracticaEntrevistaPersonal(Convocatoria_Request peticion)
        {
            List<PostulacionEntrevistaPersonal_Registro> lista = new List<PostulacionEntrevistaPersonal_Registro>();
            PostulacionEntrevistaPersonal_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulantesPracticaEntrevistaPersonal_listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", peticion.IdConvocatoria));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulacionEntrevistaPersonal_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdEvaluacion = dr.GetInt32(dr.GetOrdinal("iCodConvocatoriaPracEntrevista"));
                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoriaPrac"));
                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
                            item.IdPostulacion = dr.GetInt32(dr.GetOrdinal("iCodPostulacion"));
                            item.IdDependencia = dr.GetInt32(dr.GetOrdinal("iCodDependencia"));
                            item.IdTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            item.TipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            item.NroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            item.Paterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            item.Materno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            item.Foto = dr.GetString(dr.GetOrdinal("vFoto"));
                            item.Sexo = dr.GetString(dr.GetOrdinal("iCodigoGenero"));
                            item.Telefono = dr.GetString(dr.GetOrdinal("vTelefono"));
                            item.Celular = dr.GetString(dr.GetOrdinal("vCelular"));
                            item.CorreoElectronico = dr.GetString(dr.GetOrdinal("vCorreoElectronico"));
                            //item.Nacionalidad = dr.GetString(dr.GetOrdinal("vNacionalidad"));
                            item.Estado = 1; // dr.GetInt32(dr.GetOrdinal("bEstado"));
                            item.Observacion = dr.GetString(dr.GetOrdinal("vObservacion"));

                            item.AptoTotal = dr.GetInt32(dr.GetOrdinal("iAptoTotal"));
                            item.PuntajeTotal = dr.GetDecimal(dr.GetOrdinal("iPuntajeTotal"));
                            item.IdPresento = dr.GetInt32(dr.GetOrdinal("iPresento"));
                            item.IdTieneActa = dr.GetInt32(dr.GetOrdinal("iTieneActa"));
                            item.IdTieneExamen = dr.GetInt32(dr.GetOrdinal("iTieneExamen"));
                            item.Observacion = dr.GetString(dr.GetOrdinal("vObservacion"));
                            item.NombreEvaluador = dr.GetString(dr.GetOrdinal("Evaluador"));

                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaEntrevista"))) item.FechaEntrevista = dr.GetDateTime(dr.GetOrdinal("dFechaEntrevista")).ToString("dd/MM/yyyy");
                            if (!dr.IsDBNull(dr.GetOrdinal("dHoraEntrevista"))) item.HoraEntrevista = dr.GetTimeSpan(dr.GetOrdinal("dHoraEntrevista")).ToString(@"hh\:mm");
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaModificacion"))) item.FechaModificacion = dr.GetDateTime(dr.GetOrdinal("dFechaModificacion")).ToString("dd/MM/yyyy HH:mm");

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<PostulacionResultadoTotal_Registro> ListarPostulantesResultadosTotales(Convocatoria_Request peticion)
        {
            List<PostulacionResultadoTotal_Registro> lista = new List<PostulacionResultadoTotal_Registro>();
            PostulacionResultadoTotal_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulacionPostulantes_total]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", peticion.IdConvocatoria));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read()) 
                        {
                            item = new PostulacionResultadoTotal_Registro();

                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoria"));
                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
                            item.IdPostulacion = dr.GetInt32(dr.GetOrdinal("iCodPostulacion"));
                            item.TipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            item.NroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            item.Paterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            item.Materno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            item.Foto = dr.GetString(dr.GetOrdinal("vFoto"));
                            item.Sexo = dr.GetString(dr.GetOrdinal("iCodigoGenero"));
                            item.Telefono = dr.GetString(dr.GetOrdinal("vTelefono"));
                            item.Celular = dr.GetString(dr.GetOrdinal("vCelular"));
                            item.CorreoElectronico = dr.GetString(dr.GetOrdinal("vCorreoElectronico"));
                            //item.Nacionalidad = dr.GetString(dr.GetOrdinal("vNacionalidad"));
                            item.Estado = 1; // dr.GetInt32(dr.GetOrdinal("bEstado"));
                            item.Vacantes = dr.GetInt32(dr.GetOrdinal("vacantes"));
                            item.Posicion = Convert.ToInt32(dr.GetInt64(dr.GetOrdinal("posicion")));

                            item.AptoCurricular = dr.GetInt32(dr.GetOrdinal("AptoCurricular"));
                            item.AptoConocimiento = dr.GetInt32(dr.GetOrdinal("AptoConocimiento"));
                            item.AptoEntrevista = dr.GetInt32(dr.GetOrdinal("AptoEntrevista"));
                            item.PresentoEntrevista = dr.GetInt32(dr.GetOrdinal("PresentoEntrevista"));
                            item.BonifDiscapacidad = dr.GetInt32(dr.GetOrdinal("iBonifDiscapacidad"));
                            item.BonifFFAA = dr.GetInt32(dr.GetOrdinal("iBonifFFAA"));
                            //item.AptoTotal = dr.GetInt32(dr.GetOrdinal("iAptoTotal"));
                            item.PuntajeCurricular = dr.GetDecimal(dr.GetOrdinal("PuntajeCurricular"));
                            item.PuntajeConocimiento = dr.GetDecimal(dr.GetOrdinal("PuntajeConocimiento"));
                            item.PuntajeEntrevista = dr.GetDecimal(dr.GetOrdinal("PuntajeEntrevista"));
                            item.PuntajeTotal = dr.GetDecimal(dr.GetOrdinal("PuntajeTotal"));
                            
                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<PostulacionResultadoTotal_Registro> ListarPostulantesPracticaResultadosTotales(Convocatoria_Request peticion)
        {
            List<PostulacionResultadoTotal_Registro> lista = new List<PostulacionResultadoTotal_Registro>();
            PostulacionResultadoTotal_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulacionPostulantesPractica_total]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", peticion.IdConvocatoria));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulacionResultadoTotal_Registro();

                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoriaPrac"));
                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
                            item.IdPostulacion = dr.GetInt32(dr.GetOrdinal("iCodPostulacionPrac"));
                            item.TipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            item.NroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            item.Paterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            item.Materno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            item.Foto = dr.GetString(dr.GetOrdinal("vFoto"));
                            item.Sexo = dr.GetString(dr.GetOrdinal("iCodigoGenero"));
                            item.Telefono = dr.GetString(dr.GetOrdinal("vTelefono"));
                            item.Celular = dr.GetString(dr.GetOrdinal("vCelular"));
                            item.CorreoElectronico = dr.GetString(dr.GetOrdinal("vCorreoElectronico"));
                            //item.Nacionalidad = dr.GetString(dr.GetOrdinal("vNacionalidad"));
                            item.Estado = 1; // dr.GetInt32(dr.GetOrdinal("bEstado"));
                            item.Vacantes = dr.GetInt32(dr.GetOrdinal("vacantes"));
                            item.Posicion = Convert.ToInt32(dr.GetInt64(dr.GetOrdinal("posicion")));

                            item.AptoCurricular = dr.GetInt32(dr.GetOrdinal("AptoCurricular"));
                            item.AptoConocimiento = dr.GetInt32(dr.GetOrdinal("AptoConocimiento"));
                            item.AptoEntrevista = dr.GetInt32(dr.GetOrdinal("AptoEntrevista"));
                            item.PresentoEntrevista = dr.GetInt32(dr.GetOrdinal("PresentoEntrevista"));
                            item.BonifDiscapacidad = dr.GetInt32(dr.GetOrdinal("iBonifDiscapacidad"));
                            item.BonifFFAA = dr.GetInt32(dr.GetOrdinal("iBonifFFAA"));
                            //item.AptoTotal = dr.GetInt32(dr.GetOrdinal("iAptoTotal"));
                            item.PuntajeCurricular = dr.GetDecimal(dr.GetOrdinal("PuntajeCurricular"));
                            item.PuntajeConocimiento = 0; // dr.GetDecimal(dr.GetOrdinal("PuntajeConocimiento"));
                            item.PuntajeEntrevista = dr.GetDecimal(dr.GetOrdinal("PuntajeEntrevista"));
                            item.PuntajeTotal = dr.GetDecimal(dr.GetOrdinal("PuntajeTotal"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<PostulacionEntrevistaPersonalPregunta_Registro> ListarEntrevistaPersonalPreguntas(PostulacionEvaluacionEntrevista_Registro peticion)
        {
            List<PostulacionEntrevistaPersonalPregunta_Registro> lista = new List<PostulacionEntrevistaPersonalPregunta_Registro>();
            PostulacionEntrevistaPersonalPregunta_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_ConvocatoriaEntrevistaPreg_listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaEntrevista", peticion.IdEvaluacion ));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulacionEntrevistaPersonalPregunta_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdEvaluacion = dr.GetInt32(dr.GetOrdinal("iCodConvocatoriaEntrevista"));
                            item.IdPregunta = dr.GetInt32(dr.GetOrdinal("iCodConvocatoriaEntrevistaPreg"));
                            item.Descripcion = dr.GetString(dr.GetOrdinal("vPregunta"));
                            
                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<PostulacionEntrevistaPersonalPregunta_Registro> ListarEntrevistaPersonalPreguntasPractica(PostulacionEvaluacionEntrevista_Registro peticion)
        {
            List<PostulacionEntrevistaPersonalPregunta_Registro> lista = new List<PostulacionEntrevistaPersonalPregunta_Registro>();
            PostulacionEntrevistaPersonalPregunta_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_ConvocatoriaPracEntrevistaPreg_listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaEntrevista", peticion.IdEvaluacion));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulacionEntrevistaPersonalPregunta_Registro();

                            item.Grilla = new Grilla_Response();
                            item.Posicion = lista.Count + 1;
                            item.IdEvaluacion = dr.GetInt32(dr.GetOrdinal("iCodConvocatoriaEntrevista"));
                            item.IdPregunta = dr.GetInt32(dr.GetOrdinal("iCodConvocatoriaEntrevistaPreg"));
                            item.Descripcion = dr.GetString(dr.GetOrdinal("vPregunta"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<PostulacionEntrevistaPersonalPregunta_Registro> ListarEntrevistaPracticaPreguntasMaestras(PostulacionEvaluacionEntrevista_Registro peticion)
        {
            List<PostulacionEntrevistaPersonalPregunta_Registro> lista = new List<PostulacionEntrevistaPersonalPregunta_Registro>();
            PostulacionEntrevistaPersonalPregunta_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_ConvocatoriaPracPreguntaMaestra_listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaPrac", peticion.IdConvocatoria));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", peticion.IdTrabajador));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulacionEntrevistaPersonalPregunta_Registro();

                            item.Grilla = new Grilla_Response();
                            item.Posicion = lista.Count + 1;
                            item.IdPreguntaMaestra = dr.GetInt32(dr.GetOrdinal("iCodConvocatoriaPreguntaMaestra"));
                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoria"));
                            item.Descripcion = dr.GetString(dr.GetOrdinal("vPregunta"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<PostulacionEntrevistaPersonalPregunta_Registro> ListarEntrevistaPreguntasMaestras(PostulacionEvaluacionEntrevista_Registro peticion)
        {
            List<PostulacionEntrevistaPersonalPregunta_Registro> lista = new List<PostulacionEntrevistaPersonalPregunta_Registro>();
            PostulacionEntrevistaPersonalPregunta_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_ConvocatoriaPreguntaMaestra_listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", peticion.IdConvocatoria));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", peticion.IdTrabajador));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulacionEntrevistaPersonalPregunta_Registro();

                            item.Grilla = new Grilla_Response();
                            item.Posicion = lista.Count + 1;
                            item.IdPreguntaMaestra = dr.GetInt32(dr.GetOrdinal("iCodConvocatoriaPreguntaMaestra"));
                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoria"));
                            item.Descripcion = dr.GetString(dr.GetOrdinal("vPregunta"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public Int32 RegistrarConvocatoriaDocumentoArchivo(ConvocatoriaDocumento_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_ConvocatoriaDocumento_ins]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_CONVOCATORIA", registro.IdConvocatoria));
                cmd.Parameters.Add(new SqlParameter("@ID_TIPODOCUMENTO", registro.IdTipoDocumento));
                cmd.Parameters.Add(new SqlParameter("@ESTADO", 1));
                if (registro.archivo != null) cmd.Parameters.Add(new SqlParameter("@ARCHIVO", registro.archivo));
                cmd.Parameters.Add(new SqlParameter("@USUARIO_REGISTRO", registro.IdUsuarioRegistro));
                cmd.Parameters.Add(new SqlParameter("@FECHA_REGISTRO", registro.FechaRegistro));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdConvocatoria;
        }
        public Int32 RegistrarConvocatoriaPracticaDocumentoArchivo(ConvocatoriaDocumento_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_ConvocatoriaPracDocumento_ins]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_CONVOCATORIA", registro.IdConvocatoria));
                cmd.Parameters.Add(new SqlParameter("@ID_TIPODOCUMENTO", registro.IdTipoDocumento));
                cmd.Parameters.Add(new SqlParameter("@ESTADO", 1));
                if (registro.archivo != null) cmd.Parameters.Add(new SqlParameter("@ARCHIVO", registro.archivo));
                cmd.Parameters.Add(new SqlParameter("@USUARIO_REGISTRO", registro.IdUsuarioRegistro));
                cmd.Parameters.Add(new SqlParameter("@FECHA_REGISTRO", registro.FechaRegistro));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdConvocatoria;
        }
        public Int32 RegistrarEntrevistaPersonalPregunta(PostulacionEntrevistaPersonalPregunta_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_ConvocatoriaEntrevistaPreg_ins]";
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaEntrevista", registro.IdEvaluacion));
                cmd.Parameters.Add(new SqlParameter("@vPregunta", registro.Descripcion));
                cmd.Parameters.Add(new SqlParameter("@vUsuarioRegistro", registro.IdUsuarioRegistro));
                
                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdConvocatoria;
        }
        public Int32 RegistrarEntrevistaPersonalPreguntaPractica(PostulacionEntrevistaPersonalPregunta_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_ConvocatoriaPracEntrevistaPreg_ins]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaEntrevista", registro.IdEvaluacion));
                cmd.Parameters.Add(new SqlParameter("@vPregunta", registro.Descripcion));
                cmd.Parameters.Add(new SqlParameter("@vUsuarioRegistro", registro.IdUsuarioRegistro));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdConvocatoria;
        }
        public Int32 RegistrarPreguntaMaestraPractica(PostulacionEntrevistaPersonalPregunta_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_ConvocatoriaPracPreguntaMaestra_ins]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaPrac", registro.IdConvocatoria));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", registro.IdTrabajador ));
                cmd.Parameters.Add(new SqlParameter("@vPregunta", registro.Descripcion));
                cmd.Parameters.Add(new SqlParameter("@vUsuarioRegistro", registro.IdUsuarioRegistro));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdConvocatoria;
        }
        public Int32 RegistrarPreguntaMaestra(PostulacionEntrevistaPersonalPregunta_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_ConvocatoriaPreguntaMaestra_ins]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", registro.IdTrabajador));
                cmd.Parameters.Add(new SqlParameter("@vPregunta", registro.Descripcion));
                cmd.Parameters.Add(new SqlParameter("@vUsuarioRegistro", registro.IdUsuarioRegistro));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdConvocatoria;
        }
        public Int32 ActualizarEntrevistaPersonalPregunta(PostulacionEntrevistaPersonalPregunta_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_ConvocatoriaEntrevistaPreg_upd]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaEntrevistaPreg", registro.IdPregunta ));
                cmd.Parameters.Add(new SqlParameter("@vPregunta", registro.Descripcion));
                cmd.Parameters.Add(new SqlParameter("@bEstado", registro.Estado));
                cmd.Parameters.Add(new SqlParameter("@vUsuarioModificacion", registro.IdUsuarioModificacion));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdConvocatoria;
        }
        public Int32 ActualizarEntrevistaPersonalPreguntaPractica(PostulacionEntrevistaPersonalPregunta_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_ConvocatoriaPracEntrevistaPreg_upd]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaEntrevistaPreg", registro.IdPregunta));
                cmd.Parameters.Add(new SqlParameter("@vPregunta", registro.Descripcion));
                cmd.Parameters.Add(new SqlParameter("@bEstado", registro.Estado));
                cmd.Parameters.Add(new SqlParameter("@vUsuarioModificacion", registro.IdUsuarioModificacion));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdConvocatoria;
        }
        public Int32 ActualizarPreguntaMaestraPractica(PostulacionEntrevistaPersonalPregunta_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_ConvocatoriaPracPreguntaMaestra_upd]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaPreguntaMaestra", registro.IdPreguntaMaestra));
                cmd.Parameters.Add(new SqlParameter("@vPregunta", registro.Descripcion));
                cmd.Parameters.Add(new SqlParameter("@bEstado", registro.Estado));
                cmd.Parameters.Add(new SqlParameter("@vUsuarioModificacion", registro.IdUsuarioModificacion));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdConvocatoria;
        }
        public Int32 ActualizarPreguntaMaestra(PostulacionEntrevistaPersonalPregunta_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_ConvocatoriaPreguntaMaestra_upd]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaPreguntaMaestra", registro.IdPreguntaMaestra));
                cmd.Parameters.Add(new SqlParameter("@vPregunta", registro.Descripcion));
                cmd.Parameters.Add(new SqlParameter("@bEstado", registro.Estado));
                cmd.Parameters.Add(new SqlParameter("@vUsuarioModificacion", registro.IdUsuarioModificacion));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdConvocatoria;
        }
        #endregion

        public Int32 RegistrarConvocatoria(Convocatoria_Registro registro)
        {
            try
            {
                _iBasesSqlAdoUnitOfWork.IniciarTransaccion(IsolationLevel.ReadCommitted);

                using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
                {
                    if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                    cmd.CommandText = "[dbo].[Usp_Convocatoria_ins]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.Add(new SqlParameter("@iCodBase", registro.IdBase));
                    cmd.Parameters.Add(new SqlParameter("@iCodPerfil", registro.IdPerfil));
                    cmd.Parameters.Add(new SqlParameter("@iCodDependencia", registro.IdDependencia));
                    cmd.Parameters.Add(new SqlParameter("@vAirhsp", registro.NroAIRHSP));
                    cmd.Parameters.Add(new SqlParameter("@vMeta", registro.Meta));
                    cmd.Parameters.Add(new SqlParameter("@iRespCurricular", 0));
                    cmd.Parameters.Add(new SqlParameter("@bEstado", registro.Estado));
                    cmd.Parameters.Add(new SqlParameter("@fRequerimiento", registro.fileRequerimiento));
                    cmd.Parameters.Add(new SqlParameter("@fCertificacion", registro.fileCertificacion));
                    cmd.Parameters.Add(new SqlParameter("@fComite", registro.fileComite));
                    cmd.Parameters.Add(new SqlParameter("@dfecharegistro", registro.FechaRegistro));
                    cmd.Parameters.Add(new SqlParameter("@vUsuarioRegistro", registro.IdUsuarioRegistro)); 

                    SqlParameter IdPropuestaParameter = new SqlParameter("@iCodConvocatoria", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(IdPropuestaParameter);

                    cmd.ExecuteNonQuery();
                    registro.IdConvocatoria = Int32.Parse(IdPropuestaParameter.Value.ToString());

                    foreach (ConvocatoriaComite_Registro objComite in registro.comite) {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "[dbo].[Usp_ConvocatoriaComite_ins]";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));
                        cmd.Parameters.Add(new SqlParameter("@iCodMiembro", objComite.IdMiembro));
                        cmd.Parameters.Add(new SqlParameter("@iCodDependencia", objComite.IdDependencia));
                        cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", objComite.IdTrabajador));
                        cmd.Parameters.Add(new SqlParameter("@iCodTitular", objComite.IdTitular));
                        cmd.Parameters.Add(new SqlParameter("@bEstado", registro.Estado));
                        cmd.Parameters.Add(new SqlParameter("@dfecharegistro", registro.FechaRegistro));
                        cmd.Parameters.Add(new SqlParameter("@vUsuarioRegistro", registro.IdUsuarioRegistro));

                        cmd.ExecuteNonQuery();
                    }

                    if (_iBasesSqlAdoUnitOfWork.TieneTransaccion()) _iBasesSqlAdoUnitOfWork.ConfirmarTransaccion();
                }

            }
            catch (Exception)
            {
                if (_iBasesSqlAdoUnitOfWork.TieneTransaccion()) _iBasesSqlAdoUnitOfWork.RetrocederTransaccion();
                throw;
            }
            
            return registro.IdConvocatoria;
        }
        public Int32 RegistrarConvocatoriaPractica(Convocatoria_Registro registro)
        {
            try
            {
                _iBasesSqlAdoUnitOfWork.IniciarTransaccion(IsolationLevel.ReadCommitted);

                using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
                {
                    if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                    cmd.CommandText = "[dbo].[Usp_ConvocatoriaPrac_ins]";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@iCodBase", registro.IdBase));
                    cmd.Parameters.Add(new SqlParameter("@iCodPerfil", registro.IdPerfil));
                    cmd.Parameters.Add(new SqlParameter("@iCodOrgano", registro.IdOrgano));
                    cmd.Parameters.Add(new SqlParameter("@iCodDependencia", registro.IdDependencia));
                    cmd.Parameters.Add(new SqlParameter("@vAirhsp", registro.NroAIRHSP));
                    cmd.Parameters.Add(new SqlParameter("@vNroProceso", String.Empty)); // registro.NroConvocatoria));
                    cmd.Parameters.Add(new SqlParameter("@vNombrePuesto", registro.NombreCargo.ToUpper()));
                    cmd.Parameters.Add(new SqlParameter("@iVacantes", registro.CantidadVacantes));
                    cmd.Parameters.Add(new SqlParameter("@dRemuneracion", registro.Remuneracion));
                    cmd.Parameters.Add(new SqlParameter("@vMeta", registro.Meta));
                    cmd.Parameters.Add(new SqlParameter("@iRespCurricular", 0));
                    cmd.Parameters.Add(new SqlParameter("@bEstado", registro.Estado));
                    cmd.Parameters.Add(new SqlParameter("@fRequerimiento", registro.fileRequerimiento));
                    cmd.Parameters.Add(new SqlParameter("@fCertificacion", registro.fileCertificacion));
                    cmd.Parameters.Add(new SqlParameter("@fComite", registro.fileComite));
                    cmd.Parameters.Add(new SqlParameter("@dfecharegistro", registro.FechaRegistro));
                    cmd.Parameters.Add(new SqlParameter("@vUsuarioRegistro", registro.IdUsuarioRegistro));

                    cmd.Parameters.Add(new SqlParameter("@dFechaRegCVPostulante", registro.dFechaRegCVPostulante));
                    cmd.Parameters.Add(new SqlParameter("@dFechaDesdeEvaCV", registro.dFechaDesdeEvaCV));
                    cmd.Parameters.Add(new SqlParameter("@dFechaHastaEvaCV", registro.dFechaHastaEvaCV));
                    cmd.Parameters.Add(new SqlParameter("@dFechaPubResultadoMIDIS", registro.dFechaPubResultadoMIDIS));
                    cmd.Parameters.Add(new SqlParameter("@dFechaDesdeEntrevista", registro.dFechaDesdeEntrevista));
                    cmd.Parameters.Add(new SqlParameter("@dFechaHastaEntrevista", registro.dFechaHastaEntrevista));
                    cmd.Parameters.Add(new SqlParameter("@dFechaPubResultadoFinalMIDIS", registro.dFechaPubResultadoFinalMIDIS));
                    cmd.Parameters.Add(new SqlParameter("@dFechaDesdeSuscripcionContrato", registro.dFechaDesdeSuscripcionContrato));
                    cmd.Parameters.Add(new SqlParameter("@dFechaHastaSuscripcionContrato", registro.dFechaHastaSuscripcionContrato));

                    SqlParameter IdPropuestaParameter = new SqlParameter("@iCodConvocatoria", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(IdPropuestaParameter);

                    cmd.ExecuteNonQuery();
                    registro.IdConvocatoria = Int32.Parse(IdPropuestaParameter.Value.ToString());

                    foreach (ConvocatoriaComite_Registro objComite in registro.comite)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "[dbo].[Usp_ConvocatoriaPracComite_ins]";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));
                        cmd.Parameters.Add(new SqlParameter("@iCodMiembro", objComite.IdMiembro));
                        cmd.Parameters.Add(new SqlParameter("@iCodDependencia", objComite.IdDependencia));
                        cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", objComite.IdTrabajador));
                        cmd.Parameters.Add(new SqlParameter("@iCodTitular", objComite.IdTitular));
                        cmd.Parameters.Add(new SqlParameter("@bEstado", registro.Estado));
                        cmd.Parameters.Add(new SqlParameter("@dfecharegistro", registro.FechaRegistro));
                        cmd.Parameters.Add(new SqlParameter("@vUsuarioRegistro", registro.IdUsuarioRegistro));

                        cmd.ExecuteNonQuery();
                    }

                    if (_iBasesSqlAdoUnitOfWork.TieneTransaccion()) _iBasesSqlAdoUnitOfWork.ConfirmarTransaccion();
                }

            }
            catch (Exception)
            {
                if (_iBasesSqlAdoUnitOfWork.TieneTransaccion()) _iBasesSqlAdoUnitOfWork.RetrocederTransaccion();
                throw;
            }

            return registro.IdConvocatoria;
        }
        public Int32 RegistrarConvocatoriaServir(Convocatoria_Registro registro)
        {
            try
            {
                _iBasesSqlAdoUnitOfWork.IniciarTransaccion(IsolationLevel.ReadCommitted);

                using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
                {
                    if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                    cmd.CommandText = "[dbo].[Usp_ConvocatoriaServir_ins]";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@iCodBase", registro.IdBase));
                    cmd.Parameters.Add(new SqlParameter("@iCodPerfil", registro.IdPerfil));
                    cmd.Parameters.Add(new SqlParameter("@iCodDependencia", registro.IdDependencia));
                    cmd.Parameters.Add(new SqlParameter("@vAirhsp", registro.NroAIRHSP));
                    cmd.Parameters.Add(new SqlParameter("@vMeta", registro.Meta));
                    cmd.Parameters.Add(new SqlParameter("@iRespCurricular", 0));
                    cmd.Parameters.Add(new SqlParameter("@iTipoApertura", registro.IdTipoApertura));
                    cmd.Parameters.Add(new SqlParameter("@bEstado", registro.Estado));
                    cmd.Parameters.Add(new SqlParameter("@fRequerimiento", registro.fileRequerimiento));
                    cmd.Parameters.Add(new SqlParameter("@fCertificacion", registro.fileCertificacion));
                    cmd.Parameters.Add(new SqlParameter("@fComite", registro.fileComite));
                    cmd.Parameters.Add(new SqlParameter("@dfecharegistro", registro.FechaRegistro));
                    cmd.Parameters.Add(new SqlParameter("@vUsuarioRegistro", registro.IdUsuarioRegistro));

                    SqlParameter IdPropuestaParameter = new SqlParameter("@iCodConvocatoria", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(IdPropuestaParameter);

                    cmd.ExecuteNonQuery();
                    registro.IdConvocatoria = Int32.Parse(IdPropuestaParameter.Value.ToString());

                    foreach (ConvocatoriaComite_Registro objComite in registro.comite)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "[dbo].[Usp_ConvocatoriaServirComite_ins]";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));
                        cmd.Parameters.Add(new SqlParameter("@iCodMiembro", objComite.IdMiembro));
                        cmd.Parameters.Add(new SqlParameter("@iCodDependencia", objComite.IdDependencia));
                        cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", objComite.IdTrabajador));
                        cmd.Parameters.Add(new SqlParameter("@iCodTitular", objComite.IdTitular));
                        cmd.Parameters.Add(new SqlParameter("@bEstado", registro.Estado));
                        cmd.Parameters.Add(new SqlParameter("@dfecharegistro", registro.FechaRegistro));
                        cmd.Parameters.Add(new SqlParameter("@vUsuarioRegistro", registro.IdUsuarioRegistro));

                        cmd.ExecuteNonQuery();
                    }

                    if (_iBasesSqlAdoUnitOfWork.TieneTransaccion()) _iBasesSqlAdoUnitOfWork.ConfirmarTransaccion();
                }

            }
            catch (Exception)
            {
                if (_iBasesSqlAdoUnitOfWork.TieneTransaccion()) _iBasesSqlAdoUnitOfWork.RetrocederTransaccion();
                throw;
            }

            return registro.IdConvocatoria;
        }
        public Int32 ActualizarConvocatoria(Convocatoria_Registro registro)
        {
            try
            {
                _iBasesSqlAdoUnitOfWork.IniciarTransaccion(IsolationLevel.ReadCommitted);

                using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
                {
                    if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                    cmd.CommandText = "[dbo].[Usp_Convocatoria_upd]";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));
                    cmd.Parameters.Add(new SqlParameter("@vAirhsp", registro.NroAIRHSP));
                    cmd.Parameters.Add(new SqlParameter("@vMeta", registro.Meta));
                    cmd.Parameters.Add(new SqlParameter("@iRespCurricular", 0));
                    cmd.Parameters.Add(new SqlParameter("@bEstado", registro.Estado));
                    cmd.Parameters.Add(new SqlParameter("@fRequerimiento", registro.fileRequerimiento));
                    cmd.Parameters.Add(new SqlParameter("@fCertificacion", registro.fileCertificacion));
                    cmd.Parameters.Add(new SqlParameter("@fComite", registro.fileComite));
                    cmd.Parameters.Add(new SqlParameter("@dfechamodificacion", registro.FechaModificacion));
                    cmd.Parameters.Add(new SqlParameter("@vUsuarioModificacion", registro.IdUsuarioModificacion));

                    cmd.ExecuteNonQuery();


                    cmd.Parameters.Clear();
                    cmd.CommandText = "[dbo].[Usp_ConvocatoriaComite_eliminar]";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));
                    cmd.Parameters.Add(new SqlParameter("@dfechamodificacion", registro.FechaModificacion));
                    cmd.Parameters.Add(new SqlParameter("@vUsuarioModificacion", registro.IdUsuarioModificacion));

                    cmd.ExecuteNonQuery();

                    foreach (ConvocatoriaComite_Registro objComite in registro.comite)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "[dbo].[Usp_ConvocatoriaComite_ins]";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));
                        cmd.Parameters.Add(new SqlParameter("@iCodMiembro", objComite.IdMiembro));
                        cmd.Parameters.Add(new SqlParameter("@iCodDependencia", objComite.IdDependencia));
                        cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", objComite.IdTrabajador));
                        cmd.Parameters.Add(new SqlParameter("@iCodTitular", objComite.IdTitular));
                        cmd.Parameters.Add(new SqlParameter("@bEstado", registro.Estado));
                        cmd.Parameters.Add(new SqlParameter("@dfecharegistro", registro.FechaModificacion));
                        cmd.Parameters.Add(new SqlParameter("@vUsuarioRegistro", registro.IdUsuarioModificacion));

                        cmd.ExecuteNonQuery();
                    }

                    if (_iBasesSqlAdoUnitOfWork.TieneTransaccion()) _iBasesSqlAdoUnitOfWork.ConfirmarTransaccion();
                }

            }
            catch (Exception)
            {
                if (_iBasesSqlAdoUnitOfWork.TieneTransaccion()) _iBasesSqlAdoUnitOfWork.RetrocederTransaccion();
                throw;
            }

            return registro.IdConvocatoria;
        }
        public Int32 ActualizarConvocatoriaPractica(Convocatoria_Registro registro)
        {
            try
            {
                _iBasesSqlAdoUnitOfWork.IniciarTransaccion(IsolationLevel.ReadCommitted);

                using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
                {
                    if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                    cmd.CommandText = "[dbo].[Usp_ConvocatoriaPrac_upd]";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));
                    cmd.Parameters.Add(new SqlParameter("@vAirhsp", registro.NroAIRHSP));
                    cmd.Parameters.Add(new SqlParameter("@vMeta", registro.Meta));
                    cmd.Parameters.Add(new SqlParameter("@iRespCurricular", 0));
                    cmd.Parameters.Add(new SqlParameter("@bEstado", registro.Estado));
                    cmd.Parameters.Add(new SqlParameter("@fRequerimiento", registro.fileRequerimiento));
                    cmd.Parameters.Add(new SqlParameter("@fCertificacion", registro.fileCertificacion));
                    cmd.Parameters.Add(new SqlParameter("@fComite", registro.fileComite));
                    cmd.Parameters.Add(new SqlParameter("@dfechamodificacion", registro.FechaModificacion));
                    cmd.Parameters.Add(new SqlParameter("@vUsuarioModificacion", registro.IdUsuarioModificacion));

                    cmd.ExecuteNonQuery();

                    cmd.Parameters.Clear();
                    cmd.CommandText = "[dbo].[Usp_ConvocatoriaPracComite_eliminar]";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));
                    cmd.Parameters.Add(new SqlParameter("@dfechamodificacion", registro.FechaModificacion));
                    cmd.Parameters.Add(new SqlParameter("@vUsuarioModificacion", registro.IdUsuarioModificacion));

                    cmd.ExecuteNonQuery();

                    foreach (ConvocatoriaComite_Registro objComite in registro.comite)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "[dbo].[Usp_ConvocatoriaPracComite_ins]";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));
                        cmd.Parameters.Add(new SqlParameter("@iCodMiembro", objComite.IdMiembro));
                        cmd.Parameters.Add(new SqlParameter("@iCodDependencia", objComite.IdDependencia));
                        cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", objComite.IdTrabajador));
                        cmd.Parameters.Add(new SqlParameter("@iCodTitular", objComite.IdTitular));
                        cmd.Parameters.Add(new SqlParameter("@bEstado", registro.Estado));
                        cmd.Parameters.Add(new SqlParameter("@dfecharegistro", registro.FechaModificacion));
                        cmd.Parameters.Add(new SqlParameter("@vUsuarioRegistro", registro.IdUsuarioModificacion));

                        cmd.ExecuteNonQuery();
                    }

                    if (_iBasesSqlAdoUnitOfWork.TieneTransaccion()) _iBasesSqlAdoUnitOfWork.ConfirmarTransaccion();
                }

            }
            catch (Exception)
            {
                if (_iBasesSqlAdoUnitOfWork.TieneTransaccion()) _iBasesSqlAdoUnitOfWork.RetrocederTransaccion();
                throw;
            }

            return registro.IdConvocatoria;
        }

        public Int32 RegistrarConvocatoriaComiteEntrevista(List<ConvocatoriaComite_Registro> lista)
        {
            try
            {
                _iBasesSqlAdoUnitOfWork.IniciarTransaccion(IsolationLevel.ReadCommitted);

                using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
                {
                    if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                    cmd.CommandText = "[dbo].[Usp_ConvocatoriaComiteEntrevista_del]";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", lista[0].IdConvocatoria));
                    cmd.Parameters.Add(new SqlParameter("@vUsuarioModificacion", lista[0].IdUsuarioRegistro));
                    cmd.ExecuteNonQuery();

                    cmd.Parameters.Clear();

                    cmd.CommandText = "[dbo].[Usp_ConvocatoriaComiteEntrevista_ins]";
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (ConvocatoriaComite_Registro objComite in lista)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", objComite.IdConvocatoria));
                        cmd.Parameters.Add(new SqlParameter("@iCodPostulacion", objComite.IdPostulacion));
                        cmd.Parameters.Add(new SqlParameter("@iCodPostulante", objComite.IdPostulante));
                        cmd.Parameters.Add(new SqlParameter("@iCodDependencia", objComite.IdDependencia));
                        cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", objComite.IdTrabajador));
                        cmd.Parameters.Add(new SqlParameter("@bEstado", objComite.Estado));
                        cmd.Parameters.Add(new SqlParameter("@dFechaEntrevista", objComite.FechaEntrevista));
                        cmd.Parameters.Add(new SqlParameter("@dHoraEntrevista", objComite.HoraEntrevista));
                        cmd.Parameters.Add(new SqlParameter("@dfecharegistro", objComite.FechaRegistro));
                        cmd.Parameters.Add(new SqlParameter("@vUsuarioRegistro", objComite.IdUsuarioRegistro));

                        cmd.ExecuteNonQuery();
                    }

                    //cmd.CommandText = "[dbo].[Usp_ConvocatoriaEvaluacionCurri_ins]";
                    //cmd.CommandType = CommandType.StoredProcedure;

                    //cmd.Parameters.Clear();
                    //cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", lista[0].IdConvocatoria));
                    //cmd.Parameters.Add(new SqlParameter("@dfecharegistro", lista[0].FechaRegistro));
                    //cmd.Parameters.Add(new SqlParameter("@vUsuarioRegistro", lista[0].IdUsuarioRegistro));

                    //cmd.ExecuteNonQuery();

                    if (_iBasesSqlAdoUnitOfWork.TieneTransaccion()) _iBasesSqlAdoUnitOfWork.ConfirmarTransaccion();
                }

            }
            catch (Exception)
            {
                if (_iBasesSqlAdoUnitOfWork.TieneTransaccion()) _iBasesSqlAdoUnitOfWork.RetrocederTransaccion();
                throw;
            }

            return 1;
        }
        public Int32 LimpiarEvaluacionEntrevista(PostulacionEvaluacionEntrevista_Registro registro)
        {
            try
            {
                _iBasesSqlAdoUnitOfWork.IniciarTransaccion(IsolationLevel.ReadCommitted);

                using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
                {
                    if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                    cmd.CommandText = "[dbo].[Usp_LimpiarEvaluacionEntrevista]";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@iCodEntrevista", registro.IdEvaluacion));
                    cmd.Parameters.Add(new SqlParameter("@vUsuarioModificacion", registro.IdUsuarioModificacion));
                    cmd.ExecuteNonQuery();

                    if (_iBasesSqlAdoUnitOfWork.TieneTransaccion()) _iBasesSqlAdoUnitOfWork.ConfirmarTransaccion();
                }

            }
            catch (Exception)
            {
                if (_iBasesSqlAdoUnitOfWork.TieneTransaccion()) _iBasesSqlAdoUnitOfWork.RetrocederTransaccion();
                throw;
            }

            return 1;
        }
        public Int32 DeclararAccesitarioGanador(PostulacionEvaluacionEntrevista_Registro registro)
        {
            try
            {
                _iBasesSqlAdoUnitOfWork.IniciarTransaccion(IsolationLevel.ReadCommitted);

                using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
                {
                    if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                    cmd.CommandText = "[dbo].[Usp_DeclararAccesitarioGanador]";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@idPostulacion", registro.IdPostulacion));
                    cmd.Parameters.Add(new SqlParameter("@idConvocatoria", registro.IdConvocatoria));
                    cmd.ExecuteNonQuery();

                    if (_iBasesSqlAdoUnitOfWork.TieneTransaccion()) _iBasesSqlAdoUnitOfWork.ConfirmarTransaccion();
                }

            }
            catch (Exception)
            {
                if (_iBasesSqlAdoUnitOfWork.TieneTransaccion()) _iBasesSqlAdoUnitOfWork.RetrocederTransaccion();
                throw;
            }

            return 1;
        }
        public Int32 RegistrarConvocatoriaPracticaComiteEntrevista(List<ConvocatoriaComite_Registro> lista)
        {
            try
            {
                _iBasesSqlAdoUnitOfWork.IniciarTransaccion(IsolationLevel.ReadCommitted);

                using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
                {
                    if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                    cmd.CommandText = "[dbo].[Usp_ConvocatoriaPracComiteEntrevista_del]";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", lista[0].IdConvocatoria));
                    cmd.Parameters.Add(new SqlParameter("@vUsuarioModificacion", lista[0].IdUsuarioRegistro));
                    cmd.ExecuteNonQuery();

                    cmd.Parameters.Clear();

                    cmd.CommandText = "[dbo].[Usp_ConvocatoriaPracComiteEntrevista_ins]";
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (ConvocatoriaComite_Registro objComite in lista)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", objComite.IdConvocatoria));
                        cmd.Parameters.Add(new SqlParameter("@iCodPostulacion", objComite.IdPostulacion));
                        cmd.Parameters.Add(new SqlParameter("@iCodPostulante", objComite.IdPostulante));
                        cmd.Parameters.Add(new SqlParameter("@iCodDependencia", objComite.IdDependencia));
                        cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", objComite.IdTrabajador));
                        cmd.Parameters.Add(new SqlParameter("@bEstado", objComite.Estado));
                        cmd.Parameters.Add(new SqlParameter("@dFechaEntrevista", objComite.FechaEntrevista));
                        cmd.Parameters.Add(new SqlParameter("@dHoraEntrevista", objComite.HoraEntrevista));
                        cmd.Parameters.Add(new SqlParameter("@dfecharegistro", objComite.FechaRegistro));
                        cmd.Parameters.Add(new SqlParameter("@vUsuarioRegistro", objComite.IdUsuarioRegistro));

                        cmd.ExecuteNonQuery();
                    }

                    if (_iBasesSqlAdoUnitOfWork.TieneTransaccion()) _iBasesSqlAdoUnitOfWork.ConfirmarTransaccion();
                }

            }
            catch (Exception)
            {
                if (_iBasesSqlAdoUnitOfWork.TieneTransaccion()) _iBasesSqlAdoUnitOfWork.RetrocederTransaccion();
                throw;
            }

            return 1;
        }

        public Postulacion_Registro ObtenerInformacionPostulacion(Postulante_Request peticion)
        {
            Postulacion_Registro registro = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[usp_ConvocatoriaPostulante_info]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", peticion.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", peticion.IdConvocatoria));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            registro = new Postulacion_Registro();
                            registro.Grilla = new Grilla_Response();

                            registro.IdPostulacion = dr.GetInt32(dr.GetOrdinal("iCodPostulacion"));
                            registro.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
                            registro.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoria"));
                            registro.Estado = dr.GetInt32(dr.GetOrdinal("bEstado"));

                            if (!dr.IsDBNull(dr.GetOrdinal("dFecharegistro"))) registro.FechaRegistro = dr.GetDateTime(dr.GetOrdinal("dFecharegistro"));
                        }
                    }
                }
            }

            return registro;
        }
        public Postulacion_Registro ObtenerInformacionPostulacionServir(Postulante_Request peticion)
        {
            Postulacion_Registro registro = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[usp_ConvocatoriaPostulanteServir_info]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", peticion.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", peticion.IdConvocatoria));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            registro = new Postulacion_Registro();
                            registro.Grilla = new Grilla_Response();

                            registro.IdPostulacion = dr.GetInt32(dr.GetOrdinal("iCodPostulacion"));
                            registro.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
                            registro.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoria"));
                            registro.Estado = dr.GetInt32(dr.GetOrdinal("bEstado"));

                            if (!dr.IsDBNull(dr.GetOrdinal("dFecharegistro"))) registro.FechaRegistro = dr.GetDateTime(dr.GetOrdinal("dFecharegistro"));
                        }
                    }
                }
            }

            return registro;
        }
        public PostulacionEvaluacionEntrevista_Registro ObtenerInformacionEntrevistaPersonal(PostulacionEvaluacionEntrevista_Registro peticion)
        {
            PostulacionEvaluacionEntrevista_Registro registro = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[Usp_ConvocatoriaEntrevista_sel]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaEntrevista", peticion.IdEvaluacion));
                
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            registro = new PostulacionEvaluacionEntrevista_Registro();
                            registro.Grilla = new Grilla_Response();

                            registro.AptoTotal = dr.GetInt32(dr.GetOrdinal("iAptoTotal"));
                            registro.PuntajeAspecto1 = dr.GetInt32(dr.GetOrdinal("iPuntajeAspecto1"));
                            registro.PuntajeAspecto2 = dr.GetInt32(dr.GetOrdinal("iPuntajeAspecto2"));
                            registro.PuntajeAspecto3 = dr.GetInt32(dr.GetOrdinal("iPuntajeAspecto3"));
                            registro.PuntajeAspecto4 = dr.GetInt32(dr.GetOrdinal("iPuntajeAspecto4"));
                            registro.PuntajeAspecto5 = dr.GetInt32(dr.GetOrdinal("iPuntajeAspecto5"));
                            registro.IdPresento = dr.GetInt32(dr.GetOrdinal("iPresento"));
                            registro.Observacion = dr.GetString(dr.GetOrdinal("vObservacion"));
                            registro.NroProceso = dr.GetString(dr.GetOrdinal("vNroCAS"));
                            registro.NombrePuesto = dr.GetString(dr.GetOrdinal("vNombrePuesto"));
                            registro.Dependencia = dr.GetString(dr.GetOrdinal("vDependencia"));
                            registro.Nombre = dr.GetString(dr.GetOrdinal("vNombrePostulante"));

                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaEntrevista"))) registro.FechaEntrevista = dr.GetDateTime(dr.GetOrdinal("dFechaEntrevista")).ToString("dd/MM/yyyy");
                            if (!dr.IsDBNull(dr.GetOrdinal("dHoraEntrevista"))) registro.HoraEntrevista = dr.GetTimeSpan(dr.GetOrdinal("dHoraEntrevista")).ToString(@"hh\:mm");
                        }
                    }
                }
            }

            return registro;
        }
        public PostulacionEvaluacionEntrevista_Registro ObtenerInformacionEntrevistaPersonalPractica(PostulacionEvaluacionEntrevista_Registro peticion)
        {
            PostulacionEvaluacionEntrevista_Registro registro = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[Usp_ConvocatoriaPracEntrevista_sel]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaEntrevista", peticion.IdEvaluacion));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            registro = new PostulacionEvaluacionEntrevista_Registro();
                            registro.Grilla = new Grilla_Response();

                            registro.AptoTotal = dr.GetInt32(dr.GetOrdinal("iAptoTotal"));
                            registro.PuntajeAspecto1 = dr.GetInt32(dr.GetOrdinal("iPuntajeAspecto1"));
                            registro.PuntajeAspecto2 = dr.GetInt32(dr.GetOrdinal("iPuntajeAspecto2"));
                            registro.PuntajeAspecto3 = dr.GetInt32(dr.GetOrdinal("iPuntajeAspecto3"));
                            registro.PuntajeAspecto4 = dr.GetInt32(dr.GetOrdinal("iPuntajeAspecto4"));
                            registro.PuntajeAspecto5 = dr.GetInt32(dr.GetOrdinal("iPuntajeAspecto5"));
                            registro.IdPresento = dr.GetInt32(dr.GetOrdinal("iPresento"));
                            registro.Observacion = dr.GetString(dr.GetOrdinal("vObservacion"));
                            registro.NroProceso = dr.GetString(dr.GetOrdinal("vNroCAS"));
                            registro.NombrePuesto = dr.GetString(dr.GetOrdinal("vNombrePuesto"));
                            registro.Dependencia = dr.GetString(dr.GetOrdinal("vDependencia"));
                            registro.Nombre = dr.GetString(dr.GetOrdinal("vNombrePostulante"));

                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaEntrevista"))) registro.FechaEntrevista = dr.GetDateTime(dr.GetOrdinal("dFechaEntrevista")).ToString("dd/MM/yyyy");
                            if (!dr.IsDBNull(dr.GetOrdinal("dHoraEntrevista"))) registro.HoraEntrevista = dr.GetTimeSpan(dr.GetOrdinal("dHoraEntrevista")).ToString(@"hh\:mm");
                        }
                    }
                }
            }

            return registro;
        }

        public PostulacionAnexo_Registro ObtenerPostulacionAnexo(Postulacion_Registro peticion)
        {
            PostulacionAnexo_Registro registro = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[Usp_PostulacionAnexo_sel]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulacion", peticion.IdPostulacion));
                
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            registro = new PostulacionAnexo_Registro();
                            registro.Grilla = new Grilla_Response();

                            registro.IdPostulacionAnexo = dr.GetInt32(dr.GetOrdinal("iCodPostulacionAnexo"));
                            registro.IdPostulacion = dr.GetInt32(dr.GetOrdinal("iCodPostulacion"));
                            registro.IdAcepta = dr.GetInt32(dr.GetOrdinal("bAcepta"));
                            registro.TipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            registro.NroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            registro.Nombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            registro.Paterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            registro.Materno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            registro.NepotismoRel1 = dr.GetString(dr.GetOrdinal("vPostulanteRel1"));
                            registro.NepotismoRel2 = dr.GetString(dr.GetOrdinal("vPostulanteRel2"));
                            registro.NepotismoRel3 = dr.GetString(dr.GetOrdinal("vPostulanteRel3"));
                            registro.NepotismoApe1 = dr.GetString(dr.GetOrdinal("vPostulanteApe1"));
                            registro.NepotismoApe2 = dr.GetString(dr.GetOrdinal("vPostulanteApe2"));
                            registro.NepotismoApe3 = dr.GetString(dr.GetOrdinal("vPostulanteApe3"));
                            registro.NepotismoNom1 = dr.GetString(dr.GetOrdinal("vPostulanteNom1"));
                            registro.NepotismoNom2 = dr.GetString(dr.GetOrdinal("vPostulanteNom2"));
                            registro.NepotismoNom3 = dr.GetString(dr.GetOrdinal("vPostulanteNom3"));
                            registro.NepotismoAre1 = dr.GetString(dr.GetOrdinal("vPostulanteAre1"));
                            registro.NepotismoAre2 = dr.GetString(dr.GetOrdinal("vPostulanteAre2"));
                            registro.NepotismoAre3 = dr.GetString(dr.GetOrdinal("vPostulanteAre3"));
                            registro.Direccion = dr.GetString(dr.GetOrdinal("vDomicilio")) + " " + dr.GetString(dr.GetOrdinal("vUbigeo"));
                            registro.NroCAS = dr.GetString(dr.GetOrdinal("vNroCAS"));
                            registro.Puesto = dr.GetString(dr.GetOrdinal("vNombrePuesto"));

                            if (!dr.IsDBNull(dr.GetOrdinal("dFecharegistro"))) registro.FechaRegistro = dr.GetDateTime(dr.GetOrdinal("dFecharegistro"));
                        }
                    }
                }
            }

            return registro;
        }
        public PostulacionAnexo_Registro ObtenerPostulacionAnexoServir(Postulacion_Registro peticion)
        {
            PostulacionAnexo_Registro registro = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[Usp_PostulacionAnexoServir_sel]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulacion", peticion.IdPostulacion));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            registro = new PostulacionAnexo_Registro();
                            registro.Grilla = new Grilla_Response();

                            registro.IdPostulacionAnexo = dr.GetInt32(dr.GetOrdinal("iCodPostulacionAnexo"));
                            registro.IdPostulacion = dr.GetInt32(dr.GetOrdinal("iCodPostulacion"));
                            registro.IdAcepta = dr.GetInt32(dr.GetOrdinal("bAcepta"));
                            registro.TipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            registro.NroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            registro.Nombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            registro.Paterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            registro.Materno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            registro.NepotismoRel1 = dr.GetString(dr.GetOrdinal("vPostulanteRel1"));
                            registro.NepotismoRel2 = dr.GetString(dr.GetOrdinal("vPostulanteRel2"));
                            registro.NepotismoRel3 = dr.GetString(dr.GetOrdinal("vPostulanteRel3"));
                            registro.NepotismoApe1 = dr.GetString(dr.GetOrdinal("vPostulanteApe1"));
                            registro.NepotismoApe2 = dr.GetString(dr.GetOrdinal("vPostulanteApe2"));
                            registro.NepotismoApe3 = dr.GetString(dr.GetOrdinal("vPostulanteApe3"));
                            registro.NepotismoNom1 = dr.GetString(dr.GetOrdinal("vPostulanteNom1"));
                            registro.NepotismoNom2 = dr.GetString(dr.GetOrdinal("vPostulanteNom2"));
                            registro.NepotismoNom3 = dr.GetString(dr.GetOrdinal("vPostulanteNom3"));
                            registro.NepotismoAre1 = dr.GetString(dr.GetOrdinal("vPostulanteAre1"));
                            registro.NepotismoAre2 = dr.GetString(dr.GetOrdinal("vPostulanteAre2"));
                            registro.NepotismoAre3 = dr.GetString(dr.GetOrdinal("vPostulanteAre3"));
                            registro.Direccion = dr.GetString(dr.GetOrdinal("vDomicilio")) + " " + dr.GetString(dr.GetOrdinal("vUbigeo"));
                            registro.NroCAS = dr.GetString(dr.GetOrdinal("vNroCAS"));
                            registro.Puesto = dr.GetString(dr.GetOrdinal("vNombrePuesto"));

                            if (!dr.IsDBNull(dr.GetOrdinal("dFecharegistro"))) registro.FechaRegistro = dr.GetDateTime(dr.GetOrdinal("dFecharegistro"));
                        }
                    }
                }
            }

            return registro;
        }
        public Postulacion_Registro InsertarRegistroPostulacion(Postulante_Request peticion)
        {
            Postulacion_Registro registro = new Postulacion_Registro();

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[usp_Postulacion_ins]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                SqlParameter IdPropuestaParameter = new SqlParameter("@iCodPostulacion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(IdPropuestaParameter);
                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", peticion.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", peticion.IdConvocatoria));

                cmd.ExecuteNonQuery();

                registro.IdPostulacion = Int32.Parse(IdPropuestaParameter.Value.ToString());
                registro.IdPostulante = peticion.IdPostulante;
                registro.IdConvocatoria = peticion.IdConvocatoria;

            }

            return registro;
        }
        public Postulacion_Registro InsertarRegistroPostulacionServir(Postulante_Request peticion)
        {
            Postulacion_Registro registro = new Postulacion_Registro();

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[usp_PostulacionServir_ins]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                SqlParameter IdPropuestaParameter = new SqlParameter("@iCodPostulacion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(IdPropuestaParameter);
                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", peticion.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", peticion.IdConvocatoria));

                cmd.ExecuteNonQuery();

                registro.IdPostulacion = Int32.Parse(IdPropuestaParameter.Value.ToString());
                registro.IdPostulante = peticion.IdPostulante;
                registro.IdConvocatoria = peticion.IdConvocatoria;

            }

            return registro;
        }

        public Postulacion_Registro ListarPostulacionRequisitos(Postulacion_Registro peticion)
        {
            Postulacion_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[usp_ConvocatoriaRequisitos_info]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulacion", peticion.IdPostulacion));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoConvocatoria", peticion.IdTipoConvocatoria));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Postulacion_Registro();

                            item.IdPostulacion = dr.GetInt32(dr.GetOrdinal("iCodPostulacion"));
                            item.ExisteEstudio = dr.GetInt32(dr.GetOrdinal("existeEstudio"));
                            item.ExisteCapacitacion = dr.GetInt32(dr.GetOrdinal("existeCapacitacion"));
                            item.ExisteExperiencia = dr.GetInt32(dr.GetOrdinal("existeExperiencia"));
                            item.ExisteAnexo = dr.GetInt32(dr.GetOrdinal("existeAnexo"));
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return item;
        }

        public IEnumerable<ConvocatoriaComite_Registro> ListarConvocatoriaComite(Convocatoria_Request peticion)
        {
            List<ConvocatoriaComite_Registro> lista = new List<ConvocatoriaComite_Registro>();
            ConvocatoriaComite_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[usp_Convocatoria_ListarComite]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", peticion.IdConvocatoria));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new ConvocatoriaComite_Registro();

                            //item.Grilla = new Grilla_Response();
                            item.IdConvocatoriaComite = dr.GetInt32(dr.GetOrdinal("iCodConvocatoriaComite"));
                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoria"));
                            item.IdDependencia = dr.GetInt32(dr.GetOrdinal("iCodDependencia"));
                            item.IdMiembro = dr.GetInt32(dr.GetOrdinal("iCodMiembro"));
                            item.IdTitular = dr.GetInt32(dr.GetOrdinal("iCodTitular"));
                            item.IdTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            item.NombreMiembro = dr.GetString(dr.GetOrdinal("vNombres")) + " " + dr.GetString(dr.GetOrdinal("vApellidoPaterno")) + " " + dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            item.NombreDependencia = dr.GetString(dr.GetOrdinal("vDependencia"));
                            
                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<ConvocatoriaComite_Registro> ListarConvocatoriaServirComite(Convocatoria_Request peticion)
        {
            List<ConvocatoriaComite_Registro> lista = new List<ConvocatoriaComite_Registro>();
            ConvocatoriaComite_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[usp_ConvocatoriaServir_ListarComite]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", peticion.IdConvocatoria));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new ConvocatoriaComite_Registro();

                            //item.Grilla = new Grilla_Response();
                            item.IdConvocatoriaComite = dr.GetInt32(dr.GetOrdinal("iCodConvocatoriaComite"));
                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoria"));
                            item.IdDependencia = dr.GetInt32(dr.GetOrdinal("iCodDependencia"));
                            item.IdMiembro = dr.GetInt32(dr.GetOrdinal("iCodMiembro"));
                            item.IdTitular = dr.GetInt32(dr.GetOrdinal("iCodTitular"));
                            item.IdTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            item.NombreMiembro = dr.GetString(dr.GetOrdinal("vNombres")) + " " + dr.GetString(dr.GetOrdinal("vApellidoPaterno")) + " " + dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            item.NombreDependencia = dr.GetString(dr.GetOrdinal("vDependencia"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<ConvocatoriaComite_Registro> ListarConvocatoriaPracComite(Convocatoria_Request peticion)
        {
            List<ConvocatoriaComite_Registro> lista = new List<ConvocatoriaComite_Registro>();
            ConvocatoriaComite_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[usp_ConvocatoriaPrac_ListarComite]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", peticion.IdConvocatoria));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new ConvocatoriaComite_Registro();

                            //item.Grilla = new Grilla_Response();
                            item.IdConvocatoriaComite = dr.GetInt32(dr.GetOrdinal("iCodConvocatoriaPracComite"));
                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoriaPrac"));
                            item.IdDependencia = dr.GetInt32(dr.GetOrdinal("iCodDependencia"));
                            item.IdMiembro = dr.GetInt32(dr.GetOrdinal("iCodMiembro"));
                            item.IdTitular = dr.GetInt32(dr.GetOrdinal("iCodTitular"));
                            item.IdTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            item.NombreMiembro = dr.GetString(dr.GetOrdinal("vNombres")) + " " + dr.GetString(dr.GetOrdinal("vApellidoPaterno")) + " " + dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            item.NombreDependencia = dr.GetString(dr.GetOrdinal("vDependencia"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<ConvocatoriaDocumento_Registro> ListarConvocatoriaDocumento(Convocatoria_Request peticion)
        {
            List<ConvocatoriaDocumento_Registro> lista = new List<ConvocatoriaDocumento_Registro>();
            ConvocatoriaDocumento_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[usp_ConvocatoriaDocumento_listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", peticion.IdConvocatoria));
                cmd.Parameters.Add(new SqlParameter("@tipo", peticion.IdTipo));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new ConvocatoriaDocumento_Registro();

                            //item.Grilla = new Grilla_Response();
                            item.IdConvocatoriaDocumento = dr.GetInt32(dr.GetOrdinal("iCodConvocatoriaDocumento"));
                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoria"));
                            item.IdTipoDocumento = dr.GetInt32(dr.GetOrdinal("iTipoDocumento"));
                            item.NombreDocumento = dr.GetString(dr.GetOrdinal("vNombre"));
                            item.TipoDocumento = dr.GetString(dr.GetOrdinal("TipoPublicacion"));
                            if (!dr.IsDBNull(dr.GetOrdinal("dFecharegistro"))) item.FechaRegistro = dr.GetDateTime(dr.GetOrdinal("dFecharegistro"));
                            
                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<ConvocatoriaComite_Registro> ListarConvocatoriaTipoDocumento(Convocatoria_Request peticion)
        {
            List<ConvocatoriaComite_Registro> lista = new List<ConvocatoriaComite_Registro>();
            ConvocatoriaComite_Registro item = null;

            //using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            //{
            //    if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
            //    cmd.CommandText = "[dbo].[usp_Convocatoria_ListarComite]";
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    //cmd.CommandTimeout = cmd.CommandTimeout;

            //    cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", peticion.IdConvocatoria));

            //    using (SqlDataReader dr = cmd.ExecuteReader())
            //    {
            //        if (dr.HasRows)
            //        {
            //            while (dr.Read())
            //            {
            //                item = new ConvocatoriaComite_Registro();

            //                //item.Grilla = new Grilla_Response();
            //                item.IdConvocatoriaComite = dr.GetInt32(dr.GetOrdinal("iCodConvocatoriaComite"));
            //                item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoria"));
            //                item.IdDependencia = dr.GetInt32(dr.GetOrdinal("iCodDependencia"));
            //                item.IdMiembro = dr.GetInt32(dr.GetOrdinal("iCodMiembro"));
            //                item.IdTitular = dr.GetInt32(dr.GetOrdinal("iCodTitular"));
            //                item.IdTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
            //                item.NombreMiembro = dr.GetString(dr.GetOrdinal("vNombres")) + " " + dr.GetString(dr.GetOrdinal("vApellidoPaterno")) + " " + dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
            //                item.NombreDependencia = dr.GetString(dr.GetOrdinal("vDependencia"));

            //                lista.Add(item);
            //            }
            //        }
            //    }

            //    if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            //}

            return lista;
        }
        public Int32 EliminarComunicado(ConvocatoriaDocumento_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_ConvocatoriaDocumento_del]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaDocumento", registro.IdConvocatoriaDocumento));
                cmd.Parameters.Add(new SqlParameter("@vUsuarioModificacion", registro.IdUsuarioModificacion));
                
                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdConvocatoriaDocumento;
        }
        public Int32 EliminarComunicadoServir(ConvocatoriaDocumento_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_ConvocatoriaServirDocumento_del]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaDocumento", registro.IdConvocatoriaDocumento));
                cmd.Parameters.Add(new SqlParameter("@vUsuarioModificacion", registro.IdUsuarioModificacion));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdConvocatoriaDocumento;
        }
        public Int32 EliminarComunicadoPractica(ConvocatoriaDocumento_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_ConvocatoriaPracticaDocumento_del]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaDocumento", registro.IdConvocatoriaDocumento));
                cmd.Parameters.Add(new SqlParameter("@vUsuarioModificacion", registro.IdUsuarioModificacion));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdConvocatoriaDocumento;
        }
        public Int32 IniciarEvaluacionConocimiento(Convocatoria_Request registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_Convocatoria_EvalConocimiento_ini]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));
                cmd.Parameters.Add(new SqlParameter("@vUsuarioRegistro", registro.IdUsuarioRegistro));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdConvocatoria;
        }

        public Int32 IniciarEvaluacionCurri(Convocatoria_Request registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_Convocatoria_EvalCurri_ini]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));
                cmd.Parameters.Add(new SqlParameter("@vUsuarioRegistro", registro.IdUsuarioRegistro));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdConvocatoria;
        }
        public Int32 IniciarEvaluacionCurriPractica(Convocatoria_Request registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_ConvocatoriaPractica_EvalCurri_ini]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));
                cmd.Parameters.Add(new SqlParameter("@vUsuarioRegistro", registro.IdUsuarioRegistro));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdConvocatoria;
        }
        public Int32 IniciarEntrevistaPracticaPreguntasMaestras(PostulacionEntrevistaPersonal_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_ConvocatoriaPracPreguntaMaestra_ini]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaEntrevista", registro.IdEvaluacion));
                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", registro.IdTrabajador));
                cmd.Parameters.Add(new SqlParameter("@vUsuarioRegistro", registro.IdUsuarioRegistro));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdEvaluacion;
        }
        public Int32 IniciarEntrevistaPreguntasMaestras(PostulacionEntrevistaPersonal_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_ConvocatoriaPreguntaMaestra_ini]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaEntrevista", registro.IdEvaluacion));
                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", registro.IdTrabajador));
                cmd.Parameters.Add(new SqlParameter("@vUsuarioRegistro", registro.IdUsuarioRegistro));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdEvaluacion;
        }
        public String ActualizarEvaluacionCurri(PostulacionEvaluacionCurricular_Registro registro)
        {
            String resultado = String.Empty;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[usp_EvaluacionCurri_upd]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaEvalCurri", registro.IdEvaluacion));
                cmd.Parameters.Add(new SqlParameter("@iAptoFormacion", registro.AptoFormacion));
                cmd.Parameters.Add(new SqlParameter("@iAptoCapacitacion", registro.AptoCapacitacion));
                cmd.Parameters.Add(new SqlParameter("@iAptoExperienciaGen", registro.AptoExperienciaGen));
                cmd.Parameters.Add(new SqlParameter("@iAptoExperienciaEsp", registro.AptoExperienciaEsp));

                cmd.Parameters.Add(new SqlParameter("@iPuntajeBonifFormacion", registro.PuntajeBonifFormacion));
                cmd.Parameters.Add(new SqlParameter("@iPuntajeBonifExperienciaEsp", registro.PuntajeBonifExperienciaEsp));
                cmd.Parameters.Add(new SqlParameter("@iAptoDDJJ", registro.AptoDDJJ));
                cmd.Parameters.Add(new SqlParameter("@iAptoSanciones", registro.AptoSanciones));

                cmd.Parameters.Add(new SqlParameter("@iBonifDeporte", registro.BonifDeporte));
                cmd.Parameters.Add(new SqlParameter("@iBonifFFAA", registro.BonifFFAA));
                cmd.Parameters.Add(new SqlParameter("@iBonifDiscapacidad", registro.BonifDiscapacidad));

                if (registro.FechaConocimiento != "null") cmd.Parameters.Add(new SqlParameter("@dFechaConocimiento", registro.FechaConocimiento));
                if (registro.FechaEntrevista != "null") cmd.Parameters.Add(new SqlParameter("@dFechaEntrevista", registro.FechaEntrevista));
                if (registro.HoraConocimiento != "null") cmd.Parameters.Add(new SqlParameter("@dHoraConocimiento", registro.HoraConocimiento));
                if (registro.HoraEntrevista != "null") cmd.Parameters.Add(new SqlParameter("@dHoraEntrevista", registro.HoraEntrevista));

                cmd.Parameters.Add(new SqlParameter("@iUsuarioModificacion", registro.IdUsuarioModificacion.ToString()));

                SqlParameter IdPropuestaParameter1 = new SqlParameter("@iAptoTotalAux", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(IdPropuestaParameter1);
                SqlParameter IdPropuestaParameter2 = new SqlParameter("@dPuntajeTotalAux", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(IdPropuestaParameter2);

                cmd.ExecuteNonQuery();
                resultado = IdPropuestaParameter1.Value.ToString() + "|" + IdPropuestaParameter2.Value.ToString();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return resultado;
        }
        public String ActualizarServirEvaluacionCurri(PostulacionEvaluacionCurricular_Registro registro)
        {
            String resultado = String.Empty;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[usp_ServirEvaluacionCurri_upd]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaEvalCurri", registro.IdEvaluacion));
                cmd.Parameters.Add(new SqlParameter("@iAptoFormacion", registro.AptoFormacion));
                cmd.Parameters.Add(new SqlParameter("@iAptoCapacitacion", registro.AptoCapacitacion));
                cmd.Parameters.Add(new SqlParameter("@iAptoExperienciaGen", registro.AptoExperienciaGen));
                cmd.Parameters.Add(new SqlParameter("@iAptoExperienciaEsp", registro.AptoExperienciaEsp));

                cmd.Parameters.Add(new SqlParameter("@iPuntajeBonifFormacion", registro.PuntajeBonifFormacion));
                cmd.Parameters.Add(new SqlParameter("@iPuntajeBonifExperienciaEsp", registro.PuntajeBonifExperienciaEsp));
                cmd.Parameters.Add(new SqlParameter("@iAptoDDJJ", registro.AptoDDJJ));
                cmd.Parameters.Add(new SqlParameter("@iAptoSanciones", registro.AptoSanciones));

                cmd.Parameters.Add(new SqlParameter("@iBonifDeporte", registro.BonifDeporte));
                cmd.Parameters.Add(new SqlParameter("@iBonifFFAA", registro.BonifFFAA));
                cmd.Parameters.Add(new SqlParameter("@iBonifDiscapacidad", registro.BonifDiscapacidad));

                if (registro.FechaConocimiento != "null") cmd.Parameters.Add(new SqlParameter("@dFechaConocimiento", registro.FechaConocimiento));
                if (registro.FechaEntrevista != "null") cmd.Parameters.Add(new SqlParameter("@dFechaEntrevista", registro.FechaEntrevista));
                if (registro.HoraConocimiento != "null") cmd.Parameters.Add(new SqlParameter("@dHoraConocimiento", registro.HoraConocimiento));
                if (registro.HoraEntrevista != "null") cmd.Parameters.Add(new SqlParameter("@dHoraEntrevista", registro.HoraEntrevista));

                cmd.Parameters.Add(new SqlParameter("@iUsuarioModificacion", registro.IdUsuarioModificacion.ToString()));

                SqlParameter IdPropuestaParameter1 = new SqlParameter("@iAptoTotalAux", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(IdPropuestaParameter1);
                SqlParameter IdPropuestaParameter2 = new SqlParameter("@dPuntajeTotalAux", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(IdPropuestaParameter2);

                cmd.ExecuteNonQuery();
                resultado = IdPropuestaParameter1.Value.ToString() + "|" + IdPropuestaParameter2.Value.ToString();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return resultado;
        }
        public String ActualizarPracticaEvaluacionCurri(PostulacionEvaluacionCurricular_Registro registro)
        {
            String resultado = String.Empty;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[usp_PracticaEvaluacionCurri_upd]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaEvalCurri", registro.IdEvaluacion));
                cmd.Parameters.Add(new SqlParameter("@iAptoDDJJ", registro.AptoDDJJ));
                
                cmd.Parameters.Add(new SqlParameter("@iBonifDeporte", registro.BonifDeporte));
                cmd.Parameters.Add(new SqlParameter("@iBonifFFAA", registro.BonifFFAA));
                cmd.Parameters.Add(new SqlParameter("@iBonifDiscapacidad", registro.BonifDiscapacidad));

                //if (registro.FechaConocimiento != "null") cmd.Parameters.Add(new SqlParameter("@dFechaConocimiento", registro.FechaConocimiento));
                if (registro.FechaEntrevista != "null") cmd.Parameters.Add(new SqlParameter("@dFechaEntrevista", registro.FechaEntrevista));
                //if (registro.HoraConocimiento != "null") cmd.Parameters.Add(new SqlParameter("@dHoraConocimiento", registro.HoraConocimiento));
                if (registro.HoraEntrevista != "null") cmd.Parameters.Add(new SqlParameter("@dHoraEntrevista", registro.HoraEntrevista));

                cmd.Parameters.Add(new SqlParameter("@iUsuarioModificacion", registro.IdUsuarioModificacion.ToString()));

                SqlParameter IdPropuestaParameter1 = new SqlParameter("@iAptoTotalAux", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(IdPropuestaParameter1);
                SqlParameter IdPropuestaParameter2 = new SqlParameter("@dPuntajeTotalAux", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(IdPropuestaParameter2);

                cmd.ExecuteNonQuery();
                resultado = IdPropuestaParameter1.Value.ToString() + "|" + IdPropuestaParameter2.Value.ToString();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return resultado;
        }
        public Int32 ActualizarEvaluacionCurriObs(PostulacionEvaluacionCurricular_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[usp_EvaluacionCurriObs_upd]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaEvalCurri", registro.IdEvaluacion));
                cmd.Parameters.Add(new SqlParameter("@vObservacion", registro.Observacion));
                cmd.Parameters.Add(new SqlParameter("@iUsuarioModificacion", registro.IdUsuarioModificacion.ToString()));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdEvaluacion;
        }
        public Int32 ActualizarPracticaEvaluacionCurriObs(PostulacionEvaluacionCurricular_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[usp_EvaluacionPracticaCurriObs_upd]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaEvalCurri", registro.IdEvaluacion));
                cmd.Parameters.Add(new SqlParameter("@vObservacion", registro.Observacion));
                cmd.Parameters.Add(new SqlParameter("@iUsuarioModificacion", registro.IdUsuarioModificacion.ToString()));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdEvaluacion;
        }
        public Int32 ActualizarEvaluacionNSP(PostulacionEvaluacionEntrevista_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[usp_EvaluacionEntrevista_NSP]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaEntrevista", registro.IdEvaluacion));
                
                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdEvaluacion;
        }
        public Int32 ActualizarEvaluacionPracticaNSP(PostulacionEvaluacionEntrevista_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[usp_EvaluacionPracEntrevista_NSP]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaEntrevista", registro.IdEvaluacion));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdEvaluacion;
        }
        public Int32 ActualizarEvaluacionConocimiento(PostulacionEvaluacionConocimiento_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[usp_EvaluacionConoc_upd]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaEvalConoc", registro.IdEvaluacion));
                cmd.Parameters.Add(new SqlParameter("@iPuntajeTotal", registro.PuntajeTotal));
                cmd.Parameters.Add(new SqlParameter("@dFechaEntrevista", registro.FechaEntrevista));
                cmd.Parameters.Add(new SqlParameter("@dHoraEntrevista", registro.HoraEntrevista));
                cmd.Parameters.Add(new SqlParameter("@vObservacion", registro.Observacion));
                cmd.Parameters.Add(new SqlParameter("@iUsuarioModificacion", registro.IdUsuarioModificacion.ToString()));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdEvaluacion;
        }
        public Int32 ActualizarEvaluacionEntrevistaPersonal(PostulacionEvaluacionEntrevista_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[usp_EvaluacionEntrevista_upd]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaEntrevista", registro.IdEvaluacion));
                cmd.Parameters.Add(new SqlParameter("@iPuntajeAspecto1", registro.PuntajeAspecto1));
                cmd.Parameters.Add(new SqlParameter("@iPuntajeAspecto2", registro.PuntajeAspecto2));
                cmd.Parameters.Add(new SqlParameter("@iPuntajeAspecto3", registro.PuntajeAspecto3));
                cmd.Parameters.Add(new SqlParameter("@iPuntajeAspecto4", registro.PuntajeAspecto4));
                cmd.Parameters.Add(new SqlParameter("@iPuntajeAspecto5", registro.PuntajeAspecto5));
                cmd.Parameters.Add(new SqlParameter("@vObservacion", registro.Observacion));
                cmd.Parameters.Add(new SqlParameter("@iUsuarioModificacion", registro.IdUsuarioModificacion.ToString()));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdEvaluacion;
        }
        public Int32 ActualizarEvaluacionPracticaEntrevistaPersonal(PostulacionEvaluacionEntrevista_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[usp_EvaluacionPracEntrevista_upd]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaEntrevista", registro.IdEvaluacion));
                cmd.Parameters.Add(new SqlParameter("@iPuntajeAspecto1", registro.PuntajeAspecto1));
                cmd.Parameters.Add(new SqlParameter("@iPuntajeAspecto2", registro.PuntajeAspecto2));
                cmd.Parameters.Add(new SqlParameter("@iPuntajeAspecto3", registro.PuntajeAspecto3));
                cmd.Parameters.Add(new SqlParameter("@iPuntajeAspecto4", registro.PuntajeAspecto4));
                cmd.Parameters.Add(new SqlParameter("@iPuntajeAspecto5", registro.PuntajeAspecto5));
                cmd.Parameters.Add(new SqlParameter("@vObservacion", registro.Observacion));
                cmd.Parameters.Add(new SqlParameter("@iUsuarioModificacion", registro.IdUsuarioModificacion.ToString()));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdEvaluacion;
        }
        public Int32 ActualizarActaEntrevistaPersonal(PostulacionEvaluacionEntrevista_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[usp_EvaluacionEntrevistaActa_upd]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaEntrevista", registro.IdEvaluacion));
                if (registro.archivo != null) cmd.Parameters.Add(new SqlParameter("@ARCHIVO", registro.archivo));
                cmd.Parameters.Add(new SqlParameter("@iUsuarioModificacion", registro.IdUsuarioModificacion.ToString()));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdEvaluacion;
        }
        public Int32 ActualizarActaEntrevistaPersonalPractica(PostulacionEvaluacionEntrevista_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[usp_EvaluacionPracEntrevistaActa_upd]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaEntrevista", registro.IdEvaluacion));
                if (registro.archivo != null) cmd.Parameters.Add(new SqlParameter("@ARCHIVO", registro.archivo));
                cmd.Parameters.Add(new SqlParameter("@iUsuarioModificacion", registro.IdUsuarioModificacion.ToString()));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdEvaluacion;
        }

        public Convocatoria_Registro ObtenerParaEditar(Convocatoria_Request peticion)
        {
            Convocatoria_Registro registro = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[Usp_Convocatoria_sel]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", peticion.IdConvocatoria));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            registro = new Convocatoria_Registro();
                            registro.Grilla = new Grilla_Response();

                            registro.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoria"));
                            registro.IdBase = dr.GetInt32(dr.GetOrdinal("iCodBase"));
                            registro.IdPerfil = dr.GetInt32(dr.GetOrdinal("iCodPerfil"));
                            registro.IdDependencia = dr.GetInt32(dr.GetOrdinal("iCodDependencia"));
                            registro.NroAIRHSP = dr.GetString(dr.GetOrdinal("vAirhsp"));
                            registro.Meta = dr.GetString(dr.GetOrdinal("vMeta"));
                            registro.Estado = dr.GetInt32(dr.GetOrdinal("bEstado"));
                            registro.NombreCargo = dr.GetString(dr.GetOrdinal("vNombrePuesto"));
                            registro.NroConvocatoria = dr.GetString(dr.GetOrdinal("vNroCAS"));
                            registro.CantidadVacantes = dr.GetInt32(dr.GetOrdinal("iCantPersonalRequerido"));
                            registro.Dependencia = dr.GetString(dr.GetOrdinal("vDependencia"));
                            registro.Remuneracion = dr.GetDecimal(dr.GetOrdinal("fRemuneracion"));

                            registro.IdTieneRequerimiento = dr.GetInt32(dr.GetOrdinal("TieneDocRequerimiento"));
                            registro.IdTieneCertificacion = dr.GetInt32(dr.GetOrdinal("TieneDocCertificacion"));
                            registro.IdTieneComite = dr.GetInt32(dr.GetOrdinal("TieneDocComite"));
                            registro.IdTieneActaCurri = dr.GetInt32(dr.GetOrdinal("TieneActaCurri"));

                            registro.IdTieneExamenConoc = dr.GetInt32(dr.GetOrdinal("bTieneExamenConoc"));
                            registro.IdTieneExamenPsico = dr.GetInt32(dr.GetOrdinal("bTieneExamenPsico"));
                            //if (!dr.IsDBNull(dr.GetOrdinal("dtInicioLabores"))) registro.FechaInicio = dr.GetDateTime(dr.GetOrdinal("dtInicioLabores"));
                            //if (!dr.IsDBNull(dr.GetOrdinal("dtFinLabores"))) registro.FechaCese = dr.GetDateTime(dr.GetOrdinal("dtFinLabores"));

                        }
                    }
                }
            }

            return registro;
        }
        public Convocatoria_Registro ObtenerServirParaEditar(Convocatoria_Request peticion)
        {
            Convocatoria_Registro registro = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[Usp_ConvocatoriaServir_sel]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", peticion.IdConvocatoria));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            registro = new Convocatoria_Registro();
                            registro.Grilla = new Grilla_Response();

                            registro.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoria"));
                            registro.IdBase = dr.GetInt32(dr.GetOrdinal("iCodBase"));
                            registro.IdPerfil = dr.GetInt32(dr.GetOrdinal("iCodPerfil"));
                            registro.IdDependencia = dr.GetInt32(dr.GetOrdinal("iCodDependencia"));
                            registro.NroAIRHSP = dr.GetString(dr.GetOrdinal("vAirhsp"));
                            registro.Meta = dr.GetString(dr.GetOrdinal("vMeta"));
                            registro.Estado = dr.GetInt32(dr.GetOrdinal("bEstado"));
                            registro.NombreCargo = dr.GetString(dr.GetOrdinal("vNombrePuesto"));
                            registro.NroConvocatoria = dr.GetString(dr.GetOrdinal("vNroCAS"));
                            registro.CantidadVacantes = dr.GetInt32(dr.GetOrdinal("iCantPersonalRequerido"));
                            registro.Dependencia = dr.GetString(dr.GetOrdinal("vDependencia"));
                            registro.Remuneracion = dr.GetDecimal(dr.GetOrdinal("fRemuneracion"));

                            registro.IdTieneRequerimiento = dr.GetInt32(dr.GetOrdinal("TieneDocRequerimiento"));
                            registro.IdTieneCertificacion = dr.GetInt32(dr.GetOrdinal("TieneDocCertificacion"));
                            registro.IdTieneComite = dr.GetInt32(dr.GetOrdinal("TieneDocComite"));
                            registro.IdTieneActaCurri = dr.GetInt32(dr.GetOrdinal("TieneActaCurri"));

                            registro.IdTieneExamenConoc = dr.GetInt32(dr.GetOrdinal("bTieneExamenConoc"));
                            registro.IdTieneExamenPsico = dr.GetInt32(dr.GetOrdinal("bTieneExamenPsico"));
                            //if (!dr.IsDBNull(dr.GetOrdinal("dtInicioLabores"))) registro.FechaInicio = dr.GetDateTime(dr.GetOrdinal("dtInicioLabores"));
                            //if (!dr.IsDBNull(dr.GetOrdinal("dtFinLabores"))) registro.FechaCese = dr.GetDateTime(dr.GetOrdinal("dtFinLabores"));

                        }
                    }
                }
            }

            return registro;
        }
        public Convocatoria_Registro ObtenerPracticaParaEditar(Convocatoria_Request peticion)
        {
            Convocatoria_Registro registro = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[Usp_ConvocatoriaPrac_sel]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaPrac", peticion.IdConvocatoria));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            registro = new Convocatoria_Registro();
                            registro.Grilla = new Grilla_Response();

                            registro.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoria"));
                            registro.IdBase = dr.GetInt32(dr.GetOrdinal("iCodBase"));
                            registro.IdPerfil = dr.GetInt32(dr.GetOrdinal("iCodPerfil"));
                            registro.IdDependencia = dr.GetInt32(dr.GetOrdinal("iCodDependencia"));
                            registro.NroAIRHSP = dr.GetString(dr.GetOrdinal("vAirhsp"));
                            registro.Meta = dr.GetString(dr.GetOrdinal("vMeta"));
                            registro.Estado = dr.GetInt32(dr.GetOrdinal("bEstado"));
                            registro.NombreCargo = dr.GetString(dr.GetOrdinal("vNombrePuesto"));
                            registro.NroConvocatoria = dr.GetString(dr.GetOrdinal("vNroCAS"));
                            registro.CantidadVacantes = dr.GetInt32(dr.GetOrdinal("iCantPersonalRequerido"));
                            registro.Dependencia = dr.GetString(dr.GetOrdinal("vDependencia"));
                            registro.Remuneracion = dr.GetDecimal(dr.GetOrdinal("fRemuneracion"));

                            registro.IdTieneRequerimiento = dr.GetInt32(dr.GetOrdinal("TieneDocRequerimiento"));
                            registro.IdTieneCertificacion = dr.GetInt32(dr.GetOrdinal("TieneDocCertificacion"));
                            registro.IdTieneComite = dr.GetInt32(dr.GetOrdinal("TieneDocComite"));
                            registro.IdTieneActaCurri = dr.GetInt32(dr.GetOrdinal("TieneActaCurri"));

                            registro.IdTieneExamenConoc = dr.GetInt32(dr.GetOrdinal("bTieneExamenConoc"));
                            registro.IdTieneExamenPsico = dr.GetInt32(dr.GetOrdinal("bTieneExamenPsico"));
                            registro.IdTipo = dr.GetInt32(dr.GetOrdinal("iTipoConvocatoria"));
                            //if (!dr.IsDBNull(dr.GetOrdinal("dtInicioLabores"))) registro.FechaInicio = dr.GetDateTime(dr.GetOrdinal("dtInicioLabores"));
                            //if (!dr.IsDBNull(dr.GetOrdinal("dtFinLabores"))) registro.FechaCese = dr.GetDateTime(dr.GetOrdinal("dtFinLabores"));

                        }
                    }
                }
            }

            return registro;
        }

        public Convocatoria_Registro ObtenerConvocatoriaDocumento(Convocatoria_Request registro)
        {
            Convocatoria_Registro item = new Convocatoria_Registro();

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_Convocatoria_doc_sel]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Convocatoria_Registro();
                            item.Grilla = new Grilla_Response();

                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoria"));

                            if (!Convert.IsDBNull(dr["fRequerimiento"])) { item.fileRequerimiento = (byte[])(dr["fRequerimiento"]); }
                            if (!Convert.IsDBNull(dr["fCertificacion"])) { item.fileCertificacion = (byte[])(dr["fCertificacion"]); }
                            if (!Convert.IsDBNull(dr["fComite"])) { item.fileComite = (byte[])(dr["fComite"]); }
                            if (!Convert.IsDBNull(dr["fActaCurri"])) { item.fileActaCurri = (byte[])(dr["fActaCurri"]); }
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return item;
        }
        public Convocatoria_Registro ObtenerConvocatoriaServirDocumento(Convocatoria_Request registro)
        {
            Convocatoria_Registro item = new Convocatoria_Registro();

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_ConvocatoriaServir_doc_sel]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Convocatoria_Registro();
                            item.Grilla = new Grilla_Response();

                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoria"));

                            if (!Convert.IsDBNull(dr["fRequerimiento"])) { item.fileRequerimiento = (byte[])(dr["fRequerimiento"]); }
                            if (!Convert.IsDBNull(dr["fCertificacion"])) { item.fileCertificacion = (byte[])(dr["fCertificacion"]); }
                            if (!Convert.IsDBNull(dr["fComite"])) { item.fileComite = (byte[])(dr["fComite"]); }
                            if (!Convert.IsDBNull(dr["fActaCurri"])) { item.fileActaCurri = (byte[])(dr["fActaCurri"]); }
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return item;
        }
        public Convocatoria_Registro ObtenerConvocatoriaPracticaDocumento(Convocatoria_Request registro)
        {
            Convocatoria_Registro item = new Convocatoria_Registro();

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_ConvocatoriaPractica_doc_sel]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Convocatoria_Registro();
                            item.Grilla = new Grilla_Response();

                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoriaPrac"));

                            if (!Convert.IsDBNull(dr["fRequerimiento"])) { item.fileRequerimiento = (byte[])(dr["fRequerimiento"]); }
                            if (!Convert.IsDBNull(dr["fCertificacion"])) { item.fileCertificacion = (byte[])(dr["fCertificacion"]); }
                            if (!Convert.IsDBNull(dr["fComite"])) { item.fileComite = (byte[])(dr["fComite"]); }
                            if (!Convert.IsDBNull(dr["fActaCurri"])) { item.fileActaCurri = (byte[])(dr["fActaCurri"]); }
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return item;
        }
        public Convocatoria_Registro ObtenerConvocatoriaDocumentoPorId(Convocatoria_Request registro)
        {
            Convocatoria_Registro item = new Convocatoria_Registro();

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_Convocatoriadocumento_sel]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                //cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));
                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaDocumento", registro.IdConvocatoriaDocumento));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Convocatoria_Registro();
                            item.Grilla = new Grilla_Response();

                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoria"));

                            if (!Convert.IsDBNull(dr["fArchivo"])) { item.fileComunicado = (byte[])(dr["fArchivo"]); }
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return item;
        }
        public Convocatoria_Registro ObtenerConvocatoriaServirDocumentoPorId(Convocatoria_Request registro)
        {
            Convocatoria_Registro item = new Convocatoria_Registro();

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_ConvocatoriaServirdocumento_sel]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                //cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));
                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaDocumento", registro.IdConvocatoriaDocumento));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Convocatoria_Registro();
                            item.Grilla = new Grilla_Response();

                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoria"));

                            if (!Convert.IsDBNull(dr["fArchivo"])) { item.fileComunicado = (byte[])(dr["fArchivo"]); }
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return item;
        }
        public Convocatoria_Registro ObtenerConvocatoriaPracticaDocumentoPorId(Convocatoria_Request registro)
        {
            Convocatoria_Registro item = new Convocatoria_Registro();

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_ConvocatoriaPracticaDocumento_sel]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                //cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));
                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaDocumento", registro.IdConvocatoriaDocumento));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Convocatoria_Registro();
                            item.Grilla = new Grilla_Response();

                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoriaPrac"));

                            if (!Convert.IsDBNull(dr["fArchivo"])) { item.fileComunicado = (byte[])(dr["fArchivo"]); }
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return item;
        }

        public Convocatoria_Registro ObtenerConvocatoriaDocumentoEntrevista(Convocatoria_Request registro)
        {
            Convocatoria_Registro item = new Convocatoria_Registro();

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_Convocatoriadocumentoentrevista_sel]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                //cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));
                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaDocumento", registro.IdConvocatoriaDocumento));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Convocatoria_Registro();
                            item.Grilla = new Grilla_Response();

                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoria"));

                            if (!Convert.IsDBNull(dr["fActa"])) { item.fileEntrevista = (byte[])(dr["fActa"]); }
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return item;
        }
        public Convocatoria_Registro ObtenerConvocatoriaPracticaDocumentoEntrevista(Convocatoria_Request registro)
        {
            Convocatoria_Registro item = new Convocatoria_Registro();

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_ConvocatoriaPracticaDocumentoEntrevista_sel]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                //cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));
                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoriaDocumento", registro.IdConvocatoriaDocumento));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Convocatoria_Registro();
                            item.Grilla = new Grilla_Response();

                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoriaPrac"));

                            if (!Convert.IsDBNull(dr["fActa"])) { item.fileEntrevista = (byte[])(dr["fActa"]); }
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return item;
        }
        public IEnumerable<Convocatoria_Historica> ListarConvocatoria_Historica(Convocatoria_Request peticion)
        {
            List<Convocatoria_Historica> lista = new List<Convocatoria_Historica>();
            Convocatoria_Historica item = null;
            //BaseSqlOfWorkOld
            using (SqlCommand cmd = _iBaseSqlOfWorkOld.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paRH_Convocatoria_ConsultaHistorica]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;
                cmd.Parameters.Add(new SqlParameter("@pAnio", peticion.Anio));
                cmd.Parameters.Add(new SqlParameter("@pMes", peticion.Mes));
                //cmd.Parameters.Add(new SqlParameter("@tipo", peticion.IdTipo));
                //cmd.Parameters.Add(new SqlParameter("@nombre", peticion.NroCAS));
                //cmd.Parameters.Add(new SqlParameter("@cargo", peticion.NombreCargo));
                //cmd.Parameters.Add(new SqlParameter("@estado", peticion.Estado));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Convocatoria_Historica();

                            item.Grilla = new Grilla_Response();
                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("IdConvocatoria"));
                            item.IdDependencia = dr.GetInt32(dr.GetOrdinal("IdDependencia"));
                            item.CantidadVacantes = dr.GetInt32(dr.GetOrdinal("CantidadVacantes"));
                            item.NroConvocatoria = dr.GetString(dr.GetOrdinal("NroConvocatoria"));
                            item.Dependencia = dr.GetString(dr.GetOrdinal("Dependencia"));
                            item.NombreCargo = dr.GetString(dr.GetOrdinal("vCargo"));
                            item.Bases = dr.GetString(dr.GetOrdinal("vbases"));
                            if (!dr.IsDBNull(dr.GetOrdinal("FechaPublicacion"))) item.FechaPublicacion = dr.GetDateTime(dr.GetOrdinal("FechaPublicacion")).ToString("dd/MM/yyyy");
                            //item.IdTipo = dr.GetInt32(dr.GetOrdinal("iTipoConvocatoria"));

                            //if (!dr.IsDBNull(dr.GetOrdinal("dFechaDesdePubMIDIS"))) item.FechaPublicacionDesde = dr.GetDateTime(dr.GetOrdinal("dFechaDesdePubMIDIS")).ToString("dd/MM/yyyy");
                            //if (!dr.IsDBNull(dr.GetOrdinal("dFechaHastaPubMIDIS"))) item.FechaPublicacionHasta = dr.GetDateTime(dr.GetOrdinal("dFechaHastaPubMIDIS")).ToString("dd/MM/yyyy");
                            //if (!dr.IsDBNull(dr.GetOrdinal("dFechaRegCVPostulante"))) item.FechaPostulacion = dr.GetDateTime(dr.GetOrdinal("dFechaRegCVPostulante")).ToString("dd/MM/yyyy");
                            //if (!dr.IsDBNull(dr.GetOrdinal("dFechaDesdeEvaCV"))) item.FechaCurricularDesde = dr.GetDateTime(dr.GetOrdinal("dFechaDesdeEvaCV")).ToString("dd/MM/yyyy");
                            //if (!dr.IsDBNull(dr.GetOrdinal("dFechaHastaEvaCV"))) item.FechaCurricularHasta = dr.GetDateTime(dr.GetOrdinal("dFechaHastaEvaCV")).ToString("dd/MM/yyyy");
                            //if (!dr.IsDBNull(dr.GetOrdinal("dFechaExamenConoc"))) item.FechaConocimientos = dr.GetDateTime(dr.GetOrdinal("dFechaExamenConoc")).ToString("dd/MM/yyyy");
                            //if (!dr.IsDBNull(dr.GetOrdinal("dFechaExamenPsico"))) item.FechaPsicologico = dr.GetDateTime(dr.GetOrdinal("dFechaExamenPsico")).ToString("dd/MM/yyyy");
                            //if (!dr.IsDBNull(dr.GetOrdinal("dFechaPubResultadoConoc"))) item.FechaResultadosConoc = dr.GetDateTime(dr.GetOrdinal("dFechaPubResultadoConoc")).ToString("dd/MM/yyyy");
                            //if (!dr.IsDBNull(dr.GetOrdinal("dFechaPubResultadoPsico"))) item.FechaResultadosPsico = dr.GetDateTime(dr.GetOrdinal("dFechaPubResultadoPsico")).ToString("dd/MM/yyyy");

                            //if (!dr.IsDBNull(dr.GetOrdinal("dFechaPubResultadoMIDIS"))) item.FechaResultadosCurri = dr.GetDateTime(dr.GetOrdinal("dFechaPubResultadoMIDIS")).ToString("dd/MM/yyyy");
                            //if (!dr.IsDBNull(dr.GetOrdinal("dFechaDesdeEntrevista"))) item.FechaEntrevistaDesde = dr.GetDateTime(dr.GetOrdinal("dFechaDesdeEntrevista")).ToString("dd/MM/yyyy");
                            //if (!dr.IsDBNull(dr.GetOrdinal("dFechaHastaEntrevista"))) item.FechaEntrevistaHasta = dr.GetDateTime(dr.GetOrdinal("dFechaHastaEntrevista")).ToString("dd/MM/yyyy");
                            //if (!dr.IsDBNull(dr.GetOrdinal("dFechaPubResultadoFinalMIDIS"))) item.FechaResultadoFinal = dr.GetDateTime(dr.GetOrdinal("dFechaPubResultadoFinalMIDIS")).ToString("dd/MM/yyyy");
                            //if (!dr.IsDBNull(dr.GetOrdinal("dFechaDesdeSuscripcionContrato"))) item.FechaContratoDesde = dr.GetDateTime(dr.GetOrdinal("dFechaDesdeSuscripcionContrato")).ToString("dd/MM/yyyy");
                            //if (!dr.IsDBNull(dr.GetOrdinal("dFechaHastaSuscripcionContrato"))) item.FechaContratoHasta = dr.GetDateTime(dr.GetOrdinal("dFechaHastaSuscripcionContrato")).ToString("dd/MM/yyyy");

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBaseSqlOfWorkOld.TieneTransaccionOld()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<ConvocatoriaDocumento_Registro> ListarConvocatoriaDocumento_Historica(Convocatoria_Request peticion)
        {
            List<ConvocatoriaDocumento_Registro> lista = new List<ConvocatoriaDocumento_Registro>();
            ConvocatoriaDocumento_Registro item = null;

            using (SqlCommand cmd = _iBaseSqlOfWorkOld.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paRH_Convocatoria_ListaDocumento_Historica]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;
                cmd.Parameters.Add(new SqlParameter("@pIdConvocatoria", peticion.IdConvocatoria));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new ConvocatoriaDocumento_Registro();

                            //item.Grilla = new Grilla_Response();
                            item.IdConvocatoriaDocumento = dr.GetInt32(dr.GetOrdinal("iCodConvocatoriaDocumento"));
                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoria"));
                            item.IdTipoDocumento = dr.GetInt32(dr.GetOrdinal("iTipoDocumento"));
                            item.NombreDocumento = dr.GetString(dr.GetOrdinal("vNombre"));
                            item.TipoDocumento = dr.GetString(dr.GetOrdinal("TipoPublicacion"));
                            if (!dr.IsDBNull(dr.GetOrdinal("dFecharegistro"))) item.FechaRegistro = dr.GetDateTime(dr.GetOrdinal("dFecharegistro"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBaseSqlOfWorkOld.TieneTransaccionOld()) cmd.Connection.Close();

            }

            return lista;
        }
    }
}
