using MIDIS.UtilesWeb;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MIDIS.ORI.Entidades;
using MIDIS.Utiles;
namespace MIDIS.UtilesMVC.Filtros
{
    public class ValidaHorarioIpAttibute : ActionFilterAttribute
    {
        protected log4net.ILog Log
        {
            get
            {
                return log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().GetType());
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
                if (!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) &&
                !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                string mensajeError = "";
                if (!ValidarPermisos(filterContext, ref mensajeError))
                {
                    Log.Info("Redireccionando por horarios u otro");
                    Redireccionar(filterContext, "Home", "AccesoNoAutorizado", mensajeError);
                }
            }
            //}
        }

        private bool ValidarPermisos(ActionExecutingContext filterContext, ref string mensajeError)
        {

            bool validado = false;
            int idAplicacion = 0;

            if (int.TryParse(ConfigurationManager.AppSettings["IdAplicacion"], out idAplicacion))
            {
                if (idAplicacion > 0)
                {
                    if (VariablesWeb.oT_Genm_Usuario != null
                    && VariablesWeb.oT_Genm_Usuario.Id_usuario > 0)
                    {
                        if (VariablesWeb.ListaPerfiles != null &&
                            (from x in VariablesWeb.ListaPerfiles
                             where x.Id_aplicacion == idAplicacion
                             select x).Count() > 0)
                        {
                            List<T_genm_perfil> listaPerfiles = (from x in VariablesWeb.ListaPerfiles
                                                                 where x.Id_aplicacion == idAplicacion
                                                                 select x).ToList();

                            #region Validamos las IPS

                            //Validamos que no tenga restriccion de IPS
                            if (VariablesWeb.ListaPerfilIps != null
                                && (from x in VariablesWeb.ListaPerfilIps
                                    from y in listaPerfiles
                                    where x.Id_perfil == y.Id_perfil
                                    select x).Count() > 0)
                            {

                                if ((from x in VariablesWeb.ListaPerfilIps
                                     from y in listaPerfiles
                                     where x.Id_perfil == y.Id_perfil
                                     && x.Nro_ip.ToUpper().Trim() == UtilesWeb.IPHelper.ObtenerIPCliente().ToUpper().Trim()
                                     select x).Count() > 0)
                                {
                                    Log.Info("Si coincidia con algun perfilIP");
                                    validado = true;
                                }
                                else
                                {
                                    mensajeError = "No se encuentra en una IP valida para acceder a la Aplicacion";
                                    Log.Info("No se encontro coincidencia de la IP");

                                    return false;
                                }

                            }


                            #endregion

                            #region Validar el rango de Fecha Hora

                            DateTime fechaActual = DateTime.Now;
                            //Validamos que no tenga restriccion de IPS
                            if (VariablesWeb.ListaPerfilHorarios != null
                                && (from x in VariablesWeb.ListaPerfilHorarios
                                    from y in listaPerfiles
                                    where x.Id_perfil == y.Id_perfil
                                    select x).Count() > 0)
                            {

                                var dat = fechaActual.DayOfWeek;

                                if ((from x in VariablesWeb.ListaPerfilHorarios
                                     from y in listaPerfiles
                                     where x.Id_perfil == y.Id_perfil
                                     && fechaActual.DevolverNroDiaPeru() == x.Nro_dia
                                     && fechaActual.ToLocalTime().TimeOfDay >= x.Hora_fin.ToLocalTime().TimeOfDay
                                     && fechaActual.ToLocalTime().TimeOfDay <= x.Hora_fin.ToLocalTime().TimeOfDay
                                     select x).Count() > 0)
                                {
                                    Log.Info("Si coincidia con algun perfilHorario");
                                    validado = true;
                                }
                                else
                                {
                                    mensajeError = "No se encuentra en una fecha valida para acceder a la Aplicacion";
                                    Log.Info("No se encontro coincidencia de la perfilHorarioWW");

                                    return false;
                                }

                            }



                            #endregion


                            //Validamos que no tenga restruccion de Horario
                            validado = true;
                        }
                        else
                        {
                            validado = true;
                        }

                    }
                }
                else
                {
                    Log.Info("El codigo de aplicacion es " + idAplicacion.ToString());
                }
            }




            return validado;
        }

        private void Redireccionar(ActionExecutingContext filterContext, string controladora, string accion, string mensaje)
        {
            filterContext.Controller.TempData["MensajeErrorAcceso"] = mensaje;
            //filterContext.Controller.ViewBag.MensajeErrorAcceso = mensaje;
            filterContext.Result = new
            RedirectToRouteResult(new RouteValueDictionary { 
                {"controller",controladora},
                {"action",accion}
            });
        }
    }
}