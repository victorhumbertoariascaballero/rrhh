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
	public class CondicionController: Controller
	{
        private readonly T_genm_condicion_LN _condicion_Servicio = new T_genm_condicion_LN();
        
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
        public JsonResult ListarCondicion(String perfil)
        {
            String PERFIL_NOMINA_ABASTECIMIENTO = ConfigurationManager.AppSettings["IdPerfilNominaAbastecimiento"];

            List<Condicion_Registro> lista = new List<Condicion_Registro>();
            if (perfil == PERFIL_NOMINA_ABASTECIMIENTO)
            {
                lista.Add(new Condicion_Registro() { IdCondicion = 5, Nombre = "ORDEN DE SERVICIO" });
                return Json(lista, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Condicion_Request peticion = new Condicion_Request() { IdCondicion = 0, Nombre = "" };
                var resultado = _condicion_Servicio.ListarCondicion(peticion);
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
        }
        
	}
}