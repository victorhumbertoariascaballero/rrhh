using MIDIS.Utiles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Services;
using System.IdentityModel.Services.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MIDIS.UtilesWeb
{
    public static class SSOHelper
    {
        /// <summary>
        /// Metodo que recuperar desde el QueryString el valor App
        /// Si tiene valor, lo asigna como IdAplicacion 
        /// Este valor servira para llenar las opciones del Menu
        /// </summary>
        public static void RecuperarIdAplicacion()
        {

            int IdAplicacion = VariablesWeb.oIdAplicacion;

            if (!string.IsNullOrEmpty(HttpContext.Current.Request["App"]))
            {
                byte[] app2 = Convert.FromBase64String(HttpContext.Current.Request["App"]);
                string codigo2 = EncriptacionHelper.Decrypt(app2);
                int.TryParse(codigo2, out IdAplicacion);
            }
            else
            {
                if (ConfigurationManager.AppSettings["IdAplicacion"] != null && VariablesWeb.oIdAplicacion == 0)
                {
                    IdAplicacion = int.Parse(ConfigurationManager.AppSettings["IdAplicacion"]);
                }
            }
            VariablesWeb.oIdAplicacion = IdAplicacion;
            //VariablesWeb.oDireccionIP = "192.168.1.1";
        }

        /// <summary>
        /// Metodo que Redirecciona la aplicacion hacia el SSO Centralizado
        /// Se requiere en el AppConfig la clave sso:uniqueId
        /// Se requeire en el AppConfig la clave returnURL que especifica la url de retorno
        /// </summary>
        public static void Login(string urlRetorno)
        {
            var uniqueId = ConfigurationManager.
                    AppSettings["sso:uniqueId"];

            var returnURL = ConfigurationManager.
                AppSettings["returnURL"] + urlRetorno;
            //Se llama al formulario del Servidor
            FederatedAuthentication.
                WSFederationAuthenticationModule.
                RedirectToIdentityProvider(uniqueId,
                returnURL, true);
        }

        /// <summary>
        /// Metodo que cierra la sesion del SSO
        /// Requiere la clave ida:Issuer en el AppConfig
        /// dicha clave especifica la ubicacion del SSO
        /// </summary>
        public static void Logout()
        {
            var urlIssuer = ConfigurationManager.
                    AppSettings["ida:Issuer"];

            //Cargar la configuracion
            FederationConfiguration configuracion =
                FederatedAuthentication.FederationConfiguration;

            //Cerrar Sesion con WIF
            WSFederationAuthenticationModule.
                FederatedSignOut(new Uri(urlIssuer),
                new Uri(configuracion.WsFederationConfiguration.Realm));

//            WSFederationAuthenticationModule.FederatedSignOut(
//new Uri(ConfigurationManager.AppSettings["ida:Issuer"]),
//new Uri(configuracion.WsFederationConfiguration.Realm));
        }
    }
}
