using MIDIS.ORI.Entidades;
using MIDIS.ORI.LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using MVCSisGesRRHH.Models;
using System.Text;
using System.IO;
using System.IO.Compression;
using CrystalDecisions.CrystalReports.Engine;
using Xceed.Words.NET;
using Xceed.Document.NET;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Threading;

namespace MVCSisGesRRHH.Controllers
{
    public class ContratoController: Controller
	{
        private readonly T_genm_empleado_LN _empleado_Servicio = new T_genm_empleado_LN();
        private readonly T_genm_contrato_LN _contrato_Servicio = new T_genm_contrato_LN();
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
        public JsonResult ListarContratos(Contrato_Request peticion)
        {
            peticion.Nombre = "%" + (String.IsNullOrEmpty(peticion.Nombre) ? "" : peticion.Nombre.ToUpper()) + "%";
            object respuesta = _contrato_Servicio.ListarContratos(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Authorize]
        public JsonResult ListarEstados()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("-1", "--Todos--"));
            lista.Add(new Estado_Response("0", "PRE REGISTRO"));
            lista.Add(new Estado_Response("1", "FIRMADO"));
            lista.Add(new Estado_Response("5", "ANULADO"));
            
            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Authorize]
        public JsonResult ListarEmpleadosBoleta(Empleado_Registro peticion)
        {
            object respuesta = _empleado_Servicio.ListarEmpleadosBoleta(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public JsonResult ListarEmpleadosSisper(Empleado_Registro peticion)
        {
            object respuesta = _empleado_Servicio.ListarEmpleadosSisper(peticion);
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
        [Authorize]
        public JsonResult ListarTipoDeIngreso()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("1", "INDETERMINADO"));
            lista.Add(new Estado_Response("2", "TEMPORAL"));
            lista.Add(new Estado_Response("3", "TRANSITORIO"));
            lista.Add(new Estado_Response("4", "DESIGNADO"));
            lista.Add(new Estado_Response("5", "JUDICIAL"));

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Authorize]
        public JsonResult ListarModalidadTrabajo()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("1", "PRESENCIAL"));
            lista.Add(new Estado_Response("2", "TELETRABAJO TOTAL"));
            lista.Add(new Estado_Response("3", "TELETRABAJO PARCIAL"));
            
            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize]
        public JsonResult Validar(BoletaCarga_Registro registro)
        {
            registro.IdUsuarioRegistro = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iCodUsuario);
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
        public JsonResult Remover(BoletaCarga_Registro registro)
        {
            return Json(new { success = "True" });
        }
        [HttpPost]
        [Authorize]
        public JsonResult Registrar(EmpleadoContrato_Registro registro)
        {
            try
            {
                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioRegistro = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iCodUsuario);


                // KMM: COMENTADO POR AHORA PARA HACER PRUEBAS CON GOOGLE DRIVE
                //PostulanteInformacion_Registro postulante = _postulante_Servicio.ObtenerPostulanteFicha(new PostulanteInformacion_Registro() { IdPostulante = registro.IdPostulante, IdPostulacion = registro.IdPostulacion, IdConvocatoria = registro.IdConvocatoria });
                //object respuesta = _contrato_Servicio.RegistrarContrato(registro, postulante);

                //string[] Scopes = new[] { DriveService.Scope.DriveReadonly, DriveService.Scope.Drive };
                string[] Scopes = new[] { DriveService.Scope.Drive, DriveService.Scope.DriveFile, DriveService.Scope.DriveAppdata };
                //string ApplicationName = "LegajoOGRH";
                string ApplicationName = "bono600";
                UserCredential credential;
                using (var stream = new FileStream(Server.MapPath(@"..\bono600.json"), FileMode.Open, FileAccess.Read))
                //using (var stream = new FileStream(Server.MapPath(@"..\credentials.json"), FileMode.Open, FileAccess.Read))
                {
                    // The file token.json stores the user's access and refresh tokens, and is created
                    // automatically when the authorization flow completes for the first time.
                    string credPath = Server.MapPath(@"..\token.json");
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets, Scopes, "kmiota@midis.gob.pe", CancellationToken.None, new FileDataStore(credPath, true)).Result;
                    //credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets, Scopes, "web-api@bono600.iam.gserviceaccount.com", CancellationToken.None, new FileDataStore(credPath, true)).Result;
                    Console.WriteLine("Credential file saved to: " + credPath);
                }

                // Create Drive API service.
                var service = new DriveService(new BaseClientService.Initializer() { HttpClientInitializer = credential, ApplicationName = ApplicationName });

                // Define parameters of request.
                FilesResource.ListRequest listRequest = service.Files.List();
                listRequest.PageSize = 10;
                // listRequest.Fields = "nextPageToken, files(id, name)"
                listRequest.Q = "mimeType = 'application/vnd.google-apps.folder' and name = 'Legajo'";

                // List files.
                IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;
                Console.WriteLine("Files:");
                if (files != null && files.Count > 0)
                {
                    // buscamos por la carpeta del trabajador
                    String strIdLegajo = files[0].Id;

                    // recuperamos informacion del postulante 
                    Legajo_Registro nuevo = new Legajo_Registro();
                    nuevo.TipoDocumento = Convert.ToInt32(registro.TipoDocumento);
                    nuevo.NroDocumento = registro.NroDocumento;
                    nuevo.Nombre = registro.Nombre;
                    nuevo.Paterno = registro.Paterno;
                    nuevo.Materno = registro.Materno;
                    nuevo.Foto = registro.Foto;
                    nuevo.Domicilio = registro.Domicilio;
                    nuevo.RUC = registro.RUC;
                    nuevo.Ubigeo = registro.Ubigeo;
                    nuevo.DescripcionUbigeo = registro.DescripcionUbigeo;
                    nuevo.FechaRegistro = registro.FechaRegistro;
                    nuevo.IdUsuarioRegistro = registro.IdUsuarioRegistro;
                    
                    List<Legajo_Registro> lista = new T_genm_legajo_LN().ListarLegajos(new Postulante_Request() { IdTipoDocumento = Convert.ToInt32(registro.TipoDocumento), NroDocumento = registro.NroDocumento }).ToList();
                    if (lista != null && lista.Count > 0)
                    {
                        
                    }
                    else
                    {
                        Google.Apis.Drive.v3.Data.File fileRaiz = new Google.Apis.Drive.v3.Data.File();
                        fileRaiz.Name = registro.NombreLegajo;
                        fileRaiz.MimeType = "application/vnd.google-apps.folder";
                        fileRaiz.Parents = new List<String> { strIdLegajo };
                        FilesResource.CreateRequest createRequest = service.Files.Create(fileRaiz);
                        createRequest.Fields = "id";
                        Google.Apis.Drive.v3.Data.File file = createRequest.Execute();
                        nuevo.IdDrive = file.Id;

                        // creamos la carpeta de filiacion
                        Google.Apis.Drive.v3.Data.File fileSeccion1 = new Google.Apis.Drive.v3.Data.File();
                        fileSeccion1.Name = "Sección I: Filiación e Identificación Personal";
                        fileSeccion1.MimeType = "application/vnd.google-apps.folder";
                        fileSeccion1.Parents = new List<String> { file.Id };
                        FilesResource.CreateRequest createRequest1 = service.Files.Create(fileSeccion1);
                        createRequest1.Fields = "id";
                        Google.Apis.Drive.v3.Data.File file1 = createRequest1.Execute();
                        nuevo.IdDrive1 = file1.Id;

                        // creamos la carpeta de estudios y capacitacion
                        Google.Apis.Drive.v3.Data.File fileSeccion2 = new Google.Apis.Drive.v3.Data.File();
                        fileSeccion2.Name = "Sección II: Estudios y Capacitación";
                        fileSeccion2.MimeType = "application/vnd.google-apps.folder";
                        fileSeccion2.Parents = new List<String> { file.Id };
                        FilesResource.CreateRequest createRequest2 = service.Files.Create(fileSeccion2);
                        createRequest2.Fields = "id";
                        Google.Apis.Drive.v3.Data.File file2 = createRequest2.Execute();
                        nuevo.IdDrive2 = file2.Id;

                        // creamos la carpeta de contratos
                        Google.Apis.Drive.v3.Data.File fileSeccion3 = new Google.Apis.Drive.v3.Data.File();
                        fileSeccion3.Name = "Sección III: Contratos";
                        fileSeccion3.MimeType = "application/vnd.google-apps.folder";
                        fileSeccion3.Parents = new List<String> { file.Id };
                        FilesResource.CreateRequest createRequest3 = service.Files.Create(fileSeccion3);
                        createRequest3.Fields = "id";
                        Google.Apis.Drive.v3.Data.File file3 = createRequest3.Execute();
                        nuevo.IdDrive3 = file3.Id;

                        // creamos la carpeta de desplazamientos
                        Google.Apis.Drive.v3.Data.File fileSeccion4 = new Google.Apis.Drive.v3.Data.File();
                        fileSeccion4.Name = "Sección IV: Desplazamientos";
                        fileSeccion4.MimeType = "application/vnd.google-apps.folder";
                        fileSeccion4.Parents = new List<String> { file.Id };
                        FilesResource.CreateRequest createRequest4 = service.Files.Create(fileSeccion4);
                        createRequest4.Fields = "id";
                        Google.Apis.Drive.v3.Data.File file4 = createRequest4.Execute();
                        nuevo.IdDrive4 = file4.Id;

                        // creamos la carpeta de experiencia laboral
                        Google.Apis.Drive.v3.Data.File fileSeccion5 = new Google.Apis.Drive.v3.Data.File();
                        fileSeccion5.Name = "Sección V: Experiencia Laboral";
                        fileSeccion5.MimeType = "application/vnd.google-apps.folder";
                        fileSeccion5.Parents = new List<String> { file.Id };
                        FilesResource.CreateRequest createRequest5 = service.Files.Create(fileSeccion5);
                        createRequest5.Fields = "id";
                        Google.Apis.Drive.v3.Data.File file5 = createRequest5.Execute();
                        nuevo.IdDrive5 = file5.Id;

                        // creamos la carpeta de evaluaciones
                        Google.Apis.Drive.v3.Data.File fileSeccion6 = new Google.Apis.Drive.v3.Data.File();
                        fileSeccion6.Name = "Sección VI: Evaluaciones";
                        fileSeccion6.MimeType = "application/vnd.google-apps.folder";
                        fileSeccion6.Parents = new List<String> { file.Id };
                        FilesResource.CreateRequest createRequest6 = service.Files.Create(fileSeccion6);
                        createRequest6.Fields = "id";
                        Google.Apis.Drive.v3.Data.File file6 = createRequest6.Execute();
                        nuevo.IdDrive6 = file6.Id;

                        // creamos la carpeta de otros
                        Google.Apis.Drive.v3.Data.File fileSeccion7 = new Google.Apis.Drive.v3.Data.File();
                        fileSeccion7.Name = "Sección VII: Otros";
                        fileSeccion7.MimeType = "application/vnd.google-apps.folder";
                        fileSeccion7.Parents = new List<String> { file.Id };
                        FilesResource.CreateRequest createRequest7 = service.Files.Create(fileSeccion7);
                        createRequest7.Fields = "id";
                        Google.Apis.Drive.v3.Data.File file7 = createRequest7.Execute();
                        nuevo.IdDrive7 = file7.Id;



                        if (registro.Estado == 0)
                        {
                            // PARA GANADORES DE PROCESOS CAS
                            //.ObtenerParaEditar()
                            Convocatoria_Registro item = new T_genm_convocatoria_LN().ObtenerConvocatoriaDocumento(new Convocatoria_Request() { IdConvocatoria = registro.IdConvocatoria });

                            //case "1": return File(item.fileRequerimiento, "application/pdf", String.Format("Anexo_01_{0}.pdf", idConvocatoria));
                            //case "2": return File(item.fileCertificacion, "application/pdf", String.Format("Anexo_04_{0}.pdf", idConvocatoria));
                            //case "3": return File(item.fileComite, "application/pdf", String.Format("Anexo_03_{0}.pdf", idConvocatoria));
                            //case "10": return File(item.fileActaCurri, "application/pdf", String.Format("Anexo_07_{0}.pdf", idConvocatoria));

                            Google.Apis.Drive.v3.Data.File fileDocumento1 = new Google.Apis.Drive.v3.Data.File();
                            fileDocumento1.Name = (String.Format("{0}_{1}.pdf", "Anexo01", "Requerimiento"));
                            fileDocumento1.Parents = new List<String> { file1.Id };
                            Dictionary<string, string> properties1 = new Dictionary<string, string>();
                            properties1.Add("idTipo", "001");
                            fileDocumento1.Properties = properties1;
                            using (var streamAux = new MemoryStream(item.fileRequerimiento, 0, item.fileRequerimiento.Length))
                            {
                                FilesResource.CreateMediaUpload createUpload = service.Files.Create(fileDocumento1, streamAux, "application/pdf");
                                createUpload.Fields = "id, parents";
                                createUpload.Upload();
                            }

                            Google.Apis.Drive.v3.Data.File fileDocumento2 = new Google.Apis.Drive.v3.Data.File();
                            fileDocumento2.Name = (String.Format("{0}_{1}.pdf", "Anexo04", "Certificacion"));
                            fileDocumento2.Parents = new List<String> { file1.Id };
                            Dictionary<string, string> properties2 = new Dictionary<string, string>();
                            properties2.Add("idTipo", "002");
                            fileDocumento2.Properties = properties2;
                            using (var streamAux = new MemoryStream(item.fileCertificacion, 0, item.fileCertificacion.Length))
                            {
                                FilesResource.CreateMediaUpload createUpload = service.Files.Create(fileDocumento2, streamAux, "application/pdf");
                                createUpload.Fields = "id, parents";
                                createUpload.Upload();
                            }

                            Google.Apis.Drive.v3.Data.File fileDocumento3 = new Google.Apis.Drive.v3.Data.File();
                            fileDocumento3.Name = (String.Format("{0}_{1}.pdf", "Anexo03", "Comite"));
                            fileDocumento3.Parents = new List<String> { file1.Id };
                            Dictionary<string, string> properties = new Dictionary<string, string>();
                            properties.Add("idTipo", "003");
                            fileDocumento3.Properties = properties;
                            using (var streamAux = new MemoryStream(item.fileComite, 0, item.fileComite.Length))
                            {
                                FilesResource.CreateMediaUpload createUpload = service.Files.Create(fileDocumento3, streamAux, "application/pdf");
                                createUpload.Fields = "id, parents";
                                createUpload.Upload();
                            }
                        }


                        new T_genm_legajo_LN().InsertarLegajo(nuevo);
                    }


                    //foreach (Google.Apis.Drive.v3.Data.File File in files)
                    //    Console.WriteLine("{0} ({1})", File.Name, File.Id);
                }
                else
                {
                    Console.WriteLine("No files found.");
                }


                //KMM: comentado para prueba de google drive
                //return Json(new { success = "True", responseText = respuesta });
                return Json(new { success = "True", responseText = "1" });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        [Authorize]
        public JsonResult RegistrarContratoArchivo(EmpleadoContrato_Registro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iCodUsuario);

                String nameFile = String.Empty;
                for (Int32 j = 0; j < registro.formatos.ToList().Count; j++) {
                    HttpPostedFileBase postfile = ((HttpPostedFileBase[])(registro.formatos.ToList())[j])[0];
                    if (postfile.ContentLength > 0) {
                        nameFile = postfile.FileName;

                        Stream str = postfile.InputStream;
                        BinaryReader Br = new BinaryReader(str);
                        Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                        registro.archivo = FileDet;
                    }
                }

                object respuesta = _contrato_Servicio.RegistrarContratoArchivo(registro);
                EmpleadoContrato_Registro objContrato = _contrato_Servicio.ObtenerParaEditar(new Contrato_Request(){IdContrato = registro.IdContrato, Nombre = "", Estado = -1 });
                PostulanteInformacion_Registro obj = _postulante_Servicio.ObtenerPostulanteFicha(new PostulanteInformacion_Registro() { IdPostulante = objContrato.IdPostulante, IdPostulacion = objContrato.IdPostulacion, IdConvocatoria = objContrato.IdConvocatoria, Nombre= objContrato.Nombre });
                this.SendEmail(obj, "3");

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        public FileResult Ficha()
        {
            Int32 IdContrato = (Request.QueryString.Get("IdContrato") == null ? 0 : Int32.Parse(Request.QueryString["IdContrato"]));
            EmpleadoContrato_Registro objContrato = _contrato_Servicio.ObtenerParaEditar(new Contrato_Request() { IdContrato = IdContrato, NroDocumento = "", NroContrato = "", Nombre = "", Estado = 0 });

            var fileName = "Contrato_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".docx";
            string fullPathOri = Path.Combine(Server.MapPath("~/Templates/Contrato/formato"), "CONTRATO_ADMINISTRATIVO_SERVICIOS_N.docx");
            string fullPathNew = Path.Combine(Server.MapPath("~/Templates/Contrato"), fileName);
            using (var doc = DocX.Load(fullPathOri))
            {
                doc.ReplaceText("<NRO_CONTRATO>", String.Format("{0}-{1}", objContrato.NroContrato.ToString().PadLeft(3, '0'), objContrato.Anio));
                doc.ReplaceText("<NOMBRE>", String.Format("{0} {1} {2}", objContrato.Nombre, objContrato.Paterno, objContrato.Materno));
                doc.ReplaceText("<DNI>", objContrato.NroDocumento);
                doc.ReplaceText("<RUC>", objContrato.RUC);
                doc.ReplaceText("<DOMICILIO>", String.Format("{0}-{1}", objContrato.Domicilio, objContrato.Ubigeo));
                doc.ReplaceText("<CARGO>", objContrato.NombreCargo);
                doc.ReplaceText("<DEPENDENCIA>", objContrato.NombreOficina);
                doc.ReplaceText("<PROCESO>", objContrato.NombreProceso);
                doc.ReplaceText("<INICIO>", objContrato.FechaInicio.Value.ToLongDateString().Substring(objContrato.FechaInicio.Value.ToLongDateString().IndexOf(',') + 2));
                doc.ReplaceText("<FIN>", (objContrato.IdTipoLimite == 1 ? objContrato.FechaCese.Value.ToLongDateString().Substring(objContrato.FechaCese.Value.ToLongDateString().IndexOf(',') + 2) : "AL FINALIZAR LA DESIGNACIÓN"));
                doc.ReplaceText("<REMUNERACION>", objContrato.Remuneracion.ToString("C"));
                doc.ReplaceText("<FECHA>", DateTime.Now.ToLongDateString().Substring(DateTime.Now.ToLongDateString().IndexOf(',') + 2));
                
                doc.SaveAs(fullPathNew);
            }

            FileContentResult result = new FileContentResult(System.IO.File.ReadAllBytes(fullPathNew), "application/msword") { FileDownloadName = fileName };

            return result;
        }
        [HttpPost]
        public JsonResult ObtenerParaEditar(Contrato_Request peticion)
        {
            object respuesta = _contrato_Servicio.ObtenerParaEditar(peticion);

            return Json(respuesta);
        }
        //[HttpPost]
        //public JsonResult ExportarBoleta(String anio, String mes)
        //{
        //    var directory = "Boletas_Midis_" + DateTime.Now.ToString("yyyyMMddHHmm");
        //    //var fileName = "Boletas_Midis_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".zip";
        //    string fullPath = Path.Combine(Server.MapPath("~/temp"), directory);
        //    DirectoryInfo dir = null;

        //    if (!Directory.Exists(fullPath)) dir = Directory.CreateDirectory(fullPath);
            
        //    Empleado_Registro registro = new Empleado_Registro();
        //    registro.Anio = anio;
        //    registro.Mes = mes;

        //    try
        //    {
        //        // RECORREMOS LA LISTA DE EMPLEADOS
        //        List<EmpleadoSisper_Registro> lista = _empleado_Servicio.ListarEmpleadosSisper(registro).ToList();
        //        List<EmpleadoConceptoSisper_Registro> lstConcepto = (List<EmpleadoConceptoSisper_Registro>)_empleado_Servicio.ListarEmpleadoConceptoSisper(registro);
        //        List<EmpleadoConceptoSisper_Registro> lstConceptoIngreso = null;
        //        List<EmpleadoConceptoSisper_Registro> lstConceptoDescuento = null;
        //        List<EmpleadoConceptoSisper_Registro> lstConceptoAporte = null;

        //        foreach (EmpleadoSisper_Registro obj in lista)
        //        {
        //            using (ReportDocument report = new ReportDocument())
        //            {
        //                lstConceptoIngreso = lstConcepto.Where(x => x.TipoConcepto == "0" && 
        //                                                            x.Trabajador == obj.Trabajador &&
        //                                                            x.IdPlanilla == obj.IdPlanilla &&
        //                                                            x.TipoPlanilla == obj.TipoPlanilla).ToList();
        //                lstConceptoDescuento = lstConcepto.Where(x => x.TipoConcepto != "0" &&
        //                                                              x.Trabajador == obj.Trabajador &&
        //                                                              x.IdPlanilla == obj.IdPlanilla &&
        //                                                              x.TipoPlanilla == obj.TipoPlanilla).Where(x => x.TipoConcepto != "9" &&
        //                                                                                                             x.Trabajador == obj.Trabajador &&
        //                                                                                                             x.IdPlanilla == obj.IdPlanilla &&
        //                                                                                                             x.TipoPlanilla == obj.TipoPlanilla).ToList();
        //                lstConceptoAporte = lstConcepto.Where(x => x.TipoConcepto == "9" &&
        //                                                           x.Trabajador == obj.Trabajador &&
        //                                                           x.IdPlanilla == obj.IdPlanilla &&
        //                                                           x.TipoPlanilla == obj.TipoPlanilla).ToList();

        //                try
        //                {
        //                    report.Load(System.Web.HttpContext.Current.Server.MapPath("~/Reportes/Formato1.rpt"));
        //                    report.FileName = System.Web.HttpContext.Current.Server.MapPath("~/Reportes/Formato1.rpt");
        //                    report.OpenSubreport("lstIngreso").SetDataSource(lstConceptoIngreso);
        //                    report.OpenSubreport("lstDescuento").SetDataSource(lstConceptoDescuento);
        //                    report.OpenSubreport("lstAporte").SetDataSource(lstConceptoAporte);
        //                    report.SetDataSource(lista.Where(x => x.Trabajador == obj.Trabajador && 
        //                                                          x.IdPlanilla == obj.IdPlanilla && 
        //                                                          x.TipoPlanilla == obj.TipoPlanilla).ToList());

        //                    String fileName = Path.Combine(fullPath, String.Format("{0}-{1}-{2}-{3}{4}-{5}", obj.NroDocumento, anio, mes.PadLeft(2, '0'), obj.IdPlanilla, obj.TipoPlanilla, "01.pdf"));
        //                    FileStream file = new FileStream(fileName, FileMode.Create, FileAccess.Write);
        //                    report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat).CopyTo(file);
        //                    file.Close();
        //                }
        //                catch (Exception)
        //                {
        //                    //report.Dispose();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { fileName = "", responseText = ex.Message });
        //    }

        //    return Json(new { fileName = directory, responseText = "" });
        //}

        [AllowAnonymous]
        public ActionResult DescargarArchivo(string id)
        {
            Contrato_Request peticion = new Contrato_Request();
            peticion.IdContrato = Int32.Parse(id);
            peticion.Nombre = String.Empty;
            peticion.Estado = -1;

            var lista = _contrato_Servicio.ListarContratos(peticion).Select(p => new { p.IdContrato, p.NroContrato, p.Anio, p.NombreContrato, p.archivo });
            var item = lista.Where(x => x.IdContrato == peticion.IdContrato).SingleOrDefault();
            //if (item != null) {
            //    if (arraydata.Length > 3) {
            //        if (arraydata[3].ToString() == "1")
            //        {
            //            //ACTUALIZAMOS LA FECHA DE RECEPCION EN LA BASE DE DATOS
            //            _empleado_Servicio.ActualizarRecepcion(peticion);
            //        }
            //    }
            //}

            return File(item.archivo, "application/pdf", item.NombreContrato + ".pdf");
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
                    //if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CCcorreoCAS"].ToString()))
                    //    msg.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["CCcorreoCAS"].ToString()));

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
                html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Correo/EvaluacionProceso.txt"));
                html = html.Replace("_PROCESOCAS_", postulante.NombreProceso);
                html = html.Replace("_URLINTERNO_", ConfigurationManager.AppSettings["URL_INTERNO"].ToString()); //HttpUtility.UrlEncode(

                MailMessage msg = new MailMessage();
                msg.To.Add(new MailAddress(ConfigurationManager.AppSettings["CCcorreoCAS"].ToString()));
                //if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CCcorreoCAS"].ToString()))
                //    msg.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["CCcorreoCAS"].ToString()));

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

            return exitoEnvio;
        }

	}
}