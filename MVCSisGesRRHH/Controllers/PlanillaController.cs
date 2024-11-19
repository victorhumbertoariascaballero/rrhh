using MIDIS.ORI.Entidades;
using MIDIS.SEG.LogicaNegocio;
using MIDIS.Utiles;
//using MIDIS.UtilesMVC;
//using MIDIS.UtilesWeb;
using System;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSisGesRRHH.Models;
using System.IO;
using SelectPdf;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.IO.Compression;

namespace MVCSisGesRRHH.Controllers
{
    public class PlanillaController : Controller
    {
        private readonly T_genm_planillas_LN _planillas_Servicio = new T_genm_planillas_LN();
        private readonly T_genm_metas_LN _metas_Servicio = new T_genm_metas_LN();
        // GET: Planilla
        [Authorize]
        public ActionResult Pensiones()
        {
            return View();
        }
        [Authorize]
        public ActionResult Conceptos()
        {
            return View();
        }
        [Authorize]
        public ActionResult Conceptos_Variables_Fijos()
        {
            return View();
        }
        [Authorize]
        public ActionResult Conceptos_Variables_FijosSinBaseImponible()
        {
            return View();
        }
        [Authorize]
        public ActionResult PlanillaDescuentosJudiciales()
        {
            Session["ListaBeneficiariosPlanillaJudicial"] = null;

            return View();
        }

