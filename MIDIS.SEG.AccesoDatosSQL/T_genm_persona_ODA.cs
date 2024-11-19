/*----------------------------------------------------------------------------------------
ARCHIVO CLASE   : T_genm_permiso_ODA

Objetivo: Clase referida a los métodos de Acceso a datos de la clase T_genm_permiso
Autor: Miguel Angel Salvador Paucar (MASP)
Fecha Creacion: 2015-09-03
Metodos: 
        Insertar_T_genm_permiso
        Actualizar_T_genm_permiso
        Listar_T_genm_permiso
        Anular_T_genm_permiso_PorCodigo
        Recuperar_T_genm_permiso_PorCodigo
        ListarPaginado_T_genm_permiso

----------------------------------------------------------------------------------------*/
#region Espacio de Nombres
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using MIDIS.ORI.Entidades;
#endregion

namespace MIDIS.SEG.AccesoDatosSQL
{
    public partial class T_genm_persona_ODA
    {
        BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWork = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnOracleM"]].ConnectionString));


        public IEnumerable<Persona_Response> Listar(Persona_Request peticion)
        {
            List<Persona_Response> lista = new List<Persona_Response>();
            Persona_Response item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[USP_PERSONA_SEL_LISTAR]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                if (peticion.IdPersona.HasValue) cmd.Parameters.Add(new SqlParameter("@ID_PERSONA", peticion.IdPersona.Value));
                if (peticion.TieneDocumento.HasValue) cmd.Parameters.Add(new SqlParameter("@TIENE_DOCUMENTO", peticion.TieneDocumento.Value));
                if (peticion.TipoDeDocumento != null) cmd.Parameters.Add(new SqlParameter("@TIPO_DE_DOCUMENTO", peticion.TipoDeDocumento));
                if (peticion.NumeroDeDocumento != null) cmd.Parameters.Add(new SqlParameter("@NUMERO_DE_DOCUMENTO", peticion.NumeroDeDocumento));
                if (peticion.DniValidoEnReniec.HasValue) cmd.Parameters.Add(new SqlParameter("@DNI_VALIDO_EN_RENIEC", peticion.DniValidoEnReniec.Value));
                if (peticion.Nombres != null) cmd.Parameters.Add(new SqlParameter("@NOMBRES", peticion.Nombres));
                if (peticion.ApellidoPaterno != null) cmd.Parameters.Add(new SqlParameter("@APELLIDO_PATERNO", peticion.ApellidoPaterno));
                if (peticion.ApellidoMaterno != null) cmd.Parameters.Add(new SqlParameter("@APELLIDO_MATERNO", peticion.ApellidoMaterno));
                if (peticion.Sexo != null) cmd.Parameters.Add(new SqlParameter("@SEXO", peticion.Sexo));
                if (peticion.FechaDeNacimientoEntreDesde.HasValue) cmd.Parameters.Add(new SqlParameter("@FECHA_DE_NACIMIENTO_ENTRE_DESDE", peticion.FechaDeNacimientoEntreDesde.Value));
                if (peticion.FechaDeNacimientoEntreHasta.HasValue) cmd.Parameters.Add(new SqlParameter("@FECHA_DE_NACIMIENTO_ENTRE_HASTA", peticion.FechaDeNacimientoEntreHasta.Value));
                if (peticion.IdUbigeo.HasValue) cmd.Parameters.Add(new SqlParameter("@ID_UBIGEO", peticion.IdUbigeo.Value));
                if (peticion.IdCentroPoblado.HasValue) cmd.Parameters.Add(new SqlParameter("@ID_CENTRO_POBLADO", peticion.IdCentroPoblado.Value));
                if (peticion.IdUbigeoDomicilio.HasValue) cmd.Parameters.Add(new SqlParameter("@ID_UBIGEO_DOMICILIO", peticion.IdUbigeoDomicilio.Value));
                if (peticion.DireccionDomicilio != null) cmd.Parameters.Add(new SqlParameter("@DIRECCION_DOMICILIO", peticion.DireccionDomicilio));
                if (peticion.DomicilioEstaConfirmado.HasValue) cmd.Parameters.Add(new SqlParameter("@DOMICILIO_ESTA_CONFIRMADO", peticion.DomicilioEstaConfirmado.Value));
                if (peticion.IdCentroPobladoDomicilio.HasValue) cmd.Parameters.Add(new SqlParameter("@ID_CENTRO_POBLADO_DOMICILIO", peticion.IdCentroPobladoDomicilio.Value));
                if (peticion.RegistroUsuarioCreacion.HasValue) cmd.Parameters.Add(new SqlParameter("@REGISTRO_USUARIO_CREACION", peticion.RegistroUsuarioCreacion.Value));
                if (peticion.RegistroUsuarioModificacion.HasValue) cmd.Parameters.Add(new SqlParameter("@REGISTRO_USUARIO_MODIFICACION", peticion.RegistroUsuarioModificacion.Value));
                if (peticion.RegistroFechaCreacionEntreDesde.HasValue) cmd.Parameters.Add(new SqlParameter("@REGISTRO_FECHA_CREACION_ENTRE_DESDE", peticion.RegistroFechaCreacionEntreDesde.Value));
                if (peticion.RegistroFechaCreacionEntreHasta.HasValue) cmd.Parameters.Add(new SqlParameter("@REGISTRO_FECHA_CREACION_ENTRE_HASTA", peticion.RegistroFechaCreacionEntreHasta.Value));
                if (peticion.RegistroFechaModificacionEntreDesde.HasValue) cmd.Parameters.Add(new SqlParameter("@REGISTRO_FECHA_MODIFICACION_ENTRE_DESDE", peticion.RegistroFechaModificacionEntreDesde.Value));
                if (peticion.RegistroFechaModificacionEntreHasta.HasValue) cmd.Parameters.Add(new SqlParameter("@REGISTRO_FECHA_MODIFICACION_ENTRE_HASTA", peticion.RegistroFechaModificacionEntreHasta.Value));
                if (peticion.RegistroIpCreacion != null) cmd.Parameters.Add(new SqlParameter("@REGISTRO_IP_CREACION", peticion.RegistroIpCreacion));
                if (peticion.RegistroIpModificacion != null) cmd.Parameters.Add(new SqlParameter("@REGISTRO_IP_MODIFICACION", peticion.RegistroIpModificacion));
                if (peticion.RegistroEstaActivo.HasValue) cmd.Parameters.Add(new SqlParameter("@REGISTRO_ESTA_ACTIVO", peticion.RegistroEstaActivo.Value));
                if (peticion.RegistroEstaEliminado.HasValue) cmd.Parameters.Add(new SqlParameter("@REGISTRO_ESTA_ELIMINADO", peticion.RegistroEstaEliminado.Value));
                if (peticion.Ubigeo != null)
                {
                    if (peticion.Ubigeo.CodigoInei != null) cmd.Parameters.Add(new SqlParameter("@UBIGEO_CODIGO_INEI", peticion.Ubigeo.CodigoInei));
                    if (peticion.Ubigeo.CodigoReniec != null) cmd.Parameters.Add(new SqlParameter("@UBIGEO_CODIGO_RENIEC", peticion.Ubigeo.CodigoReniec));
                    if (peticion.Ubigeo.Departamento != null) cmd.Parameters.Add(new SqlParameter("@UBIGEO_DEPARTAMENTO", peticion.Ubigeo.Departamento));
                    if (peticion.Ubigeo.Provincia != null) cmd.Parameters.Add(new SqlParameter("@UBIGEO_PROVINCIA", peticion.Ubigeo.Provincia));
                    if (peticion.Ubigeo.Distrito != null) cmd.Parameters.Add(new SqlParameter("@UBIGEO_DISTRITO", peticion.Ubigeo.Distrito));
                }
                if (peticion.Grilla != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@REGISTROS_POR_PAGINA", peticion.Grilla.RegistrosPorPagina));
                    cmd.Parameters.Add(new SqlParameter("@PAGINA_ACTUAL", peticion.Grilla.PaginaActual));
                    cmd.Parameters.Add(new SqlParameter("@ORDENAR_POR", peticion.Grilla.OrdenarPor));
                    cmd.Parameters.Add(new SqlParameter("@ORDENAR_DE_FORMA", peticion.Grilla.OrdenarDeForma));
                }

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Persona_Response();

                            item.Grilla = new Grilla_Response();
                            item.Grilla.NumeroDeFila = dr.GetInt64(dr.GetOrdinal("NUMERO_FILA"));
                            item.Grilla.TotalDeRegistros = dr.GetInt32(dr.GetOrdinal("TOTAL_FILAS"));

                            item.IdPersona = dr.GetInt32(dr.GetOrdinal("ID_PERSONA"));
                            if (!dr.IsDBNull(dr.GetOrdinal("TIENE_DOCUMENTO"))) item.TieneDocumento = dr.GetBoolean(dr.GetOrdinal("TIENE_DOCUMENTO"));
                            item.TipoDeDocumento = dr.GetString(dr.GetOrdinal("TIPO_DE_DOCUMENTO"));
                            if (!dr.IsDBNull(dr.GetOrdinal("DSC_TIPO_DE_DOCUMENTO"))) item.DscTipoDeDocumento = dr.GetString(dr.GetOrdinal("DSC_TIPO_DE_DOCUMENTO")); item.NumeroDeDocumento = dr.GetString(dr.GetOrdinal("NUMERO_DE_DOCUMENTO"));
                            if (!dr.IsDBNull(dr.GetOrdinal("DNI_VALIDO_EN_RENIEC"))) item.DniValidoEnReniec = dr.GetBoolean(dr.GetOrdinal("DNI_VALIDO_EN_RENIEC"));
                            item.Nombres = dr.GetString(dr.GetOrdinal("NOMBRES"));
                            item.ApellidoPaterno = dr.GetString(dr.GetOrdinal("APELLIDO_PATERNO"));
                            item.ApellidoMaterno = dr.GetString(dr.GetOrdinal("APELLIDO_MATERNO"));
                            item.Sexo = dr.GetString(dr.GetOrdinal("SEXO"));
                            if (!dr.IsDBNull(dr.GetOrdinal("DSC_SEXO"))) item.DscSexo = dr.GetString(dr.GetOrdinal("DSC_SEXO")); if (!dr.IsDBNull(dr.GetOrdinal("FECHA_DE_NACIMIENTO"))) item.FechaDeNacimiento = dr.GetDateTime(dr.GetOrdinal("FECHA_DE_NACIMIENTO"));
                            if (!dr.IsDBNull(dr.GetOrdinal("ID_UBIGEO"))) item.IdUbigeo = dr.GetInt32(dr.GetOrdinal("ID_UBIGEO"));
                            if (!dr.IsDBNull(dr.GetOrdinal("ID_CENTRO_POBLADO"))) item.IdCentroPoblado = dr.GetInt32(dr.GetOrdinal("ID_CENTRO_POBLADO"));
                            if (!dr.IsDBNull(dr.GetOrdinal("ID_UBIGEO_DOMICILIO"))) item.IdUbigeoDomicilio = dr.GetInt32(dr.GetOrdinal("ID_UBIGEO_DOMICILIO"));
                            if (!dr.IsDBNull(dr.GetOrdinal("DIRECCION_DOMICILIO"))) item.DireccionDomicilio = dr.GetString(dr.GetOrdinal("DIRECCION_DOMICILIO"));
                            if (!dr.IsDBNull(dr.GetOrdinal("DOMICILIO_ESTA_CONFIRMADO"))) item.DomicilioEstaConfirmado = dr.GetBoolean(dr.GetOrdinal("DOMICILIO_ESTA_CONFIRMADO"));
                            if (!dr.IsDBNull(dr.GetOrdinal("ID_CENTRO_POBLADO_DOMICILIO"))) item.IdCentroPobladoDomicilio = dr.GetInt32(dr.GetOrdinal("ID_CENTRO_POBLADO_DOMICILIO"));
                            if (!dr.IsDBNull(dr.GetOrdinal("TELEFONO_FIJO"))) item.TelefonoFijo = dr.GetString(dr.GetOrdinal("TELEFONO_FIJO"));
                            if (!dr.IsDBNull(dr.GetOrdinal("TELEFONO_CELULAR"))) item.TelefonoCelular = dr.GetString(dr.GetOrdinal("TELEFONO_CELULAR"));
                            if (!dr.IsDBNull(dr.GetOrdinal("CORREO_ELECTRONICO"))) item.CorreoElectronico = dr.GetString(dr.GetOrdinal("CORREO_ELECTRONICO"));
                            item.RegistroEstaActivo = dr.GetBoolean(dr.GetOrdinal("REGISTRO_ESTA_ACTIVO"));
                            item.Ubigeo = new Ubigeo_Response();
                            if (!dr.IsDBNull(dr.GetOrdinal("UBIGEO_CODIGO_INEI"))) item.Ubigeo.CodigoInei = dr.GetString(dr.GetOrdinal("UBIGEO_CODIGO_INEI"));
                            if (!dr.IsDBNull(dr.GetOrdinal("UBIGEO_CODIGO_RENIEC"))) item.Ubigeo.CodigoReniec = dr.GetString(dr.GetOrdinal("UBIGEO_CODIGO_RENIEC"));
                            if (!dr.IsDBNull(dr.GetOrdinal("UBIGEO_DEPARTAMENTO"))) item.Ubigeo.Departamento = dr.GetString(dr.GetOrdinal("UBIGEO_DEPARTAMENTO"));
                            if (!dr.IsDBNull(dr.GetOrdinal("UBIGEO_PROVINCIA"))) item.Ubigeo.Provincia = dr.GetString(dr.GetOrdinal("UBIGEO_PROVINCIA"));
                            if (!dr.IsDBNull(dr.GetOrdinal("UBIGEO_DISTRITO"))) item.Ubigeo.Distrito = dr.GetString(dr.GetOrdinal("UBIGEO_DISTRITO"));
                            //item.CentroPoblado = new CentroPoblado_Response();
                            //if (!dr.IsDBNull(dr.GetOrdinal("CENTRO_POBLADO_CODIGO"))) item.CentroPoblado.Codigo = dr.GetString(dr.GetOrdinal("CENTRO_POBLADO_CODIGO"));
                            //if (!dr.IsDBNull(dr.GetOrdinal("CENTRO_POBLADO_NOMBRE"))) item.CentroPoblado.Nombre = dr.GetString(dr.GetOrdinal("CENTRO_POBLADO_NOMBRE"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<string> Validar(Persona_Registro registro)
        {
            List<string> lista = new List<string>();

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[USP_PERSONA_SEL_VALIDAR]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_PERSONA", registro.IdPersona));
                cmd.Parameters.Add(new SqlParameter("@TIENE_DOCUMENTO", registro.TieneDocumento));
                cmd.Parameters.Add(new SqlParameter("@TIPO_DE_DOCUMENTO", registro.TipoDeDocumento));
                cmd.Parameters.Add(new SqlParameter("@NUMERO_DE_DOCUMENTO", registro.NumeroDeDocumento));
                cmd.Parameters.Add(new SqlParameter("@DNI_VALIDO_EN_RENIEC", registro.DniValidoEnReniec));
                cmd.Parameters.Add(new SqlParameter("@NOMBRES", registro.Nombres));
                cmd.Parameters.Add(new SqlParameter("@APELLIDO_PATERNO", registro.ApellidoPaterno));
                cmd.Parameters.Add(new SqlParameter("@APELLIDO_MATERNO", registro.ApellidoMaterno));
                cmd.Parameters.Add(new SqlParameter("@SEXO", registro.Sexo));
                cmd.Parameters.Add(new SqlParameter("@FECHA_DE_NACIMIENTO", registro.FechaDeNacimiento));
                cmd.Parameters.Add(new SqlParameter("@ID_UBIGEO", registro.IdUbigeo));
                cmd.Parameters.Add(new SqlParameter("@ID_CENTRO_POBLADO", registro.IdCentroPoblado));
                cmd.Parameters.Add(new SqlParameter("@ID_UBIGEO_DOMICILIO", registro.IdUbigeoDomicilio));
                cmd.Parameters.Add(new SqlParameter("@DIRECCION_DOMICILIO", registro.DireccionDomicilio));
                cmd.Parameters.Add(new SqlParameter("@DOMICILIO_ESTA_CONFIRMADO", registro.DomicilioEstaConfirmado));
                cmd.Parameters.Add(new SqlParameter("@ID_CENTRO_POBLADO_DOMICILIO", registro.IdCentroPobladoDomicilio));
                cmd.Parameters.Add(new SqlParameter("@REGISTRO_USUARIO_CREACION", registro.RegistroUsuarioCreacion));
                cmd.Parameters.Add(new SqlParameter("@REGISTRO_FECHA_CREACION", registro.RegistroFechaCreacion));
                cmd.Parameters.Add(new SqlParameter("@REGISTRO_IP_CREACION", registro.RegistroIpCreacion));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            lista.Add(dr.GetString(dr.GetOrdinal("MENSAJE")));
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return lista;
        }

        public Int32 Insertar(Persona_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[USP_PERSONA_INS]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                SqlParameter IdPersonaParameter = new SqlParameter("@ID_PERSONA", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(IdPersonaParameter);
                cmd.Parameters.Add(new SqlParameter("@TIENE_DOCUMENTO", registro.TieneDocumento));
                cmd.Parameters.Add(new SqlParameter("@TIPO_DE_DOCUMENTO", registro.TipoDeDocumento));
                cmd.Parameters.Add(new SqlParameter("@NUMERO_DE_DOCUMENTO", registro.NumeroDeDocumento));
                cmd.Parameters.Add(new SqlParameter("@DNI_VALIDO_EN_RENIEC", registro.DniValidoEnReniec));
                cmd.Parameters.Add(new SqlParameter("@NOMBRES", registro.Nombres));
                cmd.Parameters.Add(new SqlParameter("@APELLIDO_PATERNO", registro.ApellidoPaterno));
                cmd.Parameters.Add(new SqlParameter("@APELLIDO_MATERNO", registro.ApellidoMaterno));
                cmd.Parameters.Add(new SqlParameter("@SEXO", registro.Sexo));
                cmd.Parameters.Add(new SqlParameter("@FECHA_DE_NACIMIENTO", registro.FechaDeNacimiento));
                cmd.Parameters.Add(new SqlParameter("@ID_UBIGEO", registro.IdUbigeo));
                cmd.Parameters.Add(new SqlParameter("@ID_CENTRO_POBLADO", registro.IdCentroPoblado));
                cmd.Parameters.Add(new SqlParameter("@ID_UBIGEO_DOMICILIO", registro.IdUbigeoDomicilio));
                cmd.Parameters.Add(new SqlParameter("@DIRECCION_DOMICILIO", registro.DireccionDomicilio));
                cmd.Parameters.Add(new SqlParameter("@DOMICILIO_ESTA_CONFIRMADO", registro.DomicilioEstaConfirmado));
                cmd.Parameters.Add(new SqlParameter("@TELEFONO_FIJO", registro.TelefonoFijo));
                cmd.Parameters.Add(new SqlParameter("@TELEFONO_CELULAR", registro.TelefonoCelular));
                cmd.Parameters.Add(new SqlParameter("@CORREO_ELECTRONICO", registro.CorreoElectronico));
                cmd.Parameters.Add(new SqlParameter("@ID_CENTRO_POBLADO_DOMICILIO", registro.IdCentroPobladoDomicilio));
                cmd.Parameters.Add(new SqlParameter("@REGISTRO_USUARIO_CREACION", registro.RegistroUsuarioCreacion));
                cmd.Parameters.Add(new SqlParameter("@REGISTRO_FECHA_CREACION", registro.RegistroFechaCreacion));
                cmd.Parameters.Add(new SqlParameter("@REGISTRO_IP_CREACION", registro.RegistroIpCreacion));

                cmd.ExecuteNonQuery();

                registro.IdPersona = Int32.Parse(IdPersonaParameter.Value.ToString());

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdPersona;
        }

        public void Actualizar(Persona_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[USP_PERSONA_UPD]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_PERSONA", registro.IdPersona));
                cmd.Parameters.Add(new SqlParameter("@TIENE_DOCUMENTO", registro.TieneDocumento));
                cmd.Parameters.Add(new SqlParameter("@TIPO_DE_DOCUMENTO", registro.TipoDeDocumento));
                cmd.Parameters.Add(new SqlParameter("@NUMERO_DE_DOCUMENTO", registro.NumeroDeDocumento));
                cmd.Parameters.Add(new SqlParameter("@DNI_VALIDO_EN_RENIEC", registro.DniValidoEnReniec));
                cmd.Parameters.Add(new SqlParameter("@NOMBRES", registro.Nombres));
                cmd.Parameters.Add(new SqlParameter("@APELLIDO_PATERNO", registro.ApellidoPaterno));
                cmd.Parameters.Add(new SqlParameter("@APELLIDO_MATERNO", registro.ApellidoMaterno));
                cmd.Parameters.Add(new SqlParameter("@SEXO", registro.Sexo));
                cmd.Parameters.Add(new SqlParameter("@FECHA_DE_NACIMIENTO", registro.FechaDeNacimiento));
                cmd.Parameters.Add(new SqlParameter("@ID_UBIGEO", registro.IdUbigeo));
                cmd.Parameters.Add(new SqlParameter("@ID_CENTRO_POBLADO", registro.IdCentroPoblado));
                cmd.Parameters.Add(new SqlParameter("@ID_UBIGEO_DOMICILIO", registro.IdUbigeoDomicilio));
                cmd.Parameters.Add(new SqlParameter("@DIRECCION_DOMICILIO", registro.DireccionDomicilio));
                cmd.Parameters.Add(new SqlParameter("@DOMICILIO_ESTA_CONFIRMADO", registro.DomicilioEstaConfirmado));
                cmd.Parameters.Add(new SqlParameter("@TELEFONO_FIJO", registro.TelefonoFijo));
                cmd.Parameters.Add(new SqlParameter("@TELEFONO_CELULAR", registro.TelefonoCelular));
                cmd.Parameters.Add(new SqlParameter("@CORREO_ELECTRONICO", registro.CorreoElectronico));
                cmd.Parameters.Add(new SqlParameter("@ID_CENTRO_POBLADO_DOMICILIO", registro.IdCentroPobladoDomicilio));

                cmd.Parameters.Add(new SqlParameter("@REGISTRO_USUARIO_MODIFICACION", registro.RegistroUsuarioModificacion));
                cmd.Parameters.Add(new SqlParameter("@REGISTRO_FECHA_MODIFICACION", registro.RegistroFechaModificacion));
                cmd.Parameters.Add(new SqlParameter("@REGISTRO_IP_MODIFICACION", registro.RegistroIpModificacion));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }
        }

        public Persona_Registro Obtener(Persona_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[USP_PERSONA_SEL]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_PERSONA", registro.IdPersona));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            registro.IdPersona = dr.GetInt32(dr.GetOrdinal("ID_PERSONA"));
                            if (!dr.IsDBNull(dr.GetOrdinal("TIENE_DOCUMENTO"))) registro.TieneDocumento = dr.GetBoolean(dr.GetOrdinal("TIENE_DOCUMENTO"));
                            registro.TipoDeDocumento = dr.GetString(dr.GetOrdinal("TIPO_DE_DOCUMENTO"));
                            registro.NumeroDeDocumento = dr.GetString(dr.GetOrdinal("NUMERO_DE_DOCUMENTO"));
                            if (!dr.IsDBNull(dr.GetOrdinal("DNI_VALIDO_EN_RENIEC"))) registro.DniValidoEnReniec = dr.GetBoolean(dr.GetOrdinal("DNI_VALIDO_EN_RENIEC"));
                            registro.Nombres = dr.GetString(dr.GetOrdinal("NOMBRES"));
                            registro.ApellidoPaterno = dr.GetString(dr.GetOrdinal("APELLIDO_PATERNO"));
                            registro.ApellidoMaterno = dr.GetString(dr.GetOrdinal("APELLIDO_MATERNO"));
                            registro.Sexo = dr.GetString(dr.GetOrdinal("SEXO"));
                            if (!dr.IsDBNull(dr.GetOrdinal("FECHA_DE_NACIMIENTO"))) registro.FechaDeNacimiento = dr.GetDateTime(dr.GetOrdinal("FECHA_DE_NACIMIENTO"));
                            if (!dr.IsDBNull(dr.GetOrdinal("ID_UBIGEO"))) registro.IdUbigeo = dr.GetInt32(dr.GetOrdinal("ID_UBIGEO"));
                            if (!dr.IsDBNull(dr.GetOrdinal("ID_CENTRO_POBLADO"))) registro.IdCentroPoblado = dr.GetInt32(dr.GetOrdinal("ID_CENTRO_POBLADO"));
                            if (!dr.IsDBNull(dr.GetOrdinal("ID_UBIGEO_DOMICILIO"))) registro.IdUbigeoDomicilio = dr.GetInt32(dr.GetOrdinal("ID_UBIGEO_DOMICILIO"));
                            if (!dr.IsDBNull(dr.GetOrdinal("DIRECCION_DOMICILIO"))) registro.DireccionDomicilio = dr.GetString(dr.GetOrdinal("DIRECCION_DOMICILIO"));
                            if (!dr.IsDBNull(dr.GetOrdinal("DOMICILIO_ESTA_CONFIRMADO"))) registro.DomicilioEstaConfirmado = dr.GetBoolean(dr.GetOrdinal("DOMICILIO_ESTA_CONFIRMADO"));
                            if (!dr.IsDBNull(dr.GetOrdinal("ID_CENTRO_POBLADO_DOMICILIO"))) registro.IdCentroPobladoDomicilio = dr.GetInt32(dr.GetOrdinal("ID_CENTRO_POBLADO_DOMICILIO"));
                            registro.RegistroUsuarioCreacion = dr.GetInt32(dr.GetOrdinal("REGISTRO_USUARIO_CREACION"));
                            if (!dr.IsDBNull(dr.GetOrdinal("REGISTRO_USUARIO_MODIFICACION"))) registro.RegistroUsuarioModificacion = dr.GetInt32(dr.GetOrdinal("REGISTRO_USUARIO_CREACION"));
                            registro.RegistroFechaCreacion = dr.GetDateTime(dr.GetOrdinal("REGISTRO_FECHA_CREACION"));
                            if (!dr.IsDBNull(dr.GetOrdinal("REGISTRO_FECHA_MODIFICACION"))) registro.RegistroFechaModificacion = dr.GetDateTime(dr.GetOrdinal("REGISTRO_FECHA_MODIFICACION"));
                            registro.RegistroIpCreacion = dr.GetString(dr.GetOrdinal("REGISTRO_IP_CREACION"));
                            if (!dr.IsDBNull(dr.GetOrdinal("REGISTRO_IP_MODIFICACION"))) registro.RegistroIpModificacion = dr.GetString(dr.GetOrdinal("REGISTRO_IP_MODIFICACION"));
                            registro.RegistroEstaActivo = dr.GetBoolean(dr.GetOrdinal("REGISTRO_ESTA_ACTIVO"));
                            registro.RegistroEstaEliminado = dr.GetBoolean(dr.GetOrdinal("REGISTRO_ESTA_ELIMINADO"));
                            registro.Ubigeo = new Ubigeo_Registro();
                            if (!dr.IsDBNull(dr.GetOrdinal("UBIGEO_CODIGO_INEI"))) registro.Ubigeo.CodigoInei = dr.GetString(dr.GetOrdinal("UBIGEO_CODIGO_INEI"));
                            if (!dr.IsDBNull(dr.GetOrdinal("UBIGEO_CODIGO_RENIEC"))) registro.Ubigeo.CodigoReniec = dr.GetString(dr.GetOrdinal("UBIGEO_CODIGO_RENIEC"));
                            if (!dr.IsDBNull(dr.GetOrdinal("UBIGEO_DEPARTAMENTO"))) registro.Ubigeo.Departamento = dr.GetString(dr.GetOrdinal("UBIGEO_DEPARTAMENTO"));
                            if (!dr.IsDBNull(dr.GetOrdinal("UBIGEO_PROVINCIA"))) registro.Ubigeo.Provincia = dr.GetString(dr.GetOrdinal("UBIGEO_PROVINCIA"));
                            if (!dr.IsDBNull(dr.GetOrdinal("UBIGEO_DISTRITO"))) registro.Ubigeo.Distrito = dr.GetString(dr.GetOrdinal("UBIGEO_DISTRITO"));
                            //registro.CentroPoblado = new CentroPoblado_Registro();
                            //if (!dr.IsDBNull(dr.GetOrdinal("CENTRO_POBLADO_CODIGO"))) registro.CentroPoblado.Codigo = dr.GetString(dr.GetOrdinal("CENTRO_POBLADO_CODIGO"));
                            //if (!dr.IsDBNull(dr.GetOrdinal("CENTRO_POBLADO_NOMBRE"))) registro.CentroPoblado.Nombre = dr.GetString(dr.GetOrdinal("CENTRO_POBLADO_NOMBRE"));
                        }
                    }
                    else
                        registro = null;
                }
            }

            return registro;
        }

        public void Eliminar(Persona_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[USP_PERSONA_DEL]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_PERSONA", registro.IdPersona));

                cmd.ExecuteReader();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }
        }

        public void ActualizarEstaActivo(Persona_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[USP_PERSONA_UPD_ESTA_ACTIVO]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_PERSONA", registro.IdPersona));
                cmd.Parameters.Add(new SqlParameter("@REGISTRO_ESTA_ACTIVO", registro.RegistroEstaActivo));

                cmd.Parameters.Add(new SqlParameter("@REGISTRO_USUARIO_MODIFICACION", registro.RegistroUsuarioModificacion));
                cmd.Parameters.Add(new SqlParameter("@REGISTRO_FECHA_MODIFICACION", registro.RegistroFechaModificacion));
                cmd.Parameters.Add(new SqlParameter("@REGISTRO_IP_MODIFICACION", registro.RegistroIpModificacion));

                cmd.ExecuteReader();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }
        }

        public void ActualizarEstaEliminado(Persona_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[USP_PERSONA_UPD_ESTA_ELIMINADO]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_PERSONA", registro.IdPersona));
                cmd.Parameters.Add(new SqlParameter("@REGISTRO_ESTA_ELIMINADO", registro.RegistroEstaEliminado));

                cmd.Parameters.Add(new SqlParameter("@REGISTRO_USUARIO_MODIFICACION", registro.RegistroUsuarioModificacion));
                cmd.Parameters.Add(new SqlParameter("@REGISTRO_FECHA_MODIFICACION", registro.RegistroFechaModificacion));
                cmd.Parameters.Add(new SqlParameter("@REGISTRO_IP_MODIFICACION", registro.RegistroIpModificacion));

                cmd.ExecuteReader();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }
        }


        public IEnumerable<Banco_Registro> ListarBancos(Banco_Request peticion)
        {
            List<Banco_Registro> lista = new List<Banco_Registro>();
            Banco_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[USP_MAEBANCO_SEL]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_BANCO", peticion.IdBanco));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Banco_Registro();
                            item.Grilla = new Grilla_Response();

                            item.IdBanco = dr.GetInt32(dr.GetOrdinal("ICODIGOBANCO"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("VDESCRIPCION"));
                            
                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<Perfil_Nivel> ListarPerfilNivel()
        {
            List<Perfil_Nivel> lista = new List<Perfil_Nivel>();
            Perfil_Nivel item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paListaPerfilNivel]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Perfil_Nivel();
                            item.Grilla = new Grilla_Response();

                            item.IdNivel = dr.GetInt32(dr.GetOrdinal("iCodNivel"));
                            item.Perfil = dr.GetString(dr.GetOrdinal("vDescripcion"));

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
