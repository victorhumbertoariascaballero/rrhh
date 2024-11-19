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
using MVCSisRRHH.Models;
using CrystalDecisions.CrystalReports.Engine;
using SelectPdf;
using MIDIS.UtilesMVC.Filtros;
using Newtonsoft.Json;
using System.Net.Http;
using MVCSisRRHH.Controllers.IRMA_Autentica;
using MVCSisRRHH.Controllers.IRMA_Reniec;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MIDIS.SEG.LogicaNegocio;

namespace MVCSisRRHH.Controllers
{
    [Authorize]
	public class PostulanteController: Controller
	{
        private readonly T_genm_empleado_LN _empleado_Servicio = new T_genm_empleado_LN();
        private readonly T_genm_postulante_LN _postulante_Servicio = new T_genm_postulante_LN();
        private readonly T_genm_dependencia_LN _dependencia_Servicio = new T_genm_dependencia_LN();
        private readonly T_genm_contrato_LN _contrato_Servicio = new T_genm_contrato_LN();
        private readonly T_genm_perfil_puesto_LN _perfilPuesto_Servicio = new T_genm_perfil_puesto_LN();

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            // CONSULTAMOS EN LA BD SI EXISTE EL POSTULANTE 
            String strDNI = VariablesWeb.ConsultaInformacion.Persona.vNroDocumento;

            Postulante_Request peticion = new Postulante_Request() { IdPostulante = 0, NroDocumento = strDNI };
            Postulante_Registro info = _postulante_Servicio.ObtenerPostulante(peticion);
            
            if (info == null)
            {
                info = _postulante_Servicio.ObtenerPostulantePersona(peticion);

                if (info.TipoDocumento == 1) {
                    try
                    {
                        info.Nacionalidad = "PERUANO";
                        //PostulanteInformacion_Registro infoAux = new PostulanteInformacion_Registro() { NroDocumento = peticion.NroDocumento };
                        //obtenerDesdeWS_RENIEC(ref infoAux);

                        Persona_Registro infoAux = new Persona_Registro();
                        infoAux = await obtenerDesdeWS_IRMA(peticion.NroDocumento);

                        if (infoAux.coResultado == "ERROR")
                        {
                            SendEmail(new PostulanteInformacion_Registro() { NombreProceso = infoAux.mensaje }, "9");
                            return View("SinConexion");
                        }
                        else {
                            info.Paterno = infoAux.ApellidoPaterno;
                            info.Materno = infoAux.ApellidoMaterno;
                            info.Nombre = infoAux.Nombres;
                            info.NroDocumento = infoAux.NumeroDeDocumento;
                            info.Foto = infoAux.sFoto;
                            info.DescripcionUbigeo = infoAux.DescripcionUbigeo;
                            info.Domicilio = infoAux.DireccionDomicilio; // Domicilio;
                            info.IdEstadoCivil = Convert.ToInt32(infoAux.EstadoCivil); // IdEstadoCivil;
                            info.Ubigeo = infoAux.DescripcionUbigeo; // Ubigeo;
                            info.FechaNacimiento = (infoAux.FechaDeNacimiento.HasValue ? infoAux.FechaDeNacimiento.Value.ToString("dd/MM/yyyy") : String.Empty);
                            info.Sexo = infoAux.Sexo;
                            info.CorreoElectronico = VariablesWeb.ConsultaInformacion.Persona.vEmail;
                            info.Celular = VariablesWeb.ConsultaInformacion.Persona.vCelular;

                            //if (info.Sexo == "F") info.Sexo = "1";
                            //if (info.Sexo == "M") info.Sexo = "2";

                            info.FechaRegistro = DateTime.Now;
                            info.IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario; // info.NroDocumento;

                            _postulante_Servicio.InsertarPostulante(info);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                if (info.TipoDocumento == 3)
                {
                    try
                    {
                        info.Nacionalidad = "";
                        //info.Foto = infoAux.Foto;
                        //info.DescripcionUbigeo = infoAux.DescripcionUbigeo;
                        //info.Domicilio = infoAux.Domicilio;
                        //info.IdEstadoCivil = infoAux.IdEstadoCivil;
                        //info.Ubigeo = infoAux.Ubigeo;
                        //info.FechaNacimiento = infoAux.FechaNacimiento;
                        info.Sexo = ""; // infoAux.Sexo;
                        info.Ubigeo = "000000";
                        info.CorreoElectronico = VariablesWeb.ConsultaInformacion.Persona.vEmail;
                        info.Celular = VariablesWeb.ConsultaInformacion.Persona.vCelular;

                        //if (info.Nombre == null) //Si es diferente a null
                        //    obtenerDesdeWS_PIDE(ref info);


                        if (info.Sexo == "F") info.Sexo = "1";
                        if (info.Sexo == "M") info.Sexo = "2";

                        info.FechaRegistro = DateTime.Now;
                        info.IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario; // info.NroDocumento;

                        _postulante_Servicio.InsertarPostulante(info);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }

            if (info != null)
            {
                ViewBag.NroDocumento = info.NroDocumento;
                ViewBag.TipoDocumento = info.TipoDocumento;
            }

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
        [AllowAnonymous]
        public JsonResult ListarTipoDeDocumento()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("1", "DNI"));
            lista.Add(new Estado_Response("3", "CARNÉ DE EXTRANJERÍA"));

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [AllowAnonymous]
        public JsonResult ListarTipoDeDocumentoDNI()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("1", "DNI"));
            
            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarTipoDocumento()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("1", "DOCUMENTO DE IDENTIDAD"));
            lista.Add(new Estado_Response("2", "COLEGIATURA"));
            lista.Add(new Estado_Response("3", "CERTIFICADO DE HABILITACIÓN PROFESIONAL"));
            lista.Add(new Estado_Response("4", "LICENCIADO FF.AA."));
            lista.Add(new Estado_Response("5", "CONADIS (DISCAPACIDAD)"));
            lista.Add(new Estado_Response("10", "DEPORTISTA DE ALTO RENDIMIENTO"));
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
            lista.Add(new Estado_Response("3", "SIN RÉGIMEN (JUBILADO)"));

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarTipoDePensionConRegimen()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("1", "AFP"));
            lista.Add(new Estado_Response("2", "ONP"));
            
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
        [HttpGet]
        public JsonResult ListarAnioPostulacion()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            for (Int32 iPos = 0; iPos < 50; iPos++)
            {
                lista.Add(new Estado_Response((DateTime.Now.Year - iPos).ToString(), (DateTime.Now.Year - iPos).ToString()));
            }
            
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarMesPostulacion()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("1", "ENERO"));
            lista.Add(new Estado_Response("2", "FEBRERO"));
            lista.Add(new Estado_Response("3", "MARZO"));
            lista.Add(new Estado_Response("4", "ABRIL"));
            lista.Add(new Estado_Response("5", "MAYO"));
            lista.Add(new Estado_Response("6", "JUNIO"));
            lista.Add(new Estado_Response("7", "JULIO"));
            lista.Add(new Estado_Response("8", "AGOSTO"));
            lista.Add(new Estado_Response("9", "SEPTIEMBRE"));
            lista.Add(new Estado_Response("10", "OCTUBRE"));
            lista.Add(new Estado_Response("11", "NOVIEMBRE"));
            lista.Add(new Estado_Response("12", "DICIEMBRE"));

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarTipoMateria()
        {
            IEnumerable<PerfilTipoMateria_Response> lista_PerfilPuesto = _perfilPuesto_Servicio.ListarMaePerfilTipoMateria();
            List<Estado_Response> lista = new List<Estado_Response>();
            lista_PerfilPuesto.ToList().ForEach(item => lista.Add(new Estado_Response(item.iCodTipoMateria.ToString(), item.strDescripcion)));
            object respuesta = lista;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarTipoMateriaOtros()
        {
            IEnumerable<PerfilTipoMateriaOtros_Response> lista_PerfilPuesto = _perfilPuesto_Servicio.ListarMaePerfilTipoMateriaOtros();
            List<Estado_Response> lista = new List<Estado_Response>();
            lista_PerfilPuesto.ToList().ForEach(item => lista.Add(new Estado_Response(item.iCodTipoMateriaOtros.ToString(), item.strDescripcion)));
            object respuesta = lista;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarNivelMateria()
        {
            IEnumerable<PerfilNivelMateria_Response> lista_PerfilPuesto = _perfilPuesto_Servicio.ListarMaePerfilNivelMateria();
            List<Estado_Response> lista = new List<Estado_Response>();
            lista_PerfilPuesto.ToList().ForEach(item => lista.Add(new Estado_Response(item.iCodTipoNivelMateria.ToString(), item.strDescripcion)));
            object respuesta = lista;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarSector()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("1", "PRIVADO"));
            lista.Add(new Estado_Response("2", "PÚBLICO"));
            object respuesta = lista;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarRegimen()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("1", "N° 276"));
            lista.Add(new Estado_Response("2", "N° 728"));
            lista.Add(new Estado_Response("3", "N° 1057"));
            lista.Add(new Estado_Response("4", "N° 1024"));
            lista.Add(new Estado_Response("5", "PAC"));
            lista.Add(new Estado_Response("6", "FAG"));
            lista.Add(new Estado_Response("7", "Ley N° 30057"));
            lista.Add(new Estado_Response("8", "N° 1401"));
            lista.Add(new Estado_Response("9", "Otro"));
            object respuesta = lista;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        //[DeleteTempFile]
        public ActionResult DescargarArchivo(String idPostulante, String idPostulacion, String idConvocatoria, String idTipo)
        {
            PostulanteInformacion_Registro peticion = new PostulanteInformacion_Registro();
            peticion.IdPostulante = Int32.Parse(idPostulante);
            peticion.IdPostulacion = Int32.Parse(idPostulacion);
            peticion.IdConvocatoria = Int32.Parse(idConvocatoria);
            peticion.TipoDocumento = Int32.Parse(idTipo);

            peticion = _postulante_Servicio.ObtenerPostulanteFichaDocumento(peticion);
            List<EmpleadoContrato_Registro> contrato = _contrato_Servicio.ListarContratos(new Contrato_Request() { Estado = 0, Nombre = "" }).Where(x => x.IdPostulante == peticion.IdPostulante &&
                                                                                                                                      x.IdPostulacion == peticion.IdPostulacion &&
                                                                                                                                      x.IdConvocatoria == peticion.IdConvocatoria).ToList();

            switch (idTipo) {
                case "1": return File(peticion.FileHojaVida, "image/jpeg", String.Format("Foto_{0}.jpg", idPostulante));
                case "2": return File(peticion.FileSustento, "application/pdf", String.Format("Voucher_{0}.pdf", idPostulante));
                case "3": return File(peticion.FileDDJJ, "application/pdf", String.Format("Formatos_{0}.pdf", idPostulante));
                case "4":
                    Postulacion_Registro postulaAux = _postulante_Servicio.ObtenerPostulacionDocumento(new Postulacion_Registro() { IdPostulacion = Convert.ToInt32(idPostulacion), IdDetalle = 6001, IdTipo = 5 });
                    return File(postulaAux.FileDocumento, "application/pdf", String.Format("Formatos_{0}.pdf", idPostulante));
                case "9": return File((contrato[0].archivo), "application/pdf", String.Format("ContratoLaboral_{0}.pdf", idPostulante));
            }

            return null;
        }
        [HttpPost]
        public JsonResult DescargarArchivoPostulante(Postulante_Registro peticion)
        {
            string pathUpload = Server.MapPath("~/temp/" + peticion.IdPostulante.ToString());
            if (!Directory.Exists(pathUpload))
                Directory.CreateDirectory(pathUpload);

            //String idPostulante = "20200001";
            //peticion.IdPostulante = Int32.Parse(idPostulante);
            //peticion.IdDetalle = 1;
            //peticion.IdTipo = 3;

            Postulante_Registro peticionAux = _postulante_Servicio.ObtenerPostulanteDocumento(peticion);

            String strArchivo = String.Empty;
            String strNombreArchivo = String.Empty;
            switch (peticion.IdTipo)
            {
                case 1:
                    strArchivo = String.Format("ES{0}_{1}.pdf", peticion.IdPostulante, DateTime.Now.ToString("yyyyMMddHHmm"));
                    strNombreArchivo = System.IO.Path.Combine(pathUpload, strArchivo);
                    System.IO.File.WriteAllBytes(strNombreArchivo, peticionAux.FileEstudio);
                    break;
                case 2:
                    strArchivo = String.Format("CA{0}_{1}.pdf", peticion.IdPostulante, DateTime.Now.ToString("yyyyMMddHHmm"));
                    strNombreArchivo = System.IO.Path.Combine(pathUpload, strArchivo);
                    System.IO.File.WriteAllBytes(strNombreArchivo, peticionAux.Filecapacitacion);
                    break;
                case 3:
                    strArchivo = String.Format("EX{0}_{1}.pdf", peticion.IdPostulante, DateTime.Now.ToString("yyyyMMddHHmm"));
                    strNombreArchivo = System.IO.Path.Combine(pathUpload, strArchivo);
                    System.IO.File.WriteAllBytes(strNombreArchivo, peticionAux.FileExperiencia);
                    break;
                case 5:
                    strArchivo = String.Format("DO{0}_{1}.pdf", peticion.IdPostulante, DateTime.Now.ToString("yyyyMMddHHmm"));
                    strNombreArchivo = System.IO.Path.Combine(pathUpload, strArchivo);
                    System.IO.File.WriteAllBytes(strNombreArchivo, peticionAux.FileDocumento);
                    break;
            }

            strNombreArchivo = Request.Url.AbsoluteUri.Substring(0,Request.Url.AbsoluteUri.IndexOf("Postulante")) + "temp/" + peticion.IdPostulante.ToString() + "/" + strArchivo;
            return Json(new { success = "True", responseText = strNombreArchivo });
        }
        [HttpPost]
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
            }

