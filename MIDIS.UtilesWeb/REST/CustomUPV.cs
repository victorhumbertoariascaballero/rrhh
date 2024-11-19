using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Configuration;

namespace MIDIS.UtilesWeb.REST
{
    public class CustomUPV : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (null == userName || null == password)
            {
                throw new ArgumentNullException("Debe ingresar ambas valores como credenciales");
            }

            if (!(userName ==  new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.AppSettings["restUsuario"]) && password ==  new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.AppSettings["restClave"])))
            {
                
                throw new FaultException("Usuario o Clave incorrectas");
                
            }
        }
    }
}