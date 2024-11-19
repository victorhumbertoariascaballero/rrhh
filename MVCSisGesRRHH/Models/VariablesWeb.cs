using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MIDIS.ORI.Entidades;
using System.Security.Claims;
using System.Web;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace MVCSisGesRRHH.Models
{
    public static class VariablesWeb
    {
        static String claimTypeInfoCompleta = "InfoCompleta";

        public static mUsuarioResponse ConsultaInformacion
        {
            get
            {
                var claims = ((ClaimsIdentity)HttpContext.Current.User.Identity).Claims;

                mUsuarioResponse usuario = null;
                if (claims != null)
                {
                    var InfoCompleta = claims.Count() > 1 ? claims.FirstOrDefault(c => c.Type == claimTypeInfoCompleta).Value : "";
                    if (!string.IsNullOrEmpty(InfoCompleta))
                    {
                        mUsuarioResponse lista = JsonConvert.DeserializeObject<mUsuarioResponse>(InfoCompleta);
                        usuario = lista;
                    }
                }

                return usuario;
            }
        }

        public static List<mUsuarioOpciones> ListaOpciones
        {
            get
            {
                var claims = ((ClaimsIdentity)HttpContext.Current.User.Identity).Claims;

                List<mUsuarioOpciones> opciones = null;
                if (claims != null)
                {
                    var InfoCompleta = claims.Count() > 1 ? claims.FirstOrDefault(c => c.Type == claimTypeInfoCompleta).Value : "";

                    if (!string.IsNullOrEmpty(InfoCompleta))
                    {
                        mUsuarioResponse lista = JsonConvert.DeserializeObject<mUsuarioResponse>(InfoCompleta);
                        opciones = lista.Opciones;
                    }
                }

                return opciones;
            }
        }

        public static mUsuarioPersona oT_Genm_Usuario
        {
            get
            {
                var claims = ((ClaimsIdentity)HttpContext.Current.User.Identity).Claims;

                mUsuarioPersona _persona = null;
                if (claims != null)
                {
                    var InfoCompleta = claims.Count() > 1 ? claims.FirstOrDefault(c => c.Type == claimTypeInfoCompleta).Value : "";

                    if (!string.IsNullOrEmpty(InfoCompleta))
                    {
                        mUsuarioResponse lista = JsonConvert.DeserializeObject<mUsuarioResponse>(InfoCompleta);
                        _persona = lista.Persona;
                    }
                }
                    
                return _persona;
            }
        }

        public static List<mUsuarioPerfil> Perfil
        {
            get
            {
                var claims = ((ClaimsIdentity)HttpContext.Current.User.Identity).Claims;

                List<mUsuarioPerfil> _persona = null;
                if (claims != null)
                {
                    var InfoCompleta = claims.Count() > 1 ? claims.FirstOrDefault(c => c.Type == claimTypeInfoCompleta).Value : "";

                    if (!string.IsNullOrEmpty(InfoCompleta))
                    {
                        mUsuarioResponse lista = JsonConvert.DeserializeObject<mUsuarioResponse>(InfoCompleta);
                        _persona = lista.Perfil;
                    }
                }
                
                return _persona;
            }
        }

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
                        return ip;
                }
                
                return "";
            }
        }

    }
}
