using MIDIS.ORI.Entidades;
using MIDIS.UtilesWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MIDIS.UtilesMVC.Filtros
{
    public class Autorizar : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            T_genm_opcion opcion = RecuperarOpcion(filterContext);
            if (opcion != null && opcion.Id_opcion > 0)
            {
                List<T_genm_operacion> listaOperaciones = (from x in VariablesWeb.ListaOperaciones
                                                           where x.Id_opcion == opcion.Id_opcion
                                                           select x).ToList();

                filterContext.Controller.ViewBag.gListaOperaciones = listaOperaciones;
            }
            
            base.OnResultExecuting(filterContext);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) &&
                !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                if (!ValidarPermisos(filterContext))
                    Redireccionar(filterContext, "Home", "PaginaNoPermitida");
            }

            base.OnActionExecuting(filterContext);
        }

        public void Redireccionar(ActionExecutingContext filterContext, string controladora, string accion)
        {
            filterContext.Result = new
            RedirectToRouteResult(new RouteValueDictionary { 
                {"controller",controladora},
                {"action",accion}
            });
        }

        public bool ValidarPermisos(ActionExecutingContext filterContext)
        {
            var autenticado = ((ClaimsIdentity)filterContext.HttpContext.User.Identity).IsAuthenticated;
            if (!filterContext.HttpContext.Request.IsAuthenticated)
            {
                return false;
            }
            T_genm_usuario usuario = VariablesWeb.oT_Genm_Usuario;
            if (usuario == null)
            {
                return false;
            }
            List<T_genm_opcion> lista = VariablesWeb.ListaOpciones;
            if (lista == null || lista.Count == 0)
            {
                return false;
            }

            T_genm_opcion opcion = RecuperarOpcion(filterContext);
            return opcion != null;
            //var sesion = filterContext.HttpContext.Session["UsuarioSesion"];
            //if (sesion != null)
            //{
            //    //Validar que tenga permisos, si tiene permisos devolver true, en caso contrario false
            //    //Se obtiene el controller y action que intenta acceder
            //    var ruteoData = RouteTable.Routes.GetRouteData(filterContext.HttpContext);
            //    var nombreControladora = (string)ruteoData.Values["controller"];
            //    var nombreAccion = (string)ruteoData.Values["action"];

            //}
            //return false;
        }

        private T_genm_opcion RecuperarOpcion(ControllerContext filterContext)
        {
            var ruteoData = RouteTable.Routes.GetRouteData(filterContext.HttpContext);
            var nombreControladora = (string)ruteoData.Values["controller"];
            var nombreAccion = (string)ruteoData.Values["action"];
            var nombreArea = ruteoData.Values["area"];

            T_genm_opcion opcion = null;
            if (nombreArea == null)
            {
                opcion = (from x in VariablesWeb.ListaOpciones
                          where x.Controladora.ToUpper() == nombreControladora.ToUpper()
                          && x.Accion.Split('?')[0].ToUpper() == nombreAccion.Split('?')[0].ToUpper()
                          select x).FirstOrDefault();

            }
            else
            {
                opcion = (from x in VariablesWeb.ListaOpciones
                          where x.Area == (string)nombreArea
                          && x.Controladora.ToUpper() == nombreControladora.ToUpper()
                          && x.Accion.Split('?')[0].ToUpper() == nombreAccion.Split('?')[0].ToUpper()
                          select x).FirstOrDefault();
            }

            return opcion;
        }
    }
}