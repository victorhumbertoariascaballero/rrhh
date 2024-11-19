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
#endregion

namespace MIDIS.SEG.AccesoDatosSQL
{
    public partial class T_genm_contrato_ODA
    {
        BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWork = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnOracle"]].ConnectionString));
        BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWorkM = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnOracleM"]].ConnectionString));
        BaseOracleAdoUnitOfWork _iBasesOracleAdoUnitOfWork = new BaseOracleAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnOracle2"]].ConnectionString));

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

        public IEnumerable<EmpleadoContrato_Registro> ListarContratos(Contrato_Request peticion)
        {
            List<EmpleadoContrato_Registro> lista = new List<EmpleadoContrato_Registro>();
            EmpleadoContrato_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[usp_Contrato_listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_CONTRATO", peticion.IdContrato));
                cmd.Parameters.Add(new SqlParameter("@ID_DEPENDENCIA", peticion.IdDependencia));
                cmd.Parameters.Add(new SqlParameter("@ID_CONDICION", peticion.IdCondicion));
                cmd.Parameters.Add(new SqlParameter("@NRO_DOCUMENTO", peticion.NroDocumento));
                cmd.Parameters.Add(new SqlParameter("@NRO_CONTRATO", peticion.NroContrato));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));
                cmd.Parameters.Add(new SqlParameter("@ESTADO", peticion.Estado));
                
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new EmpleadoContrato_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdContrato = dr.GetInt32(dr.GetOrdinal("iCodContrato"));
                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
                            item.IdPostulacion = dr.GetInt32(dr.GetOrdinal("iCodPostulacion"));
                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoria"));
                            item.NroContrato = dr.GetInt32(dr.GetOrdinal("iNroContrato"));
                            item.Anio = dr.GetString(dr.GetOrdinal("vAnioContrato"));
                            item.NroAIRHSP = dr.GetString(dr.GetOrdinal("vAirhsp"));
                            item.Meta = dr.GetString(dr.GetOrdinal("vMeta"));
                            item.IdTipoLimite = dr.GetInt32(dr.GetOrdinal("iTipoLimite"));
                            item.IdEmpleado = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            item.IdDependencia = dr.GetInt32(dr.GetOrdinal("iCodigoDependencia"));
                            item.NombreOficina = dr.GetString(dr.GetOrdinal("vDependencia"));
                            item.NroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            item.Paterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            item.Materno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            item.NombreCargo = dr.GetString(dr.GetOrdinal("vNombreCargo"));
                            item.Estado = dr.GetInt32(dr.GetOrdinal("bEstado"));
                            item.IdCondicion = dr.GetInt32(dr.GetOrdinal("iCodigoCondicion"));
                            item.CondicionLaboral = dr.GetString(dr.GetOrdinal("CondicionLaboral"));

                            item.Domicilio = dr.GetString(dr.GetOrdinal("vDomicilio"));
                            item.Ubigeo = dr.GetString(dr.GetOrdinal("vUbigeo"));
                            item.RUC = dr.GetString(dr.GetOrdinal("vRUC"));
                            item.NombreProceso = dr.GetString(dr.GetOrdinal("vNombreProceso"));
                            item.IdTipoLimite = dr.GetInt32(dr.GetOrdinal("iTipoLimite"));
                            item.Remuneracion = dr.GetDecimal(dr.GetOrdinal("dRemuneracion"));

                            if (!Convert.IsDBNull(dr["archivo"])) {
                                item.IdTieneArchivo = 1;
                                item.archivo = (byte[])(dr["archivo"]); 
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("dtVigenciaInicio"))) item.FechaInicio = dr.GetDateTime(dr.GetOrdinal("dtVigenciaInicio"));
                            if (!dr.IsDBNull(dr.GetOrdinal("dtVigenciaFin"))) item.FechaCese = dr.GetDateTime(dr.GetOrdinal("dtVigenciaFin"));
                            
                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        
        #endregion

