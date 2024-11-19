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
using MVCSisRRHH.Models;
using System.Text;
using System.IO;
using System.IO.Compression;
using CrystalDecisions.CrystalReports.Engine;

namespace MVCSisRRHH.Controllers
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
        //        registro.IdUsuarioModificacion = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;
                                
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
        //                        strNoEncontrados.Append("<div style='margin-top: 0px; background-color: rgb(255, 255, 255);'>" + "La boleta laboral del trabajador con Nro documento " + registro.NroDocumento + " se cargó de forma exitosa" + "</div>");
        //                        if (ConfigurationManager.AppSettings["envioMail"].ToString() == "1")
        //                        {
        //                            Empleado_Registro empleado = _empleado_Servicio.ListarEmpleadosBoleta(new Empleado_Registro() { IdEmpleado = registro.IdEmpleado, Anio = registro.Anio, Mes = registro.Mes }).ToList()[0];
        //                            if (!SendEmail(empleado, registro.Tipo))
        //                                strNoEncontrados.Append("<div style='margin-top: 0px; background-color: rgb(255, 255, 255);'>" + "La boleta laboral del trabajador con Nro documento " + registro.NroDocumento + " NO se pudo enviar a su correo electrónico" + "</div>");
        //                        }
        //                    }
        //                    else if (registro.IdEmpleado == -1) 
        //                        strNoEncontrados.Append("<div style='margin-top: 0px; background-color: rgb(255, 255, 255);'>" + "El trabajador con Nro documento " + registro.NroDocumento + " ya tiene una boleta cargada para el periodo seleccionado" + "</div>");
        //                    else
        //                        strNoEncontrados.Append("<div style='margin-top: 0px; background-color: rgb(255, 255, 255);'>" + "El trabajador con Nro documento " + registro.NroDocumento + " no está registrado en la nómina laboral" + "</div>");
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
                msg.Body = "<img data-placeholder='no' src='https://sdv.midis.gob.pe/Sis_Convocatoria/Content/img/logo_midis.png' style='width:400px;height:74px;'>" +
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
                // Este es el código nuevo

                clienteSmtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["usuarioemail"].ToString(), ConfigurationManager.AppSettings["contraseniaemail"].ToString());
                try
                {
                    //WriteLog("[SendEmail]: Envío de Notificación usuario MIDIS");
                    clienteSmtp.Send(msg);
                }
                catch (Exception ex)
                {
                    //WriteLog("[SendEmail]: Error en el envío de notificación usuario MIDIS " + ex.Message);
                    Console.Write(ex.Message);
                    Console.ReadLine();
                }

                exitoEnvio = true;
            }

            return exitoEnvio;
        }
	}
}