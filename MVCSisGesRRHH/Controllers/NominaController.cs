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
using SelectPdf;

namespace MVCSisGesRRHH.Controllers
{
    public class NominaController: Controller
	{
        private readonly T_genm_empleado_LN _empleado_Servicio = new T_genm_empleado_LN();
        
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult IndexAnterior()
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
        [Authorize]
        public JsonResult ListarEstados()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("-1", "--Todos--"));
            lista.Add(new Estado_Response("0", "INACTIVO"));
            lista.Add(new Estado_Response("1", "ACTIVO"));
            
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
        public JsonResult ListarAnioPostulacion()
        {
            List<Anio_Response> lista = new List<Anio_Response>();
            for (Int32 iPos = 0; iPos < 50; iPos++)
            {
                lista.Add(new Anio_Response() { 
                 Anio = (DateTime.Now.Year - iPos).ToString()
                });
            }
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

        //[HttpPost]
        //[Authorize]
        //public JsonResult Registrar(BoletaCarga_Registro registro)
        //{
        //    try
        //    {
        //        registro.FechaModificacion = DateTime.Now;
        //        registro.IdUsuarioModificacion = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iUsu);
                                
        //        StringBuilder strNoEncontrados = new StringBuilder();
        //        String[] nombre;
        //        String nameFile = String.Empty;
        //        for (Int32 j = 0; j<registro.formatos.ToList().Count; j++)
        //        {
        //            HttpPostedFileBase postfile = ((HttpPostedFileBase[])(registro.formatos.ToList())[j])[0];

        //            if (postfile.ContentLength > 0)
        //            {
        //                //string savedFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "signed", Path.GetFileName(postfile.FileName));
        //                nameFile = postfile.FileName;
        //                nombre = nameFile.Split('-');
        //                if (nombre[1] == registro.Anio && nombre[2] == registro.Mes)
        //                {
        //                    registro.NroDocumento = nombre[0];
        //                    registro.Tipo = nombre[3];

        //                    Stream str = postfile.InputStream;
        //                    BinaryReader Br = new BinaryReader(str);
        //                    Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

        //                    registro.archivo = FileDet;

        //                    registro.IdEmpleado = _empleado_Servicio.Insertar(registro);
        //                    if (registro.IdEmpleado > 0)
        //                    {
        //                        strNoEncontrados.Append("<div style='margin-top: 0px; background-color: rgb(255, 255, 255);'>" + "La boleta laboral del trabajador con Nro documento " + registro.NroDocumento + " se carg� de forma exitosa" + "</div>");
        //                        if (ConfigurationManager.AppSettings["envioMail"].ToString() == "1")
        //                        {
        //                            Empleado_Registro empleado = _empleado_Servicio.ListarEmpleadosBoleta(new Empleado_Registro() { IdEmpleado = registro.IdEmpleado, Anio = registro.Anio, Mes = registro.Mes }).ToList()[0];
        //                            if (!SendEmail(empleado, registro.Tipo))
        //                                strNoEncontrados.Append("<div style='margin-top: 0px; background-color: rgb(255, 255, 255);'>" + "La boleta laboral del trabajador con Nro documento " + registro.NroDocumento + " NO se pudo enviar a su correo electr�nico" + "</div>");
        //                        }
        //                    }
        //                    else if (registro.IdEmpleado == -1) 
        //                        strNoEncontrados.Append("<div style='margin-top: 0px; background-color: rgb(255, 255, 255);'>" + "El trabajador con Nro documento " + registro.NroDocumento + " ya tiene una boleta cargada para el periodo seleccionado" + "</div>");
        //                    else
        //                        strNoEncontrados.Append("<div style='margin-top: 0px; background-color: rgb(255, 255, 255);'>" + "El trabajador con Nro documento " + registro.NroDocumento + " no est� registrado en la n�mina laboral" + "</div>");
        //                }
        //                else 
        //                    strNoEncontrados.Append("<div style='margin-top: 0px; background-color: rgb(255, 255, 255);'>" + "La boleta laboral del trabajador con Nro documento " + registro.NroDocumento + " no corresponde al periodo seleccionado" + "</div>");
        //            }
        //        }

        //        if (String.IsNullOrEmpty(strNoEncontrados.ToString()))
        //            return Json(true);
        //        else
        //            return Json(new { success = "False", responseText = strNoEncontrados.ToString() });
        //    }
        //    catch (System.Data.SqlClient.SqlException es)
        //    {
        //        return Json(new { success = "False", responseText = es.Message });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = "False", responseText = ex.Message });
        //    }
        //}

        
        //[HttpPost]
        //public JsonResult ExportarBoleta(String anio, String mes)
        //{
        //    var fileName = "Boletas_Midis_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".zip";
        //    string fullPath = Path.Combine(Server.MapPath("~/temp"), fileName);
        //    using (var memoryStream = new MemoryStream()) //50 * 1024 * 1024
        //    {
        //        using (var ziparchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
        //        {
        //            Empleado_Registro registro = new Empleado_Registro();
        //            registro.Anio = anio;
        //            registro.Mes = mes;

        //            try
        //            {
        //                // RECORREMOS LA LISTA DE EMPLEADOS
        //                List<EmpleadoSisper_Registro> lista = _empleado_Servicio.ListarEmpleadosSisper(registro).ToList();
        //                List<EmpleadoConceptoSisper_Registro> lstConcepto = (List<EmpleadoConceptoSisper_Registro>)_empleado_Servicio.ListarEmpleadoConceptoSisper(registro);
        //                List<EmpleadoConceptoSisper_Registro> lstConceptoIngreso = null;
        //                List<EmpleadoConceptoSisper_Registro> lstConceptoDescuento = null;
        //                List<EmpleadoConceptoSisper_Registro> lstConceptoAporte = null;

        //                foreach (EmpleadoSisper_Registro obj in lista)
        //                {
        //                    using (ReportDocument report = new ReportDocument())
        //                    {
        //                        lstConceptoIngreso = lstConcepto.Where(x => x.TipoConcepto == "0" && x.Trabajador == obj.Trabajador).ToList();
        //                        lstConceptoDescuento = lstConcepto.Where(x => x.TipoConcepto != "0" && x.Trabajador == obj.Trabajador).Where(x => x.TipoConcepto != "9" && x.Trabajador == obj.Trabajador).ToList();
        //                        lstConceptoAporte = lstConcepto.Where(x => x.TipoConcepto == "9" && x.Trabajador == obj.Trabajador).ToList();

        //                        try
        //                        {
        //                            report.Load(System.Web.HttpContext.Current.Server.MapPath("~/Reportes/Formato1.rpt"));
        //                            report.FileName = System.Web.HttpContext.Current.Server.MapPath("~/Reportes/Formato1.rpt");
        //                            report.OpenSubreport("lstIngreso").SetDataSource(lstConceptoIngreso);
        //                            report.OpenSubreport("lstDescuento").SetDataSource(lstConceptoDescuento);
        //                            report.OpenSubreport("lstAporte").SetDataSource(lstConceptoAporte);
        //                            report.SetDataSource(lista.Where(x => x.Trabajador == obj.Trabajador).ToList());

        //                            Stream oStream = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //                            oStream.Seek(0, SeekOrigin.Begin);

        //                            var readmeEntry = ziparchive.CreateEntry(String.Format("{0}-{1}-{2}-{3}-{4}{5}", obj.NroDocumento, anio, mes.PadLeft(2, '0'), "01", "08", ".pdf"));
        //                            using (Stream writer = readmeEntry.Open())
        //                            {
        //                                oStream.CopyTo(writer);
        //                            }
        //                            oStream.Close();
        //                            oStream.Dispose();
        //                        }
        //                        catch (Exception)
        //                        {
        //                            report.Dispose();
        //                        }

        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                return Json(new { fileName = "", responseText = ex.Message });
        //            }
        //        }

        //        FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
        //        memoryStream.WriteTo(file);
        //        file.Close();
        //    }

        //    return Json(new { fileName = fileName, responseText = "" });
        //}
        //[HttpGet]
        //public ActionResult DescargarBoletaArchivo(string file)
        //{
        //    string fullPath = Path.Combine(Server.MapPath("~/temp"), file);
        //    //return the file for download, this is an Excel so I set the file content type to "application/vnd.ms-excel"
        //    return File(fullPath, "application/zip", file);
        //}

        [HttpPost]
        public JsonResult ExportarBoleta(String anio, String mes)
        {
            var directory = "Boletas_Midis_" + DateTime.Now.ToString("yyyyMMddHHmm");
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
                        lstConceptoIngreso = lstConcepto.Where(x => x.TipoConcepto == "0" && 
                                                                    x.Trabajador == obj.Trabajador &&
                                                                    x.IdPlanilla == obj.IdPlanilla &&
                                                                    x.TipoPlanilla == obj.TipoPlanilla).ToList();
                        lstConceptoDescuento = lstConcepto.Where(x => x.TipoConcepto != "0" &&
                                                                      x.Trabajador == obj.Trabajador &&
                                                                      x.IdPlanilla == obj.IdPlanilla &&
                                                                      x.TipoPlanilla == obj.TipoPlanilla).Where(x => x.TipoConcepto != "9" &&
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

        [HttpGet]
        public ActionResult DescargarBoletaArchivo(string file)
        {
            string fullPath = Path.Combine(Server.MapPath("~/temp"), file);
            ZipFile.CreateFromDirectory(fullPath, fullPath + ".zip");
            
            return File(fullPath + ".zip", "application/zip", "Boletas_Midis.zip");
        }

        [HttpGet]
        public ActionResult DescargarBoleta(String anio, String mes, String trabajador, String planilla, String tipoplanilla)
        {
            Empleado_Registro registro = new Empleado_Registro();
            registro.Anio = anio;
            registro.Mes = mes;
            registro.CodigoSisper = trabajador;

            ReportDocument report = new ReportDocument();

            try
            {
                List<EmpleadoSisper_Registro> lista = _empleado_Servicio.ListarEmpleadosSisper(registro).ToList();
                List<EmpleadoConceptoSisper_Registro> lstConcepto = (List<EmpleadoConceptoSisper_Registro>)_empleado_Servicio.ListarEmpleadoConceptoSisper(registro);

                List<EmpleadoConceptoSisper_Registro> lstConceptoIngreso = lstConcepto.Where(x => x.TipoConcepto == "0" &&
                                                                                                  x.IdPlanilla == planilla &&
                                                                                                  x.TipoPlanilla == tipoplanilla).ToList();
                List<EmpleadoConceptoSisper_Registro> lstConceptoDescuento = lstConcepto.Where(x => x.TipoConcepto != "0" &&
                                                                                                    x.IdPlanilla == planilla &&
                                                                                                    x.TipoPlanilla == tipoplanilla).Where(x => x.TipoConcepto != "9" &&
                                                                                                                                               x.IdPlanilla == planilla &&
                                                                                                                                               x.TipoPlanilla == tipoplanilla).ToList();
                List<EmpleadoConceptoSisper_Registro> lstConceptoAporte = lstConcepto.Where(x => x.TipoConcepto == "9" &&
                                                                                                 x.IdPlanilla == planilla &&
                                                                                                 x.TipoPlanilla == tipoplanilla).ToList();
                
                report.Load(System.Web.HttpContext.Current.Server.MapPath("~/Reportes/Formato1.rpt"));
                report.FileName = System.Web.HttpContext.Current.Server.MapPath("~/Reportes/Formato1.rpt");
                report.OpenSubreport("lstIngreso").SetDataSource(lstConceptoIngreso);
                report.OpenSubreport("lstDescuento").SetDataSource(lstConceptoDescuento);
                report.OpenSubreport("lstAporte").SetDataSource(lstConceptoAporte);
                report.SetDataSource(lista.Where(x => x.IdPlanilla == planilla &&
                                                      x.TipoPlanilla == tipoplanilla).ToList());

                Stream oStream = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat); 
                oStream.Seek(0, SeekOrigin.Begin);

                return File(oStream, "application/pdf", String.Format("{0}-{1}-{2}-{3}{4}-{5}", lista[0].NroDocumento, anio, mes.PadLeft(2, '0'), lista[0].IdPlanilla, lista[0].TipoPlanilla, "01.pdf"));
            }
            catch (Exception ex)
            {
                Byte[] nulo = null;
                return File(nulo, "application/pdf", "NoEncontrado.pdf");
            }
        }

        [AllowAnonymous]
        public ActionResult DescargarArchivo(string id)
        {
            Empleado_Registro peticion = new Empleado_Registro();
            String[] arraydata = id.Split('|');

            peticion.IdEmpleado = Int32.Parse(arraydata[0].ToString());
            peticion.Anio = arraydata[1].ToString();
            peticion.Mes = arraydata[2].ToString();

            //var codigo = int.Parse(EncryptionHelper.Decrypt(arraydata[1].ToString()));

            var lista = _empleado_Servicio.ListarEmpleadosBoleta(peticion).Select(p => new { p.IdEmpleado, p.NroDocumento, p.Boleta });

            var item = lista.Where(x => x.IdEmpleado == peticion.IdEmpleado).SingleOrDefault();
            if (item != null) {
                if (arraydata.Length > 3) {
                    if (arraydata[3].ToString() == "1")
                    {
                        //ACTUALIZAMOS LA FECHA DE RECEPCION EN LA BASE DE DATOS
                        _empleado_Servicio.ActualizarRecepcion(peticion);
                    }
                }
            }

            return File(item.Boleta, "application/pdf", item.NroDocumento + ".pdf");
        }

        public FileResult FichaPersonal()
        {
            Int32 IdEmpleado = (Request.QueryString.Get("IdEmpleado") == null ? 0 : Int32.Parse(Request.QueryString["IdEmpleado"]));
            String NroDocumento = (Request.QueryString.Get("NroDocumento") == null ? String.Empty : Convert.ToString((Request.QueryString["NroDocumento"])));

            Empleado_Request peticion = new Empleado_Request() { IdEmpleado = IdEmpleado };
            Empleado_Registro empleado = _empleado_Servicio.ObtenerParaEditar(peticion);
            List<EmpleadoFamiliar_Registro> lstFamiliar = _empleado_Servicio.ListarFamiliaresEmpleado(peticion).ToList();
            List<EmpleadoCuenta_Registro> lstCuentas = _empleado_Servicio.ListarCuentasEmpleado(peticion).ToList();

            peticion.TipoDocumento = "1";
            peticion.NroDocumento = NroDocumento;
            List<Empleado_Registro> lstMovimiento = _empleado_Servicio.ListarDesplazamientoEmpleado(peticion).ToList();
            List<EmpleadoEncargatura_Registro> lstEncargatura = _empleado_Servicio.ListarEncargaturasEmpleado(peticion).ToList();

            Stream pdfStream = GenerarFichaPersonalPdf(empleado, lstFamiliar, lstCuentas, lstMovimiento, lstEncargatura);

            return File(pdfStream, "application/pdf"); // ("Ficha", "_Layout_Blank", oAsistenciaTecnicaDetalle_Registro);
        }

        private Stream GenerarFichaPersonalPdf(Empleado_Registro oFicha, List<EmpleadoFamiliar_Registro> lstFamiliar, List<EmpleadoCuenta_Registro> lstCuentas, List<Empleado_Registro> lstMovimiento, List<EmpleadoEncargatura_Registro> lstEncargatura )
        {
            HtmlToPdf converter = new HtmlToPdf();
            converter.Options.PdfPageSize = PdfPageSize.A4;

            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            converter.Options.WebPageWidth = 1024; //1024
            converter.Options.WebPageHeight = 0;
            converter.Options.WebPageFixedSize = false;
            converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.AutoFit; // ShrinkOnly;
            converter.Options.AutoFitHeight = HtmlToPdfPageFitMode.NoAdjustment;

            converter.Options.MarginLeft = 20;
            converter.Options.MarginRight = 20;
            converter.Options.MarginTop = 20;
            converter.Options.MarginBottom = 20;

            //List<Dependencia_Registro> lista = new T_genm_dependencia_LN().ListarDependencias(new Dependencia_Request() { IdDependencia = oFicha.IdDependencia, Nombre = "%%" }).ToList();

            string html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Nomina/ficha_personal1.html"));
            //string htmlParticipantes = System.IO.File.ReadAllText(ConfigurationManager.AppSettings["Negociacion.Ficha.Plantilla.Participantes.Ruta"]);
            //string htmlIndicadores = System.IO.File.ReadAllText(ConfigurationManager.AppSettings["Negociacion.Ficha.Plantilla.Indicadores.Ruta"]);

            //CultureInfo culture = new CultureInfo("es-PE");
            html = html.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            //html = html.Replace("//planilla", oFicha.NroPlanilla);
            //html = html.Replace("//contrato", String.Empty);
            html = html.Replace("//foto", oFicha.Foto);
            html = html.Replace("//unidad_organica", oFicha.NombreOficina);
            html = html.Replace("//cargo", oFicha.NombreCargo);
            html = html.Replace("//remuneracion", oFicha.Remuneracion.ToString("C"));
            html = html.Replace("//meta", oFicha.Meta);

            html = html.Replace("//paterno", oFicha.Paterno);
            html = html.Replace("//materno", oFicha.Materno);
            html = html.Replace("//nombres", oFicha.Nombre);
            html = html.Replace("//sexo", (oFicha.IdGenero == 1 ? "F" : "M"));
            //html = html.Replace("//edad", oFicha.Edad.ToString());
            html = html.Replace("//nacionalidad", (oFicha.TipoDocumento == "01" ? "PERUANO" : String.Empty));
            html = html.Replace("//nacimiento", oFicha.FechaNacimiento);
            //html = html.Replace("//lugar_nacimiento", oFicha.LugarNacimiento.ToUpper());
            html = html.Replace("//dni", (oFicha.TipoDocumento == "01" ? oFicha.NroDocumento : String.Empty));
            html = html.Replace("//extranjeria", (oFicha.TipoDocumento != "01" ? oFicha.NroDocumento : String.Empty));
            html = html.Replace("//direccion", oFicha.Domicilio);
            html = html.Replace("//ubigeo", oFicha.DescripcionUbigeo);
            html = html.Replace("//estado_civil", (oFicha.IdEstadoCivil == 1 ? "SOLTERO(A)" : (oFicha.IdEstadoCivil == 2 ? "CASADO(A)" : (oFicha.IdEstadoCivil == 3 ? "DIVORCIADO(A)" : (oFicha.IdEstadoCivil == 4 ? "VIUDO(A)" : "")))));
            //html = html.Replace("//tipo_vivienda", (oFicha.IdTipoVivienda == 1 ? "PROPIA" : (oFicha.IdTipoVivienda == 2 ? "ALQUILADA" : (oFicha.IdTipoVivienda == 3 ? "FAMILIARES" : (oFicha.IdTipoVivienda == 4 ? "PENSI�N" : (oFicha.IdTipoVivienda == 5 ? "TEMPORAL" : ""))))));
            //html = html.Replace("//brevete", (oFicha.IdTipoBrevete == 0 ? "" : (oFicha.IdTipoBrevete == 1 ? "AUTOMOVIL" : (oFicha.IdTipoBrevete == 2 ? "MOTOCICLETA" : ""))));
            //html = html.Replace("//licencia", (oFicha.IdTipoBrevete == 0 ? "" : oFicha.NroLicencia));
            html = html.Replace("//ruc", oFicha.RUC);
            html = html.Replace("//airhsp", oFicha.NroAIRHSP);
            html = html.Replace("//nro_contrato", oFicha.NroContrato);
            html = html.Replace("//grupo_san", oFicha.GrupoSanguineo);
            html = html.Replace("//forma_ing", oFicha.TipoIngreso);
            html = html.Replace("//condicion", oFicha.CondicionLaboral);
            html = html.Replace("//sede", oFicha.Sede);
            html = html.Replace("//fecha_ing", oFicha.FechaInicio.Value.ToString("dd/MM/yyyy"));
            html = html.Replace("//docu_ing", oFicha.DocIngreso);
            html = html.Replace("//fecha_sal", (oFicha.FechaCese.HasValue ? oFicha.FechaCese.Value.ToString("dd/MM/yyyy") : ""));
            html = html.Replace("//docu_sal", oFicha.DocCese);

            html = html.Replace("//email_per", oFicha.CorreoElectronico.ToUpper());
            html = html.Replace("//email_lab", oFicha.CorreoElectronicoLaboral.ToUpper());
            html = html.Replace("//celular_per", oFicha.Celular);
            html = html.Replace("//tel_cel", oFicha.CelularLaboral);
            html = html.Replace("//tel_fijo", oFicha.TelefonoLaboral);
            html = html.Replace("//tel_anexo", oFicha.AnexoLaboral);
            
            html = html.Replace("//ch_director", (oFicha.Director == 1 ? "SI" : "NO"));
            html = html.Replace("//ch_sindicato", (oFicha.Sindicato ? "SI" : "NO"));
            html = html.Replace("//ch_discapacidad", (oFicha.Discapacidad ? " SI " : " NO "));
            html = html.Replace("//ch_dji", (oFicha.DDJJ ? " SI " : " NO "));
            
            html = html.Replace("//tipo_contrato", (oFicha.IdTipoContrato == 1 ? "INDETERMINADO" : (oFicha.IdTipoContrato == 2 ? "TEMPORAL" : (oFicha.IdTipoContrato == 3 ? "TRANSITORIO" : (oFicha.IdTipoContrato == 4 ? "DESIGNADO" : (oFicha.IdTipoContrato == 5 ? "JUDICIAL" : ""))))));
            html = html.Replace("//tipo_pensiones", (oFicha.IdTipoPension == 1 ? "AFP" : (oFicha.IdTipoPension == 2 ? "ONP" : (oFicha.IdTipoPension == 3 ? "SIN R�GIMEN" : (oFicha.IdTipoPension == 4 ? "SIN C�LCULO" : "")))));
            html = html.Replace("//codigo_afp", oFicha.CodigoAfp);
            html = html.Replace("//nombre_afp", (oFicha.IdTipoAfp == 1 ? "PROFUTURO" : (oFicha.IdTipoAfp == 2 ? "INTEGRA" : (oFicha.IdTipoAfp == 3 ? "PRIMA" : (oFicha.IdTipoAfp == 4 ? "HABITAT" : String.Empty)))));
            html = html.Replace("//comision_afp", (oFicha.IdTipoComisionAfp == 1 ? "COMISI�N POR FLUJO" : (oFicha.IdTipoComisionAfp == 2 ? "COMISI�N MIXTA" : String.Empty)));
            

            String strAux;
            String strFamiliar = "<tr><td style='font-size: 9px; width: 169.727px;'>//paren</td>" +
                                "<td style='font-size: 9px; width: 414.273px;'>//nombre</td>" +
                                "<td style='font-size: 9px; width: 113px;'>//dni</td>" +
                                "<td style='font-size: 9px; width: 137px;'>//fecha</td>" +
                                "<td style='font-size: 9px; width: 61px;'>//edad</td>" +
                                "<td style='font-size: 9px; width: 404px;'>//ocupa</td>" +
                                "<td style='font-size: 9px; width: 73px;'>//sexo</td></tr>";
            String strFamiliares = String.Empty;
            //List<PostulanteFamiliar_Registro> familiares = _empleado_Servicio.ListarFamiliaresEmpleado(peticion).ToList();
            if (lstFamiliar.Count > 0) {
                foreach (EmpleadoFamiliar_Registro item in lstFamiliar) {
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
            else
            {
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
            html = html.Replace("//familiares", strFamiliares);

            strAux = String.Empty;
            String strCuenta = "<tr><td style='font-size: 9px; width: 370px;' colspan='2'>//entidad</td>" +
                                "<td style='font-size: 9px; width: 364px;' colspan='2'>//cuenta</td>" +
                                "<td style='font-size: 9px; width: 485px;' colspan='2'>//cci</td>" +
                                "<td style='font-size: 9px; width: 153px;'>//estado</td></tr>";
            String strCuentas = String.Empty;
            if (lstCuentas.Count > 0)
            {
                foreach (EmpleadoCuenta_Registro item in lstCuentas)
                {
                    strAux = strCuenta;
                    strAux = strAux.Replace("//entidad", item.Banco.Nombre);
                    strAux = strAux.Replace("//cuenta", item.NroCuenta);
                    strAux = strAux.Replace("//cci", item.CCI);
                    strAux = strAux.Replace("//estado", item.Estado.Nombre);
                    
                    strCuentas += strAux;
                }
            }
            else
            {
                strAux = strCuenta;
                strAux = strAux.Replace("//entidad", "&nbsp;");
                strAux = strAux.Replace("//cuenta", "&nbsp;");
                strAux = strAux.Replace("//cci", "&nbsp;");
                strAux = strAux.Replace("//estado", "&nbsp;");
                
                strCuentas += strAux;
            }
            html = html.Replace("//cuentas", strCuentas);

            strAux = String.Empty;
            String strMovimiento = "<tr><td style='font-size: 9px; width: 170px;'>//condicion</td>" +
                                "<td style='font-size: 9px; width: 100px;'>//docu_ing</td>" +
                                "<td style='font-size: 9px; width: 100px;'>//tipo_ing</td>" +
                                "<td style='font-size: 9px; width: 100px;'>//contrato</td>" +
                                "<td style='font-size: 9px; width: 100px;'>//remuneracion</td>" +
                                "<td style='font-size: 9px; width: 100px;'>//inicio</td>" +
                                "<td style='font-size: 9px; width: 100px;'>//fin</td>" +
                                "<td style='font-size: 9px; width: 300px;'>//dependencia</td>" +
                                "<td style='font-size: 9px; width: 300px;'>//cargo</td></tr>";
            String strMovimientos = String.Empty;
            if (lstMovimiento.Count > 0)
            {
                foreach (Empleado_Registro item in lstMovimiento)
                {
                    strAux = strMovimiento;
                    strAux = strAux.Replace("//condicion", item.CondicionLaboral);
                    strAux = strAux.Replace("//docu_ing", item.DocIngreso);
                    strAux = strAux.Replace("//tipo_ing", item.TipoIngreso);
                    strAux = strAux.Replace("//contrato", item.NroContrato);
                    strAux = strAux.Replace("//remuneracion", item.Remuneracion.ToString());
                    strAux = strAux.Replace("//inicio", (item.FechaInicio.HasValue ? item.FechaInicio.Value.ToString("dd/MM/yyyy") : "&nbsp;"));
                    strAux = strAux.Replace("//fin", (item.FechaCese.HasValue ? item.FechaCese.Value.ToString("dd/MM/yyyy") : "&nbsp;"));
                    strAux = strAux.Replace("//dependencia", item.NombreOficina);
                    strAux = strAux.Replace("//cargo", item.NombreCargo);

                    strMovimientos += strAux;
                }
            }
            else
            {
                strAux = strMovimiento;
                strAux = strAux.Replace("//condicion", "&nbsp;");
                strAux = strAux.Replace("//docu_ing", "&nbsp;");
                strAux = strAux.Replace("//tipo_ing", "&nbsp;");
                strAux = strAux.Replace("//contrato", "&nbsp;");
                strAux = strAux.Replace("//remuneracion", "&nbsp;");
                strAux = strAux.Replace("//inicio", "&nbsp;");
                strAux = strAux.Replace("//fin", "&nbsp;");
                strAux = strAux.Replace("//dependencia", "&nbsp;");
                strAux = strAux.Replace("//cargo", "&nbsp;");

                strMovimientos += strAux;
            }
            html = html.Replace("//movimientos", strMovimientos);

            strAux = String.Empty;
            String strEncargatura = "<tr><td style='font-size: 9px; width: 570px;' colspan='2'>//dependencia</td>" +
                                "<td style='font-size: 9px; width: 200px;' colspan='2'>//docu_ini</td>" +
                                "<td style='font-size: 9px; width: 150px;' colspan='2'>//inicio</td>" +
                                "<td style='font-size: 9px; width: 200px;' colspan='2'>//docu_fin</td>" +
                                "<td style='font-size: 9px; width: 150px;' colspan='2'>//fin</td>" +
                                "<td style='font-size: 9px; width: 100px;'>//estado</td></tr>";
            String strEncargaturas = String.Empty;
            if (lstEncargatura.Count > 0)
            {
                foreach (EmpleadoEncargatura_Registro item in lstEncargatura)
                {
                    strAux = strEncargatura;
                    strAux = strAux.Replace("//dependencia", item.Dependencia.Nombre);
                    strAux = strAux.Replace("//docu_ini", item.DocEncargatura);
                    strAux = strAux.Replace("//inicio", (item.FechaIni.HasValue ? item.FechaIni.Value.ToString("dd/MM/yyyy") : "&nbsp;"));
                    strAux = strAux.Replace("//docu_fin", item.DocEncargaturaFin);
                    strAux = strAux.Replace("//fin", (item.FechaFin.HasValue ? item.FechaFin.Value.ToString("dd/MM/yyyy") : "&nbsp;"));
                    strAux = strAux.Replace("//estado", item.Estado.Nombre);

                    strEncargaturas += strAux;
                }
            }
            else
            {
                strAux = strEncargatura;
                strAux = strAux.Replace("//dependencia", "&nbsp;");
                strAux = strAux.Replace("//docu_ini", "&nbsp;");
                strAux = strAux.Replace("//inicio", "&nbsp;");
                strAux = strAux.Replace("//docu_fin", "&nbsp;");
                strAux = strAux.Replace("//fin", "&nbsp;");
                strAux = strAux.Replace("//estado", "&nbsp;");

                strEncargaturas += strAux;
            }
            html = html.Replace("//encargaturas", strEncargaturas);


            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(html, Server.MapPath("~/temp"));
            //foreach (PdfPage page in doc2.Pages)
            //    doc.AddPage(page);


            //string htmlA = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulante/declaracion_derechohabientes.html"));
            //htmlA = htmlA.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            //htmlA = htmlA.Replace("//dia", DateTime.Now.Day.ToString());
            //htmlA = htmlA.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
            //htmlA = htmlA.Replace("//anio", DateTime.Now.Year.ToString());
            //htmlA = htmlA.Replace("//dni", (oFicha.TipoDocumento == 1 ? "D.N.I. N&deg; " : "C.E. N&deg; ") + oFicha.NroDocumento);
            //htmlA = htmlA.Replace("//paterno", oFicha.Paterno);
            //htmlA = htmlA.Replace("//materno", oFicha.Materno);
            //htmlA = htmlA.Replace("//nombres", oFicha.Nombre);
            //htmlA = htmlA.Replace("//nombrecompleto", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
            //htmlA = htmlA.Replace("//email", oFicha.CorreoElectronico.ToUpper());
            //htmlA = htmlA.Replace("//fijo", oFicha.Telefono);
            //htmlA = htmlA.Replace("//celular", oFicha.Celular);
            //htmlA = htmlA.Replace("//domicilio", oFicha.Domicilio);

            //Boolean existeConyuge = false;
            //String strConyuge = "<tr><td style='width: 75%; height: 25px; font-size: 11px;'>_nombre_</td>" +
            //                    "<td style='width: 25%; height: 25px; font-size: 11px;'>_dni_</td></tr>";
            //if (familiares.Count > 0)
            //{
            //    foreach (PostulanteFamiliar_Registro item in familiares)
            //    {
            //        if (item.IdParentesco == 1 || item.IdParentesco == 2)
            //        {
            //            existeConyuge = true;
            //            strConyuge = strConyuge.Replace("_nombre_", item.Nombre);
            //            strConyuge = strConyuge.Replace("_dni_", item.NroDocumento);
            //        }
            //    }
            //}
            //if (!existeConyuge)
            //{
            //    strConyuge = strConyuge.Replace("_nombre_", "&nbsp;");
            //    strConyuge = strConyuge.Replace("_dni_", "&nbsp;");
            //}
            //htmlA = htmlA.Replace("//conyuge", strConyuge);

            //Boolean existeHijo = false;
            //String strHijos = String.Empty;
            //String strHijo = "<tr><td style='width: 75%; height: 25px; font-size: 11px;'>_nombre_</td>" +
            //                    "<td style='width: 25%; height: 25px; font-size: 11px;'>_dni_</td></tr>";
            //if (familiares.Count > 0)
            //{
            //    foreach (PostulanteFamiliar_Registro item in familiares)
            //    {
            //        if (item.IdParentesco == 3)
            //        {
            //            existeHijo = true;
            //            strAux = strHijo;
            //            strAux = strAux.Replace("_nombre_", item.Nombre);
            //            strAux = strAux.Replace("_dni_", item.NroDocumento);
            //            strHijos += strAux;
            //        }
            //    }
            //}
            //if (!existeHijo)
            //{
            //    strAux = strHijo;
            //    strAux = strAux.Replace("_nombre_", "&nbsp;");
            //    strAux = strAux.Replace("_dni_", "&nbsp;");
            //    strHijos += strAux;
            //}
            //htmlA = htmlA.Replace("//hijos", strHijos);


            //SelectPdf.PdfDocument docA = converter.ConvertHtmlString(htmlA, Server.MapPath("~/temp"));
            //foreach (PdfPage page in docA.Pages)
            //    doc.AddPage(page);



            MemoryStream pdfStream = new MemoryStream();
            doc.Save(pdfStream);
            pdfStream.Position = 0;
            doc.Close();

            return pdfStream;
        }

        [Authorize]
        private Boolean SendEmail(Empleado_Registro empleado, String tipo)
        {
            Boolean exitoEnvio = false;
            string NombreCompleto = string.Format("{0} {1} {2}", empleado.Nombre, empleado.Paterno, empleado.Materno);

            if (!String.IsNullOrEmpty(empleado.CorreoElectronicoLaboral)) {
                MailMessage msg = new MailMessage();
                //msg.To.Add(new MailAddress(ConfigurationManager.AppSettings["correonotificausuario"].ToString()));
                msg.To.Add(new MailAddress(empleado.CorreoElectronicoLaboral));
                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CCcorreo1"].ToString()))
                    msg.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["CCcorreo1"].ToString()));

                msg.From = new MailAddress(ConfigurationManager.AppSettings["correo"].ToString());
                if (tipo.IndexOf("A") >= 0)
                    msg.Subject = "Boleta de Pago Adicional " + empleado.NombreMes + " " + empleado.Anio;
                else
                    msg.Subject = "Boleta de Pago " + empleado.NombreMes + " " + empleado.Anio;
                msg.Body = "<img data-placeholder='no' src='https://app.midis.gob.pe/Sis_GesRRHH/Content/img/logo_midis.png' style='width:400px;height:74px;'>" +
                    "<br/><br/><br/>" +
                    "<strong style='font-size:20px;color:#666;font-family:''Helvetica Neue'',Helvetica,Arial,sans-serif;box-sizing:border-box;margin:0'>" + empleado.Nombre + ",</strong>" +
                    "<br/><br/>" +
                    "<span style='font-family:''Helvetica Neue'',Helvetica,Arial,sans-serif;box-sizing:border-box;font-size:14px;margin:0;padding:0 0 20px' valign='top'>Adjuntamos tu boleta laboral para el periodo <strong style='font-family:''Helvetica Neue'',Helvetica,Arial,sans-serif;box-sizing:border-box;font-size:14px;margin:0'>" + empleado.Anio + " " + empleado.NombreMes + "</strong></span>" +
                    "<br/><br/>" +
                    "<span style='font-family:''Helvetica Neue'',Helvetica,Arial,sans-serif;box-sizing:border-box;font-size:22px;width:100%;margin:0;padding:5px 10px 5px 0' align='center' valign='top'>" + "<a href='" + Request.Url.ToString().Substring(0, Request.Url.ToString().Length - 9) + "DescargarArchivo/" + empleado.IdEmpleado + '|' + empleado.Anio + '|' + empleado.Mes + "|1" + "' target='_blank'>Descargar Archivo Aqui !!</a>" + "</span>" +
                    "<br/><br/>" +
                    "Atentamente" +
                    "<br/>" +
                    "\n<b>Oficina General de Recursos Humanos - MIDIS</b>";

                msg.IsBodyHtml = true;

                //Attachment attachment;
                //attachment = new Attachment(Server.MapPath("~/Boletas/DescargarArchivo/" + empleado.IdEmpleado + '|' + empleado.Anio + '|' + empleado.Mes));
                //msg.Attachments.Add(attachment);

                SmtpClient clienteSmtp = new SmtpClient(ConfigurationManager.AppSettings["host"].ToString());
                // Este es el c�digo nuevo

                clienteSmtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["usuarioemail"].ToString(), ConfigurationManager.AppSettings["contraseniaemail"].ToString());
                try
                {
                    //WriteLog("[SendEmail]: Env�o de Notificaci�n usuario MIDIS");
                    clienteSmtp.Send(msg);
                }
                catch (Exception ex)
                {
                    //WriteLog("[SendEmail]: Error en el env�o de notificaci�n usuario MIDIS " + ex.Message);
                    Console.Write(ex.Message);
                    Console.ReadLine();
                }

                exitoEnvio = true;
            }

            return exitoEnvio;
        }
	}
}