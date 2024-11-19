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
    public partial class T_genm_postulante_ODA
    {
        BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWork = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnOracle"]].ConnectionString));
        BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWorkM = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnOracleM"]].ConnectionString));
        
        #region Metodos Generales

        string vSqlString = new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnOracleM"]].ConnectionString);

        public IEnumerable<PostulanteInformacion_Registro> ListarPostulantes(Postulante_Request peticion)
        {
            List<PostulanteInformacion_Registro> lista = new List<PostulanteInformacion_Registro>();
            PostulanteInformacion_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[usp_Postulante_listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_POSTULANTE", peticion.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@NRO_DOCUMENTO", peticion.NroDocumento));
                cmd.Parameters.Add(new SqlParameter("@PROCESO", peticion.NombreProceso));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));
                cmd.Parameters.Add(new SqlParameter("@ESTADO", peticion.Estado));
                cmd.Parameters.Add(new SqlParameter("@TIPO", peticion.Tipo));
                
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulanteInformacion_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
                            item.IdPostulacion = dr.GetInt32(dr.GetOrdinal("iCodPostulacion"));
                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoria"));
                            item.NroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            item.Paterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            item.Materno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            item.Foto = dr.GetString(dr.GetOrdinal("vFoto"));
                            item.NombreUnidadOrganica = dr.GetString(dr.GetOrdinal("vNombreUnidadOrganica"));
                            item.NombreCargo = dr.GetString(dr.GetOrdinal("nombreCargo"));
                            item.NombreProceso = dr.GetString(dr.GetOrdinal("titulo"));
                            item.Estado = dr.GetInt32(dr.GetOrdinal("bEstado"));
                            item.FechaMaximaContrato = dr.GetString(dr.GetOrdinal("vFechaLimite"));
                            
                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<PostulanteInformacion_Registro> ListarPostulantesGanadores(Postulante_Request peticion)
        {
            List<PostulanteInformacion_Registro> lista = new List<PostulanteInformacion_Registro>();
            PostulanteInformacion_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[usp_Postulante_listarganadores]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_POSTULANTE", peticion.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@NRO_DOCUMENTO", peticion.NroDocumento));
                cmd.Parameters.Add(new SqlParameter("@PROCESO", peticion.NombreProceso));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));
                cmd.Parameters.Add(new SqlParameter("@ESTADO", peticion.Estado));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulanteInformacion_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
                            item.IdPostulacion = dr.GetInt32(dr.GetOrdinal("iCodPostulacion"));
                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoria"));
                            item.NroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            item.Paterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            item.Materno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            item.Foto = dr.GetString(dr.GetOrdinal("vFoto"));
                            item.NombreUnidadOrganica = dr.GetString(dr.GetOrdinal("vNombreUnidadOrganica"));
                            item.NombreCargo = dr.GetString(dr.GetOrdinal("nombreCargo"));
                            item.NombreProceso = dr.GetString(dr.GetOrdinal("titulo"));
                            item.Estado = dr.GetInt32(dr.GetOrdinal("bEstado"));
                            
                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<Empleado_Registro> ValidarPostulante(String strDocumento, String strTipoDocumento, String strVariable)
        {
            List<Empleado_Registro> lista = new List<Empleado_Registro>();
            Empleado_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[usp_ValidarPostulante]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@vNumeroDocumento", strDocumento));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoDocumento", strTipoDocumento));
                cmd.Parameters.Add(new SqlParameter("@vVariable", strVariable));
                
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Empleado_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdEmpleado = dr.GetInt32(dr.GetOrdinal("icodigo"));
                            item.NroDocumento = dr.GetString(dr.GetOrdinal("vnumerodocumento"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("vnombres"));
                            item.Paterno = dr.GetString(dr.GetOrdinal("vapellidopat"));
                            item.Materno = dr.GetString(dr.GetOrdinal("vapellidomat"));
                            
                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<PostulanteFicha_Registro> ListarPostulantesFicha(Postulante_Request peticion)
        {
            List<Empleado_Registro> lista = new List<Empleado_Registro>();
            Empleado_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paListaTrabajador]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_TRABAJADOR", peticion.IdPostulante));
                //cmd.Parameters.Add(new SqlParameter("@ID_DEPENDENCIA", peticion.IdDependencia));
                //cmd.Parameters.Add(new SqlParameter("@ID_CONDICION", peticion.IdCondicion));
                //cmd.Parameters.Add(new SqlParameter("@ID_SEDE", peticion.IdSede));
                //cmd.Parameters.Add(new SqlParameter("@NRO_DOCUMENTO", peticion.NroDocumento));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));
                cmd.Parameters.Add(new SqlParameter("@ESTADO", peticion.Estado));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Empleado_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdEmpleado = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            item.IdDependencia = dr.GetInt32(dr.GetOrdinal("iCodigoDependencia"));
                            item.NroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            item.Paterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            item.Materno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            item.CorreoElectronicoLaboral = dr.GetString(dr.GetOrdinal("vCorreoElectronico"));
                            item.NombreOficina = dr.GetString(dr.GetOrdinal("vDependencia"));
                            item.Sigla = dr.GetString(dr.GetOrdinal("vSiglas"));
                            item.NombreCargo = dr.GetString(dr.GetOrdinal("vDescripcion"));
                            item.Estado = dr.GetInt32(dr.GetOrdinal("bEstado"));
                            item.IdGenero = dr.GetInt32(dr.GetOrdinal("iCodigoGenero"));
                            item.Domicilio = dr.GetString(dr.GetOrdinal("Domicilio"));
                            item.Telefono = dr.GetString(dr.GetOrdinal("Telefono"));
                            item.Celular = dr.GetString(dr.GetOrdinal("Celular"));
                            item.CorreoElectronico = dr.GetString(dr.GetOrdinal("CorreoElectronico"));
                            item.IdCondicion = dr.GetInt32(dr.GetOrdinal("iCodigoCondicion"));
                            item.CondicionLaboral = dr.GetString(dr.GetOrdinal("CondicionLaboral"));
                            item.IdSede = dr.GetInt32(dr.GetOrdinal("iCodigoSede"));
                            item.Sede = dr.GetString(dr.GetOrdinal("Sede"));

                            item.NroOrden = dr.GetString(dr.GetOrdinal("vNroOrden"));
                            item.NombreOrden = dr.GetString(dr.GetOrdinal("vNombreOrden"));
                            item.DuracionOrden = dr.GetInt32(dr.GetOrdinal("iDuracionOrden"));
                            item.MontoOrden = dr.GetDecimal(dr.GetOrdinal("dMontoOrden"));
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaInicioOrden"))) item.InicioOrden = dr.GetDateTime(dr.GetOrdinal("dFechaInicioOrden")).ToString("dd/MM/yyyy");
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaFinOrden"))) item.FinOrden = dr.GetDateTime(dr.GetOrdinal("dFechaFinOrden")).ToString("dd/MM/yyyy");

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return null; // lista;
        }
        public PostulanteInformacion_Registro ObtenerInformacionPostulante(Postulante_Request peticion)
        {
            PostulanteInformacion_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_Postulante_Informacion_sel]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ID_POSTULANTE", peticion.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@ID_CONVOCATORIA", peticion.IdConvocatoria));
                
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulanteInformacion_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("icoddatos"));
                            item.IdPostulacion = dr.GetInt32(dr.GetOrdinal("icodpost"));
                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("icodconv"));

                            item.TipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodTipoDocumentoIdent"));
                            item.NroDocumento = dr.GetString(dr.GetOrdinal("vdni"));
                            item.RUC = dr.GetString(dr.GetOrdinal("vruc"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("vnombres"));
                            item.Paterno = dr.GetString(dr.GetOrdinal("vapellidopat"));
                            item.Materno = dr.GetString(dr.GetOrdinal("vapellidomat"));
                            item.Sexo = dr.GetString(dr.GetOrdinal("csexo"));
                            item.Telefono = dr.GetString(dr.GetOrdinal("vtelefono"));
                            item.Celular = dr.GetString(dr.GetOrdinal("vcelular"));
                            item.CorreoElectronico = dr.GetString(dr.GetOrdinal("vcorreo"));
                            item.IdEstadoCivil = dr.GetInt32(dr.GetOrdinal("icodcivil"));
                            item.Ubigeo = dr.GetString(dr.GetOrdinal("vdireccion"));
                            item.CodigoProceso = dr.GetString(dr.GetOrdinal("vcodigo"));
                            item.NombreCargo = dr.GetString(dr.GetOrdinal("vcargo"));
                            item.IdUnidadOrganica = dr.GetInt32(dr.GetOrdinal("iCodRefUnidadOrganica"));
                            item.Remuneracion = dr.GetDecimal(dr.GetOrdinal("vRemMensual"));
                            item.Meta = dr.GetString(dr.GetOrdinal("vMeta"));
                            item.NombreProceso = dr.GetString(dr.GetOrdinal("vTitulo"));
                            item.NombreUnidadOrganica = dr.GetString(dr.GetOrdinal("vNombreUnidadOrganica"));
                            item.Nacionalidad = dr.GetString(dr.GetOrdinal("vNacionalidad"));

                            item.IdSituacionAcademicaT = dr.GetInt32(dr.GetOrdinal("IdSituacionAcademicaT"));
                            item.IdSituacionAcademicaU = dr.GetInt32(dr.GetOrdinal("IdSituacionAcademicaU"));
                            item.IdSituacionAcademicaO = dr.GetInt32(dr.GetOrdinal("IdSituacionAcademicaO"));
                            item.IdGradoAcademicoES = dr.GetInt32(dr.GetOrdinal("IdGradoAcademicoES"));
                            item.IdGradoAcademicoBA = dr.GetInt32(dr.GetOrdinal("IdGradoAcademicoBA"));
                            item.IdGradoAcademicoTI = dr.GetInt32(dr.GetOrdinal("IdGradoAcademicoTI"));
                            item.IdPostgradoM = dr.GetInt32(dr.GetOrdinal("IdPostgradoM"));
                            item.IdPostgradoD = dr.GetInt32(dr.GetOrdinal("IdPostgradoD"));
                            item.SituacionAcademicaT = dr.GetString(dr.GetOrdinal("SituacionAcademicaT"));
                            item.SituacionAcademicaU = dr.GetString(dr.GetOrdinal("SituacionAcademicaU"));
                            item.SituacionAcademicaO = dr.GetString(dr.GetOrdinal("SituacionAcademicaO"));
                            item.PostgradoCE = dr.GetString(dr.GetOrdinal("PostgradoCE"));
                            item.PostgradoGrado = dr.GetString(dr.GetOrdinal("PostgradoGrado"));

                            if (!dr.IsDBNull(dr.GetOrdinal("dfechanac"))) item.FechaNacimiento = dr.GetString(dr.GetOrdinal("dfechanac"));
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return item;
        }
        public Postulante_Registro ObtenerPostulante(Postulante_Request peticion)
        {
            Postulante_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_Postulante_sel]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", peticion.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@vNumeroDocumento", peticion.NroDocumento));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Postulante_Registro();
                            item.Grilla = new Grilla_Response();
                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
                            item.TipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            item.NroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            item.RUC = dr.GetString(dr.GetOrdinal("vRUC"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            item.Paterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            item.Materno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            item.Foto = dr.GetString(dr.GetOrdinal("vFoto"));
                            item.Sexo = dr.GetString(dr.GetOrdinal("iCodigoGenero"));
                            item.Nacionalidad = dr.GetString(dr.GetOrdinal("vNacionalidad"));
                            item.Telefono = dr.GetString(dr.GetOrdinal("vTelefono"));
                            item.Celular = dr.GetString(dr.GetOrdinal("vCelular"));
                            item.FechaNacimiento = dr.GetString(dr.GetOrdinal("dtFechaNacimiento"));
                            item.CorreoElectronico = dr.GetString(dr.GetOrdinal("vCorreoElectronico"));
                            item.IdEstadoCivil = dr.GetInt32(dr.GetOrdinal("iCodigoEstadoCivil"));
                            item.Ubigeo = String.Format("{0}{1}{2}", dr.GetString(dr.GetOrdinal("vCodigoDepartamento")), dr.GetString(dr.GetOrdinal("vCodigoProvincia")), dr.GetString(dr.GetOrdinal("vCodigoDistrito")));
                            item.FFAA = dr.GetInt32(dr.GetOrdinal("iFFAA"));
                            item.Discapacidad = dr.GetInt32(dr.GetOrdinal("iDiscapacidad"));
                            item.Deportista = dr.GetInt32(dr.GetOrdinal("iDeportista"));
                            item.Colegio = dr.GetString(dr.GetOrdinal("vColegio"));
                            item.NroColegiatura = dr.GetString(dr.GetOrdinal("vNroColegiatura"));
                            item.Domicilio = dr.GetString(dr.GetOrdinal("vDomicilio"));
                            item.DescripcionUbigeo = dr.GetString(dr.GetOrdinal("vUbigeo"));
                            item.bColigiatura = dr.GetBoolean(dr.GetOrdinal("bColegiatura"));
                            item.bColegiatura_Habilitada = dr.GetBoolean(dr.GetOrdinal("bColegiaturaHabilitado"));
                            item.CarnetConadis = dr.GetString(dr.GetOrdinal("vCarnetConadis"));
                            item.Acreditacion = dr.GetString(dr.GetOrdinal("vDeportistaAcreditado"));
                            item.bTrabajarProvincia = dr.GetBoolean(dr.GetOrdinal("bTrabajarProvincia"));
                            item.Trabajar_Provincia = dr.GetString(dr.GetOrdinal("vTrabajarProvincia"));
                            item.DireccionActual = dr.GetString(dr.GetOrdinal("vDomicilioActual"));
                            item.Referencia = dr.GetString(dr.GetOrdinal("vReferencia"));
                            item.CarnetFuerzasArmadas = dr.GetString(dr.GetOrdinal("vCarnetFuerzasArmadas"));
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return item;
        }
        public Postulante_Registro ObtenerPostulantePersona(Postulante_Request peticion)
        {
            Postulante_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_Persona_sel]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodPersona", peticion.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@vNroDocumento", peticion.NroDocumento));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Postulante_Registro();
                            item.Grilla = new Grilla_Response();
                            //item.IdPersona = dr.GetInt32(dr.GetOrdinal("biIdPersona"));
                            item.TipoDocumento = dr.GetInt32(dr.GetOrdinal("iIdTipDocumento"));
                            item.NroDocumento = dr.GetString(dr.GetOrdinal("vNroDocumento"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            item.Paterno = dr.GetString(dr.GetOrdinal("vApePaterno"));
                            item.Materno = dr.GetString(dr.GetOrdinal("vApeMaterno"));
                            item.Domicilio = dr.GetString(dr.GetOrdinal("vDireccion"));
                            item.Telefono = dr.GetString(dr.GetOrdinal("vTelefono"));
                            item.Celular = dr.GetString(dr.GetOrdinal("vCelular"));
                            item.CorreoElectronico = dr.GetString(dr.GetOrdinal("vCorreoElectronico"));                            
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return item;
        }
        public PostulacionPostulante_Registro ObtenerPostulacionPostulante(Postulante_Request peticion)
        {
            PostulacionPostulante_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulacionPostulante_sel]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulacion", peticion.IdPostulacion));
                
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulacionPostulante_Registro();
                            item.Grilla = new Grilla_Response();
                            item.IdPostulacion = peticion.IdPostulacion;
                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
                            item.TipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            item.NroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            item.RUC = dr.GetString(dr.GetOrdinal("vRUC"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            item.Paterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            item.Materno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            item.Foto = dr.GetString(dr.GetOrdinal("vFoto"));
                            item.Sexo = dr.GetString(dr.GetOrdinal("iCodigoGenero"));
                            item.Nacionalidad = dr.GetString(dr.GetOrdinal("vNacionalidad"));
                            item.Telefono = dr.GetString(dr.GetOrdinal("vTelefono"));
                            item.Celular = dr.GetString(dr.GetOrdinal("vCelular"));
                            item.FechaNacimiento = dr.GetString(dr.GetOrdinal("dtFechaNacimiento"));
                            item.CorreoElectronico = dr.GetString(dr.GetOrdinal("vCorreoElectronico"));
                            item.IdEstadoCivil = dr.GetInt32(dr.GetOrdinal("iCodigoEstadoCivil"));
                            item.Ubigeo = String.Format("{0}{1}{2}", dr.GetString(dr.GetOrdinal("vCodigoDepartamento")), dr.GetString(dr.GetOrdinal("vCodigoProvincia")), dr.GetString(dr.GetOrdinal("vCodigoDistrito")));
                            item.FFAA = dr.GetInt32(dr.GetOrdinal("iFFAA"));
                            item.Discapacidad = dr.GetInt32(dr.GetOrdinal("iDiscapacidad"));
                            item.Deportista = dr.GetInt32(dr.GetOrdinal("iDeportista"));
                            item.Colegio = dr.GetString(dr.GetOrdinal("vColegio"));
                            item.NroColegiatura = dr.GetString(dr.GetOrdinal("vNroColegiatura"));
                            item.Domicilio = dr.GetString(dr.GetOrdinal("vDomicilio"));
                            item.DescripcionUbigeo = dr.GetString(dr.GetOrdinal("vUbigeo"));
                            item.bColigiatura = dr.GetBoolean(dr.GetOrdinal("bColegiatura"));
                            item.bColegiatura_Habilitada = dr.GetBoolean(dr.GetOrdinal("bColegiaturaHabilitado"));
                            item.CarnetConadis = dr.GetString(dr.GetOrdinal("vCarnetConadis"));
                            item.Acreditacion = dr.GetString(dr.GetOrdinal("vDeportistaAcreditado"));
                            item.bTrabajarProvincia = dr.GetBoolean(dr.GetOrdinal("bTrabajarProvincia"));
                            item.Trabajar_Provincia = dr.GetString(dr.GetOrdinal("vTrabajarProvincia"));
                            item.DireccionActual = dr.GetString(dr.GetOrdinal("vDomicilioActual"));
                            item.Referencia = dr.GetString(dr.GetOrdinal("vReferencia"));
                            item.CarnetFuerzasArmadas = dr.GetString(dr.GetOrdinal("vCarnetFuerzasArmadas"));
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return item;
        }
        public PostulacionPostulante_Registro ObtenerPostulacionPostulanteServir(Postulante_Request peticion)
        {
            PostulacionPostulante_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulacionPostulanteServir_sel]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulacion", peticion.IdPostulacion));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulacionPostulante_Registro();
                            item.Grilla = new Grilla_Response();
                            item.IdPostulacion = peticion.IdPostulacion;
                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
                            item.TipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            item.NroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            item.RUC = dr.GetString(dr.GetOrdinal("vRUC"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            item.Paterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            item.Materno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            item.Foto = dr.GetString(dr.GetOrdinal("vFoto"));
                            item.Sexo = dr.GetString(dr.GetOrdinal("iCodigoGenero"));
                            item.Nacionalidad = dr.GetString(dr.GetOrdinal("vNacionalidad"));
                            item.Telefono = dr.GetString(dr.GetOrdinal("vTelefono"));
                            item.Celular = dr.GetString(dr.GetOrdinal("vCelular"));
                            item.FechaNacimiento = dr.GetString(dr.GetOrdinal("dtFechaNacimiento"));
                            item.CorreoElectronico = dr.GetString(dr.GetOrdinal("vCorreoElectronico"));
                            item.IdEstadoCivil = dr.GetInt32(dr.GetOrdinal("iCodigoEstadoCivil"));
                            item.Ubigeo = String.Format("{0}{1}{2}", dr.GetString(dr.GetOrdinal("vCodigoDepartamento")), dr.GetString(dr.GetOrdinal("vCodigoProvincia")), dr.GetString(dr.GetOrdinal("vCodigoDistrito")));
                            item.FFAA = dr.GetInt32(dr.GetOrdinal("iFFAA"));
                            item.Discapacidad = dr.GetInt32(dr.GetOrdinal("iDiscapacidad"));
                            item.Deportista = dr.GetInt32(dr.GetOrdinal("iDeportista"));
                            item.Colegio = dr.GetString(dr.GetOrdinal("vColegio"));
                            item.NroColegiatura = dr.GetString(dr.GetOrdinal("vNroColegiatura"));
                            item.Domicilio = dr.GetString(dr.GetOrdinal("vDomicilio"));
                            item.DescripcionUbigeo = dr.GetString(dr.GetOrdinal("vUbigeo"));
                            item.bColigiatura = dr.GetBoolean(dr.GetOrdinal("bColegiatura"));
                            item.bColegiatura_Habilitada = dr.GetBoolean(dr.GetOrdinal("bColegiaturaHabilitado"));
                            item.CarnetConadis = dr.GetString(dr.GetOrdinal("vCarnetConadis"));
                            item.Acreditacion = dr.GetString(dr.GetOrdinal("vDeportistaAcreditado"));
                            item.bTrabajarProvincia = dr.GetBoolean(dr.GetOrdinal("bTrabajarProvincia"));
                            item.Trabajar_Provincia = dr.GetString(dr.GetOrdinal("vTrabajarProvincia"));
                            item.DireccionActual = dr.GetString(dr.GetOrdinal("vDomicilioActual"));
                            item.Referencia = dr.GetString(dr.GetOrdinal("vReferencia"));
                            item.CarnetFuerzasArmadas = dr.GetString(dr.GetOrdinal("vCarnetFuerzasArmadas"));
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return item;
        }

        //public PostulanteExperiencia_Registro ObtenerPostulanteExperiencia(Postulante_Request peticion)
        //{
        //    PostulanteExperiencia_Registro item = null;

        //    using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
        //    {
        //        if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
        //        cmd.CommandText = "[dbo].[Usp_Postulante_sel]";
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.Add(new SqlParameter("@iCodPostulante", peticion.IdPostulante));
        //        cmd.Parameters.Add(new SqlParameter("@vNumeroDocumento", peticion.NroDocumento));

        //        using (SqlDataReader dr = cmd.ExecuteReader())
        //        {
        //            if (dr.HasRows)
        //            {
        //                while (dr.Read())
        //                {
        //                    item = new PostulanteExperiencia_Registro();
        //                    item.Grilla = new Grilla_Response();
        //                    item.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
        //                    item.TipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
        //                    item.NroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
        //                    item.RUC = dr.GetString(dr.GetOrdinal("vRUC"));
        //                    item.Nombre = dr.GetString(dr.GetOrdinal("vNombres"));
        //                    item.Paterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
        //                    item.Materno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
        //                    item.Foto = dr.GetString(dr.GetOrdinal("vFoto"));
        //                    item.Sexo = dr.GetString(dr.GetOrdinal("iCodigoGenero"));
        //                    item.Nacionalidad = dr.GetString(dr.GetOrdinal("vNacionalidad"));
        //                    item.Telefono = dr.GetString(dr.GetOrdinal("vTelefono"));
        //                    item.Celular = dr.GetString(dr.GetOrdinal("vCelular"));
        //                    item.FechaNacimiento = dr.GetString(dr.GetOrdinal("dtFechaNacimiento"));
        //                    item.CorreoElectronico = dr.GetString(dr.GetOrdinal("vCorreoElectronico"));
        //                    item.IdEstadoCivil = dr.GetInt32(dr.GetOrdinal("iCodigoEstadoCivil"));
        //                    item.Ubigeo = String.Format("{0}{1}{2}", dr.GetString(dr.GetOrdinal("vCodigoDepartamento")), dr.GetString(dr.GetOrdinal("vCodigoProvincia")), dr.GetString(dr.GetOrdinal("vCodigoDistrito")));
        //                    item.FFAA = dr.GetInt32(dr.GetOrdinal("iFFAA"));
        //                    item.Discapacidad = dr.GetInt32(dr.GetOrdinal("iDiscapacidad"));
        //                    item.Colegio = dr.GetString(dr.GetOrdinal("vColegio"));
        //                    item.NroColegiatura = dr.GetString(dr.GetOrdinal("vNroColegiatura"));
        //                    item.Domicilio = dr.GetString(dr.GetOrdinal("vDomicilio"));
        //                    item.DescripcionUbigeo = dr.GetString(dr.GetOrdinal("vUbigeo"));
        //                }
        //            }
        //        }

        //        if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

        //    }

        //    return item;
        //}
        public IEnumerable<PostulanteFamiliar_Registro> ListarPostulanteFamiliares(Postulante_Request peticion)
        {
            List<PostulanteFamiliar_Registro> lista = new List<PostulanteFamiliar_Registro>();
            PostulanteFamiliar_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulanteFamiliar_sel]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", peticion.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@iCodPostulacion", peticion.IdPostulacion));
                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", peticion.IdConvocatoria));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulanteFamiliar_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
                            item.IdPostulacion = dr.GetInt32(dr.GetOrdinal("iCodPostulacion"));
                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoria"));
                            item.IdFamiliar = dr.GetInt32(dr.GetOrdinal("iCodFamiliar"));
                            item.IdParentesco = dr.GetInt32(dr.GetOrdinal("iParentesco"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            item.NroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            item.Edad = dr.GetInt32(dr.GetOrdinal("iEdad"));
                            item.FechaNacimiento = dr.GetString(dr.GetOrdinal("dtFechaNacimiento"));
                            item.Ocupacion = dr.GetString(dr.GetOrdinal("vOcupacion"));
                            item.IdSexo = dr.GetString(dr.GetOrdinal("vSexo"));
                            item.Sexo = new Estado_Response()
                            {
                                Codigo = (item.IdSexo == "F" ? "0" : "1"),
                                Nombre = item.IdSexo
                            };

                            item.Parentesco = new Estado_Response()
                            {
                                Codigo = item.IdParentesco.ToString(),
                                Nombre = (item.IdParentesco == 1 ? "ESPOSO (A)" 
                                    : (item.IdParentesco == 2 ? "CONVIVIENTE" 
                                    : (item.IdParentesco == 3 ? "HIJO (A)" 
                                    : (item.IdParentesco == 4 ? "PADRE" 
                                    : (item.IdParentesco == 5 ? "MADRE" 
                                    : (item.IdParentesco == 6 ? "HERMANO (A)" : String.Empty))))))
                            };

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<PostulanteDocumento_Registro> ListarPostulanteDocumento(Postulante_Request peticion)
        {
            List<PostulanteDocumento_Registro> lista = new List<PostulanteDocumento_Registro>();
            PostulanteDocumento_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulanteDocumento_listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", peticion.IdPostulante));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulanteDocumento_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
                            item.IdDocumento = dr.GetInt32(dr.GetOrdinal("iCodDocumento"));
                            item.IdTipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodTipoDocumento"));
                            item.NombreTipoDocumento = dr.GetString(dr.GetOrdinal("NombreTipoDocumento"));
                            item.Estado = dr.GetInt32(dr.GetOrdinal("bEstado"));
                            item.IdTieneArchivo = dr.GetInt32(dr.GetOrdinal("tieneArchivo"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<PostulacionDocumento_Registro> ListarPostulacionDocumento(Postulante_Request peticion)
        {
            List<PostulacionDocumento_Registro> lista = new List<PostulacionDocumento_Registro>();
            PostulacionDocumento_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulacionDocumento_listar]";
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
                            item = new PostulacionDocumento_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdPostulacion = dr.GetInt32(dr.GetOrdinal("iCodPostulacion"));
                            item.IdDocumento = dr.GetInt32(dr.GetOrdinal("iCodDocumento"));
                            item.IdTipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodTipoDocumento"));
                            item.NombreTipoDocumento = dr.GetString(dr.GetOrdinal("NombreTipoDocumento"));
                            item.Estado = dr.GetInt32(dr.GetOrdinal("bEstado"));
                            item.IdTieneArchivo = dr.GetInt32(dr.GetOrdinal("tieneArchivo"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<PostulacionDocumento_Registro> ListarPostulacionPracticaDocumento(Postulante_Request peticion)
        {
            List<PostulacionDocumento_Registro> lista = new List<PostulacionDocumento_Registro>();
            PostulacionDocumento_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulacionPracticaDocumento_listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulacion", peticion.IdPostulacion));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulacionDocumento_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdPostulacion = dr.GetInt32(dr.GetOrdinal("iCodPostulacionPrac"));
                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
                            item.Estado = dr.GetInt32(dr.GetOrdinal("bEstado"));
                            
                            item.IdTieneAnexo = dr.GetInt32(dr.GetOrdinal("tieneAnexo"));
                            item.IdTieneHojaVida = dr.GetInt32(dr.GetOrdinal("tieneHojaVida"));
                            item.IdTieneCarta = dr.GetInt32(dr.GetOrdinal("tieneCarta"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<PostulanteEstudio_Registro> ListarPostulanteEstudio(Postulante_Request peticion)
        {
            List<PostulanteEstudio_Registro> lista = new List<PostulanteEstudio_Registro>();
            PostulanteEstudio_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulanteEstudio_listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", peticion.IdPostulante));
                
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulanteEstudio_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
                            item.IdEstudio = dr.GetInt32(dr.GetOrdinal("iCodEstudio"));
                            item.IdNivel = dr.GetInt32(dr.GetOrdinal("iCodNivel"));
                            item.IdNoTieneTitulo = dr.GetInt32(dr.GetOrdinal("iCodSinTitulo"));
                            item.Especialidad = dr.GetString(dr.GetOrdinal("vEspecialidad"));
                            item.Institucion = dr.GetString(dr.GetOrdinal("vInstitucion"));
                            item.IdMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            item.IdAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            item.Ciudad = dr.GetString(dr.GetOrdinal("vCiudad"));
                            item.NombreGrado = dr.GetString(dr.GetOrdinal("NombreGrado"));
                            item.NombreNivel = dr.GetString(dr.GetOrdinal("NombreNivel"));
                            item.Estado = dr.GetInt32(dr.GetOrdinal("bEstado"));
                            item.IdTieneArchivo = dr.GetInt32(dr.GetOrdinal("tieneArchivo"));
                            
                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<PostulacionEstudio_Registro> ListarPostulacionEstudio(Postulante_Request peticion)
        {
            List<PostulacionEstudio_Registro> lista = new List<PostulacionEstudio_Registro>();
            PostulacionEstudio_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulacionEstudio_listar]";
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
                            item = new PostulacionEstudio_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdPostulacionEstudio = dr.GetInt32(dr.GetOrdinal("iCodPostulacionEstudio"));
                            item.IdPostulacion = dr.GetInt32(dr.GetOrdinal("iCodPostulacion"));
                            item.IdEstudio = dr.GetInt32(dr.GetOrdinal("iCodEstudio"));
                            item.IdNivel = dr.GetInt32(dr.GetOrdinal("iCodNivel"));
                            item.IdNoTieneTitulo = dr.GetInt32(dr.GetOrdinal("iCodSinTitulo"));
                            item.Especialidad = dr.GetString(dr.GetOrdinal("vEspecialidad"));
                            item.Institucion = dr.GetString(dr.GetOrdinal("vInstitucion"));
                            item.IdMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            item.IdAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            item.Ciudad = dr.GetString(dr.GetOrdinal("vCiudad"));
                            item.NombreGrado = dr.GetString(dr.GetOrdinal("NombreGrado"));
                            item.NombreNivel = dr.GetString(dr.GetOrdinal("NombreNivel"));
                            item.Estado = dr.GetInt32(dr.GetOrdinal("bEstado"));
                            item.IdTieneArchivo = dr.GetInt32(dr.GetOrdinal("tieneArchivo"));
                            item.IdEstudioPerfil = dr.GetInt32(dr.GetOrdinal("iCodEstudioPerfil"));
                            item.IdEstudioAuditoria = dr.GetInt32(dr.GetOrdinal("iCodEstudioAuditoria"));
                            item.Requisito = new Estado_Response()
                            {
                                Codigo = item.IdEstudioPerfil.ToString(),
                                Nombre = (item.IdEstudioPerfil == 0 ? "NO" : "SI")
                            };
                            item.Auditoria = new Estado_Response()
                            {
                                Codigo = item.IdEstudioAuditoria.ToString(),
                                Nombre = (item.IdEstudioAuditoria == 0 ? "NO" : "SI")
                            };
                            item.IdFechaInicioMes = dr.GetInt32(dr.GetOrdinal("iFechaInicioMes"));
                            item.IdFechaInicioAnio = dr.GetInt32(dr.GetOrdinal("iFechaInicioAnio"));
                            item.IdFechaFinMes = dr.GetInt32(dr.GetOrdinal("iFechaFinMes"));
                            item.IdFechaFinAnio = dr.GetInt32(dr.GetOrdinal("iFechaFinAnio"));
                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        
        public IEnumerable<PerfilFormacionAcademica_Registro> ListarPostulacionRequisitosEstudio(BasesPerfilPuesto_Request peticion)
        {
            List<PerfilFormacionAcademica_Registro> lista = new List<PerfilFormacionAcademica_Registro>();
            PerfilFormacionAcademica_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulacionRequisitosEstudio]";
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

                            item.iSecuencia = dr.GetInt32(dr.GetOrdinal("iSecuencia"));
                            item.strNivel = dr.GetString(dr.GetOrdinal("NivelEducativo"));
                            item.vCompleto = dr.GetString(dr.GetOrdinal("Completo"));
                            item.strGrado = dr.GetString(dr.GetOrdinal("NivelAlcanzado"));
                            item.vColegiatura = dr.GetString(dr.GetOrdinal("Colegiatura"));
                            item.vHabilitado = dr.GetString(dr.GetOrdinal("Habilitado"));
                            item.strCodCarrera = dr.GetString(dr.GetOrdinal("Carrera"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return lista;
        }
        public IEnumerable<PerfilFormacionAcademica_Registro> ListarPostulacionRequisitosCapacitacion(BasesPerfilPuesto_Request peticion)
        {
            List<PerfilFormacionAcademica_Registro> lista = new List<PerfilFormacionAcademica_Registro>();
            PerfilFormacionAcademica_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulacionRequisitosCapacitacion]";
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

                            item.iSecuencia = dr.GetInt32(dr.GetOrdinal("iSecuencia"));
                            item.strNivel = dr.GetString(dr.GetOrdinal("Nivel"));
                            item.strNivel1 = dr.GetString(dr.GetOrdinal("TipoMateria"));
                            item.strGrado = dr.GetString(dr.GetOrdinal("Curso"));
                            item.strCodCarrera = dr.GetString(dr.GetOrdinal("ConDocumento"));
                            
                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return lista;
        }
        
        public IEnumerable<PostulanteCapacitacion_Registro> ListarPostulanteCapacitacion(Postulante_Request peticion)
        {
            List<PostulanteCapacitacion_Registro> lista = new List<PostulanteCapacitacion_Registro>();
            PostulanteCapacitacion_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulanteCapacitacion_listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", peticion.IdPostulante));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulanteCapacitacion_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
                            item.IdCapacitacion = dr.GetInt32(dr.GetOrdinal("iCodCapacitacion"));
                            item.Especialidad = dr.GetString(dr.GetOrdinal("vEspecialidad"));
                            item.Institucion = dr.GetString(dr.GetOrdinal("vInstitucion"));
                            item.Ciudad = dr.GetString(dr.GetOrdinal("vCiudad"));
                            item.Horas = dr.GetInt32(dr.GetOrdinal("iHoras"));
                            item.Estado = dr.GetInt32(dr.GetOrdinal("bEstado"));
                            item.IdTieneArchivo = dr.GetInt32(dr.GetOrdinal("tieneArchivo"));
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaInicio"))) item.FechaInicio = dr.GetDateTime(dr.GetOrdinal("dFechaInicio")).ToString("dd/MM/yyyy");
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaFin"))) item.FechaFin = dr.GetDateTime(dr.GetOrdinal("dFechaFin")).ToString("dd/MM/yyyy");

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<PostulacionCapacitacion_Registro> ListarPostulacionCapacitacion(Postulante_Request peticion)
        {
            List<PostulacionCapacitacion_Registro> lista = new List<PostulacionCapacitacion_Registro>();
            PostulacionCapacitacion_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulacionCapacitacion_listar]";
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
                            item = new PostulacionCapacitacion_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdPostulacionCapacitacion = dr.GetInt32(dr.GetOrdinal("iCodPostulacionCapacitacion"));
                            item.IdPostulacion = dr.GetInt32(dr.GetOrdinal("iCodPostulacion"));
                            item.IdCapacitacion = dr.GetInt32(dr.GetOrdinal("iCodCapacitacion"));
                            item.Especialidad = dr.GetString(dr.GetOrdinal("vEspecialidad"));
                            item.Institucion = dr.GetString(dr.GetOrdinal("vInstitucion"));
                            item.Ciudad = dr.GetString(dr.GetOrdinal("vCiudad"));
                            item.Horas = dr.GetInt32(dr.GetOrdinal("iHoras"));
                            item.Estado = dr.GetInt32(dr.GetOrdinal("bEstado"));
                            item.IdTieneArchivo = dr.GetInt32(dr.GetOrdinal("tieneArchivo"));
                            item.IdCapacitacionPerfil = dr.GetInt32(dr.GetOrdinal("iCodCapacitacionPerfil"));
                            item.IdCapacitacionAuditoria = dr.GetInt32(dr.GetOrdinal("iCodCapacitacionAuditoria"));
                            item.Requisito = new Estado_Response()
                            {
                                Codigo = item.IdCapacitacionPerfil.ToString(),
                                Nombre = (item.IdCapacitacionPerfil == 0 ? "NO" : "SI")
                            };
                            item.Auditoria = new Estado_Response()
                            {
                                Codigo = item.IdCapacitacionAuditoria.ToString(),
                                Nombre = (item.IdCapacitacionAuditoria == 0 ? "NO" : "SI")
                            };
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaInicio"))) item.FechaInicio = dr.GetDateTime(dr.GetOrdinal("dFechaInicio")).ToString("dd/MM/yyyy");
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaFin"))) item.FechaFin = dr.GetDateTime(dr.GetOrdinal("dFechaFin")).ToString("dd/MM/yyyy");
                            item.MateriaDescripcion = dr.GetString(dr.GetOrdinal("vMateriaDescripcion"));
                            item.MateriaOtrosDescripcion = dr.GetString(dr.GetOrdinal("vMateriaOtrosDescripcion"));
                            item.iTipoNivelMateria = dr.GetInt32(dr.GetOrdinal("iCodTipoNivelMateria"));
                            item.bOtroTipoEstudio = dr.GetInt32(dr.GetOrdinal("bOtroTipoEstudio"));
                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public Int32 ActualizarPostulacionEstudio(PostulacionEstudio_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[usp_PostulacionEstudio_upd]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulacionEstudio", registro.IdPostulacionEstudio));
                cmd.Parameters.Add(new SqlParameter("@iTipoActualizacion", registro.IdTipoActualizacion));
                cmd.Parameters.Add(new SqlParameter("@iCodEstudioPerfil", registro.IdEstudioPerfil));
                cmd.Parameters.Add(new SqlParameter("@iCodEstudioAuditoria", registro.IdEstudioAuditoria));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoConvocatoria", registro.IdTipoConvocatoria));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdPostulacionEstudio;
        }

        public Int32 ActualizarPostulacionCapacitacion(PostulacionCapacitacion_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[usp_PostulacionCapacitacion_upd]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulacionCapacitacion", registro.IdPostulacionCapacitacion));
                cmd.Parameters.Add(new SqlParameter("@iTipoActualizacion", registro.IdTipoActualizacion));
                cmd.Parameters.Add(new SqlParameter("@iCodCapacitacionPerfil", registro.IdCapacitacionPerfil));
                cmd.Parameters.Add(new SqlParameter("@iCodCapacitacionAuditoria", registro.IdCapacitacionAuditoria));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoConvocatoria", registro.IdTipoConvocatoria));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdPostulacionCapacitacion;
        }
        public Int32 ActualizarPostulacionExperiencia(PostulacionExperiencia_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[usp_PostulacionExperiencia_upd]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulacionExperiencia", registro.IdPostulacionExperiencia));
                cmd.Parameters.Add(new SqlParameter("@iTipoActualizacion", registro.IdTipoActualizacion));
                cmd.Parameters.Add(new SqlParameter("@iCodExperienciaPerfil", registro.IdExperienciaPerfil));
                cmd.Parameters.Add(new SqlParameter("@iCodExperienciaAuditoria", registro.IdExperienciaAuditoria));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoConvocatoria", registro.IdTipoConvocatoria));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdPostulacionExperiencia;
        }

        public Int32 ActualizarRegistroPostulacion(Postulacion_Registro registro)
        {
            //Postulacion_Registro registro = new Postulacion_Registro();

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[Usp_Postulacion_registro]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulacion", registro.IdPostulacion));
                cmd.Parameters.Add(new SqlParameter("@fAnexo", registro.FileAnexo06));
                cmd.Parameters.Add(new SqlParameter("@fHojaVida", registro.FileHojaVida));

                cmd.ExecuteNonQuery();
            }

            return registro.IdPostulacion;
        }
        public Int32 ActualizarRegistroPostulacionServir(Postulacion_Registro registro)
        {
            //Postulacion_Registro registro = new Postulacion_Registro();

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[Usp_PostulacionServir_registro]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulacion", registro.IdPostulacion));
                cmd.Parameters.Add(new SqlParameter("@fAnexo", registro.FileAnexo06));
                cmd.Parameters.Add(new SqlParameter("@fHojaVida", registro.FileHojaVida));

                cmd.ExecuteNonQuery();
            }

            return registro.IdPostulacion;
        }
        public Int32 RegistrarPostulacionPractica(Postulacion_Registro registro)
        {
            //Postulacion_Registro registro = new Postulacion_Registro();

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[Usp_PostulacionPrac_registro]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", registro.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));
                cmd.Parameters.Add(new SqlParameter("@bEstado", 1));
                cmd.Parameters.Add(new SqlParameter("@fAnexo", registro.FileAnexoPracticas));
                cmd.Parameters.Add(new SqlParameter("@fHojaVida", registro.FileHojaVida));
                cmd.Parameters.Add(new SqlParameter("@fCarta", registro.FileCarta));

                cmd.ExecuteNonQuery();
            }

            return registro.IdPostulacion;
        }
        public IEnumerable<PostulanteExperiencia_Registro> ListarPostulanteExperiencia(Postulante_Request peticion)
        {
            List<PostulanteExperiencia_Registro> lista = new List<PostulanteExperiencia_Registro>();
            PostulanteExperiencia_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulanteExperiencia_listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", peticion.IdPostulante));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulanteExperiencia_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
                            item.IdLaboral = dr.GetInt32(dr.GetOrdinal("iCodLaboral"));
                            item.Empresa = dr.GetString(dr.GetOrdinal("vEmpresa"));
                            item.Cargo = dr.GetString(dr.GetOrdinal("vCargo"));
                            item.Descripcion = dr.GetString(dr.GetOrdinal("vDescripcion"));
                            item.Estado = dr.GetInt32(dr.GetOrdinal("bEstado"));
                            item.IdTieneArchivo = dr.GetInt32(dr.GetOrdinal("tieneArchivo"));
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaInicio"))) item.FechaInicio = dr.GetDateTime(dr.GetOrdinal("dFechaInicio")).ToString("dd/MM/yyyy");
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaFin"))) item.FechaFin = dr.GetDateTime(dr.GetOrdinal("dFechaFin")).ToString("dd/MM/yyyy");

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<PostulacionExperiencia_Registro> ListarPostulacionExperiencia(Postulante_Request peticion)
        {
            List<PostulacionExperiencia_Registro> lista = new List<PostulacionExperiencia_Registro>();
            PostulacionExperiencia_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulacionExperiencia_listar]";
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
                            item = new PostulacionExperiencia_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdPostulacionExperiencia = dr.GetInt32(dr.GetOrdinal("iCodPostulacionExperiencia"));
                            item.IdPostulacion = dr.GetInt32(dr.GetOrdinal("iCodPostulacion"));
                            item.IdLaboral = dr.GetInt32(dr.GetOrdinal("iCodLaboral"));
                            item.Empresa = dr.GetString(dr.GetOrdinal("vEmpresa"));
                            item.Cargo = dr.GetString(dr.GetOrdinal("vCargo"));
                            item.Descripcion = dr.GetString(dr.GetOrdinal("vDescripcion"));
                            item.Estado = dr.GetInt32(dr.GetOrdinal("bEstado"));
                            item.IdTieneArchivo = dr.GetInt32(dr.GetOrdinal("tieneArchivo"));
                            item.IdExperienciaPerfil = dr.GetInt32(dr.GetOrdinal("iCodExperienciaPerfil"));
                            item.IdExperienciaAuditoria = dr.GetInt32(dr.GetOrdinal("iCodExperienciaAuditoria"));
                            item.Requisito = new Estado_Response()
                            {
                                Codigo = item.IdExperienciaPerfil.ToString(),
                                Nombre = (item.IdExperienciaPerfil == 0 ? "GENERAL" : "GENERAL Y ESPECÍFICA")
                            };
                            item.Auditoria = new Estado_Response()
                            {
                                Codigo = item.IdExperienciaAuditoria.ToString(),
                                Nombre = (item.IdExperienciaAuditoria == 0 ? "NO" : "SI")
                            };
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaInicio"))) item.FechaInicio = dr.GetDateTime(dr.GetOrdinal("dFechaInicio")).ToString("dd/MM/yyyy");
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaFin"))) item.FechaFin = dr.GetDateTime(dr.GetOrdinal("dFechaFin")).ToString("dd/MM/yyyy");
                            item.iSector = dr.GetInt32(dr.GetOrdinal("iSector"));
                            item.iRegimen = dr.GetInt32(dr.GetOrdinal("iRegimen"));
                            item.NombreJefeDirecto = dr.GetString(dr.GetOrdinal("vNombreJefeDirecto"));
                            item.PuestoJefeDirecto = dr.GetString(dr.GetOrdinal("vPuestoJefeDirecto"));
                            item.MotivoCambio= dr.GetString(dr.GetOrdinal("vMotivoCambio"));
                            item.Remuneracion = dr.GetDecimal(dr.GetOrdinal("mnRemuneracion"));
                            item.RefLaboralNombre = dr.GetString(dr.GetOrdinal("vRefLaboralNombre"));
                            item.RefLaboralPuesto = dr.GetString(dr.GetOrdinal("vRefLaboralPuesto"));
                            item.RefLaboralTelefono = dr.GetString(dr.GetOrdinal("vRefLaboralTelefono"));
                            item.RefLaboralCorreo = dr.GetString(dr.GetOrdinal("vRefLaboralCorreo"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<PostulantePostulacion_Registro> ListarPostulaciones(Postulante_Request peticion)
        {
            List<PostulantePostulacion_Registro> lista = new List<PostulantePostulacion_Registro>();
            PostulantePostulacion_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[usp_Postulante_ListarPostulaciones]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodDatos", peticion.IdPostulante));
                
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulantePostulacion_Registro();

                            item.Grilla = new Grilla_Response();
                            item.Nro = dr.GetInt32(dr.GetOrdinal("nro"));
                            item.IdPostulacion = dr.GetInt32(dr.GetOrdinal("icodpost"));
                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("icoddatos"));
                            item.IdPostulantePostulacion = dr.GetInt32(dr.GetOrdinal("icoddatospost"));
                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("icodconv"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("vnombre"));
                            item.Paterno = dr.GetString(dr.GetOrdinal("vapellidopat"));
                            item.Materno = dr.GetString(dr.GetOrdinal("vapellidomat"));
                            item.NombreProceso = dr.GetString(dr.GetOrdinal("vtitulo"));
                            item.NombreCargo = dr.GetString(dr.GetOrdinal("vcargo"));
                            item.AptoCurricular = dr.GetInt32(dr.GetOrdinal("iaptocurricular"));
                            item.AptoEvaluacion = dr.GetInt32(dr.GetOrdinal("iaptoevaluacion"));
                            item.AptoResultado = dr.GetInt32(dr.GetOrdinal("iaptoresultado"));
                            item.AptoContrato = dr.GetInt32(dr.GetOrdinal("iaptocontrato"));

                            if (!dr.IsDBNull(dr.GetOrdinal("fechapostulacion"))) item.FechaPostulacion = dr.GetDateTime(dr.GetOrdinal("fechapostulacion")).ToString("dd/MM/yyyy HH:mm");
                            if (!dr.IsDBNull(dr.GetOrdinal("fechalimite_contrato"))) item.FechaMaximaContrato = dr.GetDateTime(dr.GetOrdinal("fechalimite_contrato")).ToString("dd/MM/yyyy HH:mm");

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<PostulantePostulacion_Registro> ListarPostulacionesServir(Postulante_Request peticion)
        {
            List<PostulantePostulacion_Registro> lista = new List<PostulantePostulacion_Registro>();
            PostulantePostulacion_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[usp_Postulante_ListarPostulacionesServir]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodDatos", peticion.IdPostulante));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulantePostulacion_Registro();

                            item.Grilla = new Grilla_Response();
                            item.Nro = dr.GetInt32(dr.GetOrdinal("nro"));
                            item.IdPostulacion = dr.GetInt32(dr.GetOrdinal("icodpost"));
                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("icoddatos"));
                            item.IdPostulantePostulacion = dr.GetInt32(dr.GetOrdinal("icoddatospost"));
                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("icodconv"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("vnombre"));
                            item.Paterno = dr.GetString(dr.GetOrdinal("vapellidopat"));
                            item.Materno = dr.GetString(dr.GetOrdinal("vapellidomat"));
                            item.NombreProceso = dr.GetString(dr.GetOrdinal("vtitulo"));
                            item.NombreCargo = dr.GetString(dr.GetOrdinal("vcargo"));
                            item.AptoCurricular = dr.GetInt32(dr.GetOrdinal("iaptocurricular"));
                            item.AptoEvaluacion = dr.GetInt32(dr.GetOrdinal("iaptoevaluacion"));
                            item.AptoResultado = dr.GetInt32(dr.GetOrdinal("iaptoresultado"));
                            item.AptoContrato = dr.GetInt32(dr.GetOrdinal("iaptocontrato"));

                            if (!dr.IsDBNull(dr.GetOrdinal("fechapostulacion"))) item.FechaPostulacion = dr.GetDateTime(dr.GetOrdinal("fechapostulacion")).ToString("dd/MM/yyyy HH:mm");
                            if (!dr.IsDBNull(dr.GetOrdinal("fechalimite_contrato"))) item.FechaMaximaContrato = dr.GetDateTime(dr.GetOrdinal("fechalimite_contrato")).ToString("dd/MM/yyyy HH:mm");

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public Int32 RegistrarPostulanteNotificacion(PostulanteNotificacion_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[usp_Postulante_Notificacion_ins]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", registro.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@iCodPostulacion", registro.IdPostulacion));
                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));
                cmd.Parameters.Add(new SqlParameter("@vDescripcion", registro.Descripcion));
                cmd.Parameters.Add(new SqlParameter("@iOrigen", registro.IdOrigen));
                cmd.Parameters.Add(new SqlParameter("@bEstado", 1));
                cmd.Parameters.Add(new SqlParameter("@vAuditCreacion", registro.IdUsuarioRegistro));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdPostulante;
        }
        public Int32 RegistrarPostulacionAnexo(PostulacionAnexo_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[usp_PostulacionAnexo_ins]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulacion", registro.IdPostulacion));
                cmd.Parameters.Add(new SqlParameter("@bAcepta", registro.IdAcepta));
                cmd.Parameters.Add(new SqlParameter("@vPostulanteRel1", registro.NepotismoRel1.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@vPostulanteRel2", registro.NepotismoRel2.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@vPostulanteRel3", registro.NepotismoRel3.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@vPostulanteApe1", registro.NepotismoApe1.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@vPostulanteApe2", registro.NepotismoApe2.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@vPostulanteApe3", registro.NepotismoApe3.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@vPostulanteNom1", registro.NepotismoNom1.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@vPostulanteNom2", registro.NepotismoNom2.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@vPostulanteNom3", registro.NepotismoNom3.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@vPostulanteAre1", registro.NepotismoAre1.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@vPostulanteAre2", registro.NepotismoAre2.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@vPostulanteAre3", registro.NepotismoAre3.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoConvocatoria", registro.IdTipoConvocatoria));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdPostulacion;
        }

        public IEnumerable<PostulanteNotificacion_Registro> ListarNotificaciones(Postulante_Request peticion)
        {
            List<PostulanteNotificacion_Registro> lista = new List<PostulanteNotificacion_Registro>();
            PostulanteNotificacion_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[usp_PostulanteNotificacion_listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", peticion.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@iCodPostulacion", peticion.IdPostulacion));
                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", peticion.IdConvocatoria));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulanteNotificacion_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdNotificacion = dr.GetInt32(dr.GetOrdinal("iCodNotificacion"));
                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
                            item.IdPostulacion = dr.GetInt32(dr.GetOrdinal("iCodPostulacion"));
                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoria"));
                            item.Descripcion = dr.GetString(dr.GetOrdinal("vDescripcion"));
                            item.IdOrigen = dr.GetInt32(dr.GetOrdinal("iOrigen"));
                            item.IdEstado = dr.GetInt32(dr.GetOrdinal("bEstado"));

                            item.IdUsuarioRegistro = dr.GetString(dr.GetOrdinal("vAuditCreacion"));

                            if (!dr.IsDBNull(dr.GetOrdinal("dtAuditCreacion"))) item.FechaRegistro = dr.GetDateTime(dr.GetOrdinal("dtAuditCreacion")).ToString("dd/MM/yyyy HH:mm");
                            
                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public Int32 GenerarCodigoPostulante()
        {
            Int32 codigo = 0;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion()) {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulanteFicha_generar]";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter IdPropuestaParameter = new SqlParameter("@iCodPostulante", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(IdPropuestaParameter);

                cmd.ExecuteNonQuery();
                codigo = Int32.Parse(IdPropuestaParameter.Value.ToString());

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return codigo;
        }
        public Int32 InsertarPostulanteFicha(PostulanteInformacion_Registro registro)
        {
            Int32 exito = 0;
            List<Empleado_Registro> lista = new List<Empleado_Registro>();
            
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulanteFicha_ins]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", registro.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@iCodPostulacion", registro.IdPostulacion));
                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));
                cmd.Parameters.Add(new SqlParameter("@vNombres", registro.Nombre.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@vApellidoPaterno", registro.Paterno.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@vApellidoMaterno", registro.Materno.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@vFoto", registro.Foto));
                cmd.Parameters.Add(new SqlParameter("@iCodigoEstadoCivil", registro.IdEstadoCivil));
                cmd.Parameters.Add(new SqlParameter("@iTipoVivienda", registro.IdTipoVivienda));
                cmd.Parameters.Add(new SqlParameter("@vCodigoDepartamento", registro.Ubigeo.Substring(0, 2)));
                cmd.Parameters.Add(new SqlParameter("@vCodigoProvincia", registro.Ubigeo.Substring(2, 2)));
                cmd.Parameters.Add(new SqlParameter("@vCodigoDistrito", registro.Ubigeo.Substring(4, 2)));
                cmd.Parameters.Add(new SqlParameter("@iCodigoTipoDocumento", registro.TipoDocumento));
                cmd.Parameters.Add(new SqlParameter("@vNumeroDocumento", registro.NroDocumento));
                cmd.Parameters.Add(new SqlParameter("@vDomicilio", registro.Domicilio));
                cmd.Parameters.Add(new SqlParameter("@vTelefono", registro.Telefono));
                cmd.Parameters.Add(new SqlParameter("@vCelular", registro.Celular));
                cmd.Parameters.Add(new SqlParameter("@dtFechaNacimiento", registro.FechaNacimiento));
                cmd.Parameters.Add(new SqlParameter("@vRUC", registro.RUC));
                cmd.Parameters.Add(new SqlParameter("@vCorreoElectronico", registro.CorreoElectronico));
                cmd.Parameters.Add(new SqlParameter("@bEstado", 1));
                cmd.Parameters.Add(new SqlParameter("@iCodigoGenero", registro.Sexo));
                cmd.Parameters.Add(new SqlParameter("@vNacionalidad", registro.Nacionalidad));
                //cmd.Parameters.Add(new SqlParameter("@vLugarNacimiento", registro.LugarNacimiento));
                cmd.Parameters.Add(new SqlParameter("@iEdad", registro.Edad));
                cmd.Parameters.Add(new SqlParameter("@iTipoBrevete", registro.IdTipoBrevete));
                //cmd.Parameters.Add(new SqlParameter("@vNroLicencia", registro.NroLicencia));
                //cmd.Parameters.Add(new SqlParameter("@vTelefonoEmergencia1", registro.TelefonoEmergencia1));
                //cmd.Parameters.Add(new SqlParameter("@vTelefonoEmergencia2", registro.TelefonoEmergencia2));
                //cmd.Parameters.Add(new SqlParameter("@vContactoEmergencia1", registro.ContactoEmergencia1));
                //cmd.Parameters.Add(new SqlParameter("@vContactoEmergencia2", registro.ContactoEmergencia2));
                cmd.Parameters.Add(new SqlParameter("@iTienePension", registro.IdEstaAfiliadoPensiones));
                cmd.Parameters.Add(new SqlParameter("@iTipoPension", registro.IdAFPAfiliada));
                //cmd.Parameters.Add(new SqlParameter("@vTipoAfpNombre", registro.NombreAFPAfiliada));
                cmd.Parameters.Add(new SqlParameter("@iNuevoTipoPension", registro.IdTipoPensionDeseaAfiliar));
                cmd.Parameters.Add(new SqlParameter("@iTieneBanco", registro.IdEstaAfiliadoBanco));
                cmd.Parameters.Add(new SqlParameter("@iTipoBanco", registro.IdBancoAfiliado));
                //cmd.Parameters.Add(new SqlParameter("@vCuentaBanco", registro.CuentaBancoAfiliado));
                cmd.Parameters.Add(new SqlParameter("@iNuevoTipoBanco", registro.IdBancoDeseaAfiliar));
                cmd.Parameters.Add(new SqlParameter("@iSituacionAcademicaS", registro.IdSituacionAcademicaS));
                cmd.Parameters.Add(new SqlParameter("@iSituacionAcademicaT", registro.IdSituacionAcademicaT));
                cmd.Parameters.Add(new SqlParameter("@iSituacionAcademicaU", registro.IdSituacionAcademicaU));
                cmd.Parameters.Add(new SqlParameter("@iSituacionAcademicaO", registro.IdSituacionAcademicaO));
                cmd.Parameters.Add(new SqlParameter("@iCentroEstudiosPU", registro.IdCentroEstudiosPU));
                cmd.Parameters.Add(new SqlParameter("@iCentroEstudiosPR", registro.IdCentroEstudiosPR));
                cmd.Parameters.Add(new SqlParameter("@iCentroEstudiosEX", registro.IdCentroEstudiosEX));
                cmd.Parameters.Add(new SqlParameter("@vSituacionAcademicaT", registro.SituacionAcademicaT));
                cmd.Parameters.Add(new SqlParameter("@vSituacionAcademicaU", registro.SituacionAcademicaU));
                cmd.Parameters.Add(new SqlParameter("@vSituacionAcademicaO", registro.SituacionAcademicaO));
                //cmd.Parameters.Add(new SqlParameter("@vCentroEstudiosPU", registro.CentroEstudiosPU));
                //cmd.Parameters.Add(new SqlParameter("@vCentroEstudiosPR", registro.CentroEstudiosPR));
                //cmd.Parameters.Add(new SqlParameter("@vCentroEstudiosEX", registro.CentroEstudiosEX));
                cmd.Parameters.Add(new SqlParameter("@iGradoAcademicoES", registro.IdGradoAcademicoES));
                cmd.Parameters.Add(new SqlParameter("@iGradoAcademicoEG", registro.IdGradoAcademicoEG));
                cmd.Parameters.Add(new SqlParameter("@iGradoAcademicoBA", registro.IdGradoAcademicoBA));
                cmd.Parameters.Add(new SqlParameter("@iGradoAcademicoTI", registro.IdGradoAcademicoTI));
                cmd.Parameters.Add(new SqlParameter("@iPostgradoM", registro.IdPostgradoM));
                cmd.Parameters.Add(new SqlParameter("@iPostgradoD", registro.IdPostgradoD));
                cmd.Parameters.Add(new SqlParameter("@iPostgradoO", registro.IdPostgradoO));
                cmd.Parameters.Add(new SqlParameter("@vPostgradoO", registro.PostgradoO));
                cmd.Parameters.Add(new SqlParameter("@vPostgradoCE", registro.PostgradoCE));
                cmd.Parameters.Add(new SqlParameter("@vPostgradoGrado", registro.PostgradoGrado));
                cmd.Parameters.Add(new SqlParameter("@iTieneAlergias", registro.IdPresentaAlergias));
                cmd.Parameters.Add(new SqlParameter("@iTieneAlergias1", registro.IdPresentaAlergias1));
                cmd.Parameters.Add(new SqlParameter("@iTieneAlergias2", registro.IdPresentaAlergias2));
                //cmd.Parameters.Add(new SqlParameter("@vTieneAlergiasOtro", registro.PresentaAlergiasOtro));
                cmd.Parameters.Add(new SqlParameter("@iTieneEnfermedad", registro.IdPresentaEnfermedades));
                cmd.Parameters.Add(new SqlParameter("@iTieneEnfermedadD", registro.IdPresentaEnfermedadesD));
                cmd.Parameters.Add(new SqlParameter("@iTieneEnfermedadH", registro.IdPresentaEnfermedadesH));
                cmd.Parameters.Add(new SqlParameter("@iTieneEnfermedadA", registro.IdPresentaEnfermedadesA));
                cmd.Parameters.Add(new SqlParameter("@iTieneEnfermedadE", registro.IdPresentaEnfermedadesE));
                //cmd.Parameters.Add(new SqlParameter("@vTieneEnfermedadOtro", registro.PresentaEnfermedadesOtro));
                cmd.Parameters.Add(new SqlParameter("@iTieneMedicamento", registro.IdConsumeMedicamentos));
                //cmd.Parameters.Add(new SqlParameter("@vTieneMedicamentoOtro", registro.ConsumeMedicamentosOtro));
                cmd.Parameters.Add(new SqlParameter("@iCodGrupoSanguineo", registro.IdGrupoSanguineo));
                //cmd.Parameters.Add(new SqlParameter("@vSaludAdicional", registro.InformacionAdicionalSalud));
                cmd.Parameters.Add(new SqlParameter("@iTieneDiscapacidad", registro.IdPresentaDiscapacidad));
                cmd.Parameters.Add(new SqlParameter("@iTieneDiscapacidadA", registro.IdPresentaDiscapacidadA));
                cmd.Parameters.Add(new SqlParameter("@iTieneDiscapacidadV", registro.IdPresentaDiscapacidadV));
                cmd.Parameters.Add(new SqlParameter("@iTieneDiscapacidadH", registro.IdPresentaDiscapacidadH));
                cmd.Parameters.Add(new SqlParameter("@iTieneDiscapacidadC", registro.IdPresentaDiscapacidadC));
                cmd.Parameters.Add(new SqlParameter("@iTieneDiscapacidadF", registro.IdPresentaDiscapacidadF));
                //cmd.Parameters.Add(new SqlParameter("@vDiscapacidadConducta", registro.PresentaDiscapacidadC));
                //cmd.Parameters.Add(new SqlParameter("@vDiscapacidadFisica", registro.PresentaDiscapacidadF));
                cmd.Parameters.Add(new SqlParameter("@iTieneCertificadoDis", registro.IdCertificadoDiscapacidad));
                cmd.Parameters.Add(new SqlParameter("@vUbigeo", registro.DescripcionUbigeo));

                cmd.Parameters.Add(new SqlParameter("@iDeclaraIncompatibilidad", registro.IdDeclaraIncompatibilidad));
                cmd.Parameters.Add(new SqlParameter("@iDeclaraNepotismo", registro.IdDeclaraNepotismo));
                cmd.Parameters.Add(new SqlParameter("@iDeclaraNormas", registro.IdDeclaraNormas));
                cmd.Parameters.Add(new SqlParameter("@iDeclaraInteres", registro.IdDeclaraInteres));

                //cmd.Parameters.Add(new SqlParameter("@vPlanilla", registro.NroPlanilla));
                cmd.Parameters.Add(new SqlParameter("@vMeta", registro.Meta));
                cmd.Parameters.Add(new SqlParameter("@iUnidadOrganica", registro.IdUnidadOrganica));
                cmd.Parameters.Add(new SqlParameter("@vNombreCargo", registro.NombreCargo.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@vNombreUnidadOrganica", registro.NombreUnidadOrganica));
                cmd.Parameters.Add(new SqlParameter("@dRemuneracion", registro.Remuneracion));
                cmd.Parameters.Add(new SqlParameter("@dtAuditCreacion", registro.FechaRegistro));
                cmd.Parameters.Add(new SqlParameter("@vAuditCreacion", registro.IdUsuarioRegistro));
                //cmd.Parameters.Add(new SqlParameter("@dtAuditModificacion", registro.IdPostulante));
                //cmd.Parameters.Add(new SqlParameter("@vAuditModificacion", registro.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@vTitulo", registro.NombreProceso));
                cmd.Parameters.Add(new SqlParameter("@vFechaLimite", registro.FechaMaximaContrato));
                
                exito = cmd.ExecuteNonQuery();
                
                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return exito;
        }
        public Int32 InsertarPostulante(Postulante_Registro registro)
        {
            Int32 exito = 0;
            List<Empleado_Registro> lista = new List<Empleado_Registro>();

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_Postulante_ins]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", registro.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@vNombres", registro.Nombre.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@vApellidoPaterno", registro.Paterno.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@vApellidoMaterno", registro.Materno.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@vFoto", registro.Foto));
                cmd.Parameters.Add(new SqlParameter("@iCodigoEstadoCivil", registro.IdEstadoCivil));
                cmd.Parameters.Add(new SqlParameter("@vCodigoDepartamento", registro.Ubigeo.Substring(0, 2)));
                cmd.Parameters.Add(new SqlParameter("@vCodigoProvincia", registro.Ubigeo.Substring(2, 2)));
                cmd.Parameters.Add(new SqlParameter("@vCodigoDistrito", registro.Ubigeo.Substring(4, 2)));
                cmd.Parameters.Add(new SqlParameter("@iCodigoTipoDocumento", registro.TipoDocumento));
                cmd.Parameters.Add(new SqlParameter("@vNumeroDocumento", registro.NroDocumento));
                cmd.Parameters.Add(new SqlParameter("@vDomicilio", registro.Domicilio));
                cmd.Parameters.Add(new SqlParameter("@vTelefono", registro.Telefono));
                cmd.Parameters.Add(new SqlParameter("@vCelular", registro.Celular));
                cmd.Parameters.Add(new SqlParameter("@dtFechaNacimiento", registro.FechaNacimiento));
                cmd.Parameters.Add(new SqlParameter("@vRUC", registro.RUC));
                cmd.Parameters.Add(new SqlParameter("@vCorreoElectronico", registro.CorreoElectronico));
                cmd.Parameters.Add(new SqlParameter("@bEstado", 1));
                cmd.Parameters.Add(new SqlParameter("@iCodigoGenero", registro.Sexo));
                cmd.Parameters.Add(new SqlParameter("@vNacionalidad", registro.Nacionalidad));
                cmd.Parameters.Add(new SqlParameter("@vUbigeo", registro.DescripcionUbigeo));
                cmd.Parameters.Add(new SqlParameter("@vColegio", registro.Colegio));
                cmd.Parameters.Add(new SqlParameter("@vNroColegiatura", registro.NroColegiatura));
                cmd.Parameters.Add(new SqlParameter("@iFFAA", registro.FFAA));
                cmd.Parameters.Add(new SqlParameter("@iDiscapacidad", registro.Discapacidad));
                cmd.Parameters.Add(new SqlParameter("@iDeportista", registro.Deportista));
                cmd.Parameters.Add(new SqlParameter("@dtAuditCreacion", registro.FechaRegistro));
                cmd.Parameters.Add(new SqlParameter("@vAuditCreacion", registro.IdUsuarioRegistro));
                
                exito = cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return exito;
        }
        
        public Int32 ActualizarPostulanteFichaDocumento(PostulanteInformacion_Registro registro)
        {
            Int32 exito = 0;
            List<Empleado_Registro> lista = new List<Empleado_Registro>();

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulanteFicha_doc_upd]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", registro.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@iCodPostulacion", registro.IdPostulacion));
                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));
                if (registro.FileHojaVida != null) cmd.Parameters.Add(new SqlParameter("@fileHojaVida", registro.FileHojaVida));
                if (registro.FileSustento != null) cmd.Parameters.Add(new SqlParameter("@fileSustento", registro.FileSustento));
                if (registro.FileDDJJ != null) cmd.Parameters.Add(new SqlParameter("@fileDDJJ", registro.FileDDJJ));
                if (registro.FileFormato != null) cmd.Parameters.Add(new SqlParameter("@fileFormato", registro.FileFormato));
                
                cmd.Parameters.Add(new SqlParameter("@dtAuditModificacion", registro.FechaModificacion));
                cmd.Parameters.Add(new SqlParameter("@vAuditModificacion", registro.IdUsuarioModificacion));

                exito = cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return exito;
        }
        public Int32 AprobarContratoPostulante(PostulanteInformacion_Registro registro)
        {
            Int32 codigoEmpleado = 0;
            List<Empleado_Registro> lista = new List<Empleado_Registro>();

            try
            {
                _iBasesSqlAdoUnitOfWork.IniciarTransaccion(IsolationLevel.ReadCommitted);

                using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
                {
                    if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                    cmd.CommandText = "[dbo].[Usp_PostulanteFicha_contrato_aprobar]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.CommandTimeout = cmd.CommandTimeout;

                    cmd.Parameters.Add(new SqlParameter("@iCodPostulante", registro.IdPostulante));
                    cmd.Parameters.Add(new SqlParameter("@iCodPostulacion", registro.IdPostulacion));
                    cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));
                    cmd.Parameters.Add(new SqlParameter("@dtAuditModificacion", registro.FechaModificacion));
                    cmd.Parameters.Add(new SqlParameter("@vAuditModificacion", registro.IdUsuarioModificacion));
                    SqlParameter IdPropuestaParameter = new SqlParameter("@iCodEmpleado", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(IdPropuestaParameter);

                    cmd.ExecuteNonQuery();
                    codigoEmpleado = Int32.Parse(IdPropuestaParameter.Value.ToString());

                    //if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
                }

                _iBasesSqlAdoUnitOfWorkM.IniciarTransaccion(IsolationLevel.ReadCommitted);
                using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
                {
                    if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                    cmd.CommandText = "[dbo].[paActualizaTrabajadorEstado]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.CommandTimeout = cmd.CommandTimeout;

                    cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", codigoEmpleado));
                    cmd.Parameters.Add(new SqlParameter("@bEstado", 1));
                    
                    cmd.ExecuteNonQuery();
                    
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
            

            return registro.IdPostulante;
        }
        public Int32 ActualizarPostulanteFicha(PostulanteInformacion_Registro registro)
        {
            Int32 exito = 0;
            List<Empleado_Registro> lista = new List<Empleado_Registro>();

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulanteFicha_upd]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", registro.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@iCodPostulacion", registro.IdPostulacion));
                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));
                cmd.Parameters.Add(new SqlParameter("@vNombres", registro.Nombre));
                cmd.Parameters.Add(new SqlParameter("@vApellidoPaterno", registro.Paterno));
                cmd.Parameters.Add(new SqlParameter("@vApellidoMaterno", registro.Materno));
                cmd.Parameters.Add(new SqlParameter("@vFoto", registro.Foto));
                cmd.Parameters.Add(new SqlParameter("@iCodigoEstadoCivil", registro.IdEstadoCivil));
                cmd.Parameters.Add(new SqlParameter("@iTipoVivienda", registro.IdTipoVivienda));
                cmd.Parameters.Add(new SqlParameter("@vCodigoDepartamento", registro.Ubigeo.Substring(0, 2)));
                cmd.Parameters.Add(new SqlParameter("@vCodigoProvincia", registro.Ubigeo.Substring(2, 2)));
                cmd.Parameters.Add(new SqlParameter("@vCodigoDistrito", registro.Ubigeo.Substring(4, 2)));
                cmd.Parameters.Add(new SqlParameter("@iCodigoTipoDocumento", registro.TipoDocumento));
                cmd.Parameters.Add(new SqlParameter("@vNumeroDocumento", registro.NroDocumento));
                cmd.Parameters.Add(new SqlParameter("@vDomicilio", registro.Domicilio));
                cmd.Parameters.Add(new SqlParameter("@vTelefono", registro.Telefono));
                cmd.Parameters.Add(new SqlParameter("@vCelular", registro.Celular));
                cmd.Parameters.Add(new SqlParameter("@dtFechaNacimiento", registro.FechaNacimiento));
                cmd.Parameters.Add(new SqlParameter("@vRUC", registro.RUC));
                cmd.Parameters.Add(new SqlParameter("@vCorreoElectronico", registro.CorreoElectronico));
                cmd.Parameters.Add(new SqlParameter("@bEstado", 1));
                cmd.Parameters.Add(new SqlParameter("@iCodigoGenero", registro.Sexo));
                cmd.Parameters.Add(new SqlParameter("@vNacionalidad", registro.Nacionalidad));
                cmd.Parameters.Add(new SqlParameter("@vLugarNacimiento", registro.LugarNacimiento));
                cmd.Parameters.Add(new SqlParameter("@iEdad", registro.Edad));
                cmd.Parameters.Add(new SqlParameter("@iTipoBrevete", registro.IdTipoBrevete));
                cmd.Parameters.Add(new SqlParameter("@vNroLicencia", registro.NroLicencia));
                cmd.Parameters.Add(new SqlParameter("@vTelefonoEmergencia1", registro.TelefonoEmergencia1));
                cmd.Parameters.Add(new SqlParameter("@vTelefonoEmergencia2", registro.TelefonoEmergencia2));
                cmd.Parameters.Add(new SqlParameter("@vContactoEmergencia1", registro.ContactoEmergencia1));
                cmd.Parameters.Add(new SqlParameter("@vContactoEmergencia2", registro.ContactoEmergencia2));
                cmd.Parameters.Add(new SqlParameter("@iTienePension", registro.IdEstaAfiliadoPensiones));
                cmd.Parameters.Add(new SqlParameter("@iTipoPension", registro.IdAFPAfiliada));
                //cmd.Parameters.Add(new SqlParameter("@vTipoAfpNombre", registro.NombreAFPAfiliada));
                cmd.Parameters.Add(new SqlParameter("@vCodigoAfp", registro.CodigoAFP));
                cmd.Parameters.Add(new SqlParameter("@iTipoAfp", registro.IdTipoAFP));
                cmd.Parameters.Add(new SqlParameter("@iTipoComisionAfp", registro.IdTipoComisionAFP));

                cmd.Parameters.Add(new SqlParameter("@iNuevoTipoPension", registro.IdTipoPensionDeseaAfiliar));
                cmd.Parameters.Add(new SqlParameter("@iTieneBanco", registro.IdEstaAfiliadoBanco));
                cmd.Parameters.Add(new SqlParameter("@iTipoBanco", registro.IdBancoAfiliado));
                cmd.Parameters.Add(new SqlParameter("@vCuentaBanco", registro.CuentaBancoAfiliado));
                cmd.Parameters.Add(new SqlParameter("@vCuentaBancoCCI", registro.CuentaBancoCCIAfiliado));
                cmd.Parameters.Add(new SqlParameter("@iNuevoTipoBanco", registro.IdBancoDeseaAfiliar));
                cmd.Parameters.Add(new SqlParameter("@iSituacionAcademicaS", registro.IdSituacionAcademicaS));
                cmd.Parameters.Add(new SqlParameter("@iSituacionAcademicaT", registro.IdSituacionAcademicaT));
                cmd.Parameters.Add(new SqlParameter("@iSituacionAcademicaU", registro.IdSituacionAcademicaU));
                cmd.Parameters.Add(new SqlParameter("@iSituacionAcademicaO", registro.IdSituacionAcademicaO));
                cmd.Parameters.Add(new SqlParameter("@iCentroEstudiosPU", registro.IdCentroEstudiosPU));
                cmd.Parameters.Add(new SqlParameter("@iCentroEstudiosPR", registro.IdCentroEstudiosPR));
                cmd.Parameters.Add(new SqlParameter("@iCentroEstudiosEX", registro.IdCentroEstudiosEX));
                cmd.Parameters.Add(new SqlParameter("@vSituacionAcademicaT", registro.SituacionAcademicaT));
                cmd.Parameters.Add(new SqlParameter("@vSituacionAcademicaU", registro.SituacionAcademicaU));
                cmd.Parameters.Add(new SqlParameter("@vSituacionAcademicaO", registro.SituacionAcademicaO));
                cmd.Parameters.Add(new SqlParameter("@vCentroEstudiosPU", registro.CentroEstudiosPU));
                cmd.Parameters.Add(new SqlParameter("@vCentroEstudiosPR", registro.CentroEstudiosPR));
                cmd.Parameters.Add(new SqlParameter("@vCentroEstudiosEX", registro.CentroEstudiosEX));
                cmd.Parameters.Add(new SqlParameter("@iGradoAcademicoES", registro.IdGradoAcademicoES));
                cmd.Parameters.Add(new SqlParameter("@iGradoAcademicoEG", registro.IdGradoAcademicoEG));
                cmd.Parameters.Add(new SqlParameter("@iGradoAcademicoBA", registro.IdGradoAcademicoBA));
                cmd.Parameters.Add(new SqlParameter("@iGradoAcademicoTI", registro.IdGradoAcademicoTI));
                cmd.Parameters.Add(new SqlParameter("@iPostgradoM", registro.IdPostgradoM));
                cmd.Parameters.Add(new SqlParameter("@iPostgradoD", registro.IdPostgradoD));
                cmd.Parameters.Add(new SqlParameter("@iPostgradoO", registro.IdPostgradoO));
                cmd.Parameters.Add(new SqlParameter("@vPostgradoO", registro.PostgradoO));
                cmd.Parameters.Add(new SqlParameter("@vPostgradoCE", registro.PostgradoCE));
                cmd.Parameters.Add(new SqlParameter("@vPostgradoGrado", registro.PostgradoGrado));
                cmd.Parameters.Add(new SqlParameter("@iTieneAlergias", registro.IdPresentaAlergias));
                cmd.Parameters.Add(new SqlParameter("@iTieneAlergias1", registro.IdPresentaAlergias1));
                cmd.Parameters.Add(new SqlParameter("@iTieneAlergias2", registro.IdPresentaAlergias2));
                cmd.Parameters.Add(new SqlParameter("@vTieneAlergiasOtro", registro.PresentaAlergiasOtro));
                cmd.Parameters.Add(new SqlParameter("@iTieneEnfermedad", registro.IdPresentaEnfermedades));
                cmd.Parameters.Add(new SqlParameter("@iTieneEnfermedadD", registro.IdPresentaEnfermedadesD));
                cmd.Parameters.Add(new SqlParameter("@iTieneEnfermedadH", registro.IdPresentaEnfermedadesH));
                cmd.Parameters.Add(new SqlParameter("@iTieneEnfermedadA", registro.IdPresentaEnfermedadesA));
                cmd.Parameters.Add(new SqlParameter("@iTieneEnfermedadE", registro.IdPresentaEnfermedadesE));
                cmd.Parameters.Add(new SqlParameter("@vTieneEnfermedadOtro", registro.PresentaEnfermedadesOtro));
                cmd.Parameters.Add(new SqlParameter("@iTieneMedicamento", registro.IdConsumeMedicamentos));
                cmd.Parameters.Add(new SqlParameter("@vTieneMedicamentoOtro", registro.ConsumeMedicamentosOtro));
                cmd.Parameters.Add(new SqlParameter("@iCodGrupoSanguineo", registro.IdGrupoSanguineo));
                cmd.Parameters.Add(new SqlParameter("@vSaludAdicional", registro.InformacionAdicionalSalud));
                cmd.Parameters.Add(new SqlParameter("@iTieneDiscapacidad", registro.IdPresentaDiscapacidad));
                cmd.Parameters.Add(new SqlParameter("@iTieneDiscapacidadA", registro.IdPresentaDiscapacidadA));
                cmd.Parameters.Add(new SqlParameter("@iTieneDiscapacidadV", registro.IdPresentaDiscapacidadV));
                cmd.Parameters.Add(new SqlParameter("@iTieneDiscapacidadH", registro.IdPresentaDiscapacidadH));
                cmd.Parameters.Add(new SqlParameter("@iTieneDiscapacidadC", registro.IdPresentaDiscapacidadC));
                cmd.Parameters.Add(new SqlParameter("@iTieneDiscapacidadF", registro.IdPresentaDiscapacidadF));
                cmd.Parameters.Add(new SqlParameter("@vDiscapacidadConducta", registro.PresentaDiscapacidadC));
                cmd.Parameters.Add(new SqlParameter("@vDiscapacidadFisica", registro.PresentaDiscapacidadF));
                cmd.Parameters.Add(new SqlParameter("@iTieneCertificadoDis", registro.IdCertificadoDiscapacidad));
                cmd.Parameters.Add(new SqlParameter("@vUbigeo", registro.DescripcionUbigeo));
                cmd.Parameters.Add(new SqlParameter("@iDeclaraIncompatibilidad", registro.IdDeclaraIncompatibilidad));
                cmd.Parameters.Add(new SqlParameter("@iDeclaraNepotismo", registro.IdDeclaraNepotismo));
                cmd.Parameters.Add(new SqlParameter("@iDeclaraNormas", registro.IdDeclaraNormas));
                cmd.Parameters.Add(new SqlParameter("@iDeclaraInteres", registro.IdDeclaraInteres));
                cmd.Parameters.Add(new SqlParameter("@vNepotismoRel1", registro.NepotismoRel1));
                cmd.Parameters.Add(new SqlParameter("@vNepotismoRel2", registro.NepotismoRel2));
                cmd.Parameters.Add(new SqlParameter("@vNepotismoRel3", registro.NepotismoRel3));
                cmd.Parameters.Add(new SqlParameter("@vNepotismoApe1", registro.NepotismoApe1));
                cmd.Parameters.Add(new SqlParameter("@vNepotismoApe2", registro.NepotismoApe2));
                cmd.Parameters.Add(new SqlParameter("@vNepotismoApe3", registro.NepotismoApe3));
                cmd.Parameters.Add(new SqlParameter("@vNepotismoNom1", registro.NepotismoNom1));
                cmd.Parameters.Add(new SqlParameter("@vNepotismoNom2", registro.NepotismoNom2));
                cmd.Parameters.Add(new SqlParameter("@vNepotismoNom3", registro.NepotismoNom3));
                cmd.Parameters.Add(new SqlParameter("@vNepotismoAre1", registro.NepotismoAre1));
                cmd.Parameters.Add(new SqlParameter("@vNepotismoAre2", registro.NepotismoAre2));
                cmd.Parameters.Add(new SqlParameter("@vNepotismoAre3", registro.NepotismoAre3));
                
                //cmd.Parameters.Add(new SqlParameter("@vPlanilla", registro.NroPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@vMeta", registro.Meta));
                //cmd.Parameters.Add(new SqlParameter("@iUnidadOrganica", registro.IdUnidadOrganica));
                //cmd.Parameters.Add(new SqlParameter("@vNombreCargo", registro.NombreCargo.ToUpper()));
                //cmd.Parameters.Add(new SqlParameter("@vNombreUnidadOrganica", registro.NombreUnidadOrganica));
                //cmd.Parameters.Add(new SqlParameter("@dRemuneracion", registro.Remuneracion));
                cmd.Parameters.Add(new SqlParameter("@dtAuditModificacion", registro.FechaModificacion));
                cmd.Parameters.Add(new SqlParameter("@vAuditModificacion", registro.IdUsuarioModificacion));

                exito = cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return exito;
        }
        public Int32 ActualizarPostulante(Postulante_Registro registro)
        {
            Int32 exito = 0;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_Postulante_upd]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", registro.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@vNombres", registro.Nombre));
                cmd.Parameters.Add(new SqlParameter("@vApellidoPaterno", registro.Paterno));
                cmd.Parameters.Add(new SqlParameter("@vApellidoMaterno", registro.Materno));
                cmd.Parameters.Add(new SqlParameter("@vFoto", registro.Foto));
                cmd.Parameters.Add(new SqlParameter("@iCodigoEstadoCivil", registro.IdEstadoCivil));
                cmd.Parameters.Add(new SqlParameter("@vCodigoDepartamento", registro.Ubigeo.Substring(0, 2)));
                cmd.Parameters.Add(new SqlParameter("@vCodigoProvincia", registro.Ubigeo.Substring(2, 2)));
                cmd.Parameters.Add(new SqlParameter("@vCodigoDistrito", registro.Ubigeo.Substring(4, 2)));
                cmd.Parameters.Add(new SqlParameter("@iCodigoTipoDocumento", registro.TipoDocumento));
                cmd.Parameters.Add(new SqlParameter("@vNumeroDocumento", registro.NroDocumento));
                cmd.Parameters.Add(new SqlParameter("@vDomicilio", registro.Domicilio));
                cmd.Parameters.Add(new SqlParameter("@vTelefono", registro.Telefono));
                cmd.Parameters.Add(new SqlParameter("@vCelular", registro.Celular));
                cmd.Parameters.Add(new SqlParameter("@dtFechaNacimiento", registro.FechaNacimiento));
                cmd.Parameters.Add(new SqlParameter("@vRUC", registro.RUC));
                cmd.Parameters.Add(new SqlParameter("@vCorreoElectronico", registro.CorreoElectronico));
                cmd.Parameters.Add(new SqlParameter("@bEstado", registro.Estado));
                cmd.Parameters.Add(new SqlParameter("@iCodigoGenero", registro.Sexo));
                cmd.Parameters.Add(new SqlParameter("@vNacionalidad", registro.Nacionalidad));
                cmd.Parameters.Add(new SqlParameter("@vUbigeo", registro.DescripcionUbigeo));
                cmd.Parameters.Add(new SqlParameter("@vColegio", registro.Colegio));
                cmd.Parameters.Add(new SqlParameter("@vNroColegiatura", registro.NroColegiatura));
                cmd.Parameters.Add(new SqlParameter("@iFFAA", registro.FFAA));
                cmd.Parameters.Add(new SqlParameter("@iDiscapacidad", registro.Discapacidad));
                cmd.Parameters.Add(new SqlParameter("@iDeportista", registro.Deportista));
                cmd.Parameters.Add(new SqlParameter("@dtAuditModificacion", registro.FechaModificacion));
                cmd.Parameters.Add(new SqlParameter("@vAuditModificacion", registro.IdUsuarioModificacion));
                cmd.Parameters.Add(new SqlParameter("@bColegiatura", registro.bColigiatura));
                cmd.Parameters.Add(new SqlParameter("@bColegiaturaHabilitado", registro.bColegiatura_Habilitada));
                cmd.Parameters.Add(new SqlParameter("@vCarnetConadis", registro.CarnetConadis));
                cmd.Parameters.Add(new SqlParameter("@vDeportistaAcreditado", registro.Acreditacion));
                cmd.Parameters.Add(new SqlParameter("@bTrabajarProvincia", registro.bTrabajarProvincia));
                cmd.Parameters.Add(new SqlParameter("@vTrabajarProvincia", registro.Trabajar_Provincia));
                cmd.Parameters.Add(new SqlParameter("@vDomicilioActual", registro.DireccionActual));
                cmd.Parameters.Add(new SqlParameter("@vReferencia", registro.Referencia));
                cmd.Parameters.Add(new SqlParameter("@vCarnetFuerzasArmadas", registro.CarnetFuerzasArmadas));

                exito = cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return exito;
        }
        public Int32 ValidarRegistroPostulante(PostulanteInformacion_Registro registro)
        {
            Int32 exito = 0;
            List<Empleado_Registro> lista = new List<Empleado_Registro>();

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulanteFicha_validar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", registro.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@iCodPostulacion", registro.IdPostulacion));
                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));
                cmd.Parameters.Add(new SqlParameter("@bEstado", registro.Estado));
                cmd.Parameters.Add(new SqlParameter("@dtAuditModificacion", registro.FechaModificacion));
                cmd.Parameters.Add(new SqlParameter("@vAuditModificacion", registro.IdUsuarioModificacion));

                exito = cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return exito;
        }
        public PostulanteInformacion_Registro ObtenerPostulanteFicha(PostulanteInformacion_Registro registro)
        {
            PostulanteInformacion_Registro item = new PostulanteInformacion_Registro();

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulanteFicha_sel]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", registro.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@iCodPostulacion", registro.IdPostulacion));
                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));
                
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulanteInformacion_Registro();
                            item.Grilla = new Grilla_Response();
                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
                            item.IdPostulacion = dr.GetInt32(dr.GetOrdinal("iCodPostulacion"));
                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoria"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            item.Paterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            item.Materno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            item.Foto = dr.GetString(dr.GetOrdinal("vFoto"));
                            item.IdEstadoCivil = dr.GetInt32(dr.GetOrdinal("iCodigoEstadoCivil"));
                            item.IdTipoVivienda = dr.GetInt32(dr.GetOrdinal("iTipoVivienda"));
                            item.Ubigeo = String.Format("{0}{1}{2}", dr.GetString(dr.GetOrdinal("vCodigoDepartamento")), dr.GetString(dr.GetOrdinal("vCodigoProvincia")), dr.GetString(dr.GetOrdinal("vCodigoDistrito")));
                            item.TipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            item.NroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            item.Domicilio = dr.GetString(dr.GetOrdinal("vDomicilio"));
                            item.Telefono = dr.GetString(dr.GetOrdinal("vTelefono"));
                            item.Celular = dr.GetString(dr.GetOrdinal("vCelular"));
                            item.FechaNacimiento = dr.GetString(dr.GetOrdinal("dtFechaNacimiento"));
                            item.RUC = dr.GetString(dr.GetOrdinal("vRUC"));
                            item.CorreoElectronico = dr.GetString(dr.GetOrdinal("vCorreoElectronico"));
                            item.Estado = dr.GetInt32(dr.GetOrdinal("bEstado"));
                            item.Sexo = dr.GetString(dr.GetOrdinal("iCodigoGenero"));
                            item.Nacionalidad = dr.GetString(dr.GetOrdinal("vNacionalidad"));
                            item.LugarNacimiento = dr.GetString(dr.GetOrdinal("vLugarNacimiento"));
                            item.Edad = dr.GetInt32(dr.GetOrdinal("iEdad"));
                            item.IdTipoBrevete = dr.GetInt32(dr.GetOrdinal("iTipoBrevete"));
                            item.NroLicencia = dr.GetString(dr.GetOrdinal("vNroLicencia"));
                            item.TelefonoEmergencia1 = dr.GetString(dr.GetOrdinal("vTelefonoEmergencia1"));
                            item.TelefonoEmergencia2 = dr.GetString(dr.GetOrdinal("vTelefonoEmergencia2"));
                            item.ContactoEmergencia1 = dr.GetString(dr.GetOrdinal("vContactoEmergencia1"));
                            item.ContactoEmergencia2 = dr.GetString(dr.GetOrdinal("vContactoEmergencia2"));
                            item.IdEstaAfiliadoPensiones = dr.GetInt32(dr.GetOrdinal("iTienePension"));
                            item.IdAFPAfiliada = dr.GetInt32(dr.GetOrdinal("iTipoPension"));
                            //item.NombreAFPAfiliada = dr.GetString(dr.GetOrdinal("vTipoAfpNombre"));
                            item.CodigoAFP = dr.GetString(dr.GetOrdinal("vCodigoAfp"));
                            item.IdTipoAFP = dr.GetInt32(dr.GetOrdinal("iTipoAfp"));
                            item.IdTipoComisionAFP = dr.GetInt32(dr.GetOrdinal("iTipoComisionAfp"));

                            item.IdTipoPensionDeseaAfiliar = dr.GetInt32(dr.GetOrdinal("iNuevoTipoPension"));
                            item.IdEstaAfiliadoBanco = dr.GetInt32(dr.GetOrdinal("iTieneBanco"));
                            item.IdBancoAfiliado = dr.GetInt32(dr.GetOrdinal("iTipoBanco"));
                            item.CuentaBancoAfiliado = dr.GetString(dr.GetOrdinal("vCuentaBanco"));
                            item.CuentaBancoCCIAfiliado = dr.GetString(dr.GetOrdinal("vCuentaBancoCCI"));
                            item.IdBancoDeseaAfiliar = dr.GetInt32(dr.GetOrdinal("iNuevoTipoBanco"));
                            item.IdSituacionAcademicaS = dr.GetInt32(dr.GetOrdinal("iSituacionAcademicaS"));
                            item.IdSituacionAcademicaT = dr.GetInt32(dr.GetOrdinal("iSituacionAcademicaT"));
                            item.IdSituacionAcademicaU = dr.GetInt32(dr.GetOrdinal("iSituacionAcademicaU"));
                            item.IdSituacionAcademicaO = dr.GetInt32(dr.GetOrdinal("iSituacionAcademicaO"));
                            item.IdCentroEstudiosPU = dr.GetInt32(dr.GetOrdinal("iCentroEstudiosPU"));
                            item.IdCentroEstudiosPR = dr.GetInt32(dr.GetOrdinal("iCentroEstudiosPR"));
                            item.IdCentroEstudiosEX = dr.GetInt32(dr.GetOrdinal("iCentroEstudiosEX"));
                            item.SituacionAcademicaT = dr.GetString(dr.GetOrdinal("vSituacionAcademicaT"));
                            item.SituacionAcademicaU = dr.GetString(dr.GetOrdinal("vSituacionAcademicaU"));
                            item.SituacionAcademicaO = dr.GetString(dr.GetOrdinal("vSituacionAcademicaO"));
                            item.CentroEstudiosPU = dr.GetString(dr.GetOrdinal("vCentroEstudiosPU"));
                            item.CentroEstudiosPR = dr.GetString(dr.GetOrdinal("vCentroEstudiosPR"));
                            item.CentroEstudiosEX = dr.GetString(dr.GetOrdinal("vCentroEstudiosEX"));
                            item.IdGradoAcademicoES = dr.GetInt32(dr.GetOrdinal("iGradoAcademicoES"));
                            item.IdGradoAcademicoEG = dr.GetInt32(dr.GetOrdinal("iGradoAcademicoEG"));
                            item.IdGradoAcademicoBA = dr.GetInt32(dr.GetOrdinal("iGradoAcademicoBA"));
                            item.IdGradoAcademicoTI = dr.GetInt32(dr.GetOrdinal("iGradoAcademicoTI"));
                            item.IdPostgradoM = dr.GetInt32(dr.GetOrdinal("iPostgradoM"));
                            item.IdPostgradoD = dr.GetInt32(dr.GetOrdinal("iPostgradoD"));
                            item.IdPostgradoO = dr.GetInt32(dr.GetOrdinal("iPostgradoO"));
                            item.PostgradoO = dr.GetString(dr.GetOrdinal("vPostgradoO"));
                            item.PostgradoCE = dr.GetString(dr.GetOrdinal("vPostgradoCE"));
                            item.PostgradoGrado = dr.GetString(dr.GetOrdinal("vPostgradoGrado"));
                            item.IdPresentaAlergias = dr.GetInt32(dr.GetOrdinal("iTieneAlergias"));
                            item.IdPresentaAlergias1 = dr.GetInt32(dr.GetOrdinal("iTieneAlergias1"));
                            item.IdPresentaAlergias2 = dr.GetInt32(dr.GetOrdinal("iTieneAlergias2"));
                            item.PresentaAlergiasOtro = dr.GetString(dr.GetOrdinal("vTieneAlergiasOtro"));
                            item.IdPresentaEnfermedades = dr.GetInt32(dr.GetOrdinal("iTieneEnfermedad"));
                            item.IdPresentaEnfermedadesD = dr.GetInt32(dr.GetOrdinal("iTieneEnfermedadD"));
                            item.IdPresentaEnfermedadesH = dr.GetInt32(dr.GetOrdinal("iTieneEnfermedadH"));
                            item.IdPresentaEnfermedadesA = dr.GetInt32(dr.GetOrdinal("iTieneEnfermedadA"));
                            item.IdPresentaEnfermedadesE = dr.GetInt32(dr.GetOrdinal("iTieneEnfermedadE"));
                            item.PresentaEnfermedadesOtro = dr.GetString(dr.GetOrdinal("vTieneEnfermedadOtro"));
                            item.IdConsumeMedicamentos = dr.GetInt32(dr.GetOrdinal("iTieneMedicamento"));
                            item.ConsumeMedicamentosOtro = dr.GetString(dr.GetOrdinal("vTieneMedicamentoOtro"));
                            item.IdGrupoSanguineo = dr.GetInt32(dr.GetOrdinal("iCodGrupoSanguineo"));
                            item.InformacionAdicionalSalud = dr.GetString(dr.GetOrdinal("vSaludAdicional"));
                            item.IdPresentaDiscapacidad = dr.GetInt32(dr.GetOrdinal("iTieneDiscapacidad"));
                            item.IdPresentaDiscapacidadA = dr.GetInt32(dr.GetOrdinal("iTieneDiscapacidadA"));
                            item.IdPresentaDiscapacidadV = dr.GetInt32(dr.GetOrdinal("iTieneDiscapacidadV"));
                            item.IdPresentaDiscapacidadH = dr.GetInt32(dr.GetOrdinal("iTieneDiscapacidadH"));
                            item.IdPresentaDiscapacidadC = dr.GetInt32(dr.GetOrdinal("iTieneDiscapacidadC"));
                            item.IdPresentaDiscapacidadF = dr.GetInt32(dr.GetOrdinal("iTieneDiscapacidadF"));
                            item.PresentaDiscapacidadC = dr.GetString(dr.GetOrdinal("vDiscapacidadConducta"));
                            item.PresentaDiscapacidadF = dr.GetString(dr.GetOrdinal("vDiscapacidadFisica"));
                            item.IdCertificadoDiscapacidad = dr.GetInt32(dr.GetOrdinal("iTieneCertificadoDis"));
                            item.DescripcionUbigeo = dr.GetString(dr.GetOrdinal("vUbigeo"));
                            //dr.GetInt32(dr.GetOrdinal("vPlanilla", item.NroPlanilla));
                            item.Meta = dr.GetString(dr.GetOrdinal("vMeta"));
                            
                            item.IdUnidadOrganica = dr.GetInt32(dr.GetOrdinal("iUnidadOrganica"));
                            item.NombreCargo = dr.GetString(dr.GetOrdinal("vNombreCargo"));
                            item.NombreUnidadOrganica = dr.GetString(dr.GetOrdinal("vNombreUnidadOrganica"));
                            item.Remuneracion = dr.GetDecimal(dr.GetOrdinal("dRemuneracion"));

                            item.IdDeclaraIncompatibilidad = dr.GetInt32(dr.GetOrdinal("iDeclaraIncompatibilidad"));
                            item.IdDeclaraNepotismo = dr.GetInt32(dr.GetOrdinal("iDeclaraNepotismo"));
                            item.IdDeclaraNormas = dr.GetInt32(dr.GetOrdinal("iDeclaraNormas"));
                            item.IdDeclaraInteres = dr.GetInt32(dr.GetOrdinal("iDeclaraInteres"));
                            item.NepotismoRel1 = dr.GetString(dr.GetOrdinal("vNepotismoRel1")).ToUpper();
                            item.NepotismoRel2 = dr.GetString(dr.GetOrdinal("vNepotismoRel2")).ToUpper();
                            item.NepotismoRel3 = dr.GetString(dr.GetOrdinal("vNepotismoRel3")).ToUpper();
                            item.NepotismoApe1 = dr.GetString(dr.GetOrdinal("vNepotismoApe1")).ToUpper();
                            item.NepotismoApe2 = dr.GetString(dr.GetOrdinal("vNepotismoApe2")).ToUpper();
                            item.NepotismoApe3 = dr.GetString(dr.GetOrdinal("vNepotismoApe3")).ToUpper();
                            item.NepotismoNom1 = dr.GetString(dr.GetOrdinal("vNepotismoNom1")).ToUpper();
                            item.NepotismoNom2 = dr.GetString(dr.GetOrdinal("vNepotismoNom2")).ToUpper();
                            item.NepotismoNom3 = dr.GetString(dr.GetOrdinal("vNepotismoNom3")).ToUpper();
                            item.NepotismoAre1 = dr.GetString(dr.GetOrdinal("vNepotismoAre1")).ToUpper();
                            item.NepotismoAre2 = dr.GetString(dr.GetOrdinal("vNepotismoAre2")).ToUpper();
                            item.NepotismoAre3 = dr.GetString(dr.GetOrdinal("vNepotismoAre3")).ToUpper();
                            
                            item.IdEstadoRegistro = dr.GetInt32(dr.GetOrdinal("estadoRegistro"));
                            item.IdTieneHojaVida = dr.GetInt32(dr.GetOrdinal("tieneHojaVida"));
                            item.IdTieneSustento = dr.GetInt32(dr.GetOrdinal("tieneSustento"));
                            item.IdTieneDDJJ = dr.GetInt32(dr.GetOrdinal("tieneDDJJ"));
                            item.IdTieneFormato = dr.GetInt32(dr.GetOrdinal("tieneFormato"));
                            item.TotalAlertas = dr.GetInt32(dr.GetOrdinal("totalAlertas"));

                            if (!Convert.IsDBNull(dr["vAirhsp"])) item.NroPlanilla = dr.GetString(dr.GetOrdinal("vAirhsp"));
                            if (!Convert.IsDBNull(dr["NombreProceso"])) item.NombreProceso = dr.GetString(dr.GetOrdinal("NombreProceso"));
                            //if (!Convert.IsDBNull(dr["fHojaVida"])) { item.FileHojaVida = (byte[])(dr["fHojaVida"]); }
                            //if (!Convert.IsDBNull(dr["fSustento"])) { item.FileSustento = (byte[])(dr["fSustento"]); }
                            //if (!Convert.IsDBNull(dr["fDDJJ"])) { item.FileDDJJ = (byte[])(dr["fDDJJ"]); }
                            //if (!Convert.IsDBNull(dr["fFormato"])) { item.FileFormato = (byte[])(dr["fFormato"]); }
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return item;
        }
        public PostulanteInformacion_Registro ObtenerPostulanteFichaDocumento(PostulanteInformacion_Registro registro)
        {
            PostulanteInformacion_Registro item = new PostulanteInformacion_Registro();

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulanteFicha_doc_sel]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", registro.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@iCodPostulacion", registro.IdPostulacion));
                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulanteInformacion_Registro();

                            item.Grilla = new Grilla_Response();

                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
                            item.IdPostulacion = dr.GetInt32(dr.GetOrdinal("iCodPostulacion"));
                            item.IdConvocatoria = dr.GetInt32(dr.GetOrdinal("iCodConvocatoria"));

                            if (!Convert.IsDBNull(dr["fHojaVida"])) { item.FileHojaVida = (byte[])(dr["fHojaVida"]); }
                            if (!Convert.IsDBNull(dr["fSustento"])) { item.FileSustento = (byte[])(dr["fSustento"]); }
                            if (!Convert.IsDBNull(dr["fDDJJ"])) { item.FileDDJJ = (byte[])(dr["fDDJJ"]); }
                            if (!Convert.IsDBNull(dr["fFormato"])) { item.FileFormato = (byte[])(dr["fFormato"]); }
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return item;
        }
        public Postulante_Registro ObtenerPostulanteDocumento(Postulante_Registro registro)
        {
            Postulante_Registro item = new Postulante_Registro();
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_Postulante_doc_sel]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", registro.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@iCodDetalle", registro.IdDetalle));
                cmd.Parameters.Add(new SqlParameter("@iTipo", registro.IdTipo));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Postulante_Registro();
                            item.Grilla = new Grilla_Response();

                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
                            if (!Convert.IsDBNull(dr["fArchivo"])) { 
                                if(registro.IdTipo == 1)
                                    item.FileEstudio = (byte[])(dr["fArchivo"]);
                                if (registro.IdTipo == 2)
                                    item.Filecapacitacion = (byte[])(dr["fArchivo"]);
                                if (registro.IdTipo == 3)
                                    item.FileExperiencia = (byte[])(dr["fArchivo"]);
                                if (registro.IdTipo == 5)
                                    item.FileDocumento = (byte[])(dr["fArchivo"]); 
                            }
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return item;
        }
        public Postulacion_Registro ObtenerPostulacionDocumento(Postulacion_Registro registro)
        {
            Postulacion_Registro item = new Postulacion_Registro();
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_Postulacion_doc_sel]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulacion", registro.IdPostulacion));
                cmd.Parameters.Add(new SqlParameter("@iCodDetalle", registro.IdDetalle));
                cmd.Parameters.Add(new SqlParameter("@iTipo", registro.IdTipo));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Postulacion_Registro();
                            item.Grilla = new Grilla_Response();

                            item.IdPostulacion = dr.GetInt32(dr.GetOrdinal("iCodPostulacion"));
                            if (!Convert.IsDBNull(dr["fArchivo"]))
                            {
                                if (registro.IdTipo == 1)
                                    item.FileEstudio = (byte[])(dr["fArchivo"]);
                                if (registro.IdTipo == 2)
                                    item.Filecapacitacion = (byte[])(dr["fArchivo"]);
                                if (registro.IdTipo == 3)
                                    item.FileExperiencia = (byte[])(dr["fArchivo"]);
                                if (registro.IdTipo == 5)
                                    item.FileDocumento = (byte[])(dr["fArchivo"]);
                                if (registro.IdTipo == 6) {
                                    if (registro.IdDetalle == 7001)
                                        item.FileAnexoPracticas = (byte[])(dr["fArchivo"]);
                                    if (registro.IdDetalle == 7002)
                                        item.FileHojaVida = (byte[])(dr["fArchivo"]);
                                    if (registro.IdDetalle == 7003)
                                        item.FileCarta = (byte[])(dr["fArchivo"]);
                                }
                            }
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return item;
        }
        public PostulanteExperiencia_Registro ObtenerPostulanteExperiencia(Postulante_Registro registro)
        {
            PostulanteExperiencia_Registro item = new PostulanteExperiencia_Registro();
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulanteExperiencia_sel]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", registro.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@iCodLaboral", registro.IdDetalle));
                
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulanteExperiencia_Registro();
                            item.Grilla = new Grilla_Response();

                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
                            item.IdLaboral = dr.GetInt32(dr.GetOrdinal("iCodLaboral"));
                            item.Empresa = dr.GetString(dr.GetOrdinal("vEmpresa"));
                            item.Cargo = dr.GetString(dr.GetOrdinal("vCargo"));
                            item.Descripcion = dr.GetString(dr.GetOrdinal("vDescripcion"));
                            item.Estado = dr.GetInt32(dr.GetOrdinal("bEstado"));
                            item.IdTieneArchivo = dr.GetInt32(dr.GetOrdinal("tieneArchivo"));
                            if (!Convert.IsDBNull(dr["dFechaInicio"])) { item.FechaInicio = dr.GetDateTime(dr.GetOrdinal("dFechaInicio")).ToString("dd/MM/yyyy"); }
                            if (!Convert.IsDBNull(dr["dFechaFin"])) { item.FechaFin = dr.GetDateTime(dr.GetOrdinal("dFechaFin")).ToString("dd/MM/yyyy"); }

                            item.Sector = dr.GetInt32(dr.GetOrdinal("iSector"));
                            item.Regimen = dr.GetInt32(dr.GetOrdinal("iRegimen"));
                            item.NombreJefeDirecto = dr.GetString(dr.GetOrdinal("vNombreJefeDirecto"));
                            item.PuestoJefeDirecto = dr.GetString(dr.GetOrdinal("vPuestoJefeDirecto"));
                            item.MotivoCambio = dr.GetString(dr.GetOrdinal("vMotivoCambio"));
                            item.Remuneracion = dr.GetDecimal(dr.GetOrdinal("mnRemuneracion"));
                            item.ReferenciaNombre = dr.GetString(dr.GetOrdinal("vRefLaboralNombre"));
                            item.ReferenciaPuesto = dr.GetString(dr.GetOrdinal("vRefLaboralPuesto"));
                            item.ReferenciaTelefono = dr.GetString(dr.GetOrdinal("vRefLaboralTelefono"));
                            item.ReferenciaCorreo = dr.GetString(dr.GetOrdinal("vRefLaboralCorreo"));

                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return item;
        }
        public PostulanteEstudio_Registro ObtenerPostulanteEstudio(Postulante_Registro registro)
        {
            PostulanteEstudio_Registro item = new PostulanteEstudio_Registro();
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulanteEstudio_sel]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", registro.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@iCodEstudio", registro.IdDetalle));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulanteEstudio_Registro();
                            item.Grilla = new Grilla_Response();

                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
                            item.IdEstudio = dr.GetInt32(dr.GetOrdinal("iCodEstudio"));
                            item.IdNivel = dr.GetInt32(dr.GetOrdinal("iCodNivel"));
                            item.IdNoTieneTitulo = dr.GetInt32(dr.GetOrdinal("icodsintitulo"));
                            item.Especialidad = dr.GetString(dr.GetOrdinal("vEspecialidad"));
                            item.Institucion = dr.GetString(dr.GetOrdinal("vInstitucion"));
                            item.IdMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            item.IdAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            item.Ciudad = dr.GetString(dr.GetOrdinal("vCiudad"));
                            item.Estado = dr.GetInt32(dr.GetOrdinal("bEstado"));
                            item.IdTieneArchivo = dr.GetInt32(dr.GetOrdinal("tieneArchivo"));
                            item.IdFechaInicioMes = dr.GetInt32(dr.GetOrdinal("iFechaInicioMes"));
                            item.IdFechaInicioAnio = dr.GetInt32(dr.GetOrdinal("iFechaInicioAnio"));
                            item.IdFechaFinMes = dr.GetInt32(dr.GetOrdinal("iFechaFinMes"));
                            item.IdFechaFinAnio = dr.GetInt32(dr.GetOrdinal("iFechaFinAnio"));
                            //if (!Convert.IsDBNull(dr["dFechaInicio"])) { item.FechaInicio = dr.GetDateTime(dr.GetOrdinal("dFechaInicio")).ToString("dd/MM/yyyy"); }
                            //if (!Convert.IsDBNull(dr["dFechaFin"])) { item.FechaFin = dr.GetDateTime(dr.GetOrdinal("dFechaFin")).ToString("dd/MM/yyyy"); }
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return item;
        }
        public PostulanteCapacitacion_Registro ObtenerPostulanteCapacitacion(Postulante_Registro registro)
        {
            PostulanteCapacitacion_Registro item = new PostulanteCapacitacion_Registro();
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulanteCapacitacion_sel]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", registro.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@iCodCapacitacion", registro.IdDetalle));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PostulanteCapacitacion_Registro();
                            item.Grilla = new Grilla_Response();

                            item.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
                            item.IdCapacitacion = dr.GetInt32(dr.GetOrdinal("iCodCapacitacion"));
                            item.Especialidad = dr.GetString(dr.GetOrdinal("vEspecialidad"));
                            item.Institucion = dr.GetString(dr.GetOrdinal("vInstitucion"));
                            item.Ciudad = dr.GetString(dr.GetOrdinal("vCiudad"));
                            item.Horas = dr.GetInt32(dr.GetOrdinal("iHoras"));
                            item.Estado = dr.GetInt32(dr.GetOrdinal("bEstado"));
                            item.IdTieneArchivo = dr.GetInt32(dr.GetOrdinal("tieneArchivo"));
                            if (!Convert.IsDBNull(dr["dFechaInicio"])) { item.FechaInicio = dr.GetDateTime(dr.GetOrdinal("dFechaInicio")).ToString("dd/MM/yyyy"); }
                            if (!Convert.IsDBNull(dr["dFechaFin"])) { item.FechaFin = dr.GetDateTime(dr.GetOrdinal("dFechaFin")).ToString("dd/MM/yyyy"); }
                            item.TipoEstudio = dr.GetInt32(dr.GetOrdinal("iTipoEstudio"));
                            item.TipoEstudioOtro = dr.GetInt32(dr.GetOrdinal("iTipoEstudioOtro"));
                            item.NivelTipoEstudioOtro = dr.GetInt32(dr.GetOrdinal("iNivelTipoEstudioOtro"));
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return item;
        }
        //public PostulanteEstudio_Registro ObtenerPostulanteDocumento(Postulante_Registro registro)
        //{
        //    PostulanteEstudio_Registro item = new PostulanteEstudio_Registro();
        //    using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
        //    {
        //        if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
        //        cmd.CommandText = "[dbo].[Usp_PostulanteEstudio_sel]";
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        //cmd.CommandTimeout = cmd.CommandTimeout;

        //        cmd.Parameters.Add(new SqlParameter("@iCodPostulante", registro.IdPostulante));
        //        cmd.Parameters.Add(new SqlParameter("@iCodEstudio", registro.IdDetalle));

        //        using (SqlDataReader dr = cmd.ExecuteReader())
        //        {
        //            if (dr.HasRows)
        //            {
        //                while (dr.Read())
        //                {
        //                    item = new PostulanteEstudio_Registro();
        //                    item.Grilla = new Grilla_Response();

        //                    item.IdPostulante = dr.GetInt32(dr.GetOrdinal("iCodPostulante"));
        //                    item.IdEstudio = dr.GetInt32(dr.GetOrdinal("iCodEstudio"));
        //                    item.IdNivel = dr.GetInt32(dr.GetOrdinal("iCodNivel"));
        //                    item.IdNoTieneTitulo = dr.GetInt32(dr.GetOrdinal("icodsintitulo"));
        //                    item.Especialidad = dr.GetString(dr.GetOrdinal("vEspecialidad"));
        //                    item.Institucion = dr.GetString(dr.GetOrdinal("vInstitucion"));
        //                    item.IdMes = dr.GetInt32(dr.GetOrdinal("iMes"));
        //                    item.IdAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
        //                    item.Ciudad = dr.GetString(dr.GetOrdinal("vCiudad"));
        //                    item.Estado = dr.GetInt32(dr.GetOrdinal("bEstado"));
        //                    item.IdTieneArchivo = dr.GetInt32(dr.GetOrdinal("tieneArchivo"));
        //                    //if (!Convert.IsDBNull(dr["dFechaInicio"])) { item.FechaInicio = dr.GetDateTime(dr.GetOrdinal("dFechaInicio")).ToString("dd/MM/yyyy"); }
        //                    //if (!Convert.IsDBNull(dr["dFechaFin"])) { item.FechaFin = dr.GetDateTime(dr.GetOrdinal("dFechaFin")).ToString("dd/MM/yyyy"); }
        //                }
        //            }
        //        }

        //        if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
        //    }

        //    return item;
        //}
        public Int32 RegistrarPostulanteFamiliar(PostulanteFamiliar_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[usp_PostulanteFamiliar_ins]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", registro.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@iCodPostulacion", registro.IdPostulacion));
                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));
                cmd.Parameters.Add(new SqlParameter("@vNombres", registro.Nombre));
                cmd.Parameters.Add(new SqlParameter("@iParentesco", registro.IdParentesco));
                cmd.Parameters.Add(new SqlParameter("@vNumeroDocumento", registro.NroDocumento));
                cmd.Parameters.Add(new SqlParameter("@iEdad", registro.Edad));
                cmd.Parameters.Add(new SqlParameter("@dtFechaNacimiento", registro.FechaNacimiento));
                cmd.Parameters.Add(new SqlParameter("@vOcupacion", (!String.IsNullOrEmpty(registro.Ocupacion) ? registro.Ocupacion.ToUpper() : String.Empty)));
                cmd.Parameters.Add(new SqlParameter("@vSexo", registro.IdSexo));
                cmd.Parameters.Add(new SqlParameter("@bEstado", 1));
                cmd.Parameters.Add(new SqlParameter("@vAuditCreacion", registro.IdUsuarioRegistro));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdPostulante;
        }
        public Int32 RegistrarPostulanteDocumento(PostulanteDocumento_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulanteDocumento_ins]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", registro.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoDocumento", registro.IdTipoDocumento));
                cmd.Parameters.Add(new SqlParameter("@bEstado", registro.Estado));
                cmd.Parameters.Add(new SqlParameter("@fArchivo", registro.FileArchivo));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdPostulante;
        }
        public Int32 ActualizarPostulanteDocumento(PostulanteDocumento_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulanteDocumento_upd]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", registro.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@iCodDocumento", registro.IdDocumento));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoDocumento", registro.IdTipoDocumento));
                cmd.Parameters.Add(new SqlParameter("@bEstado", registro.Estado));
                cmd.Parameters.Add(new SqlParameter("@fArchivo", registro.FileArchivo));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdPostulante;
        }
        public Int32 RegistrarPostulanteEstudio(PostulanteEstudio_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulanteEstudio_ins]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", registro.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@iCodNivel", registro.IdNivel));
                cmd.Parameters.Add(new SqlParameter("@iCodSinTitulo", registro.IdNoTieneTitulo));
                cmd.Parameters.Add(new SqlParameter("@vEspecialidad", registro.Especialidad.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@vInstitucion", registro.Institucion.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@iMes", registro.IdMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", registro.IdAnio));
                cmd.Parameters.Add(new SqlParameter("@vCiudad", registro.Ciudad.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@bEstado", registro.Estado));
                cmd.Parameters.Add(new SqlParameter("@fArchivo", registro.FileArchivo));
                cmd.Parameters.Add(new SqlParameter("@iFechaInicioMes", registro.IdFechaInicioMes));
                cmd.Parameters.Add(new SqlParameter("@iFechaInicioAnio", registro.IdFechaInicioAnio));
                cmd.Parameters.Add(new SqlParameter("@iFechaFinMes", registro.IdFechaFinMes));
                cmd.Parameters.Add(new SqlParameter("@iFechaFinAnio", registro.IdFechaFinAnio));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdPostulante;
        }
        public Int32 ActualizarPostulanteEstudio(PostulanteEstudio_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulanteEstudio_upd]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", registro.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@iCodEstudio", registro.IdEstudio));
                cmd.Parameters.Add(new SqlParameter("@iCodNivel", registro.IdNivel));
                cmd.Parameters.Add(new SqlParameter("@iCodSinTitulo", registro.IdNoTieneTitulo));
                cmd.Parameters.Add(new SqlParameter("@vEspecialidad", registro.Especialidad.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@vInstitucion", registro.Institucion.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@iMes", registro.IdMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", registro.IdAnio));
                cmd.Parameters.Add(new SqlParameter("@vCiudad", registro.Ciudad.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@bEstado", registro.Estado));
                cmd.Parameters.Add(new SqlParameter("@fArchivo", registro.FileArchivo));
                cmd.Parameters.Add(new SqlParameter("@iFechaInicioMes", registro.IdFechaInicioMes));
                cmd.Parameters.Add(new SqlParameter("@iFechaInicioAnio", registro.IdFechaInicioAnio));
                cmd.Parameters.Add(new SqlParameter("@iFechaFinMes", registro.IdFechaFinMes));
                cmd.Parameters.Add(new SqlParameter("@iFechaFinAnio", registro.IdFechaFinAnio));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdPostulante;
        }
        public Int32 EliminarPostulanteEstudio(PostulanteEstudio_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulanteEstudio_del]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", registro.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@iCodEstudio", registro.IdEstudio));
                
                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdPostulante;
        }
        public Int32 RegistrarPostulanteCapacitacion(PostulanteCapacitacion_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulanteCapacitacion_ins]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", registro.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@vEspecialidad", registro.Especialidad.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@vInstitucion", registro.Institucion.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@dFechaInicio", DateTime.Parse(registro.FechaInicio)));
                cmd.Parameters.Add(new SqlParameter("@dFechaFin", DateTime.Parse(registro.FechaFin)));
                cmd.Parameters.Add(new SqlParameter("@vCiudad", registro.Ciudad));
                cmd.Parameters.Add(new SqlParameter("@iHoras", registro.Horas));
                cmd.Parameters.Add(new SqlParameter("@bEstado", registro.Estado));
                cmd.Parameters.Add(new SqlParameter("@fArchivo", registro.FileArchivo));
                cmd.Parameters.Add(new SqlParameter("@iTipoEstudio", registro.TipoEstudio));
                cmd.Parameters.Add(new SqlParameter("@iTipoEstudioOtro", registro.TipoEstudioOtro));
                cmd.Parameters.Add(new SqlParameter("@iNivelTipoEstudioOtro", registro.NivelTipoEstudioOtro));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdPostulante;
        }
        public Int32 ActualizarPostulanteCapacitacion(PostulanteCapacitacion_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulanteCapacitacion_upd]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", registro.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@iCodCapacitacion", registro.IdCapacitacion));
                cmd.Parameters.Add(new SqlParameter("@vEspecialidad", registro.Especialidad.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@vInstitucion", registro.Institucion.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@dFechaInicio", DateTime.Parse(registro.FechaInicio)));
                cmd.Parameters.Add(new SqlParameter("@dFechaFin", DateTime.Parse(registro.FechaFin)));
                cmd.Parameters.Add(new SqlParameter("@vCiudad", registro.Ciudad));
                cmd.Parameters.Add(new SqlParameter("@iHoras", registro.Horas));
                cmd.Parameters.Add(new SqlParameter("@bEstado", registro.Estado));
                cmd.Parameters.Add(new SqlParameter("@fArchivo", registro.FileArchivo));
                cmd.Parameters.Add(new SqlParameter("@iTipoEstudio", registro.TipoEstudio));
                cmd.Parameters.Add(new SqlParameter("@iTipoEstudioOtro", registro.TipoEstudioOtro));
                cmd.Parameters.Add(new SqlParameter("@iNivelTipoEstudioOtro", registro.NivelTipoEstudioOtro));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdPostulante;
        }
        public Int32 EliminarPostulanteCapacitacion(PostulanteCapacitacion_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulanteCapacitacion_del]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", registro.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@iCodCapacitacion", registro.IdCapacitacion));
                
                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdPostulante;
        }
        public Int32 RegistrarPostulanteExperiencia(PostulanteExperiencia_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulanteExperienciaLaboral_ins]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", registro.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@vEmpresa", registro.Empresa.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@vCargo", registro.Cargo.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@dFechaInicio", DateTime.Parse(registro.FechaInicio)));
                cmd.Parameters.Add(new SqlParameter("@dFechaFin", DateTime.Parse(registro.FechaFin)));
                cmd.Parameters.Add(new SqlParameter("@vDescripcion", registro.Descripcion));
                cmd.Parameters.Add(new SqlParameter("@bEstado", registro.Estado));
                cmd.Parameters.Add(new SqlParameter("@fArchivo", registro.FileArchivo));
                cmd.Parameters.Add(new SqlParameter("@iSector", registro.Sector));
                cmd.Parameters.Add(new SqlParameter("@iRegimen", registro.Regimen));
                cmd.Parameters.Add(new SqlParameter("@vNombreJefeDirecto", registro.NombreJefeDirecto));
                cmd.Parameters.Add(new SqlParameter("@vPuestoJefeDirecto", registro.PuestoJefeDirecto));
                cmd.Parameters.Add(new SqlParameter("@vMotivoCambio", registro.MotivoCambio));
                cmd.Parameters.Add(new SqlParameter("@mnRemuneracion", registro.Remuneracion));
                cmd.Parameters.Add(new SqlParameter("@vRefLaboralNombre", registro.ReferenciaNombre));
                cmd.Parameters.Add(new SqlParameter("@vRefLaboralPuesto", registro.ReferenciaPuesto));
                cmd.Parameters.Add(new SqlParameter("@vRefLaboralTelefono", registro.ReferenciaTelefono));
                cmd.Parameters.Add(new SqlParameter("@vRefLaboralCorreo", registro.ReferenciaCorreo));


                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdPostulante;
        }
        public Int32 ActualizarPostulanteExperiencia(PostulanteExperiencia_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulanteExperienciaLaboral_upd]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", registro.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@iCodLaboral", registro.IdLaboral));
                cmd.Parameters.Add(new SqlParameter("@vEmpresa", registro.Empresa.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@vCargo", registro.Cargo.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@dFechaInicio", DateTime.Parse(registro.FechaInicio)));
                cmd.Parameters.Add(new SqlParameter("@dFechaFin", DateTime.Parse(registro.FechaFin)));
                cmd.Parameters.Add(new SqlParameter("@vDescripcion", registro.Descripcion));
                cmd.Parameters.Add(new SqlParameter("@bEstado", registro.Estado));
                cmd.Parameters.Add(new SqlParameter("@fArchivo", registro.FileArchivo));
                cmd.Parameters.Add(new SqlParameter("@iSector", registro.Sector));
                cmd.Parameters.Add(new SqlParameter("@iRegimen", registro.Regimen));
                cmd.Parameters.Add(new SqlParameter("@vNombreJefeDirecto", registro.NombreJefeDirecto));
                cmd.Parameters.Add(new SqlParameter("@vPuestoJefeDirecto", registro.PuestoJefeDirecto));
                cmd.Parameters.Add(new SqlParameter("@vMotivoCambio", registro.MotivoCambio));
                cmd.Parameters.Add(new SqlParameter("@mnRemuneracion", registro.Remuneracion));
                cmd.Parameters.Add(new SqlParameter("@vRefLaboralNombre", registro.ReferenciaNombre));
                cmd.Parameters.Add(new SqlParameter("@vRefLaboralPuesto", registro.ReferenciaPuesto));
                cmd.Parameters.Add(new SqlParameter("@vRefLaboralTelefono", registro.ReferenciaTelefono));
                cmd.Parameters.Add(new SqlParameter("@vRefLaboralCorreo", registro.ReferenciaCorreo));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdPostulante;
        }
        public Int32 EliminarPostulanteExperiencia(PostulanteExperiencia_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulanteExperienciaLaboral_del]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", registro.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@iCodLaboral", registro.IdLaboral));
                
                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdPostulante;
        }
        public Int32 EliminarPostulanteDocumento(PostulanteDocumento_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulanteDocumento_del]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", registro.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoDocumento", registro.IdDocumento));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdPostulante;
        }
        public Int32 ActualizarPostulanteFamiliar(PostulanteFamiliar_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[usp_PostulanteFamiliar_upd]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", registro.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@iCodPostulacion", registro.IdPostulacion));
                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));
                cmd.Parameters.Add(new SqlParameter("@iCodFamiliar", registro.IdFamiliar));
                cmd.Parameters.Add(new SqlParameter("@vNombres", registro.Nombre));
                cmd.Parameters.Add(new SqlParameter("@iParentesco", registro.IdParentesco));
                cmd.Parameters.Add(new SqlParameter("@vNumeroDocumento", registro.NroDocumento));
                cmd.Parameters.Add(new SqlParameter("@iEdad", registro.Edad));
                cmd.Parameters.Add(new SqlParameter("@dtFechaNacimiento", registro.FechaNacimiento));
                cmd.Parameters.Add(new SqlParameter("@vOcupacion", (!String.IsNullOrEmpty(registro.Ocupacion) ? registro.Ocupacion.ToUpper() : String.Empty)));
                cmd.Parameters.Add(new SqlParameter("@vSexo", registro.IdSexo));
                cmd.Parameters.Add(new SqlParameter("@bEstado", 1));
                cmd.Parameters.Add(new SqlParameter("@vAuditModificacion", registro.IdUsuarioModificacion));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdPostulante;
        }
        public Int32 EliminarPostulanteFamiliar(PostulanteFamiliar_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_PostulanteFamiliar_del]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPostulante", registro.IdPostulante));
                cmd.Parameters.Add(new SqlParameter("@iCodPostulacion", registro.IdPostulacion));
                cmd.Parameters.Add(new SqlParameter("@iCodConvocatoria", registro.IdConvocatoria));
                cmd.Parameters.Add(new SqlParameter("@iCodFamiliar", registro.IdFamiliar));
                
                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdPostulante;
        }
        #endregion

        
    }
}
