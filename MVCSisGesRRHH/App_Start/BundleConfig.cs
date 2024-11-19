using System.Web;
using System.Web.Optimization;

namespace MVCSisGesRRHH
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //var kendoVersion = "2018.1.221";
            var kendoVersion = "2022.1.456";
            
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"
                        , "~/Scripts/jquery-todictionary.js"
                        , "~/Scripts/jquery.blockUI.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/script").Include(
                "~/Scripts/App/Intranet/App.js"
                ,"~/Scripts/App/Intranet/AppAnonimo.js"
                , "~/Scripts/Util.js"
                , "~/Scripts/bootstrap-datetimepicker.min.js"
                , "~/Scripts/locales/bootstrap-datetimepicker.es.js"
                , "~/Scripts/chosen.jquery.min.js"
                , "~/Scripts/App/Intranet/Boletas.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/scriptLogin").Include(
                "~/Scripts/Util.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/kendoui").Include(
                 string.Format("~/Scripts/kendo/{0}/kendo.web.min.js", kendoVersion)
                , string.Format("~/Scripts/kendo/{0}/cultures/kendo.culture.es-PE.min.js", kendoVersion)
                , string.Format("~/Scripts/kendo/{0}/messages/kendo.messages.es-PE.min.js", kendoVersion)
                , "~/Scripts/knockout-2.0.0.js"
                , "~/Scripts/knockout-kendo.js"
            ));

            bundles.Add(new StyleBundle("~/Content/kendouicss").Include(
                string.Format("~/Content/kendo/{0}/kendo.common.min.css", kendoVersion)
                , string.Format("~/Content/kendo/{0}/kendo.common-bootstrap.min.css", kendoVersion)
                , string.Format("~/Content/kendo/{0}/kendo.bootstrap.min.css", kendoVersion)
                ));

            bundles.Add(new StyleBundle("~/Content/style").Include(
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/bootstrap_extend.css",
                      "~/Content/css/bootstrap-select.css",
                      "~/Content/css/jquery-ui.css",
                      "~/Content/css/styles.css",
                      "~/Content/css/style-key.css",
                      "~/Content/bootstrap-datetimepicker.min.css",
                      "~/Content/css/bootstrap-chosen.css",
                      "~/Content/font-awesome.min.css"
            ));

            //adicionado
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            //bundles.Add(new ScriptBundle("~/bundles/ScriptsAppIntranet").Include(
            //    "~/Scripts/App/Intranet/App.js"
            //    , "~/Scripts/App/Intranet/Boletas.js"
            //    , "~/Scripts/App/Intranet/Usuario.js"
            //    ));

            //bundles.Add(new ScriptBundle("~/bundles/ScriptsAppPublico").Include(
            //    "~/Scripts/App/Publico/App.js"
            //    , "~/Scripts/App/Publico/Login.js"
            //    ));
        }
    }
}
