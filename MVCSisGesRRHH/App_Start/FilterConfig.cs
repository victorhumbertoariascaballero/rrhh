using MIDIS.UtilesMVC.Filtros;
using System.Web;
using System.Web.Mvc;

namespace MVCSisGesRRHH
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new SSOAttribute());
            //filters.Add(new ValidaHorarioIpAttibute());
        }
    }
}