        [Authorize]
        public ActionResult Asistencias_Permisos()
        {
            return View();
        }
        [Authorize]
        public ActionResult PlanillaCAS()
        {
            TempData["metas"] = _metas_Servicio.ListarMetas();
            if (TempData["metas"] != null)
            {
                ViewBag.metas = TempData["metas"];
            }
            return View();
        }
        [Authorize]
        public ActionResult PlanillaCASGenerar()
        {
            TempData["metas"] = _metas_Servicio.ListarMetas();
            if (TempData["metas"] != null)
            {
                ViewBag.metas = TempData["metas"];
            }
            return View();
        }
        [Authorize]
        public ActionResult PlanillaCASBusqueda()
        {
            TempData["metas"] = _metas_Servicio.ListarMetas();
            if (TempData["metas"] != null)
            {
                ViewBag.metas = TempData["metas"];
            }
            return View();
        }
        [Authorize]
        public ActionResult PlanillaCASBusquedaFinal()
        {
            TempData["metas"] = _metas_Servicio.ListarMetas();
            if (TempData["metas"] != null)
            {
                ViewBag.metas = TempData["metas"];
            }
            return View();
        }
        [Authorize]
        public ActionResult PlanillaFED()
        {
            TempData["metas"] = _metas_Servicio.ListarMetas();
            if (TempData["metas"] != null)
            {
                ViewBag.metas = TempData["metas"];
            }
            return View();
        }
        [Authorize]
        public ActionResult PlanillaFEDGenerar()
        {
            TempData["metas"] = _metas_Servicio.ListarMetas();
            if (TempData["metas"] != null)
            {
                ViewBag.metas = TempData["metas"];
            }
            return View();
        }
        [Authorize]
        public ActionResult PlanillaFEDBusqueda()
        {
            TempData["metas"] = _metas_Servicio.ListarMetas();
            if (TempData["metas"] != null)
            {
                ViewBag.metas = TempData["metas"];
            }
            return View();
        }
        [Authorize]
        public ActionResult PlanillaCASAdicional()
        {           
            return View();
        }
        [Authorize]
        public ActionResult PlanillaFuncionarios()
        {
            return View();
        }
        [Authorize]
        public ActionResult PlanillaFuncionariosGenerar()
        {
            return View();
        }
        [Authorize]
        public ActionResult PlanillaFuncionariosBusqueda()
        {
            return View();
        }
        [Authorize]
        public ActionResult PlanillaFuncionariosAdicional()
        {
            return View();
        }
        [Authorize]
        public ActionResult PlanillaVacTruncasCAS()
        {
            return View();
        }
        [Authorize]
        public ActionResult PlanillaVacTruncasFED()
        {
            return View();
        }
        [Authorize]
        public ActionResult PlanillaAdministrar()
        {
            return View();
        }
        [Authorize]
        public ActionResult PlanillaAdministrar_()
        {
            return View();
        }
        [Authorize]
        public ActionResult SuspensionRetencionCuartaCat()
        {
            return View();
        }
        [Authorize]
        public ActionResult PlanillaSECIGRISTABusqueda()
        {
            TempData["metas"] = _metas_Servicio.ListarMetas();
            if (TempData["metas"] != null)
            {
                ViewBag.metas = TempData["metas"];
            }
            return View();
        }
        #region Planilla CAS
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaGeneralCAS(string idCodFuenteFinanciamiento, string sMetas, string iMes, string iAnio)
        {
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaGeneralCAS(Convert.ToInt32(idCodFuenteFinanciamiento), sMetas, iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaIngresosCAS(string iCodTrabajador, string iMes, string iAnio)
        {
            string iCodTipoPlanilla = "1";
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaIngresosCAS(Convert.ToInt32(iCodTrabajador), iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0, iCodTipoPlanilla != string.Empty ? Convert.ToInt32(iCodTipoPlanilla) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaDescuentosCAS(string iCodTrabajador, string iMes, string iAnio)
        {
            string iCodTipoPlanilla = "1";
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaDescuentosCAS(Convert.ToInt32(iCodTrabajador), iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0, iCodTipoPlanilla != string.Empty ? Convert.ToInt32(iCodTipoPlanilla) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaGeneralCompletaCAS(string idCodFuenteFinanciamiento, string sMetas, string iMes, string iAnio)
        {
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaGeneralCompletaCAS(Convert.ToInt32(idCodFuenteFinanciamiento), sMetas, iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GenerarPlanillaCAS(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla)
        {
            try
            {

                object respuesta = _planillas_Servicio.GenerarPlanillaCAS(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla));

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult GenerarPlanillaPorTrabajador(string iCodTrabajador, string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla)
        {
            try
            {

                object respuesta = _planillas_Servicio.GenerarPlanillaPorTrabajdor(Convert.ToInt32(iCodTrabajador), Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla));//_planillas_Servicio.GenerarPlanillaCAS(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla));

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        #endregion Planilla CAS

        #region Planilla FED
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaGeneralFED(string idCodFuenteFinanciamiento, string sMetas, string iMes, string iAnio)
        {
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaGeneralFED(Convert.ToInt32(idCodFuenteFinanciamiento), sMetas, iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaIngresosFED(string iCodTrabajador, string iMes, string iAnio)
        {
            string iCodTipoPlanilla = "1";
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaIngresosFED(Convert.ToInt32(iCodTrabajador), iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0, iCodTipoPlanilla != string.Empty ? Convert.ToInt32(iCodTipoPlanilla) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaDescuentosFED(string iCodTrabajador, string iMes, string iAnio)
        {
            string iCodTipoPlanilla = "1";
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaDescuentosFED(Convert.ToInt32(iCodTrabajador), iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0, iCodTipoPlanilla != string.Empty ? Convert.ToInt32(iCodTipoPlanilla) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaGeneralCompletaFED(string idCodFuenteFinanciamiento, string sMetas, string iMes, string iAnio)
        {
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaGeneralCompletaFED(Convert.ToInt32(idCodFuenteFinanciamiento), sMetas, iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GenerarPlanillaFED(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla)
        {
            try
            {

                object respuesta = _planillas_Servicio.GenerarPlanillaFED(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla));

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        #endregion Planilla FED

        [HttpPost]
        public JsonResult ConsultarEjecucionPlanilla(string iCodPlanilla, string iMes, string iAnio, string iCodTipoPlanilla, string iCodDetPlanilla)
        {
            object respuesta = _planillas_Servicio.ConsultarEjecucionPlanilla(Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodTipoPlanilla), Convert.ToInt32(iCodDetPlanilla));            
            return Json(respuesta, JsonRequestBehavior.AllowGet);
            
            //return Json(new { success = "True", responseText = respuesta });
        }
        [HttpPost]
        public JsonResult ConsultarGeneracionPlanilla(string iCodPlanilla, string iMes, string iAnio, string iCodTipoPlanilla, string iCodDetPlanilla)
        {
            object respuesta = _planillas_Servicio.ConsultarGeneracionPlanilla(Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodTipoPlanilla), Convert.ToInt32(iCodDetPlanilla));
            return Json(respuesta, JsonRequestBehavior.AllowGet);
            //return Json(new { success = "True", responseText = respuesta });
        }
        public JsonResult ListarMetas()
        {
            object respuesta = _metas_Servicio.ListarMetas();

            //List<Metas_Response> lista = new List<Metas_Response>();
            //lista.Add(new Metas_Response { sSec_Func = "01" });
            //string[] lst = new string[]{ "01", "02" };
            //object respuesta = lst;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CerrarFase(string iCodPlanilla, string iMes, string iAnio, string iCodTipoPlanilla, string iCodDetPlanilla, string iCodFase)
        {
            object respuesta = _planillas_Servicio.CerrarFase(Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodTipoPlanilla), Convert.ToInt32(iCodDetPlanilla), Convert.ToInt32(iCodFase), User.Identity.Name);
            return Json(respuesta, JsonRequestBehavior.AllowGet);

            //return Json(new { success = "True", responseText = respuesta });
        }

        /*-------------Planilla CAS Adicional---------------------*/
        #region Planilla CAS Adicional
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaGeneralCASAdicional(string idCodFuenteFinanciamiento, string sMetas, string iMes, string iAnio, string iCodPlanilla, string iCodDetPlanilla)
        {
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaGeneralCASAdicional(Convert.ToInt32(idCodFuenteFinanciamiento), sMetas, iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0, iCodPlanilla != string.Empty ? Convert.ToInt32(iCodPlanilla) : 0, iCodDetPlanilla != string.Empty ? Convert.ToInt32(iCodDetPlanilla) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaIngresosCASAdicional(string iCodTrabajador, string iMes, string iAnio, string iCodPlanilla, string iCodDetPlanilla)
        {
            string iCodTipoPlanilla = "2";
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaIngresosCASAdicional(Convert.ToInt32(iCodTrabajador), iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0, iCodTipoPlanilla != string.Empty ? Convert.ToInt32(iCodTipoPlanilla) : 0, iCodPlanilla != string.Empty ? Convert.ToInt32(iCodPlanilla) : 0, iCodDetPlanilla != string.Empty ? Convert.ToInt32(iCodDetPlanilla) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaDescuentosCASAdicional(string iCodTrabajador, string iMes, string iAnio, string iCodPlanilla, string iCodDetPlanilla)
        {
            string iCodTipoPlanilla = "2";
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaDescuentosCASAdicional(Convert.ToInt32(iCodTrabajador), iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0, iCodTipoPlanilla != string.Empty ? Convert.ToInt32(iCodTipoPlanilla) : 0, iCodPlanilla != string.Empty ? Convert.ToInt32(iCodPlanilla) : 0, iCodDetPlanilla != string.Empty ? Convert.ToInt32(iCodDetPlanilla) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaGeneralCompletaCASAdicional(string iMes, string iAnio, string iCodPlanilla, string iCodDetPlanilla)
        {
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaGeneralCompletaCASAdicional(iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0, iCodPlanilla != string.Empty ? Convert.ToInt32(iCodPlanilla) : 0, iCodDetPlanilla != string.Empty ? Convert.ToInt32(iCodDetPlanilla) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GenerarPlanillaCASAdicional(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla, string iCodDetPlanilla, IEnumerable<String> strCodTrabajadores)
        {
            try
            {

                object respuesta = _planillas_Servicio.GenerarPlanillaCASAdicional(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla), Convert.ToInt32(iCodDetPlanilla), strCodTrabajadores);
                if (respuesta!=null)
                {
                    if (Session["ListaEmpleadosPlanillaAdicional"] != null) {
                        Session["ListaEmpleadosPlanillaAdicional"] = null;
                    }
                }
                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult ListarEmpleadosPlanilla(string strCodTipoCondicionTrabajador)
        {
            object respuesta = _planillas_Servicio.ListarEmpleadosPlanilla(strCodTipoCondicionTrabajador);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CargarEmpleadosPlanillaAdicioalTemporal()
        {
            
            List<Empleado_Registro> lstEmpleado_Registro = new List<Empleado_Registro>();
            if (Session["ListaEmpleadosPlanillaAdicional"] != null)
            {
                lstEmpleado_Registro = (List<Empleado_Registro>)Session["ListaEmpleadosPlanillaAdicional"];
            }
            object respuesta = lstEmpleado_Registro;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AgregarEmpleadosPlanillaAdicioalTemporal(string iCodTrabajador, string strNombreCompleto, string strIdTipoPlanillaAdicional, string strNombreTipoPlanillaAdicional, string strMonto)
        {
            Empleado_Registro objEmpleado_Registro = null;
            List<Empleado_Registro> lstEmpleado_Registro = new List<Empleado_Registro>();
            if (Session["ListaEmpleadosPlanillaAdicional"]==null)
            {
                if (!string.IsNullOrEmpty(iCodTrabajador))
                {
                    objEmpleado_Registro = new Empleado_Registro
                            {
                                IdEmpleado = Convert.ToInt32(iCodTrabajador),
                                Nombre = strNombreCompleto,
                                IdTipoPlanillaAdicional = Convert.ToInt32(strIdTipoPlanillaAdicional),
                                NombreTipoPlanillaAdicional = strNombreTipoPlanillaAdicional,
                                MontoPlanillaAdicional = Convert.ToDecimal(strMonto)
                            };
                    lstEmpleado_Registro.Add(objEmpleado_Registro);
                    Session["ListaEmpleadosPlanillaAdicional"] = lstEmpleado_Registro; 
                }
            }
            else
            {
                lstEmpleado_Registro = (List<Empleado_Registro>)Session["ListaEmpleadosPlanillaAdicional"];
                objEmpleado_Registro = new Empleado_Registro
                {
                    IdEmpleado = Convert.ToInt32(iCodTrabajador),
                    Nombre = strNombreCompleto,
                    IdTipoPlanillaAdicional = Convert.ToInt32(strIdTipoPlanillaAdicional),
                    NombreTipoPlanillaAdicional = strNombreTipoPlanillaAdicional,
                    MontoPlanillaAdicional = Convert.ToDecimal(strMonto)
                };
                lstEmpleado_Registro.Add(objEmpleado_Registro);
                Session["ListaEmpleadosPlanillaAdicional"] = lstEmpleado_Registro;
            }

            object respuesta = lstEmpleado_Registro;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult QuitarEmpleadosPlanillaAdicioalTemporal(string iCodTrabajador)
        {
            //Empleado_Registro objEmpleado_Registro = null;
            List<Empleado_Registro> lstEmpleado_Registro = new List<Empleado_Registro>();
            if (Session["ListaEmpleadosPlanillaAdicional"] != null)
            {
                lstEmpleado_Registro = (List<Empleado_Registro>)Session["ListaEmpleadosPlanillaAdicional"];
                int obj = lstEmpleado_Registro.FindIndex(x => x.IdEmpleado == Convert.ToInt32(iCodTrabajador));
                lstEmpleado_Registro.RemoveAt(obj);
                Session["ListaEmpleadosPlanillaAdicional"] = lstEmpleado_Registro;
            }
            object respuesta = lstEmpleado_Registro;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CargarTrabajadorPlanillaCASAdicional(string strCodTipoCondicionTrabajador, string strDNI)
        {
            object respuesta = _planillas_Servicio.ListarEmpleadosPlanilla(strCodTipoCondicionTrabajador).Where(x => x.NroDocumento == strDNI).FirstOrDefault();
            if (respuesta == null)
            {
                respuesta = new Empleado_Registro
                {
                    IdEmpleado = 0
                };
            }
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarTipoPlanillaCASAdicional()
        {
            object respuesta = _planillas_Servicio.ListarTipoPlanillaCASAdicional();
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarTrabajadoresPlanillaCASAdicional(string iCodPlanilla, string iMes, string iAnio, string iCodTipoPlanilla, string iCodDetPlanilla)
        {
            object respuesta = null;
            try
            {
                if (!string.IsNullOrEmpty(iCodPlanilla))
                {
                    Session["ListaEmpleadosPlanillaAdicional"] = _planillas_Servicio.ListarTrabajadoresPlanillaCASAdicional(Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodTipoPlanilla), Convert.ToInt32(iCodDetPlanilla)); ;
                    respuesta = Session["ListaEmpleadosPlanillaAdicional"];
                }
                return Json(respuesta, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        #endregion Planilla CAS Adicional
        /*-------------Planilla CAS Adicional---------------------*/

        #region Planilla Funcionarios
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaGeneralFUNC(string iMes, string iAnio)
        {
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaGeneralFUNC(iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaIngresosFUNC(string iCodTrabajador, string iMes, string iAnio)
        {
            string iCodTipoPlanilla = "1";
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaIngresosFUNC(Convert.ToInt32(iCodTrabajador), iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0, iCodTipoPlanilla != string.Empty ? Convert.ToInt32(iCodTipoPlanilla) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaDescuentosFUNC(string iCodTrabajador, string iMes, string iAnio)
        {
            string iCodTipoPlanilla = "1";
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaDescuentosFUNC(Convert.ToInt32(iCodTrabajador), iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0, iCodTipoPlanilla != string.Empty ? Convert.ToInt32(iCodTipoPlanilla) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaGeneralCompletaFUNC(string iMes, string iAnio)
        {
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaGeneralCompletaFUNC(iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GenerarPlanillaFUNC(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla)
        {
            try
            {
                object respuesta = _planillas_Servicio.GenerarPlanillaFUNC(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla));
                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }        
        #endregion Planilla Funcionarios

        /*-------------Planilla Funcional Adicional---------------------*/
        #region Planilla Funcional Adicional
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaGeneralFUNCAdicional(string idCodFuenteFinanciamiento, string sMetas, string iMes, string iAnio, string iCodDetPlanilla)
        {
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaGeneralFUNCAdicional(Convert.ToInt32(idCodFuenteFinanciamiento), sMetas, iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0, iCodDetPlanilla != string.Empty ? Convert.ToInt32(iCodDetPlanilla) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaIngresosFUNCAdicional(string iCodTrabajador, string iMes, string iAnio, string iCodDetPlanilla)
        {
            string iCodTipoPlanilla = "2";
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaIngresosFUNCAdicional(Convert.ToInt32(iCodTrabajador), iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0, iCodTipoPlanilla != string.Empty ? Convert.ToInt32(iCodTipoPlanilla) : 0, iCodDetPlanilla != string.Empty ? Convert.ToInt32(iCodDetPlanilla) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaDescuentosFUNCAdicional(string iCodTrabajador, string iMes, string iAnio, string iCodDetPlanilla)
        {
            string iCodTipoPlanilla = "2";
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaDescuentosFUNCAdicional(Convert.ToInt32(iCodTrabajador), iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0, iCodTipoPlanilla != string.Empty ? Convert.ToInt32(iCodTipoPlanilla) : 0, iCodDetPlanilla != string.Empty ? Convert.ToInt32(iCodDetPlanilla) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaGeneralCompletaFUNCAdicional(string iMes, string iAnio, string iCodDetPlanilla)
        {
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaGeneralCompletaFUNCAdicional(iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0, iCodDetPlanilla != string.Empty ? Convert.ToInt32(iCodDetPlanilla) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GenerarPlanillaFUNCAdicional(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla, string iCodDetPlanilla, IEnumerable<String> strCodTrabajadores)
        {
            try
            {

                object respuesta = _planillas_Servicio.GenerarPlanillaFUNCAdicional(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla), Convert.ToInt32(iCodDetPlanilla), strCodTrabajadores);
                if (respuesta != null)
                {
                    if (Session["ListaEmpleadosPlanillaAdicional"] != null)
                    {
                        Session["ListaEmpleadosPlanillaAdicional"] = null;
                    }
                }
                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult ListarEmpleadosPlanillaFUNC(string strCodTipoCondicionTrabajador)
        {
            object respuesta = _planillas_Servicio.ListarEmpleadosPlanilla(strCodTipoCondicionTrabajador);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CargarEmpleadosPlanillaFUNCAdicioalTemporal()
        {

            List<Empleado_Registro> lstEmpleado_Registro = new List<Empleado_Registro>();
            if (Session["ListaEmpleadosPlanillaFUNCAdicional"] != null)
            {
                lstEmpleado_Registro = (List<Empleado_Registro>)Session["ListaEmpleadosPlanillaFUNCAdicional"];
            }
            object respuesta = lstEmpleado_Registro;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        //[HttpPost]
        //public JsonResult AgregarEmpleadosPlanillaFUNCAdicioalTemporal(string iCodTrabajador, string strNombreCompleto)
        //{
        //    Empleado_Registro objEmpleado_Registro = null;
        //    List<Empleado_Registro> lstEmpleado_Registro = new List<Empleado_Registro>();
        //    if (Session["ListaEmpleadosPlanillaFUNCAdicional"] == null)
        //    {
        //        objEmpleado_Registro = new Empleado_Registro
        //        {
        //            IdEmpleado = Convert.ToInt32(iCodTrabajador),
        //            Nombre = strNombreCompleto
        //        };
        //        lstEmpleado_Registro.Add(objEmpleado_Registro);
        //        Session["ListaEmpleadosPlanillaFUNCAdicional"] = lstEmpleado_Registro;
        //    }
        //    else
        //    {
        //        lstEmpleado_Registro = (List<Empleado_Registro>)Session["ListaEmpleadosPlanillaFUNCAdicional"];
        //        objEmpleado_Registro = new Empleado_Registro
        //        {
        //            IdEmpleado = Convert.ToInt32(iCodTrabajador),
        //            Nombre = strNombreCompleto
        //        };
        //        lstEmpleado_Registro.Add(objEmpleado_Registro);
        //        Session["ListaEmpleadosPlanillaFUNCAdicional"] = lstEmpleado_Registro;
        //    }

        //    object respuesta = lstEmpleado_Registro;
        //    return Json(respuesta, JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        public JsonResult AgregarEmpleadosPlanillaFUNCAdicionalTemporal(string iCodTrabajador, string strNombreCompleto, string strIdTipoPlanillaAdicional, string strNombreTipoPlanillaAdicional, string strMonto)
        {
            Empleado_Registro objEmpleado_Registro = null;
            List<Empleado_Registro> lstEmpleado_Registro = new List<Empleado_Registro>();
            if (Session["ListaEmpleadosPlanillaFUNCAdicional"] == null)
            {
                if (!string.IsNullOrEmpty(iCodTrabajador))
                {
                    objEmpleado_Registro = new Empleado_Registro
                    {
                        IdEmpleado = Convert.ToInt32(iCodTrabajador),
                        Nombre = strNombreCompleto,
                        IdTipoPlanillaAdicional = Convert.ToInt32(strIdTipoPlanillaAdicional),
                        NombreTipoPlanillaAdicional = strNombreTipoPlanillaAdicional,
                        MontoPlanillaAdicional = Convert.ToDecimal(strMonto)
                    };
                    lstEmpleado_Registro.Add(objEmpleado_Registro);
                    Session["ListaEmpleadosPlanillaFUNCAdicional"] = lstEmpleado_Registro;
                }
            }
            else
            {
                lstEmpleado_Registro = (List<Empleado_Registro>)Session["ListaEmpleadosPlanillaFUNCAdicional"];
                objEmpleado_Registro = new Empleado_Registro
                {
                    IdEmpleado = Convert.ToInt32(iCodTrabajador),
                    Nombre = strNombreCompleto,
                    IdTipoPlanillaAdicional = Convert.ToInt32(strIdTipoPlanillaAdicional),
                    NombreTipoPlanillaAdicional = strNombreTipoPlanillaAdicional,
                    MontoPlanillaAdicional = Convert.ToDecimal(strMonto)
                };
                lstEmpleado_Registro.Add(objEmpleado_Registro);
                Session["ListaEmpleadosPlanillaFUNCAdicional"] = lstEmpleado_Registro;
            }

            object respuesta = lstEmpleado_Registro;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult QuitarEmpleadosPlanillaFUNCAdicionalTemporal(string iCodTrabajador)
        {
            //Empleado_Registro objEmpleado_Registro = null;
            List<Empleado_Registro> lstEmpleado_Registro = new List<Empleado_Registro>();
            if (Session["ListaEmpleadosPlanillaFUNCAdicional"] != null)
            {
                lstEmpleado_Registro = (List<Empleado_Registro>)Session["ListaEmpleadosPlanillaFUNCAdicional"];
                int obj = lstEmpleado_Registro.FindIndex(x => x.IdEmpleado == Convert.ToInt32(iCodTrabajador));
                lstEmpleado_Registro.RemoveAt(obj);
                Session["ListaEmpleadosPlanillaFUNCAdicional"] = lstEmpleado_Registro;
            }
            object respuesta = lstEmpleado_Registro;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CargarTrabajadorPlanillaFUNCAdicional(string strCodTipoCondicionTrabajador, string strDNI)
        {
            object respuesta = _planillas_Servicio.ListarEmpleadosPlanilla(strCodTipoCondicionTrabajador).Where(x => x.NroDocumento == strDNI).FirstOrDefault();
            if (respuesta == null)
            {
                respuesta = new Empleado_Registro
                {
                    IdEmpleado = 0
                };
            }
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarTrabajadoresPlanillaFUNCAdicional(string iCodPlanilla, string iMes, string iAnio, string iCodTipoPlanilla, string iCodDetPlanilla)
        {
            object respuesta = null;
            try
            {
                if (!string.IsNullOrEmpty(iCodPlanilla))
                {
                    Session["ListaEmpleadosPlanillaAdicional"] = _planillas_Servicio.ListarTrabajadoresPlanillaFUNCAdicional(Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodTipoPlanilla), Convert.ToInt32(iCodDetPlanilla)); ;
                    respuesta = Session["ListaEmpleadosPlanillaAdicional"];
                }
                return Json(respuesta, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        #endregion Planilla Funcional Adicional
        /*-------------Planilla Funcional Adicional---------------------*/

        /*-------------Planilla CAS Vacaciones Truncas---------------------*/
        #region Planilla CAS Vacaciones Truncas
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaGeneralCASVacTruncas(string idCodFuenteFinanciamiento, string sMetas, string iMes, string iAnio, string iCodDetPlanilla)
        {
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaGeneralCASVacTruncas(Convert.ToInt32(idCodFuenteFinanciamiento), sMetas, iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0, iCodDetPlanilla != string.Empty ? Convert.ToInt32(iCodDetPlanilla) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaIngresosCASVacTruncas(string iCodTrabajador, string iMes, string iAnio, string iCodDetPlanilla)
        {
            string iCodTipoPlanilla = "1";
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaIngresosCASVacTruncas(Convert.ToInt32(iCodTrabajador), iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0, iCodTipoPlanilla != string.Empty ? Convert.ToInt32(iCodTipoPlanilla) : 0, iCodDetPlanilla != string.Empty ? Convert.ToInt32(iCodDetPlanilla) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaDescuentosCASVacTruncas(string iCodTrabajador, string iMes, string iAnio, string iCodDetPlanilla)
        {
            string iCodTipoPlanilla = "1";
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaDescuentosCASVacTruncas(Convert.ToInt32(iCodTrabajador), iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0, iCodTipoPlanilla != string.Empty ? Convert.ToInt32(iCodTipoPlanilla) : 0, iCodDetPlanilla != string.Empty ? Convert.ToInt32(iCodDetPlanilla) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaGeneralCompletaCASVacTruncas(string iMes, string iAnio, string iCodDetPlanilla)
        {
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaGeneralCompletaCASVacTruncas(iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0, iCodDetPlanilla != string.Empty ? Convert.ToInt32(iCodDetPlanilla) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GenerarPlanillaCASVacTruncas(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla, string iCodDetPlanilla, string strCodTrabajadores)
        {
            try
            {
                List<Empleado_Registro> lstEmpleado_Registro = new List<Empleado_Registro>();
                object respuesta = null;
                if (Session["ListaEmpleadosPlanillaVacTruncas"] != null)
                {
                    lstEmpleado_Registro = (List<Empleado_Registro>)Session["ListaEmpleadosPlanillaVacTruncas"];
                    respuesta = _planillas_Servicio.GenerarPlanillaCASVacTruncas(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla), Convert.ToInt32(iCodDetPlanilla), lstEmpleado_Registro);
                }
                if (respuesta != null)
                {
                    if (Session["ListaEmpleadosPlanillaVacTruncas"] != null)
                    {
                        Session["ListaEmpleadosPlanillaVacTruncas"] = null;
                    }
                }
                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult ListarEmpleadosPlanillaVacTruncas(string strCodTipoCondicionTrabajador)
        {
            object respuesta = _planillas_Servicio.ListarEmpleadosPlanilla(strCodTipoCondicionTrabajador);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CargarTrabajadorPlanillaCASVacTruncas(string strCodTipoCondicionTrabajador, string strDNI)
        {
            object respuesta = _planillas_Servicio.ListarEmpleadosPlanilla(strCodTipoCondicionTrabajador).Where(x => x.NroDocumento == strDNI && x.FED == false).FirstOrDefault();
            if (respuesta == null)
            {
                respuesta = new Empleado_Registro
                {
                    IdEmpleado = 0
                };
            }
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CargarEmpleadosPlanillaVacTruncasTemporal()
        {

            List<Empleado_Registro> lstEmpleado_Registro = new List<Empleado_Registro>();
            if (Session["ListaEmpleadosPlanillaVacTruncas"] != null)
            {
                lstEmpleado_Registro = (List<Empleado_Registro>)Session["ListaEmpleadosPlanillaVacTruncas"];
            }
            object respuesta = lstEmpleado_Registro;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AgregarTrabajadorPlanillaVacTruncasTemporal(string iCodTrabajador, string strNombreCompleto, string iDiasDescansoFisico, string iMesesDescansoFisico, string iDiasVacTruncas, string iMesesVacTruncas)
        {
            Empleado_Registro objEmpleado_Registro = null;
            List<Empleado_Registro> lstEmpleado_Registro = new List<Empleado_Registro>();
            if (Session["ListaEmpleadosPlanillaVacTruncas"] == null)
            {
                objEmpleado_Registro = new Empleado_Registro
                {
                    IdEmpleado = Convert.ToInt32(iCodTrabajador),
                    Nombre = strNombreCompleto,
                    DiasDescansoFisico = Convert.ToInt32(iDiasDescansoFisico),
                    MesesDescansoFisico = Convert.ToInt32(iMesesDescansoFisico),
                    DiasVacacionesTruncas = Convert.ToInt32(iDiasVacTruncas),
                    MesesVacacionesTruncas = Convert.ToInt32(iMesesVacTruncas)
                };
                lstEmpleado_Registro.Add(objEmpleado_Registro);
                Session["ListaEmpleadosPlanillaVacTruncas"] = lstEmpleado_Registro.OrderBy(x => x.Paterno).ToList();
            }
            else
            {
                lstEmpleado_Registro = (List<Empleado_Registro>)Session["ListaEmpleadosPlanillaVacTruncas"];
                objEmpleado_Registro = new Empleado_Registro
                {
                    IdEmpleado = Convert.ToInt32(iCodTrabajador),
                    Nombre = strNombreCompleto,
                    DiasDescansoFisico = Convert.ToInt32(iDiasDescansoFisico),
                    MesesDescansoFisico = Convert.ToInt32(iMesesDescansoFisico),
                    DiasVacacionesTruncas = Convert.ToInt32(iDiasVacTruncas),
                    MesesVacacionesTruncas = Convert.ToInt32(iMesesVacTruncas)
                };
                lstEmpleado_Registro.Add(objEmpleado_Registro);
                Session["ListaEmpleadosPlanillaVacTruncas"] = lstEmpleado_Registro.OrderBy(x => x.Paterno).ToList();
            }

            object respuesta = lstEmpleado_Registro;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult QuitarEmpleadosPlanillaVacTruncasTemporal(string iCodTrabajador)
        {
            //Empleado_Registro objEmpleado_Registro = null;
            List<Empleado_Registro> lstEmpleado_Registro = new List<Empleado_Registro>();
            if (Session["ListaEmpleadosPlanillaVacTruncas"] != null)
            {
                lstEmpleado_Registro = (List<Empleado_Registro>)Session["ListaEmpleadosPlanillaVacTruncas"];
                int obj = lstEmpleado_Registro.FindIndex(x => x.IdEmpleado == Convert.ToInt32(iCodTrabajador));
                lstEmpleado_Registro.RemoveAt(obj);
                Session["ListaEmpleadosPlanillaVacTruncas"] = lstEmpleado_Registro;
            }
            object respuesta = lstEmpleado_Registro;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarTrabajadoresPlanillaVacTruncas(string iCodPlanilla, string iMes, string iAnio, string iCodTipoPlanilla, string iCodDetPlanilla, string btnBuscar)
        {
            object respuesta = null;
            try
            {
                if (!string.IsNullOrEmpty(iCodPlanilla))
                {
                    if (btnBuscar=="1")
                    {
                        Session["ListaEmpleadosPlanillaVacTruncas"] = _planillas_Servicio.ListarTrabajadoresPlanillaVacTruncas(Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodTipoPlanilla), Convert.ToInt32(iCodDetPlanilla)).OrderBy(x => x.Paterno).ToList(); ;
                        respuesta = Session["ListaEmpleadosPlanillaVacTruncas"]; 
                    }
                    else
                    {
                        List<Empleado_Registro> lstEmpleado_Registro = new List<Empleado_Registro>();
                        lstEmpleado_Registro = (List<Empleado_Registro>)Session["ListaEmpleadosPlanillaVacTruncas"];
                        respuesta = lstEmpleado_Registro.OrderBy(x => x.Paterno).ToList();
                    }
                }
                return Json(respuesta, JsonRequestBehavior.AllowGet);
                //return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }        
        [HttpPost]
        public JsonResult ModificarTrabajadoresPlanillaVacTruncas(string IdEmpleado, string NombreCompleto, string DiasDescansoFisico, string MesesDescansoFisico, string DiasVacacionesTruncas, string MesesVacacionesTruncas)
        {
            Empleado_Registro objEmpleado_Registro = null;
            List<Empleado_Registro> lstEmpleado_Registro = new List<Empleado_Registro>();
            if (Session["ListaEmpleadosPlanillaVacTruncas"] != null)            
            {
                lstEmpleado_Registro = (List<Empleado_Registro>)Session["ListaEmpleadosPlanillaVacTruncas"];
                //int index = lstEmpleado_Registro.FindIndex(x => x.IdEmpleado == Convert.ToInt32(IdEmpleado));
                //lstEmpleado_Registro.RemoveAt(index);
                for (int i = 0; i < lstEmpleado_Registro.Count; i++)
                {
                    if (lstEmpleado_Registro[i].IdEmpleado == Convert.ToInt32(IdEmpleado))
                    {
                        lstEmpleado_Registro[i].DiasDescansoFisico = Convert.ToInt32(DiasDescansoFisico);
                        lstEmpleado_Registro[i].MesesDescansoFisico = Convert.ToInt32(MesesDescansoFisico);
                        lstEmpleado_Registro[i].DiasVacacionesTruncas = Convert.ToInt32(DiasVacacionesTruncas);
                        lstEmpleado_Registro[i].MesesVacacionesTruncas = Convert.ToInt32(MesesVacacionesTruncas);
                    }
                }
                //lstEmpleado_Registro..Where(x => x.IdEmpleado == Convert.ToInt32(IdEmpleado);
                //objEmpleado_Registro = new Empleado_Registro
                //{
                //    IdEmpleado = Convert.ToInt32(IdEmpleado),
                //    Nombre = NombreCompleto,
                //    DiasDescansoFisico = Convert.ToInt32(DiasDescansoFisico),
                //    MesesDescansoFisico = Convert.ToInt32(MesesDescansoFisico),
                //    DiasVacacionesTruncas = Convert.ToInt32(DiasVacacionesTruncas),
                //    MesesVacacionesTruncas = Convert.ToInt32(MesesVacacionesTruncas)
                //};
                //lstEmpleado_Registro.Add(objEmpleado_Registro);
                Session["ListaEmpleadosPlanillaVacTruncas"] = lstEmpleado_Registro.OrderBy(x => x.Paterno).ToList();
            }

            object respuesta = lstEmpleado_Registro;
            //return Json(respuesta, JsonRequestBehavior.AllowGet);
            return Json(new { success = "True", respuesta });
        }
        #endregion Planilla CAS Vacaciones Truncas
        /*-------------Planilla CAS Vacaciones Truncas---------------------*/

        /*-------------Planilla FED Vacaciones Truncas---------------------*/
        #region Planilla FED Vacaciones Truncas
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaGeneralFEDVacTruncas(string idCodFuenteFinanciamiento, string sMetas, string iMes, string iAnio, string iCodDetPlanilla)
        {
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaGeneralFEDVacTruncas(Convert.ToInt32(idCodFuenteFinanciamiento), sMetas, iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0, iCodDetPlanilla != string.Empty ? Convert.ToInt32(iCodDetPlanilla) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaIngresosFEDVacTruncas(string iCodTrabajador, string iMes, string iAnio, string iCodDetPlanilla)
        {
            string iCodTipoPlanilla = "2";
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaIngresosFEDVacTruncas(Convert.ToInt32(iCodTrabajador), iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0, iCodTipoPlanilla != string.Empty ? Convert.ToInt32(iCodTipoPlanilla) : 0, iCodDetPlanilla != string.Empty ? Convert.ToInt32(iCodDetPlanilla) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaDescuentosFEDVacTruncas(string iCodTrabajador, string iMes, string iAnio, string iCodDetPlanilla)
        {
            string iCodTipoPlanilla = "2";
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaDescuentosFEDVacTruncas(Convert.ToInt32(iCodTrabajador), iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0, iCodTipoPlanilla != string.Empty ? Convert.ToInt32(iCodTipoPlanilla) : 0, iCodDetPlanilla != string.Empty ? Convert.ToInt32(iCodDetPlanilla) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaGeneralCompletaFEDVacTruncas(string iMes, string iAnio, string iCodDetPlanilla)
        {
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaGeneralCompletaFEDVacTruncas(iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0, iCodDetPlanilla != string.Empty ? Convert.ToInt32(iCodDetPlanilla) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GenerarPlanillaFEDVacTruncas(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla, string iCodDetPlanilla, string strCodTrabajadores)
        {
            try
            {
                List<Empleado_Registro> lstEmpleado_Registro = new List<Empleado_Registro>();
                object respuesta = null;
                if (Session["ListaEmpleadosFEDPlanillaVacTruncas"] != null)
                {
                    lstEmpleado_Registro = (List<Empleado_Registro>)Session["ListaEmpleadosFEDPlanillaVacTruncas"];
                    respuesta = _planillas_Servicio.GenerarPlanillaCASVacTruncas(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla), Convert.ToInt32(iCodDetPlanilla), lstEmpleado_Registro);
                }
                if (respuesta != null)
                {
                    if (Session["ListaEmpleadosFEDPlanillaVacTruncas"] != null)
                    {
                        Session["ListaEmpleadosFEDPlanillaVacTruncas"] = null;
                    }
                }
                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult ListarEmpleadosFEDPlanillaVacTruncas(string strCodTipoCondicionTrabajador)
        {
            object respuesta = _planillas_Servicio.ListarEmpleadosPlanilla(strCodTipoCondicionTrabajador);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CargarTrabajadorPlanillaFEDVacTruncas(string strCodTipoCondicionTrabajador, string strDNI)
        {
            object respuesta = _planillas_Servicio.ListarEmpleadosPlanilla(strCodTipoCondicionTrabajador).Where(x => x.NroDocumento == strDNI && x.FED ==true).FirstOrDefault();
            if (respuesta == null)
            {
                respuesta = new Empleado_Registro
                {
                    IdEmpleado = 0
                };
            }
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CargarEmpleadosFEDPlanillaVacTruncasTemporal()
        {

            List<Empleado_Registro> lstEmpleado_Registro = new List<Empleado_Registro>();
            if (Session["ListaEmpleadosFEDPlanillaVacTruncas"] != null)
            {
                lstEmpleado_Registro = (List<Empleado_Registro>)Session["ListaEmpleadosFEDPlanillaVacTruncas"];
            }
            object respuesta = lstEmpleado_Registro;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AgregarTrabajadorFEDPlanillaVacTruncasTemporal(string iCodTrabajador, string strNombreCompleto, string iDiasDescansoFisico, string iMesesDescansoFisico, string iDiasVacTruncas, string iMesesVacTruncas)
        {
            Empleado_Registro objEmpleado_Registro = null;
            List<Empleado_Registro> lstEmpleado_Registro = new List<Empleado_Registro>();
            if (Session["ListaEmpleadosFEDPlanillaVacTruncas"] == null)
            {
                objEmpleado_Registro = new Empleado_Registro
                {
                    IdEmpleado = Convert.ToInt32(iCodTrabajador),
                    Nombre = strNombreCompleto,
                    DiasDescansoFisico = Convert.ToInt32(iDiasDescansoFisico),
                    MesesDescansoFisico = Convert.ToInt32(iMesesDescansoFisico),
                    DiasVacacionesTruncas = Convert.ToInt32(iDiasVacTruncas),
                    MesesVacacionesTruncas = Convert.ToInt32(iMesesVacTruncas)
                };
                lstEmpleado_Registro.Add(objEmpleado_Registro);
                Session["ListaEmpleadosFEDPlanillaVacTruncas"] = lstEmpleado_Registro;
            }
            else
            {
                lstEmpleado_Registro = (List<Empleado_Registro>)Session["ListaEmpleadosFEDPlanillaVacTruncas"];
                objEmpleado_Registro = new Empleado_Registro
                {
                    IdEmpleado = Convert.ToInt32(iCodTrabajador),
                    Nombre = strNombreCompleto,
                    DiasDescansoFisico = Convert.ToInt32(iDiasDescansoFisico),
                    MesesDescansoFisico = Convert.ToInt32(iMesesDescansoFisico),
                    DiasVacacionesTruncas = Convert.ToInt32(iDiasVacTruncas),
                    MesesVacacionesTruncas = Convert.ToInt32(iMesesVacTruncas)
                };
                lstEmpleado_Registro.Add(objEmpleado_Registro);
                Session["ListaEmpleadosFEDPlanillaVacTruncas"] = lstEmpleado_Registro;
            }

            object respuesta = lstEmpleado_Registro;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult QuitarEmpleadosFEDPlanillaVacTruncasTemporal(string iCodTrabajador)
        {
            //Empleado_Registro objEmpleado_Registro = null;
            List<Empleado_Registro> lstEmpleado_Registro = new List<Empleado_Registro>();
            if (Session["ListaEmpleadosFEDPlanillaVacTruncas"] != null)
            {
                lstEmpleado_Registro = (List<Empleado_Registro>)Session["ListaEmpleadosFEDPlanillaVacTruncas"];
                int obj = lstEmpleado_Registro.FindIndex(x => x.IdEmpleado == Convert.ToInt32(iCodTrabajador));
                lstEmpleado_Registro.RemoveAt(obj);
                Session["ListaEmpleadosFEDPlanillaVacTruncas"] = lstEmpleado_Registro;
            }
            object respuesta = lstEmpleado_Registro;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        #endregion Planilla FED Vacaciones Truncas

        #region Planilla SECIGRISTAS
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaGeneralSECIGRISTAS(string idCodFuenteFinanciamiento, string sMetas, string iMes, string iAnio)
        {
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaGeneralSECIGRISTAS(Convert.ToInt32(idCodFuenteFinanciamiento), sMetas, iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaIngresosSECIGRISTAS(string iCodTrabajador, string iMes, string iAnio)
        {
            string iCodTipoPlanilla = "1";
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaIngresosSECIGRISTAS(Convert.ToInt32(iCodTrabajador), iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0, iCodTipoPlanilla != string.Empty ? Convert.ToInt32(iCodTipoPlanilla) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaDescuentosSECIGRISTAS(string iCodTrabajador, string iMes, string iAnio)
        {
            string iCodTipoPlanilla = "1";
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaDescuentosSECIGRISTAS(Convert.ToInt32(iCodTrabajador), iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0, iCodTipoPlanilla != string.Empty ? Convert.ToInt32(iCodTipoPlanilla) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaGeneralCompletaSECIGRISTAS(string idCodFuenteFinanciamiento, string sMetas, string iMes, string iAnio)
        {
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaGeneralCompletaSECIGRISTAS(Convert.ToInt32(idCodFuenteFinanciamiento), sMetas, iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GenerarPlanillaSECIGRISTAS(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla)
        {
            try
            {

                object respuesta = _planillas_Servicio.GenerarPlanillaSECIGRISTAS(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla));

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult GenerarPlanillaPorTrabajadorSECIGRISTAS(string iCodTrabajador, string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla)
        {
            try
            {

                object respuesta = _planillas_Servicio.GenerarPlanillaPorTrabajdor(Convert.ToInt32(iCodTrabajador), Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla));//_planillas_Servicio.GenerarPlanillaCAS(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla));

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        #endregion Planilla SECIGRISTAS

        #region Planilla PRACTICANTES
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaGeneralPRACTICANTES(string idCodFuenteFinanciamiento, string sMetas, string iMes, string iAnio)
        {
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaGeneralPRACTICANTES(Convert.ToInt32(idCodFuenteFinanciamiento), sMetas, iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaIngresosPRACTICANTES(string iCodTrabajador, string iMes, string iAnio)
        {
            string iCodTipoPlanilla = "1";
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaIngresosPRACTICANTES(Convert.ToInt32(iCodTrabajador), iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0, iCodTipoPlanilla != string.Empty ? Convert.ToInt32(iCodTipoPlanilla) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaDescuentosPRACTICANTES(string iCodTrabajador, string iMes, string iAnio)
        {
            string iCodTipoPlanilla = "1";
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaDescuentosPRACTICANTES(Convert.ToInt32(iCodTrabajador), iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0, iCodTipoPlanilla != string.Empty ? Convert.ToInt32(iCodTipoPlanilla) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarPlanillaCalculadaGeneralCompletaPRACTICANTES(string idCodFuenteFinanciamiento, string sMetas, string iMes, string iAnio)
        {
            object respuesta = _planillas_Servicio.ListarPlanillaCalculadaGeneralCompletaPRACTICANTES(Convert.ToInt32(idCodFuenteFinanciamiento), sMetas, iMes != string.Empty ? Convert.ToInt32(iMes) : 0, iAnio != string.Empty ? Convert.ToInt32(iAnio) : 0);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GenerarPlanillaPRACTICANTES(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla)
        {
            try
            {

                object respuesta = _planillas_Servicio.GenerarPlanillaPRACTICANTES(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla));

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult GenerarPlanillaPorTrabajadorPRACTICANTES(string iCodTrabajador, string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla)
        {
            try
            {

                object respuesta = _planillas_Servicio.GenerarPlanillaPorTrabajdor(Convert.ToInt32(iCodTrabajador), Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla));//_planillas_Servicio.GenerarPlanillaCAS(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla));

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        #endregion Planilla PRACTICANTES


        /*-------------Planilla FED Vacaciones Truncas---------------------*/

        //public JsonResult ListarRegistroRegimenPensionario(string iMes, string iAnio)
        //{
        //    List<RegimenPensionario_Response> lista = new List<RegimenPensionario_Response>();
        //    lista.Add(new RegimenPensionario_Response("1", "1", "1", "1", "9", "2020", "Profuturo", "Profuturo - Flujo", "1.69", "1.35", "10", "9788.95", "A"));
        //    lista.Add(new RegimenPensionario_Response("2", "1", "2", "1", "9", "2020", "Profuturo", "Profuturo - Mixta", "0.67", "1.35", "10", "9788.95", "A"));
        //    lista.Add(new RegimenPensionario_Response("3", "1", "1", "2", "9", "2020", "Integra", "Integra - Flujo", "1.55", "1.35", "10", "9788.95", "A"));
        //    lista.Add(new RegimenPensionario_Response("4", "1", "2", "2", "9", "2020", "Integra", "Integra - Mixta", "0.00", "1.35", "10", "9788.95", "A"));
        //    lista.Add(new RegimenPensionario_Response("5", "1", "1", "3", "9", "2020", "Prima", "Prima - Flujo", "1.60", "1.35", "10", "9788.95", "A"));
        //    lista.Add(new RegimenPensionario_Response("6", "1", "2", "3", "9", "2020", "Prima", "Prima - Mixta", "0.18", "1.35", "10", "9788.95", "A"));
        //    lista.Add(new RegimenPensionario_Response("7", "1", "1", "4", "9", "2020", "Habitat", "Habitat - Flujo", "1.47", "1.35", "10", "9788.95", "A"));
        //    lista.Add(new RegimenPensionario_Response("8", "1", "2", "4", "9", "2020", "Habitat", "Habitat - Mixta", "0.38", "1.35", "10", "9788.95", "A"));
        //    lista.Add(new RegimenPensionario_Response("9", "2", "", "", "9", "2020", "ONP", "", "1.69", "1.352", "13", "0", "A"));
            

        //    object respuesta = lista;

        //    return Json(respuesta, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult ListarConceptos(string iMes, string iAnio)
        {
            List<Conceptos_Response> lista = new List<Conceptos_Response>();
            lista.Add(new Conceptos_Response("Contraprestación", "Ingresos", "", "X", "", ""));
            lista.Add(new Conceptos_Response("Reintegros", "Ingresos", "Variable", "X", "X", "X"));
            lista.Add(new Conceptos_Response("Copago Subsidio", "Ingresos", "Variable", "X", "", ""));
            lista.Add(new Conceptos_Response("Aguinaldos", "Ingresos", "", "X", "X", ""));
            lista.Add(new Conceptos_Response("Tardanzas", "Ingresos", "Automatico", "X", "", "X"));
            lista.Add(new Conceptos_Response("Inasistencias", "Ingresos", "Automatico", "X", "", "X"));
            lista.Add(new Conceptos_Response("Permisos", "Ingresos", "Automatico", "X", "X", "X"));
            lista.Add(new Conceptos_Response("Impuesto a la Renta 4ta", "Ingresos", "Automatico", "X", "", ""));
            lista.Add(new Conceptos_Response("Impuesto a la Renta 5ta", "Ingresos", "Automatico", "", "X", ""));
            lista.Add(new Conceptos_Response("ONP", "Ingresos", "Automatico", "X", "X", ""));
            lista.Add(new Conceptos_Response("Apo. Obligatoria AFP", "Ingresos", "Automatico", "X", "X", ""));
            lista.Add(new Conceptos_Response("Comision AFP", "Ingresos", "Automatico", "X", "X", ""));
            lista.Add(new Conceptos_Response("Prima AFP", "Ingresos", "Automatico", "X", "X", ""));
            lista.Add(new Conceptos_Response("Descuento Judicial", "Ingresos", "Fijo", "X", "X", ""));
            lista.Add(new Conceptos_Response("EsSalud Mas Vida", "Ingresos", "Fijo", "X", "X", ""));
            lista.Add(new Conceptos_Response("Rimac Seguro", "Ingresos", "Fijo", "X", "X", ""));
            lista.Add(new Conceptos_Response("Descuento Pago en Exceso", "Variable", "Automatico", "X", "X", ""));
            lista.Add(new Conceptos_Response("EPS", "Ingresos", "Fijo", "X", "X", ""));
            lista.Add(new Conceptos_Response("EsSalud", "Ingresos", "Automatico", "X", "X", ""));
            lista.Add(new Conceptos_Response("Compensación Economica", "Ingresos", "", "", "X", ""));
            lista.Add(new Conceptos_Response("Estipendio", "Ingresos", "", "", "", "X"));
            


            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        
        [HttpGet]
        [Authorize]
        public JsonResult ListarMeses()
        {
            List<Mes_Response> lista = new List<Mes_Response>();
            lista.Add(new Mes_Response("", "SELECCIONE"));
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
        public JsonResult ListarEstados()
        {
            List<Mes_Response> lista = new List<Mes_Response>();
            //lista.Add(new Mes_Response("", "SELECCIONE"));
            lista.Add(new Mes_Response("1", "CERRADO"));
            lista.Add(new Mes_Response("0", "ABIERTO"));
            object respuesta = lista;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Authorize]
        public JsonResult ListarAnios()
        {
            Anio_Response item = null;
            List<Anio_Response> lista = new List<Anio_Response>();
            //item = new Anio_Response();
            //item.Anio = "SELECCIONE";
            //lista.Add(item);
            item = new Anio_Response();
            item.Anio = DateTime.Now.Year.ToString();
            lista.Add(item);
            item = new Anio_Response();
            item.Anio = (DateTime.Now.Year - 1).ToString();
            lista.Add(item);
            item = new Anio_Response();
            item.Anio = (DateTime.Now.Year - 2).ToString();
            lista.Add(item);
            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Authorize]
        public JsonResult ListarReportes()
        {
            List<Reporte_Response> lista = new List<Reporte_Response>();
            lista.Add(new Reporte_Response("", "SELECCIONE"));
            lista.Add(new Reporte_Response("1", "Resumen General"));
            lista.Add(new Reporte_Response("2", "Resumen General con Cuotas Patronales"));
            lista.Add(new Reporte_Response("3", "Resumen General con Cuotas Patronales con Partida"));
            lista.Add(new Reporte_Response("4", "Resumen General por Fuente Financiamiento - Partida - Meta"));
            //lista.Add(new Reporte_Response("5", "Resumen General por Fuente Financiamiento - Partida - Meta - Cuotas Patronales"));
            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }        

        [HttpPost]
        [Authorize]
        public JsonResult EjecutarImportarAsistencia(PlanillaAsistencia_Registro registro)
        {
            try
            {
                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                //String nameFile = String.Empty;
                //if (registro.formatos != null) {
                //    foreach (String codigo in registro.formatos) {
                //        _planillas_Servicio
                //    }
                //}
                _planillas_Servicio.EjecutarImportarAsistencia(registro);

                //object respuesta = _convocatoria_Servicio.RegistrarConvocatoria(registro);

                return Json(new { success = "True", responseText = "" });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpGet]
        [Authorize]
        public JsonResult ListarPlanillas(PlanillaAsistencia_Registro registro)
        {
            try
            {
                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                object respuesta = _planillas_Servicio.ListarPlanillas(registro);

                return Json(respuesta, JsonRequestBehavior.AllowGet);

                //object respuesta = _convocatoria_Servicio.RegistrarConvocatoria(registro);
                //return Json(new { success = "True", responseText = "" });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpGet]
        [Authorize]
        public JsonResult ListarPlanillasCASFED(PlanillaAsistencia_Registro registro)
        {
            try
            {
                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;
                var ids = new[] { "1", "2", "12", "13" };
                object respuesta = _planillas_Servicio.ListarPlanillas(registro).Where(x => ids.Contains(x.IdPlanilla.ToString()));

                return Json(respuesta, JsonRequestBehavior.AllowGet);

                //object respuesta = _convocatoria_Servicio.RegistrarConvocatoria(registro);
                //return Json(new { success = "True", responseText = "" });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpGet]
        [Authorize]
        public JsonResult ListarPlanillasTodas(PlanillaAsistencia_Registro registro)
        {
            try
            {
                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;
                var ids = new[] { "1", "2","5", "12", "13" };
                object respuesta = _planillas_Servicio.ListarPlanillas(registro).Where(x => ids.Contains(x.IdPlanilla.ToString()));

                return Json(respuesta, JsonRequestBehavior.AllowGet);

                //object respuesta = _convocatoria_Servicio.RegistrarConvocatoria(registro);
                //return Json(new { success = "True", responseText = "" });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpGet]
        [Authorize]
        public JsonResult ListarPlanillasCASFEDBusq(PlanillaAsistencia_Registro registro)
        {
            try
            {
                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;
                var ids = new[] { "1", "2" };
                object respuesta = _planillas_Servicio.ListarPlanillas(registro).Where(x => ids.Contains(x.IdPlanilla.ToString()));

                return Json(respuesta, JsonRequestBehavior.AllowGet);

                //object respuesta = _convocatoria_Servicio.RegistrarConvocatoria(registro);
                //return Json(new { success = "True", responseText = "" });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpGet]
        [Authorize]
        public JsonResult ListarPlanillasSECI_PRAC(PlanillaAsistencia_Registro registro)
        {
            try
            {
                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;
                var ids = new[] { "12", "13" };
                object respuesta = _planillas_Servicio.ListarPlanillas(registro).Where(x => ids.Contains(x.IdPlanilla.ToString()));

                return Json(respuesta, JsonRequestBehavior.AllowGet);

                //object respuesta = _convocatoria_Servicio.RegistrarConvocatoria(registro);
                //return Json(new { success = "True", responseText = "" });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpGet]
        [Authorize]
        public JsonResult ListarPlanillasAdicionales(PlanillaAsistencia_Registro registro)
        {
            try
            {
                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;
                var ids = new[] { "1", "2" };
                object respuesta = _planillas_Servicio.ListarPlanillas(registro).Where(x=>x.IdTipoPlanilla==2 && ids.Contains(x.IdPlanilla.ToString()));

                return Json(respuesta, JsonRequestBehavior.AllowGet);

                //object respuesta = _convocatoria_Servicio.RegistrarConvocatoria(registro);
                //return Json(new { success = "True", responseText = "" });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpGet]
        [Authorize]
        public JsonResult ListarPlanillasAdicionalesFuncionales(PlanillaAsistencia_Registro registro)
        {
            try
            {
                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;
                var ids = new[] { "5" };
                object respuesta = _planillas_Servicio.ListarPlanillas(registro).Where(x => x.IdTipoPlanilla == 2 && ids.Contains(x.IdPlanilla.ToString()));

                return Json(respuesta, JsonRequestBehavior.AllowGet);

                //object respuesta = _convocatoria_Servicio.RegistrarConvocatoria(registro);
                //return Json(new { success = "True", responseText = "" });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpGet]
        [Authorize]
        public JsonResult ListarPlanillasAdicinalesCASFED()
        {
            List<PlanillaAsistencia_Registro> lista = new List<PlanillaAsistencia_Registro>();
            PlanillaAsistencia_Registro be = null;
            for (int i = 0; i < 2; i++)
            {
                be = new PlanillaAsistencia_Registro();
                be.IdPlanilla = i+1;
                be.IdTipoPlanilla = 2;
                if (i==0)
                {
                    be.NombrePlanilla = "Contraprestacion CAS";    
                }
                else
                {
                    be.NombrePlanilla = "Contraprestacion FED";
                }
                be.NombreTipoPlanilla = "Adicional";
                lista.Add(be);    
            }
            object respuesta = lista;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Authorize]
        public JsonResult ListarPlanillasVacTruncasCASFED()
        {
            List<PlanillaAsistencia_Registro> lista = new List<PlanillaAsistencia_Registro>();
            PlanillaAsistencia_Registro be = null;
            for (int i = 2; i < 4; i++)
            {
                be = new PlanillaAsistencia_Registro();
                be.IdPlanilla = i + 1;
                //be.IdTipoPlanilla = 1;
                if (i == 2)
                {
                    be.NombrePlanilla = "Vacaciones Truncas CAS";
                }
                else
                {
                    be.NombrePlanilla = "Vacaciones Truncas FED";
                }
                //be.NombreTipoPlanilla = "Adicional";
                lista.Add(be);
            }
            object respuesta = lista;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }   
        [HttpGet]
        public JsonResult ListarControlTrabajador(PlanillaAsistencia_Registro peticion)
        {
            object lista = _planillas_Servicio.ListarControlAsistencia(peticion);
            
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public JsonResult EliminarControlAsistenciaPermisosPorTrabajador(PlanillaControlAsistencia_Registro registro)
        {
            try
            {
                object respuesta = _planillas_Servicio.EliminarControlAsistenciaPermisosPorTrabajador(registro);
                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        /*-------------Administrar Planilla ---------------------*/
        #region Adminitrar Planilla
        [HttpPost]
        [Authorize]
        public JsonResult InsertarPlanilla(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla)
        {
            try
            {
                PlanillaEjecucion_Registro registro = new PlanillaEjecucion_Registro();
                registro.iMes = Convert.ToInt32(iMes);
                registro.iAnio = Convert.ToInt32(iAnio);
                registro.iCodPlanilla = Convert.ToInt32(iCodPlanilla);
                registro.iCodTipoPlanilla = Convert.ToInt32(iCodTipoPlanilla);
                registro.sUsuarioApertura = User.Identity.Name; //VariablesWeb.ConsultaInformacion.vNombreUsuario;
                object respuesta = _planillas_Servicio.InsertarPlanilla(registro);

                return Json(respuesta, JsonRequestBehavior.AllowGet);

                //object respuesta = _convocatoria_Servicio.RegistrarConvocatoria(registro);
                //return Json(new { success = "True", responseText = "" });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public JsonResult CerrarPlanilla(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla, string iCodDetPlanilla)
        {
            try
            {
                PlanillaEjecucion_Registro registro = new PlanillaEjecucion_Registro();
                registro.iMes = Convert.ToInt32(iMes);
                registro.iAnio = Convert.ToInt32(iAnio);
                registro.iCodPlanilla = Convert.ToInt32(iCodPlanilla);
                registro.iCodTipoPlanilla = Convert.ToInt32(iCodTipoPlanilla);
                registro.sUsuarioCierre = User.Identity.Name; //VariablesWeb.ConsultaInformacion.vNombreUsuario;
                registro.iCodDetPlanilla = Convert.ToInt32(iCodDetPlanilla);
                object respuesta = _planillas_Servicio.CerrarPlanilla(registro);

                return Json(respuesta, JsonRequestBehavior.AllowGet);

                //object respuesta = _convocatoria_Servicio.RegistrarConvocatoria(registro);
                //return Json(new { success = "True", responseText = "" });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        [Authorize]
        public JsonResult ModificarFasePlanilla(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla, string iCodDetPlanilla, string bEstadoRegAsistencia, string bEstadoDsctoFijoVariable, string bCerrado, string sObservacion)
        {
            try
            {
                PlanillaEjecucion_Registro registro = new PlanillaEjecucion_Registro();
                registro.iMes = Convert.ToInt32(iMes);
                registro.iAnio = Convert.ToInt32(iAnio);
                registro.iCodPlanilla = Convert.ToInt32(iCodPlanilla);
                registro.iCodTipoPlanilla = Convert.ToInt32(iCodTipoPlanilla);
                registro.sUsuarioCierre = User.Identity.Name; //VariablesWeb.ConsultaInformacion.vNombreUsuario;
                registro.iCodDetPlanilla = Convert.ToInt32(iCodDetPlanilla);
                registro.sUsuarioRegAsistencia = User.Identity.Name;
                registro.sUsuarioDsctoFijoVariable = User.Identity.Name;

                registro.bEstadoRegAsistencia = Convert.ToBoolean(Convert.ToInt16(bEstadoRegAsistencia));
                registro.bEstadoDsctoFijoVariable = Convert.ToBoolean(Convert.ToInt16(bEstadoDsctoFijoVariable));
                registro.bEstadoCierre = Convert.ToBoolean(Convert.ToInt16(bCerrado));
                registro.sObservacion = sObservacion;
                object respuesta = _planillas_Servicio.ModFasePlanilla(registro);

                return Json(respuesta, JsonRequestBehavior.AllowGet);

                //object respuesta = _convocatoria_Servicio.RegistrarConvocatoria(registro);
                //return Json(new { success = "True", responseText = "" });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult ListarPlanillasCreadas(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla)
        {
            PlanillaEjecucion_Registro registro = new PlanillaEjecucion_Registro();
            registro.iMes = string.IsNullOrEmpty(iMes) ? (int?)null : Convert.ToInt32(iMes);
            registro.iAnio = string.IsNullOrEmpty(iAnio) ? (int?)null : Convert.ToInt32(iAnio);
            registro.iCodPlanilla = string.IsNullOrEmpty(iCodPlanilla) ? (int?)null : Convert.ToInt32(iCodPlanilla);
            registro.iCodTipoPlanilla = string.IsNullOrEmpty(iCodTipoPlanilla) ? (int?)null : Convert.ToInt32(iCodTipoPlanilla);
            object respuesta = _planillas_Servicio.ListarPlanillasCreadas(registro);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarPlanillasBase()
        {
            object respuesta = _planillas_Servicio.ListarPlanillasBase();
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarTipoPlanillasBase()
        {
            object respuesta = _planillas_Servicio.ListarTipoPlanillasBase();
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarPlanillaCerrada(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla, string iCodDetPlanilla)
        {
            object respuesta = _planillas_Servicio.ListarPlanillaCerrada(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla), Convert.ToInt32(iCodDetPlanilla));
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ReporteResumenGeneral(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla, string iCodDetPlanilla)
        {
            object respuesta = _planillas_Servicio.ReporteResumenGeneral(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla), Convert.ToInt32(iCodDetPlanilla));
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult RRG2(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla, string iCodDetPlanilla2)
        //public JsonResult ReporteResumenGeneralPorFTE_FTO_Partida_Meta(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla)
        {
            object respuesta = _planillas_Servicio.RRG2(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla), Convert.ToInt32(iCodDetPlanilla2));
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult RRG3(string iMes2, string iAnio2, string iCodPlanilla2, string iCodTipoPlanilla2, string iCodDetPlanilla2)        
        {
            object respuesta = _planillas_Servicio.RRG3(Convert.ToInt32(iMes2), Convert.ToInt32(iAnio2), Convert.ToInt32(iCodPlanilla2), Convert.ToInt32(iCodTipoPlanilla2), Convert.ToInt32(iCodDetPlanilla2));
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        //[HttpPost]
        //public JsonResult ReporteDetalladoEsSalud(string iMes2, string iAnio2, string iCodPlanilla2, string iCodTipoPlanilla2, string iCodDetPlanilla2)
        //{
        //    object respuesta = _planillas_Servicio.ReporteDetalladoEsSalud(Convert.ToInt32(iMes2), Convert.ToInt32(iAnio2), Convert.ToInt32(iCodPlanilla2), Convert.ToInt32(iCodTipoPlanilla2), Convert.ToInt32(iCodDetPlanilla2));
        //    return Json(respuesta, JsonRequestBehavior.AllowGet);
        //}
        //[HttpPost]
        //public JsonResult GenerarArchivosTXT(ArchivosTXT_Response peticion)
        //{
        //    object respuesta = _planillas_Servicio.GenerarArchivosTXT(peticion);
        //    return Json(respuesta, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult GenerarArchivosTXT(ArchivosTXT_Response peticion, string iMes, string iAnio)
        {

            IEnumerable<ArchivosTXT_Response> respuesta = _planillas_Servicio.GenerarArchivosTXTMCPPWEB(peticion);
            string sRUC="20545565359";
            //bool rptaJOR = false;
            //bool rptaTOC = false;
            //bool rptaREM = false;
            bool rptaTXTCAS = false;
            bool rptaTXTServir = false;
            String ruta = Server.MapPath("~/temp/Planillas");
            DirectoryInfo dir = new DirectoryInfo(ruta);
            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.Delete();
            }
            int cantCAS = respuesta.Where(x => x.sCodPlanilla == "1" || x.sCodPlanilla == "2" || x.sCodPlanilla == "3" || x.sCodPlanilla == "4").ToList().Count();
            if (cantCAS>0)
            {
                //rptaJOR = GenerarArchivoJOR(respuesta, iMes, iAnio, sRUC);
                //rptaTOC = GenerarArchivoTOC(respuesta, iMes, iAnio, sRUC);
                //rptaREM = GenerarArchivoREM(respuesta, iMes, iAnio, sRUC);
                rptaTXTCAS = GenerarArchivoTXTCAS(respuesta, iMes, iAnio, sRUC, false);
            }
            
            int cantServir = respuesta.Where(x => x.sCodPlanilla == "5").ToList().Count();
            if (cantServir>0)
            {
                rptaTXTServir = GenerarArchivoTXTServir(respuesta, iMes, iAnio, sRUC);    
            }            
            if (rptaTXTCAS)
            {
                String NombreArchivo = "Planilla_TXT_" + iAnio + Convert.ToInt32(iMes).ToString("D2");
                
                string[] filePaths = Directory.GetFiles(ruta);
                String outFileName = Path.GetFileNameWithoutExtension(NombreArchivo) + ".zip";
                //String fileNameToAdd = Path.Combine(ruta, "data", fileToAdd);
                String zipFileName = Path.Combine(ruta, outFileName);

                //Crear el archivo (si quieres puedes editar uno existente cambiando el modo a Update.
                using (ZipArchive archive = ZipFile.Open(zipFileName, ZipArchiveMode.Create))
                {
                    foreach (var item in filePaths)
                    {
                        archive.CreateEntryFromFile(item, Path.GetFileName(item));
                    }                    
                }

                return Json(new { success = "True", responseText = "" });
            }
            else if (rptaTXTServir)
            {
                String NombreArchivo = "Planilla_TXT_" + iAnio + Convert.ToInt32(iMes).ToString("D2");
                
                string[] filePaths = Directory.GetFiles(ruta);
                String outFileName = Path.GetFileNameWithoutExtension(NombreArchivo) + ".zip";
                //String fileNameToAdd = Path.Combine(ruta, "data", fileToAdd);
                String zipFileName = Path.Combine(ruta, outFileName);

                //Crear el archivo (si quieres puedes editar uno existente cambiando el modo a Update.
                using (ZipArchive archive = ZipFile.Open(zipFileName, ZipArchiveMode.Create))
                {
                    foreach (var item in filePaths)
                    {
                        archive.CreateEntryFromFile(item, Path.GetFileName(item));
                    }                    
                }

                return Json(new { success = "True", responseText = "" });
            }
            else
            {
                return Json(new { success = "False", responseText = "" });
            }
        }
        [HttpPost]
        public JsonResult GenerarArchivosTXTJudicial(ArchivosTXT_Response peticion, string iMes, string iAnio)
        {

            IEnumerable<ArchivosTXT_Response> respuesta = _planillas_Servicio.GenerarArchivosTXTMCPPWEBJUDICIAL(peticion);
            string sRUC = "20545565359";
            //bool rptaJOR = false;
            //bool rptaTOC = false;
            //bool rptaREM = false;
            bool rptaTXTCAS = false;
            bool rptaTXTServir = false;
            String ruta = Server.MapPath("~/temp/Planillas");
            DirectoryInfo dir = new DirectoryInfo(ruta);
            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.Delete();
            }
            int cantCAS = respuesta.Where(x => x.sCodPlanilla == "1" || x.sCodPlanilla == "2" || x.sCodPlanilla == "3" || x.sCodPlanilla == "4").ToList().Count();
            if (cantCAS > 0)
            {
                //rptaJOR = GenerarArchivoJOR(respuesta, iMes, iAnio, sRUC);
                //rptaTOC = GenerarArchivoTOC(respuesta, iMes, iAnio, sRUC);
                //rptaREM = GenerarArchivoREM(respuesta, iMes, iAnio, sRUC);
                rptaTXTCAS = GenerarArchivoTXTCAS(respuesta, iMes, iAnio, sRUC, true);
            }

            int cantServir = respuesta.Where(x => x.sCodPlanilla == "5").ToList().Count();
            if (cantServir > 0)
            {
                rptaTXTServir = GenerarArchivoTXTServir(respuesta, iMes, iAnio, sRUC);
            }
            if (rptaTXTCAS)
            {
                String NombreArchivo = "Planilla_TXT_" + iAnio + Convert.ToInt32(iMes).ToString("D2");

                string[] filePaths = Directory.GetFiles(ruta);
                String outFileName = Path.GetFileNameWithoutExtension(NombreArchivo) + ".zip";
                //String fileNameToAdd = Path.Combine(ruta, "data", fileToAdd);
                String zipFileName = Path.Combine(ruta, outFileName);

                //Crear el archivo (si quieres puedes editar uno existente cambiando el modo a Update.
                using (ZipArchive archive = ZipFile.Open(zipFileName, ZipArchiveMode.Create))
                {
                    foreach (var item in filePaths)
                    {
                        archive.CreateEntryFromFile(item, Path.GetFileName(item));
                    }
                }

                return Json(new { success = "True", responseText = "" });
            }
            else if (rptaTXTServir)
            {
                String NombreArchivo = "Planilla_TXT_" + iAnio + Convert.ToInt32(iMes).ToString("D2");

                string[] filePaths = Directory.GetFiles(ruta);
                String outFileName = Path.GetFileNameWithoutExtension(NombreArchivo) + ".zip";
                //String fileNameToAdd = Path.Combine(ruta, "data", fileToAdd);
                String zipFileName = Path.Combine(ruta, outFileName);

                //Crear el archivo (si quieres puedes editar uno existente cambiando el modo a Update.
                using (ZipArchive archive = ZipFile.Open(zipFileName, ZipArchiveMode.Create))
                {
                    foreach (var item in filePaths)
                    {
                        archive.CreateEntryFromFile(item, Path.GetFileName(item));
                    }
                }

                return Json(new { success = "True", responseText = "" });
            }
            else
            {
                return Json(new { success = "False", responseText = "" });
            }
        }
        [HttpPost]
        public JsonResult GenerarArchivosPDT_PLAME(ArchivosTXT_Response peticion, string iMes, string iAnio)
        {

            IEnumerable<ArchivosTXT_Response> respuesta = _planillas_Servicio.GenerarArchivosTXT(peticion);
            string sRUC = "20545565359";
            bool rptaJOR = false;
            bool rptaTOC = false;
            bool rptaREM = false;
            bool rptaSNL = false;
            //bool rptaTXTCAS = false;
            //bool rptaTXTServir = false;
            String ruta = Server.MapPath("~/temp/Planillas");
            DirectoryInfo dir = new DirectoryInfo(ruta);
            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.Delete();
            }
            //int cantCAS = respuesta.Where(x => x.sCodPlanilla == "1" || x.sCodPlanilla == "2" || x.sCodPlanilla == "3" || x.sCodPlanilla == "4").ToList().Count();
            //if (cantCAS > 0)
            //{

            if (respuesta!=null && respuesta.ToList().Count>0)
            {
                rptaJOR = GenerarArchivoJOR(respuesta, iMes, iAnio, sRUC);
                rptaTOC = GenerarArchivoTOC(respuesta, iMes, iAnio, sRUC);
                rptaREM = GenerarArchivoREM(respuesta, iMes, iAnio, sRUC);
                rptaSNL = GenerarArchivoSNL(respuesta, iMes, iAnio, sRUC); 
            }
                //rptaTXTCAS = GenerarArchivoTXTCAS(respuesta, iMes, iAnio, sRUC);
            //}

            //int cantServir = respuesta.Where(x => x.sCodPlanilla == "5").ToList().Count();
            //if (cantServir > 0)
            //{
            //    rptaTXTServir = GenerarArchivoTXTServir(respuesta, iMes, iAnio, sRUC);
            //}
            if (rptaJOR && rptaTOC && rptaREM && rptaSNL)
            {
                String NombreArchivo = "Planilla_PDT_PLAME_" + iAnio + Convert.ToInt32(iMes).ToString("D2");

                string[] filePaths = Directory.GetFiles(ruta);
                String outFileName = Path.GetFileNameWithoutExtension(NombreArchivo) + ".zip";
                //String fileNameToAdd = Path.Combine(ruta, "data", fileToAdd);
                String zipFileName = Path.Combine(ruta, outFileName);

                //Crear el archivo (si quieres puedes editar uno existente cambiando el modo a Update.
                using (ZipArchive archive = ZipFile.Open(zipFileName, ZipArchiveMode.Create))
                {
                    foreach (var item in filePaths)
                    {
                        archive.CreateEntryFromFile(item, Path.GetFileName(item));
                    }
                }

                return Json(new { success = "True", responseText = "" });
            }
            else
            {
                return Json(new { success = "False", responseText = "" });
            }
            
        } 
        public bool GenerarArchivoJOR(IEnumerable<ArchivosTXT_Response> peticion, string iMes, string iAnio, string sRUC)
        {

            bool rpta = false;
            //string[] arrayFile = file.Split(',');            
            String NombreArchivo = "0601" + iAnio + Convert.ToInt32(iMes).ToString("D2") + sRUC;
            String ExtensionArchivo = "JOR";
            String Archivo = NombreArchivo + "." + ExtensionArchivo;
            String ruta = Server.MapPath("~/temp/Planillas");
            using (StreamWriter sw = System.IO.File.CreateText(ruta +"/"+ Archivo))
            {
                var jorGroup = from peticiones in peticion
                               where peticiones.sHorasJOR !="0"
                          group peticiones by new { peticiones.sCodTipoDocumento, peticiones.sNumeroDocumento, peticiones.sHorasJOR } into peticionesGroup
                          select peticionesGroup;
                foreach (var item in jorGroup.ToList())
                {
                    sw.WriteLine(item.Key.sCodTipoDocumento.Trim() + "|" + item.Key.sNumeroDocumento.Trim() + "|" + item.Key.sHorasJOR.Trim() + "|0|0|0|");
                }
                sw.Close();
                sw.Dispose();
                rpta = true;                
                
            }            
            return rpta;
        }
        public bool GenerarArchivoTOC(IEnumerable<ArchivosTXT_Response> peticion, string iMes, string iAnio, string sRUC)
        {

            bool rpta = false;
            //string[] arrayFile = file.Split(',');            
            String NombreArchivo = "0601" + iAnio + Convert.ToInt32(iMes).ToString("D2") + sRUC;
            String ExtensionArchivo = "TOC";
            String Archivo = NombreArchivo + "." + ExtensionArchivo;
            String ruta = Server.MapPath("~/temp/Planillas");
            using (StreamWriter sw = System.IO.File.CreateText(ruta + "/" + Archivo))
            {
                int count = peticion.Where(x => x.sCodigoTOC == "1").ToList().Count();
                if (count>0)
                {
                    var tocGroup = from peticiones in peticion
                                   where peticiones.sCodigoTOC == "1"
                                   group peticiones by new { peticiones.sCodTipoDocumento, peticiones.sNumeroDocumento} into peticionesGroup
                                   select peticionesGroup;
                    foreach (var item in tocGroup.ToList())
                    {
                        sw.WriteLine(item.Key.sCodTipoDocumento.Trim() + "|" + item.Key.sNumeroDocumento.Trim() + "|0|1||1|");
                    } 
                }
                sw.Close();
                sw.Dispose();
                rpta = true;

            }
            return rpta;
        }
        public bool GenerarArchivoREM(IEnumerable<ArchivosTXT_Response> peticion, string iMes, string iAnio, string sRUC)
        {

            bool rpta = false;
            //string[] arrayFile = file.Split(',');            
            String NombreArchivo = "0601" + iAnio + Convert.ToInt32(iMes).ToString("D2") + sRUC;
            String ExtensionArchivo = "rem";
            String Archivo = NombreArchivo + "." + ExtensionArchivo;
            String ruta = Server.MapPath("~/temp/Planillas");
            using (StreamWriter sw = System.IO.File.CreateText(ruta + "/" + Archivo))
            {
                string codExternoEPS = peticion.Where(x => x.sConcepto == "EPS").FirstOrDefault() != null ? peticion.Where(x => x.sConcepto == "EPS").FirstOrDefault().sCodigoExterno.ToString() : null;
                string codExternoCONTRAPREST = peticion.Where(x => x.sConcepto == "CONTRAPREST.").FirstOrDefault() != null ? peticion.Where(x => x.sConcepto == "CONTRAPREST.").FirstOrDefault().sCodigoExterno.ToString() : null;
                string strMonto = string.Empty;
                if (codExternoEPS != null)
                {
                    foreach (var item in peticion.Where(x => x.sCodigoExterno != "" && !x.sCodigoExterno.Contains(codExternoCONTRAPREST) && x.sCodTipoConcepto == "1").ToList())
                    {
                        strMonto = (item.dcMonto > Convert.ToInt32(Math.Floor(item.dcMonto)) ? item.dcMonto.ToString() : Convert.ToInt32(Math.Floor(item.dcMonto)).ToString());
                        //strMonto = Convert.ToInt32(Math.Floor(item.dcMonto)).ToString();
                        sw.WriteLine(item.sCodTipoDocumento.Trim() + "|" + item.sNumeroDocumento.Trim() + "|" + item.sCodigoExterno + "|" + strMonto + "|" + strMonto + "|");
                    }


                    foreach (var item in peticion.Where(x => x.sCodigoExterno != "" && !x.sCodigoExterno.Contains(codExternoEPS) && x.sCodTipoConcepto == "2").ToList())
                    {
                        strMonto = (item.dcMonto > Convert.ToInt32(Math.Floor(item.dcMonto)) ? item.dcMonto.ToString() : Convert.ToInt32(Math.Floor(item.dcMonto)).ToString());
                        //strMonto = Convert.ToInt32(Math.Floor(item.dcMonto)).ToString();
                        sw.WriteLine(item.sCodTipoDocumento.Trim() + "|" + item.sNumeroDocumento.Trim() + "|" + item.sCodigoExterno + "|" + strMonto + "|" + strMonto + "|");
                    }
                    var totals = peticion.Where(x => x.sCodigoExterno != "" && x.sCodigoExterno.Contains(codExternoEPS) && x.sCodTipoConcepto == "2").ToList()
                    .GroupBy(x => new { x.sCodTipoDocumento, x.sNumeroDocumento, x.sCodigoExterno }).Select(y =>
                        new
                        {
                            sCodTipoDocumento = y.Key.sCodTipoDocumento,
                            sNumeroDocumento = y.Key.sNumeroDocumento,
                            sCodigoExterno = y.Key.sCodigoExterno,
                            dcMonto = y.Sum(i => i.dcMonto)
                        });
                    
                    var totalsContraprestacion = peticion.Where(x => x.sCodigoExterno != "" && x.sCodigoExterno.Contains(codExternoCONTRAPREST) && x.sCodTipoConcepto == "1").ToList()
                    .GroupBy(x => new { x.sCodTipoDocumento, x.sNumeroDocumento, x.sCodigoExterno }).Select(y =>
                        new
                        {
                            sCodTipoDocumento = y.Key.sCodTipoDocumento,
                            sNumeroDocumento = y.Key.sNumeroDocumento,
                            sCodigoExterno = y.Key.sCodigoExterno,
                            dcMonto = y.Sum(i => i.dcMonto)
                        });
                    //foreach (var item in peticion.Where(x => x.sCodigoExterno != "" && !x.sCodigoExterno.Contains("0706") && x.sCodTipoConcepto == "2").ToList())
                    foreach (var item in totals)
                    {
                        strMonto = (item.dcMonto > Convert.ToInt32(Math.Floor(item.dcMonto)) ? item.dcMonto.ToString() : Convert.ToInt32(Math.Floor(item.dcMonto)).ToString());
                        //strMonto = Convert.ToInt32(Math.Floor(item.dcMonto)).ToString();
                        sw.WriteLine(item.sCodTipoDocumento + "|" + item.sNumeroDocumento + "|" + item.sCodigoExterno + "|" + strMonto + "|" + strMonto + "|");
                    }
                    foreach (var item in totalsContraprestacion)
                    {
                        strMonto = (item.dcMonto > Convert.ToInt32(Math.Floor(item.dcMonto)) ? item.dcMonto.ToString() : Convert.ToInt32(Math.Floor(item.dcMonto)).ToString());
                        //strMonto = Convert.ToInt32(Math.Floor(item.dcMonto)).ToString();
                        sw.WriteLine(item.sCodTipoDocumento + "|" + item.sNumeroDocumento + "|" + item.sCodigoExterno + "|" + strMonto + "|" + strMonto + "|");
                    }
                }
                sw.Close();
                sw.Dispose();
                rpta = true;           
            }
        
            return rpta;
        }

        public bool GenerarArchivoSNL(IEnumerable<ArchivosTXT_Response> peticion, string iMes, string iAnio, string sRUC)
        {

            bool rpta = false;
            //string[] arrayFile = file.Split(',');            
            String NombreArchivo = "0601" + iAnio + Convert.ToInt32(iMes).ToString("D2") + sRUC;
            String ExtensionArchivo = "snl";
            String Archivo = NombreArchivo + "." + ExtensionArchivo;
            String ruta = Server.MapPath("~/temp/Planillas");
            using (StreamWriter sw = System.IO.File.CreateText(ruta + "/" + Archivo))
            {
                var jorGroup = from peticiones in peticion
                               where peticiones.iVacaciones > 0 ||
                                    peticiones.iLicencia_sin_Goce > 0 ||
                                    peticiones.iSancion_Disciplinaria > 0 ||
                                    peticiones.iEnfermedad_o_Accidente > 0 ||
                                    peticiones.iLicencia_con_Goce > 0 ||
                                    peticiones.iLicencia_por_Paternidad > 0 ||
                                    peticiones.iLicencia_por_Fallecimiento_de_Padres > 0 ||
                                    peticiones.iLicencia_por_Enfermedad_Grave_o_Terminal_o_Accidente_Grave_de_Familiares_Directos > 0
                               group peticiones by new { 
                                   peticiones.sCodTipoDocumento, 
                                   peticiones.sNumeroDocumento, 
                                   peticiones.iVacaciones,
                                   peticiones.iLicencia_sin_Goce,
                                   peticiones.iSancion_Disciplinaria,
                                   peticiones.iEnfermedad_o_Accidente,
                                   peticiones.iLicencia_con_Goce,
                                   peticiones.iLicencia_por_Paternidad,
                                   peticiones.iLicencia_por_Fallecimiento_de_Padres,
                                   peticiones.iLicencia_por_Enfermedad_Grave_o_Terminal_o_Accidente_Grave_de_Familiares_Directos
                               } into peticionesGroup
                               select peticionesGroup;
                foreach (var item in jorGroup.ToList())
                {
                    if (item.Key.iVacaciones>0)
                    {
                        sw.WriteLine(item.Key.sCodTipoDocumento.Trim() + "|" + item.Key.sNumeroDocumento.Trim() + "|23|" + item.Key.iVacaciones.ToString("D2"));    
                    }
                    if (item.Key.iLicencia_sin_Goce > 0)
                    {
                        sw.WriteLine(item.Key.sCodTipoDocumento.Trim() + "|" + item.Key.sNumeroDocumento.Trim() + "|05|" + item.Key.iVacaciones.ToString("D2"));
                    }
                    if (item.Key.iSancion_Disciplinaria > 0)
                    {
                        sw.WriteLine(item.Key.sCodTipoDocumento.Trim() + "|" + item.Key.sNumeroDocumento.Trim() + "|01|" + item.Key.iVacaciones.ToString("D2"));
                    }
                    if (item.Key.iEnfermedad_o_Accidente > 0)
                    {
                        sw.WriteLine(item.Key.sCodTipoDocumento.Trim() + "|" + item.Key.sNumeroDocumento.Trim() + "|20|" + item.Key.iVacaciones.ToString("D2"));
                    }
                    if (item.Key.iLicencia_con_Goce > 0)
                    {
                        sw.WriteLine(item.Key.sCodTipoDocumento.Trim() + "|" + item.Key.sNumeroDocumento.Trim() + "|26|" + item.Key.iVacaciones.ToString("D2"));
                    }
                    if (item.Key.iLicencia_por_Paternidad > 0)
                    {
                        sw.WriteLine(item.Key.sCodTipoDocumento.Trim() + "|" + item.Key.sNumeroDocumento.Trim() + "|28|" + item.Key.iVacaciones.ToString("D2"));
                    }
                    if (item.Key.iLicencia_por_Fallecimiento_de_Padres > 0)
                    {
                        sw.WriteLine(item.Key.sCodTipoDocumento.Trim() + "|" + item.Key.sNumeroDocumento.Trim() + "|32|" + item.Key.iVacaciones.ToString("D2"));
                    }
                    if (item.Key.iLicencia_por_Enfermedad_Grave_o_Terminal_o_Accidente_Grave_de_Familiares_Directos > 0)
                    {
                        sw.WriteLine(item.Key.sCodTipoDocumento.Trim() + "|" + item.Key.sNumeroDocumento.Trim() + "|35|" + item.Key.iVacaciones.ToString("D2"));
                    }
                }
                sw.Close();
                sw.Dispose();
                rpta = true;

            }
            return rpta;
        }
        //public bool GenerarArchivoTXTCAS(IEnumerable<ArchivosTXT_Response> peticion, string iMes, string iAnio, string sRUC)
        //{

        //    bool rpta = false;
        //    //string[] arrayFile = file.Split(',');            
        //    string strCodigoTXT2 = "01";
        //    string strCodigoTXTTipoPlanilla = "03";
        //    String NombreArchivo = "PLL001424" + iAnio + Convert.ToInt32(iMes).ToString("D2") + strCodigoTXT2 + strCodigoTXTTipoPlanilla + Convert.ToInt32(1).ToString("D4");
        //    String ExtensionArchivo = "TXT";
            
        //    String Archivo = NombreArchivo + "." + ExtensionArchivo;
        //    String ruta = Server.MapPath("~/temp/Planillas");
        //    ArchivosTXT_Response be = null;
        //    List<ArchivosTXT_Response> lstArchivo = new List<ArchivosTXT_Response>();
        //    using (StreamWriter sw = System.IO.File.CreateText(ruta + "/" + Archivo))
        //    {
        //        lstArchivo = peticion.ToList();
        //        string codExternoEPS = peticion.Where(x => x.sConcepto == "EPS").FirstOrDefault().sCodigoExterno.ToString();
        //        string strCodigoTXT1 = "001424";
        //        string strCantReg = peticion.Where(x => x.sCodPlanilla == "1" || x.sCodPlanilla == "2").ToList().Count().ToString();
        //        string strMontoTotalIng = peticion.Where(x => x.sCodTipoConcepto == "1" && (x.sCodPlanilla == "1" || x.sCodPlanilla == "2")).ToList().Sum(x => x.dcMonto).ToString();
        //        string strMontoTotalDsctos = peticion.Where(x => x.sCodTipoConcepto == "2" && (x.sCodPlanilla == "1" || x.sCodPlanilla == "2")).ToList().Sum(x => x.dcMonto).ToString();
        //        string strMontoTotalAportes = peticion.Where(x => x.sCodTipoConcepto == "3" && (x.sCodPlanilla == "1" || x.sCodPlanilla == "2")).ToList().Sum(x => x.dcMonto).ToString();

        //        sw.WriteLine(strCodigoTXT1 + "|" + iAnio + "|" + Convert.ToInt32(iMes).ToString("D2") + "|" + strCodigoTXT2 + "|" + strCodigoTXTTipoPlanilla + "|" + Convert.ToInt32(1).ToString("D4") + "|" + strCantReg + "|" + strMontoTotalIng + "|" + strMontoTotalDsctos + "|" + strMontoTotalAportes + "|" + "0.00" + "|" + "0.00");

        //        var dnis = peticion.Where(x => x.sCodPlanilla == "1" || x.sCodPlanilla == "2").ToList()
        //            .GroupBy(x => new { x.sNumeroDocumento}).Select(y =>
        //                new
        //                {                            
        //                    sNumeroDocumento = y.Key.sNumeroDocumento                            
        //                });
        //        foreach (var item in dnis)
        //        {
        //            decimal dcMontoIng = peticion.Where(x => x.sCodTipoConcepto == "1" && x.sNumeroDocumento == item.sNumeroDocumento && (x.sCodPlanilla == "1" || x.sCodPlanilla == "2")).ToList().Sum(x => x.dcMonto);
        //            decimal dcMontoDscto = peticion.Where(x => x.sCodTipoConcepto == "2" && x.sNumeroDocumento == item.sNumeroDocumento && (x.sCodPlanilla == "1" || x.sCodPlanilla == "2")).ToList().Sum(x => x.dcMonto);

        //            be = new ArchivosTXT_Response();

        //            be.sCodPlanilla = peticion.Where(x => x.sCodTipoConcepto == "1" && x.sNumeroDocumento == item.sNumeroDocumento && (x.sCodPlanilla == "1" || x.sCodPlanilla == "2")).FirstOrDefault().sCodPlanilla;
        //            be.sCodTipoDocumento = peticion.Where(x => x.sCodTipoConcepto == "1" && x.sNumeroDocumento == item.sNumeroDocumento && (x.sCodPlanilla == "1" || x.sCodPlanilla == "2")).FirstOrDefault().sCodTipoDocumento;
        //            be.sNumeroDocumento = peticion.Where(x => x.sCodTipoConcepto == "1" && x.sNumeroDocumento == item.sNumeroDocumento && (x.sCodPlanilla == "1" || x.sCodPlanilla == "2")).FirstOrDefault().sNumeroDocumento;
        //            be.sCodTipFuenteFinanciamiento = peticion.Where(x => x.sCodTipoConcepto == "1" && x.sNumeroDocumento == item.sNumeroDocumento && (x.sCodPlanilla == "1" || x.sCodPlanilla == "2")).FirstOrDefault().sCodTipFuenteFinanciamiento;
        //            be.sFuenteFinanciamiento = peticion.Where(x => x.sCodTipoConcepto == "1" && x.sNumeroDocumento == item.sNumeroDocumento && (x.sCodPlanilla == "1" || x.sCodPlanilla == "2")).FirstOrDefault().sFuenteFinanciamiento;
        //            be.sCodTipoConcepto = "9";
        //            //be.sTipoConcepto = peticion.Where(x => x.sCodTipoConcepto == "1" && x.sNumeroDocumento == item.sNumeroDocumento && (x.sCodPlanilla == "1" || x.sCodPlanilla == "2")).FirstOrDefault().sTipoConcepto;
        //            be.sCodConcepto = "9999";
        //            be.sConcepto = "NETO";
        //            //be.sNombreConcepto = dr.GetString(dr.GetOrdinal("vNombreConcepto"));
        //            be.dcMonto = dcMontoIng - dcMontoDscto;
        //            be.sCodigoAIRHSP = peticion.Where(x => x.sCodTipoConcepto == "1" && x.sNumeroDocumento == item.sNumeroDocumento && (x.sCodPlanilla == "1" || x.sCodPlanilla == "2")).FirstOrDefault().sCodigoAIRHSP;
        //            be.sCodTipRegSISPER = peticion.Where(x => x.sCodTipoConcepto == "1" && x.sNumeroDocumento == item.sNumeroDocumento && (x.sCodPlanilla == "1" || x.sCodPlanilla == "2")).FirstOrDefault().sCodTipRegSISPER;
        //            lstArchivo.Add(be);
        //        }
        //        foreach (var item in lstArchivo.Where(x => x.sCodPlanilla == "1" || x.sCodPlanilla == "2").ToList().OrderBy(x => x.sNumeroDocumento).ThenBy(y => y.sCodTipoConcepto))
        //        {
        //            sw.WriteLine(item.sCodTipoDocumento.Trim() + "|" + item.sNumeroDocumento.Trim() + "|" + item.sCodTipFuenteFinanciamiento + "|" + item.sCodTipoConcepto + "|" + item.sCodConcepto + "|" + item.sConcepto + "|" + item.dcMonto + "|" + item.sCodigoAIRHSP + "|" + item.sCodTipRegSISPER);
        //        }                
        //        sw.Close();
        //        sw.Dispose();
        //        rpta = true;
        //    }

        //    return rpta;
        //}

        public bool GenerarArchivoTXTCAS(IEnumerable<ArchivosTXT_Response> peticion, string iMes, string iAnio, string sRUC, bool bJudicial)
        {

            bool rpta = false;
            //string[] arrayFile = file.Split(',');            
            string strCodigoTXT2 = string.Empty;
            string strCodigoTXTTipoPlanilla = string.Empty;
            String NombreArchivo = "PLL001424" + iAnio + Convert.ToInt32(iMes).ToString("D2") + strCodigoTXT2 + strCodigoTXTTipoPlanilla + Convert.ToInt32(1).ToString("D4");
            String ExtensionArchivo = "TXT";

            String Archivo = NombreArchivo + "." + ExtensionArchivo;
            String ruta = Server.MapPath("~/temp/Planillas");
            ArchivosTXT_Response be = null;
            List<ArchivosTXT_Response> lstArchivo = new List<ArchivosTXT_Response>();
            using (StreamWriter sw = System.IO.File.CreateText(ruta + "/" + Archivo))
            {
                //if (bJudicial)
                //{
                //    lstArchivo = peticion.Where(x => x.sCodConcepto == "0025").ToList();    
                //}
                //else
                //{
                //    lstArchivo = peticion.Where(x => x.sCodConcepto != "0025").ToList();
                //}
                
                //string codExternoEPS = peticion.Where(x => x.sConcepto == "EPS").FirstOrDefault() != null ? peticion.Where(x => x.sConcepto == "EPS").FirstOrDefault().sCodigoExterno.ToString() : null;
                string strCodigoTXT1 = "001424";
                string strCantReg = string.Empty;
                string strMontoTotalIng = string.Empty;
                string strMontoTotalDsctos = string.Empty;
                string strMontoTotalAportes = string.Empty;
                if (!bJudicial)
                {
                    strCodigoTXT2 = "01";
                    strCodigoTXTTipoPlanilla = "03";
                    strCantReg = peticion.Where(x => x.sCodPlanilla == "1" || x.sCodPlanilla == "2" || x.sCodPlanilla == "3" || x.sCodPlanilla == "4" && (x.iCodTipoCondicionTrabajador == 1 || x.iCodTipoCondicionTrabajador == 6 || x.iCodTipoCondicionTrabajador == 8)).ToList().Count().ToString();
                    strMontoTotalIng = peticion.Where(x => x.sCodTipoConcepto == "1" && (x.sCodPlanilla == "1" || x.sCodPlanilla == "2" || x.sCodPlanilla == "3" || x.sCodPlanilla == "4") && (x.iCodTipoCondicionTrabajador == 1 || x.iCodTipoCondicionTrabajador == 6 || x.iCodTipoCondicionTrabajador == 8)).ToList().Sum(x => x.dcMonto).ToString();
                    strMontoTotalDsctos = peticion.Where(x => x.sCodTipoConcepto == "2" && (x.sCodPlanilla == "1" || x.sCodPlanilla == "2" || x.sCodPlanilla == "3" || x.sCodPlanilla == "4") && (x.iCodTipoCondicionTrabajador == 1 || x.iCodTipoCondicionTrabajador == 6 || x.iCodTipoCondicionTrabajador == 8)).ToList().Sum(x => x.dcMonto).ToString();
                    strMontoTotalAportes = peticion.Where(x => x.sCodTipoConcepto == "3" && (x.sCodPlanilla == "1" || x.sCodPlanilla == "2" || x.sCodPlanilla == "3" || x.sCodPlanilla == "4") && (x.iCodTipoCondicionTrabajador == 1 || x.iCodTipoCondicionTrabajador == 6 || x.iCodTipoCondicionTrabajador == 8)).ToList().Sum(x => x.dcMonto).ToString(); 
                }
                else
                {
                    strCodigoTXT2 = "04";
                    strCodigoTXTTipoPlanilla = "12";
                    strCantReg = peticion.Where(x => x.sCodPlanilla == "1" || x.sCodPlanilla == "2" || x.sCodPlanilla == "3" || x.sCodPlanilla == "4" && (x.iCodTipoCondicionTrabajador == 1 || x.iCodTipoCondicionTrabajador == 6 || x.iCodTipoCondicionTrabajador == 8)).ToList().Count().ToString();
                    strMontoTotalIng = peticion.Where(x => x.sCodTipoConcepto == "1" && (x.sCodPlanilla == "1" || x.sCodPlanilla == "2" || x.sCodPlanilla == "3" || x.sCodPlanilla == "4") && (x.iCodTipoCondicionTrabajador == 1 || x.iCodTipoCondicionTrabajador == 6 || x.iCodTipoCondicionTrabajador == 8)).ToList().Sum(x => x.dcMonto).ToString();
                    strMontoTotalDsctos = "0.00";
                    strMontoTotalAportes = "0.00"; 
                }

                //sw.WriteLine(strCodigoTXT1 + "|" + iAnio + "|" + Convert.ToInt32(iMes).ToString("D2") + "|" + strCodigoTXT2 + "|" + strCodigoTXTTipoPlanilla + "|" + Convert.ToInt32(1).ToString("D4") + "|" + strCantReg + "|" + strMontoTotalIng + "|" + strMontoTotalDsctos + "|" + strMontoTotalAportes + "|" + "0.00" + "|" + "0.00");
                sw.WriteLine(strCodigoTXT1 + "|" + iAnio + "|" + Convert.ToInt32(iMes).ToString("D2") + "|" + strCodigoTXT2 + "|" + strCodigoTXTTipoPlanilla + "|" + Convert.ToInt32(1).ToString("D4") + "|" + strCantReg + "|" + strMontoTotalIng + "|" + strMontoTotalDsctos + "|" + strMontoTotalAportes);

                /*var dnis = peticion.Where(x => x.sCodPlanilla == "1" || x.sCodPlanilla == "2").ToList()
                    .GroupBy(x => new { x.sNumeroDocumento }).Select(y =>
                        new
                        {
                            sNumeroDocumento = y.Key.sNumeroDocumento
                        });
                foreach (var item in dnis)
                {
                    decimal dcMontoIng = peticion.Where(x => x.sCodTipoConcepto == "1" && x.sNumeroDocumento == item.sNumeroDocumento && (x.sCodPlanilla == "1" || x.sCodPlanilla == "2")).ToList().Sum(x => x.dcMonto);
                    decimal dcMontoDscto = peticion.Where(x => x.sCodTipoConcepto == "2" && x.sNumeroDocumento == item.sNumeroDocumento && (x.sCodPlanilla == "1" || x.sCodPlanilla == "2")).ToList().Sum(x => x.dcMonto);

                    be = new ArchivosTXT_Response();

                    be.sCodPlanilla = peticion.Where(x => x.sCodTipoConcepto == "1" && x.sNumeroDocumento == item.sNumeroDocumento && (x.sCodPlanilla == "1" || x.sCodPlanilla == "2")).FirstOrDefault().sCodPlanilla;
                    be.sCodTipoDocumento = peticion.Where(x => x.sCodTipoConcepto == "1" && x.sNumeroDocumento == item.sNumeroDocumento && (x.sCodPlanilla == "1" || x.sCodPlanilla == "2")).FirstOrDefault().sCodTipoDocumento;
                    be.sNumeroDocumento = peticion.Where(x => x.sCodTipoConcepto == "1" && x.sNumeroDocumento == item.sNumeroDocumento && (x.sCodPlanilla == "1" || x.sCodPlanilla == "2")).FirstOrDefault().sNumeroDocumento;
                    be.sCodTipFuenteFinanciamiento = peticion.Where(x => x.sCodTipoConcepto == "1" && x.sNumeroDocumento == item.sNumeroDocumento && (x.sCodPlanilla == "1" || x.sCodPlanilla == "2")).FirstOrDefault().sCodTipFuenteFinanciamiento;
                    be.sFuenteFinanciamiento = peticion.Where(x => x.sCodTipoConcepto == "1" && x.sNumeroDocumento == item.sNumeroDocumento && (x.sCodPlanilla == "1" || x.sCodPlanilla == "2")).FirstOrDefault().sFuenteFinanciamiento;                    
                    be.sCodTipoConcepto = "9";
                    //be.sTipoConcepto = peticion.Where(x => x.sCodTipoConcepto == "1" && x.sNumeroDocumento == item.sNumeroDocumento && (x.sCodPlanilla == "1" || x.sCodPlanilla == "2")).FirstOrDefault().sTipoConcepto;
                    be.sCodConcepto = "9999";
                    be.sConcepto = "NETO";
                    //be.sNombreConcepto = dr.GetString(dr.GetOrdinal("vNombreConcepto"));
                    be.dcMonto = dcMontoIng - dcMontoDscto;
                    be.sCodigoAIRHSP = peticion.Where(x => x.sCodTipoConcepto == "1" && x.sNumeroDocumento == item.sNumeroDocumento && (x.sCodPlanilla == "1" || x.sCodPlanilla == "2")).FirstOrDefault().sCodigoAIRHSP;
                    be.sCodTipRegSISPER = peticion.Where(x => x.sCodTipoConcepto == "1" && x.sNumeroDocumento == item.sNumeroDocumento && (x.sCodPlanilla == "1" || x.sCodPlanilla == "2")).FirstOrDefault().sCodTipRegSISPER;
                    lstArchivo.Add(be);
                }*/
                if (!bJudicial)
                {
                    foreach (var item in peticion.Where(x => x.sCodPlanilla == "1" || x.sCodPlanilla == "2" || x.sCodPlanilla == "3" || x.sCodPlanilla == "4").ToList().OrderBy(x => x.sNumeroDocumento).ThenBy(y => y.sCodTipoConcepto))
                    {
                        if (item.sCodPlanilla == "1" || item.sCodPlanilla == "2")
                        {
                            sw.WriteLine(item.sCodTipoDocumento.Trim() + "|" + item.sNumeroDocumento.Trim() + "|" + item.sCodTipFuenteFinanciamiento + "|" + item.sCodTipoConcepto + "|" + item.sCodMCPP + "|" + item.sConcepto + "|" + item.dcMonto + "|" + item.sCodTipRegSISPER + "|" + item.sCodigoAIRHSP);
                        }
                        else
                        {
                            sw.WriteLine(item.sCodTipoDocumento.Trim() + "|" + item.sNumeroDocumento.Trim() + "|" + item.sCodTipFuenteFinanciamiento + "|" + item.sCodTipoConcepto + "|" + item.sCodMCPP + "|" + item.sConcepto + "|" + item.dcMonto + "|" + item.sCodTipRegSISPER);
                        }

                    } 
                }
                else
                {
                    foreach (var item in peticion.Where(x => x.sCodPlanilla == "1" || x.sCodPlanilla == "2" || x.sCodPlanilla == "3" || x.sCodPlanilla == "4").ToList().OrderBy(x => x.sNumeroDocumento).ThenBy(y => y.sCodTipoConcepto))
                    {
                        if (item.sCodPlanilla == "1" || item.sCodPlanilla == "2")
                        {
                            sw.WriteLine(item.sCodTipoDocumento.Trim() + "|" + item.sNumeroDocumento.Trim() + "|" + item.sCodTipFuenteFinanciamiento + "|1|0210|ASIG.JUDICIAL|" + item.dcMonto + "|" + item.sCodTipRegSISPER + "|" + item.sCodigoAIRHSP);
                        }
                        else
                        {
                            sw.WriteLine(item.sCodTipoDocumento.Trim() + "|" + item.sNumeroDocumento.Trim() + "|" + item.sCodTipFuenteFinanciamiento + "|1|0210|ASIG.JUDICIAL|" + item.dcMonto + "|" + item.sCodTipRegSISPER);
                        }

                    } 
                }
                
                sw.Close();
                sw.Dispose();
                rpta = true;
            }

            return rpta;
        }

        public bool GenerarArchivoTXTServir(IEnumerable<ArchivosTXT_Response> peticion, string iMes, string iAnio, string sRUC)
        {

            bool rpta = false;
            //string[] arrayFile = file.Split(',');            
            string strCodigoTXT2 = "01";
            string strCodigoTXTTipoPlanilla = "01";
            String NombreArchivo = "PLL001424" + iAnio + Convert.ToInt32(iMes).ToString("D2") + strCodigoTXT2 + strCodigoTXTTipoPlanilla + Convert.ToInt32(1).ToString("D4");
            String ExtensionArchivo = "TXT";

            String Archivo = NombreArchivo + "." + ExtensionArchivo;
            String ruta = Server.MapPath("~/temp/Planillas");
            ArchivosTXT_Response be = null;
            List<ArchivosTXT_Response> lstArchivo = new List<ArchivosTXT_Response>();
            using (StreamWriter sw = System.IO.File.CreateText(ruta + "/" + Archivo))
            {
                lstArchivo = peticion.ToList();
                //string codExternoEPS = peticion.Where(x => x.sConcepto == "EPS").FirstOrDefault().sCodigoExterno.ToString();
                string strCodigoTXT1 = "001424";
                string strCantReg = peticion.Where(x => x.sCodPlanilla == "5").ToList().Count().ToString();
                string strMontoTotalIng = peticion.Where(x => x.sCodTipoConcepto == "1" && (x.sCodPlanilla == "5")).ToList().Sum(x => x.dcMonto).ToString();
                string strMontoTotalDsctos = peticion.Where(x => x.sCodTipoConcepto == "2" && (x.sCodPlanilla == "5")).ToList().Sum(x => x.dcMonto).ToString();
                string strMontoTotalAportes = peticion.Where(x => x.sCodTipoConcepto == "3" && (x.sCodPlanilla == "5")).ToList().Sum(x => x.dcMonto).ToString();

                sw.WriteLine(strCodigoTXT1 + "|" + iAnio + "|" + Convert.ToInt32(iMes).ToString("D2") + "|" + strCodigoTXT2 + "|" + strCodigoTXTTipoPlanilla + "|" + Convert.ToInt32(1).ToString("D4") + "|" + strCantReg + "|" + strMontoTotalIng + "|" + strMontoTotalDsctos + "|" + strMontoTotalAportes + "|" + "0.00" + "|" + "0.00");

                /*var dnis = peticion.Where(x => x.sCodPlanilla == "5").ToList()
                    .GroupBy(x => new { x.sNumeroDocumento }).Select(y =>
                        new
                        {
                            sNumeroDocumento = y.Key.sNumeroDocumento
                        });
                foreach (var item in dnis)
                {
                    decimal dcMontoIng = peticion.Where(x => x.sCodTipoConcepto == "1" && x.sNumeroDocumento == item.sNumeroDocumento && (x.sCodPlanilla == "5")).ToList().Sum(x => x.dcMonto);
                    decimal dcMontoDscto = peticion.Where(x => x.sCodTipoConcepto == "2" && x.sNumeroDocumento == item.sNumeroDocumento && (x.sCodPlanilla == "5")).ToList().Sum(x => x.dcMonto);

                    be = new ArchivosTXT_Response();

                    be.sCodPlanilla = peticion.Where(x => x.sCodTipoConcepto == "1" && x.sNumeroDocumento == item.sNumeroDocumento && (x.sCodPlanilla == "5")).FirstOrDefault().sCodPlanilla;
                    be.sCodTipoDocumento = peticion.Where(x => x.sCodTipoConcepto == "1" && x.sNumeroDocumento == item.sNumeroDocumento && (x.sCodPlanilla == "5")).FirstOrDefault().sCodTipoDocumento;
                    be.sNumeroDocumento = peticion.Where(x => x.sCodTipoConcepto == "1" && x.sNumeroDocumento == item.sNumeroDocumento && (x.sCodPlanilla == "5")).FirstOrDefault().sNumeroDocumento;
                    be.sCodTipFuenteFinanciamiento = peticion.Where(x => x.sCodTipoConcepto == "1" && x.sNumeroDocumento == item.sNumeroDocumento && (x.sCodPlanilla == "5")).FirstOrDefault().sCodTipFuenteFinanciamiento;
                    be.sFuenteFinanciamiento = peticion.Where(x => x.sCodTipoConcepto == "1" && x.sNumeroDocumento == item.sNumeroDocumento && (x.sCodPlanilla == "5")).FirstOrDefault().sFuenteFinanciamiento;
                    be.sCodTipoConcepto = "9";
                    //be.sTipoConcepto = peticion.Where(x => x.sCodTipoConcepto == "1" && x.sNumeroDocumento == item.sNumeroDocumento && (x.sCodPlanilla == "1" || x.sCodPlanilla == "2")).FirstOrDefault().sTipoConcepto;
                    be.sCodConcepto = "9999";
                    be.sConcepto = "NETO";
                    //be.sNombreConcepto = dr.GetString(dr.GetOrdinal("vNombreConcepto"));
                    be.dcMonto = dcMontoIng - dcMontoDscto;
                    ;
                    be.sCodigoAIRHSP = peticion.Where(x => x.sCodTipoConcepto == "1" && x.sNumeroDocumento == item.sNumeroDocumento && (x.sCodPlanilla == "5")).FirstOrDefault().sCodigoAIRHSP;
                    be.sCodTipRegSISPER = peticion.Where(x => x.sCodTipoConcepto == "1" && x.sNumeroDocumento == item.sNumeroDocumento && (x.sCodPlanilla == "5")).FirstOrDefault().sCodTipRegSISPER;
                    lstArchivo.Add(be);
                }*/
                foreach (var item in lstArchivo.Where(x => x.sCodPlanilla == "5").ToList().OrderBy(x => x.sNumeroDocumento).ThenBy(y => y.sCodTipoConcepto))
                {
                    sw.WriteLine(item.sCodTipoDocumento.Trim() + "|" + item.sNumeroDocumento.Trim() + "|" + item.sCodTipFuenteFinanciamiento + "|" + item.sCodTipoConcepto + "|" + item.sCodMCPP + "|" + item.sConcepto + "|" + item.dcMonto + "|" + item.sCodTipRegSISPER + "|" + item.sCodigoAIRHSP);
                }
                sw.Close();
                sw.Dispose();
                rpta = true;
            }

            return rpta;
        }

        [HttpGet]
        public FileResult DescargarPlanillaZIP(string iMes, string iAnio, string sNombre)
        {
            String ExtensionArchivo = "zip";
            String NombreArchivo = "Planilla_" + sNombre + iAnio + Convert.ToInt32(iMes).ToString("D2");
            String NombreArchivoCompleto = NombreArchivo + "." + ExtensionArchivo;            
            String ruta = Server.MapPath("~/temp/Planillas");

            //return ruta(stream, "text/plain", NombreArchivo + "." + ExtensionArchivo);
            //var directoryPath = (string)TempData.Peek("FileDirectory");
            String zipFileName = Path.Combine(ruta, NombreArchivoCompleto);
            byte[] finalResult = System.IO.File.ReadAllBytes(zipFileName);

            return File(finalResult, "application/zip", NombreArchivoCompleto);  
        }
        #endregion Adminitrar Planilla
        /*-------------Adminitrar Planilla---------------------*/

        /*-------------Suspencion Retencion de 4ta Categoria ---------------------*/
        #region Suspencion Retencion de 4ta Categoria
        [HttpPost]
        public JsonResult ListarTrabajadoresSuspRet4Ta(SuspensionRetencionCuartaCat_Registro peticion)
        {
            object respuesta = _planillas_Servicio.ListarTrabajadoresSuspRet4Ta(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertarTrabajadoresSuspRet4Ta(SuspensionRetencionCuartaCat_Registro peticion)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _planillas_Servicio.InsertarTrabajadoresSuspRet4Ta(peticion);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult QuitarTrabajadoresSuspRet4Ta(SuspensionRetencionCuartaCat_Registro peticion)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _planillas_Servicio.EliminarTrabajadoresSuspRet4Ta(peticion);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        //[HttpPost]
        //public JsonResult ActualizarDetRequisitosAdicionales(RequisitosAdicionales_Registro peticion)
        //{
        //    try
        //    {
        //        //registro.FechaRegistro = DateTime.Now;
        //        //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

        //        object respuesta = _perfil_Puesto_Servicio.ActualizarDetRequisitosAdicionales(peticion);

        //        return Json(new { success = "True", responseText = respuesta });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = "False", responseText = ex.Message });
        //    }
        //}

        [HttpPost]
        public JsonResult EliminarTrabajadoresSuspRet4Ta(SuspensionRetencionCuartaCat_Registro peticion)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _planillas_Servicio.EliminarTrabajadoresSuspRet4Ta(peticion);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        #endregion
        /*-------------Suspencion Retencion de 4ta Categoria ---------------------*/

        /*-------------PagosTrabajador ---------------------*/
        #region PagosTrabajador
        [HttpPost]
        public JsonResult ListarConceptosPagosTrabajador(int iCodTrabajador, int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            object respuesta = _planillas_Servicio.ListarConceptosPagosTrabajador(iCodTrabajador, iMes, iAnio, iCodPlanilla, iCodTipoPlanilla, iCodDetPlanilla);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertarConceptoPagosTrabajador(PlanillaCalculada_Request peticion)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _planillas_Servicio.InsertarConceptoPagosTrabajador(peticion);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult ActualizarConceptoPagosTrabajador(PlanillaCalculada_Request peticion)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _planillas_Servicio.ActualizarConceptoPagosTrabajador(peticion);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        //[HttpPost]
        //public JsonResult ActualizarDetRequisitosAdicionales(RequisitosAdicionales_Registro peticion)
        //{
        //    try
        //    {
        //        //registro.FechaRegistro = DateTime.Now;
        //        //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

        //        object respuesta = _perfil_Puesto_Servicio.ActualizarDetRequisitosAdicionales(peticion);

        //        return Json(new { success = "True", responseText = respuesta });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = "False", responseText = ex.Message });
        //    }
        //}

        [HttpPost]
        public JsonResult EliminarConceptoPagosTrabajador(PlanillaCalculada_Request peticion)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _planillas_Servicio.EliminarConceptoPagosTrabajador(peticion);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        #endregion
        /*-------------PagosTrabajador ---------------------*/
        /*-------------ProyecionAnualRta5ta ---------------------*/
        #region ProyecionAnualRta5ta
        [HttpPost]
        public JsonResult ListarProyeccionAnualRta5ta_RegistroTrabajador(int iCodTrabajador, int iAnio)
        {
            object respuesta = _planillas_Servicio.ListarProyeccionAnualRta5ta_RegistroTrabajador(iCodTrabajador, iAnio);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ActualizarProyeccionAnualRta5taTrabajador(PlanillaProyeccionAnualRta5ta_Registro peticion)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _planillas_Servicio.ActualizarProyeccionAnualRta5taTrabajador(peticion);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        #endregion
        /*-------------ProyecionAnualRta5ta ---------------------*/
    
    
        /*REPORTES*/
        #region Reportes
        [Authorize]
        [Route("~/Views/Planilla/Reportes/ResumenGeneral/")]
        public ActionResult ResumenGeneral()
        {
            return View();
        }
        #endregion Reportes
        /*REPORTES*/
    
    }
}