using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Configuration;
using System.Reflection;
//using Infraestructura.WCF;
using log4net;
using log4net.Core;



namespace MIDIS.Utiles.WCF   
{

    public class BaseAgent<TChannel> : IDisposable
        where TChannel : class
    {
        #region Log

        public static ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Construtor

        public BaseAgent(string serviceName)
        {
            _configService = new ConfigHelperWCF(serviceName, false);
        }

        public BaseAgent(string serviceName, bool transactionFlow)
        {
            _configService = new ConfigHelperWCF(serviceName, transactionFlow);
        }

        #endregion

        #region Campos privados
        private readonly object _bloqueo = new object();
        private ChannelFactory<TChannel> _factory = null;
        private TChannel _proxy = default(TChannel);
        ConfigHelperWCF _configService = null;
        #endregion

        #region Propiedades

        private ChannelFactory<TChannel> Factory
        {
            get
            {
                if (object.Equals(_factory, default(ChannelFactory<TChannel>)))
                {
                    lock (_bloqueo)
                    {
                        _factory = new ChannelFactory<TChannel>(_configService.Binding, _configService.Address);
                        foreach (var operacion in _factory.Endpoint.Contract.Operations)
                        {
                            operacion.Behaviors.Find<DataContractSerializerOperationBehavior>().MaxItemsInObjectGraph = int.MaxValue;

                        }

                        _configService.ConfigurarCredenciales(_factory.Credentials);
                        _factory.Endpoint.Behaviors.Add(new InspectorBehavior());
                    }
                }

                return _factory;
            }
        }
        private ICommunicationObject Comunicacion
        {
            get
            {
                return _proxy as ICommunicationObject;
            }
        }

        public TChannel Proxy
        {
            get
            {

                if (!object.Equals(_proxy, default(TChannel)))
                {
                    if (Comunicacion.State == CommunicationState.Faulted || Comunicacion.State == CommunicationState.Closed)
                    {
                        Comunicacion.Abort();
                        _proxy = default(TChannel);
                    }
                }

                if (object.Equals(_proxy, default(TChannel)))
                {
                        _proxy = Factory.CreateChannel();
                    Comunicacion.Closed += new EventHandler(Comunicacion_Closed);
                    Comunicacion.Faulted += new EventHandler(Comunicacion_Faulted);
                }



                if (object.Equals(_proxy, default(TChannel)))
                {
                    throw new Exception("No se pudo crear el proxy.");
                }

                return _proxy;
            }
        }

        public string Uri
        {
            get
            {
                return _configService.Address.Uri.AbsoluteUri;
            }
        }

        public string ServiceName
        {
            get
            {
                return _configService.ServiceName;
            }
        }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Close();
        }

        public void Close()
        {
            try
            {
                bool cerrarConn;
                bool.TryParse(ConfigurationManager.AppSettings["CerrarConn"], out cerrarConn);
                if (!cerrarConn)
                {
                    _proxy = null;
                    return;
                }


                if (Comunicacion != null)
                {
                    if (Comunicacion.State == CommunicationState.Faulted)
                    {
                        Comunicacion.Abort();
                    }
                    else if (Comunicacion.State == CommunicationState.Opened)
                    {
                        Comunicacion.Abort();
                    }
                }
            }
            catch (CommunicationException)
            {
                Comunicacion.Abort();
            }
            catch (TimeoutException)
            {
                Comunicacion.Abort();
            }
            catch (Exception)
            {
                Comunicacion.Abort();
                throw;
            }
            finally
            {
                _factory = default(ChannelFactory<TChannel>);
                _proxy = default(TChannel);
            }

        }
        #endregion

        #region Manejador de eventos

        void Comunicacion_Faulted(object sender, EventArgs e)
        {
            Comunicacion.Abort();
        }

        void Comunicacion_Closed(object sender, EventArgs e)
        {
            _proxy = default(TChannel);
        }

        #endregion

    }
}
