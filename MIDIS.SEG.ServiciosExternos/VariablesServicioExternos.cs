using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.SEG.ServiciosExternos
{
    public static class VariablesServicioExternos
    {

        #region Servicios Administrados
        public static string UrlServicioAdministrado
        {
            get
            {
                if (ConfigurationManager.AppSettings["UrlServicioAdministrado"] != null)
                {
                    return ConfigurationManager.AppSettings["UrlServicioAdministrado"];
                }
                return "";
            }
        }
        public static string UsuarioServicioAdministrado
        {
            get
            {
                if (new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.AppSettings["UsuarioServicioAdministrado"]) != null)
                {
                    return new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.AppSettings["UsuarioServicioAdministrado"]);
                }
                return "";
            }
        }
        public static string ClaveServicioAdministrado
        {
            get
            {
                if (new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.AppSettings["ClaveServicioAdministrado"]) != null)
                {
                    return new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.AppSettings["ClaveServicioAdministrado"]);
                }
                return "";
            }
        }



        #endregion
    }

}
