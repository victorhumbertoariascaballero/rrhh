using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IdentityModel.Protocols.WSTrust;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MIDIS.Autenticacion
{
    public static class AuthLDAP
    {
        public static bool ValidateAuthLDAP(string username, string pwd)
        {
            bool auth = false;
            //string office365STS = "https://login.microsoftonline.com/extSTS.srf";
            //string portalO365 = "https://portal.office.com/admin/default.aspx";
            
            string Server = "192.168.64.16";
            string mensaje = "";
            string ruta = "LDAP://" + Server + "/DC=midis,DC=gob,DC=pe";
            DirectoryEntry raiz = new DirectoryEntry();
            raiz.Path = ruta;
            raiz.AuthenticationType = AuthenticationTypes.Secure;
            raiz.Username = username;
            raiz.Password = pwd;

            string filtro = "sAMAccountName";
            string strSearch = filtro + "=" + username;
            DirectorySearcher dsSystem = new DirectorySearcher(raiz, strSearch);
            dsSystem.SearchScope = SearchScope.Subtree;
            try
            {
                SearchResult srSystem = dsSystem.FindOne();
                auth = true;
                //Response.Redirect("index.aspx");
            }
            catch (Exception e)
            {
                auth = false;
                mensaje = e.Message;
            }

            return auth;
        }
    }
}