        public Int32 RegistrarContrato(EmpleadoContrato_Registro registro, PostulanteInformacion_Registro postulante)
        {
            String CodDep = String.Empty;
            String CodProv = String.Empty;
            String CodDist = String.Empty;
            if (!String.IsNullOrEmpty(registro.Ubigeo))
            {
                if (registro.Ubigeo.Length == 6)
                {
                    CodDep = registro.Ubigeo.Substring(0, 2);
                    CodProv = registro.Ubigeo.Substring(2, 2);
                    CodDist = registro.Ubigeo.Substring(4, 2);
                }
            }

            try
            {
                _iBasesSqlAdoUnitOfWorkM.IniciarTransaccion(IsolationLevel.ReadCommitted);

                using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
                {
                    if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                    cmd.CommandText = "[dbo].[paIngresaTrabajador]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.Add(new SqlParameter("@vNombres", registro.Nombre));
                    cmd.Parameters.Add(new SqlParameter("@vApellidoPaterno", registro.Paterno));
                    cmd.Parameters.Add(new SqlParameter("@vApellidoMaterno", registro.Materno));
                    cmd.Parameters.Add(new SqlParameter("@iCodigoDependencia", registro.IdDependencia));
                    cmd.Parameters.Add(new SqlParameter("@vDepartamento", CodDep));
                    cmd.Parameters.Add(new SqlParameter("@vProvincia", CodProv));
                    cmd.Parameters.Add(new SqlParameter("@vDistrito", CodDist));
                    cmd.Parameters.Add(new SqlParameter("@iCodigoTipoDocumento", registro.TipoDocumento));
                    cmd.Parameters.Add(new SqlParameter("@vNumeroDocumento", registro.NroDocumento));
                    cmd.Parameters.Add(new SqlParameter("@vDomicilio", registro.Domicilio.ToUpper()));
                    cmd.Parameters.Add(new SqlParameter("@vTelefono", String.Empty)); //registro.Telefono));
                    cmd.Parameters.Add(new SqlParameter("@vCelular", String.Empty)); //registro.Celular));
                    cmd.Parameters.Add(new SqlParameter("@fecha", registro.FechaNacimiento));
                    cmd.Parameters.Add(new SqlParameter("@vRUC", registro.RUC));
                    cmd.Parameters.Add(new SqlParameter("@vCorreoElectronico", String.Empty)); //registro.CorreoElectronico));
                    cmd.Parameters.Add(new SqlParameter("@bEstado", registro.Estado));
                    cmd.Parameters.Add(new SqlParameter("@vUsuario", registro.IdUsuarioRegistro));
                    cmd.Parameters.Add(new SqlParameter("@iGenero", registro.IdGenero));
                    cmd.Parameters.Add(new SqlParameter("@iCodigoCargo", 0));
                    cmd.Parameters.Add(new SqlParameter("@iDirector", 0));
                    cmd.Parameters.Add(new SqlParameter("@iCondicionTrabajador", registro.IdCondicion));
                    cmd.Parameters.Add(new SqlParameter("@iCodigoSede", registro.IdSede));
                    cmd.Parameters.Add(new SqlParameter("@iGrupoSanguineo", registro.IdGrupoSanguineo));
                    cmd.Parameters.Add(new SqlParameter("@iPais", 0));
                    cmd.Parameters.Add(new SqlParameter("@vCodDep_Nac", "00"));
                    cmd.Parameters.Add(new SqlParameter("@vCodProv_Nac", "00"));
                    cmd.Parameters.Add(new SqlParameter("@vCodDist_Nac", "00"));
                    cmd.Parameters.Add(new SqlParameter("@vCargo", registro.NombreCargo));
                    cmd.Parameters.Add(new SqlParameter("@vFoto", registro.Foto));
                    cmd.Parameters.Add(new SqlParameter("@vTelefonoInstitucional", String.Empty)); //registro.TelefonoLaboral));
                    cmd.Parameters.Add(new SqlParameter("@vAnexoInstitucional", String.Empty)); //registro.AnexoLaboral));
                    cmd.Parameters.Add(new SqlParameter("@vCelularInstitucional", String.Empty)); //registro.CelularLaboral));
                    cmd.Parameters.Add(new SqlParameter("@vCorreoInstitucional", String.Empty)); //registro.CorreoElectronicoLaboral));
                    cmd.Parameters.Add(new SqlParameter("@iCodigoEstadoCivil", registro.IdEstadoCivil));
                    cmd.Parameters.Add(new SqlParameter("@vUbigeo", registro.DescripcionUbigeo));

                    if (registro.FechaInicio.HasValue) cmd.Parameters.Add(new SqlParameter("@dtInicioLabores", registro.FechaInicio.Value));
                    //if (registro.FechaCese.HasValue) cmd.Parameters.Add(new SqlParameter("@dtFinLabores", registro.FechaCese.Value));
                    cmd.Parameters.Add(new SqlParameter("@iTipoPension", postulante.IdAFPAfiliada));
                    cmd.Parameters.Add(new SqlParameter("@iTipoAfp", postulante.IdTipoAFP));
                    cmd.Parameters.Add(new SqlParameter("@vCodigoAfp", postulante.CodigoAFP));
                    cmd.Parameters.Add(new SqlParameter("@iTipoComisionAFP", postulante.IdTipoComisionAFP));
                    
                    SqlParameter IdPropuestaParameter = new SqlParameter("@resultado", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(IdPropuestaParameter);

                    cmd.ExecuteNonQuery();
                    registro.IdEmpleado = Int32.Parse(IdPropuestaParameter.Value.ToString());


                    cmd.Parameters.Clear();
                    cmd.CommandText = "[dbo].[paActualizaTrabajadorPostulacion]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", registro.IdEmpleado));
                    //cmd.Parameters.Add(new SqlParameter("@vAirhsp", registro.NroAIRHSP));
                    //cmd.Parameters.Add(new SqlParameter("@vMeta", registro.Meta));
                    //cmd.Parameters.Add(new SqlParameter("@dRemuneracion", registro.Remuneracion));
                    cmd.Parameters.Add(new SqlParameter("@vTelefono", postulante.Telefono));
                    cmd.Parameters.Add(new SqlParameter("@vCelular", postulante.Celular));
                    cmd.Parameters.Add(new SqlParameter("@vCorreoElectronico", postulante.CorreoElectronico));
                    cmd.Parameters.Add(new SqlParameter("@iTienePension", postulante.IdEstaAfiliadoPensiones));
                    cmd.Parameters.Add(new SqlParameter("@iTipoPension", postulante.IdAFPAfiliada));
                    cmd.Parameters.Add(new SqlParameter("@iTipoAfp", postulante.IdTipoAFP));
                    cmd.Parameters.Add(new SqlParameter("@vCodigoAfp", postulante.CodigoAFP));
                    cmd.Parameters.Add(new SqlParameter("@iTipoComisionAfp", postulante.IdTipoComisionAFP));
                    cmd.Parameters.Add(new SqlParameter("@iNuevoTipoPension", postulante.IdTipoPensionDeseaAfiliar));
                    cmd.Parameters.Add(new SqlParameter("@iTieneBanco", postulante.IdEstaAfiliadoBanco));
                    cmd.Parameters.Add(new SqlParameter("@iTipoBanco", postulante.IdBancoAfiliado)); //registro.Telefono));
                    cmd.Parameters.Add(new SqlParameter("@vCuentaBanco", postulante.CuentaBancoAfiliado)); //registro.Celular));
                    cmd.Parameters.Add(new SqlParameter("@vCuentaBancoCCI", postulante.CuentaBancoCCIAfiliado));
                    cmd.Parameters.Add(new SqlParameter("@iNuevoTipoBanco", postulante.IdBancoDeseaAfiliar));

                    cmd.ExecuteNonQuery();

                    //if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
                }

                _iBasesSqlAdoUnitOfWork.IniciarTransaccion(IsolationLevel.ReadCommitted);

                using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
                {
                    if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                    cmd.CommandText = "[dbo].[usp_Contrato_ins]";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@iCodEmpleado", registro.IdEmpleado));
                    cmd.Parameters.Add(new SqlParameter("@iCodPostulante", registro.IdPostulante));
                    cmd.Parameters.Add(new SqlParameter("@iCodPostulacion", registro.IdPostulacion));
                    cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));
                    cmd.Parameters.Add(new SqlParameter("@vAnioContrato", registro.FechaInicio.Value.Year.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@vNombres", registro.Nombre));
                    cmd.Parameters.Add(new SqlParameter("@vApellidoPaterno", registro.Paterno));
                    cmd.Parameters.Add(new SqlParameter("@vApellidoMaterno", registro.Materno));
                    cmd.Parameters.Add(new SqlParameter("@vFoto", registro.Foto));
                    cmd.Parameters.Add(new SqlParameter("@vCodigoDepartamento", CodDep));
                    cmd.Parameters.Add(new SqlParameter("@vCodigoProvincia", CodProv));
                    cmd.Parameters.Add(new SqlParameter("@vCodigoDistrito", CodDist));
                    cmd.Parameters.Add(new SqlParameter("@iCodigoTipoDocumento", registro.TipoDocumento));
                    cmd.Parameters.Add(new SqlParameter("@vNumeroDocumento", registro.NroDocumento));
                    cmd.Parameters.Add(new SqlParameter("@vDomicilio", registro.Domicilio.ToUpper()));
                    cmd.Parameters.Add(new SqlParameter("@vRUC", registro.RUC));
                    cmd.Parameters.Add(new SqlParameter("@bEstado", registro.Estado));
                    cmd.Parameters.Add(new SqlParameter("@vUbigeo", registro.DescripcionUbigeo));
                    cmd.Parameters.Add(new SqlParameter("@vPlanilla", registro.Planilla));
                    cmd.Parameters.Add(new SqlParameter("@vAirhsp", registro.NroAIRHSP));
                    cmd.Parameters.Add(new SqlParameter("@vMeta", registro.Meta));
                    cmd.Parameters.Add(new SqlParameter("@iUnidadOrganica", registro.IdDependencia));
                    cmd.Parameters.Add(new SqlParameter("@vNombreCargo", registro.NombreCargo));
                    cmd.Parameters.Add(new SqlParameter("@vNombreUnidadOrganica", registro.NombreUnidadOrganica));
                    cmd.Parameters.Add(new SqlParameter("@dRemuneracion", registro.Remuneracion));
                    cmd.Parameters.Add(new SqlParameter("@vNombreProceso", registro.NombreProceso));
                    cmd.Parameters.Add(new SqlParameter("@iTipoLimite", registro.IdTipoLimite));
                    cmd.Parameters.Add(new SqlParameter("@dtVigenciaInicio", registro.FechaInicio.Value));
                    if (registro.IdTipoLimite == 1)
                        cmd.Parameters.Add(new SqlParameter("@dtVigenciaFin", registro.FechaCese.Value));

                    cmd.Parameters.Add(new SqlParameter("@dtAuditCreacion", registro.FechaRegistro));
                    cmd.Parameters.Add(new SqlParameter("@vAuditCreacion", registro.IdUsuarioRegistro));
                    
                    SqlParameter IdPropuestaParameter1 = new SqlParameter("@iCodContrato", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(IdPropuestaParameter1);
                    SqlParameter IdPropuestaParameter2 = new SqlParameter("@iNroContrato", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(IdPropuestaParameter2);

                    cmd.ExecuteNonQuery();
                    registro.IdContrato = Int32.Parse(IdPropuestaParameter1.Value.ToString());

                    //if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
                }

                if (_iBasesSqlAdoUnitOfWork.TieneTransaccion()) _iBasesSqlAdoUnitOfWork.ConfirmarTransaccion();
                if (_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) _iBasesSqlAdoUnitOfWorkM.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                if (_iBasesSqlAdoUnitOfWork.TieneTransaccion()) _iBasesSqlAdoUnitOfWork.RetrocederTransaccion();
                if (_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) _iBasesSqlAdoUnitOfWorkM.RetrocederTransaccion();

                throw;
            }
            
            return registro.IdContrato;
        }

        public Int32 RegistrarContratoArchivo(EmpleadoContrato_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_Contrato_file_ins]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_CONTRATO", registro.IdContrato));
                if (registro.archivo != null) cmd.Parameters.Add(new SqlParameter("@ARCHIVO", registro.archivo));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE_ARCHIVO", registro.NombreArchivo));
                cmd.Parameters.Add(new SqlParameter("@USUARIO_MODIFICACION", registro.IdUsuarioModificacion));
                cmd.Parameters.Add(new SqlParameter("@FECHA_MODIFICACION", registro.FechaModificacion));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdContrato;
        }

        public Int32 ActualizarContratoNominaTrabajador(EmpleadoContrato_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_Contrato_Trabajador_upd]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_CONTRATO", registro.IdContrato));
                cmd.Parameters.Add(new SqlParameter("@NroAIRHSP", registro.NroAIRHSP));
                cmd.Parameters.Add(new SqlParameter("@USUARIO_MODIFICACION", registro.IdUsuarioModificacion));
                cmd.Parameters.Add(new SqlParameter("@FECHA_MODIFICACION", registro.FechaModificacion));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdContrato;
        }


    }
}
