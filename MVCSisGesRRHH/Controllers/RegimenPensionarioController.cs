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
    public class RegimenPensionarioController : Controller
    {
        //
        private readonly T_genm_regimenpensionario_LN _regimenpensionario_Servicio = new T_genm_regimenpensionario_LN();

        // GET: /RegimenPensionario/
        public ActionResult Index()
        {
            return View();
        }

        /* Agregado por : MASJ */
        /* BUSQUEDA */
        [HttpGet]
        [Authorize]
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
        [Authorize]
        public JsonResult ListarMeses()
        {
            List<Mes_Response> lista = new List<Mes_Response>();
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


        public JsonResult ListarRegimenPensionario(RegimenPensionario_Request peticion)
        {

            object respuesta = _regimenpensionario_Servicio.ListarRegimenPensionario(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarAfps(RegimenAfp_Request peticion)
        {

            object respuesta = _regimenpensionario_Servicio.ListarRegimenAfp(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarTipoRegimenPensionario(TipoRegimenPensionario_Request peticion)
        {
            object respuesta = _regimenpensionario_Servicio.ListarTipoRegimenPensionario(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
       
        /**/
        public JsonResult ListarRegistroRegimenPensionario(string iMes, string iAnio)
        {
            List<RegistroRegimenPensionario_Registro> lista = new List<RegistroRegimenPensionario_Registro>();

            //peticion.RegistroEstaEliminado = null;
            //peticion.TieneDocumento = null;

            object respuesta = _regimenpensionario_Servicio.ListarRegistroRegimenPensionario(Convert.ToInt32(iMes), Convert.ToInt32(iAnio));

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public JsonResult ObtenerRegimenParaEditar(RegistroRegimenPensionario_Request peticion)
        {
            object respuesta = _regimenpensionario_Servicio.ObtenerRegimenParaEditar(peticion);

            return Json(respuesta);
        }

        [HttpPost]
        [Authorize]
        public JsonResult Registrar(RegistroRegimenPensionario_Registro registro)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.IdUsuarioRegistro = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iUsu);

                object respuesta = _regimenpensionario_Servicio.InsertarRegistroRegimenPensionario(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public JsonResult Guardar(RegistroRegimenPensionario_Registro registro)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.IdUsuarioModificacion = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iUsu);

                object respuesta = _regimenpensionario_Servicio.ActualizarRegistroRegimenPensionario(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (System.Data.SqlClient.SqlException es)
            {
                if (es.Number == 2627)
                    return Json(new { success = "False", responseText = "RegistroRegimenPensionario ya existe" });
                else
                    return Json(new { success = "False", responseText = es.Message });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CopiarRegimen(RegistroRegimenPensionario_Registro registro)
        {
            try
            {
                //registro.FechaModificacion = DateTime.Now;
                //registro.IdUsuarioModificacion = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iUsu);

                object respuesta = _regimenpensionario_Servicio.CopiarRegimenPensionario(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

	}
}