using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MIDIS.ORI.Entidades;
using System.Security.Claims;
using System.Web;
using System.Web.Script.Serialization;

namespace MIDIS.UtilesWeb
{
    public static class VariablesWeb
    {

        public static List<T_gend_empresa_aplicacion> ListaEmpresaAplicacion
        {
            get
            {
                var claims = ((ClaimsIdentity)HttpContext.Current.User.Identity).Claims;
                if (claims != null)
                {
                    var ListaOpciones = claims.Count() > 1 ? claims.FirstOrDefault(
                        p => p.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/empresaaplicacion").
                        Value : "";

                    if (!string.IsNullOrEmpty(ListaOpciones))
                    {
                        List<T_gend_empresa_aplicacion> lista = (List<T_gend_empresa_aplicacion>)(new JavaScriptSerializer().Deserialize<List<T_gend_empresa_aplicacion>>(ListaOpciones));
                        lista = (from x in lista where x.Id_aplicacion == VariablesWeb.oIdAplicacion select x).ToList();
                        return lista;
                    }
                }
                return null;

            }
        }


        public static List<T_genm_operacion> ListaOperaciones
        {
            get
            {
                var claims = ((ClaimsIdentity)HttpContext.Current.User.Identity).Claims;
                if (claims != null)
                {
                    var ListaOpciones = claims.Count() > 1 ? claims.FirstOrDefault(
                        p => p.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/operaciones").
                        Value : "";

                    if (!string.IsNullOrEmpty(ListaOpciones))
                    {
                        List<T_genm_operacion> lista = (List<T_genm_operacion>)(new JavaScriptSerializer().Deserialize<List<T_genm_operacion>>(ListaOpciones));
                        return lista;
                    }
                }
                return null;

            }
        }


        public static List<T_gend_perfil_horario> ListaPerfilHorarios
        {
            get
            {
                var claims = ((ClaimsIdentity)HttpContext.Current.User.Identity).Claims;
                if (claims != null)
                {
                    var ListaOpciones = claims.Count() > 1 ? claims.FirstOrDefault(
                        p => p.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/perfilhorarios").
                        Value : "";

                    if (!string.IsNullOrEmpty(ListaOpciones))
                    {
                        List<T_gend_perfil_horario> lista = (List<T_gend_perfil_horario>)(new JavaScriptSerializer().Deserialize<List<T_gend_perfil_horario>>(ListaOpciones));
                        return lista;
                        //return lista.Where(x => x.Id_aplicacion == VariablesWeb.oIdAplicacion).ToList();
                    }
                }
                return null;

            }
        }

        /// <summary>
        /// Lista de Perfiles de Usuario que tiene el usuario validado
        /// </summary>
        public static List<T_gend_perfil_ip> ListaPerfilIps
        {
            get
            {
                var claims = ((ClaimsIdentity)HttpContext.Current.User.Identity).Claims;
                if (claims != null)
                {
                    var ListaPerfiles = claims.Count() > 1 ? claims.FirstOrDefault(
                        p => p.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/perfilip").
                        Value : "";

                    if (!string.IsNullOrEmpty(ListaPerfiles))
                    {
                        List<T_gend_perfil_ip> lista = (List<T_gend_perfil_ip>)(new JavaScriptSerializer().Deserialize<List<T_gend_perfil_ip>>(ListaPerfiles));
                        return lista.Where(x => x.Id_aplicacion == VariablesWeb.oIdAplicacion).ToList();
                    }
                }
                return null;

            }
        }

        /// <summary>
        /// Lista de Opciones de Menu o Paginas a las que tiene un usuario
        /// se basa en la aplicacion actual
        /// </summary>
        public static List<T_genm_opcion> ListaOpciones
        {
            get
            {
                var claims = ((ClaimsIdentity)HttpContext.Current.User.Identity).Claims;
                if (claims != null)
                {
                    var ListaOpciones = claims.Count() > 1 ? claims.FirstOrDefault(
                        p => p.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/opciones").
                        Value : "";

                    if (!string.IsNullOrEmpty(ListaOpciones))
                    {
                        List<T_genm_opcion> lista = (List<T_genm_opcion>)(new JavaScriptSerializer().Deserialize<List<T_genm_opcion>>(ListaOpciones));
                        return lista.Where(x => x.Id_aplicacion == VariablesWeb.oIdAplicacion).OrderBy(x => x.Nroorden).ToList();
                    }
                }
                return null;

            }
        }

        /// <summary>
        /// Lista de Perfiles de Usuario que tiene el usuario validado
        /// </summary>
        public static List<T_genm_perfil> ListaPerfiles
        {
            get
            {
                var claims = ((ClaimsIdentity)HttpContext.Current.User.Identity).Claims;
                if (claims != null)
                {
                    var ListaPerfiles = claims.Count() > 1 ? claims.FirstOrDefault(
                        p => p.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/perfiles").
                        Value : "";

                    if (!string.IsNullOrEmpty(ListaPerfiles))
                    {
                        List<T_genm_perfil> lista = (List<T_genm_perfil>)(new JavaScriptSerializer().Deserialize<List<T_genm_perfil>>(ListaPerfiles));
                        return lista.Where(x => x.Id_aplicacion == VariablesWeb.oIdAplicacion).ToList();
                    }
                }
                return null;

            }
        }

        /// <summary>
        /// Usuario validado, contiene informacion de codigo de usuario.
        /// Dentro esta la entidad donde muestra la informacion de Oficina, area, etc
        /// </summary>
        public static T_genm_usuario oT_Genm_Usuario
        {
            get
            {
                var claims = ((ClaimsIdentity)HttpContext.Current.User.Identity).Claims;
                if (claims != null)
                {
                    var UsuarioTexto = claims.Count() > 1 ? claims.FirstOrDefault(
                        p => p.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/usuario").
                        Value : "";

                    if (!string.IsNullOrEmpty(UsuarioTexto))
                    {
                        return (T_genm_usuario)(new JavaScriptSerializer().Deserialize(UsuarioTexto, typeof(T_genm_usuario)));
                    }
                }

                return null;
            }


        }

        /// <summary>
        /// Aplicacion en la que se encuentra validado el usuario
        /// </summary>
        public static int oIdAplicacion
        {
            get
            {
                if (HttpContext.Current.Session["IdAplicacion"] != null)
                {
                    return (int)HttpContext.Current.Session["IdAplicacion"];
                }
                return 0;
            }
            set { HttpContext.Current.Session["IdAplicacion"] = value; }
        }

        /// <summary>
        /// Direccion IP a donde se ha validado el usuario
        /// </summary>
        public static string oDireccionIP
        {
            get
            {
                var claims = ((ClaimsIdentity)HttpContext.Current.User.Identity).Claims;
                if (claims != null)
                {
                    var ip = claims.Count() > 1 ? claims.FirstOrDefault(
                        p => p.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/direccionip").
                        Value : "";

                    if (!string.IsNullOrEmpty(ip))
                    {
                        //string direccionIP = new JavaScriptSerializer().Deserialize<string>(ip);
                        return ip;
                    }
                }
                return "";

            }
        }

        public static string MensajeException { get; set; }

        /// <summary>
        /// Ruta del servidor donde actualmente estamos validados
        /// </summary>
        public static string RutaServidor
        {
            get
            {
                if (HttpContext.Current.Session["oRutaServidor"] != null)
                {
                    return (string)HttpContext.Current.Session["oRutaServidor"];
                }
                return "";
            }
            set { HttpContext.Current.Session["oRutaServidor"] = value; }
        }
    }
}
