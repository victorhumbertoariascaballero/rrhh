using MIDIS.Utiles;
using MIDIS.ORI.Entidades;
using MIDIS.ORI.Entidades.Core;
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
using MVCSisRRHH.Models;
using System.Text;
using System.IO.Compression;
using CrystalDecisions.CrystalReports.Engine;
using SelectPdf;
using MIDIS.SEG.LogicaNegocio;

namespace MVCSisRRHH.Controllers
{
    public class ConvocatoriaController: Controller
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
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Lista()
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
        public JsonResult ListarMesConsulta()
        {
            List<Mes_Response> lista = new List<Mes_Response>();
            lista.Add(new Mes_Response("1", "ENERO"));
            lista.Add(new Mes_Response("2", "FEBRERO"));
            lista.Add(new Mes_Response("3", "MARZO"));
            lista.Add(new Mes_Response("4", "ABRIL"));
            lista.Add(new Mes_Response("5", "MAYO"));
            lista.Add(new Mes_Response("6", "JUNIO"));
            lista.Add(new Mes_Response("7", "JULIO"));
            lista.Add(new Mes_Response("8", "AGOSTO"));
            lista.Add(new Mes_Response("9", "SETIEMBRE"));
            lista.Add(new Mes_Response("10", "OCTUBRE"));
            lista.Add(new Mes_Response("11", "NOVIEMBRE"));
            lista.Add(new Mes_Response("12", "DICIEMBRE"));
            object respuesta = lista;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Postular(String id)
        {
            String strConvocatoria = String.Empty;
            String strVacantes = String.Empty;
            String strCargo = String.Empty;
            String strPerfil = String.Empty;
            String[] arraydata = new Crypto().Desencriptar(id).Split('|');

            Postulante_Request peticion = new Postulante_Request() { IdPostulante = Int32.Parse(arraydata[0]), IdConvocatoria = Int32.Parse(arraydata[1])};
            List<Convocatoria_Registro> lista = _convocatoria_Servicio.ListarConvocatoria(new Convocatoria_Request() { IdConvocatoria = Int32.Parse(arraydata[1]), Estado = 0, IdTipo = 1 }).ToList();
            if (lista != null) {
                strConvocatoria = lista[0].NroConvocatoria;
                strVacantes = lista[0].CantidadVacantes.ToString();
                strCargo = lista[0].NombreCargo.ToString();
                strPerfil = lista[0].IdPerfil.ToString();
            }

            Postulacion_Registro info = _convocatoria_Servicio.ObtenerInformacionPostulacion(peticion);

            if (info == null)
            {
                try
                {
                    //Insertamos la informacion del postulante 
                    info = _convocatoria_Servicio.InsertarRegistroPostulacion(peticion);
                    //_postulante_Servicio.InsertarRegistroPostulante(info);
                    //this.SendEmail(info, "1");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            
            ViewBag.IdPostulante = info.IdPostulante;
            ViewBag.IdPostulacion = info.IdPostulacion;
            ViewBag.IdConvocatoria = info.IdConvocatoria;
            ViewBag.IdPerfil = strPerfil;
            ViewBag.IdTipoConvocatoria = 1;
            ViewBag.Convocatoria = strConvocatoria;
            ViewBag.Vacantes = strVacantes;
            ViewBag.Cargo = strCargo; 
            
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult PostularServir(String id)
        {
            String strConvocatoria = String.Empty;
            String strVacantes = String.Empty;
            String strCargo = String.Empty;
            String strPerfil = String.Empty;
            String[] arraydata = new Crypto().Desencriptar(id).Split('|');

            Postulante_Request peticion = new Postulante_Request() { IdPostulante = Int32.Parse(arraydata[0]), IdConvocatoria = Int32.Parse(arraydata[1]) };
            List<Convocatoria_Registro> lista = _convocatoria_Servicio.ListarConvocatoria(new Convocatoria_Request() { IdConvocatoria = Int32.Parse(arraydata[1]), Estado = 0, IdTipo = 3 }).ToList();
            if (lista != null)
            {
                strConvocatoria = lista[0].NroConvocatoria;
                strVacantes = lista[0].CantidadVacantes.ToString();
                strCargo = lista[0].NombreCargo.ToString();
                strPerfil = lista[0].IdPerfil.ToString();
            }

            Postulacion_Registro info = _convocatoria_Servicio.ObtenerInformacionPostulacionServir(peticion);

            if (info == null)
            {
                try
                {
                    //Insertamos la informacion del postulante 
                    info = _convocatoria_Servicio.InsertarRegistroPostulacionServir(peticion);
                    //_postulante_Servicio.InsertarRegistroPostulante(info);
                    //this.SendEmail(info, "1");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            ViewBag.IdPostulante = info.IdPostulante;
            ViewBag.IdPostulacion = info.IdPostulacion;
            ViewBag.IdConvocatoria = info.IdConvocatoria;
            ViewBag.IdPerfil = strPerfil;
            ViewBag.IdTipoConvocatoria = 3;
            ViewBag.Convocatoria = strConvocatoria;
            ViewBag.Vacantes = strVacantes;
            ViewBag.Cargo = strCargo;

            return View();
        }

        [HttpPost]
        [Authorize]
        public JsonResult ValidarPostulacionServir(String id)
        {
            String strConvocatoria = String.Empty;
            String strRespuesta = String.Empty;
            String strCargo = String.Empty;
            String strPerfil = String.Empty;
            String[] arraydata = new Crypto().Desencriptar(HttpUtility.UrlDecode(id)).Split('|');

            List<Convocatoria_Registro> lista = _convocatoria_Servicio.ListarConvocatoria(new Convocatoria_Request() { IdConvocatoria = Int32.Parse(arraydata[1]), Estado = 0, IdTipo = 3 }).ToList();

            if (lista != null)
            {
                strConvocatoria = lista[0].NroConvocatoria;
                if (lista[0].IdTipoApertura == 2)
                {//CERRADA
                    Postulante_Request peticion = new Postulante_Request() { IdPostulante = Int32.Parse(arraydata[0]), IdConvocatoria = Int32.Parse(arraydata[1]), NroDocumento = String.Empty };
                    //Postulacion_Registro info = _convocatoria_Servicio.ObtenerInformacionPostulacionServir(peticion);
                    //PostulanteInformacion_Registro objPostulante = _postulante_Servicio.ObtenerInformacionPostulante(peticion);
                    Postulante_Registro objPostulante = _postulante_Servicio.ObtenerPostulante(peticion);
                    List<Empleado_Registro> objEmpleado = _empleado_Servicio.ListarEmpleados(new Empleado_Request() { NroDocumento = objPostulante.NroDocumento, Estado = 1, Nombre = String.Empty }).ToList();

                    if (objEmpleado != null)
                    {
                        if (objEmpleado[0].IdCondicion == 1 ||     //CAS
                            objEmpleado[0].IdCondicion == 10 ||    //DESTAQUE
                            objEmpleado[0].IdCondicion == 2 ||     //FAG
                            objEmpleado[0].IdCondicion == 3 ||     //PAC
                            objEmpleado[0].IdCondicion == 4)       //SERVIR
                        
                            strRespuesta = "1|" + "";
                        else
                            strRespuesta = "0|" + "";
                    }
                    else 
                        strRespuesta = "0|" + "";
                }
                else 
                    strRespuesta = "1|" + "";
            }
            else 
                strRespuesta = "0|" + "";


            return Json(new { success = "True", responseText = strRespuesta }); 
        }

        [HttpGet]
        [Authorize]
        public JsonResult ListarConvocatorias(Convocatoria_Request peticion)
        {
            ////peticion.Nombre = "%" + (String.IsNullOrEmpty(peticion.Nombre) ? "" : peticion.Nombre.ToUpper()) + "%";
            //object respuesta = _convocatoria_Servicio.ListarConvocatoria(peticion);
            //return Json(respuesta, JsonRequestBehavior.AllowGet);
            
            String strDNI = VariablesWeb.ConsultaInformacion.Persona.vNroDocumento;
            Postulante_Request peticionPos = new Postulante_Request() { IdPostulante = 0, NroDocumento = strDNI };
            Postulante_Registro info = _postulante_Servicio.ObtenerPostulante(peticionPos);
            
            List<Convocatoria_Registro> lista = _convocatoria_Servicio.ListarConvocatoria(peticion).ToList();
            foreach (Convocatoria_Registro obj in lista)
            {
                obj.Contrasena = HttpUtility.UrlEncode(new Crypto().Encriptar(String.Format("{0}|{1}", info.IdPostulante, obj.IdConvocatoria)));
                if (DateTime.Parse(obj.FechaPostulacion).Year == DateTime.Now.Year &&
                    DateTime.Parse(obj.FechaPostulacion).Month == DateTime.Now.Month &&
                    DateTime.Parse(obj.FechaPostulacion).Day == DateTime.Now.Day)
                    obj.IdMostrarPostular = 1;

                String strDocumentos = String.Empty;
                peticion.IdConvocatoria = obj.IdConvocatoria;
                List<ConvocatoriaDocumento_Registro> listaDoc = _convocatoria_Servicio.ListarConvocatoriaDocumento(peticion).ToList();
                foreach (ConvocatoriaDocumento_Registro objDoc in listaDoc) {
                    if (objDoc.IdTipoDocumento >= 20 && objDoc.IdTipoDocumento < 30) {
                        if (!String.IsNullOrEmpty(strDocumentos)) strDocumentos += "|";
                        strDocumentos += objDoc.NombreDocumento + "#" + objDoc.IdConvocatoriaDocumento + "#" + objDoc.FechaRegistro.Value.ToString("dd/MM/yyyy"); // HH:mm
                    }
                    if (objDoc.IdTipoDocumento == 30)
                        obj.TextoCurricular = objDoc.NombreDocumento + "#" + objDoc.IdConvocatoriaDocumento + "#" + objDoc.FechaRegistro.Value.ToString("dd/MM/yyyy"); // HH:mm
                    if (objDoc.IdTipoDocumento == 31)
                        obj.TextoConocimientos = objDoc.NombreDocumento + "#" + objDoc.IdConvocatoriaDocumento + "#" + objDoc.FechaRegistro.Value.ToString("dd/MM/yyyy"); // HH:mm
                    if (objDoc.IdTipoDocumento == 33)
                        obj.TextoFinal = objDoc.NombreDocumento + "#" + objDoc.IdConvocatoriaDocumento + "#" + objDoc.FechaRegistro.Value.ToString("dd/MM/yyyy"); // HH:mm
                }

                obj.TextoComunicados = strDocumentos;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarConvocatoriasPublico(Convocatoria_Request peticion)
        {
            ////peticion.Nombre = "%" + (String.IsNullOrEmpty(peticion.Nombre) ? "" : peticion.Nombre.ToUpper()) + "%";
            //object respuesta = _convocatoria_Servicio.ListarConvocatoria(peticion);
            //return Json(respuesta, JsonRequestBehavior.AllowGet);

            //String strDNI = VariablesWeb.ConsultaInformacion.Persona.vNroDocumento;
            //Postulante_Request peticionPos = new Postulante_Request() { IdPostulante = 0, NroDocumento = strDNI };
            //Postulante_Registro info = _postulante_Servicio.ObtenerPostulante(peticionPos);

            List<Convocatoria_Registro> lista = _convocatoria_Servicio.ListarConvocatoria(peticion).ToList();
            foreach (Convocatoria_Registro obj in lista)
            {
                //obj.Contrasena = HttpUtility.UrlEncode(new Crypto().Encriptar(String.Format("{0}|{1}", info.IdPostulante, obj.IdConvocatoria)));
                if (DateTime.Parse(obj.FechaPostulacion).Year == DateTime.Now.Year &&
                    DateTime.Parse(obj.FechaPostulacion).Month == DateTime.Now.Month &&
                    DateTime.Parse(obj.FechaPostulacion).Day == DateTime.Now.Day)
                    obj.IdMostrarPostular = 1;

                String strDocumentos = String.Empty;
                peticion.IdConvocatoria = obj.IdConvocatoria;
                List<ConvocatoriaDocumento_Registro> listaDoc = _convocatoria_Servicio.ListarConvocatoriaDocumento(peticion).ToList();
                foreach (ConvocatoriaDocumento_Registro objDoc in listaDoc)
                {
                    if (objDoc.IdTipoDocumento >= 20 && objDoc.IdTipoDocumento < 30)
                    {
                        if (!String.IsNullOrEmpty(strDocumentos)) strDocumentos += "|";
                        strDocumentos += objDoc.NombreDocumento + "#" + objDoc.IdConvocatoriaDocumento + "#" + objDoc.FechaRegistro.Value.ToString("dd/MM/yyyy"); // HH:mm
                    }
                    if (objDoc.IdTipoDocumento == 30)
                        obj.TextoCurricular = objDoc.NombreDocumento + "#" + objDoc.IdConvocatoriaDocumento + "#" + objDoc.FechaRegistro.Value.ToString("dd/MM/yyyy"); // HH:mm
                    if (objDoc.IdTipoDocumento == 31)
                        obj.TextoConocimientos = objDoc.NombreDocumento + "#" + objDoc.IdConvocatoriaDocumento + "#" + objDoc.FechaRegistro.Value.ToString("dd/MM/yyyy"); // HH:mm
                    if (objDoc.IdTipoDocumento == 33)
                        obj.TextoFinal = objDoc.NombreDocumento + "#" + objDoc.IdConvocatoriaDocumento + "#" + objDoc.FechaRegistro.Value.ToString("dd/MM/yyyy"); // HH:mm
                }

                obj.TextoComunicados = strDocumentos;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult ListarConvocatorias_Historica(Convocatoria_Request peticion)
        {
            List<Convocatoria_Historica> lista = _convocatoria_Servicio.ListarConvocatoria_Historica(peticion).ToList();

            foreach (Convocatoria_Historica obj in lista)
            {
                String strDocumentos = String.Empty;
                peticion.IdConvocatoria = obj.IdConvocatoria;
                List<ConvocatoriaDocumento_Registro> listaDoc = _convocatoria_Servicio.ListarConvocatoriaDocumento_Historica(peticion).ToList();

                foreach (ConvocatoriaDocumento_Registro objDoc in listaDoc)
                {
                    if (objDoc.IdTipoDocumento >= 1 && objDoc.IdTipoDocumento <7)
                    {
                        if (!String.IsNullOrEmpty(strDocumentos)) strDocumentos += "|";
                        strDocumentos += objDoc.TipoDocumento+ "#" + objDoc.NombreDocumento + "#" + objDoc.FechaRegistro.Value.ToString("dd/MM/yyyy"); // HH:mm
                    }
                    if (objDoc.IdTipoDocumento == 9)
                        obj.DocumentoCurricular = objDoc.NombreDocumento + "#" + objDoc.FechaRegistro.Value.ToString("dd/MM/yyyy"); // HH:mm
                    if (objDoc.IdTipoDocumento == 8)
                        obj.DocumentoConocimientos = objDoc.NombreDocumento + "#" + objDoc.FechaRegistro.Value.ToString("dd/MM/yyyy"); // HH:mm
                    if (objDoc.IdTipoDocumento == 7)
                        obj.DocumentoResultadoFinal = objDoc.NombreDocumento + "#" + objDoc.FechaRegistro.Value.ToString("dd/MM/yyyy"); // HH:mm
                    if (objDoc.IdTipoDocumento == 11)
                        obj.DocumentoReactivar = objDoc.NombreDocumento + "#" + objDoc.FechaRegistro.Value.ToString("dd/MM/yyyy"); // HH:mm
                }
                obj.DocumentoComunicados = strDocumentos;
            }
            
            return Json(lista, JsonRequestBehavior.AllowGet);
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
        [HttpPost]
        public JsonResult ListarPostulacionRequisitos(Postulacion_Registro peticion)
        {
            object respuesta = _convocatoria_Servicio.ListarPostulacionRequisitos(peticion);
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
                }
                if (registro.formatos != null)
                {
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
                }
                //PostulanteInformacion_Registro postulante = _postulante_Servicio.ObtenerPostulanteFicha(new PostulanteInformacion_Registro() { IdPostulante = registro.IdPostulante, IdPostulacion = registro.IdPostulacion, IdConvocatoria = registro.IdConvocatoria });
                object respuesta = _convocatoria_Servicio.RegistrarConvocatoria(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        //[Authorize]
        public FileResult DescargarAnexo()
        {
            Int32 IdPostulacion = (Request.QueryString.Get("IdPostulacion") == null ? 0 : Int32.Parse(Request.QueryString["IdPostulacion"]));
            
            Postulacion_Registro peticion = new Postulacion_Registro() { IdPostulacion = IdPostulacion};
            PostulacionAnexo_Registro objAnexo = _convocatoria_Servicio.ObtenerPostulacionAnexo(peticion);

            Stream pdfStream = GenerarAnexoPostulacionPdf(objAnexo);

            return File(pdfStream, "application/pdf"); // ("Ficha", "_Layout_Blank", oAsistenciaTecnicaDetalle_Registro);
        }

        public FileResult DescargarAnexo_Servir()
        {
            Int32 IdPostulacion = (Request.QueryString.Get("IdPostulacion") == null ? 0 : Int32.Parse(Request.QueryString["IdPostulacion"]));

            Postulacion_Registro peticion = new Postulacion_Registro() { IdPostulacion = IdPostulacion };
            PostulacionAnexo_Registro objAnexo = _convocatoria_Servicio.ObtenerPostulacionAnexoServir(peticion);

            Stream pdfStream = GenerarAnexoPostulacion_ServirPdf(objAnexo);

            return File(pdfStream, "application/pdf"); // ("Ficha", "_Layout_Blank", oAsistenciaTecnicaDetalle_Registro);
        }

        private Stream GenerarAnexoPostulacionPdf(PostulacionAnexo_Registro oFicha)
        {
            HtmlToPdf converter = new HtmlToPdf();
            converter.Options.PdfPageSize = PdfPageSize.A4;

            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            converter.Options.WebPageWidth = 1024;
            converter.Options.WebPageHeight = 0;
            converter.Options.WebPageFixedSize = false;
            converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.AutoFit;
            converter.Options.AutoFitHeight = HtmlToPdfPageFitMode.NoAdjustment;

            converter.Options.MarginLeft = 20;
            converter.Options.MarginRight = 20;
            converter.Options.MarginTop = 20;
            converter.Options.MarginBottom = 20;


            string html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulacion/declaracion_anexo06.html"));
            html = html.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            html = html.Replace("//dia", DateTime.Now.Day.ToString());
            html = html.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
            html = html.Replace("//anio", DateTime.Now.Year.ToString());
            html = html.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
            html = html.Replace("//dni", (oFicha.TipoDocumento == 1 ? "D.N.I. N&deg; " : "C.E. N&deg; ") + oFicha.NroDocumento);
            html = html.Replace("//direccion", oFicha.Direccion);
            html = html.Replace("//proceso_cas", oFicha.NroCAS);
            html = html.Replace("//cargo", oFicha.Puesto);

            html = html.Replace("//neporelacion1", oFicha.NepotismoRel1);
            html = html.Replace("//nepoapellido1", oFicha.NepotismoApe1);
            html = html.Replace("//neponombre1", oFicha.NepotismoNom1);
            html = html.Replace("//nepoarea1", oFicha.NepotismoAre1);
            html = html.Replace("//neporelacion2", oFicha.NepotismoRel2);
            html = html.Replace("//nepoapellido2", oFicha.NepotismoApe2);
            html = html.Replace("//neponombre2", oFicha.NepotismoNom2);
            html = html.Replace("//nepoarea2", oFicha.NepotismoAre2);
            html = html.Replace("//neporelacion3", oFicha.NepotismoRel3);
            html = html.Replace("//nepoapellido3", oFicha.NepotismoApe3);
            html = html.Replace("//neponombre3", oFicha.NepotismoNom3);
            html = html.Replace("//nepoarea3", oFicha.NepotismoAre3);

            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(html, Server.MapPath("~/temp"));
            

            string html2 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulacion/declaracion_prohibicion.html"));
            html2 = html2.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            html2 = html2.Replace("//dia", DateTime.Now.Day.ToString());
            html2 = html2.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
            html2 = html2.Replace("//anio", DateTime.Now.Year.ToString());
            html2 = html2.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
            html2 = html2.Replace("//dni", (oFicha.TipoDocumento == 1 ? "D.N.I. N&deg; " : "C.E. N&deg; ") + oFicha.NroDocumento);

            SelectPdf.PdfDocument doc2 = converter.ConvertHtmlString(html2, Server.MapPath("~/temp"));
            foreach (PdfPage page in doc2.Pages)
                doc.AddPage(page);



            string html3 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulacion/declaracion_formato01.html"));
            html3 = html3.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            html3 = html3.Replace("//dia", DateTime.Now.Day.ToString());
            html3 = html3.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
            html3 = html3.Replace("//anio", DateTime.Now.Year.ToString());
            html3 = html3.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
            html3 = html3.Replace("//dni", (oFicha.TipoDocumento == 1 ? "D.N.I. N&deg; " : "C.E. N&deg; ") + oFicha.NroDocumento);
            html3 = html3.Replace("//direccion", oFicha.Direccion);
            html3 = html3.Replace("//cargo", oFicha.Puesto);

            SelectPdf.PdfDocument doc3 = converter.ConvertHtmlString(html3, Server.MapPath("~/temp"));
            foreach (PdfPage page in doc3.Pages)
                doc.AddPage(page);


            string html4 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulacion/declaracion_formato02.html"));
            html4 = html4.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            html4 = html4.Replace("//dia", DateTime.Now.Day.ToString());
            html4 = html4.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
            html4 = html4.Replace("//anio", DateTime.Now.Year.ToString());
            html4 = html4.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
            html4 = html4.Replace("//dni", (oFicha.TipoDocumento == 1 ? "D.N.I. N&deg; " : "C.E. N&deg; ") + oFicha.NroDocumento);
            html4 = html4.Replace("//proceso_cas", oFicha.NroCAS);

            SelectPdf.PdfDocument doc4 = converter.ConvertHtmlString(html4, Server.MapPath("~/temp"));
            foreach (PdfPage page in doc4.Pages)
                doc.AddPage(page);

            string html5 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulacion/declaracion_formato02_anexo.html"));
            html5 = html5.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));

            SelectPdf.PdfDocument doc5 = converter.ConvertHtmlString(html5, Server.MapPath("~/temp"));
            foreach (PdfPage page in doc5.Pages)
                doc.AddPage(page);



            MemoryStream pdfStream = new MemoryStream();
            doc.Save(pdfStream);
            pdfStream.Position = 0;
            doc.Close();

            return pdfStream;
        }

        private Stream GenerarAnexoPostulacion_ServirPdf(PostulacionAnexo_Registro oFicha)
        {
            HtmlToPdf converter = new HtmlToPdf();
            converter.Options.PdfPageSize = PdfPageSize.A4;

            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            converter.Options.WebPageWidth = 1024;
            converter.Options.WebPageHeight = 0;
            converter.Options.WebPageFixedSize = false;
            converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.AutoFit;
            converter.Options.AutoFitHeight = HtmlToPdfPageFitMode.NoAdjustment;

            converter.Options.MarginLeft = 20;
            converter.Options.MarginRight = 20;
            converter.Options.MarginTop = 20;
            converter.Options.MarginBottom = 20;


            string html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulacion/declaracion_anexo06.html"));
            html = html.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            html = html.Replace("//dia", DateTime.Now.Day.ToString());
            html = html.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
            html = html.Replace("//anio", DateTime.Now.Year.ToString());
            html = html.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
            html = html.Replace("//dni", (oFicha.TipoDocumento == 1 ? "D.N.I. N&deg; " : "C.E. N&deg; ") + oFicha.NroDocumento);
            html = html.Replace("//direccion", oFicha.Direccion);
            html = html.Replace("//proceso_cas", oFicha.NroCAS);
            html = html.Replace("//cargo", oFicha.Puesto);

            html = html.Replace("//neporelacion1", oFicha.NepotismoRel1);
            html = html.Replace("//nepoapellido1", oFicha.NepotismoApe1);
            html = html.Replace("//neponombre1", oFicha.NepotismoNom1);
            html = html.Replace("//nepoarea1", oFicha.NepotismoAre1);
            html = html.Replace("//neporelacion2", oFicha.NepotismoRel2);
            html = html.Replace("//nepoapellido2", oFicha.NepotismoApe2);
            html = html.Replace("//neponombre2", oFicha.NepotismoNom2);
            html = html.Replace("//nepoarea2", oFicha.NepotismoAre2);
            html = html.Replace("//neporelacion3", oFicha.NepotismoRel3);
            html = html.Replace("//nepoapellido3", oFicha.NepotismoApe3);
            html = html.Replace("//neponombre3", oFicha.NepotismoNom3);
            html = html.Replace("//nepoarea3", oFicha.NepotismoAre3);

            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(html, Server.MapPath("~/temp"));


            string html2 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulacion/declaracion_prohibicion.html"));
            html2 = html2.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            html2 = html2.Replace("//dia", DateTime.Now.Day.ToString());
            html2 = html2.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
            html2 = html2.Replace("//anio", DateTime.Now.Year.ToString());
            html2 = html2.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
            html2 = html2.Replace("//dni", (oFicha.TipoDocumento == 1 ? "D.N.I. N&deg; " : "C.E. N&deg; ") + oFicha.NroDocumento);

            SelectPdf.PdfDocument doc2 = converter.ConvertHtmlString(html2, Server.MapPath("~/temp"));
            foreach (PdfPage page in doc2.Pages)
                doc.AddPage(page);



            string html3 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulacion/declaracion_formato01.html"));
            html3 = html3.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            html3 = html3.Replace("//dia", DateTime.Now.Day.ToString());
            html3 = html3.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
            html3 = html3.Replace("//anio", DateTime.Now.Year.ToString());
            html3 = html3.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
            html3 = html3.Replace("//dni", (oFicha.TipoDocumento == 1 ? "D.N.I. N&deg; " : "C.E. N&deg; ") + oFicha.NroDocumento);
            html3 = html3.Replace("//direccion", oFicha.Direccion);
            html3 = html3.Replace("//cargo", oFicha.Puesto);

            SelectPdf.PdfDocument doc3 = converter.ConvertHtmlString(html3, Server.MapPath("~/temp"));
            foreach (PdfPage page in doc3.Pages)
                doc.AddPage(page);


            string html4 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulacion/declaracion_formato02.html"));
            html4 = html4.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            html4 = html4.Replace("//dia", DateTime.Now.Day.ToString());
            html4 = html4.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
            html4 = html4.Replace("//anio", DateTime.Now.Year.ToString());
            html4 = html4.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
            html4 = html4.Replace("//dni", (oFicha.TipoDocumento == 1 ? "D.N.I. N&deg; " : "C.E. N&deg; ") + oFicha.NroDocumento);
            html4 = html4.Replace("//proceso_cas", oFicha.NroCAS);

            SelectPdf.PdfDocument doc4 = converter.ConvertHtmlString(html4, Server.MapPath("~/temp"));
            foreach (PdfPage page in doc4.Pages)
                doc.AddPage(page);

            string html5 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulacion/declaracion_formato02_anexo.html"));
            html5 = html5.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));

            SelectPdf.PdfDocument doc5 = converter.ConvertHtmlString(html5, Server.MapPath("~/temp"));
            foreach (PdfPage page in doc5.Pages)
                doc.AddPage(page);

            string html6 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulacion/declaracion_formato01_anexo05.html"));
            html6 = html6.Replace("//logo_PCM", Path.Combine(Server.MapPath("~/Templates/"), "Postulacion/PCM_logo.png"));
            html6 = html6.Replace("//logo_SERVIR", Path.Combine(Server.MapPath("~/Templates/"), "Postulacion/Servir_Logo.png"));
            html6 = html6.Replace("//dia", DateTime.Now.Day.ToString());
            html6 = html6.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
            html6 = html6.Replace("//anio", DateTime.Now.Year.ToString());
            html6 = html6.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
            html6 = html6.Replace("//dni", oFicha.NroDocumento);
            SelectPdf.PdfDocument doc6 = converter.ConvertHtmlString(html6, Server.MapPath("~/temp"));
            foreach (PdfPage page in doc6.Pages)
                doc.AddPage(page);

            string html7 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulacion/declaracion_formato02_anexo05.html"));
            html7 = html7.Replace("//logo_PCM", Path.Combine(Server.MapPath("~/Templates/"), "Postulacion/PCM_logo.png"));
            html7 = html7.Replace("//logo_SERVIR", Path.Combine(Server.MapPath("~/Templates/"), "Postulacion/Servir_Logo.png"));
            html7 = html7.Replace("//dia", DateTime.Now.Day.ToString());
            html7 = html7.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
            html7 = html7.Replace("//anio", DateTime.Now.Year.ToString());
            html7 = html7.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
            html7 = html7.Replace("//dni", oFicha.NroDocumento);
            SelectPdf.PdfDocument doc7 = converter.ConvertHtmlString(html7, Server.MapPath("~/temp"));
            foreach (PdfPage page in doc7.Pages)
                doc.AddPage(page);

            MemoryStream pdfStream = new MemoryStream();
            doc.Save(pdfStream);
            pdfStream.Position = 0;
            doc.Close();

            return pdfStream;
        }

        [HttpPost]
        public JsonResult ObtenerParaEditar(Convocatoria_Request peticion)
        {
            object respuesta = _convocatoria_Servicio.ObtenerParaEditar(peticion);

            return Json(respuesta);
        }
        
        [AllowAnonymous]
        public ActionResult DescargarArchivo(string id)
        {
            Contrato_Request peticion = new Contrato_Request();
            peticion.IdContrato = Int32.Parse(id);
            peticion.Nombre = String.Empty;
            peticion.Estado = -1;

            //var lista = _contrato_Servicio.ListarContratos(peticion).Select(p => new { p.IdContrato, p.NroContrato, p.Anio, p.NombreContrato, p.archivo });
            //var item = lista.Where(x => x.IdContrato == peticion.IdContrato).SingleOrDefault();
            ////if (item != null) {
            ////    if (arraydata.Length > 3) {
            ////        if (arraydata[3].ToString() == "1")
            ////        {
            ////            //ACTUALIZAMOS LA FECHA DE RECEPCION EN LA BASE DE DATOS
            ////            _empleado_Servicio.ActualizarRecepcion(peticion);
            ////        }
            ////    }
            ////}

            //return File(item.archivo, "application/pdf", item.NombreContrato + ".pdf");

            return null;
        }
        //[DeleteTempFile]
        [AllowAnonymous]
        [HttpGet]
        public ActionResult DescargarArchivoPractica(String idConvocatoria, String idTipo)
        {
            Convocatoria_Request peticion = new Convocatoria_Request();
            peticion.IdConvocatoria = Int32.Parse(idConvocatoria);

            Convocatoria_Registro item = _convocatoria_Servicio.ObtenerConvocatoriaPracticaDocumento(peticion);

            switch (idTipo)
            {
                case "1": return File(item.fileRequerimiento, "application/pdf", String.Format("Bases_{0}.pdf", idConvocatoria));
                case "2": return File(item.fileCertificacion, "application/pdf", String.Format("Anexos_{0}.pdf", idConvocatoria));
                case "3": return File(item.fileComite, "application/pdf", String.Format("Anexo_03_{0}.pdf", idConvocatoria));
                case "10": return File(item.fileActaCurri, "application/pdf", String.Format("Anexo_07_{0}.pdf", idConvocatoria));
            }

            return null;
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

        [AllowAnonymous]
        public ActionResult DescargarArchivoBases(string id)
        {
            BasesPerfilPuestoRegistro peticion = new BasesPerfilPuestoRegistro();
            peticion.iCodBasePerfil = Int32.Parse(id);
            //peticion.Nombre = String.Empty;
            //peticion.Estado = -1;

            var lista = new T_genm_bases_perfil_puesto_LN().ObtenerBasesPerfilesPuestoPorID(id).Select(p => new { p.iCodBasePerfil, p.strNombreArchivo, p.archivo });
            var item = lista.Where(x => x.iCodBasePerfil == peticion.iCodBasePerfil).SingleOrDefault();

            return File(item.archivo, "application/pdf", "BasesConvocatoria" + ".pdf");
        }
        [AllowAnonymous]
        public ActionResult DescargarArchivoBasesPractica(string id)
        {
            BasesPerfilPuestoRegistro peticion = new BasesPerfilPuestoRegistro();
            peticion.iCodBasePerfil = Int32.Parse(id);
            //peticion.Nombre = String.Empty;
            //peticion.Estado = -1;

            var lista = new T_genm_bases_perfil_puesto_LN().ObtenerBasesPerfilesPuestoPorID(id).Select(p => new { p.iCodBasePerfil, p.strNombreArchivo, p.archivo });
            var item = lista.Where(x => x.iCodBasePerfil == peticion.iCodBasePerfil).SingleOrDefault();

            return File(item.archivo, "application/pdf", "BasesConvocatoria" + ".pdf");
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

    }
}