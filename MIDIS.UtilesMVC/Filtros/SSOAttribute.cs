using MIDIS.UtilesWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
namespace MIDIS.UtilesMVC.Filtros
{
    public class SSOAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            SSOHelper.RecuperarIdAplicacion();
            base.OnActionExecuting(filterContext);
        }

    }
}
