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
    public partial class T_genm_empleado_ODA
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

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                            strMensaje = dr.GetString(dr.GetOrdinal("MENSAJE"));
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return strMensaje;
        }

        public IEnumerable<Empleado_Registro> ListarEmpleados(Empleado_Request peticion)
        {
            List<Empleado_Registro> lista = new List<Empleado_Registro>();
            Empleado_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paListaTrabajador]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_TRABAJADOR", peticion.IdEmpleado));
                cmd.Parameters.Add(new SqlParameter("@ID_DEPENDENCIA", peticion.IdDependencia));
                cmd.Parameters.Add(new SqlParameter("@ID_CONDICION", peticion.IdCondicion));
                cmd.Parameters.Add(new SqlParameter("@ID_SEDE", peticion.IdSede));
                cmd.Parameters.Add(new SqlParameter("@NRO_DOCUMENTO", peticion.NroDocumento));
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
                            if (!dr.IsDBNull(dr.GetOrdinal("dtInicioLabores"))) item.FechaInicio = dr.GetDateTime(dr.GetOrdinal("dtInicioLabores"));


                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaInicioOrden"))) item.InicioOrden = dr.GetDateTime(dr.GetOrdinal("dFechaInicioOrden")).ToString("dd/MM/yyyy");
                            if (!dr.IsDBNull(dr.GetOrdinal("dFechaFinOrden"))) item.FinOrden = dr.GetDateTime(dr.GetOrdinal("dFechaFinOrden")).ToString("dd/MM/yyyy");

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<Empleado_Registro> ListarEmpleadosMaestro(Empleado_Request peticion)
        {
            List<Empleado_Registro> lista = new List<Empleado_Registro>();
            Empleado_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paListaTrabajadorMaestro]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_TRABAJADOR", peticion.IdEmpleado));
                cmd.Parameters.Add(new SqlParameter("@ID_DEPENDENCIA", peticion.IdDependencia));
                cmd.Parameters.Add(new SqlParameter("@ID_CONDICION", peticion.IdCondicion));
                cmd.Parameters.Add(new SqlParameter("@ID_SEDE", peticion.IdSede));
                cmd.Parameters.Add(new SqlParameter("@NRO_DOCUMENTO", peticion.NroDocumento));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));
                cmd.Parameters.Add(new SqlParameter("@ESTADO", peticion.Estado));
                cmd.Parameters.Add(new SqlParameter("@ID_ORGANO", DBNull.Value));

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
                            item.IdTipoIngreso = dr.GetInt32(dr.GetOrdinal("iCodigoTipoIngreso"));
                            item.TipoIngreso = dr.GetString(dr.GetOrdinal("TipoIngreso"));
                            item.IdSede = dr.GetInt32(dr.GetOrdinal("iCodigoSede"));
                            item.Sede = dr.GetString(dr.GetOrdinal("Sede"));
                            item.NroContrato = dr.GetString(dr.GetOrdinal("NroContrato"));

                            //item.NroOrden = dr.GetString(dr.GetOrdinal("vNroOrden"));
                            //item.NombreOrden = dr.GetString(dr.GetOrdinal("vNombreOrden"));
                            //item.DuracionOrden = dr.GetInt32(dr.GetOrdinal("iDuracionOrden"));
                            //item.MontoOrden = dr.GetDecimal(dr.GetOrdinal("dMontoOrden"));
                            if (!dr.IsDBNull(dr.GetOrdinal("dtInicioLabores"))) item.FechaInicio = dr.GetDateTime(dr.GetOrdinal("dtInicioLabores"));

                            item.TieneJefatura = dr.GetInt32(dr.GetOrdinal("TieneJefatura"));
                            item.TieneEncargatura = dr.GetInt32(dr.GetOrdinal("TieneEncargatura"));

                            //if (!dr.IsDBNull(dr.GetOrdinal("dFechaInicioOrden"))) item.InicioOrden = dr.GetDateTime(dr.GetOrdinal("dFechaInicioOrden")).ToString("dd/MM/yyyy");
                            //if (!dr.IsDBNull(dr.GetOrdinal("dFechaFinOrden"))) item.FinOrden = dr.GetDateTime(dr.GetOrdinal("dFechaFinOrden")).ToString("dd/MM/yyyy");

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<EmpleadoCuenta_Registro> ListarCuentasEmpleado(Empleado_Request peticion)
        {
            List<EmpleadoCuenta_Registro> lista = new List<EmpleadoCuenta_Registro>();
            EmpleadoCuenta_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paListarTrabajadorCuenta]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_EMPLEADO", peticion.IdEmpleado));
                cmd.Parameters.Add(new SqlParameter("@ESTADO", peticion.Estado));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new EmpleadoCuenta_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdEmpleadoBanco = dr.GetInt32(dr.GetOrdinal("iCodigoTrabajadorBanco"));
                            item.IdEmpleado = dr.GetInt32(dr.GetOrdinal("iCodigoTrabajador"));
                            item.NroCuenta = dr.GetString(dr.GetOrdinal("vCuenta"));
                            item.CCI = dr.GetString(dr.GetOrdinal("vCCI"));
                            item.Estado = new Estado_Response()
                            {
                                Codigo = dr.GetInt32(dr.GetOrdinal("bEstado")).ToString(),
                                Nombre = (dr.GetInt32(dr.GetOrdinal("bEstado")).ToString() == "0" ? "INACTIVO" : (dr.GetInt32(dr.GetOrdinal("bEstado")).ToString() == "1" ? "ACTIVO" : String.Empty))
                            };

                            item.Banco = new Banco_Registro()
                            {
                                IdBanco = dr.GetInt32(dr.GetOrdinal("iCodigoBanco")),
                                Nombre = dr.GetString(dr.GetOrdinal("vDescripcion"))
                            };

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<EmpleadoOrden_Registro> ListarOrdenesEmpleado(Empleado_Request peticion)
        {
            List<EmpleadoOrden_Registro> lista = new List<EmpleadoOrden_Registro>();
            EmpleadoOrden_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paListarTrabajadorOrdenServicio]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_EMPLEADO", peticion.IdEmpleado));
                cmd.Parameters.Add(new SqlParameter("@ESTADO", peticion.Estado));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new EmpleadoOrden_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdEmpleadoOrden = dr.GetInt32(dr.GetOrdinal("iCodigoTrabajadorOrden"));
                            item.IdEmpleado = dr.GetInt32(dr.GetOrdinal("iCodigoTrabajador"));
                            item.Duracion = dr.GetInt32(dr.GetOrdinal("iDuracion"));
                            item.NroOrden = dr.GetString(dr.GetOrdinal("vNroOrden"));
                            item.NroSIAF = dr.GetString(dr.GetOrdinal("vNroSIAF"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("vNombre"));
                            item.Monto = dr.GetDecimal(dr.GetOrdinal("vMonto"));
                            if (!Convert.IsDBNull(dr["dfechaInicio"])) { item.FechaInicio = dr.GetDateTime(dr.GetOrdinal("dfechaInicio")).ToString("dd/MM/yyyy"); }
                            if (!Convert.IsDBNull(dr["dfechaFin"])) { item.FechaFin = dr.GetDateTime(dr.GetOrdinal("dfechaFin")).ToString("dd/MM/yyyy"); }

                            item.Estado = new Estado_Response()
                            {
                                Codigo = dr.GetInt32(dr.GetOrdinal("bEstado")).ToString(),
                                Nombre = (dr.GetInt32(dr.GetOrdinal("bEstado")).ToString() == "0" ? "INACTIVO" : (dr.GetInt32(dr.GetOrdinal("bEstado")).ToString() == "1" ? "ACTIVO" : String.Empty))
                            };

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return lista;
        }

        public Int32 TotalBoletasValidas(Empleado_Registro peticion)
        {
            Int32 iTotal = 0;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_Trabajador_Boleta_Valida_cont]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ANIO", peticion.Anio));
                cmd.Parameters.Add(new SqlParameter("@MES", Convert.ToInt32(peticion.Mes).ToString()));
                if (!String.IsNullOrEmpty(peticion.Planilla) && !String.IsNullOrEmpty(peticion.TipoPlanilla))
                    cmd.Parameters.Add(new SqlParameter("@TIPO", peticion.Planilla + peticion.TipoPlanilla));

                iTotal = Convert.ToInt32(cmd.ExecuteScalar());

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return iTotal;
        }

        public Int32 TotalBoletasEnviadas(Empleado_Registro peticion)
        {
            Int32 iTotal = 0;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_Trabajador_Boleta_Enviada_cont]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ANIO", peticion.Anio));
                cmd.Parameters.Add(new SqlParameter("@MES", peticion.Mes.PadLeft(2, '0')));
                if (!String.IsNullOrEmpty(peticion.Planilla) && !String.IsNullOrEmpty(peticion.TipoPlanilla))
                    cmd.Parameters.Add(new SqlParameter("@TIPO", peticion.Planilla + peticion.TipoPlanilla));

                iTotal = Convert.ToInt32(cmd.ExecuteScalar());

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return iTotal;
        }

        public IEnumerable<Empleado_Registro> ListarEmpleadosBoleta(Empleado_Registro peticion)
        {
            List<Empleado_Registro> lista = new List<Empleado_Registro>();
            Empleado_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_Empleado_Boleta_sel]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_EMPLEADO", peticion.IdEmpleado));
                cmd.Parameters.Add(new SqlParameter("@ID_OFICINA", peticion.IdDependencia));
                cmd.Parameters.Add(new SqlParameter("@ANIO", peticion.Anio));
                cmd.Parameters.Add(new SqlParameter("@MES", peticion.Mes));
                cmd.Parameters.Add(new SqlParameter("@NRO_DOCUMENTO", peticion.NroDocumento));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Empleado_Registro();

                            item.Grilla = new Grilla_Response();
                            //item.Grilla.NumeroDeFila = dr.GetInt64(dr.GetOrdinal("NUMERO_FILA"));
                            //item.Grilla.TotalDeRegistros = dr.GetInt32(dr.GetOrdinal("TOTAL_FILAS"));

                            item.IdEmpleado = dr.GetInt32(dr.GetOrdinal("ID_EMPLEADO"));
                            item.IdDependencia = dr.GetInt32(dr.GetOrdinal("ID_OFICINA"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("NOMBREEMPLEADO"));
                            item.Paterno = dr.GetString(dr.GetOrdinal("PATERNO"));
                            item.Materno = dr.GetString(dr.GetOrdinal("MATERNO"));
                            //item.TipoDocumento = dr.GetString(dr.GetOrdinal("TIPO_DOC"));
                            item.NroDocumento = dr.GetString(dr.GetOrdinal("NUMERO_DOC")).Trim();
                            item.CorreoElectronicoLaboral = dr.GetString(dr.GetOrdinal("CORREO"));
                            item.Sigla = dr.GetString(dr.GetOrdinal("SIGLA"));
                            item.NombreOficina = dr.GetString(dr.GetOrdinal("NOMBREOFICINA"));
                            item.Anio = dr.GetString(dr.GetOrdinal("ANIO"));
                            item.Mes = dr.GetString(dr.GetOrdinal("MES"));
                            item.Guid = dr.GetString(dr.GetOrdinal("GUID"));
                            item.TipoPlanilla = dr.GetString(dr.GetOrdinal("TIPO"));
                            item.Estado = dr.GetInt32(dr.GetOrdinal("bEstado"));
                            item.EstadoEnvio = dr.GetInt32(dr.GetOrdinal("bEstadoEnvio"));
                            if (!Convert.IsDBNull(dr["boleta"]) && peticion.IdEmpleado > 0) { item.Boleta = (byte[])(dr["BOLETA"]); }
                            if (!Convert.IsDBNull(dr["fecha_envio"])) { item.FechaEnvio = dr.GetDateTime(dr.GetOrdinal("FECHA_ENVIO")).ToString("dd/MM/yyyy HH:mm"); }
                            if (!Convert.IsDBNull(dr["fecha_recepcion"])) { item.FechaRecepcion = dr.GetDateTime(dr.GetOrdinal("FECHA_RECEPCION")).ToString("dd/MM/yyyy HH:mm"); }

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<Empleado_Registro> ListarEmpleadosValidacionBoleta(Empleado_Registro peticion)
        {
            List<Empleado_Registro> lista = new List<Empleado_Registro>();
            Empleado_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_Empleado_Validacion_Boleta_sel]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ANIO", peticion.Anio));
                cmd.Parameters.Add(new SqlParameter("@MES", peticion.Mes));
                cmd.Parameters.Add(new SqlParameter("@TIPO", peticion.Planilla + peticion.TipoPlanilla));
                
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Empleado_Registro();

                            item.Grilla = new Grilla_Response();

                            item.CodigoSisper = dr.GetString(dr.GetOrdinal("trabajador"));
                            item.IdEmpleado = dr.GetInt32(dr.GetOrdinal("ID_EMPLEADO"));
                            item.Anio = dr.GetString(dr.GetOrdinal("ANIO"));
                            item.Mes = dr.GetString(dr.GetOrdinal("MES"));
                            item.TipoPlanilla = dr.GetString(dr.GetOrdinal("TIPO"));
                            item.Estado = dr.GetInt32(dr.GetOrdinal("estado"));
                            item.NombreArchivo = dr.GetString(dr.GetOrdinal("nombre_archivo"));
                            if (!Convert.IsDBNull(dr["boleta"])) { item.Boleta = (byte[])(dr["BOLETA"]); }
                            //if (!Convert.IsDBNull(dr["fecha_registro"])) { item.FechaRegistro = dr.GetDateTime(dr.GetOrdinal("fecha_registro")).ToString("dd/MM/yyyy HH:mm"); }

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<EmpleadoSisper_Registro> ListarEmpleadosSisper(Empleado_Registro peticion)
        {
            List<EmpleadoSisper_Registro> lista = new List<EmpleadoSisper_Registro>();
            EmpleadoSisper_Registro item = null;

            using (OracleCommand cmd = _iBasesOracleAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                //peticion.CodigoSisper = "000826";
                cmd.CommandText = "SELECT op.planilla, op.t_planilla, op.pagina, pp.ano, pp.mes, pp.trabajador, pp.nivel_remunerativo, pp.cta_bancaria, nvl(pp.nro_afiliacion, ' ') as nro_afiliacion, " +
                //" case when (  select nvl(monto_concepto, 0)  " +
                //" from PA_INGRESOS_DESCUENTOS_FIJOS  " +
                //" where trabajador = pp.trabajador " +
                //" and concepto in ('001', '007')) = (   SELECT nvl(monto_concepto, 0)  " +
                //"                                       FROM sisper.pa_planilla_calculada_mes  " +
                //"                                       where trabajador = pp.trabajador  " +
                //"                                         and ano = pp.ano " +
                //"                                         and mes = pp.mes  " +
                //"                                         and planilla = op.planilla " +
                //"                                         and t_planilla = op.t_planilla " +
                //"                                         and concepto in ('001', '007')) THEN pp.dias_laborados else CAST(round( ( SELECT nvl(monto_concepto, 0)  " +
                //"                                                                                                                   FROM sisper.pa_planilla_calculada_mes  " +
                //"                                                                                                                   where trabajador = pp.trabajador  " +
                //"                                                                                                                   and ano = pp.ano " +
                //"                                                                                                                   and mes = pp.mes  " +
                //"                                                                                                                   and planilla = op.planilla " +
                //"                                                                                                                   and t_planilla = op.t_planilla " +
                //"                                                                                                                   and concepto in ('001', '007')) * 30 / (select nvl(monto_concepto, 0)  " +
                //"                                                                                                                                                           from PA_INGRESOS_DESCUENTOS_FIJOS  " +
                //"                                                                                                                                                           where trabajador = pp.trabajador " +
                //"                                                                                                                                                           and concepto in ('001', '007')) ) AS INTEGER) end as dias_laborados,  " +
                "nvl((select sum(es.dia_fin - es.dia_inicio + 1) from pa_trabajador_estado es where es.ano = pp.ano and es.mes = pp.mes and es.estado in ('A') and es.trabajador = pp.trabajador), 0) as dias_laborados," +
                "pn.apellidos, pn.nombres, pn.documento, d.nom_dependencia, nvl(ce.abrev_cargo_estruct, '') as nom_cargo_estruc, cl.nom_condicion_laboral, " +
                                         "rp.nom_regimen_pensionario, rp.abrev_regimen_pensionario, b.nom_banco, b.abrev_banco, nvl(a.nom_afp, ' ') as nom_afp, tp.nom_t_planilla, p.nom_planilla, pn.f_inicio_mef, " +
                                         "nvl(( SELECT sum(nvl(pcm.monto_concepto, 0)) " +
                                                "FROM sisper.pa_planilla_calculada_mes pcm " +
                                                "left join sisper.pa_concepto c " +
                                                "on pcm.concepto = c.concepto " +
                                                "left join sisper.pa_t_concepto tc " +
                                                "on c.t_concepto = tc.t_concepto " +
                                                "where pcm.trabajador = pp.trabajador " +
                                                "  and pcm.ano = pp.ano " +
                                                "  and pcm.mes = pp.mes " +
                                                "  and pcm.planilla = op.planilla " +
                                                "  and pcm.t_planilla = op.t_planilla " +
                                                "  and tc.t_concepto in (0,1,3,4) " +
                                                "  ), 0) as Ingresos, " +
                                        "nvl(( SELECT sum(nvl(pcm.monto_concepto, 0)) " +
                                        "  FROM sisper.pa_planilla_calculada_mes pcm " +
                                        "  left join sisper.pa_concepto c " +
                                        "  on pcm.concepto = c.concepto " +
                                        "  left join sisper.pa_t_concepto tc " +
                                        "  on c.t_concepto = tc.t_concepto " +
                                        "  where pcm.trabajador = pp.trabajador " +
                                        "    and pcm.ano = pp.ano " +
                                        "    and pcm.mes = pp.mes  " +
                                        "    and pcm.planilla = op.planilla " +
                                        "    and pcm.t_planilla = op.t_planilla " +
                                        "    and tc.t_concepto in (6,7,8) " +
                                        "    ), 0) as Descuentos, " +
                                        "nvl(( SELECT sum(pcm.monto_concepto) " +
                                        "      FROM sisper.pa_planilla_calculada_mes pcm " +
                                        "  left join sisper.pa_concepto c " +
                                        "  on pcm.concepto = c.concepto " +
                                        "  left join sisper.pa_t_concepto tc " +
                                        "  on c.t_concepto = tc.t_concepto " +
                                        "  where pcm.trabajador = pp.trabajador " +
                                        "    and pcm.ano = pp.ano " +
                                        "    and pcm.mes = pp.mes  " +
                                        "    and pcm.planilla = op.planilla " +
                                        "    and pcm.t_planilla = op.t_planilla " +
                                        "    and tc.t_concepto = 9 " +
                                        "    ), 0) as Aportes " +
                                "FROM SISPER.PA_DATOS_ORDEN_PLANILLA op " +
                                    "inner join sisper.pa_datos_personal_planilla pp " +
                                    "on op.ano = pp.ano " +
                                    "and op.mes = pp.mes " +
                                    "and op.trabajador = pp.trabajador " +
                                    "left join sisper.pa_datos_personal_nombres pn " +
                                    "on pp.trabajador = pn.trabajador " +
                                    "left join sisper.pa_dependencia d " +
                                    "on  pp.entidad = d.entidad   " +
                                    "and pp.dependencia = d.dependencia " +
                                    "left join sisper.pa_cargo_estructural ce " +
                                    "on pp.cargo_estructural = ce.cargo_estructural " +
                                    "left join sisper.pa_condicion_laboral cl " +
                                    "on pp.condicion_laboral = cl.condicion_laboral " +
                                    "left join sisper.pa_regimen_pensionario rp " +
                                    "on pp.regimen_pensiones = rp.regimen_pensiones " +
                                    "left join sisper.pa_banco b " +
                                    "on pp.banco = b.banco " +
                                    "left join sisper.pa_afp a " +
                                    "on pp.afp = a.afp " +
                                    "left join sisper.pa_t_planilla tp " +
                                    "on op.t_planilla = tp.t_planilla " +
                                    "left join sisper.pa_planilla p " +
                                    "on op.planilla = p.planilla " +
                                "WHERE " +
                                (String.IsNullOrEmpty(peticion.CodigoSisper) ? "1=1" : " pp.trabajador='" + peticion.CodigoSisper + "'") +
                                (String.IsNullOrEmpty(peticion.Anio) ? " and 1=1" : " and pp.ano=" + Convert.ToInt32(peticion.Anio).ToString()) +
                                (String.IsNullOrEmpty(peticion.Mes) ? " and 1=1" : " and pp.mes=" + Convert.ToInt32(peticion.Mes).ToString()) +
                                (String.IsNullOrEmpty(peticion.NroDocumento) ? " and 1=1" : " and pn.documento='" + peticion.NroDocumento + "'") +
                                (String.IsNullOrEmpty(peticion.Nombre) ? " and 1=1" : " and pn.apellidos || ' ' || pn.nombres like '%" + peticion.Nombre.ToUpper() + "%'") +
                                " ORDER BY op.ano desc, op.mes desc, d.nom_dependencia asc, pn.apellidos, pn.nombres, op.pagina desc ";

                cmd.CommandType = CommandType.Text;

                using (OracleDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new EmpleadoSisper_Registro();

                            item.Grilla = new Grilla_Response();

                            item.IdPlanilla = dr.GetString(dr.GetOrdinal("planilla"));
                            item.TipoPlanilla = dr.GetString(dr.GetOrdinal("t_planilla"));
                            item.Anio = dr.GetInt32(dr.GetOrdinal("ano"));
                            item.Mes = dr.GetInt32(dr.GetOrdinal("mes"));
                            item.Trabajador = dr.GetString(dr.GetOrdinal("trabajador"));
                            item.NivelRemunerativo = dr.GetString(dr.GetOrdinal("nivel_remunerativo"));
                            item.CuentaBancaria = dr.GetString(dr.GetOrdinal("cta_bancaria"));
                            item.NroAfiliacion = dr.GetString(dr.GetOrdinal("nro_afiliacion"));
                            item.Apellidos = dr.GetString(dr.GetOrdinal("apellidos"));
                            item.Nombres = dr.GetString(dr.GetOrdinal("nombres"));
                            item.NroDocumento = dr.GetString(dr.GetOrdinal("documento"));
                            item.NombreDependencia = dr.GetString(dr.GetOrdinal("nom_dependencia"));

                            if (!dr.IsDBNull(dr.GetOrdinal("nom_cargo_estruc"))) item.CargoEstructural = dr.GetString(dr.GetOrdinal("nom_cargo_estruc"));
                            if (!dr.IsDBNull(dr.GetOrdinal("nom_regimen_pensionario"))) item.NombreRegimenPensiones = dr.GetString(dr.GetOrdinal("nom_regimen_pensionario"));
                            if (!dr.IsDBNull(dr.GetOrdinal("abrev_regimen_pensionario"))) item.AbrevRegimenPensiones = dr.GetString(dr.GetOrdinal("abrev_regimen_pensionario"));

                            item.CondicionLaboral = dr.GetString(dr.GetOrdinal("nom_condicion_laboral")).ToUpper();
                            item.NombreBanco = dr.GetString(dr.GetOrdinal("nom_banco"));
                            item.AbrevBanco = dr.GetString(dr.GetOrdinal("abrev_banco"));
                            item.NombreAFP = dr.GetString(dr.GetOrdinal("nom_afp"));
                            item.NombreTipoPlanilla = dr.GetString(dr.GetOrdinal("nom_t_planilla")).ToUpper();
                            item.NombrePlanilla = dr.GetString(dr.GetOrdinal("nom_planilla"));
                            item.Ingresos = dr.GetDecimal(dr.GetOrdinal("Ingresos"));
                            item.Descuentos = dr.GetDecimal(dr.GetOrdinal("Descuentos"));
                            item.Aportes = dr.GetDecimal(dr.GetOrdinal("Aportes"));
                            if (!dr.IsDBNull(dr.GetOrdinal("dias_laborados"))) {
                                if (dr.GetInt32(dr.GetOrdinal("dias_laborados")) == 60)
                                    item.DiasLaborados = 30;
                                else
                                    item.DiasLaborados = dr.GetInt32(dr.GetOrdinal("dias_laborados"));
                            }   
                            else
                                item.DiasLaborados = 0;
                            if (!dr.IsDBNull(dr.GetOrdinal("f_inicio_mef"))) item.FechaIngreso = dr.GetDateTime(dr.GetOrdinal("f_inicio_mef")).ToString("dd/MM/yyyy");

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<EmpleadoSisper_Registro> ListarEmpleadosSispla(Empleado_Registro peticion)
        {
            List<EmpleadoSisper_Registro> lista = new List<EmpleadoSisper_Registro>();
            EmpleadoSisper_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                //peticion.CodigoSisper = "000826";
                cmd.CommandText = "[dbo].[paPlanillaEmisionBoletasCabecera_TipoPlanilla]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                Int32 cero = 0;
                cmd.Parameters.Add(new SqlParameter("@iAnio", peticion.Anio));
                cmd.Parameters.Add(new SqlParameter("@iMes", peticion.Mes));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", cero));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", peticion.CodigoSisper));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new EmpleadoSisper_Registro();

                            item.Grilla = new Grilla_Response();

                            item.IdPlanilla = dr.GetInt32(dr.GetOrdinal("iCodPlanilla")).ToString();
                            item.TipoPlanilla = dr.GetString(dr.GetOrdinal("iCodTipoPlanilla"));
                            item.Anio = dr.GetInt32(dr.GetOrdinal("ano"));
                            item.Mes = dr.GetInt32(dr.GetOrdinal("mes"));
                            item.Trabajador = dr.GetInt32(dr.GetOrdinal("trabajador")).ToString();
                            item.NivelRemunerativo = "SNR"; // dr.GetString(dr.GetOrdinal("nivel_remunerativo"));
                            item.CuentaBancaria = ""; // dr.GetString(dr.GetOrdinal("cta_bancaria"));
                            item.NroAfiliacion = dr.GetString(dr.GetOrdinal("nro_afiliacion"));
                            item.Apellidos = dr.GetString(dr.GetOrdinal("apellidos"));
                            item.Nombres = dr.GetString(dr.GetOrdinal("nombres"));
                            item.NroDocumento = dr.GetString(dr.GetOrdinal("documento"));
                            item.NombreDependencia = dr.GetString(dr.GetOrdinal("nom_dependencia"));

                            if (!dr.IsDBNull(dr.GetOrdinal("nom_cargo_estruc"))) item.CargoEstructural = dr.GetString(dr.GetOrdinal("nom_cargo_estruc"));
                            if (!dr.IsDBNull(dr.GetOrdinal("nom_reg_pensionario"))) item.NombreRegimenPensiones = dr.GetString(dr.GetOrdinal("nom_reg_pensionario"));
                            //if (!dr.IsDBNull(dr.GetOrdinal("abrev_regimen_pensionario"))) item.AbrevRegimenPensiones = dr.GetString(dr.GetOrdinal("abrev_regimen_pensionario"));

                            item.CondicionLaboral = dr.GetString(dr.GetOrdinal("nom_condicion_laboral")).ToUpper();
                            item.NombreBanco = dr.GetString(dr.GetOrdinal("nom_banco"));
                            item.AbrevBanco = dr.GetString(dr.GetOrdinal("abrev_banco"));
                            item.NombreAFP = dr.GetString(dr.GetOrdinal("nom_afp"));
                            item.NombreTipoPlanilla = dr.GetString(dr.GetOrdinal("nom_t_planilla")).ToUpper();
                            item.NombrePlanilla = dr.GetString(dr.GetOrdinal("nom_planilla"));
                            item.Ingresos = dr.GetDecimal(dr.GetOrdinal("Ingresos"));
                            item.Descuentos = dr.GetDecimal(dr.GetOrdinal("Descuentos"));
                            item.Aportes = dr.GetDecimal(dr.GetOrdinal("Aportes"));
                            if (!dr.IsDBNull(dr.GetOrdinal("dias_laborados")))
                                item.DiasLaborados = dr.GetInt32(dr.GetOrdinal("dias_laborados"));
                            else
                                item.DiasLaborados = 0;
                            //if (!dr.IsDBNull(dr.GetOrdinal("f_inicio_mef"))) item.FechaIngreso = dr.GetDateTime(dr.GetOrdinal("f_inicio_mef")).ToString("dd/MM/yyyy");

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<EmpleadoConceptoSisper_Registro> ListarEmpleadoConceptoSisper(Empleado_Registro peticion)
        {
            List<EmpleadoConceptoSisper_Registro> lista = new List<EmpleadoConceptoSisper_Registro>();
            EmpleadoConceptoSisper_Registro item = null;

            using (OracleCommand cmd = _iBasesOracleAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                //peticion.CodigoSisper = "000826";
                cmd.CommandText = "SELECT pcm.ano, pcm.mes, pcm.planilla, pcm.t_planilla, pcm.trabajador, pcm.concepto, pcm.monto_concepto, " +
                                         "c.nom_concepto, c.abreviatura, c.t_concepto, tc.nom_t_concepto, tp.nom_t_planilla, p.nom_planilla, p.abrev_planilla, p.reg_laboral " +
                                "FROM sisper.pa_planilla_calculada_mes pcm " +
                                    "left join sisper.pa_concepto c " +
                                    "on pcm.concepto = c.concepto " +
                                    "left join sisper.pa_t_concepto tc " +
                                    "on c.t_concepto = tc.t_concepto " +
                                    "left join sisper.pa_t_planilla tp " +
                                    "on pcm.t_planilla = tp.t_planilla " +
                                    "left join sisper.pa_planilla p " +
                                    "on pcm.planilla = p.planilla " +
                                "WHERE " +
                                (String.IsNullOrEmpty(peticion.CodigoSisper) ? "1=1" : " pcm.trabajador='" + peticion.CodigoSisper + "'") +
                                (String.IsNullOrEmpty(peticion.Anio) ? " and 1=1" : " and pcm.ano=" + Convert.ToInt32(peticion.Anio).ToString()) +
                                (String.IsNullOrEmpty(peticion.Mes) ? " and 1=1" : " and pcm.mes=" + Convert.ToInt32(peticion.Mes).ToString()) +
                                " ORDER BY pcm.concepto";

                cmd.CommandType = CommandType.Text;

                using (OracleDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new EmpleadoConceptoSisper_Registro();

                            item.Grilla = new Grilla_Response();

                            item.IdPlanilla = dr.GetString(dr.GetOrdinal("planilla"));
                            item.TipoPlanilla = dr.GetString(dr.GetOrdinal("t_planilla"));
                            item.Anio = dr.GetInt32(dr.GetOrdinal("ano"));
                            item.Mes = dr.GetInt32(dr.GetOrdinal("mes"));
                            item.Trabajador = dr.GetString(dr.GetOrdinal("trabajador"));
                            item.Concepto = dr.GetString(dr.GetOrdinal("concepto"));
                            item.MontoConcepto = dr.GetDecimal(dr.GetOrdinal("monto_concepto"));
                            item.NombreConcepto = dr.GetString(dr.GetOrdinal("nom_concepto"));
                            item.Abreviatura = dr.GetString(dr.GetOrdinal("abreviatura"));
                            item.TipoConcepto = dr.GetString(dr.GetOrdinal("t_concepto"));
                            item.NombreTipoConcepto = dr.GetString(dr.GetOrdinal("nom_t_concepto"));
                            item.NombreTipoPlanilla = dr.GetString(dr.GetOrdinal("nom_t_planilla"));
                            item.NombrePlanilla = dr.GetString(dr.GetOrdinal("nom_planilla"));
                            item.AbreviaturaPlanilla = dr.GetString(dr.GetOrdinal("abrev_planilla"));
                            item.RegimenLaboral = dr.GetString(dr.GetOrdinal("reg_laboral"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<EmpleadoConceptoSisper_Registro> ListarEmpleadoConceptoSispla(Empleado_Registro peticion)
        {
            List<EmpleadoConceptoSisper_Registro> lista = new List<EmpleadoConceptoSisper_Registro>();
            EmpleadoConceptoSisper_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                //peticion.CodigoSisper = "000826";
                cmd.CommandText = "[dbo].[paPlanillaEmisionBoletasDetalle_TipoPlanilla]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                Int32 cero = 0;
                cmd.Parameters.Add(new SqlParameter("@iAnio", peticion.Anio));
                cmd.Parameters.Add(new SqlParameter("@iMes", peticion.Mes));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", peticion.TipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", peticion.CodigoSisper));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new EmpleadoConceptoSisper_Registro();

                            item.Grilla = new Grilla_Response();

                            item.IdPlanilla = dr.GetInt32(dr.GetOrdinal("iCodPlanilla")).ToString();
                            item.TipoPlanilla = dr.GetString(dr.GetOrdinal("iCodTipoPlanilla"));
                            item.Anio = dr.GetInt32(dr.GetOrdinal("ano"));
                            item.Mes = dr.GetInt32(dr.GetOrdinal("mes"));
                            item.Trabajador = dr.GetInt32(dr.GetOrdinal("trabajador")).ToString();
                            item.Concepto = dr.GetInt32(dr.GetOrdinal("concepto")).ToString().PadLeft(2, '0');
                            item.MontoConcepto = dr.GetDecimal(dr.GetOrdinal("monto_concepto"));
                            item.NombreConcepto = dr.GetString(dr.GetOrdinal("nom_concepto"));
                            item.Abreviatura = dr.GetString(dr.GetOrdinal("vAbreviatura"));
                            item.TipoConcepto = dr.GetInt32(dr.GetOrdinal("t_concepto")).ToString();
                            item.NombreTipoConcepto = dr.GetString(dr.GetOrdinal("nom_t_concepto"));
                            item.NombreTipoPlanilla = dr.GetString(dr.GetOrdinal("nom_t_planilla"));
                            item.NombrePlanilla = dr.GetString(dr.GetOrdinal("nom_planilla"));
                            //item.AbreviaturaPlanilla = dr.GetString(dr.GetOrdinal("abrev_planilla"));
                            item.RegimenLaboral = dr.GetString(dr.GetOrdinal("reg_laboral"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<EmpleadoBoletaResumen_Registro> ListarResumenBoletas(Empleado_Registro peticion)
        {
            List<EmpleadoBoletaResumen_Registro> lista = new List<EmpleadoBoletaResumen_Registro>();
            EmpleadoBoletaResumen_Registro item = null;

            using (OracleCommand cmd = _iBasesOracleAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "SELECT pp.ano, pp.mes, op.planilla, p.nom_planilla, op.t_planilla, tp.nom_t_planilla, count(*) as TotalSisper " +
                                "FROM SISPER.PA_DATOS_ORDEN_PLANILLA op " +
                                    "inner join sisper.pa_datos_personal_planilla pp " +
                                    "on op.ano = pp.ano " +
                                    "and op.mes = pp.mes " +
                                    "and op.trabajador = pp.trabajador " +
                                    "left join sisper.pa_datos_personal_nombres pn " +
                                    "on pp.trabajador = pn.trabajador " +
                                    "left join sisper.pa_dependencia d " +
                                    "on  pp.entidad = d.entidad   " +
                                    "and pp.dependencia = d.dependencia " +
                                    "left join sisper.pa_cargo_estructural ce " +
                                    "on pp.cargo_estructural = ce.cargo_estructural " +
                                    "left join sisper.pa_condicion_laboral cl " +
                                    "on pp.condicion_laboral = cl.condicion_laboral " +
                                    "left join sisper.pa_regimen_pensionario rp " +
                                    "on pp.regimen_pensiones = rp.regimen_pensiones " +
                                    "left join sisper.pa_banco b " +
                                    "on pp.banco = b.banco " +
                                    "left join sisper.pa_afp a " +
                                    "on pp.afp = a.afp " +
                                    "left join sisper.pa_t_planilla tp " +
                                    "on op.t_planilla = tp.t_planilla " +
                                    "left join sisper.pa_planilla p " +
                                    "on op.planilla = p.planilla " +
                                "WHERE " +
                                (String.IsNullOrEmpty(peticion.Anio) ? " 1=1" : " pp.ano=" + Convert.ToInt32(peticion.Anio).ToString()) +
                                (String.IsNullOrEmpty(peticion.Mes) ? " and 1=1" : " and pp.mes=" + Convert.ToInt32(peticion.Mes).ToString()) +
                                " GROUP BY pp.ano, pp.mes, op.planilla, p.nom_planilla, op.t_planilla, tp.nom_t_planilla " +
                                " ORDER BY pp.ano desc, pp.mes desc, p.nom_planilla, tp.nom_t_planilla ";

                cmd.CommandType = CommandType.Text;

                using (OracleDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new EmpleadoBoletaResumen_Registro();

                            item.Grilla = new Grilla_Response();

                            item.Anio = dr.GetInt32(dr.GetOrdinal("ano"));
                            item.Mes = dr.GetInt32(dr.GetOrdinal("mes"));
                            item.TotalSisper = dr.GetInt32(dr.GetOrdinal("TotalSisper"));
                            item.IdPlanilla = dr.GetString(dr.GetOrdinal("planilla"));
                            item.IdTipoPlanilla = dr.GetString(dr.GetOrdinal("t_planilla"));
                            item.NombrePlanilla = dr.GetString(dr.GetOrdinal("nom_planilla"));
                            item.NombreTipoPlanilla = dr.GetString(dr.GetOrdinal("nom_t_planilla"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        #endregion

        public Int32 Insertar(BoletaCarga_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_Empleado_Boleta_ins]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@NRO_DOCUMENTO", registro.NroDocumento));
                cmd.Parameters.Add(new SqlParameter("@ANIO", registro.Anio));
                cmd.Parameters.Add(new SqlParameter("@MES", registro.Mes));
                cmd.Parameters.Add(new SqlParameter("@TIPO", registro.IdPlanilla + registro.TipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@GUID", "XXXXXXXX"));
                if (registro.archivo != null) cmd.Parameters.Add(new SqlParameter("@ARCHIVO", registro.archivo));
                SqlParameter IdPropuestaParameter = new SqlParameter("@AUX_EMPLEADO", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(IdPropuestaParameter);

                cmd.ExecuteNonQuery();
                registro.IdEmpleado = Int32.Parse(IdPropuestaParameter.Value.ToString());

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdEmpleado;
        }
        public Int32 InsertarBoletaValida(BoletaCarga_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_Empleado_BoletaValida_ins]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@NRO_DOCUMENTO", registro.NroDocumento));
                cmd.Parameters.Add(new SqlParameter("@TRABAJADOR", registro.Trabajador));
                cmd.Parameters.Add(new SqlParameter("@ANIO", registro.Anio));
                cmd.Parameters.Add(new SqlParameter("@MES", registro.Mes));
                cmd.Parameters.Add(new SqlParameter("@TIPO", registro.IdPlanilla + registro.TipoPlanilla));
                if (registro.archivo != null) cmd.Parameters.Add(new SqlParameter("@ARCHIVO", registro.archivo));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE_ARCHIVO", registro.NombreArchivo));
                cmd.Parameters.Add(new SqlParameter("@ESTADO", registro.Estado));
                cmd.Parameters.Add(new SqlParameter("@USUARIO_REGISTRO", registro.IdUsuarioRegistro));
                cmd.Parameters.Add(new SqlParameter("@FECHA_REGISTRO", registro.FechaRegistro));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdEmpleado;
        }
        public Int32 InsertarNotificacion(BoletaCarga_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_Empleado_Notificacion_ins]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ID_EMPLEADO", registro.IdEmpleado));
                cmd.Parameters.Add(new SqlParameter("@TIPO", "01"));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdEmpleado;
        }

        public Int32 ValidarExisteBoletaValida(BoletaCarga_Registro registro)
        {
            Int32 iTotal = 0;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_Empleado_BoletaValida_Sel]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@TRABAJADOR", registro.Trabajador));
                cmd.Parameters.Add(new SqlParameter("@ANIO", registro.Anio));
                cmd.Parameters.Add(new SqlParameter("@MES", registro.Mes));
                cmd.Parameters.Add(new SqlParameter("@TIPO", registro.IdPlanilla + registro.TipoPlanilla));

                iTotal = Convert.ToInt32(cmd.ExecuteScalar());

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return iTotal;
        }
        public Int32 ActualizarRecepcion(Empleado_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_Empleado_Boleta_upd_recepcion]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_EMPLEADO", registro.IdEmpleado));
                cmd.Parameters.Add(new SqlParameter("@ANIO", registro.Anio));
                cmd.Parameters.Add(new SqlParameter("@MES", registro.Mes));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdEmpleado;
        }
        public Int32 ActualizarRecepcionNotificacion(Empleado_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_Empleado_Notificacion_upd_recepcion]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_EMPLEADO", registro.IdEmpleado));
                cmd.Parameters.Add(new SqlParameter("@TIPO", "01"));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdEmpleado;
        }
        public Empleado_Registro RegistrarEmpleado(Empleado_Registro registro)
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
                    //cmd.CommandTimeout = cmd.CommandTimeout;

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
                    cmd.Parameters.Add(new SqlParameter("@vTelefono", registro.Telefono));
                    cmd.Parameters.Add(new SqlParameter("@vCelular", registro.Celular));
                    cmd.Parameters.Add(new SqlParameter("@fecha", registro.FechaNacimiento));
                    cmd.Parameters.Add(new SqlParameter("@vRUC", registro.RUC));
                    cmd.Parameters.Add(new SqlParameter("@vCorreoElectronico", registro.CorreoElectronico));
                    cmd.Parameters.Add(new SqlParameter("@bEstado", registro.Estado));
                    cmd.Parameters.Add(new SqlParameter("@vUsuario", registro.IdUsuarioRegistro));
                    cmd.Parameters.Add(new SqlParameter("@iGenero", registro.IdGenero));
                    cmd.Parameters.Add(new SqlParameter("@iCodigoCargo", 0));
                    cmd.Parameters.Add(new SqlParameter("@iDirector", registro.Director));
                    cmd.Parameters.Add(new SqlParameter("@iCondicionTrabajador", registro.IdCondicion));
                    cmd.Parameters.Add(new SqlParameter("@iCodigoSede", registro.IdSede));
                    cmd.Parameters.Add(new SqlParameter("@iGrupoSanguineo", registro.IdGrupoSanguineo));
                    cmd.Parameters.Add(new SqlParameter("@iPais", 0));
                    cmd.Parameters.Add(new SqlParameter("@vCodDep_Nac", "00"));
                    cmd.Parameters.Add(new SqlParameter("@vCodProv_Nac", "00"));
                    cmd.Parameters.Add(new SqlParameter("@vCodDist_Nac", "00"));
                    cmd.Parameters.Add(new SqlParameter("@vCargo", registro.NombreCargo));
                    cmd.Parameters.Add(new SqlParameter("@vFoto", registro.Foto));
                    cmd.Parameters.Add(new SqlParameter("@vTelefonoInstitucional", registro.TelefonoLaboral));
                    cmd.Parameters.Add(new SqlParameter("@vAnexoInstitucional", registro.AnexoLaboral));
                    cmd.Parameters.Add(new SqlParameter("@vCelularInstitucional", registro.CelularLaboral));
                    cmd.Parameters.Add(new SqlParameter("@vCorreoInstitucional", registro.CorreoElectronicoLaboral));
                    cmd.Parameters.Add(new SqlParameter("@iCodigoEstadoCivil", registro.IdEstadoCivil));
                    cmd.Parameters.Add(new SqlParameter("@vUbigeo", registro.DescripcionUbigeo));

                    if (registro.FechaInicio.HasValue) cmd.Parameters.Add(new SqlParameter("@dtInicioLabores", registro.FechaInicio.Value));
                    if (registro.FechaCese.HasValue) cmd.Parameters.Add(new SqlParameter("@dtFinLabores", registro.FechaCese.Value));

                    cmd.Parameters.Add(new SqlParameter("@iTipoPension", registro.IdTipoPension));
                    cmd.Parameters.Add(new SqlParameter("@iTipoAfp", registro.IdTipoAfp));
                    cmd.Parameters.Add(new SqlParameter("@vCodigoAfp", registro.CodigoAfp));
                    cmd.Parameters.Add(new SqlParameter("@iTipoComisionAFP", registro.IdTipoComisionAfp));
                    cmd.Parameters.Add(new SqlParameter("@bEsSaludEPS", registro.EPSEsSalud));
                    cmd.Parameters.Add(new SqlParameter("@bSuspRet4ta", registro.SupsRet4taCat));
                    cmd.Parameters.Add(new SqlParameter("@vNroAutorizacionExoneracion", registro.sNroAutorizacionExoneracion));
                    cmd.Parameters.Add(new SqlParameter("@bDescuentoJudicial", registro.DsctoJudicial));
                    cmd.Parameters.Add(new SqlParameter("@bEncargatura", registro.Encargatura));
                    cmd.Parameters.Add(new SqlParameter("@bFed", registro.FED));
                    cmd.Parameters.Add(new SqlParameter("@bSindicato", registro.Sindicato));
                    cmd.Parameters.Add(new SqlParameter("@bDDJJ", registro.DDJJ));
                    cmd.Parameters.Add(new SqlParameter("@bFFAA", registro.FFAA));
                    cmd.Parameters.Add(new SqlParameter("@bDeportista", registro.Deportista));
                    cmd.Parameters.Add(new SqlParameter("@bDiscapacidad", registro.Discapacidad));

                    cmd.Parameters.Add(new SqlParameter("@iTipoIngreso", registro.IdTipoIngreso));
                    cmd.Parameters.Add(new SqlParameter("@iTipoContrato", registro.IdTipoContrato));
                    cmd.Parameters.Add(new SqlParameter("@vAirhsp", registro.NroAIRHSP));
                    cmd.Parameters.Add(new SqlParameter("@vDocIngreso", registro.DocIngreso));
                    cmd.Parameters.Add(new SqlParameter("@vDocCese", registro.DocCese));
                    cmd.Parameters.Add(new SqlParameter("@vMeta", registro.Meta));
                    cmd.Parameters.Add(new SqlParameter("@vNroContrato", registro.NroContrato));
                    cmd.Parameters.Add(new SqlParameter("@dRemuneracion", registro.Remuneracion));
                    cmd.Parameters.Add(new SqlParameter("@iTipoModalidad", registro.IdTipoModalidad));

                    SqlParameter IdPropuestaParameter = new SqlParameter("@resultado", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(IdPropuestaParameter);
                    cmd.Parameters.Add(new SqlParameter("@CantDirector", SqlDbType.Int)).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();

                    registro.IdEmpleado = Int32.Parse(IdPropuestaParameter.Value.ToString());
                    registro.ExisteDirector = Convert.ToBoolean(cmd.Parameters["@CantDirector"].Value);


                    cmd.Parameters.Clear();
                    cmd.CommandText = "[dbo].[paIngresaMovTrabajadorMaestro]";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@iCodigoTipoDocumento", registro.TipoDocumento));
                    cmd.Parameters.Add(new SqlParameter("@vNumeroDocumento", registro.NroDocumento));
                    cmd.Parameters.Add(new SqlParameter("@iCodEmpleado", registro.IdEmpleado));
                    cmd.Parameters.Add(new SqlParameter("@bEstado", registro.Estado));
                    cmd.Parameters.Add(new SqlParameter("@vUsuario", registro.IdUsuarioRegistro));

                    cmd.ExecuteNonQuery();

                    if (_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) _iBasesSqlAdoUnitOfWorkM.ConfirmarTransaccion();
                }
            }
            catch (Exception)
            {
                if (_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) _iBasesSqlAdoUnitOfWorkM.RetrocederTransaccion();
                throw;
            }
            

            return registro;
        }

        public Empleado_Registro ActualizarEmpleado(Empleado_Registro registro)
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

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                cmd.CommandText = "[dbo].[paActualizaTrabajador]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", registro.IdEmpleado));
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
                cmd.Parameters.Add(new SqlParameter("@vTelefono", registro.Telefono));
                cmd.Parameters.Add(new SqlParameter("@vCelular", registro.Celular));
                cmd.Parameters.Add(new SqlParameter("@fecha", registro.FechaNacimiento));
                cmd.Parameters.Add(new SqlParameter("@vRUC", registro.RUC));
                cmd.Parameters.Add(new SqlParameter("@vCorreoElectronico", registro.CorreoElectronico));
                cmd.Parameters.Add(new SqlParameter("@bEstado", registro.Estado));
                cmd.Parameters.Add(new SqlParameter("@vUsuario", registro.IdUsuarioModificacion));
                cmd.Parameters.Add(new SqlParameter("@iGenero", registro.IdGenero));
                cmd.Parameters.Add(new SqlParameter("@iCodigoCargo", 0));
                cmd.Parameters.Add(new SqlParameter("@iDirector", registro.Director));
                cmd.Parameters.Add(new SqlParameter("@iCondicionTrabajador", registro.IdCondicion));
                cmd.Parameters.Add(new SqlParameter("@iCodigoSede", registro.IdSede));
                cmd.Parameters.Add(new SqlParameter("@iGrupoSanguineo", registro.IdGrupoSanguineo));
                cmd.Parameters.Add(new SqlParameter("@iPais", 0));
                cmd.Parameters.Add(new SqlParameter("@vCodDep_Nac", "00"));
                cmd.Parameters.Add(new SqlParameter("@vCodProv_Nac", "00"));
                cmd.Parameters.Add(new SqlParameter("@vCodDist_Nac", "00"));
                cmd.Parameters.Add(new SqlParameter("@vCargo", registro.NombreCargo));
                cmd.Parameters.Add(new SqlParameter("@vFoto", registro.Foto));
                cmd.Parameters.Add(new SqlParameter("@vTelefonoInstitucional", registro.TelefonoLaboral));
                cmd.Parameters.Add(new SqlParameter("@vAnexoInstitucional", registro.AnexoLaboral));
                cmd.Parameters.Add(new SqlParameter("@vCelularInstitucional", registro.CelularLaboral));
                cmd.Parameters.Add(new SqlParameter("@vCorreoInstitucional", registro.CorreoElectronicoLaboral));
                cmd.Parameters.Add(new SqlParameter("@iCodigoEstadoCivil", registro.IdEstadoCivil));
                cmd.Parameters.Add(new SqlParameter("@vUbigeo", registro.DescripcionUbigeo));

                if (registro.FechaInicio.HasValue) cmd.Parameters.Add(new SqlParameter("@dtInicioLabores", registro.FechaInicio.Value));
                if (registro.FechaCese.HasValue) cmd.Parameters.Add(new SqlParameter("@dtFinLabores", registro.FechaCese.Value));

                cmd.Parameters.Add(new SqlParameter("@iTipoPension", registro.IdTipoPension));
                cmd.Parameters.Add(new SqlParameter("@iTipoAfp", registro.IdTipoAfp));
                cmd.Parameters.Add(new SqlParameter("@vCodigoAfp", registro.CodigoAfp));
                cmd.Parameters.Add(new SqlParameter("@iTipoComisionAFP", registro.IdTipoComisionAfp));
                cmd.Parameters.Add(new SqlParameter("@bEsSaludEPS", registro.EPSEsSalud));
                cmd.Parameters.Add(new SqlParameter("@bSuspRet4ta", registro.SupsRet4taCat));
                cmd.Parameters.Add(new SqlParameter("@vNroAutorizacionExoneracion", registro.sNroAutorizacionExoneracion));
                cmd.Parameters.Add(new SqlParameter("@bDescuentoJudicial", registro.DsctoJudicial));
                cmd.Parameters.Add(new SqlParameter("@bEncargatura", registro.Encargatura));
                cmd.Parameters.Add(new SqlParameter("@bFed", registro.FED));
                cmd.Parameters.Add(new SqlParameter("@bSindicato", registro.Sindicato));
                cmd.Parameters.Add(new SqlParameter("@bDDJJ", registro.DDJJ));
                cmd.Parameters.Add(new SqlParameter("@bFFAA", registro.FFAA));
                cmd.Parameters.Add(new SqlParameter("@bDeportista", registro.Deportista));
                cmd.Parameters.Add(new SqlParameter("@bDiscapacidad", registro.Discapacidad));

                cmd.Parameters.Add(new SqlParameter("@iTipoIngreso", registro.IdTipoIngreso));
                cmd.Parameters.Add(new SqlParameter("@iTipoContrato", registro.IdTipoContrato));
                cmd.Parameters.Add(new SqlParameter("@vAirhsp", registro.NroAIRHSP));
                cmd.Parameters.Add(new SqlParameter("@vDocIngreso", registro.DocIngreso));
                cmd.Parameters.Add(new SqlParameter("@vDocCese", registro.DocCese));
                cmd.Parameters.Add(new SqlParameter("@vMeta", registro.Meta));
                cmd.Parameters.Add(new SqlParameter("@vNroContrato", registro.NroContrato));
                cmd.Parameters.Add(new SqlParameter("@dRemuneracion", registro.Remuneracion));
                cmd.Parameters.Add(new SqlParameter("@iTipoModalidad", registro.IdTipoModalidad));

                cmd.Parameters.Add(new SqlParameter("@CantDirector", SqlDbType.Int)).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                registro.ExisteDirector = Convert.ToBoolean(cmd.Parameters["@CantDirector"].Value);

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro;
        }

        public Int32 RegistrarCese(Empleado_Registro registro) {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                cmd.CommandText = "[dbo].[paActualizaTrabajadorCese]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", registro.IdEmpleado));
                cmd.Parameters.Add(new SqlParameter("@dtFinLabores", registro.FechaCese));
                cmd.Parameters.Add(new SqlParameter("@vDocCese", registro.DocCese));
                cmd.Parameters.Add(new SqlParameter("@vUsuario", registro.IdUsuarioModificacion));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdEmpleado;
        }
        public String ObtenerIndicadores()
        {
            String respuesta = String.Empty;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                cmd.CommandText = "[dbo].[paObtenerIndicadores]";
                cmd.CommandType = CommandType.StoredProcedure;

                respuesta = Convert.ToString(cmd.ExecuteScalar());

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return respuesta;
        }

        public Int32 ActualizarEncargatura(Empleado_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                cmd.CommandText = "[dbo].[paActualizaTrabajadorContacto]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", registro.IdEmpleado));
                cmd.Parameters.Add(new SqlParameter("@bEncargatura", registro.Encargatura));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdEmpleado;
        }

        public Int32 ActualizarEmpleadoContacto(Empleado_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                cmd.CommandText = "[dbo].[paActualizaTrabajadorContacto]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", registro.IdEmpleado));
                cmd.Parameters.Add(new SqlParameter("@vTelefono", registro.Telefono));
                cmd.Parameters.Add(new SqlParameter("@vCelular", registro.Celular));
                cmd.Parameters.Add(new SqlParameter("@vCorreoElectronico", registro.CorreoElectronico));
                cmd.Parameters.Add(new SqlParameter("@vUsuario", registro.IdUsuarioModificacion));
                cmd.Parameters.Add(new SqlParameter("@vTelefonoInstitucional", registro.TelefonoLaboral));
                cmd.Parameters.Add(new SqlParameter("@vAnexoInstitucional", registro.AnexoLaboral));
                cmd.Parameters.Add(new SqlParameter("@vCelularInstitucional", registro.CelularLaboral));
                cmd.Parameters.Add(new SqlParameter("@vCorreoInstitucional", registro.CorreoElectronicoLaboral));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdEmpleado;
        }

        public Empleado_Registro ObtenerParaEditar(Empleado_Request peticion)
        {
            Empleado_Registro registro = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[Usp_Trabajador_listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_EMPLEADO", peticion.IdEmpleado));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            registro = new Empleado_Registro();
                            registro.Grilla = new Grilla_Response();

                            registro.IdEmpleado = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            registro.Nombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            registro.Paterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            registro.Materno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            registro.IdDependencia = dr.GetInt32(dr.GetOrdinal("iCodigoDependencia"));
                            registro.Ubigeo = String.Format("{0}{1}{2}", dr.GetString(dr.GetOrdinal("vCodigoDepartamento")), dr.GetString(dr.GetOrdinal("vCodigoProvincia")), dr.GetString(dr.GetOrdinal("vCodigoDistrito")));
                            registro.TipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento")).ToString().PadLeft(2, '0');
                            registro.NroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            registro.Domicilio = dr.GetString(dr.GetOrdinal("vDomicilio"));
                            registro.Telefono = dr.GetString(dr.GetOrdinal("vTelefono"));
                            registro.Celular = dr.GetString(dr.GetOrdinal("vCelular"));
                            registro.CorreoElectronico = dr.GetString(dr.GetOrdinal("vCorreoElectronico"));
                            //registro.fecnaci = dr.GetInt32(dr.GetOrdinal("dtFechaNacimiento"));
                            //registro.ruc = dr.GetInt32(dr.GetOrdinal("vRUC"));
                            registro.Estado = dr.GetInt32(dr.GetOrdinal("bEstado"));
                            registro.IdGenero = dr.GetInt32(dr.GetOrdinal("iCodigoGenero"));
                            registro.IdCondicion = dr.GetInt32(dr.GetOrdinal("iCodTipoCondicionTrabajador"));
                            registro.IdSede = dr.GetInt32(dr.GetOrdinal("iCodigoSede"));
                            registro.IdGrupoSanguineo = dr.GetInt32(dr.GetOrdinal("iCodGrupoSanguineo"));
                            registro.NombreCargo = dr.GetString(dr.GetOrdinal("vCargo"));
                            registro.Foto = dr.GetString(dr.GetOrdinal("vFoto"));
                            registro.TelefonoLaboral = dr.GetString(dr.GetOrdinal("vTelefonoInstitucional"));
                            registro.AnexoLaboral = dr.GetString(dr.GetOrdinal("vAnexoInstitucional"));
                            registro.CelularLaboral = dr.GetString(dr.GetOrdinal("vCelularInstitucional"));
                            registro.CorreoElectronicoLaboral = dr.GetString(dr.GetOrdinal("vCorreoInstitucional"));
                            registro.IdEstadoCivil = dr.GetInt32(dr.GetOrdinal("iCodigoEstadoCivil"));
                            registro.DescripcionUbigeo = dr.GetString(dr.GetOrdinal("vUbigeo"));
                            registro.RUC = dr.GetString(dr.GetOrdinal("vRUC"));

                            registro.IdTipoPension = dr.GetInt32(dr.GetOrdinal("iTipoPension"));
                            registro.IdTipoAfp = dr.GetInt32(dr.GetOrdinal("iTipoAfp"));
                            registro.CodigoAfp = dr.GetString(dr.GetOrdinal("vCodigoAfp"));
                            registro.IdTipoComisionAfp = dr.GetInt32(dr.GetOrdinal("iTipoComisionAFP"));
                            registro.EPSEsSalud = dr.GetBoolean(dr.GetOrdinal("bEsSaludEPS"));
                            registro.SupsRet4taCat = dr.GetBoolean(dr.GetOrdinal("bSuspRet4ta"));
                            registro.DsctoJudicial = dr.GetBoolean(dr.GetOrdinal("bDescuentoJudicial"));
                            registro.sNroAutorizacionExoneracion = dr.GetString(dr.GetOrdinal("vNroAutorizacionExoneracion"));
                            if (dr.IsDBNull(dr.GetOrdinal("iDirector"))) registro.Director = 0;
                            else registro.Director = dr.GetInt32(dr.GetOrdinal("iDirector"));
                            registro.Encargatura = dr.GetBoolean(dr.GetOrdinal("bEncargatura"));
                            registro.FED = dr.GetBoolean(dr.GetOrdinal("bFED"));
                            registro.Sindicato = dr.GetBoolean(dr.GetOrdinal("bSindicato"));
                            registro.DDJJ = dr.GetBoolean(dr.GetOrdinal("bDDJJ"));
                            registro.FFAA = dr.GetBoolean(dr.GetOrdinal("bFFAA"));
                            registro.Deportista = dr.GetBoolean(dr.GetOrdinal("bDeportista"));
                            registro.Discapacidad = dr.GetBoolean(dr.GetOrdinal("bDiscapacidad"));
                            if (!dr.IsDBNull(dr.GetOrdinal("dtFechaNacimiento")))
                            {
                                DateTime aux;
                                if (DateTime.TryParse(dr.GetString(dr.GetOrdinal("dtFechaNacimiento")), out aux))
                                    registro.FechaNacimiento = aux.ToString("dd/MM/yyyy");
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("dtInicioLabores"))) registro.FechaInicio = dr.GetDateTime(dr.GetOrdinal("dtInicioLabores"));
                            if (!dr.IsDBNull(dr.GetOrdinal("dtFinLabores"))) registro.FechaCese = dr.GetDateTime(dr.GetOrdinal("dtFinLabores"));

                            if (registro.DsctoJudicial)
                            {
                                registro.lstDescuentoJudicialBeneficiario_Registro = new List<DescuentoJudicialBeneficiario_Registro>();
                                registro.lstDescuentoJudicialBeneficiario_Registro = ObtenerBeneficiarios(peticion);
                            }

                            registro.IdTipoIngreso = dr.GetInt32(dr.GetOrdinal("iTipoIngreso"));
                            registro.IdTipoContrato = dr.GetInt32(dr.GetOrdinal("iTipoContrato"));
                            registro.NroAIRHSP = dr.GetString(dr.GetOrdinal("vAirhsp"));
                            registro.DocIngreso = dr.GetString(dr.GetOrdinal("vDocIngreso"));
                            registro.DocCese = dr.GetString(dr.GetOrdinal("vDocCese"));
                            registro.Meta = dr.GetString(dr.GetOrdinal("vMeta"));
                            registro.NroContrato = dr.GetString(dr.GetOrdinal("vNroContrato"));
                            registro.Remuneracion = dr.GetDecimal(dr.GetOrdinal("dRemuneracion"));
                            registro.IdTipoModalidad = dr.GetInt32(dr.GetOrdinal("iTipoModalidad"));

                            registro.NombreOficina = dr.GetString(dr.GetOrdinal("vDependencia"));
                            registro.CondicionLaboral = dr.GetString(dr.GetOrdinal("CondicionLaboral"));
                            registro.TipoIngreso = dr.GetString(dr.GetOrdinal("TipoIngreso"));
                            registro.Sede = dr.GetString(dr.GetOrdinal("Sede"));
                            registro.GrupoSanguineo = dr.GetString(dr.GetOrdinal("GrupoSanguineo"));
                            //if (!dr.IsDBNull(dr.GetOrdinal("PERSONA_ID_CENTRO_POBLADO_DOMICILIO"))) registro.Persona.IdCentroPobladoDomicilio = dr.GetInt32(dr.GetOrdinal("PERSONA_ID_CENTRO_POBLADO_DOMICILIO"));
                        }
                    }
                    if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
                }
            }

            return registro;
        }

        public List<DescuentoJudicialBeneficiario_Registro> ObtenerBeneficiarios(Empleado_Request peticion)
        {

            List<DescuentoJudicialBeneficiario_Registro> lstDescuentoJudicialBeneficiario_Registro = null;
            using (SqlCommand cmdBene = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmdBene.Connection.State != ConnectionState.Open) cmdBene.Connection.Open();
                cmdBene.Parameters.Add(new SqlParameter("@iCodTrabajador", peticion.IdEmpleado));
                lstDescuentoJudicialBeneficiario_Registro = new List<DescuentoJudicialBeneficiario_Registro>();
                DescuentoJudicialBeneficiario_Registro item = null;


                cmdBene.CommandText = "[dbo].[paPlanillaJudicialTrabajadorBeneficiarioNomina_Lista]";
                cmdBene.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;



                using (SqlDataReader drBeneficiarios = cmdBene.ExecuteReader())
                {
                    if (drBeneficiarios.HasRows)
                    {
                        while (drBeneficiarios.Read())
                        {
                            item = new DescuentoJudicialBeneficiario_Registro();

                            item.Grilla = new Grilla_Response();
                            //item.iNro = dr.GetInt64(dr.GetOrdinal("Nro"));
                            item.iCodJudicial = drBeneficiarios.GetInt32(drBeneficiarios.GetOrdinal("iCodPlanillaJudicial"));
                            item.iCodJudicialDetalle = drBeneficiarios.GetInt32(drBeneficiarios.GetOrdinal("iCodPlanillaJudicialBeneficiario"));
                            item.iCodTrabajador = drBeneficiarios.GetInt32(drBeneficiarios.GetOrdinal("iCodTrabajador"));
                            item.vDniBeneficiario = drBeneficiarios.GetString(drBeneficiarios.GetOrdinal("vDniBeneficiario"));
                            item.vNombreBeneficiario = drBeneficiarios.GetString(drBeneficiarios.GetOrdinal("vNombreBeneficiario"));
                            item.iCodigoBanco = drBeneficiarios.GetInt32(drBeneficiarios.GetOrdinal("iCodBanco"));
                            item.vNombreBanco = drBeneficiarios.GetString(drBeneficiarios.GetOrdinal("vNombreBanco"));
                            item.vNumeroCuenta = drBeneficiarios.GetString(drBeneficiarios.GetOrdinal("vNumeroCuenta"));
                            item.iCodTipoRetencion = drBeneficiarios.GetInt32(drBeneficiarios.GetOrdinal("iCodTipoRetencion"));
                            item.vNombreRetencion = drBeneficiarios.GetString(drBeneficiarios.GetOrdinal("vNombreRetencion"));
                            item.dValorPorcentaje = drBeneficiarios.GetDecimal(drBeneficiarios.GetOrdinal("dValorPorcentaje"));
                            item.dMontoRetencion = drBeneficiarios.GetDecimal(drBeneficiarios.GetOrdinal("dMontoRetencion"));
                            item.vObservacion = drBeneficiarios.GetString(drBeneficiarios.GetOrdinal("vObservacion"));
                            item.iCodFormaPago = drBeneficiarios.GetInt32(drBeneficiarios.GetOrdinal("iCodFormaPago"));
                            item.vNombreFormaPago = drBeneficiarios.GetString(drBeneficiarios.GetOrdinal("vNombreFormaPago"));
                            item.sFechaLlegadaDoc = drBeneficiarios.GetString(drBeneficiarios.GetOrdinal("dtFechaLlegadaDoc"));
                            lstDescuentoJudicialBeneficiario_Registro.Add(item);

                        }
                    }
                }
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmdBene.Connection.Close();
            }
            return lstDescuentoJudicialBeneficiario_Registro;
        }

        public Int32 RegistrarCuentaEmpleado(EmpleadoCuenta_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paIngresaTrabajadorCuenta]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodigoTrabajador", registro.IdEmpleado));
                cmd.Parameters.Add(new SqlParameter("@iCodigoBanco", registro.IdBanco));
                cmd.Parameters.Add(new SqlParameter("@vCuenta", registro.NroCuenta));
                cmd.Parameters.Add(new SqlParameter("@vCCI", registro.CCI));
                cmd.Parameters.Add(new SqlParameter("@bEstado", registro.IdEstado));
                cmd.Parameters.Add(new SqlParameter("@vAuditCreacion", registro.IdUsuarioRegistro));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdEmpleado;
        }

        public Int32 RegistrarOrdenEmpleado(EmpleadoOrden_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paIngresaTrabajadorOrdenServicio]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodigoTrabajador", registro.IdEmpleado));
                cmd.Parameters.Add(new SqlParameter("@vNroOrden", registro.NroOrden));
                cmd.Parameters.Add(new SqlParameter("@vNroSIAF", registro.NroSIAF));
                cmd.Parameters.Add(new SqlParameter("@vNombre", registro.Nombre));
                cmd.Parameters.Add(new SqlParameter("@iDuracion", registro.Duracion));
                cmd.Parameters.Add(new SqlParameter("@dFechaInicio", DateTime.Parse(registro.FechaInicio)));
                cmd.Parameters.Add(new SqlParameter("@dFechaFin", DateTime.Parse(registro.FechaFin)));
                cmd.Parameters.Add(new SqlParameter("@vMonto", registro.Monto));
                cmd.Parameters.Add(new SqlParameter("@bEstado", registro.IdEstado));
                cmd.Parameters.Add(new SqlParameter("@vAuditCreacion", registro.IdUsuarioRegistro));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdEmpleado;
        }
        public Int32 RegistrarSolicitudBoleta(EmpleadoBoletaSolicitud_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_SolicitudBoleta_ins]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@NroDOCUMENTO", registro.Documento));
                cmd.Parameters.Add(new SqlParameter("@CLAVE", registro.Clave));
                SqlParameter IdPropuestaParameter = new SqlParameter("@ID_REGISTRO", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(IdPropuestaParameter);

                cmd.ExecuteNonQuery();
                registro.IdSolicitud = Int32.Parse(IdPropuestaParameter.Value.ToString());

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdSolicitud;
        }
        public String ValidarSolicitudBoleta(EmpleadoBoletaSolicitud_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_SolicitudBoleta_validar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_REGISTRO", registro.IdSolicitud));
                cmd.Parameters.Add(new SqlParameter("@CLAVE", registro.Clave));
                SqlParameter IdPropuestaParameter = new SqlParameter("@ID_ESTADO", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(IdPropuestaParameter);
                SqlParameter IdPropuestaParameter2 = new SqlParameter("@NroDOCUMENTO", SqlDbType.VarChar, 10)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(IdPropuestaParameter2);

                cmd.ExecuteNonQuery();
                registro.Estado = Int32.Parse(IdPropuestaParameter.Value.ToString());
                registro.Documento = IdPropuestaParameter2.Value.ToString();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.Estado.ToString() + "|" + registro.Documento;
        }

        public Int32 ActualizarCuentaEmpleado(EmpleadoCuenta_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paActualizaTrabajadorCuenta]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodigoTrabajadorBanco", registro.IdEmpleadoBanco));
                cmd.Parameters.Add(new SqlParameter("@iCodigoTrabajador", registro.IdEmpleado));
                cmd.Parameters.Add(new SqlParameter("@iCodigoBanco", registro.IdBanco));
                cmd.Parameters.Add(new SqlParameter("@vCuenta", registro.NroCuenta));
                cmd.Parameters.Add(new SqlParameter("@vCCI", registro.CCI));
                cmd.Parameters.Add(new SqlParameter("@bEstado", registro.IdEstado));
                cmd.Parameters.Add(new SqlParameter("@vAuditModificacion", registro.IdUsuarioModificacion));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdEmpleado;
        }
        public Int32 ActualizarOrdenEmpleado(EmpleadoOrden_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paActualizaTrabajadorOrdenServicio]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodigoTrabajadorOrden", registro.IdEmpleadoOrden));
                cmd.Parameters.Add(new SqlParameter("@iCodigoTrabajador", registro.IdEmpleado));
                cmd.Parameters.Add(new SqlParameter("@vNroOrden", registro.NroOrden));
                cmd.Parameters.Add(new SqlParameter("@vNroSIAF", registro.NroSIAF));
                cmd.Parameters.Add(new SqlParameter("@vNombre", registro.Nombre));
                cmd.Parameters.Add(new SqlParameter("@iDuracion", registro.Duracion));
                cmd.Parameters.Add(new SqlParameter("@dFechaInicio", DateTime.Parse(registro.FechaInicio)));
                cmd.Parameters.Add(new SqlParameter("@dFechaFin", DateTime.Parse(registro.FechaFin)));
                cmd.Parameters.Add(new SqlParameter("@vMonto", registro.Monto));
                cmd.Parameters.Add(new SqlParameter("@bEstado", registro.IdEstado));
                cmd.Parameters.Add(new SqlParameter("@vAuditModificacion", registro.IdUsuarioModificacion));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdEmpleado;
        }
        public Int32 EliminarCuentaEmpleado(EmpleadoCuenta_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paEliminarTrabajadorCuenta]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodigoTrabajadorBanco", registro.IdEmpleadoBanco));
                cmd.Parameters.Add(new SqlParameter("@vAuditModificacion", registro.IdUsuarioModificacion));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdEmpleado;
        }

        public Int32 EliminarHistorialEmpleado(EmpleadoCuenta_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paEliminarTrabajadorHistorial]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodigoTrabajadorBanco", registro.IdEmpleadoBanco));
                cmd.Parameters.Add(new SqlParameter("@vAuditModificacion", registro.IdUsuarioModificacion));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdEmpleado;
        }

        public Int32 EliminarOrdenEmpleado(EmpleadoOrden_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paEliminarTrabajadorOrdenServicio]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodigoTrabajadorOrden", registro.IdEmpleadoOrden));
                cmd.Parameters.Add(new SqlParameter("@iCodigoTrabajador", registro.IdEmpleado));
                cmd.Parameters.Add(new SqlParameter("@vAuditModificacion", registro.IdUsuarioModificacion));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdEmpleado;
        }

        public IEnumerable<EmpleadoEncargatura_Registro> ListarEncargaturasEmpleado(Empleado_Request peticion)
        {
            List<EmpleadoEncargatura_Registro> lista = new List<EmpleadoEncargatura_Registro>();
            EmpleadoEncargatura_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paListarTrabajadorEncargatura]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                //cmd.Parameters.Add(new SqlParameter("@ID_EMPLEADO", peticion.IdEmpleado));
                //cmd.Parameters.Add(new SqlParameter("@ESTADO", peticion.Estado));
                cmd.Parameters.Add(new SqlParameter("@TIPO_DOCUMENTO", peticion.TipoDocumento));
                cmd.Parameters.Add(new SqlParameter("@NRO_DOCUMENTO", peticion.NroDocumento));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new EmpleadoEncargatura_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdMaestro = dr.GetInt32(dr.GetOrdinal("iCodMaestro"));
                            item.IdMaestroDetalle = dr.GetInt32(dr.GetOrdinal("iDetalle"));
                            item.IdEmpleadoEncargatura = dr.GetInt32(dr.GetOrdinal("iCodMaeTrabDepEnc"));
                            item.IdEmpleado = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            item.IdDependenciaEncargatura = dr.GetInt32(dr.GetOrdinal("iCodDepEncargatura"));
                            item.FechaIni = dr.GetDateTime(dr.GetOrdinal("dtFecIni"));
                            item.DocEncargatura = dr.GetString(dr.GetOrdinal("vDocEncargatura"));
                            item.DocEncargaturaFin = dr.GetString(dr.GetOrdinal("vDocEncargaturaFin"));
                            int x = dr.GetOrdinal("dtFecFin");
                            item.FechaFin = dr.IsDBNull(x) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("dtFecFin"));
                            item.Estado = new Estado_Response()
                            {
                                Codigo = dr.GetInt32(dr.GetOrdinal("bEstado")).ToString(),
                                Nombre = (dr.GetInt32(dr.GetOrdinal("bEstado")).ToString() == "0" ? "INACTIVO" : (dr.GetInt32(dr.GetOrdinal("bEstado")).ToString() == "1" ? "ACTIVO" : String.Empty))
                            };
                            item.DDJJ = new Estado_Response()
                            {
                                Codigo = dr.GetInt32(dr.GetOrdinal("bDDJJ")).ToString(),
                                Nombre = (dr.GetInt32(dr.GetOrdinal("bDDJJ")).ToString() == "0" ? "NO" : (dr.GetInt32(dr.GetOrdinal("bDDJJ")).ToString() == "1" ? "SI" : String.Empty))
                            };

                            item.Dependencia = new Dependencia_Registro()
                            {
                                IdDependencia = dr.GetInt32(dr.GetOrdinal("iCodDepEncargatura")),
                                Nombre = dr.GetString(dr.GetOrdinal("vDependencia"))
                            };

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<EmpleadoFamiliar_Registro> ListarFamiliaresEmpleado(Empleado_Request peticion)
        {
            List<EmpleadoFamiliar_Registro> lista = new List<EmpleadoFamiliar_Registro>();
            EmpleadoFamiliar_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paListarTrabajadorFamiliar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_EMPLEADO", peticion.IdEmpleado));
                cmd.Parameters.Add(new SqlParameter("@ESTADO", peticion.Estado));
                //cmd.Parameters.Add(new SqlParameter("@TIPO_DOCUMENTO", peticion.TipoDocumento));
                //cmd.Parameters.Add(new SqlParameter("@NRO_DOCUMENTO", peticion.NroDocumento));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new EmpleadoFamiliar_Registro();

                            item.Grilla = new Grilla_Response();
                            //item.IdEmpleadoFamiliar = dr.GetInt32(dr.GetOrdinal("iCodTrabajadorFam"));
                            item.IdEmpleado = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
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
        public IEnumerable<Empleado_Registro> ListarDesplazamientoEmpleado(Empleado_Request peticion)
        {
            List<Empleado_Registro> lista = new List<Empleado_Registro>();
            Empleado_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paListarTrabajadorDesplazamiento]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@TIPO_DOCUMENTO", peticion.TipoDocumento));
                cmd.Parameters.Add(new SqlParameter("@NRO_DOCUMENTO", peticion.NroDocumento));

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
                            item.NombreEntidad = dr.GetString(dr.GetOrdinal("vEntidad"));
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
                            item.TipoIngreso = dr.GetString(dr.GetOrdinal("TipoIngreso"));
                            item.NroContrato = dr.GetString(dr.GetOrdinal("NroContrato"));

                            item.DocIngreso = dr.GetString(dr.GetOrdinal("vDocIngreso"));
                            item.Remuneracion = dr.GetDecimal(dr.GetOrdinal("dRemuneracion"));


                            item.IdMaestro = dr.GetInt32(dr.GetOrdinal("iCodMaestro"));
                            item.IdMaestroDetalle = dr.GetInt32(dr.GetOrdinal("iDetalle"));
                            if (!dr.IsDBNull(dr.GetOrdinal("dtInicioLabores"))) item.FechaInicio = dr.GetDateTime(dr.GetOrdinal("dtInicioLabores"));
                            if (!dr.IsDBNull(dr.GetOrdinal("dtFinLabores"))) item.FechaCese = dr.GetDateTime(dr.GetOrdinal("dtFinLabores"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public string RegistrarEncargaturasEmpleado(EmpleadoEncargatura_Registro registro)
        {
            string msg = string.Empty;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paIngresaTrabajadorEncargatura]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodigoTrabajador", registro.IdEmpleado));
                cmd.Parameters.Add(new SqlParameter("@iCodDepEncargatura", registro.IdDependenciaEncargatura));
                cmd.Parameters.Add(new SqlParameter("@dtFecIni", registro.FechaIni));
                if (registro.FechaFin != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@dtFecFin", registro.FechaFin));
                }

                cmd.Parameters.Add(new SqlParameter("@bDDJJ", registro.IdDDJJ));
                cmd.Parameters.Add(new SqlParameter("@vDocEncargatura", registro.DocEncargatura));
                cmd.Parameters.Add(new SqlParameter("@vDocEncargaturaFin", registro.DocEncargaturaFin));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabAuditCreacion", registro.IdUsuarioRegistro));

                SqlParameter Mensaje = new SqlParameter("@nvMensaje", SqlDbType.VarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(Mensaje);


                cmd.ExecuteNonQuery();
                msg = Mensaje.Value.ToString();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return msg;
        }

        public string ActualizarEncargaturasEmpleado(EmpleadoEncargatura_Registro registro)
        {
            string msg = string.Empty;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paActualizaTrabajadorEncargatura]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodMaeTrabDepEnc", registro.IdEmpleadoEncargatura));
                cmd.Parameters.Add(new SqlParameter("@iCodigoTrabajador", registro.IdEmpleado));
                cmd.Parameters.Add(new SqlParameter("@iCodDepEncargatura", registro.IdDependenciaEncargatura));
                cmd.Parameters.Add(new SqlParameter("@bEstado", registro.IdEstado));
                cmd.Parameters.Add(new SqlParameter("@bDDJJ", registro.IdDDJJ));
                cmd.Parameters.Add(new SqlParameter("@vDocEncargatura", registro.DocEncargatura));
                cmd.Parameters.Add(new SqlParameter("@vDocEncargaturaFin", registro.DocEncargaturaFin));
                cmd.Parameters.Add(new SqlParameter("@dtFecIni", registro.FechaIni));
                cmd.Parameters.Add(new SqlParameter("@dtFecFin", registro.FechaFin));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabAuditModificacion", registro.IdUsuarioModificacion));

                SqlParameter Mensaje = new SqlParameter("@nvMensaje", SqlDbType.VarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(Mensaje);


                cmd.ExecuteNonQuery();
                msg = Mensaje.Value.ToString();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return msg;
        }

        public Int32 RegistrarFamiliarEmpleado(EmpleadoFamiliar_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paIngresaTrabajadorFamiliar]"; 
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", registro.IdEmpleado));
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

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdFamiliar;
        }
        public Int32 ActualizarFamiliarEmpleado(EmpleadoFamiliar_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paActualizaTrabajadorFamiliar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", registro.IdEmpleado));
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

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdFamiliar;
        }
        public Int32 EliminarFamiliarEmpleado(EmpleadoFamiliar_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paEliminaTrabajadorFamiliar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", registro.IdEmpleado));
                cmd.Parameters.Add(new SqlParameter("@iCodFamiliar", registro.IdFamiliar));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdFamiliar;
        }
        public IEnumerable<Empleado_Registro> ListarParticipantesCertificadoPrueba(Empleado_Request peticion)
        {
            List<Empleado_Registro> lista = new List<Empleado_Registro>();
            Empleado_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "SELECT [F1], [NOMBRE Y APELLIDO (VARIOS RECTIFICADOS)], [CORREO ELECTRÓNICO REGISTRADO], [CORREO ELECTRÓNICO ALTERNATIVO], [PAÍS], [ORGANIZACIÓN], [ESTATUS] FROM [BDSISGESRRHH].[dbo].[PERÚ$]";
                cmd.CommandType = CommandType.Text;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Empleado_Registro();

                            item.Grilla = new Grilla_Response();
                            item.Nombre = dr.GetString(1);
                            item.Sede = dr.GetString(5);
                            item.CorreoElectronicoLaboral = dr.GetString(2);
                            if (!dr.IsDBNull(3)) item.CorreoElectronico = dr.GetString(3);

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<Empleado_Registro> ListarEmpleadoAltas(EmpleadoReporte_Request peticion)
        {
            List<Empleado_Registro> lista = new List<Empleado_Registro>();
            Empleado_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paListaReporteAltas]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ANIO", peticion.Anio));
                cmd.Parameters.Add(new SqlParameter("@MES", peticion.Mes));
                
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

                            item.IdTipoIngreso = dr.GetInt32(dr.GetOrdinal("iTipoIngreso"));
                            item.IdTipoContrato = dr.GetInt32(dr.GetOrdinal("iTipoContrato"));
                            item.NroAIRHSP = dr.GetString(dr.GetOrdinal("vAirhsp"));
                            item.DocIngreso = dr.GetString(dr.GetOrdinal("vDocIngreso"));
                            item.DocCese = dr.GetString(dr.GetOrdinal("vDocCese"));
                            item.Meta = dr.GetString(dr.GetOrdinal("vMeta"));
                            item.NroContrato = dr.GetString(dr.GetOrdinal("vNroContrato"));
                            item.Remuneracion = dr.GetDecimal(dr.GetOrdinal("dRemuneracion"));
                            item.NombreOficina = dr.GetString(dr.GetOrdinal("vDependencia"));
                            item.TipoIngreso = dr.GetString(dr.GetOrdinal("TipoIngreso"));
                            
                            if (!dr.IsDBNull(dr.GetOrdinal("dtInicioLabores"))) item.FechaInicio = dr.GetDateTime(dr.GetOrdinal("dtInicioLabores"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<Empleado_Registro> ListarEmpleadoBajas(EmpleadoReporte_Request peticion)
        {
            List<Empleado_Registro> lista = new List<Empleado_Registro>();
            Empleado_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paListaReporteBajas]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ANIO", peticion.Anio));
                cmd.Parameters.Add(new SqlParameter("@MES", peticion.Mes));

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

                            item.IdTipoIngreso = dr.GetInt32(dr.GetOrdinal("iTipoIngreso"));
                            item.IdTipoContrato = dr.GetInt32(dr.GetOrdinal("iTipoContrato"));
                            item.NroAIRHSP = dr.GetString(dr.GetOrdinal("vAirhsp"));
                            item.DocIngreso = dr.GetString(dr.GetOrdinal("vDocIngreso"));
                            item.DocCese = dr.GetString(dr.GetOrdinal("vDocCese"));
                            item.Meta = dr.GetString(dr.GetOrdinal("vMeta"));
                            item.NroContrato = dr.GetString(dr.GetOrdinal("vNroContrato"));
                            item.Remuneracion = dr.GetDecimal(dr.GetOrdinal("dRemuneracion"));
                            item.NombreOficina = dr.GetString(dr.GetOrdinal("vDependencia"));
                            item.TipoIngreso = dr.GetString(dr.GetOrdinal("TipoIngreso"));

                            if (!dr.IsDBNull(dr.GetOrdinal("dtInicioLabores"))) item.FechaInicio = dr.GetDateTime(dr.GetOrdinal("dtInicioLabores"));
                            if (!dr.IsDBNull(dr.GetOrdinal("dtFinLabores"))) item.FechaCese = dr.GetDateTime(dr.GetOrdinal("dtFinLabores"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        #region Empleado Estudios
        public IEnumerable<EmpleadoEstudio_Registro> ListarEmpleadoEstudios(Empleado_Request peticion)
        {
            List<EmpleadoEstudio_Registro> lista = new List<EmpleadoEstudio_Registro>();
            EmpleadoEstudio_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paListarTrabajadorEstudio]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ID_EMPLEADO", peticion.IdEmpleado));
                cmd.Parameters.Add(new SqlParameter("@ESTADO", peticion.Estado));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new EmpleadoEstudio_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdEstudio = dr.GetInt32(dr.GetOrdinal("iCodEstudio"));
                            item.IdEmpleado = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            item.IdSinTitulo = dr.GetInt32(dr.GetOrdinal("icodsintitulo"));
                            item.Especialidad = dr.GetString(dr.GetOrdinal("vEspecialidad"));
                            item.Institucion = dr.GetString(dr.GetOrdinal("vInstitucion"));
                            item.Ciudad = dr.GetString(dr.GetOrdinal("vCiudad"));
                            item.IdFechaInicioMes = dr.GetInt32(dr.GetOrdinal("iFechaInicioMes"));
                            item.IdFechaInicioAnio = dr.GetInt32(dr.GetOrdinal("iFechaInicioAnio"));
                            item.IdFechaFinMes = dr.GetInt32(dr.GetOrdinal("iFechaFinMes"));
                            item.IdFechaFinAnio = dr.GetInt32(dr.GetOrdinal("iFechaFinAnio"));
                            item.NivelAlcanzado = new PerfillNivelEducativo_Response()
                            {
                                iCodNivel = dr.GetInt32(dr.GetOrdinal("iCodNivel")),
                                strDescripcion = dr.GetString(dr.GetOrdinal("vPerfilNivel"))
                            };
                            item.Obtencion_Estudio_Mes = new Mes_Response(
                                dr.GetInt32(dr.GetOrdinal("iMes")).ToString().PadLeft(2, '0'),
                                dr.GetString(dr.GetOrdinal("vObtMes"))
                                );
                            item.Obtencion_Estudio_Anio = new Anio_Response()
                            {
                                Anio = dr.GetInt32(dr.GetOrdinal("iAnio")).ToString()
                            };
                            item.Inicio_Estudio_Mes = new Mes_Response(
                                dr.GetInt32(dr.GetOrdinal("iFechaInicioMes")).ToString().PadLeft(2, '0'),
                                dr.GetString(dr.GetOrdinal("vInicioMes"))
                                );
                            item.Inicio_Estudio_Anio = new Anio_Response()
                            {
                                Anio = dr.GetInt32(dr.GetOrdinal("iFechaInicioAnio")).ToString()
                            };
                            item.Fin_Estudio_Mes = new Mes_Response(
                                dr.GetInt32(dr.GetOrdinal("iFechaFinMes")).ToString().PadLeft(2, '0'),
                                dr.GetString(dr.GetOrdinal("vFinMes"))
                                );
                            item.Fin_Estudio_Anio = new Anio_Response()
                            {
                                Anio = dr.GetInt32(dr.GetOrdinal("iFechaFinAnio")).ToString()
                            };
                            lista.Add(item);
                        }
                    }
                }
                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }
            return lista;
        }
        public Int32 RegistrarEmpleadoEstudio(EmpleadoEstudio_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paIngresaTrabajadorEstudio]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", registro.IdEmpleado));
                cmd.Parameters.Add(new SqlParameter("@iCodNivel", registro.IdNivel));
                cmd.Parameters.Add(new SqlParameter("@vEspecialidad", registro.Especialidad));
                cmd.Parameters.Add(new SqlParameter("@vInstitucion", registro.Institucion));
                cmd.Parameters.Add(new SqlParameter("@iMes", registro.IdMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", registro.IdAnio));
                cmd.Parameters.Add(new SqlParameter("@vCiudad", registro.Ciudad));
                cmd.Parameters.Add(new SqlParameter("@iFechaInicioMes", registro.IdFechaInicioMes));
                cmd.Parameters.Add(new SqlParameter("@iFechaInicioAnio", registro.IdFechaInicioAnio));
                cmd.Parameters.Add(new SqlParameter("@iFechaFinMes", registro.IdFechaFinMes));
                cmd.Parameters.Add(new SqlParameter("@iFechaFinAnio", registro.IdFechaFinAnio));
                cmd.Parameters.Add(new SqlParameter("@vAuditCreacion", registro.IdUsuarioRegistro));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdEstudio;
        }
        public Int32 ActualizarEmpleadoEstudio(EmpleadoEstudio_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paActualizaTrabajadorEstudio]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodEstudio", registro.IdEstudio));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", registro.IdEmpleado));
                cmd.Parameters.Add(new SqlParameter("@iCodNivel", registro.IdNivel));
                cmd.Parameters.Add(new SqlParameter("@vEspecialidad", registro.Especialidad));
                cmd.Parameters.Add(new SqlParameter("@vInstitucion", registro.Institucion));
                cmd.Parameters.Add(new SqlParameter("@iMes", registro.IdMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", registro.IdAnio));
                cmd.Parameters.Add(new SqlParameter("@vCiudad", registro.Ciudad));
                cmd.Parameters.Add(new SqlParameter("@iFechaInicioMes", registro.IdFechaInicioMes));
                cmd.Parameters.Add(new SqlParameter("@iFechaInicioAnio", registro.IdFechaInicioAnio));
                cmd.Parameters.Add(new SqlParameter("@iFechaFinMes", registro.IdFechaFinMes));
                cmd.Parameters.Add(new SqlParameter("@iFechaFinAnio", registro.IdFechaFinAnio));
                cmd.Parameters.Add(new SqlParameter("@vAuditModificacion", registro.IdUsuarioModificacion.ToString()));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdEstudio;
        }
        public Int32 EliminarEmpleadoEstudio(EmpleadoEstudio_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkM.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paEliminaTrabajadorEstudio]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", registro.IdEmpleado));
                cmd.Parameters.Add(new SqlParameter("@iCodEstudio", registro.IdEstudio));
                cmd.Parameters.Add(new SqlParameter("@vAuditModificacion", registro.IdUsuarioModificacion.ToString()));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWorkM.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.IdEstudio;
        }
        #endregion

    }
}
