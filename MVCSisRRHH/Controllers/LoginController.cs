﻿//using MIDIS.UtilesWeb;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using MVCSisRRHH.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using System.Web.Security;
using System.Threading.Tasks;
using System.Security.Cryptography;
using MIDIS.ORI.LogicaNegocio;
using MIDIS.ORI.Entidades;
using MIDIS.ORI.Entidades.Auth;

namespace MVCSisRRHH.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            //SSOHelper.Login("/Boletas/Index");
            //return View();

            String AuthenticationUrl = ConfigurationManager.AppSettings.Get("LoginEndPoint");
            String iCodAplicacion = ConfigurationManager.AppSettings.Get("iCodAplicacion");

            return Redirect(AuthenticationUrl + "?id=" + iCodAplicacion);
        }

        //[HttpPost]
        //[AllowAnonymous]
        //public ActionResult Index(LoginSSO login, string ReturnUrl) {
        //    if (ModelState.IsValid) {
        //        mUsuarioResponse usuarioValido = ValidaUsuarioClave(login);

        //        if (usuarioValido != null && usuarioValido.iCodUsuario != 0) {
        //            var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        //            try
        //            {
        //                var claims = new[]{
        //                    new Claim(ClaimTypes.Name, login.NombreUsuario),
        //                    new Claim("InfoCompleta", JsonConvert.SerializeObject(usuarioValido))
        //                };
        //                // Setting    
        //                var claimIdenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
        //                HttpContext.GetOwinContext().Authentication.SignIn(
        //                new AuthenticationProperties { IsPersistent = false }, claimIdenties);
        //            }
        //            catch (Exception ex)
        //            {
        //                throw ex;
        //            }

        //            return RedirectToAction("Index", "Postulante");
        //            //return RedirectToAction("Listar", "Postulante");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("*", "USUARIO O CLAVE INCORRECTAS");
        //        }
        //    }

        //    return View();
        //}

        [HttpGet]
        public async Task<ActionResult> Connect(string access_token)
        {
            #region Perfiles - Cargos heredados de SISPAD
            //79  ABOGADO
            //80  SECRETARIO TECNICO DE LOS PROCESOS ADMINISTRATIVOS
            //81  ASISTENTE ADMINISTRATIVO
            //88  COORDINADOR
            #endregion

            #region Instancias
            ApiUser user;
            string[] token; // for split 3 parts
            string base64Content; // base64 ready for deserialize

            BE_TAB_Usuario oResponse = new BE_TAB_Usuario(); //Heredado de SISPAD
            BE_TAB_Persona opersona = new BE_TAB_Persona();
            #endregion

            try
            {
                token = access_token.Split('.');
                int mod4 = token[1].Length % 4;
                if (mod4 > 0)
                    token[1] += new string('=', 4 - mod4);
                
                base64Content = Encoding.UTF8.GetString(Convert.FromBase64String(token[1]));
                base64Content = base64Content.Replace("\"[", "[").Replace("]\"", "]").Replace("\\", "");
                user = JsonConvert.DeserializeObject<ApiUser>(base64Content);

                mUsuarioResponse _persona = new mUsuarioResponse();
                _persona.iCodUsuario = Int64.Parse(user.iCodUsuario);
                _persona.Persona = new mUsuarioPersona();
                //_persona.Persona.iIdTipDocumento = user.;
                _persona.Persona.vNroDocumento = user.vPersonaNroDocumento;
                _persona.Persona.vNombres = user.vPersonaNombres;
                _persona.Persona.vApePaterno = user.vPersonaApellidoPaterno;
                _persona.Persona.vApeMaterno = user.vPersonaApellidoMaterno;
                _persona.Persona.vEmail = user.vPersonaEmail;
                _persona.Persona.vCelular = user.vPersonaCelular;
                //opersona.vCargo = user.perfiles[0].vNomPer;
                //opersona.iCodPersona = user.iCodPersona;
                //oResponse.oPersona = opersona;

                try
                {
                    var claims = new[]{
                            new Claim(ClaimTypes.Name, user.vUsuarioNombre),
                            new Claim("InfoCompleta", JsonConvert.SerializeObject(_persona))
                        };
                    // Setting    
                    var claimIdenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                    HttpContext.GetOwinContext().Authentication.SignIn(
                    new AuthenticationProperties { IsPersistent = false }, claimIdenties);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
                //Session["iCodUsuario"] = user.iCodUsuario;
                //Session["Usuario"] = user.vUsuarioNombre;
                //Session["UsuarioIntegrado"] = user.bFlgUsuarioIntegrado;
                //Session["vCargo"] = user.perfiles[0].vNomPer;
                //Session["iCodEntidad"] = user.iCodEntidad;
                //Session["vEntidad"] = user.vEntidadRazonSocial;

                //opersona.vNombres = user.vPersonaNombres;
                //opersona.vApePaterno = user.vPersonaApellidoPaterno;
                //opersona.vCargo = user.perfiles[0].vNomPer;
                //opersona.iCodPersona = user.iCodPersona;
                //oResponse.oPersona = opersona;

                //Session["DatosUsuario"] = oResponse;
                //ObtenerSessionIpTerminal();
                //ObtenerSessionvNombreTerminal();

            }
            catch (Exception ex)
            {
                var x = 0;
            }
            return RedirectToAction("Index", "Postulante");
        }


        [HttpGet]
        public ActionResult ChangePasswordResult()
        {
            Session.Abandon();
            return View();

            //return RedirectToAction("Index", "Account");
        }
        [HttpGet]
        public ActionResult CancelChangePasswordResult()
        {
            return RedirectToAction("ExpedientesPAD", "Sispad");
        }

        [HttpPost]
        public ActionResult Login(BE_TAB_Usuario oParam)
        {
            BE_TAB_Usuario oResponse = null;
            BL_TAB_Usuario bl = new BL_TAB_Usuario();
            try
            {
                oResponse = bl.Autenticar(oParam);
                if (oResponse.MessageCode == "0000")
                {

                    Session["iCodUsuario"] = oResponse.iCodUsuario;
                    Session["Usuario"] = oParam.vUsuario;
                    Session["DatosUsuario"] = oResponse;

                    ObtenerSessionIpTerminal();
                    ObtenerSessionvNombreTerminal();
                    if (oParam.vUrlRedireccion != "")
                    {
                        oResponse.vRutaInicio = Url.Content("~/") + oParam.vUrlRedireccion;
                    }
                    else
                    {
                        oResponse.vRutaInicio = Url.Content("~/") + "Sispad/ExpedientesPAD";
                    }
                }
                else
                {
                    oResponse.vRutaInicio = "";
                }

                return Json(oResponse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult LogOut()
        {
            Session.Abandon();

            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            System.Web.HttpContext.Current.GetOwinContext().Authentication.SignOut(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie);
            FormsAuthentication.SignOut();
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            Request.GetOwinContext().Authentication.SignOut();

            Request.GetOwinContext().Authentication.SignOut(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie);
            authenticationManager.SignOut();
            HttpContext.Session.RemoveAll();
            HttpContext.Session.Clear();
            IOwinContext context = Request.GetOwinContext();
            IAuthenticationManager authenticationManager1 = context.Authentication;
            authenticationManager1.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("Index", "Account");
        }

        public ActionResult ChangePassword()
        {
            string AuthenticationUrl = ConfigurationManager.AppSettings.Get("LoginEndPoint");
            string ChangePasswordEndPoint = ConfigurationManager.AppSettings.Get("ChangePasswordEndPoint");
            string iCodAplicacion = ConfigurationManager.AppSettings.Get("iCodAplicacion");
            string iCodUsuario = VariablesWeb.ConsultaInformacion.iCodUsuario.ToString(); // Session["iCodUsuario"].ToString();
            string NombreUsuario = VariablesWeb.ConsultaInformacion.vNombreUsuario; // Session["Usuario"].ToString();

            return Redirect(AuthenticationUrl + ChangePasswordEndPoint + "?idApp=" + iCodAplicacion + "&idUsuario=" + iCodUsuario + "&NombreUsuario=" + NombreUsuario);
        }

        public void ObtenerSessionIpTerminal()
        {
            string ipaddress = string.Empty;
            try
            {
                ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ipaddress == "" || ipaddress == null)
                {
                    ipaddress = Request.ServerVariables["REMOTE_ADDR"];
                    if (ipaddress == "")
                    {
                        string HostName = Dns.GetHostName();
                        IPAddress[] ip = Dns.GetHostAddresses(HostName);
                        ipaddress = ip[1].ToString();
                    }
                }
            }
            catch (Exception)
            {
                ipaddress = "IpAddress no identificada";
            }

            Session["vIpTerminal"] = ipaddress;
        }


        public void ObtenerSessionvNombreTerminal()
        {
            string nombreTerminal = string.Empty;
            try
            {
                var strHostName = Dns.GetHostName();
                var ipEntry = Dns.GetHostEntry(strHostName);
                nombreTerminal = ipEntry.HostName;
            }
            catch (Exception)
            {
                nombreTerminal = "PC no identificada";
            }
            Session["vNombreTerminal"] = nombreTerminal;
        }
        [HttpPost]
        public ActionResult ChangePassword(BE_TAB_Usuario oParam)
        {

            BE_TAB_Usuario oResponse = null;
            try
            {
                BL_TAB_Usuario bl = new BL_TAB_Usuario();
                oResponse = bl.ObtenerDatosUsuario(oParam);

                if (oResponse != null)
                {
                    if (oResponse.oPersona.cCodDocPersona == oParam.oPersona.cCodDocPersona)
                    {
                        string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
    Request.ApplicationPath.TrimEnd('/');
                        oResponse.vRutaInicio = baseUrl + "/Account/ChangePasswordConfirm";
                        oResponse = bl.GenerarTokenCorreo(oResponse);
                    }
                    else
                    {
                        oResponse = new BE_TAB_Usuario();
                        oResponse.MessageCode = "9999";
                        oResponse.Message = "El DNI ingresado no coincide con el de su usuario";
                        return Json(oResponse, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    oResponse = new BE_TAB_Usuario();
                    oResponse.MessageCode = "9999";
                    oResponse.Message = "No existe el usuario ingresado";
                    return Json(oResponse, JsonRequestBehavior.AllowGet);
                }

                return Json(oResponse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult ChangePasswordConfirm(string token)
        {
            try
            {
                ViewBag.vToken = token;
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult ChangePasswordConfirm(BE_TAB_Usuario oParam)
        {

            BE_TAB_Usuario oResponse = null;
            try
            {
                BL_TAB_Usuario bl = new BL_TAB_Usuario();

                oResponse = bl.ActualizarContrasena(oParam);

                return Json(oResponse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //[HttpPost]
        //public ActionResult EnviarCorreoNotificacion(BE_TAB_Expediente oParam)
        //{
        //    try
        //    {
        //        BL_TAB_Expediente bl = new BL_TAB_Expediente();
        //        string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
        //        var lst = bl.EnviarCorreoEsperaRIVencida(new BE_TAB_Expediente(), baseUrl);
        //        return Json(lst, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //private mUsuarioResponse ValidaUsuarioClave(LoginSSO login)
        //{
        //    mUsuarioResponse _persona = null;
        //    //bool respuesta = false;
        //    try
        //    {
        //        login.ClaveNuevaTexto = Encriptar(login.Clave);

        //        List<Empleado_Registro> lista = new T_genm_postulante_LN().ValidarPostulante(login.NombreUsuario, login.TipoDocumento, login.ClaveNuevaTexto).ToList();
        //        if (lista.Count > 0) {
        //            _persona = new mUsuarioResponse();
        //            _persona.iCodUsuario = lista[0].IdEmpleado;
        //            _persona.Persona = new mUsuarioPersona();
        //            _persona.Persona.iIdTipDocumento = login.TipoDocumento;
        //            _persona.Persona.vNroDocumento = login.NombreUsuario;
        //        }

        //        return _persona;

        //        //using (HttpClient client = new HttpClient())
        //        //{
        //        //    var dict = new Dictionary<string, string>();
        //        //    var url = ConfigurationManager.AppSettings["urlToken"].ToString();
        //        //    dict.Add("client_id", ConfigurationManager.AppSettings["client_id"]);
        //        //    dict.Add("client_secret", ConfigurationManager.AppSettings["client_secret"]);
        //        //    dict.Add("grant_type", ConfigurationManager.AppSettings["grant_type"]);

        //        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        //    HttpResponseMessage response = client.PostAsync(url, new FormUrlEncodedContent(dict)).Result;
        //        //    if (response.StatusCode == HttpStatusCode.OK)
        //        //    {
        //        //        var result = response.Content.ReadAsStringAsync().Result;
        //        //        _resulttoken = JsonConvert.DeserializeObject<ResultToken>(result);
        //        //    }
        //        //    else
        //        //        return _persona;
        //        //}

        //        //using (var client = new HttpClient()) {
        //        //    var url = ConfigurationManager.AppSettings["urlServicios"].ToString();

        //        //    string myJson = "{'vNombreUsuario': '" + login.NombreUsuario + "','vClave':'" + login.Clave + "','iCodAplicacion':'" + ConfigurationManager.AppSettings["IdAplicacion"] + "'}";

        //        //    client.DefaultRequestHeaders.Add("Authorization", "bearer " + _resulttoken.access_token);
        //        //    client.DefaultRequestHeaders.Add("ContentType", "application/json");
        //        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        //    HttpResponseMessage response = client.PostAsync(url, new StringContent(myJson, Encoding.UTF8, "application/json")).Result;
        //        //    if (response.StatusCode == HttpStatusCode.OK)
        //        //    {
        //        //        try
        //        //        {
        //        //            var result = response.Content.ReadAsStringAsync().Result;
        //        //            _persona = JsonConvert.DeserializeObject<mUsuarioResponse>(result);
        //        //            //if (_persona.vCodigoRespuesta == "0000")
        //        //            //    respuesta = true;
        //        //            //else
        //        //            //{
        //        //                if (_persona.vCodigoRespuesta == "E1001") _persona.sMensaje = "No existe usuario";
        //        //                if (_persona.vCodigoRespuesta == "E1002") _persona.sMensaje = "Usuario bloqueado";
        //        //                if (_persona.vCodigoRespuesta == "E1003") _persona.sMensaje = "Contraseña incorrecta - AD";
        //        //                if (_persona.vCodigoRespuesta == "E1004") _persona.sMensaje = "Error en servidor de dominio";
        //        //                if (_persona.vCodigoRespuesta == "E1005") _persona.sMensaje = "Contraseña incorrecta - BD";
        //        //                if (_persona.vCodigoRespuesta == "E1006") _persona.sMensaje = "Error en servidor - BD";
        //        //            //    respuesta = false;
        //        //            //}
        //        //        }
        //        //        catch (JsonSerializationException)
        //        //        {
        //        //            _persona = new mUsuarioResponse();
        //        //            _persona.sMensaje = "El usuario se encuentra inactivo";
        //        //        }
                        
        //        //    }
        //        //    else
        //        //        return _persona;
        //        //}
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //    return _persona;
        //}

        //private string Encriptar(string str)
        //{
        //    SHA1 sha1 = SHA1Managed.Create();
        //    ASCIIEncoding encoding = new ASCIIEncoding();
        //    byte[] stream = null;
        //    StringBuilder sb = new StringBuilder();
        //    stream = sha1.ComputeHash(encoding.GetBytes(str));
        //    for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
        //    return sb.ToString();
        //}
        public ActionResult CerrarSesion()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            System.Web.HttpContext.Current.GetOwinContext().Authentication.SignOut(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie);
            FormsAuthentication.SignOut();
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            Request.GetOwinContext().Authentication.SignOut();

            Request.GetOwinContext().Authentication.SignOut(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie);
            authenticationManager.SignOut();
            HttpContext.Session.RemoveAll();
            HttpContext.Session.Clear();
            IOwinContext context = Request.GetOwinContext();
            IAuthenticationManager authenticationManager1 = context.Authentication;
            authenticationManager1.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index");
        }

        //public ActionResult CambioClave()
        //{
        //    //Request.Url.AbsoluteUri
        //    //string ruta = ConfigurationManager.AppSettings["RutaCambioClave"];
        //    return PartialView("_CambioClave");
        //}

        internal class ResultToken
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public string expires_in { get; set; }
        }
    }
}