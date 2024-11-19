using System;

namespace MIDIS.Utiles.Log
{
    internal sealed class Log4NetWrapper: ILogger
    {
        readonly log4net.ILog _logManager;

        public Log4NetWrapper(Type type)
        {
            _logManager = log4net.LogManager.GetLogger(type);
        }

        public static void Configure()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
    

        #region ILog Members

        public void Info(string mensaje)
        {
            _logManager.Info(mensaje);
        }

        public void Debug(string mensaje)
        {
            _logManager.Debug(mensaje);
        }

        public void Error(string mensaje)
        {
            _logManager.Error(mensaje);
        }

        public void Error(string mensaje, Exception ex)
        {
            _logManager.Error(mensaje, ex);
        }

        #endregion
    }
}
