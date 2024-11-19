/*----------------------------------------------------------------------------------------
ARCHIVO CLASE   : T_gend_empresa_aplicacion_item_ODA

Objetivo: Clase referida a los métodos de Acceso a datos de la clase T_gend_empresa_aplicacion_item
Autor: Miguel Angel Salvador Paucar (MASP)
Fecha Creacion: 2015-09-03
Metodos: 
        Insertar_T_gend_empresa_aplicacion_item
        Actualizar_T_gend_empresa_aplicacion_item
        Listar_T_gend_empresa_aplicacion_item
        Anular_T_gend_empresa_aplicacion_item_PorCodigo
        Recuperar_T_gend_empresa_aplicacion_item_PorCodigo
        ListarPaginado_T_gend_empresa_aplicacion_item

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
    public partial class T_genm_ubigeo_ODA
    {
        //string vSqlString = new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnOracle"]].ConnectionString);
        BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWorkM = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnOracleM"]].ConnectionString));

        public IEnumerable<Ubigeo_Response> Listar(Ubigeo_Request peticion)
        {
            List<Ubigeo_Response> lista = new List<Ubigeo_Response>();
            Ubigeo_Response item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[USP_UBIGEO_SEL_LISTAR]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                if (peticion.IdUbigeo.HasValue) cmd.Parameters.Add(new SqlParameter("@ID_UBIGEO", peticion.IdUbigeo.Value));
                if (peticion.CodigoInei != null) cmd.Parameters.Add(new SqlParameter("@CODIGO_INEI", peticion.CodigoInei));
                if (peticion.CodigoReniec != null) cmd.Parameters.Add(new SqlParameter("@CODIGO_RENIEC", peticion.CodigoReniec));
                if (peticion.Departamento != null) cmd.Parameters.Add(new SqlParameter("@DEPARTAMENTO", peticion.Departamento));
                if (peticion.Provincia != null) cmd.Parameters.Add(new SqlParameter("@PROVINCIA", peticion.Provincia));
                if (peticion.Distrito != null) cmd.Parameters.Add(new SqlParameter("@DISTRITO", peticion.Distrito));
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
                            item = new Ubigeo_Response();

                            item.Grilla = new Grilla_Response();
                            item.Grilla.NumeroDeFila = dr.GetInt64(dr.GetOrdinal("NUMERO_FILA"));
                            item.Grilla.TotalDeRegistros = dr.GetInt32(dr.GetOrdinal("TOTAL_FILAS"));

                            item.IdUbigeo = dr.GetInt32(dr.GetOrdinal("ID_UBIGEO"));
                            if (!dr.IsDBNull(dr.GetOrdinal("CODIGO_INEI"))) item.CodigoInei = dr.GetString(dr.GetOrdinal("CODIGO_INEI"));
                            if (!dr.IsDBNull(dr.GetOrdinal("CODIGO_RENIEC"))) item.CodigoReniec = dr.GetString(dr.GetOrdinal("CODIGO_RENIEC"));
                            item.Departamento = dr.GetString(dr.GetOrdinal("DEPARTAMENTO"));
                            if (!dr.IsDBNull(dr.GetOrdinal("PROVINCIA"))) item.Provincia = dr.GetString(dr.GetOrdinal("PROVINCIA"));
                            if (!dr.IsDBNull(dr.GetOrdinal("DISTRITO"))) item.Distrito = dr.GetString(dr.GetOrdinal("DISTRITO"));
                            item.RegistroEstaActivo = dr.GetBoolean(dr.GetOrdinal("REGISTRO_ESTA_ACTIVO"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<string> Validar(Ubigeo_Registro registro)
        {
            List<string> lista = new List<string>();

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[USP_UBIGEO_SEL_VALIDAR]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_UBIGEO", registro.IdUbigeo));
                cmd.Parameters.Add(new SqlParameter("@CODIGO_INEI", registro.CodigoInei));
                cmd.Parameters.Add(new SqlParameter("@CODIGO_RENIEC", registro.CodigoReniec));
                cmd.Parameters.Add(new SqlParameter("@DEPARTAMENTO", registro.Departamento));
                cmd.Parameters.Add(new SqlParameter("@PROVINCIA", registro.Provincia));
                cmd.Parameters.Add(new SqlParameter("@DISTRITO", registro.Distrito));
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

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return lista;
        }

        public Int32 Insertar(Ubigeo_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[USP_UBIGEO_INS]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                SqlParameter IdUbigeoParameter = new SqlParameter("@ID_UBIGEO", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(IdUbigeoParameter);
                cmd.Parameters.Add(new SqlParameter("@CODIGO_INEI", registro.CodigoInei));
                cmd.Parameters.Add(new SqlParameter("@CODIGO_RENIEC", registro.CodigoReniec));
                cmd.Parameters.Add(new SqlParameter("@DEPARTAMENTO", registro.Departamento));
                cmd.Parameters.Add(new SqlParameter("@PROVINCIA", registro.Provincia));
                cmd.Parameters.Add(new SqlParameter("@DISTRITO", registro.Distrito));
                cmd.Parameters.Add(new SqlParameter("@REGISTRO_USUARIO_CREACION", registro.RegistroUsuarioCreacion));
                cmd.Parameters.Add(new SqlParameter("@REGISTRO_FECHA_CREACION", registro.RegistroFechaCreacion));
                cmd.Parameters.Add(new SqlParameter("@REGISTRO_IP_CREACION", registro.RegistroIpCreacion));

                cmd.ExecuteNonQuery();

                registro.IdUbigeo = Int32.Parse(IdUbigeoParameter.Value.ToString());

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdUbigeo;
        }

        public void Actualizar(Ubigeo_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[USP_UBIGEO_UPD]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_UBIGEO", registro.IdUbigeo));
                cmd.Parameters.Add(new SqlParameter("@CODIGO_INEI", registro.CodigoInei));
                cmd.Parameters.Add(new SqlParameter("@CODIGO_RENIEC", registro.CodigoReniec));
                cmd.Parameters.Add(new SqlParameter("@DEPARTAMENTO", registro.Departamento));
                cmd.Parameters.Add(new SqlParameter("@PROVINCIA", registro.Provincia));
                cmd.Parameters.Add(new SqlParameter("@DISTRITO", registro.Distrito));

                cmd.Parameters.Add(new SqlParameter("@REGISTRO_USUARIO_MODIFICACION", registro.RegistroUsuarioModificacion));
                cmd.Parameters.Add(new SqlParameter("@REGISTRO_FECHA_MODIFICACION", registro.RegistroFechaModificacion));
                cmd.Parameters.Add(new SqlParameter("@REGISTRO_IP_MODIFICACION", registro.RegistroIpModificacion));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }
        }

        public Ubigeo_Registro Obtener(Ubigeo_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[USP_UBIGEO_SEL]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_UBIGEO", registro.IdUbigeo));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            registro.IdUbigeo = dr.GetInt32(dr.GetOrdinal("ID_UBIGEO"));
                            if (!dr.IsDBNull(dr.GetOrdinal("CODIGO_INEI"))) registro.CodigoInei = dr.GetString(dr.GetOrdinal("CODIGO_INEI"));
                            if (!dr.IsDBNull(dr.GetOrdinal("CODIGO_RENIEC"))) registro.CodigoReniec = dr.GetString(dr.GetOrdinal("CODIGO_RENIEC"));
                            registro.Departamento = dr.GetString(dr.GetOrdinal("DEPARTAMENTO"));
                            if (!dr.IsDBNull(dr.GetOrdinal("PROVINCIA"))) registro.Provincia = dr.GetString(dr.GetOrdinal("PROVINCIA"));
                            if (!dr.IsDBNull(dr.GetOrdinal("DISTRITO"))) registro.Distrito = dr.GetString(dr.GetOrdinal("DISTRITO"));
                            registro.RegistroUsuarioCreacion = dr.GetInt32(dr.GetOrdinal("REGISTRO_USUARIO_CREACION"));
                            if (!dr.IsDBNull(dr.GetOrdinal("REGISTRO_USUARIO_MODIFICACION"))) registro.RegistroUsuarioModificacion = dr.GetInt32(dr.GetOrdinal("REGISTRO_USUARIO_CREACION"));
                            registro.RegistroFechaCreacion = dr.GetDateTime(dr.GetOrdinal("REGISTRO_FECHA_CREACION"));
                            if (!dr.IsDBNull(dr.GetOrdinal("REGISTRO_FECHA_MODIFICACION"))) registro.RegistroFechaModificacion = dr.GetDateTime(dr.GetOrdinal("REGISTRO_FECHA_MODIFICACION"));
                            registro.RegistroIpCreacion = dr.GetString(dr.GetOrdinal("REGISTRO_IP_CREACION"));
                            if (!dr.IsDBNull(dr.GetOrdinal("REGISTRO_IP_MODIFICACION"))) registro.RegistroIpModificacion = dr.GetString(dr.GetOrdinal("REGISTRO_IP_MODIFICACION"));
                            registro.RegistroEstaActivo = dr.GetBoolean(dr.GetOrdinal("REGISTRO_ESTA_ACTIVO"));
                            registro.RegistroEstaEliminado = dr.GetBoolean(dr.GetOrdinal("REGISTRO_ESTA_ELIMINADO"));
                        }
                    }
                    else
                        registro = null;
                }
            }

            return registro;
        }

        public void Eliminar(Ubigeo_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[USP_UBIGEO_DEL]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_UBIGEO", registro.IdUbigeo));

                cmd.ExecuteReader();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }
        }

        public void ActualizarEstaActivo(Ubigeo_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[USP_UBIGEO_UPD_ESTA_ACTIVO]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_UBIGEO", registro.IdUbigeo));
                cmd.Parameters.Add(new SqlParameter("@REGISTRO_ESTA_ACTIVO", registro.RegistroEstaActivo));

                cmd.Parameters.Add(new SqlParameter("@REGISTRO_USUARIO_MODIFICACION", registro.RegistroUsuarioModificacion));
                cmd.Parameters.Add(new SqlParameter("@REGISTRO_FECHA_MODIFICACION", registro.RegistroFechaModificacion));
                cmd.Parameters.Add(new SqlParameter("@REGISTRO_IP_MODIFICACION", registro.RegistroIpModificacion));

                cmd.ExecuteReader();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }
        }

        public void ActualizarEstaEliminado(Ubigeo_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[USP_UBIGEO_UPD_ESTA_ELIMINADO]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_UBIGEO", registro.IdUbigeo));
                cmd.Parameters.Add(new SqlParameter("@REGISTRO_ESTA_ELIMINADO", registro.RegistroEstaEliminado));

                cmd.Parameters.Add(new SqlParameter("@REGISTRO_USUARIO_MODIFICACION", registro.RegistroUsuarioModificacion));
                cmd.Parameters.Add(new SqlParameter("@REGISTRO_FECHA_MODIFICACION", registro.RegistroFechaModificacion));
                cmd.Parameters.Add(new SqlParameter("@REGISTRO_IP_MODIFICACION", registro.RegistroIpModificacion));

                cmd.ExecuteReader();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }
        }

    }
}
