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
using MVCSisGesRRHH.Models;
using System.IO;
using SelectPdf;
using MIDIS.ORI.LogicaNegocio;

namespace MVCSisGesRRHH.Controllers
{
    public class PerfilesController : Controller
    {
        private readonly T_genm_verbo_LN _verbo_Servicio = new T_genm_verbo_LN();
        private readonly T_genm_uuoo_LN _uuoo_Servicio = new T_genm_uuoo_LN();
        private readonly T_genm_perfil_puesto_LN _perfil_Puesto_Servicio = new T_genm_perfil_puesto_LN();
        private readonly T_genm_cualidad_LN _cualidad_Servicio = new T_genm_cualidad_LN();
        private readonly T_genm_empleado_LN _empleado_Servicio = new T_genm_empleado_LN();

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
        public ActionResult NuevoSERVIR()
        {
            
            ViewBag.Titulo = "PERFILES DE PUESTO  ->  REGISTRO DE PERFIL DE PUESTO SERVIR";
            if (TempData["id"] != null)
            {
                ViewBag.idPerfil = TempData["id"];
                ViewBag.Titulo = "PERFILES DE PUESTO  ->  ACTUALIZACIÓN DE PERFIL DE PUESTO SERVIR";
            }
            //else {
            //    PerfilPuestoRegistro registro = new PerfilPuestoRegistro();
            //    registro.iTipoPerfil = 1; // TIPO DE PERFIL CAS
            //    registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;
            //    registro.iCodPerfil = _perfil_Puesto_Servicio.InsertarCab(registro);

            //    TempData["id"] = registro.iCodPerfil;
            //}

            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult nuevoCAS()
        {
            if (TempData["id"] != null)
            {
                ViewBag.idPerfil = TempData["id"];
                ViewBag.idTrab = TempData["idTrab"];
                ViewBag.nombreTrab = TempData["nombreTrab"];

                return View();
            }
            else {
                return View("mensajeSinAcceso");
            }
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
        public JsonResult ListarVerbos()
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _verbo_Servicio.ListarVerbos();
            return Json(respuesta, JsonRequestBehavior.AllowGet);
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
        public JsonResult ListarNivelEducativoMaestria()
        {
            List<PerfillNivelEducativo_Response> lista = new List<PerfillNivelEducativo_Response>();
            lista.Add(new PerfillNivelEducativo_Response() { iCodNivel = 1, strDescripcion = "Egresado", bEstado = true});
            lista.Add(new PerfillNivelEducativo_Response() { iCodNivel = 5, strDescripcion = "Grado (Magister)", bEstado = true });
            lista.Add(new PerfillNivelEducativo_Response() { iCodNivel = 4, strDescripcion = "Otros", bEstado = true });

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarNivelEducativoDoctorado()
        {
            List<PerfillNivelEducativo_Response> lista = new List<PerfillNivelEducativo_Response>();
            lista.Add(new PerfillNivelEducativo_Response() { iCodNivel = 1, strDescripcion = "Egresado", bEstado = true });
            lista.Add(new PerfillNivelEducativo_Response() { iCodNivel = 6, strDescripcion = "Grado (Doctor)", bEstado = true });
            lista.Add(new PerfillNivelEducativo_Response() { iCodNivel = 4, strDescripcion = "Otros", bEstado = true });

            object respuesta = lista;

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
            peticion.strDescripcion = "%" + (String.IsNullOrEmpty(peticion.strDescripcion) ? "" : peticion.strDescripcion) + "%";

            object respuesta = _perfil_Puesto_Servicio.ListarCarreraNivel4(peticion.strCodCarrera, peticion.strDescripcion);
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
            peticion.strDescripcion = "%" + (String.IsNullOrEmpty(peticion.strDescripcion) ? "" : peticion.strDescripcion) + "%";

            object respuesta = _perfil_Puesto_Servicio.ListarCarreraNivel4_Mae(peticion.strCodCarrera, peticion.strDescripcion);
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
            peticion.strDescripcion = "%" + (String.IsNullOrEmpty(peticion.strDescripcion) ? "" : peticion.strDescripcion) + "%";

            object respuesta = _perfil_Puesto_Servicio.ListarCarreraNivel4_Doc(peticion.strCodCarrera, peticion.strDescripcion);
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
        [HttpGet]
        public JsonResult ListarTipoCualidad()
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _cualidad_Servicio.ListarTiposCualidad();
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarCualidadCompetencias()
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _cualidad_Servicio.ListarCualidadesPorTipo(2);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarCualidadHabilidades()
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _cualidad_Servicio.ListarCualidadesPorTipo(1);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarEstadoPerfil()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("P", "PENDIENTE"));
            lista.Add(new Estado_Response("F", "REVISADO"));
            lista.Add(new Estado_Response("A", "FIRMADO"));

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarPeriodicidad()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("1", "TEMPORAL"));
            lista.Add(new Estado_Response("2", "PERMANENTE"));
            
            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarTipoReqPerfil()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("1", "NUEVO"));
            lista.Add(new Estado_Response("2", "POR REEMPLAZO"));

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarTipoServicioPerfil()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("1", "PRESENCIAL"));
            lista.Add(new Estado_Response("2", "REMOTO"));
            lista.Add(new Estado_Response("3", "MIXTO"));

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarRequiereExamenPerfil()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("0", "NO"));
            lista.Add(new Estado_Response("1", "SI"));

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ConsultarUUOO(string id)
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _uuoo_Servicio.ObtenerUnidadOrganica(id);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        //[Authorize]
        public JsonResult ListarOrganos()
        {
            //Verbo_Registro verbos = new Verbo_Registro();
            List<UnidadOrganica_Registro> lista = new List<UnidadOrganica_Registro>();
            lista.Add(new UnidadOrganica_Registro() { iCodigoDependencia = 25, strUnidad_Organica = "COMITE DE TRANSPARENCIA Y VIGILANCIA CIUDADANA" });
            lista.Add(new UnidadOrganica_Registro() { iCodigoDependencia = 1, strUnidad_Organica = "DESPACHO MINISTERIAL" });
            lista.Add(new UnidadOrganica_Registro() { iCodigoDependencia = 10, strUnidad_Organica = "DESPACHO VICE MINISTERIAL DE POLITICAS Y EVALUACION SOCIAL" });
            lista.Add(new UnidadOrganica_Registro() { iCodigoDependencia = 11, strUnidad_Organica = "DESPACHO VICE MINISTERIAL DE PRESTACIONES SOCIALES" });
            lista.Add(new UnidadOrganica_Registro() { iCodigoDependencia = 77, strUnidad_Organica = "DIRECCIÓN GENERAL DE CALIDAD DE LA GESTIÓN DE LAS PRESTACIONES SOCIALES" });
            lista.Add(new UnidadOrganica_Registro() { iCodigoDependencia = 76, strUnidad_Organica = "DIRECCIÓN GENERAL DE DISEÑO Y ARTICULACIÓN DE LAS PRESTACIONES SOCIALES" });
            lista.Add(new UnidadOrganica_Registro() { iCodigoDependencia = 74, strUnidad_Organica = "DIRECCIÓN GENERAL DE FOCALIZACIÓN E INFORMACIÓN SOCIAL" });
            lista.Add(new UnidadOrganica_Registro() { iCodigoDependencia = 75, strUnidad_Organica = "DIRECCIÓN GENERAL DE IMPLEMENTACIÓN DE POLÍTICAS Y ARTICULACIÓN TERRITORIAL" });
            lista.Add(new UnidadOrganica_Registro() { iCodigoDependencia = 13, strUnidad_Organica = "DIRECCION GENERAL DE POLITICAS Y ESTRATEGIAS" });
            lista.Add(new UnidadOrganica_Registro() { iCodigoDependencia = 14, strUnidad_Organica = "DIRECCION GENERAL DE SEGUIMIENTO Y EVALUACION" });
            lista.Add(new UnidadOrganica_Registro() { iCodigoDependencia = 21, strUnidad_Organica = "MESA DE CONCERTACION PARA LA LUCHA CONTRA LA POBREZA" });
            lista.Add(new UnidadOrganica_Registro() { iCodigoDependencia = 5, strUnidad_Organica = "OFICINA GENERAL DE ADMINISTRACION" });
            lista.Add(new UnidadOrganica_Registro() { iCodigoDependencia = 6, strUnidad_Organica = "OFICINA GENERAL DE ASESORIA JURIDICA" });
            lista.Add(new UnidadOrganica_Registro() { iCodigoDependencia = 38, strUnidad_Organica = "OFICINA GENERAL DE COMUNICACION ESTRATEGICA" });
            lista.Add(new UnidadOrganica_Registro() { iCodigoDependencia = 7, strUnidad_Organica = "OFICINA GENERAL DE COOPERACION Y ASUNTOS INTERNACIONALES" });
            lista.Add(new UnidadOrganica_Registro() { iCodigoDependencia = 32, strUnidad_Organica = "OFICINA GENERAL DE PLANEAMIENTO PRESUPUESTO Y  MODERNIZACIÓN" });
            lista.Add(new UnidadOrganica_Registro() { iCodigoDependencia = 37, strUnidad_Organica = "OFICINA GENERAL DE RECURSOS HUMANOS" });
            lista.Add(new UnidadOrganica_Registro() { iCodigoDependencia = 73, strUnidad_Organica = "OFICINA GENERAL DE TECNOLOGÍAS DE LA INFORMACIÓN" });
            lista.Add(new UnidadOrganica_Registro() { iCodigoDependencia = 4, strUnidad_Organica = "ORGANO DE CONTROL INSTITUCIONAL" });
            lista.Add(new UnidadOrganica_Registro() { iCodigoDependencia = 15, strUnidad_Organica = "PROCURADURIA PUBLICA" });
            lista.Add(new UnidadOrganica_Registro() { iCodigoDependencia = 9, strUnidad_Organica = "SECRETARIA GENERAL" });
            
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        //[Authorize]
        [HttpGet]
        public JsonResult ListarUnidadesOrganicas(UnidadOrganica_Registro peticion)
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _uuoo_Servicio.ListarUnidadesOrganicas(peticion.iCodOrgano.ToString());
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
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
                registro.iCodTrabajador = (VariablesWeb.oT_Genm_Usuario is null ? registro.iCodTrabajador : (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador);

                object respuesta = _perfil_Puesto_Servicio.ActualizarCab(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult ActualizarPerfilAnexo1(PerfilPuestoRegistro registro)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                registro.iCodTrabajador = (VariablesWeb.oT_Genm_Usuario is null ? registro.iCodTrabajador : (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador);

                object respuesta = _perfil_Puesto_Servicio.ActualizarAnexo1(registro);

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
        public JsonResult ObtenerPerfilesPuestoUserRRHH(String strOrgano, String strUO, String strEstado, string strNombre)
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _perfil_Puesto_Servicio.ObtenerPerfilesPuestoUserRRHH(strOrgano, strUO, strEstado, strNombre);
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
            //return RedirectToAction("nuevo", "Perfiles");
            return RedirectToAction("nuevo", "Perfiles");

        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ActualizarPerfilxUsuario(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                String[] arraydata = new Crypto().Desencriptar(id).Split('|');
                if (arraydata != null)
                {
                    PerfilPuestoRegistro peticion = new PerfilPuestoRegistro() { iCodPerfil = Int32.Parse(arraydata[0]), IdUsuarioRegistro = Int32.Parse(arraydata[1]) };
                    
                    ViewBag.IdPerfil = arraydata[0];
                    ViewBag.IdTrabajador = arraydata[1];
                    ViewBag.Trabajador = arraydata[2];

                    //Verbo_Registro verbos = new Verbo_Registro();
                    //object respuesta = _perfil_Puesto_Servicio.ObtenerPerfilesPuesto(id);
                    //return Json(respuesta, JsonRequestBehavior.AllowGet);
                    //ViewBag.ProgramaSocial
                    TempData["id"] = arraydata[0];
                    TempData["idTrab"] = arraydata[1];
                    TempData["nombreTrab"] = arraydata[2];
                    
                    return RedirectToAction("nuevoCAS", "Perfiles");
                }
            }
            
            return null;
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

        [HttpPost]

        public JsonResult ObtenerPerfilesAnexo1PorID(string id)
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _perfil_Puesto_Servicio.ObtenerPerfilesAnexo1PorID(id);
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
        public JsonResult InsertarCarreraProfesional(PerfilFormacionAcademica_Registro peticion)
        {
            object respuesta = _perfil_Puesto_Servicio.InsertarCarreraProfesional(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult InsertarMaestria(PerfilFormacionAcademica_Registro peticion)
        {
            object respuesta = _perfil_Puesto_Servicio.InsertarMaestria(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult InsertarDoctorado(PerfilFormacionAcademica_Registro peticion)
        {
            object respuesta = _perfil_Puesto_Servicio.InsertarDoctorado(peticion);
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
        public JsonResult PerfilEliminar(PerfilPuestoRegistro peticion)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _perfil_Puesto_Servicio.PerfilEliminar(peticion);

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


            return File(pdfStream, "application/pdf"); //, "PerfilPuesto.pdf"); // ("Ficha", "_Layout_Blank", oAsistenciaTecnicaDetalle_Registro);
        }
        public FileResult PlantillaRequerimientoPuesto(string id)
        {
            IEnumerable<PerfilPuestoRegistro> PerfilCab = _perfil_Puesto_Servicio.ObtenerPerfilesAnexo1PorID(id);
            Stream pdfStream = GenerarFichaRequerimientoPdf(PerfilCab);

            return File(pdfStream, "application/pdf"); //, "PerfilPuesto.pdf"); // ("Ficha", "_Layout_Blank", oAsistenciaTecnicaDetalle_Registro);
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
            
            #region Cabecera
            html = html.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "PerfilPuesto/logo_midis.png"));
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
            
            #region Funciones
            String strAux;
            String strFuncion = "<tr style='height: 30px;'>" +
                                "<td style='font-size: 13px; width: 10px; height: 30px;'>//posi</td>" +
                                "<td style='font-size: 13px; width: 990px; height: 30px; border-bottom: thin solid; padding-top:1px; padding-bottom:1px' colspan='3'>//funcion</td>" +
                                "</tr>";
            String strFunciones = String.Empty;

            int contFunciones = 0;
            if (Funciones.Count() > 0)
            {
                foreach (PerfilFunciones_Request item in Funciones)
                {
                    contFunciones += 1;
                    strAux = strFuncion;
                    strAux = strAux.Replace("//posi", (contFunciones.ToString().Length == 1 ? "&nbsp;" + contFunciones.ToString() : contFunciones.ToString()));
                    strAux = strAux.Replace("//funcion", item.Verbo.strDescripcion + " " + item.Objetivo + " " + item.Funcion);

                    strFunciones += strAux;
                    //html = html.Replace("//funciones" + "_" + contFunciones, item.Verbo.strDescripcion + " " + item.Objetivo + "" + item.Funcion);
                }
            }
            else {
                strAux = strFuncion;
                strAux = strAux.Replace("//posi", "&nbsp;");
                strAux = strAux.Replace("//funcion", "&nbsp;");

                strFunciones += strAux;
            }

            html = html.Replace("//funciones", strFunciones);
            
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

            #region Otros campos
            html = html.Replace("//condiciones", PerfilCab.FirstOrDefault().strCondiciones);
            html = html.Replace("//periodicidad", PerfilCab.FirstOrDefault().strPeriodicidad);

            String strTipoPeriodicidad = string.Empty;
            String strP1 = "&nbsp;";
            String strP2 = "&nbsp;";
            switch (PerfilCab.FirstOrDefault().iPeriodicidad)
            {
                case 1:
                    {
                        strP1 = "X";
                        break;
                    }
                case 2:
                    {
                        strP2 = "X";
                        break;
                    }
            }

            strTipoPeriodicidad = "<table><tr><td style='width: 10px; height: 18px;'>&nbsp;</td>" +
                        "<td style='width: 100px; height: 18px; font-size: 12px; text-align: center;'>Temporal:&nbsp;</td>" +
                        "<td style='width: 40px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>" + strP1 + "</td>" +
                        "<td style='width: 10px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 100px; height: 18px; font-size: 12px; text-align: center;'>Permanente:&nbsp;</td>" +
                        "<td style='width: 40px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>" + strP2 + "</td>" +
                        "<td style='width: 10px; height: 18px; font-size: 12px;'>&nbsp;</td></tr></table>";

            html = html.Replace("//cod_periodici", strTipoPeriodicidad);
            #endregion

            //FormAcaNivelEducativo
            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(html, Server.MapPath("~/temp"));
            string html2 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "PerfilPuesto/perfil_puesto2.html"));

            #region Formacion Academica

            if ((FormAcaNivelBasico.Count() > 0) || (FormAcaNivelEducativo.Count() > 0))
            {
                foreach (var item in FormAcaNivelBasico)
                {
                    switch (FormAcaNivelBasico.FirstOrDefault().iCodGrado)
                    {
                        case 1:
                            {
                                html2 = html2.Replace("//NP_", "X");
                                if (FormAcaNivelBasico.FirstOrDefault().bCompleto != null && FormAcaNivelBasico.FirstOrDefault().bCompleto == false)
                                    html2 = html2.Replace("//IP_", "X");
                                else
                                    html2 = html2.Replace("//CP_", "X");

                                break;
                            }
                        case 2:
                            {
                                html2 = html2.Replace("//NS_", "X");
                                if (FormAcaNivelBasico.FirstOrDefault().bCompleto != null && FormAcaNivelBasico.FirstOrDefault().bCompleto == false)
                                    html2 = html2.Replace("//IS_", "X");
                                else
                                    html2 = html2.Replace("//CS_", "X");

                                break;
                            }
                    }
                }
                foreach (var item in FormAcaNivelEducativo)
                {
                    switch (FormAcaNivelEducativo.FirstOrDefault().iCodGrado)
                    {
                        case 3:
                            {
                                html2 = html2.Replace("//NB_", "X");
                                if (FormAcaNivelEducativo.FirstOrDefault().bCompleto != null && FormAcaNivelEducativo.FirstOrDefault().bCompleto == false)
                                    html2 = html2.Replace("//IB_", "X");
                                else
                                    html2 = html2.Replace("//CB_", "X");

                                break;
                            }
                        case 4:
                            {
                                html2 = html2.Replace("//NT_", "X");
                                if (FormAcaNivelEducativo.FirstOrDefault().bCompleto != null && FormAcaNivelEducativo.FirstOrDefault().bCompleto == false)
                                    html2 = html2.Replace("//IT_", "X");
                                else
                                    html2 = html2.Replace("//CT_", "X");

                                break;
                            }
                        case 5:
                            {
                                html2 = html2.Replace("//NU_", "X");
                                if (FormAcaNivelEducativo.FirstOrDefault().bCompleto != null && FormAcaNivelEducativo.FirstOrDefault().bCompleto == false)
                                    html2 = html2.Replace("//IU_", "X");
                                else
                                {
                                    html2 = html2.Replace("//CU_", "X");

                                    if (FormAcaNivelEducativo.FirstOrDefault().iCodNivel != null)
                                    {
                                        switch (FormAcaNivelEducativo.FirstOrDefault().iCodNivel)
                                        {
                                            case 1:
                                                {
                                                    html2 = html2.Replace("//EgreUni", "X");
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    html2 = html2.Replace("//BacUni", "X");
                                                    break;
                                                }
                                            case 3:
                                                {
                                                    html2 = html2.Replace("//LicUni", "X");
                                                    if (FormAcaNivelEducativo.FirstOrDefault().bColegiatura == true)
                                                    {
                                                        html2 = html2.Replace("//ColegSi", "X");
                                                        if (FormAcaNivelEducativo.FirstOrDefault().bHabilitado == true)
                                                            html2 = html2.Replace("//HabSi", "X");
                                                        else
                                                            html2 = html2.Replace("//HabNo", "X");
                                                    }
                                                    else
                                                        html2 = html2.Replace("//ColegNo", "X");

                                                    break;
                                                }
                                        }
                                    }

                                    String strCarrerasUniversitarias = String.Empty;
                                    if (FormAcaNivelEducativo.Count() > 0)
                                    {
                                        foreach (PerfilFormacionAcademica_Registro itemCarrUni in FormAcaNivelEducativo)
                                        {
                                            if (itemCarrUni.iCodGrado == 5)
                                            {
                                                if (!String.IsNullOrEmpty(strCarrerasUniversitarias))
                                                    strCarrerasUniversitarias += " y/o ";

                                                strCarrerasUniversitarias += itemCarrUni.strNivel4;
                                            }
                                        }
                                    }

                                    html2 = html2.Replace("//carreras_universitarias", strCarrerasUniversitarias);
                                }

                                break;
                            }
                    }
                }
                foreach (var item in FormAcaMaestria)
                {
                    switch (FormAcaMaestria.FirstOrDefault().iCodGrado)
                    {
                        case 6:
                            {
                                html2 = html2.Replace("//Maestria", "X");
                                if (FormAcaMaestria.FirstOrDefault().iCodNivel != null)
                                {
                                    switch (FormAcaMaestria.FirstOrDefault().iCodNivel)
                                    {
                                        case 1:
                                            {
                                                html2 = html2.Replace("//EgreMaes", "X");
                                                break;
                                            }
                                        case 3:
                                            {
                                                html2 = html2.Replace("//LicMaes", "X");
                                                break;
                                            }
                                    }
                                }

                                String strCarrerasMaestrias = String.Empty;
                                if (FormAcaMaestria.Count() > 0)
                                {
                                    foreach (PerfilFormacionAcademica_Registro itemCarrMae in FormAcaMaestria)
                                    {
                                        if (itemCarrMae.iCodGrado == 6)
                                        {
                                            if (!String.IsNullOrEmpty(strCarrerasMaestrias))
                                                strCarrerasMaestrias += " y/o ";

                                            strCarrerasMaestrias += itemCarrMae.strNivel4;
                                        }
                                    }
                                }

                                html2 = html2.Replace("//carreras_maestria", strCarrerasMaestrias);
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
                                html2 = html2.Replace("//Doct", "X");
                                if (FormAcaDoctorado.FirstOrDefault().iCodNivel != null)
                                {
                                    switch (FormAcaDoctorado.FirstOrDefault().iCodNivel)
                                    {
                                        case 1:
                                            {
                                                html2 = html2.Replace("//EgreDoct", "X");
                                                break;
                                            }
                                        case 3:
                                            {
                                                html2 = html2.Replace("//LicDoct", "X");
                                                break;
                                            }
                                    }
                                }
                                String strCarrerasDoctorado = String.Empty;

                                if (FormAcaDoctorado.Count() > 0)
                                {
                                    foreach (PerfilFormacionAcademica_Registro itemCarrDoc in FormAcaDoctorado)
                                    {
                                        if (itemCarrDoc.iCodGrado == 7)
                                        {
                                            if (!String.IsNullOrEmpty(strCarrerasDoctorado))
                                                strCarrerasDoctorado += " y/o ";

                                            strCarrerasDoctorado += itemCarrDoc.strNivel4;
                                        }
                                    }
                                }

                                html2 = html2.Replace("//carreras_doctorado", strCarrerasDoctorado);
                                break;
                            }
                    }
                }
                html2 = html2.Replace("//NP_", "");
                html2 = html2.Replace("//IP_", "");
                html2 = html2.Replace("//CP_", "");
                html2 = html2.Replace("//NS_", "");
                html2 = html2.Replace("//IS_", "");
                html2 = html2.Replace("//CS_", "");
                html2 = html2.Replace("//NB_", "");
                html2 = html2.Replace("//IB_", "");
                html2 = html2.Replace("//CB_", "");
                html2 = html2.Replace("//NT_", "");
                html2 = html2.Replace("//IT_", "");
                html2 = html2.Replace("//CT_", "");
                html2 = html2.Replace("//NU_", "");
                html2 = html2.Replace("//IU_", "");
                html2 = html2.Replace("//CU_", "");

                html2 = html2.Replace("//EgreUni", "");
                html2 = html2.Replace("//BacUni", "");
                html2 = html2.Replace("//LicUni", "");
                html2 = html2.Replace("//carreras_universitarias", "");
                html2 = html2.Replace("//Maestria", "");
                html2 = html2.Replace("//EgreMaes", "");
                html2 = html2.Replace("//LicMaes", "");
                html2 = html2.Replace("//carreras_maestria", "");
                html2 = html2.Replace("//Doct", "");
                html2 = html2.Replace("//EgreDoct", "");
                html2 = html2.Replace("//LicDoct", "");
                html2 = html2.Replace("//carreras_doctorado", "");
                html2 = html2.Replace("//ColegSi", "");
                html2 = html2.Replace("//ColegNo", "");
                html2 = html2.Replace("//HabSi", "");
                html2 = html2.Replace("//HabNo", "");
            }
            else {
                html2 = html2.Replace("//NP_", "");
                html2 = html2.Replace("//IP_", "");
                html2 = html2.Replace("//CP_", "");
                html2 = html2.Replace("//NS_", "");
                html2 = html2.Replace("//IS_", "");
                html2 = html2.Replace("//CS_", "");
                html2 = html2.Replace("//NB_", "");
                html2 = html2.Replace("//IB_", "");
                html2 = html2.Replace("//CB_", "");
                html2 = html2.Replace("//NT_", "");
                html2 = html2.Replace("//IT_", "");
                html2 = html2.Replace("//CT_", "");
                html2 = html2.Replace("//NU_", "");
                html2 = html2.Replace("//IU_", "");
                html2 = html2.Replace("//CU_", "");

                html2 = html2.Replace("//EgreUni", "");
                html2 = html2.Replace("//BacUni", "");
                html2 = html2.Replace("//LicUni", "");
                html2 = html2.Replace("//carreras_universitarias", "");
                html2 = html2.Replace("//Maestria", "");
                html2 = html2.Replace("//EgreMaes", "");
                html2 = html2.Replace("//LicMaes", "");
                html2 = html2.Replace("//carreras_maestria", "");
                html2 = html2.Replace("//Doct", "");
                html2 = html2.Replace("//EgreDoct", "");
                html2 = html2.Replace("//LicDoct", "");
                html2 = html2.Replace("//carreras_doctorado", "");
                html2 = html2.Replace("//ColegSi", "");
                html2 = html2.Replace("//ColegNo", "");
                html2 = html2.Replace("//HabSi", "");
                html2 = html2.Replace("//HabNo", "");
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

            html2 = html2.Replace("//conocimiento_tecnico", strConocimiento_tecnico);
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
                    strAuxConoc_prog = strAuxConoc_prog.Replace("//Conoc_prog", " • " + item.PerfilTipoMateria.strDescripcion + (item.PerfilTipoMateria.iCodTipoMateria == 1 ? " en " : (item.PerfilTipoMateria.iCodTipoMateria == 2 ? " en " : (item.PerfilTipoMateria.iCodTipoMateria == 3 ? " de " : (item.PerfilTipoMateria.iCodTipoMateria == 4 ? " en " : (item.PerfilTipoMateria.iCodTipoMateria == 5 ? " en " : (item.PerfilTipoMateria.iCodTipoMateria == 6 ? " de " : String.Empty)))))) + item.Conocimientos + "</br>");
                    strConocimiento_programa += strAuxConoc_prog;
                }
            }
            else
            {
                strAuxConoc_prog = strConoc_prog;
                strAuxConoc_prog = strAuxConoc_prog.Replace("//Conoc_prog", "");
                strConocimiento_programa += strAuxConoc_prog;
            }

            html2 = html2.Replace("//conocimiento_Programas", strConocimiento_programa);
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
                                                "Otros (Especificar)" +
                                                "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>" +
                                                "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>" +
                                                "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>" +
                                                "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>";
            String strConoc_IdiomaOtro = "<td style='border: 1pt solid black; font-size: 12px; width: 130px; text-align:center;'>" +
                                         "Otros (Especificar)" +
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
                                                "Otros (Especificar)" +
                                                "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>" +
                                                "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>" +
                                                "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>" +
                                                "<td style='border: 1pt solid black; font-size: 12px;width: 80px; text-align:center;'>&nbsp;</td>";
            String strConoc_DialectoOtros = "<td style='border: 1pt solid black; font-size: 12px; width: 130px; text-align:center;'>" +
                                         "Otros (Especificar)" +
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
                                            html2 = html2.Replace("//procesador_texto", strConocimiento_ofimaticaProcText);
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
                                                        strAuxConoc_ofimHojCal = strAuxConoc_ofimHojCal.Replace("//Conoc_ofimHojCal_Intermedio", "X");
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
                                            html2 = html2.Replace("//hojacalculo", strConocimiento_ofimaticaHojCal);
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
                                            html2 = html2.Replace("//ProgPres", strConocimiento_ofimaticaProgPres);
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
                                            html2 = html2.Replace("//ingles", strConocimiento_IdiomaIngles);
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
                                                html2 = html2.Replace("//otroIdiomaDialecto_1", strConocimiento_IdiomaOtro);
                                            }
                                            else
                                            {
                                                html2 = html2.Replace("//otroIdiomaDialecto_2", strConocimiento_IdiomaOtro);
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
                                            html2 = html2.Replace("//quechua", strConocimiento_DialectoQuechua);

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
                                                html2 = html2.Replace("//otroIdiomaDialecto_1", strConocimiento_DialectoOtros);
                                            }
                                            else
                                            {
                                                html2 = html2.Replace("//otroIdiomaDialecto_2", strConocimiento_DialectoOtros);
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
                html2 = html2.Replace("//procesador_texto", strConocimiento_ofimaticaProcText);
                html2 = html2.Replace("//hojacalculo", strConocimiento_ofimaticaHojCal);
                html2 = html2.Replace("//ProgPres", strConocimiento_ofimaticaProgPres);
                html2 = html2.Replace("//ingles", strConocimiento_IdiomaIngles);
                html2 = html2.Replace("//otroIdiomaDialecto_1", strConocimiento_IdiomaOtro);
                html2 = html2.Replace("//otroIdiomaDialecto_2", strConocimiento_IdiomaOtro);
                html2 = html2.Replace("//quechua", strConocimiento_DialectoQuechua);
            }
            else
            {
                //    strAuxConoc_ofimProcText = strConoc_ofimProcText;
                //    strAuxConoc_ofimProcText = strAuxConoc_ofimProcText.Replace("//Conoc_ofim", " • " + ConocimientosTecnicos.FirstOrDefault().strConocimientos);
                //    strConocimiento_ofimaticaProcText += strAuxConoc_ofimProcText;
                html2 = html2.Replace("//procesador_texto", strConocimiento_ofimaticaProcText);
                html2 = html2.Replace("//hojacalculo", strConocimiento_ofimaticaHojCal);
                html2 = html2.Replace("//ProgPres", strConocimiento_ofimaticaProgPres);
                html2 = html2.Replace("//ingles", strConocimiento_IdiomaIngles);
                html2 = html2.Replace("//otroIdiomaDialecto_1", strConocimiento_IdiomaOtro);
                html2 = html2.Replace("//otroIdiomaDialecto_2", strConocimiento_IdiomaOtro);
                html2 = html2.Replace("//quechua", strConocimiento_DialectoQuechua);
            }

            #endregion

            #region Experiencia - General
            html2 = html2.Replace("//experiencia_general", PerfilCab.FirstOrDefault().iAnioExpGeneral.ToString() + " año(s) ");
            #endregion

            #region Experiencia - Especifica
            html2 = html2.Replace("//experiencia_especifica", PerfilCab.FirstOrDefault().iAnioExpEspecifica.ToString() + " año(s) " + (String.IsNullOrEmpty(PerfilCab.FirstOrDefault().strDesExpEspecifica) ? String.Empty : PerfilCab.FirstOrDefault().strDesExpEspecifica));
            #endregion

            #region Experiencia - Sector Publico
            html2 = html2.Replace("//experiencia_sector_publico", PerfilCab.FirstOrDefault().iAnioExpSectorPublico.ToString() + " año(s) ");
            #endregion

            #region Nivel Minimo
            String strNivelMinimo = string.Empty;
            String strN1 = "&nbsp;";
            String strN2 = "&nbsp;";
            String strN3 = "&nbsp;";
            String strN4 = "&nbsp;";
            String strN5 = "&nbsp;";
            String strN6 = "&nbsp;";
            String strN7 = "&nbsp;";
            switch (PerfilCab.FirstOrDefault().iCodNivelMinimo)
            {
                case 1:
                    {
                        strN1 = "X";     
                        break;
                    }
                case 2:
                    {
                        strN2 = "X";
                        break;
                    }
                case 3:
                    {
                        strN3 = "X";
                        break;
                    }
                case 4:
                    {
                        strN4 = "X";
                        break;
                    }
                case 5:
                    {
                        strN5 = "X";
                        break;
                    }
                case 6:
                    {
                        strN6 = "X";
                        break;
                    }
                case 7:
                    {
                        strN7 = "X";
                        break;
                    }
            }


            strNivelMinimo = "<td style='width: 10px; height: 18px;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Practicante profesional&nbsp;</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>" + strN1 + "</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Auxiliar o Asistente</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>" + strN2 + "</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Analista</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>" + strN3 + "</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Especialista</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>" + strN4 + "</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Supervisor / Coordinador&nbsp;</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>" + strN5 + "</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Jefe de &Aacute;rea o Departamento</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>" + strN6 + "</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 139px; height: 18px; font-size: 12px; text-align: center;'>Gerente&nbsp;o Director</td>" +
                        "<td style='width: 80px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>" + strN7 + "</td>" +
                        "<td style='width: 30px; height: 18px; font-size: 12px;'>&nbsp;</td>";

            html2 = html2.Replace("//nivelMinimo", strNivelMinimo);
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
                        strHab += item.Cualidad.strNombre.Replace("\r\n", String.Empty);
                    else
                        strHab += ", " + item.Cualidad.strNombre.Replace("\r\n", String.Empty);
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
                        strComp += item.Cualidad.strNombre;
                    else
                        strComp += ", " + item.Cualidad.strNombre;
                }
            }
            if (strHab == string.Empty)
                strHabilidadesCompetencias = strComp;
            else
            {
                if (strComp == string.Empty)
                    strHabilidadesCompetencias = strHab;
                else
                    strHabilidadesCompetencias = strHab + ", " + strComp;
            }
            html2 = html2.Replace("//habilidades_competencias", strHabilidadesCompetencias);
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

            html2 = html2.Replace("//requisitos_adicionales", strRequisitosAdicionales);
            #endregion


            SelectPdf.PdfDocument doc2 = converter.ConvertHtmlString(html2, Server.MapPath("~/temp"));
            foreach (PdfPage page in doc2.Pages)
                doc.AddPage(page);

            MemoryStream pdfStream = new MemoryStream();
            doc.Save(pdfStream);
            pdfStream.Position = 0;
            doc.Close();

            return pdfStream;
        }

