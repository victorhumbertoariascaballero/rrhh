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
    public class VacacionesController : Controller
    {
        private readonly T_genm_vacaciones_LN _Servicio = new T_genm_vacaciones_LN();
        private readonly T_genm_estadoproceso_LN _Servicio_estado = new T_genm_estadoproceso_LN();
        private readonly T_genm_controlasistencia_LN _controlAsistencia_Servicio = new T_genm_controlasistencia_LN();
        //private readonly T_genm_vacacionesproceso_LN _Servicio_justificacionesProceso = new T_genm_justificacionesproceso_LN();


        private readonly int Jefe = Convert.ToInt32(ConfigurationManager.AppSettings["IdPerfilJefeCtrlAsistencia"].ToString());
        private readonly int Admin = Convert.ToInt32(ConfigurationManager.AppSettings["IdPerfilAdminCtrlAsistencia"].ToString());
        private readonly string PathVacaciones = "~/Files/Vacaciones/";

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
        public JsonResult ListarVacacionesProcesoHistorial(int iCodVacaciones)
        {
            //request.bVigente = true;
            //request.bEstado = true;
            IEnumerable<VacacionesProceso> lista = _Servicio.ListarVacacionesProcesoHistorial(iCodVacaciones);

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public JsonResult ListarVacacionesTrabajador(VacacionesTrabajador_Request request)
        {
            //request.bVigente = true;
            //request.bEstado = true;
            IEnumerable<Vacaciones_Registro> lista = _Servicio.ListarVacacionesTrabajador(request);

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
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
        public JsonResult GrabarVacacionesTrabajador(VacacionesTrabajador_Registro request)
        {
            //ME QUEDE HACIENDO LA PARTE BACK DEL GRABAR VACACIONES
            //request.iCodTrabajador = 38041; //Convert.ToInt32(VariablesWeb.ConsultaInformacion.Persona.iCodTrabajador);
            request.Archivos = new List<VacacionesArchivo>();
            List<HttpPostedFileBase> files = request.filesUpload;
            HttpPostedFileBase fileFormato = request.filesUploadFormato;
            string path = Server.MapPath(PathVacaciones);
            if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }

            if (files != null)
            {
                foreach (var file in files)
                {
                    AgregarFichero(request, path, file, false);
                }
            }
            if (fileFormato != null)
            {
                AgregarFichero(request, path, fileFormato, true);
            }

            VacacionesTrabajador_Registro response;
            request.bEstado = true;
            if (request.iCodVacaciones > 0)
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
                request.iCodEstadoProceso = (int)EnumMaeEstadoProceso.PENDIENTE;
                request.vAuditCreacion = VariablesWeb.ConsultaInformacion.iCodUsuario.ToString();
                response = _Servicio.Insertar(request);
            }

            return Json(new { success = (response.iCodVacaciones > 0) ? "True" : "False" });
        }

        [HttpGet]
        public ActionResult DescargarArchivo(string fileName)
        {
            string filePath = Server.MapPath(PathVacaciones + fileName);

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

        [HttpPost]
        [Authorize]
        public JsonResult AprobarDenegarVacacionesTrabajador(VacacionesTrabajador_Registro request)
        {
            VacacionesTrabajador_Registro response;
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

            return Json(new { success = (response.iCodVacaciones > 0) ? "True" : "False" });
        }


        [HttpPost]
        [Authorize]
        public JsonResult AprobarDenegarVacacionesTrabajadorMas(VacacionesTrabajador_Registro request)
        {
            VacacionesTrabajador_Registro response;
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

        private static void AgregarFichero(VacacionesTrabajador_Registro request, string path, HttpPostedFileBase file, bool considerarTipo)
        {
            string fnameResult = string.Empty;

            string archivoPDF = file.FileName.ToString();
            string nombreCodigo = "Vacas_" + Guid.NewGuid().ToString() + "_" + file.FileName.ToString();
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
                    nvoNombreCodigo = "Vacas_" + Guid.NewGuid().ToString() + "_" + NvoNombre;
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

            var iCodTipoVacacionesFormato = 1;
            if (considerarTipo)
            {
                if ((bool)request.bFraccionamientoVacacionalMediaJornada)
                    iCodTipoVacacionesFormato = 3;
                else if (request.iCodTipoVacaciones == 2)
                    iCodTipoVacacionesFormato = 2;
            }

            request.Archivos.Add(new VacacionesArchivo()
            {
                vUrlArchivo = fnameResult,
                bEstado = true,
                vObservaciones = string.Empty,
                iCodTipoVacacionesFormato = iCodTipoVacacionesFormato,
                vAuditCreacion = VariablesWeb.ConsultaInformacion.iCodUsuario.ToString()
            });
        }

        [HttpGet]
        [Authorize]
        public JsonResult ListarVacacionesPeriodo(VacacionesPeriodo_Registro request)
        {
            object respuesta = _Servicio.ListarVacacionesPeriodo(request);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerVacacionesPorId(int iCodVacaciones)
        {
            object respuesta = _Servicio.ObtenerVacacionesPorId(iCodVacaciones);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
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
    }
}