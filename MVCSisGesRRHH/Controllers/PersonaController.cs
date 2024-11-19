using MIDIS.ORI.Entidades;
using MIDIS.ORI.LogicaNegocio;
using MIDIS.Utiles;
//using MIDIS.UtilesMVC;
//using MIDIS.UtilesWeb;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSisGesRRHH.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using MVCSisGesRRHH.Controllers.IRMA_Autentica;
using MVCSisGesRRHH.Controllers.IRMA_Reniec;
using System.Threading.Tasks;

namespace MVCSisGesRRHH.Controllers
{
    public class PersonaController : Controller
    {
        private readonly T_genm_persona_LN _persona_Servicio = new T_genm_persona_LN();
        //private readonly T_genm_enumerado_detalle_LN _enumeradoDetalle_Servicio = new T_genm_enumerado_detalle_LN();
        private readonly T_genm_ubigeo_LN _ubigeo_Servicio = new T_genm_ubigeo_LN();

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
        public JsonResult ListarTipoDeDocumento()
        {
            //EnumeradoDetalle_Request enumeradoDetalle_Request = new EnumeradoDetalle_Request();
            //enumeradoDetalle_Request.Grilla = new Grilla_Request();
            //enumeradoDetalle_Request.Grilla.PaginaActual = 0;
            //enumeradoDetalle_Request.Grilla.OrdenarPor = "TEXTO_DETALLE";
            //enumeradoDetalle_Request.Grilla.OrdenarDeForma = "ASC";
            //enumeradoDetalle_Request.EnumeradoCabecera = new EnumeradoCabecera_Request();

            //enumeradoDetalle_Request.EnumeradoCabecera.Nombre = "PERSONA_TIPO_DE_DOCUMENTO";
            //var respuesta = _enumeradoDetalle_Servicio.Listar(enumeradoDetalle_Request);

            //return Json(respuesta, JsonRequestBehavior.AllowGet);

            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("01", "DNI"));

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarSexo()
        {
            //EnumeradoDetalle_Request enumeradoDetalle_Request = new EnumeradoDetalle_Request();
            //enumeradoDetalle_Request.Grilla = new Grilla_Request();
            //enumeradoDetalle_Request.Grilla.PaginaActual = 0;
            //enumeradoDetalle_Request.Grilla.OrdenarPor = "TEXTO_DETALLE";
            //enumeradoDetalle_Request.Grilla.OrdenarDeForma = "ASC";
            //enumeradoDetalle_Request.EnumeradoCabecera = new EnumeradoCabecera_Request();

            //enumeradoDetalle_Request.EnumeradoCabecera.Nombre = "PERSONA_SEXO";
            //var respuesta = _enumeradoDetalle_Servicio.Listar(enumeradoDetalle_Request);

            //return Json(respuesta, JsonRequestBehavior.AllowGet);

            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("1", "FEMENINO"));
            lista.Add(new Estado_Response("2", "MASCULINO"));

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarEstadoCivil()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("1", "SOLTERO(A)"));
            lista.Add(new Estado_Response("2", "CASADO(A)"));
            lista.Add(new Estado_Response("3", "DIVORCIADO(A)"));
            lista.Add(new Estado_Response("4", "VIUDO(A)"));

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarGrupoSanguineo()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("1", "O-"));
            lista.Add(new Estado_Response("2", "O+"));
            lista.Add(new Estado_Response("3", "A-"));
            lista.Add(new Estado_Response("4", "A+"));
            lista.Add(new Estado_Response("5", "B-"));
            lista.Add(new Estado_Response("6", "B+"));
            lista.Add(new Estado_Response("7", "AB-"));
            lista.Add(new Estado_Response("8", "AB+"));

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public JsonResult ListarSede()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("1", "SEDE CENTRAL"));
            lista.Add(new Estado_Response("2", "SEDE PALACIO"));

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarBancos(string term)
        {
            Banco_Request peticion = new Banco_Request();
            peticion.IdBanco = 0;
            peticion.Nombre = "%" + (String.IsNullOrEmpty(term) ? "" : term.ToUpper()) + "%";
            object respuesta = _persona_Servicio.ListarBancos(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarPerfilNivel()
        {
            Perfil_Nivel peticion = new Perfil_Nivel();
            object respuesta = _persona_Servicio.ListarPerfilNivel();
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarEstado()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("1", "ACTIVO"));
            lista.Add(new Estado_Response("0", "INACTIVO"));

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        //[AutorizacionFilter(Roles = "*")]
        public JsonResult ListarIdUbigeo()
        {
            Ubigeo_Request ubigeo_Request = new Ubigeo_Request();
            ubigeo_Request.Grilla = new Grilla_Request();
            ubigeo_Request.Grilla.RegistrosPorPagina = 0;
            ubigeo_Request.Grilla.OrdenarPor = "CODIGO_INEI";
            ubigeo_Request.Grilla.OrdenarDeForma = "ASC";
            object respuesta = _ubigeo_Servicio.Listar(ubigeo_Request);

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        //[AutorizacionFilter(Roles = "*")]
        public JsonResult Listar(Persona_Request peticion)
        {
            peticion.RegistroEstaEliminado = null;
            peticion.TieneDocumento = null;
            object respuesta = _persona_Servicio.Listar(peticion);

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[AutorizacionFilter(Roles = "*")]
        public JsonResult Validar(Persona_Registro registro)
        {
            //JObject autenticadoDeCookie = JObject.Parse(CookieSeguridadHelper.ObtenerAutenticado().ToString());
            registro.RegistroUsuarioCreacion = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iCodUsuario); ;
            registro.RegistroFechaCreacion = DateTime.Now;
            registro.RegistroIpCreacion = Request.UserHostAddress;
            object respuesta = _persona_Servicio.Validar(registro);

            return Json(respuesta);
        }

        [HttpPost]
        //[AutorizacionFilter(Roles = "*")]
        public JsonResult Registrar(Persona_Registro registro)
        {
            //JObject autenticadoDeCookie = JObject.Parse(CookieSeguridadHelper.ObtenerAutenticado().ToString());
            registro.RegistroUsuarioCreacion = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iCodUsuario); ;
            registro.RegistroFechaCreacion = DateTime.Now;
            registro.RegistroIpCreacion = Request.UserHostAddress;

            object respuesta = _persona_Servicio.Insertar(registro);

            return Json(respuesta);
        }

        [HttpPost]
        //[AutorizacionFilter(Roles = "*")]
        public JsonResult ObtenerParaMostrar(Persona_Registro registro)
        {
            object respuesta = _persona_Servicio.Obtener(registro);

            return Json(respuesta);
        }

        [HttpPost]
        //[AutorizacionFilter(Roles = "*")]
        public JsonResult ObtenerParaEditar(Persona_Registro registro)
        {
            object respuesta = _persona_Servicio.Obtener(registro);

            return Json(respuesta);
        }

        [HttpPost]
        //[AutorizacionFilter(Roles = "*")]
        public JsonResult Guardar(Persona_Registro registro)
        {
            //JObject autenticadoDeCookie = JObject.Parse(CookieSeguridadHelper.ObtenerAutenticado().ToString());
            registro.RegistroUsuarioModificacion = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iCodUsuario);
            registro.RegistroFechaModificacion = DateTime.Now;
            registro.RegistroIpModificacion = Request.UserHostAddress;

            _persona_Servicio.Actualizar(registro);

            return Json(true);
        }

        [HttpPost]
        //[AutorizacionFilter(Roles = "*")]
        public JsonResult GuardarEstaActivo(Persona_Registro registro)
        {
            //JObject autenticadoDeCookie = JObject.Parse(CookieSeguridadHelper.ObtenerAutenticado().ToString());
            registro.RegistroUsuarioModificacion = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iCodUsuario);
            registro.RegistroFechaModificacion = DateTime.Now;
            registro.RegistroIpModificacion = Request.UserHostAddress;

            _persona_Servicio.ActualizarEstaActivo(registro);

            return Json(true);
        }

        [HttpPost]
        //[AutorizacionFilter(Roles = "*")]
        public JsonResult ConfirmarEliminacion(Persona_Registro registro)
        {
            List<string> listaMensaje = new List<string>();

            object respuesta = new
            {
                permite = true,
                listaMensaje = listaMensaje

            };
            return Json(respuesta);
        }

        [HttpPost]
        //[AutorizacionFilter(Roles = "*")]
        public JsonResult Eliminar(Persona_Registro registro)
        {
            //JObject autenticadoDeCookie = JObject.Parse(CookieSeguridadHelper.ObtenerAutenticado().ToString());
            registro.RegistroUsuarioModificacion = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iCodUsuario); ;
            registro.RegistroFechaModificacion = DateTime.Now;
            registro.RegistroIpModificacion = Request.UserHostAddress;
            registro.RegistroEstaEliminado = true;

            _persona_Servicio.ActualizarEstaEliminado(registro);
            return Json(true);
        }

        public async Task<JsonResult> Buscar()
        {
            //Buscar Persona
            String vNumDocum = Request["vNumDocum"].ToString();
            //decimal nIdiPaisSolic = Int32.Parse(Request["nIdiPaisSolic"]);
            String tipoDoc = Request["tipoDoc"].ToString();
            Persona_Registro maePersona = new Persona_Registro();

            try
            {
                //maePersona = obtenerDesdeWS_RENIEC(vNumDocum);
                maePersona = await obtenerDesdeWS_IRMA(vNumDocum);

                return Json(maePersona, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                maePersona.IdPersona = -1;
                maePersona.Nombres = ex.Message;
                return Json(maePersona, JsonRequestBehavior.AllowGet);
            }

        }

        //private Persona_Registro obtenerDesdeWS_PIDE(String vNumDocum)
        //{
        //    Persona_Registro ori = new Persona_Registro();

        //    try
        //    {
        //        reniec.pe.gob.midis.app_pruebas.ReniecPersona_Servicio oClient = new reniec.pe.gob.midis.app_pruebas.ReniecPersona_Servicio();
        //        reniec.pe.gob.midis.app_pruebas.ReniecPersona_Request Request = new reniec.pe.gob.midis.app_pruebas.ReniecPersona_Request();
        //        reniec.pe.gob.midis.app_pruebas.ReniecPersona_Response Response = new reniec.pe.gob.midis.app_pruebas.ReniecPersona_Response();

        //        Request.NumeroDeDocumento = vNumDocum;
        //        Request.Usuario = ConfigurationManager.AppSettings.Get("UsuarioSW");
        //        Request.Clave = ConfigurationManager.AppSettings.Get("ClaveSW");

        //        Response = oClient.ConsultarPorNumeroDeDocumento(Request);

        //        try
        //        {
        //            ori.coResultado = Response.Codigo;
        //            ori.mensaje = Response.Mensaje;
        //            if (Response.Codigo != "NHC") {
        //                ori.Nombres = Response.PersonaDato.Nombres; //vPriNombre 
        //                ori.ApellidoPaterno = Response.PersonaDato.ApellidoPaterno; //vPriApellido
        //                ori.ApellidoMaterno = Response.PersonaDato.ApellidoMaterno; //vSegApellido
        //                ori.DireccionDomicilio = Response.PersonaDato.DireccionDomicilio; //vSegApellido
        //                ori.Ubigeo = new Ubigeo_Registro();
        //                ori.Ubigeo.CodigoReniec = String.Format("{0}{1}{2}", Response.PersonaDato.UbigeoDptoDomicilio, Response.PersonaDato.UbigeoProvDomicilio, Response.PersonaDato.UbigeoDistDomicilio);
        //                ori.NumeroDeDocumento = vNumDocum; //vNumDocum
        //                try
        //                {
        //                    string xFecha = Response.PersonaDato.FechaNacimiento;
        //                    string dia = xFecha.Substring(6, 2);
        //                    string mes = xFecha.Substring(4, 2);
        //                    string anno = xFecha.Substring(0, 4);

        //                    xFecha = "{" + dia + "/" + mes + "/" + anno + " 00:00:00}";
        //                    var x = xFecha;
        //                    DateTime thisDate = new DateTime(Int16.Parse(anno), Int16.Parse(mes), Int16.Parse(dia));
        //                    ori.FechaDeNacimiento = thisDate; //dtFecNacim
        //                                                      //LLENAR DIRECCION 

        //                    List<Ubigeo_Response> lstUbigeo = new T_genm_ubigeo_LN().Listar(new Ubigeo_Request() { CodigoReniec = ori.Ubigeo.CodigoReniec }).ToList();
        //                    if (lstUbigeo != null)
        //                    {
        //                        if (lstUbigeo.Count > 0)
        //                            ori.Ubigeo.IdUbigeo = lstUbigeo[0].IdUbigeo;
        //                    }
        //                }
        //                catch (Exception)
        //                {

        //                }

        //                ori.Sexo = (Response.PersonaDato.Sexo == "1" ? "2" : (Response.PersonaDato.Sexo == "2" ? "1" : String.Empty));

        //            }

        //            //ori.nIdeTipoDocum = 37;
        //            //ori.nIdeCodPaisDoc = 73;
        //            //ori.dtFecIniVig = DateTime.Now;
        //            //ori.nIdeUsuReg = 1;
        //            //ori.dtFecReg = DateTime.Now;
        //            //ori.nIdeTipoPer = 12;

        //        }
        //        catch (Exception)
        //        {

        //        }
        //        return ori;

        //    }
            
        //    catch (Exception ex)
        //    {
        //        //mReniec.mensaje = ex.Message;
        //    }
        //    return null;
        //}
        //public Persona_Registro obtenerDesdeWS_RENIEC(string xDNI)
        //{
        //    Persona_Registro mReniec = new Persona_Registro();

        //    try
        //    {
        //        //reniec.pe.gob.pide.ws5.ReniecConsultaDni oClient = new reniec.pe.gob.pide.ws5.ReniecConsultaDni();
        //        //reniec.pe.gob.pide.ws5.resultadoConsulta oResult = new reniec.pe.gob.pide.ws5.resultadoConsulta();
        //        //reniec.pe.gob.pide.ws5.peticionConsulta args = new reniec.pe.gob.pide.ws5.peticionConsulta();

        //        reniec.pe.gob.midis.app1.ReniecPersonaFoto_Servicio oClient = new reniec.pe.gob.midis.app1.ReniecPersonaFoto_Servicio();
        //        reniec.pe.gob.midis.app1.ReniecPersonaFoto_Request args = new reniec.pe.gob.midis.app1.ReniecPersonaFoto_Request(); 
        //        reniec.pe.gob.midis.app1.ReniecPersonaFoto_Response oResult = new reniec.pe.gob.midis.app1.ReniecPersonaFoto_Response();

        //        try
        //        {
        //            args.NumeroDeDocumento = xDNI;
        //            args.Usuario = ConfigurationManager.AppSettings.Get("UsuarioSW");
        //            args.Clave = ConfigurationManager.AppSettings.Get("ClaveSW");

        //            oResult = oClient.ConsultarPorNumeroDeDocumento(args);
                    
        //            mReniec.mensaje = oResult.Mensaje;      // deResultado;
        //            mReniec.coResultado = oResult.Codigo;  // coResultado;

        //            if (oResult.Codigo == "0000") {
        //                mReniec.ApellidoPaterno = oResult.PersonaFotoDato.PrimerApellido;
        //                mReniec.ApellidoMaterno = oResult.PersonaFotoDato.SegundoApellido;
        //                mReniec.Nombres = oResult.PersonaFotoDato.PreNombres;
        //                mReniec.EstadoCivil = oResult.PersonaFotoDato.EstadoCivil;
        //                mReniec.Foto = oResult.PersonaFotoDato.Foto;
        //                mReniec.sFoto = Convert.ToBase64String(oResult.PersonaFotoDato.Foto);
        //                mReniec.DescripcionUbigeo = oResult.PersonaFotoDato.Ubigeo.Replace("/", " / ");
        //                mReniec.DireccionDomicilio = oResult.PersonaFotoDato.Direccion;
        //                mReniec.EstadoCivil = (oResult.PersonaFotoDato.EstadoCivil.IndexOf("SOLTER") >= 0 ? "1" : (oResult.PersonaFotoDato.EstadoCivil.IndexOf("CASAD") >= 0 ? "2" : (oResult.PersonaFotoDato.EstadoCivil.IndexOf("DIVORCIAD") >= 0 ? "3" : (oResult.PersonaFotoDato.EstadoCivil.IndexOf("VIUD") >= 0 ? "4" : string.Empty))));

        //                //mReniec.restriccion = oResult.datosPersona.restriccion;

        //                Persona_Registro maePersonaAux = obtenerDesdeWS_PIDE(xDNI);
        //                if (maePersonaAux.FechaDeNacimiento != null) mReniec.FechaDeNacimiento = maePersonaAux.FechaDeNacimiento;
        //                if (maePersonaAux.Sexo != null) mReniec.Sexo = maePersonaAux.Sexo;
        //                if (maePersonaAux.Ubigeo != null)
        //                {
        //                    mReniec.Ubigeo = new Ubigeo_Registro();
        //                    mReniec.Ubigeo.CodigoReniec = maePersonaAux.Ubigeo.CodigoReniec;
        //                }
        //            }
                    
        //        }
        //        catch (Exception)
        //        {

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        mReniec.mensaje = ex.Message;
        //    }

        //    return mReniec;
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
                        if (resultadoAutentica.codigo == "2000") {
                            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", resultadoAutentica.data.token);
                            HttpResponseMessage responseReniec = await cliente.PostAsync(IRMA_WS_ReniecUri, jsonContentRequest);

                            if (responseReniec.IsSuccessStatusCode) {
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
                                else { 
                                //en caso de error en la consulta 
                                }
                            }
                        }
                        else { 
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

        //private Persona_Registro obtenerDesdeWS_CE(String vNumDocum)
        //{
        //    Persona_Registro ori = new Persona_Registro();

        //    pe.gob.midis.sdv_CE.MigracionesCE_Request peticion = new pe.gob.midis.sdv_CE.MigracionesCE_Request();
        //    peticion.Usuario = ConfigurationManager.AppSettings.Get("UsuarioSW");
        //    peticion.Clave = ConfigurationManager.AppSettings.Get("ClaveSW");
        //    peticion.NumeroDeDocumento = vNumDocum;

        //    pe.gob.midis.sdv_CE.MigracionesCE_Servicio cliente = new pe.gob.midis.sdv_CE.MigracionesCE_Servicio();

        //    pe.gob.midis.sdv_CE.MigracionesCE_Response respuesta = cliente.ConsultarPorNumeroDeDocumento(peticion);

        //    ori.Nombres = respuesta.CEDato.Nombres;
        //    ori.ApellidoMaterno = respuesta.CEDato.SegundoApellido;
        //    ori.ApellidoPaterno = respuesta.CEDato.PrimerApellido;

        //    return ori;
        //}

        //private Persona_Registro obtenerDesdeWS_RUC(String vNumDocum)
        //{
        //    Persona_Registro ori = new Persona_Registro();

        //    pe.gob.midis.sdv_RUC.SunatContribuyente_Request peticion = new pe.gob.midis.sdv_RUC.SunatContribuyente_Request();
        //    peticion.Usuario = ConfigurationManager.AppSettings.Get("UsuarioSW");
        //    peticion.Clave = ConfigurationManager.AppSettings.Get("ClaveSW");
        //    peticion.NumeroDeRUC = vNumDocum;

        //    pe.gob.midis.sdv_RUC.SunatContribuyente_Servicio cliente = new pe.gob.midis.sdv_RUC.SunatContribuyente_Servicio();

        //    pe.gob.midis.sdv_RUC.SunatContribuyente_Response respuesta = cliente.ConsultarPorNumeroDeRUC(peticion);

        //    ori.Nombres = respuesta.ContribuyenteDato.RazonSocial;
        //    return ori;

        //}

        //public string ConvertirCodRENIECtoCodINEILOCAL(string CodReniec)
        //{
        //    string CodINEILocal;
        //    rUbigeo Repository = new rUbigeo();

        //    CodINEILocal = Repository.Get_CodINEI_Local(CodReniec);
        //    if (CodINEILocal.Length > 0)
        //    {

        //    }
        //    else
        //    {
        //        CodINEILocal = "010302";
        //    }


        //    return CodINEILocal;
        //}

        [HttpPost]
        public virtual JsonResult HistorialAtencion()
        {
            String vNumDocum = Request["vNumDocum"].ToString();
            decimal nIdiPaisSolic = Int32.Parse(Request["nIdiPaisSolic"]);
            decimal tipoDoc = Int32.Parse(Request["tipoDoc"]);

            try
            {
                var jsonResponse = new JsonResponse();
                //var solicitudHelper = new SolicitudHelper();
                //var items = SQLHelper.executeStore("paORIHistorialAtencionPersona", new String[] { vNumDocum, nIdiPaisSolic.ToString(), tipoDoc.ToString() }, new String[] { "vNumDocum", "nIdiPaisSolic", "tipoDoc" }, new SqlDbType[] { System.Data.SqlDbType.NVarChar, System.Data.SqlDbType.NVarChar, System.Data.SqlDbType.NVarChar });

                jsonResponse.Success = true;
                jsonResponse.Data = "prueba"; // items;
                return Json(jsonResponse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse { Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }


    public class JsonResponse
    {
        public string Message { get; set; }

        public bool Success { get; set; }

        public object Data { get; set; }
    }

    namespace IRMA_Autentica
    {
        public class Data
        {
            public string token { get; set; }
        }

        public class RootAutentica
        {
            public string codigo { get; set; }
            public string mensaje { get; set; }
            public Data data { get; set; }
        }
    }
    namespace IRMA_Reniec
    {
        public class Data
        {
            public string numDoc { get; set; }
            public string apPaterno { get; set; }
            public string apMaterno { get; set; }
            public string apCasada { get; set; }
            public string nombres { get; set; }
            public string direccion { get; set; }
            public string estadoCivil { get; set; }
            public string restriccion { get; set; }
            public string ubigeo { get; set; }
            public string foto { get; set; }
            public string ubigeoDepartamentoDomicilio { get; set; }
            public string ubigeoProvinciaDomicilio { get; set; }
            public string ubigeoDistritoDomicilio { get; set; }
            public string departamentoDomicilio { get; set; }
            public string provinciaDomicilio { get; set; }
            public string distritoDomicilio { get; set; }
            public string direccionDomicilio { get; set; }
            public string sexo { get; set; }
            public string fechaNacimiento { get; set; }
        }

        public class RootReniec
        {
            public string codigo { get; set; }
            public string mensaje { get; set; }
            public Data data { get; set; }
        }
    }
    
}