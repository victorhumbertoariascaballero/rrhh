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

namespace MVCSisGesRRHH.Controllers
{
    public class TurnosController : Controller
    {
        private readonly T_genm_Turnos_LN _Servicio = new T_genm_Turnos_LN();
        private readonly T_genm_controlasistencia_LN _controlAsistencia_Servicio = new T_genm_controlasistencia_LN();

        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            ControlAsistenciaModel oModel = new ControlAsistenciaModel();
            //var oUsuario = VariablesWeb.ConsultaInformacion.Persona;
            oModel.Empleado = _controlAsistencia_Servicio.ObtenerParaEditar(new Empleado_Request() { IdEmpleado = Convert.ToInt32(VariablesWeb.ConsultaInformacion.Persona.iCodTrabajador) });
            return View(oModel);
        }

        [HttpPost]
        [Authorize]
        public JsonResult Inicializar()
        {
            object respuesta = new
            {
                permisos = "acceder,buscar,insertar,actualizar,mostrar,eliminar,activar"
            };

            return Json(respuesta);
        }

        [HttpGet]
        [Authorize]
        public JsonResult ListarEstados()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("-1", "--Todos--"));
            lista.Add(new Estado_Response("0", "INACTIVO"));
            lista.Add(new Estado_Response("1", "ACTIVO"));

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public JsonResult ListarTurnos()
        {
            IEnumerable<Turno_Registro> lista = _Servicio.ListarTurnos();

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public JsonResult ListarTurnosTrabajador(TurnoTrabajador_Request request)
        {
            request.bVigente = true;
            request.bEstado = true;
            IEnumerable<TurnoTrabajador_Registro> lista = _Servicio.ListarTurnosTrabajador(request);

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public JsonResult GrabarTurnoTrabajador(TurnoTrabajador_Registro request)
        {
            //var Empleado = _controlAsistencia_Servicio.ObtenerParaEditar(new Empleado_Request() { IdEmpleado = Convert.ToInt32(VariablesWeb.ConsultaInformacion.Persona.iCodTrabajador) });

            //request.iCodTrabajador = 38041; //Convert.ToInt32(VariablesWeb.ConsultaInformacion.Persona.iCodTrabajador);
            //request.iCodigoDependencia = ;
            request.vAuditCreacion = VariablesWeb.ConsultaInformacion.iCodUsuario.ToString();
            request.bVigente = true;
            request.bEstado = true;
            var response = _Servicio.Insertar(request);


            return Json(new { success = (response.iCodTurnoTrabajador > 0) ? "True" : "False" });
            //if (String.IsNullOrEmpty(respuesta))
            //    return Json(new { success = "True" });
            //else
            //    return Json(new { success = "False", responseText = respuesta });
        }
    }
}