using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security;

namespace MIDIS.Utiles.WCF
{
    internal sealed class ConfigHelperWCF
    {
        private readonly string _serviceName;
        private readonly UriType _tipoUri;
        private Binding _binding;
        private EndpointAddress _address;
        private readonly Uri _uri;
        private readonly bool _transactionFlow;

        public ConfigHelperWCF(string serviceName, bool transactionFlow)
        {
            _serviceName = serviceName;
            _uri = ConexionHelperWCF.GetUri(serviceName);
            _tipoUri = ConexionHelperWCF.GetUriType(_uri);
            _transactionFlow = transactionFlow;
            Inicializar();
        }



        private void Inicializar()
        {

            _address = new EndpointAddress(_uri, CrearIdentidad(), AddressHeader.CreateAddressHeader("url", "", 1));

            switch (_tipoUri)
            {
                case UriType.Http:
                    // Crea el Binding HTTP
                    WSHttpBinding bindigHttp = new WSHttpBinding();

                    bindigHttp.Security.Mode = SecurityMode.None;
                    bindigHttp.Security.Message.ClientCredentialType = MessageCredentialType.None;

                    bindigHttp.MaxBufferPoolSize = int.MaxValue;
                    bindigHttp.MaxReceivedMessageSize = int.MaxValue;
                    bindigHttp.ReaderQuotas.MaxArrayLength = int.MaxValue;
                    bindigHttp.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
                    bindigHttp.ReaderQuotas.MaxDepth = int.MaxValue;
                    bindigHttp.ReaderQuotas.MaxNameTableCharCount = int.MaxValue;
                    bindigHttp.ReaderQuotas.MaxStringContentLength = int.MaxValue;
                    bindigHttp.TransactionFlow = _transactionFlow;
                    _binding = bindigHttp;
                    break;
                case UriType.Https:
                    // Crea el Binding HTTP
                    var bindigHttps = new WSHttpBinding();

                    bindigHttps.Security.Mode = SecurityMode.Transport;
                    bindigHttps.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
                    //bindigHttps.Security.Transport.ProtectionLevel = ProtectionLevel.EncryptAndSign;

                    bindigHttps.MaxBufferPoolSize = int.MaxValue;
                    bindigHttps.MaxReceivedMessageSize = int.MaxValue;
                    bindigHttps.ReaderQuotas.MaxArrayLength = int.MaxValue;
                    bindigHttps.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
                    bindigHttps.ReaderQuotas.MaxDepth = int.MaxValue;
                    bindigHttps.ReaderQuotas.MaxNameTableCharCount = int.MaxValue;
                    bindigHttps.ReaderQuotas.MaxStringContentLength = int.MaxValue;
                    bindigHttps.TransactionFlow = _transactionFlow;

                    //Habilitar si es que no se desea validar el certificado
                    //System.Net.ServicePointManager.ServerCertificateValidationCallback =
                    //((sender, certificate, chain, sslPolicyErrors) => true);

                    _binding = bindigHttps;
                    break;
                case UriType.Tcp:
                    // Crea el Binding TCP
                    NetTcpBinding bindigTcp = new NetTcpBinding();

                    bindigTcp.Security.Mode = SecurityMode.None;
                    bindigTcp.Security.Message.ClientCredentialType = MessageCredentialType.None;

                    //Habilitar si se usa seguridad por transporte
                    //bindigTcp.Security.Transport.ProtectionLevel = ProtectionLevel.EncryptAndSign;

                    bindigTcp.MaxBufferPoolSize = int.MaxValue;
                    bindigTcp.MaxReceivedMessageSize = int.MaxValue;
                    bindigTcp.ReaderQuotas.MaxArrayLength = int.MaxValue;
                    bindigTcp.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
                    bindigTcp.ReaderQuotas.MaxDepth = int.MaxValue;
                    bindigTcp.ReaderQuotas.MaxNameTableCharCount = int.MaxValue;
                    bindigTcp.ReaderQuotas.MaxStringContentLength = int.MaxValue;
                    bindigTcp.TransactionFlow = _transactionFlow;
                    _binding = bindigTcp;
                    break;
                default:
                    break;
            }
            _binding.ReceiveTimeout = ConexionHelperWCF.ReceiveTimeout;
            _binding.OpenTimeout = ConexionHelperWCF.OpenTimeout;
            _binding.CloseTimeout = ConexionHelperWCF.CloseTimeout;
            _binding.SendTimeout = ConexionHelperWCF.SendTimeout;
        }

        public EndpointAddress Address
        {
            get
            {
                return _address;
            }
        }

        public ContractDescription Contract(Type tipo)
        {
            ContractDescription contract = new ContractDescription("");

            return contract;
        }

        public string ServiceName
        {
            get
            {
                return _serviceName;
            }
        }

        public Binding Binding
        {
            get { return _binding; }

        }

        private EndpointIdentity CrearIdentidad()
        {
            return EndpointIdentity.CreateDnsIdentity("localhost");
        }

        public void ConfigurarCredenciales(ClientCredentials credencial)
        {
            credencial.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;

        }

    }
}
