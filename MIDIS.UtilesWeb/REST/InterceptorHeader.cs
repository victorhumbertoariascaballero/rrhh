using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System.Text;
using System.Web;


namespace MIDIS.UtilesWeb.REST
{
    public class InterceptorHeader : IDispatchMessageInspector
    {
        private const string BasicAutorizacion = "Basic";

        

        public object AfterReceiveRequest(ref Message request,
      IClientChannel channel,
      InstanceContext instanceContext)
        {

            if (WebOperationContext.Current.IncomingRequest.Headers["Authorization"] == null)
            {
                WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"myrealm\"");
                throw new WebFaultException<string>("Authorization no ha sido asignado.", HttpStatusCode.Unauthorized);
            }
            else
            {
                string usuarioClaveEncoded = ObtenerCredencialesEncodificadas();
                if (!string.IsNullOrEmpty(usuarioClaveEncoded))
                {
                    byte[] decodedBytes = null;
                    try
                    {
                        decodedBytes = Convert.FromBase64String(usuarioClaveEncoded);
                    }
                    catch (FormatException)
                    {
                        return false;
                    }

                    string credentials = ASCIIEncoding.ASCII.GetString(decodedBytes);

                    // Valida el Usuario y Password
                    string[] authParts = credentials.Split(':');
                    CustomUPV oCustomUPV = new CustomUPV();
                    oCustomUPV.Validate(authParts[0], authParts[1]);


                }

            }
            return null;

        }
        public void BeforeSendReply(ref Message reply, object
            correlationState)
        {
        }

        


        private static string ObtenerCredencialesEncodificadas()
        {
            WebOperationContext ctx = WebOperationContext.Current;


            string credsHeader = ctx.IncomingRequest.Headers[HttpRequestHeader.Authorization];
            if (credsHeader != null)
            {

                string creds = null;
                int credsPosicion = credsHeader.IndexOf(BasicAutorizacion, StringComparison.OrdinalIgnoreCase);
                if (credsPosicion != -1)
                {

                    credsPosicion += BasicAutorizacion.Length + 1;
                    if (credsPosicion < credsHeader.Length - 1)
                    {
                        creds = credsHeader.Substring(credsPosicion, credsHeader.Length - credsPosicion);
                        return creds;
                    }
                    return null;
                }
                else
                {
                    return null;
                }
            }


            return null;
        }

    }
}