using log4net;
using MIDIS.Utiles.Log;
using MIDIS.UtilesMVC.Binder;
//using MVCSisRRHH;
using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MVCSisRRHH
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            LogHelper.Configure();
            #region Modificamos la metadata para que los vacios no lo considere nulos

            ModelMetadataProviders.Current = new EmptyStringDataAnnotations();

            #endregion


            #region Agregando los formatos para las fechas

            var binder = new DateTimeBinder("dd/MM/yyyy hh:mm:ss tt");
            ModelBinders.Binders.Add(typeof(DateTime), binder);
            ModelBinders.Binders.Add(typeof(DateTime?), binder);

            #endregion
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().GetType());
            Exception error = Server.GetLastError();
            CryptographicException cryptoEx = error as CryptographicException;
            if (cryptoEx != null)
            {
                FederatedAuthentication.WSFederationAuthenticationModule.SignOut();
                Server.ClearError();
            }
            else
            {
                Log.Error(error.Message, error);

            }


        }
    }
}
