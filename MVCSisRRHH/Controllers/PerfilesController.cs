using MIDIS.ORI.Entidades;
using MIDIS.SEG.LogicaNegocio;
using MIDIS.Utiles;
//using MIDIS.UtilesMVC;
//using MIDIS.UtilesWeb;
using System;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSisRRHH.Models;
using System.IO;
using SelectPdf;

namespace MVCSisRRHH.Controllers
{
    public class PerfilesController : Controller
    {
        //private readonly T_genm_uuoo_LN _uuoo_Servicio = new T_genm_uuoo_LN();
        private readonly T_genm_perfil_puesto_LN _perfil_Puesto_Servicio = new T_genm_perfil_puesto_LN();
        //private readonly T_genm_cualidad_LN _cualidad_Servicio = new T_genm_cualidad_LN();

        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult Index_user()
        {
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult Index_jefe()
        {
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult Index_jefeRRHH()
        {
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult Nuevo()
        {
            if (TempData["id"]!=null)
            {
                ViewBag.idPerfil = TempData["id"];
            }
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult Ver()
        {
            if (TempData["id"] != null)
            {
                ViewBag.idPerfil = TempData["id"];
            }
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult Ver_Jefe()
        {
            if (TempData["id"] != null)
            {
                ViewBag.idPerfil = TempData["id"];
            }
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult Ver_JefeRRHH()
        {
            if (TempData["id"] != null)
            {
                ViewBag.idPerfil = TempData["id"];
            }
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult Ver_User()
        {
            if (TempData["id"] != null)
            {
                ViewBag.idPerfil = TempData["id"];
            }
            return View();
        }
        [HttpGet]
        public JsonResult ListarNivelMimino()
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _perfil_Puesto_Servicio.ListarNivelMimino();
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarNivelMateria()
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _perfil_Puesto_Servicio.ListarMaePerfilNivelMateria();
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarTipoMateriaOtros()
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _perfil_Puesto_Servicio.ListarMaePerfilTipoMateriaOtros();
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarNivelEducativo()
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _perfil_Puesto_Servicio.ListarNivelEducativo();
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarGradosBasico()
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _perfil_Puesto_Servicio.ListarGradosBasico();
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarGrados()
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _perfil_Puesto_Servicio.ListarGrados();
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarGradosTodos()
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _perfil_Puesto_Servicio.ListarGradosTodos();
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarCarreraNivel1(PerfilCarrera_Response peticion)
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _perfil_Puesto_Servicio.ListarCarreraNivel1(peticion.iCodTipoCarrera);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarCarreraNivel2(PerfilCarrera_Response peticion)
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _perfil_Puesto_Servicio.ListarCarreraNivel2(peticion.strCodCarrera);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarCarreraNivel3(PerfilCarrera_Response peticion)
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _perfil_Puesto_Servicio.ListarCarreraNivel3(peticion.strCodCarrera);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarCarreraNivel4(PerfilCarrera_Response peticion)
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _perfil_Puesto_Servicio.ListarCarreraNivel4(peticion.strCodCarrera, String.Empty);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ListarCarreraNivel1_Mae(PerfilCarrera_Response peticion)
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _perfil_Puesto_Servicio.ListarCarreraNivel1_Mae(peticion.iCodTipoCarrera);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarCarreraNivel2_Mae(PerfilCarrera_Response peticion)
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _perfil_Puesto_Servicio.ListarCarreraNivel2_Mae(peticion.strCodCarrera);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarCarreraNivel3_Mae(PerfilCarrera_Response peticion)
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _perfil_Puesto_Servicio.ListarCarreraNivel3_Mae(peticion.strCodCarrera);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarCarreraNivel4_Mae(PerfilCarrera_Response peticion)
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _perfil_Puesto_Servicio.ListarCarreraNivel4_Mae(peticion.strCodCarrera, String.Empty);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarCarreraNivel1_Doc(PerfilCarrera_Response peticion)
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _perfil_Puesto_Servicio.ListarCarreraNivel1_Doc(peticion.iCodTipoCarrera);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarCarreraNivel2_Doc(PerfilCarrera_Response peticion)
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _perfil_Puesto_Servicio.ListarCarreraNivel2_Doc(peticion.strCodCarrera);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarCarreraNivel3_Doc(PerfilCarrera_Response peticion)
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _perfil_Puesto_Servicio.ListarCarreraNivel3_Doc(peticion.strCodCarrera);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarCarreraNivel4_Doc(PerfilCarrera_Response peticion)
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _perfil_Puesto_Servicio.ListarCarreraNivel4_Doc(peticion.strCodCarrera, String.Empty);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }








        [HttpGet]
        public JsonResult ListarTipoSubMateriaOtros(PerfilTipoSubMateriaOtros_Response peticion)
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _perfil_Puesto_Servicio.ListarMaePerfilTipoSubMateriaOtros(peticion.iCodTipoMateriaOtros);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarTipoMateria()
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _perfil_Puesto_Servicio.ListarMaePerfilTipoMateria();
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        //[HttpGet]
        //public JsonResult ListarTipoCualidad()
        //{
        //    //Verbo_Registro verbos = new Verbo_Registro();

        //    object respuesta = _cualidad_Servicio.ListarTiposCualidad();
        //    return Json(respuesta, JsonRequestBehavior.AllowGet);
        //}
        //[HttpGet]
        //public JsonResult ListarCualidadCompetencias()
        //{
        //    //Verbo_Registro verbos = new Verbo_Registro();

        //    object respuesta = _cualidad_Servicio.ListarCualidadesPorTipo(2);
        //    return Json(respuesta, JsonRequestBehavior.AllowGet);
        //}
        //[HttpGet]
        //public JsonResult ListarCualidadHabilidades()
        //{
        //    //Verbo_Registro verbos = new Verbo_Registro();

        //    object respuesta = _cualidad_Servicio.ListarCualidadesPorTipo(1);
        //    return Json(respuesta, JsonRequestBehavior.AllowGet);
        //}        
        //[HttpPost]
        //public JsonResult ConsultarUUOO(string id)
        //{
        //    //Verbo_Registro verbos = new Verbo_Registro();

        //    object respuesta = _uuoo_Servicio.ObtenerUnidadOrganica(id);
        //    return Json(respuesta, JsonRequestBehavior.AllowGet);
        //}
        //[HttpGet]
        //[Authorize]
        //public JsonResult ListarOrganos()
        //{
        //    //Verbo_Registro verbos = new Verbo_Registro();

        //    object respuesta = _uuoo_Servicio.ListarOrganos();
        //    return Json(respuesta, JsonRequestBehavior.AllowGet);
        //}
        //[Authorize]
        //[HttpGet]
        //public JsonResult ListarUnidadesOrganicas(UnidadOrganica_Registro peticion)
        //{
        //    //Verbo_Registro verbos = new Verbo_Registro();

        //    object respuesta = _uuoo_Servicio.ListarUnidadesOrganicas(peticion.iCodOrgano.ToString());
        //    return Json(respuesta, JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        public JsonResult RegistrarPerfilCab(PerfilPuestoRegistro registro)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _perfil_Puesto_Servicio.InsertarCab(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult ActualizarPerfilCab(PerfilPuestoRegistro registro)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _perfil_Puesto_Servicio.ActualizarCab(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult RegistrarPerfilMision(PerfilPuestoRegistro registro)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _perfil_Puesto_Servicio.InsertarMision(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult RegistrarPerfilExperiencia(PerfilPuestoRegistro registro)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _perfil_Puesto_Servicio.InsertarExperiencia(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult ObtenerPerfilesPuesto(string id, string fechaIni, string fechaFin)
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _perfil_Puesto_Servicio.ObtenerPerfilesPuesto(id, fechaIni, fechaFin);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ObtenerPerfilesPuestoUserRRHH(string id, string fechaIni, string fechaFin)
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _perfil_Puesto_Servicio.ObtenerPerfilesPuestoUserRRHH(String.Empty, String.Empty, String.Empty, String.Empty);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ObtenerPerfilesPuestoJefe(string IdDependencia, string iCodUnidadOrganica, string fechaIni, string fechaFin)
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _perfil_Puesto_Servicio.ObtenerPerfilesPuestoJefe(IdDependencia, iCodUnidadOrganica, fechaIni, fechaFin);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ObtenerPerfilesPuestoJefeRRHH(string id, string fechaIni, string fechaFin)
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _perfil_Puesto_Servicio.ObtenerPerfilesPuestoJefeRRHH(id, fechaIni, fechaFin);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ObtenerPerfilesPuestoUser(string id, string fechaIni, string fechaFin)
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _perfil_Puesto_Servicio.ObtenerPerfilesPuestoUser(id, fechaIni, fechaFin);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ObtenerDependenciasPorUUOO(string id)
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _perfil_Puesto_Servicio.ObtenerDependenciasPorUUOO(id);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ActualizarPerfil(string id)
        {
            //Verbo_Registro verbos = new Verbo_Registro();
            //object respuesta = _perfil_Puesto_Servicio.ObtenerPerfilesPuesto(id);
            //return Json(respuesta, JsonRequestBehavior.AllowGet);
            //ViewBag.ProgramaSocial
            TempData["id"] = id;
            return RedirectToAction("nuevo", "Perfiles");
            
        }
        [HttpGet]
        public ActionResult VerPerfil(string id)
        {            
            TempData["id"] = id;
            return RedirectToAction("ver", "Perfiles");

        }
        [HttpGet]
        public ActionResult VerPerfil_Jefe(string id)
        {
            TempData["id"] = id;
            return RedirectToAction("ver_jefe", "Perfiles");

        }
        [HttpGet]
        public ActionResult VerPerfil_JefeRRHH(string id)
        {
            TempData["id"] = id;
            return RedirectToAction("ver_jefeRRHH", "Perfiles");

        }
        [HttpGet]
        public ActionResult VerPerfil_User(string id)
        {
            TempData["id"] = id;
            return RedirectToAction("ver_user", "Perfiles");

        }
        [HttpPost]
        public JsonResult ObtenerPerfilesPuestoPorID(string id)
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _perfil_Puesto_Servicio.ObtenerPerfilesPuestoPorID(id);
            var jsonResult = Json(respuesta, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = Int32.MaxValue;

            return jsonResult;
        }

        #region Fuciones
        [HttpGet]
        public JsonResult ListarPerfilDetFunciones(PerfilFunciones_Request peticion)
        {
            object respuesta = _perfil_Puesto_Servicio.ListarPerfilDetFunciones(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertarFunciones(PerfilFunciones_Request peticion)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _perfil_Puesto_Servicio.InsertarFunciones(peticion);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult ActualizarFunciones(PerfilFunciones_Request peticion)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _perfil_Puesto_Servicio.ActualizarFunciones(peticion);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult EliminarFunciones(PerfilFunciones_Request peticion)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _perfil_Puesto_Servicio.EliminarFunciones(peticion);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        #endregion

        #region Requisitos Adicionales
        [HttpGet]
        public JsonResult ListarPerfilDetRequisitosAdicionales(RequisitosAdicionales_Registro peticion)
        {
            object respuesta = _perfil_Puesto_Servicio.ListarPerfilDetRequisitosAdicionales(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertarDetRequisitosAdicionales(RequisitosAdicionales_Registro peticion)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _perfil_Puesto_Servicio.InsertarDetRequisitosAdicionales(peticion);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult ActualizarDetRequisitosAdicionales(RequisitosAdicionales_Registro peticion)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _perfil_Puesto_Servicio.ActualizarDetRequisitosAdicionales(peticion);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult EliminarDetRequisitosAdicionales(RequisitosAdicionales_Registro peticion)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _perfil_Puesto_Servicio.EliminarDetRequisitosAdicionales(peticion);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        #endregion

        #region Habilidades y Competencias
        [HttpGet]
        public JsonResult ListarPerfilDetHabilidades_Competencias(Habilidad_Competencias_Registro peticion)
        {
            object respuesta = _perfil_Puesto_Servicio.ListarPerfilDetHabilidades_Competencias(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarPerfilDetHabilidades(Habilidad_Competencias_Registro peticion)
        {
            object respuesta = _perfil_Puesto_Servicio.ListarPerfilDetHabilidades(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarPerfilDetCompetencias(Habilidad_Competencias_Registro peticion)
        {
            object respuesta = _perfil_Puesto_Servicio.ListarPerfilDetCompetencias(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertarDetHabilidades_Competencias(Habilidad_Competencias_Registro peticion)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _perfil_Puesto_Servicio.InsertarDetHabilidades_Competencias(peticion);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult ActualizarDetHabilidades_Competencias(Habilidad_Competencias_Registro peticion)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _perfil_Puesto_Servicio.ActualizarDetHabilidades_Competencias(peticion);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult EliminarDetHabilidades_Competencias(Habilidad_Competencias_Registro peticion)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _perfil_Puesto_Servicio.EliminarDetHabilidades_Competencias(peticion);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        #endregion

        #region Coordinaciones Internas y Externas
        [HttpGet]
        public JsonResult ListarPerfilDetCoordinacionInterna(PerfilCoordinaciones_Registro peticion)
        {
            object respuesta = _perfil_Puesto_Servicio.ListarPerfilDetCoordinacionInterna(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarPerfilDetCoordinacionExterna(PerfilCoordinaciones_Registro peticion)
        {
            object respuesta = _perfil_Puesto_Servicio.ListarPerfilDetCoordinacionExterna(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertarDetCoordinacionesInt_Ext(PerfilCoordinaciones_Registro peticion)
        {
            object respuesta = _perfil_Puesto_Servicio.InsertarDetCoordinacionesInt_Ext(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ActualizarDetCoordinacionesInt_Ext(PerfilCoordinaciones_Registro peticion)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _perfil_Puesto_Servicio.ActualizarDetCoordinacionesInt_Ext(peticion);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult EliminarDetCoordinacionesInt_Ext(PerfilCoordinaciones_Registro peticion)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _perfil_Puesto_Servicio.EliminarDetCoordinacionesInt_Ext(peticion);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }       

        #endregion

        #region Conocimientos
        [HttpGet]
        public JsonResult ListarPerfilDetConocimientosTecnicos(PerfilConocimientos_Registro peticion)
        {
            object respuesta = _perfil_Puesto_Servicio.ListarPerfilDetConocimientosTecnicos(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarPerfilDetConocimientosCursosProgramas(PerfilConocimientos_Registro peticion)
        {
            object respuesta = _perfil_Puesto_Servicio.ListarPerfilDetConocimientosCursosProgramas(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarPerfilDetConocimientosOfficeIdiomas(PerfilConocimientos_Registro peticion)
        {
            object respuesta = _perfil_Puesto_Servicio.ListarPerfilDetConocimientosOfficeIdiomas(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertarDetConocimientos(PerfilConocimientos_Registro peticion)
        {
            object respuesta = _perfil_Puesto_Servicio.InsertarDetConocimientos(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ActualizarDetConocimientos(PerfilConocimientos_Registro peticion)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _perfil_Puesto_Servicio.ActualizarDetConocimientos(peticion);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult EliminarDetConocimientos(PerfilConocimientos_Registro peticion)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _perfil_Puesto_Servicio.EliminarDetConocimientos(peticion);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        #endregion


        #region Formacion Academica
        [HttpGet]
        public JsonResult ListarPerfilDetFormAcaNivelBasico(PerfilFormacionAcademica_Registro peticion)
        {
            object respuesta = _perfil_Puesto_Servicio.ListarPerfilDetFormAcaNivelBasico(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public JsonResult ListarPerfilDetFormAcaNivelEducativo(PerfilFormacionAcademica_Registro peticion)
        {
            object respuesta = _perfil_Puesto_Servicio.ListarPerfilDetFormAcaNivelEducativo(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarPerfilDetFormAcaMaestria(PerfilFormacionAcademica_Registro peticion)
        {
            object respuesta = _perfil_Puesto_Servicio.ListarPerfilDetFormAcaMaestria(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarPerfilDetFormAcaDoctorado(PerfilFormacionAcademica_Registro peticion)
        {
            object respuesta = _perfil_Puesto_Servicio.ListarPerfilDetFormAcaDoctorado(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertarDetFormacionAcademica(PerfilFormacionAcademica_Registro peticion)
        {
            object respuesta = _perfil_Puesto_Servicio.InsertarDetFormacionAcademica(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ActualizarDetFormacionAcademica(PerfilFormacionAcademica_Registro peticion)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _perfil_Puesto_Servicio.ActualizarDetFormacionAcademica(peticion);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult EliminarDetFormacionAcademica(PerfilFormacionAcademica_Registro peticion)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _perfil_Puesto_Servicio.EliminarDetFormacionAcademica(peticion);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        #endregion
        [HttpPost]
        public JsonResult PerfilFinalizar(PerfilPuestoRegistro peticion)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _perfil_Puesto_Servicio.PerfilFinalizar(peticion);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult PerfilDerivarUser(PerfilPuestoRegistro peticion)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _perfil_Puesto_Servicio.PerfilDerivarUser(peticion);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult PerfilDerivarJefe(PerfilPuestoRegistro peticion)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _perfil_Puesto_Servicio.PerfilDerivarJefe(peticion);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult PerfilDesaprobar(PerfilPuestoRegistro peticion)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _perfil_Puesto_Servicio.PerfilDesaprobar(peticion);
                if (respuesta!=null)
                {
                    SendEmail(peticion);
                }
                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        public void SendEmail(PerfilPuestoRegistro perfil)
        {
            string id = perfil.iCodPerfil.ToString();
            IEnumerable<PerfilPuestoRegistro> PerfilCab = _perfil_Puesto_Servicio.ObtenerPerfilesPuestoPorID(id);
            //object respuesta = _perfil_Puesto_Servicio.ObtenerPerfilesPuestoPorID(id);
            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(ConfigurationManager.AppSettings["CcEmail1"].ToString()));            
            msg.From = new MailAddress(ConfigurationManager.AppSettings["EmailOwner"].ToString());
            msg.Subject = "Devolución de Perfil de Puesto";
            msg.Body = "Estimado(a) colaborador(a), se ha devuelto la solicitud de perfil de puesto <strong><a> " + PerfilCab.FirstOrDefault().strNombrePuesto + "</a></strong>, por favor revisar su bandeja" +
                        "<br/><br/>" +
                        "Atentamente, " +
                        "<br/><br/>" +
                        "\n<b>Ministerio de Desarrollo e Inclusión Social</b>" ;
                        
            msg.IsBodyHtml = true;
            SmtpClient clienteSmtp = new SmtpClient(ConfigurationManager.AppSettings["host"].ToString());
            clienteSmtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailOwnerUsuario"].ToString(), ConfigurationManager.AppSettings["EmailOwnerPassword"].ToString());
            try
            {
                clienteSmtp.Send(msg);
            }
            catch (Exception ex)
            {
                //HomeController.WriteLog("ComunidadController2", ex, "");
                //Console.Write(ex.Message);
                //Console.ReadLine();
                return;
            }
            return;
        }


        [HttpPost]
        public JsonResult PerfilDerivarJefeRRHH(PerfilPuestoRegistro peticion)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _perfil_Puesto_Servicio.PerfilDerivarJefeRRHH(peticion);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult PerfilDerivarUserRRHH(PerfilPuestoRegistro peticion)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _perfil_Puesto_Servicio.PerfilDerivarUserRRHH(peticion);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult ListarPerfilHistorico(string id)
        {
            object respuesta = _perfil_Puesto_Servicio.ListarPerfilHistorico(id);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        public FileResult PlantillaPerfilPuesto(string id)
        {
            //ObtenerPerfilesPuestoPorID

            PerfilFunciones_Request pPerfilFunciones_Request = new PerfilFunciones_Request() { iCodPerfil = Convert.ToInt32(id) };
            RequisitosAdicionales_Registro pRequisitosAdicionales_Registro = new RequisitosAdicionales_Registro() { iCodPerfil = Convert.ToInt32(id) };
            Habilidad_Competencias_Registro pHabilidad_Competencias_Registro = new Habilidad_Competencias_Registro() { iCodPerfil = Convert.ToInt32(id) };
            PerfilCoordinaciones_Registro pPerfilCoordinaciones_Registro = new PerfilCoordinaciones_Registro() { iCodPerfil = Convert.ToInt32(id) };
            PerfilConocimientos_Registro pPerfilConocimientos_Registro = new PerfilConocimientos_Registro() { iCodPerfil = Convert.ToInt32(id) };
            PerfilFormacionAcademica_Registro pPerfilFormacionAcademica_Registro = new PerfilFormacionAcademica_Registro() { iCodPerfil = Convert.ToInt32(id) };


            IEnumerable<PerfilPuestoRegistro> PerfilCab = _perfil_Puesto_Servicio.ObtenerPerfilesPuestoPorID(id);
            IEnumerable<PerfilFunciones_Request> Funciones = _perfil_Puesto_Servicio.ListarPerfilDetFunciones(pPerfilFunciones_Request);
            IEnumerable<RequisitosAdicionales_Registro> RequisitosAdicionales = _perfil_Puesto_Servicio.ListarPerfilDetRequisitosAdicionales(pRequisitosAdicionales_Registro);
            IEnumerable<Habilidad_Competencias_Registro> Habilidades = _perfil_Puesto_Servicio.ListarPerfilDetHabilidades(pHabilidad_Competencias_Registro);
            IEnumerable<Habilidad_Competencias_Registro> Competencias = _perfil_Puesto_Servicio.ListarPerfilDetCompetencias(pHabilidad_Competencias_Registro);
            IEnumerable<PerfilCoordinaciones_Registro> CoordinacionInterna = _perfil_Puesto_Servicio.ListarPerfilDetCoordinacionInterna(pPerfilCoordinaciones_Registro);
            IEnumerable<PerfilCoordinaciones_Registro> CoordinacionExterna = _perfil_Puesto_Servicio.ListarPerfilDetCoordinacionExterna(pPerfilCoordinaciones_Registro);
            IEnumerable<PerfilConocimientos_Registro> ConocimientosTecnicos = _perfil_Puesto_Servicio.ListarPerfilDetConocimientosTecnicos(pPerfilConocimientos_Registro);
            IEnumerable<PerfilConocimientos_Registro> ConocimientosCursosProgramas = _perfil_Puesto_Servicio.ListarPerfilDetConocimientosCursosProgramas(pPerfilConocimientos_Registro);
            IEnumerable<PerfilConocimientos_Registro> ConocimientosOfficeIdiomas = _perfil_Puesto_Servicio.ListarPerfilDetConocimientosOfficeIdiomas(pPerfilConocimientos_Registro);
            IEnumerable<PerfilFormacionAcademica_Registro> FormAcaNivelBasico = _perfil_Puesto_Servicio.ListarPerfilDetFormAcaNivelBasico(pPerfilFormacionAcademica_Registro);
            IEnumerable<PerfilFormacionAcademica_Registro> FormAcaNivelEducativo = _perfil_Puesto_Servicio.ListarPerfilDetFormAcaNivelEducativo(pPerfilFormacionAcademica_Registro);
            IEnumerable<PerfilFormacionAcademica_Registro> FormAcaMaestria = _perfil_Puesto_Servicio.ListarPerfilDetFormAcaMaestria(pPerfilFormacionAcademica_Registro);
            IEnumerable<PerfilFormacionAcademica_Registro> FormAcaDoctorado = _perfil_Puesto_Servicio.ListarPerfilDetFormAcaDoctorado(pPerfilFormacionAcademica_Registro);
            
            
            
            
            //PostulanteInformacion_Registro peticion = new PostulanteInformacion_Registro() { IdPostulante = IdPostulante, IdPostulacion = IdPostulacion, IdConvocatoria = IdConvocatoria };
            //peticion = _postulante_Servicio.ObtenerPostulanteFicha(peticion);

            Stream pdfStream = GenerarFichaPersonalPdf(PerfilCab, Funciones, RequisitosAdicionales, Habilidades, Competencias, CoordinacionInterna, CoordinacionExterna, ConocimientosTecnicos, ConocimientosCursosProgramas, ConocimientosOfficeIdiomas, FormAcaNivelBasico, FormAcaNivelEducativo, FormAcaMaestria, FormAcaDoctorado);


            return File(pdfStream, "application/pdf", "PerfilPuesto.pdf"); // ("Ficha", "_Layout_Blank", oAsistenciaTecnicaDetalle_Registro);
        }

        private Stream GenerarFichaPersonalPdf(IEnumerable<PerfilPuestoRegistro> PerfilCab, IEnumerable<PerfilFunciones_Request> Funciones, IEnumerable<RequisitosAdicionales_Registro> RequisitosAdicionales, IEnumerable<Habilidad_Competencias_Registro> Habilidades, IEnumerable<Habilidad_Competencias_Registro> Competencias, IEnumerable<PerfilCoordinaciones_Registro> CoordinacionInterna, IEnumerable<PerfilCoordinaciones_Registro> CoordinacionExterna, IEnumerable<PerfilConocimientos_Registro> ConocimientosTecnicos, IEnumerable<PerfilConocimientos_Registro> ConocimientosCursosProgramas, IEnumerable<PerfilConocimientos_Registro> ConocimientosOfficeIdiomas, IEnumerable<PerfilFormacionAcademica_Registro> FormAcaNivelBasico, IEnumerable<PerfilFormacionAcademica_Registro> FormAcaNivelEducativo, IEnumerable<PerfilFormacionAcademica_Registro> FormAcaMaestria, IEnumerable<PerfilFormacionAcademica_Registro> FormAcaDoctorado)
        {
            HtmlToPdf converter = new HtmlToPdf();
            converter.Options.PdfPageSize = PdfPageSize.A4;

            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            converter.Options.WebPageWidth = 1024;
            converter.Options.WebPageHeight = 0;
            converter.Options.WebPageFixedSize = false;
            converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.ShrinkOnly;
            converter.Options.AutoFitHeight = HtmlToPdfPageFitMode.NoAdjustment;

            converter.Options.MarginLeft = 30;
            converter.Options.MarginRight = 30;
            converter.Options.MarginTop = 30;
            converter.Options.MarginBottom = 30;

            string html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "PerfilPuesto/perfil_puesto1.html"));
            //string htmlParticipantes = System.IO.File.ReadAllText(ConfigurationManager.AppSettings["Negociacion.Ficha.Plantilla.Participantes.Ruta"]);
            //string htmlIndicadores = System.IO.File.ReadAllText(ConfigurationManager.AppSettings["Negociacion.Ficha.Plantilla.Indicadores.Ruta"]);

            //CultureInfo culture = new CultureInfo("es-PE");
            #region Cabecera
            html = html.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "PerfilPuesto/logo_servir.png"));
            html = html.Replace("//organo", PerfilCab.FirstOrDefault().strOrgano);
            html = html.Replace("//unidad_organica", PerfilCab.FirstOrDefault().strUnidadOrganica);
            html = html.Replace("//puesto_estructural", PerfilCab.FirstOrDefault().strPuestoEstructural);
            html = html.Replace("//nombre_puesto", PerfilCab.FirstOrDefault().strNombrePuesto);
            html = html.Replace("//dependencia_jerarquica", PerfilCab.FirstOrDefault().strDependenciaJerarquicaLineal);
            html = html.Replace("//dependencia_funcional", PerfilCab.FirstOrDefault().strDependenciaFuncional);
            html = html.Replace("//puestos_a_su_cargo", PerfilCab.FirstOrDefault().strPuestos_a_su_Cargo);
            #endregion
            
            #region Mision
            html = html.Replace("//mision", PerfilCab.FirstOrDefault().strMision);
            #endregion
            //SelectPdf.PdfDocument doc = converter.ConvertHtmlString(html, Server.MapPath("~/temp"));


            #region Funciones
            //String strAuxFuncion;
            //String strFuncion = "<tr><td style='font-size: 12px; width: 303px; height: 16px; border-bottom: thin solid;' colspan='3'>//funcion</td>" +
            //                    "<td style='width: 15px; height: 18px;'>&nbsp;</td></tr>";
            //String strFunciones = String.Empty;
            int contFunciones = 0;
            if (Funciones.Count() > 0)
            {
                
                foreach (PerfilFunciones_Request item in Funciones)
                {
                    contFunciones += 1;
                    html = html.Replace("//funciones" + "_" + contFunciones, item.Verbo.strDescripcion + " " + item.Objetivo + "" + item.Funcion);
                    //contFunciones += 1;
                    //strAuxFuncion = strFuncion;
                    //strAuxFuncion = strAuxFuncion.Replace("//funcion", contFunciones + ". " + item.Verbo + " " + item.strObjeto + "" + item.strDescripcion);
                    //strFunciones += strAuxFuncion;
                }
            }
            if (contFunciones <= 10)
            {
                for (int i = contFunciones + 1; i <= 10; i++)
                {
                    if (i<10)
                    {
                        html = html.Replace("//funciones" + "_" + i, "");
                    }
                    else
                    {
                        html = html.Replace("//funciones" + i, "");
                    }
                    
                }
            }
            //else
            //{
            //    strAuxFuncion = strFuncion;
            //    strAuxFuncion = strAuxFuncion.Replace("//funcion", "1. " + Funciones.FirstOrDefault().Verbo + " " + Funciones.FirstOrDefault().strObjeto + "" + Funciones.FirstOrDefault().strDescripcion);
            //    strFunciones += strAuxFuncion;
            //}

            //html = html.Replace("//funciones"+"_", strFunciones);
            #endregion
            
            #region Coordinaciones Principales - Internas
            String strAuxCoordInt;
            String strCoordInt = "//CoordInt</br>";
            String strCoordInternas = String.Empty;

            if (CoordinacionInterna.Count() > 0)
            {                
                foreach (PerfilCoordinaciones_Registro item in CoordinacionInterna)
                {                    
                    strAuxCoordInt = strCoordInt;
                    strAuxCoordInt = strAuxCoordInt.Replace("//CoordInt", " • " + item.Coordinacion);
                    strCoordInternas += strAuxCoordInt;
                }
            }
            else
            {
                strAuxCoordInt = strCoordInt;
                strAuxCoordInt = strAuxCoordInt.Replace("//CoordInt", "");
                strCoordInternas += strAuxCoordInt;
            }

            html = html.Replace("//coordinacion_interna", strCoordInternas);
            #endregion

            #region Coordinaciones Principales - Externas
            String strAuxCoordExt;
            String strCoordExt = "//CoordInt</br>";
            String strCoordExternas = String.Empty;

            if (CoordinacionExterna.Count() > 0)
            {                
                foreach (PerfilCoordinaciones_Registro item in CoordinacionExterna)
                {                    
                    strAuxCoordExt = strCoordExt;
                    strAuxCoordExt = strAuxCoordExt.Replace("//CoordInt", " • " + item.Coordinacion);
                    strCoordExternas += strAuxCoordExt;
                }
            }
            else
            {
                strAuxCoordExt = strCoordExt;
                strAuxCoordExt = strAuxCoordExt.Replace("//CoordInt", "");
                strCoordExternas += strAuxCoordExt;
            }

            html = html.Replace("//coordinacion_externa", strCoordExternas);
            #endregion

            //FormAcaNivelEducativo
            #region Formacion Academica
            //String strAuxCoordInt;
            //String strCoordInt = "//CoordInt</br>";
            //String strCoordInternas = String.Empty;


            if (FormAcaNivelEducativo.Count()>0)
            {
                //string strPrimaria = string.Empty;
                //string strImpPrim = string.Empty;
                //string strCompPrim = string.Empty;
                //string strSecundaria = string.Empty;
                //string strImpSec = string.Empty;
                //string strCompSec = string.Empty;
                //string strTecnica = string.Empty;
                //string strImpTec = string.Empty;
                //string strCompTec = string.Empty;
                //string strTecnicaSup = string.Empty;
                //string strImpTecSup = string.Empty;
                //string strCompTecSup = string.Empty;
                //string strUniversitaria = string.Empty;
                //string strImpUniversitaria = string.Empty;
                //string strCompUniversitaria = string.Empty;
                //string strEgreUni = string.Empty;
                //string strBacUni = string.Empty;
                //string strLicUni = string.Empty;
                //string strMaestria = string.Empty;
                //string strEgreMaes = string.Empty;
                //string strLicMaes = string.Empty;
                //string strDoct = string.Empty;
                //string strEgreDoct = string.Empty;
                //string strLicDoct = string.Empty;
                //string strColegSi = string.Empty;
                //string strColegNo = string.Empty;
                //string strHabSi = string.Empty;
                //string strHabNo = string.Empty;
                //html = html.Replace("//Primaria", "");
                //html = html.Replace("//ImpPrim", "");
                //html = html.Replace("//CompPrim", "");
                //html = html.Replace("//Secundaria", "");
                //html = html.Replace("//ImpSec", "");
                //html = html.Replace("//CompSec", "");
                //html = html.Replace("//Tecnica", "");
                //html = html.Replace("//ImpTec", "");
                //html = html.Replace("//CompTec", "");
                //html = html.Replace("//_TecnicaSup", "");
                //html = html.Replace("//_ImpTecSup", "");
                //html = html.Replace("//_CompTecSup", "");
                //html = html.Replace("//Universitaria", "");
                //html = html.Replace("//ImpUniversitaria", "");
                //html = html.Replace("//CompUniversitaria", "");

                //html = html.Replace("//EgreUni", "");
                //html = html.Replace("//BacUni", "");
                //html = html.Replace("//LicUni", "");
                //html = html.Replace("//Maestria", "");
                //html = html.Replace("//EgreMaes", "");
                //html = html.Replace("//LicMaes", "");
                //html = html.Replace("//Doct", "");
                //html = html.Replace("//EgreDoct", "");
                //html = html.Replace("//LicDoct", "");

                //html = html.Replace("//ColegSi", "");
                //html = html.Replace("//ColegNo", "");
                //html = html.Replace("//HabSi", "");
                //html = html.Replace("//HabNo", "");

                foreach (var item in FormAcaNivelBasico)
                {
                    switch (FormAcaNivelBasico.FirstOrDefault().iCodGrado)
                    {
                        case 1:
                            {
                                html = html.Replace("//Primaria", "X");
                                if (FormAcaNivelEducativo.FirstOrDefault().bCompleto != null && FormAcaNivelEducativo.FirstOrDefault().bCompleto == false)
                                {
                                    html = html.Replace("//ImpPrim", "X");
                                }
                                else
                                {
                                    html = html.Replace("//CompPrim", "X");
                                }
                                break;
                            }
                        case 2:
                            {
                                html = html.Replace("//Secundaria", "X");
                                if (FormAcaNivelEducativo.FirstOrDefault().bCompleto != null && FormAcaNivelEducativo.FirstOrDefault().bCompleto == false)
                                {
                                    html = html.Replace("//ImpSec", "X");
                                }
                                else
                                {
                                    html = html.Replace("//CompSec", "X");
                                }
                                break;
                            }
                       
                    }
                }
                foreach (var item in FormAcaNivelEducativo)
                {
                    switch (FormAcaNivelEducativo.FirstOrDefault().iCodGrado)
                    {
                        //case 1:
                        //    {
                        //        html = html.Replace("//Primaria", "X");
                        //        if (FormAcaNivelEducativo.FirstOrDefault().bCompleto != null && FormAcaNivelEducativo.FirstOrDefault().bCompleto == false)
                        //        {
                        //             html = html.Replace("//ImpPrim", "X");
                        //        }
                        //        else
                        //        {
                        //            html = html.Replace("//CompPrim", "X");
                        //        }                                
                        //        break;
                        //    }
                        //case 2:
                        //    {
                        //        html = html.Replace("//Secundaria", "X");
                        //        if (FormAcaNivelEducativo.FirstOrDefault().bCompleto != null && FormAcaNivelEducativo.FirstOrDefault().bCompleto == false)
                        //        {
                        //            html = html.Replace("//ImpSec", "X");
                        //        }
                        //        else
                        //        {
                        //            html = html.Replace("//CompSec", "X");
                        //        }   
                        //        break;
                        //    }
                        case 3:
                            {
                                html = html.Replace("//Tecnica", "X");
                                if (FormAcaNivelEducativo.FirstOrDefault().bCompleto != null && FormAcaNivelEducativo.FirstOrDefault().bCompleto == false)
                                {
                                    html = html.Replace("//ImpTec", "X");
                                }
                                else
                                {
                                    html = html.Replace("//CompTec", "X");
                                } 
                                break;
                            }
                        case 4:
                            {
                                html = html.Replace("//TecnicaSup", "X");
                                if (FormAcaNivelEducativo.FirstOrDefault().bCompleto != null && FormAcaNivelEducativo.FirstOrDefault().bCompleto == false)
                                {
                                    html = html.Replace("//ImpTecSup", "X");
                                }
                                else
                                {
                                    html = html.Replace("//CompTecSup", "X");
                                } 
                                break;
                            }
                        case 5:
                            {
                                html = html.Replace("//Universitaria", "X");
                                if (FormAcaNivelEducativo.FirstOrDefault().bCompleto != null && FormAcaNivelEducativo.FirstOrDefault().bCompleto == false)
                                {
                                    html = html.Replace("//ImpUniversitaria", "X");
                                }
                                else
                                {
                                    html = html.Replace("//CompUniversitaria", "X");

                                    /*html = html.Replace("//EgreUni", strEgreUni);
                                        html = html.Replace("//BacUni", strBacUni);
                                        html = html.Replace("//LicUni", strLicUni);*/
                                    
                                    if (FormAcaNivelEducativo.FirstOrDefault().iCodNivel != null)
                                    {
                                        switch (FormAcaNivelEducativo.FirstOrDefault().iCodNivel)
                                        {
                                            case 1:
                                                {
                                                    html = html.Replace("//EgreUni", "X");
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    html = html.Replace("//BacUni", "X");
                                                    break;
                                                }
                                            case 3:
                                                {
                                                    html = html.Replace("//LicUni", "X");
                                                    /*html = html.Replace("//ColegSi", strColegSi);
                                                        html = html.Replace("//ColegNo", strColegNo);
                                                        html = html.Replace("//HabSi", strHabSi);
                                                        html = html.Replace("//HabNo", strHabNo);*/
                                                    if (FormAcaNivelEducativo.FirstOrDefault().bColegiatura == true)
                                                    {
                                                        html = html.Replace("//ColegSi", "X");
                                                        if (FormAcaNivelEducativo.FirstOrDefault().bHabilitado == true)
                                                        {
                                                            html = html.Replace("//HabSi", "X");
                                                        }
                                                        else
                                                        {
                                                            html = html.Replace("//HabNo", "X");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        html = html.Replace("//ColegNo", "X");
                                                    }

                                                    break;
                                                }                                            
                                        }
                                    }
                                    
                                    String strAuxCarrer_Uni;
                                    String strCarrer_Uni = "//Carrer_Uni";
                                    String strCarrerasUniversitarias = String.Empty;

                                    if (FormAcaNivelEducativo.Count() > 0)
                                    {
                                        foreach (PerfilFormacionAcademica_Registro itemCarrUni in FormAcaNivelEducativo)
                                        {
                                            if (itemCarrUni.iCodGrado == 5)
                                            {
                                                strAuxCarrer_Uni = strCarrer_Uni;
                                                strAuxCarrer_Uni = strAuxCarrer_Uni.Replace("//Carrer_Uni", " • " + itemCarrUni.strNivel4 + "</br>");
                                                strCarrerasUniversitarias += strAuxCarrer_Uni;    
                                            }                                            
                                        }
                                    }
                                    else
                                    {
                                        strAuxCarrer_Uni = strCarrer_Uni;
                                        strAuxCarrer_Uni = strAuxCarrer_Uni.Replace("//Carrer_Uni", "");
                                        strCarrerasUniversitarias += strAuxCarrer_Uni;
                                    }
                                    html = html.Replace("//carreras_universitarias", strCarrerasUniversitarias);
                                }
                                //foreach (var item in FormAcaNivelEducativo)
                                //{
                                    
                                //}
                                //html = html.Replace("//carreras_universitarias ", "X");
                                break;
                            }
                        //case 6:
                        //    {
                        //        html = html.Replace("//Maestria", "X");
                        //        if (FormAcaNivelEducativo.FirstOrDefault().iCodNivel != null)
                        //        {
                        //            switch (FormAcaNivelEducativo.FirstOrDefault().iCodNivel)
                        //            {
                        //                case 1:
                        //                    {
                        //                        html = html.Replace("//EgreMaes", "X");
                        //                        break;
                        //                    }
                        //                case 3:
                        //                    {
                        //                        html = html.Replace("//LicMaes", "X");
                        //                        break;
                        //                    }
                        //            }
                        //        }
                        //        String strAuxCarrer_Mae;
                        //        String strCarrer_Mae = "//Carrer_Mae";
                        //        String strCarrerasMaestrias = String.Empty;

                        //        if (FormAcaNivelEducativo.Count() > 0)
                        //        {
                        //            foreach (PerfilFormacionAcademica_Registro itemCarrMae in FormAcaNivelEducativo)
                        //            {
                        //                if (itemCarrMae.iCodGrado == 6)
                        //                {
                        //                    strAuxCarrer_Mae = strCarrer_Mae;
                        //                    strAuxCarrer_Mae = strAuxCarrer_Mae.Replace("//Carrer_Mae", " • " + itemCarrMae.strNivel4 + "</br>");
                        //                    strCarrerasMaestrias += strAuxCarrer_Mae;
                        //                }
                        //            }
                        //        }
                        //        else
                        //        {
                        //            strAuxCarrer_Mae = strCarrer_Mae;
                        //            strAuxCarrer_Mae = strAuxCarrer_Mae.Replace("//Carrer_Mae", "");
                        //            strCarrerasMaestrias += strAuxCarrer_Mae;
                        //        }                                
                        //        html = html.Replace("//carreras_maestria", strCarrerasMaestrias);
                        //        break;
                        //    }
                        //case 7:
                        //    {
                        //        html = html.Replace("//Doct", "X");                                
                        //        if (FormAcaNivelEducativo.FirstOrDefault().iCodNivel != null)
                        //        {
                        //            switch (FormAcaNivelEducativo.FirstOrDefault().iCodNivel)
                        //            {
                        //                case 1:
                        //                    {
                        //                        html = html.Replace("//EgreDoct", "X");
                        //                        break;
                        //                    }
                        //                case 3:
                        //                    {
                        //                        html = html.Replace("//LicDoct", "X");
                        //                        break;
                        //                    }
                        //            }
                        //        }
                        //        String strAuxCarrer_Doc;
                        //        String strCarrer_Doc = "//Carrer_Doc";
                        //        String strCarrerasDoctorado = String.Empty;

                        //        if (FormAcaNivelEducativo.Count() > 0)
                        //        {
                        //            foreach (PerfilFormacionAcademica_Registro itemCarrDoc in FormAcaNivelEducativo)
                        //            {
                        //                if (itemCarrDoc.iCodGrado == 7)
                        //                {
                        //                    strAuxCarrer_Doc = strCarrer_Doc;
                        //                    strAuxCarrer_Doc = strAuxCarrer_Doc.Replace("//Carrer_Mae", " • " + itemCarrDoc.strNivel4 + "</br>");
                        //                    strCarrerasDoctorado += strAuxCarrer_Doc;
                        //                }
                        //            }
                        //        }
                        //        else
                        //        {
                        //            strAuxCarrer_Doc = strCarrer_Doc;
                        //            strAuxCarrer_Doc = strAuxCarrer_Doc.Replace("//Carrer_Mae", "");
                        //            strCarrerasDoctorado += strAuxCarrer_Doc;
                        //        }
                                
                        //        html = html.Replace("//carreras_doctorado", strCarrerasDoctorado);
                        //        break;
                        //    }                        
                    }
                }
                foreach (var item in FormAcaMaestria)
                {
                    switch (FormAcaMaestria.FirstOrDefault().iCodGrado)
                    {                        
                        case 6:
                            {
                                html = html.Replace("//Maestria", "X");
                                if (FormAcaMaestria.FirstOrDefault().iCodNivel != null)
                                {
                                    switch (FormAcaMaestria.FirstOrDefault().iCodNivel)
                                    {
                                        case 1:
                                            {
                                                html = html.Replace("//EgreMaes", "X");
                                                break;
                                            }
                                        case 3:
                                            {
                                                html = html.Replace("//LicMaes", "X");
                                                break;
                                            }
                                    }
                                }
                                String strAuxCarrer_Mae;
                                String strCarrer_Mae = "//Carrer_Mae";
                                String strCarrerasMaestrias = String.Empty;

                                if (FormAcaMaestria.Count() > 0)
                                {
                                    foreach (PerfilFormacionAcademica_Registro itemCarrMae in FormAcaMaestria)
                                    {
                                        if (itemCarrMae.iCodGrado == 6)
                                        {
                                            strAuxCarrer_Mae = strCarrer_Mae;
                                            strAuxCarrer_Mae = strAuxCarrer_Mae.Replace("//Carrer_Mae", " • " + itemCarrMae.strNivel4 + "</br>");
                                            strCarrerasMaestrias += strAuxCarrer_Mae;
                                        }
                                    }
                                }
                                else
                                {
                                    strAuxCarrer_Mae = strCarrer_Mae;
                                    strAuxCarrer_Mae = strAuxCarrer_Mae.Replace("//Carrer_Mae", "");
                                    strCarrerasMaestrias += strAuxCarrer_Mae;
                                }
                                html = html.Replace("//carreras_maestria", strCarrerasMaestrias);
                                break;
                            }                        
                    }
                }
                foreach (var item in FormAcaDoctorado)
                {
                    switch (FormAcaDoctorado.FirstOrDefault().iCodGrado)
                    {                        
                        case 7:
                            {
                                html = html.Replace("//Doct", "X");
                                if (FormAcaDoctorado.FirstOrDefault().iCodNivel != null)
                                {
                                    switch (FormAcaDoctorado.FirstOrDefault().iCodNivel)
                                    {
                                        case 1:
                                            {
                                                html = html.Replace("//EgreDoct", "X");
                                                break;
                                            }
                                        case 3:
                                            {
                                                html = html.Replace("//LicDoct", "X");
                                                break;
                                            }
                                    }
                                }
                                String strAuxCarrer_Doc;
                                String strCarrer_Doc = "//Carrer_Doc";
                                String strCarrerasDoctorado = String.Empty;

                                if (FormAcaDoctorado.Count() > 0)
                                {
                                    foreach (PerfilFormacionAcademica_Registro itemCarrDoc in FormAcaDoctorado)
                                    {
                                        if (itemCarrDoc.iCodGrado == 7)
                                        {
                                            strAuxCarrer_Doc = strCarrer_Doc;
                                            strAuxCarrer_Doc = strAuxCarrer_Doc.Replace("//Carrer_Doc", " • " + itemCarrDoc.strNivel4 + "</br>");
                                            strCarrerasDoctorado += strAuxCarrer_Doc;
                                        }
                                    }
                                }
                                else
                                {
                                    strAuxCarrer_Doc = strCarrer_Doc;
                                    strAuxCarrer_Doc = strAuxCarrer_Doc.Replace("//Carrer_Doc", "");
                                    strCarrerasDoctorado += strAuxCarrer_Doc;
                                }

                                html = html.Replace("//carreras_doctorado", strCarrerasDoctorado);
                                break;
                            }
                    }
                }
                html = html.Replace("//Primaria", "");
                html = html.Replace("//ImpPrim", "");
                html = html.Replace("//CompPrim", "");
                html = html.Replace("//Secundaria", "");
                html = html.Replace("//ImpSec", "");
                html = html.Replace("//CompSec", "");
                html = html.Replace("//Tecnica", "");
                html = html.Replace("//ImpTec", "");
                html = html.Replace("//CompTec", "");
                html = html.Replace("//_TecnicaSup", "");
                html = html.Replace("//_ImpTecSup", "");
                html = html.Replace("//_CompTecSup", "");
                html = html.Replace("//Universitaria", "");
                html = html.Replace("//ImpUniversitaria", "");
                html = html.Replace("//CompUniversitaria", "");

                html = html.Replace("//EgreUni", "");
                html = html.Replace("//BacUni", "");
                html = html.Replace("//LicUni", "");
                html = html.Replace("//carreras_universitarias", "");
                html = html.Replace("//Maestria", "");
                html = html.Replace("//EgreMaes", "");
                html = html.Replace("//LicMaes", "");
                html = html.Replace("//carreras_maestria", "");
                html = html.Replace("//Doct", "");
                html = html.Replace("//EgreDoct", "");
                html = html.Replace("//LicDoct", "");
                html = html.Replace("//carreras_doctorado", "");
                html = html.Replace("//ColegSi", "");
                html = html.Replace("//ColegNo", "");
                html = html.Replace("//HabSi", "");
                html = html.Replace("//HabNo", "");




            }
            
            #endregion

            #region Conocimientos - Cursos Tecnicos
            String strAuxConoc_tec;
            String strConoc_tec = "//Conoc_tec";
            String strConocimiento_tecnico = String.Empty;

            if (ConocimientosTecnicos.Count() > 0)
            {
                foreach (PerfilConocimientos_Registro item in ConocimientosTecnicos)
                {
                    strAuxConoc_tec = strConoc_tec;
                    strAuxConoc_tec = strAuxConoc_tec.Replace("//Conoc_tec", " • " + item.Conocimientos + "</br>");
                    strConocimiento_tecnico += strAuxConoc_tec;
                }
            }
            else
            {
                strAuxConoc_tec = strConoc_tec;
                strAuxConoc_tec = strAuxConoc_tec.Replace("//Conoc_tec", "");
                strConocimiento_tecnico += strAuxConoc_tec;
            }

            html = html.Replace("//conocimiento_tecnico", strConocimiento_tecnico);
            #endregion

            #region Conocimientos - Cursos Programas
            String strAuxConoc_prog;
            String strConoc_prog = "//Conoc_prog";
            String strConocimiento_programa = String.Empty;

            if (ConocimientosCursosProgramas.Count() > 0)
            {
                foreach (PerfilConocimientos_Registro item in ConocimientosCursosProgramas)
                {
                    strAuxConoc_prog = strConoc_prog;
                    strAuxConoc_prog = strAuxConoc_prog.Replace("//Conoc_prog", " • " + item.Conocimientos + "</br>");
                    strConocimiento_programa += strAuxConoc_prog;
                }
            }
            else
            {
                strAuxConoc_prog = strConoc_prog;
                strAuxConoc_prog = strAuxConoc_prog.Replace("//Conoc_prog", "");
                strConocimiento_programa += strAuxConoc_prog;
            }

            html = html.Replace("//conocimiento_Programas", strConocimiento_programa);
            #endregion

            #region Conocimientos - Ofimatica - Idiomas - Dialectos
            String strAuxConoc_ofimProcText;
            String strConoc_ofimProcText_temp = "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>" +
                                    "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>" +
                                    "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>" +
                                    "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>";
            String strConoc_ofimProcText = "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>//Conoc_ofimProcText_NoAplica</td>" +
                                    "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>//Conoc_ofimProcText_Basico</td>" +
                                    "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>//Conoc_ofimProcText_Intermedio</td>" +
                                    "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>//Conoc_ofimProcText_Avanzado</td>";

            String strConocimiento_ofimaticaProcText = strConoc_ofimProcText_temp;


            String strAuxConoc_ofimHojCal;
            String strConoc_ofimHojCal_temp = "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>" +
                                    "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>" +
                                    "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>" +
                                    "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>";
            String strConoc_ofimHojCal = "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>//Conoc_ofimHojCal_NoAplica</td>" +
                                    "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>//Conoc_ofimHojCal_Basico</td>" +
                                    "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>//Conoc_ofimHojCal_Intermedio</td>" +
                                    "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>//Conoc_ofimHojCal_Avanzado</td>";
            String strConocimiento_ofimaticaHojCal = strConoc_ofimHojCal_temp;



            String strAuxConoc_ofimProgPres;
            String strConoc_ofimProgPres_temp = "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>" +
                                    "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>" +
                                    "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>" +
                                    "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>";
            String strConoc_ofimProgPres = "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>//Conoc_ofimProgPres_NoAplica</td>" +
                                    "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>//Conoc_ofimProgPres_Basico</td>" +
                                    "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>//Conoc_ofimProgPres_Intermedio</td>" +
                                    "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>//Conoc_ofimProgPres_Avanzado</td>";
            String strConocimiento_ofimaticaProgPres = strConoc_ofimProgPres_temp;


            
            
            String strAuxConoc_IdiomaIngles;
            String strConoc_IdiomaIngles_temp = "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>" +
                                    "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>" +
                                    "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>" +
                                    "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>";
            String strConoc_IdiomaIngles = "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>//Conoc_IdiomaIngles_NoAplica</td>" +
                                    "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>//Conoc_IdiomaIngles_Basico</td>" +
                                    "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>//Conoc_IdiomaIngles_Intermedio</td>" +
                                    "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>//Conoc_IdiomaIngles_Avanzado</td>";
            String strConocimiento_IdiomaIngles = strConoc_IdiomaIngles_temp;

            String strAuxConoc_IdiomaOtro;
            String strConoc_IdiomaOtro_temp = "<td style='border: 1pt solid black; font-size: 12px; width: 130px; text-align:center;'>" +
                                                "<p>Otros (Especificar)</p>"+
                                                "<p>______________</p>" +
                                                "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>" +
                                                "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>" +
                                                "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>" +
                                                "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>";
            String strConoc_IdiomaOtro = "<td style='border: 1pt solid black; font-size: 12px; width: 130px; text-align:center;'>" +
                                         "<p>Otros (Especificar)</p>" +
                                         "<p>//otro_Idioma</p>" +
                                        "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>//Conoc_IdiomaOtro_NoAplica</td>" +
                                        "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>//Conoc_IdiomaOtro_Basico</td>" +
                                        "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>//Conoc_IdiomaOtro_Intermedio</td>" +
                                        "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>//Conoc_IdiomaOtro_Avanzado</td>";
            String strConocimiento_IdiomaOtro = strConoc_IdiomaOtro_temp;

            String strAuxConoc_DialectoQuechua;
            String strConoc_DialectoQuechua_temp = "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>" +
                                    "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>" +
                                    "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>" +
                                    "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>";
            String strConoc_DialectoQuechua = "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>//Conoc_DialectoQuechua_NoAplica</td>" +
                                    "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>//Conoc_DialectoQuechua_Basico</td>" +
                                    "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>//Conoc_DialectoQuechua_Intermedio</td>" +
                                    "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>//Conoc_DialectoQuechua_Avanzado</td>";
            String strConocimiento_DialectoQuechua = strConoc_DialectoQuechua_temp;


            String strAuxConoc_DialectoOtros;
            String strConoc_DialectoOtros_temp = "<td style='border: 1pt solid black; font-size: 12px; width: 130px; text-align:center;'>" +
                                                "<p>Otros (Especificar)</p>" +
                                                "<p>______________</p>" +
                                                "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>" +
                                                "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>" +
                                                "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>" +
                                                "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>";
            String strConoc_DialectoOtros = "<td style='border: 1pt solid black; font-size: 12px; width: 130px; text-align:center;'>" +
                                         "<p>Otros (Especificar)</p>" +
                                         "<p>//otro_dialecto</p>" +
                                        "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>//Conoc_DialectoOtros_NoAplica</td>" +
                                        "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>//Conoc_DialectoOtros_Basico</td>" +
                                        "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>//Conoc_DialectoOtros_Intermedio</td>" +
                                        "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>//Conoc_DialectoOtros_Avanzado</td>";
            String strConocimiento_DialectoOtros = strConoc_DialectoOtros_temp;
                        
            if (ConocimientosOfficeIdiomas.Count() > 0)
            {
                int contOtroIdiomaDialecto = 0;
                foreach (PerfilConocimientos_Registro item in ConocimientosOfficeIdiomas)
                {

                    switch (item.PerfilTipoMateriaOtros.iCodTipoMateriaOtros)
                    {
                        case 1:
                            {                                
                                switch (item.PerfilTipoSubMateriaOtros.iCodTipoSubMateriaOtros)
                                {
                                    case 1:
                                        {
                                            strAuxConoc_ofimProcText = strConoc_ofimProcText;
                                            switch (item.PerfilNivelMateria.iCodTipoNivelMateria)
                                            {
                                                case 1:
                                                    {
                                                        strAuxConoc_ofimProcText = strAuxConoc_ofimProcText.Replace("//Conoc_ofimProcText_NoAplica", " ");
                                                        strAuxConoc_ofimProcText = strAuxConoc_ofimProcText.Replace("//Conoc_ofimProcText_Basico", "X");
                                                        strAuxConoc_ofimProcText = strAuxConoc_ofimProcText.Replace("//Conoc_ofimProcText_Intermedio", " ");
                                                        strAuxConoc_ofimProcText = strAuxConoc_ofimProcText.Replace("//Conoc_ofimProcText_Avanzado", " ");
                                                        break;
                                                    }
                                                case 2:
                                                    {
                                                        strAuxConoc_ofimProcText = strAuxConoc_ofimProcText.Replace("//Conoc_ofimProcText_NoAplica", " ");
                                                        strAuxConoc_ofimProcText = strAuxConoc_ofimProcText.Replace("//Conoc_ofimProcText_Basico", " ");
                                                        strAuxConoc_ofimProcText = strAuxConoc_ofimProcText.Replace("//Conoc_ofimProcText_Intermedio", "X");
                                                        strAuxConoc_ofimProcText = strAuxConoc_ofimProcText.Replace("//Conoc_ofimProcText_Avanzado", " ");
                                                        break;
                                                    }
                                                case 3:
                                                    {
                                                        strAuxConoc_ofimProcText = strAuxConoc_ofimProcText.Replace("//Conoc_ofimProcText_NoAplica", " ");
                                                        strAuxConoc_ofimProcText = strAuxConoc_ofimProcText.Replace("//Conoc_ofimProcText_Basico", " ");
                                                        strAuxConoc_ofimProcText = strAuxConoc_ofimProcText.Replace("//Conoc_ofimProcText_Intermedio", " ");
                                                        strAuxConoc_ofimProcText = strAuxConoc_ofimProcText.Replace("//Conoc_ofimProcText_Avanzado", "X");
                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }
                                            strConocimiento_ofimaticaProcText = strAuxConoc_ofimProcText;
                                            html = html.Replace("//procesador_texto", strConocimiento_ofimaticaProcText);                                            
                                            break;
                                        }
                                    case 2:
                                        {
                                            strAuxConoc_ofimHojCal = strConoc_ofimHojCal;
                                            switch (item.PerfilNivelMateria.iCodTipoNivelMateria)
                                            {
                                                case 1:
                                                    {
                                                        strAuxConoc_ofimHojCal = strAuxConoc_ofimHojCal.Replace("//Conoc_ofimHojCal_NoAplica", " ");
                                                        strAuxConoc_ofimHojCal = strAuxConoc_ofimHojCal.Replace("//Conoc_ofimHojCal_Basico", "X");
                                                        strAuxConoc_ofimHojCal = strAuxConoc_ofimHojCal.Replace("//Conoc_ofimHojCal_Intermedio", " ");
                                                        strAuxConoc_ofimHojCal = strAuxConoc_ofimHojCal.Replace("//Conoc_ofimHojCal_Avanzado", " ");
                                                        break;
                                                    }
                                                case 2:
                                                    {
                                                        strAuxConoc_ofimHojCal = strAuxConoc_ofimHojCal.Replace("//Conoc_ofimHojCal_NoAplica", " ");
                                                        strAuxConoc_ofimHojCal = strAuxConoc_ofimHojCal.Replace("//Conoc_ofimHojCal_Basico", " ");
                                                        strAuxConoc_ofimHojCal = strAuxConoc_ofimHojCal.Replace("//Conoc_ofimProcText_NoAplica", "X");
                                                        strAuxConoc_ofimHojCal = strAuxConoc_ofimHojCal.Replace("//Conoc_ofimHojCal_Avanzado", " ");
                                                        break;
                                                    }
                                                case 3:
                                                    {
                                                        strAuxConoc_ofimHojCal = strAuxConoc_ofimHojCal.Replace("//Conoc_ofimHojCal_NoAplica", " ");
                                                        strAuxConoc_ofimHojCal = strAuxConoc_ofimHojCal.Replace("//Conoc_ofimHojCal_Basico", " ");
                                                        strAuxConoc_ofimHojCal = strAuxConoc_ofimHojCal.Replace("//Conoc_ofimHojCal_Intermedio", " ");
                                                        strAuxConoc_ofimHojCal = strAuxConoc_ofimHojCal.Replace("//Conoc_ofimHojCal_Avanzado", "X");
                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }
                                            strConocimiento_ofimaticaHojCal = strAuxConoc_ofimHojCal;                                            
                                            html = html.Replace("//hojacalculo", strConocimiento_ofimaticaHojCal);                                            
                                            break;
                                        }
                                    case 3:
                                        {
                                            strAuxConoc_ofimProgPres = strConoc_ofimProgPres;
                                            switch (item.PerfilNivelMateria.iCodTipoNivelMateria)
                                            {
                                                case 1:
                                                    {
                                                        strAuxConoc_ofimProgPres = strAuxConoc_ofimProgPres.Replace("//Conoc_ofimProgPres_NoAplica", " ");
                                                        strAuxConoc_ofimProgPres = strAuxConoc_ofimProgPres.Replace("//Conoc_ofimProgPres_Basico", "X");
                                                        strAuxConoc_ofimProgPres = strAuxConoc_ofimProgPres.Replace("//Conoc_ofimProgPres_Intermedio", item.Conocimientos);
                                                        strAuxConoc_ofimProgPres = strAuxConoc_ofimProgPres.Replace("//Conoc_ofimProgPres_Avanzado", item.Conocimientos);
                                                        break;
                                                    }
                                                case 2:
                                                    {
                                                        strAuxConoc_ofimProgPres = strAuxConoc_ofimProgPres.Replace("//Conoc_ofimProgPres_NoAplica", " ");
                                                        strAuxConoc_ofimProgPres = strAuxConoc_ofimProgPres.Replace("//Conoc_ofimProgPres_Basico", " ");
                                                        strAuxConoc_ofimProgPres = strAuxConoc_ofimProgPres.Replace("//Conoc_ofimProgPres_Intermedio", "X");
                                                        strAuxConoc_ofimProgPres = strAuxConoc_ofimProgPres.Replace("//Conoc_ofimProgPres_Avanzado", " ");
                                                        break;
                                                    }
                                                case 3:
                                                    {
                                                        strAuxConoc_ofimProgPres = strAuxConoc_ofimProgPres.Replace("//Conoc_ofimProgPres_NoAplica", " ");
                                                        strAuxConoc_ofimProgPres = strAuxConoc_ofimProgPres.Replace("//Conoc_ofimProgPres_Basico", " ");
                                                        strAuxConoc_ofimProgPres = strAuxConoc_ofimProgPres.Replace("//Conoc_ofimProgPres_Intermedio", " ");
                                                        strAuxConoc_ofimProgPres = strAuxConoc_ofimProgPres.Replace("//Conoc_ofimProgPres_Avanzado", "X");
                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }
                                            strConocimiento_ofimaticaProgPres = strAuxConoc_ofimProgPres;                                            
                                            html = html.Replace("//ProgPres", strConocimiento_ofimaticaProgPres);
                                            break;                                            
                                        }
                                    
                                    default:
                                        break;
                                }                                
                                
                                break;
                            }
                        case 2:
                            {
                                switch (item.PerfilTipoSubMateriaOtros.strDescripcion)
                                {
                                    case "Inglés":
                                        {
                                            strAuxConoc_IdiomaIngles = strConoc_IdiomaIngles;
                                            switch (item.PerfilNivelMateria.iCodTipoNivelMateria)
                                            {
                                                case 1:
                                                    {
                                                        strAuxConoc_IdiomaIngles = strAuxConoc_IdiomaIngles.Replace("//Conoc_IdiomaIngles_NoAplica", " ");
                                                        strAuxConoc_IdiomaIngles = strAuxConoc_IdiomaIngles.Replace("//Conoc_IdiomaIngles_Basico", "X");
                                                        strAuxConoc_IdiomaIngles = strAuxConoc_IdiomaIngles.Replace("//Conoc_IdiomaIngles_Intermedio", " ");
                                                        strAuxConoc_IdiomaIngles = strAuxConoc_IdiomaIngles.Replace("//Conoc_IdiomaIngles_Avanzado", " ");
                                                        break;
                                                    }
                                                case 2:
                                                    {
                                                        strAuxConoc_IdiomaIngles = strAuxConoc_IdiomaIngles.Replace("//Conoc_IdiomaIngles_NoAplica", " ");
                                                        strAuxConoc_IdiomaIngles = strAuxConoc_IdiomaIngles.Replace("//Conoc_IdiomaIngles_Basico", " ");
                                                        strAuxConoc_IdiomaIngles = strAuxConoc_IdiomaIngles.Replace("//Conoc_IdiomaIngles_Intermedio", "X");
                                                        strAuxConoc_IdiomaIngles = strAuxConoc_IdiomaIngles.Replace("//Conoc_IdiomaIngles_Avanzado", " ");
                                                        break;
                                                    }
                                                case 3:
                                                    {
                                                        strAuxConoc_IdiomaIngles = strAuxConoc_IdiomaIngles.Replace("//Conoc_IdiomaIngles_NoAplica", " ");
                                                        strAuxConoc_IdiomaIngles = strAuxConoc_IdiomaIngles.Replace("//Conoc_IdiomaIngles_Basico", " ");
                                                        strAuxConoc_IdiomaIngles = strAuxConoc_IdiomaIngles.Replace("//Conoc_IdiomaIngles_Intermedio", " ");
                                                        strAuxConoc_IdiomaIngles = strAuxConoc_IdiomaIngles.Replace("//Conoc_IdiomaIngles_Avanzado", "X");
                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }
                                            strConocimiento_IdiomaIngles = strAuxConoc_IdiomaIngles;                                            
                                            html = html.Replace("//ingles", strConocimiento_IdiomaIngles);                                            
                                            break;                                            
                                        }
                                    default:
                                        {
                                            contOtroIdiomaDialecto += 1;
                                            strAuxConoc_IdiomaOtro = strConoc_IdiomaOtro;
                                            switch (item.PerfilNivelMateria.iCodTipoNivelMateria)
                                            {
                                                case 1:
                                                    {
                                                        
                                                        strAuxConoc_IdiomaOtro = strAuxConoc_IdiomaOtro.Replace("//otro_Idioma", item.PerfilTipoSubMateriaOtros.strDescripcion);
                                                        strAuxConoc_IdiomaOtro = strAuxConoc_IdiomaOtro.Replace("//Conoc_IdiomaOtro_NoAplica", " ");
                                                        strAuxConoc_IdiomaOtro = strAuxConoc_IdiomaOtro.Replace("//Conoc_IdiomaOtro_Basico", "X");
                                                        strAuxConoc_IdiomaOtro = strAuxConoc_IdiomaOtro.Replace("//Conoc_IdiomaOtro_Intermedio", " ");
                                                        strAuxConoc_IdiomaOtro = strAuxConoc_IdiomaOtro.Replace("//Conoc_IdiomaOtro_Avanzado", " ");
                                                        break;
                                                    }
                                                case 2:
                                                    {
                                                        strAuxConoc_IdiomaOtro = strAuxConoc_IdiomaOtro.Replace("//otro_Idioma", item.PerfilTipoSubMateriaOtros.strDescripcion);
                                                        strAuxConoc_IdiomaOtro = strAuxConoc_IdiomaOtro.Replace("//Conoc_IdiomaOtro_NoAplica", " ");
                                                        strAuxConoc_IdiomaOtro = strAuxConoc_IdiomaOtro.Replace("//Conoc_IdiomaOtro_Basico", " ");
                                                        strAuxConoc_IdiomaOtro = strAuxConoc_IdiomaOtro.Replace("//Conoc_IdiomaOtro_Intermedio", "X");
                                                        strAuxConoc_IdiomaOtro = strAuxConoc_IdiomaOtro.Replace("//Conoc_IdiomaOtro_Avanzado", " ");
                                                        break;
                                                    }
                                                case 3:
                                                    {
                                                        strAuxConoc_IdiomaOtro = strAuxConoc_IdiomaOtro.Replace("//otro_Idioma", item.PerfilTipoSubMateriaOtros.strDescripcion);
                                                        strAuxConoc_IdiomaOtro = strAuxConoc_IdiomaOtro.Replace("//Conoc_IdiomaOtro_NoAplica", " ");
                                                        strAuxConoc_IdiomaOtro = strAuxConoc_IdiomaOtro.Replace("//Conoc_IdiomaOtro_Basico", " ");
                                                        strAuxConoc_IdiomaOtro = strAuxConoc_IdiomaOtro.Replace("//Conoc_IdiomaOtro_Intermedio", " ");
                                                        strAuxConoc_IdiomaOtro = strAuxConoc_IdiomaOtro.Replace("//Conoc_IdiomaOtro_Avanzado", "X");
                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }
                                            strConocimiento_IdiomaOtro = strAuxConoc_IdiomaOtro;
                                            if (contOtroIdiomaDialecto == 1)
                                            {
                                                html = html.Replace("//otroIdiomaDialecto_1", strConocimiento_IdiomaOtro);
                                            }
                                            else
                                            {
                                                html = html.Replace("//otroIdiomaDialecto_2", strConocimiento_IdiomaOtro);
                                            }                                            
                                            break;           
                                        }                                    
                                }    
                                break;
                            }
                        case 3:
                            {
                                switch (item.PerfilTipoSubMateriaOtros.strDescripcion)
                                {
                                    case "Quechua":
                                        {
                                            strAuxConoc_DialectoQuechua = strConoc_DialectoQuechua;
                                            switch (item.PerfilNivelMateria.iCodTipoNivelMateria)
                                            {
                                                case 1:
                                                    {
                                                        strAuxConoc_DialectoQuechua = strAuxConoc_DialectoQuechua.Replace("//Conoc_DialectoQuechua_NoAplica", " ");
                                                        strAuxConoc_DialectoQuechua = strAuxConoc_DialectoQuechua.Replace("//Conoc_DialectoQuechua_Basico", "X");
                                                        strAuxConoc_DialectoQuechua = strAuxConoc_DialectoQuechua.Replace("//Conoc_DialectoQuechua_Intermedio", " ");
                                                        strAuxConoc_DialectoQuechua = strAuxConoc_DialectoQuechua.Replace("//Conoc_DialectoQuechua_Avanzado", " ");
                                                        break;
                                                    }
                                                case 2:
                                                    {
                                                        strAuxConoc_DialectoQuechua = strAuxConoc_DialectoQuechua.Replace("//Conoc_DialectoQuechua_NoAplica", " ");
                                                        strAuxConoc_DialectoQuechua = strAuxConoc_DialectoQuechua.Replace("//Conoc_DialectoQuechua_Basico", " ");
                                                        strAuxConoc_DialectoQuechua = strAuxConoc_DialectoQuechua.Replace("//Conoc_DialectoQuechua_Intermedio", "X");
                                                        strAuxConoc_DialectoQuechua = strAuxConoc_DialectoQuechua.Replace("//Conoc_DialectoQuechua_Avanzado", " ");
                                                        break;
                                                    }
                                                case 3:
                                                    {
                                                        strAuxConoc_DialectoQuechua = strAuxConoc_DialectoQuechua.Replace("//Conoc_DialectoQuechua_NoAplica", " ");
                                                        strAuxConoc_DialectoQuechua = strAuxConoc_DialectoQuechua.Replace("//Conoc_DialectoQuechua_Basico", " ");
                                                        strAuxConoc_DialectoQuechua = strAuxConoc_DialectoQuechua.Replace("//Conoc_DialectoQuechua_Intermedio", " ");
                                                        strAuxConoc_DialectoQuechua = strAuxConoc_DialectoQuechua.Replace("//Conoc_DialectoQuechua_Avanzado", "X");
                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }
                                            strConocimiento_DialectoQuechua = strAuxConoc_DialectoQuechua;                                            
                                            html = html.Replace("//quechua", strConocimiento_DialectoQuechua);
                                            
                                            break;                                            

                                        }
                                    default:
                                        {
                                            contOtroIdiomaDialecto += 1;
                                            strAuxConoc_DialectoOtros = strConoc_DialectoOtros;
                                            switch (item.PerfilNivelMateria.iCodTipoNivelMateria)
                                            {
                                                case 1:
                                                    {

                                                        strAuxConoc_DialectoOtros = strAuxConoc_DialectoOtros.Replace("//otro_dialecto", item.PerfilTipoSubMateriaOtros.strDescripcion);
                                                        strAuxConoc_DialectoOtros = strAuxConoc_DialectoOtros.Replace("//Conoc_DialectoOtros_NoAplica", " ");
                                                        strAuxConoc_DialectoOtros = strAuxConoc_DialectoOtros.Replace("//Conoc_DialectoOtros_Basico", "X");
                                                        strAuxConoc_DialectoOtros = strAuxConoc_DialectoOtros.Replace("//Conoc_DialectoOtros_Intermedio", " ");
                                                        strAuxConoc_DialectoOtros = strAuxConoc_DialectoOtros.Replace("//Conoc_DialectoOtros_Avanzado", " ");
                                                        break;
                                                    }
                                                case 2:
                                                    {
                                                        strAuxConoc_DialectoOtros = strAuxConoc_DialectoOtros.Replace("//otro_dialecto", item.PerfilTipoSubMateriaOtros.strDescripcion);
                                                        strAuxConoc_DialectoOtros = strAuxConoc_DialectoOtros.Replace("//Conoc_DialectoOtros_NoAplica", " ");
                                                        strAuxConoc_DialectoOtros = strAuxConoc_DialectoOtros.Replace("//Conoc_DialectoOtros_Basico", " ");
                                                        strAuxConoc_DialectoOtros = strAuxConoc_DialectoOtros.Replace("//Conoc_DialectoOtros_Intermedio", "X");
                                                        strAuxConoc_DialectoOtros = strAuxConoc_DialectoOtros.Replace("//Conoc_DialectoOtros_Avanzado", " ");
                                                        break;
                                                    }
                                                case 3:
                                                    {
                                                        strAuxConoc_DialectoOtros = strAuxConoc_DialectoOtros.Replace("//otro_dialecto", item.PerfilTipoSubMateriaOtros.strDescripcion);
                                                        strAuxConoc_DialectoOtros = strAuxConoc_DialectoOtros.Replace("//Conoc_DialectoOtros_NoAplica", " ");
                                                        strAuxConoc_DialectoOtros = strAuxConoc_DialectoOtros.Replace("//Conoc_DialectoOtros_Basico", " ");
                                                        strAuxConoc_DialectoOtros = strAuxConoc_DialectoOtros.Replace("//Conoc_DialectoOtros_Intermedio", " ");
                                                        strAuxConoc_DialectoOtros = strAuxConoc_DialectoOtros.Replace("//Conoc_DialectoOtros_Avanzado", "X");
                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }
                                            strConocimiento_DialectoOtros = strAuxConoc_DialectoOtros;
                                            if (contOtroIdiomaDialecto == 1)
                                            {
                                                html = html.Replace("//otroIdiomaDialecto_1", strConocimiento_DialectoOtros);
                                            }
                                            else
                                            {
                                                html = html.Replace("//otroIdiomaDialecto_2", strConocimiento_DialectoOtros);
                                            }
                                            break;
                                        }
                                }
                                break;
                            }
                        default:
                            break;
                    }
                    
                }
                html = html.Replace("//procesador_texto", strConocimiento_ofimaticaProcText);
                html = html.Replace("//hojacalculo", strConocimiento_ofimaticaHojCal);
                html = html.Replace("//ProgPres", strConocimiento_ofimaticaProgPres);
                html = html.Replace("//ingles", strConocimiento_IdiomaIngles);
                html = html.Replace("//otroIdiomaDialecto_1", strConocimiento_IdiomaOtro);
                html = html.Replace("//otroIdiomaDialecto_2", strConocimiento_IdiomaOtro);
                html = html.Replace("//quechua", strConocimiento_DialectoQuechua);
            }
            else
            {
            //    strAuxConoc_ofimProcText = strConoc_ofimProcText;
            //    strAuxConoc_ofimProcText = strAuxConoc_ofimProcText.Replace("//Conoc_ofim", " • " + ConocimientosTecnicos.FirstOrDefault().strConocimientos);
            //    strConocimiento_ofimaticaProcText += strAuxConoc_ofimProcText;
                html = html.Replace("//procesador_texto", strConocimiento_ofimaticaProcText);
                html = html.Replace("//hojacalculo", strConocimiento_ofimaticaHojCal);
                html = html.Replace("//ProgPres", strConocimiento_ofimaticaProgPres);
                html = html.Replace("//ingles", strConocimiento_IdiomaIngles);
                html = html.Replace("//otroIdiomaDialecto_1", strConocimiento_IdiomaOtro);
                html = html.Replace("//otroIdiomaDialecto_2", strConocimiento_IdiomaOtro);
                html = html.Replace("//quechua", strConocimiento_DialectoQuechua);
            }
            
            #endregion

            #region Experiencia - General
            html = html.Replace("//experiencia_general", "Se requiere " + PerfilCab.FirstOrDefault().iAnioExpGeneral.ToString() + " año(s) de experiencia general");            
            #endregion

            #region Experiencia - Especifica
            html = html.Replace("//experiencia_especifica", "Se requiere " + PerfilCab.FirstOrDefault().iAnioExpEspecifica.ToString() + " año(s) de experiencia específica");
            #endregion

            #region Experiencia - Sector Publico
            html = html.Replace("//experiencia_sector_publico", "Se requiere " + PerfilCab.FirstOrDefault().iAnioExpSectorPublico.ToString() + " año(s) de experiencia en el sector público");
            #endregion

            #region Nivel Minimo
            String strNivelMinimo = string.Empty;
            switch (PerfilCab.FirstOrDefault().iCodNivelMinimo)
            {
                case 1:
                    {
                        strNivelMinimo = "<td style='width: 10px; height: 18px;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Practicante profesional&nbsp;</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>X</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Auxiliar o Asistente</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Analista</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Especialista</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Supervisor / Coordinador&nbsp;</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Jefe de &Aacute;rea o Departamento</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Gerente&nbsp;o Director</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px;'>&nbsp;</td>";
                        break;
                
                    }
                case 2:
                    {
                        strNivelMinimo = "<td style='width: 10px; height: 18px;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Practicante profesional&nbsp;</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Auxiliar o Asistente</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>X</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Analista</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Especialista</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Supervisor / Coordinador&nbsp;</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Jefe de &Aacute;rea o Departamento</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Gerente&nbsp;o Director</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px;'>&nbsp;</td>";
                        break;

                    }
                case 3:
                    {
                        strNivelMinimo = "<td style='width: 10px; height: 18px;'>&nbsp;</td>" +                            
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Practicante profesional&nbsp;</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Auxiliar o Asistente</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Analista</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>X</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Especialista</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Supervisor / Coordinador&nbsp;</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Jefe de &Aacute;rea o Departamento</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Gerente&nbsp;o Director</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px;'>&nbsp;</td>";
                        break;

                    }
                case 4:
                    {
                        strNivelMinimo = "<td style='width: 10px; height: 18px;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Practicante profesional&nbsp;</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Auxiliar o Asistente</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Analista</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Especialista</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>X</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Supervisor / Coordinador&nbsp;</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Jefe de &Aacute;rea o Departamento</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Gerente&nbsp;o Director</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px;'>&nbsp;</td>";
                        break;

                    }
                case 5:
                    {
                        strNivelMinimo = "<td style='width: 10px; height: 18px;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Practicante profesional&nbsp;</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Auxiliar o Asistente</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Analista</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Especialista</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Supervisor / Coordinador&nbsp;</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>X</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Jefe de &Aacute;rea o Departamento</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Gerente&nbsp;o Director</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px;'>&nbsp;</td>";
                        break;

                    }

                case 6:
                    {
                        strNivelMinimo = "<td style='width: 10px; height: 18px;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Practicante profesional&nbsp;</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Auxiliar o Asistente</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Analista</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Especialista</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Supervisor / Coordinador&nbsp;</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Jefe de &Aacute;rea o Departamento</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>X</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Gerente&nbsp;o Director</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px;'>&nbsp;</td>";
                        break;

                    }
                case 7:
                    {
                        strNivelMinimo = "<td style='width: 10px; height: 18px;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Practicante profesional&nbsp;</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Auxiliar o Asistente</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Analista</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Especialista</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Supervisor / Coordinador&nbsp;</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Jefe de &Aacute;rea o Departamento</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Gerente&nbsp;o Director</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>X</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px;'>&nbsp;</td>";
                        break;

                    }
                
            }

            html = html.Replace("//nivelMinimo", strNivelMinimo);            
            #endregion


            #region Habilidades - Competencias
            String strAuxHab;
            String strHab_ = "//Hab</br>";
            String strHabilidades = String.Empty;
            String strHab = String.Empty;

            String strHabilidadesCompetencias = String.Empty;

            if (Habilidades.Count() > 0)
            {
                //strAuxHab = strHab_;
                foreach (Habilidad_Competencias_Registro item in Habilidades)
                {
                    
                    if (Habilidades.First() == item)
                    {
                        strHab += item.Cualidad.strNombre;
                    }
                    else
                    {
                        strHab += ", " + item.Cualidad.strNombre;                        
                    }                    
                }
                //strAuxHab = strAuxHab.Replace("//Hab", strHab);
                //strHabilidades += strAuxHab;
            }

            String strAuxComp;
            String strComp_ = "//Comp</br>";
            String strCompetencias = String.Empty;
            String strComp = String.Empty;
            if (Competencias.Count() > 0)
            {
                //strAuxComp = strComp_;
                foreach (Habilidad_Competencias_Registro item in Competencias)
                {

                    if (Competencias.First() == item)
                    {
                        strComp += item.Cualidad.strNombre;
                    }
                    else
                    {
                        strComp += ", " + item.Cualidad.strNombre;
                    }
                }
                //strAuxComp = strAuxComp.Replace("//Comp", strComp);
                //strCompetencias += strAuxComp;
            }
            if (strHab==string.Empty)
            {
                strHabilidadesCompetencias = strComp;
            }
            else
            {
                if (strComp==string.Empty)
                {
                    strHabilidadesCompetencias = strHab;
                }
                else
                {
                    strHabilidadesCompetencias = strHab + ", " + strComp;
                }
            }
            html = html.Replace("//habilidades_competencias", strHabilidadesCompetencias);
            #endregion

            #region Requisitos Adicionales
            String strAuxReqAdic;
            String strReqAdic = "//ReqAdic";
            String strRequisitosAdicionales = String.Empty;

            if (RequisitosAdicionales.Count() > 0)
            {
                foreach (RequisitosAdicionales_Registro item in RequisitosAdicionales)
                {
                    strAuxReqAdic = strReqAdic;
                    strAuxReqAdic = strAuxReqAdic.Replace("//ReqAdic", " • " + item.Requisito + "</br>");
                    strRequisitosAdicionales += strAuxReqAdic;
                }
            }
            else
            {
                strAuxReqAdic = strReqAdic;
                strAuxReqAdic = strAuxReqAdic.Replace("//ReqAdic", "&nbsp;");
                strRequisitosAdicionales += strAuxReqAdic;
            }

            html = html.Replace("//requisitos_adicionales", strRequisitosAdicionales);
            #endregion


            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(html, Server.MapPath("~/temp"));
            
            MemoryStream pdfStream = new MemoryStream();
            doc.Save(pdfStream);
            pdfStream.Position = 0;
            doc.Close();

            return pdfStream;
        }
    }
}
