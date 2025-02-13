using MIDIS.ORI.Entidades;
using MIDIS.ORI.LogicaNegocio;
using MIDIS.Utiles;
//using MIDIS.UtilesMVC;
//using MIDIS.UtilesWeb;
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
using SevenZip.Compression.LZMA;
using SevenZip.Sdk.Compression.Lzma;
using System.Reflection;

namespace MVCSisGesRRHH.Controllers
{
    public class BoletasController: Controller
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
        public ActionResult Resumen()
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
        public JsonResult ListarPlantillasValidacion(Empleado_Registro peticion)
        {
            List<EmpleadoSisper_Registro> lista = new List<EmpleadoSisper_Registro>();
            List<EmpleadoSisper_Registro> listaSisper = new List<EmpleadoSisper_Registro>();
            if (peticion.IdDependencia == 0) listaSisper = _empleado_Servicio.ListarEmpleadosSisper(peticion).ToList();

            EmpleadoSisper_Registro item = null;
            foreach (EmpleadoSisper_Registro objSisper in listaSisper)
            {
                if (lista.Where(x => x.IdPlanilla == objSisper.IdPlanilla).Where(x => x.TipoPlanilla == objSisper.TipoPlanilla).Count() == 0)
                {
                    item = new EmpleadoSisper_Registro();
                    item.Ingresos = objSisper.Ingresos;
                    item.Descuentos = objSisper.Descuentos;
                    item.Aportes = objSisper.Aportes;
                    item.Anio = objSisper.Anio;
                    item.Mes = objSisper.Mes;
                    item.IdPlanilla = objSisper.IdPlanilla;
                    item.NombrePlanilla = objSisper.NombrePlanilla;
                    item.TipoPlanilla = objSisper.TipoPlanilla;
                    item.NombreTipoPlanilla = objSisper.NombreTipoPlanilla;
                    item.DiasLaborados = 1;

                    lista.Add(item);
                }
                else {
                    Predicate<EmpleadoSisper_Registro> p1 = c => c.IdPlanilla == objSisper.IdPlanilla;
                    Predicate<EmpleadoSisper_Registro> p2 = c => c.TipoPlanilla == objSisper.TipoPlanilla;
                    Predicate<EmpleadoSisper_Registro> combinado = c => (p1(c) && p2(c));
                    EmpleadoSisper_Registro aux = lista.Find(combinado);
                    aux.DiasLaborados += 1;
                    aux.Ingresos += objSisper.Ingresos;
                    aux.Descuentos += objSisper.Descuentos;
                    aux.Aportes += objSisper.Aportes;
                }
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public JsonResult ListarEmpleadosBoleta(Empleado_Registro peticion)
        {
            List<Empleado_Registro> lista = _empleado_Servicio.ListarEmpleadosBoleta(peticion).ToList();
            List<EmpleadoSisper_Registro> listaSisper = new List<EmpleadoSisper_Registro>();
            if (peticion.IdDependencia == 0) listaSisper = _empleado_Servicio.ListarEmpleadosSisper(peticion).ToList();

            foreach (Empleado_Registro obj in lista)
            {
                obj.Contrasena = HttpUtility.UrlEncode(new Crypto().Encriptar(String.Format("{0}|{1}|{2}|{3}", obj.IdEmpleado, obj.Anio, obj.Mes, obj.TipoPlanilla)));
            }

            Empleado_Registro item = null;
            foreach (EmpleadoSisper_Registro objSisper in listaSisper) {
                if (lista.Where(x => x.NroDocumento == objSisper.NroDocumento).Where(x => x.Contrasena != String.Empty).Count() == 0)
                {
                    item = new Empleado_Registro();
                    item.EstadoEnvio = 0;
                    item.Anio = objSisper.Anio.ToString();
                    item.Mes = objSisper.Mes.ToString().PadLeft(2, '0');
                    item.NombreOficina = objSisper.NombreDependencia;
                    item.NroDocumento = objSisper.NroDocumento;
                    item.Nombre = objSisper.Nombres;
                    item.Paterno = objSisper.Apellidos;
                    item.Contrasena = String.Empty;
                    item.Planilla = objSisper.NombrePlanilla;
                    item.TipoPlanilla = objSisper.NombreTipoPlanilla;

                    List<Empleado_Registro> listaEmp = _empleado_Servicio.ListarEmpleados(new Empleado_Request() { NroDocumento = objSisper.NroDocumento, Estado = -1, Nombre = "" }).ToList();
                    if (listaEmp.Exists(x => x.Estado == 1)) {
                        item.Estado = 1;
                        item.CorreoElectronicoLaboral = listaEmp.Where(x => x.Estado == 1).FirstOrDefault().CorreoElectronicoLaboral;
                    }
                    else if (listaEmp.Exists(x => x.Estado == 0)) {
                        item.Estado = 0;
                        item.CorreoElectronicoLaboral = listaEmp.Where(x => x.Estado == 0).FirstOrDefault().CorreoElectronicoLaboral;
                    }
                    else
                        item.Estado = 2;

                    lista.Add(item);
                }
            }

            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = Int32.MaxValue;

            return jsonResult;
        }
        [HttpGet]
        public JsonResult ListarEmpleadosBoletaAnonimo(Empleado_Registro peticion)
        {
            List<Empleado_Registro> lista = _empleado_Servicio.ListarEmpleadosBoleta(peticion).ToList();
            List<EmpleadoSisper_Registro> listaSisper = new List<EmpleadoSisper_Registro>();
            if (peticion.IdDependencia == 0) listaSisper = _empleado_Servicio.ListarEmpleadosSisper(peticion).ToList();

            foreach (Empleado_Registro obj in lista)
            {
                obj.Contrasena = HttpUtility.UrlEncode(new Crypto().Encriptar(String.Format("{0}|{1}|{2}|{3}", obj.IdEmpleado, obj.Anio, obj.Mes, obj.TipoPlanilla)));
            }

            Empleado_Registro item = null;
            foreach (EmpleadoSisper_Registro objSisper in listaSisper)
            {
                if (lista.Where(x => x.NroDocumento == objSisper.NroDocumento).Where(x => x.Contrasena != String.Empty).Count() == 0)
                {
                    item = new Empleado_Registro();
                    item.EstadoEnvio = 0;
                    item.Anio = objSisper.Anio.ToString();
                    item.Mes = objSisper.Mes.ToString().PadLeft(2, '0');
                    item.NombreOficina = objSisper.NombreDependencia;
                    item.NroDocumento = objSisper.NroDocumento;
                    item.Nombre = objSisper.Nombres;
                    item.Paterno = objSisper.Apellidos;
                    item.Contrasena = String.Empty;

                    List<Empleado_Registro> listaEmp = _empleado_Servicio.ListarEmpleados(new Empleado_Request() { NroDocumento = objSisper.NroDocumento, Estado = -1, Nombre = "" }).ToList();
                    if (listaEmp.Exists(x => x.Estado == 1))
                    {
                        item.Estado = 1;
                        item.CorreoElectronicoLaboral = listaEmp.Where(x => x.Estado == 1).FirstOrDefault().CorreoElectronicoLaboral;
                    }
                    else if (listaEmp.Exists(x => x.Estado == 0))
                    {
                        item.Estado = 0;
                        item.CorreoElectronicoLaboral = listaEmp.Where(x => x.Estado == 0).FirstOrDefault().CorreoElectronicoLaboral;
                    }
                    else
                        item.Estado = 2;

                    lista.Add(item);
                }
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
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
        public JsonResult ListarEmpleadosSispla(Empleado_Registro peticion)
        {
            object respuesta = _empleado_Servicio.ListarEmpleadosSispla(peticion);
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
            item = new Anio_Response();
            item.Anio = (DateTime.Now.Year - 2).ToString();
            lista.Add(item);
            item = new Anio_Response();
            item.Anio = (DateTime.Now.Year - 3).ToString();
            lista.Add(item);
            item = new Anio_Response();
            item.Anio = (DateTime.Now.Year - 4).ToString();
            lista.Add(item);
            item = new Anio_Response();
            item.Anio = (DateTime.Now.Year - 5).ToString();
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

        [HttpPost]
        [Authorize]
        public JsonResult Registrar(BoletaCarga_Registro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                StringBuilder strNoEncontrados = new StringBuilder();
                String[] nombre;
                String nameFile = String.Empty;
                for (Int32 j = 0; j < registro.formatos.ToList().Count; j++)
                {
                    HttpPostedFileBase postfile = ((HttpPostedFileBase[])(registro.formatos.ToList())[j])[0];

                    if (postfile.ContentLength > 0)
                    {
                        //string savedFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "signed", Path.GetFileName(postfile.FileName));
                        nameFile = postfile.FileName;
                        nombre = nameFile.Split('-');
                        if (nombre[1] == registro.Anio && nombre[2] == registro.Mes)
                        {
                            registro.NroDocumento = nombre[0];
                            registro.IdPlanilla = nombre[3].Substring(0, nombre[3].Length - 1);
                            registro.TipoPlanilla = nombre[3].Substring(nombre[3].Length - 1);

                            Stream str = postfile.InputStream;
                            BinaryReader Br = new BinaryReader(str);
                            Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                            registro.archivo = FileDet;

                            registro.IdEmpleado = _empleado_Servicio.Insertar(registro);
                            if (registro.IdEmpleado > 0)
                            {
                                strNoEncontrados.Append("<div style='margin-top: 0px; background-color: rgb(255, 255, 255);'>" + "La boleta laboral del trabajador con Nro documento " + registro.NroDocumento + " se carg� de forma exitosa" + "</div>");
                                if (ConfigurationManager.AppSettings["envioMail"].ToString() == "1")
                                {
                                    Empleado_Registro empleado = _empleado_Servicio.ListarEmpleadosBoleta(new Empleado_Registro() { IdEmpleado = registro.IdEmpleado, Anio = registro.Anio, Mes = registro.Mes, Nombre = "" }).ToList()[0];
                                    if (!SendEmail(empleado, registro.IdPlanilla + registro.TipoPlanilla))
                                        strNoEncontrados.Append("<div style='margin-top: 0px; background-color: rgb(255, 255, 255);'>" + "La boleta laboral del trabajador con Nro documento " + registro.NroDocumento + " NO se pudo enviar a su correo electr�nico" + "</div>");
                                }
                            }
                            else if (registro.IdEmpleado == -1)
                                strNoEncontrados.Append("<div style='margin-top: 0px; background-color: rgb(255, 255, 255);'>" + "El trabajador con Nro documento " + registro.NroDocumento + " ya tiene una boleta cargada para el periodo seleccionado" + "</div>");
                            else if (registro.IdEmpleado == -2)
                                strNoEncontrados.Append("<div style='margin-top: 0px; background-color: rgb(255, 255, 255);'>" + "El trabajador con Nro documento " + registro.NroDocumento + " se encuentra inactivo" + "</div>");
                            else
                                strNoEncontrados.Append("<div style='margin-top: 0px; background-color: rgb(255, 255, 255);'>" + "El trabajador con Nro documento " + registro.NroDocumento + " no est� registrado en la n�mina laboral" + "</div>");
                        }
                        else
                            strNoEncontrados.Append("<div style='margin-top: 0px; background-color: rgb(255, 255, 255);'>" + "La boleta laboral del trabajador con Nro documento " + registro.NroDocumento + " no corresponde al periodo seleccionado" + "</div>");
                    }
                }

                if (String.IsNullOrEmpty(strNoEncontrados.ToString()))
                    return Json(true);
                else
                    return Json(new { success = "False", responseText = strNoEncontrados.ToString() });
            }
            catch (System.Data.SqlClient.SqlException es)
            {
                return Json(new { success = "False", responseText = es.Message });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Descarga()
        {
            //PostulanteInformacion_Registro info = new PostulanteInformacion_Registro() { IdPostulante = _postulante_Servicio.GenerarCodigoPostulante(), IdPostulacion = 0, IdConvocatoria = 0 };
            //info.Nacionalidad = "PERUANO";
            //info.IdDeclaraIncompatibilidad = 1;
            //info.IdDeclaraNepotismo = 1;
            //info.IdDeclaraNormas = 1;
            //info.FechaRegistro = DateTime.Now;
            //info.IdUsuarioRegistro = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iCodUsuario); // info.NroDocumento;

            //if (info != null)
            //{
            //    ViewBag.IdPostulante = info.IdPostulante;
            //    ViewBag.IdPostulacion = info.IdPostulacion;
                ViewBag.IdEstado = 1;
            //}

            return View();
        }
        [HttpPost]
        public JsonResult ExportarBoleta(String anio, String mes)
        {
            var fileName = "Boletas_Midis_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".zip";
            string fullPath = Path.Combine(Server.MapPath("~/temp"), fileName);
            using (var memoryStream = new MemoryStream()) //50 * 1024 * 1024
            {
                using (var ziparchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
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
                                lstConceptoIngreso = lstConcepto.Where(x => ((x.TipoConcepto == "0") || (x.TipoConcepto == "1") || (x.TipoConcepto == "3") || (x.TipoConcepto == "4")) &&
                                                                            x.Trabajador == obj.Trabajador &&
                                                                            x.IdPlanilla == obj.IdPlanilla &&
                                                                            x.TipoPlanilla == obj.TipoPlanilla).ToList();
                                lstConceptoDescuento = lstConcepto.Where(x => ((x.TipoConcepto != "0") && (x.TipoConcepto != "1") && (x.TipoConcepto != "3") && (x.TipoConcepto != "4") && (x.TipoConcepto != "9")) &&
                                                                                x.Trabajador == obj.Trabajador &&
                                                                                x.IdPlanilla == obj.IdPlanilla &&
                                                                                x.TipoPlanilla == obj.TipoPlanilla).Where(x => x.TipoConcepto != "9" && x.Trabajador == obj.Trabajador).ToList();
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

                                    Stream oStream = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                                    oStream.Seek(0, SeekOrigin.Begin);

                                    var readmeEntry = ziparchive.CreateEntry(String.Format("{0}-{1}-{2}-{3}{4}-{5}", obj.NroDocumento, anio, mes.PadLeft(2, '0'), obj.IdPlanilla, obj.TipoPlanilla, "01.pdf"));
                                    using (Stream writer = readmeEntry.Open())
                                    {
                                        oStream.CopyTo(writer);
                                    }
                                    oStream.Close();
                                    oStream.Dispose();
                                }
                                catch (Exception)
                                {
                                    report.Dispose();
                                }

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return Json(new { fileName = "", responseText = ex.Message });
                    }
                }

                FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                memoryStream.WriteTo(file);
                file.Close();
            }

            return Json(new { fileName = fileName, responseText = "" });
        }
        //[HttpGet]
        //public ActionResult DescargarBoletaArchivo(string file)
        //{
        //    string fullPath = Path.Combine(Server.MapPath("~/temp"), file);
        //    //return the file for download, this is an Excel so I set the file content type to "application/vnd.ms-excel"
        //    return File(fullPath, "application/zip", file);
        //}

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

        //        String[] funcionarios = { "09392269", "07572557", "10808968", "25705594", "25001443", "09437622", "42720013", "41163027", "07637221", "10736198", "42960883", "41674704", "09062501", "40034504", "06268687", "20051578", "41286746", "07249966", "10107594", "40945950", "42209814", "40886335", "40656312", "41573179", "41290906", "07759242", "44335734", "16727735", "40383798", "10327508", "41359593" };
        //        foreach (EmpleadoSisper_Registro obj in lista)
        //        {
        //            if (funcionarios.Contains(obj.NroDocumento))
        //            {
        //                if (obj.DiasLaborados == 14) obj.DiasLaborados = 15;
        //                if (obj.DiasLaborados == 27) obj.DiasLaborados = 30;

        //                using (ReportDocument report = new ReportDocument())
        //                {
        //                    lstConceptoIngreso = lstConcepto.Where(x => ((x.TipoConcepto == "0") || (x.TipoConcepto == "1")) &&
        //                                                                x.Trabajador == obj.Trabajador &&
        //                                                                x.IdPlanilla == obj.IdPlanilla &&
        //                                                                x.TipoPlanilla == obj.TipoPlanilla).ToList();
        //                    lstConceptoDescuento = lstConcepto.Where(x => ((x.TipoConcepto != "0") && (x.TipoConcepto != "1") && (x.TipoConcepto != "9")) &&
        //                                                                  x.Trabajador == obj.Trabajador &&
        //                                                                  x.IdPlanilla == obj.IdPlanilla &&
        //                                                                  x.TipoPlanilla == obj.TipoPlanilla).ToList();
        //                    lstConceptoAporte = lstConcepto.Where(x => x.TipoConcepto == "9" &&
        //                                                               x.Trabajador == obj.Trabajador &&
        //                                                               x.IdPlanilla == obj.IdPlanilla &&
        //                                                               x.TipoPlanilla == obj.TipoPlanilla).ToList();

        //                    if (lstConceptoIngreso[0].Concepto == "001")
        //                        lstConceptoIngreso[0].Abreviatura = "CONTRAPRE (DU.063-2020)";
        //                    if (lstConceptoIngreso[0].Concepto == "007")
        //                        lstConceptoIngreso[0].Abreviatura = "LEY 30057 (DU.063-2020)";

        //                    try
        //                    {
        //                        report.Load(System.Web.HttpContext.Current.Server.MapPath("~/Reportes/Formato1.rpt"));
        //                        report.FileName = System.Web.HttpContext.Current.Server.MapPath("~/Reportes/Formato1.rpt");
        //                        report.OpenSubreport("lstIngreso").SetDataSource(lstConceptoIngreso);
        //                        report.OpenSubreport("lstDescuento").SetDataSource(lstConceptoDescuento);
        //                        report.OpenSubreport("lstAporte").SetDataSource(lstConceptoAporte);
        //                        report.SetDataSource(lista.Where(x => x.Trabajador == obj.Trabajador &&
        //                                                              x.IdPlanilla == obj.IdPlanilla &&
        //                                                              x.TipoPlanilla == obj.TipoPlanilla).ToList());

        //                        String fileName = Path.Combine(fullPath, String.Format("{0}-{1}-{2}-{3}{4}-{5}", obj.NroDocumento, anio, mes.PadLeft(2, '0'), obj.IdPlanilla, obj.TipoPlanilla, "01.pdf"));
        //                        FileStream file = new FileStream(fileName, FileMode.Create, FileAccess.Write);
        //                        report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat).CopyTo(file);
        //                        file.Close();
        //                    }
        //                    catch (Exception)
        //                    {
        //                        //report.Dispose();
        //                    }
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

        [HttpPost]
        public JsonResult RegistrarSolicitudDescargaBoleta(String dni)
        {
            try
            {
                EmpleadoBoletaSolicitud_Registro objSolicitud = new EmpleadoBoletaSolicitud_Registro() { Documento = dni, Estado = 1 };   
                objSolicitud.Clave = HttpUtility.UrlEncode(new Crypto().Encriptar(String.Format("{0}|{1}", objSolicitud.Documento, DateTime.Now.ToString())));

                List<Empleado_Registro> lista = _empleado_Servicio.ListarEmpleados(new Empleado_Request() { Nombre = "", NroDocumento = dni, Estado = 1 }).ToList();
                if (lista == null)
                {
                    return Json(new { success = "False", responseText = "El n�mero de documento ingresado no existe en la n�mina de trabajadores" });
                }
                else {
                    if (lista.Count > 0)
                    {
                        if (String.IsNullOrEmpty(lista[0].CorreoElectronicoLaboral))
                            return Json(new { success = "False", responseText = "El n�mero de documento ingresado corresponde a un trabajador que no tiene un correo electr�nico ingresado. Comun�quese con la Oficina General de Recursos Humanos" });
                        else {
                            _empleado_Servicio.RegistrarSolicitudBoleta(objSolicitud);
                            SendEmailSolicitudBoleta(objSolicitud, lista[0]);
                            
                            return Json(new { success = "True", responseText = "" });
                        }
                    }
                    else
                        return Json(new { success = "False", responseText = "El n�mero de documento ingresado no existe en la n�mina de trabajadores" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult ValidarSolicitudDescargaBoleta(String id, String clave)
        {
            try
            {
                EmpleadoBoletaSolicitud_Registro objSolicitud = new EmpleadoBoletaSolicitud_Registro() { IdSolicitud = Convert.ToInt32(id), Clave = clave };
                
                String iResultado = _empleado_Servicio.ValidarSolicitudBoleta(objSolicitud);

                //List<Empleado_Registro> lista = _empleado_Servicio.ListarEmpleados(new Empleado_Request() { Nombre = "", NroDocumento = dni, Estado = 1 }).ToList();
                //if (lista == null)
                //{
                //    return Json(new { success = "False", responseText = "El n�mero de documento ingresado no existe en la n�mina de trabajadores" });
                //}
                //else
                //{
                //    if (lista.Count > 0)
                //    {
                //        if (String.IsNullOrEmpty(lista[0].CorreoElectronicoLaboral))
                //            return Json(new { success = "False", responseText = "El n�mero de documento ingresado corresponde a un trabajador que no tiene un correo electr�nico ingresado. Comun�quese con la Oficina General de Recursos Humanos" });
                //        else
                //        {
                //            _empleado_Servicio.RegistrarSolicitudBoleta(objSolicitud);
                //            SendEmailSolicitudBoleta(objSolicitud, lista[0]);

                //            return Json(new { success = "True", responseText = "" });
                //        }
                //    }
                //    else
                //        return Json(new { success = "False", responseText = "El n�mero de documento ingresado no existe en la n�mina de trabajadores" });
                //}

                return Json(new { success = "True", responseText = iResultado });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }


        [HttpPost]
        public JsonResult EliminarTemporal()
        {
            string fullPath = Server.MapPath("~/temp");
            DirectoryInfo dir = null;

            try
            {
                if (!Directory.Exists(fullPath))
                    dir = Directory.CreateDirectory(fullPath);
                else
                    dir = new DirectoryInfo(fullPath);

                dir.Delete(true);

                if (!Directory.Exists(fullPath))
                    dir = Directory.CreateDirectory(fullPath);
            }
            catch (Exception ex)
            {
                return Json(new { fileName = "", responseText = ex.Message });
            }

            return Json(new { fileName = "temp", responseText = "ok" });
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

            //try
            //{
                List<EmpleadoSisper_Registro> lista = _empleado_Servicio.ListarEmpleadosSisper(registro).ToList();
                List<EmpleadoConceptoSisper_Registro> lstConcepto = (List<EmpleadoConceptoSisper_Registro>)_empleado_Servicio.ListarEmpleadoConceptoSisper(registro);

                //lista[0].DiasLaborados = 30;
                //lstConcepto[0].Abreviatura = "CONTRAPRE (DU.063-2020)";


                List<EmpleadoConceptoSisper_Registro> lstConceptoIngreso = lstConcepto.Where(x => ((x.TipoConcepto == "0") || (x.TipoConcepto == "1") || (x.TipoConcepto == "3") || (x.TipoConcepto == "4")) &&
                                                                                                  x.IdPlanilla == planilla &&
                                                                                                  x.TipoPlanilla == tipoplanilla).ToList();
                List<EmpleadoConceptoSisper_Registro> lstConceptoDescuento = lstConcepto.Where(x => ((x.TipoConcepto != "0") && (x.TipoConcepto != "1") && (x.TipoConcepto != "3") && (x.TipoConcepto != "4") && (x.TipoConcepto != "9")) &&
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

                return File(oStream, 
                            "application/pdf", 
                            String.Format("{0}-{1}-{2}-{3}{4}-{5}", 
                                            lista[0].NroDocumento, anio, mes.PadLeft(2, '0'), 
                                            planilla, 
                                            tipoplanilla, "01.pdf"));
            //}
            //catch (Exception ex)
            //{
            //    //Byte[] nulo = null;
            //    //return File(nulo, "application/pdf", "NoEncontrado.pdf");
            //}
        }
        [HttpGet]
        public ActionResult DescargarBoletaSispla(String anio, String mes, String trabajador, String planilla, String tipoplanilla)
        {
            Empleado_Registro registro = new Empleado_Registro();
            registro.Anio = anio;
            registro.Mes = mes;
            registro.CodigoSisper = trabajador;
            registro.TipoPlanilla = tipoplanilla;

            ReportDocument report = new ReportDocument();

            //try
            //{
            List<EmpleadoSisper_Registro> lista = _empleado_Servicio.ListarEmpleadosSispla(registro).ToList();
            List<EmpleadoConceptoSisper_Registro> lstConcepto = (List<EmpleadoConceptoSisper_Registro>)_empleado_Servicio.ListarEmpleadoConceptoSispla(registro);

            //lista[0].DiasLaborados = 30;
            //lstConcepto[0].Abreviatura = "CONTRAPRE (DU.063-2020)";


            List<EmpleadoConceptoSisper_Registro> lstConceptoIngreso = lstConcepto.Where(x => x.TipoConcepto == "1" &&
                                                                                              x.IdPlanilla == planilla &&
                                                                                              x.TipoPlanilla == tipoplanilla).ToList();
            List<EmpleadoConceptoSisper_Registro> lstConceptoDescuento = lstConcepto.Where(x => x.TipoConcepto == "2" &&
                                                                                                x.IdPlanilla == planilla &&
                                                                                                x.TipoPlanilla == tipoplanilla).ToList();
            List<EmpleadoConceptoSisper_Registro> lstConceptoAporte = lstConcepto.Where(x => x.TipoConcepto == "3" &&
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
            //}
            //catch (Exception ex)
            //{
            //    //Byte[] nulo = null;
            //    //return File(nulo, "application/pdf", "NoEncontrado.pdf");
            //}
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ConsultarBoleta(String id)
        {
            String[] arraydata = new Crypto().Desencriptar(id).Split('|');

            // solicitud - empleado - estado
            ViewBag.IdSolicitud = arraydata[0];
            ViewBag.IdTrabajador = arraydata[1];
            ViewBag.IdEstado = arraydata[2];

            return View("Descarga");
        }

        [HttpGet]
        [Authorize]
        public JsonResult TotalBoletas(Empleado_Registro peticion)
        {
            Int32 iBoletas = 0;
            Int32 iBoletasValidas = 0;
            
            iBoletas = _empleado_Servicio.ListarEmpleadosSisper(peticion).Count();
            iBoletasValidas = _empleado_Servicio.TotalBoletasValidas(peticion);

            return Json(new { Boletas = iBoletas, BoletasValidas = iBoletasValidas }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ValidarBoleta(BoletaCarga_Registro obj)
        {
            try
            {
                Int32 iOperacion = 0;
                if (_empleado_Servicio.ValidarExisteBoletaValida(obj) == 0) {
                    Empleado_Registro registro = new Empleado_Registro();
                    registro.Anio = obj.Anio;
                    registro.Mes = obj.Mes;
                    registro.CodigoSisper = obj.Trabajador;

                    using (ReportDocument report = new ReportDocument()) {
                        try
                        {
                            List<EmpleadoSisper_Registro> lista = _empleado_Servicio.ListarEmpleadosSisper(registro).ToList();
                            List<EmpleadoConceptoSisper_Registro> lstConcepto = (List<EmpleadoConceptoSisper_Registro>)_empleado_Servicio.ListarEmpleadoConceptoSisper(registro);

                            List<EmpleadoConceptoSisper_Registro> lstConceptoIngreso = lstConcepto.Where(x => ((x.TipoConcepto == "0") || (x.TipoConcepto == "1") || (x.TipoConcepto == "3") || (x.TipoConcepto == "4")) &&
                                                                                                              x.IdPlanilla == obj.IdPlanilla &&
                                                                                                              x.TipoPlanilla == obj.TipoPlanilla).ToList();
                            List<EmpleadoConceptoSisper_Registro> lstConceptoDescuento = lstConcepto.Where(x => ((x.TipoConcepto != "0") && (x.TipoConcepto != "1") && (x.TipoConcepto != "3") && (x.TipoConcepto != "4") && (x.TipoConcepto != "9")) &&
                                                                                                                x.IdPlanilla == obj.IdPlanilla &&
                                                                                                                x.TipoPlanilla == obj.TipoPlanilla).ToList();
                            List<EmpleadoConceptoSisper_Registro> lstConceptoAporte = lstConcepto.Where(x => x.TipoConcepto == "9" &&
                                                                                                             x.IdPlanilla == obj.IdPlanilla &&
                                                                                                             x.TipoPlanilla == obj.TipoPlanilla).ToList();

                            report.Load(System.Web.HttpContext.Current.Server.MapPath("~/Reportes/Formato1.rpt"));
                            report.FileName = System.Web.HttpContext.Current.Server.MapPath("~/Reportes/Formato1.rpt");
                            report.OpenSubreport("lstIngreso").SetDataSource(lstConceptoIngreso);
                            report.OpenSubreport("lstDescuento").SetDataSource(lstConceptoDescuento);
                            report.OpenSubreport("lstAporte").SetDataSource(lstConceptoAporte);
                            report.SetDataSource(lista.Where(x => x.IdPlanilla == obj.IdPlanilla &&
                                                                  x.TipoPlanilla == obj.TipoPlanilla).ToList());

                            Stream oStream = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                            //oStream.Seek(0, SeekOrigin.Begin);
                            //return File(oStream, "application/pdf", String.Format("{0}-{1}-{2}-{3}{4}-{5}", lista[0].NroDocumento, anio, mes.PadLeft(2, '0'), lista[0].IdPlanilla, lista[0].TipoPlanilla, "01.pdf"));

                            BinaryReader Br = new BinaryReader(oStream);
                            Byte[] FileDet = Br.ReadBytes((Int32)oStream.Length);

                            BoletaCarga_Registro objBoleta = new BoletaCarga_Registro();
                            objBoleta.Anio = obj.Anio;
                            objBoleta.Mes = obj.Mes;
                            objBoleta.NroDocumento = obj.NroDocumento;
                            objBoleta.archivo = FileDet;
                            objBoleta.Trabajador = obj.Trabajador;
                            objBoleta.IdPlanilla = obj.IdPlanilla;
                            objBoleta.TipoPlanilla = obj.TipoPlanilla;
                            objBoleta.Estado = 0;
                            objBoleta.NombreArchivo = String.Format("{0}-{1}-{2}-{3}{4}-{5}", obj.NroDocumento, obj.Anio, obj.Mes.PadLeft(2, '0'), obj.IdPlanilla, obj.TipoPlanilla, "01.pdf");

                            objBoleta.IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;
                            objBoleta.FechaRegistro = DateTime.Now;

                            _empleado_Servicio.InsertarBoletaValida(objBoleta);
                            iOperacion = 1;

                            report.Close();
                            report.Dispose();
                        }
                        catch (Exception ex1)
                        {
                            if (report != null) {
                                report.Close();
                                report.Dispose();
                            }
                            return Json(new { success = "False", responseText = ex1.Message }, JsonRequestBehavior.AllowGet);
                        }
                        
                    }
                }

                return Json(new { success = "True", operacion = iOperacion }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Authorize]
        public JsonResult ListarResumenBoletas(Empleado_Registro peticion)
        {
            object respuesta = _empleado_Servicio.ListarResumenBoletas(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult DescargarArchivo(string id)
        {
            Empleado_Registro peticion = new Empleado_Registro();
            String[] arraydata = new Crypto().Desencriptar(id).Split('|');

            peticion.IdEmpleado = Int32.Parse(arraydata[0].ToString());
            peticion.Anio = arraydata[1].ToString();
            peticion.Mes = arraydata[2].ToString();
            peticion.TipoPlanilla = arraydata[3].ToString();
            peticion.Nombre = "";

            var lista = _empleado_Servicio.ListarEmpleadosBoleta(peticion).Select(p => new { p.IdEmpleado, p.NroDocumento, p.Boleta, p.TipoPlanilla });

            var item = lista.Where(x => x.TipoPlanilla == peticion.TipoPlanilla).SingleOrDefault();
            if (item != null) {
                if (arraydata.Length > 4) {
                    if (arraydata[4].ToString() == "1")
                    {
                        //ACTUALIZAMOS LA FECHA DE RECEPCION EN LA BASE DE DATOS
                        _empleado_Servicio.ActualizarRecepcion(peticion);
                    }
                }
            }

            return File(item.Boleta, "application/pdf", item.NroDocumento + ".pdf");
        }

        [AllowAnonymous]
        public ActionResult DescargarNotificacion(string id)
        {
            Empleado_Registro peticion = new Empleado_Registro();
            String[] arraydata = new Crypto().Desencriptar(id).Split('|');

            peticion.IdEmpleado = Int32.Parse(arraydata[0].ToString());
            peticion.NroDocumento = arraydata[1].ToString();
            peticion.Nombre = "";

            if (arraydata[2].ToString() == "1")
            {
                //ACTUALIZAMOS LA FECHA DE RECEPCION EN LA BASE DE DATOS
                _empleado_Servicio.ActualizarRecepcionNotificacion(peticion);
            }

            //return File(Path.Combine(Server.MapPath("~/docs"), "RSG_028_2019MIDIS.pdf"), "application/pdf", "RISC_MIDIS.pdf");  
            return File(Path.Combine(Server.MapPath("~/docs"), "RJ_010_2020_MIDIS_SG_OGRH.pdf"), "application/pdf", "PlanVigilanciaControlCovid19_MIDIS.pdf");  
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult EnviarEmailBoleta(string contrasena, string email)
        {
            Empleado_Registro peticion = new Empleado_Registro();
            String[] arraydata = new Crypto().Desencriptar(HttpUtility.UrlDecode(contrasena)).Split('|');

            peticion.IdEmpleado = Int32.Parse(arraydata[0].ToString());
            peticion.Anio = arraydata[1].ToString();
            peticion.Mes = arraydata[2].ToString();
            peticion.TipoPlanilla = arraydata[3].ToString();
            peticion.Nombre = "";

            var lista = _empleado_Servicio.ListarEmpleadosBoleta(peticion).Select(p => new { p.IdEmpleado, p.NroDocumento, p.Boleta, p.TipoPlanilla });

            var item = lista.Where(x => x.TipoPlanilla == peticion.TipoPlanilla).SingleOrDefault();
            if (item != null)
            {
                Empleado_Registro empleado = _empleado_Servicio.ListarEmpleadosBoleta(peticion).ToList()[0];
                empleado.CorreoElectronicoLaboral = email;

                SendEmail(empleado, peticion.TipoPlanilla);
            }

            return Json(new { success = "True" });
        }

        //[HttpPost]
        //[AllowAnonymous]
        //public JsonResult EnviarNotificacion()
        //{
        //    Empleado_Request peticion = new Empleado_Request(){ Estado = 1, IdCondicion = 0, Nombre = "" };
        //    T_genm_empleado_LN _empleado_Servicio = new T_genm_empleado_LN();
        //    List <Empleado_Registro> lista = _empleado_Servicio.ListarEmpleados(peticion).ToList();

        //    String documentos_envio = ConfigurationManager.AppSettings["DOCUMENTO_ENVIO"].ToString();
        //    foreach (Empleado_Registro obj in lista)
        //    {
        //        //obj.Contrasena = HttpUtility.UrlEncode(new Crypto().Encriptar(String.Format("{0}|{1}", obj.IdEmpleado, obj.NroDocumento)));
        //        if (documentos_envio == "0"){
        //            _empleado_Servicio.InsertarNotificacion(new BoletaCarga_Registro(){ IdEmpleado = obj.IdEmpleado });
        //            SendEmailNotificacion(obj);
        //        }   
        //        else if(documentos_envio.IndexOf(obj.NroDocumento) >= 0) {
        //            _empleado_Servicio.InsertarNotificacion(new BoletaCarga_Registro() { IdEmpleado = obj.IdEmpleado });
        //            SendEmailNotificacion(obj);
        //        }   
        //    }

        //    return Json(new { success = "True" });
        //}
        [HttpPost]
        [AllowAnonymous]
        public JsonResult EnviarNotificacion()
        {
            Empleado_Request peticion = new Empleado_Request() { Estado = 1, IdCondicion = 0, Nombre = "" };
            T_genm_empleado_LN _empleado_Servicio = new T_genm_empleado_LN();
            List<Empleado_Registro> lista = _empleado_Servicio.ListarParticipantesCertificadoPrueba(peticion).ToList();

            foreach (Empleado_Registro obj in lista)
            {
                SendEmailNotificacion(obj);
            }

            return Json(new { success = "True" });
        }
        [HttpPost]
        public JsonResult CrearZipFirma(Empleado_Registro peticion)
        {
            String rpta = String.Empty;
            
            List<Empleado_Registro> list = new List<Empleado_Registro>();
            IEnumerable<Empleado_Registro> listado = _empleado_Servicio.ListarEmpleadosValidacionBoleta(peticion);

            var directory = peticion.Anio + peticion.Mes.PadLeft(2, '0') + "_" + DateTime.Now.ToString("yyyyMMddHHmm");
            string fullPath = Path.Combine(Server.MapPath("~/temp"), directory);
            DirectoryInfo dir = null;
            if (!Directory.Exists(fullPath)) dir = Directory.CreateDirectory(fullPath);
            //String fileName = Path.Combine(fullPath, String.Format("{0}{1}", , ".7z"));

            String pathFile = String.Empty;
            foreach (Empleado_Registro item in listado)
            {
                pathFile = Path.Combine(fullPath, item.NombreArchivo);
                System.IO.File.WriteAllBytes(pathFile, item.Boleta);
            }

            //string sourceCodeFolder = @"E:\Proyectos\BOLETAS\MIDIS.BOLETAS\MVCBoletas\temp\Boletas_Midis_201909270709";
            string targetFolder = Server.MapPath("~/temp");
            if (System.IO.Directory.Exists(targetFolder))
            {
                // Specify where 7z.dll DLL is located
                //SevenZip.SevenZipCompressor.SetLibraryPath(@"C:\Program Files\7-Zip\7z.dll");
                //SevenZip.SevenZipCompressor.SetLibraryPath(@"C:\Users\apoyo3_dpip\Downloads\7z1900-extra\7za.dll");
                var path = Path.Combine(Server.MapPath("~/util"), Environment.Is64BitProcess ? "x64" : "x86", "7z.dll");
                //SevenZip.SevenZipBase.SetLibraryPath(path);

                //SevenZip.SevenZipCompressor.SetLibraryPath(@"C:\Users\apoyo3_dpip\Downloads\SevenZipSharp.Interop-master\SevenZipSharp.Interop-master\src\build\x86\7z.dll");
                SevenZip.SevenZipCompressor.SetLibraryPath(path);

                SevenZip.SevenZipCompressor sevenZipCompressor = new SevenZip.SevenZipCompressor();
                //sevenZipCompressor.CompressionLevel = SevenZip.CompressionLevel.Ultra;
                //sevenZipCompressor.CompressionMethod = SevenZip.CompressionMethod.Lzma;
                // Compress the directory and save the file in a yyyyMMdd_project-files.7z format (eg. 20141024_project-files.7z

                sevenZipCompressor.CompressionMode = SevenZip.CompressionMode.Create;
                sevenZipCompressor.TempFolderPath = Path.GetTempPath();
                sevenZipCompressor.ArchiveFormat = SevenZip.OutArchiveFormat.SevenZip;
                //sevenZipCompressor.CompressDirectory(compressFrom, compressTo);

                rpta = String.Concat(directory, ".7z");
                sevenZipCompressor.CompressDirectory(fullPath, Path.Combine(targetFolder, String.Concat(directory, ".7z")));
                
                // ELIMINAMOS LAS BOLETAS VALIDAS CREADAS PARA LA FIRMA
                //Directory.Delete(fullPath);
                //sevenZipCompressor.CompressionFinished += sevenZipCompressor_CompressionFinished; 
            }

            return Json(new { success = "True", responseText = rpta }, JsonRequestBehavior.AllowGet);
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

                String urlCrypto = HttpUtility.UrlEncode(new Crypto().Encriptar(empleado.IdEmpleado + "|" + empleado.Anio + "|" + empleado.Mes + "|" + tipo + "|1"));

                msg.Body = "<div class=WordSection1 style='width: 650px;border-style: solid;border-width: 2px;border-color: #6773b9;padding: 30px;border-radius: 5px;'>" +
"<img data-placeholder='no' src='https://app.midis.gob.pe/Sis_GesRRHH/Content/img/logo_midis.png' style='width:300px;height:55px;'>" +
"<br><br>" +
"<p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;text-align:" +
"justify;line-height:normal'><span lang=ES style='font-family:'Arial Black,sans-serif'>Hola <span style='color:red'>" + empleado.Nombre + "!</span><o:p></o:p></span></p>" +

"<p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;text-align:" +
"justify;line-height:normal'><span lang=ES style='font-family:'Arial Black,sans-serif'>Mediante el presente remitimos tu BOLETA DE PAGO correspondiente al mes de <span " +
"style='color:red'>" + empleado.NombreMes + " " + empleado.Anio + "</span>.<br> Para visualizarla e imprimirla <u><span" +
"style='color:red'>" + "<a style='color:red' href='" + ConfigurationManager.AppSettings["URL_MAIL"].ToString() + "DescargarArchivo/?id=" + urlCrypto + "' target='_blank'> Haz click aqui</a>" + "</span></u>.<o:p></o:p></span></p>" +

"<p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;text-align:" +
"justify;line-height:normal'><span lang=ES style='font-family:'Arial Black,sans-serif'>Si tienes alguna duda o consulta sobre tu boleta escribe a </span><span lang=ES><a" +
"href='mailto:jflores@midis.gob.pe'><span style='font-family:'Arial Black,sans-serif';" +
"color:windowtext'>jflores@midis.gob.pe</span></a></span><span lang=ES" +
"style='font-family:'Arial Black,sans-serif'> o llama al anexo <span style='color:red'>1511 </span>y gustosos te atenderemos.<o:p></o:p></span></p>" +

"<p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;text-align:justify;line-height:normal'><span lang=ES style='font-family:'Arial Black,sans-serif'>Recuerda guardar tu boleta en un archivo personal.<o:p></o:p></span></p>" +

"<p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;text-align:justify;line-height:normal'><span lang=ES style='font-family:'Arial Black,sans-serif'><o:p>&nbsp;</o:p></span></p>" +

"<p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;text-align:justify;line-height:normal'><span lang=ES style='font-family:'Arial Black,sans-serif'>Atentamente,<o:p></o:p></span></p>" +

"<p class=MsoNormal style='margin-top:0cm;margin-right:0cm;margin-bottom:0cm;" +
"margin-left:141.6pt;margin-bottom:.0001pt;text-align:justify;text-indent:35.4pt;" +
"line-height:normal'><span lang=ES style='font-family:'Arial Black,sans-serif'><o:p>&nbsp;</o:p></span></p>" +

"<p class=MsoNormal style='margin-top:0cm;margin-right:0cm;margin-bottom:0cm;" +
"margin-left:141.6pt;margin-bottom:.0001pt;text-align:justify;text-indent:35.4pt;" +
"line-height:normal'><span lang=ES style='font-family:'Arial Black,sans-serif'>Oficina General de Recursos Humanos<o:p></o:p></span></p>" +

"</div>";
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

        //        [AllowAnonymous]
        //        private Boolean SendEmailNotificacion(Empleado_Registro empleado)
        //        {
        //            Boolean exitoEnvio = false;
        //            string NombreCompleto = string.Format("{0} {1} {2}", empleado.Nombre, empleado.Paterno, empleado.Materno);

        //            if (!String.IsNullOrEmpty(empleado.CorreoElectronicoLaboral))
        //            {
        //                MailMessage msg = new MailMessage();
        //                msg.To.Add(new MailAddress(empleado.CorreoElectronicoLaboral));
        //                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CCcorreo1"].ToString()))
        //                    msg.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["CCcorreo1"].ToString()));

        //                msg.From = new MailAddress(ConfigurationManager.AppSettings["correo"].ToString());
        //                msg.Subject = "Inf�rmate sobre el nuevo Reglamento Interno de los/las Servidores/as Civiles del MIDIS";

        //                String urlCrypto = HttpUtility.UrlEncode(new Crypto().Encriptar(empleado.IdEmpleado + "|" + empleado.NroDocumento + "|1"));

        //                msg.Body = "<div class=WordSection1 style='width: 650px;border-style: solid;border-width: 2px;border-color: #6773b9;padding: 30px;border-radius: 5px;'>" +
        //"<img data-placeholder='no' src='https://app.midis.gob.pe/Sis_GesRRHH/Content/img/logo_midis.png' style='width:300px;height:55px;'>" +
        //"<br><br>" +
        //"<p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;text-align:" +
        //"justify;line-height:normal'><span lang=ES style='font-family:'Arial Black,sans-serif'>Hola <span style='color:red'>" + empleado.Nombre + "!</span><o:p></o:p></span></p>" +

        ////"<p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;text-align:justify;line-height:normal'><span lang=ES style='font-family:'Arial Black,sans-serif'>Mediante el presente ponemos a tu disposici�n el nuevo Reglamento Interno de los/las Servidores/as Civiles del Ministerio de Desarrollo e Inclusi�n Social.<br> "  + 
        //"<p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;text-align:justify;line-height:normal'><span lang=ES style='font-family:'Arial Black,sans-serif'>�Sabias que?<br>El RIS tiene por objeto normar las relaciones laborales y condiciones a las que se someten cada una de las partes  de la relaci�n laboral que se tiene con el MIDIS, propiciando, fomentando y manteniendo la armon�a en las relaciones laborales, orientada al logro de los objetivos institucionales y la mejora de los servicios brindados a la ciudadan�a.<br><br>" + 
        ////"Para visualizarla e imprimirla <u><span" +
        ////"style='color:red'>" + "<a style='color:red' href='" + ConfigurationManager.AppSettings["URL_MAIL"].ToString() + "DescargarNotificacion/?id=" + urlCrypto + "' target='_blank'> Haz click aqui</a>" + "</span></u>.<o:p></o:p></span></p>" +
        //"Ponemos a tu disposici�n el Nuevo Reglamento Interno de los/las Servidores/as Civiles del MIDIS <u><span" +
        //"style='color:red'>" + "<a style='color:red' href='" + ConfigurationManager.AppSettings["URL_MAIL"].ToString() + "DescargarNotificacion/?id=" + urlCrypto + "' target='_blank'> Desc�rgalo aqui</a>" + "</span></u>.<o:p></o:p></span></p>" +

        //"<p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;text-align:justify;line-height:normal'><span lang=ES style='font-family:'Arial Black,sans-serif'><o:p>&nbsp;</o:p></span></p>" +

        //"<p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;text-align:justify;line-height:normal'><span lang=ES style='font-family:'Arial Black,sans-serif'>Atentamente,<o:p></o:p></span></p>" +

        //"<p class=MsoNormal style='margin-top:0cm;margin-right:0cm;margin-bottom:0cm;" +
        //"margin-left:141.6pt;margin-bottom:.0001pt;text-align:justify;text-indent:35.4pt;" +
        //"line-height:normal'><span lang=ES style='font-family:'Arial Black,sans-serif'><o:p>&nbsp;</o:p></span></p>" +

        //"<p class=MsoNormal style='margin-top:0cm;margin-right:0cm;margin-bottom:0cm;" +
        //"margin-left:141.6pt;margin-bottom:.0001pt;text-align:justify;text-indent:35.4pt;" +
        //"line-height:normal'><span lang=ES style='font-family:'Arial Black,sans-serif'>Oficina General de Recursos Humanos<o:p></o:p></span></p>" +

        //"</div>";
        //                msg.IsBodyHtml = true;

        //                SmtpClient clienteSmtp = new SmtpClient(ConfigurationManager.AppSettings["host"].ToString());
        //                clienteSmtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["usuarioemail"].ToString(), ConfigurationManager.AppSettings["contraseniaemail"].ToString());
        //                try
        //                {
        //                    clienteSmtp.Send(msg);
        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.Write(ex.Message);
        //                    Console.ReadLine();
        //                }

        //                exitoEnvio = true;
        //            }

        //            return exitoEnvio;
        //        }
        //        [AllowAnonymous]
        //        private Boolean SendEmailNotificacion(Empleado_Registro empleado)
        //        {
        //            Boolean exitoEnvio = false;
        //            string NombreCompleto = string.Format("{0} {1} {2}", empleado.Nombre, empleado.Paterno, empleado.Materno);

        //            if (!String.IsNullOrEmpty(empleado.CorreoElectronicoLaboral))
        //            {
        //                MailMessage msg = new MailMessage();
        //                msg.To.Add(new MailAddress(empleado.CorreoElectronicoLaboral));
        //                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CCcorreo1"].ToString()))
        //                    msg.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["CCcorreo1"].ToString()));

        //                msg.From = new MailAddress(ConfigurationManager.AppSettings["correo"].ToString());
        //                msg.Subject = "Inf�rmate sobre Plan para Vigilancia, prevenci�n y control de Covid-19 en el trabajo del MIDIS";

        //                String urlCrypto = HttpUtility.UrlEncode(new Crypto().Encriptar(empleado.IdEmpleado + "|" + empleado.NroDocumento + "|1"));

        //                msg.Body = "<div class=WordSection1 style='width: 650px;border-style: solid;border-width: 2px;border-color: #6773b9;padding: 30px;border-radius: 5px;'>" +
        //"<img data-placeholder='no' src='https://app.midis.gob.pe/Sis_GesRRHH/Content/img/logo_midis.png' style='width:300px;height:55px;'>" +
        //"<br><br>" +
        //"<p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;text-align:" +
        //"justify;line-height:normal'><span lang=ES style='font-family:'Arial Black,sans-serif'>Hola <span style='color:red'>" + empleado.Nombre + "!</span><o:p></o:p></span></p>" +

        ////"<p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;text-align:justify;line-height:normal'><span lang=ES style='font-family:'Arial Black,sans-serif'>Mediante el presente ponemos a tu disposici�n el nuevo Reglamento Interno de los/las Servidores/as Civiles del Ministerio de Desarrollo e Inclusi�n Social.<br> "  + 
        //"<p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;text-align:justify;line-height:normal'><span lang=ES style='font-family:'Arial Black,sans-serif'>�Sabes cu�les son las actividades, acciones e intervenciones que aseguren la vigilancia, prevenci�n y control de COVID-19 en el MIDIS?<br><br>Conoce m�s sobre nuestro Plan para la Vigilancia, prevenci�n y control de COVID-19 en el trabajo del Ministerio de Desarrollo e Inclusi�n Social MIDIS.<br><br>" +
        //                    //"Para visualizarla e imprimirla <u><span" +
        //                    //"style='color:red'>" + "<a style='color:red' href='" + ConfigurationManager.AppSettings["URL_MAIL"].ToString() + "DescargarNotificacion/?id=" + urlCrypto + "' target='_blank'> Haz click aqui</a>" + "</span></u>.<o:p></o:p></span></p>" +
        ////"<u><span style='color:red'>" + "<a style='color:red' href='" + ConfigurationManager.AppSettings["URL_MAIL"].ToString() + "DescargarNotificacion/?id=" + urlCrypto + "' target='_blank'> Desc�rgalo aqui</a>" + "</span></u>.<o:p></o:p></span></p>" +
        //"<u><span style='color:red'>" + "<a style='color:red' href='https://cdn.www.gob.pe/uploads/document/file/733522/RJ_010_2020_MIDIS_SG_OGRH.pdf' target='_blank'> Desc�rgalo aqui</a>" + "</span></u>.<o:p></o:p></span></p>" +

        //"<p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;text-align:justify;line-height:normal'><span lang=ES style='font-family:'Arial Black,sans-serif'><o:p>&nbsp;</o:p></span></p>" +

        //"<p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;text-align:justify;line-height:normal'><span lang=ES style='font-family:'Arial Black,sans-serif'>Atentamente,<o:p></o:p></span></p>" +

        //"<p class=MsoNormal style='margin-top:0cm;margin-right:0cm;margin-bottom:0cm;" +
        //"margin-left:141.6pt;margin-bottom:.0001pt;text-align:justify;text-indent:35.4pt;" +
        //"line-height:normal'><span lang=ES style='font-family:'Arial Black,sans-serif'><o:p>&nbsp;</o:p></span></p>" +

        //"<p class=MsoNormal style='margin-top:0cm;margin-right:0cm;margin-bottom:0cm;" +
        //"margin-left:141.6pt;margin-bottom:.0001pt;text-align:justify;text-indent:35.4pt;" +
        //"line-height:normal'><span lang=ES style='font-family:'Arial Black,sans-serif'>Oficina General de Recursos Humanos<o:p></o:p></span></p>" +

        //"</div>";
        //                msg.IsBodyHtml = true;

        //                SmtpClient clienteSmtp = new SmtpClient(ConfigurationManager.AppSettings["host"].ToString());
        //                clienteSmtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["usuarioemail"].ToString(), ConfigurationManager.AppSettings["contraseniaemail"].ToString());
        //                try
        //                {
        //                    clienteSmtp.Send(msg);
        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.Write(ex.Message);
        //                    Console.ReadLine();
        //                }

        //                exitoEnvio = true;
        //            }

        //            return exitoEnvio;
        //        }

        [AllowAnonymous]
        private Boolean SendEmailNotificacion(Empleado_Registro empleado)
        {
            Boolean exitoEnvio = false;
            string NombreCompleto = string.Format("{0} {1} {2}", empleado.Nombre, empleado.Paterno, empleado.Materno);

            if (!String.IsNullOrEmpty(empleado.CorreoElectronicoLaboral))
            {
                MailMessage msg = new MailMessage();
                //msg.To.Add(new MailAddress("kmiota@midis.gob.pe"));
                msg.To.Add(new MailAddress(empleado.CorreoElectronicoLaboral));
                //if (!String.IsNullOrEmpty(empleado.CorreoElectronico))
                //    msg.Bcc.Add(new MailAddress(empleado.CorreoElectronico));

                msg.From = new MailAddress(ConfigurationManager.AppSettings["correo"].ToString());
                msg.Subject = "Certificaci�n del curso virtual: 'La alimentaci�n escolar como estrategia educativa para una vida saludable'";

                msg.Body = "<div class=WordSection1 style='width: 650px;border-style: solid;border-width: 2px;border-color: #6773b9;padding: 30px;border-radius: 5px;'>" +
//"<img data-placeholder='no' src='https://app.midis.gob.pe/Sis_GesRRHH/Content/img/logo_midis.png' style='width:300px;height:55px;'>" +
//"<br><br>" +
"<p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;text-align:" +
"justify;line-height:normal'><span lang=ES style='font-family:'Arial Black,sans-serif'>Hola <span style='color:red'>" + empleado.Nombre + "!</span><o:p></o:p></span><br><br></p>" +

//"<p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;text-align:justify;line-height:normal'><span lang=ES style='font-family:'Arial Black,sans-serif'>Mediante el presente ponemos a tu disposici�n el nuevo Reglamento Interno de los/las Servidores/as Civiles del Ministerio de Desarrollo e Inclusi�n Social.<br> "  + 
"<p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;text-align:justify;line-height:normal'><span lang=ES style='font-family:'Arial Black,sans-serif'>Recibe un cordial saludo del Viceministro de Prestaciones Sociales, William Contreras, quien te agradece por tu participaci&oacute;n en el&nbsp;<strong>Curso:&nbsp;</strong><strong>&lsquo;</strong><strong>Alimentaci&oacute;n escolar como estrategia educativa para una vida saludable</strong><strong>&rsquo;</strong>, implementado por&nbsp;el Proyecto Consolidaci&oacute;n de Programas de Alimentaci&oacute;n Escolar en Am&eacute;rica Latina y el Caribe, y desarrollado en el marco de la Cooperaci&oacute;n T&eacute;cnica Internacional Brasil-FAO y el MIDIS.<br><br>Asimismo, deseamos felicitarte por haber culminado y aprobado dicho Curso, por lo que alcanzamos el certificado del mismo. Estamos seguros que este aprendizaje te ayudar&aacute; en tu formaci&oacute;n profesional y en tu aporte a la mejora de nuestras acciones en favor de una mejor alimentaci&oacute;n escolar a nivel nacional.<br><br>" +

"<p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;text-align:justify;line-height:normal'><span lang=ES style='font-family:'Arial Black,sans-serif'><o:p>&nbsp;</o:p></span></p>" +
"<p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;text-align:justify;line-height:normal'><span lang=ES style='font-family:'Arial Black,sans-serif'>Atentamente,<o:p></o:p></span></p>" +

"<p class=MsoNormal style='margin-top:0cm;margin-right:0cm;margin-bottom:0cm;" +
"margin-left:141.6pt;margin-bottom:.0001pt;text-align:justify;text-indent:35.4pt;" +
"line-height:normal'><span lang=ES style='font-family:'Arial Black,sans-serif'><o:p>&nbsp;</o:p></span></p>" +

"<p class=MsoNormal style='margin-top:0cm;margin-right:0cm;margin-bottom:0cm;" +
"margin-left:141.6pt;margin-bottom:.0001pt;text-align:justify;text-indent:35.4pt;" +
"line-height:normal'><span lang=ES style='font-family:'Arial Black,sans-serif'>Ministerio de Desarrollo e Inclusi�n Social<o:p></o:p></span></p>" +

"</div>";
                msg.IsBodyHtml = true;
                
                
                try
                {
                    String archivo = Path.Combine("D:\\Temp\\Certificados\\", empleado.Nombre + ".pdf");
                    if (!System.IO.File.Exists(archivo))
                        throw new Exception("No se encuentra el certificado");

                    msg.Attachments.Add(new Attachment(archivo));

                    SmtpClient clienteSmtp = new SmtpClient(ConfigurationManager.AppSettings["host"].ToString());
                    clienteSmtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["usuarioemail"].ToString(), ConfigurationManager.AppSettings["contraseniaemail"].ToString());
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
        private Boolean SendEmailSolicitudBoleta(EmpleadoBoletaSolicitud_Registro solicitud, Empleado_Registro empleado)
        {
            Boolean exitoEnvio = false;
            String html = String.Empty;
            string NombreCompleto = string.Format("{0} {1} {2}", empleado.Nombre, empleado.Paterno, empleado.Materno);

            if (!String.IsNullOrEmpty(empleado.CorreoElectronicoLaboral))
            {
                String urlCrypto = HttpUtility.UrlEncode(new Crypto().Encriptar(solicitud.IdSolicitud + "|" + empleado.IdEmpleado + "|2"));

                html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Boleta/EnvioRegistroSolicitud.txt"));
                html = html.Replace("_NOMBRE_", empleado.Nombre);
                html = html.Replace("_CLAVE_", solicitud.Clave);
                html = html.Replace("_URLVALIDACION_", ConfigurationManager.AppSettings["URL_MAIL"].ToString() + "ConsultarBoleta/?id=" + urlCrypto); //HttpUtility.UrlEncode(

                MailMessage msg = new MailMessage();
                msg.To.Add(new MailAddress(empleado.CorreoElectronicoLaboral));
                msg.From = new MailAddress(ConfigurationManager.AppSettings["correo"].ToString());
                msg.Subject = "Registro de solicitud para consulta de boletas de trabajo del MIDIS";
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

        #region Firma Digital
        public string getArguments(String id)
        {
            //nombre del archivo
            string nombre = "25400040-2019-01-1N-01.7z";
            //String nombre = "FORM01" + id.Replace("-","") + ".pdf";
            //String nombre = "FORM01" + id.Replace("-", "") + ".7z";
            return nombre;
        }

        public string postArgumentsLotePDF(string type, string documentName)
        {
            string rpta = "";
            StringBuilder sb = new StringBuilder();
            String url = ConfigurationManager.AppSettings["SERVER_PATH"];
            
            if (type == "W")
            {
                sb.Append("{");
                //
                sb.Append("\"app\":\"pcx\",");
                //
                sb.Append("\"mode\":\"lot-p\",");
                //
                sb.Append("\"clientId\":\"");
                sb.Append(ConfigurationManager.AppSettings["CLIENTID"]);
                sb.Append("\",");
                //
                sb.Append("\"clientSecret\":\"");
                sb.Append(ConfigurationManager.AppSettings["CLIENTSECRET"]);
                sb.Append("\",");
                //
                sb.Append("\"idFile\":\"100\",");
                //
                sb.Append("\"type\":\"W\",");
                //
                sb.Append("\"protocol\":\"T\",");
                //
                sb.Append("\"fileDownloadUrl\":\"");
                sb.Append(String.Format("{0}{1}{2}", url, "temp/", documentName));
                //sb.Append("temp/firmado/20191211_project-files.7z");  //Archivo a leer/firmar
                sb.Append("\",");
                //
                sb.Append("\"fileUploadUrl\":\"");
                sb.Append(url);
                sb.Append(ConfigurationManager.AppSettings["FILEUPLOADURL"]);
                sb.Append("\",");
                //
                sb.Append(String.Format("\"contentFile\":\"{0}\",", documentName));
                sb.Append("\"isSignatureVisible\":\"true\",");
                //
                sb.Append("\"dcfilter\":\".*FIR.*|.*FAU.*\",");
                sb.Append("\"stampAppearanceId\":\"0\",");
                sb.Append(String.Format("\"fileDownloadLogoUrl\":\"{0}{1}{2}", url, "Content/img/iLogo1.png", "\","));
                sb.Append(String.Format("\"fileDownloadStampUrl\":\"{0}{1}{2}", url, "Content/img/iFirma1.png", "\","));
                sb.Append("\"pageNumber\":\"0\",");
                sb.Append("\"posx\":\"20\",");
                sb.Append("\"posy\":\"400\",");
                sb.Append("\"fontSize\":\"7\",");
                sb.Append("\"reason\":\"");
                sb.Append(ConfigurationManager.AppSettings["GLOSA"]);
                sb.Append("\",");
                sb.Append("\"signatureLevel\":\"0\",");
                sb.Append("\"maxFileSize\":\"26214400\"");

                sb.Append("}");
            }
            byte[] byt = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            rpta = Convert.ToBase64String(byt);
            return rpta;
        }

        //colocarlo como metodo anonimo a ver como funciona 
        // primero probaremos con la firma de Bryand para usuarios de OGTI y luego con todos el personal de midis 
        [AllowAnonymous]
        public void upload()
        {
            string filePathExtractor = String.Empty;
            try
            {
                string[] claves = Request.Form.AllKeys;
                string pathUpload = Server.MapPath("~/temp/firmado/");
                string idFile;
                string fileName;
                string filePath = String.Empty;
                int cFile = 0;
                if (!System.IO.Directory.Exists(pathUpload))
                    System.IO.Directory.CreateDirectory(pathUpload);
                foreach (string clave in claves)
                {
                    idFile = clave;
                    fileName = Path.GetFileName(Request.Files[cFile].FileName);
                    filePath = Path.Combine(pathUpload, fileName);
                    filePathExtractor = Path.Combine(pathUpload, Path.GetFileNameWithoutExtension(Request.Files[cFile].FileName));
                    if (System.IO.File.Exists(filePath)) System.IO.File.Delete(filePath);
                    Request.Files[cFile].SaveAs(filePath);
                    cFile++;
                }
                Response.StatusCode = 200;

                // LUEGO DE LA FIRMA MASIVA SE HACE LAS NOTIFICACIONES
                if (!Directory.Exists(filePathExtractor)) Directory.CreateDirectory(filePathExtractor);
                if (!System.IO.File.Exists(Path.Combine(filePathExtractor + "\\log.txt"))) {
                    using (FileStream fs = System.IO.File.Create(Path.Combine(filePathExtractor + "\\log.txt")))
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes(String.Format("{0}-{1}", DateTime.Now.ToString(), "Inicio de descompresi�n del archivo 7z"));
                        fs.Write(info, 0, info.Length);
                    }
                }

                using (StreamWriter sw = System.IO.File.AppendText(Path.Combine(filePathExtractor + "\\log.txt")))
                {
                    try
                    {
                        sw.WriteLine(String.Format("{0}-{1}", DateTime.Now.ToString(), SevenZip.SevenZipExtractor.CurrentLibraryFeatures.ToString()));
                    }
                    catch (Exception)
                    {
                        sw.WriteLine(String.Format("{0}-{1}", DateTime.Now.ToString(), "Error prueba"));
                    }
                }
                var path = Path.Combine(Server.MapPath("~/util"), Environment.Is64BitProcess ? "x64" : "x86", "7z.dll");
                SevenZip.SevenZipExtractor.SetLibraryPath(path);

                SevenZip.SevenZipExtractor sevenZipExtractor = new SevenZip.SevenZipExtractor(filePath);
                using (StreamWriter sw = System.IO.File.AppendText(Path.Combine(filePathExtractor + "\\log.txt")))
                {
                    sw.WriteLine(String.Format("{0}-{1}", DateTime.Now.ToString(), "Clase archivo 7z creada"));
                }
                sevenZipExtractor.ExtractArchive(filePathExtractor);
                using (StreamWriter sw = System.IO.File.AppendText(Path.Combine(filePathExtractor + "\\log.txt")))
                {
                    sw.WriteLine(String.Format("{0}-{1}", DateTime.Now.ToString(), "Archivos extraidos con �xito"));
                }
                
                String[] nombre;
                String nameFile = String.Empty;
                DirectoryInfo firmados = new System.IO.DirectoryInfo(filePathExtractor);
                BoletaCarga_Registro registro = null;
                foreach ( FileInfo obj in firmados.GetFiles()){
                    if (obj.Extension == ".pdf")
                    {
                        using (StreamWriter sw = System.IO.File.AppendText(Path.Combine(filePathExtractor + "\\log.txt")))
                        {
                            sw.WriteLine(String.Format("{0}-{1}-{2}", DateTime.Now.ToString(), "Envio de boleta empleado: ", obj.Name));
                        }

                        nameFile = obj.Name;
                        nombre = nameFile.Split('-');

                        registro = new BoletaCarga_Registro();
                        registro.NroDocumento = nombre[0];
                        registro.Anio = nombre[1];
                        registro.Mes = nombre[2];
                        registro.IdPlanilla = nombre[3].Substring(0, nombre[3].Length - 1);
                        registro.TipoPlanilla = nombre[3].Substring(nombre[3].Length - 1);

                        Stream str = obj.OpenRead();
                        BinaryReader Br = new BinaryReader(str);
                        Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                        registro.archivo = FileDet;

                        registro.IdEmpleado = _empleado_Servicio.Insertar(registro);
                        if (registro.IdEmpleado > 0)
                        {
                            if (ConfigurationManager.AppSettings["envioMail"].ToString() == "1")
                            {
                                Empleado_Registro empleado = _empleado_Servicio.ListarEmpleadosBoleta(new Empleado_Registro() { IdEmpleado = registro.IdEmpleado, Anio = registro.Anio, Mes = registro.Mes, Nombre = "" }).ToList()[0];
                                SendEmail(empleado, registro.IdPlanilla + registro.TipoPlanilla);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //if (!System.IO.File.Exists(Path.Combine(filePathExtractor + "\\log.txt"))) System.IO.File.Create(Path.Combine(filePathExtractor + "\\log.txt"));
                using (StreamWriter sw = System.IO.File.AppendText(Path.Combine(filePathExtractor + "\\log.txt")))
                {
                    sw.WriteLine(String.Format("{0}-{1}-{2}", DateTime.Now.ToString(), ex.Message, ex.StackTrace));
                }
                Response.StatusCode = 500;
            }
        }
        #endregion

    }
}