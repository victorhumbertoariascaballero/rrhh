using System;
using System.Collections.Generic;
using System.IdentityModel.Protocols.WSTrust;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MIDIS.Autenticacion
{
    public static class AuthPassiveO365
    {
        public static bool ValidatePassiveAuthO365(string userO365, string passO365)
        {
            bool authO365 = false;
            string office365STS = "https://login.microsoftonline.com/extSTS.srf";
            string portalO365 = "https://portal.office.com/admin/default.aspx";
            try
            {
                RequestSecurityToken rst = new RequestSecurityToken
                {
                    RequestType = RequestTypes.Issue,
                    AppliesTo = new EndpointReference(portalO365),
                    KeyType = KeyTypes.Bearer,
                    TokenType = System.IdentityModel.Tokens.SecurityTokenTypes.Saml
                };
                WSTrustFeb2005RequestSerializer trustSerializer = new WSTrustFeb2005RequestSerializer();
                WSHttpBinding binding = new WSHttpBinding();
                binding.Security.Mode = SecurityMode.TransportWithMessageCredential;
                binding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;
                binding.Security.Message.EstablishSecurityContext = false;
                binding.Security.Message.NegotiateServiceCredential = false;
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
                EndpointAddress address = new EndpointAddress(office365STS);
                using (WSTrustFeb2005ContractClient trustClient = new WSTrustFeb2005ContractClient(binding, address))
                {
                    trustClient.ClientCredentials.UserName.UserName = userO365;
                    trustClient.ClientCredentials.UserName.Password = passO365;
                    Message response = trustClient.EndIssue(
                        trustClient.BeginIssue(
                            Message.CreateMessage(
                                MessageVersion.Default,
                                "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Issue",
                                new RequestBodyWriter(trustSerializer, rst)
                            ),
                            null,
                            null));
                    trustClient.Close();
                    authO365 = true;

                    //KMM COMENTADO PARA REVISION POR CAMBIOS EN OFFICE 365
                    //using (XmlDictionaryReader reader = response.GetReaderAtBodyContents())
                    //{
                    //    string respuesta = reader.ReadOuterXml();
                    //    if (respuesta.IndexOf("wst:BinarySecret") > -1) authO365 = true;
                    //}
                }
            }
            catch (Exception ex)
            {
                authO365 = false;

            }
            return authO365;

        }
    }
}
