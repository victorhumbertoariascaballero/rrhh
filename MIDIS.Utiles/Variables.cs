using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace MIDIS.Utiles
{
    public static class Variables
    {
        public const string ValorVerdadero = "1";
        public const string ValorFalso = "0";
        public const string UsuarioExterno = "UsuarioWeb";
        public const string MensajeObligatorio = "Dato obligatorio";

        public static DateTime FechaHoy
        {
            get
            {
                return TimeZoneInfo.ConvertTime(
                DateTime.Now, 
                TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));
            }
        }

        public static decimal MaxBytesAdjunto
        {
            get
            {
                decimal maxRequestLength = 0;
                HttpRuntimeSection section =
                ConfigurationManager.GetSection("system.web/httpRuntime") as HttpRuntimeSection;
                if (section != null)
                {
                    maxRequestLength = section.MaxRequestLength;
                }
                else
                {
                    maxRequestLength = 1024m;
                }
                return maxRequestLength;
            }
        }
    }
}
