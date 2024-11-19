using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Web.Helpers;
using System.IdentityModel.Claims;

[assembly: OwinStartup("StartupConfiguration", typeof(MVCSisGesRRHH.Startup))]

namespace MVCSisGesRRHH
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Index"),
                CookieName = "OGRH",
                ExpireTimeSpan = TimeSpan.FromMinutes(60)
            });

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }

    }
}
