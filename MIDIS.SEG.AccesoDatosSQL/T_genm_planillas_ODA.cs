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
    public class T_genm_planillas_ODA
    {
        BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWork = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnOracle"]].ConnectionString));

        /*------Planilla CAS------*/
        #region Planilla CAS General
        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralCAS(int idCodFuenteFinanciamiento, string sMetas, int iMes, int iAnio)
        {
            List<PlanillaCalculada_Request> lista = new List<PlanillaCalculada_Request>();
            PlanillaCalculada_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcCASGeneralConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                if (idCodFuenteFinanciamiento != 0)
                {
                    cmd.Parameters.Add(new SqlParameter("@iCodFuenteFinanciamiento", idCodFuenteFinanciamiento));
                }
                if (sMetas != null && sMetas != string.Empty)
                {
                    cmd.Parameters.Add(new SqlParameter("@vMetas", sMetas));
                }

                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", 1));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculada_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            be.iCodTipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            be.sNombreTipoDocumento = dr.GetString(dr.GetOrdinal("vNombreTipoDocumento"));
                            be.sNroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            be.sNombres = dr.GetString(dr.GetOrdinal("vNombres"));
                            be.sApepPaterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            be.sApepMaterno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            be.sNombreCompleto = be.sApepPaterno + " " + be.sApepMaterno + " " + be.sNombres;
                            be.sCargo = dr.GetString(dr.GetOrdinal("vCargo"));
                            be.iCodPensionario = dr.GetInt32(dr.GetOrdinal("iCodRegimenPen"));
                            be.sNombreRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreRegimenPen"));
                            be.iCodTipoPensionario = dr.GetInt32(dr.GetOrdinal("iCodTipoRegimenPen"));
                            be.sNombreTipoRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreTipoRegimenPen"));
                            if (be.sNombreTipoRegimenPensionario!="")
                            {
                                be.sRegimenPensionario = be.sNombreRegimenPensionario + " - " + be.sNombreTipoRegimenPensionario;    
                            }
                            else
                            {
                                be.sRegimenPensionario = be.sNombreRegimenPensionario;
                            }                            
                            be.bExoneracionRenta4ta = dr.GetBoolean(dr.GetOrdinal("bEstadoSuspencionRetencion"));
                            //be.sFechaInicio = dr.GetString(dr.GetOrdinal("dtInicioLabores"));
                            be.iDiasLaborados = dr.GetInt32(dr.GetOrdinal("iDiasLaboradosPorMes"));
                            be.iDiasVacaciones = dr.GetInt32(dr.GetOrdinal("iDiasVacaciones"));
                            be.iDiasLicencias = dr.GetInt32(dr.GetOrdinal("iDiasLicencia"));
                            be.iDiasDescansos_Subsidios = dr.GetInt32(dr.GetOrdinal("iDiasDescanso"));
                            be.dcMontoRemuneracionBasica = dr.GetDecimal(dr.GetOrdinal("dRemuneracion"));
                            be.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));
                            be.dcMontoTotalDescuentos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));
                            be.dcMontoTotalNeto = dr.GetDecimal(dr.GetOrdinal("dcMontoNeto"));
                            be.dcMontoAporteEsSalud = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion"));
                            be.dcMontoAporteEsSalud_675 = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion_675"));
                            be.dcMontoAporteEsSaludEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacionEPS"));
                            be.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            be.iAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            be.iCodDetPlanilla = 1;
                            be.iCodPlanilla = 1;
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaCalculadaIngresos_Request> ListarPlanillaCalculadaIngresosCAS(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla)
        {
            List<PlanillaCalculadaIngresos_Request> lista = new List<PlanillaCalculadaIngresos_Request>();
            PlanillaCalculadaIngresos_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcCASIngresosConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", 1));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculadaIngresos_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.dcMontoContraprestacion = dr.GetDecimal(dr.GetOrdinal("dcMontoContraprestacion"));
                            be.dcMontoContraprescionVacaional = dr.GetDecimal(dr.GetOrdinal("dcMontoVacaciones"));
                            be.dcMontoReintegros = dr.GetDecimal(dr.GetOrdinal("dcMontoReintegro"));
                            be.dcMontoCopagoSubsidio = dr.GetDecimal(dr.GetOrdinal("dcMontoCoPagoSubsidio"));
                            be.dcMontoAguinaldos = dr.GetDecimal(dr.GetOrdinal("dcMontoAguinaldo"));
                            be.dcMontoPagoContratoAnterior = dr.GetDecimal(dr.GetOrdinal("dcMontoPagoContratoAnterior"));
                            be.dcMontoReintegroCoPagoSubsidio = dr.GetDecimal(dr.GetOrdinal("dcMontoReintegroCoPagoSubsidio"));
                            be.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));
                            
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaCalculadaDescuentos_Request> ListarPlanillaCalculadaDescuentosCAS(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla)
        {
            List<PlanillaCalculadaDescuentos_Request> lista = new List<PlanillaCalculadaDescuentos_Request>();
            PlanillaCalculadaDescuentos_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcCASDescuentosConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", 1));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculadaDescuentos_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.dcMontoTardanzas = dr.GetDecimal(dr.GetOrdinal("dcMontoTardanzas"));
                            be.dcMontoInasistencias = dr.GetDecimal(dr.GetOrdinal("dcMontoInasistencias"));
                            be.dcMontoPermisos = dr.GetDecimal(dr.GetOrdinal("dcMontoPermisos"));
                            be.dcMontoLicenciaSinGoce = dr.GetDecimal(dr.GetOrdinal("dcMontoLicenciaSinGoce"));
                            be.dcMontoSalidasAnticipadas = dr.GetDecimal(dr.GetOrdinal("dcMontoSalidasAnticipadas"));
                            be.dcMontoImpuestoRenta4ta = dr.GetDecimal(dr.GetOrdinal("dcMontoRenta4ta"));
                            be.dcMontoImpuestoRenta4taAguinaldo = dr.GetDecimal(dr.GetOrdinal("dcMontoImpuestoRenta4taAguinaldo"));
                            be.dcMontoImpuestoRenta4taDsctoJudicialAguinaldo = dr.GetDecimal(dr.GetOrdinal("dcMontoImpuestoRenta4taDsctoJudicialAguinaldo"));
                            be.dcMontoONP = dr.GetDecimal(dr.GetOrdinal("dcMontoONP"));
                            be.dcMontoAporteOblAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoAporteAFP"));
                            be.dcMontoComisionAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoComisionAFP"));
                            be.dcMontoPrimaSegAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoPrimaAFP"));
                            be.dcMontoDescuentoJudicial = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoJudicial"));
                            be.dcMontoEsSaludMasVida = dr.GetDecimal(dr.GetOrdinal("dcMontoEsSaludMasVida"));
                            be.dcMontoRimacSeguro = dr.GetDecimal(dr.GetOrdinal("dcMontoRimacSeguro"));
                            be.dcMontoDsctoPagoExceso = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoPagoExceso"));
                            be.dcMontoEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoEPS"));
                            be.dcMontoCampOftalmologica = dr.GetDecimal(dr.GetOrdinal("dcMontoCampOftalmologica"));
                            be.dcMontoCapacitaciones = dr.GetDecimal(dr.GetOrdinal("dcMontoCapacitaciones"));
                            be.dcMontoTotalDescuento = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));
                            
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralCompletaCAS(int idCodFuenteFinanciamiento, string sMetas, int iMes, int iAnio)
        {
            List<PlanillaCalculada_Request> lista = new List<PlanillaCalculada_Request>();
            PlanillaCalculada_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcCASGeneralCompletaConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                if (idCodFuenteFinanciamiento != 0)
                {
                    cmd.Parameters.Add(new SqlParameter("@iCodFuenteFinanciamiento", idCodFuenteFinanciamiento));
                }
                if (sMetas != null && sMetas != string.Empty)
                {
                    cmd.Parameters.Add(new SqlParameter("@vMetas", sMetas));
                }

                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", 1));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculada_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            be.iCodTipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            be.sNombreTipoDocumento = dr.GetString(dr.GetOrdinal("vNombreTipoDocumento"));
                            be.sNroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            be.sNombres = dr.GetString(dr.GetOrdinal("vNombres"));
                            be.sApepPaterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            be.sApepMaterno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            be.sNombreCompleto = be.sApepPaterno + " " + be.sApepMaterno + " " + be.sNombres;
                            be.sCargo = dr.GetString(dr.GetOrdinal("vCargo"));
                            be.iCodPensionario = dr.GetInt32(dr.GetOrdinal("iCodRegimenPen"));
                            be.sNombreRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreRegimenPen"));
                            be.iCodTipoPensionario = dr.GetInt32(dr.GetOrdinal("iCodTipoRegimenPen"));
                            be.sNombreTipoRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreTipoRegimenPen"));
                            if (be.sNombreTipoRegimenPensionario != "")
                            {
                                be.sRegimenPensionario = be.sNombreRegimenPensionario + " - " + be.sNombreTipoRegimenPensionario;
                            }
                            else
                            {
                                be.sRegimenPensionario = be.sNombreRegimenPensionario;
                            }
                            be.bExoneracionRenta4ta = dr.GetBoolean(dr.GetOrdinal("bEstadoSuspencionRetencion"));
                            //be.sFechaInicio = dr.GetString(dr.GetOrdinal("dtInicioLabores"));
                            be.iDiasLaborados = dr.GetInt32(dr.GetOrdinal("iDiasLaboradosPorMes"));
                            be.iDiasVacaciones = dr.GetInt32(dr.GetOrdinal("iDiasVacaciones"));
                            be.iDiasLicencias = dr.GetInt32(dr.GetOrdinal("iDiasLicencia"));
                            be.iDiasDescansos_Subsidios = dr.GetInt32(dr.GetOrdinal("iDiasDescanso"));
                            be.dcMontoRemuneracionBasica = dr.GetDecimal(dr.GetOrdinal("dRemuneracion"));
                            /*Ingresos*/
                            be.objPlanillaCalculadaIngresos_Request = new PlanillaCalculadaIngresos_Request();
                            be.objPlanillaCalculadaIngresos_Request.dcMontoContraprestacion = dr.GetDecimal(dr.GetOrdinal("dcMontoContraprestacion"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoContraprescionVacaional = dr.GetDecimal(dr.GetOrdinal("dcMontoVacaciones"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoReintegros = dr.GetDecimal(dr.GetOrdinal("dcMontoReintegro"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoCopagoSubsidio = dr.GetDecimal(dr.GetOrdinal("dcMontoCoPagoSubsidio"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoAguinaldos = dr.GetDecimal(dr.GetOrdinal("dcMontoAguinaldo"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoPagoContratoAnterior = dr.GetDecimal(dr.GetOrdinal("dcMontoPagoContratoAnterior"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoReintegroCoPagoSubsidio = dr.GetDecimal(dr.GetOrdinal("dcMontoReintegroCoPagoSubsidio"));
                            //be.objPlanillaCalculadaIngresos_Request.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));
                            be.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));
                            /*Ingresos*/
                            /*Descuentos*/
                            be.objPlanillaCalculadaDescuentos_Request = new PlanillaCalculadaDescuentos_Request();
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoTardanzas = dr.GetDecimal(dr.GetOrdinal("dcMontoTardanzas"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoInasistencias = dr.GetDecimal(dr.GetOrdinal("dcMontoInasistencias"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoPermisos = dr.GetDecimal(dr.GetOrdinal("dcMontoPermisos"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoLicenciaSinGoce = dr.GetDecimal(dr.GetOrdinal("dcMontoLicenciaSinGoce"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoSalidasAnticipadas = dr.GetDecimal(dr.GetOrdinal("dcMontoSalidasAnticipadas"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoImpuestoRenta4ta = dr.GetDecimal(dr.GetOrdinal("dcMontoRenta4ta"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoImpuestoRenta4taAguinaldo = dr.GetDecimal(dr.GetOrdinal("dcMontoImpuestoRenta4taAguinaldo"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoImpuestoRenta4taDsctoJudicialAguinaldo = dr.GetDecimal(dr.GetOrdinal("dcMontoImpuestoRenta4taDsctoJudicialAguinaldo"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoONP = dr.GetDecimal(dr.GetOrdinal("dcMontoONP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoAporteOblAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoAporteAFP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoComisionAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoComisionAFP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoPrimaSegAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoPrimaAFP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoDescuentoJudicial = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoJudicial"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoEsSaludMasVida = dr.GetDecimal(dr.GetOrdinal("dcMontoEsSaludMasVida"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoRimacSeguro = dr.GetDecimal(dr.GetOrdinal("dcMontoRimacSeguro"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoDsctoPagoExceso = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoPagoExceso"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoEPS"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoCampOftalmologica = dr.GetDecimal(dr.GetOrdinal("dcMontoCampOftalmologica"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoCapacitaciones = dr.GetDecimal(dr.GetOrdinal("dcMontoCapacitaciones"));
                            //be.dcMontoTotalDescuento = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));
                            be.dcMontoTotalDescuentos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));
                            /*Descuentos*/
                            be.dcMontoTotalNeto = dr.GetDecimal(dr.GetOrdinal("dcMontoNeto"));
                            be.dcMontoAporteEsSalud = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion"));
                            be.dcMontoAporteEsSalud_675 = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion_675"));
                            be.dcMontoAporteEsSaludEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacionEPS"));
                            be.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            be.iAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            be.sNombreFuenteFinanciamiento = dr.GetString(dr.GetOrdinal("vNombreTipoFuenteFinanciamiento"));
                            be.sNombreMeta = dr.GetString(dr.GetOrdinal("vMeta"));
                            be.sBanco = dr.GetString(dr.GetOrdinal("vBanco"));
                            be.sCuenta = dr.GetString(dr.GetOrdinal("vCuenta"));
                            be.sCCI = dr.GetString(dr.GetOrdinal("vCCI"));
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public bool GenerarPlanillaCAS(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcCAS]";
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

        public bool GenerarPlanillaPorTrabajdor(int iCodTrabajador, int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                if (iCodPlanilla==5)
                {
                    cmd.CommandText = "[dbo].[paPlanillaclcFUNCIONARIOS4_byId]";
                }
                else
                {
                    cmd.CommandText = "[dbo].[paPlanillaclcCASPorTrabajador]";
                }
                
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));                
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", 1));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", iCodTrabajador));
                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        public bool GenerarPlanillaCAS_v2(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcCAS_v2]";
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
        
        #endregion Planilla CAS General

        /*------Planilla FED------*/
        #region Planilla FED General
        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralFED(int idCodFuenteFinanciamiento, string sMetas, int iMes, int iAnio)
        {
            List<PlanillaCalculada_Request> lista = new List<PlanillaCalculada_Request>();
            PlanillaCalculada_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcFEDGeneralConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                if (idCodFuenteFinanciamiento != 0)
                {
                    cmd.Parameters.Add(new SqlParameter("@iCodFuenteFinanciamiento", idCodFuenteFinanciamiento));
                }
                if (sMetas != null && sMetas != string.Empty)
                {
                    cmd.Parameters.Add(new SqlParameter("@vMetas", sMetas));
                }

                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", 1));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculada_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            be.iCodTipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            be.sNombreTipoDocumento = dr.GetString(dr.GetOrdinal("vNombreTipoDocumento"));
                            be.sNroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            be.sNombres = dr.GetString(dr.GetOrdinal("vNombres"));
                            be.sApepPaterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            be.sApepMaterno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            be.sNombreCompleto = be.sApepPaterno + " " + be.sApepMaterno + " " + be.sNombres;
                            be.sCargo = dr.GetString(dr.GetOrdinal("vCargo"));
                            be.iCodPensionario = dr.GetInt32(dr.GetOrdinal("iCodRegimenPen"));
                            be.sNombreRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreRegimenPen"));
                            be.iCodTipoPensionario = dr.GetInt32(dr.GetOrdinal("iCodTipoRegimenPen"));
                            be.sNombreTipoRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreTipoRegimenPen"));
                            if (be.sNombreTipoRegimenPensionario != "")
                            {
                                be.sRegimenPensionario = be.sNombreRegimenPensionario + " - " + be.sNombreTipoRegimenPensionario;
                            }
                            else
                            {
                                be.sRegimenPensionario = be.sNombreRegimenPensionario;
                            }
                            be.bExoneracionRenta4ta = dr.GetBoolean(dr.GetOrdinal("bEstadoSuspencionRetencion"));
                            //be.sFechaInicio = dr.GetString(dr.GetOrdinal("dtInicioLabores"));
                            be.iDiasLaborados = dr.GetInt32(dr.GetOrdinal("iDiasLaboradosPorMes"));
                            be.iDiasVacaciones = dr.GetInt32(dr.GetOrdinal("iDiasVacaciones"));
                            be.iDiasLicencias = dr.GetInt32(dr.GetOrdinal("iDiasLicencia"));
                            be.iDiasDescansos_Subsidios = dr.GetInt32(dr.GetOrdinal("iDiasDescanso"));
                            be.dcMontoRemuneracionBasica = dr.GetDecimal(dr.GetOrdinal("dRemuneracion"));
                            be.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));
                            be.dcMontoTotalDescuentos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));
                            be.dcMontoTotalNeto = dr.GetDecimal(dr.GetOrdinal("dcMontoNeto"));
                            be.dcMontoAporteEsSalud = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion"));
                            be.dcMontoAporteEsSalud_675 = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion_675"));
                            be.dcMontoAporteEsSaludEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacionEPS"));
                            be.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            be.iAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            be.iCodDetPlanilla = 1;
                            be.iCodPlanilla = 2;
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaCalculadaIngresos_Request> ListarPlanillaCalculadaIngresosFED(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla)
        {
            List<PlanillaCalculadaIngresos_Request> lista = new List<PlanillaCalculadaIngresos_Request>();
            PlanillaCalculadaIngresos_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcFEDIngresosConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", 1));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculadaIngresos_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.dcMontoContraprestacion = dr.GetDecimal(dr.GetOrdinal("dcMontoContraprestacion"));
                            be.dcMontoContraprescionVacaional = dr.GetDecimal(dr.GetOrdinal("dcMontoVacaciones"));
                            be.dcMontoReintegros = dr.GetDecimal(dr.GetOrdinal("dcMontoReintegro"));
                            be.dcMontoCopagoSubsidio = dr.GetDecimal(dr.GetOrdinal("dcMontoCoPagoSubsidio"));
                            be.dcMontoAguinaldos = dr.GetDecimal(dr.GetOrdinal("dcMontoAguinaldo"));
                            be.dcMontoPagoContratoAnterior = dr.GetDecimal(dr.GetOrdinal("dcMontoPagoContratoAnterior"));
                            be.dcMontoReintegroCoPagoSubsidio = dr.GetDecimal(dr.GetOrdinal("dcMontoReintegroCoPagoSubsidio"));
                            be.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));

                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaCalculadaDescuentos_Request> ListarPlanillaCalculadaDescuentosFED(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla)
        {
            List<PlanillaCalculadaDescuentos_Request> lista = new List<PlanillaCalculadaDescuentos_Request>();
            PlanillaCalculadaDescuentos_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcFEDDescuentosConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", 1));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculadaDescuentos_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.dcMontoTardanzas = dr.GetDecimal(dr.GetOrdinal("dcMontoTardanzas"));
                            be.dcMontoInasistencias = dr.GetDecimal(dr.GetOrdinal("dcMontoInasistencias"));
                            be.dcMontoPermisos = dr.GetDecimal(dr.GetOrdinal("dcMontoPermisos"));
                            be.dcMontoLicenciaSinGoce = dr.GetDecimal(dr.GetOrdinal("dcMontoLicenciaSinGoce"));
                            be.dcMontoSalidasAnticipadas = dr.GetDecimal(dr.GetOrdinal("dcMontoSalidasAnticipadas"));
                            be.dcMontoImpuestoRenta4ta = dr.GetDecimal(dr.GetOrdinal("dcMontoRenta4ta"));
                            be.dcMontoImpuestoRenta4taAguinaldo = dr.GetDecimal(dr.GetOrdinal("dcMontoImpuestoRenta4taAguinaldo"));
                            be.dcMontoImpuestoRenta4taDsctoJudicialAguinaldo = dr.GetDecimal(dr.GetOrdinal("dcMontoImpuestoRenta4taDsctoJudicialAguinaldo"));
                            be.dcMontoONP = dr.GetDecimal(dr.GetOrdinal("dcMontoONP"));
                            be.dcMontoAporteOblAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoAporteAFP"));
                            be.dcMontoComisionAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoComisionAFP"));
                            be.dcMontoPrimaSegAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoPrimaAFP"));
                            be.dcMontoDescuentoJudicial = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoJudicial"));
                            be.dcMontoEsSaludMasVida = dr.GetDecimal(dr.GetOrdinal("dcMontoEsSaludMasVida"));
                            be.dcMontoRimacSeguro = dr.GetDecimal(dr.GetOrdinal("dcMontoRimacSeguro"));
                            be.dcMontoDsctoPagoExceso = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoPagoExceso"));
                            be.dcMontoEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoEPS"));
                            be.dcMontoCampOftalmologica = dr.GetDecimal(dr.GetOrdinal("dcMontoCampOftalmologica"));
                            be.dcMontoCapacitaciones = dr.GetDecimal(dr.GetOrdinal("dcMontoCapacitaciones"));
                            be.dcMontoTotalDescuento = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));

                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralCompletaFED(int idCodFuenteFinanciamiento, string sMetas, int iMes, int iAnio)
        {
            List<PlanillaCalculada_Request> lista = new List<PlanillaCalculada_Request>();
            PlanillaCalculada_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcFEDGeneralCompletaConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                if (idCodFuenteFinanciamiento != 0)
                {
                    cmd.Parameters.Add(new SqlParameter("@iCodFuenteFinanciamiento", idCodFuenteFinanciamiento));
                }
                if (sMetas != null && sMetas != string.Empty)
                {
                    cmd.Parameters.Add(new SqlParameter("@vMetas", sMetas));
                }

                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", 1));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculada_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            be.iCodTipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            be.sNombreTipoDocumento = dr.GetString(dr.GetOrdinal("vNombreTipoDocumento"));
                            be.sNroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            be.sNombres = dr.GetString(dr.GetOrdinal("vNombres"));
                            be.sApepPaterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            be.sApepMaterno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            be.sNombreCompleto = be.sApepPaterno + " " + be.sApepMaterno + " " + be.sNombres;
                            be.sCargo = dr.GetString(dr.GetOrdinal("vCargo"));
                            be.iCodPensionario = dr.GetInt32(dr.GetOrdinal("iCodRegimenPen"));
                            be.sNombreRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreRegimenPen"));
                            be.iCodTipoPensionario = dr.GetInt32(dr.GetOrdinal("iCodTipoRegimenPen"));
                            be.sNombreTipoRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreTipoRegimenPen"));
                            if (be.sNombreTipoRegimenPensionario != "")
                            {
                                be.sRegimenPensionario = be.sNombreRegimenPensionario + " - " + be.sNombreTipoRegimenPensionario;
                            }
                            else
                            {
                                be.sRegimenPensionario = be.sNombreRegimenPensionario;
                            }
                            be.bExoneracionRenta4ta = dr.GetBoolean(dr.GetOrdinal("bEstadoSuspencionRetencion"));
                            //be.sFechaInicio = dr.GetString(dr.GetOrdinal("dtInicioLabores"));
                            be.iDiasLaborados = dr.GetInt32(dr.GetOrdinal("iDiasLaboradosPorMes"));
                            be.iDiasVacaciones = dr.GetInt32(dr.GetOrdinal("iDiasVacaciones"));
                            be.iDiasLicencias = dr.GetInt32(dr.GetOrdinal("iDiasLicencia"));
                            be.iDiasDescansos_Subsidios = dr.GetInt32(dr.GetOrdinal("iDiasDescanso"));
                            be.dcMontoRemuneracionBasica = dr.GetDecimal(dr.GetOrdinal("dRemuneracion"));
                            /*Ingresos*/
                            be.objPlanillaCalculadaIngresos_Request = new PlanillaCalculadaIngresos_Request();
                            be.objPlanillaCalculadaIngresos_Request.dcMontoContraprestacion = dr.GetDecimal(dr.GetOrdinal("dcMontoContraprestacion"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoContraprescionVacaional = dr.GetDecimal(dr.GetOrdinal("dcMontoVacaciones"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoReintegros = dr.GetDecimal(dr.GetOrdinal("dcMontoReintegro"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoCopagoSubsidio = dr.GetDecimal(dr.GetOrdinal("dcMontoCoPagoSubsidio"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoAguinaldos = dr.GetDecimal(dr.GetOrdinal("dcMontoAguinaldo"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoPagoContratoAnterior = dr.GetDecimal(dr.GetOrdinal("dcMontoPagoContratoAnterior"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoReintegroCoPagoSubsidio = dr.GetDecimal(dr.GetOrdinal("dcMontoReintegroCoPagoSubsidio"));
                            //be.objPlanillaCalculadaIngresos_Request.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));
                            be.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));
                            /*Ingresos*/
                            /*Descuentos*/
                            be.objPlanillaCalculadaDescuentos_Request = new PlanillaCalculadaDescuentos_Request();
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoTardanzas = dr.GetDecimal(dr.GetOrdinal("dcMontoTardanzas"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoInasistencias = dr.GetDecimal(dr.GetOrdinal("dcMontoInasistencias"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoPermisos = dr.GetDecimal(dr.GetOrdinal("dcMontoPermisos"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoLicenciaSinGoce = dr.GetDecimal(dr.GetOrdinal("dcMontoLicenciaSinGoce"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoSalidasAnticipadas = dr.GetDecimal(dr.GetOrdinal("dcMontoSalidasAnticipadas"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoImpuestoRenta4ta = dr.GetDecimal(dr.GetOrdinal("dcMontoRenta4ta"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoImpuestoRenta4taAguinaldo = dr.GetDecimal(dr.GetOrdinal("dcMontoImpuestoRenta4taAguinaldo"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoImpuestoRenta4taDsctoJudicialAguinaldo = dr.GetDecimal(dr.GetOrdinal("dcMontoImpuestoRenta4taDsctoJudicialAguinaldo"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoONP = dr.GetDecimal(dr.GetOrdinal("dcMontoONP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoAporteOblAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoAporteAFP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoComisionAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoComisionAFP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoPrimaSegAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoPrimaAFP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoDescuentoJudicial = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoJudicial"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoEsSaludMasVida = dr.GetDecimal(dr.GetOrdinal("dcMontoEsSaludMasVida"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoRimacSeguro = dr.GetDecimal(dr.GetOrdinal("dcMontoRimacSeguro"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoDsctoPagoExceso = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoPagoExceso"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoEPS"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoCampOftalmologica = dr.GetDecimal(dr.GetOrdinal("dcMontoCampOftalmologica"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoCapacitaciones = dr.GetDecimal(dr.GetOrdinal("dcMontoCapacitaciones"));
                            //be.dcMontoTotalDescuento = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));
                            be.dcMontoTotalDescuentos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));
                            /*Descuentos*/
                            be.dcMontoTotalNeto = dr.GetDecimal(dr.GetOrdinal("dcMontoNeto"));
                            be.dcMontoAporteEsSalud = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion"));
                            be.dcMontoAporteEsSalud_675 = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion_675"));
                            be.dcMontoAporteEsSaludEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacionEPS"));
                            be.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            be.iAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            be.sNombreFuenteFinanciamiento = dr.GetString(dr.GetOrdinal("vNombreTipoFuenteFinanciamiento"));
                            be.sNombreMeta = dr.GetString(dr.GetOrdinal("vMeta"));
                            be.sBanco = dr.GetString(dr.GetOrdinal("vBanco"));
                            be.sCuenta = dr.GetString(dr.GetOrdinal("vCuenta"));
                            be.sCCI = dr.GetString(dr.GetOrdinal("vCCI"));
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public bool GenerarPlanillaFED(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcFED]";
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

        #endregion Planilla FED General

        /*------Planilla SECIGRISTAS------*/
        #region Planilla SECIGRISTAS General
        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralSECIGRISTAS(int idCodFuenteFinanciamiento, string sMetas, int iMes, int iAnio)
        {
            List<PlanillaCalculada_Request> lista = new List<PlanillaCalculada_Request>();
            PlanillaCalculada_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcSECIGRISTAGeneralConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                if (idCodFuenteFinanciamiento != 0)
                {
                    cmd.Parameters.Add(new SqlParameter("@iCodFuenteFinanciamiento", idCodFuenteFinanciamiento));
                }
                if (sMetas != null && sMetas != string.Empty)
                {
                    cmd.Parameters.Add(new SqlParameter("@vMetas", sMetas));
                }

                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", 13));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", 1));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculada_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            be.iCodTipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            be.sNombreTipoDocumento = dr.GetString(dr.GetOrdinal("vNombreTipoDocumento"));
                            be.sNroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            be.sNombres = dr.GetString(dr.GetOrdinal("vNombres"));
                            be.sApepPaterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            be.sApepMaterno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            be.sNombreCompleto = be.sApepPaterno + " " + be.sApepMaterno + " " + be.sNombres;
                            //be.sCargo = dr.GetString(dr.GetOrdinal("vCargo"));
                            //be.iCodPensionario = dr.GetInt32(dr.GetOrdinal("iCodRegimenPen"));
                            //be.sNombreRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreRegimenPen"));
                            //be.iCodTipoPensionario = dr.GetInt32(dr.GetOrdinal("iCodTipoRegimenPen"));
                            //be.sNombreTipoRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreTipoRegimenPen"));
                            //if (be.sNombreTipoRegimenPensionario != "")
                            //{
                            //    be.sRegimenPensionario = be.sNombreRegimenPensionario + " - " + be.sNombreTipoRegimenPensionario;
                            //}
                            //else
                            //{
                            //    be.sRegimenPensionario = be.sNombreRegimenPensionario;
                            //}
                            //be.bExoneracionRenta4ta = dr.GetBoolean(dr.GetOrdinal("bEstadoSuspencionRetencion"));
                            //be.sFechaInicio = dr.GetString(dr.GetOrdinal("dtInicioLabores"));
                            be.iDiasLaborados = dr.GetInt32(dr.GetOrdinal("iDiasLaboradosPorMes"));
                            //be.iDiasVacaciones = dr.GetInt32(dr.GetOrdinal("iDiasVacaciones"));
                            //be.iDiasLicencias = dr.GetInt32(dr.GetOrdinal("iDiasLicencia"));
                            //be.iDiasDescansos_Subsidios = dr.GetInt32(dr.GetOrdinal("iDiasDescanso"));
                            be.dcMontoRemuneracionBasica = dr.GetDecimal(dr.GetOrdinal("dRemuneracion"));
                            be.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));
                            be.dcMontoTotalDescuentos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));
                            be.dcMontoTotalNeto = dr.GetDecimal(dr.GetOrdinal("dcMontoNeto"));
                            //be.dcMontoAporteEsSalud = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion"));
                            //be.dcMontoAporteEsSalud_675 = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion_675"));
                            //be.dcMontoAporteEsSaludEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacionEPS"));
                            be.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            be.iAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            be.iCodDetPlanilla = 1;
                            be.iCodPlanilla = 1;
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaCalculadaIngresos_Request> ListarPlanillaCalculadaIngresosSECIGRISTAS(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla)
        {
            List<PlanillaCalculadaIngresos_Request> lista = new List<PlanillaCalculadaIngresos_Request>();
            PlanillaCalculadaIngresos_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcSECIGRISTAIngresosConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", 1));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", 13));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculadaIngresos_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.dcMontoContraprestacion = dr.GetDecimal(dr.GetOrdinal("dcMontoContraprestacion"));
                            //be.dcMontoContraprescionVacaional = dr.GetDecimal(dr.GetOrdinal("dcMontoVacaciones"));
                            be.dcMontoReintegros = dr.GetDecimal(dr.GetOrdinal("dcMontoReintegro"));
                            //be.dcMontoCopagoSubsidio = dr.GetDecimal(dr.GetOrdinal("dcMontoCoPagoSubsidio"));
                            //be.dcMontoAguinaldos = dr.GetDecimal(dr.GetOrdinal("dcMontoAguinaldo"));
                            //be.dcMontoPagoContratoAnterior = dr.GetDecimal(dr.GetOrdinal("dcMontoPagoContratoAnterior"));
                            //be.dcMontoReintegroCoPagoSubsidio = dr.GetDecimal(dr.GetOrdinal("dcMontoReintegroCoPagoSubsidio"));
                            be.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));

                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaCalculadaDescuentos_Request> ListarPlanillaCalculadaDescuentosSECIGRISTAS(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla)
        {
            List<PlanillaCalculadaDescuentos_Request> lista = new List<PlanillaCalculadaDescuentos_Request>();
            PlanillaCalculadaDescuentos_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcCASDescuentosConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", 1));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculadaDescuentos_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            //be.dcMontoTardanzas = dr.GetDecimal(dr.GetOrdinal("dcMontoTardanzas"));
                            //be.dcMontoInasistencias = dr.GetDecimal(dr.GetOrdinal("dcMontoInasistencias"));
                            //be.dcMontoPermisos = dr.GetDecimal(dr.GetOrdinal("dcMontoPermisos"));
                            //be.dcMontoLicenciaSinGoce = dr.GetDecimal(dr.GetOrdinal("dcMontoLicenciaSinGoce"));
                            //be.dcMontoSalidasAnticipadas = dr.GetDecimal(dr.GetOrdinal("dcMontoSalidasAnticipadas"));
                            //be.dcMontoImpuestoRenta4ta = dr.GetDecimal(dr.GetOrdinal("dcMontoRenta4ta"));
                            //be.dcMontoImpuestoRenta4taAguinaldo = dr.GetDecimal(dr.GetOrdinal("dcMontoImpuestoRenta4taAguinaldo"));
                            //be.dcMontoImpuestoRenta4taDsctoJudicialAguinaldo = dr.GetDecimal(dr.GetOrdinal("dcMontoImpuestoRenta4taDsctoJudicialAguinaldo"));
                            //be.dcMontoONP = dr.GetDecimal(dr.GetOrdinal("dcMontoONP"));
                            //be.dcMontoAporteOblAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoAporteAFP"));
                            //be.dcMontoComisionAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoComisionAFP"));
                            //be.dcMontoPrimaSegAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoPrimaAFP"));
                            //be.dcMontoDescuentoJudicial = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoJudicial"));
                            //be.dcMontoEsSaludMasVida = dr.GetDecimal(dr.GetOrdinal("dcMontoEsSaludMasVida"));
                            //be.dcMontoRimacSeguro = dr.GetDecimal(dr.GetOrdinal("dcMontoRimacSeguro"));
                            //be.dcMontoDsctoPagoExceso = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoPagoExceso"));
                            //be.dcMontoEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoEPS"));
                            //be.dcMontoCampOftalmologica = dr.GetDecimal(dr.GetOrdinal("dcMontoCampOftalmologica"));
                            //be.dcMontoCapacitaciones = dr.GetDecimal(dr.GetOrdinal("dcMontoCapacitaciones"));
                            //be.dcMontoTotalDescuento = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));

                            //lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralCompletaSECIGRISTAS(int idCodFuenteFinanciamiento, string sMetas, int iMes, int iAnio)
        {
            List<PlanillaCalculada_Request> lista = new List<PlanillaCalculada_Request>();
            PlanillaCalculada_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcCASGeneralCompletaConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                if (idCodFuenteFinanciamiento != 0)
                {
                    cmd.Parameters.Add(new SqlParameter("@iCodFuenteFinanciamiento", idCodFuenteFinanciamiento));
                }
                if (sMetas != null && sMetas != string.Empty)
                {
                    cmd.Parameters.Add(new SqlParameter("@vMetas", sMetas));
                }

                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", 1));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculada_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            be.iCodTipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            be.sNombreTipoDocumento = dr.GetString(dr.GetOrdinal("vNombreTipoDocumento"));
                            be.sNroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            be.sNombres = dr.GetString(dr.GetOrdinal("vNombres"));
                            be.sApepPaterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            be.sApepMaterno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            be.sNombreCompleto = be.sApepPaterno + " " + be.sApepMaterno + " " + be.sNombres;
                            be.sCargo = dr.GetString(dr.GetOrdinal("vCargo"));
                            be.iCodPensionario = dr.GetInt32(dr.GetOrdinal("iCodRegimenPen"));
                            be.sNombreRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreRegimenPen"));
                            be.iCodTipoPensionario = dr.GetInt32(dr.GetOrdinal("iCodTipoRegimenPen"));
                            be.sNombreTipoRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreTipoRegimenPen"));
                            if (be.sNombreTipoRegimenPensionario != "")
                            {
                                be.sRegimenPensionario = be.sNombreRegimenPensionario + " - " + be.sNombreTipoRegimenPensionario;
                            }
                            else
                            {
                                be.sRegimenPensionario = be.sNombreRegimenPensionario;
                            }
                            be.bExoneracionRenta4ta = dr.GetBoolean(dr.GetOrdinal("bEstadoSuspencionRetencion"));
                            //be.sFechaInicio = dr.GetString(dr.GetOrdinal("dtInicioLabores"));
                            be.iDiasLaborados = dr.GetInt32(dr.GetOrdinal("iDiasLaboradosPorMes"));
                            be.iDiasVacaciones = dr.GetInt32(dr.GetOrdinal("iDiasVacaciones"));
                            be.iDiasLicencias = dr.GetInt32(dr.GetOrdinal("iDiasLicencia"));
                            be.iDiasDescansos_Subsidios = dr.GetInt32(dr.GetOrdinal("iDiasDescanso"));
                            be.dcMontoRemuneracionBasica = dr.GetDecimal(dr.GetOrdinal("dRemuneracion"));
                            /*Ingresos*/
                            be.objPlanillaCalculadaIngresos_Request = new PlanillaCalculadaIngresos_Request();
                            be.objPlanillaCalculadaIngresos_Request.dcMontoContraprestacion = dr.GetDecimal(dr.GetOrdinal("dcMontoContraprestacion"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoContraprescionVacaional = dr.GetDecimal(dr.GetOrdinal("dcMontoVacaciones"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoReintegros = dr.GetDecimal(dr.GetOrdinal("dcMontoReintegro"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoCopagoSubsidio = dr.GetDecimal(dr.GetOrdinal("dcMontoCoPagoSubsidio"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoAguinaldos = dr.GetDecimal(dr.GetOrdinal("dcMontoAguinaldo"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoPagoContratoAnterior = dr.GetDecimal(dr.GetOrdinal("dcMontoPagoContratoAnterior"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoReintegroCoPagoSubsidio = dr.GetDecimal(dr.GetOrdinal("dcMontoReintegroCoPagoSubsidio"));
                            //be.objPlanillaCalculadaIngresos_Request.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));
                            be.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));
                            /*Ingresos*/
                            /*Descuentos*/
                            be.objPlanillaCalculadaDescuentos_Request = new PlanillaCalculadaDescuentos_Request();
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoTardanzas = dr.GetDecimal(dr.GetOrdinal("dcMontoTardanzas"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoInasistencias = dr.GetDecimal(dr.GetOrdinal("dcMontoInasistencias"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoPermisos = dr.GetDecimal(dr.GetOrdinal("dcMontoPermisos"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoLicenciaSinGoce = dr.GetDecimal(dr.GetOrdinal("dcMontoLicenciaSinGoce"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoSalidasAnticipadas = dr.GetDecimal(dr.GetOrdinal("dcMontoSalidasAnticipadas"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoImpuestoRenta4ta = dr.GetDecimal(dr.GetOrdinal("dcMontoRenta4ta"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoImpuestoRenta4taAguinaldo = dr.GetDecimal(dr.GetOrdinal("dcMontoImpuestoRenta4taAguinaldo"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoImpuestoRenta4taDsctoJudicialAguinaldo = dr.GetDecimal(dr.GetOrdinal("dcMontoImpuestoRenta4taDsctoJudicialAguinaldo"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoONP = dr.GetDecimal(dr.GetOrdinal("dcMontoONP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoAporteOblAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoAporteAFP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoComisionAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoComisionAFP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoPrimaSegAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoPrimaAFP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoDescuentoJudicial = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoJudicial"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoEsSaludMasVida = dr.GetDecimal(dr.GetOrdinal("dcMontoEsSaludMasVida"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoRimacSeguro = dr.GetDecimal(dr.GetOrdinal("dcMontoRimacSeguro"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoDsctoPagoExceso = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoPagoExceso"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoEPS"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoCampOftalmologica = dr.GetDecimal(dr.GetOrdinal("dcMontoCampOftalmologica"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoCapacitaciones = dr.GetDecimal(dr.GetOrdinal("dcMontoCapacitaciones"));
                            //be.dcMontoTotalDescuento = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));
                            be.dcMontoTotalDescuentos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));
                            /*Descuentos*/
                            be.dcMontoTotalNeto = dr.GetDecimal(dr.GetOrdinal("dcMontoNeto"));
                            be.dcMontoAporteEsSalud = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion"));
                            be.dcMontoAporteEsSalud_675 = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion_675"));
                            be.dcMontoAporteEsSaludEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacionEPS"));
                            be.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            be.iAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            be.sNombreFuenteFinanciamiento = dr.GetString(dr.GetOrdinal("vNombreTipoFuenteFinanciamiento"));
                            be.sNombreMeta = dr.GetString(dr.GetOrdinal("vMeta"));
                            be.sBanco = dr.GetString(dr.GetOrdinal("vBanco"));
                            be.sCuenta = dr.GetString(dr.GetOrdinal("vCuenta"));
                            be.sCCI = dr.GetString(dr.GetOrdinal("vCCI"));
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public bool GenerarPlanillaSECIGRISTAS(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcSECIGRISTA]";
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

        public bool GenerarPlanillaPorTrabajdorSECIGRISTAS(int iCodTrabajador, int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                if (iCodPlanilla == 5)
                {
                    cmd.CommandText = "[dbo].[paPlanillaclcFUNCIONARIOS4_byId]";
                }
                else
                {
                    cmd.CommandText = "[dbo].[paPlanillaclcCASPorTrabajador]";
                }

                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", 1));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", iCodTrabajador));
                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }
                
        #endregion Planilla SECIGRISTAS General

        /*------Planilla PRACTICANTES------*/
        #region Planilla PRACTICANTES General
        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralPRACTICANTES(int idCodFuenteFinanciamiento, string sMetas, int iMes, int iAnio)
        {
            List<PlanillaCalculada_Request> lista = new List<PlanillaCalculada_Request>();
            PlanillaCalculada_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcPRACTICANTEGeneralConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                if (idCodFuenteFinanciamiento != 0)
                {
                    cmd.Parameters.Add(new SqlParameter("@iCodFuenteFinanciamiento", idCodFuenteFinanciamiento));
                }
                if (sMetas != null && sMetas != string.Empty)
                {
                    cmd.Parameters.Add(new SqlParameter("@vMetas", sMetas));
                }

                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", 1));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", 12));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculada_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            be.iCodTipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            be.sNombreTipoDocumento = dr.GetString(dr.GetOrdinal("vNombreTipoDocumento"));
                            be.sNroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            be.sNombres = dr.GetString(dr.GetOrdinal("vNombres"));
                            be.sApepPaterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            be.sApepMaterno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            be.sNombreCompleto = be.sApepPaterno + " " + be.sApepMaterno + " " + be.sNombres;
                            be.sCargo = dr.GetString(dr.GetOrdinal("vCargo"));
                            //be.iCodPensionario = dr.GetInt32(dr.GetOrdinal("iCodRegimenPen"));
                            //be.sNombreRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreRegimenPen"));
                            //be.iCodTipoPensionario = dr.GetInt32(dr.GetOrdinal("iCodTipoRegimenPen"));
                            //be.sNombreTipoRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreTipoRegimenPen"));
                            //if (be.sNombreTipoRegimenPensionario != "")
                            //{
                            //    be.sRegimenPensionario = be.sNombreRegimenPensionario + " - " + be.sNombreTipoRegimenPensionario;
                            //}
                            //else
                            //{
                            //    be.sRegimenPensionario = be.sNombreRegimenPensionario;
                            //}
                            //be.bExoneracionRenta4ta = dr.GetBoolean(dr.GetOrdinal("bEstadoSuspencionRetencion"));
                            //be.sFechaInicio = dr.GetString(dr.GetOrdinal("dtInicioLabores"));
                            be.iDiasLaborados = dr.GetInt32(dr.GetOrdinal("iDiasLaboradosPorMes"));
                            //be.iDiasVacaciones = dr.GetInt32(dr.GetOrdinal("iDiasVacaciones"));
                            //be.iDiasLicencias = dr.GetInt32(dr.GetOrdinal("iDiasLicencia"));
                            //be.iDiasDescansos_Subsidios = dr.GetInt32(dr.GetOrdinal("iDiasDescanso"));
                            be.dcMontoRemuneracionBasica = dr.GetDecimal(dr.GetOrdinal("dRemuneracion"));
                            be.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));
                            be.dcMontoTotalDescuentos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));
                            be.dcMontoTotalNeto = dr.GetDecimal(dr.GetOrdinal("dcMontoNeto"));
                            //be.dcMontoAporteEsSalud = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion"));
                            //be.dcMontoAporteEsSalud_675 = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion_675"));
                            //be.dcMontoAporteEsSaludEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacionEPS"));
                            be.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            be.iAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            be.iCodDetPlanilla = 1;
                            be.iCodPlanilla = 1;
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaCalculadaIngresos_Request> ListarPlanillaCalculadaIngresosPRACTICANTES(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla)
        {
            List<PlanillaCalculadaIngresos_Request> lista = new List<PlanillaCalculadaIngresos_Request>();
            PlanillaCalculadaIngresos_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcPRACTICANTEIngresosConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", 1));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", 12));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculadaIngresos_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.dcMontoContraprestacion = dr.GetDecimal(dr.GetOrdinal("dcMontoContraprestacion"));
                            //be.dcMontoContraprescionVacaional = dr.GetDecimal(dr.GetOrdinal("dcMontoVacaciones"));
                            be.dcMontoReintegros = dr.GetDecimal(dr.GetOrdinal("dcMontoReintegro"));
                            //be.dcMontoCopagoSubsidio = dr.GetDecimal(dr.GetOrdinal("dcMontoCoPagoSubsidio"));
                            //be.dcMontoAguinaldos = dr.GetDecimal(dr.GetOrdinal("dcMontoAguinaldo"));
                            //be.dcMontoPagoContratoAnterior = dr.GetDecimal(dr.GetOrdinal("dcMontoPagoContratoAnterior"));
                            //be.dcMontoReintegroCoPagoSubsidio = dr.GetDecimal(dr.GetOrdinal("dcMontoReintegroCoPagoSubsidio"));
                            be.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));

                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaCalculadaDescuentos_Request> ListarPlanillaCalculadaDescuentosPRACTICANTES(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla)
        {
            List<PlanillaCalculadaDescuentos_Request> lista = new List<PlanillaCalculadaDescuentos_Request>();
            PlanillaCalculadaDescuentos_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcPRACTICANTEDescuentosConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", 1));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", 12));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculadaDescuentos_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.dcMontoTardanzas = dr.GetDecimal(dr.GetOrdinal("dcMontoTardanzas"));
                            be.dcMontoInasistencias = dr.GetDecimal(dr.GetOrdinal("dcMontoInasistencias"));
                            //be.dcMontoPermisos = dr.GetDecimal(dr.GetOrdinal("dcMontoPermisos"));
                            //be.dcMontoLicenciaSinGoce = dr.GetDecimal(dr.GetOrdinal("dcMontoLicenciaSinGoce"));
                            //be.dcMontoSalidasAnticipadas = dr.GetDecimal(dr.GetOrdinal("dcMontoSalidasAnticipadas"));
                            //be.dcMontoImpuestoRenta4ta = dr.GetDecimal(dr.GetOrdinal("dcMontoRenta4ta"));
                            //be.dcMontoImpuestoRenta4taAguinaldo = dr.GetDecimal(dr.GetOrdinal("dcMontoImpuestoRenta4taAguinaldo"));
                            //be.dcMontoImpuestoRenta4taDsctoJudicialAguinaldo = dr.GetDecimal(dr.GetOrdinal("dcMontoImpuestoRenta4taDsctoJudicialAguinaldo"));
                            //be.dcMontoONP = dr.GetDecimal(dr.GetOrdinal("dcMontoONP"));
                            //be.dcMontoAporteOblAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoAporteAFP"));
                            //be.dcMontoComisionAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoComisionAFP"));
                            //be.dcMontoPrimaSegAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoPrimaAFP"));
                            //be.dcMontoDescuentoJudicial = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoJudicial"));
                            //be.dcMontoEsSaludMasVida = dr.GetDecimal(dr.GetOrdinal("dcMontoEsSaludMasVida"));
                            //be.dcMontoRimacSeguro = dr.GetDecimal(dr.GetOrdinal("dcMontoRimacSeguro"));
                            //be.dcMontoDsctoPagoExceso = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoPagoExceso"));
                            //be.dcMontoEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoEPS"));
                            //be.dcMontoCampOftalmologica = dr.GetDecimal(dr.GetOrdinal("dcMontoCampOftalmologica"));
                            //be.dcMontoCapacitaciones = dr.GetDecimal(dr.GetOrdinal("dcMontoCapacitaciones"));
                            be.dcMontoTotalDescuento = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));

                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralCompletaPRACTICANTES(int idCodFuenteFinanciamiento, string sMetas, int iMes, int iAnio)
        {
            List<PlanillaCalculada_Request> lista = new List<PlanillaCalculada_Request>();
            PlanillaCalculada_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcCASGeneralCompletaConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                if (idCodFuenteFinanciamiento != 0)
                {
                    cmd.Parameters.Add(new SqlParameter("@iCodFuenteFinanciamiento", idCodFuenteFinanciamiento));
                }
                if (sMetas != null && sMetas != string.Empty)
                {
                    cmd.Parameters.Add(new SqlParameter("@vMetas", sMetas));
                }

                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", 1));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculada_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            be.iCodTipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            be.sNombreTipoDocumento = dr.GetString(dr.GetOrdinal("vNombreTipoDocumento"));
                            be.sNroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            be.sNombres = dr.GetString(dr.GetOrdinal("vNombres"));
                            be.sApepPaterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            be.sApepMaterno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            be.sNombreCompleto = be.sApepPaterno + " " + be.sApepMaterno + " " + be.sNombres;
                            be.sCargo = dr.GetString(dr.GetOrdinal("vCargo"));
                            be.iCodPensionario = dr.GetInt32(dr.GetOrdinal("iCodRegimenPen"));
                            be.sNombreRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreRegimenPen"));
                            be.iCodTipoPensionario = dr.GetInt32(dr.GetOrdinal("iCodTipoRegimenPen"));
                            be.sNombreTipoRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreTipoRegimenPen"));
                            if (be.sNombreTipoRegimenPensionario != "")
                            {
                                be.sRegimenPensionario = be.sNombreRegimenPensionario + " - " + be.sNombreTipoRegimenPensionario;
                            }
                            else
                            {
                                be.sRegimenPensionario = be.sNombreRegimenPensionario;
                            }
                            be.bExoneracionRenta4ta = dr.GetBoolean(dr.GetOrdinal("bEstadoSuspencionRetencion"));
                            //be.sFechaInicio = dr.GetString(dr.GetOrdinal("dtInicioLabores"));
                            be.iDiasLaborados = dr.GetInt32(dr.GetOrdinal("iDiasLaboradosPorMes"));
                            be.iDiasVacaciones = dr.GetInt32(dr.GetOrdinal("iDiasVacaciones"));
                            be.iDiasLicencias = dr.GetInt32(dr.GetOrdinal("iDiasLicencia"));
                            be.iDiasDescansos_Subsidios = dr.GetInt32(dr.GetOrdinal("iDiasDescanso"));
                            be.dcMontoRemuneracionBasica = dr.GetDecimal(dr.GetOrdinal("dRemuneracion"));
                            /*Ingresos*/
                            be.objPlanillaCalculadaIngresos_Request = new PlanillaCalculadaIngresos_Request();
                            be.objPlanillaCalculadaIngresos_Request.dcMontoContraprestacion = dr.GetDecimal(dr.GetOrdinal("dcMontoContraprestacion"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoContraprescionVacaional = dr.GetDecimal(dr.GetOrdinal("dcMontoVacaciones"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoReintegros = dr.GetDecimal(dr.GetOrdinal("dcMontoReintegro"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoCopagoSubsidio = dr.GetDecimal(dr.GetOrdinal("dcMontoCoPagoSubsidio"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoAguinaldos = dr.GetDecimal(dr.GetOrdinal("dcMontoAguinaldo"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoPagoContratoAnterior = dr.GetDecimal(dr.GetOrdinal("dcMontoPagoContratoAnterior"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoReintegroCoPagoSubsidio = dr.GetDecimal(dr.GetOrdinal("dcMontoReintegroCoPagoSubsidio"));
                            //be.objPlanillaCalculadaIngresos_Request.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));
                            be.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));
                            /*Ingresos*/
                            /*Descuentos*/
                            be.objPlanillaCalculadaDescuentos_Request = new PlanillaCalculadaDescuentos_Request();
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoTardanzas = dr.GetDecimal(dr.GetOrdinal("dcMontoTardanzas"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoInasistencias = dr.GetDecimal(dr.GetOrdinal("dcMontoInasistencias"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoPermisos = dr.GetDecimal(dr.GetOrdinal("dcMontoPermisos"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoLicenciaSinGoce = dr.GetDecimal(dr.GetOrdinal("dcMontoLicenciaSinGoce"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoSalidasAnticipadas = dr.GetDecimal(dr.GetOrdinal("dcMontoSalidasAnticipadas"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoImpuestoRenta4ta = dr.GetDecimal(dr.GetOrdinal("dcMontoRenta4ta"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoImpuestoRenta4taAguinaldo = dr.GetDecimal(dr.GetOrdinal("dcMontoImpuestoRenta4taAguinaldo"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoImpuestoRenta4taDsctoJudicialAguinaldo = dr.GetDecimal(dr.GetOrdinal("dcMontoImpuestoRenta4taDsctoJudicialAguinaldo"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoONP = dr.GetDecimal(dr.GetOrdinal("dcMontoONP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoAporteOblAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoAporteAFP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoComisionAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoComisionAFP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoPrimaSegAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoPrimaAFP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoDescuentoJudicial = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoJudicial"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoEsSaludMasVida = dr.GetDecimal(dr.GetOrdinal("dcMontoEsSaludMasVida"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoRimacSeguro = dr.GetDecimal(dr.GetOrdinal("dcMontoRimacSeguro"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoDsctoPagoExceso = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoPagoExceso"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoEPS"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoCampOftalmologica = dr.GetDecimal(dr.GetOrdinal("dcMontoCampOftalmologica"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoCapacitaciones = dr.GetDecimal(dr.GetOrdinal("dcMontoCapacitaciones"));
                            //be.dcMontoTotalDescuento = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));
                            be.dcMontoTotalDescuentos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));
                            /*Descuentos*/
                            be.dcMontoTotalNeto = dr.GetDecimal(dr.GetOrdinal("dcMontoNeto"));
                            be.dcMontoAporteEsSalud = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion"));
                            be.dcMontoAporteEsSalud_675 = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion_675"));
                            be.dcMontoAporteEsSaludEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacionEPS"));
                            be.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            be.iAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            be.sNombreFuenteFinanciamiento = dr.GetString(dr.GetOrdinal("vNombreTipoFuenteFinanciamiento"));
                            be.sNombreMeta = dr.GetString(dr.GetOrdinal("vMeta"));
                            be.sBanco = dr.GetString(dr.GetOrdinal("vBanco"));
                            be.sCuenta = dr.GetString(dr.GetOrdinal("vCuenta"));
                            be.sCCI = dr.GetString(dr.GetOrdinal("vCCI"));
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public bool GenerarPlanillaPRACTICANTES(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcPRACTICANTE]";
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

        public bool GenerarPlanillaPorTrabajdorPRACTICANTES(int iCodTrabajador, int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                if (iCodPlanilla == 5)
                {
                    cmd.CommandText = "[dbo].[paPlanillaclcFUNCIONARIOS4_byId]";
                }
                else
                {
                    cmd.CommandText = "[dbo].[paPlanillaclcCASPorTrabajador]";
                }

                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", 1));
                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", iCodTrabajador));
                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        #endregion Planilla PRACTICANTES General

        /*------Planilla CAS Adicional------*/
        #region Planilla CAS Adicional
        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralCASAdicional(int idCodFuenteFinanciamiento, string sMetas, int iMes, int iAnio, int iCodPlanilla, int iCodDetPlanilla)
        {
            List<PlanillaCalculada_Request> lista = new List<PlanillaCalculada_Request>();
            PlanillaCalculada_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcCASAdicionalConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //if (idCodFuenteFinanciamiento != 0)
                //{
                //    cmd.Parameters.Add(new SqlParameter("@iCodFuenteFinanciamiento", idCodFuenteFinanciamiento));
                //}
                //if (sMetas != null && sMetas != string.Empty)
                //{
                //    cmd.Parameters.Add(new SqlParameter("@vMetas", sMetas));
                //}

                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculada_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            be.iCodPlanilla = dr.GetInt32(dr.GetOrdinal("iCodPlanilla"));
                            be.iCodDetPlanilla = dr.GetInt32(dr.GetOrdinal("iCodDetPlanilla"));
                            be.iCodTipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            be.sNombreTipoDocumento = dr.GetString(dr.GetOrdinal("vNombreTipoDocumento"));
                            be.sNroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            be.sNombres = dr.GetString(dr.GetOrdinal("vNombres"));
                            be.sApepPaterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            be.sApepMaterno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            be.sNombreCompleto = be.sApepPaterno + " " + be.sApepMaterno + " " + be.sNombres;
                            be.sCargo = dr.GetString(dr.GetOrdinal("vCargo"));
                            be.iCodPensionario = dr.GetInt32(dr.GetOrdinal("iCodRegimenPen"));
                            be.sNombreRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreRegimenPen"));
                            be.iCodTipoPensionario = dr.GetInt32(dr.GetOrdinal("iCodTipoRegimenPen"));
                            be.sNombreTipoRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreTipoRegimenPen"));
                            if (be.sNombreTipoRegimenPensionario != "")
                            {
                                be.sRegimenPensionario = be.sNombreRegimenPensionario + " - " + be.sNombreTipoRegimenPensionario;
                            }
                            else
                            {
                                be.sRegimenPensionario = be.sNombreRegimenPensionario;
                            }
                            be.bExoneracionRenta4ta = dr.GetBoolean(dr.GetOrdinal("bEstadoSuspencionRetencion"));
                            //be.sFechaInicio = dr.GetString(dr.GetOrdinal("dtInicioLabores"));
                            be.iDiasLaborados = dr.GetInt32(dr.GetOrdinal("iDiasLaboradosPorMes"));
                            be.iDiasVacaciones = dr.GetInt32(dr.GetOrdinal("iDiasVacaciones"));
                            be.iDiasLicencias = dr.GetInt32(dr.GetOrdinal("iDiasLicencia"));
                            be.iDiasDescansos_Subsidios = dr.GetInt32(dr.GetOrdinal("iDiasDescanso"));
                            be.dcMontoRemuneracionBasica = dr.GetDecimal(dr.GetOrdinal("dRemuneracion"));
                            be.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));
                            be.dcMontoTotalDescuentos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));
                            be.dcMontoTotalNeto = dr.GetDecimal(dr.GetOrdinal("dcMontoNeto"));
                            be.dcMontoAporteEsSalud = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion"));
                            be.dcMontoAporteEsSalud_675 = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion_675"));
                            be.dcMontoAporteEsSaludEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacionEPS"));
                            be.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            be.iAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaCalculadaIngresos_Request> ListarPlanillaCalculadaIngresosCASAdicional(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla, int iCodPlanilla, int iCodDetPlanilla)
        {
            List<PlanillaCalculadaIngresos_Request> lista = new List<PlanillaCalculadaIngresos_Request>();
            PlanillaCalculadaIngresos_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcADICIONALIngresosConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculadaIngresos_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.dcMontoContraprestacion = dr.GetDecimal(dr.GetOrdinal("dcMontoContraprestacion"));
                            be.dcMontoContraprescionVacaional = dr.GetDecimal(dr.GetOrdinal("dcMontoVacaciones"));
                            be.dcMontoReintegros = dr.GetDecimal(dr.GetOrdinal("dcMontoReintegro"));
                            be.dcMontoCopagoSubsidio = dr.GetDecimal(dr.GetOrdinal("dcMontoCoPagoSubsidio"));
                            be.dcMontoAguinaldos = dr.GetDecimal(dr.GetOrdinal("dcMontoAguinaldo"));
                            be.dcMontoPagoContratoAnterior = dr.GetDecimal(dr.GetOrdinal("dcMontoPagoContratoAnterior"));
                            be.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));
                            

                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaCalculadaDescuentos_Request> ListarPlanillaCalculadaDescuentosCASAdicional(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla, int iCodPlanilla, int iCodDetPlanilla)
        {
            List<PlanillaCalculadaDescuentos_Request> lista = new List<PlanillaCalculadaDescuentos_Request>();
            PlanillaCalculadaDescuentos_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcADICIONALDescuentosConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculadaDescuentos_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.dcMontoTardanzas = dr.GetDecimal(dr.GetOrdinal("dcMontoTardanzas"));
                            be.dcMontoInasistencias = dr.GetDecimal(dr.GetOrdinal("dcMontoInasistencias"));
                            be.dcMontoPermisos = dr.GetDecimal(dr.GetOrdinal("dcMontoPermisos"));
                            be.dcMontoLicenciaSinGoce = dr.GetDecimal(dr.GetOrdinal("dcMontoLicenciaSinGoce"));
                            be.dcMontoSalidasAnticipadas = dr.GetDecimal(dr.GetOrdinal("dcMontoSalidasAnticipadas"));
                            be.dcMontoImpuestoRenta4ta = dr.GetDecimal(dr.GetOrdinal("dcMontoRenta4ta"));
                            be.dcMontoImpuestoRenta4taAguinaldo = dr.GetDecimal(dr.GetOrdinal("dcMontoImpuestoRenta4taAguinaldo"));
                            be.dcMontoImpuestoRenta4taDsctoJudicialAguinaldo = dr.GetDecimal(dr.GetOrdinal("dcMontoImpuestoRenta4taDsctoJudicialAguinaldo"));
                            be.dcMontoONP = dr.GetDecimal(dr.GetOrdinal("dcMontoONP"));
                            be.dcMontoAporteOblAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoAporteAFP"));
                            be.dcMontoComisionAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoComisionAFP"));
                            be.dcMontoPrimaSegAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoPrimaAFP"));
                            be.dcMontoDescuentoJudicial = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoJudicial"));
                            be.dcMontoEsSaludMasVida = dr.GetDecimal(dr.GetOrdinal("dcMontoEsSaludMasVida"));
                            be.dcMontoRimacSeguro = dr.GetDecimal(dr.GetOrdinal("dcMontoRimacSeguro"));
                            be.dcMontoDsctoPagoExceso = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoPagoExceso"));
                            be.dcMontoEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoEPS"));
                            be.dcMontoTotalDescuento = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));

                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralCompletaCASAdicional(int iMes, int iAnio, int iCodPlanilla, int iCodDetPlanilla)
        {
            List<PlanillaCalculada_Request> lista = new List<PlanillaCalculada_Request>();
            PlanillaCalculada_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcCASAdicionalCompletaConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                //cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculada_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            be.iCodTipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            be.sNombreTipoDocumento = dr.GetString(dr.GetOrdinal("vNombreTipoDocumento"));
                            be.sNroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            be.sNombres = dr.GetString(dr.GetOrdinal("vNombres"));
                            be.sApepPaterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            be.sApepMaterno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            be.sNombreCompleto = be.sApepPaterno + " " + be.sApepMaterno + " " + be.sNombres;
                            be.sCargo = dr.GetString(dr.GetOrdinal("vCargo"));
                            be.iCodPensionario = dr.GetInt32(dr.GetOrdinal("iCodRegimenPen"));
                            be.sNombreRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreRegimenPen"));
                            be.iCodTipoPensionario = dr.GetInt32(dr.GetOrdinal("iCodTipoRegimenPen"));
                            be.sNombreTipoRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreTipoRegimenPen"));
                            if (be.sNombreTipoRegimenPensionario != "")
                            {
                                be.sRegimenPensionario = be.sNombreRegimenPensionario + " - " + be.sNombreTipoRegimenPensionario;
                            }
                            else
                            {
                                be.sRegimenPensionario = be.sNombreRegimenPensionario;
                            }
                            be.bExoneracionRenta4ta = dr.GetBoolean(dr.GetOrdinal("bEstadoSuspencionRetencion"));
                            //be.sFechaInicio = dr.GetString(dr.GetOrdinal("dtInicioLabores"));
                            be.iDiasLaborados = dr.GetInt32(dr.GetOrdinal("iDiasLaboradosPorMes"));
                            be.iDiasVacaciones = dr.GetInt32(dr.GetOrdinal("iDiasVacaciones"));
                            be.iDiasLicencias = dr.GetInt32(dr.GetOrdinal("iDiasLicencia"));
                            be.iDiasDescansos_Subsidios = dr.GetInt32(dr.GetOrdinal("iDiasDescanso"));
                            be.dcMontoRemuneracionBasica = dr.GetDecimal(dr.GetOrdinal("dRemuneracion"));
                            /*Ingresos*/
                            be.objPlanillaCalculadaIngresos_Request = new PlanillaCalculadaIngresos_Request();
                            be.objPlanillaCalculadaIngresos_Request.dcMontoContraprestacion = dr.GetDecimal(dr.GetOrdinal("dcMontoContraprestacion"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoContraprescionVacaional = dr.GetDecimal(dr.GetOrdinal("dcMontoVacaciones"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoReintegros = dr.GetDecimal(dr.GetOrdinal("dcMontoReintegro"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoCopagoSubsidio = dr.GetDecimal(dr.GetOrdinal("dcMontoCoPagoSubsidio"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoAguinaldos = dr.GetDecimal(dr.GetOrdinal("dcMontoAguinaldo"));
                            //be.objPlanillaCalculadaIngresos_Request.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));
                            be.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));
                            /*Ingresos*/
                            /*Descuentos*/
                            be.objPlanillaCalculadaDescuentos_Request = new PlanillaCalculadaDescuentos_Request();
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoTardanzas = dr.GetDecimal(dr.GetOrdinal("dcMontoTardanzas"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoInasistencias = dr.GetDecimal(dr.GetOrdinal("dcMontoInasistencias"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoPermisos = dr.GetDecimal(dr.GetOrdinal("dcMontoPermisos"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoSalidasAnticipadas = dr.GetDecimal(dr.GetOrdinal("dcMontoSalidasAnticipadas"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoImpuestoRenta4ta = dr.GetDecimal(dr.GetOrdinal("dcMontoRenta4ta"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoImpuestoRenta4taAguinaldo = dr.GetDecimal(dr.GetOrdinal("dcMontoImpuestoRenta4taAguinaldo"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoImpuestoRenta4taDsctoJudicialAguinaldo = dr.GetDecimal(dr.GetOrdinal("dcMontoImpuestoRenta4taDsctoJudicialAguinaldo"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoONP = dr.GetDecimal(dr.GetOrdinal("dcMontoONP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoAporteOblAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoAporteAFP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoComisionAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoComisionAFP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoPrimaSegAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoPrimaAFP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoDescuentoJudicial = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoJudicial"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoEsSaludMasVida = dr.GetDecimal(dr.GetOrdinal("dcMontoEsSaludMasVida"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoRimacSeguro = dr.GetDecimal(dr.GetOrdinal("dcMontoRimacSeguro"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoDsctoPagoExceso = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoPagoExceso"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoEPS"));
                            //be.dcMontoTotalDescuento = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));
                            be.dcMontoTotalDescuentos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));
                            /*Descuentos*/
                            be.dcMontoTotalNeto = dr.GetDecimal(dr.GetOrdinal("dcMontoNeto"));
                            be.dcMontoAporteEsSalud_675 = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion_675"));
                            be.dcMontoAporteEsSalud = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion"));
                            be.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            be.iAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        //public bool GenerarPlanillaCASAdicional(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, string strCodTrabajadores)
        //{
        //    bool rpta = false;
        //    using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
        //    {
        //        int id = 0;
        //        if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
        //        cmd.CommandText = "[dbo].[paPlanillaclcCASAdicional2]";
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        //cmd.CommandTimeout = cmd.CommandTimeout;
        //        cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
        //        cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
        //        cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
        //        cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
        //        cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
        //        cmd.Parameters.Add(new SqlParameter("@vCodTrabajadores", strCodTrabajadores));
        //        cmd.ExecuteNonQuery();
        //        rpta = true;
        //        if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
        //    }

        //    return rpta;
        //}

        public bool GenerarPlanillaCASAdicional(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, IEnumerable<String> strCodTrabajadores)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

                if (strCodTrabajadores != null)
                {
                    cmd.CommandText = "[dbo].[paPlanillaclcCASAdicional_Limpiar]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.CommandTimeout = cmd.CommandTimeout;
                    cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                    cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                    cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                    cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                    cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                    cmd.ExecuteNonQuery();
                    foreach (String item in strCodTrabajadores)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "[dbo].[paPlanillaclcCASAdicional2]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = cmd.CommandTimeout;
                        cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                        cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                        cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                        cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                        cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                        cmd.Parameters.Add(new SqlParameter("@iCodTrabajador_", item.Split('|')[0]));
                        cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanillaAdic", item.Split('|')[1]));
                        cmd.Parameters.Add(new SqlParameter("@dcMonto", item.Split('|')[2]));

                        cmd.ExecuteNonQuery();
                    }
                    rpta = true; 
                }
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        public IEnumerable<TipoPlanillaAdicional_Response> ListarTipoPlanillaCASAdicional()
        {
            List<TipoPlanillaAdicional_Response> lista = new List<TipoPlanillaAdicional_Response>();
            TipoPlanillaAdicional_Response be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaTipoPlanillaAdicional_listar]";
                cmd.CommandType = CommandType.StoredProcedure;

                
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new TipoPlanillaAdicional_Response();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.iCodTipoPlanillaAdicional = dr.GetInt32(dr.GetOrdinal("iCodTipoPlanillaAdicional"));
                            be.sNombreTipoPlanillaAdicional = dr.GetString(dr.GetOrdinal("vNombreTipoPlanillaAdicional"));
                            be.bEstado = dr.GetBoolean(dr.GetOrdinal("bEstado"));
                            
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<Empleado_Registro> ListarTrabajadoresPlanillaCASAdicional(int iCodPlanilla, int iMes, int iAnio, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            List<Empleado_Registro> lista = new List<Empleado_Registro>();
            Empleado_Registro be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaAdicionalTrabajadores_listar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new Empleado_Registro();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();                            
                            be.IdEmpleado = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            be.Paterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            be.Materno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            be.Nombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            be.IdTipoPlanillaAdicional = dr.GetInt32(dr.GetOrdinal("iCodTipoPlanillaAdicional"));
                            be.NombreTipoPlanillaAdicional = dr.GetString(dr.GetOrdinal("vNombreTipoPlanillaAdicional"));

                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        #endregion Planilla CAS Adicional

        /*------Planilla FUNC------*/
        #region Planilla Funcionarios
        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralFUNC(int iMes, int iAnio)
        {
            List<PlanillaCalculada_Request> lista = new List<PlanillaCalculada_Request>();
            PlanillaCalculada_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcFUNCGeneralConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //if (idCodFuenteFinanciamiento != 0)
                //{
                //    cmd.Parameters.Add(new SqlParameter("@iCodFuenteFinanciamiento", idCodFuenteFinanciamiento));
                //}
                //if (sMetas != null && sMetas != string.Empty)
                //{
                //    cmd.Parameters.Add(new SqlParameter("@vMetas", sMetas));
                //}

                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", 1));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculada_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            be.iCodTipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            be.sNombreTipoDocumento = dr.GetString(dr.GetOrdinal("vNombreTipoDocumento"));
                            be.sNroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            be.sNombres = dr.GetString(dr.GetOrdinal("vNombres"));
                            be.sApepPaterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            be.sApepMaterno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            be.sNombreCompleto = be.sApepPaterno + " " + be.sApepMaterno + " " + be.sNombres;
                            be.sCargo = dr.GetString(dr.GetOrdinal("vCargo"));
                            be.iCodPensionario = dr.GetInt32(dr.GetOrdinal("iCodRegimenPen"));
                            be.sNombreRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreRegimenPen"));
                            be.iCodTipoPensionario = dr.GetInt32(dr.GetOrdinal("iCodTipoRegimenPen"));
                            be.sNombreTipoRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreTipoRegimenPen"));
                            if (be.sNombreTipoRegimenPensionario != "")
                            {
                                be.sRegimenPensionario = be.sNombreRegimenPensionario + " - " + be.sNombreTipoRegimenPensionario;
                            }
                            else
                            {
                                be.sRegimenPensionario = be.sNombreRegimenPensionario;
                            }
                            //be.bExoneracionRenta4ta = dr.GetBoolean(dr.GetOrdinal("bEstadoSuspencionRetencion"));
                            //be.sFechaInicio = dr.GetString(dr.GetOrdinal("dtInicioLabores"));
                            be.iDiasLaborados = dr.GetInt32(dr.GetOrdinal("iDiasLaboradosPorMes"));
                            be.iDiasVacaciones = dr.GetInt32(dr.GetOrdinal("iDiasVacaciones"));
                            be.iDiasLicencias = dr.GetInt32(dr.GetOrdinal("iDiasLicencia"));
                            //be.iDiasDescansos_Subsidios = dr.GetInt32(dr.GetOrdinal("iDiasDescanso"));
                            be.dcMontoRemuneracionBasica = dr.GetDecimal(dr.GetOrdinal("dRemuneracion"));
                            be.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));
                            be.dcMontoTotalDescuentos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));
                            be.dcMontoTotalNeto = dr.GetDecimal(dr.GetOrdinal("dcMontoNeto"));
                            be.dcMontoAporteEsSalud = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion"));
                            be.dcMontoAporteEsSalud_675 = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion_675"));
                            be.dcMontoAporteEsSaludEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacionEPS"));
                            be.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            be.iAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            be.iCodDetPlanilla = 1;
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaCalculadaIngresos_Request> ListarPlanillaCalculadaIngresosFUNC(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla)
        {
            List<PlanillaCalculadaIngresos_Request> lista = new List<PlanillaCalculadaIngresos_Request>();
            PlanillaCalculadaIngresos_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcFUNCIngresosConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", 1));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculadaIngresos_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.dcMontoContraprestacion = dr.GetDecimal(dr.GetOrdinal("dcMontoContraprestacion"));
                            be.dcMontoContraprescionVacaional = dr.GetDecimal(dr.GetOrdinal("dcMontoVacaciones"));
                            be.dcMontoReintegros = dr.GetDecimal(dr.GetOrdinal("dcMontoReintegro"));
                            //be.dcMontoCopagoSubsidio = dr.GetDecimal(dr.GetOrdinal("dcMontoCoPagoSubsidio"));
                            be.dcMontoAguinaldos = dr.GetDecimal(dr.GetOrdinal("dcMontoAguinaldo"));
                            be.dcMontoCTS = dr.GetDecimal(dr.GetOrdinal("dcMontoCTS"));
                            be.dcMontoPagoContratoAnterior = dr.GetDecimal(dr.GetOrdinal("dcMontoPagoContratoAnterior"));
                            be.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));

                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaCalculadaDescuentos_Request> ListarPlanillaCalculadaDescuentosFUNC(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla)
        {
            List<PlanillaCalculadaDescuentos_Request> lista = new List<PlanillaCalculadaDescuentos_Request>();
            PlanillaCalculadaDescuentos_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcFUNCDescuentosConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", 1));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculadaDescuentos_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            //be.dcMontoTardanzas = dr.GetDecimal(dr.GetOrdinal("dcMontoTardanzas"));
                            //be.dcMontoInasistencias = dr.GetDecimal(dr.GetOrdinal("dcMontoInasistencias"));
                            be.dcMontoPermisos = dr.GetDecimal(dr.GetOrdinal("dcMontoPermisos"));
                            be.dcMontoLicenciaSinGoce = dr.GetDecimal(dr.GetOrdinal("dcMontoLicenciaSinGoce"));
                            be.dcMontoImpuestoRenta5ta = dr.GetDecimal(dr.GetOrdinal("dcMontoRenta5ta"));
                            be.dcMontoONP = dr.GetDecimal(dr.GetOrdinal("dcMontoONP"));
                            be.dcMontoAporteOblAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoAporteAFP"));
                            be.dcMontoComisionAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoComisionAFP"));
                            be.dcMontoPrimaSegAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoPrimaAFP"));
                            be.dcMontoDescuentoJudicial = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoJudicial"));
                            //be.dcMontoEsSaludMasVida = dr.GetDecimal(dr.GetOrdinal("dcMontoEsSaludMasVida"));
                            be.dcMontoRimacSeguro = dr.GetDecimal(dr.GetOrdinal("dcMontoRimacSeguro"));
                            be.dcMontoDsctoPagoExceso = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoPagoExceso"));
                            be.dcMontoEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoEPS"));
                            be.dcMontoTotalDescuento = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));

                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralCompletaFUNC(int iMes, int iAnio)
        {
            List<PlanillaCalculada_Request> lista = new List<PlanillaCalculada_Request>();
            PlanillaCalculada_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcFUNCCompletaConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //if (idCodFuenteFinanciamiento != 0)
                //{
                //    cmd.Parameters.Add(new SqlParameter("@iCodFuenteFinanciamiento", idCodFuenteFinanciamiento));
                //}
                //if (sMetas != null && sMetas != string.Empty)
                //{
                //    cmd.Parameters.Add(new SqlParameter("@vMetas", sMetas));
                //}

                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", 1));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculada_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            be.iCodTipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            be.sNombreTipoDocumento = dr.GetString(dr.GetOrdinal("vNombreTipoDocumento"));
                            be.sNroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            be.sNombres = dr.GetString(dr.GetOrdinal("vNombres"));
                            be.sApepPaterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            be.sApepMaterno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            be.sNombreCompleto = be.sApepPaterno + " " + be.sApepMaterno + " " + be.sNombres;
                            be.sCargo = dr.GetString(dr.GetOrdinal("vCargo"));
                            be.iCodPensionario = dr.GetInt32(dr.GetOrdinal("iCodRegimenPen"));
                            be.sNombreRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreRegimenPen"));
                            be.iCodTipoPensionario = dr.GetInt32(dr.GetOrdinal("iCodTipoRegimenPen"));
                            be.sNombreTipoRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreTipoRegimenPen"));
                            if (be.sNombreTipoRegimenPensionario != "")
                            {
                                be.sRegimenPensionario = be.sNombreRegimenPensionario + " - " + be.sNombreTipoRegimenPensionario;
                            }
                            else
                            {
                                be.sRegimenPensionario = be.sNombreRegimenPensionario;
                            }
                            //be.bExoneracionRenta4ta = dr.GetBoolean(dr.GetOrdinal("bEstadoSuspencionRetencion"));
                            //be.sFechaInicio = dr.GetString(dr.GetOrdinal("dtInicioLabores"));
                            be.iDiasLaborados = dr.GetInt32(dr.GetOrdinal("iDiasLaboradosPorMes"));
                            be.iDiasVacaciones = dr.GetInt32(dr.GetOrdinal("iDiasVacaciones"));
                            be.iDiasLicencias = dr.GetInt32(dr.GetOrdinal("iDiasLicencia"));
                            //be.iDiasDescansos_Subsidios = dr.GetInt32(dr.GetOrdinal("iDiasDescanso"));
                            be.dcMontoRemuneracionBasica = dr.GetDecimal(dr.GetOrdinal("dRemuneracion"));
                            /*Ingresos*/
                            be.objPlanillaCalculadaIngresos_Request = new PlanillaCalculadaIngresos_Request();
                            be.objPlanillaCalculadaIngresos_Request.dcMontoContraprestacion = dr.GetDecimal(dr.GetOrdinal("dcMontoContraprestacion"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoContraprescionVacaional = dr.GetDecimal(dr.GetOrdinal("dcMontoVacaciones"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoReintegros = dr.GetDecimal(dr.GetOrdinal("dcMontoReintegro"));
                            //be.objPlanillaCalculadaIngresos_Request.dcMontoCopagoSubsidio = dr.GetDecimal(dr.GetOrdinal("dcMontoCoPagoSubsidio"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoAguinaldos = dr.GetDecimal(dr.GetOrdinal("dcMontoAguinaldo"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoCTS = dr.GetDecimal(dr.GetOrdinal("dcMontoCTS"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoPagoContratoAnterior = dr.GetDecimal(dr.GetOrdinal("dcMontoPagoContratoAnterior"));
                            //be.objPlanillaCalculadaIngresos_Request.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));
                            be.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));
                            /*Ingresos*/
                            /*Descuentos*/
                            be.objPlanillaCalculadaDescuentos_Request = new PlanillaCalculadaDescuentos_Request();
                            //be.objPlanillaCalculadaDescuentos_Request.dcMontoTardanzas = dr.GetDecimal(dr.GetOrdinal("dcMontoTardanzas"));
                            //be.objPlanillaCalculadaDescuentos_Request.dcMontoInasistencias = dr.GetDecimal(dr.GetOrdinal("dcMontoInasistencias"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoPermisos = dr.GetDecimal(dr.GetOrdinal("dcMontoPermisos"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoLicenciaSinGoce = dr.GetDecimal(dr.GetOrdinal("dcMontoLicenciaSinGoce"));
                            //be.objPlanillaCalculadaDescuentos_Request.dcMontoImpuestoRenta4ta = dr.GetDecimal(dr.GetOrdinal("dcMontoRenta4ta"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoImpuestoRenta5ta = dr.GetDecimal(dr.GetOrdinal("dcMontoRenta5ta"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoONP = dr.GetDecimal(dr.GetOrdinal("dcMontoONP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoAporteOblAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoAporteAFP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoComisionAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoComisionAFP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoPrimaSegAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoPrimaAFP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoDescuentoJudicial = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoJudicial"));
                            //be.objPlanillaCalculadaDescuentos_Request.dcMontoEsSaludMasVida = dr.GetDecimal(dr.GetOrdinal("dcMontoEsSaludMasVida"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoRimacSeguro = dr.GetDecimal(dr.GetOrdinal("dcMontoRimacSeguro"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoDsctoPagoExceso = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoPagoExceso"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoEPS"));
                            //be.dcMontoTotalDescuento = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));
                            be.dcMontoTotalDescuentos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));
                            /*Descuentos*/
                            be.dcMontoTotalNeto = dr.GetDecimal(dr.GetOrdinal("dcMontoNeto"));
                            be.dcMontoAporteEsSalud = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion"));
                            be.dcMontoAporteEsSalud_675 = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion_675"));
                            be.dcMontoAporteEsSaludEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacionEPS"));
                            be.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            be.iAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            //be.sNombreFuenteFinanciamiento = dr.GetString(dr.GetOrdinal("vNombreTipoFuenteFinanciamiento"));
                            //be.sNombreMeta = dr.GetString(dr.GetOrdinal("vMeta"));
                            //be.sNombreFuenteFinanciamiento = "";
                            //be.sNombreMeta = "";
                            be.sNombreFuenteFinanciamiento = dr.GetString(dr.GetOrdinal("vNombreTipoFuenteFinanciamiento"));
                            be.sNombreMeta = dr.GetString(dr.GetOrdinal("vMeta"));
                            be.sBanco = dr.GetString(dr.GetOrdinal("vBanco"));
                            be.sCuenta = dr.GetString(dr.GetOrdinal("vCuenta"));
                            be.sCCI = dr.GetString(dr.GetOrdinal("vCCI"));
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public bool GenerarPlanillaFUNC(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcFUNCIONARIOS4]";
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

        #endregion Planilla Funcionarios

        public string ConsultarEjecucionPlanilla(int iCodPlanilla, int iMes, int iAnio, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            int iEstado = 0;
            bool? bExiste = null;
            string rpta = string.Empty;
            bool? bEstadoDsctoFijoVariable = null;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaEjecucionConsultar_2]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            bExiste = dr.GetBoolean(dr.GetOrdinal("bExiste"));
                            bEstadoDsctoFijoVariable = dr.GetBoolean(dr.GetOrdinal("bEstadoDsctoFijoVariable"));
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }
            if (bExiste != null)
            {
                if (bExiste == true)
                {
                    iEstado = 1;
                }
                else
                {
                    iEstado = 2;
                }
            }
            rpta = iEstado + "|" + Convert.ToInt16(bEstadoDsctoFijoVariable);
            return rpta;
        }

        public bool ConsultarGeneracionPlanilla(int iCodPlanilla, int iMes, int iAnio, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            bool bEstadoEjecutado = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaCASGeneradaConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            bEstadoEjecutado = dr.GetBoolean(dr.GetOrdinal("bEstadoEjecutado"));
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }
            return bEstadoEjecutado;
        }

        public IEnumerable<Empleado_Registro> ListarEmpleadosPlanilla(string strCodTipoCondicionTrabajador)
        {
            List<Empleado_Registro> lista = new List<Empleado_Registro>();
            Empleado_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePlanillaListaTrabajador]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;


                cmd.Parameters.Add(new SqlParameter("@vCodTipoCondicionTrabajador", strCodTipoCondicionTrabajador));


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Empleado_Registro();

                            item.Grilla = new Grilla_Response();
                            item.IdEmpleado = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            //item.IdDependencia = dr.GetInt32(dr.GetOrdinal("iCodigoDependencia"));
                            item.TipoDocumento = dr.GetString(dr.GetOrdinal("vNombreTipoDocumento"));
                            item.NroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            item.Paterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            item.Materno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            //item.CorreoElectronicoLaboral = dr.GetString(dr.GetOrdinal("vCorreoElectronico"));
                            item.NombreOficina = dr.GetString(dr.GetOrdinal("vDependencia"));
                            item.Sigla = dr.GetString(dr.GetOrdinal("vSiglas"));
                            //item.NombreCargo = dr.GetString(dr.GetOrdinal("vDescripcion"));
                            item.Estado = dr.GetInt32(dr.GetOrdinal("bEstado"));
                            item.FED = dr.GetBoolean(dr.GetOrdinal("bFED"));
                            //item.IdGenero = dr.GetInt32(dr.GetOrdinal("iCodigoGenero"));
                            //item.Domicilio = dr.GetString(dr.GetOrdinal("Domicilio"));
                            //item.Telefono = dr.GetString(dr.GetOrdinal("Telefono"));
                            //item.Celular = dr.GetString(dr.GetOrdinal("Celular"));
                            //item.CorreoElectronico = dr.GetString(dr.GetOrdinal("CorreoElectronico"));
                            //item.IdCondicion = dr.GetInt32(dr.GetOrdinal("iCodigoCondicion"));
                            //item.CondicionLaboral = dr.GetString(dr.GetOrdinal("CondicionLaboral"));
                            //item.IdSede = dr.GetInt32(dr.GetOrdinal("iCodigoSede"));
                            //item.Sede = dr.GetString(dr.GetOrdinal("Sede"));

                            //item.NroOrden = dr.GetString(dr.GetOrdinal("vNroOrden"));
                            //item.NombreOrden = dr.GetString(dr.GetOrdinal("vNombreOrden"));
                            //item.DuracionOrden = dr.GetInt32(dr.GetOrdinal("iDuracionOrden"));
                            //item.MontoOrden = dr.GetDecimal(dr.GetOrdinal("dMontoOrden"));
                            //if (!dr.IsDBNull(dr.GetOrdinal("dtInicioLabores"))) item.FechaInicio = dr.GetDateTime(dr.GetOrdinal("dtInicioLabores"));


                            //if (!dr.IsDBNull(dr.GetOrdinal("dFechaInicioOrden"))) item.InicioOrden = dr.GetDateTime(dr.GetOrdinal("dFechaInicioOrden")).ToString("dd/MM/yyyy");
                            //if (!dr.IsDBNull(dr.GetOrdinal("dFechaFinOrden"))) item.FinOrden = dr.GetDateTime(dr.GetOrdinal("dFechaFinOrden")).ToString("dd/MM/yyyy");

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public int CerrarFase(int iCodPlanilla, int iMes, int iAnio, int iCodTipoPlanilla, int iCodDetPlanilla, int iCodFase, string sUsuario)
        {
            int iEstado = 0;
            
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaEjecucion_CerrarFase]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodFase", iCodFase));
                cmd.Parameters.Add(new SqlParameter("@vUsuario", sUsuario)); 
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            iEstado = dr.GetInt32(dr.GetOrdinal("Rpta"));
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }            
            return iEstado;
        }

        /*------Planilla Funcional Adicional------*/
        #region Planilla Funcional Adicional
        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralFUNCAdicional(int idCodFuenteFinanciamiento, string sMetas, int iMes, int iAnio, int iCodDetPlanilla)
        {
            List<PlanillaCalculada_Request> lista = new List<PlanillaCalculada_Request>();
            PlanillaCalculada_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcFUNCAdicionalConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //if (idCodFuenteFinanciamiento != 0)
                //{
                //    cmd.Parameters.Add(new SqlParameter("@iCodFuenteFinanciamiento", idCodFuenteFinanciamiento));
                //}
                //if (sMetas != null && sMetas != string.Empty)
                //{
                //    cmd.Parameters.Add(new SqlParameter("@vMetas", sMetas));
                //}

                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));

                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculada_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            be.iCodPlanilla = dr.GetInt32(dr.GetOrdinal("iCodPlanilla"));
                            be.iCodDetPlanilla = dr.GetInt32(dr.GetOrdinal("iCodDetPlanilla"));
                            be.iCodTipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            be.sNombreTipoDocumento = dr.GetString(dr.GetOrdinal("vNombreTipoDocumento"));
                            be.sNroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            be.sNombres = dr.GetString(dr.GetOrdinal("vNombres"));
                            be.sApepPaterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            be.sApepMaterno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            be.sNombreCompleto = be.sApepPaterno + " " + be.sApepMaterno + " " + be.sNombres;
                            be.sCargo = dr.GetString(dr.GetOrdinal("vCargo"));
                            be.iCodPensionario = dr.GetInt32(dr.GetOrdinal("iCodRegimenPen"));
                            be.sNombreRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreRegimenPen"));
                            be.iCodTipoPensionario = dr.GetInt32(dr.GetOrdinal("iCodTipoRegimenPen"));
                            be.sNombreTipoRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreTipoRegimenPen"));
                            if (be.sNombreTipoRegimenPensionario != "")
                            {
                                be.sRegimenPensionario = be.sNombreRegimenPensionario + " - " + be.sNombreTipoRegimenPensionario;
                            }
                            else
                            {
                                be.sRegimenPensionario = be.sNombreRegimenPensionario;
                            }
                            //be.bExoneracionRenta4ta = dr.GetBoolean(dr.GetOrdinal("bEstadoSuspencionRetencion"));
                            //be.sFechaInicio = dr.GetString(dr.GetOrdinal("dtInicioLabores"));
                            be.iDiasLaborados = dr.GetInt32(dr.GetOrdinal("iDiasLaboradosPorMes"));
                            be.iDiasVacaciones = dr.GetInt32(dr.GetOrdinal("iDiasVacaciones"));
                            be.iDiasLicencias = dr.GetInt32(dr.GetOrdinal("iDiasLicencia"));
                            //be.iDiasDescansos_Subsidios = dr.GetInt32(dr.GetOrdinal("iDiasDescanso"));
                            be.dcMontoRemuneracionBasica = dr.GetDecimal(dr.GetOrdinal("dRemuneracion"));
                            be.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));
                            be.dcMontoTotalDescuentos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));
                            be.dcMontoTotalNeto = dr.GetDecimal(dr.GetOrdinal("dcMontoNeto"));
                            be.dcMontoAporteEsSalud = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion"));
                            be.dcMontoAporteEsSalud_675 = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion_675"));
                            be.dcMontoAporteEsSaludEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacionEPS"));
                            be.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            be.iAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaCalculadaIngresos_Request> ListarPlanillaCalculadaIngresosFUNCAdicional(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            List<PlanillaCalculadaIngresos_Request> lista = new List<PlanillaCalculadaIngresos_Request>();
            PlanillaCalculadaIngresos_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcFUNCIngresosConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculadaIngresos_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.dcMontoContraprestacion = dr.GetDecimal(dr.GetOrdinal("dcMontoContraprestacion"));
                            be.dcMontoContraprescionVacaional = dr.GetDecimal(dr.GetOrdinal("dcMontoVacaciones"));
                            be.dcMontoReintegros = dr.GetDecimal(dr.GetOrdinal("dcMontoReintegro"));
                            //be.dcMontoCopagoSubsidio = dr.GetDecimal(dr.GetOrdinal("dcMontoCoPagoSubsidio"));
                            be.dcMontoAguinaldos = dr.GetDecimal(dr.GetOrdinal("dcMontoAguinaldo"));
                            be.dcMontoCTS = dr.GetDecimal(dr.GetOrdinal("dcMontoCTS"));
                            be.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));

                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaCalculadaDescuentos_Request> ListarPlanillaCalculadaDescuentosFUNCAdicional(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            List<PlanillaCalculadaDescuentos_Request> lista = new List<PlanillaCalculadaDescuentos_Request>();
            PlanillaCalculadaDescuentos_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcFUNCDescuentosConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculadaDescuentos_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            //be.dcMontoTardanzas = dr.GetDecimal(dr.GetOrdinal("dcMontoTardanzas"));
                            //be.dcMontoInasistencias = dr.GetDecimal(dr.GetOrdinal("dcMontoInasistencias"));
                            be.dcMontoPermisos = dr.GetDecimal(dr.GetOrdinal("dcMontoPermisos"));
                            be.dcMontoLicenciaSinGoce = dr.GetDecimal(dr.GetOrdinal("dcMontoLicenciaSinGoce"));
                            be.dcMontoImpuestoRenta5ta = dr.GetDecimal(dr.GetOrdinal("dcMontoRenta5ta"));
                            be.dcMontoONP = dr.GetDecimal(dr.GetOrdinal("dcMontoONP"));
                            be.dcMontoAporteOblAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoAporteAFP"));
                            be.dcMontoComisionAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoComisionAFP"));
                            be.dcMontoPrimaSegAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoPrimaAFP"));
                            be.dcMontoDescuentoJudicial = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoJudicial"));
                            //be.dcMontoEsSaludMasVida = dr.GetDecimal(dr.GetOrdinal("dcMontoEsSaludMasVida"));
                            be.dcMontoRimacSeguro = dr.GetDecimal(dr.GetOrdinal("dcMontoRimacSeguro"));
                            be.dcMontoDsctoPagoExceso = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoPagoExceso"));
                            be.dcMontoEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoEPS"));
                            be.dcMontoTotalDescuento = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));

                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralCompletaFUNCAdicional(int iMes, int iAnio, int iCodDetPlanilla)
        {
            List<PlanillaCalculada_Request> lista = new List<PlanillaCalculada_Request>();
            PlanillaCalculada_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcFUNCAdicionalCompletaConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculada_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            be.iCodTipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            be.sNombreTipoDocumento = dr.GetString(dr.GetOrdinal("vNombreTipoDocumento"));
                            be.sNroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            be.sNombres = dr.GetString(dr.GetOrdinal("vNombres"));
                            be.sApepPaterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            be.sApepMaterno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            be.sNombreCompleto = be.sApepPaterno + " " + be.sApepMaterno + " " + be.sNombres;
                            be.sCargo = dr.GetString(dr.GetOrdinal("vCargo"));
                            be.iCodPensionario = dr.GetInt32(dr.GetOrdinal("iCodRegimenPen"));
                            be.sNombreRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreRegimenPen"));
                            be.iCodTipoPensionario = dr.GetInt32(dr.GetOrdinal("iCodTipoRegimenPen"));
                            be.sNombreTipoRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreTipoRegimenPen"));
                            if (be.sNombreTipoRegimenPensionario != "")
                            {
                                be.sRegimenPensionario = be.sNombreRegimenPensionario + " - " + be.sNombreTipoRegimenPensionario;
                            }
                            else
                            {
                                be.sRegimenPensionario = be.sNombreRegimenPensionario;
                            }
                            //be.bExoneracionRenta4ta = dr.GetBoolean(dr.GetOrdinal("bEstadoSuspencionRetencion"));
                            //be.sFechaInicio = dr.GetString(dr.GetOrdinal("dtInicioLabores"));
                            be.iDiasLaborados = dr.GetInt32(dr.GetOrdinal("iDiasLaboradosPorMes"));
                            be.iDiasVacaciones = dr.GetInt32(dr.GetOrdinal("iDiasVacaciones"));
                            be.iDiasLicencias = dr.GetInt32(dr.GetOrdinal("iDiasLicencia"));
                            //be.iDiasDescansos_Subsidios = dr.GetInt32(dr.GetOrdinal("iDiasDescanso"));
                            be.dcMontoRemuneracionBasica = dr.GetDecimal(dr.GetOrdinal("dRemuneracion"));
                            /*Ingresos*/
                            be.objPlanillaCalculadaIngresos_Request = new PlanillaCalculadaIngresos_Request();
                            be.objPlanillaCalculadaIngresos_Request.dcMontoContraprestacion = dr.GetDecimal(dr.GetOrdinal("dcMontoContraprestacion"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoContraprescionVacaional = dr.GetDecimal(dr.GetOrdinal("dcMontoVacaciones"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoReintegros = dr.GetDecimal(dr.GetOrdinal("dcMontoReintegro"));
                            //be.objPlanillaCalculadaIngresos_Request.dcMontoCopagoSubsidio = dr.GetDecimal(dr.GetOrdinal("dcMontoCoPagoSubsidio"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoAguinaldos = dr.GetDecimal(dr.GetOrdinal("dcMontoAguinaldo"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoCTS = dr.GetDecimal(dr.GetOrdinal("dcMontoCTS"));                            
                            //be.objPlanillaCalculadaIngresos_Request.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));
                            be.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));
                            /*Ingresos*/
                            /*Descuentos*/
                            be.objPlanillaCalculadaDescuentos_Request = new PlanillaCalculadaDescuentos_Request();
                            //be.objPlanillaCalculadaDescuentos_Request.dcMontoTardanzas = dr.GetDecimal(dr.GetOrdinal("dcMontoTardanzas"));
                            //be.objPlanillaCalculadaDescuentos_Request.dcMontoInasistencias = dr.GetDecimal(dr.GetOrdinal("dcMontoInasistencias"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoPermisos = dr.GetDecimal(dr.GetOrdinal("dcMontoPermisos"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoLicenciaSinGoce = dr.GetDecimal(dr.GetOrdinal("dcMontoLicenciaSinGoce"));
                            //be.objPlanillaCalculadaDescuentos_Request.dcMontoImpuestoRenta4ta = dr.GetDecimal(dr.GetOrdinal("dcMontoRenta4ta"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoImpuestoRenta5ta = dr.GetDecimal(dr.GetOrdinal("dcMontoRenta5ta"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoONP = dr.GetDecimal(dr.GetOrdinal("dcMontoONP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoAporteOblAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoAporteAFP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoComisionAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoComisionAFP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoPrimaSegAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoPrimaAFP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoDescuentoJudicial = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoJudicial"));
                            //be.objPlanillaCalculadaDescuentos_Request.dcMontoEsSaludMasVida = dr.GetDecimal(dr.GetOrdinal("dcMontoEsSaludMasVida"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoRimacSeguro = dr.GetDecimal(dr.GetOrdinal("dcMontoRimacSeguro"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoDsctoPagoExceso = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoPagoExceso"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoEPS"));
                            //be.dcMontoTotalDescuento = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));
                            be.dcMontoTotalDescuentos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));
                            /*Descuentos*/
                            be.dcMontoTotalNeto = dr.GetDecimal(dr.GetOrdinal("dcMontoNeto"));
                            be.dcMontoAporteEsSalud = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion"));
                            be.dcMontoAporteEsSalud_675 = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion_675"));
                            be.dcMontoAporteEsSaludEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacionEPS"));
                            be.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            be.iAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));

                            be.sNombreFuenteFinanciamiento = dr.GetString(dr.GetOrdinal("vNombreTipoFuenteFinanciamiento"));
                            be.sNombreMeta = dr.GetString(dr.GetOrdinal("vMeta"));
                            be.sBanco = dr.GetString(dr.GetOrdinal("vBanco"));
                            be.sCuenta = dr.GetString(dr.GetOrdinal("vCuenta"));
                            be.sCCI = dr.GetString(dr.GetOrdinal("vCCI"));
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public bool GenerarPlanillaFUNCAdicional(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, IEnumerable<String> strCodTrabajadores)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

                if (strCodTrabajadores != null)
                {
                    cmd.CommandText = "[dbo].[paPlanillaclcFUNCIONARIOSAdicional_Limpiar]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.CommandTimeout = cmd.CommandTimeout;
                    cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                    cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                    cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                    cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                    cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                    cmd.ExecuteNonQuery();
                    foreach (String item in strCodTrabajadores)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "[dbo].[paPlanillaclcFUNCIONARIOSAdicional]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandTimeout = cmd.CommandTimeout;
                        cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                        cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                        cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                        cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                        cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                        cmd.Parameters.Add(new SqlParameter("@iCodTrabajador_", item.Split('|')[0]));
                        cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanillaAdic", item.Split('|')[1]));
                        cmd.Parameters.Add(new SqlParameter("@dcMonto", item.Split('|')[2]));

                        cmd.ExecuteNonQuery();
                    }
                    rpta = true;
                    //if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                    //cmd.CommandText = "[dbo].[paPlanillaclcFUNCIONARIOSAdicional]";
                    //cmd.CommandType = CommandType.StoredProcedure;
                    ////cmd.CommandTimeout = cmd.CommandTimeout;
                    //cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                    //cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                    //cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                    //cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                    //cmd.Parameters.Add(new SqlParameter("@vCodTrabajadores", strCodTrabajadores));
                    //cmd.ExecuteNonQuery();
                    //rpta = true; 
                }
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        public IEnumerable<TipoPlanillaAdicional_Response> ListarTipoPlanillaFUNCAdicional()
        {
            List<TipoPlanillaAdicional_Response> lista = new List<TipoPlanillaAdicional_Response>();
            TipoPlanillaAdicional_Response be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaTipoPlanillaAdicional_listar]";
                cmd.CommandType = CommandType.StoredProcedure;


                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new TipoPlanillaAdicional_Response();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.iCodTipoPlanillaAdicional = dr.GetInt32(dr.GetOrdinal("iCodTipoPlanillaAdicional"));
                            be.sNombreTipoPlanillaAdicional = dr.GetString(dr.GetOrdinal("vNombreTipoPlanillaAdicional"));
                            be.bEstado = dr.GetBoolean(dr.GetOrdinal("bEstado"));

                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<Empleado_Registro> ListarTrabajadoresPlanillaFUNCAdicional(int iCodPlanilla, int iMes, int iAnio, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            List<Empleado_Registro> lista = new List<Empleado_Registro>();
            Empleado_Registro be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaAdicionalTrabajadores_listar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new Empleado_Registro();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();
                            be.IdEmpleado = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            be.Paterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            be.Materno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            be.Nombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            be.IdTipoPlanillaAdicional = dr.GetInt32(dr.GetOrdinal("iCodTipoPlanillaAdicional"));
                            be.NombreTipoPlanillaAdicional = dr.GetString(dr.GetOrdinal("vNombreTipoPlanillaAdicional"));

                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        
        #endregion Planilla Funcional Adicional

        /*------Planilla CAS Vacaciones Truncas------*/
        #region Planilla CAS Vacaciones Truncas
        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralCASVacTruncas(int idCodFuenteFinanciamiento, string sMetas, int iMes, int iAnio, int iCodDetPlanilla)
        {
            List<PlanillaCalculada_Request> lista = new List<PlanillaCalculada_Request>();
            PlanillaCalculada_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcCASVacTruncasConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //if (idCodFuenteFinanciamiento != 0)
                //{
                //    cmd.Parameters.Add(new SqlParameter("@iCodFuenteFinanciamiento", idCodFuenteFinanciamiento));
                //}
                //if (sMetas != null && sMetas != string.Empty)
                //{
                //    cmd.Parameters.Add(new SqlParameter("@vMetas", sMetas));
                //}

                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", 3));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculada_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            be.iCodTipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            be.sNombreTipoDocumento = dr.GetString(dr.GetOrdinal("vNombreTipoDocumento"));
                            be.sNroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            be.sNombres = dr.GetString(dr.GetOrdinal("vNombres"));
                            be.sApepPaterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            be.sApepMaterno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            be.sNombreCompleto = be.sApepPaterno + " " + be.sApepMaterno + " " + be.sNombres;
                            be.sCargo = dr.GetString(dr.GetOrdinal("vCargo"));
                            be.iCodPensionario = dr.GetInt32(dr.GetOrdinal("iCodRegimenPen"));
                            be.sNombreRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreRegimenPen"));
                            be.iCodTipoPensionario = dr.GetInt32(dr.GetOrdinal("iCodTipoRegimenPen"));
                            be.sNombreTipoRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreTipoRegimenPen"));
                            if (be.sNombreTipoRegimenPensionario != "")
                            {
                                be.sRegimenPensionario = be.sNombreRegimenPensionario + " - " + be.sNombreTipoRegimenPensionario;
                            }
                            else
                            {
                                be.sRegimenPensionario = be.sNombreRegimenPensionario;
                            }
                            be.bExoneracionRenta4ta = dr.GetBoolean(dr.GetOrdinal("bEstadoSuspencionRetencion"));
                            be.sFechaInicio = dr.GetString(dr.GetOrdinal("dtInicioLabores"));
                            be.sFechaFin = dr.GetString(dr.GetOrdinal("dtFinLabores"));
                            //be.iDiasLaborados = dr.GetInt32(dr.GetOrdinal("iDiasLaboradosPorMes"));
                            //be.iDiasVacaciones = dr.GetInt32(dr.GetOrdinal("iDiasVacaciones"));
                            //be.iDiasLicencias = dr.GetInt32(dr.GetOrdinal("iDiasLicencia"));
                            //be.iDiasDescansos_Subsidios = dr.GetInt32(dr.GetOrdinal("iDiasDescanso"));
                            be.dcMontoRemuneracionBasica = dr.GetDecimal(dr.GetOrdinal("dRemuneracion"));
                            be.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));
                            be.dcMontoTotalDescuentos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));
                            be.dcMontoTotalNeto = dr.GetDecimal(dr.GetOrdinal("dcMontoNeto"));
                            be.dcMontoAporteEsSalud = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion"));
                            be.dcMontoAporteEsSalud_675 = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion_675"));
                            be.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            be.iAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            be.iCodDetPlanilla = dr.GetInt32(dr.GetOrdinal("iCodDetPlanilla"));
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaCalculadaIngresos_Request> ListarPlanillaCalculadaIngresosCASVacTruncas(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            List<PlanillaCalculadaIngresos_Request> lista = new List<PlanillaCalculadaIngresos_Request>();
            PlanillaCalculadaIngresos_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcCASVacTruncasIngresosConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", 3));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculadaIngresos_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.dcMontoContraprestacion = dr.GetDecimal(dr.GetOrdinal("dcMontoContraprestacion"));
                            //be.dcMontoContraprescionVacaional = dr.GetDecimal(dr.GetOrdinal("dcMontoVacaciones"));
                            be.dcMontoReintegros = dr.GetDecimal(dr.GetOrdinal("dcMontoReintegro"));
                            //be.dcMontoCopagoSubsidio = dr.GetDecimal(dr.GetOrdinal("dcMontoCoPagoSubsidio"));
                            be.dcMontoAguinaldos = dr.GetDecimal(dr.GetOrdinal("dcMontoAguinaldo"));
                            be.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));

                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaCalculadaDescuentos_Request> ListarPlanillaCalculadaDescuentosCASVacTruncas(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            List<PlanillaCalculadaDescuentos_Request> lista = new List<PlanillaCalculadaDescuentos_Request>();
            PlanillaCalculadaDescuentos_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcCASVacTruncasDescuentosConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", 3));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculadaDescuentos_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.dcMontoTardanzas = dr.GetDecimal(dr.GetOrdinal("dcMontoTardanzas"));
                            be.dcMontoInasistencias = dr.GetDecimal(dr.GetOrdinal("dcMontoInasistencias"));
                            be.dcMontoPermisos = dr.GetDecimal(dr.GetOrdinal("dcMontoPermisos"));
                            be.dcMontoImpuestoRenta4ta = dr.GetDecimal(dr.GetOrdinal("dcMontoRenta4ta"));
                            be.dcMontoONP = dr.GetDecimal(dr.GetOrdinal("dcMontoONP"));
                            be.dcMontoAporteOblAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoAporteAFP"));
                            be.dcMontoComisionAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoComisionAFP"));
                            be.dcMontoPrimaSegAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoPrimaAFP"));
                            be.dcMontoDescuentoJudicial = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoJudicial"));
                            be.dcMontoEsSaludMasVida = dr.GetDecimal(dr.GetOrdinal("dcMontoEsSaludMasVida"));
                            be.dcMontoRimacSeguro = dr.GetDecimal(dr.GetOrdinal("dcMontoRimacSeguro"));
                            be.dcMontoDsctoPagoExceso = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoPagoExceso"));
                            be.dcMontoEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoEPS"));
                            be.dcMontoTotalDescuento = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));

                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralCompletaCASVacTruncas(int iMes, int iAnio, int iCodDetPlanilla)
        {
            List<PlanillaCalculada_Request> lista = new List<PlanillaCalculada_Request>();
            PlanillaCalculada_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcCASVacTruncasGeneralCompletaConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", 3));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculada_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            be.iCodTipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            be.sNombreTipoDocumento = dr.GetString(dr.GetOrdinal("vNombreTipoDocumento"));
                            be.sNroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            be.sNombres = dr.GetString(dr.GetOrdinal("vNombres"));
                            be.sApepPaterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            be.sApepMaterno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            be.sNombreCompleto = be.sApepPaterno + " " + be.sApepMaterno + " " + be.sNombres;
                            be.sCargo = dr.GetString(dr.GetOrdinal("vCargo"));
                            be.iCodPensionario = dr.GetInt32(dr.GetOrdinal("iCodRegimenPen"));
                            be.sNombreRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreRegimenPen"));
                            be.iCodTipoPensionario = dr.GetInt32(dr.GetOrdinal("iCodTipoRegimenPen"));
                            be.sNombreTipoRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreTipoRegimenPen"));
                            if (be.sNombreTipoRegimenPensionario != "")
                            {
                                be.sRegimenPensionario = be.sNombreRegimenPensionario + " - " + be.sNombreTipoRegimenPensionario;
                            }
                            else
                            {
                                be.sRegimenPensionario = be.sNombreRegimenPensionario;
                            }
                            be.bExoneracionRenta4ta = dr.GetBoolean(dr.GetOrdinal("bEstadoSuspencionRetencion"));
                            be.sFechaInicio = dr.GetString(dr.GetOrdinal("dtInicioLabores"));
                            be.sFechaFin = dr.GetString(dr.GetOrdinal("dtFinLabores"));
                            //be.iDiasLaborados = dr.GetInt32(dr.GetOrdinal("iDiasLaboradosPorMes"));
                            //be.iDiasVacaciones = dr.GetInt32(dr.GetOrdinal("iDiasVacaciones"));
                            //be.iDiasLicencias = dr.GetInt32(dr.GetOrdinal("iDiasLicencia"));
                            //be.iDiasDescansos_Subsidios = dr.GetInt32(dr.GetOrdinal("iDiasDescanso"));
                            be.dcMontoRemuneracionBasica = dr.GetDecimal(dr.GetOrdinal("dRemuneracion"));
                            /*Ingresos*/
                            be.objPlanillaCalculadaIngresos_Request = new PlanillaCalculadaIngresos_Request();
                            be.objPlanillaCalculadaIngresos_Request.dcMontoContraprestacion = dr.GetDecimal(dr.GetOrdinal("dcMontoContraprestacion"));
                            //be.objPlanillaCalculadaIngresos_Request.dcMontoContraprescionVacaional = dr.GetDecimal(dr.GetOrdinal("dcMontoVacaciones"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoReintegros = dr.GetDecimal(dr.GetOrdinal("dcMontoReintegro"));
                            //be.objPlanillaCalculadaIngresos_Request.dcMontoCopagoSubsidio = dr.GetDecimal(dr.GetOrdinal("dcMontoCoPagoSubsidio"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoAguinaldos = dr.GetDecimal(dr.GetOrdinal("dcMontoAguinaldo"));
                            //be.objPlanillaCalculadaIngresos_Request.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));
                            be.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));
                            /*Ingresos*/
                            /*Descuentos*/
                            be.objPlanillaCalculadaDescuentos_Request = new PlanillaCalculadaDescuentos_Request();
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoTardanzas = dr.GetDecimal(dr.GetOrdinal("dcMontoTardanzas"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoInasistencias = dr.GetDecimal(dr.GetOrdinal("dcMontoInasistencias"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoPermisos = dr.GetDecimal(dr.GetOrdinal("dcMontoPermisos"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoImpuestoRenta4ta = dr.GetDecimal(dr.GetOrdinal("dcMontoRenta4ta"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoONP = dr.GetDecimal(dr.GetOrdinal("dcMontoONP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoAporteOblAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoAporteAFP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoComisionAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoComisionAFP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoPrimaSegAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoPrimaAFP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoDescuentoJudicial = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoJudicial"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoEsSaludMasVida = dr.GetDecimal(dr.GetOrdinal("dcMontoEsSaludMasVida"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoRimacSeguro = dr.GetDecimal(dr.GetOrdinal("dcMontoRimacSeguro"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoDsctoPagoExceso = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoPagoExceso"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoEPS"));
                            //be.dcMontoTotalDescuento = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));
                            be.dcMontoTotalDescuentos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));
                            /*Descuentos*/
                            be.dcMontoTotalNeto = dr.GetDecimal(dr.GetOrdinal("dcMontoNeto"));
                            be.dcMontoAporteEsSalud = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion"));
                            be.dcMontoAporteEsSalud_675 = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion_675"));
                            be.dcMontoAporteEsSaludEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacionEPS"));
                            be.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            be.iAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public bool GenerarPlanillaCASVacTruncas(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, List<Empleado_Registro> lstEmpleados)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

                cmd.CommandText = "[dbo].[paPlanillaLiquidacionTrabajadorEliminar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                cmd.ExecuteNonQuery();

                if (lstEmpleados != null && lstEmpleados.Count > 0)
                {
                    foreach (var item in lstEmpleados)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "[dbo].[paPlanillaLiquidacionTrabajadorRegistrar]";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", item.IdEmpleado));
                        cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                        cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                        cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                        cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                        cmd.Parameters.Add(new SqlParameter("@iDiasDescansoFisico", item.DiasDescansoFisico));
                        cmd.Parameters.Add(new SqlParameter("@iMesesDescansoFisico", item.MesesDescansoFisico));
                        cmd.Parameters.Add(new SqlParameter("@iDiasVacTruncas", item.DiasVacacionesTruncas));
                        cmd.Parameters.Add(new SqlParameter("@iMesesVacTruncas", item.MesesVacacionesTruncas));
                        cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                        cmd.ExecuteNonQuery();
                    }
                }
                cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[paPlanillaclcVACTRUNCAS2]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@vCodTrabajadores", strCodTrabajadores));
                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        public IEnumerable<Empleado_Registro> ListarTrabajadoresPlanillaVacTruncas(int iCodPlanilla, int iMes, int iAnio, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            List<Empleado_Registro> lista = new List<Empleado_Registro>();
            Empleado_Registro be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaAdicionalTrabajadores_listar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new Empleado_Registro();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();
                            be.IdEmpleado = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            be.Paterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            be.Materno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            be.Nombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            be.DiasDescansoFisico = 0;
                            be.MesesDescansoFisico = 0;
                            be.DiasVacacionesTruncas = 0;
                            be.MesesVacacionesTruncas = 0;
                            //be.IdTipoPlanillaAdicional = dr.GetInt32(dr.GetOrdinal("iCodTipoPlanillaAdicional"));
                            //be.NombreTipoPlanillaAdicional = dr.GetString(dr.GetOrdinal("vNombreTipoPlanillaAdicional"));

                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        

        #endregion Planilla CAS Vacaciones Truncas

        /*------Planilla FED Vacaciones Truncas------*/
        #region Planilla FED Vacaciones Truncas
        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralFEDVacTruncas(int idCodFuenteFinanciamiento, string sMetas, int iMes, int iAnio, int iCodDetPlanilla)
        {
            List<PlanillaCalculada_Request> lista = new List<PlanillaCalculada_Request>();
            PlanillaCalculada_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcCASVacTruncasConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //if (idCodFuenteFinanciamiento != 0)
                //{
                //    cmd.Parameters.Add(new SqlParameter("@iCodFuenteFinanciamiento", idCodFuenteFinanciamiento));
                //}
                //if (sMetas != null && sMetas != string.Empty)
                //{
                //    cmd.Parameters.Add(new SqlParameter("@vMetas", sMetas));
                //}

                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", 4));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculada_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            be.iCodTipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            be.sNombreTipoDocumento = dr.GetString(dr.GetOrdinal("vNombreTipoDocumento"));
                            be.sNroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            be.sNombres = dr.GetString(dr.GetOrdinal("vNombres"));
                            be.sApepPaterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            be.sApepMaterno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            be.sNombreCompleto = be.sApepPaterno + " " + be.sApepMaterno + " " + be.sNombres;
                            be.sCargo = dr.GetString(dr.GetOrdinal("vCargo"));
                            be.iCodPensionario = dr.GetInt32(dr.GetOrdinal("iCodRegimenPen"));
                            be.sNombreRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreRegimenPen"));
                            be.iCodTipoPensionario = dr.GetInt32(dr.GetOrdinal("iCodTipoRegimenPen"));
                            be.sNombreTipoRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreTipoRegimenPen"));
                            if (be.sNombreTipoRegimenPensionario != "")
                            {
                                be.sRegimenPensionario = be.sNombreRegimenPensionario + " - " + be.sNombreTipoRegimenPensionario;
                            }
                            else
                            {
                                be.sRegimenPensionario = be.sNombreRegimenPensionario;
                            }
                            be.bExoneracionRenta4ta = dr.GetBoolean(dr.GetOrdinal("bEstadoSuspencionRetencion"));
                            be.sFechaInicio = dr.GetString(dr.GetOrdinal("dtInicioLabores"));
                            be.sFechaFin = dr.GetString(dr.GetOrdinal("dtFinLabores"));
                            //be.iDiasLaborados = dr.GetInt32(dr.GetOrdinal("iDiasLaboradosPorMes"));
                            //be.iDiasVacaciones = dr.GetInt32(dr.GetOrdinal("iDiasVacaciones"));
                            //be.iDiasLicencias = dr.GetInt32(dr.GetOrdinal("iDiasLicencia"));
                            //be.iDiasDescansos_Subsidios = dr.GetInt32(dr.GetOrdinal("iDiasDescanso"));
                            be.dcMontoRemuneracionBasica = dr.GetDecimal(dr.GetOrdinal("dRemuneracion"));
                            be.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));
                            be.dcMontoTotalDescuentos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));
                            be.dcMontoTotalNeto = dr.GetDecimal(dr.GetOrdinal("dcMontoNeto"));
                            be.dcMontoAporteEsSalud = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion"));
                            be.dcMontoAporteEsSalud_675 = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion_675"));
                            be.dcMontoAporteEsSaludEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacionEPS"));
                            be.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            be.iAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            be.iCodDetPlanilla = dr.GetInt32(dr.GetOrdinal("iCodDetPlanilla"));
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaCalculadaIngresos_Request> ListarPlanillaCalculadaIngresosFEDVacTruncas(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            List<PlanillaCalculadaIngresos_Request> lista = new List<PlanillaCalculadaIngresos_Request>();
            PlanillaCalculadaIngresos_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcCASVacTruncasIngresosConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", 4));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculadaIngresos_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.dcMontoContraprestacion = dr.GetDecimal(dr.GetOrdinal("dcMontoContraprestacion"));
                            //be.dcMontoContraprescionVacaional = dr.GetDecimal(dr.GetOrdinal("dcMontoVacaciones"));
                            be.dcMontoReintegros = dr.GetDecimal(dr.GetOrdinal("dcMontoReintegro"));
                            //be.dcMontoCopagoSubsidio = dr.GetDecimal(dr.GetOrdinal("dcMontoCoPagoSubsidio"));
                            be.dcMontoAguinaldos = dr.GetDecimal(dr.GetOrdinal("dcMontoAguinaldo"));
                            be.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));

                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaCalculadaDescuentos_Request> ListarPlanillaCalculadaDescuentosFEDVacTruncas(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            List<PlanillaCalculadaDescuentos_Request> lista = new List<PlanillaCalculadaDescuentos_Request>();
            PlanillaCalculadaDescuentos_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcCASVacTruncasDescuentosConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", 4));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculadaDescuentos_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.dcMontoTardanzas = dr.GetDecimal(dr.GetOrdinal("dcMontoTardanzas"));
                            be.dcMontoInasistencias = dr.GetDecimal(dr.GetOrdinal("dcMontoInasistencias"));
                            be.dcMontoPermisos = dr.GetDecimal(dr.GetOrdinal("dcMontoPermisos"));
                            be.dcMontoImpuestoRenta4ta = dr.GetDecimal(dr.GetOrdinal("dcMontoRenta4ta"));
                            be.dcMontoONP = dr.GetDecimal(dr.GetOrdinal("dcMontoONP"));
                            be.dcMontoAporteOblAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoAporteAFP"));
                            be.dcMontoComisionAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoComisionAFP"));
                            be.dcMontoPrimaSegAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoPrimaAFP"));
                            be.dcMontoDescuentoJudicial = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoJudicial"));
                            be.dcMontoEsSaludMasVida = dr.GetDecimal(dr.GetOrdinal("dcMontoEsSaludMasVida"));
                            be.dcMontoRimacSeguro = dr.GetDecimal(dr.GetOrdinal("dcMontoRimacSeguro"));
                            be.dcMontoDsctoPagoExceso = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoPagoExceso"));
                            be.dcMontoEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoEPS"));
                            be.dcMontoTotalDescuento = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));

                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralCompletaFEDVacTruncas(int iMes, int iAnio, int iCodDetPlanilla)
        {
            List<PlanillaCalculada_Request> lista = new List<PlanillaCalculada_Request>();
            PlanillaCalculada_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaclcCASVacTruncasGeneralCompletaConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", 4));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculada_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            be.iCodTipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento"));
                            be.sNombreTipoDocumento = dr.GetString(dr.GetOrdinal("vNombreTipoDocumento"));
                            be.sNroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            be.sNombres = dr.GetString(dr.GetOrdinal("vNombres"));
                            be.sApepPaterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            be.sApepMaterno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            be.sNombreCompleto = be.sApepPaterno + " " + be.sApepMaterno + " " + be.sNombres;
                            be.sCargo = dr.GetString(dr.GetOrdinal("vCargo"));
                            be.iCodPensionario = dr.GetInt32(dr.GetOrdinal("iCodRegimenPen"));
                            be.sNombreRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreRegimenPen"));
                            be.iCodTipoPensionario = dr.GetInt32(dr.GetOrdinal("iCodTipoRegimenPen"));
                            be.sNombreTipoRegimenPensionario = dr.GetString(dr.GetOrdinal("vNombreTipoRegimenPen"));
                            if (be.sNombreTipoRegimenPensionario != "")
                            {
                                be.sRegimenPensionario = be.sNombreRegimenPensionario + " - " + be.sNombreTipoRegimenPensionario;
                            }
                            else
                            {
                                be.sRegimenPensionario = be.sNombreRegimenPensionario;
                            }
                            be.bExoneracionRenta4ta = dr.GetBoolean(dr.GetOrdinal("bEstadoSuspencionRetencion"));
                            be.sFechaInicio = dr.GetString(dr.GetOrdinal("dtInicioLabores"));
                            be.sFechaFin = dr.GetString(dr.GetOrdinal("dtFinLabores"));
                            //be.iDiasLaborados = dr.GetInt32(dr.GetOrdinal("iDiasLaboradosPorMes"));
                            //be.iDiasVacaciones = dr.GetInt32(dr.GetOrdinal("iDiasVacaciones"));
                            //be.iDiasLicencias = dr.GetInt32(dr.GetOrdinal("iDiasLicencia"));
                            //be.iDiasDescansos_Subsidios = dr.GetInt32(dr.GetOrdinal("iDiasDescanso"));
                            be.dcMontoRemuneracionBasica = dr.GetDecimal(dr.GetOrdinal("dRemuneracion"));
                            /*Ingresos*/
                            be.objPlanillaCalculadaIngresos_Request = new PlanillaCalculadaIngresos_Request();
                            be.objPlanillaCalculadaIngresos_Request.dcMontoContraprestacion = dr.GetDecimal(dr.GetOrdinal("dcMontoContraprestacion"));
                            //be.objPlanillaCalculadaIngresos_Request.dcMontoContraprescionVacaional = dr.GetDecimal(dr.GetOrdinal("dcMontoVacaciones"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoReintegros = dr.GetDecimal(dr.GetOrdinal("dcMontoReintegro"));
                            //be.objPlanillaCalculadaIngresos_Request.dcMontoCopagoSubsidio = dr.GetDecimal(dr.GetOrdinal("dcMontoCoPagoSubsidio"));
                            be.objPlanillaCalculadaIngresos_Request.dcMontoAguinaldos = dr.GetDecimal(dr.GetOrdinal("dcMontoAguinaldo"));
                            //be.objPlanillaCalculadaIngresos_Request.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));
                            be.dcMontoTotalIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));
                            /*Ingresos*/
                            /*Descuentos*/
                            be.objPlanillaCalculadaDescuentos_Request = new PlanillaCalculadaDescuentos_Request();
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoTardanzas = dr.GetDecimal(dr.GetOrdinal("dcMontoTardanzas"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoInasistencias = dr.GetDecimal(dr.GetOrdinal("dcMontoInasistencias"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoPermisos = dr.GetDecimal(dr.GetOrdinal("dcMontoPermisos"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoImpuestoRenta4ta = dr.GetDecimal(dr.GetOrdinal("dcMontoRenta4ta"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoONP = dr.GetDecimal(dr.GetOrdinal("dcMontoONP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoAporteOblAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoAporteAFP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoComisionAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoComisionAFP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoPrimaSegAFP = dr.GetDecimal(dr.GetOrdinal("dcMontoPrimaAFP"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoDescuentoJudicial = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoJudicial"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoEsSaludMasVida = dr.GetDecimal(dr.GetOrdinal("dcMontoEsSaludMasVida"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoRimacSeguro = dr.GetDecimal(dr.GetOrdinal("dcMontoRimacSeguro"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoDsctoPagoExceso = dr.GetDecimal(dr.GetOrdinal("dcMontoDsctoPagoExceso"));
                            be.objPlanillaCalculadaDescuentos_Request.dcMontoEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoEPS"));
                            //be.dcMontoTotalDescuento = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));
                            be.dcMontoTotalDescuentos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));
                            /*Descuentos*/
                            be.dcMontoTotalNeto = dr.GetDecimal(dr.GetOrdinal("dcMontoNeto"));
                            be.dcMontoAporteEsSalud = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion"));
                            be.dcMontoAporteEsSalud_675 = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacion_675"));
                            be.dcMontoAporteEsSaludEPS = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportacionEPS"));
                            be.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            be.iAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public bool GenerarPlanillaFEDVacTruncas(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, List<Empleado_Registro> lstEmpleados)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {

                cmd.CommandText = "[dbo].[paPlanillaLiquidacionTrabajadorEliminar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                cmd.ExecuteNonQuery();

                if (lstEmpleados != null && lstEmpleados.Count > 0)
                {
                    foreach (var item in lstEmpleados)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "[dbo].[paPlanillaLiquidacionTrabajadorRegistrar]";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", item.IdEmpleado));
                        cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                        cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                        cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                        cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                        cmd.Parameters.Add(new SqlParameter("@iDiasDescansoFisico", item.DiasDescansoFisico));
                        cmd.Parameters.Add(new SqlParameter("@iMesesDescansoFisico", item.MesesDescansoFisico));
                        cmd.Parameters.Add(new SqlParameter("@iDiasVacTruncas", item.DiasVacacionesTruncas));
                        cmd.Parameters.Add(new SqlParameter("@iMesesVacTruncas", item.MesesVacacionesTruncas));
                        cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                        cmd.ExecuteNonQuery();
                    }
                }
                cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[paPlanillaclcVACTRUNCAS2]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@vCodTrabajadores", strCodTrabajadores));
                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }




        #endregion Planilla FED Vacaciones Truncas

        #region Importacion de Asistencia
        public bool EjecutarImportarAsistencia(PlanillaAsistencia_Registro registro)
        {
            bool rpta = false;
            try
            {
                _iBasesSqlAdoUnitOfWork.IniciarTransaccion(IsolationLevel.ReadCommitted);

                using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
                {
                    if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                    cmd.CommandText = "[dbo].[paPlanillaclcASIS_PERM_del_2]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.CommandTimeout = cmd.CommandTimeout;
                    cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", registro.IdPlanilla));
                    cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", registro.IdTipoPlanilla));
                    cmd.Parameters.Add(new SqlParameter("@iMes", registro.IdMes));
                    cmd.Parameters.Add(new SqlParameter("@iAnio", registro.IdAnio));
                    cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", registro.iCodDetPlanilla));
                    cmd.ExecuteNonQuery();

                    String nameFile = String.Empty;
                    if (registro.formatos != null)
                    {
                        foreach (String codigo in registro.formatos)
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandText = "[dbo].[paPlanillaclcASIS_PERM_ins]";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@vNroDocumento", codigo.Split('|')[0]));
                            cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", registro.IdPlanilla));
                            cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", registro.IdTipoPlanilla));
                            cmd.Parameters.Add(new SqlParameter("@iMes", registro.IdMes));
                            cmd.Parameters.Add(new SqlParameter("@iAnio", registro.IdAnio));
                            cmd.Parameters.Add(new SqlParameter("@iVacaciones", codigo.Split('|')[1]));
                            cmd.Parameters.Add(new SqlParameter("@iFalta", codigo.Split('|')[2]));
                            cmd.Parameters.Add(new SqlParameter("@iLicencia", codigo.Split('|')[3]));
                            cmd.Parameters.Add(new SqlParameter("@iTardanza", codigo.Split('|')[4]));
                            cmd.Parameters.Add(new SqlParameter("@iPermiso", codigo.Split('|')[5]));
                            cmd.Parameters.Add(new SqlParameter("@iSalidas", codigo.Split('|')[6]));
                            cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", registro.iCodDetPlanilla));

                            cmd.ExecuteNonQuery();
                        }
                    }

                    cmd.Parameters.Clear();
                    cmd.CommandText = "[dbo].[paPlanillaclcASIS_PERM]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.CommandTimeout = cmd.CommandTimeout;
                    cmd.Parameters.Add(new SqlParameter("@iMes", registro.IdMes));
                    cmd.Parameters.Add(new SqlParameter("@iAnio", registro.IdAnio));
                    cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", registro.IdPlanilla));
                    cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", registro.IdTipoPlanilla));
                    cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", registro.iCodDetPlanilla));
                    cmd.ExecuteNonQuery();

                    if (_iBasesSqlAdoUnitOfWork.TieneTransaccion()) _iBasesSqlAdoUnitOfWork.ConfirmarTransaccion();
                    if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
                    
                    rpta = true;
                }
            }
            catch (Exception)
            {
                if (_iBasesSqlAdoUnitOfWork.TieneTransaccion()) _iBasesSqlAdoUnitOfWork.RetrocederTransaccion();
                throw;
            }
            
            return rpta;
        }

        public IEnumerable<PlanillaAsistencia_Registro> ListarPlanillas(PlanillaAsistencia_Registro registro)
        {
            List<PlanillaAsistencia_Registro> lista = new List<PlanillaAsistencia_Registro>();
            PlanillaAsistencia_Registro be = null;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaEjecucion_listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", registro.IdPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", registro.IdTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", registro.IdMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", registro.IdAnio));
                cmd.ExecuteNonQuery();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaAsistencia_Registro();
                            be.Grilla = new Grilla_Response();

                            be.IdPlanilla = dr.GetInt32(dr.GetOrdinal("iCodPlanilla"));
                            be.IdTipoPlanilla = dr.GetInt32(dr.GetOrdinal("iCodTipoPlanilla"));
                            be.iCodDetPlanilla = dr.GetInt32(dr.GetOrdinal("iCodDetPlanilla"));
                            be.IdAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            be.IdMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            be.NombrePlanilla = dr.GetString(dr.GetOrdinal("NombrePlanilla"));
                            be.NombreTipoPlanilla = dr.GetString(dr.GetOrdinal("NombreTipoPlanilla"));
                            be.iCodDetPlanilla = dr.GetInt32(dr.GetOrdinal("iCodDetPlanilla"));
                            be.bEstadoRegAsistencia = dr.GetBoolean(dr.GetOrdinal("bEstadoRegAsistencia"));
                            be.bEstadoDsctoFijoVariable = dr.GetBoolean(dr.GetOrdinal("bEstadoDsctoFijoVariable"));
                            lista.Add(be);
                        }
                    }
                }
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return lista;
        }

        public IEnumerable<PlanillaControlAsistencia_Registro> ListarControlAsistencia(PlanillaAsistencia_Registro registro)
        {
            List<PlanillaControlAsistencia_Registro> lista = new List<PlanillaControlAsistencia_Registro>();
            PlanillaControlAsistencia_Registro be = null;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaControlTrabajador_listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", registro.IdPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", registro.IdTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", registro.IdMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", registro.IdAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", registro.iCodDetPlanilla));
                cmd.ExecuteNonQuery();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaControlAsistencia_Registro();
                            be.Grilla = new Grilla_Response();

                            be.IdTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            be.IdPlanilla = dr.GetInt32(dr.GetOrdinal("iCodPlanilla"));
                            be.IdTipoPlanilla = dr.GetInt32(dr.GetOrdinal("iCodTipoPlanilla"));
                            be.IdDetPlanilla = dr.GetInt32(dr.GetOrdinal("iCodDetPlanilla"));
                            be.IdAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            be.IdMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            be.NombrePlanilla = dr.GetString(dr.GetOrdinal("NombrePlanilla"));
                            be.NombreTipoPlanilla = dr.GetString(dr.GetOrdinal("NombreTipoPlanilla"));

                            be.NombreTrabajador = String.Format("{0} {1} {2}", dr.GetString(dr.GetOrdinal("vNombres")), dr.GetString(dr.GetOrdinal("vApellidoPaterno")), dr.GetString(dr.GetOrdinal("vApellidoMaterno")));
                            be.NombreDependencia = dr.GetString(dr.GetOrdinal("vDependencia"));
                            be.NombreCondicion = dr.GetString(dr.GetOrdinal("NombreCondicion"));
                            
                            be.DiasLaborados = dr.GetInt32(dr.GetOrdinal("iDiasLaborados"));
                            be.Vacaciones = dr.GetInt32(dr.GetOrdinal("iDiasVacaciones"));
                            be.Licencia = dr.GetInt32(dr.GetOrdinal("iDiasLicencia"));
                            be.ImporteLicencias = dr.GetDecimal(dr.GetOrdinal("dImporteLicencias"));
                            be.Tardanza = dr.GetInt32(dr.GetOrdinal("iMinutosTardanza"));
                            be.ImporteTardanzas = dr.GetDecimal(dr.GetOrdinal("dImporteTardanzas"));
                            be.Permisos = dr.GetInt32(dr.GetOrdinal("iMinutosPermiso"));
                            be.ImportePermisos = dr.GetDecimal(dr.GetOrdinal("dImportePermisos"));
                            be.Faltas = dr.GetInt32(dr.GetOrdinal("iFaltas"));
                            be.ImporteFaltas = dr.GetDecimal(dr.GetOrdinal("dImporteFaltas"));
                            be.Salidas = dr.GetInt32(dr.GetOrdinal("iSalidaAnticipada"));
                            be.ImporteSalidas = dr.GetDecimal(dr.GetOrdinal("dImporteSalidaAnticipada"));
                            be.ImporteDescuento= dr.GetDecimal(dr.GetOrdinal("dImporteDescontar"));

                            be.Sancion_Disciplinaria = dr.GetInt32(dr.GetOrdinal("iSancionDisciplinaria"));
                            be.Enfermedad_o_Accidente = dr.GetInt32(dr.GetOrdinal("iEnfermedadAccidente"));
                            be.Licencia_con_Goce = dr.GetInt32(dr.GetOrdinal("iLicenciaConGoce"));
                            be.Licencia_por_Paternidad = dr.GetInt32(dr.GetOrdinal("iLicenciaPaternidad"));
                            be.Licencia_por_Fallecimiento_de_Padres = dr.GetInt32(dr.GetOrdinal("iLicenciaFallecimientoPadres"));
                            be.Licencia_por_Enfermedad_Grave_o_Terminal_o_Accidente_Grave_de_Familiares_Directos = dr.GetInt32(dr.GetOrdinal("iLicenciaEnfermedadGraveFam"));

                            lista.Add(be);
                        }
                    }
                }
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return lista;
        }

        public bool EliminarControlAsistenciaPermisosPorTrabajador(PlanillaControlAsistencia_Registro registro)
        {
            bool rpta = false;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                cmd.CommandText = "[dbo].[paPlanillaclcASIS_PERM_delPorID]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", registro.IdTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", registro.IdPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", registro.IdTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", registro.IdMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", registro.IdAnio));                
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", registro.IdDetPlanilla));

                cmd.ExecuteNonQuery();
                rpta = true;

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            //return registro.iCodConcepto;
            return rpta;
        }
        
        
        
        #endregion

        #region Adminitrar Planilla
        public bool InsertarPlanilla(PlanillaEjecucion_Registro registro)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaEjecucion_Registrar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", registro.iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", registro.iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", registro.iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", registro.iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@vUsuario", registro.sUsuarioApertura)); 
                
                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        public bool CerrarPlanilla(PlanillaEjecucion_Registro registro)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaEjecucion_Cerrar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", registro.iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", registro.iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", registro.iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", registro.iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@vUsuario", registro.sUsuarioCierre));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", registro.iCodDetPlanilla));
                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        public bool ModFasePlanilla(PlanillaEjecucion_Registro registro)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaEjecucion_Modificar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", registro.iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", registro.iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", registro.iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", registro.iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@vUsuario", registro.sUsuarioCierre));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", registro.iCodDetPlanilla));
                cmd.Parameters.Add(new SqlParameter("@bEstadoRegAsistencia", registro.bEstadoRegAsistencia));
                cmd.Parameters.Add(new SqlParameter("@bEstadoDsctoFijoVariable", registro.bEstadoDsctoFijoVariable));
                cmd.Parameters.Add(new SqlParameter("@bEstadoCierre", registro.bEstadoCierre));
                cmd.Parameters.Add(new SqlParameter("@sObservacion", registro.sObservacion));
                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        public IEnumerable<PlanillaEjecucion_Registro> ListarPlanillasCreadas(PlanillaEjecucion_Registro objPlanillaEjecucion_Registro)
        {
            List<PlanillaEjecucion_Registro> lista = new List<PlanillaEjecucion_Registro>();
            PlanillaEjecucion_Registro be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaEjecucionConsultar2]";
                cmd.CommandType = CommandType.StoredProcedure;

                if (objPlanillaEjecucion_Registro != null)
                {
                    if (objPlanillaEjecucion_Registro.iCodPlanilla != 0)
                    {
                        cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", objPlanillaEjecucion_Registro.iCodPlanilla));
                    }
                    if (objPlanillaEjecucion_Registro.iCodTipoPlanilla != 0)
                    {
                        cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", objPlanillaEjecucion_Registro.iCodTipoPlanilla));
                    }
                    if (objPlanillaEjecucion_Registro.iMes != 0)
                    {
                        cmd.Parameters.Add(new SqlParameter("@iMes", objPlanillaEjecucion_Registro.iMes));
                    }
                    if (objPlanillaEjecucion_Registro.iAnio != 0)
                    {
                        cmd.Parameters.Add(new SqlParameter("@iAnio", objPlanillaEjecucion_Registro.iAnio));
                    }                    
                }                

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaEjecucion_Registro();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();                           

                            be.iCodPlanilla = dr.GetInt32(dr.GetOrdinal("iCodPlanilla"));
                            be.sNombrePlanilla = dr.GetString(dr.GetOrdinal("NombrePlanilla"));
                            be.iCodTipoPlanilla = dr.GetInt32(dr.GetOrdinal("iCodTipoPlanilla"));
                            be.iCodDetPlanilla = dr.GetInt32(dr.GetOrdinal("iCodDetPlanilla"));
                            be.sNombreTipoPlanilla = dr.GetString(dr.GetOrdinal("NombreTipoPlanilla"));
                            be.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            be.iAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            be.sFechaReg = dr.GetString(dr.GetOrdinal("vFechaReg"));
                            be.bEstadoEjecutado = dr.GetBoolean(dr.GetOrdinal("bEstadoEjecutado"));
                            be.sEstadoEjecutado = dr.GetString(dr.GetOrdinal("vEstadoEjecutado"));
                            be.sFechaGeneracion = dr.GetString(dr.GetOrdinal("vFechaGenracion"));

                            be.bEstadoCierre = dr.GetBoolean(dr.GetOrdinal("bEstadoCierre"));
                            be.sEstadoCierre = dr.GetString(dr.GetOrdinal("vEstadoCierre"));
                            be.sFechaCierre = dr.GetString(dr.GetOrdinal("vFechaCierre"));

                            be.bEstadoRegAsistencia = dr.GetBoolean(dr.GetOrdinal("bEstadoRegAsistencia"));
                            be.sEstadoRegAsistencia = dr.GetString(dr.GetOrdinal("vEstadoRegAsistencia"));
                            be.sFechaRegAsistencia = dr.GetString(dr.GetOrdinal("vFechaRegAsistencia"));

                            be.bEstadoDsctoFijoVariable = dr.GetBoolean(dr.GetOrdinal("bEstadoDsctoFijoVariable"));
                            be.sEstadoDsctoFijoVariable = dr.GetString(dr.GetOrdinal("vEstadoDsctoFijoVariable"));
                            be.sFechaDsctoFijoVariable = dr.GetString(dr.GetOrdinal("vFechaDsctoFijoVariable"));

                            be.dIngresos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));
                            be.dDescuentos = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));
                            be.dAportes = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAportes"));
                            be.sObservacion = dr.GetString(dr.GetOrdinal("vObservacion"));

                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<Planilla_Request> ListarPlanillasBase()
        {
            List<Planilla_Request> lista = new List<Planilla_Request>();
            Planilla_Request be = null;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePlanillaConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;
                
                cmd.ExecuteNonQuery();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new Planilla_Request();
                            be.iCodPlanilla = dr.GetInt32(dr.GetOrdinal("iCodPlanilla"));
                            be.vNombrePlanilla = dr.GetString(dr.GetOrdinal("vNombre"));
                            lista.Add(be);
                        }
                    }
                }
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return lista;
        }
        
        public IEnumerable<TipoPlanilla_Request> ListarTipoPlanillasBase()
        {
            List<TipoPlanilla_Request> lista = new List<TipoPlanilla_Request>();
            TipoPlanilla_Request be = null;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaeTipoPlanillaConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.ExecuteNonQuery();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new TipoPlanilla_Request();
                            be.iCodTipoPlanilla = dr.GetInt32(dr.GetOrdinal("iCodTipoPlanilla"));
                            be.vNombre = dr.GetString(dr.GetOrdinal("vNombre"));
                            lista.Add(be);
                        }
                    }
                }
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return lista;
        }

        public IEnumerable<EmpleadoReporteBanco_Registro> ListarPlanillaCerrada(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            List<EmpleadoReporteBanco_Registro> lista = new List<EmpleadoReporteBanco_Registro>();
            EmpleadoReporteBanco_Registro be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaRptoBCOConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new EmpleadoReporteBanco_Registro();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();


                            be.IdEmpleado = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            
                            be.TipoDocumento = dr.GetString(dr.GetOrdinal("vNombreTipoDocumento"));
                            be.NroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            be.Nombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            be.Paterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            be.Materno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            be.Banco = dr.GetString(dr.GetOrdinal("vBanco"));
                            be.NroCuenta = dr.GetString(dr.GetOrdinal("vCuenta"));
                            be.NroCCI = dr.GetString(dr.GetOrdinal("vCCI"));
                            be.ImporteAbonar = dr.GetDecimal(dr.GetOrdinal("dcImporteAbonar"));
                            
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaRpteResumenGeneraResponse> ReporteResumenGeneral(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            List<PlanillaRpteResumenGeneraResponse> lista = new List<PlanillaRpteResumenGeneraResponse>();
            PlanillaRpteResumenGeneraResponse be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaRpteGeneral]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        //int cantLst = 0;
                        //int cantTipo1 = 0;
                        while (dr.Read())
                        {
                            be = new PlanillaRpteResumenGeneraResponse();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();

                            be.iCodTipoConcepto = dr.GetInt32(dr.GetOrdinal("iCodTipoConcepto"));
                            be.sClasificadorGastoIng = dr.GetString(dr.GetOrdinal("vClasificadorGastoIng"));
                            be.sConceptoIng = dr.GetString(dr.GetOrdinal("vConceptoIng"));
                            be.dcMontoIng = dr.GetDecimal(dr.GetOrdinal("dcMontoIng")) == 0 ? (decimal?)null : dr.GetDecimal(dr.GetOrdinal("dcMontoIng"));
                            be.iCantIng = dr.GetInt32(dr.GetOrdinal("iCantIng")) == 0 ? (int?)null : dr.GetInt32(dr.GetOrdinal("iCantIng"));

                            be.sClasificadorGastoDscto = dr.GetString(dr.GetOrdinal("vClasificadorGastoDscto"));
                            be.sConceptoDscto = dr.GetString(dr.GetOrdinal("vConceptoDscto"));
                            be.dcMontoDscto = dr.GetDecimal(dr.GetOrdinal("dcMontoDscto")) == 0 ? (decimal?)null : dr.GetDecimal(dr.GetOrdinal("dcMontoDscto"));
                            be.iCantDscto = dr.GetInt32(dr.GetOrdinal("iCantDscto")) == 0 ? (int?)null : dr.GetInt32(dr.GetOrdinal("iCantDscto"));

                            be.sClasificadorGastoAporte = dr.GetString(dr.GetOrdinal("vClasificadorGastoAportes"));
                            be.sConceptoAporte = dr.GetString(dr.GetOrdinal("vConceptoAportes"));
                            be.dcMontoAporte = dr.GetDecimal(dr.GetOrdinal("dcMontoAportes")) == 0 ? (decimal?)null : dr.GetDecimal(dr.GetOrdinal("dcMontoAportes"));
                            be.iCantAporte = dr.GetInt32(dr.GetOrdinal("iCantAportes")) == 0 ? (int?)null : dr.GetInt32(dr.GetOrdinal("iCantAportes"));
                            //if (lista.Count()>0)
                            //{
                            //    cantLst = lista.Where(x=>x.iCodTipoConcepto==1).Count();
                            //}
                            
                            //be.iCodTipoConcepto = dr.GetInt32(dr.GetOrdinal("iCodTipoConcepto"));
                            //if (be.iCodTipoConcepto==1)
                            //{
                            //    be.sClasificadorGastoIng = dr.GetString(dr.GetOrdinal("vClasificadorGastoIng"));
                            //    be.sConceptoIng = dr.GetString(dr.GetOrdinal("vConceptoIng"));
                            //    be.dcMontoIng = dr.GetDecimal(dr.GetOrdinal("dcMontoIng")) == 0 ? (decimal?)null : dr.GetDecimal(dr.GetOrdinal("dcMontoIng"));
                            //    be.iCantIng = dr.GetInt32(dr.GetOrdinal("iCantIng")) == 0 ? (int?)null : dr.GetInt32(dr.GetOrdinal("iCantIng"));
                            //    lista.Add(be);
                            //}
                            //if (be.iCodTipoConcepto==2)
                            //{
                            //    cantTipo1 += 1;
                            //    if (cantTipo1<=cantLst)
                            //    {
                            //        be.sClasificadorGastoDscto = lista.Where(x => x.iCodTipoConcepto == 1).Select(x => x.dcMontoIng).Sum();
                            //        be.sConceptoDscto = lista.Where(x => x.iCodTipoConcepto == 2).Select(x => x.dcMontoDscto).Sum();
                            //        be.dcMontoDscto = lista.Where(x => x.iCodTipoConcepto == 3).Select(x => x.dcMontoAporte).Sum();
                            //        be.iCantDscto = be.dcTotalIng - be.dcTotalDscto;
                            //    }
                            //    be.sClasificadorGastoDscto = dr.GetString(dr.GetOrdinal("vClasificadorGastoDscto"));
                            //    be.sConceptoDscto = dr.GetString(dr.GetOrdinal("vConceptoDscto"));
                            //    be.dcMontoDscto = dr.GetDecimal(dr.GetOrdinal("dcMontoDscto")) == 0 ? (decimal?)null : dr.GetDecimal(dr.GetOrdinal("dcMontoDscto"));
                            //    be.iCantDscto = dr.GetInt32(dr.GetOrdinal("iCantDscto")) == 0 ? (int?)null : dr.GetInt32(dr.GetOrdinal("iCantDscto"));
                            //}

                            

                            //be.sClasificadorGastoAporte = dr.GetString(dr.GetOrdinal("vClasificadorGastoAportes"));
                            //be.sConceptoAporte = dr.GetString(dr.GetOrdinal("vConceptoAportes"));
                            //be.dcMontoAporte = dr.GetDecimal(dr.GetOrdinal("dcMontoAportes")) == 0 ? (decimal?)null : dr.GetDecimal(dr.GetOrdinal("dcMontoAportes"));
                            //be.iCantAporte = dr.GetInt32(dr.GetOrdinal("iCantAportes")) == 0 ? (int?)null : dr.GetInt32(dr.GetOrdinal("iCantAportes"));
                            lista.Add(be);
                        }
                        be = new PlanillaRpteResumenGeneraResponse();
                        be.dcTotalIng = lista.Where(x=>x.iCodTipoConcepto==1).Select(x=>x.dcMontoIng).Sum();
                        be.dcTotalDscto = lista.Where(x => x.iCodTipoConcepto == 2).Select(x => x.dcMontoDscto).Sum();
                        be.dcTotalAportes = lista.Where(x => x.iCodTipoConcepto == 3).Select(x => x.dcMontoAporte).Sum();
                        be.dcTotalLiquido = be.dcTotalIng - be.dcTotalDscto;
                        lista.Add(be);
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaRpteResumenGeneraResponse> ReporteResumenGeneral2(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            List<PlanillaRpteResumenGeneraResponse> lista = new List<PlanillaRpteResumenGeneraResponse>();
            PlanillaRpteResumenGeneraResponse be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaRpteGeneral_3]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        //int cantLst = 0;
                        //int cantTipo1 = 0;
                        while (dr.Read())
                        {
                            be = new PlanillaRpteResumenGeneraResponse();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();

                            be.iCodTipoConcepto = dr.GetInt32(dr.GetOrdinal("iCodTipoConcepto"));
                            be.sClasificadorGasto = dr.GetString(dr.GetOrdinal("vClasificadorGasto"));
                            be.sConcepto = dr.GetString(dr.GetOrdinal("vConcepto"));
                            be.dcMonto = dr.GetDecimal(dr.GetOrdinal("dcMonto"));
                            be.iCant = dr.GetInt32(dr.GetOrdinal("iCant"));
                            be.iAnio = iAnio;
                            be.sMes = sMes;
                            be.sPlanilla = sPlanilla;
                            be.sNombreReporte = sNombreReporte;
                            lista.Add(be);
                        }
                        //be = new PlanillaRpteResumenGeneraResponse();
                        //be.dcTotalIng = lista.Where(x => x.iCodTipoConcepto == 1).Select(x => x.dcMonto).Sum();
                        //be.dcTotalDscto = lista.Where(x => x.iCodTipoConcepto == 2).Select(x => x.dcMonto).Sum();
                        //be.dcTotalAportes = lista.Where(x => x.iCodTipoConcepto == 3).Select(x => x.dcMonto).Sum();
                        //be.dcTotalLiquido = be.dcTotalIng - be.dcTotalDscto;
                        //be.iAnio = iAnio;
                        //be.sMes = sMes;
                        //be.sPlanilla = sPlanilla;
                        //lista.Add(be);
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public string fnMes(int iMes)
        {
            string sMes = string.Empty;
            switch (iMes)
            {
                case 1:
                    {
                        sMes = "ENERO";
                        break;
                    }
                case 2:
                    {
                        sMes = "FEBRERO";
                        break;
                    }
                case 3:
                    {
                        sMes = "MARZO";
                        break;
                    }
                case 4:
                    {
                        sMes = "ABRIL";
                        break;
                    }
                case 5:
                    {
                        sMes = "MAYO";
                        break;
                    }
                case 6:
                    {
                        sMes = "JUNIO";
                        break;
                    }
                case 7:
                    {
                        sMes = "JULIO";
                        break;
                    }
                case 8:
                    {
                        sMes = "AGOSTO";
                        break;
                    }
                case 9:
                    {
                        sMes = "SETIEMBRE";
                        break;
                    }
                case 10:
                    {
                        sMes = "OCTUBRE";
                        break;
                    }
                case 11:
                    {
                        sMes = "NOVIEMBRE";
                        break;
                    }
                case 12:
                    {
                        sMes = "DICIEMBRE";
                        break;
                    }
            }
            return sMes;
        }

        public IEnumerable<PlanillaRpteResumenGeneralResponse2> RRG2(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            List<PlanillaRpteResumenGeneralResponse2> lista = new List<PlanillaRpteResumenGeneralResponse2>();
            //List<PlanillaRpteResumenGeneral2> lista_2 = null;
            //List<PlanillaRpteResumenGeneral2> lista_3 = null;
            PlanillaRpteResumenGeneralResponse2 be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaRpteGeneralPorFTE_FTO_PARTIDA_META]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        //int cantLst = 0;
                        //int cantTipo1 = 0;
                        while (dr.Read())
                        {
                            be = new PlanillaRpteResumenGeneralResponse2();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();

                            be.iCodTipFuenteFinanciamiento = dr.GetInt32(dr.GetOrdinal("iCodTipFuenteFinanciamiento"));
                            be.sFuenteFinanciamiento = dr.GetString(dr.GetOrdinal("vFuenteFinanciamiento"));
                            be.sSec_Func = dr.GetString(dr.GetOrdinal("vSec_Func"));
                            be.sMetaPresupuestal = dr.GetString(dr.GetOrdinal("vMetaPresupuestal"));
                            be.iCodDependencia = dr.GetInt32(dr.GetOrdinal("iCodigoDependencia"));
                            be.sDependencia = dr.GetString(dr.GetOrdinal("vDependencia"));
                            be.iCodTipoConcepto = dr.GetInt32(dr.GetOrdinal("iCodTipoConcepto"));
                            be.sTipoConcepto = dr.GetString(dr.GetOrdinal("vTipoConcepto"));
                            be.sClasificadorGasto = dr.GetString(dr.GetOrdinal("vClasificadorGasto"));
                            be.dcMontoIng = dr.GetDecimal(dr.GetOrdinal("dcMontoIng"));
                            be.dcMontoEgre = dr.GetDecimal(dr.GetOrdinal("dcMontoEgre"));
                            be.dcMontoSaldo = dr.GetDecimal(dr.GetOrdinal("dcMontoSaldo"));                            
                            lista.Add(be);
                        }
                        //int cantFteFto = lista.GroupBy(i => i.sFuenteFinanciamiento).Count();
                        //if (cantFteFto>0)
                        //{
                        //    lista_2 = new List<PlanillaRpteResumenGeneralPorFTE_FTO_Partida_Meta>();
                        //}
                        //int cantClasificadores = lista.GroupBy(i => i.sClasificadorGasto).Count();
                        //var clasificadores = lista.GroupBy(i => i.sClasificadorGasto).ToList();
                        //int cantTipoConcepto = lista.GroupBy(i => i.iCodTipoConcepto).Count();
                        //var TipoConcepto = lista.GroupBy(i => i.iCodTipoConcepto).ToList();
                        //for (int i = 0; i < cantFteFto; i++)
                        //{
                        //    be = new PlanillaRpteResumenGeneralPorFTE_FTO_Partida_Meta();
                        //    lista_3 = new List<PlanillaRpteResumenGeneralPorFTE_FTO_Partida_Meta>();
                        //    for (int j = 0; j < cantClasificadores; j++)
                        //    {
                        //        be = new PlanillaRpteResumenGeneralPorFTE_FTO_Partida_Meta();
                        //        be.iCodTipFuenteFinanciamiento = lista.Where(x => x.iCodTipFuenteFinanciamiento == i + 1 && x.sClasificadorGasto == clasificadores[j].Key).FirstOrDefault().iCodTipFuenteFinanciamiento;
                        //        be.sFuenteFinanciamiento = lista.Where(x => x.iCodTipFuenteFinanciamiento == i + 1 && x.sClasificadorGasto == clasificadores[j].Key).FirstOrDefault().sFuenteFinanciamiento;
                        //        be.iCodTipoConcepto = lista.Where(x => x.iCodTipFuenteFinanciamiento == i + 1 && x.sClasificadorGasto == clasificadores[j].Key).FirstOrDefault().iCodTipoConcepto;
                        //        be.sSec_Func = lista.Where(x => x.iCodTipFuenteFinanciamiento == i + 1 && x.sClasificadorGasto == clasificadores[j].Key).FirstOrDefault().sSec_Func;
                        //        be.dcMontoTotalIng = lista.Where(x => x.iCodTipFuenteFinanciamiento == i + 1 && x.sClasificadorGasto==clasificadores[j].Key).ToList().Sum(x => x.dcMontoIng);
                        //        be.dcMontoTotalEgre = lista.Where(x => x.iCodTipFuenteFinanciamiento == i + 1 && x.sClasificadorGasto == clasificadores[j].Key).ToList().Sum(x => x.dcMontoEgre);
                        //        be.dcMontoTotalSaldo = lista.Where(x => x.iCodTipFuenteFinanciamiento == i + 1 && x.sClasificadorGasto == clasificadores[j].Key).ToList().Sum(x => x.dcMontoSaldo);
                        //        be.lstObj = lista.Where(x => x.iCodTipFuenteFinanciamiento == i + 1 && x.sClasificadorGasto == clasificadores[j].Key).ToList();
                        //        lista_3.Add(be);
                        //    }
                        //    be = new PlanillaRpteResumenGeneralPorFTE_FTO_Partida_Meta();
                        //    be.sFuenteFinanciamiento = lista_3.FirstOrDefault().sFuenteFinanciamiento;
                        //    be.sSec_Func = "";
                        //    be.sMetaPresupuestal = "";
                        //    //be.iCodDependencia = "";
                        //    be.sDependencia = "";
                        //    //be.iCodTipoConcepto = dr.GetInt32(dr.GetOrdinal("iCodTipoConcepto"));
                        //    be.sTipoConcepto = "";
                        //    be.sClasificadorGasto = "";
                        //    be.dcMontoTotalIng = lista_3.Where(x=>x.iCodTipoConcepto==1).Sum(x => x.dcMontoTotalIng);
                        //    be.dcMontoTotalEgre = lista_3.Where(x => x.iCodTipoConcepto == 3).Sum(x => x.dcMontoTotalIng);
                        //    be.lstObj = lista_3.ToList();
                        //    lista_2.Add(be);
                        //}

                        //be = new PlanillaRpteResumenGeneralPorFTE_FTO_Partida_Meta();
                        //be.dcTotalIng = lista.Where(x => x.iCodTipoConcepto == 1).Select(x => x.dcMontoIng).Sum();
                        //be.dcTotalDscto = lista.Where(x => x.iCodTipoConcepto == 2).Select(x => x.dcMontoDscto).Sum();
                        //be.dcTotalAportes = lista.Where(x => x.iCodTipoConcepto == 3).Select(x => x.dcMontoAporte).Sum();
                        //be.dcTotalLiquido = be.dcTotalIng - be.dcTotalDscto;
                        //lista.Add(be);
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        //public IEnumerable<PlanillaRpteResumenGeneralResponse2> RRG2_1(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, string sMes, string sPlanilla)
        //{
        //    List<PlanillaRpteResumenGeneralResponse2> lista = new List<PlanillaRpteResumenGeneralResponse2>();
        //    //List<PlanillaRpteResumenGeneral2> lista_2 = null;
        //    //List<PlanillaRpteResumenGeneral2> lista_3 = null;
        //    PlanillaRpteResumenGeneralResponse2 be = null;

        //    using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
        //    {
        //        if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
        //        cmd.CommandText = "[dbo].[paPlanillaRpteGeneralPorFTE_FTO_PARTIDA_META]";
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
        //        cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
        //        cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
        //        cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
        //        cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
        //        //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

        //        using (SqlDataReader dr = cmd.ExecuteReader())
        //        {
        //            if (dr.HasRows)
        //            {
        //                //int cantLst = 0;
        //                //int cantTipo1 = 0;
        //                while (dr.Read())
        //                {
        //                    be = new PlanillaRpteResumenGeneralResponse2();
        //                    //item.Grilla = new Grilla_Response();
        //                    be.Grilla = new Grilla_Response();

        //                    be.iCodTipFuenteFinanciamiento = dr.GetInt32(dr.GetOrdinal("iCodTipFuenteFinanciamiento"));
        //                    be.sFuenteFinanciamiento = dr.GetString(dr.GetOrdinal("vFuenteFinanciamiento"));
        //                    be.sSec_Func = dr.GetString(dr.GetOrdinal("vSec_Func"));
        //                    be.sMetaPresupuestal = dr.GetString(dr.GetOrdinal("vMetaPresupuestal"));
        //                    be.iCodDependencia = dr.GetInt32(dr.GetOrdinal("iCodigoDependencia"));
        //                    be.sDependencia = dr.GetString(dr.GetOrdinal("vDependencia"));
        //                    be.iCodTipoConcepto = dr.GetInt32(dr.GetOrdinal("iCodTipoConcepto"));
        //                    be.sTipoConcepto = dr.GetString(dr.GetOrdinal("vTipoConcepto"));
        //                    be.sClasificadorGasto = dr.GetString(dr.GetOrdinal("vClasificadorGasto"));
        //                    be.dcMontoIng = dr.GetDecimal(dr.GetOrdinal("dcMontoIng"));
        //                    be.dcMontoEgre = dr.GetDecimal(dr.GetOrdinal("dcMontoEgre"));
        //                    be.dcMontoSaldo = dr.GetDecimal(dr.GetOrdinal("dcMontoSaldo"));
        //                    be.sMes = sMes;
        //                    be.sPlanilla = sPlanilla;
        //                    be.iAnio = iAnio;
        //                    lista.Add(be);
        //                }
        //                //int cantFteFto = lista.GroupBy(i => i.sFuenteFinanciamiento).Count();
        //                //if (cantFteFto>0)
        //                //{
        //                //    lista_2 = new List<PlanillaRpteResumenGeneralPorFTE_FTO_Partida_Meta>();
        //                //}
        //                //int cantClasificadores = lista.GroupBy(i => i.sClasificadorGasto).Count();
        //                //var clasificadores = lista.GroupBy(i => i.sClasificadorGasto).ToList();
        //                //int cantTipoConcepto = lista.GroupBy(i => i.iCodTipoConcepto).Count();
        //                //var TipoConcepto = lista.GroupBy(i => i.iCodTipoConcepto).ToList();
        //                //for (int i = 0; i < cantFteFto; i++)
        //                //{
        //                //    be = new PlanillaRpteResumenGeneralPorFTE_FTO_Partida_Meta();
        //                //    lista_3 = new List<PlanillaRpteResumenGeneralPorFTE_FTO_Partida_Meta>();
        //                //    for (int j = 0; j < cantClasificadores; j++)
        //                //    {
        //                //        be = new PlanillaRpteResumenGeneralPorFTE_FTO_Partida_Meta();
        //                //        be.iCodTipFuenteFinanciamiento = lista.Where(x => x.iCodTipFuenteFinanciamiento == i + 1 && x.sClasificadorGasto == clasificadores[j].Key).FirstOrDefault().iCodTipFuenteFinanciamiento;
        //                //        be.sFuenteFinanciamiento = lista.Where(x => x.iCodTipFuenteFinanciamiento == i + 1 && x.sClasificadorGasto == clasificadores[j].Key).FirstOrDefault().sFuenteFinanciamiento;
        //                //        be.iCodTipoConcepto = lista.Where(x => x.iCodTipFuenteFinanciamiento == i + 1 && x.sClasificadorGasto == clasificadores[j].Key).FirstOrDefault().iCodTipoConcepto;
        //                //        be.sSec_Func = lista.Where(x => x.iCodTipFuenteFinanciamiento == i + 1 && x.sClasificadorGasto == clasificadores[j].Key).FirstOrDefault().sSec_Func;
        //                //        be.dcMontoTotalIng = lista.Where(x => x.iCodTipFuenteFinanciamiento == i + 1 && x.sClasificadorGasto==clasificadores[j].Key).ToList().Sum(x => x.dcMontoIng);
        //                //        be.dcMontoTotalEgre = lista.Where(x => x.iCodTipFuenteFinanciamiento == i + 1 && x.sClasificadorGasto == clasificadores[j].Key).ToList().Sum(x => x.dcMontoEgre);
        //                //        be.dcMontoTotalSaldo = lista.Where(x => x.iCodTipFuenteFinanciamiento == i + 1 && x.sClasificadorGasto == clasificadores[j].Key).ToList().Sum(x => x.dcMontoSaldo);
        //                //        be.lstObj = lista.Where(x => x.iCodTipFuenteFinanciamiento == i + 1 && x.sClasificadorGasto == clasificadores[j].Key).ToList();
        //                //        lista_3.Add(be);
        //                //    }
        //                //    be = new PlanillaRpteResumenGeneralPorFTE_FTO_Partida_Meta();
        //                //    be.sFuenteFinanciamiento = lista_3.FirstOrDefault().sFuenteFinanciamiento;
        //                //    be.sSec_Func = "";
        //                //    be.sMetaPresupuestal = "";
        //                //    //be.iCodDependencia = "";
        //                //    be.sDependencia = "";
        //                //    //be.iCodTipoConcepto = dr.GetInt32(dr.GetOrdinal("iCodTipoConcepto"));
        //                //    be.sTipoConcepto = "";
        //                //    be.sClasificadorGasto = "";
        //                //    be.dcMontoTotalIng = lista_3.Where(x=>x.iCodTipoConcepto==1).Sum(x => x.dcMontoTotalIng);
        //                //    be.dcMontoTotalEgre = lista_3.Where(x => x.iCodTipoConcepto == 3).Sum(x => x.dcMontoTotalIng);
        //                //    be.lstObj = lista_3.ToList();
        //                //    lista_2.Add(be);
        //                //}

        //                //be = new PlanillaRpteResumenGeneralPorFTE_FTO_Partida_Meta();
        //                //be.dcTotalIng = lista.Where(x => x.iCodTipoConcepto == 1).Select(x => x.dcMontoIng).Sum();
        //                //be.dcTotalDscto = lista.Where(x => x.iCodTipoConcepto == 2).Select(x => x.dcMontoDscto).Sum();
        //                //be.dcTotalAportes = lista.Where(x => x.iCodTipoConcepto == 3).Select(x => x.dcMontoAporte).Sum();
        //                //be.dcTotalLiquido = be.dcTotalIng - be.dcTotalDscto;
        //                //lista.Add(be);
        //            }
        //        }

        //        if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

        //    }

        //    return lista;
        //}

        public IEnumerable<PlanillaRpteResumenGeneralResponse2> RRG3(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            List<PlanillaRpteResumenGeneralResponse2> lista = new List<PlanillaRpteResumenGeneralResponse2>();            
            PlanillaRpteResumenGeneralResponse2 be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

                if (iCodPlanilla==5)
                {
                    cmd.CommandText = "[dbo].[paPlanillaRpteGeneral2_FUNC]"; 
                }
                else
                {
                    cmd.CommandText = "[dbo].[paPlanillaRpteGeneral2]"; 
                }
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        //int cantLst = 0;
                        //int cantTipo1 = 0;
                        while (dr.Read())
                        {
                            be = new PlanillaRpteResumenGeneralResponse2();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();

                            be.iCodTipFuenteFinanciamiento = dr.GetInt32(dr.GetOrdinal("iCodTipFuenteFinanciamiento"));
                            be.sFuenteFinanciamiento = dr.GetString(dr.GetOrdinal("vFuenteFinanciamiento"));
                            be.sSec_Func = dr.GetString(dr.GetOrdinal("vSec_Func"));
                            be.sMetaPresupuestal = dr.GetString(dr.GetOrdinal("vMetaPresupuestal"));
                            be.iCodDependencia = dr.GetInt32(dr.GetOrdinal("iCodigoDependencia"));
                            be.sDependencia = dr.GetString(dr.GetOrdinal("vDependencia"));
                            be.iCodTipoConcepto = dr.GetInt32(dr.GetOrdinal("iCodTipoConcepto"));
                            be.sTipoConcepto = dr.GetString(dr.GetOrdinal("vTipoConcepto"));
                            be.sClasificadorGasto = dr.GetString(dr.GetOrdinal("vClasificadorGasto"));
                            be.dcMontoIng = dr.GetDecimal(dr.GetOrdinal("dcMontoIng"));
                            be.dcMontoEgre = dr.GetDecimal(dr.GetOrdinal("dcMontoEgre"));
                            be.dcMontoSaldo = dr.GetDecimal(dr.GetOrdinal("dcMontoSaldo"));                            
                            lista.Add(be);
                        }
                        
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        //public IEnumerable<PlanillaRpteResumenGeneralResponse2> RRG3_1(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, string sMes, string sPlanilla)
        //{
        //    List<PlanillaRpteResumenGeneralResponse2> lista = new List<PlanillaRpteResumenGeneralResponse2>();
        //    PlanillaRpteResumenGeneralResponse2 be = null;

        //    using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
        //    {
        //        if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

        //        if (iCodPlanilla == 5)
        //        {
        //            cmd.CommandText = "[dbo].[paPlanillaRpteGeneral2_FUNC]";
        //        }
        //        else
        //        {
        //            cmd.CommandText = "[dbo].[paPlanillaRpteGeneral2]";
        //        }
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
        //        cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
        //        cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
        //        cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
        //        cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
        //        //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

        //        using (SqlDataReader dr = cmd.ExecuteReader())
        //        {
        //            if (dr.HasRows)
        //            {
        //                //int cantLst = 0;
        //                //int cantTipo1 = 0;
        //                while (dr.Read())
        //                {
        //                    be = new PlanillaRpteResumenGeneralResponse2();
        //                    //item.Grilla = new Grilla_Response();
        //                    be.Grilla = new Grilla_Response();

        //                    be.iCodTipFuenteFinanciamiento = dr.GetInt32(dr.GetOrdinal("iCodTipFuenteFinanciamiento"));
        //                    be.sFuenteFinanciamiento = dr.GetString(dr.GetOrdinal("vFuenteFinanciamiento"));
        //                    be.sSec_Func = dr.GetString(dr.GetOrdinal("vSec_Func"));
        //                    be.sMetaPresupuestal = dr.GetString(dr.GetOrdinal("vMetaPresupuestal"));
        //                    be.iCodDependencia = dr.GetInt32(dr.GetOrdinal("iCodigoDependencia"));
        //                    be.sDependencia = dr.GetString(dr.GetOrdinal("vDependencia"));
        //                    be.iCodTipoConcepto = dr.GetInt32(dr.GetOrdinal("iCodTipoConcepto"));
        //                    be.sTipoConcepto = dr.GetString(dr.GetOrdinal("vTipoConcepto"));
        //                    be.sClasificadorGasto = dr.GetString(dr.GetOrdinal("vClasificadorGasto"));
        //                    be.dcMontoIng = dr.GetDecimal(dr.GetOrdinal("dcMontoIng"));
        //                    be.dcMontoEgre = dr.GetDecimal(dr.GetOrdinal("dcMontoEgre"));
        //                    be.dcMontoSaldo = dr.GetDecimal(dr.GetOrdinal("dcMontoSaldo"));
        //                    be.sMes = sMes;
        //                    be.sPlanilla = sPlanilla;
        //                    be.iAnio = iAnio;
        //                    lista.Add(be);
        //                }

        //            }
        //        }

        //        if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

        //    }

        //    return lista;
        //}

        public IEnumerable<PlanillaRpteResumenGeneralResponse2> RRG4(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            List<PlanillaRpteResumenGeneralResponse2> lista = new List<PlanillaRpteResumenGeneralResponse2>();
            PlanillaRpteResumenGeneralResponse2 be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

                if (iCodPlanilla == 5)
                {
                    cmd.CommandText = "[dbo].[paPlanillaRpteGeneral3_FUNC]";
                }
                else
                {
                    cmd.CommandText = "[dbo].[paPlanillaRpteGeneral3]";
                }
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        //int cantLst = 0;
                        //int cantTipo1 = 0;
                        while (dr.Read())
                        {
                            be = new PlanillaRpteResumenGeneralResponse2();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();

                            be.iCodTipFuenteFinanciamiento = dr.GetInt32(dr.GetOrdinal("iCodTipFuenteFinanciamiento"));
                            be.sFuenteFinanciamiento = dr.GetString(dr.GetOrdinal("vFuenteFinanciamiento"));
                            be.sSec_Func = dr.GetString(dr.GetOrdinal("vSec_Func"));
                            be.sMetaPresupuestal = dr.GetString(dr.GetOrdinal("vMetaPresupuestal"));
                            be.iCodDependencia = dr.GetInt32(dr.GetOrdinal("iCodigoDependencia"));
                            be.sDependencia = dr.GetString(dr.GetOrdinal("vDependencia"));
                            be.iCodTipoConcepto = dr.GetInt32(dr.GetOrdinal("iCodTipoConcepto"));
                            be.sTipoConcepto = dr.GetString(dr.GetOrdinal("vTipoConcepto"));
                            be.sClasificadorGasto = dr.GetString(dr.GetOrdinal("vClasificadorGasto"));
                            be.sRegLaboral = dr.GetString(dr.GetOrdinal("vRegLab"));
                            be.dcMontoIng = dr.GetDecimal(dr.GetOrdinal("dcMontoIng"));
                            be.dcMontoEgre = dr.GetDecimal(dr.GetOrdinal("dcMontoEgre"));
                            be.dcMontoSaldo = dr.GetDecimal(dr.GetOrdinal("dcMontoSaldo"));                            
                            be.sMes = sMes;
                            be.sPlanilla = sPlanilla;
                            be.iAnio = iAnio;
                            be.sNombreReporte = sNombreReporte;
                            lista.Add(be);
                        }

                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaRpteResumenGeneralResponse2> RRGEsSalud(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            List<PlanillaRpteResumenGeneralResponse2> lista = new List<PlanillaRpteResumenGeneralResponse2>();
            PlanillaRpteResumenGeneralResponse2 be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

                //if (iCodPlanilla == 5)
                //{
                //    cmd.CommandText = "[dbo].[paPlanillaRpteGeneral3_FUNC]";
                //}
                //else
                //{
                //    cmd.CommandText = "[dbo].[paPlanillaRpteGeneralEsSalud]";
                //}
                cmd.CommandText = "[dbo].[paPlanillaRpteGeneralEsSalud]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        //int cantLst = 0;
                        //int cantTipo1 = 0;
                        while (dr.Read())
                        {
                            be = new PlanillaRpteResumenGeneralResponse2();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();

                            be.iCodTipFuenteFinanciamiento = dr.GetInt32(dr.GetOrdinal("iCodTipFuenteFinanciamiento"));
                            be.sFuenteFinanciamiento = dr.GetString(dr.GetOrdinal("vFuenteFinanciamiento"));
                            be.sSec_Func = dr.GetString(dr.GetOrdinal("vSec_Func"));
                            be.sMetaPresupuestal = dr.GetString(dr.GetOrdinal("vMetaPresupuestal"));
                            be.iCodDependencia = dr.GetInt32(dr.GetOrdinal("iCodigoDependencia"));
                            be.sDependencia = dr.GetString(dr.GetOrdinal("vDependencia"));
                            be.iCodTipoConcepto = dr.GetInt32(dr.GetOrdinal("iCodTipoConcepto"));
                            be.sTipoConcepto = dr.GetString(dr.GetOrdinal("vTipoConcepto"));
                            be.sClasificadorGasto = dr.GetString(dr.GetOrdinal("vClasificadorGasto"));
                            be.dcMontoIng = dr.GetDecimal(dr.GetOrdinal("dcMontoIng"));
                            be.dcMontoEgre = dr.GetDecimal(dr.GetOrdinal("dcMontoEgre"));
                            be.dcMontoSaldo = dr.GetDecimal(dr.GetOrdinal("dcMontoSaldo"));
                            be.sMes = sMes;
                            be.sPlanilla = sPlanilla;
                            be.iAnio = iAnio;
                            be.sNombreReporte = sNombreReporte;
                            lista.Add(be);
                        }

                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaRpteResumenGeneralResponse2> ReporteDetalladoEsSalud(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            List<PlanillaRpteResumenGeneralResponse2> lista = new List<PlanillaRpteResumenGeneralResponse2>();
            PlanillaRpteResumenGeneralResponse2 be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                
                cmd.CommandText = "[dbo].[paPlanillaRpteDetalladoMetaClsfGastoEsSalud]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        //int cantLst = 0;
                        //int cantTipo1 = 0;
                        while (dr.Read())
                        {
                            be = new PlanillaRpteResumenGeneralResponse2();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();                            
                            be.sSec_Func = dr.GetString(dr.GetOrdinal("vSec_Func"));
                            be.sConcepto = dr.GetString(dr.GetOrdinal("vNombreConcepto"));
                            be.sClasificadorGasto = dr.GetString(dr.GetOrdinal("vClasificadorGasto"));
                            be.dcMontoIng = dr.GetDecimal(dr.GetOrdinal("dcMontoIng"));
                            be.sMes = sMes;
                            be.sPlanilla = sPlanilla;
                            be.iAnio = iAnio;
                            be.sNombreReporte = sNombreReporte;
                            lista.Add(be);
                        }

                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaRpteResumenGeneralResponse2> ReporteResumenRetencionesFteFto(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            List<PlanillaRpteResumenGeneralResponse2> lista = new List<PlanillaRpteResumenGeneralResponse2>();
            PlanillaRpteResumenGeneralResponse2 be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

                cmd.CommandText = "[dbo].[paPlanillaRpteFteFtoMetaPartidaConcepto]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        //int cantLst = 0;
                        //int cantTipo1 = 0;
                        while (dr.Read())
                        {
                            be = new PlanillaRpteResumenGeneralResponse2();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();
                            be.sFuenteFinanciamiento = dr.GetString(dr.GetOrdinal("vSiglas"));
                            be.sSec_Func = dr.GetString(dr.GetOrdinal("vSec_Func"));
                            be.sConcepto = dr.GetString(dr.GetOrdinal("vNombreConcepto"));
                            be.sClasificadorGasto = dr.GetString(dr.GetOrdinal("vClasificadorGasto"));
                            be.dcMontoIng = dr.GetDecimal(dr.GetOrdinal("dcMontoIng"));
                            be.sMes = sMes;
                            be.sPlanilla = sPlanilla;
                            be.iAnio = iAnio;
                            be.sNombreReporte = sNombreReporte;
                            lista.Add(be);
                        }

                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaRpteResumenGeneralResponse2> ReporteResumenIngEgreAporMetaPartidaConcepto(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, int iCodTipoConcepto, bool bJudicial, string sMes, string sPlanilla, string sNombreReporte)
        {
            List<PlanillaRpteResumenGeneralResponse2> lista = new List<PlanillaRpteResumenGeneralResponse2>();
            PlanillaRpteResumenGeneralResponse2 be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

                cmd.CommandText = "[dbo].[paPlanillaRpteIngEgreMetaPartidaConcepto]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoConcepto", iCodTipoConcepto));
                cmd.Parameters.Add(new SqlParameter("@bJudicial", bJudicial));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        //int cantLst = 0;
                        //int cantTipo1 = 0;
                        while (dr.Read())
                        {
                            be = new PlanillaRpteResumenGeneralResponse2();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();
                            be.sFuenteFinanciamiento = dr.GetString(dr.GetOrdinal("vSiglas"));
                            be.sSec_Func = dr.GetString(dr.GetOrdinal("vSec_Func"));
                            be.sConcepto = dr.GetString(dr.GetOrdinal("vNombreConcepto"));
                            be.sNumeroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            be.sApePaterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            be.sApeMaterno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            be.sNombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            be.sClasificadorGasto = dr.GetString(dr.GetOrdinal("vClasificadorGasto"));
                            be.sDependencia = dr.GetString(dr.GetOrdinal("vDependencia"));
                            be.dcMontoIng = dr.GetDecimal(dr.GetOrdinal("dcMontoIng"));
                            be.sMes = sMes;
                            be.sPlanilla = sPlanilla;
                            be.iAnio = iAnio;
                            be.sNombreReporte = sNombreReporte;
                            lista.Add(be);
                        }

                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaRpteResumenGeneralResponse2> ReporteResumenIngEgreAporMetaPartidaPorConcepto(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, int iCodTipoConcepto, bool bJudicial, string sMes, string sPlanilla, string sNombreReporte, int iCodConcepto)
        {
            List<PlanillaRpteResumenGeneralResponse2> lista = new List<PlanillaRpteResumenGeneralResponse2>();
            PlanillaRpteResumenGeneralResponse2 be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

                cmd.CommandText = "[dbo].[paPlanillaRpteIngEgreMetaPartidaPorConcepto]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoConcepto", iCodTipoConcepto));
                cmd.Parameters.Add(new SqlParameter("@bJudicial", bJudicial));
                cmd.Parameters.Add(new SqlParameter("@iCodConcepto", iCodConcepto));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        //int cantLst = 0;
                        //int cantTipo1 = 0;
                        while (dr.Read())
                        {
                            be = new PlanillaRpteResumenGeneralResponse2();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();
                            be.sFuenteFinanciamiento = dr.GetString(dr.GetOrdinal("vSiglas"));
                            be.sSec_Func = dr.GetString(dr.GetOrdinal("vSec_Func"));
                            be.sConcepto = dr.GetString(dr.GetOrdinal("vNombreConcepto"));
                            be.sNumeroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            be.sApePaterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            be.sApeMaterno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            be.sNombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            be.sClasificadorGasto = dr.GetString(dr.GetOrdinal("vClasificadorGasto"));
                            be.sDependencia = dr.GetString(dr.GetOrdinal("vDependencia"));
                            be.dcMontoIng = dr.GetDecimal(dr.GetOrdinal("dcMontoIng"));
                            be.sMes = sMes;
                            be.sPlanilla = sPlanilla;
                            be.iAnio = iAnio;
                            be.sNombreReporte = sNombreReporte;
                            lista.Add(be);
                        }

                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaRpteResumenGeneralResponse2> ReporteResumenGralAFPconNroAfiliados(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            List<PlanillaRpteResumenGeneralResponse2> lista = new List<PlanillaRpteResumenGeneralResponse2>();
            List<PlanillaRpteResumenGeneralResponse2> lista2 = new List<PlanillaRpteResumenGeneralResponse2>();
            PlanillaRpteResumenGeneralResponse2 be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

                cmd.CommandText = "[dbo].[paPlanillaRpteGralAFPconNroAfiliados]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));                
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        //int cantLst = 0;
                        //int cantTipo1 = 0;
                        while (dr.Read())
                        {
                            be = new PlanillaRpteResumenGeneralResponse2();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();
                            be.sSiglas = dr.GetString(dr.GetOrdinal("vSiglas"));
                            be.sFuenteFinanciamiento = dr.GetString(dr.GetOrdinal("vFuenteFinanciamiento"));
                            be.sSec_Func = dr.GetString(dr.GetOrdinal("vSec_Func"));
                            be.sConcepto = dr.GetString(dr.GetOrdinal("vNombreConcepto"));                            
                            be.sClasificadorGasto = dr.GetString(dr.GetOrdinal("vClasificadorGasto"));
                            be.sDependencia = dr.GetString(dr.GetOrdinal("vDependencia"));
                            be.sAFP = dr.GetString(dr.GetOrdinal("vAFP"));
                            be.iCodConcepto = dr.GetInt32(dr.GetOrdinal("iCodConcepto"));
                            be.dcMontoIng = dr.GetDecimal(dr.GetOrdinal("dcMonto"));

                            lista.Add(be);
                        }

                        var lstAFP = lista.Select(x => new { _sSiglas = x.sSiglas, _sFuenteFinanciamiento = x.sFuenteFinanciamiento, _sAFP = x.sAFP }).GroupBy(x => x._sAFP).ToList();
                        //var lstConcepto = lista.GroupBy(x => x._sAFP).ToList()
                            
                        for (int i = 0; i < lstAFP.Count; i++)
                        {
                            be = new PlanillaRpteResumenGeneralResponse2();
                            be.sSiglas = lstAFP[i].FirstOrDefault()._sSiglas;
                            be.sFuenteFinanciamiento = lstAFP[i].FirstOrDefault()._sFuenteFinanciamiento;
                            be.sAFP = lstAFP[i].FirstOrDefault()._sAFP;
                            be.dcMontoTotalIng = lista.Where(x => x.sAFP == lstAFP[i].FirstOrDefault()._sAFP && x.iCodConcepto == 21).Sum(x => x.dcMontoIng);
                            be.dcMontoEgre = lista.Where(x => x.sAFP == lstAFP[i].FirstOrDefault()._sAFP && x.iCodConcepto == 22).Sum(x => x.dcMontoIng);
                            be.dcMontoSaldo = lista.Where(x => x.sAFP == lstAFP[i].FirstOrDefault()._sAFP && x.iCodConcepto == 23).Sum(x => x.dcMontoIng);
                            be.dcMontoTotalEgre = be.dcMontoEgre + be.dcMontoSaldo;
                            be.dcMontoTotalSaldo = be.dcMontoTotalIng + be.dcMontoTotalEgre;
                            be.sMes = sMes;
                            be.sPlanilla = sPlanilla;
                            be.iAnio = iAnio;
                            be.sNombreReporte = sNombreReporte;
                            lista2.Add(be);
                        }                        
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista2;
        }

        public IEnumerable<PlanillaRpteResumenGeneralResponse2> ReporteResumenGralAFPconAFPDepMetaClasGasto(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            List<PlanillaRpteResumenGeneralResponse2> lista = new List<PlanillaRpteResumenGeneralResponse2>();
            List<PlanillaRpteResumenGeneralResponse2> lista2 = new List<PlanillaRpteResumenGeneralResponse2>();
            PlanillaRpteResumenGeneralResponse2 be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

                cmd.CommandText = "[dbo].[paPlanillaRpteGralAFPporAFP]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        //int cantLst = 0;
                        //int cantTipo1 = 0;
                        while (dr.Read())
                        {
                            be = new PlanillaRpteResumenGeneralResponse2();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();
                            be.sSiglas = dr.GetString(dr.GetOrdinal("vSiglas"));
                            be.sFuenteFinanciamiento = dr.GetString(dr.GetOrdinal("vFuenteFinanciamiento"));
                            be.sSec_Func = dr.GetString(dr.GetOrdinal("vSec_Func"));
                            be.sConcepto = dr.GetString(dr.GetOrdinal("vNombreConcepto"));
                            be.sClasificadorGasto = dr.GetString(dr.GetOrdinal("vClasificadorGasto"));
                            be.sDependencia = dr.GetString(dr.GetOrdinal("vDependencia"));
                            be.sAFP = dr.GetString(dr.GetOrdinal("vAFP"));
                            be.iCodConcepto = dr.GetInt32(dr.GetOrdinal("iCodConcepto"));
                            be.dcMontoIng = dr.GetDecimal(dr.GetOrdinal("dcMontoAportaciones"));
                            be.dcMontoEgre = dr.GetDecimal(dr.GetOrdinal("dcMontoComision"));
                            be.dcMontoSaldo = dr.GetDecimal(dr.GetOrdinal("dcMontoPrima"));

                            lista.Add(be);
                        }

                        //var lstAFP = lista.Select(x => new { _sSiglas = x.sSiglas, _sFuenteFinanciamiento = x.sFuenteFinanciamiento, _sAFP = x.sAFP, _sDependencia = x.sDependencia, _sSec_Func = x.sSec_Func, _sClasificadorGasto = x.sClasificadorGasto }).GroupBy(x => x._sAFP).ToList();
                        var lstAFP = lista.Select(x => new { _sAFP = x.sAFP}).GroupBy(x => x._sAFP).ToList();
                        var lstSiglas = lista.Select(x => new { _sSiglas = x.sSiglas }).GroupBy(x => x._sSiglas).ToList();
                        var lstFuenteFinanciamiento = lista.Select(x => new { _sFuenteFinanciamiento = x.sFuenteFinanciamiento }).GroupBy(x => x._sFuenteFinanciamiento).ToList();
                        var lstDependencia = lista.Select(x => new { _sDependencia = x.sDependencia }).GroupBy(x => x._sDependencia).ToList();
                        var lstSec_Func = lista.Select(x => new { _sSec_Func = x.sSec_Func }).GroupBy(x => x._sSec_Func).ToList();
                        var lstClasificadorGasto = lista.Select(x => new { _sClasificadorGasto = x.sClasificadorGasto }).GroupBy(x => x._sClasificadorGasto).ToList();

                        //var lst = lista
                        //    .Select(x => new { _sSiglas = x.sSiglas, _sFuenteFinanciamiento = x.sFuenteFinanciamiento, _sAFP = x.sAFP, _sDependencia = x.sDependencia, _sSec_Func = x.sSec_Func, _sClasificadorGasto = x.sClasificadorGasto })
                        //    .Distinct().ToList();
                            
                        //var lstAFP = lista.GroupBy(x => x.sAFP).Select(x => new { _sSiglas = x., _sFuenteFinanciamiento = x.sFuenteFinanciamiento, _sAFP = x.sAFP, _sDependencia = x.sDependencia, _sSec_Func = x.sSec_Func, _sClasificadorGasto = x.sClasificadorGasto }).ToList();
                        //var lstConcepto = lista.GroupBy(x => x._sAFP).ToList()
                        foreach (var itemFF in lstFuenteFinanciamiento)
                        {
                            foreach (var itemAFP in lstAFP)
                            {
                                foreach (var itemDependencia in lstDependencia)
                                {
                                    foreach (var itemSec_Func in lstSec_Func)
                                    {
                                        var existe = lista.Where(x => x.sAFP == itemAFP.FirstOrDefault()._sAFP && x.sFuenteFinanciamiento == itemFF.FirstOrDefault()._sFuenteFinanciamiento && x.sDependencia == itemDependencia.FirstOrDefault()._sDependencia && x.sSec_Func == itemSec_Func.FirstOrDefault()._sSec_Func).FirstOrDefault();
                                        if (existe!=null)
                                        {
                                            be = new PlanillaRpteResumenGeneralResponse2();
                                            be.sSiglas = "";
                                            be.sFuenteFinanciamiento = itemFF.FirstOrDefault()._sFuenteFinanciamiento;
                                            be.sAFP = itemAFP.FirstOrDefault()._sAFP;
                                            be.sSec_Func = itemSec_Func.FirstOrDefault()._sSec_Func;
                                            be.sDependencia = itemDependencia.FirstOrDefault()._sDependencia;
                                            be.sClasificadorGasto = lstClasificadorGasto.FirstOrDefault().FirstOrDefault()._sClasificadorGasto;
                                            be.dcMontoTotalIng = lista.Where(x => x.sAFP == itemAFP.FirstOrDefault()._sAFP && x.sFuenteFinanciamiento == itemFF.FirstOrDefault()._sFuenteFinanciamiento && x.sDependencia == itemDependencia.FirstOrDefault()._sDependencia && x.sSec_Func == itemSec_Func.FirstOrDefault()._sSec_Func).Sum(x => x.dcMontoIng);
                                            be.dcMontoEgre = lista.Where(x => x.sAFP == itemAFP.FirstOrDefault()._sAFP && x.sFuenteFinanciamiento == itemFF.FirstOrDefault()._sFuenteFinanciamiento && x.sDependencia == itemDependencia.FirstOrDefault()._sDependencia && x.sSec_Func == itemSec_Func.FirstOrDefault()._sSec_Func).Sum(x => x.dcMontoEgre);
                                            be.dcMontoSaldo = lista.Where(x => x.sAFP == itemAFP.FirstOrDefault()._sAFP && x.sFuenteFinanciamiento == itemFF.FirstOrDefault()._sFuenteFinanciamiento && x.sDependencia == itemDependencia.FirstOrDefault()._sDependencia && x.sSec_Func == itemSec_Func.FirstOrDefault()._sSec_Func).Sum(x => x.dcMontoSaldo);
                                            be.dcMontoTotalEgre = be.dcMontoEgre + be.dcMontoSaldo;
                                            be.dcMontoTotalSaldo = be.dcMontoTotalIng + be.dcMontoTotalEgre;
                                            be.sMes = sMes;
                                            be.sPlanilla = sPlanilla;
                                            be.iAnio = iAnio;
                                            be.sNombreReporte = sNombreReporte;
                                            lista2.Add(be);
                                        }
                                        
                                    }
                                }
                            }
                        }
                        //for (int i = 0; i < lstAFP.Count; i++)
                        //{

                        //    foreach (var item in lstAFP[i].ToList())
                        //    {
                        //        be = new PlanillaRpteResumenGeneralResponse2();
                        //        be.sSiglas = "";
                        //        be.sFuenteFinanciamiento = "";
                        //        be.sAFP = item._sAFP;
                        //        be.sSec_Func = "";
                        //        be.sDependencia = "";
                        //        be.sClasificadorGasto = "";
                        //        //be.dcMontoTotalIng = lista.Where(x => x.sAFP == item._sAFP && x.sFuenteFinanciamiento == item._sFuenteFinanciamiento && x.sDependencia == item._sDependencia && x.sSec_Func == item._sSec_Func).Sum(x => x.dcMontoIng);
                        //        //be.dcMontoEgre = lista.Where(x => x.sAFP == item._sAFP && x.sFuenteFinanciamiento == item._sFuenteFinanciamiento && x.sDependencia == item._sDependencia && x.sSec_Func == item._sSec_Func).Sum(x => x.dcMontoEgre);
                        //        //be.dcMontoSaldo = lista.Where(x => x.sAFP == item._sAFP && x.sFuenteFinanciamiento == item._sFuenteFinanciamiento && x.sDependencia == item._sDependencia && x.sSec_Func == item._sSec_Func).Sum(x => x.dcMontoSaldo);
                        //        be.dcMontoTotalEgre = be.dcMontoEgre + be.dcMontoSaldo;
                        //        be.dcMontoTotalSaldo = be.dcMontoTotalIng + be.dcMontoTotalEgre;
                        //        lista2.Add(be);
                        //    }                            
                        //}
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista2.OrderBy(x=>x.sSec_Func).ToList();
        }

        public IEnumerable<PlanillaRpteResumenGeneralResponse2> ReporteResumenGralBcosPorInstCantTotIngTotEgre(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            List<PlanillaRpteResumenGeneralResponse2> lista = new List<PlanillaRpteResumenGeneralResponse2>();            
            PlanillaRpteResumenGeneralResponse2 be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

                cmd.CommandText = "[dbo].[paPlanillaRpteGralBcos]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));                

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {                        
                        while (dr.Read())
                        {
                            be = new PlanillaRpteResumenGeneralResponse2();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();
                            be.sEntidadBancaria = dr.GetString(dr.GetOrdinal("vBanco"));
                            be.iCantTrabEntBancaria = dr.GetInt32(dr.GetOrdinal("iCantidad"));
                            be.dcMontoTotalIng = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));
                            be.dcMontoTotalEgre = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));
                            be.dcMontoTotalSaldo = be.dcMontoTotalIng - be.dcMontoTotalEgre;
                            be.sMes = sMes;
                            be.sPlanilla = sPlanilla;
                            be.iAnio = iAnio;
                            be.sNombreReporte = sNombreReporte;
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }
            return lista;
        }

        public IEnumerable<PlanillaRpteResumenGeneralResponse2> ReporteResumenGralBcosFteFtoMeta(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            List<PlanillaRpteResumenGeneralResponse2> lista = new List<PlanillaRpteResumenGeneralResponse2>();
            PlanillaRpteResumenGeneralResponse2 be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

                cmd.CommandText = "[dbo].[paPlanillaRpteGralFteFtoMetaBcos]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaRpteResumenGeneralResponse2();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();
                            be.sEntidadBancaria = dr.GetString(dr.GetOrdinal("vBanco"));
                            be.sSiglas = dr.GetString(dr.GetOrdinal("vSiglas"));
                            be.sFuenteFinanciamiento = dr.GetString(dr.GetOrdinal("vFuenteFinanciamiento"));
                            be.sSec_Func = dr.GetString(dr.GetOrdinal("vSec_Func"));                            
                            be.sClasificadorGasto = dr.GetString(dr.GetOrdinal("vClasificadorGasto"));
                            be.sMetaPresupuestal = dr.GetString(dr.GetOrdinal("vMetaPresupuestal"));
                            be.sDependencia = dr.GetString(dr.GetOrdinal("vDependencia"));
                            be.iCantTrabEntBancaria = dr.GetInt32(dr.GetOrdinal("iCantidad"));
                            be.dcMontoTotalIng = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));
                            be.dcMontoTotalEgre = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));
                            be.dcMontoTotalSaldo = be.dcMontoTotalIng - be.dcMontoTotalEgre;
                            be.sMes = sMes;
                            be.sPlanilla = sPlanilla;
                            be.iAnio = iAnio;
                            be.sNombreReporte = sNombreReporte;
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }
            return lista;
        }

        public IEnumerable<PlanillaRpteResumenGeneralResponse2> ReporteResumenDetalladoBcos(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            List<PlanillaRpteResumenGeneralResponse2> lista = new List<PlanillaRpteResumenGeneralResponse2>();
            PlanillaRpteResumenGeneralResponse2 be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

                cmd.CommandText = "[dbo].[paPlanillaRpteDetalladoBcos]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaRpteResumenGeneralResponse2();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();
                            be.sEntidadBancaria = dr.GetString(dr.GetOrdinal("vBanco"));
                            be.sNumeroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            be.sNombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            be.sApePaterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            be.sApeMaterno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            be.sCuenta = dr.GetString(dr.GetOrdinal("vCuenta"));
                            be.sCCI = dr.GetString(dr.GetOrdinal("vCCI"));
                            be.dcMontoTotalIng = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIngresos"));
                            be.dcMontoTotalEgre = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalDescuentos"));
                            be.dcMontoTotalSaldo = be.dcMontoTotalIng - be.dcMontoTotalEgre;
                            be.sMes = sMes;
                            be.sPlanilla = sPlanilla;
                            be.iAnio = iAnio;
                            be.sNombreReporte = sNombreReporte;
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }
            return lista;
        }

        public IEnumerable<PlanillaRpteResumenGeneralResponse2> ReportePlanillaUnicaPagos(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            List<PlanillaRpteResumenGeneralResponse2> lista = new List<PlanillaRpteResumenGeneralResponse2>();
            PlanillaRpteResumenGeneralResponse2 be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();


                cmd.CommandText = "[dbo].[paPlanillaRptePlanillaUnicaPagos]";
                
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        //int cantLst = 0;
                        //int cantTipo1 = 0;
                        while (dr.Read())
                        {
                            be = new PlanillaRpteResumenGeneralResponse2();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();
                                                        
                            be.sSec_Func = dr.GetString(dr.GetOrdinal("vSec_Func"));
                            be.sMetaPresupuestal = dr.GetString(dr.GetOrdinal("vMetaPresupuestal"));
                            be.iCodDependencia = dr.GetInt32(dr.GetOrdinal("iCodigoDependencia"));
                            be.sDependencia = dr.GetString(dr.GetOrdinal("vDependencia"));
                            be.sNumeroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            be.sApePaterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            be.sApeMaterno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            be.sNombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            be.sCargo = dr.GetString(dr.GetOrdinal("vCargo"));
                            be.sFechaInicioLab = dr.GetString(dr.GetOrdinal("dtInicioLabores"));
                            be.sCondTrabajador = dr.GetString(dr.GetOrdinal("vCondTrabajador"));
                            be.sCuenta = dr.GetString(dr.GetOrdinal("vCuenta"));
                            be.iDiasLaborados = dr.GetInt32(dr.GetOrdinal("iDiasLaborados"));
                            be.sAFP = dr.GetString(dr.GetOrdinal("vAFP"));
                            be.iCodTipoConcepto = dr.GetInt32(dr.GetOrdinal("iCodTipoConcepto"));
                            be.sTipoConcepto = dr.GetString(dr.GetOrdinal("vTipoConcepto"));
                            be.sConcepto = dr.GetString(dr.GetOrdinal("vConcepto"));
                            be.dcMontoIng = dr.GetDecimal(dr.GetOrdinal("dcMontoIng"));
                            be.dcMontoTotal = dr.GetDecimal(dr.GetOrdinal("dcMontoTotal"));
                            be.sMes = sMes;
                            be.sPlanilla = sPlanilla;
                            be.iAnio = iAnio;
                            be.sNombreReporte = sNombreReporte;
                            lista.Add(be);
                        }

                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }



        public IEnumerable<PlanillaRpteEstructuraInfoAFPNETResponse> ReporteEstructuraInfoAFPNET(int iMes, int iAnio, string sNombreReporte, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            List<PlanillaRpteEstructuraInfoAFPNETResponse> lista = new List<PlanillaRpteEstructuraInfoAFPNETResponse>();
            PlanillaRpteEstructuraInfoAFPNETResponse be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

                cmd.CommandText = "[dbo].[paPlanillaRpteEstrucCargaInfPortalAFPNET]";
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));  

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaRpteEstructuraInfoAFPNETResponse();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();

                            be.sCuspp = dr.GetString(dr.GetOrdinal("vCuspp"));
                            be.iCodigoTipoDocumento = dr.GetInt32(dr.GetOrdinal("iCodigoTipoDocumento")); ;
                            be.sNumeroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            be.sNombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            be.sApePaterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            be.sApeMaterno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            be.sRelacionLaboral = dr.GetString(dr.GetOrdinal("vRelacionLaboral"));
                            be.sInicioRL = dr.GetString(dr.GetOrdinal("vInicioRL"));
                            be.sCeseRL = dr.GetString(dr.GetOrdinal("vCeseRL"));
                            be.sExcepcionAportar = dr.GetString(dr.GetOrdinal("vExcepcionAportar"));
                            be.dcRemuneracion = dr.GetDecimal(dr.GetOrdinal("dRemuneracion"));
                            be.dcAporteVoluntario = dr.GetDecimal(dr.GetOrdinal("dcAporteVoluntario"));
                            be.sTipoTrabajo = dr.GetString(dr.GetOrdinal("vTipoTrabajo"));
                            be.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            //be.sMes = sMes;                            
                            be.iAnio = iAnio;
                            be.sNombreReporte = sNombreReporte;
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }
            return lista;
        }

        public IEnumerable<PlanillaRpteEstructuraInfoAFPNETResponse> ReporteTrabajadoresAfiliadosONPE(int iMes, int iAnio, string sNombreReporte)
        {
            List<PlanillaRpteEstructuraInfoAFPNETResponse> lista = new List<PlanillaRpteEstructuraInfoAFPNETResponse>();
            PlanillaRpteEstructuraInfoAFPNETResponse be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

                cmd.CommandText = "[dbo].[paPlanillaRpteAfiliadosONP]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaRpteEstructuraInfoAFPNETResponse();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();
                                                        
                            be.sNumeroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            be.sNombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            be.sApePaterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            be.sApeMaterno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));                            
                            be.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            //be.sMes = sMes;                            
                            be.iAnio = iAnio;
                            be.sNombreReporte = sNombreReporte;
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }
            return lista;
        }

        public IEnumerable<PlanillaRpteResumenGeneralAnualResponse> ReporteInfoPortalTranspEstandar(int iMes, int iAnio, string sMes, string sNombreReporte)
        {
            List<PlanillaRpteResumenGeneralAnualResponse> lista = new List<PlanillaRpteResumenGeneralAnualResponse>();
            PlanillaRpteResumenGeneralAnualResponse be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

                cmd.CommandText = "[dbo].[paPlanillaRpteInfPortalTransEstandar]";
                cmd.CommandType = CommandType.StoredProcedure;                
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));                

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaRpteResumenGeneralAnualResponse();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();

                            be.sRUC = dr.GetString(dr.GetOrdinal("vRUC"));
                            be.sNumeroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            be.iCodTipoCondicionTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTipoCondicionTrabajador"));
                            be.sNombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            be.sApePaterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            be.sApeMaterno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            be.sCargo = dr.GetString(dr.GetOrdinal("vCargo"));
                            be.sDependencia = dr.GetString(dr.GetOrdinal("vDependencia"));
                            be.dcRemuneracion = dr.GetDecimal(dr.GetOrdinal("dcRemuneracion"));
                            be.dcTotalIng = dr.GetDecimal(dr.GetOrdinal("dcTotalIng"));
                            be.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            be.sMes = sMes;                            
                            be.iAnio = dr.GetInt32(dr.GetOrdinal("iAnio")); ;
                            be.sNombreReporte = sNombreReporte;
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }
            return lista;
        }

        public IEnumerable<PlanillaRpteResumenGeneralAnualResponse> ReporteResumenPlanillaMensIngEgreEsSalud(int iAnio, string sNombreReporte, int iCodTipoConcepto)
        {
            List<PlanillaRpteResumenGeneralAnualResponse> lista = new List<PlanillaRpteResumenGeneralAnualResponse>();
            PlanillaRpteResumenGeneralAnualResponse be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

                cmd.CommandText = "[dbo].[paPlanillaRpteResAnualPorTipoPlanilla]";
                cmd.CommandType = CommandType.StoredProcedure;                
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoConcepto", iCodTipoConcepto));
                

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaRpteResumenGeneralAnualResponse();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();
                            be.iCodPlanilla = dr.GetInt32(dr.GetOrdinal("iCodPlanilla"));
                            //be.sNumeroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            //be.sNombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            //be.sApePaterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            //be.sApeMaterno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            be.iCodTipoConcepto = dr.GetInt32(dr.GetOrdinal("iCodTipoConcepto"));
                            be.sTipoConcepto = dr.GetString(dr.GetOrdinal("vTipoConcepto"));
                            be.dcTotalIng = dr.GetDecimal(dr.GetOrdinal("dcMontoIng"));

                            be.sMes = dr.GetString(dr.GetOrdinal("vMes"));
                            be.sPlanilla = dr.GetString(dr.GetOrdinal("vPlanilla"));
                            be.iCodDetPlanilla = dr.GetInt32(dr.GetOrdinal("iCodDetPlanilla"));
                            be.sTipoPlanilla = dr.GetString(dr.GetOrdinal("vTipoPlanilla"));
                            be.iAnio = iAnio;
                            be.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            be.sNombreReporte = sNombreReporte;
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }
            return lista;
        }

        public IEnumerable<PlanillaRpteResumenGeneralAnualResponse> ReporteResumenAnuaPorTrab(int iAnio, string sNombreReporte, string sDNI)
        {
            List<PlanillaRpteResumenGeneralAnualResponse> lista = new List<PlanillaRpteResumenGeneralAnualResponse>();
            PlanillaRpteResumenGeneralAnualResponse be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

                cmd.CommandText = "[dbo].[paPlanillaRpteResAnualTrabDependencia]";
                cmd.CommandType = CommandType.StoredProcedure;

                
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@sDNI", sDNI));
                

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaRpteResumenGeneralAnualResponse();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();

                            be.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            be.sNumeroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            be.sNombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            be.sApePaterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            be.sApeMaterno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));                            
                            be.sNroCta = dr.GetString(dr.GetOrdinal("vCuenta"));
                            be.sDependencia = dr.GetString(dr.GetOrdinal("vDependencia"));
                            be.sCargo = dr.GetString(dr.GetOrdinal("vCargo"));
                            be.iCodTipoConcepto = dr.GetInt32(dr.GetOrdinal("iCodTipoConcepto"));
                            be.sTipoConcepto = dr.GetString(dr.GetOrdinal("vTipoConcepto"));
                            be.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            be.sMes = dr.GetString(dr.GetOrdinal("vMes"));
                            be.iAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            be.sConcepto = dr.GetString(dr.GetOrdinal("vConcepto"));
                            be.dcTotalIng = dr.GetDecimal(dr.GetOrdinal("dcMontoIng"));
                            be.sNombreReporte = sNombreReporte;
                            lista.Add(be);
                        }
                        be = new PlanillaRpteResumenGeneralAnualResponse();                        
                        be.Grilla = new Grilla_Response();



                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }
            return lista;
        }

        public IEnumerable<PlanillaRpteResumenGeneralAnualResponse> ReporteResumenIngRent4ta(int iAnio, string sNombreReporte)
        {
            List<PlanillaRpteResumenGeneralAnualResponse> lista = new List<PlanillaRpteResumenGeneralAnualResponse>();
            PlanillaRpteResumenGeneralAnualResponse be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

                cmd.CommandText = "[dbo].[paPlanillaRpteIngRenta4taCat]";
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));                

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaRpteResumenGeneralAnualResponse();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();
                            
                            be.sNumeroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            be.sNombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            be.sApePaterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            be.sApeMaterno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            be.sSec_Func = dr.GetString(dr.GetOrdinal("vSec_Func"));
                            be.sSiglas = dr.GetString(dr.GetOrdinal("vSiglas"));
                            be.dcRemuneracion = dr.GetDecimal(dr.GetOrdinal("dcRemuneracion"));
                            be.dcRet4ta = dr.GetDecimal(dr.GetOrdinal("dcRet4ta"));
                            be.iAnio = iAnio;
                            be.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            be.sMes = dr.GetString(dr.GetOrdinal("vMes"));
                            be.sNombreReporte = sNombreReporte;
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }
            return lista;
        }

        public IEnumerable<ArchivosTXT_Response> GenerarArchivosTXT(ArchivosTXT_Response objArchivosTXT_Response)
        {
            List<ArchivosTXT_Response> lista = new List<ArchivosTXT_Response>();
            ArchivosTXT_Response be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                
                foreach (String item in objArchivosTXT_Response.formatos)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "[dbo].[paPlanillaGenerarTXT]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", item.Split('|')[0]));
                    cmd.Parameters.Add(new SqlParameter("@iMes", item.Split('|')[2]));
                    cmd.Parameters.Add(new SqlParameter("@iAnio", item.Split('|')[3]));
                    cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", item.Split('|')[1]));
                    cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", item.Split('|')[4]));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            //int cantLst = 0;
                            //int cantTipo1 = 0;
                            while (dr.Read())
                            {
                                be = new ArchivosTXT_Response();
                                //item.Grilla = new Grilla_Response();
                                be.Grilla = new Grilla_Response();
                                be.sCodPlanilla = dr.GetInt32(dr.GetOrdinal("iCodPlanilla")).ToString();
                                be.sCodTipoDocumento = dr.GetString(dr.GetOrdinal("vCodigoTipoDocumento"));
                                be.sNumeroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                                be.sCodTipFuenteFinanciamiento = dr.GetString(dr.GetOrdinal("vCodTipFuenteFinanciamiento"));
                                be.sFuenteFinanciamiento = dr.GetString(dr.GetOrdinal("vFuenteFinanciamiento"));
                                be.sCodTipoConcepto = dr.GetString(dr.GetOrdinal("vCodTipoConcepto"));
                                be.sTipoConcepto = dr.GetString(dr.GetOrdinal("vTipoConcepto"));
                                be.sCodConcepto = dr.GetString(dr.GetOrdinal("vCodConcepto"));
                                be.sConcepto = dr.GetString(dr.GetOrdinal("vConcepto"));
                                //be.sNombreConcepto = dr.GetString(dr.GetOrdinal("vNombreConcepto"));
                                be.dcMonto = dr.GetDecimal(dr.GetOrdinal("dcMonto"));
                                be.sHorasJOR = dr.GetString(dr.GetOrdinal("vHorasJOR"));
                                be.sCodigoTOC = dr.GetString(dr.GetOrdinal("vCodigoTOC"));
                                be.sCodigoExterno = dr.GetString(dr.GetOrdinal("vCodigoExterno"));
                                be.sCodigoAIRHSP = dr.GetString(dr.GetOrdinal("vCodigoAIRHSP"));
                                be.sCodTipRegSISPER = dr.GetString(dr.GetOrdinal("vCodTipRegSISPER"));
                                be.sCodMCPP = dr.GetString(dr.GetOrdinal("vCodigoMCPP"));
                                be.iCodTipoCondicionTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTipoCondicionTrabajador"));

                                be.iVacaciones = dr.GetInt32(dr.GetOrdinal("iDiasVacaciones"));
                                be.iLicencia_sin_Goce = dr.GetInt32(dr.GetOrdinal("iDiasLicencia"));
                                be.iSancion_Disciplinaria = dr.GetInt32(dr.GetOrdinal("iSancionDisciplinaria"));
                                be.iEnfermedad_o_Accidente = dr.GetInt32(dr.GetOrdinal("iEnfermedadAccidente"));
                                be.iLicencia_con_Goce = dr.GetInt32(dr.GetOrdinal("iLicenciaConGoce"));
                                be.iLicencia_por_Paternidad = dr.GetInt32(dr.GetOrdinal("iLicenciaPaternidad"));
                                be.iLicencia_por_Fallecimiento_de_Padres = dr.GetInt32(dr.GetOrdinal("iLicenciaFallecimientoPadres"));
                                be.iLicencia_por_Enfermedad_Grave_o_Terminal_o_Accidente_Grave_de_Familiares_Directos = dr.GetInt32(dr.GetOrdinal("iLicenciaEnfermedadGraveFam"));

                                lista.Add(be);
                            }
                                
                        }
                    }
                }                

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<ArchivosTXT_Response> GenerarArchivosTXTMCPPWEB(ArchivosTXT_Response objArchivosTXT_Response)
        {
            List<ArchivosTXT_Response> lista = new List<ArchivosTXT_Response>();
            ArchivosTXT_Response be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

                foreach (String item in objArchivosTXT_Response.formatos)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "[dbo].[paPlanillaGenerarTXTMCPPWEB]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", item.Split('|')[0]));
                    cmd.Parameters.Add(new SqlParameter("@iMes", item.Split('|')[2]));
                    cmd.Parameters.Add(new SqlParameter("@iAnio", item.Split('|')[3]));
                    cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", item.Split('|')[1]));
                    cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", item.Split('|')[4]));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            //int cantLst = 0;
                            //int cantTipo1 = 0;
                            while (dr.Read())
                            {
                                be = new ArchivosTXT_Response();
                                //item.Grilla = new Grilla_Response();
                                be.Grilla = new Grilla_Response();
                                be.sCodPlanilla = dr.GetInt32(dr.GetOrdinal("iCodPlanilla")).ToString();
                                be.sCodTipoDocumento = dr.GetString(dr.GetOrdinal("vCodigoTipoDocumento"));
                                be.sNumeroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                                be.sCodTipFuenteFinanciamiento = dr.GetString(dr.GetOrdinal("vCodTipFuenteFinanciamiento"));
                                be.sFuenteFinanciamiento = dr.GetString(dr.GetOrdinal("vFuenteFinanciamiento"));
                                be.sCodTipoConcepto = dr.GetString(dr.GetOrdinal("vCodTipoConcepto"));
                                be.sTipoConcepto = dr.GetString(dr.GetOrdinal("vTipoConcepto"));
                                be.sCodConcepto = dr.GetString(dr.GetOrdinal("vCodConcepto"));
                                be.sConcepto = dr.GetString(dr.GetOrdinal("vConcepto"));
                                //be.sNombreConcepto = dr.GetString(dr.GetOrdinal("vNombreConcepto"));
                                be.dcMonto = dr.GetDecimal(dr.GetOrdinal("dcMonto"));
                                be.sHorasJOR = dr.GetString(dr.GetOrdinal("vHorasJOR"));
                                be.sCodigoTOC = dr.GetString(dr.GetOrdinal("vCodigoTOC"));
                                be.sCodigoExterno = dr.GetString(dr.GetOrdinal("vCodigoExterno"));
                                be.sCodigoAIRHSP = dr.GetString(dr.GetOrdinal("vCodigoAIRHSP"));
                                be.sCodTipRegSISPER = dr.GetString(dr.GetOrdinal("vCodTipRegSISPER"));
                                be.sCodMCPP = dr.GetString(dr.GetOrdinal("vCodigoMCPP"));
                                be.iCodTipoCondicionTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTipoCondicionTrabajador"));
                                lista.Add(be);
                            }

                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<ArchivosTXT_Response> GenerarArchivosTXTMCPPWEBJUDICIAL(ArchivosTXT_Response objArchivosTXT_Response)
        {
            List<ArchivosTXT_Response> lista = new List<ArchivosTXT_Response>();
            ArchivosTXT_Response be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

                foreach (String item in objArchivosTXT_Response.formatos)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "[dbo].[paPlanillaGenerarTXTMCPPWEBDSCTOJUD]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", item.Split('|')[0]));
                    cmd.Parameters.Add(new SqlParameter("@iMes", item.Split('|')[2]));
                    cmd.Parameters.Add(new SqlParameter("@iAnio", item.Split('|')[3]));
                    cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", item.Split('|')[1]));
                    cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", item.Split('|')[4]));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            //int cantLst = 0;
                            //int cantTipo1 = 0;
                            while (dr.Read())
                            {
                                be = new ArchivosTXT_Response();
                                //item.Grilla = new Grilla_Response();
                                be.Grilla = new Grilla_Response();
                                be.sCodPlanilla = dr.GetInt32(dr.GetOrdinal("iCodPlanilla")).ToString();
                                be.sCodTipoDocumento = dr.GetString(dr.GetOrdinal("vCodigoTipoDocumento"));
                                be.sNumeroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                                be.sCodTipFuenteFinanciamiento = dr.GetString(dr.GetOrdinal("vCodTipFuenteFinanciamiento"));
                                be.sFuenteFinanciamiento = dr.GetString(dr.GetOrdinal("vFuenteFinanciamiento"));
                                be.sCodTipoConcepto = dr.GetString(dr.GetOrdinal("vCodTipoConcepto"));
                                be.sTipoConcepto = dr.GetString(dr.GetOrdinal("vTipoConcepto"));
                                be.sCodConcepto = dr.GetString(dr.GetOrdinal("vCodConcepto"));
                                be.sConcepto = dr.GetString(dr.GetOrdinal("vConcepto"));
                                //be.sNombreConcepto = dr.GetString(dr.GetOrdinal("vNombreConcepto"));
                                be.dcMonto = dr.GetDecimal(dr.GetOrdinal("dcMonto"));
                                be.sHorasJOR = dr.GetString(dr.GetOrdinal("vHorasJOR"));
                                be.sCodigoTOC = dr.GetString(dr.GetOrdinal("vCodigoTOC"));
                                be.sCodigoExterno = dr.GetString(dr.GetOrdinal("vCodigoExterno"));
                                be.sCodigoAIRHSP = dr.GetString(dr.GetOrdinal("vCodigoAIRHSP"));
                                be.sCodTipRegSISPER = dr.GetString(dr.GetOrdinal("vCodTipRegSISPER"));
                                be.sCodMCPP = dr.GetString(dr.GetOrdinal("vCodigoMCPP"));
                                be.iCodTipoCondicionTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTipoCondicionTrabajador"));
                                lista.Add(be);
                            }

                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<PlanillaRpteResumenGeneralAnualResponse> ReporteEjecucionMensualMetaEspGasto(int iAnio, string sNombreReporte, int iTipo)
        {
            List<PlanillaRpteResumenGeneralAnualResponse> lista = new List<PlanillaRpteResumenGeneralAnualResponse>();
            PlanillaRpteResumenGeneralAnualResponse be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

                if (iTipo==1)
                {
                    cmd.CommandText = "[dbo].[paPlanillaRpteEjecMensPorMetaEspecifGastoCAS]";
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                else
                {
                    cmd.CommandText = "[dbo].[paPlanillaRpteEjecMensPorMetaEspecifGastoFUNC]";
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaRpteResumenGeneralAnualResponse();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();

                            
                            be.sSec_Func = dr.GetString(dr.GetOrdinal("vSec_Func"));
                            be.sSiglas = dr.GetString(dr.GetOrdinal("vSiglas"));
                            be.sDependencia = dr.GetString(dr.GetOrdinal("vDependencia"));
                            be.sConcepto = dr.GetString(dr.GetOrdinal("vNombreConcepto"));
                            be.sClasificadorGasto = dr.GetString(dr.GetOrdinal("vClasificadorGasto"));
                            be.dcTotalIng = dr.GetDecimal(dr.GetOrdinal("dcMontoIng"));                            
                            be.iAnio = iAnio;
                            be.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            be.sMes = dr.GetString(dr.GetOrdinal("vMes"));
                            be.sNombreReporte = sNombreReporte;
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }
            return lista;
        }

        #endregion Adminitrar Planilla

        

        #region Suspencion Retencion 4ta Categoria
        public IEnumerable<SuspensionRetencionCuartaCat_Registro> ListarTrabajadoresSuspRet4Ta(SuspensionRetencionCuartaCat_Registro peticion)
        {
            List<SuspensionRetencionCuartaCat_Registro> lista = new List<SuspensionRetencionCuartaCat_Registro>();
            SuspensionRetencionCuartaCat_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaSuspencionRetencion4TaConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                //cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", peticion.iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iMes", peticion.iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", peticion.iAnio));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new SuspensionRetencionCuartaCat_Registro();

                            //item.Grilla = new Grilla_Response();
                            item.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            item.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            item.iAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));                            
                            item.TipoDocumento = dr.GetString(dr.GetOrdinal("vTipoDocumento"));
                            item.NroDocumento = dr.GetString(dr.GetOrdinal("vNumeroDocumento"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("vNombres"));
                            item.Paterno = dr.GetString(dr.GetOrdinal("vApellidoPaterno"));
                            item.Materno = dr.GetString(dr.GetOrdinal("vApellidoMaterno"));
                            item.sNroAutorizacionExoneracion = dr.GetString(dr.GetOrdinal("vNroAutorizacionExoneracion"));
                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }


        public bool InsertarTrabajadoresSuspRet4Ta(SuspensionRetencionCuartaCat_Registro peticion)
        {
            bool rpta = false;
            try
            {
                _iBasesSqlAdoUnitOfWork.IniciarTransaccion(IsolationLevel.ReadCommitted);
                using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
                {

                    //if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                    if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                    cmd.CommandText = "[dbo].[paPlanillaSuspencionRetencion4Ta_Eliminar]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.CommandTimeout = cmd.CommandTimeout;                    
                    cmd.Parameters.Add(new SqlParameter("@iMes", peticion.iMes));
                    cmd.Parameters.Add(new SqlParameter("@iAnio", peticion.iAnio));
                    cmd.ExecuteNonQuery();

                    if (peticion.formatos != null)
                    {
                        foreach (String codigo in peticion.formatos)
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandText = "[dbo].[paPlanillaSuspencionRetencion4Ta_Registrar]";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@vNroDocumento", codigo.Split('|')[0]));
                            cmd.Parameters.Add(new SqlParameter("@iMes", peticion.iMes));
                            cmd.Parameters.Add(new SqlParameter("@iAnio", peticion.iAnio));
                            cmd.Parameters.Add(new SqlParameter("@vNroAutorizacionExoneracion", codigo.Split('|')[1]));
                            cmd.ExecuteNonQuery();                            
                        }
                        
                    }
                    if (_iBasesSqlAdoUnitOfWork.TieneTransaccion()) _iBasesSqlAdoUnitOfWork.ConfirmarTransaccion();
                    if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

                    rpta = true;
                }
            }
            catch (Exception)
            {
                if (_iBasesSqlAdoUnitOfWork.TieneTransaccion()) _iBasesSqlAdoUnitOfWork.RetrocederTransaccion();                
                throw;
            }            

            return rpta;
        }

        public bool ActualizarTrabajadoresSuspRet4Ta(RequisitosAdicionales_Registro peticion)
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

        public bool EliminarTrabajadoresSuspRet4Ta(SuspensionRetencionCuartaCat_Registro peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaSuspencionRetencion4Ta_EliminarPorID]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", peticion.iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iMes", peticion.iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", peticion.iAnio));

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


        #region PagosTrabajador

        public IEnumerable<PlanillaCalculada_Request> ListarConceptosPagosTrabajador(int iCodTrabajador, int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            List<PlanillaCalculada_Request> lista = new List<PlanillaCalculada_Request>();
            PlanillaCalculada_Request be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePlanillaRegistroCalculo_CptosPorTrabConsultarPorID]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", iCodTipoPlanilla));                
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", iCodDetPlanilla));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaCalculada_Request();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();

                            be.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            be.iCodPlanilla = dr.GetInt32(dr.GetOrdinal("iCodPlanilla"));
                            be.iCodTipoPlanilla = dr.GetInt32(dr.GetOrdinal("iCodTipoPlanilla"));
                            be.iCodDetPlanilla = dr.GetInt32(dr.GetOrdinal("iCodDetPlanilla"));
                            be.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            be.iAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            be.iCodConcepto = dr.GetInt32(dr.GetOrdinal("iCodConcepto"));
                            be.sNombreConcepto = dr.GetString(dr.GetOrdinal("vNombreConcepto"));
                            be.iCodTipoConcepto = dr.GetInt32(dr.GetOrdinal("iCodTipoConcepto"));
                            be.sNombreTipoConcepto = dr.GetString(dr.GetOrdinal("vNombreTipoConcepto"));
                            be.iCodSubTipoConcepto = dr.GetInt32(dr.GetOrdinal("iCodSubTipoConcepto"));
                            be.sNombreSubTipoConcepto = dr.GetString(dr.GetOrdinal("vNombreSubTipoConcepto"));
                            be.dcMonto = dr.GetDecimal(dr.GetOrdinal("dMonto"));
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public bool InsertarConceptoPagosTrabajador(PlanillaCalculada_Request peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {

                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePlanillaRegistroCalculo_Registrar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", peticion.iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", peticion.iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", peticion.iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", peticion.iCodDetPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", peticion.iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", peticion.iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodConcepto", peticion.iCodConcepto));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoConcepto", peticion.iCodTipoConcepto));
                cmd.Parameters.Add(new SqlParameter("@iCodSubTipoConcepto", peticion.iCodSubTipoConcepto));
                cmd.Parameters.Add(new SqlParameter("@dMonto", peticion.dcMonto));

                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        public bool ActualizarConceptoPagosTrabajador(PlanillaCalculada_Request peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePlanillaRegistroCalculo_Actualizar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", peticion.iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", peticion.iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", peticion.iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", peticion.iCodDetPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", peticion.iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", peticion.iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodConcepto", peticion.iCodConcepto));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoConcepto", peticion.iCodTipoConcepto));
                cmd.Parameters.Add(new SqlParameter("@iCodSubTipoConcepto", peticion.iCodSubTipoConcepto));
                cmd.Parameters.Add(new SqlParameter("@dMonto", peticion.dcMonto));
                //cmd.Parameters.Add(new SqlParameter("@vObservacion", peticion.sObservacion));

                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        public bool EliminarConceptoPagosTrabajador(PlanillaCalculada_Request peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePlanillaRegistroCalculo_Eliminar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", peticion.iCodTrabajador));
                cmd.Parameters.Add(new SqlParameter("@iCodPlanilla", peticion.iCodPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoPlanilla", peticion.iCodTipoPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iCodDetPlanilla", peticion.iCodDetPlanilla));
                cmd.Parameters.Add(new SqlParameter("@iMes", peticion.iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", peticion.iAnio));
                cmd.Parameters.Add(new SqlParameter("@iCodConcepto", peticion.iCodConcepto));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoConcepto", peticion.iCodTipoConcepto));
                cmd.Parameters.Add(new SqlParameter("@iCodSubTipoConcepto", peticion.iCodSubTipoConcepto));

                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        #endregion PagosTrabajador

        #region ProyecionAnualRta5ta
        public IEnumerable<PlanillaProyeccionAnualRta5ta_Registro> ListarProyeccionAnualRta5ta_RegistroTrabajador(int iCodTrabajador, int iAnio)
        {
            List<PlanillaProyeccionAnualRta5ta_Registro> lista = new List<PlanillaProyeccionAnualRta5ta_Registro>();
            PlanillaProyeccionAnualRta5ta_Registro be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePlanillaProyectoAnualRet5taConsultarPorIdTrab]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", iCodTrabajador));                
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));

                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new PlanillaProyeccionAnualRta5ta_Registro();
                            //item.Grilla = new Grilla_Response();
                            be.Grilla = new Grilla_Response();

                            be.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));                            
                            be.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            be.iAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            be.dcMontoTotalIng4ta = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalIng4ta"));
                            be.dcRemuneracion = dr.GetDecimal(dr.GetOrdinal("dcRemuneracion"));
                            be.dcAguinaldo = dr.GetDecimal(dr.GetOrdinal("dcAguinaldo"));
                            be.dcReintegro = dr.GetDecimal(dr.GetOrdinal("dcReintegro"));
                            be.dcTotal = dr.GetDecimal(dr.GetOrdinal("dcTotal"));
                            be.dcRetencion = dr.GetDecimal(dr.GetOrdinal("dcRetencion"));
                            be.dcMontoTotalImpRenta4ta = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalImpRenta4ta"));
                            be.dcMontoTotalVacImpRenta4ta = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalVacImpRenta4ta"));
                            be.dcMontoTotalOtrosIng5ta = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalOtrosIng5ta"));
                            be.dcMontoTotalAguinaldoTrunco = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalAguinaldoTrunco"));
                            be.dcMontoTotalCompensacionVacacional = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalCompensacionVacacional"));
                            be.dcMontoTotalOtrosRenta5ta = dr.GetDecimal(dr.GetOrdinal("dcMontoTotalOtrosRenta5ta"));
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public bool ActualizarProyeccionAnualRta5taTrabajador(PlanillaProyeccionAnualRta5ta_Registro peticion)
        {
            bool rpta = false;
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                int id = 0;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePlanillaProyectoAnualRet5ta_Actualizar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", peticion.iCodTrabajador));                
                cmd.Parameters.Add(new SqlParameter("@iMes", peticion.iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", peticion.iAnio));
                cmd.Parameters.Add(new SqlParameter("@dcMontoTotalIng4ta", peticion.dcMontoTotalIng4ta));
                cmd.Parameters.Add(new SqlParameter("@dcRemuneracion", peticion.dcRemuneracion));
                cmd.Parameters.Add(new SqlParameter("@dcAguinaldo", peticion.dcAguinaldo));
                cmd.Parameters.Add(new SqlParameter("@dcReintegro", peticion.dcReintegro));
                cmd.Parameters.Add(new SqlParameter("@dcTotal", peticion.dcTotal));
                cmd.Parameters.Add(new SqlParameter("@dcRetencion", peticion.dcRetencion));
                cmd.Parameters.Add(new SqlParameter("@dcMontoTotalImpRenta4ta", peticion.dcMontoTotalImpRenta4ta));
                cmd.Parameters.Add(new SqlParameter("@dcMontoTotalVacImpRenta4ta", peticion.dcMontoTotalVacImpRenta4ta));
                cmd.Parameters.Add(new SqlParameter("@dcMontoTotalOtrosIng5ta", peticion.dcMontoTotalOtrosIng5ta));
                cmd.Parameters.Add(new SqlParameter("@dcMontoTotalAguinaldoTrunco", peticion.dcMontoTotalAguinaldoTrunco));
                cmd.Parameters.Add(new SqlParameter("@dcMontoTotalCompensacionVacacional", peticion.dcMontoTotalCompensacionVacacional));
                cmd.Parameters.Add(new SqlParameter("@dcMontoTotalOtrosRenta5ta", peticion.dcMontoTotalOtrosRenta5ta));

                //cmd.Parameters.Add(new SqlParameter("@vObservacion", peticion.sObservacion));

                cmd.ExecuteNonQuery();
                rpta = true;
                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return rpta;
        }

        #endregion ProyecionAnualRta5ta
    }
}
