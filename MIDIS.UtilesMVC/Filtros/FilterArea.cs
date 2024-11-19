using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace MIDIS.UtilesMVC.Filtros
{
    public sealed class FilterArea : ActionFilterAttribute
    {
        private string Mensaje { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string paginaPermiso = FormsAuthentication.DefaultUrl;


            //string area;
            //RouteData routeData = HttpContext.Request.RequestContext.RouteData;



            RouteData ruteoData = RouteTable.Routes.GetRouteData(
                    filterContext.HttpContext);
            var nombreControladora = (string)ruteoData.Values["controller"];
            var nombreAccion = (string)ruteoData.Values["action"];
            var nombreArea = ruteoData.Values["area"];


            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(
                                typeof(AllowAnonymousAttribute), inherit: true)
                        || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(
                            typeof(AllowAnonymousAttribute), inherit: true);

            if (skipAuthorization)
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            //            string area = (string)ruteoData.Values["area"];
            var oarea = ruteoData.DataTokens["area"];
            string area;// = oarea.ToString();


            if (oarea == null)
            {
                base.OnActionExecuting(filterContext);
                return;
            }
            else
            {
                area = oarea.ToString();
            }

            var x = ((ClaimsIdentity)filterContext.HttpContext.User.Identity).IsAuthenticated;
            string nombre = ((ClaimsIdentity)filterContext.HttpContext.User.Identity).Name;
            if (!ValidarPermisos(nombre, area))
            {
                Redireccionar(filterContext, paginaPermiso);
            }

            base.OnActionExecuting(filterContext);
        }

        public void Redireccionar(ActionExecutingContext filterContext, string paginaPermiso)
        {
            if (paginaPermiso.Split('/').Count() == 3)
            {
                filterContext.Result = new
                    RedirectToRouteResult(new RouteValueDictionary {
                        {"controller",paginaPermiso.Split('/')[1]},
                        {"action",paginaPermiso.Split('/')[2]},
                        {"area", ""},
                        {"mensaje",HttpUtility.UrlEncode(Mensaje)}
                    });
            }
            else if (paginaPermiso.Split('/').Count() == 4)
            {
                filterContext.Result = new
                   RedirectToRouteResult(new RouteValueDictionary {
                        {"controller",paginaPermiso.Split('/')[2]},
                        {"action",paginaPermiso.Split('/')[3]},
                        {"area",paginaPermiso.Split('/')[1]},
                        {"mensaje",HttpUtility.UrlEncode(Mensaje)}
                    });
            }

        }
        private bool ValidarPermisos(string nombre, string area)
        {
            string permisos = ConfigurationManager.AppSettings[nombre];
            if (permisos == null)
            {
                return false;
            }

            string[] modulos = permisos.Split(',');

            bool tienePermiso = false;

            foreach (string item in modulos)
            {
                if (item.ToUpper() == area.ToUpper())
                {
                    tienePermiso = true;
                    return tienePermiso;
                }
            }
            return tienePermiso;
        }


    }
}
