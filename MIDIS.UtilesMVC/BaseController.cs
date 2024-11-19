using MIDIS.Utiles.Entidades;
using MIDIS.Utiles;
using MIDIS.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using Utiles;

namespace MIDIS.UtilesMVC
{
    public class BaseController : Controller
    {

        protected log4net.ILog Log
        {
            get
            {
                return log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().GetType());
            }
        }

        public Dictionary<EnumMaestras, List<T_mae_maestras>> oListaEnumMaestras;
        public List<EnumMaestras> listaMaestra = new List<EnumMaestras>();
        
        //
        // GET: /Base/
        public string GenerarMensaje(ModelStateDictionary modelState)
        {
            var erroresObtenidos = string.Join("<br/>", modelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return erroresObtenidos;
        }
    }
}