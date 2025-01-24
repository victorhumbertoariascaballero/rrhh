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
    /*
     String PERFIL_JEFE_CTRL_ASISTENCIA = ConfigurationManager.AppSettings["IdPerfilJefeCtrlAsistencia"]; //182
    String PERFIL_EMPLE_CTRL_ASISTENCIA = ConfigurationManager.AppSettings["IdPerfilEmpleadoCtrlAsistencia"]; //183
    String PERFIL_ADMIN_CTRL_ASISTENCIA = ConfigurationManager.AppSettings["IdPerfilAdminCtrlAsistencia"]; //184
     */
    public class JustificacionesController : Controller
    {
        private readonly T_genm_justificaciones_LN _Servicio = new T_genm_justificaciones_LN();
        private readonly T_genm_motivojustificacion_LN _Servicio_motivo = new T_genm_motivojustificacion_LN();
        private readonly T_genm_TipoJustificacion_LN _Servicio_tipojusti = new T_genm_TipoJustificacion_LN();
        private readonly T_genm_estadoproceso_LN _Servicio_estado = new T_genm_estadoproceso_LN();
        private readonly T_genm_justificacionesproceso_LN _Servicio_justificacionesProceso = new T_genm_justificacionesproceso_LN();


        private readonly int Jefe = Convert.ToInt32(ConfigurationManager.AppSettings["IdPerfilJefeCtrlAsistencia"].ToString());
        private readonly int Admin = Convert.ToInt32(ConfigurationManager.AppSettings["IdPerfilAdminCtrlAsistencia"].ToString());

        private readonly T_genm_tipogoce_LN _Servicio_tipogoce = new T_genm_tipogoce_LN();
        private readonly T_genm_controlasistencia_LN _controlAsistencia_Servicio = new T_genm_controlasistencia_LN();
        private readonly string PathJustificaciones = "~/Files/Justificaciones/";

        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            ControlAsistenciaModel oModel = new ControlAsistenciaModel();
            //var oUsuario = VariablesWeb.ConsultaInformacion.Persona;
            oModel.Empleado = _controlAsistencia_Servicio.ObtenerParaEditar(new Empleado_Request() { IdEmpleado = Convert.ToInt32(VariablesWeb.ConsultaInformacion.Persona.iCodTrabajador) });
            return View(oModel);
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
        public JsonResult ListarJustificacionProcesoHistorial(int iCodJustificaciones)
        {
            //request.bVigente = true;
            //request.bEstado = true;
            IEnumerable<JustificacionesProceso_Registro> lista = _Servicio_justificacionesProceso.ListarJustificacionProcesoHistorial(iCodJustificaciones);

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public JsonResult ListarJustificacionesTrabajador(JustificacionesTrabajador_Request request)
        {
            //request.bVigente = true;
            //request.bEstado = true;
            IEnumerable<Justificaciones_Registro> lista = _Servicio.ListarJustificacionesTrabajador(request);

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public JsonResult ListarMotivoJustificacion(MotivoJustificacion_Request request)
        {
            //request.bVigente = true;
            request.bEstado = true;
            IEnumerable<MotivoJustificacion_Registro> lista = _Servicio_motivo.ListarMotivoJustificacion(request);

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public JsonResult ListarTipoJustificacion()
        {
            //request.bVigente = true;

            IEnumerable<TipoJustificacion_Registro> lista = _Servicio_tipojusti.ListarTipoJustificacion();

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarEstadoProceso()
        {
            object respuesta = _Servicio_estado.ListarEstadoProceso();
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarTipoGoce()
        {
            object respuesta = _Servicio_tipogoce.ListarTipoGoce();
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerJustificacionPorId(int iCodJustificaciones)
        {
            object respuesta = _Servicio.ObtenerJustificacionPorId(iCodJustificaciones);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public JsonResult GrabarJustificacionTrabajador(JustificacionesTrabajador_Registro request)
        {
            //request.iCodTrabajador = 38041; //Convert.ToInt32(VariablesWeb.ConsultaInformacion.Persona.iCodTrabajador);
            request.Archivos = new List<JustificacionArchivo>();

            List<HttpPostedFileBase> files = request.filesUpload;
            string path = Server.MapPath(PathJustificaciones);
            if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }

            if (files != null)
            {
                foreach (var file in files)
                {
                    AgregarFichero(request, path, file);
                }
            }

            JustificacionesTrabajador_Registro response;
            request.bEstado = true;
            if (request.iCodJustificaciones > 0)
            {
                request.vAuditModificacion = VariablesWeb.ConsultaInformacion.iCodUsuario.ToString();
                if (request.iCodEstadoProceso == (int)EnumMaeEstadoProceso.RECHAZADO_POR_JEFE)
                {
                    request.iCodEstadoProceso = (int)EnumMaeEstadoProceso.SUBSANADO;
                }
                else if (request.iCodEstadoProceso == (int)EnumMaeEstadoProceso.RECHAZADO_POR_ADMINISTRADOR)
                {
                    request.iCodEstadoProceso = (int)EnumMaeEstadoProceso.SUBSANADO;
                }
                
                response = _Servicio.Actualizar(request);
            }
            else
            {
                request.vAuditCreacion = VariablesWeb.ConsultaInformacion.iCodUsuario.ToString();
                response = _Servicio.Insertar(request);
            }            

            return Json(new { success = (response.iCodJustificaciones > 0) ? "True" : "False" });
        }


        [HttpPost]
        [Authorize]
        public JsonResult AprobarDenegarJustificacionTrabajador(JustificacionesTrabajador_Registro request)
        {
            JustificacionesTrabajador_Registro response;
            request.bEstado = true;
            request.vAuditModificacion = VariablesWeb.ConsultaInformacion.iCodUsuario.ToString();
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

            return Json(new { success = (response.iCodJustificaciones > 0) ? "True" : "False" });
        }

        [HttpPost]
        [Authorize]
        public JsonResult AprobarDenegarJustificacionTrabajadorMas(JustificacionesTrabajador_Registro request)
        {
            JustificacionesTrabajador_Registro response;
            request.bEstado = true;
            request.vAuditModificacion = VariablesWeb.ConsultaInformacion.iCodUsuario.ToString();
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

        [HttpGet]
        public ActionResult DescargarArchivo(string fileName)
        {
            string filePath = Server.MapPath(PathJustificaciones + fileName);

            if (System.IO.File.Exists(filePath))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

                var fileExtension = Path.GetExtension(fileName);
                string mimeType = GetMimeType(fileExtension);

                string fileNameOnly = Path.GetFileName(fileName);

                return File(fileBytes, mimeType, fileNameOnly);
            }
            else
            {
                return Content("El archivo no fue  encontrado", "text/plain");
            }
        }

        [HttpGet]
        [Authorize]
        public JsonResult GetEmpleado(int iCodTrabajador)
        {
            ControlAsistenciaModel oModel = new ControlAsistenciaModel();
            //var oUsuario = VariablesWeb.ConsultaInformacion.Persona;
            oModel.Empleado = _controlAsistencia_Servicio.ObtenerParaEditar(new Empleado_Request() { IdEmpleado = iCodTrabajador });


            return Json(oModel.Empleado, JsonRequestBehavior.AllowGet);
        }
        private string GetMimeType(string fileExtension)
        {
            switch (fileExtension.ToLower())
            {
                case ".pdf": return "application/pdf";
                case ".jpg": return "image/jpeg";
                case ".jpeg": return "image/jpeg";
                case ".png": return "image/png";
                case ".txt": return "text/plain";
                case ".csv": return "text/csv";
                default: return "application/octet-stream";
            }
        }

        private static void AgregarFichero(JustificacionesTrabajador_Registro request, string path, HttpPostedFileBase file)
        {
            string fnameResult = string.Empty;

            string archivoPDF = file.FileName.ToString();
            string nombreCodigo = "Justi_" + Guid.NewGuid().ToString() + "_" + file.FileName.ToString();
            string nomArchivoRuta = path + nombreCodigo;

            /* VALIDA SI EXISTE */
            if (System.IO.File.Exists(nomArchivoRuta))
            {
                string arch = Path.GetFileName(file.FileName);
                string ext = Path.GetExtension(arch);
                string NvoNombre = arch;  //nombre + ext;
                string nombreSolo = Path.GetFileNameWithoutExtension(archivoPDF);
                string nvoNombreCodigo = "";
                int indice = 1;
                while (true)
                {
                    NvoNombre = nombreSolo + indice.ToString() + ext;
                    nvoNombreCodigo = "Justi_" + Guid.NewGuid().ToString() + "_" + NvoNombre;
                    if (System.IO.File.Exists(path + nvoNombreCodigo))
                        indice = indice + 1;
                    else
                        break;
                }

                nomArchivoRuta = Path.Combine(path, nvoNombreCodigo);
                fnameResult = nvoNombreCodigo;
            }
            else
            {
                fnameResult = nombreCodigo;
            }

            file.SaveAs(nomArchivoRuta);

            request.Archivos.Add(new JustificacionArchivo()
            {
                vUrlArchivo = fnameResult,
                bEstado = true,
                vObservaciones = string.Empty,
                vAuditCreacion = VariablesWeb.ConsultaInformacion.iCodUsuario.ToString()
            });
        }



    }
}