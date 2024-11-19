using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;

namespace MVCSisRRHH.Filters
{
    public class CustomFilter : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var ctx = HttpContext.Current.GetOwinContext(); 
            var authenticationManager = ctx.Authentication;
            bool isAuthorized = authenticationManager.User.Identity.IsAuthenticated;

            //bool isAuthorized = IsAuthorized(filterContext); // check authorization
            base.OnAuthorization(filterContext);
            if (!isAuthorized && !filterContext.ActionDescriptor.ActionName.Equals("Unauthorized", StringComparison.InvariantCultureIgnoreCase)
                && !filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.Equals("CerrarSesion", StringComparison.InvariantCultureIgnoreCase))
            {
                HandleUnauthorizedRequest(filterContext);
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result =
           new RedirectToRouteResult(
               new RouteValueDictionary{{ "controller", "Login" },
                                          { "action", "CerrarSesion" }

                                         });
        }
    }
}