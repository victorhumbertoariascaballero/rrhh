using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MIDIS.ORI.Entidades;
using MIDIS.SEG.LogicaNegocio;

namespace MVCSisGesRRHH.Controllers
{
    public class ReportesController : Controller
    {
        private readonly T_genm_planillas_LN _planillas_Servicio = new T_genm_planillas_LN();
        private readonly T_genm_concepto_LN _concepto_Servicio = new T_genm_concepto_LN();
        // GET: Reportes
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]        
        public ActionResult ResumenGeneral()
        {
            return View();
        }
        [Authorize]
        public ActionResult ResumenAnual()
        {
            return View();
        }
        [Authorize]
        public ActionResult ResumenAlta()
        {
            return View();
        }
        [Authorize]
        public ActionResult ResumenCese()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public JsonResult ListarMeses()
        {
            List<Mes_Response> lista = new List<Mes_Response>();
            lista.Add(new Mes_Response("", "TODOS"));
            lista.Add(new Mes_Response("1", "ENERO"));
            lista.Add(new Mes_Response("2", "FEBRERO"));
            lista.Add(new Mes_Response("3", "MARZO"));
            lista.Add(new Mes_Response("4", "ABRIL"));
            lista.Add(new Mes_Response("5", "MAYO"));
            lista.Add(new Mes_Response("6", "JUNIO"));
            lista.Add(new Mes_Response("7", "JULIO"));
            lista.Add(new Mes_Response("8", "AGOSTO"));
            lista.Add(new Mes_Response("9", "SETIEMBRE"));
            lista.Add(new Mes_Response("10", "OCTUBRE"));
            lista.Add(new Mes_Response("11", "NOVIEMBRE"));
            lista.Add(new Mes_Response("12", "DICIEMBRE"));

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public JsonResult ListarReportes()
        {
            List<Mes_Response> lista = new List<Mes_Response>();
            lista.Add(new Mes_Response("", "SELECCIONE"));
            lista.Add(new Mes_Response("1", "RESUMEN GENERAL"));
            lista.Add(new Mes_Response("2", "RESUMEN GENERAL POR FTE. FTO. - PARTIDA - META"));
            lista.Add(new Mes_Response("3", "REPORTE FUENTE DE FINANCIAMIENTO PARTIDA META CUOTAS PATRONALES"));
            lista.Add(new Mes_Response("4", "RESUMEN POR RETENCIONES DE CUOTAS PATRONALES - META"));
            lista.Add(new Mes_Response("5", "RESUMEN POR RETENCIONES FUENTE DE FINANCIAMIENTO - META PARTIDA - CONCEPTO"));
            lista.Add(new Mes_Response("6", "REPORTE DE INGRESOS - META - PARTIDA"));
            lista.Add(new Mes_Response("7", "REPORTE DE EGRESOS - META - PARTIDA"));
            lista.Add(new Mes_Response("8", "REPORTE DE APORTES - META - PARTIDA"));
            lista.Add(new Mes_Response("9", "LISTADO DE DESCUENTOS JUDICIALES Y BENFICIARIOS A NIVEL DE META Y CLASIFICADOR DE GASTO"));
            lista.Add(new Mes_Response("10", "RESUMEN FUENTE DE FINANCIAMIENTO - AFP"));
            lista.Add(new Mes_Response("11", "RESUMEN GENERAL POR FUENTE DE FINANCIAMIENTO - AFP - META"));
            lista.Add(new Mes_Response("12", "RESUMEN DE ABONO AL BANCO"));
            lista.Add(new Mes_Response("13", "RESUMEN GENERAL POR FUENTE DE FINANCIAMIENTO - META"));
            lista.Add(new Mes_Response("14", "REPORTE DE ABONO AL BANCO"));
            lista.Add(new Mes_Response("15", "PLANILLA UNICA DE PAGO"));
            lista.Add(new Mes_Response("16", "REPORTE - META - PARTIDA"));

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Authorize]
        public JsonResult ListarReportes2()
        {
            List<Mes_Response> lista = new List<Mes_Response>();
            lista.Add(new Mes_Response("", "SELECCIONE"));
            lista.Add(new Mes_Response("1", "REPORTE CON ESTRUCTURA PARA CARGA DE INFORMACIÓN EN EL PORTAL AFPNET"));
            lista.Add(new Mes_Response("2", "REPORTE CON AFILIADOS DE ONP"));
            lista.Add(new Mes_Response("3", "REPORTE PARA LA PRESENTACIÓN DE INFORMACIÓN PARA EL PORTAL DE TRANSPARENCIA ESTÁNDAR"));
            lista.Add(new Mes_Response("4", "REPORTE DE RESUMEN POR PLANILLAS MENSUALIZADAS POR INGRESOS"));
            lista.Add(new Mes_Response("5", "REPORTE DE RESUMEN POR PLANILLAS MENSUALIZADAS POR EGRESOS"));
            lista.Add(new Mes_Response("6", "REPORTE DE RESUMEN POR PLANILLAS MENSUALIZADAS POR CONTRIBUCIONES A ESSALUD"));
            lista.Add(new Mes_Response("7", "REPORTE DE RESUMEN ANUALIZADO POR TRABAJADOR CON SUS PLANILLAS GENERADAS"));
            lista.Add(new Mes_Response("8", "REPORTE DE RESUMEN INGRESOS AFECTOS A RENTA DE 4TA CATEGORÍA"));
            lista.Add(new Mes_Response("9", "REPORTE DE EJECUCIÓN MENSUAL POR META Y ESPECIFICA DE GASTO CAS"));
            lista.Add(new Mes_Response("10", "REPORTE DE EJECUCIÓN MENSUAL POR META Y ESPECIFICA DE GASTO FUNCIONARIOS"));
            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public JsonResult ListarTiposConceptos()
        {
            List<Mes_Response> lista = new List<Mes_Response>();            
            lista.Add(new Mes_Response("1", "INGRESOS"));
            lista.Add(new Mes_Response("2", "EGRESOS"));
            lista.Add(new Mes_Response("3", "APORTES"));
            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarConceptoPorTipo(Concepto_Request peticion)
        {
            IEnumerable<Concepto_Response> respuesta = _concepto_Servicio.ListarConceptoPorTipoConcepto(peticion);
            if (peticion.bRegCAS)
            {
                respuesta = respuesta.Where(x => x.bRegCAS == peticion.bRegCAS).ToList();    
            }
            if (peticion.bRegFunc)
            {
                respuesta = respuesta.Where(x => x.bRegFunc == peticion.bRegFunc);
            }
            if (peticion.bRegFunc)
            {
                respuesta = respuesta.Where(x => x.vRegSeci == peticion.bRegSeci);
            }
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ReporteResumenGeneral(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla, string iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            object respuesta = _planillas_Servicio.ReporteResumenGeneral2(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla), Convert.ToInt32(iCodDetPlanilla), sMes, sPlanilla, sNombreReporte);
            Session["Reporte_General"] = respuesta;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }        
        [HttpPost]
        public JsonResult ReporteResumenGeneralPorMeta(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla, string iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            object respuesta = _planillas_Servicio.RRG4(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla), Convert.ToInt32(iCodDetPlanilla), sMes, sPlanilla, sNombreReporte);
            Session["Reporte_GeneralPorMeta"] = respuesta;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ReporteResumenGeneralPorMetaEsSalud(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla, string iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            object respuesta = _planillas_Servicio.RRGEsSalud(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla), Convert.ToInt32(iCodDetPlanilla), sMes, sPlanilla, sNombreReporte);
            Session["Reporte_GeneralPorMetaEsSalud"] = respuesta;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ReporteDetalladoEsSalud(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla, string iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            object respuesta = _planillas_Servicio.ReporteDetalladoEsSalud(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla), Convert.ToInt32(iCodDetPlanilla), sMes, sPlanilla, sNombreReporte);
            Session["Reporte_DetalladoEsSalud"] = respuesta;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ReporteResumenRetencionesFteFto(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla, string iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            object respuesta = _planillas_Servicio.ReporteResumenRetencionesFteFto(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla), Convert.ToInt32(iCodDetPlanilla), sMes, sPlanilla, sNombreReporte);
            Session["ReporteResumenRetencionesFteFto"] = respuesta;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ReporteResumenIngresosMetaPartidaConcepto(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla, string iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            object respuesta = _planillas_Servicio.ReporteResumenIngresosMetaPartidaConcepto(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla), Convert.ToInt32(iCodDetPlanilla), sMes, sPlanilla, sNombreReporte);
            Session["ReporteResumenIngEgreAporMetaPartidaConcepto"] = respuesta;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ReporteResumenEgresosMetaPartidaConcepto(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla, string iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            try
            {
                object respuesta = _planillas_Servicio.ReporteResumenEgresosMetaPartidaConcepto(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla), Convert.ToInt32(iCodDetPlanilla), sMes, sPlanilla, sNombreReporte);
                Session["ReporteResumenIngEgreAporMetaPartidaConcepto"] = respuesta;
                var jsonResult = Json(respuesta, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
                throw;
                return Json(new { success = "False", responseText = ex.Message });                
            }
        }
        [HttpPost]
        public JsonResult ReporteResumenAportacionesMetaPartidaConcepto(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla, string iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            object respuesta = _planillas_Servicio.ReporteResumenAportacionesMetaPartidaConcepto(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla), Convert.ToInt32(iCodDetPlanilla), sMes, sPlanilla, sNombreReporte);
            Session["ReporteResumenIngEgreAporMetaPartidaConcepto"] = respuesta;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ReporteResumenIngresosMetaPartidaPorConcepto(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla, string iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte, string iCodTipoConcepto, string iCodConcepto)
        {
            object respuesta = _planillas_Servicio.ReporteResumenIngresosMetaPartidaPorConcepto(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla), Convert.ToInt32(iCodDetPlanilla), sMes, sPlanilla, sNombreReporte, Convert.ToInt32(iCodTipoConcepto), Convert.ToInt32(iCodConcepto));
            Session["ReporteResumenIngEgreAporMetaPartidaConcepto"] = respuesta;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ReporteResumenDsctosJudicialesMetaPartidaConcepto(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla, string iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            object respuesta = _planillas_Servicio.ReporteResumenDsctosJudicialesMetaPartidaConcepto(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla), Convert.ToInt32(iCodDetPlanilla), sMes, sPlanilla, sNombreReporte);
            Session["ReporteResumenIngEgreAporMetaPartidaConcepto"] = respuesta;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ReporteResumenGralAFPconNroAfiliados(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla, string iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            object respuesta = _planillas_Servicio.ReporteResumenGralAFPconNroAfiliados(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla), Convert.ToInt32(iCodDetPlanilla), sMes, sPlanilla, sNombreReporte);
            Session["ReporteResumenGralAFPconNroAfiliados"] = respuesta;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ReporteResumenGralAFPconAFPDepMetaClasGasto(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla, string iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            object respuesta = _planillas_Servicio.ReporteResumenGralAFPconAFPDepMetaClasGasto(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla), Convert.ToInt32(iCodDetPlanilla), sMes, sPlanilla, sNombreReporte);
            Session["ReporteResumenGralAFPconAFPDepMetaClasGasto"] = respuesta;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ReporteResumenGralBcosPorInstCantTotIngTotEgre(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla, string iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            object respuesta = _planillas_Servicio.ReporteResumenGralBcosPorInstCantTotIngTotEgre(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla), Convert.ToInt32(iCodDetPlanilla), sMes, sPlanilla, sNombreReporte);
            Session["ReporteResumenGralBcosPorInstCantTotIngTotEgre"] = respuesta;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ReporteResumenGralBcosFteFtoMeta(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla, string iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            object respuesta = _planillas_Servicio.ReporteResumenGralBcosFteFtoMeta(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla), Convert.ToInt32(iCodDetPlanilla), sMes, sPlanilla, sNombreReporte);
            Session["ReporteResumenGralBcosFteFtoMeta"] = respuesta;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ReporteResumenDetalladoBcos(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla, string iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            object respuesta = _planillas_Servicio.ReporteResumenDetalladoBcos(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla), Convert.ToInt32(iCodDetPlanilla), sMes, sPlanilla, sNombreReporte);
            Session["ReporteResumenDetalladoBcos"] = respuesta;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ReportePlanillaUnicaPagos(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla, string iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            object respuesta = _planillas_Servicio.ReportePlanillaUnicaPagos(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla), Convert.ToInt32(iCodDetPlanilla), sMes, sPlanilla, sNombreReporte);
            Session["ReportePlanillaUnicaPagos"] = respuesta;
            var jsonResult = Json(respuesta, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult ReporteEstructuraInfoAFPNET(string iMes, string iAnio, string sNombreReporte, string iCodPlanilla, string iCodTipoPlanilla, string iCodDetPlanilla)
        {
            object respuesta = _planillas_Servicio.ReporteEstructuraInfoAFPNET(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), sNombreReporte, Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla), Convert.ToInt32(iCodDetPlanilla));
            Session["ReporteEstructuraInfoAFPNET"] = respuesta;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ReporteTrabajadoresAfiliadosONPE(string iMes, string iAnio, string sNombreReporte)
        {
            object respuesta = _planillas_Servicio.ReporteTrabajadoresAfiliadosONPE(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), sNombreReporte);
            Session["ReporteTrabajadoresAfiliadosONPE"] = respuesta;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ReporteInfoPortalTranspEstandar(string iMes, string iAnio, string sMes, string sNombreReporte)
        {
            object respuesta = _planillas_Servicio.ReporteInfoPortalTranspEstandar(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), sMes, sNombreReporte);
            Session["ReporteInfoPortalTranspEstandar"] = respuesta;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ReporteResumenPlanillaMensIngEgreEsSalud(string iAnio, string sNombreReporte, string iCodTipoConcepto)
        {
            object respuesta = _planillas_Servicio.ReporteResumenPlanillaMensIngEgreEsSalud(Convert.ToInt32(iAnio), sNombreReporte, Convert.ToInt32(iCodTipoConcepto));
            Session["ReporteResumenPlanillaMensIngEgreEsSalud"] = respuesta;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ReporteResumenAnuaPorTrab(string iAnio, string sNombreReporte, string sDNI)
        {
            object respuesta = _planillas_Servicio.ReporteResumenAnuaPorTrab(Convert.ToInt32(iAnio), sNombreReporte, sDNI);
            Session["ReporteResumenAnuaPorTrab"] = respuesta;
            var jsonResult = Json(respuesta, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpPost]
        public JsonResult ReporteResumenIngRent4ta(string iAnio, string sNombreReporte)
        {
            object respuesta = _planillas_Servicio.ReporteResumenIngRent4ta(Convert.ToInt32(iAnio), sNombreReporte);
            Session["ReporteResumenIngRent4ta"] = respuesta;
            var jsonResult = Json(respuesta, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpPost]
        public JsonResult ReporteEjecucionMensualMetaEspGastoCAS(string iAnio, string sNombreReporte)
        {
            object respuesta = _planillas_Servicio.ReporteEjecucionMensualMetaEspGasto(Convert.ToInt32(iAnio), sNombreReporte, 1);
            Session["ReporteEjecucionMensualMetaEspGasto"] = respuesta;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ReporteEjecucionMensualMetaEspGastoFUNC(string iAnio, string sNombreReporte)
        {
            object respuesta = _planillas_Servicio.ReporteEjecucionMensualMetaEspGasto(Convert.ToInt32(iAnio), sNombreReporte, 2);
            Session["ReporteEjecucionMensualMetaEspGasto"] = respuesta;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
    }
}