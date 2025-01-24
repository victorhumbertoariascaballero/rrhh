using MIDIS.ORI.Entidades;
using MIDIS.ORI.LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using MVCSisGesRRHH.Models;
using System.Text;
using System.IO.Compression;
using CrystalDecisions.CrystalReports.Engine;
using SelectPdf;
using MIDIS.SEG.LogicaNegocio;
using MIDIS.ORI.Entidades.Core;

namespace MVCSisGesRRHH.Controllers
{
    public class ReportesCAController : Controller
    {
        private readonly T_genm_controlasistencia_LN _controlAsistencia_Servicio = new T_genm_controlasistencia_LN();

        [HttpGet]
        [Authorize]
        public ActionResult horizontal()
        {
            ControlAsistenciaModel oModel = new ControlAsistenciaModel();
            //var oUsuario = VariablesWeb.ConsultaInformacion.Persona;
            oModel.Empleado = _controlAsistencia_Servicio.ObtenerParaEditar(new Empleado_Request() { IdEmpleado = Convert.ToInt32(VariablesWeb.ConsultaInformacion.Persona.iCodTrabajador) });
            return View("horizontal", oModel);
        }

        [HttpGet]
        [Authorize]
        public JsonResult GetHorizontal(ReporteHorizontal_Request request)
        {
            IEnumerable<ReporteHorizontal_Registro> lista = _controlAsistencia_Servicio.GetReporteHorizontal(request);
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public ActionResult planilla()
        {
            ControlAsistenciaModel oModel = new ControlAsistenciaModel();
            //var oUsuario = VariablesWeb.ConsultaInformacion.Persona;
            oModel.Empleado = _controlAsistencia_Servicio.ObtenerParaEditar(new Empleado_Request() { IdEmpleado = Convert.ToInt32(VariablesWeb.ConsultaInformacion.Persona.iCodTrabajador) });
            return View("planilla", oModel);
        }

        [HttpGet]
        [Authorize]
        public JsonResult GetPlanilla(ReportePlanilla_Request request)
        {
            List<ReportePlanilla_Registro> lista = _controlAsistencia_Servicio.GetReportePlanilla(request);
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public ActionResult sobretiempo()
        {
            ControlAsistenciaModel oModel = new ControlAsistenciaModel();
            //var oUsuario = VariablesWeb.ConsultaInformacion.Persona;
            oModel.Empleado = _controlAsistencia_Servicio.ObtenerParaEditar(new Empleado_Request() { IdEmpleado = Convert.ToInt32(VariablesWeb.ConsultaInformacion.Persona.iCodTrabajador) });
            return View("sobretiempo", oModel);
        }

        [HttpGet]
        [Authorize]
        public JsonResult GetSobretiempo(ReporteSobretiempo_Request request)
        {
            List<ReporteSobretiempo_Registro> lista = _controlAsistencia_Servicio.GetReporteSobretiempo(request);
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
    }
}