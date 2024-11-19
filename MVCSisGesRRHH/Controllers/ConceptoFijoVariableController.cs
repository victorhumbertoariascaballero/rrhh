using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using MIDIS.ORI.Entidades;
using MIDIS.SEG.LogicaNegocio;
using MIDIS.Utiles;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Net.Http.Headers;
using System.Text;

namespace MVCSisGesRRHH.Controllers
{
    public class ConceptoFijoVariableController : Controller
    {
        //
        private readonly T_genm_conceptofijovariable_LN _conceptofijovariable_Servicio = new T_genm_conceptofijovariable_LN();

        // GET: /ConceptoFijoVariable/
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult ListarConceptoVariable(ConceptoFijoVariable_Request peticion)
        {

            object respuesta = _conceptofijovariable_Servicio.ListarConceptoVariable(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarConceptoVariableNoBaseImponible(ConceptoFijoVariable_Request peticion)
        {

            object respuesta = _conceptofijovariable_Servicio.ListarConceptoVariableNoBaseImponible(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarPlanillaEjecucionAperturada(PlanillaEjecucion_Request peticion)
        {
            object respuesta = _conceptofijovariable_Servicio.ListarPlanillaEjecucionAperturada(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        [Authorize]
        public JsonResult ObtenerConceptoTrabajadorParaEditar(ConceptoFijoVariable_Request peticion)
        {
            object respuesta = _conceptofijovariable_Servicio.ObtenerConceptoTrabajadorParaEditar(peticion);

            return Json(respuesta);
        }

        [HttpPost]
        [Authorize]
        public JsonResult Registrar(ConceptoFijoVariable_Registro registro)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.IdUsuarioRegistro = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iUsu);

                string respuesta = _conceptofijovariable_Servicio.RegistrarConceptoTrabajador(registro);
                if (respuesta!=string.Empty)
                {
                    //DescargarExportableTXT(respuesta);
                    return Json(respuesta);
                }
                else
                {
                    return Json(new { success = "True", responseText = respuesta });
                }
                
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
            //object respuesta = _conceptofijovariable_Servicio.RegistrarConceptoTrabajador(registro);
            //return Json(respuesta);
        }

        [HttpGet]
        public FileResult DescargarExportableTXT(string file)
        {
            
            string[] arrayFile = file.Split(',');            
            StringWriter sw = new StringWriter();
            using (sw)
            {                
                for (int i = 0; i < arrayFile.Length; i++)
                {
                    sw.WriteLine(arrayFile[i].ToString().Trim());                    
                }
            }
            String contenido = sw.ToString();
            String NombreArchivo = "ListadoDNIsIncorrectos_" + DateTime.Now.ToShortDateString();
            String ExtensionArchivo = "txt";
            var stream = new MemoryStream(Encoding.ASCII.GetBytes(contenido));
            //return File(new System.Text.UTF8Encoding().GetBytes(contenido), "text/" + ExtensionArchivo, NombreArchivo + "." + ExtensionArchivo);
            return File(stream, "text/plain", NombreArchivo + "." + ExtensionArchivo);
            
        }

        [HttpPost]
        [Authorize]
        public JsonResult Guardar(ConceptoFijoVariable_Registro registro)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.IdUsuarioModificacion = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iUsu);

                object respuesta = _conceptofijovariable_Servicio.ActualizarConceptoTrabajador(registro);

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
        [Authorize]
        public JsonResult Eliminar(ConceptoFijoVariable_Registro registro)
        {
            try
            {
                object respuesta = _conceptofijovariable_Servicio.EliminarConceptoTrabajador(registro);
                return Json(new { success = "True", responseText = respuesta });
            }            
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
	}
}