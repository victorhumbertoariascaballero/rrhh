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
    public partial class T_genm_conceptofijovariable_ODA
    {
        BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWork = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnOracle"]].ConnectionString));

        #region Metodos Generales Concepto Fijo y Variables - CABECERA

        string vSqlString = new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnOracleM"]].ConnectionString);

        public IEnumerable<ConceptoFijoVariable_Registro> ListarConceptoVariable(ConceptoFijoVariable_Request peticion)
        {
            List<ConceptoFijoVariable_Registro> lista = new List<ConceptoFijoVariable_Registro>();
            ConceptoFijoVariable_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaConceptoTrabajador_Lista]";
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
                            item = new ConceptoFijoVariable_Registro();

                            item.Grilla = new Grilla_Response();
                            item.iNro = dr.GetInt64(dr.GetOrdinal("Nro"));
                            item.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            item.vTrabajador = dr.GetString(dr.GetOrdinal("Trabajador"));
                            item.vNroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            item.iCodPlanilla = dr.GetInt32(dr.GetOrdinal("iCodPlanilla"));
                            item.vNombrePlanilla = dr.GetString(dr.GetOrdinal("vNombre"));
                            item.iCodTipoPlanilla = dr.GetInt32(dr.GetOrdinal("iCodTipoPlanilla"));
                            item.vTipoPlanilla= dr.GetString(dr.GetOrdinal("vTipoPlanilla"));
                            item.iAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            item.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            item.iCodConcepto = dr.GetInt32(dr.GetOrdinal("iCodConcepto"));
                            item.vConcepto = dr.GetString(dr.GetOrdinal("vNombreConcepto"));
                            item.iCodTipoConcepto = dr.GetInt32(dr.GetOrdinal("iCodTipoConcepto"));
                            item.vTipoConcepto = dr.GetString(dr.GetOrdinal("vNombreTipoConcepto"));
                            item.iDiaSubsidio = dr.GetInt32(dr.GetOrdinal("iCodSubTipoConcepto"));
                            item.vSubTipoConcepto = dr.GetString(dr.GetOrdinal("vSubTipoConcepto"));
                            item.dMonto = dr.GetDecimal(dr.GetOrdinal("dMonto"));
                            //item.iDiaSubsidio = dr.GetInt32(dr.GetOrdinal("iDiaSubsidio"));
                            item.dFechaReg = dr.GetString(dr.GetOrdinal("dFechaReg"));
                            item.iCodDetPlanilla = dr.GetInt32(dr.GetOrdinal("iCodDetPlanilla"));
                            lista.Add(item);

                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<ConceptoFijoVariable_Registro> ListarConceptoVariableNoBaseImponible(ConceptoFijoVariable_Request peticion)
        {
            List<ConceptoFijoVariable_Registro> lista = new List<ConceptoFijoVariable_Registro>();
            ConceptoFijoVariable_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaConceptoTrabajador_NoBaseImp_Lista]";
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
                            item = new ConceptoFijoVariable_Registro();

                            item.Grilla = new Grilla_Response();
                            item.iNro = dr.GetInt64(dr.GetOrdinal("Nro"));
                            item.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            item.vTrabajador = dr.GetString(dr.GetOrdinal("Trabajador"));
                            item.vNroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            item.iCodPlanilla = dr.GetInt32(dr.GetOrdinal("iCodPlanilla"));
                            item.vNombrePlanilla = dr.GetString(dr.GetOrdinal("vNombre"));
                            item.iCodTipoPlanilla = dr.GetInt32(dr.GetOrdinal("iCodTipoPlanilla"));
                            item.vTipoPlanilla = dr.GetString(dr.GetOrdinal("vTipoPlanilla"));
                            item.iAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            item.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            item.iCodConcepto = dr.GetInt32(dr.GetOrdinal("iCodConcepto"));
                            item.vConcepto = dr.GetString(dr.GetOrdinal("vNombreConcepto"));
                            item.iCodTipoConcepto = dr.GetInt32(dr.GetOrdinal("iCodTipoConcepto"));
                            item.vTipoConcepto = dr.GetString(dr.GetOrdinal("vNombreTipoConcepto"));
                            item.iDiaSubsidio = dr.GetInt32(dr.GetOrdinal("iCodSubTipoConcepto"));
                            item.vSubTipoConcepto = dr.GetString(dr.GetOrdinal("vSubTipoConcepto"));
                            item.dMonto = dr.GetDecimal(dr.GetOrdinal("dMonto"));
                            //item.iDiaSubsidio = dr.GetInt32(dr.GetOrdinal("iDiaSubsidio"));
                            item.dFechaReg = dr.GetString(dr.GetOrdinal("dFechaReg"));
                            item.iCodDetPlanilla = dr.GetInt32(dr.GetOrdinal("iCodDetPlanilla"));
                            lista.Add(item);

                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaEjecucion_Response> ListarPlanillaEjecucionAperturada(PlanillaEjecucion_Request peticion)
        {
            List<PlanillaEjecucion_Response> lista = new List<PlanillaEjecucion_Response>();
            PlanillaEjecucion_Response item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaEjecucionAperturada_Lista]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iAnio", peticion.iAnio));
                cmd.Parameters.Add(new SqlParameter("@iMes", peticion.iMes));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new PlanillaEjecucion_Response();

                            item.Grilla = new Grilla_Response();
                            item.vCodigo = dr.GetString(dr.GetOrdinal("vCodigoPlanilla"));
                            item.vNombrePlanilla = dr.GetString(dr.GetOrdinal("vNombrePlanilla"));
                            item.iCodDetPlanilla = dr.GetInt32(dr.GetOrdinal("iCodDetPlanilla"));
                            lista.Add(item);

                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public ConceptoFijoVariable_Registro ObtenerConceptoTrabajadorParaEditar(ConceptoFijoVariable_Request peticion)
        {
            ConceptoFijoVariable_Registro registro = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[paPlanillaConceptoTrabajador_Buscar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", peticion.iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", peticion.iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", peticion.iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", peticion.iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", peticion.iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodConcepto", peticion.iCodConcepto));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoConcepto", peticion.iCodTipoConcepto));
                cmd.Parameters.Add(new SqlParameter("@iCodSubTipoConcepto", peticion.iNroDias));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", peticion.iCodDetPlanilla));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            registro = new ConceptoFijoVariable_Registro();
                            registro.Grilla = new Grilla_Response();

                            registro.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            registro.vTrabajador = dr.GetString(dr.GetOrdinal("vTrabajador"));

                            registro.iCodTipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            registro.vNroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));

                            registro.iCodPlanilla = dr.GetInt32(dr.GetOrdinal("iCodPlanilla"));
                            registro.vNombrePlanilla = dr.GetString(dr.GetOrdinal("vNombre"));
                            registro.iCodTipoPlanilla = dr.GetInt32(dr.GetOrdinal("iCodTipoPlanilla"));
                            registro.vTipoPlanilla = dr.GetString(dr.GetOrdinal("vTipoPlanilla"));
                            registro.iAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            registro.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            registro.iCodConcepto = dr.GetInt32(dr.GetOrdinal("iCodConcepto"));
                            registro.vConcepto = dr.GetString(dr.GetOrdinal("vNombreConcepto"));

                            registro.iCodTipoConcepto = dr.GetInt32(dr.GetOrdinal("iCodTipoConcepto"));
                            registro.vTipoConcepto = dr.GetString(dr.GetOrdinal("vNombreTipoConcepto"));

                            registro.iCodSubTipoConcepto = dr.GetInt32(dr.GetOrdinal("iCodSubTipoConcepto"));
                            //registro.vSubTipoConcepto = dr.GetString(dr.GetOrdinal("vSubTipoConcepto"));

                            registro.dMonto = dr.GetDecimal(dr.GetOrdinal("dMonto"));
                            registro.iDiaSubsidio = dr.GetInt32(dr.GetOrdinal("iDiaSubsidio"));
                            registro.dFechaReg = dr.GetString(dr.GetOrdinal("dFechaReg"));
                            registro.vCodigoPlanilla = dr.GetString(dr.GetOrdinal("vCodigoPlanilla"));

                            registro.iCodDetPlanilla = dr.GetInt32(dr.GetOrdinal("iCodDetPlanilla"));
                        }
                    }
                }
            }

            return registro;
        }

        //public Int32 RegistrarConcepto(ConceptoFijoVariable_Registro registro)
        //{
        //    int nReg = 0;

        //    using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
        //    {
        //        if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
        //        cmd.CommandText = "[dbo].[paPlanillaConceptoTrabajador_Registrar]";
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        //cmd.CommandTimeout = cmd.CommandTimeout;
        //        cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", registro.iCodTrabajador));
        //        cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", registro.iCodPlanilla));
        //        cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", registro.iCodTipoPlanilla));
        //        cmd.Parameters.Add(new SqlParameter("@iMes", registro.iMes));
        //        cmd.Parameters.Add(new SqlParameter("@iAnio", registro.iAnio));

        //        cmd.Parameters.Add(new SqlParameter("@iCodConcepto", registro.iCodConcepto));
        //        cmd.Parameters.Add(new SqlParameter("@iCodTipoConcepto", registro.iCodTipoConcepto));
        //        cmd.Parameters.Add(new SqlParameter("@iCodSubTipoConcepto", registro.iCodSubTipoConcepto));
        //        cmd.Parameters.Add(new SqlParameter("@dcMonto", registro.dMonto));

        //        if (registro.iDiaSubsidio != 0)
        //        {
        //            cmd.Parameters.Add(new SqlParameter("@iDiaSubsidio", registro.iDiaSubsidio));
        //        }
                

        //        //cmd.Parameters.Add(new SqlParameter("@vUsuario", registro.IdUsuarioRegistro));

        //        nReg=Convert.ToInt32(cmd.ExecuteNonQuery());
        //        //registro.iCodRegistroRegimenPen = Int32.Parse(IdPropuestaParameter.Value.ToString());

        //        if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
        //    }

        //    //return registro.iCodConcepto;
        //    return nReg;

        //}
        public string RegistrarConcepto(ConceptoFijoVariable_Registro registro)
        {
            //bool rpta = false;
            string sDNIsInvalidos = string.Empty;
            string sDNIsInvalidosTemp = string.Empty;
            try
            {
                _iBasesSqlAdoUnitOfWork.IniciarTransaccion(IsolationLevel.ReadCommitted);
                using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
                {
                    if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

                    if (registro.formatos != null)
                    {
                        foreach (String codigo in registro.formatos)
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandText = "[dbo].[paPlanillaConceptoTrabajador_Registrar2]";
                            cmd.CommandType = CommandType.StoredProcedure;
                            //cmd.CommandTimeout = cmd.CommandTimeout;
                            //cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", registro.iCodTrabajador));
                            cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", registro.iCodPlanilla));
                            cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", registro.iCodTipoPlanilla));
                            cmd.Parameters.Add(new SqlParameter("@iMes", registro.iMes));
                            cmd.Parameters.Add(new SqlParameter("@iAnio", registro.iAnio));
                            cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", registro.iCodDetPlanilla));

                            cmd.Parameters.Add(new SqlParameter("@iCodConcepto", registro.iCodConcepto));
                            cmd.Parameters.Add(new SqlParameter("@iCodTipoConcepto", registro.iCodTipoConcepto));
                            cmd.Parameters.Add(new SqlParameter("@iCodSubTipoConcepto", registro.iCodSubTipoConcepto));
                            cmd.Parameters.Add(new SqlParameter("@vNroDocumento", codigo.Split('|')[0]));
                            cmd.Parameters.Add(new SqlParameter("@dcMonto", codigo.Split('|')[1]));
                            if (registro.iCodConcepto == 4)
                            {
                                cmd.Parameters.Add(new SqlParameter("@iDiaSubsidio", codigo.Split('|')[2]));
                            }
                            sDNIsInvalidosTemp = string.Empty;
                            cmd.Parameters.Add(new SqlParameter("@NroDocumentoOut", SqlDbType.VarChar, 10)).Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();
                            sDNIsInvalidosTemp = cmd.Parameters["@NroDocumentoOut"].Value.ToString();
                            if (sDNIsInvalidosTemp!=string.Empty)
                            {
                                if (sDNIsInvalidos == string.Empty)
                                {
                                    sDNIsInvalidos = sDNIsInvalidosTemp;
                                }
                                else
                                {
                                    sDNIsInvalidos = sDNIsInvalidos + ", " + sDNIsInvalidosTemp;
                                }
                            }                            
                        }
                    }
                    if (_iBasesSqlAdoUnitOfWork.TieneTransaccion()) _iBasesSqlAdoUnitOfWork.ConfirmarTransaccion();
                    if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

                    //cmd.Parameters.Add(new SqlParameter("@vUsuario", registro.IdUsuarioRegistro));

                    //rpta = true;
                    //registro.iCodRegistroRegimenPen = Int32.Parse(IdPropuestaParameter.Value.ToString());

                    if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
                }
            }
            catch (Exception)
            {
                if (_iBasesSqlAdoUnitOfWork.TieneTransaccion()) _iBasesSqlAdoUnitOfWork.RetrocederTransaccion();
                throw;
            }
            

            //return registro.iCodConcepto;
            return sDNIsInvalidos;

        }

        public Int32 ActualizarConcepto(ConceptoFijoVariable_Registro registro)
        {
            Int32 nReg = 0;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                cmd.CommandText = "[dbo].[paPlanillaConceptoTrabajador_Actualizar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", registro.iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", registro.iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", registro.iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", registro.iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", registro.iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", registro.iCodDetPlanilla));

                cmd.Parameters.Add(new SqlParameter("@iCodConcepto", registro.iCodConcepto));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoConcepto", registro.iCodTipoConcepto));
                cmd.Parameters.Add(new SqlParameter("@iCodSubTipoConcepto", registro.iCodSubTipoConcepto));
                cmd.Parameters.Add(new SqlParameter("@dcMonto", registro.dMonto));
                cmd.Parameters.Add(new SqlParameter("@iDiaSubsidio", registro.iDiaSubsidio));
                //cmd.Parameters.Add(new SqlParameter("@vUsuario", registro.IdUsuarioModificacion));
                
                nReg= Convert.ToInt32(cmd.ExecuteNonQuery());

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            //return registro.iCodConcepto;
            return nReg;
        }

        public bool EliminarConcepto(ConceptoFijoVariable_Registro registro)
        {
            bool rpta = false;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                cmd.CommandText = "[dbo].[paPlanillaConceptoTrabajador_Eliminar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", registro.iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", registro.iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", registro.iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", registro.iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", registro.iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodConcepto", registro.iCodConcepto));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", registro.iCodDetPlanilla));
                
                cmd.ExecuteNonQuery();
                rpta = true;

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            //return registro.iCodConcepto;
            return rpta;
        }
        //public Int32 EliminarConcepto(Concepto_Registro registro)
        //{
        //    using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
        //    {
        //        if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
        //        cmd.CommandText = "[dbo].[paPlanillaConcepto_Eliminar]";
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        //cmd.CommandTimeout = cmd.CommandTimeout;

        //        cmd.Parameters.Add(new SqlParameter("@iCodConcepto", registro.iCodConcepto));

        //        cmd.ExecuteNonQuery();

        //        if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
        //    }

        //    return registro.iCodConcepto;
        //}
       
        #endregion

    }
}
