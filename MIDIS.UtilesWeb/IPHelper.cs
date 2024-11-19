using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MIDIS.UtilesWeb
{
    /// <summary>
    /// Clase estatica gestiona lo relacionado a las IP
    /// </summary>
    public static class IPHelper
    {
        /// <summary>
        /// Metodo estatico que retorna la IP del cliente Web que esta accediendo
        /// </summary>
        /// <returns></returns>
        public static string ObtenerIPCliente()
        {
            string ipV4V6Cliente = String.Empty, ipFamilia = "InterNetwork";

            foreach (IPAddress ipNodo in Dns.GetHostAddresses(HttpContext.Current.Request.UserHostAddress))
            {
                if (ipNodo.AddressFamily.ToString().Equals(ipFamilia))
                {
                    ipV4V6Cliente = ipNodo.ToString();
                    break;
                }
            }

            if (!String.IsNullOrEmpty(ipV4V6Cliente)) return ipV4V6Cliente;

            foreach (IPAddress ipNodo in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (ipNodo.AddressFamily.ToString().Equals(ipFamilia))
                {
                    ipV4V6Cliente = ipNodo.ToString();
                    break;
                }

            }

            return ipV4V6Cliente;
        }
    }
}
