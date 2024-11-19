using MIDIS.ORI.Entidades;
using MIDIS.ORI.LogicaNegocio;
using MIDIS.Utiles;
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
using CrystalDecisions.CrystalReports.Engine;
using SelectPdf;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using MVCSisGesRRHH.Controllers.IRMA_Autentica;
using System.Net.Http.Headers;
using MVCSisGesRRHH.Controllers.IRMA_Reniec;

namespace MVCSisGesRRHH.Controllers
{
    [Authorize]
	public class PostulanteController: Controller
	{
        private readonly T_genm_empleado_LN _empleado_Servicio = new T_genm_empleado_LN();
        private readonly T_genm_postulante_LN _postulante_Servicio = new T_genm_postulante_LN();
        private readonly T_genm_dependencia_LN _dependencia_Servicio = new T_genm_dependencia_LN();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
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
        public JsonResult ListarEstados()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("0", "--Todos--"));
            lista.Add(new Estado_Response("1", "REGISTRO"));
            lista.Add(new Estado_Response("2", "PENDIENTE DE FIRMA"));
            lista.Add(new Estado_Response("3", "CON CONTRATO FIRMADO"));

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarTipoDeDocumento()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("1", "DNI"));
            lista.Add(new Estado_Response("3", "CARNÉ DE EXTRANJERÍA"));

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarTipoDeBrevete()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("1", "AUTOMOVIL"));
            lista.Add(new Estado_Response("2", "MOTOCICLETA"));

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarTipoDePension()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("1", "AFP"));
            lista.Add(new Estado_Response("2", "ONP"));
            lista.Add(new Estado_Response("3", "SIN RÉGIMEN"));
            lista.Add(new Estado_Response("4", "SIN CÁLCULO"));

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarTipoDeAFP()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("1", "PROFUTURO"));
            lista.Add(new Estado_Response("2", "INTEGRA"));
            lista.Add(new Estado_Response("3", "PRIMA"));
            lista.Add(new Estado_Response("4", "HABITAT"));

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarTipoDeComisionAFP()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("1", "COMISIÓN POR FLUJO"));
            lista.Add(new Estado_Response("2", "COMISIÓN MIXTA"));

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarNuevoBanco()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("1", "BBVA"));
            lista.Add(new Estado_Response("2", "BCP"));

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarTipoVivienda()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("1", "PROPIA"));
            lista.Add(new Estado_Response("2", "ALQUILADA"));
            lista.Add(new Estado_Response("3", "FAMILIARES"));
            lista.Add(new Estado_Response("4", "PENSIÓN"));
            lista.Add(new Estado_Response("5", "TEMPORAL"));
            
            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarParentesco()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("1", "ESPOSO (A)"));
            lista.Add(new Estado_Response("2", "CONVIVIENTE"));
            lista.Add(new Estado_Response("3", "HIJO (A)"));
            lista.Add(new Estado_Response("4", "PADRE"));
            lista.Add(new Estado_Response("5", "MADRE"));
            lista.Add(new Estado_Response("6", "HERMANO (A)"));

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public ActionResult DescargarArchivo(String idPostulante, String idPostulacion, String idConvocatoria, String idTipo)
        {
            PostulanteInformacion_Registro peticion = new PostulanteInformacion_Registro();
            peticion.IdPostulante = Int32.Parse(idPostulante);
            peticion.IdPostulacion = Int32.Parse(idPostulacion);
            peticion.IdConvocatoria = Int32.Parse(idConvocatoria);
            peticion.TipoDocumento = Int32.Parse(idTipo);

            peticion = _postulante_Servicio.ObtenerPostulanteFichaDocumento(peticion);

            switch (idTipo)
            {
                case "1": return File(peticion.FileHojaVida, "image/jpeg", String.Format("Foto_{0}.jpg", idPostulante));
                case "2": return File(peticion.FileSustento, "application/pdf", String.Format("Voucher_{0}.pdf", idPostulante));
                case "3": return File(peticion.FileDDJJ, "application/pdf", String.Format("Formatos_{0}.pdf", idPostulante));
                case "4": 
                    Postulacion_Registro postulaAux = _postulante_Servicio.ObtenerPostulacionDocumento(new Postulacion_Registro() { IdPostulacion = Convert.ToInt32(idPostulacion), IdDetalle = 6001, IdTipo = 5 });                    
                    return File(postulaAux.FileDocumento , "application/pdf", String.Format("Formatos_{0}.pdf", idPostulante));
            }

            return null;
        }
        public FileResult HojaVida()
        {
            Int32 IdPostulante = (Request.QueryString.Get("IdPostulante") == null ? 0 : Int32.Parse(Request.QueryString["IdPostulante"]));
            Int32 IdPostulacion = (Request.QueryString.Get("IdPostulacion") == null ? 0 : Int32.Parse(Request.QueryString["IdPostulacion"]));
            Int32 IdConvocatoria = (Request.QueryString.Get("IdConvocatoria") == null ? 0 : Int32.Parse(Request.QueryString["IdConvocatoria"]));

            Postulante_Request peticion = new Postulante_Request() { IdPostulante = IdPostulante, IdPostulacion = IdPostulacion, IdConvocatoria = IdConvocatoria };
            PostulacionPostulante_Registro obj = _postulante_Servicio.ObtenerPostulacionPostulante(peticion);
            List<PostulacionDocumento_Registro> lstDocumento = _postulante_Servicio.ListarPostulacionDocumento(peticion).ToList();
            List<PostulacionEstudio_Registro> lstEstudio = _postulante_Servicio.ListarPostulacionEstudio(peticion).ToList();
            List<PostulacionCapacitacion_Registro> lstCapacitacion = _postulante_Servicio.ListarPostulacionCapacitacion(peticion).ToList();
            List<PostulacionExperiencia_Registro> lstExperiencia = _postulante_Servicio.ListarPostulacionExperiencia(peticion).ToList();
            Convocatoria_Registro objConvocatoria = new T_genm_convocatoria_LN().ObtenerParaEditar(new Convocatoria_Request() { IdConvocatoria = IdConvocatoria });

            Stream pdfStream = GenerarHojaVidaPdf(obj, lstDocumento, lstEstudio, lstCapacitacion, lstExperiencia, objConvocatoria);

            //oAsistenciaTecnicaDetalle_Registro.IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;
            //oAsistenciaTecnicaDetalle_Registro.FechaRegistro = DateTime.Now;
            //oAsistenciaTecnicaDetalle_Registro.ActaGenerada = true;
            //oAsistenciaTecnicaDetalle_Registro.Estado = 1;
            //new T_genm_negociacion_LN().Actualizar(oAsistenciaTecnicaDetalle_Registro);

            /*AsistenciaTecnica_Participante_Request peticionParticipantes = new AsistenciaTecnica_Participante_Request()
            {
                Estado = 1,
                IdAsistenciaTecnica = oAsistenciaTecnicaDetalle_Registro.IdAsistenciaTecnica
            };

            IEnumerable<AsistenciaTecnica_Participante_Response> participantes = _asistenciatecnica_detalle_Servicio.ListarParticipante(peticionParticipantes);

            foreach (AsistenciaTecnica_Participante_Response item in participantes)
            {
                byte[] bytesActa = ReadFully(pdfStream);
                UtilHelper.SendMail("Haz sido participante de una Asistencia Técnica, por favor revisar documento adjunto.", ConfigurationManager.AppSettings["AsistenciaTecnica.From.Address"], item.Correo, null, ConfigurationManager.AppSettings["AsistenciaTecnica.Bcc.Address"], "Asistencia Ténica - Acta N° " + oAsistenciaTecnicaDetalle_Registro.Codigo, bytesActa, "Acta.pdf");
            }
            
            pdfStream.Position = 0;*/
            return File(pdfStream, "application/pdf"); // ("Ficha", "_Layout_Blank", oAsistenciaTecnicaDetalle_Registro);
        }
        private Stream GenerarHojaVidaPdf(PostulacionPostulante_Registro oPostulante,
                                            List<PostulacionDocumento_Registro> lstDocumento,
                                            List<PostulacionEstudio_Registro> lstEstudio,
                                            List<PostulacionCapacitacion_Registro> lstCapacitacion,
                                            List<PostulacionExperiencia_Registro> lstExperiencia,
                                            Convocatoria_Registro objConvocatoria)
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

            string html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulante/formato_postulacion1.html"));

            //CultureInfo culture = new CultureInfo("es-PE");
            html = html.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            html = html.Replace("//unidad_organica", (String.IsNullOrEmpty(objConvocatoria.Dependencia) ? objConvocatoria.Organo : objConvocatoria.Dependencia));
            html = html.Replace("//cargo", objConvocatoria.NombreCargo);
            html = html.Replace("//proceso", objConvocatoria.NroConvocatoria);

            html = html.Replace("//paterno", oPostulante.Paterno);
            html = html.Replace("//materno", oPostulante.Materno);
            html = html.Replace("//nombres", oPostulante.Nombre);
            html = html.Replace("//sexo", (oPostulante.Sexo == "1" ? "F" : "M"));
            //html = html.Replace("//edad", oPostulante.Edad.ToString());
            html = html.Replace("//nacionalidad", oPostulante.Nacionalidad);
            html = html.Replace("//nacimiento", oPostulante.FechaNacimiento);
            html = html.Replace("//dia", DateTime.Now.Day.ToString());
            html = html.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
            html = html.Replace("//anio", DateTime.Now.Year.ToString());
            html = html.Replace("//nombre", String.Format("{0} {1} {2}", oPostulante.Nombre, oPostulante.Paterno, oPostulante.Materno));
            html = html.Replace("//dnifirma", (oPostulante.TipoDocumento == 1 ? "DNI: " : "CE: ") + oPostulante.NroDocumento);
            html = html.Replace("//dni", (oPostulante.TipoDocumento == 1 ? oPostulante.NroDocumento : String.Empty));
            //html = html.Replace("//extranjeria", (oFicha.TipoDocumento != 1 ? oFicha.NroDocumento : String.Empty));
            html = html.Replace("//direccion", oPostulante.Domicilio);
            html = html.Replace("//ubigeo", oPostulante.DescripcionUbigeo);
            html = html.Replace("//estado_civil", (oPostulante.IdEstadoCivil == 1 ? "SOLTERO(A)" : (oPostulante.IdEstadoCivil == 2 ? "CASADO(A)" : (oPostulante.IdEstadoCivil == 3 ? "DIVORCIADO(A)" : (oPostulante.IdEstadoCivil == 4 ? "VIUDO(A)" : "")))));
            html = html.Replace("//ruc", oPostulante.RUC);
            html = html.Replace("//email", oPostulante.CorreoElectronico.ToUpper());
            html = html.Replace("//fijo", oPostulante.Telefono);
            html = html.Replace("//celular", oPostulante.Celular);
            html = html.Replace("//ffaa", (oPostulante.FFAA == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//disc", (oPostulante.Discapacidad == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//depo", (oPostulante.Deportista == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));

            //string html2 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulante/ficha_personal2.html"));
            //// LISTAR FAMILIARES 
            //Postulante_Request peticion = new Postulante_Request()
            //{
            //    IdPostulante = oFicha.IdPostulante,
            //    IdPostulacion = oFicha.IdPostulacion,
            //    IdConvocatoria = oFicha.IdConvocatoria
            //};

            String strAux;
            String strEstudio = "<tr><td style='font-size: 9px; width: 150px;'>//nivel_e</td>" +
                                "<td style='font-size: 9px; width: 150px;'>//nivel_a</td>" +
                                "<td style='font-size: 9px; width: 200px;'>//especialidad</td>" +
                                "<td style='font-size: 9px; width: 200px;'>//institucion</td>" +
                                "<td style='font-size: 9px; width: 150px;'>//ciudad</td>" +
                                "<td style='font-size: 9px; width: 150px;'>//obtencion</td></tr>";
            String strEstudios = String.Empty;
            if (lstEstudio.Count > 0)
            {
                foreach (PostulacionEstudio_Registro item in lstEstudio)
                {
                    strAux = strEstudio;
                    strAux = strAux.Replace("//nivel_e", item.NombreGrado);
                    strAux = strAux.Replace("//nivel_a", item.NombreNivel);
                    strAux = strAux.Replace("//especialidad", item.Especialidad);
                    strAux = strAux.Replace("//institucion", item.Institucion);
                    strAux = strAux.Replace("//ciudad", item.Ciudad);
                    strAux = strAux.Replace("//obtencion", item.AnioMes);

                    strEstudios += strAux;
                }
            }
            else
            {
                strAux = strEstudio;
                strAux = strAux.Replace("//nivel_e", "&nbsp;");
                strAux = strAux.Replace("//nivel_a", "&nbsp;");
                strAux = strAux.Replace("//especialidad", "&nbsp;");
                strAux = strAux.Replace("//institucion", "&nbsp;");
                strAux = strAux.Replace("//ciudad", "&nbsp;");
                strAux = strAux.Replace("//obtencion", "&nbsp;");

                strEstudios += strAux;
            }

            html = html.Replace("//formacion", strEstudios);

            String strCapacitacion = "<tr><td style='font-size: 9px; width: 250px;'>//capacitacion</td>" +
                                    "<td style='font-size: 9px; width: 250px;'>//institucion</td>" +
                                    "<td style='font-size: 9px; width: 200px;'>//ciudad</td>" +
                                    "<td style='font-size: 9px; width: 100px;'>//inicio</td>" +
                                    "<td style='font-size: 9px; width: 100px;'>//fin</td>" +
                                    "<td style='font-size: 9px; width: 100px;'>//horas</td></tr>";
            String strCapacitaciones = String.Empty;
            if (lstCapacitacion.Count > 0)
            {
                foreach (PostulacionCapacitacion_Registro item in lstCapacitacion)
                {
                    strAux = strCapacitacion;
                    strAux = strAux.Replace("//capacitacion", item.Especialidad);
                    strAux = strAux.Replace("//institucion", item.Institucion);
                    strAux = strAux.Replace("//ciudad", item.Ciudad);
                    strAux = strAux.Replace("//inicio", item.FechaInicio);
                    strAux = strAux.Replace("//fin", item.FechaFin);
                    strAux = strAux.Replace("//horas", item.Horas.ToString());

                    strCapacitaciones += strAux;
                }
            }
            else
            {
                strAux = strCapacitacion;
                strAux = strAux.Replace("//capacitacion", "&nbsp;");
                strAux = strAux.Replace("//institucion", "&nbsp;");
                strAux = strAux.Replace("//ciudad", "&nbsp;");
                strAux = strAux.Replace("//inicio", "&nbsp;");
                strAux = strAux.Replace("//fin", "&nbsp;");
                strAux = strAux.Replace("//horas", "&nbsp;");

                strCapacitaciones += strAux;
            }

            html = html.Replace("//conocimiento", strCapacitaciones);

            String strExperiencia = "<tr><td style='font-size: 9px; width: 300px;'>//entidad</td>" +
                                "<td style='font-size: 9px; width: 350px;'>//cargo</td>" +
                                "<td style='font-size: 9px; width: 100px;'>//inicio</td>" +
                                "<td style='font-size: 9px; width: 100px;'>//fin</td>" +
                                "<td style='font-size: 9px; width: 150px;'>//tiempo</td></tr>" +
                                "<tr><td colspan='5' style='font-size: 9px; width: 1000px;'>TRABAJO REALIZADO:<br>//descripcion</td>";
            String strExperiencias = String.Empty;
            if (lstExperiencia.Count > 0)
            {
                foreach (PostulacionExperiencia_Registro item in lstExperiencia)
                {
                    strAux = strExperiencia;
                    strAux = strAux.Replace("//entidad", item.Empresa);
                    strAux = strAux.Replace("//cargo", item.Cargo);
                    strAux = strAux.Replace("//inicio", item.FechaInicio);
                    strAux = strAux.Replace("//fin", item.FechaFin);
                    strAux = strAux.Replace("//tiempo", item.RangoFechaCadena);
                    strAux = strAux.Replace("//descripcion", item.Descripcion);

                    strExperiencias += strAux;
                }
            }
            else
            {
                strAux = strExperiencia;
                strAux = strAux.Replace("//entidad", "&nbsp;");
                strAux = strAux.Replace("//cargo", "&nbsp;");
                strAux = strAux.Replace("//inicio", "&nbsp;");
                strAux = strAux.Replace("//fin", "&nbsp;");
                strAux = strAux.Replace("//tiempo", "&nbsp;");

                strExperiencias += strAux;
            }

            html = html.Replace("//experiencia", strExperiencias);

            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(html, Server.MapPath("~/temp"));

            MemoryStream pdfStream = new MemoryStream();
            doc.Save(pdfStream);
            pdfStream.Position = 0;
            doc.Close();

            return pdfStream;
        }

        [AllowAnonymous]
        public FileResult HojaVidaAnonimo()
        {
            Int32 IdPostulante = (Request.QueryString.Get("IdPostulante") == null ? 0 : Int32.Parse(Request.QueryString["IdPostulante"]));
            Int32 IdPostulacion = (Request.QueryString.Get("IdPostulacion") == null ? 0 : Int32.Parse(Request.QueryString["IdPostulacion"]));
            Int32 IdConvocatoria = (Request.QueryString.Get("IdConvocatoria") == null ? 0 : Int32.Parse(Request.QueryString["IdConvocatoria"]));

            Postulante_Request peticion = new Postulante_Request() { IdPostulante = IdPostulante, IdPostulacion = IdPostulacion, IdConvocatoria = IdConvocatoria };
            PostulacionPostulante_Registro obj = _postulante_Servicio.ObtenerPostulacionPostulante(peticion);
            List<PostulacionDocumento_Registro> lstDocumento = _postulante_Servicio.ListarPostulacionDocumento(peticion).ToList();
            List<PostulacionEstudio_Registro> lstEstudio = _postulante_Servicio.ListarPostulacionEstudio(peticion).ToList();
            List<PostulacionCapacitacion_Registro> lstCapacitacion = _postulante_Servicio.ListarPostulacionCapacitacion(peticion).ToList();
            List<PostulacionExperiencia_Registro> lstExperiencia = _postulante_Servicio.ListarPostulacionExperiencia(peticion).ToList();
            Convocatoria_Registro objConvocatoria = new T_genm_convocatoria_LN().ObtenerParaEditar(new Convocatoria_Request() { IdConvocatoria = IdConvocatoria });

            Stream pdfStream = GenerarHojaVidaPdfAnonimo(obj, lstDocumento, lstEstudio, lstCapacitacion, lstExperiencia, objConvocatoria);

            return File(pdfStream, "application/pdf"); // ("Ficha", "_Layout_Blank", oAsistenciaTecnicaDetalle_Registro);
        }
        [AllowAnonymous]
        private Stream GenerarHojaVidaPdfAnonimo(PostulacionPostulante_Registro oPostulante,
                                            List<PostulacionDocumento_Registro> lstDocumento,
                                            List<PostulacionEstudio_Registro> lstEstudio,
                                            List<PostulacionCapacitacion_Registro> lstCapacitacion,
                                            List<PostulacionExperiencia_Registro> lstExperiencia,
                                            Convocatoria_Registro objConvocatoria)
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

            string html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulante/formato_postulacion1.html"));

            //CultureInfo culture = new CultureInfo("es-PE");
            html = html.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            html = html.Replace("//unidad_organica", (String.IsNullOrEmpty(objConvocatoria.Dependencia) ? objConvocatoria.Organo : objConvocatoria.Dependencia));
            html = html.Replace("//cargo", objConvocatoria.NombreCargo);
            html = html.Replace("//proceso", objConvocatoria.NroConvocatoria);

            html = html.Replace("//paterno", oPostulante.Paterno);
            html = html.Replace("//materno", oPostulante.Materno);
            html = html.Replace("//nombres", oPostulante.Nombre);
            html = html.Replace("//sexo", (oPostulante.Sexo == "1" ? "F" : "M"));
            //html = html.Replace("//edad", oPostulante.Edad.ToString());
            html = html.Replace("//nacionalidad", oPostulante.Nacionalidad);
            html = html.Replace("//nacimiento", oPostulante.FechaNacimiento);
            html = html.Replace("//dia", DateTime.Now.Day.ToString());
            html = html.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
            html = html.Replace("//anio", DateTime.Now.Year.ToString());
            html = html.Replace("//nombre", String.Format("{0} {1} {2}", oPostulante.Nombre, oPostulante.Paterno, oPostulante.Materno));
            html = html.Replace("//dnifirma", (oPostulante.TipoDocumento == 1 ? "DNI: " : "CE: ") + oPostulante.NroDocumento);
            html = html.Replace("//dni", (oPostulante.TipoDocumento == 1 ? oPostulante.NroDocumento : String.Empty));
            //html = html.Replace("//extranjeria", (oFicha.TipoDocumento != 1 ? oFicha.NroDocumento : String.Empty));
            html = html.Replace("//direccion", oPostulante.Domicilio);
            html = html.Replace("//ubigeo", oPostulante.DescripcionUbigeo);
            html = html.Replace("//estado_civil", (oPostulante.IdEstadoCivil == 1 ? "SOLTERO(A)" : (oPostulante.IdEstadoCivil == 2 ? "CASADO(A)" : (oPostulante.IdEstadoCivil == 3 ? "DIVORCIADO(A)" : (oPostulante.IdEstadoCivil == 4 ? "VIUDO(A)" : "")))));
            html = html.Replace("//ruc", oPostulante.RUC);
            html = html.Replace("//email", oPostulante.CorreoElectronico.ToUpper());
            html = html.Replace("//fijo", oPostulante.Telefono);
            html = html.Replace("//celular", oPostulante.Celular);
            html = html.Replace("//ffaa", (oPostulante.FFAA == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//disc", (oPostulante.Discapacidad == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//depo", (oPostulante.Deportista == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));

            String strAux;
            String strEstudio = "<tr><td style='font-size: 9px; width: 150px;'>//nivel_e</td>" +
                                "<td style='font-size: 9px; width: 150px;'>//nivel_a</td>" +
                                "<td style='font-size: 9px; width: 200px;'>//especialidad</td>" +
                                "<td style='font-size: 9px; width: 200px;'>//institucion</td>" +
                                "<td style='font-size: 9px; width: 150px;'>//ciudad</td>" +
                                "<td style='font-size: 9px; width: 150px;'>//obtencion</td></tr>";
            String strEstudios = String.Empty;
            if (lstEstudio.Count > 0)
            {
                foreach (PostulacionEstudio_Registro item in lstEstudio)
                {
                    strAux = strEstudio;
                    strAux = strAux.Replace("//nivel_e", item.NombreGrado);
                    strAux = strAux.Replace("//nivel_a", item.NombreNivel);
                    strAux = strAux.Replace("//especialidad", item.Especialidad);
                    strAux = strAux.Replace("//institucion", item.Institucion);
                    strAux = strAux.Replace("//ciudad", item.Ciudad);
                    strAux = strAux.Replace("//obtencion", item.AnioMes);

                    strEstudios += strAux;
                }
            }
            else
            {
                strAux = strEstudio;
                strAux = strAux.Replace("//nivel_e", "&nbsp;");
                strAux = strAux.Replace("//nivel_a", "&nbsp;");
                strAux = strAux.Replace("//especialidad", "&nbsp;");
                strAux = strAux.Replace("//institucion", "&nbsp;");
                strAux = strAux.Replace("//ciudad", "&nbsp;");
                strAux = strAux.Replace("//obtencion", "&nbsp;");

                strEstudios += strAux;
            }

            html = html.Replace("//formacion", strEstudios);

            String strCapacitacion = "<tr><td style='font-size: 9px; width: 250px;'>//capacitacion</td>" +
                                    "<td style='font-size: 9px; width: 250px;'>//institucion</td>" +
                                    "<td style='font-size: 9px; width: 200px;'>//ciudad</td>" +
                                    "<td style='font-size: 9px; width: 100px;'>//inicio</td>" +
                                    "<td style='font-size: 9px; width: 100px;'>//fin</td>" +
                                    "<td style='font-size: 9px; width: 100px;'>//horas</td></tr>";
            String strCapacitaciones = String.Empty;
            if (lstCapacitacion.Count > 0)
            {
                foreach (PostulacionCapacitacion_Registro item in lstCapacitacion)
                {
                    strAux = strCapacitacion;
                    strAux = strAux.Replace("//capacitacion", item.Especialidad);
                    strAux = strAux.Replace("//institucion", item.Institucion);
                    strAux = strAux.Replace("//ciudad", item.Ciudad);
                    strAux = strAux.Replace("//inicio", item.FechaInicio);
                    strAux = strAux.Replace("//fin", item.FechaFin);
                    strAux = strAux.Replace("//horas", item.Horas.ToString());

                    strCapacitaciones += strAux;
                }
            }
            else
            {
                strAux = strCapacitacion;
                strAux = strAux.Replace("//capacitacion", "&nbsp;");
                strAux = strAux.Replace("//institucion", "&nbsp;");
                strAux = strAux.Replace("//ciudad", "&nbsp;");
                strAux = strAux.Replace("//inicio", "&nbsp;");
                strAux = strAux.Replace("//fin", "&nbsp;");
                strAux = strAux.Replace("//horas", "&nbsp;");

                strCapacitaciones += strAux;
            }

            html = html.Replace("//conocimiento", strCapacitaciones);

            String strExperiencia = "<tr><td style='font-size: 9px; width: 300px;'>//entidad</td>" +
                                "<td style='font-size: 9px; width: 350px;'>//cargo</td>" +
                                "<td style='font-size: 9px; width: 100px;'>//inicio</td>" +
                                "<td style='font-size: 9px; width: 100px;'>//fin</td>" +
                                "<td style='font-size: 9px; width: 150px;'>//tiempo</td></tr>" +
                                "<tr><td colspan='5' style='font-size: 9px; width: 1000px;'>TRABAJO REALIZADO:<br>//descripcion</td>";
            String strExperiencias = String.Empty;
            if (lstExperiencia.Count > 0)
            {
                foreach (PostulacionExperiencia_Registro item in lstExperiencia)
                {
                    strAux = strExperiencia;
                    strAux = strAux.Replace("//entidad", item.Empresa);
                    strAux = strAux.Replace("//cargo", item.Cargo);
                    strAux = strAux.Replace("//inicio", item.FechaInicio);
                    strAux = strAux.Replace("//fin", item.FechaFin);
                    strAux = strAux.Replace("//tiempo", item.RangoFechaCadena);
                    strAux = strAux.Replace("//descripcion", item.Descripcion);

                    strExperiencias += strAux;
                }
            }
            else
            {
                strAux = strExperiencia;
                strAux = strAux.Replace("//entidad", "&nbsp;");
                strAux = strAux.Replace("//cargo", "&nbsp;");
                strAux = strAux.Replace("//inicio", "&nbsp;");
                strAux = strAux.Replace("//fin", "&nbsp;");
                strAux = strAux.Replace("//tiempo", "&nbsp;");

                strExperiencias += strAux;
            }

            html = html.Replace("//experiencia", strExperiencias);

            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(html, Server.MapPath("~/temp"));

            MemoryStream pdfStream = new MemoryStream();
            doc.Save(pdfStream);
            pdfStream.Position = 0;
            doc.Close();

            return pdfStream;
        }

        [HttpGet]
        public JsonResult ListarTipoPostulante()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("1", "POR CONVOCATORIA CAS"));
            lista.Add(new Estado_Response("2", "OTRA MODALIDAD"));

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ObtenerPostulanteFicha(PostulanteInformacion_Registro peticion)
        {
            object respuesta = _postulante_Servicio.ObtenerPostulanteFicha(peticion);

            return Json(respuesta);
        }
        [HttpGet]
        public JsonResult ListarNotificaciones(Postulante_Request peticion)
        {
            List<PostulanteNotificacion_Registro> lista = _postulante_Servicio.ListarNotificaciones(peticion).ToList();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public JsonResult ListarPostulaciones(Postulante_Request peticion)
        {
            List<PostulantePostulacion_Registro> lista = _postulante_Servicio.ListarPostulaciones(peticion).ToList();
            foreach(PostulantePostulacion_Registro obj in lista){
                obj.Contrasena = HttpUtility.UrlEncode(new Crypto().Encriptar(String.Format("{0}|{1}|{2}", obj.IdPostulante, obj.IdPostulacion, obj.IdConvocatoria)));
            }
            
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarPostulanteFamiliares(Postulante_Request peticion)
        {
            object respuesta = _postulante_Servicio.ListarPostulanteFamiliares(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult ListarPostulacionDocumento(Postulante_Request peticion)
        {
            List<PostulacionDocumento_Registro> respuesta = _postulante_Servicio.ListarPostulacionDocumento(peticion).ToList();

            // SE AGREGA EL DOCUMENTO DE DECLARACION JURADA 
            PostulacionDocumento_Registro item = new PostulacionDocumento_Registro();

            item.IdPostulacion = peticion.IdPostulacion;
            item.IdDocumento = 6000;
            item.IdTipoDocumento = 0;
            item.NombreTipoDocumento = "DDJJ DE POSTULACIÓN E INCOMPATIBILIDAD";
            item.Estado = 1;
            item.IdTieneArchivo = 1;
            respuesta.Insert(0, item);

            item = new PostulacionDocumento_Registro();
            item.IdPostulacion = peticion.IdPostulacion;
            item.IdDocumento = 6001;
            item.IdTipoDocumento = 0;
            item.NombreTipoDocumento = "HOJA DE VIDA";
            item.Estado = 1;
            item.IdTieneArchivo = 1;
            respuesta.Insert(0, item);

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [AllowAnonymous]
        public JsonResult ListarPostulacionPracticaDocumento(Postulante_Request peticion)
        {
            List<PostulacionDocumento_Registro> lista = new List<PostulacionDocumento_Registro>();
            List <PostulacionDocumento_Registro> respuesta = _postulante_Servicio.ListarPostulacionPracticaDocumento(peticion).ToList();

            // SE AGREGAN LOS DOCUMENTOS DE POSTULACION
            if (respuesta.Count > 0) {
                if (respuesta[0].IdTieneAnexo == 1) {
                    PostulacionDocumento_Registro item = new PostulacionDocumento_Registro();
                    item.IdPostulacion = peticion.IdPostulacion;
                    item.IdDocumento = 7001;
                    item.IdTipoDocumento = 0;
                    item.NombreTipoDocumento = "ANEXOS DE CONCURSO DE PRÁCTICAS";
                    item.Estado = 1;
                    item.IdTieneArchivo = 1;
                    lista.Insert(0, item);
                }
                if (respuesta[0].IdTieneHojaVida == 1)
                {
                    PostulacionDocumento_Registro item = new PostulacionDocumento_Registro();
                    item.IdPostulacion = peticion.IdPostulacion;
                    item.IdDocumento = 7002;
                    item.IdTipoDocumento = 0;
                    item.NombreTipoDocumento = "HOJA DE VIDA DOCUMENTADA";
                    item.Estado = 1;
                    item.IdTieneArchivo = 1;
                    lista.Insert(0, item);
                }
                if (respuesta[0].IdTieneCarta == 1)
                {
                    PostulacionDocumento_Registro item = new PostulacionDocumento_Registro();
                    item.IdPostulacion = peticion.IdPostulacion;
                    item.IdDocumento = 7003;
                    item.IdTipoDocumento = 0;
                    item.NombreTipoDocumento = "CARTA DE PRESENTACIÓN EMITIDO POR EL CENTRO DE ESTUDIOS";
                    item.Estado = 1;
                    item.IdTieneArchivo = 1;
                    lista.Insert(0, item);
                }

            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [AllowAnonymous]
        public JsonResult ListarPostulacionEstudio(Postulante_Request peticion)
        {
            object respuesta = _postulante_Servicio.ListarPostulacionEstudio(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [AllowAnonymous]
        public JsonResult ListarPostulacionCapacitacion(Postulante_Request peticion)
        {
            object respuesta = _postulante_Servicio.ListarPostulacionCapacitacion(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [AllowAnonymous]
        public JsonResult ListarPostulacionExperiencia(Postulante_Request peticion)
        {
            object respuesta = _postulante_Servicio.ListarPostulacionExperiencia(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ActualizarPostulacionEstudio(PostulacionEstudio_Registro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                object respuesta = _postulante_Servicio.ActualizarPostulacionEstudio(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult ActualizarPostulacionCapacitacion(PostulacionCapacitacion_Registro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                object respuesta = _postulante_Servicio.ActualizarPostulacionCapacitacion(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult ActualizarPostulacionExperiencia(PostulacionExperiencia_Registro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                object respuesta = _postulante_Servicio.ActualizarPostulacionExperiencia(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }


        [HttpPost]
        [AllowAnonymous]
        public JsonResult DescargarArchivoPostulacion(Postulacion_Registro peticion)
        {
            string pathUpload = Server.MapPath("~/temp/P" + peticion.IdPostulacion.ToString());
            if (!Directory.Exists(pathUpload))
                Directory.CreateDirectory(pathUpload);

            //String idPostulante = "20200001";
            //peticion.IdPostulante = Int32.Parse(idPostulante);
            //peticion.IdDetalle = 1;
            //peticion.IdTipo = 3;

            Postulacion_Registro peticionAux = _postulante_Servicio.ObtenerPostulacionDocumento(peticion);

            String strArchivo = String.Empty;
            String strNombreArchivo = String.Empty;
            switch (peticion.IdTipo)
            {
                case 1:
                    strArchivo = String.Format("P_ES{0}_{1}.pdf", peticion.IdPostulacion, DateTime.Now.ToString("yyyyMMddHHmm"));
                    strNombreArchivo = System.IO.Path.Combine(pathUpload, strArchivo);
                    System.IO.File.WriteAllBytes(strNombreArchivo, peticionAux.FileEstudio);
                    break;
                case 2:
                    strArchivo = String.Format("P_CA{0}_{1}.pdf", peticion.IdPostulacion, DateTime.Now.ToString("yyyyMMddHHmm"));
                    strNombreArchivo = System.IO.Path.Combine(pathUpload, strArchivo);
                    System.IO.File.WriteAllBytes(strNombreArchivo, peticionAux.Filecapacitacion);
                    break;
                case 3:
                    strArchivo = String.Format("P_EX{0}_{1}.pdf", peticion.IdPostulacion, DateTime.Now.ToString("yyyyMMddHHmm"));
                    strNombreArchivo = System.IO.Path.Combine(pathUpload, strArchivo);
                    System.IO.File.WriteAllBytes(strNombreArchivo, peticionAux.FileExperiencia);
                    break;
                case 5:
                    strArchivo = String.Format("P_DO{0}_{1}.pdf", peticion.IdPostulacion, DateTime.Now.ToString("yyyyMMddHHmm"));
                    strNombreArchivo = System.IO.Path.Combine(pathUpload, strArchivo);
                    System.IO.File.WriteAllBytes(strNombreArchivo, peticionAux.FileDocumento);
                    break;
                case 6:
                    if (peticion.IdDetalle == 7001) {
                        strArchivo = String.Format("PO_DO{0}_{1}.pdf", peticion.IdPostulacion, DateTime.Now.ToString("yyyyMMddHHmm"));
                        strNombreArchivo = System.IO.Path.Combine(pathUpload, strArchivo);
                        System.IO.File.WriteAllBytes(strNombreArchivo, peticionAux.FileAnexoPracticas);
                    }
                    if (peticion.IdDetalle == 7002)
                    {
                        strArchivo = String.Format("PO_DO{0}_{1}.pdf", peticion.IdPostulacion, DateTime.Now.ToString("yyyyMMddHHmm"));
                        strNombreArchivo = System.IO.Path.Combine(pathUpload, strArchivo);
                        System.IO.File.WriteAllBytes(strNombreArchivo, peticionAux.FileHojaVida);
                    }
                    if (peticion.IdDetalle == 7003)
                    {
                        strArchivo = String.Format("PO_DO{0}_{1}.pdf", peticion.IdPostulacion, DateTime.Now.ToString("yyyyMMddHHmm"));
                        strNombreArchivo = System.IO.Path.Combine(pathUpload, strArchivo);
                        System.IO.File.WriteAllBytes(strNombreArchivo, peticionAux.FileCarta);
                    }
                    break;
            }

            strNombreArchivo = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf("Postulante")) + "temp/P" + peticion.IdPostulacion.ToString() + "/" + strArchivo;
            return Json(new { success = "True", responseText = strNombreArchivo });
        }
        [HttpGet]
        public JsonResult Listar(Postulante_Request peticion)
        {
            object respuesta = _postulante_Servicio.ListarPostulantes(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarGanadores(Postulante_Request peticion)
        {
            object respuesta = _postulante_Servicio.ListarPostulantesGanadores(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Authorize]
        public ActionResult Ingresar()
        {
            PostulanteInformacion_Registro info = new PostulanteInformacion_Registro() { IdPostulante = _postulante_Servicio.GenerarCodigoPostulante(), IdPostulacion = 0, IdConvocatoria = 0 };
            info.Nacionalidad = "PERUANO";
            info.IdDeclaraIncompatibilidad = 1;
            info.IdDeclaraNepotismo = 1;
            info.IdDeclaraNormas = 1;
            info.FechaRegistro = DateTime.Now;
            info.IdUsuarioRegistro = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iCodUsuario); // info.NroDocumento;
            
            if (info != null) {
                ViewBag.IdPostulante = info.IdPostulante;
                ViewBag.IdPostulacion = info.IdPostulacion;
                ViewBag.IdConvocatoria = info.IdConvocatoria;
            }

            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult Editar(String IdPostulante, String IdPostulacion, String IdConvocatoria )
        {
            PostulanteInformacion_Registro info = new PostulanteInformacion_Registro() { IdPostulante = Int32.Parse(IdPostulante), IdPostulacion = Int32.Parse(IdPostulacion), IdConvocatoria = Int32.Parse(IdConvocatoria) };

            if (info != null)
            {
                ViewBag.IdPostulante = info.IdPostulante;
                ViewBag.IdPostulacion = info.IdPostulacion;
                ViewBag.IdConvocatoria = info.IdConvocatoria;
                ViewBag.NombreProceso = info.NombreProceso;
            }

            return View();
        }
        
        [HttpPost]
        public JsonResult RegistrarPostulanteFamiliar(PostulanteFamiliar_Registro registro)
        {
            try
            {
                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioRegistro = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iCodUsuario);

                object respuesta = _postulante_Servicio.RegistrarPostulanteFamiliar(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult RegistrarNotificacion(PostulanteNotificacion_Registro registro)
        {
            try
            {
                registro.IdUsuarioRegistro = VariablesWeb.ConsultaInformacion.iCodUsuario.ToString();

                object respuesta = _postulante_Servicio.RegistrarPostulanteNotificacion(registro);

                Postulante_Request peticion = new Postulante_Request() { IdPostulante = registro.IdPostulante, IdPostulacion = registro.IdPostulacion, IdConvocatoria = registro.IdConvocatoria, NroDocumento = "" };
                Postulante_Registro postu = _postulante_Servicio.ObtenerPostulante(peticion);
                Convocatoria_Registro objConvocatoria = new T_genm_convocatoria_LN().ObtenerParaEditar(new Convocatoria_Request() { IdConvocatoria = registro.IdConvocatoria });

                postu.NroColegiatura = objConvocatoria.NroConvocatoria;
                this.SendEmail(postu, "20");

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult ActualizarPostulanteFamiliar(PostulanteFamiliar_Registro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iCodUsuario);

                object respuesta = _postulante_Servicio.ActualizarPostulanteFamiliar(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult EliminarPostulanteFamiliar(PostulanteFamiliar_Registro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iCodUsuario);

                object respuesta = _postulante_Servicio.EliminarPostulanteFamiliar(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult Registrar(Empleado_Registro registro)
        {
            try
            {
                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioRegistro = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iCodUsuario);

                object respuesta = _empleado_Servicio.RegistrarEmpleado(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Guardar(PostulanteInformacion_Registro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iCodUsuario);

                object respuesta = _postulante_Servicio.ActualizarRegistroPostulante(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            //catch (System.Data.SqlClient.SqlException es)
            //{
            //    if (es.Number == 2627)
            //        return Json(new { success = "False", responseText = "Empleado ya existente en la nómina" });
            //    else
            //        return Json(new { success = "False", responseText = es.Message });
            //}
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult Validar(PostulanteInformacion_Registro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iCodUsuario);

                object respuesta = _postulante_Servicio.ValidarRegistroPostulante(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            //catch (System.Data.SqlClient.SqlException es)
            //{
            //    if (es.Number == 2627)
            //        return Json(new { success = "False", responseText = "Empleado ya existente en la nómina" });
            //    else
            //        return Json(new { success = "False", responseText = es.Message });
            //}
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult GuardarContacto(Empleado_Registro registro)
        {
            try
            {
                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioModificacion = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iCodUsuario);

                object respuesta = _empleado_Servicio.ActualizarEmpleadoContacto(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (System.Data.SqlClient.SqlException es)
            {
                if (es.Number == 2627)
                    return Json(new { success = "False", responseText = "Empleado ya existente en la nómina" });
                else
                    return Json(new { success = "False", responseText = es.Message });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }


        //private void obtenerDesdeWS_RENIEC(ref PostulanteInformacion_Registro info)
        //{
        //    //Persona_Registro mReniec = new Persona_Registro();

        //    try
        //    {
        //        //reniec.pe.gob.pide.ws5.ReniecConsultaDni oClient = new reniec.pe.gob.pide.ws5.ReniecConsultaDni();
        //        //reniec.pe.gob.pide.ws5.resultadoConsulta oResult = new reniec.pe.gob.pide.ws5.resultadoConsulta();
        //        //reniec.pe.gob.pide.ws5.peticionConsulta args = new reniec.pe.gob.pide.ws5.peticionConsulta();

        //        reniec.pe.gob.midis.app1.ReniecPersonaFoto_Servicio oClient = new reniec.pe.gob.midis.app1.ReniecPersonaFoto_Servicio();
        //        reniec.pe.gob.midis.app1.ReniecPersonaFoto_Request args = new reniec.pe.gob.midis.app1.ReniecPersonaFoto_Request();
        //        reniec.pe.gob.midis.app1.ReniecPersonaFoto_Response oResult = new reniec.pe.gob.midis.app1.ReniecPersonaFoto_Response();

        //        args.NumeroDeDocumento = info.NroDocumento;
        //        args.Usuario = ConfigurationManager.AppSettings.Get("UsuarioSW");
        //        args.Clave = ConfigurationManager.AppSettings.Get("ClaveSW");

        //        oResult = oClient.ConsultarPorNumeroDeDocumento(args);
        //        //info.NumeroDeDocumento = vDNI;
        //        try
        //        {
        //            //mReniec.mensaje = oResult.deResultado;
        //            //mReniec.coResultado = oResult.coResultado;

        //            info.Paterno = oResult.PersonaFotoDato.PrimerApellido;
        //            info.Materno = oResult.PersonaFotoDato.SegundoApellido;
        //            info.Nombre = oResult.PersonaFotoDato.PreNombres;
        //            info.Foto = Convert.ToBase64String(oResult.PersonaFotoDato.Foto);
        //            info.DescripcionUbigeo = oResult.PersonaFotoDato.Ubigeo.Replace("/", " / ");
        //            info.Domicilio = oResult.PersonaFotoDato.Direccion;
        //            info.IdEstadoCivil = (oResult.PersonaFotoDato.EstadoCivil.IndexOf("SOLTER") >= 0 ? 1 : (oResult.PersonaFotoDato.EstadoCivil.IndexOf("CASAD") >= 0 ? 2 : (oResult.PersonaFotoDato.EstadoCivil.IndexOf("DIVORCIAD") >= 0 ? 3 : (oResult.PersonaFotoDato.EstadoCivil.IndexOf("VIUD") >= 0 ? 4 : 0))));

        //            //mReniec.restriccion = oResult.datosPersona.restriccion;

        //            obtenerDesdeWS_PIDE(ref info);
        //            //info.FechaNacimiento = maePersonaAux.FechaDeNacimiento.Value.ToString("dd/MM/yyyy");
        //            //info.Sexo = maePersonaAux.Sexo;
        //            //info.Ubigeo = maePersonaAux.Ubigeo.CodigoReniec;
        //        }
        //        catch (Exception)
        //        {

        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}
        //private void obtenerDesdeWS_PIDE(ref PostulanteInformacion_Registro info)
        //{
        //    //Persona_Registro ori = new Persona_Registro();

        //    try
        //    {
        //        reniec.pe.gob.midis.app_pruebas.ReniecPersona_Servicio oClient = new reniec.pe.gob.midis.app_pruebas.ReniecPersona_Servicio();
        //        reniec.pe.gob.midis.app_pruebas.ReniecPersona_Request Request = new reniec.pe.gob.midis.app_pruebas.ReniecPersona_Request();
        //        reniec.pe.gob.midis.app_pruebas.ReniecPersona_Response Response = new reniec.pe.gob.midis.app_pruebas.ReniecPersona_Response();

        //        Request.NumeroDeDocumento = info.NroDocumento;
        //        Request.Usuario = ConfigurationManager.AppSettings.Get("UsuarioSW");
        //        Request.Clave = ConfigurationManager.AppSettings.Get("ClaveSW");

        //        Response = oClient.ConsultarPorNumeroDeDocumento(Request);

        //        try
        //        {
        //            info.Nombre = Response.PersonaDato.Nombres; //vPriNombre 
        //            info.Paterno = Response.PersonaDato.ApellidoPaterno; //vPriApellido
        //            info.Materno = Response.PersonaDato.ApellidoMaterno; //vSegApellido
        //            info.Domicilio = Response.PersonaDato.DireccionDomicilio; //vSegApellido
        //            info.Ubigeo = String.Format("{0}{1}{2}", Response.PersonaDato.UbigeoDptoDomicilio, Response.PersonaDato.UbigeoProvDomicilio, Response.PersonaDato.UbigeoDistDomicilio);
        //            try
        //            {
        //                string xFecha = Response.PersonaDato.FechaNacimiento;
        //                string dia = xFecha.Substring(6, 2);
        //                string mes = xFecha.Substring(4, 2);
        //                string anno = xFecha.Substring(0, 4);

        //                xFecha = "{" + dia + "/" + mes + "/" + anno + " 00:00:00}";
        //                var x = xFecha;
        //                DateTime thisDate = new DateTime(Int16.Parse(anno), Int16.Parse(mes), Int16.Parse(dia));
        //                info.FechaNacimiento = thisDate.ToString("dd/MM/yyyy"); //dtFecNacim
        //                //LLENAR DIRECCION 

        //                //List<Ubigeo_Response> lstUbigeo = new T_genm_ubigeo_LN().Listar(new Ubigeo_Request() { CodigoReniec = info.Ubigeo }).ToList();
        //                //if (lstUbigeo != null)
        //                //{
        //                //    if (lstUbigeo.Count > 0)
        //                //        info.Ubigeo = lstUbigeo[0].CodigoReniec;
        //                //}
        //            }
        //            catch (Exception)
        //            {

        //            }

        //            info.Sexo = (Response.PersonaDato.Sexo == "1" ? "M" : (Response.PersonaDato.Sexo == "2" ? "F" : String.Empty));
        //        }
        //        catch (Exception)
        //        {

        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        //mReniec.mensaje = ex.Message;
        //    }
        //}
        public async Task<Persona_Registro> obtenerDesdeWS_IRMA(string xDNI)
        {
            Persona_Registro mReniec = new Persona_Registro();

            String IRMA_WS_User = ConfigurationManager.AppSettings.Get("IRMA_WS_User");
            String IRMA_WS_Pass = ConfigurationManager.AppSettings.Get("IRMA_WS_Pass");
            String IRMA_WS_AuthUri = ConfigurationManager.AppSettings.Get("IRMA_WS_AuthUri");
            String IRMA_WS_ReniecUri = ConfigurationManager.AppSettings.Get("IRMA_WS_ReniecUri");
            try
            {
                Dictionary<string, string> request = new Dictionary<string, string>
                {
                    { "nuDniConsulta", xDNI }
                };
                Dictionary<string, string> login = new Dictionary<string, string>
                {
                    { "usuario", IRMA_WS_User },
                    { "clave", IRMA_WS_Pass }
                };
                String jsonRequest = JsonConvert.SerializeObject(request);
                StringContent jsonContentRequest = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                String jsonLogin = JsonConvert.SerializeObject(login);
                StringContent jsonContentLogin = new StringContent(jsonLogin, Encoding.UTF8, "application/json");

                HttpClient cliente = new HttpClient();
                HttpResponseMessage response = await cliente.PostAsync(IRMA_WS_AuthUri, jsonContentLogin);

                if (response.IsSuccessStatusCode)
                {
                    var respAuntentica = await response.Content.ReadAsStringAsync();
                    try
                    {
                        RootAutentica resultadoAutentica = JsonConvert.DeserializeObject<RootAutentica>(respAuntentica);
                        if (resultadoAutentica.codigo == "2000")
                        {
                            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", resultadoAutentica.data.token);
                            HttpResponseMessage responseReniec = await cliente.PostAsync(IRMA_WS_ReniecUri, jsonContentRequest);

                            if (responseReniec.IsSuccessStatusCode)
                            {
                                var respReniec = await responseReniec.Content.ReadAsStringAsync();
                                RootReniec resultadoReniec = JsonConvert.DeserializeObject<RootReniec>(respReniec);
                                if (resultadoReniec.codigo == "0000")
                                {
                                    //oResult = oClient.ConsultarPorNumeroDeDocumento(args);
                                    mReniec.mensaje = resultadoReniec.mensaje;      // deResultado;
                                    mReniec.coResultado = resultadoReniec.codigo;  // coResultado;

                                    mReniec.ApellidoPaterno = resultadoReniec.data.apPaterno;
                                    mReniec.ApellidoMaterno = resultadoReniec.data.apMaterno + " " + resultadoReniec.data.apCasada;
                                    mReniec.Nombres = resultadoReniec.data.nombres;
                                    mReniec.EstadoCivil = resultadoReniec.data.estadoCivil;
                                    //mReniec.Foto = resultadoReniec.data.foto;
                                    mReniec.sFoto = resultadoReniec.data.foto;
                                    mReniec.DescripcionUbigeo = resultadoReniec.data.ubigeo.Replace("/", " / ");
                                    mReniec.DireccionDomicilio = resultadoReniec.data.direccionDomicilio;
                                    mReniec.EstadoCivil = (resultadoReniec.data.estadoCivil.IndexOf("SOLTER") >= 0 ? "1" : (resultadoReniec.data.estadoCivil.IndexOf("CASAD") >= 0 ? "2" : (resultadoReniec.data.estadoCivil.IndexOf("DIVORCIAD") >= 0 ? "3" : (resultadoReniec.data.estadoCivil.IndexOf("VIUD") >= 0 ? "4" : string.Empty))));
                                    //mReniec.restriccion = oResult.datosPersona.restriccion;
                                    mReniec.FechaDeNacimiento = new DateTime(Convert.ToInt32(resultadoReniec.data.fechaNacimiento.Substring(0, 4)),
                                                                             Convert.ToInt32(resultadoReniec.data.fechaNacimiento.Substring(4, 2)),
                                                                             Convert.ToInt32(resultadoReniec.data.fechaNacimiento.Substring(6, 2)));
                                    mReniec.Sexo = (resultadoReniec.data.sexo == "1" ? "2" : (resultadoReniec.data.sexo == "2" ? "1" : String.Empty));

                                    mReniec.Ubigeo = new Ubigeo_Registro();
                                    mReniec.Ubigeo.CodigoReniec = String.Format("{0}{1}{2}", resultadoReniec.data.ubigeoDepartamentoDomicilio, resultadoReniec.data.ubigeoProvinciaDomicilio, resultadoReniec.data.ubigeoDistritoDomicilio);

                                    //Persona_Registro maePersonaAux = obtenerDesdeWS_PIDE(xDNI);
                                    //if (maePersonaAux.FechaDeNacimiento != null) mReniec.FechaDeNacimiento = maePersonaAux.FechaDeNacimiento;
                                    //if (maePersonaAux.Sexo != null) mReniec.Sexo = maePersonaAux.Sexo;
                                    //if (maePersonaAux.Ubigeo != null)
                                    //{
                                    //    mReniec.Ubigeo = new Ubigeo_Registro();
                                    //    mReniec.Ubigeo.CodigoReniec = maePersonaAux.Ubigeo.CodigoReniec;
                                    //}
                                }
                                else
                                {
                                    //en caso de error en la consulta 
                                }
                            }
                        }
                        else
                        {
                            // return 
                        }

                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                mReniec.mensaje = ex.Message;
            }

            return mReniec;
        }


        [HttpPost]
        public JsonResult ExportarBoleta(String anio, String mes)
        {
            var directory = "Formatos_Midis_" + DateTime.Now.ToString("yyyyMMddHHmm");
            //var fileName = "Boletas_Midis_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".zip";
            string fullPath = Path.Combine(Server.MapPath("~/temp"), directory);
            DirectoryInfo dir = null;

            if (!Directory.Exists(fullPath)) dir = Directory.CreateDirectory(fullPath);

            Empleado_Registro registro = new Empleado_Registro();
            registro.Anio = anio;
            registro.Mes = mes;

            try
            {
                // RECORREMOS LA LISTA DE EMPLEADOS
                List<EmpleadoSisper_Registro> lista = _empleado_Servicio.ListarEmpleadosSisper(registro).ToList();
                List<EmpleadoConceptoSisper_Registro> lstConcepto = (List<EmpleadoConceptoSisper_Registro>)_empleado_Servicio.ListarEmpleadoConceptoSisper(registro);
                List<EmpleadoConceptoSisper_Registro> lstConceptoIngreso = null;
                List<EmpleadoConceptoSisper_Registro> lstConceptoDescuento = null;
                List<EmpleadoConceptoSisper_Registro> lstConceptoAporte = null;

                foreach (EmpleadoSisper_Registro obj in lista)
                {
                    using (ReportDocument report = new ReportDocument())
                    {
                        lstConceptoIngreso = lstConcepto.Where(x => ((x.TipoConcepto == "0") || (x.TipoConcepto == "1")) &&
                                                                    x.Trabajador == obj.Trabajador &&
                                                                    x.IdPlanilla == obj.IdPlanilla &&
                                                                    x.TipoPlanilla == obj.TipoPlanilla).ToList();
                        lstConceptoDescuento = lstConcepto.Where(x => ((x.TipoConcepto != "0") && (x.TipoConcepto != "1") && (x.TipoConcepto != "9")) &&
                                                                      x.Trabajador == obj.Trabajador &&
                                                                      x.IdPlanilla == obj.IdPlanilla &&
                                                                      x.TipoPlanilla == obj.TipoPlanilla).ToList();
                        lstConceptoAporte = lstConcepto.Where(x => x.TipoConcepto == "9" &&
                                                                   x.Trabajador == obj.Trabajador &&
                                                                   x.IdPlanilla == obj.IdPlanilla &&
                                                                   x.TipoPlanilla == obj.TipoPlanilla).ToList();

                        try
                        {
                            report.Load(System.Web.HttpContext.Current.Server.MapPath("~/Reportes/Formato1.rpt"));
                            report.FileName = System.Web.HttpContext.Current.Server.MapPath("~/Reportes/Formato1.rpt");
                            report.OpenSubreport("lstIngreso").SetDataSource(lstConceptoIngreso);
                            report.OpenSubreport("lstDescuento").SetDataSource(lstConceptoDescuento);
                            report.OpenSubreport("lstAporte").SetDataSource(lstConceptoAporte);
                            report.SetDataSource(lista.Where(x => x.Trabajador == obj.Trabajador &&
                                                                  x.IdPlanilla == obj.IdPlanilla &&
                                                                  x.TipoPlanilla == obj.TipoPlanilla).ToList());

                            String fileName = Path.Combine(fullPath, String.Format("{0}-{1}-{2}-{3}{4}-{5}", obj.NroDocumento, anio, mes.PadLeft(2, '0'), obj.IdPlanilla, obj.TipoPlanilla, "01.pdf"));
                            FileStream file = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                            report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat).CopyTo(file);
                            file.Close();
                        }
                        catch (Exception)
                        {
                            //report.Dispose();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { fileName = "", responseText = ex.Message });
            }

            return Json(new { fileName = directory, responseText = "" });
        }

        public FileResult FichaPersonal()
        {
            Int32 IdPostulante = (Request.QueryString.Get("IdPostulante") == null ? 0 : Int32.Parse(Request.QueryString["IdPostulante"]));
            Int32 IdPostulacion = (Request.QueryString.Get("IdPostulacion") == null ? 0 : Int32.Parse(Request.QueryString["IdPostulacion"]));
            Int32 IdConvocatoria = (Request.QueryString.Get("IdConvocatoria") == null ? 0 : Int32.Parse(Request.QueryString["IdConvocatoria"]));

            PostulanteInformacion_Registro peticion = new PostulanteInformacion_Registro() { IdPostulante = IdPostulante, IdPostulacion = IdPostulacion, IdConvocatoria = IdConvocatoria };
            peticion = _postulante_Servicio.ObtenerPostulanteFicha(peticion);

            Stream pdfStream = GenerarFichaPersonalPdf(peticion);

            //oAsistenciaTecnicaDetalle_Registro.IdUsuarioRegistro = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iUsu);
            //oAsistenciaTecnicaDetalle_Registro.FechaRegistro = DateTime.Now;
            //oAsistenciaTecnicaDetalle_Registro.ActaGenerada = true;
            //oAsistenciaTecnicaDetalle_Registro.Estado = 1;
            //new T_genm_negociacion_LN().Actualizar(oAsistenciaTecnicaDetalle_Registro);

            /*AsistenciaTecnica_Participante_Request peticionParticipantes = new AsistenciaTecnica_Participante_Request()
            {
                Estado = 1,
                IdAsistenciaTecnica = oAsistenciaTecnicaDetalle_Registro.IdAsistenciaTecnica
            };

            IEnumerable<AsistenciaTecnica_Participante_Response> participantes = _asistenciatecnica_detalle_Servicio.ListarParticipante(peticionParticipantes);

            foreach (AsistenciaTecnica_Participante_Response item in participantes)
            {
                byte[] bytesActa = ReadFully(pdfStream);
                UtilHelper.SendMail("Haz sido participante de una Asistencia Técnica, por favor revisar documento adjunto.", ConfigurationManager.AppSettings["AsistenciaTecnica.From.Address"], item.Correo, null, ConfigurationManager.AppSettings["AsistenciaTecnica.Bcc.Address"], "Asistencia Ténica - Acta N° " + oAsistenciaTecnicaDetalle_Registro.Codigo, bytesActa, "Acta.pdf");
            }
            
            pdfStream.Position = 0;*/
            return File(pdfStream, "application/pdf"); // ("Ficha", "_Layout_Blank", oAsistenciaTecnicaDetalle_Registro);
        }

        private Stream GenerarFichaPersonalPdf(PostulanteInformacion_Registro oFicha)
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

            string html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulante/ficha_personal1.html"));
            //string htmlParticipantes = System.IO.File.ReadAllText(ConfigurationManager.AppSettings["Negociacion.Ficha.Plantilla.Participantes.Ruta"]);
            //string htmlIndicadores = System.IO.File.ReadAllText(ConfigurationManager.AppSettings["Negociacion.Ficha.Plantilla.Indicadores.Ruta"]);
            
            //CultureInfo culture = new CultureInfo("es-PE");
            html = html.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            //html = html.Replace("//planilla", oFicha.NroPlanilla);
            //html = html.Replace("//contrato", String.Empty);
            //html = html.Replace("//ingreso", String.Empty);
            html = html.Replace("//unidad_organica", oFicha.NombreUnidadOrganica);
            html = html.Replace("//cargo", oFicha.NombreCargo);
            html = html.Replace("//remuneracion", oFicha.Remuneracion.ToString("C"));
            html = html.Replace("//meta", oFicha.Meta);

            html = html.Replace("//paterno", oFicha.Paterno);
            html = html.Replace("//materno", oFicha.Materno);
            html = html.Replace("//nombres", oFicha.Nombre);
            html = html.Replace("//sexo", (oFicha.Sexo == "1" ? "F" : "M"));
            html = html.Replace("//edad", oFicha.Edad.ToString());
            html = html.Replace("//nacionalidad", oFicha.Nacionalidad);
            html = html.Replace("//nacimiento", oFicha.FechaNacimiento);
            html = html.Replace("//lugar_nacimiento", oFicha.LugarNacimiento.ToUpper());
            html = html.Replace("//dni", (oFicha.TipoDocumento == 1 ? oFicha.NroDocumento : String.Empty));
            html = html.Replace("//extranjeria", (oFicha.TipoDocumento != 1 ? oFicha.NroDocumento : String.Empty));
            html = html.Replace("//direccion", oFicha.Domicilio);
            html = html.Replace("//ubigeo", oFicha.DescripcionUbigeo);
            html = html.Replace("//estado_civil", (oFicha.IdEstadoCivil == 1 ? "SOLTERO(A)" : (oFicha.IdEstadoCivil == 2 ? "CASADO(A)" : (oFicha.IdEstadoCivil == 3 ? "DIVORCIADO(A)" : (oFicha.IdEstadoCivil == 4 ? "VIUDO(A)" : "")))));
            html = html.Replace("//tipo_vivienda", (oFicha.IdTipoVivienda == 1 ? "PROPIA" : (oFicha.IdTipoVivienda == 2 ? "ALQUILADA" : (oFicha.IdTipoVivienda == 3 ? "FAMILIARES" : (oFicha.IdTipoVivienda == 4 ? "PENSIÓN" : (oFicha.IdTipoVivienda == 5 ? "TEMPORAL" : ""))))));
            html = html.Replace("//brevete", (oFicha.IdTipoBrevete == 0 ? "" : (oFicha.IdTipoBrevete == 1 ? "AUTOMOVIL" : (oFicha.IdTipoBrevete == 2 ? "MOTOCICLETA" : ""))));
            html = html.Replace("//licencia", (oFicha.IdTipoBrevete == 0 ? "" : oFicha.NroLicencia));
            html = html.Replace("//ruc", oFicha.RUC);
            html = html.Replace("//email", oFicha.CorreoElectronico.ToUpper());
            html = html.Replace("//fijo", oFicha.Telefono);
            html = html.Replace("//celular", oFicha.Celular);
            html = html.Replace("//emergencia1", oFicha.TelefonoEmergencia1);
            html = html.Replace("//emergencia2", oFicha.TelefonoEmergencia2);
            html = html.Replace("//contacto1", oFicha.ContactoEmergencia1.ToUpper());
            html = html.Replace("//contacto2", oFicha.ContactoEmergencia2.ToUpper());
            html = html.Replace("//afp", (oFicha.IdEstaAfiliadoPensiones == 1 ? (oFicha.IdAFPAfiliada == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;") : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//onp", (oFicha.IdEstaAfiliadoPensiones == 1 ? (oFicha.IdAFPAfiliada == 2 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;") : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//sinreg", (oFicha.IdEstaAfiliadoPensiones == 1 ? (oFicha.IdAFPAfiliada == 3 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;") : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//nombre_afp", (oFicha.IdEstaAfiliadoPensiones == 1 ? (oFicha.IdAFPAfiliada == 1 ? oFicha.NombreAFPAfiliada.ToUpper() : "") : ""));
            html = html.Replace("//nombre_banco", (oFicha.IdEstaAfiliadoBanco == 1 ? oFicha.NombreBanco.ToString() : ""));
            html = html.Replace("//cuenta_banco", (oFicha.IdEstaAfiliadoBanco == 1 ? oFicha.CuentaBancoAfiliado : ""));
            html = html.Replace("//cci_banco", (oFicha.IdEstaAfiliadoBanco == 1 ? oFicha.CuentaBancoCCIAfiliado : ""));
            html = html.Replace("//_afp", (oFicha.IdEstaAfiliadoPensiones != 1 ? (oFicha.IdTipoPensionDeseaAfiliar == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;") : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//_onp", (oFicha.IdEstaAfiliadoPensiones != 1 ? (oFicha.IdTipoPensionDeseaAfiliar == 2 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;") : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//_bbva", (oFicha.IdEstaAfiliadoBanco != 1 ? (oFicha.IdBancoDeseaAfiliar == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;") : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//_bcp", (oFicha.IdEstaAfiliadoBanco != 1 ? (oFicha.IdBancoDeseaAfiliar == 2 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;") : "&nbsp;&nbsp;&nbsp;"));

            html = html.Replace("//sa_s", (oFicha.IdSituacionAcademicaS == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//sa_t", (oFicha.IdSituacionAcademicaT == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//sa_u", (oFicha.IdSituacionAcademicaU == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//sa_o", (oFicha.IdSituacionAcademicaO == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//sa_ct", oFicha.SituacionAcademicaT);
            html = html.Replace("//sa_cu", oFicha.SituacionAcademicaU);
            html = html.Replace("//sa_co", oFicha.SituacionAcademicaO);
            html = html.Replace("//ce_pu", (oFicha.IdCentroEstudiosPU == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//ce_pr", (oFicha.IdCentroEstudiosPR == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//ce_ex", (oFicha.IdCentroEstudiosEX == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//ce_nopu", oFicha.CentroEstudiosPU);
            html = html.Replace("//ce_nopr", oFicha.CentroEstudiosPR);
            html = html.Replace("//ce_noex", oFicha.CentroEstudiosEX);
            html = html.Replace("//ga_es", (oFicha.IdGradoAcademicoES == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//ga_eg", (oFicha.IdGradoAcademicoEG == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//ga_ba", (oFicha.IdGradoAcademicoBA == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//ga_ti", (oFicha.IdGradoAcademicoTI == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//pg_m", (oFicha.IdPostgradoM == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//pg_d", (oFicha.IdPostgradoD == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//pg_o", (oFicha.IdPostgradoO == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//pg_no", oFicha.PostgradoO);
            html = html.Replace("//pg_ce", oFicha.PostgradoCE);
            html = html.Replace("//pg_gr", oFicha.PostgradoGrado);

            //html2 = html2.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            html = html.Replace("//aler", (oFicha.IdPresentaAlergias == 1 ? "SI" : "NO"));
            html = html.Replace("//beta", (oFicha.IdPresentaAlergias1 == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//analge", (oFicha.IdPresentaAlergias2 == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//otral", oFicha.PresentaAlergiasOtro);
            html = html.Replace("//enfe", (oFicha.IdPresentaEnfermedades == 1 ? "SI" : "NO"));
            html = html.Replace("//diab", (oFicha.IdPresentaEnfermedadesD == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//hipe", (oFicha.IdPresentaEnfermedadesH == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//asma", (oFicha.IdPresentaEnfermedadesA == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//epil", (oFicha.IdPresentaEnfermedadesE == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//otrae", oFicha.PresentaEnfermedadesOtro);
            html = html.Replace("//medi", (oFicha.IdConsumeMedicamentos == 1 ? "SI" : "NO"));
            html = html.Replace("//otrom", oFicha.ConsumeMedicamentosOtro.ToUpper());
            html = html.Replace("//grs", (oFicha.IdGrupoSanguineo == 1 ? "O-" : (oFicha.IdGrupoSanguineo == 2 ? "O+" : (oFicha.IdGrupoSanguineo == 3 ? "A-" : (oFicha.IdGrupoSanguineo == 4 ? "A+" : (oFicha.IdGrupoSanguineo == 5 ? "B-" : (oFicha.IdGrupoSanguineo == 6 ? "B+" : (oFicha.IdGrupoSanguineo == 7 ? "AB-" : (oFicha.IdGrupoSanguineo == 8 ? "AB+" : "")))))))));
            html = html.Replace("//adic", oFicha.InformacionAdicionalSalud.ToUpper());

            html = html.Replace("//disc", (oFicha.IdPresentaDiscapacidad == 1 ? "SI" : "NO"));
            html = html.Replace("//diau", (oFicha.IdPresentaDiscapacidadA == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//dico", (oFicha.IdPresentaDiscapacidadC == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//difi", (oFicha.IdPresentaDiscapacidadF == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//visu", (oFicha.IdPresentaDiscapacidadV == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//habl", (oFicha.IdPresentaDiscapacidadH == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html = html.Replace("//cond", oFicha.PresentaDiscapacidadC.ToUpper());
            html = html.Replace("//fisi", oFicha.PresentaDiscapacidadF.ToUpper());
            html = html.Replace("//cert", (oFicha.IdCertificadoDiscapacidad == 1 ? "SI" : "NO"));

            
            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(html, Server.MapPath("~/temp"));
            string html2 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulante/ficha_personal2.html"));
            // LISTAR FAMILIARES 
            Postulante_Request peticion = new Postulante_Request()
            {
                IdPostulante = oFicha.IdPostulante,
                IdPostulacion = oFicha.IdPostulacion,
                IdConvocatoria = oFicha.IdConvocatoria
            };

            String strAux;
            String strFamiliar = "<tr><td style='font-size: 9px; width: 169.727px;'>//paren</td>" + 
                                "<td style='font-size: 9px; width: 414.273px;'>//nombre</td>" + 
                                "<td style='font-size: 9px; width: 113px;'>//dni</td>" + 
                                "<td style='font-size: 9px; width: 137px;'>//fecha</td>" + 
                                "<td style='font-size: 9px; width: 61px;'>//edad</td>" + 
                                "<td style='font-size: 9px; width: 404px;'>//ocupa</td>" + 
                                "<td style='font-size: 9px; width: 73px;'>//sexo</td></tr>";
            String strFamiliares = String.Empty;
            List<PostulanteFamiliar_Registro> familiares = _postulante_Servicio.ListarPostulanteFamiliares(peticion).ToList();
            if (familiares.Count > 0)
            {
                foreach (PostulanteFamiliar_Registro item in familiares)
                {
                    strAux = strFamiliar;
                    strAux = strAux.Replace("//paren", item.Parentesco.Nombre);
                    strAux = strAux.Replace("//nombre", item.Nombre);
                    strAux = strAux.Replace("//dni", item.NroDocumento);
                    strAux = strAux.Replace("//fecha", item.FechaNacimiento);
                    strAux = strAux.Replace("//edad", item.Edad.ToString());
                    strAux = strAux.Replace("//ocupa", item.Ocupacion);
                    strAux = strAux.Replace("//sexo", item.Sexo.Nombre);
                    
                    strFamiliares += strAux;
                }
            }
            else {
                strAux = strFamiliar;
                strAux = strAux.Replace("//paren", "&nbsp;");
                strAux = strAux.Replace("//nombre", "&nbsp;");
                strAux = strAux.Replace("//dni", "&nbsp;");
                strAux = strAux.Replace("//fecha", "&nbsp;");
                strAux = strAux.Replace("//edad", "&nbsp;");
                strAux = strAux.Replace("//ocupa", "&nbsp;");
                strAux = strAux.Replace("//sexo", "&nbsp;");

                strFamiliares += strAux;
            }
            
            html2 = html2.Replace("//familiares", strFamiliares);

            html2 = html2.Replace("//dia", DateTime.Now.Day.ToString());
            html2 = html2.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
            html2 = html2.Replace("//anio", DateTime.Now.Year.ToString());
            html2 = html2.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
            html2 = html2.Replace("//dni", (oFicha.TipoDocumento == 1 ? "DNI: " : "CE: ") + oFicha.NroDocumento);
            
            SelectPdf.PdfDocument doc2 = converter.ConvertHtmlString(html2, Server.MapPath("~/temp"));
            foreach (PdfPage page in doc2.Pages)
                doc.AddPage(page);

            string html3 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulante/declaracion_incompatibilidad.html"));
            html3 = html3.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            html3 = html3.Replace("//dia", DateTime.Now.Day.ToString());
            html3 = html3.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
            html3 = html3.Replace("//anio", DateTime.Now.Year.ToString());
            html3 = html3.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
            html3 = html3.Replace("//dni", (oFicha.TipoDocumento == 1 ? "D.N.I. N&deg; " : "C.E. N&deg; ") + oFicha.NroDocumento);
            
            SelectPdf.PdfDocument doc3 = converter.ConvertHtmlString(html3, Server.MapPath("~/temp"));
            foreach (PdfPage page in doc3.Pages)
                doc.AddPage(page);

            if (oFicha.IdDeclaraNepotismo == 0)
            {
                string html4 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulante/declaracion_nepotismo_si.html"));
                html4 = html4.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
                html4 = html4.Replace("//dia", DateTime.Now.Day.ToString());
                html4 = html4.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
                html4 = html4.Replace("//anio", DateTime.Now.Year.ToString());
                html4 = html4.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
                html4 = html4.Replace("//dni", (oFicha.TipoDocumento == 1 ? "D.N.I. N&deg; " : "C.E. N&deg; ") + oFicha.NroDocumento);
                html4 = html4.Replace("//neporelacion1", oFicha.NepotismoRel1);
                html4 = html4.Replace("//nepoapellido1", oFicha.NepotismoApe1);
                html4 = html4.Replace("//neponombre1", oFicha.NepotismoNom1);
                html4 = html4.Replace("//nepoarea1", oFicha.NepotismoAre1);
                html4 = html4.Replace("//neporelacion2", oFicha.NepotismoRel2);
                html4 = html4.Replace("//nepoapellido2", oFicha.NepotismoApe2);
                html4 = html4.Replace("//neponombre2", oFicha.NepotismoNom2);
                html4 = html4.Replace("//nepoarea2", oFicha.NepotismoAre2);
                html4 = html4.Replace("//neporelacion3", oFicha.NepotismoRel3);
                html4 = html4.Replace("//nepoapellido3", oFicha.NepotismoApe3);
                html4 = html4.Replace("//neponombre3", oFicha.NepotismoNom3);
                html4 = html4.Replace("//nepoarea3", oFicha.NepotismoAre3);
                
                SelectPdf.PdfDocument doc4 = converter.ConvertHtmlString(html4, Server.MapPath("~/temp"));
                foreach (PdfPage page in doc4.Pages)
                    doc.AddPage(page);

            }
            else {
                string html4 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulante/declaracion_nepotismo_no.html"));
                html4 = html4.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
                html4 = html4.Replace("//dia", DateTime.Now.Day.ToString());
                html4 = html4.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
                html4 = html4.Replace("//anio", DateTime.Now.Year.ToString());
                html4 = html4.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
                html4 = html4.Replace("//dni", (oFicha.TipoDocumento == 1 ? "D.N.I. N&deg; " : "C.E. N&deg; ") + oFicha.NroDocumento);

                SelectPdf.PdfDocument doc4 = converter.ConvertHtmlString(html4, Server.MapPath("~/temp"));
                foreach (PdfPage page in doc4.Pages)
                    doc.AddPage(page);
            
            }

            string html5 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulante/declaracion_conocimiento.html"));
            html5 = html5.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            html5 = html5.Replace("//dia", DateTime.Now.Day.ToString());
            html5 = html5.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
            html5 = html5.Replace("//anio", DateTime.Now.Year.ToString());
            html5 = html5.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
            html5 = html5.Replace("//dni", (oFicha.TipoDocumento == 1 ? "D.N.I. N&deg; " : "C.E. N&deg; ") + oFicha.NroDocumento);

            SelectPdf.PdfDocument doc5 = converter.ConvertHtmlString(html5, Server.MapPath("~/temp"));
            foreach (PdfPage page in doc5.Pages)
                doc.AddPage(page);

            if (oFicha.IdEstaAfiliadoBanco == 0)
            {
                string html6 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulante/declaracion_sueldo_si.html"));
                html6 = html6.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
                html6 = html6.Replace("//dia", DateTime.Now.Day.ToString());
                html6 = html6.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
                html6 = html6.Replace("//anio", DateTime.Now.Year.ToString());
                html6 = html6.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
                html6 = html6.Replace("//dni", (oFicha.TipoDocumento == 1 ? "D.N.I. N&deg; " : "C.E. N&deg; ") + oFicha.NroDocumento);
                html6 = html6.Replace("//banco", (oFicha.IdBancoDeseaAfiliar == 1 ? "BBVA" : (oFicha.IdBancoDeseaAfiliar == 2 ? "BCP" : String.Empty)));

                SelectPdf.PdfDocument doc6 = converter.ConvertHtmlString(html6, Server.MapPath("~/temp"));
                foreach (PdfPage page in doc6.Pages)
                    doc.AddPage(page);
            }
            else
            {
                string html6 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulante/declaracion_sueldo_no.html"));
                html6 = html6.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
                html6 = html6.Replace("//dia", DateTime.Now.Day.ToString());
                html6 = html6.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
                html6 = html6.Replace("//anio", DateTime.Now.Year.ToString());
                html6 = html6.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
                html6 = html6.Replace("//dni", (oFicha.TipoDocumento == 1 ? "D.N.I. N&deg; " : "C.E. N&deg; ") + oFicha.NroDocumento);
                html6 = html6.Replace("//banco", oFicha.NombreBanco);
                html6 = html6.Replace("//cuenta", oFicha.CuentaBancoAfiliado);
                html6 = html6.Replace("//cci", oFicha.CuentaBancoCCIAfiliado);

                SelectPdf.PdfDocument doc6 = converter.ConvertHtmlString(html6, Server.MapPath("~/temp"));
                foreach (PdfPage page in doc6.Pages)
                    doc.AddPage(page);
            }

            if (oFicha.IdEstaAfiliadoPensiones == 0)
            {
                string html7 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulante/declaracion_pensiones_no_afiliado.html"));
                html7 = html7.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
                html7 = html7.Replace("//dia", DateTime.Now.Day.ToString());
                html7 = html7.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
                html7 = html7.Replace("//anio", DateTime.Now.Year.ToString());
                html7 = html7.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
                html7 = html7.Replace("//dni", (oFicha.TipoDocumento == 1 ? "D.N.I. N&deg; " : "C.E. N&deg; ") + oFicha.NroDocumento);
                html7 = html7.Replace("//tipo", (oFicha.IdTipoPensionDeseaAfiliar == 1 ? "AFP" : (oFicha.IdTipoPensionDeseaAfiliar == 2 ? "ONP" : String.Empty)));
                html7 = html7.Replace("//descripcion", (oFicha.IdTipoPensionDeseaAfiliar == 1 ? "Sistema Privado de Pensiones" : (oFicha.IdTipoPensionDeseaAfiliar == 2 ? "Sistema Nacional de Pensiones" : String.Empty)));
                
                SelectPdf.PdfDocument doc7 = converter.ConvertHtmlString(html7, Server.MapPath("~/temp"));
                foreach (PdfPage page in doc7.Pages)
                    doc.AddPage(page);
            }
            else {
                if (oFicha.IdAFPAfiliada == 1) { // AFILIADO A UNA AFP
                    string html7 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulante/declaracion_pensiones_afp.html"));
                    html7 = html7.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
                    html7 = html7.Replace("//dia", DateTime.Now.Day.ToString());
                    html7 = html7.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
                    html7 = html7.Replace("//anio", DateTime.Now.Year.ToString());
                    html7 = html7.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
                    html7 = html7.Replace("//dni", (oFicha.TipoDocumento == 1 ? "D.N.I. N&deg; " : "C.E. N&deg; ") + oFicha.NroDocumento);
                    html7 = html7.Replace("//afp", oFicha.NombreAFPAfiliada.ToUpper());
                    
                    SelectPdf.PdfDocument doc7 = converter.ConvertHtmlString(html7, Server.MapPath("~/temp"));
                    foreach (PdfPage page in doc7.Pages)
                        doc.AddPage(page);
                }
                if (oFicha.IdAFPAfiliada == 2)// AFILIADO A LA ONP
                {
                    string html7 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulante/declaracion_pensiones_onp.html"));
                    html7 = html7.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
                    html7 = html7.Replace("//dia", DateTime.Now.Day.ToString());
                    html7 = html7.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
                    html7 = html7.Replace("//anio", DateTime.Now.Year.ToString());
                    html7 = html7.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
                    html7 = html7.Replace("//dni", (oFicha.TipoDocumento == 1 ? "D.N.I. N&deg; " : "C.E. N&deg; ") + oFicha.NroDocumento);
                    
                    SelectPdf.PdfDocument doc7 = converter.ConvertHtmlString(html7, Server.MapPath("~/temp"));
                    foreach (PdfPage page in doc7.Pages)
                        doc.AddPage(page);
                }
            }

            string html8 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulante/declaracion_directivas.html"));
            html8 = html8.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            html8 = html8.Replace("//dia", DateTime.Now.Day.ToString());
            html8 = html8.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
            html8 = html8.Replace("//anio", DateTime.Now.Year.ToString());
            html8 = html8.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
            html8 = html8.Replace("//dni", (oFicha.TipoDocumento == 1 ? "D.N.I. N&deg; " : "C.E. N&deg; ") + oFicha.NroDocumento);

            SelectPdf.PdfDocument doc8 = converter.ConvertHtmlString(html8, Server.MapPath("~/temp"));
            foreach (PdfPage page in doc8.Pages)
                doc.AddPage(page);

            string html9 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulante/declaracion_autenticidad.html"));
            html9 = html9.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            html9 = html9.Replace("//dia", DateTime.Now.Day.ToString());
            html9 = html9.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
            html9 = html9.Replace("//anio", DateTime.Now.Year.ToString());
            html9 = html9.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
            html9 = html9.Replace("//dni", (oFicha.TipoDocumento == 1 ? "D.N.I. N&deg; " : "C.E. N&deg; ") + oFicha.NroDocumento);
            html9 = html9.Replace("//proceso_cas", oFicha.NombreProceso);

            SelectPdf.PdfDocument doc9 = converter.ConvertHtmlString(html9, Server.MapPath("~/temp"));
            foreach (PdfPage page in doc9.Pages)
                doc.AddPage(page);

            MemoryStream pdfStream = new MemoryStream();
            doc.Save(pdfStream);
            pdfStream.Position = 0;
            doc.Close();

            return pdfStream;
        }
        private Boolean SendEmail(Postulante_Registro postulante, String tipo)
        {
            Boolean exitoEnvio = false;
            String html = String.Empty;
            if (tipo == "20")
            { //AL RECIBIR LA DOCUMENTACION DEL POSTULANTE
                html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Correo/EnvioObservacionPos.txt"));
                html = html.Replace("_POSTULANTE_", postulante.Nombre);
                html = html.Replace("_URLEXTERNO_", ConfigurationManager.AppSettings["URL_EXTERNO"].ToString()); //HttpUtility.UrlEncode(

                MailMessage msg = new MailMessage();
                msg.To.Add(new MailAddress(postulante.CorreoElectronico));
                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CCcorreoINC"].ToString()))
                    msg.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["CCcorreoINC"].ToString()));

                msg.From = new MailAddress(ConfigurationManager.AppSettings["correo"].ToString());
                msg.Subject = String.Format("{0} {1}-MIDIS", "Proceso de Convocatorias CAS", postulante.NroColegiatura);
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

    //namespace IRMA_Autentica
    //{
    //    public class Data
    //    {
    //        public string token { get; set; }
    //    }

    //    public class RootAutentica
    //    {
    //        public string codigo { get; set; }
    //        public string mensaje { get; set; }
    //        public Data data { get; set; }
    //    }
    //}
    //namespace IRMA_Reniec
    //{
    //    public class Data
    //    {
    //        public string numDoc { get; set; }
    //        public string apPaterno { get; set; }
    //        public string apMaterno { get; set; }
    //        public string apCasada { get; set; }
    //        public string nombres { get; set; }
    //        public string direccion { get; set; }
    //        public string estadoCivil { get; set; }
    //        public string restriccion { get; set; }
    //        public string ubigeo { get; set; }
    //        public string foto { get; set; }
    //        public string ubigeoDepartamentoDomicilio { get; set; }
    //        public string ubigeoProvinciaDomicilio { get; set; }
    //        public string ubigeoDistritoDomicilio { get; set; }
    //        public string departamentoDomicilio { get; set; }
    //        public string provinciaDomicilio { get; set; }
    //        public string distritoDomicilio { get; set; }
    //        public string direccionDomicilio { get; set; }
    //        public string sexo { get; set; }
    //        public string fechaNacimiento { get; set; }
    //    }

    //    public class RootReniec
    //    {
    //        public string codigo { get; set; }
    //        public string mensaje { get; set; }
    //        public Data data { get; set; }
    //    }
    //}
}