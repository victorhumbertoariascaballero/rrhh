using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using MIDIS.ORI.Entidades;
using MIDIS.SEG.LogicaNegocio;
using MIDIS.Utiles;

namespace MVCSisGesRRHH.Controllers
{
    public class DescuentoJudicialController : Controller
    {
        //
        //private readonly T_genm_banco_LN _banco_Servicio = new T_genm_banco_LN();
        private readonly T_genm_descuentojudicial_LN _descuentojudicial_Servicio = new T_genm_descuentojudicial_LN();
        private string nroDni;

        // GET: /DescuentoJudicial/
        public ActionResult Index()
        {
            Session["ListaBeneficiariosPlanillaJudicial"] = null;

            return View();
        }

        public JsonResult ListarBancos(Banco_Request peticion)
        {

            object respuesta = _descuentojudicial_Servicio.ListarBancos(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        [Authorize]
        public JsonResult ListarTipoRetencion()
        {
            List<TipoRetencion_Response> lista = new List<TipoRetencion_Response>();
            lista.Add(new TipoRetencion_Response("1", "IMPORTE FIJO"));
            lista.Add(new TipoRetencion_Response("2", "PORCENTAJE"));

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Authorize]
        public JsonResult ListarFormaPago()
        {
            List<FormaPago_Response> lista = new List<FormaPago_Response>();
            lista.Add(new FormaPago_Response("1", "TRANSFERENCIA BANCARIA"));
            lista.Add(new FormaPago_Response("2", "CHEQUE"));

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ListarDescuentoJudicial(DescuentoJudicial_Request peticion)
        {

            object respuesta = _descuentojudicial_Servicio.ListarDescuentoJudicial(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarDescuentoJudicialTrabajadores()
        {

            object respuesta = _descuentojudicial_Servicio.ListarDescuentoJudicialTrabajadores();
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public JsonResult ObtenerDescuentoJudicialParaEditar(DescuentoJudicial_Request peticion)
        {
            Session["ListaBeneficiariosPlanillaJudicial"] = null;

            object respuesta = _descuentojudicial_Servicio.ObtenerDescuentoJudicialParaEditar(peticion);

            return Json(respuesta);
        }

        [HttpPost]
        [Authorize]
        public JsonResult ObtenerDescuentoJudicialTrabajadorParaEditar(DescuentoJudicial_Request peticion)
        {
            Session["ListaBeneficiariosPlanillaJudicialTrabajadores"] = null;

            object respuesta = _descuentojudicial_Servicio.ObtenerDescuentoJudicialTrabajadorParaEditar(peticion);

            return Json(respuesta);
        }

        [HttpPost]
        [Authorize]
        public JsonResult Registrar(DescuentoJudicial_Registro registro)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.IdUsuarioRegistro = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iUsu);

                object respuesta = _descuentojudicial_Servicio.RegistrarDescuentoJudicialTrabajador(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public JsonResult Registrar_Nuevo(DescuentoJudicial_Registro registro)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.IdUsuarioRegistro = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iUsu);

                object respuesta = _descuentojudicial_Servicio.RegistrarDescuentoJudicialTrabajador_Nuevo(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public JsonResult Guardar(DescuentoJudicial_Registro registro)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.IdUsuarioModificacion = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iUsu);

                object respuesta = _descuentojudicial_Servicio.ActualizarDescuentoJudicialTrabajador(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (System.Data.SqlClient.SqlException es)
            {
                if (es.Number == 2627)
                    return Json(new { success = "False", responseText = "Descuento Judicial ya existe" });
                else
                    return Json(new { success = "False", responseText = es.Message });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public JsonResult Guardar_Nuevo(DescuentoJudicial_Registro registro)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.IdUsuarioModificacion = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iUsu);

                object respuesta = _descuentojudicial_Servicio.ActualizarDescuentoJudicialTrabajador_Nuevo(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (System.Data.SqlClient.SqlException es)
            {
                if (es.Number == 2627)
                    return Json(new { success = "False", responseText = "Descuento Judicial ya existe" });
                else
                    return Json(new { success = "False", responseText = es.Message });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Eliminar(DescuentoJudicial_Registro registro)
        {
            try
            {
                //registro.FechaModificacion = DateTime.Now;
                //registro.IdUsuarioModificacion = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iUsu);

                object respuesta = _descuentojudicial_Servicio.EliminarDescuentoJudicialTrabajador(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult EliminarBeneficiario(DescuentoJudicial_Registro registro)
        {
            try
            {
                //registro.FechaModificacion = DateTime.Now;
                //registro.IdUsuarioModificacion = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iUsu);

                object respuesta = _descuentojudicial_Servicio.EliminarDescuentoJudicialBeneficiario(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CargarRetencionJudicial(DescuentoJudicial_Registro registro)
        {
            try
            {
                //registro.FechaModificacion = DateTime.Now;
                //registro.IdUsuarioModificacion = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iUsu);

                object respuesta = _descuentojudicial_Servicio.CargarDescuentoJudicial(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        #region "Descuento Judicial Detalle - Beneficiarios"
        public JsonResult ListarBeneficiarios(DescuentoJudicialBeneficiario_Request peticion)
        {
            object respuesta;
            List<DescuentoJudicialBeneficiario_Registro> lstBeneficiario_Registro = new List<DescuentoJudicialBeneficiario_Registro>();
            if (Session["ListaBeneficiariosPlanillaJudicial"] != null)
            {

                lstBeneficiario_Registro = (List<DescuentoJudicialBeneficiario_Registro>)Session["ListaBeneficiariosPlanillaJudicial"];
                respuesta = lstBeneficiario_Registro;
            }
            else {

                respuesta = _descuentojudicial_Servicio.ListarDescuentoJudicial_Beneficiario(peticion);
                Session["ListaBeneficiariosPlanillaJudicial"] = respuesta;
            }
            
            
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarBeneficiariosPlanilla(DescuentoJudicialBeneficiario_Request peticion)
        {
            object respuesta = _descuentojudicial_Servicio.ListarDescuentoJudicial_Beneficiario(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarBeneficiariosTrabajadores(DescuentoJudicialBeneficiario_Request peticion)
        {
            object respuesta;
            List<DescuentoJudicialBeneficiario_Registro> lstBeneficiario_Registro = new List<DescuentoJudicialBeneficiario_Registro>();
            if (Session["ListaBeneficiariosPlanillaJudicialTrabajadores"] != null)
            {

                lstBeneficiario_Registro = (List<DescuentoJudicialBeneficiario_Registro>)Session["ListaBeneficiariosPlanillaJudicialTrabajadores"];
                respuesta = lstBeneficiario_Registro;
            }
            else
            {

                respuesta = _descuentojudicial_Servicio.ListarDescuentoJudicial_BeneficiarioTrabajadores(peticion);
                Session["ListaBeneficiariosPlanillaJudicialTrabajadores"] = respuesta;
            }


            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CargarBeneficiariosTemporal()
        {

            List<DescuentoJudicialBeneficiario_Registro> lstBeneficiario_Registro = new List<DescuentoJudicialBeneficiario_Registro>();
            if (Session["ListaBeneficiariosPlanillaJudicial"] != null)
            {
                lstBeneficiario_Registro = (List<DescuentoJudicialBeneficiario_Registro>)Session["ListaBeneficiariosPlanillaJudicial"];
            }

            object respuesta = lstBeneficiario_Registro;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AgregarBeneficiarioTemporal(string cDniBeneficiario, string cNomBeneficiario, string iCodBanco, string cNomBanco,
            string cNroCuenta, string iCodRetencion, string cNomRetencion, string dPorReten, string dMonReten, string iCodFormaPago, string cNomFormaPago)
        {
            DescuentoJudicialBeneficiario_Registro objBeneficiario_Registro = null;

            List<DescuentoJudicialBeneficiario_Registro> lstBeneficiario_Registro = new List<DescuentoJudicialBeneficiario_Registro>();
            if (Session["ListaBeneficiariosPlanillaJudicial"] == null)
            {
                objBeneficiario_Registro = new DescuentoJudicialBeneficiario_Registro
                {
                    vDniBeneficiario = cDniBeneficiario,
                    vNombreBeneficiario = cNomBeneficiario,
                    iCodigoBanco = Convert.ToInt32(iCodBanco),
                    vNombreBanco = cNomBanco,
                    vNumeroCuenta = cNroCuenta,
                    iCodTipoRetencion = Convert.ToInt32(iCodRetencion),
                    vNombreRetencion = cNomRetencion,
                    iCodFormaPago = Convert.ToInt32(iCodFormaPago),
                    vNombreFormaPago = cNomFormaPago,
                    dValorPorcentaje = dPorReten == "" ? 0 : Convert.ToDecimal(dPorReten),
                    dMontoRetencion = dMonReten == "" ? 0 : Convert.ToDecimal(dMonReten)

                };
                lstBeneficiario_Registro.Add(objBeneficiario_Registro);

                Session["ListaBeneficiariosPlanillaJudicial"] = lstBeneficiario_Registro;
            }
            else
            {
                lstBeneficiario_Registro = (List<DescuentoJudicialBeneficiario_Registro>)Session["ListaBeneficiariosPlanillaJudicial"];
                objBeneficiario_Registro = new DescuentoJudicialBeneficiario_Registro
                {
                   vDniBeneficiario = cDniBeneficiario,
                    vNombreBeneficiario = cNomBeneficiario,
                    iCodigoBanco = Convert.ToInt32(iCodBanco),
                    vNombreBanco = cNomBanco,
                    vNumeroCuenta = cNroCuenta,
                    iCodTipoRetencion = Convert.ToInt32(iCodRetencion),
                    vNombreRetencion = cNomRetencion,
                    iCodFormaPago = Convert.ToInt32(iCodFormaPago),
                    vNombreFormaPago = cNomFormaPago,
                    dValorPorcentaje = dPorReten=="" ? 0 : Convert.ToDecimal(dPorReten),
                    dMontoRetencion = dMonReten=="" ? 0 : Convert.ToDecimal(dMonReten)
                };

                lstBeneficiario_Registro.Add(objBeneficiario_Registro);
                Session["ListaBeneficiariosPlanillaJudicial"] = lstBeneficiario_Registro;
            }

            object respuesta = lstBeneficiario_Registro;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AgregarBeneficiarioTrabajadorTemporal(string cDniBeneficiario, string cNomBeneficiario, string iCodBanco, string cNomBanco,
            string cNroCuenta, string iCodRetencion, string cNomRetencion, string dPorReten, string dMonReten, string iCodFormaPago, string cNomFormaPago)
        {
            DescuentoJudicialBeneficiario_Registro objBeneficiario_Registro = null;

            List<DescuentoJudicialBeneficiario_Registro> lstBeneficiario_Registro = new List<DescuentoJudicialBeneficiario_Registro>();
            if (Session["ListaBeneficiariosPlanillaJudicialTrabajadores"] == null)
            {
                objBeneficiario_Registro = new DescuentoJudicialBeneficiario_Registro
                {
                    vDniBeneficiario = cDniBeneficiario,
                    vNombreBeneficiario = cNomBeneficiario,
                    iCodigoBanco = Convert.ToInt32(iCodBanco),
                    vNombreBanco = cNomBanco,
                    vNumeroCuenta = cNroCuenta,
                    iCodTipoRetencion = Convert.ToInt32(iCodRetencion),
                    vNombreRetencion = cNomRetencion,
                    iCodFormaPago = Convert.ToInt32(iCodFormaPago),
                    vNombreFormaPago = cNomFormaPago,
                    dValorPorcentaje = dPorReten == "" ? 0 : Convert.ToDecimal(dPorReten),
                    dMontoRetencion = dMonReten == "" ? 0 : Convert.ToDecimal(dMonReten)

                };
                lstBeneficiario_Registro.Add(objBeneficiario_Registro);

                Session["ListaBeneficiariosPlanillaJudicialTrabajadores"] = lstBeneficiario_Registro;
            }
            else
            {
                lstBeneficiario_Registro = (List<DescuentoJudicialBeneficiario_Registro>)Session["ListaBeneficiariosPlanillaJudicialTrabajadores"];
                objBeneficiario_Registro = new DescuentoJudicialBeneficiario_Registro
                {
                    vDniBeneficiario = cDniBeneficiario,
                    vNombreBeneficiario = cNomBeneficiario,
                    iCodigoBanco = Convert.ToInt32(iCodBanco),
                    vNombreBanco = cNomBanco,
                    vNumeroCuenta = cNroCuenta,
                    iCodTipoRetencion = Convert.ToInt32(iCodRetencion),
                    vNombreRetencion = cNomRetencion,
                    iCodFormaPago = Convert.ToInt32(iCodFormaPago),
                    vNombreFormaPago = cNomFormaPago,
                    dValorPorcentaje = dPorReten == "" ? 0 : Convert.ToDecimal(dPorReten),
                    dMontoRetencion = dMonReten == "" ? 0 : Convert.ToDecimal(dMonReten)
                };

                lstBeneficiario_Registro.Add(objBeneficiario_Registro);
                Session["ListaBeneficiariosPlanillaJudicialTrabajadores"] = lstBeneficiario_Registro;
            }

            object respuesta = lstBeneficiario_Registro;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult QuitarBeneficiarioTemporal(string nroDNI)
        {
            //Empleado_Registro objEmpleado_Registro = null;
            List<DescuentoJudicialBeneficiario_Registro> lstBeneficiario_Registro = new List<DescuentoJudicialBeneficiario_Registro>();
            if (Session["ListaBeneficiariosPlanillaJudicial"] != null)
            {
                lstBeneficiario_Registro = (List<DescuentoJudicialBeneficiario_Registro>)Session["ListaBeneficiariosPlanillaJudicial"];
                int obj = lstBeneficiario_Registro.FindIndex(x => x.vDniBeneficiario == nroDNI);
                lstBeneficiario_Registro.RemoveAt(obj);
                Session["ListaBeneficiariosPlanillaJudicial"] = lstBeneficiario_Registro;
            }
            object respuesta = lstBeneficiario_Registro;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult QuitarBeneficiarioTrabajadorTemporal(string nroDNI)
        {
            //Empleado_Registro objEmpleado_Registro = null;
            List<DescuentoJudicialBeneficiario_Registro> lstBeneficiario_Registro = new List<DescuentoJudicialBeneficiario_Registro>();
            if (Session["ListaBeneficiariosPlanillaJudicialTrabajador"] != null)
            {
                lstBeneficiario_Registro = (List<DescuentoJudicialBeneficiario_Registro>)Session["ListaBeneficiariosPlanillaJudicialTrabajador"];
                int obj = lstBeneficiario_Registro.FindIndex(x => x.vDniBeneficiario == nroDNI);
                lstBeneficiario_Registro.RemoveAt(obj);
                Session["ListaBeneficiariosPlanillaJudicialTrabajador"] = lstBeneficiario_Registro;
            }
            object respuesta = lstBeneficiario_Registro;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GenerarPlanillaJudicial(string iMes, string iAnio, string iCodPlanilla, string iCodTipoPlanilla)
        {
            try
            {

                object respuesta = _descuentojudicial_Servicio.GenerarPlanillaJudicial(Convert.ToInt32(iMes), Convert.ToInt32(iAnio), Convert.ToInt32(iCodPlanilla), Convert.ToInt32(iCodTipoPlanilla));

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public JsonResult Registrar_Beneficiario(DescuentoJudicialBeneficiario_Registro registro)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.IdUsuarioRegistro = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iUsu);

                object respuesta = _descuentojudicial_Servicio.Registrar_Beneficiario(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public JsonResult Quitar_Beneficiario(DescuentoJudicialBeneficiario_Registro registro)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.IdUsuarioRegistro = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iUsu);

                object respuesta = _descuentojudicial_Servicio.Quitar_Beneficiario(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        #endregion

    }
}