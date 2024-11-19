using MIDIS.ORI.Entidades;
using MIDIS.ORI.LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using MVCSisGesRRHH.Models;
using System.Text;
using System.IO.Compression;
using CrystalDecisions.CrystalReports.Engine;
using Xceed.Words.NET;
using Xceed.Document.NET;
using MIDIS.Utiles;
using MIDIS.UtilesMVC.Filtros;
using SelectPdf;

namespace MVCSisGesRRHH.Controllers
{
    public class ConvocatoriaServirController: Controller
	{
        private readonly T_genm_empleado_LN _empleado_Servicio = new T_genm_empleado_LN();
        private readonly T_genm_convocatoria_LN _convocatoria_Servicio = new T_genm_convocatoria_LN();
        private readonly T_genm_postulante_LN _postulante_Servicio = new T_genm_postulante_LN();

        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            return View();
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
        public JsonResult ListarConvocatorias(Convocatoria_Request peticion)
        {
            //peticion.Nombre = "%" + (String.IsNullOrEmpty(peticion.Nombre) ? "" : peticion.Nombre.ToUpper()) + "%";
            IEnumerable<Convocatoria_Registro> lista = _convocatoria_Servicio.ListarConvocatoria(peticion);
            foreach (Convocatoria_Registro obj in lista) {
                obj.Contrasena = HttpUtility.UrlEncode(new Crypto().Encriptar(obj.IdConvocatoria + "|" + VariablesWeb.ConsultaInformacion.Persona.iCodTrabajador.ToString() + "|" + String.Format("{0} {1} {2}", VariablesWeb.ConsultaInformacion.Persona.vNombres, VariablesWeb.ConsultaInformacion.Persona.vApePaterno, VariablesWeb.ConsultaInformacion.Persona.vApeMaterno)));
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarPostulantes(Convocatoria_Request peticion)
        {
            //peticion.Nombre = "%" + (String.IsNullOrEmpty(peticion.Nombre) ? "" : peticion.Nombre.ToUpper()) + "%";
            object respuesta = _convocatoria_Servicio.ListarPostulantes(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarPostulantesPractica(Convocatoria_Request peticion)
        {
            //peticion.Nombre = "%" + (String.IsNullOrEmpty(peticion.Nombre) ? "" : peticion.Nombre.ToUpper()) + "%";
            object respuesta = _convocatoria_Servicio.ListarPostulantesPractica(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarPostulantesEvaluacionCurri(Convocatoria_Request peticion)
        {
            //peticion.Nombre = "%" + (String.IsNullOrEmpty(peticion.Nombre) ? "" : peticion.Nombre.ToUpper()) + "%";
            object respuesta = _convocatoria_Servicio.ListarPostulantesEvaluacionCurri(peticion);
            var jsonResult = Json(respuesta, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = Int32.MaxValue;

            return jsonResult;
        }
        [HttpGet]
        public JsonResult ListarPostulantesPracticaEvaluacionCurri(Convocatoria_Request peticion)
        {
            //peticion.Nombre = "%" + (String.IsNullOrEmpty(peticion.Nombre) ? "" : peticion.Nombre.ToUpper()) + "%";
            object respuesta = _convocatoria_Servicio.ListarPostulantesPracticaEvaluacionCurri(peticion);
            var jsonResult = Json(respuesta, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = Int32.MaxValue;

            return jsonResult;
        }
        [HttpGet]
        public JsonResult ListarPostulantesEvaluacionConocimiento(Convocatoria_Request peticion)
        {
            //peticion.Nombre = "%" + (String.IsNullOrEmpty(peticion.Nombre) ? "" : peticion.Nombre.ToUpper()) + "%";
            object respuesta = _convocatoria_Servicio.ListarPostulantesEvaluacionConocimiento(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarPostulantesEntrevistaPersonal(Convocatoria_Request peticion)
        {
            //peticion.Nombre = "%" + (String.IsNullOrEmpty(peticion.Nombre) ? "" : peticion.Nombre.ToUpper()) + "%";
            object respuesta = _convocatoria_Servicio.ListarPostulantesEntrevistaPersonal(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarPostulantesPracticaEntrevistaPersonal(Convocatoria_Request peticion)
        {
            //peticion.Nombre = "%" + (String.IsNullOrEmpty(peticion.Nombre) ? "" : peticion.Nombre.ToUpper()) + "%";
            object respuesta = _convocatoria_Servicio.ListarPostulantesPracticaEntrevistaPersonal(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarPostulantesResultadosTotales(Convocatoria_Request peticion)
        {
            //peticion.Nombre = "%" + (String.IsNullOrEmpty(peticion.Nombre) ? "" : peticion.Nombre.ToUpper()) + "%";
            object respuesta = _convocatoria_Servicio.ListarPostulantesResultadosTotales(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarPostulantesPracticaResultadosTotales(Convocatoria_Request peticion)
        {
            //peticion.Nombre = "%" + (String.IsNullOrEmpty(peticion.Nombre) ? "" : peticion.Nombre.ToUpper()) + "%";
            object respuesta = _convocatoria_Servicio.ListarPostulantesPracticaResultadosTotales(peticion);
            var jsonResult = Json(respuesta, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = Int32.MaxValue;

            return jsonResult;
        }
        [HttpGet]
        public JsonResult ListarEntrevistaPersonalPreguntas(PostulacionEvaluacionEntrevista_Registro peticion)
        {
            object respuesta = _convocatoria_Servicio.ListarEntrevistaPersonalPreguntas(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult RegistrarEntrevistaPersonalPregunta(PostulacionEntrevistaPersonalPregunta_Registro registro)
        {
            try
            {
                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioRegistro = 1; // (int)VariablesWeb.ConsultaInformacion.iCodUsuario;
                
                object respuesta = _convocatoria_Servicio.RegistrarEntrevistaPersonalPregunta(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult ActualizarEntrevistaPersonalPregunta(PostulacionEntrevistaPersonalPregunta_Registro registro)
        {
            try
            {
                //registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = 1; // (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                object respuesta = _convocatoria_Servicio.ActualizarEntrevistaPersonalPregunta(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        //[HttpPost]
        //public JsonResult EliminarEntrevistaPersonalPregunta(PostulacionEntrevistaPersonalPregunta_Registro registro)
        //{
        //    try
        //    {
        //        //registro.FechaModificacion = DateTime.Now;
        //        registro.IdUsuarioModificacion = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

        //        object respuesta = _empleado_Servicio.EliminarCuentaEmpleado(registro);

        //        return Json(new { success = "True", responseText = respuesta });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = "False", responseText = ex.Message });
        //    }
        //}


        [HttpGet]
        [Authorize]
        public JsonResult ListarEstados()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("0", "--Todos--"));
            lista.Add(new Estado_Response("1", "PENDIENTE"));
            lista.Add(new Estado_Response("2", "EN PROCESO"));
            lista.Add(new Estado_Response("3", "CONCLUIDA"));
            lista.Add(new Estado_Response("5", "CANCELADA"));
            
            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Authorize]
        public JsonResult ListarTipoConvocatoria()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("1", "ABIERTA"));
            lista.Add(new Estado_Response("2", "CERRADA"));
            
            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

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
        [HttpGet]
        public JsonResult ListarConvocatoriaTipoDocumento()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("0", "--Seleccione--"));
            lista.Add(new Estado_Response("20", "AMPLIACIÓN"));
            lista.Add(new Estado_Response("21", "CANCELADO"));
            lista.Add(new Estado_Response("22", "COMUNICADO"));
            lista.Add(new Estado_Response("23", "DESIERTO"));
            lista.Add(new Estado_Response("24", "FE DE ERRATAS"));
            lista.Add(new Estado_Response("25", "PROCESO CAS - REACTIVADO"));
            lista.Add(new Estado_Response("26", "REPROGRAMACIÓN"));
            lista.Add(new Estado_Response("30", "RESULTADO DE EVALUACIÓN CURRICULAR"));
            lista.Add(new Estado_Response("31", "RESULTADO DE EVALUACIÓN DE CONOCIMIENTOS"));
            lista.Add(new Estado_Response("32", "RESULTADO DE EVALUACIÓN PSICOLÓGICO"));
            lista.Add(new Estado_Response("33", "RESULTADO FINAL"));

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarTipoNotificacion()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("0", "--Todos-- (MIEMBROS DEL COMITÉ Y POSTULANTES"));
            lista.Add(new Estado_Response("1", "SÓLO MIEMBROS DEL COMITÉ"));
            lista.Add(new Estado_Response("2", "SÓLO POSTULANTES"));
            
            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize]
        public JsonResult Validar(BoletaCarga_Registro registro)
        {
            registro.IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;
            registro.FechaRegistro = DateTime.Now;
            String respuesta = String.Empty;
            String nameFile = registro.NombreArchivo;
            String[] nombre = nameFile.Split('-');
            if (nombre[1] == registro.Anio && nombre[2] == registro.Mes)
                respuesta = _empleado_Servicio.Validar(registro);
            else
                respuesta = "La boleta laboral del trabajador con Nro documento " + registro.NroDocumento + " no corresponde al periodo seleccionado";
            
            if (String.IsNullOrEmpty(respuesta))
                return Json(new { success = "True" });
            else
                return Json(new { success = "False", responseText = respuesta });
        }
        
        [HttpPost]
        [Authorize]
        public JsonResult Registrar(Convocatoria_Registro registro)
        {
            try
            {
                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                String nameFile = String.Empty;
                if (registro.formatos != null)
                {
                    if ((registro.formatos.ToList())[0].GetType().FullName != "System.String[]")
                    {
                        HttpPostedFileBase postfile = ((HttpPostedFileBase[])(registro.formatos.ToList())[0])[0];
                        if (postfile != null)
                        {
                            if (postfile.ContentLength > 0)
                            {
                                nameFile = postfile.FileName;

                                Stream str = postfile.InputStream;
                                BinaryReader Br = new BinaryReader(str);
                                Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                                registro.fileCertificacion = FileDet;
                            }
                        }
                    }
                    if ((registro.formatos.ToList())[1].GetType().FullName != "System.String[]")
                    {
                        HttpPostedFileBase postfile = ((HttpPostedFileBase[])(registro.formatos.ToList())[1])[0];
                        if (postfile != null)
                        {
                            if (postfile.ContentLength > 0)
                            {
                                nameFile = postfile.FileName;

                                Stream str = postfile.InputStream;
                                BinaryReader Br = new BinaryReader(str);
                                Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                                registro.fileRequerimiento = FileDet;
                            }
                        }
                    }
                    if ((registro.formatos.ToList())[2].GetType().FullName != "System.String[]")
                    {
                        HttpPostedFileBase postfile = ((HttpPostedFileBase[])(registro.formatos.ToList())[2])[0];
                        if (postfile != null)
                        {
                            if (postfile.ContentLength > 0)
                            {
                                nameFile = postfile.FileName;

                                Stream str = postfile.InputStream;
                                BinaryReader Br = new BinaryReader(str);
                                Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                                registro.fileComite = FileDet;
                            }
                        }
                    }
                }
                
                registro.comite = new List<ConvocatoriaComite_Registro>();
                registro.comite.Add( new ConvocatoriaComite_Registro() {
                IdMiembro = 1, 
                IdDependencia = registro.IdComiteDependencia1, 
                IdTrabajador = registro.IdComiteMiembro1T,
                IdTitular= 1,
                Estado = 1
                });
                registro.comite.Add( new ConvocatoriaComite_Registro() {
                IdMiembro= 1,
                IdDependencia = registro.IdComiteDependencia1, 
                IdTrabajador = registro.IdComiteMiembro1S,
                IdTitular= 0,
                Estado = 1
                });
                registro.comite.Add( new ConvocatoriaComite_Registro() {
                IdMiembro = 2,
                IdDependencia = registro.IdComiteDependencia2, 
                IdTrabajador = registro.IdComiteMiembro2T,
                IdTitular = 1,
                Estado = 1
                });
                registro.comite.Add( new ConvocatoriaComite_Registro() {
                IdMiembro = 2,
                IdDependencia = registro.IdComiteDependencia2, 
                IdTrabajador = registro.IdComiteMiembro2S,
                IdTitular = 0,
                Estado = 1
                });
                registro.comite.Add( new ConvocatoriaComite_Registro() {
                IdMiembro = 3,
                IdDependencia = registro.IdComiteDependencia3, 
                IdTrabajador = registro.IdComiteMiembro3T,
                IdTitular = 1,
                Estado = 1
                });
                registro.comite.Add( new ConvocatoriaComite_Registro() {
                IdMiembro = 3,
                IdDependencia = registro.IdComiteDependencia3, 
                IdTrabajador = registro.IdComiteMiembro3S,
                IdTitular = 0,
                Estado = 1
                });
            
                //PostulanteInformacion_Registro postulante = _postulante_Servicio.ObtenerPostulanteFicha(new PostulanteInformacion_Registro() { IdPostulante = registro.IdPostulante, IdPostulacion = registro.IdPostulacion, IdConvocatoria = registro.IdConvocatoria });
                object respuesta = _convocatoria_Servicio.RegistrarConvocatoria(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        [Authorize]
        public JsonResult Actualizar(Convocatoria_Registro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                String nameFile = String.Empty;
                if (registro.formatos != null)
                {
                    if ((registro.formatos.ToList())[0].GetType().FullName != "System.String[]")
                    {
                        HttpPostedFileBase postfile = ((HttpPostedFileBase[])(registro.formatos.ToList())[0])[0];
                        if (postfile != null)
                        {
                            if (postfile.ContentLength > 0)
                            {
                                nameFile = postfile.FileName;

                                Stream str = postfile.InputStream;
                                BinaryReader Br = new BinaryReader(str);
                                Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                                registro.fileCertificacion = FileDet;
                            }
                        }
                    }
                    if ((registro.formatos.ToList())[1].GetType().FullName != "System.String[]")
                    {
                        HttpPostedFileBase postfile = ((HttpPostedFileBase[])(registro.formatos.ToList())[1])[0];
                        if (postfile != null)
                        {
                            if (postfile.ContentLength > 0)
                            {
                                nameFile = postfile.FileName;

                                Stream str = postfile.InputStream;
                                BinaryReader Br = new BinaryReader(str);
                                Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                                registro.fileRequerimiento = FileDet;
                            }
                        }
                    }
                    if ((registro.formatos.ToList())[2].GetType().FullName != "System.String[]")
                    {
                        HttpPostedFileBase postfile = ((HttpPostedFileBase[])(registro.formatos.ToList())[2])[0];
                        if (postfile != null)
                        {
                            if (postfile.ContentLength > 0)
                            {
                                nameFile = postfile.FileName;

                                Stream str = postfile.InputStream;
                                BinaryReader Br = new BinaryReader(str);
                                Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                                registro.fileComite = FileDet;
                            }
                        }
                    }
                }

                registro.comite = new List<ConvocatoriaComite_Registro>();
                registro.comite.Add(new ConvocatoriaComite_Registro()
                {
                    IdMiembro = 1,
                    IdDependencia = registro.IdComiteDependencia1,
                    IdTrabajador = registro.IdComiteMiembro1T,
                    IdTitular = 1,
                    Estado = 1
                });
                registro.comite.Add(new ConvocatoriaComite_Registro()
                {
                    IdMiembro = 1,
                    IdDependencia = registro.IdComiteDependencia1,
                    IdTrabajador = registro.IdComiteMiembro1S,
                    IdTitular = 0,
                    Estado = 1
                });
                registro.comite.Add(new ConvocatoriaComite_Registro()
                {
                    IdMiembro = 2,
                    IdDependencia = registro.IdComiteDependencia2,
                    IdTrabajador = registro.IdComiteMiembro2T,
                    IdTitular = 1,
                    Estado = 1
                });
                registro.comite.Add(new ConvocatoriaComite_Registro()
                {
                    IdMiembro = 2,
                    IdDependencia = registro.IdComiteDependencia2,
                    IdTrabajador = registro.IdComiteMiembro2S,
                    IdTitular = 0,
                    Estado = 1
                });
                registro.comite.Add(new ConvocatoriaComite_Registro()
                {
                    IdMiembro = 3,
                    IdDependencia = registro.IdComiteDependencia3,
                    IdTrabajador = registro.IdComiteMiembro3T,
                    IdTitular = 1,
                    Estado = 1
                });
                registro.comite.Add(new ConvocatoriaComite_Registro()
                {
                    IdMiembro = 3,
                    IdDependencia = registro.IdComiteDependencia3,
                    IdTrabajador = registro.IdComiteMiembro3S,
                    IdTitular = 0,
                    Estado = 1
                });

                //PostulanteInformacion_Registro postulante = _postulante_Servicio.ObtenerPostulanteFicha(new PostulanteInformacion_Registro() { IdPostulante = registro.IdPostulante, IdPostulacion = registro.IdPostulacion, IdConvocatoria = registro.IdConvocatoria });
                object respuesta = _convocatoria_Servicio.ActualizarConvocatoria(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        [Authorize]
        public JsonResult ActualizarPractica(Convocatoria_Registro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                String nameFile = String.Empty;
                if (registro.formatos != null)
                {
                    if ((registro.formatos.ToList())[0].GetType().FullName != "System.String[]")
                    {
                        HttpPostedFileBase postfile = ((HttpPostedFileBase[])(registro.formatos.ToList())[0])[0];
                        if (postfile != null)
                        {
                            if (postfile.ContentLength > 0)
                            {
                                nameFile = postfile.FileName;

                                Stream str = postfile.InputStream;
                                BinaryReader Br = new BinaryReader(str);
                                Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                                registro.fileCertificacion = FileDet;
                            }
                        }
                    }
                    if ((registro.formatos.ToList())[1].GetType().FullName != "System.String[]")
                    {
                        HttpPostedFileBase postfile = ((HttpPostedFileBase[])(registro.formatos.ToList())[1])[0];
                        if (postfile != null)
                        {
                            if (postfile.ContentLength > 0)
                            {
                                nameFile = postfile.FileName;

                                Stream str = postfile.InputStream;
                                BinaryReader Br = new BinaryReader(str);
                                Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                                registro.fileRequerimiento = FileDet;
                            }
                        }
                    }
                    //if ((registro.formatos.ToList())[2].GetType().FullName != "System.String[]")
                    //{
                    //    HttpPostedFileBase postfile = ((HttpPostedFileBase[])(registro.formatos.ToList())[2])[0];
                    //    if (postfile != null)
                    //    {
                    //        if (postfile.ContentLength > 0)
                    //        {
                    //            nameFile = postfile.FileName;

                    //            Stream str = postfile.InputStream;
                    //            BinaryReader Br = new BinaryReader(str);
                    //            Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                    //            registro.fileComite = FileDet;
                    //        }
                    //    }
                    //}
                }

                registro.comite = new List<ConvocatoriaComite_Registro>();
                registro.comite.Add(new ConvocatoriaComite_Registro()
                {
                    IdMiembro = 1,
                    IdDependencia = registro.IdComiteDependencia1,
                    IdTrabajador = registro.IdComiteMiembro1T,
                    IdTitular = 1,
                    Estado = 1
                });
                registro.comite.Add(new ConvocatoriaComite_Registro()
                {
                    IdMiembro = 1,
                    IdDependencia = registro.IdComiteDependencia1,
                    IdTrabajador = registro.IdComiteMiembro1S,
                    IdTitular = 0,
                    Estado = 1
                });
                registro.comite.Add(new ConvocatoriaComite_Registro()
                {
                    IdMiembro = 2,
                    IdDependencia = registro.IdComiteDependencia2,
                    IdTrabajador = registro.IdComiteMiembro2T,
                    IdTitular = 1,
                    Estado = 1
                });
                registro.comite.Add(new ConvocatoriaComite_Registro()
                {
                    IdMiembro = 2,
                    IdDependencia = registro.IdComiteDependencia2,
                    IdTrabajador = registro.IdComiteMiembro2S,
                    IdTitular = 0,
                    Estado = 1
                });
                registro.comite.Add(new ConvocatoriaComite_Registro()
                {
                    IdMiembro = 3,
                    IdDependencia = registro.IdComiteDependencia3,
                    IdTrabajador = registro.IdComiteMiembro3T,
                    IdTitular = 1,
                    Estado = 1
                });
                registro.comite.Add(new ConvocatoriaComite_Registro()
                {
                    IdMiembro = 3,
                    IdDependencia = registro.IdComiteDependencia3,
                    IdTrabajador = registro.IdComiteMiembro3S,
                    IdTitular = 0,
                    Estado = 1
                });

                //PostulanteInformacion_Registro postulante = _postulante_Servicio.ObtenerPostulanteFicha(new PostulanteInformacion_Registro() { IdPostulante = registro.IdPostulante, IdPostulacion = registro.IdPostulacion, IdConvocatoria = registro.IdConvocatoria });
                object respuesta = _convocatoria_Servicio.ActualizarConvocatoriaPractica(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        [Authorize]
        public JsonResult RegistrarConvocatoriaComiteEntrevista(Convocatoria_Registro registro)
        {
            try
            {
                Convocatoria_Registro objConvocatoria = _convocatoria_Servicio.ObtenerParaEditar(new Convocatoria_Request() { IdConvocatoria = registro.IdConvocatoria });
                List<ConvocatoriaComite_Registro> lista = new List<ConvocatoriaComite_Registro>();

                String fila = String.Empty;
                String todos = String.Empty;
                Int32 iPos = 0;
                if (objConvocatoria.IdTieneExamenConoc == 1) {
                    List<PostulacionEvaluacionConocimiento_Registro> listaPostulante = _convocatoria_Servicio.ListarPostulantesEvaluacionConocimiento(new Convocatoria_Request() { IdConvocatoria = registro.IdConvocatoria }).ToList();

                    registro.FechaRegistro = DateTime.Now;
                    registro.IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;
                    iPos = 1;
                    foreach (PostulacionEvaluacionConocimiento_Registro obj in listaPostulante)
                    {
                        if (obj.AptoTotal == 1)
                        {
                            lista.Add(new ConvocatoriaComite_Registro()
                            {
                                IdConvocatoria = registro.IdConvocatoria,
                                IdDependencia = registro.IdComiteDependencia1,
                                IdTrabajador = registro.IdComiteMiembro1T,
                                IdPostulacion = obj.IdPostulacion,
                                IdPostulante = obj.IdPostulante,
                                FechaEntrevista = obj.FechaEntrevista,
                                HoraEntrevista = obj.HoraEntrevista,
                                Estado = 1,
                                FechaRegistro = DateTime.Now,
                                IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario
                            });
                            lista.Add(new ConvocatoriaComite_Registro()
                            {
                                IdConvocatoria = registro.IdConvocatoria,
                                IdDependencia = registro.IdComiteDependencia2,
                                IdTrabajador = registro.IdComiteMiembro2T,
                                IdPostulacion = obj.IdPostulacion,
                                IdPostulante = obj.IdPostulante,
                                FechaEntrevista = obj.FechaEntrevista,
                                HoraEntrevista = obj.HoraEntrevista,
                                Estado = 1,
                                FechaRegistro = DateTime.Now,
                                IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario
                            });
                            if (registro.IdComiteDependencia3 > 0 && registro.IdComiteMiembro3T > 0)
                            {
                                lista.Add(new ConvocatoriaComite_Registro()
                                {
                                    IdConvocatoria = registro.IdConvocatoria,
                                    IdDependencia = registro.IdComiteDependencia3,
                                    IdTrabajador = registro.IdComiteMiembro3T,
                                    IdPostulacion = obj.IdPostulacion,
                                    IdPostulante = obj.IdPostulante,
                                    FechaEntrevista = obj.FechaEntrevista,
                                    HoraEntrevista = obj.HoraEntrevista,
                                    Estado = 1,
                                    FechaRegistro = DateTime.Now,
                                    IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario
                                });
                            }

                            fila = String.Empty;
                            fila += "<tr>";
                            fila += "<td style='width: 15px; text-align: right; ' >" + iPos.ToString() + ". </td>";
                            //fila += "<td style='width: 100px; text-align: center; ' >" + obj.NroDocumento + "</td>";
                            fila += "<td style='width: 335px;' >" + obj.NombreCompleto + "</td>";
                            fila += "<td style='width: 75px; text-align: center; ' > " + obj.FechaEntrevista + " </td>";
                            fila += "<td style='width: 75px; text-align: center; ' >" + obj.HoraEntrevista + "</td>";
                            fila += "</tr>";

                            todos += fila;
                            iPos += 1;
                        }
                    }
                }
                else {
                    List<PostulacionEvaluacionCurricular_Registro> listaPostulante = _convocatoria_Servicio.ListarPostulantesEvaluacionCurri(new Convocatoria_Request() { IdConvocatoria = registro.IdConvocatoria }).ToList();

                    registro.FechaRegistro = DateTime.Now;
                    registro.IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;
                    iPos = 1;
                    foreach (PostulacionEvaluacionCurricular_Registro obj in listaPostulante)
                    {
                        if (obj.AptoTotal == 1)
                        {
                            lista.Add(new ConvocatoriaComite_Registro()
                            {
                                IdConvocatoria = registro.IdConvocatoria,
                                IdDependencia = registro.IdComiteDependencia1,
                                IdTrabajador = registro.IdComiteMiembro1T,
                                IdPostulacion = obj.IdPostulacion,
                                IdPostulante = obj.IdPostulante,
                                FechaEntrevista = obj.FechaEntrevista,
                                HoraEntrevista = obj.HoraEntrevista,
                                Estado = 1,
                                FechaRegistro = DateTime.Now,
                                IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario
                            });
                            lista.Add(new ConvocatoriaComite_Registro()
                            {
                                IdConvocatoria = registro.IdConvocatoria,
                                IdDependencia = registro.IdComiteDependencia2,
                                IdTrabajador = registro.IdComiteMiembro2T,
                                IdPostulacion = obj.IdPostulacion,
                                IdPostulante = obj.IdPostulante,
                                FechaEntrevista = obj.FechaEntrevista,
                                HoraEntrevista = obj.HoraEntrevista,
                                Estado = 1,
                                FechaRegistro = DateTime.Now,
                                IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario
                            });
                            if (registro.IdComiteDependencia3 > 0 && registro.IdComiteMiembro3T > 0)
                            {
                                lista.Add(new ConvocatoriaComite_Registro()
                                {
                                    IdConvocatoria = registro.IdConvocatoria,
                                    IdDependencia = registro.IdComiteDependencia3,
                                    IdTrabajador = registro.IdComiteMiembro3T,
                                    IdPostulacion = obj.IdPostulacion,
                                    IdPostulante = obj.IdPostulante,
                                    FechaEntrevista = obj.FechaEntrevista,
                                    HoraEntrevista = obj.HoraEntrevista,
                                    Estado = 1,
                                    FechaRegistro = DateTime.Now,
                                    IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario
                                });
                            }

                            fila = String.Empty;
                            fila += "<tr>";
                            fila += "<td style='width: 15px; text-align: right; ' >" + iPos.ToString() + ". </td>";
                            //fila += "<td style='width: 100px; text-align: center; ' >" + obj.NroDocumento + "</td>";
                            fila += "<td style='width: 335px;' >" + obj.NombreCompleto + "</td>";
                            fila += "<td style='width: 75px; text-align: center; ' > " + obj.FechaEntrevista + " </td>";
                            fila += "<td style='width: 75px; text-align: center; ' >" + obj.HoraEntrevista + "</td>";
                            fila += "</tr>";

                            todos += fila;
                            iPos += 1;
                        }
                    }
                }

                //PostulanteInformacion_Registro postulante = _postulante_Servicio.ObtenerPostulanteFicha(new PostulanteInformacion_Registro() { IdPostulante = registro.IdPostulante, IdPostulacion = registro.IdPostulacion, IdConvocatoria = registro.IdConvocatoria });
                object respuesta = _convocatoria_Servicio.RegistrarConvocatoriaComiteEntrevista(lista);
                //Convocatoria_Registro objConvocatoria = _convocatoria_Servicio.ObtenerParaEditar(new Convocatoria_Request() { IdConvocatoria = registro.IdConvocatoria });
                objConvocatoria.Meta = registro.Meta;

                if (registro.IdTipoNotificacion == 0 || registro.IdTipoNotificacion == 1) { 
                    foreach (Int32 obj in lista.Select(o => o.IdTrabajador).Distinct())
                    { 
                        Empleado_Registro objempleado = _empleado_Servicio.ObtenerParaEditar(new Empleado_Request(){ IdEmpleado = obj });
                        SendEmailEntrevista(objempleado, objConvocatoria, "1", todos);
                    }
                }
                if (registro.IdTipoNotificacion == 0 || registro.IdTipoNotificacion == 2)
                {
                    //envio de los correos a los postulantes 
                    Boolean blnEnvio = false;
                    String strExistePostulante = String.Empty;
                    List<PostulacionPostulante_Registro> lstPostulante = _convocatoria_Servicio.ListarPostulantes(new Convocatoria_Request() { IdConvocatoria = registro.IdConvocatoria }).ToList();
                    foreach (ConvocatoriaComite_Registro obj in lista) //.Select(o => o.IdPostulacion).Distinct())
                    {
                        if (strExistePostulante.IndexOf(obj.IdPostulacion.ToString()) < 0)
                        {
                            strExistePostulante = strExistePostulante + obj.IdPostulacion.ToString() + ",";
                            blnEnvio = false;
                            foreach (PostulacionPostulante_Registro objPostulante in lstPostulante)
                            {
                                if (obj.IdPostulacion == objPostulante.IdPostulacion && blnEnvio == false)
                                {
                                    SendEmailEntrevistaPostulante(objPostulante, obj, objConvocatoria, "10");
                                    blnEnvio = true;
                                }
                            }
                        }
                    }
                }
                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        [Authorize]
        public JsonResult LimpiarEvaluacionEntrevista(PostulacionEvaluacionEntrevista_Registro registro)
        {
            try
            {
                registro.IdUsuarioModificacion = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;
                object respuesta = _convocatoria_Servicio.LimpiarEvaluacionEntrevista(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        [Authorize]
        public JsonResult DeclararAccesitarioGanador(PostulacionEvaluacionEntrevista_Registro registro)
        {
            try
            {
                registro.IdUsuarioModificacion = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;
                object respuesta = _convocatoria_Servicio.DeclararAccesitarioGanador(registro);

                Convocatoria_Registro objConvocatoria = _convocatoria_Servicio.ObtenerParaEditar(new Convocatoria_Request() { IdConvocatoria = registro.IdConvocatoria });
                PostulacionPostulante_Registro objPostulante = _convocatoria_Servicio.ListarPostulantes(new Convocatoria_Request() { IdConvocatoria = registro.IdConvocatoria }).Where(x => x.IdPostulante == registro.IdPostulante).FirstOrDefault();
                SendEmailEntrevistaPostulante(objPostulante, null, objConvocatoria, "20");

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        [Authorize]
        public JsonResult AsignarConvocatoriaEvaluacionCurricular(Convocatoria_Registro registro)
        {
            try
            {
                Convocatoria_Registro objConvocatoria = _convocatoria_Servicio.ObtenerParaEditar(new Convocatoria_Request() { IdConvocatoria = registro.IdConvocatoria });
                Empleado_Registro objempleado = _empleado_Servicio.ObtenerParaEditar(new Empleado_Request() { IdEmpleado = registro.IdComiteMiembro1T });
                String enlace = HttpUtility.UrlEncode(new Crypto().Encriptar(registro.IdConvocatoria + "|" + objempleado.IdEmpleado.ToString() + "|" + String.Format("{0} {1} {2}", objempleado.Nombre, objempleado.Paterno, objempleado.Materno)));

                SendEmailAsignarEvaluacion(objempleado, objConvocatoria, enlace);
                
                return Json(new { success = "True", responseText = "1" });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        [Authorize]
        public JsonResult RegistrarConvocatoriaPracComiteEntrevista(Convocatoria_Registro registro)
        {
            try
            {
                Convocatoria_Registro objConvocatoria = _convocatoria_Servicio.ObtenerPracticaParaEditar(new Convocatoria_Request() { IdConvocatoria = registro.IdConvocatoria });
                List<ConvocatoriaComite_Registro> lista = new List<ConvocatoriaComite_Registro>();

                String fila = String.Empty;
                String todos = String.Empty;
                Int32 iPos = 0;
                List<PostulacionEvaluacionCurricular_Registro> listaPostulante = _convocatoria_Servicio.ListarPostulantesPracticaEvaluacionCurri(new Convocatoria_Request() { IdConvocatoria = registro.IdConvocatoria }).ToList();

                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;
                iPos = 1;
                foreach (PostulacionEvaluacionCurricular_Registro obj in listaPostulante)
                {
                    if (obj.AptoTotal == 1)
                    {
                        lista.Add(new ConvocatoriaComite_Registro()
                        {
                            IdConvocatoria = registro.IdConvocatoria,
                            IdDependencia = registro.IdComiteDependencia1,
                            IdTrabajador = registro.IdComiteMiembro1T,
                            IdPostulacion = obj.IdPostulacion,
                            IdPostulante = obj.IdPostulante,
                            FechaEntrevista = obj.FechaEntrevista,
                            HoraEntrevista = obj.HoraEntrevista,
                            Estado = 1,
                            FechaRegistro = DateTime.Now,
                            IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario
                        });
                        lista.Add(new ConvocatoriaComite_Registro()
                        {
                            IdConvocatoria = registro.IdConvocatoria,
                            IdDependencia = registro.IdComiteDependencia2,
                            IdTrabajador = registro.IdComiteMiembro2T,
                            IdPostulacion = obj.IdPostulacion,
                            IdPostulante = obj.IdPostulante,
                            FechaEntrevista = obj.FechaEntrevista,
                            HoraEntrevista = obj.HoraEntrevista,
                            Estado = 1,
                            FechaRegistro = DateTime.Now,
                            IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario
                        });
                        if (registro.IdComiteDependencia3 > 0 && registro.IdComiteMiembro3T > 0)
                        {
                            lista.Add(new ConvocatoriaComite_Registro()
                            {
                                IdConvocatoria = registro.IdConvocatoria,
                                IdDependencia = registro.IdComiteDependencia3,
                                IdTrabajador = registro.IdComiteMiembro3T,
                                IdPostulacion = obj.IdPostulacion,
                                IdPostulante = obj.IdPostulante,
                                FechaEntrevista = obj.FechaEntrevista,
                                HoraEntrevista = obj.HoraEntrevista,
                                Estado = 1,
                                FechaRegistro = DateTime.Now,
                                IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario
                            });
                        }

                        fila = String.Empty;
                        fila += "<tr>";
                        fila += "<td style='width: 15px; text-align: right; ' >" + iPos.ToString() + ". </td>";
                        //fila += "<td style='width: 100px; text-align: center; ' >" + obj.NroDocumento + "</td>";
                        fila += "<td style='width: 335px;' >" + obj.NombreCompleto + "</td>";
                        fila += "<td style='width: 75px; text-align: center; ' > " + obj.FechaEntrevista + " </td>";
                        fila += "<td style='width: 75px; text-align: center; ' >" + obj.HoraEntrevista + "</td>";
                        fila += "</tr>";

                        todos += fila;
                        iPos += 1;
                    }
                }
                
                //PostulanteInformacion_Registro postulante = _postulante_Servicio.ObtenerPostulanteFicha(new PostulanteInformacion_Registro() { IdPostulante = registro.IdPostulante, IdPostulacion = registro.IdPostulacion, IdConvocatoria = registro.IdConvocatoria });
                object respuesta = _convocatoria_Servicio.RegistrarConvocatoriaPracticaComiteEntrevista(lista);
                //Convocatoria_Registro objConvocatoria = _convocatoria_Servicio.ObtenerParaEditar(new Convocatoria_Request() { IdConvocatoria = registro.IdConvocatoria });
                objConvocatoria.Meta = registro.Meta;
                foreach (Int32 obj in lista.Select(o => o.IdTrabajador).Distinct())
                {
                    Empleado_Registro objempleado = _empleado_Servicio.ObtenerParaEditar(new Empleado_Request() { IdEmpleado = obj });
                    SendEmailEntrevista(objempleado, objConvocatoria, "2", todos);
                }
                //envio de los correos a los postulantes 
                Boolean blnEnvio = false;
                String strExistePostulante = String.Empty;
                List<PostulacionPostulante_Registro> lstPostulante = _convocatoria_Servicio.ListarPostulantesPractica(new Convocatoria_Request() { IdConvocatoria = registro.IdConvocatoria }).ToList();
                foreach (ConvocatoriaComite_Registro obj in lista) //.Select(o => o.IdPostulacion).Distinct())
                {
                    if (strExistePostulante.IndexOf(obj.IdPostulacion.ToString()) < 0)
                    {
                        strExistePostulante = strExistePostulante + obj.IdPostulacion.ToString() + ",";
                        blnEnvio = false;
                        foreach (PostulacionPostulante_Registro objPostulante in lstPostulante)
                        {
                            if (obj.IdPostulacion == objPostulante.IdPostulacion && blnEnvio == false)
                            {
                                SendEmailEntrevistaPostulante(objPostulante, obj, objConvocatoria, "11");
                                blnEnvio = true;
                            }
                        }
                    }
                }

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult ActualizarEvaluacionCurri(PostulacionEvaluacionCurricular_Registro registro)
        {
            try
            {
                //registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = registro.IdUsuarioModificacion;

                object respuesta = _convocatoria_Servicio.ActualizarEvaluacionCurri(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult ActualizarPracticaEvaluacionCurri(PostulacionEvaluacionCurricular_Registro registro)
        {
            try
            {
                //registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = registro.IdUsuarioModificacion;

                object respuesta = _convocatoria_Servicio.ActualizarPracticaEvaluacionCurri(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult ActualizarEvaluacionConocimiento(PostulacionEvaluacionConocimiento_Registro registro)
        {
            try
            {
                //registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = registro.IdUsuarioModificacion;

                DateTime aux;
                if (!DateTime.TryParse(registro.FechaEntrevista, out aux)) registro.FechaEntrevista = null;
                if (!DateTime.TryParse(registro.HoraEntrevista, out aux)) registro.HoraEntrevista = null;
                object respuesta = _convocatoria_Servicio.ActualizarEvaluacionConocimiento(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult ActualizarEvaluacionCurriObs(PostulacionEvaluacionCurricular_Registro registro)
        {
            try
            {
                //registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = registro.IdUsuarioModificacion;

                object respuesta = _convocatoria_Servicio.ActualizarEvaluacionCurriObs(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult ActualizarPracticaEvaluacionCurriObs(PostulacionEvaluacionCurricular_Registro registro)
        {
            try
            {
                //registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = registro.IdUsuarioModificacion;

                object respuesta = _convocatoria_Servicio.ActualizarPracticaEvaluacionCurriObs(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult ActualizarEvaluacionNSP(PostulacionEvaluacionEntrevista_Registro registro)
        {
            try
            {
                //registro.FechaModificacion = DateTime.Now;
                //registro.IdUsuarioModificacion = registro.IdUsuarioModificacion;

                object respuesta = _convocatoria_Servicio.ActualizarEvaluacionNSP(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult ActualizarEvaluacionPracticaNSP(PostulacionEvaluacionEntrevista_Registro registro)
        {
            try
            {
                //registro.FechaModificacion = DateTime.Now;
                //registro.IdUsuarioModificacion = registro.IdUsuarioModificacion;

                object respuesta = _convocatoria_Servicio.ActualizarEvaluacionPracticaNSP(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult ActualizarEvaluacionEntrevistaPersonal(PostulacionEvaluacionEntrevista_Registro registro)
        {
            try
            {
                //registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = registro.IdUsuarioModificacion;

                object respuesta = _convocatoria_Servicio.ActualizarEvaluacionEntrevistaPersonal(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult ActualizarEvaluacionPracticaEntrevistaPersonal(PostulacionEvaluacionEntrevista_Registro registro)
        {
            try
            {
                //registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = registro.IdUsuarioModificacion;

                object respuesta = _convocatoria_Servicio.ActualizarEvaluacionPracticaEntrevistaPersonal(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult ActualizarActaEntrevistaPersonal(PostulacionEvaluacionEntrevista_Registro registro)
        {
            try
            {
                //registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = registro.IdUsuarioModificacion;

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

                object respuesta = _convocatoria_Servicio.ActualizarActaEntrevistaPersonal(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        //[HttpPost]
        //[Authorize]
        //public JsonResult RegistrarContratoArchivo(EmpleadoContrato_Registro registro)
        //{
        //    try
        //    {
        //        registro.FechaModificacion = DateTime.Now;
        //        registro.IdUsuarioModificacion = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

        //        String nameFile = String.Empty;
        //        for (Int32 j = 0; j < registro.formatos.ToList().Count; j++) {
        //            HttpPostedFileBase postfile = ((HttpPostedFileBase[])(registro.formatos.ToList())[j])[0];
        //            if (postfile.ContentLength > 0) {
        //                nameFile = postfile.FileName;

        //                Stream str = postfile.InputStream;
        //                BinaryReader Br = new BinaryReader(str);
        //                Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

        //                registro.archivo = FileDet;

        //            }
        //        }

        //        object respuesta = _contrato_Servicio.RegistrarContratoArchivo(registro);
        //        EmpleadoContrato_Registro objContrato = _contrato_Servicio.ObtenerParaEditar(new Contrato_Request(){IdContrato = registro.IdContrato, Nombre = "", Estado = -1 });
        //        PostulanteInformacion_Registro obj = _postulante_Servicio.ObtenerPostulanteFicha(new PostulanteInformacion_Registro() { IdPostulante = objContrato.IdPostulante, IdPostulacion = objContrato.IdPostulacion, IdConvocatoria = objContrato.IdConvocatoria, Nombre= objContrato.Nombre });
        //        this.SendEmail(obj, "3");

        //        return Json(new { success = "True", responseText = respuesta });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = "False", responseText = ex.Message });
        //    }
        //}
        //public FileResult Ficha()
        //{
        //    Int32 IdContrato = (Request.QueryString.Get("IdContrato") == null ? 0 : Int32.Parse(Request.QueryString["IdContrato"]));
        //    EmpleadoContrato_Registro objContrato = _contrato_Servicio.ObtenerParaEditar(new Contrato_Request() { IdContrato = IdContrato, NroDocumento = "", NroContrato = "", Nombre = "", Estado = 0 });

        //    var fileName = "Contrato_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".docx";
        //    string fullPathOri = Path.Combine(Server.MapPath("~/Templates/Contrato/formato"), "CONTRATO_ADMINISTRATIVO_SERVICIOS_N.docx");
        //    string fullPathNew = Path.Combine(Server.MapPath("~/Templates/Contrato"), fileName);
        //    using (var doc = DocX.Load(fullPathOri))
        //    {
        //        doc.ReplaceText("<NRO_CONTRATO>", String.Format("{0}-{1}", objContrato.NroContrato.ToString().PadLeft(3, '0'), objContrato.Anio));
        //        doc.ReplaceText("<NOMBRE>", String.Format("{0} {1} {2}", objContrato.Nombre, objContrato.Paterno, objContrato.Materno));
        //        doc.ReplaceText("<DNI>", objContrato.NroDocumento);
        //        doc.ReplaceText("<RUC>", objContrato.RUC);
        //        doc.ReplaceText("<DOMICILIO>", String.Format("{0}-{1}", objContrato.Domicilio, objContrato.Ubigeo));
        //        doc.ReplaceText("<CARGO>", objContrato.NombreCargo);
        //        doc.ReplaceText("<DEPENDENCIA>", objContrato.NombreOficina);
        //        doc.ReplaceText("<PROCESO>", objContrato.NombreProceso);
        //        doc.ReplaceText("<INICIO>", objContrato.FechaInicio.Value.ToLongDateString().Substring(objContrato.FechaInicio.Value.ToLongDateString().IndexOf(',') + 2));
        //        doc.ReplaceText("<FIN>", (objContrato.IdTipoLimite == 1 ? objContrato.FechaCese.Value.ToLongDateString().Substring(objContrato.FechaCese.Value.ToLongDateString().IndexOf(',') + 2) : "AL FINALIZAR LA DESIGNACIÓN"));
        //        doc.ReplaceText("<REMUNERACION>", objContrato.Remuneracion.ToString("C"));
        //        doc.ReplaceText("<FECHA>", DateTime.Now.ToLongDateString().Substring(DateTime.Now.ToLongDateString().IndexOf(',') + 2));

        //        doc.SaveAs(fullPathNew);
        //    }

        //    FileContentResult result = new FileContentResult(System.IO.File.ReadAllBytes(fullPathNew), "application/msword") { FileDownloadName = fileName };

        //    return result;
        //}
        [HttpPost]
        public JsonResult ObtenerParaEditar(Convocatoria_Request peticion)
        {
            Convocatoria_Registro respuesta = _convocatoria_Servicio.ObtenerParaEditar(peticion);
            //respuesta.Contrasena = HttpUtility.UrlEncode(new Crypto().Encriptar(peticion.IdConvocatoria + "|" + VariablesWeb.ConsultaInformacion.iCodUsuario.ToString()));

            return Json(respuesta);
        }
        [HttpPost]
        public JsonResult ObtenerPracticaParaEditar(Convocatoria_Request peticion)
        {
            Convocatoria_Registro respuesta = _convocatoria_Servicio.ObtenerPracticaParaEditar(peticion);
            //respuesta.Contrasena = HttpUtility.UrlEncode(new Crypto().Encriptar(peticion.IdConvocatoria + "|" + VariablesWeb.ConsultaInformacion.iCodUsuario.ToString()));

            return Json(respuesta);
        }
        [HttpGet]
        public JsonResult ListarConvocatoriaComite(Convocatoria_Request peticion)
        {
            object respuesta = _convocatoria_Servicio.ListarConvocatoriaComite(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarConvocatoriaPracComite(Convocatoria_Request peticion)
        {
            object respuesta = _convocatoria_Servicio.ListarConvocatoriaPracComite(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarConvocatoriaDocumento(Convocatoria_Request peticion)
        {
            object respuesta = _convocatoria_Servicio.ListarConvocatoriaDocumento(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarComunicado(ConvocatoriaDocumento_Registro registro)
        {
            try
            {
                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioModificacion = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _convocatoria_Servicio.EliminarComunicado(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult EliminarComunicadoPractica(ConvocatoriaDocumento_Registro registro)
        {
            try
            {
                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioModificacion = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _convocatoria_Servicio.EliminarComunicadoPractica(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        //[AllowAnonymous]
        //[DeleteTempFile]
        [HttpGet]
        public ActionResult DescargarArchivo(String idConvocatoria, String idTipo)
        {
            Convocatoria_Request peticion = new Convocatoria_Request();
            peticion.IdConvocatoria = Int32.Parse(idConvocatoria);

            Convocatoria_Registro item = _convocatoria_Servicio.ObtenerConvocatoriaDocumento(peticion);
            
            switch (idTipo)
            {
                case "1": return File(item.fileRequerimiento, "application/pdf", String.Format("Anexo_01_{0}.pdf", idConvocatoria));
                case "2": return File(item.fileCertificacion, "application/pdf", String.Format("Anexo_04_{0}.pdf", idConvocatoria));
                case "3": return File(item.fileComite, "application/pdf", String.Format("Anexo_03_{0}.pdf", idConvocatoria));
                case "10": return File(item.fileActaCurri, "application/pdf", String.Format("Anexo_07_{0}.pdf", idConvocatoria));
            }

            return null;
        }
        [HttpGet]
        public ActionResult DescargarArchivoPractica(String idConvocatoria, String idTipo)
        {
            Convocatoria_Request peticion = new Convocatoria_Request();
            peticion.IdConvocatoria = Int32.Parse(idConvocatoria);

            Convocatoria_Registro item = _convocatoria_Servicio.ObtenerConvocatoriaPracticaDocumento(peticion);

            switch (idTipo)
            {
                case "1": return File(item.fileRequerimiento, "application/pdf", String.Format("Anexo_01_{0}.pdf", idConvocatoria));
                case "2": return File(item.fileCertificacion, "application/pdf", String.Format("Anexo_04_{0}.pdf", idConvocatoria));
                case "3": return File(item.fileComite, "application/pdf", String.Format("Anexo_03_{0}.pdf", idConvocatoria));
                case "10": return File(item.fileActaCurri, "application/pdf", String.Format("Anexo_07_{0}.pdf", idConvocatoria));
            }

            return null;
        }
        [HttpGet]
        public ActionResult DescargarArchivoPorId(String idConvocatoriaDoc)
        {
            Convocatoria_Request peticion = new Convocatoria_Request();
            peticion.IdConvocatoriaDocumento = Int32.Parse(idConvocatoriaDoc);
            //peticion.IdConvocatoria = Int32.Parse(idConvocatoria);

            Convocatoria_Registro item = _convocatoria_Servicio.ObtenerConvocatoriaDocumentoPorId(peticion);

            return File(item.fileComunicado, "application/pdf", String.Format("Comunicado_{0}.pdf", idConvocatoriaDoc));
        }
        [HttpGet]
        public ActionResult DescargarArchivoPracticaPorId(String idConvocatoriaDoc)
        {
            Convocatoria_Request peticion = new Convocatoria_Request();
            peticion.IdConvocatoriaDocumento = Int32.Parse(idConvocatoriaDoc);
            //peticion.IdConvocatoria = Int32.Parse(idConvocatoria);

            Convocatoria_Registro item = _convocatoria_Servicio.ObtenerConvocatoriaPracticaDocumentoPorId(peticion);

            return File(item.fileComunicado, "application/pdf", String.Format("Comunicado_{0}.pdf", idConvocatoriaDoc));
        }
        [HttpGet]
        public ActionResult DescargarArchivoEntrevista(String idEntrevista)
        {
            Convocatoria_Request peticion = new Convocatoria_Request();
            peticion.IdConvocatoriaDocumento = Int32.Parse(idEntrevista);
            //peticion.IdConvocatoria = Int32.Parse(idConvocatoria);

            Convocatoria_Registro item = _convocatoria_Servicio.ObtenerConvocatoriaDocumentoEntrevista(peticion);

            return File(item.fileEntrevista, "application/pdf", String.Format("Entrevista_{0}.pdf", idEntrevista));
        }
        [HttpGet]
        public ActionResult DescargarArchivoPracticaEntrevista(String idEntrevista)
        {
            Convocatoria_Request peticion = new Convocatoria_Request();
            peticion.IdConvocatoriaDocumento = Int32.Parse(idEntrevista);
            //peticion.IdConvocatoria = Int32.Parse(idConvocatoria);

            Convocatoria_Registro item = _convocatoria_Servicio.ObtenerConvocatoriaPracticaDocumentoEntrevista(peticion);

            return File(item.fileEntrevista, "application/pdf", String.Format("Entrevista_{0}.pdf", idEntrevista));
        }
        //[AllowAnonymous]
        [HttpGet]
        [Authorize]
        public ActionResult EvaluacionCurricular(String id)
        {
            if (!String.IsNullOrEmpty(id)) {
                String[] arraydata = new Crypto().Desencriptar(id).Split('|');

                Convocatoria_Request peticion = new Convocatoria_Request() { IdConvocatoria = Int32.Parse(arraydata[0]), IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario };
                _convocatoria_Servicio.IniciarEvaluacionCurri(peticion);

                if (arraydata != null)
                {
                    ViewBag.IdConvocatoria = arraydata[0];
                    ViewBag.IdTrabajador = arraydata[1];
                    ViewBag.Trabajador = arraydata[2];
                }
            }   

            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult EvaluacionConocimiento(String id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                String[] arraydata = new Crypto().Desencriptar(id).Split('|');

                Convocatoria_Request peticion = new Convocatoria_Request() { IdConvocatoria = Int32.Parse(arraydata[0]), IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario };
                _convocatoria_Servicio.IniciarEvaluacionConocimiento(peticion);

                if (arraydata != null)
                {
                    ViewBag.IdConvocatoria = arraydata[0];
                    ViewBag.IdTrabajador = arraydata[1];
                    ViewBag.Trabajador = arraydata[2];
                }
            }   

            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult EntrevistaPersonal(String id)
        {
            if (!String.IsNullOrEmpty(id)) {
                String[] arraydata = new Crypto().Desencriptar(id).Split('|');
                Empleado_Registro objempleado = _empleado_Servicio.ObtenerParaEditar(new Empleado_Request() { IdEmpleado = Convert.ToInt32(arraydata[1]) });

                //Postulante_Request peticion = new Postulante_Request() { IdPostulante = Int32.Parse(arraydata[0]), IdPostulacion = Int32.Parse(arraydata[1]), IdConvocatoria = Int32.Parse(arraydata[2]) };

                if (arraydata != null)
                {
                    ViewBag.IdConvocatoria = arraydata[0];
                    ViewBag.IdTrabajador = arraydata[1];
                    ViewBag.Trabajador = objempleado.Nombre + " " + objempleado.Paterno + " " + objempleado.Materno;
                }
            }
            
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult EntrevistaPersonalPracticas(String id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                String[] arraydata = new Crypto().Desencriptar(id).Split('|');
                Empleado_Registro objempleado = _empleado_Servicio.ObtenerParaEditar(new Empleado_Request() { IdEmpleado = Convert.ToInt32(arraydata[1]) });

                //Postulante_Request peticion = new Postulante_Request() { IdPostulante = Int32.Parse(arraydata[0]), IdPostulacion = Int32.Parse(arraydata[1]), IdConvocatoria = Int32.Parse(arraydata[2]) };

                if (arraydata != null)
                {
                    ViewBag.IdConvocatoria = arraydata[0];
                    ViewBag.IdTrabajador = arraydata[1];
                    ViewBag.Trabajador = objempleado.Nombre + " " + objempleado.Paterno + " " + objempleado.Materno;
                }
            }

            return View();
        }

        public FileResult DescargarAnexo07()
        {
            Int32 IdConvocatoria = (Request.QueryString.Get("id") == null ? 0 : Int32.Parse(Request.QueryString["id"]));
            Int32 IdTrabajador = (Request.QueryString.Get("idTra") == null ? 0 : Int32.Parse(Request.QueryString["idTra"]));

            Convocatoria_Request peticion = new Convocatoria_Request() { IdConvocatoria = IdConvocatoria };
            Convocatoria_Registro objConvocatoria = _convocatoria_Servicio.ObtenerParaEditar(peticion);
            Empleado_Registro objEmpleado = _empleado_Servicio.ObtenerParaEditar(new Empleado_Request() { IdEmpleado = IdTrabajador });
            List<PostulacionEvaluacionCurricular_Registro> lista = _convocatoria_Servicio.ListarPostulantesEvaluacionCurri(peticion).ToList();
            //Persona_Registro objPersona = new T_genm_persona_LN().Listar(new Persona_Request() { IdPersona = IdTrabajador });

            Stream pdfStream = GenerarAnexoEvaluacionCurricularPdf(objConvocatoria, objEmpleado, lista);

            return File(pdfStream, "application/pdf"); // ("Ficha", "_Layout_Blank", oAsistenciaTecnicaDetalle_Registro);
        }
        public FileResult DescargarAnexo07Practica()
        {
            Int32 IdConvocatoria = (Request.QueryString.Get("id") == null ? 0 : Int32.Parse(Request.QueryString["id"]));
            Int32 IdTrabajador = (Request.QueryString.Get("idTra") == null ? 0 : Int32.Parse(Request.QueryString["idTra"]));

            Convocatoria_Request peticion = new Convocatoria_Request() { IdConvocatoria = IdConvocatoria };
            Convocatoria_Registro objConvocatoria = _convocatoria_Servicio.ObtenerPracticaParaEditar(peticion);
            Empleado_Registro objEmpleado = _empleado_Servicio.ObtenerParaEditar(new Empleado_Request() { IdEmpleado = IdTrabajador });
            List<PostulacionEvaluacionCurricular_Registro> lista = _convocatoria_Servicio.ListarPostulantesPracticaEvaluacionCurri(peticion).ToList();
            //Persona_Registro objPersona = new T_genm_persona_LN().Listar(new Persona_Request() { IdPersona = IdTrabajador });

            Stream pdfStream = GenerarAnexoEvaluacionCurricularPracticaPdf(objConvocatoria, objEmpleado, lista);

            return File(pdfStream, "application/pdf"); // ("Ficha", "_Layout_Blank", oAsistenciaTecnicaDetalle_Registro);
        }

        private Stream GenerarAnexoEvaluacionCurricularPdf(Convocatoria_Registro oConvocatoria, Empleado_Registro oEmpleado, List<PostulacionEvaluacionCurricular_Registro> lstPostulantes)
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

            string html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Convocatoria/evaluacion_anexo07.html"));
            html = html.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            html = html.Replace("//dia", DateTime.Now.Day.ToString());
            html = html.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
            html = html.Replace("//anio", DateTime.Now.Year.ToString());
            html = html.Replace("//horas", System.DateTime.Now.ToString("HH:mm"));
            html = html.Replace("//fecha", System.DateTime.Now.ToString("dd/MM/yyyy"));
            html = html.Replace("//proceso_cas", oConvocatoria.NroConvocatoria);
            html = html.Replace("//cargo", oConvocatoria.NombreCargo.ToUpper());
            //html = html.Replace("//evaluador", objEmpleado.NombreCompleto);
            //html = html.Replace("//dni", objEmpleado.NroDocumento);

            String postulantes = "<table style='font-family: 'PT Sans Narrow', sans-serif; font-size: 11px; width: 500px; border: 0px solid #f0f0f0;' border='0' cellspacing='0' cellpadding='2'>";
            postulantes += "<tbody>";
            String fila = String.Empty;
            Int32 iPos = 1;
            foreach (PostulacionEvaluacionCurricular_Registro obj in lstPostulantes) {
                fila = String.Empty;
                fila += "<tr>";
                fila += "<td style='width: 15px; text-align: right; ' >" + iPos.ToString() + ". </td>";
                fila += "<td style='width: 485px;' >" + obj.NombreCompleto + "</td>";
                fila += "</tr>";

                postulantes += fila;
                iPos += 1;
            }
            postulantes += "</tbody></table>";

            String todos = String.Empty;
            iPos = 1;
            foreach (PostulacionEvaluacionCurricular_Registro obj in lstPostulantes)
            {
                fila = String.Empty;
                fila += "<tr>";
                fila += "<td style='width: 15px; text-align: right; ' >" + iPos.ToString() + ". </td>";
                fila += "<td style='width: 100px; text-align: center; ' >" + obj.NroDocumento + "</td>";
                fila += "<td style='width: 485px;' >" + obj.NombreCompleto + "</td>";
                fila += "<td style='width: 100px; text-align: center; ' > " + (obj.AptoTotal == 1 ? "APTO" : "NO APTO") + " </td>";
                fila += "<td style='width: 100px; text-align: center; ' >" + obj.PuntajeTotal.ToString() + "</td>";
                fila += "</tr>";

                todos += fila;
                iPos += 1;
            }

            String aptos = String.Empty;
            iPos = 1;
            foreach (PostulacionEvaluacionCurricular_Registro obj in lstPostulantes) {
                if (obj.AptoTotal == 1) {
                    fila = String.Empty;
                    fila += "<tr>";
                    fila += "<td style='width: 15px; text-align: right; ' >" + iPos.ToString() + ". </td>";
                    fila += "<td style='width: 100px; text-align: center; ' >" + obj.NroDocumento + "</td>";
                    fila += "<td style='width: 485px;' >" + obj.NombreCompleto + "</td>";
                    fila += "<td style='width: 100px; text-align: center; ' > APTO </td>";
                    fila += "<td style='width: 100px; text-align: center; ' >" + obj.PuntajeTotal.ToString() + "</td>";
                    fila += "</tr>";

                    aptos += fila;
                    iPos += 1;
                }
            }
            
            html = html.Replace("//POSTULANTES", postulantes);
            html = html.Replace("//TODOS", todos);
            html = html.Replace("//APTOS", aptos);
            html = html.Replace("//EVALUADOR", oEmpleado.NombreCompleto);
            html = html.Replace("//DNI", oEmpleado.NroDocumento);

            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(html, Server.MapPath("~/temp"));

            MemoryStream pdfStream = new MemoryStream();
            doc.Save(pdfStream);
            pdfStream.Position = 0;
            doc.Close();

            return pdfStream;
        }
        private Stream GenerarAnexoEvaluacionCurricularPracticaPdf(Convocatoria_Registro oConvocatoria, Empleado_Registro oEmpleado, List<PostulacionEvaluacionCurricular_Registro> lstPostulantes)
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

            string html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Convocatoria/evaluacion_anexo07_practica.html"));
            html = html.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            html = html.Replace("//dia", DateTime.Now.Day.ToString());
            html = html.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
            html = html.Replace("//anio", DateTime.Now.Year.ToString());
            html = html.Replace("//horas", System.DateTime.Now.ToString("HH:mm"));
            html = html.Replace("//fecha", System.DateTime.Now.ToString("dd/MM/yyyy"));
            html = html.Replace("//proceso_cas", oConvocatoria.NroConvocatoria);
            html = html.Replace("//cargo", oConvocatoria.NombreCargo.ToUpper());
            //html = html.Replace("//evaluador", objEmpleado.NombreCompleto);
            //html = html.Replace("//dni", objEmpleado.NroDocumento);

            String postulantes = "<table style='font-family: 'PT Sans Narrow', sans-serif; font-size: 11px; width: 500px; border: 0px solid #f0f0f0;' border='0' cellspacing='0' cellpadding='2'>";
            postulantes += "<tbody>";
            String fila = String.Empty;
            Int32 iPos = 1;
            foreach (PostulacionEvaluacionCurricular_Registro obj in lstPostulantes)
            {
                fila = String.Empty;
                fila += "<tr>";
                fila += "<td style='width: 15px; text-align: right; ' >" + iPos.ToString() + ". </td>";
                fila += "<td style='width: 485px;' >" + obj.NombreCompleto + "</td>";
                fila += "</tr>";

                postulantes += fila;
                iPos += 1;
            }
            postulantes += "</tbody></table>";

            String todos = String.Empty;
            iPos = 1;
            foreach (PostulacionEvaluacionCurricular_Registro obj in lstPostulantes)
            {
                fila = String.Empty;
                fila += "<tr>";
                fila += "<td style='width: 15px; text-align: right; ' >" + iPos.ToString() + ". </td>";
                fila += "<td style='width: 100px; text-align: center; ' >" + obj.NroDocumento + "</td>";
                fila += "<td style='width: 485px;' >" + obj.NombreCompleto + "</td>";
                fila += "<td style='width: 100px; text-align: center; ' > " + (obj.AptoTotal == 1 ? "APTO" : "NO APTO") + " </td>";
                fila += "<td style='width: 100px; text-align: center; ' >" + obj.PuntajeTotal.ToString() + "</td>";
                fila += "</tr>";

                todos += fila;
                iPos += 1;
            }

            String aptos = String.Empty;
            iPos = 1;
            foreach (PostulacionEvaluacionCurricular_Registro obj in lstPostulantes)
            {
                if (obj.AptoTotal == 1)
                {
                    fila = String.Empty;
                    fila += "<tr>";
                    fila += "<td style='width: 15px; text-align: right; ' >" + iPos.ToString() + ". </td>";
                    fila += "<td style='width: 100px; text-align: center; ' >" + obj.NroDocumento + "</td>";
                    fila += "<td style='width: 485px;' >" + obj.NombreCompleto + "</td>";
                    fila += "<td style='width: 100px; text-align: center; ' > APTO </td>";
                    fila += "<td style='width: 100px; text-align: center; ' >" + obj.PuntajeTotal.ToString() + "</td>";
                    fila += "</tr>";

                    aptos += fila;
                    iPos += 1;
                }
            }

            html = html.Replace("//POSTULANTES", postulantes);
            html = html.Replace("//TODOS", todos);
            html = html.Replace("//APTOS", aptos);
            html = html.Replace("//EVALUADOR", oEmpleado.NombreCompleto);
            html = html.Replace("//DNI", oEmpleado.NroDocumento);

            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(html, Server.MapPath("~/temp"));

            MemoryStream pdfStream = new MemoryStream();
            doc.Save(pdfStream);
            pdfStream.Position = 0;
            doc.Close();

            return pdfStream;
        }

        //public FileResult DescargarAnexo5()
        //{
        //    Int32 IdConvocatoria = (Request.QueryString.Get("id") == null ? 0 : Int32.Parse(Request.QueryString["id"]));
        //    //Int32 IdTrabajador = (Request.QueryString.Get("idTra") == null ? 0 : Int32.Parse(Request.QueryString["idTra"]));

        //    Convocatoria_Request peticion = new Convocatoria_Request() { IdConvocatoria = IdConvocatoria };
        //    Convocatoria_Registro objConvocatoria = _convocatoria_Servicio.ObtenerPracticaParaEditar(peticion);
        //    //Empleado_Registro objEmpleado = _empleado_Servicio.ObtenerParaEditar(new Empleado_Request() { IdEmpleado = IdTrabajador });
        //    List<PostulacionEntrevistaPersonal_Registro> lstEntrevista = _convocatoria_Servicio.ListarPostulantesPracticaEntrevistaPersonal(peticion).ToList();
        //    List<PostulacionResultadoTotal_Registro> lstTotal = _convocatoria_Servicio.ListarPostulantesPracticaResultadosTotales(peticion).ToList();

        //    List<Int32> data = lstEntrevista.Select(x => x.IdTrabajador).Distinct().ToList();
        //    Empleado_Registro objEvaluador1 = null;
        //    Empleado_Registro objEvaluador2 = null;
        //    if (data.Count > 0)
        //    {
        //        objEvaluador1 = _empleado_Servicio.ObtenerParaEditar(new Empleado_Request() { IdEmpleado = data[0] });
        //        objEvaluador2 = _empleado_Servicio.ObtenerParaEditar(new Empleado_Request() { IdEmpleado = data[1] });
        //    }

        //    Stream pdfStream = GenerarAnexoPracticaResultadoFinalPdf(objConvocatoria, lstEntrevista, lstTotal, objEvaluador1, objEvaluador2);

        //    return File(pdfStream, "application/pdf"); // ("Ficha", "_Layout_Blank", oAsistenciaTecnicaDetalle_Registro);
        //}
        //private Stream GenerarAnexoPracticaResultadoFinalPdf(   Convocatoria_Registro oConvocatoria,
        //                                                        List<PostulacionEntrevistaPersonal_Registro> lstEntrevista,
        //                                                        List<PostulacionResultadoTotal_Registro> lstTotal,
        //                                                        Empleado_Registro objEvaluador1,
        //                                                        Empleado_Registro objEvaluador2)
        //{
        //    HtmlToPdf converter = new HtmlToPdf();
        //    converter.Options.PdfPageSize = PdfPageSize.A4;

        //    converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
        //    converter.Options.WebPageWidth = 1024;
        //    converter.Options.WebPageHeight = 0;
        //    converter.Options.WebPageFixedSize = false;
        //    converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.ShrinkOnly;
        //    converter.Options.AutoFitHeight = HtmlToPdfPageFitMode.NoAdjustment;

        //    converter.Options.MarginLeft = 30;
        //    converter.Options.MarginRight = 30;
        //    converter.Options.MarginTop = 30;
        //    converter.Options.MarginBottom = 30;

        //    String html = String.Empty;
        //    String todos = String.Empty;
        //    String aptos = String.Empty;
            
        //    html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Convocatoria/evaluacion_anexo11_se.html"));
        //    html = html.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
        //    html = html.Replace("//fecha", System.DateTime.Now.ToString("dd/MM/yyyy"));
        //    html = html.Replace("//proceso_cas", oConvocatoria.NroConvocatoria);
        //    html = html.Replace("//cargo", oConvocatoria.NombreCargo.ToUpper());
        //    html = html.Replace("//EVALUADOR1", objEvaluador1.NombreCompleto.ToUpper());
        //    html = html.Replace("//EVALUADOR2", objEvaluador2.NombreCompleto.ToUpper());
        //    html = html.Replace("//DNI1", objEvaluador1.NroDocumento);
        //    html = html.Replace("//DNI2", objEvaluador2.NroDocumento);

        //    //var query = lstEntrevista
        //    //.GroupBy(c => c.NombreEvaluador)
        //    //.Select(g => new {
        //    //    CustId = g.Key,
        //    //    EVALUADOR1 = g.Where(c => c.IdTrabajador == 2561).Sum(c => c.PuntajeTotal),
        //    //    EVALUADOR2 = g.Where(c => c.IdTrabajador == 2271).Sum(c => c.PuntajeTotal)
        //    //});

        //    todos = String.Empty;
        //    String fila = String.Empty;
        //    Int32 iPos = 1;
        //    foreach (PostulacionResultadoTotal_Registro data in lstTotal)
        //    {
        //        if (data.AptoCurricular == 1)
        //        {
        //            fila = String.Empty;
        //            fila += "<tr>";
        //            fila += "<td style='width: 15px; text-align: right; ' >" + iPos.ToString() + ". </td>";
        //            fila += "<td style='width: 85px; text-align: center; ' >" + data.NroDocumento + "</td>";
        //            fila += "<td style='width: 250px; text-align: left; ' >" + data.NombreCompleto + "</td>";
        //            fila += "<td style='width: 250px; text-align: center; ' > " + lstEntrevista.Find(x => x.IdTrabajador == objEvaluador1.IdEmpleado && x.IdPostulacion == data.IdPostulacion).PuntajeTotal.ToString("N0");
        //            fila += "<td style='width: 250px; text-align: center; ' > " + lstEntrevista.Find(x => x.IdTrabajador == objEvaluador2.IdEmpleado && x.IdPostulacion == data.IdPostulacion).PuntajeTotal.ToString("N0");
        //            fila += "<td style='width: 100px; text-align: center; ' >" + data.PuntajeEntrevista.ToString("N0") + "</td>";
        //            fila += "</tr>";

        //            todos += fila;
        //            iPos += 1;
        //        }
        //    }

        //    aptos = String.Empty;
        //    iPos = 1;
        //    foreach (PostulacionResultadoTotal_Registro obj in lstTotal)
        //    {
        //        if (obj.AptoCurricular == 1)
        //        {
        //            fila = String.Empty;
        //            fila += "<tr>";
        //            fila += "<td style='width: 15px; text-align: right; ' >" + iPos.ToString() + ". </td>";
        //            fila += "<td style='width: 85px; text-align: center; ' >" + obj.NroDocumento + "</td>";
        //            fila += "<td style='width: 250px;' >" + obj.NombreCompleto + "</td>";
        //            fila += "<td style='width: 250px; text-align: center; ' > " + obj.PuntajeCurricular.ToString("N0") + " </td>";
        //            fila += "<td style='width: 250px; text-align: center; ' > " + obj.PuntajeEntrevista.ToString("N0") + " </td>";
        //            fila += "<td style='width: 100px; text-align: center; ' >" + obj.PuntajeTotal.ToString("N0") + "</td>";
        //            fila += "</tr>";

        //            aptos += fila;
        //            iPos += 1;
        //        }
        //    }

            
        //    //html = html.Replace("//POSTULANTES", postulantes);
        //    html = html.Replace("//TODOS", todos);
        //    html = html.Replace("//APTOS", aptos);
        //    //html = html.Replace("//EVALUADOR", oEmpleado.NombreCompleto);
        //    //html = html.Replace("//DNI", oEmpleado.NroDocumento);

        //    SelectPdf.PdfDocument doc = converter.ConvertHtmlString(html, Server.MapPath("~/temp"));

        //    MemoryStream pdfStream = new MemoryStream();
        //    doc.Save(pdfStream);
        //    pdfStream.Position = 0;
        //    doc.Close();

        //    return pdfStream;
        //}

        public FileResult DescargarAnexo11()
        {
            Int32 IdConvocatoria = (Request.QueryString.Get("id") == null ? 0 : Int32.Parse(Request.QueryString["id"]));
            //Int32 IdTrabajador = (Request.QueryString.Get("idTra") == null ? 0 : Int32.Parse(Request.QueryString["idTra"]));

            Convocatoria_Request peticion = new Convocatoria_Request() { IdConvocatoria = IdConvocatoria };
            Convocatoria_Registro objConvocatoria = _convocatoria_Servicio.ObtenerParaEditar(peticion);
            //Empleado_Registro objEmpleado = _empleado_Servicio.ObtenerParaEditar(new Empleado_Request() { IdEmpleado = IdTrabajador });
            List<PostulacionEntrevistaPersonal_Registro> lstEntrevista = _convocatoria_Servicio.ListarPostulantesEntrevistaPersonal(peticion).ToList();
            List<PostulacionResultadoTotal_Registro> lstTotal = _convocatoria_Servicio.ListarPostulantesResultadosTotales(peticion).ToList();

            List<Int32> data = lstEntrevista.Select(x => x.IdTrabajador).Distinct().ToList();
            Empleado_Registro objEvaluador1 = null;
            Empleado_Registro objEvaluador2 = null; 
            if (data.Count > 0) {
                objEvaluador1 = _empleado_Servicio.ObtenerParaEditar(new Empleado_Request() { IdEmpleado = data[0] });
                objEvaluador2 = _empleado_Servicio.ObtenerParaEditar(new Empleado_Request() { IdEmpleado = data[1] });
            }

            Stream pdfStream = GenerarAnexoResultadoFinalPdf(objConvocatoria, lstEntrevista, lstTotal, objEvaluador1, objEvaluador2);

            return File(pdfStream, "application/pdf"); // ("Ficha", "_Layout_Blank", oAsistenciaTecnicaDetalle_Registro);
        }
        public FileResult DescargarAnexo11Practica()
        {
            Int32 IdConvocatoria = (Request.QueryString.Get("id") == null ? 0 : Int32.Parse(Request.QueryString["id"]));
            //Int32 IdTrabajador = (Request.QueryString.Get("idTra") == null ? 0 : Int32.Parse(Request.QueryString["idTra"]));

            Convocatoria_Request peticion = new Convocatoria_Request() { IdConvocatoria = IdConvocatoria };
            Convocatoria_Registro objConvocatoria = _convocatoria_Servicio.ObtenerPracticaParaEditar(peticion);
            //Empleado_Registro objEmpleado = _empleado_Servicio.ObtenerParaEditar(new Empleado_Request() { IdEmpleado = IdTrabajador });
            List<PostulacionEntrevistaPersonal_Registro> lstEntrevista = _convocatoria_Servicio.ListarPostulantesPracticaEntrevistaPersonal(peticion).ToList();
            List<PostulacionResultadoTotal_Registro> lstTotal = _convocatoria_Servicio.ListarPostulantesPracticaResultadosTotales(peticion).ToList();

            List<Int32> data = lstEntrevista.Select(x => x.IdTrabajador).Distinct().ToList();
            Empleado_Registro objEvaluador1 = null;
            Empleado_Registro objEvaluador2 = null;
            if (data.Count > 0)
            {
                objEvaluador1 = _empleado_Servicio.ObtenerParaEditar(new Empleado_Request() { IdEmpleado = data[0] });
                objEvaluador2 = _empleado_Servicio.ObtenerParaEditar(new Empleado_Request() { IdEmpleado = data[1] });
            }

            Stream pdfStream = GenerarAnexoPracticaResultadoFinalPdf(objConvocatoria, lstEntrevista, lstTotal, objEvaluador1, objEvaluador2);

            return File(pdfStream, "application/pdf"); // ("Ficha", "_Layout_Blank", oAsistenciaTecnicaDetalle_Registro);
        }
        private Stream GenerarAnexoResultadoFinalPdf(   Convocatoria_Registro oConvocatoria, 
                                                        List<PostulacionEntrevistaPersonal_Registro> lstEntrevista, 
                                                        List<PostulacionResultadoTotal_Registro> lstTotal,
                                                        Empleado_Registro objEvaluador1,
                                                        Empleado_Registro objEvaluador2)
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

            String html = String.Empty;
            String todos = String.Empty;
            String aptos = String.Empty;
            if (oConvocatoria.IdTieneExamenConoc == 1) {
                html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Convocatoria/evaluacion_anexo11_ce.html"));
                html = html.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
                //html = html.Replace("//dia", DateTime.Now.Day.ToString());
                //html = html.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
                //html = html.Replace("//anio", DateTime.Now.Year.ToString());
                //html = html.Replace("//horas", System.DateTime.Now.ToString("HH:mm"));
                html = html.Replace("//fecha", System.DateTime.Now.ToString("dd/MM/yyyy"));
                html = html.Replace("//proceso_cas", oConvocatoria.NroConvocatoria);
                html = html.Replace("//cargo", oConvocatoria.NombreCargo.ToUpper());
                html = html.Replace("//EVALUADOR1", objEvaluador1.NombreCompleto.ToUpper());
                html = html.Replace("//EVALUADOR2", objEvaluador2.NombreCompleto.ToUpper());
                html = html.Replace("//DNI1", objEvaluador1.NroDocumento);
                html = html.Replace("//DNI2", objEvaluador2.NroDocumento);

                todos = String.Empty;
                String fila = String.Empty;
                Int32 iPos = 1;
                foreach (PostulacionResultadoTotal_Registro data in lstTotal)
                {
                    if (data.AptoCurricular == 1 && data.AptoConocimiento == 1)
                    {
                        fila = String.Empty;
                        fila += "<tr>";
                        fila += "<td style='width: 15px; text-align: right; ' >" + iPos.ToString() + ". </td>";
                        fila += "<td style='width: 85px; text-align: center; ' >" + data.NroDocumento + "</td>";
                        fila += "<td style='width: 250px; text-align: left; ' >" + data.NombreCompleto + "</td>";
                        fila += "<td style='width: 250px; text-align: center; ' > " + lstEntrevista.Find(x => x.IdTrabajador == objEvaluador1.IdEmpleado && x.IdPostulacion == data.IdPostulacion).PuntajeTotal.ToString("N0");
                        fila += "<td style='width: 250px; text-align: center; ' > " + lstEntrevista.Find(x => x.IdTrabajador == objEvaluador2.IdEmpleado && x.IdPostulacion == data.IdPostulacion).PuntajeTotal.ToString("N0");
                        fila += "<td style='width: 100px; text-align: center; ' >" + data.PuntajeEntrevista.ToString("N0") + "</td>";
                        fila += "</tr>";

                        todos += fila;
                        iPos += 1;
                    }
                }

                aptos = String.Empty;
                iPos = 1;
                foreach (PostulacionResultadoTotal_Registro obj in lstTotal)
                {
                    if (obj.AptoCurricular == 1 && obj.AptoConocimiento == 1)
                    {
                        fila = String.Empty;
                        fila += "<tr>";
                        fila += "<td style='width: 15px; text-align: right; ' >" + iPos.ToString() + ". </td>";
                        fila += "<td style='width: 85px; text-align: center; ' >" + obj.NroDocumento + "</td>";
                        fila += "<td style='width: 187.5px;' >" + obj.NombreCompleto + "</td>";
                        fila += "<td style='width: 187.5px; text-align: center; ' > " + obj.PuntajeCurricular.ToString("N0") + " </td>";
                        fila += "<td style='width: 187.5px; text-align: center; ' > " + obj.PuntajeConocimiento.ToString("N0") + " </td>";
                        fila += "<td style='width: 187.5px; text-align: center; ' > " + obj.PuntajeEntrevista.ToString("N0") + " </td>";
                        fila += "<td style='width: 100px; text-align: center; ' >" + obj.PuntajeTotal.ToString("N0") + "</td>";
                        fila += "</tr>";

                        aptos += fila;
                        iPos += 1;
                    }
                }
            }
            else {
                html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Convocatoria/evaluacion_anexo11_se.html"));
                html = html.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
                html = html.Replace("//fecha", System.DateTime.Now.ToString("dd/MM/yyyy"));
                html = html.Replace("//proceso_cas", oConvocatoria.NroConvocatoria);
                html = html.Replace("//cargo", oConvocatoria.NombreCargo.ToUpper());
                html = html.Replace("//EVALUADOR1", objEvaluador1.NombreCompleto.ToUpper());
                html = html.Replace("//EVALUADOR2", objEvaluador2.NombreCompleto.ToUpper());
                html = html.Replace("//DNI1", objEvaluador1.NroDocumento);
                html = html.Replace("//DNI2", objEvaluador2.NroDocumento);

                //var query = lstEntrevista
                //.GroupBy(c => c.NombreEvaluador)
                //.Select(g => new {
                //    CustId = g.Key,
                //    EVALUADOR1 = g.Where(c => c.IdTrabajador == 2561).Sum(c => c.PuntajeTotal),
                //    EVALUADOR2 = g.Where(c => c.IdTrabajador == 2271).Sum(c => c.PuntajeTotal)
                //});

                todos = String.Empty;
                String fila = String.Empty;
                Int32 iPos = 1;
                foreach (PostulacionResultadoTotal_Registro data in lstTotal)
                {
                    if (data.AptoCurricular == 1 )
                    {
                        fila = String.Empty;
                        fila += "<tr>";
                        fila += "<td style='width: 15px; text-align: right; ' >" + iPos.ToString() + ". </td>";
                        fila += "<td style='width: 85px; text-align: center; ' >" + data.NroDocumento + "</td>";
                        fila += "<td style='width: 250px; text-align: left; ' >" + data.NombreCompleto + "</td>";
                        fila += "<td style='width: 250px; text-align: center; ' > " + lstEntrevista.Find(x => x.IdTrabajador == objEvaluador1.IdEmpleado && x.IdPostulacion == data.IdPostulacion).PuntajeTotal.ToString("N0");
                        fila += "<td style='width: 250px; text-align: center; ' > " + lstEntrevista.Find(x => x.IdTrabajador == objEvaluador2.IdEmpleado && x.IdPostulacion == data.IdPostulacion).PuntajeTotal.ToString("N0");
                        fila += "<td style='width: 100px; text-align: center; ' >" + data.PuntajeEntrevista.ToString("N0") + "</td>";
                        fila += "</tr>";

                        todos += fila;
                        iPos += 1;
                    }                        
                }

                aptos = String.Empty;
                iPos = 1;
                foreach (PostulacionResultadoTotal_Registro obj in lstTotal)
                {
                    if (obj.AptoCurricular == 1)
                    {
                        fila = String.Empty;
                        fila += "<tr>";
                        fila += "<td style='width: 15px; text-align: right; ' >" + iPos.ToString() + ". </td>";
                        fila += "<td style='width: 85px; text-align: center; ' >" + obj.NroDocumento + "</td>";
                        fila += "<td style='width: 250px;' >" + obj.NombreCompleto + "</td>";
                        fila += "<td style='width: 250px; text-align: center; ' > " + obj.PuntajeCurricular.ToString("N0") + " </td>";
                        fila += "<td style='width: 250px; text-align: center; ' > " + obj.PuntajeEntrevista.ToString("N0") + " </td>";
                        fila += "<td style='width: 100px; text-align: center; ' >" + obj.PuntajeTotal.ToString("N0") + "</td>";
                        fila += "</tr>";

                        aptos += fila;
                        iPos += 1;
                    }
                }

            }
            //html = html.Replace("//evaluador", objEmpleado.NombreCompleto);
            //html = html.Replace("//dni", objEmpleado.NroDocumento);
            //lstEntrevista[0].PuntajeTotal

            

            

            //html = html.Replace("//POSTULANTES", postulantes);
            html = html.Replace("//TODOS", todos);
            html = html.Replace("//APTOS", aptos);
            //html = html.Replace("//EVALUADOR", oEmpleado.NombreCompleto);
            //html = html.Replace("//DNI", oEmpleado.NroDocumento);

            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(html, Server.MapPath("~/temp"));

            MemoryStream pdfStream = new MemoryStream();
            doc.Save(pdfStream);
            pdfStream.Position = 0;
            doc.Close();

            return pdfStream;
        }

        private Stream GenerarAnexoPracticaResultadoFinalPdf(Convocatoria_Registro oConvocatoria,
                                                        List<PostulacionEntrevistaPersonal_Registro> lstEntrevista,
                                                        List<PostulacionResultadoTotal_Registro> lstTotal,
                                                        Empleado_Registro objEvaluador1,
                                                        Empleado_Registro objEvaluador2)
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

            String html = String.Empty;
            String todos = String.Empty;
            String aptos = String.Empty;
            html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Convocatoria/evaluacion_anexo11_practica.html"));
            html = html.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            html = html.Replace("//fecha", System.DateTime.Now.ToString("dd/MM/yyyy"));
            html = html.Replace("//proceso_cas", oConvocatoria.NroConvocatoria);
            html = html.Replace("//cargo", oConvocatoria.NombreCargo.ToUpper());
            html = html.Replace("//EVALUADOR1", objEvaluador1.NombreCompleto.ToUpper());
            html = html.Replace("//EVALUADOR2", objEvaluador2.NombreCompleto.ToUpper());
            html = html.Replace("//DNI1", objEvaluador1.NroDocumento);
            html = html.Replace("//DNI2", objEvaluador2.NroDocumento);

            todos = String.Empty;
            String fila = String.Empty;
            Int32 iPos = 1;
            foreach (PostulacionResultadoTotal_Registro data in lstTotal)
            {
                if (data.AptoCurricular == 1)
                {
                    fila = String.Empty;
                    fila += "<tr>";
                    fila += "<td style='width: 15px; text-align: right; ' >" + iPos.ToString() + ". </td>";
                    fila += "<td style='width: 85px; text-align: center; ' >" + data.NroDocumento + "</td>";
                    fila += "<td style='width: 250px; text-align: left; ' >" + data.NombreCompleto + "</td>";
                    if (data.PresentoEntrevista == 1)
                    {
                        fila += "<td style='width: 250px; text-align: center; ' > " + lstEntrevista.Find(x => x.IdTrabajador == objEvaluador1.IdEmpleado && x.IdPostulacion == data.IdPostulacion).PuntajeTotal.ToString("N0") + "</td>";
                        fila += "<td style='width: 250px; text-align: center; ' > " + lstEntrevista.Find(x => x.IdTrabajador == objEvaluador2.IdEmpleado && x.IdPostulacion == data.IdPostulacion).PuntajeTotal.ToString("N0") + "</td>";
                        fila += "<td style='width: 100px; text-align: center; ' >" + data.PuntajeEntrevista.ToString("N0") + "</td>";
                    }
                    else {
                        fila += "<td style='width: 250px; text-align: center; ' > NSP " + "</td>";
                        fila += "<td style='width: 250px; text-align: center; ' > NSP " + "</td>";
                        fila += "<td style='width: 100px; text-align: center; ' > NSP " + "</td>";
                    }
                    fila += "</tr>";

                    todos += fila;
                    iPos += 1;
                }
            }

            aptos = String.Empty;
            iPos = 1;
            foreach (PostulacionResultadoTotal_Registro obj in lstTotal)
            {
                if (obj.AptoCurricular == 1)
                {
                    fila = String.Empty;
                    fila += "<tr>";
                    fila += "<td style='width: 15px; text-align: right; ' >" + iPos.ToString() + ". </td>";
                    fila += "<td style='width: 85px; text-align: center; ' >" + obj.NroDocumento + "</td>";
                    fila += "<td style='width: 250px;' >" + obj.NombreCompleto + "</td>";
                    fila += "<td style='width: 175px; text-align: center; ' > " + obj.PuntajeCurricular.ToString("N0") + " </td>";

                    if (obj.PresentoEntrevista == 1)
                    {
                        fila += "<td style='width: 175px; text-align: center; ' > " + obj.PuntajeEntrevista.ToString("N0") + " </td>";
                        fila += "<td style='width: 100px; text-align: center; ' >" + obj.PuntajeTotal.ToString("N0") + "</td>";
                    }
                    else {
                        fila += "<td style='width: 175px; text-align: center; ' > NSP </td>";
                        fila += "<td style='width: 100px; text-align: center; ' > -- </td>";
                    }
                        
                    if (obj.AptoGanador == 1 && obj.Posicion <= obj.Vacantes)
                        fila += "<td style='width: 150px; text-align: center;'>GANADOR(A)</td>";
                    else if (obj.AptoGanador == 1 && obj.Posicion - 1 == obj.Vacantes)
                        fila += "<td style='width: 150px; text-align: center;'>ACCESITARIO(A)</td>";
                    else
                        fila += "<td style='width: 150px; text-align: center;'>DESCALIFICADO(A)</td>";
                    fila += "</tr>";

                    aptos += fila;
                    iPos += 1;
                }
            }

            
            //html = html.Replace("//POSTULANTES", postulantes);
            html = html.Replace("//TODOS", todos);
            html = html.Replace("//APTOS", aptos);
            //html = html.Replace("//EVALUADOR", oEmpleado.NombreCompleto);
            //html = html.Replace("//DNI", oEmpleado.NroDocumento);

            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(html, Server.MapPath("~/temp"));

            MemoryStream pdfStream = new MemoryStream();
            doc.Save(pdfStream);
            pdfStream.Position = 0;
            doc.Close();

            return pdfStream;
        }

        public FileResult DescargarFormatoEntrevista()
        {
            Int32 IdEvaluacion = (Request.QueryString.Get("id") == null ? 0 : Int32.Parse(Request.QueryString["id"]));
            Int32 IdTrabajador = (Request.QueryString.Get("idTra") == null ? 0 : Int32.Parse(Request.QueryString["idTra"]));
            Int32 IdExamen = (Request.QueryString.Get("idEx") == null ? 0 : Int32.Parse(Request.QueryString["idEx"]));
            Int32 IdPresento = (Request.QueryString.Get("idPre") == null ? 0 : Int32.Parse(Request.QueryString["idPre"]));

            PostulacionEvaluacionEntrevista_Registro peticion = new PostulacionEvaluacionEntrevista_Registro() { IdEvaluacion = IdEvaluacion };
            PostulacionEvaluacionEntrevista_Registro objEvaluacion = _convocatoria_Servicio.ObtenerInformacionEntrevistaPersonal(peticion);
            Empleado_Registro objEmpleado = _empleado_Servicio.ObtenerParaEditar(new Empleado_Request() { IdEmpleado = IdTrabajador });
            List<PostulacionEntrevistaPersonalPregunta_Registro> lista = _convocatoria_Servicio.ListarEntrevistaPersonalPreguntas(peticion).ToList();
            //Persona_Registro objPersona = new T_genm_persona_LN().Listar(new Persona_Request() { IdPersona = IdTrabajador });

            Stream pdfStream = GenerarFormatoEntrevistaPdf(objEvaluacion, objEmpleado, lista, IdExamen, IdPresento);

            return File(pdfStream, "application/pdf"); // ("Ficha", "_Layout_Blank", oAsistenciaTecnicaDetalle_Registro);
        }
        private Stream GenerarFormatoEntrevistaPdf(PostulacionEvaluacionEntrevista_Registro oEvaluacion, Empleado_Registro oEmpleado,
                                                    List<PostulacionEntrevistaPersonalPregunta_Registro> lista, Int32 IdExamen, Int32 IdPresento)
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

            string html = String.Empty;
            if (IdPresento == 1)
            {
                if (IdExamen == 0)
                {
                    //SIN EXAMEN
                    html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Convocatoria/evaluacion_entrevista_se.html"));

                    switch (oEvaluacion.PuntajeAspecto1)
                    {
                        case 1:
                            html = html.Replace("//P1_5", oEvaluacion.PuntajeAspecto1.ToString());
                            html = html.Replace("//P1_4", String.Empty);
                            html = html.Replace("//P1_3", String.Empty);
                            html = html.Replace("//P1_2", String.Empty);
                            html = html.Replace("//P1_1", String.Empty);
                            html = html.Replace("//P1_T", oEvaluacion.PuntajeAspecto1.ToString());
                            break;
                        case 2:
                            html = html.Replace("//P1_5", oEvaluacion.PuntajeAspecto1.ToString());
                            html = html.Replace("//P1_4", String.Empty);
                            html = html.Replace("//P1_3", String.Empty);
                            html = html.Replace("//P1_2", String.Empty);
                            html = html.Replace("//P1_1", String.Empty);
                            html = html.Replace("//P1_T", oEvaluacion.PuntajeAspecto1.ToString());
                            break;
                        case 3:
                            html = html.Replace("//P1_5", oEvaluacion.PuntajeAspecto1.ToString());
                            html = html.Replace("//P1_4", String.Empty);
                            html = html.Replace("//P1_3", String.Empty);
                            html = html.Replace("//P1_2", String.Empty);
                            html = html.Replace("//P1_1", String.Empty);
                            html = html.Replace("//P1_T", oEvaluacion.PuntajeAspecto1.ToString());
                            break;
                        case 4:
                            html = html.Replace("//P1_4", oEvaluacion.PuntajeAspecto1.ToString());
                            html = html.Replace("//P1_5", String.Empty);
                            html = html.Replace("//P1_3", String.Empty);
                            html = html.Replace("//P1_2", String.Empty);
                            html = html.Replace("//P1_1", String.Empty);
                            html = html.Replace("//P1_T", oEvaluacion.PuntajeAspecto1.ToString());
                            break;
                        case 5:
                            html = html.Replace("//P1_4", oEvaluacion.PuntajeAspecto1.ToString());
                            html = html.Replace("//P1_5", String.Empty);
                            html = html.Replace("//P1_3", String.Empty);
                            html = html.Replace("//P1_2", String.Empty);
                            html = html.Replace("//P1_1", String.Empty);
                            html = html.Replace("//P1_T", oEvaluacion.PuntajeAspecto1.ToString());
                            break;
                        case 6:
                            html = html.Replace("//P1_4", oEvaluacion.PuntajeAspecto1.ToString());
                            html = html.Replace("//P1_5", String.Empty);
                            html = html.Replace("//P1_3", String.Empty);
                            html = html.Replace("//P1_2", String.Empty);
                            html = html.Replace("//P1_1", String.Empty);
                            html = html.Replace("//P1_T", oEvaluacion.PuntajeAspecto1.ToString());
                            break;
                        case 7:
                            html = html.Replace("//P1_3", oEvaluacion.PuntajeAspecto1.ToString());
                            html = html.Replace("//P1_5", String.Empty);
                            html = html.Replace("//P1_4", String.Empty);
                            html = html.Replace("//P1_2", String.Empty);
                            html = html.Replace("//P1_1", String.Empty);
                            html = html.Replace("//P1_T", oEvaluacion.PuntajeAspecto1.ToString());
                            break;
                        case 8:
                            html = html.Replace("//P1_3", oEvaluacion.PuntajeAspecto1.ToString());
                            html = html.Replace("//P1_5", String.Empty);
                            html = html.Replace("//P1_4", String.Empty);
                            html = html.Replace("//P1_2", String.Empty);
                            html = html.Replace("//P1_1", String.Empty);
                            html = html.Replace("//P1_T", oEvaluacion.PuntajeAspecto1.ToString());
                            break;
                        case 9:
                            html = html.Replace("//P1_2", oEvaluacion.PuntajeAspecto1.ToString());
                            html = html.Replace("//P1_5", String.Empty);
                            html = html.Replace("//P1_4", String.Empty);
                            html = html.Replace("//P1_3", String.Empty);
                            html = html.Replace("//P1_1", String.Empty);
                            html = html.Replace("//P1_T", oEvaluacion.PuntajeAspecto1.ToString());
                            break;
                        case 10:
                            html = html.Replace("//P1_1", oEvaluacion.PuntajeAspecto1.ToString());
                            html = html.Replace("//P1_5", String.Empty);
                            html = html.Replace("//P1_4", String.Empty);
                            html = html.Replace("//P1_3", String.Empty);
                            html = html.Replace("//P1_2", String.Empty);
                            html = html.Replace("//P1_T", oEvaluacion.PuntajeAspecto1.ToString());
                            break;
                    }
                    switch (oEvaluacion.PuntajeAspecto2)
                    {
                        case 1:
                            html = html.Replace("//P2_5", oEvaluacion.PuntajeAspecto2.ToString());
                            html = html.Replace("//P2_4", String.Empty);
                            html = html.Replace("//P2_3", String.Empty);
                            html = html.Replace("//P2_2", String.Empty);
                            html = html.Replace("//P2_1", String.Empty);
                            html = html.Replace("//P2_T", oEvaluacion.PuntajeAspecto2.ToString());
                            break;
                        case 2:
                            html = html.Replace("//P2_5", oEvaluacion.PuntajeAspecto2.ToString());
                            html = html.Replace("//P2_4", String.Empty);
                            html = html.Replace("//P2_3", String.Empty);
                            html = html.Replace("//P2_2", String.Empty);
                            html = html.Replace("//P2_1", String.Empty);
                            html = html.Replace("//P2_T", oEvaluacion.PuntajeAspecto2.ToString());
                            break;
                        case 3:
                            html = html.Replace("//P2_5", oEvaluacion.PuntajeAspecto2.ToString());
                            html = html.Replace("//P2_4", String.Empty);
                            html = html.Replace("//P2_3", String.Empty);
                            html = html.Replace("//P2_2", String.Empty);
                            html = html.Replace("//P2_1", String.Empty);
                            html = html.Replace("//P2_T", oEvaluacion.PuntajeAspecto2.ToString());
                            break;
                        case 4:
                            html = html.Replace("//P2_4", oEvaluacion.PuntajeAspecto2.ToString());
                            html = html.Replace("//P2_5", String.Empty);
                            html = html.Replace("//P2_3", String.Empty);
                            html = html.Replace("//P2_2", String.Empty);
                            html = html.Replace("//P2_1", String.Empty);
                            html = html.Replace("//P2_T", oEvaluacion.PuntajeAspecto2.ToString());
                            break;
                        case 5:
                            html = html.Replace("//P2_4", oEvaluacion.PuntajeAspecto2.ToString());
                            html = html.Replace("//P2_5", String.Empty);
                            html = html.Replace("//P2_3", String.Empty);
                            html = html.Replace("//P2_2", String.Empty);
                            html = html.Replace("//P2_1", String.Empty);
                            html = html.Replace("//P2_T", oEvaluacion.PuntajeAspecto2.ToString());
                            break;
                        case 6:
                            html = html.Replace("//P2_4", oEvaluacion.PuntajeAspecto2.ToString());
                            html = html.Replace("//P2_5", String.Empty);
                            html = html.Replace("//P2_3", String.Empty);
                            html = html.Replace("//P2_2", String.Empty);
                            html = html.Replace("//P2_1", String.Empty);
                            html = html.Replace("//P2_T", oEvaluacion.PuntajeAspecto2.ToString());
                            break;
                        case 7:
                            html = html.Replace("//P2_3", oEvaluacion.PuntajeAspecto2.ToString());
                            html = html.Replace("//P2_5", String.Empty);
                            html = html.Replace("//P2_4", String.Empty);
                            html = html.Replace("//P2_2", String.Empty);
                            html = html.Replace("//P2_1", String.Empty);
                            html = html.Replace("//P2_T", oEvaluacion.PuntajeAspecto2.ToString());
                            break;
                        case 8:
                            html = html.Replace("//P2_3", oEvaluacion.PuntajeAspecto2.ToString());
                            html = html.Replace("//P2_5", String.Empty);
                            html = html.Replace("//P2_4", String.Empty);
                            html = html.Replace("//P2_2", String.Empty);
                            html = html.Replace("//P2_1", String.Empty);
                            html = html.Replace("//P2_T", oEvaluacion.PuntajeAspecto2.ToString());
                            break;
                        case 9:
                            html = html.Replace("//P2_2", oEvaluacion.PuntajeAspecto2.ToString());
                            html = html.Replace("//P2_5", String.Empty);
                            html = html.Replace("//P2_4", String.Empty);
                            html = html.Replace("//P2_3", String.Empty);
                            html = html.Replace("//P2_1", String.Empty);
                            html = html.Replace("//P2_T", oEvaluacion.PuntajeAspecto2.ToString());
                            break;
                        case 10:
                            html = html.Replace("//P2_1", oEvaluacion.PuntajeAspecto2.ToString());
                            html = html.Replace("//P2_5", String.Empty);
                            html = html.Replace("//P2_4", String.Empty);
                            html = html.Replace("//P2_3", String.Empty);
                            html = html.Replace("//P2_2", String.Empty);
                            html = html.Replace("//P2_T", oEvaluacion.PuntajeAspecto2.ToString());
                            break;
                    }
                    switch (oEvaluacion.PuntajeAspecto3)
                    {
                        case 1:
                            html = html.Replace("//P3_5", oEvaluacion.PuntajeAspecto3.ToString());
                            html = html.Replace("//P3_4", String.Empty);
                            html = html.Replace("//P3_3", String.Empty);
                            html = html.Replace("//P3_2", String.Empty);
                            html = html.Replace("//P3_1", String.Empty);
                            html = html.Replace("//P3_T", oEvaluacion.PuntajeAspecto3.ToString());
                            break;
                        case 2:
                            html = html.Replace("//P3_5", oEvaluacion.PuntajeAspecto3.ToString());
                            html = html.Replace("//P3_4", String.Empty);
                            html = html.Replace("//P3_3", String.Empty);
                            html = html.Replace("//P3_2", String.Empty);
                            html = html.Replace("//P3_1", String.Empty);
                            html = html.Replace("//P3_T", oEvaluacion.PuntajeAspecto3.ToString());
                            break;
                        case 3:
                            html = html.Replace("//P3_5", oEvaluacion.PuntajeAspecto3.ToString());
                            html = html.Replace("//P3_4", String.Empty);
                            html = html.Replace("//P3_3", String.Empty);
                            html = html.Replace("//P3_2", String.Empty);
                            html = html.Replace("//P3_1", String.Empty);
                            html = html.Replace("//P3_T", oEvaluacion.PuntajeAspecto3.ToString());
                            break;
                        case 4:
                            html = html.Replace("//P3_4", oEvaluacion.PuntajeAspecto3.ToString());
                            html = html.Replace("//P3_5", String.Empty);
                            html = html.Replace("//P3_3", String.Empty);
                            html = html.Replace("//P3_2", String.Empty);
                            html = html.Replace("//P3_1", String.Empty);
                            html = html.Replace("//P3_T", oEvaluacion.PuntajeAspecto3.ToString());
                            break;
                        case 5:
                            html = html.Replace("//P3_4", oEvaluacion.PuntajeAspecto3.ToString());
                            html = html.Replace("//P3_5", String.Empty);
                            html = html.Replace("//P3_3", String.Empty);
                            html = html.Replace("//P3_2", String.Empty);
                            html = html.Replace("//P3_1", String.Empty);
                            html = html.Replace("//P3_T", oEvaluacion.PuntajeAspecto3.ToString());
                            break;
                        case 6:
                            html = html.Replace("//P3_4", oEvaluacion.PuntajeAspecto3.ToString());
                            html = html.Replace("//P3_5", String.Empty);
                            html = html.Replace("//P3_3", String.Empty);
                            html = html.Replace("//P3_2", String.Empty);
                            html = html.Replace("//P3_1", String.Empty);
                            html = html.Replace("//P3_T", oEvaluacion.PuntajeAspecto3.ToString());
                            break;
                        case 7:
                            html = html.Replace("//P3_3", oEvaluacion.PuntajeAspecto3.ToString());
                            html = html.Replace("//P3_5", String.Empty);
                            html = html.Replace("//P3_4", String.Empty);
                            html = html.Replace("//P3_2", String.Empty);
                            html = html.Replace("//P3_1", String.Empty);
                            html = html.Replace("//P3_T", oEvaluacion.PuntajeAspecto3.ToString());
                            break;
                        case 8:
                            html = html.Replace("//P3_3", oEvaluacion.PuntajeAspecto3.ToString());
                            html = html.Replace("//P3_5", String.Empty);
                            html = html.Replace("//P3_4", String.Empty);
                            html = html.Replace("//P3_2", String.Empty);
                            html = html.Replace("//P3_1", String.Empty);
                            html = html.Replace("//P3_T", oEvaluacion.PuntajeAspecto3.ToString());
                            break;
                        case 9:
                            html = html.Replace("//P3_2", oEvaluacion.PuntajeAspecto3.ToString());
                            html = html.Replace("//P3_5", String.Empty);
                            html = html.Replace("//P3_4", String.Empty);
                            html = html.Replace("//P3_3", String.Empty);
                            html = html.Replace("//P3_1", String.Empty);
                            html = html.Replace("//P3_T", oEvaluacion.PuntajeAspecto3.ToString());
                            break;
                        case 10:
                            html = html.Replace("//P3_1", oEvaluacion.PuntajeAspecto3.ToString());
                            html = html.Replace("//P3_5", String.Empty);
                            html = html.Replace("//P3_4", String.Empty);
                            html = html.Replace("//P3_3", String.Empty);
                            html = html.Replace("//P3_2", String.Empty);
                            html = html.Replace("//P3_T", oEvaluacion.PuntajeAspecto3.ToString());
                            break;
                    }
                    switch (oEvaluacion.PuntajeAspecto4)
                    {
                        case 1:
                            html = html.Replace("//P4_5", oEvaluacion.PuntajeAspecto4.ToString());
                            html = html.Replace("//P4_4", String.Empty);
                            html = html.Replace("//P4_3", String.Empty);
                            html = html.Replace("//P4_2", String.Empty);
                            html = html.Replace("//P4_1", String.Empty);
                            html = html.Replace("//P4_T", oEvaluacion.PuntajeAspecto4.ToString());
                            break;
                        case 2:
                            html = html.Replace("//P4_5", oEvaluacion.PuntajeAspecto4.ToString());
                            html = html.Replace("//P4_4", String.Empty);
                            html = html.Replace("//P4_3", String.Empty);
                            html = html.Replace("//P4_2", String.Empty);
                            html = html.Replace("//P4_1", String.Empty);
                            html = html.Replace("//P4_T", oEvaluacion.PuntajeAspecto4.ToString());
                            break;
                        case 3:
                            html = html.Replace("//P4_5", oEvaluacion.PuntajeAspecto4.ToString());
                            html = html.Replace("//P4_4", String.Empty);
                            html = html.Replace("//P4_3", String.Empty);
                            html = html.Replace("//P4_2", String.Empty);
                            html = html.Replace("//P4_1", String.Empty);
                            html = html.Replace("//P4_T", oEvaluacion.PuntajeAspecto4.ToString());
                            break;
                        case 4:
                            html = html.Replace("//P4_4", oEvaluacion.PuntajeAspecto4.ToString());
                            html = html.Replace("//P4_5", String.Empty);
                            html = html.Replace("//P4_3", String.Empty);
                            html = html.Replace("//P4_2", String.Empty);
                            html = html.Replace("//P4_1", String.Empty);
                            html = html.Replace("//P4_T", oEvaluacion.PuntajeAspecto4.ToString());
                            break;
                        case 5:
                            html = html.Replace("//P4_4", oEvaluacion.PuntajeAspecto4.ToString());
                            html = html.Replace("//P4_5", String.Empty);
                            html = html.Replace("//P4_3", String.Empty);
                            html = html.Replace("//P4_2", String.Empty);
                            html = html.Replace("//P4_1", String.Empty);
                            html = html.Replace("//P4_T", oEvaluacion.PuntajeAspecto4.ToString());
                            break;
                        case 6:
                            html = html.Replace("//P4_4", oEvaluacion.PuntajeAspecto4.ToString());
                            html = html.Replace("//P4_5", String.Empty);
                            html = html.Replace("//P4_3", String.Empty);
                            html = html.Replace("//P4_2", String.Empty);
                            html = html.Replace("//P4_1", String.Empty);
                            html = html.Replace("//P4_T", oEvaluacion.PuntajeAspecto4.ToString());
                            break;
                        case 7:
                            html = html.Replace("//P4_3", oEvaluacion.PuntajeAspecto4.ToString());
                            html = html.Replace("//P4_5", String.Empty);
                            html = html.Replace("//P4_4", String.Empty);
                            html = html.Replace("//P4_2", String.Empty);
                            html = html.Replace("//P4_1", String.Empty);
                            html = html.Replace("//P4_T", oEvaluacion.PuntajeAspecto4.ToString());
                            break;
                        case 8:
                            html = html.Replace("//P4_3", oEvaluacion.PuntajeAspecto4.ToString());
                            html = html.Replace("//P4_5", String.Empty);
                            html = html.Replace("//P4_4", String.Empty);
                            html = html.Replace("//P4_2", String.Empty);
                            html = html.Replace("//P4_1", String.Empty);
                            html = html.Replace("//P4_T", oEvaluacion.PuntajeAspecto4.ToString());
                            break;
                        case 9:
                            html = html.Replace("//P4_2", oEvaluacion.PuntajeAspecto4.ToString());
                            html = html.Replace("//P4_5", String.Empty);
                            html = html.Replace("//P4_4", String.Empty);
                            html = html.Replace("//P4_3", String.Empty);
                            html = html.Replace("//P4_1", String.Empty);
                            html = html.Replace("//P4_T", oEvaluacion.PuntajeAspecto4.ToString());
                            break;
                        case 10:
                            html = html.Replace("//P4_1", oEvaluacion.PuntajeAspecto4.ToString());
                            html = html.Replace("//P4_5", String.Empty);
                            html = html.Replace("//P4_4", String.Empty);
                            html = html.Replace("//P4_3", String.Empty);
                            html = html.Replace("//P4_2", String.Empty);
                            html = html.Replace("//P4_T", oEvaluacion.PuntajeAspecto4.ToString());
                            break;
                    }
                    switch (oEvaluacion.PuntajeAspecto5)
                    {
                        case 1:
                            html = html.Replace("//P5_5", oEvaluacion.PuntajeAspecto5.ToString());
                            html = html.Replace("//P5_4", String.Empty);
                            html = html.Replace("//P5_3", String.Empty);
                            html = html.Replace("//P5_2", String.Empty);
                            html = html.Replace("//P5_1", String.Empty);
                            html = html.Replace("//P5_T", oEvaluacion.PuntajeAspecto5.ToString());
                            break;
                        case 2:
                            html = html.Replace("//P5_5", oEvaluacion.PuntajeAspecto5.ToString());
                            html = html.Replace("//P5_4", String.Empty);
                            html = html.Replace("//P5_3", String.Empty);
                            html = html.Replace("//P5_2", String.Empty);
                            html = html.Replace("//P5_1", String.Empty);
                            html = html.Replace("//P5_T", oEvaluacion.PuntajeAspecto5.ToString());
                            break;
                        case 3:
                            html = html.Replace("//P5_5", oEvaluacion.PuntajeAspecto5.ToString());
                            html = html.Replace("//P5_4", String.Empty);
                            html = html.Replace("//P5_3", String.Empty);
                            html = html.Replace("//P5_2", String.Empty);
                            html = html.Replace("//P5_1", String.Empty);
                            html = html.Replace("//P5_T", oEvaluacion.PuntajeAspecto5.ToString());
                            break;
                        case 4:
                            html = html.Replace("//P5_4", oEvaluacion.PuntajeAspecto5.ToString());
                            html = html.Replace("//P5_5", String.Empty);
                            html = html.Replace("//P5_3", String.Empty);
                            html = html.Replace("//P5_2", String.Empty);
                            html = html.Replace("//P5_1", String.Empty);
                            html = html.Replace("//P5_T", oEvaluacion.PuntajeAspecto5.ToString());
                            break;
                        case 5:
                            html = html.Replace("//P5_4", oEvaluacion.PuntajeAspecto5.ToString());
                            html = html.Replace("//P5_5", String.Empty);
                            html = html.Replace("//P5_3", String.Empty);
                            html = html.Replace("//P5_2", String.Empty);
                            html = html.Replace("//P5_1", String.Empty);
                            html = html.Replace("//P5_T", oEvaluacion.PuntajeAspecto5.ToString());
                            break;
                        case 6:
                            html = html.Replace("//P5_4", oEvaluacion.PuntajeAspecto5.ToString());
                            html = html.Replace("//P5_5", String.Empty);
                            html = html.Replace("//P5_3", String.Empty);
                            html = html.Replace("//P5_2", String.Empty);
                            html = html.Replace("//P5_1", String.Empty);
                            html = html.Replace("//P5_T", oEvaluacion.PuntajeAspecto5.ToString());
                            break;
                        case 7:
                            html = html.Replace("//P5_3", oEvaluacion.PuntajeAspecto5.ToString());
                            html = html.Replace("//P5_5", String.Empty);
                            html = html.Replace("//P5_4", String.Empty);
                            html = html.Replace("//P5_2", String.Empty);
                            html = html.Replace("//P5_1", String.Empty);
                            html = html.Replace("//P5_T", oEvaluacion.PuntajeAspecto5.ToString());
                            break;
                        case 8:
                            html = html.Replace("//P5_3", oEvaluacion.PuntajeAspecto5.ToString());
                            html = html.Replace("//P5_5", String.Empty);
                            html = html.Replace("//P5_4", String.Empty);
                            html = html.Replace("//P5_2", String.Empty);
                            html = html.Replace("//P5_1", String.Empty);
                            html = html.Replace("//P5_T", oEvaluacion.PuntajeAspecto5.ToString());
                            break;
                        case 9:
                            html = html.Replace("//P5_2", oEvaluacion.PuntajeAspecto5.ToString());
                            html = html.Replace("//P5_5", String.Empty);
                            html = html.Replace("//P5_4", String.Empty);
                            html = html.Replace("//P5_3", String.Empty);
                            html = html.Replace("//P5_1", String.Empty);
                            html = html.Replace("//P5_T", oEvaluacion.PuntajeAspecto5.ToString());
                            break;
                        case 10:
                            html = html.Replace("//P5_1", oEvaluacion.PuntajeAspecto5.ToString());
                            html = html.Replace("//P5_5", String.Empty);
                            html = html.Replace("//P5_4", String.Empty);
                            html = html.Replace("//P5_3", String.Empty);
                            html = html.Replace("//P5_2", String.Empty);
                            html = html.Replace("//P5_T", oEvaluacion.PuntajeAspecto5.ToString());
                            break;
                    }

                }
                if (IdExamen == 1)
                {
                    //CON EXAMEN
                    html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Convocatoria/evaluacion_entrevista_ce.html"));

                    switch (oEvaluacion.PuntajeAspecto1)
                    {
                        case 1:
                            html = html.Replace("//P1_5", oEvaluacion.PuntajeAspecto1.ToString());
                            html = html.Replace("//P1_4", String.Empty);
                            html = html.Replace("//P1_3", String.Empty);
                            html = html.Replace("//P1_2", String.Empty);
                            html = html.Replace("//P1_1", String.Empty);
                            html = html.Replace("//P1_T", oEvaluacion.PuntajeAspecto1.ToString());
                            break;
                        case 2:
                            html = html.Replace("//P1_5", oEvaluacion.PuntajeAspecto1.ToString());
                            html = html.Replace("//P1_4", String.Empty);
                            html = html.Replace("//P1_3", String.Empty);
                            html = html.Replace("//P1_2", String.Empty);
                            html = html.Replace("//P1_1", String.Empty);
                            html = html.Replace("//P1_T", oEvaluacion.PuntajeAspecto1.ToString());
                            break;
                        case 3:
                            html = html.Replace("//P1_5", oEvaluacion.PuntajeAspecto1.ToString());
                            html = html.Replace("//P1_4", String.Empty);
                            html = html.Replace("//P1_3", String.Empty);
                            html = html.Replace("//P1_2", String.Empty);
                            html = html.Replace("//P1_1", String.Empty);
                            html = html.Replace("//P1_T", oEvaluacion.PuntajeAspecto1.ToString());
                            break;
                        case 4:
                            html = html.Replace("//P1_4", oEvaluacion.PuntajeAspecto1.ToString());
                            html = html.Replace("//P1_5", String.Empty);
                            html = html.Replace("//P1_3", String.Empty);
                            html = html.Replace("//P1_2", String.Empty);
                            html = html.Replace("//P1_1", String.Empty);
                            html = html.Replace("//P1_T", oEvaluacion.PuntajeAspecto1.ToString());
                            break;
                        case 5:
                            html = html.Replace("//P1_4", oEvaluacion.PuntajeAspecto1.ToString());
                            html = html.Replace("//P1_5", String.Empty);
                            html = html.Replace("//P1_3", String.Empty);
                            html = html.Replace("//P1_2", String.Empty);
                            html = html.Replace("//P1_1", String.Empty);
                            html = html.Replace("//P1_T", oEvaluacion.PuntajeAspecto1.ToString());
                            break;
                        case 6:
                            html = html.Replace("//P1_3", oEvaluacion.PuntajeAspecto1.ToString());
                            html = html.Replace("//P1_5", String.Empty);
                            html = html.Replace("//P1_4", String.Empty);
                            html = html.Replace("//P1_2", String.Empty);
                            html = html.Replace("//P1_1", String.Empty);
                            html = html.Replace("//P1_T", oEvaluacion.PuntajeAspecto1.ToString());
                            break;
                        case 7:
                            html = html.Replace("//P1_2", oEvaluacion.PuntajeAspecto1.ToString());
                            html = html.Replace("//P1_5", String.Empty);
                            html = html.Replace("//P1_4", String.Empty);
                            html = html.Replace("//P1_3", String.Empty);
                            html = html.Replace("//P1_1", String.Empty);
                            html = html.Replace("//P1_T", oEvaluacion.PuntajeAspecto1.ToString());
                            break;
                        case 8:
                            html = html.Replace("//P1_1", oEvaluacion.PuntajeAspecto1.ToString());
                            html = html.Replace("//P1_5", String.Empty);
                            html = html.Replace("//P1_4", String.Empty);
                            html = html.Replace("//P1_3", String.Empty);
                            html = html.Replace("//P1_2", String.Empty);
                            html = html.Replace("//P1_T", oEvaluacion.PuntajeAspecto1.ToString());
                            break;
                    }
                    switch (oEvaluacion.PuntajeAspecto2)
                    {
                        case 1:
                            html = html.Replace("//P2_5", oEvaluacion.PuntajeAspecto2.ToString());
                            html = html.Replace("//P2_4", String.Empty);
                            html = html.Replace("//P2_3", String.Empty);
                            html = html.Replace("//P2_2", String.Empty);
                            html = html.Replace("//P2_1", String.Empty);
                            html = html.Replace("//P2_T", oEvaluacion.PuntajeAspecto2.ToString());
                            break;
                        case 2:
                            html = html.Replace("//P2_5", oEvaluacion.PuntajeAspecto2.ToString());
                            html = html.Replace("//P2_4", String.Empty);
                            html = html.Replace("//P2_3", String.Empty);
                            html = html.Replace("//P2_2", String.Empty);
                            html = html.Replace("//P2_1", String.Empty);
                            html = html.Replace("//P2_T", oEvaluacion.PuntajeAspecto2.ToString());
                            break;
                        case 3:
                            html = html.Replace("//P2_5", oEvaluacion.PuntajeAspecto2.ToString());
                            html = html.Replace("//P2_4", String.Empty);
                            html = html.Replace("//P2_3", String.Empty);
                            html = html.Replace("//P2_2", String.Empty);
                            html = html.Replace("//P2_1", String.Empty);
                            html = html.Replace("//P2_T", oEvaluacion.PuntajeAspecto2.ToString());
                            break;
                        case 4:
                            html = html.Replace("//P2_4", oEvaluacion.PuntajeAspecto2.ToString());
                            html = html.Replace("//P2_5", String.Empty);
                            html = html.Replace("//P2_3", String.Empty);
                            html = html.Replace("//P2_2", String.Empty);
                            html = html.Replace("//P2_1", String.Empty);
                            html = html.Replace("//P2_T", oEvaluacion.PuntajeAspecto2.ToString());
                            break;
                        case 5:
                            html = html.Replace("//P2_4", oEvaluacion.PuntajeAspecto2.ToString());
                            html = html.Replace("//P2_5", String.Empty);
                            html = html.Replace("//P2_3", String.Empty);
                            html = html.Replace("//P2_2", String.Empty);
                            html = html.Replace("//P2_1", String.Empty);
                            html = html.Replace("//P2_T", oEvaluacion.PuntajeAspecto2.ToString());
                            break;
                        case 6:
                            html = html.Replace("//P2_3", oEvaluacion.PuntajeAspecto2.ToString());
                            html = html.Replace("//P2_5", String.Empty);
                            html = html.Replace("//P2_4", String.Empty);
                            html = html.Replace("//P2_2", String.Empty);
                            html = html.Replace("//P2_1", String.Empty);
                            html = html.Replace("//P2_T", oEvaluacion.PuntajeAspecto2.ToString());
                            break;
                        case 7:
                            html = html.Replace("//P2_2", oEvaluacion.PuntajeAspecto2.ToString());
                            html = html.Replace("//P2_5", String.Empty);
                            html = html.Replace("//P2_4", String.Empty);
                            html = html.Replace("//P2_3", String.Empty);
                            html = html.Replace("//P2_1", String.Empty);
                            html = html.Replace("//P2_T", oEvaluacion.PuntajeAspecto2.ToString());
                            break;
                        case 8:
                            html = html.Replace("//P2_1", oEvaluacion.PuntajeAspecto2.ToString());
                            html = html.Replace("//P2_5", String.Empty);
                            html = html.Replace("//P2_4", String.Empty);
                            html = html.Replace("//P2_3", String.Empty);
                            html = html.Replace("//P2_2", String.Empty);
                            html = html.Replace("//P2_T", oEvaluacion.PuntajeAspecto2.ToString());
                            break;
                    }
                    switch (oEvaluacion.PuntajeAspecto3)
                    {
                        case 1:
                            html = html.Replace("//P3_5", oEvaluacion.PuntajeAspecto3.ToString());
                            html = html.Replace("//P3_4", String.Empty);
                            html = html.Replace("//P3_3", String.Empty);
                            html = html.Replace("//P3_2", String.Empty);
                            html = html.Replace("//P3_1", String.Empty);
                            html = html.Replace("//P3_T", oEvaluacion.PuntajeAspecto3.ToString());
                            break;
                        case 2:
                            html = html.Replace("//P3_5", oEvaluacion.PuntajeAspecto3.ToString());
                            html = html.Replace("//P3_4", String.Empty);
                            html = html.Replace("//P3_3", String.Empty);
                            html = html.Replace("//P3_2", String.Empty);
                            html = html.Replace("//P3_1", String.Empty);
                            html = html.Replace("//P3_T", oEvaluacion.PuntajeAspecto3.ToString());
                            break;
                        case 3:
                            html = html.Replace("//P3_5", oEvaluacion.PuntajeAspecto3.ToString());
                            html = html.Replace("//P3_4", String.Empty);
                            html = html.Replace("//P3_3", String.Empty);
                            html = html.Replace("//P3_2", String.Empty);
                            html = html.Replace("//P3_1", String.Empty);
                            html = html.Replace("//P3_T", oEvaluacion.PuntajeAspecto3.ToString());
                            break;
                        case 4:
                            html = html.Replace("//P3_4", oEvaluacion.PuntajeAspecto3.ToString());
                            html = html.Replace("//P3_5", String.Empty);
                            html = html.Replace("//P3_3", String.Empty);
                            html = html.Replace("//P3_2", String.Empty);
                            html = html.Replace("//P3_1", String.Empty);
                            html = html.Replace("//P3_T", oEvaluacion.PuntajeAspecto3.ToString());
                            break;
                        case 5:
                            html = html.Replace("//P3_4", oEvaluacion.PuntajeAspecto3.ToString());
                            html = html.Replace("//P3_5", String.Empty);
                            html = html.Replace("//P3_3", String.Empty);
                            html = html.Replace("//P3_2", String.Empty);
                            html = html.Replace("//P3_1", String.Empty);
                            html = html.Replace("//P3_T", oEvaluacion.PuntajeAspecto3.ToString());
                            break;
                        case 6:
                            html = html.Replace("//P3_3", oEvaluacion.PuntajeAspecto3.ToString());
                            html = html.Replace("//P3_5", String.Empty);
                            html = html.Replace("//P3_4", String.Empty);
                            html = html.Replace("//P3_2", String.Empty);
                            html = html.Replace("//P3_1", String.Empty);
                            html = html.Replace("//P3_T", oEvaluacion.PuntajeAspecto3.ToString());
                            break;
                        case 7:
                            html = html.Replace("//P3_2", oEvaluacion.PuntajeAspecto3.ToString());
                            html = html.Replace("//P3_5", String.Empty);
                            html = html.Replace("//P3_4", String.Empty);
                            html = html.Replace("//P3_3", String.Empty);
                            html = html.Replace("//P3_1", String.Empty);
                            html = html.Replace("//P3_T", oEvaluacion.PuntajeAspecto3.ToString());
                            break;
                        case 8:
                            html = html.Replace("//P3_1", oEvaluacion.PuntajeAspecto3.ToString());
                            html = html.Replace("//P3_5", String.Empty);
                            html = html.Replace("//P3_4", String.Empty);
                            html = html.Replace("//P3_3", String.Empty);
                            html = html.Replace("//P3_2", String.Empty);
                            html = html.Replace("//P3_T", oEvaluacion.PuntajeAspecto3.ToString());
                            break;
                    }
                    switch (oEvaluacion.PuntajeAspecto4)
                    {
                        case 1:
                            html = html.Replace("//P4_5", oEvaluacion.PuntajeAspecto4.ToString());
                            html = html.Replace("//P4_4", String.Empty);
                            html = html.Replace("//P4_3", String.Empty);
                            html = html.Replace("//P4_2", String.Empty);
                            html = html.Replace("//P4_1", String.Empty);
                            html = html.Replace("//P4_T", oEvaluacion.PuntajeAspecto4.ToString());
                            break;
                        case 2:
                            html = html.Replace("//P4_5", oEvaluacion.PuntajeAspecto4.ToString());
                            html = html.Replace("//P4_4", String.Empty);
                            html = html.Replace("//P4_3", String.Empty);
                            html = html.Replace("//P4_2", String.Empty);
                            html = html.Replace("//P4_1", String.Empty);
                            html = html.Replace("//P4_T", oEvaluacion.PuntajeAspecto4.ToString());
                            break;
                        case 3:
                            html = html.Replace("//P4_5", oEvaluacion.PuntajeAspecto4.ToString());
                            html = html.Replace("//P4_4", String.Empty);
                            html = html.Replace("//P4_3", String.Empty);
                            html = html.Replace("//P4_2", String.Empty);
                            html = html.Replace("//P4_1", String.Empty);
                            html = html.Replace("//P4_T", oEvaluacion.PuntajeAspecto4.ToString());
                            break;
                        case 4:
                            html = html.Replace("//P4_4", oEvaluacion.PuntajeAspecto4.ToString());
                            html = html.Replace("//P4_5", String.Empty);
                            html = html.Replace("//P4_3", String.Empty);
                            html = html.Replace("//P4_2", String.Empty);
                            html = html.Replace("//P4_1", String.Empty);
                            html = html.Replace("//P4_T", oEvaluacion.PuntajeAspecto4.ToString());
                            break;
                        case 5:
                            html = html.Replace("//P4_4", oEvaluacion.PuntajeAspecto4.ToString());
                            html = html.Replace("//P4_5", String.Empty);
                            html = html.Replace("//P4_3", String.Empty);
                            html = html.Replace("//P4_2", String.Empty);
                            html = html.Replace("//P4_1", String.Empty);
                            html = html.Replace("//P4_T", oEvaluacion.PuntajeAspecto4.ToString());
                            break;
                        case 6:
                            html = html.Replace("//P4_3", oEvaluacion.PuntajeAspecto4.ToString());
                            html = html.Replace("//P4_5", String.Empty);
                            html = html.Replace("//P4_4", String.Empty);
                            html = html.Replace("//P4_2", String.Empty);
                            html = html.Replace("//P4_1", String.Empty);
                            html = html.Replace("//P4_T", oEvaluacion.PuntajeAspecto4.ToString());
                            break;
                        case 7:
                            html = html.Replace("//P4_2", oEvaluacion.PuntajeAspecto4.ToString());
                            html = html.Replace("//P4_5", String.Empty);
                            html = html.Replace("//P4_4", String.Empty);
                            html = html.Replace("//P4_3", String.Empty);
                            html = html.Replace("//P4_1", String.Empty);
                            html = html.Replace("//P4_T", oEvaluacion.PuntajeAspecto4.ToString());
                            break;
                        case 8:
                            html = html.Replace("//P4_1", oEvaluacion.PuntajeAspecto4.ToString());
                            html = html.Replace("//P4_5", String.Empty);
                            html = html.Replace("//P4_4", String.Empty);
                            html = html.Replace("//P4_3", String.Empty);
                            html = html.Replace("//P4_2", String.Empty);
                            html = html.Replace("//P4_T", oEvaluacion.PuntajeAspecto4.ToString());
                            break;
                    }
                    switch (oEvaluacion.PuntajeAspecto5)
                    {
                        case 1:
                            html = html.Replace("//P5_5", oEvaluacion.PuntajeAspecto5.ToString());
                            html = html.Replace("//P5_4", String.Empty);
                            html = html.Replace("//P5_3", String.Empty);
                            html = html.Replace("//P5_2", String.Empty);
                            html = html.Replace("//P5_1", String.Empty);
                            html = html.Replace("//P5_T", oEvaluacion.PuntajeAspecto5.ToString());
                            break;
                        case 2:
                            html = html.Replace("//P5_5", oEvaluacion.PuntajeAspecto5.ToString());
                            html = html.Replace("//P5_4", String.Empty);
                            html = html.Replace("//P5_3", String.Empty);
                            html = html.Replace("//P5_2", String.Empty);
                            html = html.Replace("//P5_1", String.Empty);
                            html = html.Replace("//P5_T", oEvaluacion.PuntajeAspecto5.ToString());
                            break;
                        case 3:
                            html = html.Replace("//P5_5", oEvaluacion.PuntajeAspecto5.ToString());
                            html = html.Replace("//P5_4", String.Empty);
                            html = html.Replace("//P5_3", String.Empty);
                            html = html.Replace("//P5_2", String.Empty);
                            html = html.Replace("//P5_1", String.Empty);
                            html = html.Replace("//P5_T", oEvaluacion.PuntajeAspecto5.ToString());
                            break;
                        case 4:
                            html = html.Replace("//P5_4", oEvaluacion.PuntajeAspecto5.ToString());
                            html = html.Replace("//P5_5", String.Empty);
                            html = html.Replace("//P5_3", String.Empty);
                            html = html.Replace("//P5_2", String.Empty);
                            html = html.Replace("//P5_1", String.Empty);
                            html = html.Replace("//P5_T", oEvaluacion.PuntajeAspecto5.ToString());
                            break;
                        case 5:
                            html = html.Replace("//P5_4", oEvaluacion.PuntajeAspecto5.ToString());
                            html = html.Replace("//P5_5", String.Empty);
                            html = html.Replace("//P5_3", String.Empty);
                            html = html.Replace("//P5_2", String.Empty);
                            html = html.Replace("//P5_1", String.Empty);
                            html = html.Replace("//P5_T", oEvaluacion.PuntajeAspecto5.ToString());
                            break;
                        case 6:
                            html = html.Replace("//P5_3", oEvaluacion.PuntajeAspecto5.ToString());
                            html = html.Replace("//P5_5", String.Empty);
                            html = html.Replace("//P5_4", String.Empty);
                            html = html.Replace("//P5_2", String.Empty);
                            html = html.Replace("//P5_1", String.Empty);
                            html = html.Replace("//P5_T", oEvaluacion.PuntajeAspecto5.ToString());
                            break;
                        case 7:
                            html = html.Replace("//P5_2", oEvaluacion.PuntajeAspecto5.ToString());
                            html = html.Replace("//P5_5", String.Empty);
                            html = html.Replace("//P5_4", String.Empty);
                            html = html.Replace("//P5_3", String.Empty);
                            html = html.Replace("//P5_1", String.Empty);
                            html = html.Replace("//P5_T", oEvaluacion.PuntajeAspecto5.ToString());
                            break;
                        case 8:
                            html = html.Replace("//P5_1", oEvaluacion.PuntajeAspecto5.ToString());
                            html = html.Replace("//P5_5", String.Empty);
                            html = html.Replace("//P5_4", String.Empty);
                            html = html.Replace("//P5_3", String.Empty);
                            html = html.Replace("//P5_2", String.Empty);
                            html = html.Replace("//P5_T", oEvaluacion.PuntajeAspecto5.ToString());
                            break;
                    }

                }

                html = html.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
                html = html.Replace("//postulante", oEvaluacion.Nombre);
                html = html.Replace("//cargo", oEvaluacion.NombrePuesto.ToUpper());
                html = html.Replace("//proceso_cas", oEvaluacion.NroProceso);
                html = html.Replace("//area_usuario", oEvaluacion.Dependencia);
                html = html.Replace("//hora", oEvaluacion.HoraEntrevista);
                html = html.Replace("//fecha", oEvaluacion.FechaEntrevista);
                html = html.Replace("//observacion", oEvaluacion.Observacion);
                html = html.Replace("//total", (oEvaluacion.PuntajeAspecto1 + oEvaluacion.PuntajeAspecto2 + oEvaluacion.PuntajeAspecto3 + oEvaluacion.PuntajeAspecto4 + oEvaluacion.PuntajeAspecto5).ToString());
                //html = html.Replace("//preguntas", System.DateTime.Now.ToString("dd/MM/yyyy"));

            }
            else {
                // NO SE PRESENTO
                if (IdExamen == 0)
                {
                    //SIN EXAMEN
                    html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Convocatoria/evaluacion_entrevista_se.html"));

                    html = html.Replace("//P1_5", "--");
                    html = html.Replace("//P1_4", "--");
                    html = html.Replace("//P1_3", "--");
                    html = html.Replace("//P1_2", "--");
                    html = html.Replace("//P1_1", "--");
                    html = html.Replace("//P1_T", "--");

                    html = html.Replace("//P2_5", "--");
                    html = html.Replace("//P2_4", "--");
                    html = html.Replace("//P2_3", "--");
                    html = html.Replace("//P2_2", "--");
                    html = html.Replace("//P2_1", "--");
                    html = html.Replace("//P2_T", "--");
                            
                    html = html.Replace("//P3_5", "--");
                    html = html.Replace("//P3_4", "--");
                    html = html.Replace("//P3_3", "--");
                    html = html.Replace("//P3_2", "--");
                    html = html.Replace("//P3_1", "--");
                    html = html.Replace("//P3_T", "--");
                            
                    html = html.Replace("//P4_5", "--");
                    html = html.Replace("//P4_4", "--");
                    html = html.Replace("//P4_3", "--");
                    html = html.Replace("//P4_2", "--");
                    html = html.Replace("//P4_1", "--");
                    html = html.Replace("//P4_T", "--");
                            
                    html = html.Replace("//P5_5", "--");
                    html = html.Replace("//P5_4", "--");
                    html = html.Replace("//P5_3", "--");
                    html = html.Replace("//P5_2", "--");
                    html = html.Replace("//P5_1", "--");
                    html = html.Replace("//P5_T", "--");
                            
                }
                if (IdExamen == 1)
                {
                    //CON EXAMEN
                    html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Convocatoria/evaluacion_entrevista_ce.html"));

                    html = html.Replace("//P1_5", "--");
                    html = html.Replace("//P1_4", "--");
                    html = html.Replace("//P1_3", "--");
                    html = html.Replace("//P1_2", "--");
                    html = html.Replace("//P1_1", "--");
                    html = html.Replace("//P1_T", "--");

                    html = html.Replace("//P2_5", "--");
                    html = html.Replace("//P2_4", "--");
                    html = html.Replace("//P2_3", "--");
                    html = html.Replace("//P2_2", "--");
                    html = html.Replace("//P2_1", "--");
                    html = html.Replace("//P2_T", "--");

                    html = html.Replace("//P3_5", "--");
                    html = html.Replace("//P3_4", "--");
                    html = html.Replace("//P3_3", "--");
                    html = html.Replace("//P3_2", "--");
                    html = html.Replace("//P3_1", "--");
                    html = html.Replace("//P3_T", "--");

                    html = html.Replace("//P4_5", "--");
                    html = html.Replace("//P4_4", "--");
                    html = html.Replace("//P4_3", "--");
                    html = html.Replace("//P4_2", "--");
                    html = html.Replace("//P4_1", "--");
                    html = html.Replace("//P4_T", "--");

                    html = html.Replace("//P5_5", "--");
                    html = html.Replace("//P5_4", "--");
                    html = html.Replace("//P5_3", "--");
                    html = html.Replace("//P5_2", "--");
                    html = html.Replace("//P5_1", "--");
                    html = html.Replace("//P5_T", "--");
                }

                html = html.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
                html = html.Replace("//postulante", oEvaluacion.Nombre);
                html = html.Replace("//cargo", oEvaluacion.NombrePuesto.ToUpper());
                html = html.Replace("//proceso_cas", oEvaluacion.NroProceso);
                html = html.Replace("//area_usuario", oEvaluacion.Dependencia);
                html = html.Replace("//hora", oEvaluacion.HoraEntrevista);
                html = html.Replace("//fecha", oEvaluacion.FechaEntrevista);
                html = html.Replace("//observacion", "NO SE PRESENTO");
                html = html.Replace("//total", "--");
                //html = html.Replace("//preguntas", System.DateTime.Now.ToString("dd/MM/yyyy"));

            }

            String preguntas = "<table style='font-family: 'PT Sans Narrow', sans-serif; font-size: 11px; width: 500px; border: 0px solid #f0f0f0;' border='0' cellspacing='0' cellpadding='2'>";
            preguntas += "<tbody>";
            String fila = String.Empty;
            Int32 iPos = 1;
            foreach (PostulacionEntrevistaPersonalPregunta_Registro obj in lista)
            {
                fila = String.Empty;
                fila += "<tr>";
                fila += "<td style='width: 15px; text-align: right; ' >" + iPos.ToString() + ". </td>";
                fila += "<td style='width: 885px;' >" + obj.Descripcion + "</td>";
                fila += "</tr>";

                preguntas += fila;
                iPos += 1;
            }
            preguntas += "</tbody></table>";

            html = html.Replace("//preguntas", preguntas);
            html = html.Replace("//EVALUADOR", oEmpleado.NombreCompleto);
            html = html.Replace("//DNI", oEmpleado.NroDocumento);

            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(html, Server.MapPath("~/temp"));

            MemoryStream pdfStream = new MemoryStream();
            doc.Save(pdfStream);
            pdfStream.Position = 0;
            doc.Close();

            return pdfStream;
        }

        public FileResult FichaComunicado()
        {
            Int32 IdConvocatoria = (Request.QueryString.Get("idConvocatoria") == null ? 0 : Int32.Parse(Request.QueryString["idConvocatoria"]));
            Int32 IdTipo = (Request.QueryString.Get("idTipo") == null ? 0 : Int32.Parse(Request.QueryString["idTipo"]));
            Convocatoria_Registro objConvocatoria = _convocatoria_Servicio.ObtenerParaEditar(new Convocatoria_Request() { IdConvocatoria = IdConvocatoria });

            var fileName = "Resultado_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".docx";
            string fullPathOri = String.Empty;
            string fullPathNew = String.Empty;

            switch (IdTipo) {
                case 30:
                    List<PostulacionEvaluacionCurricular_Registro> listaPostulante = _convocatoria_Servicio.ListarPostulantesEvaluacionCurri(new Convocatoria_Request() { IdConvocatoria = IdConvocatoria }).ToList();
                    if (objConvocatoria.IdTieneExamenConoc == 1) {
                        if (listaPostulante.Count > 100)
                            fullPathOri = Path.Combine(Server.MapPath("~/Templates/Convocatoria/formato"), "RESULTADOS_EVALUACION_CURRICULAR_CON_CONOCIMIENTO_200.docx");
                        else if (listaPostulante.Count > 50)
                            fullPathOri = Path.Combine(Server.MapPath("~/Templates/Convocatoria/formato"), "RESULTADOS_EVALUACION_CURRICULAR_CON_CONOCIMIENTO_100.docx");
                        else if (listaPostulante.Count > 20)
                            fullPathOri = Path.Combine(Server.MapPath("~/Templates/Convocatoria/formato"), "RESULTADOS_EVALUACION_CURRICULAR_CON_CONOCIMIENTO_50.docx");
                        else
                            fullPathOri = Path.Combine(Server.MapPath("~/Templates/Convocatoria/formato"), "RESULTADOS_EVALUACION_CURRICULAR_CON_CONOCIMIENTO_20.docx");

                        fullPathNew = Path.Combine(Server.MapPath("~/Templates/Convocatoria"), fileName);
                        using (var doc = DocX.Load(fullPathOri))
                        {
                            doc.ReplaceText("<NRO_CAS>", objConvocatoria.NroConvocatoria);
                            doc.ReplaceText("<CARGO>", objConvocatoria.NombreCargo);
                            doc.ReplaceText("<FECHA>", DateTime.Now.ToString("dd MMMM yyyy"));

                            for (Int32 iPos =0; iPos < listaPostulante.Count; iPos++) {
                                doc.ReplaceText("<DNI" + (iPos + 1).ToString() + ">", listaPostulante[iPos].NroDocumento);
                                doc.ReplaceText("<POS" + (iPos + 1).ToString() + ">", listaPostulante[iPos].Paterno + " " + listaPostulante[iPos].Materno + " " + listaPostulante[iPos].Nombre);
                                doc.ReplaceText("<APTO" + (iPos + 1).ToString() + ">", (listaPostulante[iPos].AptoTotal == 1 ? "APTO/A" : "NO APTO/A"));
                                doc.ReplaceText("<PUN" + (iPos + 1).ToString() + ">", listaPostulante[iPos].PuntajeTotal.ToString());
                                doc.ReplaceText("<FEC" + (iPos + 1).ToString() + ">", (listaPostulante[iPos].FechaConocimiento == null ? "--" : listaPostulante[iPos].FechaConocimiento));
                                doc.ReplaceText("<HOR" + (iPos + 1).ToString() + ">", (listaPostulante[iPos].HoraConocimiento == null ? "--" : listaPostulante[iPos].HoraConocimiento));
                            }

                            doc.SaveAs(fullPathNew);
                        }
                    }
                    else {
                        if (listaPostulante.Count > 100)
                            fullPathOri = Path.Combine(Server.MapPath("~/Templates/Convocatoria/formato"), "RESULTADOS_EVALUACION_CURRICULAR_CON_ENTREVISTA_200.docx");
                        else if (listaPostulante.Count > 50)
                            fullPathOri = Path.Combine(Server.MapPath("~/Templates/Convocatoria/formato"), "RESULTADOS_EVALUACION_CURRICULAR_CON_ENTREVISTA_100.docx");
                        else if (listaPostulante.Count > 20)
                            fullPathOri = Path.Combine(Server.MapPath("~/Templates/Convocatoria/formato"), "RESULTADOS_EVALUACION_CURRICULAR_CON_ENTREVISTA_50.docx");
                        else
                            fullPathOri = Path.Combine(Server.MapPath("~/Templates/Convocatoria/formato"), "RESULTADOS_EVALUACION_CURRICULAR_CON_ENTREVISTA_20.docx");

                        fullPathNew = Path.Combine(Server.MapPath("~/Templates/Convocatoria"), fileName);
                        using (var doc = DocX.Load(fullPathOri))
                        {
                            doc.ReplaceText("<NRO_CAS>", objConvocatoria.NroConvocatoria);
                            doc.ReplaceText("<CARGO>", objConvocatoria.NombreCargo);
                            doc.ReplaceText("<FECHA>", DateTime.Now.ToString("dd MMMM yyyy"));

                            for (Int32 iPos = 0; iPos < listaPostulante.Count; iPos++)
                            {
                                doc.ReplaceText("<DNI" + (iPos + 1).ToString() + ">", listaPostulante[iPos].NroDocumento);
                                doc.ReplaceText("<POS" + (iPos + 1).ToString() + ">", listaPostulante[iPos].Paterno + " " + listaPostulante[iPos].Materno + " " + listaPostulante[iPos].Nombre);
                                doc.ReplaceText("<APTO" + (iPos + 1).ToString() + ">", (listaPostulante[iPos].AptoTotal == 1 ? "APTO/A" : "NO APTO/A"));
                                doc.ReplaceText("<PUN" + (iPos + 1).ToString() + ">", listaPostulante[iPos].PuntajeTotal.ToString());
                                doc.ReplaceText("<FEC" + (iPos + 1).ToString() + ">", (listaPostulante[iPos].FechaEntrevista == null ? "--" : listaPostulante[iPos].FechaEntrevista));
                                doc.ReplaceText("<HOR" + (iPos + 1).ToString() + ">", (listaPostulante[iPos].HoraEntrevista == null ? "--" : listaPostulante[iPos].HoraEntrevista));
                            }

                            doc.SaveAs(fullPathNew);
                        }
                    }

                    break;
                case 31:
                    List<PostulacionEvaluacionConocimiento_Registro> listaPostulanteC = _convocatoria_Servicio.ListarPostulantesEvaluacionConocimiento(new Convocatoria_Request() { IdConvocatoria = IdConvocatoria }).ToList();
                    if (listaPostulanteC.Count > 20)
                        fullPathOri = Path.Combine(Server.MapPath("~/Templates/Convocatoria/formato"), "RESULTADOS_EVALUACION_CONOCIMIENTO_50.docx");
                    else
                        fullPathOri = Path.Combine(Server.MapPath("~/Templates/Convocatoria/formato"), "RESULTADOS_EVALUACION_CONOCIMIENTO_20.docx");

                    fullPathNew = Path.Combine(Server.MapPath("~/Templates/Convocatoria"), fileName);
                    using (var doc = DocX.Load(fullPathOri))
                    {
                        doc.ReplaceText("<NRO_CAS>", objConvocatoria.NroConvocatoria);
                        doc.ReplaceText("<CARGO>", objConvocatoria.NombreCargo);
                        doc.ReplaceText("<FECHA>", DateTime.Now.ToString("dd MMMM yyyy"));

                        for (Int32 iPos = 0; iPos < listaPostulanteC.Count; iPos++)
                        {
                            doc.ReplaceText("<DNI" + (iPos + 1).ToString() + ">", listaPostulanteC[iPos].NroDocumento);
                            doc.ReplaceText("<POS" + (iPos + 1).ToString() + ">", listaPostulanteC[iPos].Paterno + " " + listaPostulanteC[iPos].Materno + " " + listaPostulanteC[iPos].Nombre);
                            doc.ReplaceText("<APTO" + (iPos + 1).ToString() + ">", (listaPostulanteC[iPos].AptoTotal == 1 ? "APTO/A" : "NO APTO/A"));
                            doc.ReplaceText("<PUN" + (iPos + 1).ToString() + ">", listaPostulanteC[iPos].PuntajeTotal.ToString());
                            doc.ReplaceText("<FEC" + (iPos + 1).ToString() + ">", (listaPostulanteC[iPos].FechaEntrevista == null ? "--" : listaPostulanteC[iPos].FechaEntrevista));
                            doc.ReplaceText("<HOR" + (iPos + 1).ToString() + ">", (listaPostulanteC[iPos].HoraEntrevista == null ? "--" : listaPostulanteC[iPos].HoraEntrevista));
                        }

                        doc.SaveAs(fullPathNew);
                    }
                    break;
                case 33:
                    List<PostulacionResultadoTotal_Registro> listaPostulanteF = _convocatoria_Servicio.ListarPostulantesResultadosTotales(new Convocatoria_Request() { IdConvocatoria = IdConvocatoria }).ToList();
                    if (objConvocatoria.IdTieneExamenConoc == 1) {
                        if (listaPostulanteF.Count > 20)
                            fullPathOri = Path.Combine(Server.MapPath("~/Templates/Convocatoria/formato"), "RESULTADOS_FINAL_CON_CONOCIMIENTO_50.docx");
                        else
                            fullPathOri = Path.Combine(Server.MapPath("~/Templates/Convocatoria/formato"), "RESULTADOS_FINAL_CON_CONOCIMIENTO_20.docx");

                        fullPathNew = Path.Combine(Server.MapPath("~/Templates/Convocatoria"), fileName);
                        using (var doc = DocX.Load(fullPathOri))
                        {
                            doc.ReplaceText("<NRO_CAS>", objConvocatoria.NroConvocatoria);
                            doc.ReplaceText("<CARGO>", objConvocatoria.NombreCargo);
                            doc.ReplaceText("<FECHA>", DateTime.Now.ToString("dd MMMM yyyy"));

                            String strGanadores = String.Empty;
                            for (Int32 iPos = 0; iPos < listaPostulanteF.Count; iPos++)
                            {
                                if (listaPostulanteF[iPos].AptoCurricular == 1 && listaPostulanteF[iPos].AptoConocimiento == 1) {
                                    doc.ReplaceText("<DNI" + (iPos + 1).ToString() + ">", listaPostulanteF[iPos].NroDocumento);
                                    doc.ReplaceText("<POS" + (iPos + 1).ToString() + ">", listaPostulanteF[iPos].Paterno + " " + listaPostulanteF[iPos].Materno + " " + listaPostulanteF[iPos].Nombre);
                                    doc.ReplaceText("<CUR" + (iPos + 1).ToString() + ">", listaPostulanteF[iPos].PuntajeCurricular.ToString("N0"));      //(listaPostulanteF[iPos].AptoCurricular == 1 ? listaPostulanteF[iPos].PuntajeCurricular.ToString() : "NO APTO/A"));
                                    doc.ReplaceText("<CON" + (iPos + 1).ToString() + ">", listaPostulanteF[iPos].PuntajeConocimiento.ToString("N0"));
                                    doc.ReplaceText("<ENT" + (iPos + 1).ToString() + ">", listaPostulanteF[iPos].PuntajeEntrevista.ToString("N0"));
                                    doc.ReplaceText("<PUN" + (iPos + 1).ToString() + ">", listaPostulanteF[iPos].PuntajeTotal.ToString("N0"));
                                    doc.ReplaceText("<BON" + (iPos + 1).ToString() + ">", (listaPostulanteF[iPos].PuntajeBonificacion > 0 ? listaPostulanteF[iPos].PuntajeBonificacion.ToString() : "--"));
                                    if (listaPostulanteF[iPos].AptoGanador == 1 && listaPostulanteF[iPos].Posicion <= listaPostulanteF[iPos].Vacantes) {
                                        doc.ReplaceText("<RES" + (iPos + 1).ToString() + ">", "GANADOR(A)");
                                        strGanadores += listaPostulanteF[iPos].Paterno + " " + listaPostulanteF[iPos].Materno + " " + listaPostulanteF[iPos].Nombre + ", ";
                                    }
                                    else if (listaPostulanteF[iPos].AptoGanador == 1 && listaPostulanteF[iPos].Posicion - 1 == listaPostulanteF[iPos].Vacantes)
                                        doc.ReplaceText("<RES" + (iPos + 1).ToString() + ">", "ACCESITARIO(A)");
                                    else 
                                        doc.ReplaceText("<RES" + (iPos + 1).ToString() + ">", "--");
                                }
                            }

                            if (!String.IsNullOrEmpty(strGanadores))
                            {
                                if (objConvocatoria.CantidadVacantes > 1)
                                {
                                    doc.ReplaceText("<VACANTE>", "a los postulantes");
                                    doc.ReplaceText("<GANADOR>", strGanadores + "GANADORES(AS) ");

                                }
                                else
                                {
                                    doc.ReplaceText("<VACANTE>", "al postulante");
                                    doc.ReplaceText("<GANADOR>", strGanadores + "GANADOR(A) ");
                                }
                            }
                            else {
                                doc.ReplaceText("<VACANTE>", "");
                                doc.ReplaceText("<GANADOR>", "DESIERTO ");
                            }

                            doc.SaveAs(fullPathNew);
                        }
                    }
                    else {
                        if (listaPostulanteF.Count > 20)
                            fullPathOri = Path.Combine(Server.MapPath("~/Templates/Convocatoria/formato"), "RESULTADOS_FINAL_CON_ENTREVISTA_50.docx");
                        else
                            fullPathOri = Path.Combine(Server.MapPath("~/Templates/Convocatoria/formato"), "RESULTADOS_FINAL_CON_ENTREVISTA_20.docx");

                        fullPathNew = Path.Combine(Server.MapPath("~/Templates/Convocatoria"), fileName);
                        using (var doc = DocX.Load(fullPathOri))
                        {
                            doc.ReplaceText("<NRO_CAS>", objConvocatoria.NroConvocatoria);
                            doc.ReplaceText("<CARGO>", objConvocatoria.NombreCargo);
                            doc.ReplaceText("<FECHA>", DateTime.Now.ToString("dd MMMM yyyy"));

                            String strGanadores = String.Empty;
                            for (Int32 iPos = 0; iPos < listaPostulanteF.Count; iPos++)
                            {
                                if (listaPostulanteF[iPos].AptoCurricular == 1) {
                                    doc.ReplaceText("<DNI" + (iPos + 1).ToString() + ">", listaPostulanteF[iPos].NroDocumento);
                                    doc.ReplaceText("<POS" + (iPos + 1).ToString() + ">", listaPostulanteF[iPos].Paterno + " " + listaPostulanteF[iPos].Materno + " " + listaPostulanteF[iPos].Nombre);
                                    doc.ReplaceText("<CUR" + (iPos + 1).ToString() + ">", listaPostulanteF[iPos].PuntajeCurricular.ToString("N0"));      //(listaPostulanteF[iPos].AptoCurricular == 1 ? listaPostulanteF[iPos].PuntajeCurricular.ToString() : "NO APTO/A"));
                                    //doc.ReplaceText("<CON" + (iPos + 1).ToString() + ">", listaPostulanteF[iPos].PuntajeConocimiento.ToString());
                                    doc.ReplaceText("<ENT" + (iPos + 1).ToString() + ">", listaPostulanteF[iPos].PuntajeEntrevista.ToString("N0"));
                                    doc.ReplaceText("<PUN" + (iPos + 1).ToString() + ">", listaPostulanteF[iPos].PuntajeTotal.ToString("N0"));
                                    doc.ReplaceText("<BON" + (iPos + 1).ToString() + ">", (listaPostulanteF[iPos].PuntajeBonificacion > 0 ? listaPostulanteF[iPos].PuntajeBonificacion.ToString() : "--"));
                                    if (listaPostulanteF[iPos].AptoGanador == 1 && listaPostulanteF[iPos].Posicion <= listaPostulanteF[iPos].Vacantes) {
                                        doc.ReplaceText("<RES" + (iPos + 1).ToString() + ">", "GANADOR(A)");
                                        strGanadores += listaPostulanteF[iPos].Paterno + " " + listaPostulanteF[iPos].Materno + " " + listaPostulanteF[iPos].Nombre + ", ";
                                    }
                                    else if (listaPostulanteF[iPos].AptoGanador == 1 && listaPostulanteF[iPos].Posicion - 1 == listaPostulanteF[iPos].Vacantes)
                                        doc.ReplaceText("<RES" + (iPos + 1).ToString() + ">", "ACCESITARIO(A)");
                                    else
                                        doc.ReplaceText("<RES" + (iPos + 1).ToString() + ">", "--");
                                }
                            }

                            if (!String.IsNullOrEmpty(strGanadores))
                            {
                                if (objConvocatoria.CantidadVacantes > 1)
                                {
                                    doc.ReplaceText("<VACANTE>", "a los postulantes");
                                    doc.ReplaceText("<GANADOR>", strGanadores + "GANADORES(AS) ");

                                }
                                else
                                {
                                    doc.ReplaceText("<VACANTE>", "al postulante");
                                    doc.ReplaceText("<GANADOR>", strGanadores + "GANADOR(A) ");
                                }
                            }
                            else
                            {
                                doc.ReplaceText("<VACANTE>", "");
                                doc.ReplaceText("<GANADOR>", "DESIERTO ");
                            }

                            doc.SaveAs(fullPathNew);
                        }
                    }
                    break;
            }
            
            FileContentResult result = new FileContentResult(System.IO.File.ReadAllBytes(fullPathNew), "application/msword") { FileDownloadName = fileName };

            return result;
        }
        public FileResult FichaComunicadoPractica()
        {
            Int32 IdConvocatoria = (Request.QueryString.Get("idConvocatoria") == null ? 0 : Int32.Parse(Request.QueryString["idConvocatoria"]));
            Int32 IdTipo = (Request.QueryString.Get("idTipo") == null ? 0 : Int32.Parse(Request.QueryString["idTipo"]));
            Convocatoria_Registro objConvocatoria = _convocatoria_Servicio.ObtenerPracticaParaEditar(new Convocatoria_Request() { IdConvocatoria = IdConvocatoria });

            var fileName = "ResultadoPrac_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".docx";
            string fullPathOri = String.Empty;
            string fullPathNew = String.Empty;

            switch (IdTipo)
            {
                case 30:
                    List<PostulacionEvaluacionCurricular_Registro> listaPostulante = _convocatoria_Servicio.ListarPostulantesPracticaEvaluacionCurri(new Convocatoria_Request() { IdConvocatoria = IdConvocatoria }).ToList();
                    if (listaPostulante.Count > 100)
                        fullPathOri = Path.Combine(Server.MapPath("~/Templates/Convocatoria/formato"), "RESULTADOS_EVALUACION_CURRICULAR_PRACTICA_200.docx");
                    else if (listaPostulante.Count > 50)
                        fullPathOri = Path.Combine(Server.MapPath("~/Templates/Convocatoria/formato"), "RESULTADOS_EVALUACION_CURRICULAR_PRACTICA_100.docx");
                    else if (listaPostulante.Count > 20)
                        fullPathOri = Path.Combine(Server.MapPath("~/Templates/Convocatoria/formato"), "RESULTADOS_EVALUACION_CURRICULAR_PRACTICA_50.docx");
                    else
                        fullPathOri = Path.Combine(Server.MapPath("~/Templates/Convocatoria/formato"), "RESULTADOS_EVALUACION_CURRICULAR_PRACTICA_20.docx");

                    fullPathNew = Path.Combine(Server.MapPath("~/Templates/Convocatoria"), fileName);
                    using (var doc = DocX.Load(fullPathOri))
                    {
                        doc.ReplaceText("<NRO_CAS>", objConvocatoria.NroConvocatoria);
                        doc.ReplaceText("<CARGO>", objConvocatoria.NombreCargo);
                        doc.ReplaceText("<FECHA>", DateTime.Now.ToString("dd MMMM yyyy"));

                        for (Int32 iPos = 0; iPos < listaPostulante.Count; iPos++)
                        {
                            doc.ReplaceText("<DNI" + (iPos + 1).ToString() + ">", listaPostulante[iPos].NroDocumento);
                            doc.ReplaceText("<POS" + (iPos + 1).ToString() + ">", listaPostulante[iPos].Paterno + " " + listaPostulante[iPos].Materno + " " + listaPostulante[iPos].Nombre);
                            doc.ReplaceText("<APTO" + (iPos + 1).ToString() + ">", (listaPostulante[iPos].AptoTotal == 1 ? "APTO/A" : "NO APTO/A"));
                            doc.ReplaceText("<PUN" + (iPos + 1).ToString() + ">", listaPostulante[iPos].PuntajeTotal.ToString());
                            doc.ReplaceText("<FEC" + (iPos + 1).ToString() + ">", (listaPostulante[iPos].FechaEntrevista == null ? "--" : listaPostulante[iPos].FechaEntrevista));
                            doc.ReplaceText("<HOR" + (iPos + 1).ToString() + ">", (listaPostulante[iPos].HoraEntrevista == null ? "--" : listaPostulante[iPos].HoraEntrevista));
                        }

                        doc.SaveAs(fullPathNew);
                    }

                    break;
                case 33:
                    List<PostulacionResultadoTotal_Registro> listaPostulanteF = _convocatoria_Servicio.ListarPostulantesPracticaResultadosTotales(new Convocatoria_Request() { IdConvocatoria = IdConvocatoria }).ToList();
                    if (listaPostulanteF.Count > 20)
                        fullPathOri = Path.Combine(Server.MapPath("~/Templates/Convocatoria/formato"), "RESULTADOS_FINAL_PRACTICA_50.docx");
                    else
                        fullPathOri = Path.Combine(Server.MapPath("~/Templates/Convocatoria/formato"), "RESULTADOS_FINAL_PRACTICA_20.docx");

                    fullPathNew = Path.Combine(Server.MapPath("~/Templates/Convocatoria"), fileName);
                    using (var doc = DocX.Load(fullPathOri))
                    {
                        doc.ReplaceText("<NRO_CAS>", objConvocatoria.NroConvocatoria);
                        doc.ReplaceText("<CARGO>", objConvocatoria.NombreCargo);
                        doc.ReplaceText("<FECHA>", DateTime.Now.ToString("dd MMMM yyyy"));

                        String strGanadores = String.Empty;
                        for (Int32 iPos = 0; iPos < listaPostulanteF.Count; iPos++)
                        {
                            if (listaPostulanteF[iPos].AptoCurricular == 1)
                            {
                                doc.ReplaceText("<DNI" + (iPos + 1).ToString() + ">", listaPostulanteF[iPos].NroDocumento);
                                doc.ReplaceText("<POS" + (iPos + 1).ToString() + ">", listaPostulanteF[iPos].Paterno + " " + listaPostulanteF[iPos].Materno + " " + listaPostulanteF[iPos].Nombre);
                                doc.ReplaceText("<CUR" + (iPos + 1).ToString() + ">", listaPostulanteF[iPos].PuntajeCurricular.ToString("N0"));      //(listaPostulanteF[iPos].AptoCurricular == 1 ? listaPostulanteF[iPos].PuntajeCurricular.ToString() : "NO APTO/A"));
                                //doc.ReplaceText("<CON" + (iPos + 1).ToString() + ">", listaPostulanteF[iPos].PuntajeConocimiento.ToString());
                                doc.ReplaceText("<ENT" + (iPos + 1).ToString() + ">", listaPostulanteF[iPos].PuntajeEntrevista.ToString("N0"));
                                doc.ReplaceText("<PUN" + (iPos + 1).ToString() + ">", listaPostulanteF[iPos].PuntajeTotal.ToString("N0"));
                                doc.ReplaceText("<BON" + (iPos + 1).ToString() + ">", (listaPostulanteF[iPos].PuntajeBonificacion > 0 ? listaPostulanteF[iPos].PuntajeBonificacion.ToString() : "--"));
                                if (listaPostulanteF[iPos].AptoGanador == 1 && listaPostulanteF[iPos].Posicion <= listaPostulanteF[iPos].Vacantes)
                                {
                                    doc.ReplaceText("<RES" + (iPos + 1).ToString() + ">", "GANADOR(A)");
                                    strGanadores += listaPostulanteF[iPos].Paterno + " " + listaPostulanteF[iPos].Materno + " " + listaPostulanteF[iPos].Nombre + ", ";
                                }
                                else if (listaPostulanteF[iPos].AptoGanador == 1 && listaPostulanteF[iPos].Posicion - 1 == listaPostulanteF[iPos].Vacantes)
                                    doc.ReplaceText("<RES" + (iPos + 1).ToString() + ">", "ACCESITARIO(A)");
                                else
                                    doc.ReplaceText("<RES" + (iPos + 1).ToString() + ">", "--");
                            }
                        }

                        if (!String.IsNullOrEmpty(strGanadores))
                        {
                            if (objConvocatoria.CantidadVacantes > 1)
                            {
                                doc.ReplaceText("<VACANTE>", "a los postulantes");
                                doc.ReplaceText("<GANADOR>", strGanadores + "GANADORES(AS) ");

                            }
                            else
                            {
                                doc.ReplaceText("<VACANTE>", "al postulante");
                                doc.ReplaceText("<GANADOR>", strGanadores + "GANADOR(A) ");
                            }
                        }
                        else
                        {
                            doc.ReplaceText("<VACANTE>", "");
                            doc.ReplaceText("<GANADOR>", "DESIERTO ");
                        }

                        doc.SaveAs(fullPathNew);
                    }
                    
                    break;
            }

            FileContentResult result = new FileContentResult(System.IO.File.ReadAllBytes(fullPathNew), "application/msword") { FileDownloadName = fileName };

            return result;
        }


        [HttpPost]
        [Authorize]
        public JsonResult RegistrarConvocatoriaDocumento(ConvocatoriaDocumento_Registro registro)
        {
            try
            {
                //registro.IdTipoDocumento = 10;
                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

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

                object respuesta = _convocatoria_Servicio.RegistrarConvocatoriaDocumentoArchivo(registro);

                if (registro.IdTipoDocumento == 10) { //ACTA DE EVALUACION CURRICULAR
                    Convocatoria_Registro objConvocatoria = _convocatoria_Servicio.ObtenerParaEditar(new Convocatoria_Request() { IdConvocatoria = registro.IdConvocatoria });
                    objConvocatoria.Meta = registro.NombreDocumento;
                    if (objConvocatoria.IdTieneExamenConoc == 1) {
                        List<PostulacionEvaluacionCurricular_Registro> lstPostulante = _convocatoria_Servicio.ListarPostulantesEvaluacionCurri(new Convocatoria_Request() { IdConvocatoria = registro.IdConvocatoria }).ToList();

                        foreach (PostulacionEvaluacionCurricular_Registro objPostulante in lstPostulante)
                        {
                            if (objPostulante.AptoTotal == 1)
                            {
                                SendEmailConocimientosPostulante(objPostulante, objConvocatoria, "10");
                            }
                        }
                    }
                }

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        [Authorize]
        public JsonResult RegistrarConvocatoriaPracticaDocumento(ConvocatoriaDocumento_Registro registro)
        {
            try
            {
                //registro.IdTipoDocumento = 10;
                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

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

                object respuesta = _convocatoria_Servicio.RegistrarConvocatoriaPracticaDocumentoArchivo(registro);

                //if (registro.IdTipoDocumento == 10)
                //{ //ACTA DE EVALUACION CURRICULAR
                //    Convocatoria_Registro objConvocatoria = _convocatoria_Servicio.ObtenerPracticaParaEditar(new Convocatoria_Request() { IdConvocatoria = registro.IdConvocatoria });
                //    objConvocatoria.Meta = registro.NombreDocumento;
                //    if (objConvocatoria.IdTieneExamenConoc == 1)
                //    {
                //        List<PostulacionEvaluacionCurricular_Registro> lstPostulante = _convocatoria_Servicio.ListarPostulantesEvaluacionCurri(new Convocatoria_Request() { IdConvocatoria = registro.IdConvocatoria }).ToList();

                //        foreach (PostulacionEvaluacionCurricular_Registro objPostulante in lstPostulante)
                //        {
                //            if (objPostulante.AptoTotal == 1)
                //            {
                //                SendEmailConocimientosPostulante(objPostulante, objConvocatoria, "10");
                //            }
                //        }
                //    }
                //}

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [Authorize]
        private Boolean SendEmail(PostulanteInformacion_Registro postulante, String tipo)
        {
            Boolean exitoEnvio = false;
            String html = String.Empty;
            if (tipo == "3")
            { //AL INICIAR EL PROCESO DE EVALUACION CAS
                html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Correo/EnvioContrato.txt"));
                html = html.Replace("_POSTULANTE_", postulante.Nombre);
                html = html.Replace("_URLEXTERNO_", ConfigurationManager.AppSettings["URL_EXTERNO"].ToString()); //HttpUtility.UrlEncode(

                if (!String.IsNullOrEmpty(postulante.CorreoElectronico))
                {
                    MailMessage msg = new MailMessage();
                    msg.To.Add(new MailAddress(postulante.CorreoElectronico));
                    if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CCcorreoCAS"].ToString()))
                        msg.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["CCcorreoCAS"].ToString()));

                    msg.From = new MailAddress(ConfigurationManager.AppSettings["correo"].ToString());
                    msg.Subject = "Proceso de Convocatorias CAS - MIDIS";
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
            }
            if (tipo == "X")
            { //AL RECIBIR LA DOCUMENTACION DEL POSTULANTE
                //html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Correo/EvaluacionProceso.txt"));
                //html = html.Replace("_PROCESOCAS_", postulante.NombreProceso);
                //html = html.Replace("_URLINTERNO_", ConfigurationManager.AppSettings["URL_INTERNO"].ToString()); //HttpUtility.UrlEncode(

                //MailMessage msg = new MailMessage();
                //msg.To.Add(new MailAddress(ConfigurationManager.AppSettings["CCcorreoCAS"].ToString()));
                ////if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CCcorreoCAS"].ToString()))
                ////    msg.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["CCcorreoCAS"].ToString()));

                //msg.From = new MailAddress(ConfigurationManager.AppSettings["correo"].ToString());
                //msg.Subject = String.Format("{0} {1}-MIDIS", "Proceso de Convocatorias CAS", convocatoria.NroConvocatoria);
                //msg.Body = html;
                //msg.IsBodyHtml = true;

                //SmtpClient clienteSmtp = new SmtpClient(ConfigurationManager.AppSettings["host"].ToString());
                //clienteSmtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["usuarioemail"].ToString(), ConfigurationManager.AppSettings["contraseniaemail"].ToString());
                //try
                //{
                //    clienteSmtp.Send(msg);
                //}
                //catch (Exception ex)
                //{
                //    Console.Write(ex.Message);
                //    Console.ReadLine();
                //}

                exitoEnvio = true;
            }

            return exitoEnvio;
        }

        [Authorize]
        private Boolean SendEmailEntrevista(Empleado_Registro trabajador, Convocatoria_Registro convocatoria, String tipo, String aptos)
        {
            Boolean exitoEnvio = false;
            String html = String.Empty;
            if (tipo == "1")
            { //AL INICIAR NOTIFICACION DE ENTREVISTA PERSONAL
                html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Correo/EnvioNotificacionEntrevista.txt"));
                //html = html.Replace("_TRABAJADOR_", trabajador.Nombre);
                html = html.Replace("_PROCESOCAS_", convocatoria.NroConvocatoria + " - " + convocatoria.NombreCargo);
                //html = html.Replace("_CLAVE_", HttpUtility.UrlEncode(new Crypto().Encriptar(trabajador.IdEmpleado + "|" + convocatoria.IdConvocatoria)));
                html = html.Replace("_ZOOM_", convocatoria.Meta);
                html = html.Replace("//TODOS", aptos);

                html = html.Replace("_URLEVALUACION_", ConfigurationManager.AppSettings["SERVER_PATH"].ToString() + "Convocatoria/EntrevistaPersonal/?id=" + HttpUtility.UrlEncode(new Crypto().Encriptar(convocatoria.IdConvocatoria + "|" + trabajador.IdEmpleado + "|" + trabajador.NroDocumento))); //HttpUtility.UrlEncode(

                if (!String.IsNullOrEmpty(trabajador.CorreoElectronicoLaboral))
                {
                    MailMessage msg = new MailMessage();
                    msg.To.Add(new MailAddress(trabajador.CorreoElectronicoLaboral));
                    if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CCcorreoCAS"].ToString()))
                        msg.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["CCcorreoCAS"].ToString()));

                    msg.From = new MailAddress(ConfigurationManager.AppSettings["correo"].ToString());
                    msg.Subject = String.Format("{0} {1}-MIDIS", "Proceso de Convocatorias CAS", convocatoria.NroConvocatoria);
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
            }
            if (tipo == "2")
            { //AL INICIAR NOTIFICACION DE ENTREVISTA PERSONAL
                html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Correo/EnvioNotificacionEntrevistaPractica.txt"));
                //html = html.Replace("_TRABAJADOR_", trabajador.Nombre);
                html = html.Replace("_PROCESOCAS_", convocatoria.NroConvocatoria + " - " + convocatoria.NombreCargo);
                //html = html.Replace("_CLAVE_", HttpUtility.UrlEncode(new Crypto().Encriptar(trabajador.IdEmpleado + "|" + convocatoria.IdConvocatoria)));
                html = html.Replace("_ZOOM_", convocatoria.Meta);
                html = html.Replace("//TODOS", aptos);

                html = html.Replace("_URLEVALUACION_", ConfigurationManager.AppSettings["SERVER_PATH"].ToString() + "Convocatoria/EntrevistaPersonalPracticas/?id=" + HttpUtility.UrlEncode(new Crypto().Encriptar(convocatoria.IdConvocatoria + "|" + trabajador.IdEmpleado + "|" + trabajador.NroDocumento))); //HttpUtility.UrlEncode(

                if (!String.IsNullOrEmpty(trabajador.CorreoElectronicoLaboral))
                {
                    MailMessage msg = new MailMessage();
                    msg.To.Add(new MailAddress(trabajador.CorreoElectronicoLaboral));
                    if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CCcorreoCAS"].ToString()))
                        msg.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["CCcorreoCAS"].ToString()));

                    msg.From = new MailAddress(ConfigurationManager.AppSettings["correo"].ToString());
                    msg.Subject = String.Format("{0} {1}-MIDIS", "Proceso de Convocatorias de Prácticas", convocatoria.NroConvocatoria);
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
            }

            return exitoEnvio;
        }
        [Authorize]
        private Boolean SendEmailEntrevistaPostulante(PostulacionPostulante_Registro postulante, ConvocatoriaComite_Registro comite, Convocatoria_Registro convocatoria, String tipo)
        {
            Boolean exitoEnvio = false;
            String html = String.Empty;
            
            if (tipo == "10")
            { //AL INICIAR NOTIFICACION DE ENTREVISTA PERSONAL
                html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Correo/EnvioNotificacionEntrevistaPos.txt"));
                //html = html.Replace("_TRABAJADOR_", trabajador.Nombre);
                html = html.Replace("_PROCESOCAS_", convocatoria.NroConvocatoria + " - " + convocatoria.NombreCargo);
                //html = html.Replace("_CLAVE_", HttpUtility.UrlEncode(new Crypto().Encriptar(trabajador.IdEmpleado + "|" + convocatoria.IdConvocatoria)));
                html = html.Replace("_ZOOM_", convocatoria.Meta);
                html = html.Replace("_FECHA_", comite.FechaEntrevista);
                html = html.Replace("_HORA_", comite.HoraEntrevista);

                //html = html.Replace("_URLEVALUACION_", ConfigurationManager.AppSettings["SERVER_PATH"].ToString() + "Convocatoria/EntrevistaPersonal/?id=" + HttpUtility.UrlEncode(new Crypto().Encriptar(convocatoria.IdConvocatoria + "|" + trabajador.IdEmpleado + "|" + trabajador.NroDocumento))); //HttpUtility.UrlEncode(

                if (!String.IsNullOrEmpty(postulante.CorreoElectronico))
                {
                    MailMessage msg = new MailMessage();
                    msg.To.Add(new MailAddress(postulante.CorreoElectronico));
                    if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CCcorreoCAS"].ToString()))
                        msg.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["CCcorreoCAS"].ToString()));

                    msg.From = new MailAddress(ConfigurationManager.AppSettings["correo"].ToString());
                    msg.Subject = String.Format("{0} {1}-MIDIS", "Proceso de Convocatorias CAS", convocatoria.NroConvocatoria);
                    msg.Body = html;
                    msg.IsBodyHtml = true;

                    try
                    {
                        msg.Attachments.Add(new Attachment(Path.Combine(Server.MapPath("~/Templates/"), "Convocatoria/adjuntos/GUIA_RAPIDA_PARA_ACCEDER_VIDEOLLAMADA.pdf")));
                        msg.Attachments.Add(new Attachment(Path.Combine(Server.MapPath("~/Templates/"), "Convocatoria/adjuntos/RECOMENDACIONES_PARA_ENTREVISTA_A_TRAVES_VIDEOLLAMADA.pdf")));
                    }
                    catch (Exception)
                    {
                    }

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
            }
            if (tipo == "11")
            { //AL INICIAR NOTIFICACION DE ENTREVISTA PERSONAL
                html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Correo/EnvioNotificacionEntrevistaPracticaPos.txt"));
                //html = html.Replace("_TRABAJADOR_", trabajador.Nombre);
                html = html.Replace("_PROCESOCAS_", convocatoria.NroConvocatoria + " - " + convocatoria.NombreCargo);
                //html = html.Replace("_CLAVE_", HttpUtility.UrlEncode(new Crypto().Encriptar(trabajador.IdEmpleado + "|" + convocatoria.IdConvocatoria)));
                html = html.Replace("_ZOOM_", convocatoria.Meta);
                html = html.Replace("_FECHA_", comite.FechaEntrevista);
                html = html.Replace("_HORA_", comite.HoraEntrevista);

                //html = html.Replace("_URLEVALUACION_", ConfigurationManager.AppSettings["SERVER_PATH"].ToString() + "Convocatoria/EntrevistaPersonal/?id=" + HttpUtility.UrlEncode(new Crypto().Encriptar(convocatoria.IdConvocatoria + "|" + trabajador.IdEmpleado + "|" + trabajador.NroDocumento))); //HttpUtility.UrlEncode(

                if (!String.IsNullOrEmpty(postulante.CorreoElectronico))
                {
                    MailMessage msg = new MailMessage();
                    msg.To.Add(new MailAddress(postulante.CorreoElectronico));
                    if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CCcorreoCAS"].ToString()))
                        msg.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["CCcorreoCAS"].ToString()));

                    msg.From = new MailAddress(ConfigurationManager.AppSettings["correo"].ToString());
                    msg.Subject = String.Format("{0} {1}-MIDIS", "Proceso de Convocatorias de Prácticas", convocatoria.NroConvocatoria);
                    msg.Body = html;
                    msg.IsBodyHtml = true;

                    try
                    {
                        msg.Attachments.Add(new Attachment(Path.Combine(Server.MapPath("~/Templates/"), "Convocatoria/adjuntos/GUIA_RAPIDA_PARA_ACCEDER_VIDEOLLAMADA.pdf")));
                        msg.Attachments.Add(new Attachment(Path.Combine(Server.MapPath("~/Templates/"), "Convocatoria/adjuntos/RECOMENDACIONES_PARA_ENTREVISTA_A_TRAVES_VIDEOLLAMADA.pdf")));
                    }
                    catch (Exception)
                    {
                    }

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
            }

            if (tipo == "20")
            { //AL DECLARAR ACCESITARIO COMO GANADOR
                html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Correo/EnvioNotificacionAccesitario.txt"));
                //html = html.Replace("_TRABAJADOR_", trabajador.Nombre);
                html = html.Replace("_PROCESOCAS_", convocatoria.NroConvocatoria + " - " + convocatoria.NombreCargo);
                
                if (!String.IsNullOrEmpty(postulante.CorreoElectronico))
                {
                    MailMessage msg = new MailMessage();
                    msg.To.Add(new MailAddress(postulante.CorreoElectronico));
                    if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CCcorreoCAS"].ToString()))
                        msg.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["CCcorreoCAS"].ToString()));

                    msg.From = new MailAddress(ConfigurationManager.AppSettings["correo"].ToString());
                    msg.Subject = String.Format("{0} {1}-MIDIS", "Proceso de Convocatorias", convocatoria.NroConvocatoria);
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
            }

            return exitoEnvio;
        }
        [Authorize]
        private Boolean SendEmailConocimientosPostulante(PostulacionEvaluacionCurricular_Registro postulante, Convocatoria_Registro convocatoria, String tipo)
        {
            Boolean exitoEnvio = false;
            String html = String.Empty;
            if (tipo == "10")
            { //AL INICIAR NOTIFICACION DE ENTREVISTA PERSONAL
                html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Correo/EnvioNotificacionConocimientoPos.txt"));
                //html = html.Replace("_TRABAJADOR_", trabajador.Nombre);
                html = html.Replace("_PROCESOCAS_", convocatoria.NroConvocatoria + " - " + convocatoria.NombreCargo);
                //html = html.Replace("_CLAVE_", HttpUtility.UrlEncode(new Crypto().Encriptar(trabajador.IdEmpleado + "|" + convocatoria.IdConvocatoria)));
                html = html.Replace("_ZOOM_", convocatoria.Meta);
                html = html.Replace("_FECHA_", postulante.FechaConocimiento);
                html = html.Replace("_HORA_", postulante.HoraConocimiento);

                //html = html.Replace("_URLEVALUACION_", ConfigurationManager.AppSettings["SERVER_PATH"].ToString() + "Convocatoria/EntrevistaPersonal/?id=" + HttpUtility.UrlEncode(new Crypto().Encriptar(convocatoria.IdConvocatoria + "|" + trabajador.IdEmpleado + "|" + trabajador.NroDocumento))); //HttpUtility.UrlEncode(

                if (!String.IsNullOrEmpty(postulante.CorreoElectronico))
                {
                    MailMessage msg = new MailMessage();
                    msg.To.Add(new MailAddress(postulante.CorreoElectronico));
                    if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CCcorreoCAS"].ToString()))
                        msg.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["CCcorreoCAS"].ToString()));

                    msg.From = new MailAddress(ConfigurationManager.AppSettings["correo"].ToString());
                    msg.Subject = String.Format("{0} {1}-MIDIS", "Proceso de Convocatorias CAS", convocatoria.NroConvocatoria);
                    msg.Body = html;
                    msg.IsBodyHtml = true;

                    try
                    {
                        msg.Attachments.Add(new Attachment(Path.Combine(Server.MapPath("~/Templates/"), "Convocatoria/adjuntos/GUIA_RAPIDA_PARA_ACCEDER_VIDEOLLAMADA.pdf")));
                        //msg.Attachments.Add(new Attachment(Path.Combine(Server.MapPath("~/Templates/"), "Convocatoria/adjuntos/RECOMENDACIONES_PARA_ENTREVISTA_A_TRAVES_VIDEOLLAMADA.pdf")));
                    }
                    catch (Exception)
                    {
                    }

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
            }

            return exitoEnvio;
        }
        [Authorize]
        private Boolean SendEmailAsignarEvaluacion(Empleado_Registro trabajador, Convocatoria_Registro convocatoria, String enlace)
        {
            Boolean exitoEnvio = false;
            String html = String.Empty;
            
            html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Correo/EnvioAsignarEvaluacion.txt"));
            //html = html.Replace("_TRABAJADOR_", trabajador.Nombre);
            html = html.Replace("_PROCESOCAS_", convocatoria.NroConvocatoria + " - " + convocatoria.NombreCargo);
            //html = html.Replace("_CLAVE_", HttpUtility.UrlEncode(new Crypto().Encriptar(trabajador.IdEmpleado + "|" + convocatoria.IdConvocatoria)));
            
            html = html.Replace("_URLEVALUACION_", ConfigurationManager.AppSettings["SERVER_PATH"].ToString() + "Convocatoria/EvaluacionCurricular/?id=" + enlace);

                if (!String.IsNullOrEmpty(trabajador.CorreoElectronicoLaboral))
                {
                    MailMessage msg = new MailMessage();
                    msg.To.Add(new MailAddress(trabajador.CorreoElectronicoLaboral));
                    if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CCcorreoCAS"].ToString()))
                        msg.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["CCcorreoCAS"].ToString()));

                    msg.From = new MailAddress(ConfigurationManager.AppSettings["correo"].ToString());
                    msg.Subject = String.Format("{0} {1}-MIDIS", "Proceso de Convocatorias CAS", convocatoria.NroConvocatoria);
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

        [HttpGet]
        public JsonResult ListarEntrevistaPreguntasMaestras(PostulacionEvaluacionEntrevista_Registro peticion)
        {
            object respuesta = _convocatoria_Servicio.ListarEntrevistaPreguntasMaestras(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ActualizarPreguntaMaestra(PostulacionEntrevistaPersonalPregunta_Registro registro)
        {
            try
            {
                //registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = 1; // (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                object respuesta = _convocatoria_Servicio.ActualizarPreguntaMaestra(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult RegistrarPreguntaMaestra(PostulacionEntrevistaPersonalPregunta_Registro registro)
        {
            try
            {
                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioRegistro = 1; // (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                object respuesta = _convocatoria_Servicio.RegistrarPreguntaMaestra(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult IniciarEntrevistaPreguntasMaestras(PostulacionEntrevistaPersonal_Registro peticion)
        {
            peticion.IdUsuarioRegistro = 1;
            object respuesta = _convocatoria_Servicio.IniciarEntrevistaPreguntasMaestras(peticion);
            //respuesta.Contrasena = HttpUtility.UrlEncode(new Crypto().Encriptar(peticion.IdConvocatoria + "|" + VariablesWeb.ConsultaInformacion.iCodUsuario.ToString()));

            return Json(new { success = "True", responseText = respuesta });
        }
        [AllowAnonymous]
        public string postArgumentsEntrevistaPDF(string type, string IdEvaluacion, string IdTrabajador, string IdExamen, string IdPresento)
        {
            // CREACION DEL FORMATO DE ENTREVISTA
            PostulacionEvaluacionEntrevista_Registro peticion = new PostulacionEvaluacionEntrevista_Registro() { IdEvaluacion = Convert.ToInt32(IdEvaluacion) };
            PostulacionEvaluacionEntrevista_Registro objEvaluacion = _convocatoria_Servicio.ObtenerInformacionEntrevistaPersonal(peticion);
            Empleado_Registro objEmpleado = _empleado_Servicio.ObtenerParaEditar(new Empleado_Request() { IdEmpleado = Convert.ToInt32(IdTrabajador) });
            List<PostulacionEntrevistaPersonalPregunta_Registro> lista = _convocatoria_Servicio.ListarEntrevistaPersonalPreguntas(peticion).ToList();
            
            Stream pdfStream = GenerarFormatoEntrevistaPdf(objEvaluacion, objEmpleado, lista, Convert.ToInt32(IdExamen), Convert.ToInt32(IdPresento));

            String fileName = String.Format("{0}_{1}{2}_{3}.pdf", IdEvaluacion, IdTrabajador, IdExamen, DateTime.Now.Year); 
            String path = Path.Combine(Server.MapPath("~/temp"), fileName);
            using (FileStream outputFileStream = new FileStream(path, FileMode.Create))
            {
                pdfStream.CopyTo(outputFileStream);
            }

            ////System.IO.File.Create()
            //new FileStream()
            //return File(pdfStream, "application/pdf"); // ("Ficha", "_Layout_Blank", oAsistenciaTecnicaDetalle_Registro);
            
            string rpta = "";
            StringBuilder sb = new StringBuilder();
            String url = ConfigurationManager.AppSettings["SERVER_PATH"];
            String fileNameFirmado = String.Format("{0}_{1}{2}_{3}_F.pdf", IdEvaluacion, IdTrabajador, IdExamen, DateTime.Now.Year);

            if (type == "W")
            {
                sb.Append("{");
                //
                sb.Append("\"app\":\"pdf\",");
                //
                sb.Append("\"clientId\":\"");
                sb.Append(ConfigurationManager.AppSettings["CLIENTID"]);
                sb.Append("\",");
                //
                sb.Append("\"clientSecret\":\"");
                sb.Append(ConfigurationManager.AppSettings["CLIENTSECRET"]);
                sb.Append("\",");
                //
                sb.Append("\"idFile\":\"001\","); //estaba el valor 100
                //
                sb.Append("\"type\":\"W\",");
                //
                sb.Append("\"outputFile\":\"");
                sb.Append(fileNameFirmado);   //Nombre del archivo a generar
                sb.Append("\",");

                sb.Append("\"protocol\":\"T\",");
                //
                sb.Append("\"fileDownloadUrl\":\"");
                sb.Append(String.Format("{0}{1}{2}", url, "temp/", fileName));
                //sb.Append("temp/firmado/20191211_project-files.7z");  //Archivo a leer/firmar
                sb.Append("\",");
                //
                sb.Append("\"fileUploadUrl\":\"");
                sb.Append(url);
                sb.Append("Convocatoria/upload");
                sb.Append("\",");
                //
                sb.Append(String.Format("\"contentFile\":\"{0}\",", fileName));
                sb.Append("\"isSignatureVisible\":\"true\",");
                //
                sb.Append("\"dcfilter\":\".*FIR.*|.*FAU.*\",");
                sb.Append("\"stampAppearanceId\":\"0\",");
                sb.Append(String.Format("\"fileDownloadLogoUrl\":\"{0}{1}{2}", url, "Content/img/iLogo1.png", "\","));
                sb.Append(String.Format("\"fileDownloadStampUrl\":\"{0}{1}{2}", url, "Content/img/iFirma1.png", "\","));
                sb.Append("\"pageNumber\":\"0\",");
                sb.Append("\"posx\":\"140\",");
                sb.Append("\"posy\":\"630\",");
                sb.Append("\"fontSize\":\"7\",");
                sb.Append("\"timestamp\":\"false\",");
                sb.Append("\"reason\":\"");
                sb.Append("Miembro de Comité de Selección");
                sb.Append("\",");
                sb.Append("\"maxFileSize\":\"5242880\"");

                sb.Append("}");
            }
            byte[] byt = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            rpta = Convert.ToBase64String(byt);
            return rpta;
        }

        [AllowAnonymous]
        public void upload()
        {
            try
            {
                string[] claves = Request.Form.AllKeys;
                string pathUpload = Server.MapPath("~/temp/firmado/");
                string idFile;
                string fileName = String.Empty;
                string filePath = String.Empty;
                int cFile = 0;
                if (!System.IO.Directory.Exists(pathUpload))
                    System.IO.Directory.CreateDirectory(pathUpload);
                foreach (string clave in claves)
                {
                    idFile = clave;
                    fileName = Path.GetFileName(Request.Files[cFile].FileName);
                    filePath = Path.Combine(pathUpload, fileName);
                    if (System.IO.File.Exists(filePath)) System.IO.File.Delete(filePath);
                    Request.Files[cFile].SaveAs(filePath);
                    cFile++;
                }

                PostulacionEvaluacionEntrevista_Registro registro = new PostulacionEvaluacionEntrevista_Registro();
                registro.IdUsuarioModificacion = 1;
                registro.IdEvaluacion = Convert.ToInt32(fileName.Split('_')[0]);

                using (Stream str = new FileStream(filePath, FileMode.Open, FileAccess.Read)) {
                    BinaryReader Br = new BinaryReader(str);
                    Byte[] FileDet = Br.ReadBytes((Int32)str.Length);
                    registro.archivo = FileDet;
                }

                object respuesta = _convocatoria_Servicio.ActualizarActaEntrevistaPersonal(registro);

                Response.StatusCode = 200;

            }
            catch (Exception ex)
            {
                //if (!System.IO.File.Exists(Path.Combine(filePathExtractor + "\\log.txt"))) System.IO.File.Create(Path.Combine(filePathExtractor + "\\log.txt"));
                //using (StreamWriter sw = System.IO.File.AppendText(Path.Combine(filePath + "\\log.txt")))
                //{
                //    sw.WriteLine(String.Format("{0}-{1}-{2}", DateTime.Now.ToString(), ex.Message, ex.StackTrace));
                //}
                Response.StatusCode = 500;
            }
        }
        //public FileResult Ficha()
        //{
        //    Int32 IdConvocatoria = (Request.QueryString.Get("IdConvocatoria") == null ? 0 : Int32.Parse(Request.QueryString["IdConvocatoria"]));
        //    EmpleadoContrato_Registro objContrato = _contrato_Servicio.ObtenerParaEditar(new Contrato_Request() { IdContrato = IdContrato, NroDocumento = "", NroContrato = "", Nombre = "", Estado = 0 });

        //    var fileName = "Resultado_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".docx";
        //    string fullPathOri = Path.Combine(Server.MapPath("~/Templates/Contrato/formato"), "CONTRATO_ADMINISTRATIVO_SERVICIOS_N.docx");
        //    string fullPathNew = Path.Combine(Server.MapPath("~/Templates/Contrato"), fileName);
        //    using (var doc = DocX.Load(fullPathOri))
        //    {
        //        doc.ReplaceText("<NRO_CONTRATO>", String.Format("{0}-{1}", objContrato.NroContrato.ToString().PadLeft(3, '0'), objContrato.Anio));
        //        doc.ReplaceText("<NOMBRE>", String.Format("{0} {1} {2}", objContrato.Nombre, objContrato.Paterno, objContrato.Materno));
        //        doc.ReplaceText("<DNI>", objContrato.NroDocumento);
        //        doc.ReplaceText("<RUC>", objContrato.RUC);
        //        doc.ReplaceText("<DOMICILIO>", String.Format("{0}-{1}", objContrato.Domicilio, objContrato.Ubigeo));
        //        doc.ReplaceText("<CARGO>", objContrato.NombreCargo);
        //        doc.ReplaceText("<DEPENDENCIA>", objContrato.NombreOficina);
        //        doc.ReplaceText("<PROCESO>", objContrato.NombreProceso);
        //        doc.ReplaceText("<INICIO>", objContrato.FechaInicio.Value.ToLongDateString().Substring(objContrato.FechaInicio.Value.ToLongDateString().IndexOf(',') + 2));
        //        doc.ReplaceText("<FIN>", (objContrato.IdTipoLimite == 1 ? objContrato.FechaCese.Value.ToLongDateString().Substring(objContrato.FechaCese.Value.ToLongDateString().IndexOf(',') + 2) : "AL FINALIZAR LA DESIGNACIÓN"));
        //        doc.ReplaceText("<REMUNERACION>", objContrato.Remuneracion.ToString("C"));
        //        doc.ReplaceText("<FECHA>", DateTime.Now.ToLongDateString().Substring(DateTime.Now.ToLongDateString().IndexOf(',') + 2));

        //        doc.SaveAs(fullPathNew);
        //    }

        //    FileContentResult result = new FileContentResult(System.IO.File.ReadAllBytes(fullPathNew), "application/msword") { FileDownloadName = fileName };

        //    return result;
        //}

    }
}