            strNombreArchivo = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf("Postulante")) + "temp/P" + peticion.IdPostulacion.ToString() + "/" + strArchivo;
            return Json(new { success = "True", responseText = strNombreArchivo });
        }
        [HttpPost]
        public JsonResult ObtenerPostulanteFicha(PostulanteInformacion_Registro peticion)
        {
            object respuesta = _postulante_Servicio.ObtenerPostulanteFicha(peticion);

            return Json(respuesta);
        }
        [HttpPost]
        public JsonResult ObtenerPostulanteExperiencia(Postulante_Registro peticion)
        {
            object respuesta = _postulante_Servicio.ObtenerPostulanteExperiencia(peticion);

            return Json(respuesta);
        }
        [HttpPost]
        public JsonResult ObtenerPostulanteCapacitacion(Postulante_Registro peticion)
        {
            object respuesta = _postulante_Servicio.ObtenerPostulanteCapacitacion(peticion);

            return Json(respuesta);
        }
        [HttpPost]
        public JsonResult ObtenerPostulanteEstudio(Postulante_Registro peticion)
        {
            object respuesta = _postulante_Servicio.ObtenerPostulanteEstudio(peticion);

            return Json(respuesta);
        }
        //[HttpPost]
        //public JsonResult ObtenerPostulanteDocumento(Postulante_Registro peticion)
        //{
        //    object respuesta = _postulante_Servicio.ObtenerPostulanteDocumento(peticion);

        //    return Json(respuesta);
        //}
        [HttpPost]
        public JsonResult ObtenerPostulante(Postulante_Request peticion)
        {
            object respuesta = _postulante_Servicio.ObtenerPostulante(peticion);

            return Json(respuesta);
        }
        [HttpPost]
        public JsonResult ObtenerPostulacionPostulante(Postulante_Request peticion)
        {
            object respuesta = _postulante_Servicio.ObtenerPostulacionPostulante(peticion);

            return Json(respuesta);
        }
        [HttpPost]
        public JsonResult ObtenerPostulacionPostulanteServir(Postulante_Request peticion)
        {
            object respuesta = _postulante_Servicio.ObtenerPostulacionPostulanteServir(peticion);

            return Json(respuesta);
        }
        [HttpGet]
        public ActionResult Listar()
        {
            ViewBag.IdPostulante = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;
            return View("~/Views/Postulaciones/Index.cshtml");
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
            String strDNI = VariablesWeb.ConsultaInformacion.Persona.vNroDocumento;

            peticion.IdPostulante = 0;
            peticion.NroDocumento = strDNI;
            Postulante_Registro info = _postulante_Servicio.ObtenerPostulante(peticion);

            peticion.IdPostulante = info.IdPostulante;
            List<PostulantePostulacion_Registro> lista = _postulante_Servicio.ListarPostulaciones(peticion).ToList();
            foreach(PostulantePostulacion_Registro obj in lista){
                obj.Contrasena = HttpUtility.UrlEncode(new Crypto().Encriptar(String.Format("{0}|{1}|{2}|{3}", obj.IdPostulante, obj.IdPostulacion, obj.IdConvocatoria, obj.FechaMaximaContrato)));
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
        public JsonResult ListarPostulanteDocumento(Postulante_Request peticion)
        {
            object respuesta = _postulante_Servicio.ListarPostulanteDocumento(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarPostulacionDocumento(Postulante_Request peticion)
        {
            object respuesta = _postulante_Servicio.ListarPostulacionDocumento(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarPostulanteEstudio(Postulante_Request peticion)
        {
            object respuesta = _postulante_Servicio.ListarPostulanteEstudio(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarPostulacionEstudio(Postulante_Request peticion)
        {
            object respuesta = _postulante_Servicio.ListarPostulacionEstudio(peticion);
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
        public JsonResult EliminarPostulanteEstudio(PostulanteEstudio_Registro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                object respuesta = _postulante_Servicio.EliminarPostulanteEstudio(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult EliminarPostulanteCapacitacion(PostulanteCapacitacion_Registro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                object respuesta = _postulante_Servicio.EliminarPostulanteCapacitacion(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult EliminarPostulanteExperiencia(PostulanteExperiencia_Registro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                object respuesta = _postulante_Servicio.EliminarPostulanteExperiencia(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult EliminarPostulanteDocumento(PostulanteDocumento_Registro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                object respuesta = _postulante_Servicio.EliminarPostulanteDocumento(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult ActualizarRegistroPostulacion(Postulacion_Registro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;

                List<PostulantePostulacion_Registro> lista = _postulante_Servicio.ListarPostulaciones(new Postulante_Request() { IdPostulante = registro.IdPostulante }).ToList();
                if (lista.Exists(x => x.IdConvocatoria == registro.IdConvocatoria))
                {
                    return Json(new { success = "False", responseText = "Ya tiene una postulación registrada para este proceso de convocatoria" });
                }
                else {
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

                                    registro.FileAnexo06 = FileDet;
                                }
                            }
                        }
                        if ((registro.formatos.ToList())[1].GetType().FullName != "System.String[]")
                        {
                            HttpPostedFileBase postfile1 = ((HttpPostedFileBase[])(registro.formatos.ToList())[1])[0];
                            if (postfile1 != null)
                            {
                                if (postfile1.ContentLength > 0)
                                {
                                    nameFile = postfile1.FileName;

                                    Stream str = postfile1.InputStream;
                                    BinaryReader Br = new BinaryReader(str);
                                    Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                                    registro.FileHojaVida = FileDet;
                                }
                            }
                        }
                    }

                    object respuesta = _postulante_Servicio.ActualizarRegistroPostulacion(registro);

                    Postulante_Request peticion = new Postulante_Request() { IdPostulante = registro.IdPostulante, IdPostulacion = registro.IdPostulacion, IdConvocatoria = registro.IdConvocatoria, NroDocumento = "" };
                    PostulanteInformacion_Registro info = new PostulanteInformacion_Registro(); // _postulante_Servicio.ObtenerInformacionPostulante(peticion);
                    Postulante_Registro postu = _postulante_Servicio.ObtenerPostulante(peticion);
                    Convocatoria_Registro convo = new T_genm_convocatoria_LN().ObtenerParaEditar(new Convocatoria_Request() { IdConvocatoria = registro.IdConvocatoria });

                    info.Nombre = postu.Nombre;
                    info.CorreoElectronico = postu.CorreoElectronico;
                    info.NombreProceso = convo.NroConvocatoria;

                    this.SendEmail(info, "5");

                    return Json(new { success = "True", responseText = respuesta });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult ActualizarRegistroPostulacionServir(Postulacion_Registro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;

                List<PostulantePostulacion_Registro> lista = _postulante_Servicio.ListarPostulacionesServir(new Postulante_Request() { IdPostulante = registro.IdPostulante, IdTipoConvocatoria = 3 }).ToList();
                if (lista.Exists(x => x.IdConvocatoria == registro.IdConvocatoria))
                {
                    return Json(new { success = "False", responseText = "Ya tiene una postulación registrada para este proceso de convocatoria" });
                }
                else
                {
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

                                    registro.FileAnexo06 = FileDet;
                                }
                            }
                        }
                        if ((registro.formatos.ToList())[1].GetType().FullName != "System.String[]")
                        {
                            HttpPostedFileBase postfile1 = ((HttpPostedFileBase[])(registro.formatos.ToList())[1])[0];
                            if (postfile1 != null)
                            {
                                if (postfile1.ContentLength > 0)
                                {
                                    nameFile = postfile1.FileName;

                                    Stream str = postfile1.InputStream;
                                    BinaryReader Br = new BinaryReader(str);
                                    Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                                    registro.FileHojaVida = FileDet;
                                }
                            }
                        }
                    }

                    object respuesta = _postulante_Servicio.ActualizarRegistroPostulacionServir(registro);

                    Postulante_Request peticion = new Postulante_Request() { IdPostulante = registro.IdPostulante, IdPostulacion = registro.IdPostulacion, IdConvocatoria = registro.IdConvocatoria, NroDocumento = "", IdTipoConvocatoria = 3 };
                    PostulanteInformacion_Registro info = new PostulanteInformacion_Registro(); // _postulante_Servicio.ObtenerInformacionPostulante(peticion);
                    Postulante_Registro postu = _postulante_Servicio.ObtenerPostulante(peticion);
                    Convocatoria_Registro convo = new T_genm_convocatoria_LN().ObtenerServirParaEditar(new Convocatoria_Request() { IdConvocatoria = registro.IdConvocatoria });

                    info.Nombre = postu.Nombre;
                    info.CorreoElectronico = postu.CorreoElectronico;
                    info.NombreProceso = convo.NroConvocatoria;

                    this.SendEmail(info, "6");

                    return Json(new { success = "True", responseText = respuesta });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> IngresarPostulacionPractica(Postulacion_Registro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;
                
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

                                registro.FileAnexoPracticas = FileDet;
                            }
                        }
                    }
                    if ((registro.formatos.ToList())[1].GetType().FullName != "System.String[]")
                    {
                        HttpPostedFileBase postfile1 = ((HttpPostedFileBase[])(registro.formatos.ToList())[1])[0];
                        if (postfile1 != null)
                        {
                            if (postfile1.ContentLength > 0)
                            {
                                nameFile = postfile1.FileName;

                                Stream str = postfile1.InputStream;
                                BinaryReader Br = new BinaryReader(str);
                                Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                                registro.FileHojaVida = FileDet;
                            }
                        }
                    }
                    if ((registro.formatos.ToList())[2].GetType().FullName != "System.String[]")
                    {
                        HttpPostedFileBase postfile2 = ((HttpPostedFileBase[])(registro.formatos.ToList())[2])[0];
                        if (postfile2 != null)
                        {
                            if (postfile2.ContentLength > 0)
                            {
                                nameFile = postfile2.FileName;

                                Stream str = postfile2.InputStream;
                                BinaryReader Br = new BinaryReader(str);
                                Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                                registro.FileCarta = FileDet;
                            }
                        }
                    }
                }

                // CONSULTAMOS EN LA BD SI EXISTE EL POSTULANTE 
                Postulante_Request peticion = new Postulante_Request() { IdPostulante = 0, NroDocumento = registro.NroDocumento };
                Postulante_Registro info = _postulante_Servicio.ObtenerPostulante(peticion);

                if (info == null)
                {
                    info = new Postulante_Registro();
                    info.TipoDocumento = 1;
                    try
                    {
                        if (info.TipoDocumento == 1)
                        {
                            info.Nacionalidad = "PERUANO";
                            //PostulanteInformacion_Registro infoAux = new PostulanteInformacion_Registro() { NroDocumento = peticion.NroDocumento };
                            //obtenerDesdeWS_RENIEC(ref infoAux);

                            Persona_Registro infoAux = new Persona_Registro();
                            infoAux = await obtenerDesdeWS_IRMA(peticion.NroDocumento);

                            info.Paterno = infoAux.ApellidoPaterno;
                            info.Materno = infoAux.ApellidoMaterno;
                            info.Nombre = infoAux.Nombres;
                            info.NroDocumento = infoAux.NumeroDeDocumento;
                            info.Foto = infoAux.sFoto;
                            info.DescripcionUbigeo = infoAux.DescripcionUbigeo;
                            info.Domicilio = infoAux.DireccionDomicilio; // Domicilio;
                            info.IdEstadoCivil = Convert.ToInt32(infoAux.EstadoCivil); // IdEstadoCivil;
                            info.Ubigeo = infoAux.DescripcionUbigeo; // Ubigeo;
                            info.FechaNacimiento = (infoAux.FechaDeNacimiento.HasValue ? infoAux.FechaDeNacimiento.Value.ToString("dd/MM/yyyy") : String.Empty);
                            info.Sexo = infoAux.Sexo;
                            info.CorreoElectronico = VariablesWeb.ConsultaInformacion.Persona.vEmail;
                            info.Celular = VariablesWeb.ConsultaInformacion.Persona.vCelular;

                            //if (info.Nombre == null) //Si es diferente a null
                            //    obtenerDesdeWS_PIDE(ref info);
                        }
                        else
                            info.Nacionalidad = "";

                        //if (info.Sexo == "F") info.Sexo = "1";
                        //if (info.Sexo == "M") info.Sexo = "2";

                        info.FechaRegistro = DateTime.Now;
                        info.IdUsuarioRegistro = 1;

                        _postulante_Servicio.InsertarPostulante(info);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                if (info != null)
                {
                    info = _postulante_Servicio.ObtenerPostulante(peticion);
                    registro.IdPostulante = info.IdPostulante;

                    object respuesta = _postulante_Servicio.RegistrarPostulacionPractica(registro);

                    Convocatoria_Registro convo = new T_genm_convocatoria_LN().ObtenerPracticaParaEditar(new Convocatoria_Request() { IdConvocatoria = registro.IdConvocatoria });

                    //info.Nombre = .Nombre;
                    info.CorreoElectronico = registro.CorreoElectronico;
                    info.NroColegiatura = convo.NroConvocatoria;

                    this.SendEmailPracticas(info, "5");

                    return Json(new { success = "True", responseText = respuesta });
                }
                else {
                    return Json(new { success = "False", responseText = "Intente su postulación nuevamente por favor" });
                }
                
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpGet]
        public JsonResult ListarPostulanteCapacitacion(Postulante_Request peticion)
        {
            object respuesta = _postulante_Servicio.ListarPostulanteCapacitacion(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarPostulacionCapacitacion(Postulante_Request peticion)
        {
            object respuesta = _postulante_Servicio.ListarPostulacionCapacitacion(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarPostulacionRequisitosCapacitacion(BasesPerfilPuesto_Request peticion)
        {
            object respuesta = _postulante_Servicio.ListarPostulacionRequisitosCapacitacion(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarPostulacionRequisitosEstudio(BasesPerfilPuesto_Request peticion)
        {
            object respuesta = _postulante_Servicio.ListarPostulacionRequisitosEstudio(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public JsonResult ListarPostulanteExperiencia(Postulante_Request peticion)
        {
            object respuesta = _postulante_Servicio.ListarPostulanteExperiencia(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarPostulacionExperiencia(Postulante_Request peticion)
        {
            object respuesta = _postulante_Servicio.ListarPostulacionExperiencia(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarPostulante(Postulante_Request peticion)
        {
            object respuesta = _postulante_Servicio.ListarPostulantes(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Ingresar(String id)
        {
            String[] arraydata = new Crypto().Desencriptar(id).Split('|');

            Postulante_Request peticion = new Postulante_Request(){ IdPostulante = Int32.Parse(arraydata[0]), IdPostulacion = Int32.Parse(arraydata[1]), IdConvocatoria = Int32.Parse(arraydata[2]) };

            PostulanteInformacion_Registro info = _postulante_Servicio.ObtenerInformacionPostulante(peticion);
            //if (info.IdUnidadOrganica > 0) {
            //    // CONSULTAMOS LA TABLA DE DEPENDENCIAS DE LOS SISTEMAS ANTIGUOS 
            //    List<Dependencia_Registro> lista = _dependencia_Servicio.ListarDependenciasAntiguo(new Dependencia_Request() { IdDependencia = info.IdUnidadOrganica }).ToList();
            //    if (lista != null)
            //        info.NombreUnidadOrganica = lista[0].Nombre;
            //}
            
            if (info != null) { 
                //Insertamos la informacion del postulante actualizada de RENIEC 
                //Persona_Registro maePersona = new Persona_Registro();
                PostulanteInformacion_Registro infoAux = _postulante_Servicio.ObtenerPostulanteFicha(info);
                if (infoAux.IdPostulante == 0)
                {
                    try
                    {
                        if (info.TipoDocumento == 1)
                        {
                            info.Nacionalidad = "PERUANO";
                            //obtenerDesdeWS_RENIEC(ref info);

                            //if (info.Nombre == null) //Si es diferente a null
                            //    obtenerDesdeWS_PIDE(ref info);

                            Persona_Registro info2 = new Persona_Registro();
                            info2 = await obtenerDesdeWS_IRMA(peticion.NroDocumento);

                            info.Paterno = info2.ApellidoPaterno;
                            info.Materno = info2.ApellidoMaterno;
                            info.Nombre = info2.Nombres;
                            info.NroDocumento = info2.NumeroDeDocumento;
                            info.Foto = info2.sFoto;
                            info.DescripcionUbigeo = info2.DescripcionUbigeo;
                            info.Domicilio = info2.DireccionDomicilio; // Domicilio;
                            info.IdEstadoCivil = Convert.ToInt32(info2.EstadoCivil); // IdEstadoCivil;
                            info.Ubigeo = info2.DescripcionUbigeo; // Ubigeo;
                            info.FechaNacimiento = (info2.FechaDeNacimiento.HasValue ? info2.FechaDeNacimiento.Value.ToString("dd/MM/yyyy") : String.Empty);
                            info.Sexo = info2.Sexo;
                            info.CorreoElectronico = VariablesWeb.ConsultaInformacion.Persona.vEmail;
                            info.Celular = VariablesWeb.ConsultaInformacion.Persona.vCelular;

                        }
                        else
                            info.Nacionalidad = "";

                        //if (info.Sexo == "F") info.Sexo = "1";
                        //if (info.Sexo == "M") info.Sexo = "2";

                        info.IdEstaAfiliadoPensiones = 1;
                        info.IdEstaAfiliadoBanco = 1;
                        info.IdDeclaraIncompatibilidad = 1;
                        info.IdDeclaraNepotismo = 1;
                        info.IdDeclaraNormas = 1;
                        info.IdDeclaraInteres = 1;
                        info.FechaRegistro = DateTime.Now;
                        info.IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario; // info.NroDocumento;
                        try
                        {
                            info.FechaMaximaContrato = arraydata[3];
                        }
                        catch (Exception)
                        {
                        }
                        _postulante_Servicio.InsertarRegistroPostulante(info);
                        this.SendEmail(info, "1");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else if (String.IsNullOrEmpty(infoAux.Foto))
                {
                    try
                    {
                        if (info.TipoDocumento == 1)
                        {
                            info.Nacionalidad = "PERUANO";
                            //obtenerDesdeWS_RENIEC(ref info);

                            //if (info.Nombre == null) //Si es diferente a null
                            //    obtenerDesdeWS_PIDE(ref info);
                            Persona_Registro info2 = new Persona_Registro();
                            info2 = await obtenerDesdeWS_IRMA(peticion.NroDocumento);

                            info.Paterno = info2.ApellidoPaterno;
                            info.Materno = info2.ApellidoMaterno;
                            info.Nombre = info2.Nombres;
                            info.NroDocumento = info2.NumeroDeDocumento;
                            info.Foto = info2.sFoto;
                            info.DescripcionUbigeo = info2.DescripcionUbigeo;
                            info.Domicilio = info2.DireccionDomicilio; // Domicilio;
                            info.IdEstadoCivil = Convert.ToInt32(info2.EstadoCivil); // IdEstadoCivil;
                            info.Ubigeo = info2.DescripcionUbigeo; // Ubigeo;
                            info.FechaNacimiento = (info2.FechaDeNacimiento.HasValue ? info2.FechaDeNacimiento.Value.ToString("dd/MM/yyyy") : String.Empty);
                            info.Sexo = info2.Sexo;
                            info.CorreoElectronico = VariablesWeb.ConsultaInformacion.Persona.vEmail;
                            info.Celular = VariablesWeb.ConsultaInformacion.Persona.vCelular;
                        }
                        else
                            info.Nacionalidad = "";

                        //if (info.Sexo == "F") info.Sexo = "1";
                        //if (info.Sexo == "M") info.Sexo = "2";

                        info.IdDeclaraIncompatibilidad = 1;
                        info.IdDeclaraNepotismo = 1;
                        info.IdDeclaraNormas = 1;
                        info.IdDeclaraInteres = 1;
                        info.FechaRegistro = DateTime.Now;
                        info.IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario; // info.NroDocumento;
                        try
                        {
                            info.FechaMaximaContrato = arraydata[3];
                        }
                        catch (Exception)
                        {
                        }
                        _postulante_Servicio.InsertarRegistroPostulante(info);
                        this.SendEmail(info, "1");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                } 
            }

            if (info != null) {
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
                registro.IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                object respuesta = _postulante_Servicio.RegistrarPostulanteFamiliar(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult RegistrarPostulanteDocumento(PostulanteDocumento_Registro registro)
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

                                registro.FileArchivo = FileDet;
                            }
                        }
                    }
                }

                object respuesta = _postulante_Servicio.RegistrarPostulanteDocumento(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult RegistrarPostulanteEstudio(PostulanteEstudio_Registro registro)
        {
            try
            {
                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;
                String nameFile = String.Empty;
                if (registro.formatos != null) {
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

                                registro.FileArchivo = FileDet;
                            }
                        }
                    }
                }

                object respuesta = _postulante_Servicio.RegistrarPostulanteEstudio(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult RegistrarPostulanteCapacitacion(PostulanteCapacitacion_Registro registro)
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

                                registro.FileArchivo = FileDet;
                            }
                        }
                    }
                }

                object respuesta = _postulante_Servicio.RegistrarPostulanteCapacitacion(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult RegistrarPostulanteExperiencia(PostulanteExperiencia_Registro registro)
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

                                registro.FileArchivo = FileDet;
                            }
                        }
                    }
                }

                List<PostulanteExperiencia_Registro> lista = _postulante_Servicio.ListarPostulanteExperiencia(new Postulante_Request() { IdPostulante = registro.IdPostulante }).ToList();
                foreach (PostulanteExperiencia_Registro obj in lista) {
                    if (registro.IdLaboral != obj.IdLaboral) {
                        if ((DateTime.Parse(obj.FechaFin) >= DateTime.Parse(registro.FechaInicio) && DateTime.Parse(registro.FechaInicio) >= DateTime.Parse(obj.FechaInicio)) ||
                            (DateTime.Parse(obj.FechaFin) >= DateTime.Parse(registro.FechaFin) && DateTime.Parse(registro.FechaFin) >= DateTime.Parse(obj.FechaInicio)) ||
                            (DateTime.Parse(obj.FechaInicio) >= DateTime.Parse(registro.FechaInicio) && DateTime.Parse(obj.FechaFin) <= DateTime.Parse(registro.FechaFin)))
                            throw new Exception("Ya existe una experiencia laboral que contiene las fechas ingresadas");
                    }                    
                }

                object respuesta = _postulante_Servicio.RegistrarPostulanteExperiencia(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult RegistrarPostulacionAnexo(PostulacionAnexo_Registro registro)
        {
            try
            {
                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                object respuesta = _postulante_Servicio.RegistrarPostulacionAnexo(registro);

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
                registro.IdUsuarioModificacion = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

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
                registro.IdUsuarioModificacion = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                object respuesta = _postulante_Servicio.EliminarPostulanteFamiliar(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult ActualizarPostulante(Postulante_Registro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                object respuesta = _postulante_Servicio.ActualizarPostulante(registro);

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
        public JsonResult Guardar(PostulanteInformacion_Registro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

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
        [Authorize]
        public JsonResult GuardarDocumentosPostulante(PostulanteInformacion_Registro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                String nameFile = String.Empty;
                if ((registro.formatos.ToList())[0].GetType().FullName != "System.String[]") {
                    HttpPostedFileBase postfile = ((HttpPostedFileBase[])(registro.formatos.ToList())[0])[0];
                    if (postfile != null)
                    {
                        if (postfile.ContentLength > 0)
                        {
                            nameFile = postfile.FileName;

                            Stream str = postfile.InputStream;
                            BinaryReader Br = new BinaryReader(str);
                            Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                            registro.FileHojaVida = FileDet;
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

                            registro.FileSustento = FileDet;
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

                            registro.FileDDJJ = FileDet;
                        }
                    }
                }
                //if ((registro.formatos.ToList())[3].GetType().FullName != "System.String[]")
                //{
                //    HttpPostedFileBase postfile = ((HttpPostedFileBase[])(registro.formatos.ToList())[3])[0];
                //    if (postfile != null)
                //    {
                //        if (postfile.ContentLength > 0)
                //        {
                //            nameFile = postfile.FileName;

                //            Stream str = postfile.InputStream;
                //            BinaryReader Br = new BinaryReader(str);
                //            Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                //            registro.FileFormato = FileDet;
                //        }
                //    }
                //}
                
                object respuesta = _postulante_Servicio.ActualizarPostulanteFichaDocumento(registro);
                Convocatoria_Registro objConvocatotia = new T_genm_convocatoria_LN().ObtenerParaEditar(new Convocatoria_Request() { IdConvocatoria = registro.IdConvocatoria });
                registro = _postulante_Servicio.ObtenerPostulanteFicha(registro);

                registro.NombreCargo = objConvocatotia.NombreCargo;
                registro.NombreProceso = objConvocatotia.NroConvocatoria;
                this.SendEmail(registro, "2");

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
        [Authorize]
        public JsonResult AprobarContratoPostulante(PostulanteInformacion_Registro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                object respuesta = _postulante_Servicio.AprobarContratoPostulante(registro);

                return Json(new { success = "True", responseText = respuesta });
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
        //            //KMM REVISAR
        //            info.Sexo = (Response.PersonaDato.Sexo == "1" ? "2" : (Response.PersonaDato.Sexo == "2" ? "1" : String.Empty));
        //            //info.Sexo = Response.PersonaDato.Sexo;
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
                                    mReniec.NumeroDeDocumento = resultadoReniec.data.numDoc;
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
                                    mReniec.mensaje = resultadoReniec.mensaje;
                                    mReniec.coResultado = "ERROR";
                                }
                            }
                        }
                        else
                        {
                            mReniec.mensaje = resultadoAutentica.mensaje;
                            mReniec.coResultado = "ERROR";
                        }

                    }
                    catch (Exception ex1)
                    {
                        mReniec.mensaje = ex1.Message;
                        mReniec.coResultado = "ERROR";
                    }
                }
            }
            catch (Exception ex2)
            {
                mReniec.mensaje = ex2.Message;
                mReniec.coResultado = "ERROR";
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
        public FileResult HojaVida()
        {
            Int32 IdPostulante = (Request.QueryString.Get("IdPostulante") == null ? 0 : Int32.Parse(Request.QueryString["IdPostulante"]));
            Int32 IdPostulacion = (Request.QueryString.Get("IdPostulacion") == null ? 0 : Int32.Parse(Request.QueryString["IdPostulacion"]));
            Int32 IdConvocatoria = (Request.QueryString.Get("IdConvocatoria") == null ? 0 : Int32.Parse(Request.QueryString["IdConvocatoria"]));

            Postulante_Request peticion = new Postulante_Request() {    IdPostulante = IdPostulante, 
                                                                        IdPostulacion = IdPostulacion, 
                                                                        IdConvocatoria = IdConvocatoria,
                                                                        IdTipoConvocatoria = 1};
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

        public FileResult HojaVidaServir()
        {
            Int32 IdPostulante = (Request.QueryString.Get("IdPostulante") == null ? 0 : Int32.Parse(Request.QueryString["IdPostulante"]));
            Int32 IdPostulacion = (Request.QueryString.Get("IdPostulacion") == null ? 0 : Int32.Parse(Request.QueryString["IdPostulacion"]));
            Int32 IdConvocatoria = (Request.QueryString.Get("IdConvocatoria") == null ? 0 : Int32.Parse(Request.QueryString["IdConvocatoria"]));

            Postulante_Request peticion = new Postulante_Request() {    IdPostulante = IdPostulante, 
                                                                        IdPostulacion = IdPostulacion, 
                                                                        IdConvocatoria = IdConvocatoria, 
                                                                        IdTipoConvocatoria = 3 };
            PostulacionPostulante_Registro obj = _postulante_Servicio.ObtenerPostulacionPostulanteServir(peticion);
            List<PostulacionDocumento_Registro> lstDocumento = _postulante_Servicio.ListarPostulacionDocumento(peticion).ToList();
            List<PostulacionEstudio_Registro> lstEstudio = _postulante_Servicio.ListarPostulacionEstudio(peticion).ToList();
            List<PostulacionCapacitacion_Registro> lstCapacitacion = _postulante_Servicio.ListarPostulacionCapacitacion(peticion).ToList();
            List<PostulacionExperiencia_Registro> lstExperiencia = _postulante_Servicio.ListarPostulacionExperiencia(peticion).ToList();
            Convocatoria_Registro objConvocatoria = new T_genm_convocatoria_LN().ObtenerServirParaEditar(new Convocatoria_Request() { IdConvocatoria = IdConvocatoria });

            Stream pdfStream = GenerarHojaVida_ServirPdf(obj, lstDocumento, lstEstudio, lstCapacitacion, lstExperiencia, objConvocatoria);

            return File(pdfStream, "application/pdf");
        }

        private Stream GenerarFichaPersonalPdf(PostulanteInformacion_Registro oFicha)
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

            //html = html.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            html2 = html2.Replace("//aler", (oFicha.IdPresentaAlergias == 1 ? "SI" : "NO"));
            html2 = html2.Replace("//beta", (oFicha.IdPresentaAlergias1 == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html2 = html2.Replace("//analge", (oFicha.IdPresentaAlergias2 == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html2 = html2.Replace("//otral", oFicha.PresentaAlergiasOtro);
            html2 = html2.Replace("//enfe", (oFicha.IdPresentaEnfermedades == 1 ? "SI" : "NO"));
            html2 = html2.Replace("//diab", (oFicha.IdPresentaEnfermedadesD == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html2 = html2.Replace("//hipe", (oFicha.IdPresentaEnfermedadesH == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html2 = html2.Replace("//asma", (oFicha.IdPresentaEnfermedadesA == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html2 = html2.Replace("//epil", (oFicha.IdPresentaEnfermedadesE == 1 ? "&nbsp;X&nbsp;" : "&nbsp;&nbsp;&nbsp;"));
            html2 = html2.Replace("//otrae", oFicha.PresentaEnfermedadesOtro);
            html2 = html2.Replace("//medi", (oFicha.IdConsumeMedicamentos == 1 ? "SI" : "NO"));
            html2 = html2.Replace("//otrom", oFicha.ConsumeMedicamentosOtro.ToUpper());
            html2 = html2.Replace("//grs", (oFicha.IdGrupoSanguineo == 1 ? "O-" : (oFicha.IdGrupoSanguineo == 2 ? "O+" : (oFicha.IdGrupoSanguineo == 3 ? "A-" : (oFicha.IdGrupoSanguineo == 4 ? "A+" : (oFicha.IdGrupoSanguineo == 5 ? "B-" : (oFicha.IdGrupoSanguineo == 6 ? "B+" : (oFicha.IdGrupoSanguineo == 7 ? "AB-" : (oFicha.IdGrupoSanguineo == 8 ? "AB+" : "")))))))));
            html2 = html2.Replace("//adic", oFicha.InformacionAdicionalSalud.ToUpper());


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


            string htmlA = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulante/declaracion_derechohabientes.html"));
            htmlA = htmlA.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            htmlA = htmlA.Replace("//dia", DateTime.Now.Day.ToString());
            htmlA = htmlA.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
            htmlA = htmlA.Replace("//anio", DateTime.Now.Year.ToString());
            htmlA = htmlA.Replace("//dni", (oFicha.TipoDocumento == 1 ? "D.N.I. N&deg; " : "C.E. N&deg; ") + oFicha.NroDocumento);
            htmlA = htmlA.Replace("//paterno", oFicha.Paterno);
            htmlA = htmlA.Replace("//materno", oFicha.Materno);
            htmlA = htmlA.Replace("//nombres", oFicha.Nombre);
            htmlA = htmlA.Replace("//nombrecompleto", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
            htmlA = htmlA.Replace("//email", oFicha.CorreoElectronico.ToUpper());
            htmlA = htmlA.Replace("//fijo", oFicha.Telefono);
            htmlA = htmlA.Replace("//celular", oFicha.Celular);
            htmlA = htmlA.Replace("//domicilio", oFicha.Domicilio);

            Boolean existeConyuge = false;
            String strConyuge = "<tr><td style='width: 75%; height: 25px; font-size: 11px;'>_nombre_</td>" +
                                "<td style='width: 25%; height: 25px; font-size: 11px;'>_dni_</td></tr>";
            if (familiares.Count > 0)
            {
                foreach (PostulanteFamiliar_Registro item in familiares)
                {
                    if (item.IdParentesco == 1 || item.IdParentesco == 2) {
                        existeConyuge = true;
                        strConyuge = strConyuge.Replace("_nombre_", item.Nombre);
                        strConyuge = strConyuge.Replace("_dni_", item.NroDocumento);
                    }
                }
            }
            if (!existeConyuge)
            {
                strConyuge = strConyuge.Replace("_nombre_", "&nbsp;");
                strConyuge = strConyuge.Replace("_dni_", "&nbsp;");
            }
            htmlA = htmlA.Replace("//conyuge", strConyuge);

            Boolean existeHijo = false;
            String strHijos = String.Empty;
            String strHijo = "<tr><td style='width: 75%; height: 25px; font-size: 11px;'>_nombre_</td>" +
                                "<td style='width: 25%; height: 25px; font-size: 11px;'>_dni_</td></tr>";
            if (familiares.Count > 0)
            {
                foreach (PostulanteFamiliar_Registro item in familiares)
                {
                    if (item.IdParentesco == 3)
                    {
                        existeHijo = true;
                        strAux = strHijo;
                        strAux = strAux.Replace("_nombre_", item.Nombre);
                        strAux = strAux.Replace("_dni_", item.NroDocumento);
                        strHijos += strAux;
                    }
                }
            }
            if (!existeHijo)
            {
                strAux = strHijo;
                strAux = strAux.Replace("_nombre_", "&nbsp;");
                strAux = strAux.Replace("_dni_", "&nbsp;");
                strHijos += strAux;
            }
            htmlA = htmlA.Replace("//hijos", strHijos);


            SelectPdf.PdfDocument docA = converter.ConvertHtmlString(htmlA, Server.MapPath("~/temp"));
            foreach (PdfPage page in docA.Pages)
                doc.AddPage(page);



            string html21 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulante/declaracion_formato01.html"));
            html21 = html21.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            html21 = html21.Replace("//dia", DateTime.Now.Day.ToString());
            html21 = html21.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
            html21 = html21.Replace("//anio", DateTime.Now.Year.ToString());
            html21 = html21.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
            html21 = html21.Replace("//dni", (oFicha.TipoDocumento == 1 ? "D.N.I. N&deg; " : "C.E. N&deg; ") + oFicha.NroDocumento);
            html21 = html21.Replace("//direccion", oFicha.Domicilio);
            html21 = html21.Replace("//cargo", oFicha.NombreCargo);

            SelectPdf.PdfDocument doc21 = converter.ConvertHtmlString(html21, Server.MapPath("~/temp"));
            foreach (PdfPage page in doc21.Pages)
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


            string html4 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulante/declaracion_nepotismo.html"));
            html4 = html4.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            html4 = html4.Replace("//dia", DateTime.Now.Day.ToString());
            html4 = html4.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
            html4 = html4.Replace("//anio", DateTime.Now.Year.ToString());
            html4 = html4.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
            html4 = html4.Replace("//dni", (oFicha.TipoDocumento == 1 ? "D.N.I. N&deg; " : "C.E. N&deg; ") + oFicha.NroDocumento);

            if (oFicha.IdDeclaraNepotismo == 0)
            {
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
            }
            else {
                html4 = html4.Replace("//neporelacion1", String.Empty);
                html4 = html4.Replace("//nepoapellido1", String.Empty);
                html4 = html4.Replace("//neponombre1", String.Empty);
                html4 = html4.Replace("//nepoarea1", String.Empty);
                html4 = html4.Replace("//neporelacion2", String.Empty);
                html4 = html4.Replace("//nepoapellido2", String.Empty);
                html4 = html4.Replace("//neponombre2", String.Empty);
                html4 = html4.Replace("//nepoarea2", String.Empty);
                html4 = html4.Replace("//neporelacion3", String.Empty);
                html4 = html4.Replace("//nepoapellido3", String.Empty);
                html4 = html4.Replace("//neponombre3", String.Empty);
                html4 = html4.Replace("//nepoarea3", String.Empty);
            }
            
            SelectPdf.PdfDocument doc4 = converter.ConvertHtmlString(html4, Server.MapPath("~/temp"));
            foreach (PdfPage page in doc4.Pages)
                doc.AddPage(page);


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

            
            string html6 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulante/declaracion_sueldo.html"));
            html6 = html6.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            html6 = html6.Replace("//dia", DateTime.Now.Day.ToString());
            html6 = html6.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
            html6 = html6.Replace("//anio", DateTime.Now.Year.ToString());
            html6 = html6.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
            html6 = html6.Replace("//dni", (oFicha.TipoDocumento == 1 ? "D.N.I. N&deg; " : "C.E. N&deg; ") + oFicha.NroDocumento);
            
            if (oFicha.IdEstaAfiliadoBanco == 0)
            {
                
                html6 = html6.Replace("//suel_no", "&nbsp;X&nbsp;");
                html6 = html6.Replace("//suel_si", "&nbsp;&nbsp;&nbsp;");
                html6 = html6.Replace("//banco", "&nbsp;&nbsp;&nbsp;");
                html6 = html6.Replace("//cuenta", "&nbsp;&nbsp;&nbsp;");
                html6 = html6.Replace("//cci", "&nbsp;&nbsp;&nbsp;");
            }
            else {
                html6 = html6.Replace("//suel_si", "&nbsp;X&nbsp;");
                html6 = html6.Replace("//suel_no", "&nbsp;&nbsp;&nbsp;");
                html6 = html6.Replace("//banco", oFicha.NombreBanco);
                html6 = html6.Replace("//cuenta", oFicha.CuentaBancoAfiliado);
                html6 = html6.Replace("//cci", oFicha.CuentaBancoCCIAfiliado);
            }

            SelectPdf.PdfDocument doc6 = converter.ConvertHtmlString(html6, Server.MapPath("~/temp"));
            foreach (PdfPage page in doc6.Pages)
                doc.AddPage(page);



            string html7 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulante/declaracion_pensiones.html"));
            html7 = html7.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            html7 = html7.Replace("//dia", DateTime.Now.Day.ToString());
            html7 = html7.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
            html7 = html7.Replace("//anio", DateTime.Now.Year.ToString());
            html7 = html7.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
            html7 = html7.Replace("//dni", (oFicha.TipoDocumento == 1 ? "D.N.I. N&deg; " : "C.E. N&deg; ") + oFicha.NroDocumento);

            if (oFicha.IdEstaAfiliadoPensiones == 0)
            {
                html7 = html7.Replace("//afp_nombre", "&nbsp;&nbsp;&nbsp;");
                html7 = html7.Replace("//afp_sel", "&nbsp;&nbsp;&nbsp;");
                html7 = html7.Replace("//onp_sel", "&nbsp;&nbsp;&nbsp;");
                html7 = html7.Replace("//jub_sel", "&nbsp;&nbsp;&nbsp;");
                html7 = html7.Replace("//no_afiliado", "&nbsp;X&nbsp;");

                if (oFicha.IdTipoPensionDeseaAfiliar == 1) {
                    html7 = html7.Replace("//afp_no", "&nbsp;X&nbsp;");
                    html7 = html7.Replace("//onp_no", "&nbsp;&nbsp;&nbsp;");
                }
                else if (oFicha.IdTipoPensionDeseaAfiliar == 2)
                {
                    html7 = html7.Replace("//afp_no", "&nbsp;&nbsp;&nbsp;");
                    html7 = html7.Replace("//onp_no", "&nbsp;X&nbsp;");
                }
                else {
                    html7 = html7.Replace("//afp_no", "&nbsp;&nbsp;&nbsp;");
                    html7 = html7.Replace("//onp_no", "&nbsp;&nbsp;&nbsp;");
                }
            }
            else {
                if (oFicha.IdAFPAfiliada == 1) { // AFILIADO A UNA AFP
                    html7 = html7.Replace("//afp_nombre", oFicha.NombreAFPAfiliada.ToUpper());
                    html7 = html7.Replace("//afp_sel", "&nbsp;X&nbsp;");
                    html7 = html7.Replace("//onp_sel", "&nbsp;&nbsp;&nbsp;");
                    html7 = html7.Replace("//jub_sel", "&nbsp;&nbsp;&nbsp;");
                    html7 = html7.Replace("//no_afiliado", "&nbsp;&nbsp;&nbsp;");
                    html7 = html7.Replace("//afp_no", "&nbsp;&nbsp;&nbsp;");
                    html7 = html7.Replace("//onp_no", "&nbsp;&nbsp;&nbsp;");
                }
                if (oFicha.IdAFPAfiliada == 2)// AFILIADO A LA ONP
                {
                    html7 = html7.Replace("//afp_nombre", "&nbsp;&nbsp;&nbsp;");
                    html7 = html7.Replace("//afp_sel", "&nbsp;&nbsp;&nbsp;");
                    html7 = html7.Replace("//onp_sel", "&nbsp;X&nbsp;");
                    html7 = html7.Replace("//jub_sel", "&nbsp;&nbsp;&nbsp;");
                    html7 = html7.Replace("//no_afiliado", "&nbsp;&nbsp;&nbsp;");
                    html7 = html7.Replace("//afp_no", "&nbsp;&nbsp;&nbsp;");
                    html7 = html7.Replace("//onp_no", "&nbsp;&nbsp;&nbsp;");
                }
                if (oFicha.IdAFPAfiliada == 3)// JUBILADO
                {
                    html7 = html7.Replace("//afp_nombre", "&nbsp;&nbsp;&nbsp;");
                    html7 = html7.Replace("//afp_sel", "&nbsp;&nbsp;&nbsp;");
                    html7 = html7.Replace("//onp_sel", "&nbsp;&nbsp;&nbsp;");
                    html7 = html7.Replace("//jub_sel", "&nbsp;X&nbsp;");
                    html7 = html7.Replace("//no_afiliado", "&nbsp;&nbsp;&nbsp;");
                    html7 = html7.Replace("//afp_no", "&nbsp;&nbsp;&nbsp;");
                    html7 = html7.Replace("//onp_no", "&nbsp;&nbsp;&nbsp;");
                }
            }
            SelectPdf.PdfDocument doc7 = converter.ConvertHtmlString(html7, Server.MapPath("~/temp"));
            foreach (PdfPage page in doc7.Pages)
                doc.AddPage(page);


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


            string html9 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulante/declaracion_prohibicion.html"));
            html9 = html9.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            html9 = html9.Replace("//dia", DateTime.Now.Day.ToString());
            html9 = html9.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
            html9 = html9.Replace("//anio", DateTime.Now.Year.ToString());
            html9 = html9.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
            html9 = html9.Replace("//dni", (oFicha.TipoDocumento == 1 ? "D.N.I. N&deg; " : "C.E. N&deg; ") + oFicha.NroDocumento);
            //html9 = html9.Replace("//proceso_cas", oFicha.NombreProceso);

            SelectPdf.PdfDocument doc9 = converter.ConvertHtmlString(html9, Server.MapPath("~/temp"));
            foreach (PdfPage page in doc9.Pages)
                doc.AddPage(page);


            string html10 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulante/declaracion_autenticidad_copia.html"));
            html10 = html10.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            html10 = html10.Replace("//dia", DateTime.Now.Day.ToString());
            html10 = html10.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
            html10 = html10.Replace("//anio", DateTime.Now.Year.ToString());
            html10 = html10.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
            html10 = html10.Replace("//dni", (oFicha.TipoDocumento == 1 ? "D.N.I. N&deg; " : "C.E. N&deg; ") + oFicha.NroDocumento);
            html10 = html10.Replace("//proceso_cas", oFicha.NombreProceso);

            SelectPdf.PdfDocument doc10 = converter.ConvertHtmlString(html10, Server.MapPath("~/temp"));
            foreach (PdfPage page in doc10.Pages)
                doc.AddPage(page);


            string html11 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulante/declaracion_domicilio.html"));
            html11 = html11.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            html11 = html11.Replace("//dia", DateTime.Now.Day.ToString());
            html11 = html11.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
            html11 = html11.Replace("//anio", DateTime.Now.Year.ToString());
            html11 = html11.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
            html11 = html11.Replace("//dni", (oFicha.TipoDocumento == 1 ? "D.N.I. N&deg; " : "C.E. N&deg; ") + oFicha.NroDocumento);
            html11 = html11.Replace("//direccion", oFicha.Domicilio);
            html11 = html11.Replace("//email", oFicha.CorreoElectronico);

            SelectPdf.PdfDocument doc11 = converter.ConvertHtmlString(html11, Server.MapPath("~/temp"));
            foreach (PdfPage page in doc11.Pages)
                doc.AddPage(page);

            if (oFicha.IdDeclaraInteres == 1)
            {
                string html12 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulante/declaracion_interes.html"));
                html12 = html12.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
                html12 = html12.Replace("//dia", DateTime.Now.Day.ToString());
                html12 = html12.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
                html12 = html12.Replace("//anio", DateTime.Now.Year.ToString());
                html12 = html12.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
                html12 = html12.Replace("//dni", (oFicha.TipoDocumento == 1 ? "D.N.I. N&deg; " : "C.E. N&deg; ") + oFicha.NroDocumento);
                html12 = html12.Replace("//int_no", "&nbsp;&nbsp;&nbsp;");
                html12 = html12.Replace("//int_si", "&nbsp;X&nbsp;");

                SelectPdf.PdfDocument doc12 = converter.ConvertHtmlString(html12, Server.MapPath("~/temp"));
                foreach (PdfPage page in doc12.Pages)
                    doc.AddPage(page);
            }
            else {
                string html12 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulante/declaracion_interes.html"));
                html12 = html12.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
                html12 = html12.Replace("//dia", DateTime.Now.Day.ToString());
                html12 = html12.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
                html12 = html12.Replace("//anio", DateTime.Now.Year.ToString());
                html12 = html12.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
                html12 = html12.Replace("//dni", (oFicha.TipoDocumento == 1 ? "D.N.I. N&deg; " : "C.E. N&deg; ") + oFicha.NroDocumento);
                html12 = html12.Replace("//int_si", "&nbsp;&nbsp;&nbsp;");
                html12 = html12.Replace("//int_no", "&nbsp;X&nbsp;");

                SelectPdf.PdfDocument doc12 = converter.ConvertHtmlString(html12, Server.MapPath("~/temp"));
                foreach (PdfPage page in doc12.Pages)
                    doc.AddPage(page);
            }

            string html13 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulante/declaracion_integridad.html"));
            html13 = html13.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            html13 = html13.Replace("//dia", DateTime.Now.Day.ToString());
            html13 = html13.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
            html13 = html13.Replace("//anio", DateTime.Now.Year.ToString());
            html13 = html13.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
            html13 = html13.Replace("//dni", (oFicha.TipoDocumento == 1 ? "D.N.I. N&deg; " : "C.E. N&deg; ") + oFicha.NroDocumento);
            html13 = html13.Replace("//cargo", oFicha.NombreCargo);

            SelectPdf.PdfDocument doc13 = converter.ConvertHtmlString(html13, Server.MapPath("~/temp"));
            foreach (PdfPage page in doc13.Pages)
                doc.AddPage(page);

            string html14 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulante/declaracion_doble_percepcion.html"));
            html14 = html14.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            html14 = html14.Replace("//dia", DateTime.Now.Day.ToString());
            html14 = html14.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
            html14 = html14.Replace("//anio", DateTime.Now.Year.ToString());
            html14 = html14.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
            html14 = html14.Replace("//dni", (oFicha.TipoDocumento == 1 ? "D.N.I. N&deg; " : "C.E. N&deg; ") + oFicha.NroDocumento);
            
            SelectPdf.PdfDocument doc14 = converter.ConvertHtmlString(html14, Server.MapPath("~/temp"));
            foreach (PdfPage page in doc14.Pages)
                doc.AddPage(page);


            MemoryStream pdfStream = new MemoryStream();
            doc.Save(pdfStream);
            pdfStream.Position = 0;
            doc.Close();

            return pdfStream;
        }
        private Stream GenerarHojaVidaPdf(  PostulacionPostulante_Registro oPostulante, 
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

            //html2 = html2.Replace("//dia", DateTime.Now.Day.ToString());
            //html2 = html2.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
            //html2 = html2.Replace("//anio", DateTime.Now.Year.ToString());
            //html2 = html2.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
            //html2 = html2.Replace("//dni", (oFicha.TipoDocumento == 1 ? "DNI: " : "CE: ") + oFicha.NroDocumento);

            //SelectPdf.PdfDocument doc2 = converter.ConvertHtmlString(html2, Server.MapPath("~/temp"));
            //foreach (PdfPage page in doc2.Pages)
            //    doc.AddPage(page);

            //string html3 = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulante/declaracion_incompatibilidad.html"));
            //html3 = html3.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            //html3 = html3.Replace("//dia", DateTime.Now.Day.ToString());
            //html3 = html3.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
            //html3 = html3.Replace("//anio", DateTime.Now.Year.ToString());
            //html3 = html3.Replace("//nombre", String.Format("{0} {1} {2}", oFicha.Nombre, oFicha.Paterno, oFicha.Materno));
            //html3 = html3.Replace("//dni", (oFicha.TipoDocumento == 1 ? "D.N.I. N&deg; " : "C.E. N&deg; ") + oFicha.NroDocumento);

            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(html, Server.MapPath("~/temp"));

            MemoryStream pdfStream = new MemoryStream();
            doc.Save(pdfStream);
            pdfStream.Position = 0;
            doc.Close();

            return pdfStream;
        }

        private Stream GenerarHojaVida_ServirPdf(PostulacionPostulante_Registro oPostulante,
                                           List<PostulacionDocumento_Registro> lstDocumento,
                                           List<PostulacionEstudio_Registro> lstEstudio,
                                           List<PostulacionCapacitacion_Registro> lstCapacitaciones,
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

            string html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulante/formato_postulacion_servir.html"));
            html = html.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "Postulante/logo_ogrh.png"));
            html = html.Replace("//proceso", objConvocatoria.NroConvocatoria);

            #region Datos Laborales
            html = html.Replace("//codigo_puesto", "");
            html = html.Replace("//codigo_posicion", "");
            html = html.Replace("//nombre_puesto", objConvocatoria.NombreCargo);
            html = html.Replace("//grupo_servidores", "");
            html = html.Replace("//organo", objConvocatoria.Organo);
            html = html.Replace("//unidad_organica", objConvocatoria.Dependencia);
            html = html.Replace("//puesto_jefe", "");
            #endregion

            #region Datos Personales
            html = html.Replace("//nroDni", (oPostulante.TipoDocumento == 1 ? oPostulante.NroDocumento : String.Empty));
            html = html.Replace("//NroExtranjeria", "");
            html = html.Replace("//nombre_apellidos", $"{oPostulante.Nombre} {oPostulante.Materno} {oPostulante.Paterno}");
            html = html.Replace("//femenino", oPostulante.Sexo == "1" ? "X" : "");
            html = html.Replace("//masculino", oPostulante.Sexo == "2" ? "X" : "");

            html = html.Replace("//direccion_domicilio", oPostulante.Domicilio);
            html = html.Replace("//distrito", oPostulante.DescripcionUbigeo.Split('/')[2]);

            html = html.Replace("//provincia", oPostulante.DescripcionUbigeo.Split('/')[1]);
            html = html.Replace("//departamento", oPostulante.DescripcionUbigeo.Split('/')[0]);
            html = html.Replace("//referencia", oPostulante.Referencia.ToUpper());

            html = html.Replace("//fecha_nacimiento", oPostulante.FechaNacimiento);
            html = html.Replace("//lugar_nacimiento", String.Join("/",oPostulante.DescripcionUbigeo.Split('/').Reverse()));
            html = html.Replace("//nacionalidad", oPostulante.Nacionalidad);

            html = html.Replace("//tel_domicilio", oPostulante.Telefono);
            html = html.Replace("//telefono_celular", oPostulante.Celular);
            html = html.Replace("//celular_2", "");
            html = html.Replace("//correo_electronico", oPostulante.CorreoElectronico.ToUpper());
            html = html.Replace("//correo_opcional", "");

            html = html.Replace("//acreditacion_discapacidad", oPostulante.CarnetConadis);
            html = html.Replace("//carnet_fuerzas", oPostulante.FFAA == 1 ? oPostulante.CarnetFuerzasArmadas : "");

            html = html.Replace("//depoSi", oPostulante.Deportista == 1 ? "X" : "");
            html = html.Replace("//depoNo", oPostulante.Deportista != 1 ? "X" : "");
            html = html.Replace("//depoDoc", oPostulante.Deportista == 1 ? oPostulante.Acreditacion : "");
            #endregion

            #region Estudios
            string formacion_completa = "";
            foreach (var estudio in lstEstudio)
            { 
                string str_formacion = "<tr>" +
                                        "<td style='border: 1px solid black; padding: 10px;'>//nivel</td>" +
                                        "<td style='border: 1px solid black;'>//grado</td>" +
                                        "<td style='border: 1px solid black;'>//carrera_nombre</td>" +
                                        "<td style='border: 1px solid black;'>//carreraDesde</td>" +
                                        "<td style='border: 1px solid black;'>//carreraHasta</td>" +
                                        "<td style='border: 1px solid black;'>//fecha_obtencion</td>" +
                                        "<td style='border: 1px solid black;'>//centroEstudios</td>" +
                                        "</tr>";

                str_formacion = str_formacion.Replace("//nivel", estudio.NombreGrado);
                str_formacion = str_formacion.Replace("//grado", estudio.NombreNivel);
                str_formacion = str_formacion.Replace("//carrera_nombre", estudio.Especialidad);
                str_formacion = str_formacion.Replace("//carreraDesde", String.Join("-",estudio.FechaInicioAnioMes.Split('-').Reverse()));
                str_formacion = str_formacion.Replace("//carreraHasta", String.Join("-", estudio.FechaFinAnioMes.Split('-').Reverse()));
                str_formacion = str_formacion.Replace("//fecha_obtencion", estudio.AnioMes);
                str_formacion = str_formacion.Replace("//centroEstudios", estudio.Institucion);

                formacion_completa += str_formacion;
            }
            html = html.Replace("//estudios", formacion_completa);
            #endregion

            #region Colegiatura
            html = html.Replace("//ColegioProfesional", (bool)oPostulante.bColigiatura ? oPostulante.Colegio.ToUpper() : "");
            html = html.Replace("//NroColegiatura", (bool)oPostulante.bColigiatura ? oPostulante.NroColegiatura : "");
            html = html.Replace("//ColegiaturaHabilitadoSi", (bool)oPostulante.bColegiatura_Habilitada && (bool)oPostulante.bColigiatura ? "X": "");
            html = html.Replace("//ColegiaturaHabilitadoNo", (bool)!oPostulante.bColegiatura_Habilitada && (bool)oPostulante.bColigiatura ? "X" : "");
            html = html.Replace("//ColegiaturaInhabilitado", "");
            #endregion

            #region Capacitacion
            var lstCapacitacion = lstCapacitaciones.Where(e => e.bOtroTipoEstudio == 0).ToList();
            string capacitacion_completa = "";
            foreach (var capacitacion in lstCapacitacion)
            {
                string str_capacitacion = "<tr>" +
                                        "<td style='border: 1px solid black; padding: 5px;'>//tipoEstudio</td>" +
                                        "<td style='border: 1px solid black; padding: 5px;'>//nombreEstudio</td>" +
                                        "<td style='border: 1px solid black; padding: 5px;'>//capDesde</td>" +
                                        "<td style='border: 1px solid black; padding: 5px;'>//capHasta</td>" +
                                        "<td style='border: 1px solid black; padding: 5px;'>//horas</td>" +
                                        "<td style='border: 1px solid black; padding: 5px;'>//centroEstudios</td>" +
                                        "</tr>";

                str_capacitacion = str_capacitacion.Replace("//tipoEstudio", capacitacion.MateriaDescripcion);
                str_capacitacion = str_capacitacion.Replace("//nombreEstudio", capacitacion.Especialidad);
                str_capacitacion = str_capacitacion.Replace("//capDesde", capacitacion.FechaInicio);
                str_capacitacion = str_capacitacion.Replace("//capHasta", capacitacion.FechaFin);
                str_capacitacion = str_capacitacion.Replace("//horas", Convert.ToString(capacitacion.Horas));
                str_capacitacion = str_capacitacion.Replace("//centroEstudios", capacitacion.Institucion);

                capacitacion_completa += str_capacitacion;
            }

            html = html.Replace("//capacitacion", capacitacion_completa);
            #endregion

            #region Ofimatica y Dialectos
            
            #region Dialecto
            var lstIdiomaDialecto = lstCapacitaciones
                                    .Where(e => e.bOtroTipoEstudio == 1 && (e.MateriaOtrosDescripcion == "Idiomas" || e.MateriaOtrosDescripcion == "Dialectos"))
                                    .ToList();
            string dialecto_completa = "";
            if (lstIdiomaDialecto.Count == 0)
            {
                string str_capacitacion = "<tr>" + "<td colspan='5' style='border: 1px solid black; padding: 5px;'>No existen registros</td>" + "</tr>";
                dialecto_completa += str_capacitacion;
            }
            else 
            {
                foreach (var capacitacion in lstIdiomaDialecto)
                {
                    string str_capacitacion = "<tr>" +
                                            "<td style='border: 1px solid black; padding: 5px;'>//idioma</td>" +
                                            "<td style='border: 1px solid black; padding: 5px;'>//noAplica</td>" +
                                            "<td style='border: 1px solid black; padding: 5px;'>//basico</td>" +
                                            "<td style='border: 1px solid black; padding: 5px;'>//intermedio</td>" +
                                            "<td style='border: 1px solid black; padding: 5px;'>//avanzado</td>" +
                                            "</tr>";

                    str_capacitacion = str_capacitacion.Replace("//idioma", capacitacion.Especialidad);
                    str_capacitacion = str_capacitacion.Replace("//noAplica", "");
                    str_capacitacion = str_capacitacion.Replace("//basico", capacitacion.iTipoNivelMateria == 1 ? "X" : "");
                    str_capacitacion = str_capacitacion.Replace("//intermedio", capacitacion.iTipoNivelMateria == 2 ? "X" : "");
                    str_capacitacion = str_capacitacion.Replace("//avanzado", capacitacion.iTipoNivelMateria == 3 ? "X" : "");

                    dialecto_completa += str_capacitacion;
                }
            }

            html = html.Replace("//idiomaDialecto", dialecto_completa);
            #endregion

            #region Ofimatica
            var lstOfimatica = lstCapacitaciones
                                    .Where(e => e.bOtroTipoEstudio == 1 && e.MateriaOtrosDescripcion == "Ofimática")
                                    .ToList();
            string ofimatica_completa = "";
            if (lstOfimatica.Count == 0)
            {
                string str_capacitacion = "<tr>" + "<td colspan='5' style='border: 1px solid black; padding: 5px;'>No existen registros</td>" + "</tr>";
                ofimatica_completa += str_capacitacion;
            }
            else
            {
                foreach (var capacitacion in lstOfimatica)
                {
                    string str_capacitacion = "<tr>" +
                                            "<td style='border: 1px solid black; padding: 5px;'>//ofim</td>" +
                                            "<td style='border: 1px solid black; padding: 5px;'>//noAplica</td>" +
                                            "<td style='border: 1px solid black; padding: 5px;'>//basico</td>" +
                                            "<td style='border: 1px solid black; padding: 5px;'>//intermedio</td>" +
                                            "<td style='border: 1px solid black; padding: 5px;'>//avanzado</td>" +
                                            "</tr>";

                    str_capacitacion = str_capacitacion.Replace("//ofim", capacitacion.Especialidad);
                    str_capacitacion = str_capacitacion.Replace("//noAplica", "");
                    str_capacitacion = str_capacitacion.Replace("//basico", capacitacion.iTipoNivelMateria == 1 ? "X" : "");
                    str_capacitacion = str_capacitacion.Replace("//intermedio", capacitacion.iTipoNivelMateria == 2 ? "X" : "");
                    str_capacitacion = str_capacitacion.Replace("//avanzado", capacitacion.iTipoNivelMateria == 3 ? "X" : "");

                    ofimatica_completa += str_capacitacion;
                }
            }

            html = html.Replace("//ofimatica", ofimatica_completa);
            #endregion
            
            #endregion

            #region Experiencia
            string experiencia_completa = "";
            foreach (var experiencia in lstExperiencia)
            {
                string experiencia_template = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Postulante/SERVIR_ExperienciaLaboral.txt"));
                experiencia_template = experiencia_template.Replace("//nombre_empresa", experiencia.Empresa);
                experiencia_template = experiencia_template.Replace("//sector", experiencia.Sector);
                experiencia_template = experiencia_template.Replace("//regimen", experiencia.Regimen);
                experiencia_template = experiencia_template.Replace("//trabajo_puesto", experiencia.Cargo);
                experiencia_template = experiencia_template.Replace("//FinDesde", experiencia.FechaInicio);
                experiencia_template = experiencia_template.Replace("//FinHasta", experiencia.FechaFin);

                experiencia_template = experiencia_template.Replace("//JefeDirecto", experiencia.NombreJefeDirecto);
                experiencia_template = experiencia_template.Replace("//JefeCargo", experiencia.PuestoJefeDirecto);
                experiencia_template = experiencia_template.Replace("//MotivoCambio", experiencia.MotivoCambio);
                experiencia_template = experiencia_template.Replace("//remuneracion", experiencia.Remuneracion.ToString("F2"));

                experiencia_template = experiencia_template.Replace("//ReferenciaNombre", experiencia.RefLaboralNombre);
                experiencia_template = experiencia_template.Replace("//ReferenciaPuesto", experiencia.RefLaboralPuesto);
                experiencia_template = experiencia_template.Replace("//ReferenciaTelCorreo", $"{experiencia.RefLaboralTelefono} - {experiencia.RefLaboralCorreo}");

                experiencia_template = experiencia_template.Replace("//funciones", experiencia.Descripcion);
                experiencia_completa += experiencia_template;
            }

            html = html.Replace("//experiencia_laboral", experiencia_completa);
            #endregion

            #region Informacion Adicional
            html = html.Replace("//trabajarInteriorSi", (bool)oPostulante.bTrabajarProvincia ? "X" : "");
            html = html.Replace("//trabajarInteriorNo", (bool)!oPostulante.bTrabajarProvincia ? "X" : "");
            html = html.Replace("//trabajarInteriorProvincia", (bool)oPostulante.bTrabajarProvincia ? oPostulante.Trabajar_Provincia : "");
            #endregion

            html = html.Replace("//dia", DateTime.Now.Day.ToString());
            html = html.Replace("//mes", (DateTime.Now.Month == 1 ? "enero" : (DateTime.Now.Month == 2 ? "febrero" : (DateTime.Now.Month == 3 ? "marzo" : (DateTime.Now.Month == 4 ? "abril" : (DateTime.Now.Month == 5 ? "mayo" : (DateTime.Now.Month == 6 ? "junio" : (DateTime.Now.Month == 7 ? "julio" : (DateTime.Now.Month == 8 ? "agosto" : (DateTime.Now.Month == 9 ? "setiembre" : (DateTime.Now.Month == 10 ? "octubre" : (DateTime.Now.Month == 11 ? "noviembre" : (DateTime.Now.Month == 12 ? "diciembre" : String.Empty)))))))))))));
            html = html.Replace("//anio", DateTime.Now.Year.ToString());

            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(html, Server.MapPath("~/temp"));

            MemoryStream pdfStream = new MemoryStream();
            doc.Save(pdfStream);
            pdfStream.Position = 0;
            doc.Close();

            return pdfStream;
        }

        [Authorize]
        private Boolean SendEmail(PostulanteInformacion_Registro postulante, String tipo)
        {
            Boolean exitoEnvio = false;
            String html = String.Empty;
            if (tipo == "1") { //AL INICIAR EL PROCESO DE EVALUACION CAS
                html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Correo/InicioProceso.txt"));
                html = html.Replace("_POSTULANTE_", postulante.Nombre);
                html = html.Replace("_PROCESOCAS_", postulante.CodigoProceso);
                html = html.Replace("_URLEXTERNO_", ConfigurationManager.AppSettings["URL_EXTERNO"].ToString()); //HttpUtility.UrlEncode(

                if (!String.IsNullOrEmpty(postulante.CorreoElectronico))
                {
                    MailMessage msg = new MailMessage();
                    msg.To.Add(new MailAddress(postulante.CorreoElectronico));
                    if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CCcorreoINC"].ToString()))
                        msg.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["CCcorreoINC"].ToString()));

                    msg.From = new MailAddress(ConfigurationManager.AppSettings["correo"].ToString());
                    msg.Subject = String.Format("{0} {1}-MIDIS", "Proceso de Convocatorias CAS", postulante.CodigoProceso);
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
            { //AL RECIBIR LA DOCUMENTACION DEL POSTULANTE
                html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Correo/EvaluacionProceso.txt"));
                html = html.Replace("_PROCESOCAS_", postulante.NombreProceso + " - " + postulante.NombreCargo);
                html = html.Replace("_URLINTERNO_", ConfigurationManager.AppSettings["URL_INTERNO"].ToString()); //HttpUtility.UrlEncode(

                MailMessage msg = new MailMessage();
                msg.To.Add(new MailAddress(postulante.CorreoElectronico));
                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CCcorreoINC"].ToString()))
                    msg.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["CCcorreoINC"].ToString()));

                msg.From = new MailAddress(ConfigurationManager.AppSettings["correo"].ToString());
                msg.Subject = String.Format("{0} {1}-MIDIS", "Proceso de Convocatorias CAS", postulante.NombreProceso);
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
            if (tipo == "5")
            { //AL CONFIRMAR LA POSTULACION
                html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Correo/RegistroPostulacion.txt"));
                html = html.Replace("_POSTULANTE_", postulante.Nombre);
                html = html.Replace("_PROCESOCAS_", postulante.NombreProceso);
                //html = html.Replace("_URLINTERNO_", ConfigurationManager.AppSettings["URL_INTERNO"].ToString()); //HttpUtility.UrlEncode(

                if (!String.IsNullOrEmpty(postulante.CorreoElectronico)) {
                    MailMessage msg = new MailMessage();
                    msg.To.Add(new MailAddress(postulante.CorreoElectronico));
                    if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CCcorreoCAS"].ToString()))
                        msg.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["CCcorreoCAS"].ToString()));

                    msg.From = new MailAddress(ConfigurationManager.AppSettings["correo"].ToString());
                    msg.Subject = String.Format("{0} {1}-MIDIS", "Proceso de Convocatorias CAS", postulante.NombreProceso);
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
            if (tipo == "6")
            { //AL CONFIRMAR LA POSTULACION
                html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Correo/RegistroPostulacionServir.txt"));
                html = html.Replace("_POSTULANTE_", postulante.Nombre);
                html = html.Replace("_PROCESOCAS_", postulante.NombreProceso);
                //html = html.Replace("_URLINTERNO_", ConfigurationManager.AppSettings["URL_INTERNO"].ToString()); //HttpUtility.UrlEncode(

                if (!String.IsNullOrEmpty(postulante.CorreoElectronico))
                {
                    MailMessage msg = new MailMessage();
                    msg.To.Add(new MailAddress(postulante.CorreoElectronico));
                    if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CCcorreoCAS"].ToString()))
                        msg.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["CCcorreoCAS"].ToString()));

                    msg.From = new MailAddress(ConfigurationManager.AppSettings["correo"].ToString());
                    msg.Subject = String.Format("{0} {1}-MIDIS", "Proceso de Convocatorias Servicio Civil", postulante.NombreProceso);
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
            if (tipo == "9")
            { //ERROR DE CONEXION CON IRMA
                html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Correo/ErrorConsultaIrma.txt"));
                html = html.Replace("//mensaje", postulante.NombreProceso);
                
                MailMessage msg = new MailMessage();
                //msg.To.Add(new MailAddress(postulante.CorreoElectronico));
                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["usuariosoporte"].ToString()))
                    msg.To.Add(new MailAddress(ConfigurationManager.AppSettings["usuariosoporte"].ToString()));

                    msg.From = new MailAddress(ConfigurationManager.AppSettings["correo"].ToString());
                    msg.Subject = String.Format("{0} {1}-MIDIS", "Error ocurrido en Sistema de Convocatorias CAS", "Comunicación con IRMA");
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
        [Authorize]
        private Boolean SendEmailPracticas(Postulante_Registro postulante, String tipo)
        {
            Boolean exitoEnvio = false;
            String html = String.Empty;
            //if (tipo == "1")
            //{ //AL INICIAR EL PROCESO DE EVALUACION CAS
            //    html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Correo/InicioProceso.txt"));
            //    html = html.Replace("_POSTULANTE_", postulante.Nombre);
            //    html = html.Replace("_PROCESOCAS_", postulante.CodigoProceso);
            //    html = html.Replace("_URLEXTERNO_", ConfigurationManager.AppSettings["URL_EXTERNO"].ToString()); //HttpUtility.UrlEncode(

            //    if (!String.IsNullOrEmpty(postulante.CorreoElectronico))
            //    {
            //        MailMessage msg = new MailMessage();
            //        msg.To.Add(new MailAddress(postulante.CorreoElectronico));
            //        if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CCcorreoINC"].ToString()))
            //            msg.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["CCcorreoINC"].ToString()));

            //        msg.From = new MailAddress(ConfigurationManager.AppSettings["correo"].ToString());
            //        msg.Subject = String.Format("{0} {1}-MIDIS", "Proceso de Convocatorias CAS", postulante.CodigoProceso);
            //        msg.Body = html;
            //        msg.IsBodyHtml = true;

            //        SmtpClient clienteSmtp = new SmtpClient(ConfigurationManager.AppSettings["host"].ToString());
            //        clienteSmtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["usuarioemail"].ToString(), ConfigurationManager.AppSettings["contraseniaemail"].ToString());
            //        try
            //        {
            //            clienteSmtp.Send(msg);
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.Write(ex.Message);
            //            Console.ReadLine();
            //        }

            //        exitoEnvio = true;
            //    }
            //}
            //if (tipo == "2")
            //{ //AL RECIBIR LA DOCUMENTACION DEL POSTULANTE
            //    html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Correo/EvaluacionProceso.txt"));
            //    html = html.Replace("_PROCESOCAS_", postulante.NombreProceso + " - " + postulante.NombreCargo);
            //    html = html.Replace("_URLINTERNO_", ConfigurationManager.AppSettings["URL_INTERNO"].ToString()); //HttpUtility.UrlEncode(

            //    MailMessage msg = new MailMessage();
            //    msg.To.Add(new MailAddress(postulante.CorreoElectronico));
            //    if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CCcorreoINC"].ToString()))
            //        msg.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["CCcorreoINC"].ToString()));

            //    msg.From = new MailAddress(ConfigurationManager.AppSettings["correo"].ToString());
            //    msg.Subject = String.Format("{0} {1}-MIDIS", "Proceso de Convocatorias CAS", postulante.NombreProceso);
            //    msg.Body = html;
            //    msg.IsBodyHtml = true;

            //    SmtpClient clienteSmtp = new SmtpClient(ConfigurationManager.AppSettings["host"].ToString());
            //    clienteSmtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["usuarioemail"].ToString(), ConfigurationManager.AppSettings["contraseniaemail"].ToString());
            //    try
            //    {
            //        clienteSmtp.Send(msg);
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.Write(ex.Message);
            //        Console.ReadLine();
            //    }

            //    exitoEnvio = true;
            //}

            if (tipo == "5")
            { //AL CONFIRMAR LA POSTULACION
                html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Correo/RegistroPostulacionPracticas.txt"));
                html = html.Replace("_POSTULANTE_", postulante.Nombre);
                html = html.Replace("_PROCESOCAS_", postulante.NroColegiatura);
                //html = html.Replace("_URLINTERNO_", ConfigurationManager.AppSettings["URL_INTERNO"].ToString()); //HttpUtility.UrlEncode(

                if (!String.IsNullOrEmpty(postulante.CorreoElectronico))
                {
                    MailMessage msg = new MailMessage();
                    msg.To.Add(new MailAddress(postulante.CorreoElectronico));
                    if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CCcorreoCAS"].ToString()))
                        msg.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["CCcorreoCAS"].ToString()));

                    msg.From = new MailAddress(ConfigurationManager.AppSettings["correo"].ToString());
                    msg.Subject = String.Format("{0} {1}-MIDIS", "Proceso de Convocatorias de Prácticas", postulante.NroColegiatura);
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

    }
}