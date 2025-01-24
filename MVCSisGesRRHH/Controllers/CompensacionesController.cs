using MIDIS.ORI.Entidades;
using MIDIS.ORI.Entidades.Core;
using MIDIS.SEG.LogicaNegocio;
using MIDIS.Utiles;
using MVCSisGesRRHH.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSisGesRRHH.Controllers
{
    public class CompensacionesController : Controller
    {
        private readonly T_genm_compensaciones_LN _Servicio = new T_genm_compensaciones_LN();
        private readonly T_genm_estadoproceso_LN _Servicio_estado = new T_genm_estadoproceso_LN();
        //private readonly T_genm_justificacionesproceso_LN _Servicio_justificacionesProceso = new T_genm_justificacionesproceso_LN();
        private readonly T_genm_controlasistencia_LN _controlAsistencia_Servicio = new T_genm_controlasistencia_LN();
        private readonly T_genm_Turnos_LN _ServicioTurnos = new T_genm_Turnos_LN();


        private readonly int Jefe = Convert.ToInt32(ConfigurationManager.AppSettings["IdPerfilJefeCtrlAsistencia"].ToString());
        private readonly int Admin = Convert.ToInt32(ConfigurationManager.AppSettings["IdPerfilAdminCtrlAsistencia"].ToString());

        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            ControlAsistenciaModel oModel = new ControlAsistenciaModel();
            //var oUsuario = VariablesWeb.ConsultaInformacion.Persona;
            oModel.Empleado = _controlAsistencia_Servicio.ObtenerParaEditar(new Empleado_Request() { IdEmpleado = Convert.ToInt32(VariablesWeb.ConsultaInformacion.Persona.iCodTrabajador) });
            return View(oModel);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Horas()
        {
            ControlAsistenciaModel oModel = new ControlAsistenciaModel();
            //var oUsuario = VariablesWeb.ConsultaInformacion.Persona;
            oModel.Empleado = _controlAsistencia_Servicio.ObtenerParaEditar(new Empleado_Request() { IdEmpleado = Convert.ToInt32(VariablesWeb.ConsultaInformacion.Persona.iCodTrabajador) });
            return View("horas", oModel);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Compensar()
        {
            ControlAsistenciaModel oModel = new ControlAsistenciaModel();
            //var oUsuario = VariablesWeb.ConsultaInformacion.Persona;
            oModel.Empleado = _controlAsistencia_Servicio.ObtenerParaEditar(new Empleado_Request() { IdEmpleado = Convert.ToInt32(VariablesWeb.ConsultaInformacion.Persona.iCodTrabajador) });
            oModel.TurnosDiasSemana = _ServicioTurnos.ObtenerTurnoDiaSenamaTrabajadorVigente(new TurnoDiaSemana_Request() { iCodTrabajador = Convert.ToInt32(VariablesWeb.ConsultaInformacion.Persona.iCodTrabajador) });

            return View("compensar", oModel);
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
        public JsonResult ListarCompensacionesProcesoHistorial(int iCodCompensaciones)
        {
            //request.bVigente = true;
            //request.bEstado = true;
            IEnumerable<CompensacionesProceso> lista = _Servicio.ListarCompensacionesProcesoHistorial(iCodCompensaciones);

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public JsonResult ListarCompensaciones(CompensacionesTrabajador_Request request)
        {
            //request.bVigente = true;
            //request.bEstado = true;
            IEnumerable<Compensaciones_Registro> lista = _Servicio.ListarCompensaciones(request);

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public JsonResult ListarCompensacionesAptas(CompensacionesTrabajador_Request request)
        {
            //request.bVigente = true;
            //request.bEstado = true;
            IEnumerable<Compensaciones_Registro> lista = _Servicio.ListarCompensacionesAptas(request);

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Authorize]
        public JsonResult ListarCalendarAptas(CompensacionesTrabajador_Request request)
        {
            //request.bVigente = true;
            //request.bEstado = true;
            IEnumerable<CompensacionesCalendarioTrabajador_Registro> lista = _Servicio.ListarCompensacionesCalendarAptas(request);

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ObtenerCompensacionPorId(int iCodCompensaciones)
        {
            object respuesta = _Servicio.ObtenerCompensacionPorId(iCodCompensaciones);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarEstadoProceso()
        {
            object respuesta = _Servicio_estado.ListarEstadoProceso();
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //public JsonResult ObtenerJustificacionPorId(int iCodJustificaciones)
        //{
        //    object respuesta = _Servicio.ObtenerJustificacionPorId(iCodJustificaciones);
        //    return Json(respuesta, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        [Authorize]
        public JsonResult GrabarCompensacionTrabajador(CompensacionesTrabajador_Registro request)
        {
            CompensacionesTrabajador_Registro response;
            request.BEstado = true;
            if (request.ICodCompensaciones > 0)
            {
                request.VAuditModificacion = VariablesWeb.ConsultaInformacion.iCodUsuario.ToString();
                if (request.ICodEstadoProceso == (int)EnumMaeEstadoProceso.RECHAZADO_POR_JEFE)
                {
                    request.ICodEstadoProceso = (int)EnumMaeEstadoProceso.SUBSANADO;
                }
                else if (request.ICodEstadoProceso == (int)EnumMaeEstadoProceso.RECHAZADO_POR_ADMINISTRADOR)
                {
                    request.ICodEstadoProceso = (int)EnumMaeEstadoProceso.SUBSANADO_PARA_ADMINISTRADOR;
                }

                response = _Servicio.ActualizarCompensacion(request);
            }
            else
            {
                request.VAuditCreacion = VariablesWeb.ConsultaInformacion.iCodUsuario.ToString();
                response = _Servicio.InsertarCompensacion(request);
            }

            return Json(new { success = (response.ICodCompensaciones > 0) ? "True" : "False" });
        }


        [HttpPost]
        [Authorize]
        public JsonResult AprobarDenegarCompensacionTrabajador(CompensacionesTrabajador_Registro request)
        {
            CompensacionesTrabajador_Registro response;
            request.BEstado = true;
            request.VAuditModificacion = VariablesWeb.ConsultaInformacion.iCodUsuario.ToString();
            if (Jefe == VariablesWeb.ConsultaInformacion.Perfil[0].iCodPerfil)
            {
                request.adminJefe = false;
                if (request.aprobarDenegar)
                    response = _Servicio.Aprobar(request);
                else
                    response = _Servicio.Denegar(request);
            }
            else
            {
                request.adminJefe = true;
                if (request.aprobarDenegar)
                    response = _Servicio.Aprobar(request);
                else
                    response = _Servicio.Denegar(request);
            }

            return Json(new { success = (response.ICodCompensaciones > 0) ? "True" : "False" });
        }

        [HttpPost]
        [Authorize]
        public JsonResult AprobarDenegarCompensacionTrabajadorMas(CompensacionesTrabajador_Registro request)
        {
            CompensacionesTrabajador_Registro response;
            request.BEstado = true;
            request.VAuditModificacion = VariablesWeb.ConsultaInformacion.iCodUsuario.ToString();
            if (Jefe == VariablesWeb.ConsultaInformacion.Perfil[0].iCodPerfil)
            {
                request.adminJefe = false;
                if (request.aprobarDenegar)
                    response = _Servicio.AprobarMas(request);
                else
                    response = _Servicio.DenegarMas(request);
            }
            else
            {
                request.adminJefe = true;
                if (request.aprobarDenegar)
                    response = _Servicio.AprobarMas(request);
                else
                    response = _Servicio.DenegarMas(request);
            }

            return Json(new { success = (response.resMasivo) ? "True" : "False" });
        }

        [HttpPost]
        [Authorize]
        public JsonResult GrabarDetalleCompensacionTrabajador(List<CompensacionesDetalleTrabajador_Request> request)
        {
            foreach (var item in request)
            {
                item.VAuditCreacion = VariablesWeb.ConsultaInformacion.iCodUsuario.ToString();
                item.BEstado = true;
            }

            bool bolSuccess = _Servicio.InsertarCompensacionDetalle(request);
            return Json(new { success = bolSuccess ? "True" : "False" });
        }

        [HttpGet]
        public JsonResult ObtenerTotalHorasPorCompensacion(int iCodCompensaciones)
        {
            object respuesta = _Servicio.ObtenerTotalHorasPorCompensacion(iCodCompensaciones);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public JsonResult GetMinutosXFecha(CompensacionesTrabajador_Request request)
        {
            decimal respuesta = 0;
            foreach (var item in request.dFechas)
            {
                request.dFecha = Convert.ToDateTime(item);
                respuesta += _Servicio.GetMinutosXFecha(request);
            }

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public JsonResult ListarCompensacionesDetalle(CompensacionesDetalleTrabajador_Request request)
        {
            //request.bVigente = true;
            //request.bEstado = true;
            IEnumerable<CompensacionesDetalleTrabajador_Request> lista = _Servicio.ListarCompensacionesDetalle(request);

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public JsonResult ListarCompensacionesDetalleProcesoHistorial(int iCodCompensacionesDetalle)
        {
            //request.bVigente = true;
            //request.bEstado = true;
            IEnumerable<CompensacionesDetalleProceso> lista = _Servicio.ListarCompensacionesDetalleProcesoHistorial(iCodCompensacionesDetalle);

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public JsonResult AprobarDenegarCompensacionDetalleTrabajador(CompensacionesDetalleTrabajador_Request request)
        {
            CompensacionesDetalleTrabajador_Request response;
            request.BEstado = true;
            request.VAuditModificacion = VariablesWeb.ConsultaInformacion.iCodUsuario.ToString();
            if (Jefe == VariablesWeb.ConsultaInformacion.Perfil[0].iCodPerfil)
            {
                request.adminJefe = false;
                if (request.aprobarDenegar)
                    response = _Servicio.AprobarDetalle(request);
                else
                    response = _Servicio.DenegarDetalle(request);
            }
            else
            {
                request.adminJefe = true;
                if (request.aprobarDenegar)
                    response = _Servicio.AprobarDetalle(request);
                else
                    response = _Servicio.DenegarDetalle(request);
            }

            return Json(new { success = (response.ICodCompensacionesDetalle > 0) ? "True" : "False" });
        }

        [HttpPost]
        [Authorize]
        public JsonResult AprobarDenegarCompensacionDetalleTrabajadorMas(CompensacionesDetalleTrabajador_Request request)
        {
            CompensacionesDetalleTrabajador_Request response;
            request.BEstado = true;
            request.VAuditModificacion = VariablesWeb.ConsultaInformacion.iCodUsuario.ToString();
            if (Jefe == VariablesWeb.ConsultaInformacion.Perfil[0].iCodPerfil)
            {
                request.adminJefe = false;
                if (request.aprobarDenegar)
                    response = _Servicio.AprobarDetalleMas(request);
                else
                    response = _Servicio.DenegarDetalleMas(request);
            }
            else
            {
                request.adminJefe = true;
                if (request.aprobarDenegar)
                    response = _Servicio.AprobarDetalleMas(request);
                else
                    response = _Servicio.DenegarDetalleMas(request);
            }

            return Json(new { success = (response.resMasivo) ? "True" : "False" });
        }
    }
}