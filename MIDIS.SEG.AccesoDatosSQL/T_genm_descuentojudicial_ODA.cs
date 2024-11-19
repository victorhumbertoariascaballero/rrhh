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
    public partial class T_genm_descuentojudicial_ODA
    {
        //BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWork = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnOracleM"]].ConnectionString));

        BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWork = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnOracle"]].ConnectionString));

        #region Metodos Generales

        string vSqlString = new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnOracleM"]].ConnectionString);

        public IEnumerable<Banco_Registro> ListarBancos(Banco_Request peticion)
        {
            List<Banco_Registro> lista = new List<Banco_Registro>();
            Banco_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaBanco_Listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                //cmd.Parameters.Add(new SqlParameter("@IdBanco", peticion.IdBanco));
                //cmd.Parameters.Add(new SqlParameter("@Nombre", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Banco_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdBanco = dr.GetInt32(dr.GetOrdinal("iCodigoBanco"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("vDescripcion"));

                            lista.Add(item);

                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }


        public IEnumerable<DescuentoJudicial_Registro> ListarDescuentoJudicial(DescuentoJudicial_Request peticion)
        {
            List<DescuentoJudicial_Registro> lista = new List<DescuentoJudicial_Registro>();
            DescuentoJudicial_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaJudicialTrabajador_Lista]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;
                cmd.Parameters.Add(new SqlParameter("@iMes", peticion.iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", peticion.iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", peticion.iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", peticion.iCodTipoPlanilla));                
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", peticion.iCodDetPlanilla));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new DescuentoJudicial_Registro();

                            item.Grilla = new Grilla_Response();
                            item.iNro = dr.GetInt64(dr.GetOrdinal("Nro"));
                            item.iCodJudicial = dr.GetInt32(dr.GetOrdinal("iCodJudicial"));
                            item.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            item.vTrabajador = dr.GetString(dr.GetOrdinal("vNombreTrabajador"));
                            item.vNroDocumentoTrabajador = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            item.iCodPlanilla = dr.GetInt32(dr.GetOrdinal("iCodPlanilla"));
                            item.vNombrePlanilla = dr.GetString(dr.GetOrdinal("vPlanilla"));
                            item.iCodTipoPlanilla = dr.GetInt32(dr.GetOrdinal("iCodTipoPlanilla"));
                            item.vTipoPlanilla = dr.GetString(dr.GetOrdinal("vTipoPlanilla"));
                            item.iAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            item.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            item.dMontoRetencionTotal = dr.GetDecimal(dr.GetOrdinal("dMontoRetencionTotal"));
                            item.iCodDetPlanilla = dr.GetInt32(dr.GetOrdinal("iCodDetPlanilla"));

                            lista.Add(item);

                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<DescuentoJudicial_Registro> ListarDescuentoJudicialTrabajadores()
        {
            List<DescuentoJudicial_Registro> lista = new List<DescuentoJudicial_Registro>();
            DescuentoJudicial_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaJudicial_Lista]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;                

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new DescuentoJudicial_Registro();

                            item.Grilla = new Grilla_Response();
                            item.iNro = dr.GetInt64(dr.GetOrdinal("Nro"));
                            item.iCodJudicial = dr.GetInt32(dr.GetOrdinal("iCodPlanillaJudicial"));
                            item.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            item.vTrabajador = dr.GetString(dr.GetOrdinal("vNombreTrabajador"));
                            item.vNroDocumentoTrabajador = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            lista.Add(item);

                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public DescuentoJudicial_Registro ObtenerDescuentoJudicialParaEditar(DescuentoJudicial_Request peticion)
        {
            DescuentoJudicial_Registro registro = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[paPlanillaJudicialTrabajador_BuscarPorID]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;
                cmd.Parameters.Add(new SqlParameter("@iCodJudicial", peticion.iCodJudicial));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            registro = new DescuentoJudicial_Registro();
                            registro.Grilla = new Grilla_Response();

                            registro.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            registro.vTrabajador = dr.GetString(dr.GetOrdinal("vNombreTrabajador"));
                            registro.iCodTipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            registro.vNroDocumentoTrabajador = dr.GetString(dr.GetOrdinal("vNroDocumentoTrabajador"));
                            registro.iCodPlanilla = dr.GetInt32(dr.GetOrdinal("iCodPlanilla"));
                            registro.vCodigoPlanilla = dr.GetString(dr.GetOrdinal("vNombre"));
                            registro.iCodTipoPlanilla = dr.GetInt32(dr.GetOrdinal("iCodTipoPlanilla"));
                            registro.vTipoPlanilla = dr.GetString(dr.GetOrdinal("vTipoPlanilla"));
                            registro.iAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            registro.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            registro.dMontoRetencionTotal = dr.GetDecimal(dr.GetOrdinal("dMontoRetencionTotal"));
                            registro.vCodigoPlanilla = dr.GetString(dr.GetOrdinal("vCodigoPlanilla"));
                            registro.iCodDetPlanilla = dr.GetInt32(dr.GetOrdinal("iCodDetPlanilla"));
                            //registro.vNroDocumentoBeneficiario = dr.GetString(dr.GetOrdinal("vNroDocumentoBeneficiario"));
                            //registro.vBeneficiario = dr.GetString(dr.GetOrdinal("vNombreBeneficiario"));
                            //registro.iCodigoBanco= dr.GetInt32(dr.GetOrdinal("iCodigoBanco"));
                            //registro.vNumeroCuenta = dr.GetString(dr.GetOrdinal("vNumeroCuenta"));
                            //registro.iCodTipoRetencion = dr.GetInt32(dr.GetOrdinal("iCodTipoRetencion"));
                            //registro.dMontoRetencion = dr.GetDecimal(dr.GetOrdinal("dMontoRetencion"));
                            //registro.dValorPorcentaje = dr.GetDecimal(dr.GetOrdinal("dValorPorcentaje"));
                            //registro.dMontoPorcentaje= dr.GetDecimal(dr.GetOrdinal("dMontoPorcentaje"));
                            //
                            

                        }
                    }
                }
            }

            return registro;
        }

        public DescuentoJudicial_Registro ObtenerDescuentoJudicialTrabajadorParaEditar(DescuentoJudicial_Request peticion)
        {
            DescuentoJudicial_Registro registro = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[paPlanillaJudicial_BuscarPorID]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;
                cmd.Parameters.Add(new SqlParameter("@iCodJudicial", peticion.iCodJudicial));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            registro = new DescuentoJudicial_Registro();
                            registro.Grilla = new Grilla_Response();

                            registro.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            registro.vTrabajador = dr.GetString(dr.GetOrdinal("vNombreTrabajador"));
                            registro.iCodTipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            registro.vNroDocumentoTrabajador = dr.GetString(dr.GetOrdinal("vNroDocumentoTrabajador"));
                            //registro.iCodPlanilla = dr.GetInt32(dr.GetOrdinal("iCodPlanilla"));
                            //registro.vCodigoPlanilla = dr.GetString(dr.GetOrdinal("vNombre"));
                            //registro.iCodTipoPlanilla = dr.GetInt32(dr.GetOrdinal("iCodTipoPlanilla"));
                            //registro.vTipoPlanilla = dr.GetString(dr.GetOrdinal("vTipoPlanilla"));
                            //registro.iAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            //registro.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            //registro.dMontoRetencionTotal = dr.GetDecimal(dr.GetOrdinal("dMontoRetencionTotal"));
                            //registro.vCodigoPlanilla = dr.GetString(dr.GetOrdinal("vCodigoPlanilla"));
                            //registro.iCodDetPlanilla = dr.GetInt32(dr.GetOrdinal("iCodDetPlanilla"));
                            //registro.vNroDocumentoBeneficiario = dr.GetString(dr.GetOrdinal("vNroDocumentoBeneficiario"));
                            //registro.vBeneficiario = dr.GetString(dr.GetOrdinal("vNombreBeneficiario"));
                            //registro.iCodigoBanco= dr.GetInt32(dr.GetOrdinal("iCodigoBanco"));
                            //registro.vNumeroCuenta = dr.GetString(dr.GetOrdinal("vNumeroCuenta"));
                            //registro.iCodTipoRetencion = dr.GetInt32(dr.GetOrdinal("iCodTipoRetencion"));
                            //registro.dMontoRetencion = dr.GetDecimal(dr.GetOrdinal("dMontoRetencion"));
                            //registro.dValorPorcentaje = dr.GetDecimal(dr.GetOrdinal("dValorPorcentaje"));
                            //registro.dMontoPorcentaje= dr.GetDecimal(dr.GetOrdinal("dMontoPorcentaje"));
                            //


                        }
                    }
                }
            }

            return registro;
        }

        public Int32 RegistrarDescuentoJudicial(DescuentoJudicial_Registro registro)
        {
 
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaJudicialTrabajador_Registrar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                SqlParameter parmOUT = new SqlParameter("@iCodJudicial", SqlDbType.Int);
                parmOUT.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(parmOUT);
                //cmd.Parameters.Add(new SqlParameter("@iCodJudicial", registro.iCodJudicial));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", registro.iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", registro.iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", registro.iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", registro.iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", registro.iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", registro.iCodDetPlanilla));

                cmd.Parameters.Add(new SqlParameter("@vNroDocumentoTrabajador", registro.vNroDocumentoTrabajador));
                cmd.Parameters.Add(new SqlParameter("@dMontoRetencionTotal", registro.dMontoRetencionTotal));
                //cmd.Parameters.Add(new SqlParameter("@vUsuario", registro.IdUsuarioRegistro));

                cmd.ExecuteNonQuery();
                registro.iCodJudicial = Int32.Parse(parmOUT.Value.ToString());

                if (registro.iCodJudicial > 0)
                {

                    if (registro.detBeneficiarios != null)
                    {
                        foreach (String oDetalle in registro.detBeneficiarios)
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandText = "[dbo].[paPlanillaJudicialTrabajadorBeneficiario_Registrar]";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@iCodJudicial", registro.iCodJudicial));
                            cmd.Parameters.Add(new SqlParameter("@vNroDocumentoBeneficiario", oDetalle.Split('|')[0]));
                            cmd.Parameters.Add(new SqlParameter("@vNombreBeneficiario", oDetalle.Split('|')[1]));
                            cmd.Parameters.Add(new SqlParameter("@iCodigoBanco", oDetalle.Split('|')[2]));
                            cmd.Parameters.Add(new SqlParameter("@vNumeroCuenta", oDetalle.Split('|')[3]));
                            cmd.Parameters.Add(new SqlParameter("@iCodTipoRetencion", oDetalle.Split('|')[4]));
                            cmd.Parameters.Add(new SqlParameter("@iCodFormaPago", oDetalle.Split('|')[5]));
                            cmd.Parameters.Add(new SqlParameter("@dcValorPorcentaje", oDetalle.Split('|')[6]));
                            cmd.Parameters.Add(new SqlParameter("@dcMontoRetencion", oDetalle.Split('|')[7]));
                            //cmd.Parameters.Add(new SqlParameter("@vObservacion", oDetalle.Split('|')[7]));

                            cmd.ExecuteNonQuery();

                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return registro.iCodJudicial;
            

        }

        public Int32 RegistrarDescuentoJudicial_Nuevo(DescuentoJudicial_Registro registro)
        {

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaJudicial_Registrar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                SqlParameter parmOUT = new SqlParameter("@iCodJudicial", SqlDbType.Int);
                parmOUT.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(parmOUT);
                //cmd.Parameters.Add(new SqlParameter("@iCodJudicial", registro.iCodJudicial));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", registro.iCodTrabajador));                
                //cmd.Parameters.Add(new SqlParameter("@vUsuario", registro.IdUsuarioRegistro));

                cmd.ExecuteNonQuery();
                registro.iCodJudicial = Int32.Parse(parmOUT.Value.ToString());

                if (registro.iCodJudicial > 0)
                {

                    if (registro.detBeneficiarios != null)
                    {
                        foreach (String oDetalle in registro.detBeneficiarios)
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandText = "[dbo].[paPlanillaJudicialBeneficiario_Registrar]";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@iCodPlanillaJudicial", registro.iCodJudicial));
                            cmd.Parameters.Add(new SqlParameter("@vNroDocumentoBeneficiario", oDetalle.Split('|')[0]));
                            cmd.Parameters.Add(new SqlParameter("@vNombreBeneficiario", oDetalle.Split('|')[1]));
                            cmd.Parameters.Add(new SqlParameter("@iCodigoBanco", oDetalle.Split('|')[2]));
                            cmd.Parameters.Add(new SqlParameter("@vNumeroCuenta", oDetalle.Split('|')[3]));
                            cmd.Parameters.Add(new SqlParameter("@iCodTipoRetencion", oDetalle.Split('|')[4]));
                            cmd.Parameters.Add(new SqlParameter("@iCodFormaPago", oDetalle.Split('|')[5]));
                            cmd.Parameters.Add(new SqlParameter("@dcValorPorcentaje", oDetalle.Split('|')[6]));
                            cmd.Parameters.Add(new SqlParameter("@dcMontoRetencion", oDetalle.Split('|')[7]));
                            //cmd.Parameters.Add(new SqlParameter("@vObservacion", oDetalle.Split('|')[7]));

                            cmd.ExecuteNonQuery();

                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return registro.iCodJudicial;


        }

        public Int32 ActualizarDescuentoJudicial(DescuentoJudicial_Registro registro)
        {
           
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                cmd.CommandText = "[dbo].[paPlanillaJudicialTrabajador_Actualizar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodJudicial", registro.iCodJudicial));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", registro.iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", registro.iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", registro.iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", registro.iCodDetPlanilla));

                cmd.Parameters.Add(new SqlParameter("@iMes", registro.iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", registro.iAnio));
                cmd.Parameters.Add(new SqlParameter("@vNroDocumentoTrabajador", registro.vNroDocumentoTrabajador));
                cmd.Parameters.Add(new SqlParameter("@dcMontoRetencionTotal", registro.dMontoRetencionTotal));

                cmd.ExecuteNonQuery();

                /* ACTUALZIAR DETALLE DEBENEFICIARIO */
                if (registro.detBeneficiarios != null)
                {
                    /* ELIMINAR BENEFICIARIO ANTERIOR */
                    if (registro.iValidarCambio == 1)
                    {
                        if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                        cmd.Parameters.Clear();
                        cmd.CommandText = "[dbo].[paPlanillaJudicialTrabajadorBeneficiario_Eliminar]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@iCodJudicial", registro.iCodJudicial));
                        cmd.ExecuteNonQuery();


                        /* AGREGAR LOS BENEFICIARIOS ACTUALIZADOS */
                        foreach (String oDetalle in registro.detBeneficiarios)
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandText = "[dbo].[paPlanillaJudicialTrabajadorBeneficiario_Registrar]";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@iCodJudicial", registro.iCodJudicial));
                            cmd.Parameters.Add(new SqlParameter("@vNroDocumentoBeneficiario", oDetalle.Split('|')[0]));
                            cmd.Parameters.Add(new SqlParameter("@vNombreBeneficiario", oDetalle.Split('|')[1]));
                            cmd.Parameters.Add(new SqlParameter("@iCodigoBanco", oDetalle.Split('|')[2]));
                            cmd.Parameters.Add(new SqlParameter("@vNumeroCuenta", oDetalle.Split('|')[3]));
                            cmd.Parameters.Add(new SqlParameter("@iCodTipoRetencion", oDetalle.Split('|')[4]));
                            cmd.Parameters.Add(new SqlParameter("@iCodFormaPago", oDetalle.Split('|')[5]));
                            cmd.Parameters.Add(new SqlParameter("@dcValorPorcentaje", oDetalle.Split('|')[6]));
                            cmd.Parameters.Add(new SqlParameter("@dcMontoRetencion", oDetalle.Split('|')[7]));
                            //cmd.Parameters.Add(new SqlParameter("@vObservacion", oDetalle.Split('|')[7]));

                            cmd.ExecuteNonQuery();

                        }
                    }


                }

                if (_iBasesSqlAdoUnitOfWork.TieneTransaccion()) _iBasesSqlAdoUnitOfWork.ConfirmarTransaccion();
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.iCodJudicial;
           
        }

        public Int32 ActualizarDescuentoJudicial_Nuevo(DescuentoJudicial_Registro registro)
        {

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                //if (cmd.Connection.State != ConnectionState.Open)
                //    cmd.Connection.Open();

                //cmd.CommandText = "[dbo].[paPlanillaJudicialTrabajador_Actualizar]";
                //cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.Add(new SqlParameter("@iCodJudicial", registro.iCodJudicial));
                //cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", registro.iCodTrabajador));
                //cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", registro.iCodPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", registro.iCodTipoPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", registro.iCodDetPlanilla));

                //cmd.Parameters.Add(new SqlParameter("@iMes", registro.iMes));
                //cmd.Parameters.Add(new SqlParameter("@iAnio", registro.iAnio));
                //cmd.Parameters.Add(new SqlParameter("@vNroDocumentoTrabajador", registro.vNroDocumentoTrabajador));
                //cmd.Parameters.Add(new SqlParameter("@dcMontoRetencionTotal", registro.dMontoRetencionTotal));

                //cmd.ExecuteNonQuery();

                /* ACTUALZIAR DETALLE DEBENEFICIARIO */
                if (registro.detBeneficiarios != null)
                {
                    /* ELIMINAR BENEFICIARIO ANTERIOR */
                    if (registro.iValidarCambio == 1)
                    {
                        if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                        //cmd.Parameters.Clear();
                        //cmd.CommandText = "[dbo].[paPlanillaJudicialTrabajadorBeneficiario_Eliminar]";
                        //cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add(new SqlParameter("@iCodJudicial", registro.iCodJudicial));
                        //cmd.ExecuteNonQuery();


                        /* AGREGAR LOS BENEFICIARIOS ACTUALIZADOS */
                        foreach (String oDetalle in registro.detBeneficiarios)
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandText = "[dbo].[paPlanillaJudicialBeneficiario_Registrar]";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@iCodJudicial", registro.iCodJudicial));
                            cmd.Parameters.Add(new SqlParameter("@vNroDocumentoBeneficiario", oDetalle.Split('|')[0]));
                            cmd.Parameters.Add(new SqlParameter("@vNombreBeneficiario", oDetalle.Split('|')[1]));
                            cmd.Parameters.Add(new SqlParameter("@iCodigoBanco", oDetalle.Split('|')[2]));
                            cmd.Parameters.Add(new SqlParameter("@vNumeroCuenta", oDetalle.Split('|')[3]));
                            cmd.Parameters.Add(new SqlParameter("@iCodTipoRetencion", oDetalle.Split('|')[4]));
                            cmd.Parameters.Add(new SqlParameter("@iCodFormaPago", oDetalle.Split('|')[5]));
                            cmd.Parameters.Add(new SqlParameter("@dcValorPorcentaje", oDetalle.Split('|')[6]));
                            cmd.Parameters.Add(new SqlParameter("@dcMontoRetencion", oDetalle.Split('|')[7]));
                            //cmd.Parameters.Add(new SqlParameter("@vObservacion", oDetalle.Split('|')[7]));

                            cmd.ExecuteNonQuery();

                        }
                    }


                }

                if (_iBasesSqlAdoUnitOfWork.TieneTransaccion()) _iBasesSqlAdoUnitOfWork.ConfirmarTransaccion();
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.iCodJudicial;

        }

        public Int32 EliminarDescuentoJudicialBeneficiario(DescuentoJudicial_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaJudicialBeneficiario_Eliminar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPlanillaJudicialBeneficiario", registro.iCodJudicial));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.iCodJudicial;
        }

        public Int32 EliminarDescuentoJudicial(DescuentoJudicial_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaJudicialTrabajador_Eliminar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodJudicial", registro.iCodJudicial));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.iCodJudicial;
        }

        public Int32 CargarDescuentoJudicial(DescuentoJudicial_Registro registro)
        {
            Int32 iReg;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaJudicialTrabajador_CargarDescuento]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", registro.iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", registro.iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iAnio", registro.iAnio));
                cmd.Parameters.Add(new SqlParameter("@iMes", registro.iMes));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", registro.iCodDetPlanilla));
                
                //cmd.Parameters.Add(new SqlParameter("@vUsuario", registro.IdUsuarioRegistro));

                iReg = Convert.ToInt32(cmd.ExecuteNonQuery());
                //registro.iCodRegistroRegimenPen = Int32.Parse(IdPropuestaParameter.Value.ToString());

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            registro.iResultado = iReg;

            return registro.iResultado;
        }


        #endregion


        #region Metodos Generales Concepto Fijo y Variables - DETALLE BENEFICIARIOS

        public IEnumerable<DescuentoJudicialBeneficiario_Registro> ListarDescuentoJudicial_Beneficiario(DescuentoJudicialBeneficiario_Request peticion)
        {
            List<DescuentoJudicialBeneficiario_Registro> lista = new List<DescuentoJudicialBeneficiario_Registro>();
            DescuentoJudicialBeneficiario_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaJudicialTrabajadorBeneficiario_Lista]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodJudicial", peticion.iCodJudicial));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new DescuentoJudicialBeneficiario_Registro();

                            item.Grilla = new Grilla_Response();
                            //item.iNro = dr.GetInt64(dr.GetOrdinal("Nro"));
                            item.iCodJudicial = dr.GetInt32(dr.GetOrdinal("iCodJudicial"));
                            item.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            item.vDniBeneficiario = dr.GetString(dr.GetOrdinal("vDniBeneficiario"));
                            item.vNombreBeneficiario = dr.GetString(dr.GetOrdinal("vNombreBeneficiario"));
                            item.iCodigoBanco = dr.GetInt32(dr.GetOrdinal("iCodigoBanco"));
                            item.vNombreBanco = dr.GetString(dr.GetOrdinal("vNombreBanco"));
                            item.vNumeroCuenta = dr.GetString(dr.GetOrdinal("vNumeroCuenta"));
                            item.iCodTipoRetencion = dr.GetInt32(dr.GetOrdinal("iTipoRetencion"));
                            item.vNombreRetencion = dr.GetString(dr.GetOrdinal("vNombreRetencion"));
                            item.dValorPorcentaje= dr.GetDecimal(dr.GetOrdinal("dValorPorcentaje"));
                            item.dMontoRetencion = dr.GetDecimal(dr.GetOrdinal("dMontoRetencion"));
                            item.vObservacion = dr.GetString(dr.GetOrdinal("vObservacion"));
                            item.iCodFormaPago = dr.GetInt32(dr.GetOrdinal("iFormaPago"));
                            item.vNombreFormaPago = dr.GetString(dr.GetOrdinal("vNombreFormaPago"));
                            lista.Add(item);

                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<DescuentoJudicialBeneficiario_Registro> ListarDescuentoJudicial_BeneficiarioTrabajadores(DescuentoJudicialBeneficiario_Request peticion)
        {
            List<DescuentoJudicialBeneficiario_Registro> lista = new List<DescuentoJudicialBeneficiario_Registro>();
            DescuentoJudicialBeneficiario_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaJudicialBeneficiario_Lista]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodJudicial", peticion.iCodJudicial));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new DescuentoJudicialBeneficiario_Registro();

                            item.Grilla = new Grilla_Response();
                            //item.iNro = dr.GetInt64(dr.GetOrdinal("Nro"));
                            item.iCodJudicial = dr.GetInt32(dr.GetOrdinal("iCodPlanillaJudicial"));
                            item.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            item.vDniBeneficiario = dr.GetString(dr.GetOrdinal("vDniBeneficiario"));
                            item.vNombreBeneficiario = dr.GetString(dr.GetOrdinal("vNombreBeneficiario"));
                            item.iCodigoBanco = dr.GetInt32(dr.GetOrdinal("iCodBanco"));
                            item.vNombreBanco = dr.GetString(dr.GetOrdinal("vNombreBanco"));
                            item.vNumeroCuenta = dr.GetString(dr.GetOrdinal("vNumeroCuenta"));
                            item.iCodTipoRetencion = dr.GetInt32(dr.GetOrdinal("iCodTipoRetencion"));
                            item.vNombreRetencion = dr.GetString(dr.GetOrdinal("vNombreRetencion"));
                            item.dValorPorcentaje = dr.GetDecimal(dr.GetOrdinal("dValorPorcentaje"));
                            item.dMontoRetencion = dr.GetDecimal(dr.GetOrdinal("dMontoRetencion"));
                            item.vObservacion = dr.GetString(dr.GetOrdinal("vObservacion"));
                            item.iCodFormaPago = dr.GetInt32(dr.GetOrdinal("iCodFormaPago"));
                            item.vNombreFormaPago = dr.GetString(dr.GetOrdinal("vNombreFormaPago"));
                            lista.Add(item);

                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public bool GenerarPlanillaJudicial(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcJudicial]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", 1));
                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        public Int32 Registrar_Beneficiario(DescuentoJudicialBeneficiario_Registro registro)
        {

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaJudicial_Registrar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                SqlParameter parmOUT = new SqlParameter("@iCodJudicial", SqlDbType.Int);
                parmOUT.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(parmOUT);
                //cmd.Parameters.Add(new SqlParameter("@iCodJudicial", registro.iCodJudicial));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", registro.iCodTrabajador));
                //cmd.Parameters.Add(new SqlParameter("@vUsuario", registro.IdUsuarioRegistro));

                cmd.ExecuteNonQuery();
                registro.iCodJudicial = Int32.Parse(parmOUT.Value.ToString());

                if (registro.iCodJudicial > 0)
                {

                    //if (registro.detBeneficiarios != null)
                    //{
                        //foreach (String oDetalle in registro.detBeneficiarios)
                        //{
                            cmd.Parameters.Clear();
                            cmd.CommandText = "[dbo].[paPlanillaJudicialBeneficiario_Registrar]";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@iCodJudicial", registro.iCodJudicial));
                            cmd.Parameters.Add(new SqlParameter("@vNroDocumentoBeneficiario", registro.vDniBeneficiario));
                            cmd.Parameters.Add(new SqlParameter("@vNombreBeneficiario", registro.vNombreBeneficiario));
                            cmd.Parameters.Add(new SqlParameter("@iCodigoBanco", registro.iCodigoBanco));
                            cmd.Parameters.Add(new SqlParameter("@vNumeroCuenta", registro.vNumeroCuenta));
                            cmd.Parameters.Add(new SqlParameter("@iCodTipoRetencion", registro.iCodTipoRetencion));
                            cmd.Parameters.Add(new SqlParameter("@iCodFormaPago", registro.iCodFormaPago));
                            cmd.Parameters.Add(new SqlParameter("@dcValorPorcentaje", registro.dValorPorcentaje));
                            cmd.Parameters.Add(new SqlParameter("@dcMontoRetencion", registro.dMontoRetencion));
                            cmd.Parameters.Add(new SqlParameter("@dtFechaLlegadaDoc", registro.sFechaLlegadaDoc));
                            //cmd.Parameters.Add(new SqlParameter("@vObservacion", oDetalle.Split('|')[7]));

                            cmd.ExecuteNonQuery();

                        //}
                    //}
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return registro.iCodJudicial;


        }

        public bool Quitar_Beneficiario(DescuentoJudicialBeneficiario_Registro registro)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaJudicialBeneficiario_Eliminar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodPlanillaJudicialBeneficiario", registro.iCodJudicialDetalle));
                cmd.ExecuteNonQuery();                

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
                rpta = true;

            }

            return rpta;


        }
        #endregion

    }
}