        private Stream GenerarFichaRequerimientoPdf(IEnumerable<PerfilPuestoRegistro> PerfilCab)
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

            string html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "PerfilPuesto/perfil_anexo1.html"));

            #region Cabecera
            html = html.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "PerfilPuesto/logo_midis.png"));
            html = html.Replace("//organo", PerfilCab.FirstOrDefault().strOrgano);
            html = html.Replace("//unidad_organica", PerfilCab.FirstOrDefault().strUnidadOrganica);
            html = html.Replace("//cesado", PerfilCab.FirstOrDefault().strTrabajadorCese);
            html = html.Replace("//fechacese", (PerfilCab.FirstOrDefault().datFechaCese.HasValue ? PerfilCab.FirstOrDefault().datFechaCese.Value.ToString("dd/MM/yyyy") : String.Empty));
            html = html.Replace("//posiciones", PerfilCab.FirstOrDefault().iPosiciones.ToString());
            html = html.Replace("//periodo", PerfilCab.FirstOrDefault().strPeriodo);
            html = html.Replace("//meta", PerfilCab.FirstOrDefault().strMeta);
            html = html.Replace("//justificacion", PerfilCab.FirstOrDefault().strJustificacion);

            if (PerfilCab.FirstOrDefault().iTipoReq == 2) {
                html = html.Replace("//nombrepuesto1", PerfilCab.FirstOrDefault().strNombrePuesto);
                html = html.Replace("//remuneracion1", PerfilCab.FirstOrDefault().strRemuneracion);
                html = html.Replace("//nombrepuesto2", String.Empty);
                html = html.Replace("//remuneracion2", String.Empty);
            }
            if (PerfilCab.FirstOrDefault().iTipoReq == 1)
            {
                html = html.Replace("//nombrepuesto2", PerfilCab.FirstOrDefault().strNombrePuesto);
                html = html.Replace("//remuneracion2", PerfilCab.FirstOrDefault().strRemuneracion);
                html = html.Replace("//nombrepuesto1", String.Empty);
                html = html.Replace("//remuneracion1", String.Empty);
            }

            html = html.Replace("//nombrepuesto", PerfilCab.FirstOrDefault().strNombrePuesto);
            html = html.Replace("//remuneracion", PerfilCab.FirstOrDefault().strRemuneracion);
            #endregion

            #region Tipo Servicio
            String strTipoServicio = string.Empty;
            String strN1 = "&nbsp;";
            String strN2 = "&nbsp;";
            String strN3 = "&nbsp;";
            switch (PerfilCab.FirstOrDefault().iTipoServicio) {
                case 1:
                    {
                        strN1 = "X";
                        break;
                    }
                case 2:
                    {
                        strN2 = "X";
                        break;
                    }
                case 3:
                    {
                        strN3 = "X";
                        break;
                    }
            }

            strTipoServicio = "<table><tr><td style='width: 10px; height: 18px;'>&nbsp;</td>" +
                        "<td style='width: 100px; height: 18px; font-size: 12px; text-align: center;'>Presencial&nbsp;</td>" +
                        "<td style='width: 40px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>" + strN1 + "</td>" +
                        "<td style='width: 10px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 100px; height: 18px; font-size: 12px; text-align: center;'>Remoto</td>" +
                        "<td style='width: 40px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>" + strN2 + "</td>" +
                        "<td style='width: 10px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 100px; height: 18px; font-size: 12px; text-align: center;'>Mixto</td>" +
                        "<td style='width: 40px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>" + strN3 + "</td>" +
                        //"<td style='width: 30px; height: 18px; font-size: 12px; text-align: center;'>&nbsp;</td>" +
                        "<td style='width: 10px; height: 18px; font-size: 12px;'>&nbsp;</td></tr></table>";

            html = html.Replace("//tipo_servicio", strTipoServicio);
            #endregion

            #region Evaluaciones
            String strTipoExamen = string.Empty;
            String strE1 = "&nbsp;";
            String strE2 = "&nbsp;";
            if (PerfilCab.FirstOrDefault().iConocimiento == 1) strE1 = "X";
            if (PerfilCab.FirstOrDefault().iPsicologico == 1) strE2 = "X";
            
            strTipoExamen = "<table><tr><td style='width: 10px; height: 18px;'>&nbsp;</td>" +
                        "<td style='width: 40px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>" + strE1 + "</td>" +
                        "<td style='width: 250px; height: 18px; font-size: 12px; text-align: left;'>Evaluación de Conocimiento</td>" +
                        "</tr><tr>" +
                        "<td style='width: 10px; height: 18px; font-size: 12px;'>&nbsp;</td>" +
                        "<td style='width: 40px; height: 18px; font-size: 12px; border: 1pt solid black; text-align: center;'>" + strE2 + "</td>" +
                        "<td style='width: 250px; height: 18px; font-size: 12px; text-align: left;'>Evalaución Psicológica</td>" +
                        "</tr></table>";
            //"<td style='width: 10px; height: 18px; font-size: 12px;'>&nbsp;</td></tr></table>";

            html = html.Replace("//evaluaciones", strTipoExamen);
            #endregion

            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(html, Server.MapPath("~/temp"));
            //foreach (PdfPage page in doc2.Pages)
            //    doc.AddPage(page);

            MemoryStream pdfStream = new MemoryStream();
            doc.Save(pdfStream);
            pdfStream.Position = 0;
            doc.Close();

            return pdfStream;
        }

        [HttpPost]
        [Authorize]
        public JsonResult RegistrarPerfilArchivo(PerfilPuestoRegistro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                String nameFile = String.Empty;
                for (Int32 j = 0; j < registro.formatos.ToList().Count; j++)
                {
                    HttpPostedFileBase postfile = ((HttpPostedFileBase[])(registro.formatos.ToList())[j])[0];
                    if (postfile.ContentLength > 0)
                    {
                        nameFile = postfile.FileName;

                        Stream str = postfile.InputStream;
                        BinaryReader Br = new BinaryReader(str);
                        Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                        registro.archivo = FileDet;
                    }
                }

                object respuesta = _perfil_Puesto_Servicio.RegistrarPerfilArchivo(registro);
                //EmpleadoContrato_Registro objContrato = _contrato_Servicio.ObtenerParaEditar(new Contrato_Request() { IdContrato = registro.IdContrato, Nombre = "", Estado = -1 });
                //PostulanteInformacion_Registro obj = _postulante_Servicio.ObtenerPostulanteFicha(new PostulanteInformacion_Registro() { IdPostulante = objContrato.IdPostulante, IdPostulacion = objContrato.IdPostulacion, IdConvocatoria = objContrato.IdConvocatoria, Nombre = objContrato.Nombre });
                //this.SendEmail(obj, "3");

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [AllowAnonymous]
        public ActionResult DescargarArchivo(string id)
        {
            PerfilPuestoRegistro peticion = new PerfilPuestoRegistro();
            peticion.iCodPerfil = Int32.Parse(id);
            //peticion.Nombre = String.Empty;
            //peticion.Estado = -1;

            var lista = _perfil_Puesto_Servicio.ObtenerPerfilesPuestoPorID(id).Select(p => new { p.iCodPerfil, p.archivo });
            var item = lista.Where(x => x.iCodPerfil == peticion.iCodPerfil).SingleOrDefault();

            return File(item.archivo, "application/pdf", "PerfilConvocatoria" + ".pdf");
        }


        [HttpPost]
        [Authorize]
        public JsonResult AsignarNuevaSolicitudPerfilPuesto(PerfilPuestoRegistro registro)
        {
            try
            {
                registro.iTipoPerfil = 1; // TIPO DE PERFIL CAS
                registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;
                registro.iCodPerfil = _perfil_Puesto_Servicio.InsertarCab(registro);
                
                Empleado_Registro objempleado = _empleado_Servicio.ObtenerParaEditar(new Empleado_Request() { IdEmpleado = registro.IdUsuarioAsignado });
                String enlace = HttpUtility.UrlEncode(new Crypto().Encriptar(registro.iCodPerfil.ToString() + "|" + objempleado.IdEmpleado.ToString() + "|" + String.Format("{0} {1} {2}", objempleado.Nombre, objempleado.Paterno, objempleado.Materno)));

                SendEmailAsignarNuevaSolicitud(objempleado, registro, enlace);

                return Json(new { success = "True", responseText = "1" });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult SolicitarEvaluacionPerfilAnexo1(PerfilPuestoRegistro registro)
        {
            try
            {
                registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;
                registro.iCodPerfil = _perfil_Puesto_Servicio.InsertarCab(registro);

                Empleado_Registro objempleado = _empleado_Servicio.ObtenerParaEditar(new Empleado_Request() { IdEmpleado = registro.IdUsuarioAsignado });
                String enlace = HttpUtility.UrlEncode(new Crypto().Encriptar(registro.iCodPerfil.ToString() + "|" + objempleado.IdEmpleado.ToString() + "|" + String.Format("{0} {1} {2}", objempleado.Nombre, objempleado.Paterno, objempleado.Materno)));

                SendEmailSolicitarEvaluacionAnexo1(registro);

                return Json(new { success = "True", responseText = "1" });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [Authorize]
        private Boolean SendEmailAsignarNuevaSolicitud(Empleado_Registro trabajador, PerfilPuestoRegistro perfil, String enlace)
        {
            Boolean exitoEnvio = false;
            String html = String.Empty;

            html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Correo/EnvioAsignarNuevoPerfil.txt"));
            //html = html.Replace("_TRABAJADOR_", trabajador.Nombre);
            html = html.Replace("_NOMBREPERFIL_", perfil.strNombrePuesto);
            //html = html.Replace("_CLAVE_", HttpUtility.UrlEncode(new Crypto().Encriptar(trabajador.IdEmpleado + "|" + convocatoria.IdConvocatoria)));

            //window.location.href = controladorApp.obtenerRutaBase() + "Perfiles/ActualizarPerfil?id=" + id;
            html = html.Replace("_URLREGISTRO_", ConfigurationManager.AppSettings["SERVER_PATH"].ToString() + "Perfiles/ActualizarPerfilxUsuario/?id=" + enlace);

            if (!String.IsNullOrEmpty(trabajador.CorreoElectronicoLaboral))
            {
                MailMessage msg = new MailMessage();
                msg.To.Add(new MailAddress(trabajador.CorreoElectronicoLaboral));
                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CCcorreoCAS"].ToString()))
                    msg.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["CCcorreoCAS"].ToString()));

                msg.From = new MailAddress(ConfigurationManager.AppSettings["correo"].ToString());
                msg.Subject = String.Format("{0} {1}-MIDIS", "Registro de Perfil de Puesto CAS: ", perfil.strNombrePuesto);
                msg.Body = html;
                msg.IsBodyHtml = true;

                SmtpClient clienteSmtp = new SmtpClient(ConfigurationManager.AppSettings["host"].ToString());
                clienteSmtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["usuarioemail"].ToString(), ConfigurationManager.AppSettings["contraseniaemail"].ToString());
                try
                {
                    clienteSmtp.Send(msg);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    Console.ReadLine();
                }

                exitoEnvio = true;
            }


            return exitoEnvio;
        }

        [AllowAnonymous]
        private Boolean SendEmailSolicitarEvaluacionAnexo1(PerfilPuestoRegistro perfil)
        {
            Boolean exitoEnvio = false;
            String html = String.Empty;

            html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Correo/EnvioSolicitarAprobarPerfil.txt"));
            //html = html.Replace("_TRABAJADOR_", trabajador.Nombre);
            html = html.Replace("_NOMBREPERFIL_", perfil.strNombrePuesto);
            //html = html.Replace("_CLAVE_", HttpUtility.UrlEncode(new Crypto().Encriptar(trabajador.IdEmpleado + "|" + convocatoria.IdConvocatoria)));

            //window.location.href = controladorApp.obtenerRutaBase() + "Perfiles/ActualizarPerfil?id=" + id;
            html = html.Replace("_URLREGISTRO_", ConfigurationManager.AppSettings["SERVER_PATH"].ToString() + "Perfiles/");

            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(ConfigurationManager.AppSettings["CCcorreoCAS"].ToString()));
                
            msg.From = new MailAddress(ConfigurationManager.AppSettings["correo"].ToString());
            msg.Subject = String.Format("{0} {1}-MIDIS", "Revisión de Perfil de Puesto: ", perfil.strNombrePuesto);
            msg.Body = html;
            msg.IsBodyHtml = true;

            SmtpClient clienteSmtp = new SmtpClient(ConfigurationManager.AppSettings["host"].ToString());
            clienteSmtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["usuarioemail"].ToString(), ConfigurationManager.AppSettings["contraseniaemail"].ToString());
            try
            {
                clienteSmtp.Send(msg);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                Console.ReadLine();
            }

            exitoEnvio = true;
            
            return exitoEnvio;
        }
    }
}
