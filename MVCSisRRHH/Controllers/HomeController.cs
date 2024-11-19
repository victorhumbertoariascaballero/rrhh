using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Services;
using System.IdentityModel.Services.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSisRRHH.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //return View();
            //return RedirectToAction("Listar", "Postulante");

            return RedirectToAction("Index", "Account");
        }
        public ActionResult CerrarSesion()
        {

            WsFederationConfiguration config = FederatedAuthentication.FederationConfiguration.WsFederationConfiguration;

            // Redirect to SignOutCallback after signing out.
            string callbackUrl = Url.Action("SignOutCallback", "Home", routeValues: null, protocol: Request.Url.Scheme);
            SignOutRequestMessage signoutMessage = new SignOutRequestMessage(new Uri(config.Issuer), callbackUrl);
            signoutMessage.SetParameter("wtrealm", ConfigurationManager.AppSettings["ida:realm"] ?? config.Realm);
            FederatedAuthentication.SessionAuthenticationModule.SignOut();

            return new RedirectResult(signoutMessage.WriteQueryString());
        }

        public ActionResult SignOutCallback()
        {
            //if (Request.IsAuthenticated)
            //{
            // Redirect to home page if the user is authenticated.
            return RedirectToAction("Index", "Home");
            //}

            //return View();
        }

        [Authorize]
        public ActionResult Login()
        {
            //return RedirectToAction("Index", "Home");
            return View();
        }

        public ActionResult PaginaNoPermitida()
        {
            return View();
        }

        public ActionResult AccesoNoAutorizado()
        {
            if (TempData["MensajeErrorAcceso"] != null)
            {
                ViewBag.MensajeErrorAcceso = TempData["MensajeErrorAcceso"];
            }
            return View();
        }

    }
}