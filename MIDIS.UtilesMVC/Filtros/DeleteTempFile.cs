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
    public class DeleteTempFile : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Flush();
            var filePathResult = filterContext.Result as FilePathResult;
            if (filePathResult != null)
            {
                System.IO.File.Delete(filePathResult.FileName);
            }
        }

    }
}