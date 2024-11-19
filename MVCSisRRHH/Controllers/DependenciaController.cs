using MIDIS.ORI.Entidades;
using MIDIS.ORI.LogicaNegocio;
using MIDIS.Utiles;
using MIDIS.UtilesMVC;
using MIDIS.UtilesWeb;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
//using System.Web.Mail;
using System.Web.Mvc;


namespace MVCSisRRHH.Controllers
{
    [Authorize]
	public class DependenciaController: Controller
	{
        private readonly T_genm_dependencia_LN _dependencia_Servicio = new T_genm_dependencia_LN();
        
        [HttpGet]
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
        public JsonResult ListarDependencias(string term)
        {
            Dependencia_Request peticion = new Dependencia_Request();
            peticion.IdDependencia = 0;
            peticion.Nombre = "%" + (String.IsNullOrEmpty(term) ? "" : term.ToUpper()) + "%";
            object respuesta = _dependencia_Servicio.ListarDependencias(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarAnios()
        {
            Anio_Response item = null;
            List<Anio_Response> lista = new List<Anio_Response>();
            item = new Anio_Response();
            item.Anio = DateTime.Now.Year.ToString();
            lista.Add(item);
            item = new Anio_Response();
            item.Anio = (DateTime.Now.Year - 1).ToString();
            lista.Add(item);
            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarMeses()
        {
            List<Mes_Response> lista = new List<Mes_Response>();
            lista.Add(new Mes_Response("01", "ENERO"));
            lista.Add(new Mes_Response("02", "FEBRERO"));
            lista.Add(new Mes_Response("03", "MARZO"));
            lista.Add(new Mes_Response("04", "ABRIL"));
            lista.Add(new Mes_Response("05", "MAYO"));
            lista.Add(new Mes_Response("06", "JUNIO"));
            lista.Add(new Mes_Response("07", "JULIO"));
            lista.Add(new Mes_Response("08", "AGOSTO"));
            lista.Add(new Mes_Response("09", "SETIEMBRE"));
            lista.Add(new Mes_Response("10", "OCTUBRE"));
            lista.Add(new Mes_Response("11", "NOVIEMBRE"));
            lista.Add(new Mes_Response("12", "DICIEMBRE"));
            
            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        
	}
}