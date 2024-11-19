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
    public class MarcacionesController : Controller
    {
        private readonly T_genm_Marcaciones_LN _marcaciones_Servicio = new T_genm_Marcaciones_LN();

        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult IndexAnterior()
        {
            return View();
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
        public JsonResult ListarTipoMarcaciones()
        {
            IEnumerable<TipoMarcaciones_Registro> lista = _marcaciones_Servicio.ListarTipoMarcaciones();

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public JsonResult ListarMarcaciones(Marcaciones_Request request)
        {
            request.dtFechaMarcacionIni = request.dtFechaMarcacionIni.Date;
            request.dtFechaMarcacionFin = request.dtFechaMarcacionFin.Date;
            request.dtFechaMarcacionFin = request.dtFechaMarcacionFin.AddDays(1).AddTicks(-1);
            IEnumerable<Marcaciones_Registro> lista = _marcaciones_Servicio.ListarMarcaciones(request);

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public JsonResult GrabarMarcacion(Marcaciones_Registro request)
        {
            request.iCodTrabajador = 38041; //Convert.ToInt32(VariablesWeb.ConsultaInformacion.Persona.iCodTrabajador);
            request.iCodigoDependencia = 0;
            request.vAuditCreacion = VariablesWeb.ConsultaInformacion.iCodUsuario.ToString();
            request.vIpCliente = HttpContext.Request.UserHostAddress;
            request.bEstado = true;
            var response = _marcaciones_Servicio.Insertar(request);


            return Json(new { success = (response.iCodMarcaciones > 0) ? "True" : "False" });
            //if (String.IsNullOrEmpty(respuesta))
            //    return Json(new { success = "True" });
            //else
            //    return Json(new { success = "False", responseText = respuesta });
        }
    }
}