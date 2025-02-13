using MIDIS.ORI.Entidades;
using MIDIS.ORI.LogicaNegocio;
using MIDIS.Utiles;
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
using CrystalDecisions.CrystalReports.Engine;
using SelectPdf;

namespace MVCSisGesRRHH.Controllers
{
    [Authorize]
	public class LegajoController: Controller
	{
        private readonly T_genm_empleado_LN _empleado_Servicio = new T_genm_empleado_LN();
        private readonly T_genm_legajo_LN _legajo_Servicio = new T_genm_legajo_LN();
        
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
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
            lista.Add(new Estado_Response("0", "--Todos--"));
            lista.Add(new Estado_Response("1", "REGISTRO"));
            lista.Add(new Estado_Response("2", "PENDIENTE DE FIRMA"));
            lista.Add(new Estado_Response("3", "CON CONTRATO FIRMADO"));

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarTipoDeDocumento()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("1", "DNI"));
            lista.Add(new Estado_Response("3", "CARN� DE EXTRANJER�A"));

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarLegajos(Postulante_Request peticion)
        {
            object respuesta = _legajo_Servicio.ListarLegajos(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public JsonResult ObtenerParaEditar(Legajo_Registro peticion)
        {
            object respuesta = _legajo_Servicio.ObtenerParaEditar(peticion);

            return Json(respuesta);
        }
    }
}