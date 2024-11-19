using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.Utiles.REST
{
    public static class RESTClienteHelper
    {
        public static T RecuperarREST<T>(string metodo,
            Dictionary<string, string> parametros, string direccion,
            bool requiereAutenticacion = false, string usuario = "",
            string clave = "")
        //where T : struct
        {

            T resultado;
            WebClient wc = new WebClient();

            if (requiereAutenticacion)
            {
                string credenciales = Convert.ToBase64String
               (ASCIIEncoding.ASCII.GetBytes
               (usuario + ":" + clave));
                wc.Headers.Add("Authorization", "Basic " + credenciales);
            }

            wc.Headers["Content-type"] = "application/json";
            StringBuilder sb = new StringBuilder();
            if (parametros != null && parametros.Count > 0)
            {
                int cantidad = 0;
                foreach (KeyValuePair<string, string> item in parametros)
                {
                    cantidad++;
                    if (cantidad == 1)
                    {
                        sb.Append("?" + item.Key + "=" + item.Value);
                    }
                    else
                    {
                        sb.Append("&" + item.Key + "=" + item.Value);
                    }
                }
            }
            string ruta = direccion + metodo + sb.ToString();
            var datos = wc.DownloadData(ruta);
            string download = System.Text.Encoding.UTF8.GetString(datos);
            
            var settings = new DataContractJsonSerializerSettings
            {
                DateTimeFormat = new System.Runtime.Serialization.DateTimeFormat("dd/MM/yyyy")
            };


            //string p = @"12\/11\/2014";
            //MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertDateStringToJsonDate);

            //Regex reg = new Regex(p);
            //download = reg.Replace(download, matchEvaluator);

            using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(download)))
            {
                DataContractJsonSerializer serializa = new DataContractJsonSerializer(typeof(T));
                resultado = (T)serializa.ReadObject(ms);
            }


            return resultado;
        }


        public static U EnviarDatosServicioREST<T, U>(string metodo,
            string accion, T entidadParametros, string ruta,
            bool requiereAutenticacion = false, string usuario = "",
            string clave = "")
            //where U : class
            where T : class
        {


            //HttpWebRequest request = (HttpWebRequest)HttpWebRequest.
            //    Create(ConfigurationManager.AppSettings["UrlServicio"] + metodo);


            string ub = ruta + metodo;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.
            Create(ruta + metodo);
            if (requiereAutenticacion)
            {
                string credenciales = Convert.ToBase64String
              (ASCIIEncoding.ASCII.GetBytes
              (usuario + ":" + clave));
                request.Headers.Add("Authorization", "Basic " + credenciales);
            }

            request.Method = accion;
            request.Accept = "application/json";
            request.ContentType = "application/json";
            var serializer = new DataContractJsonSerializer(typeof(T));

            var requestStream = request.GetRequestStream();
            serializer.WriteObject(requestStream, entidadParametros);

            requestStream.Close();
            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();

            var serializer2 = new DataContractJsonSerializer(typeof(U));
            U responseObject = (U)serializer2.ReadObject(responseStream);
            responseStream.Close();
            return responseObject;
        }


    }
}
