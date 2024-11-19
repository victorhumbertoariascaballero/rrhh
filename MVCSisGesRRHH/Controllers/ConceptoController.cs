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
    public class ConceptoController : Controller
    {
        //
        private readonly T_genm_concepto_LN _concepto_Servicio = new T_genm_concepto_LN();

        // GET: /Concepto/
        public ActionResult Index()
        {
            return View();
        }

        //public JsonResult ListarConcepto(Concepto_Request peticion)
        //{
        //    List<Concepto_Registro> lista = new List<Concepto_Registro>();
        //    //peticion.RegistroEstaEliminado = null;

        //    object respuesta = _concepto_Servicio.ListarConcepto(peticion);

        //    return Json(respuesta, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult ListarConceptos(Concepto_Request peticion)
        {

            object respuesta = _concepto_Servicio.ListarConcepto(peticion); 
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarTipoConcepto(TipoConcepto_Request peticion)
        {
            object respuesta = _concepto_Servicio.ListarTipoConcepto(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }        

        public JsonResult ListarSubTipoConcepto(SubTipoConcepto_Request peticion)
        {

            object respuesta = _concepto_Servicio.ListarSubTipoConcepto(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarSubTipoConceptoNoBaseImponible(SubTipoConcepto_Request peticion)
        {

            object respuesta = _concepto_Servicio.ListarSubTipoConceptoNoBaseImponible(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarConceptoPorTipo(Concepto_Request peticion)
        {

            object respuesta = _concepto_Servicio.ListarConceptoPorTipo(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarConceptoPorTipoNoBaseImponible(Concepto_Request peticion)
        {

            object respuesta = _concepto_Servicio.ListarConceptoPorTipoNoBaseImponible(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [Authorize]
        public JsonResult ObtenerConceptoParaEditar(Concepto_Request peticion)
        {
            object respuesta = _concepto_Servicio.ObtenerConceptoParaEditar(peticion);

            return Json(respuesta);
        }


        [HttpPost]
        [Authorize]
        public JsonResult Registrar(Concepto_Registro registro)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.IdUsuarioRegistro = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iUsu);

                object respuesta = _concepto_Servicio.RegistrarConcepto(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public JsonResult Guardar(Concepto_Registro registro)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.IdUsuarioModificacion = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iUsu);

                object respuesta = _concepto_Servicio.ActualizarConcepto(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (System.Data.SqlClient.SqlException es)
            {
                if (es.Number == 2627)
                    return Json(new { success = "False", responseText = "Concepto ya existe" });
                else
                    return Json(new { success = "False", responseText = es.Message });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Eliminar(Concepto_Registro registro)
        {
            try
            {
                //registro.FechaModificacion = DateTime.Now;
                //registro.IdUsuarioModificacion = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iUsu);

                object respuesta = _concepto_Servicio.EliminarConcepto(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

       
	}
}