using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.UtilesWeb.SSRS
{
    public static class SSRSHelper
    {
        public static string ConstruirIFrameReporte(string nombreReporte, string parametros)
        {
            string servidor = ConfigurationManager.AppSettings["UrlIFrameReporte"];
            string nombrepar = ConfigurationManager.AppSettings["ParametroListaParametro"];
            string nombrerep = ConfigurationManager.AppSettings["ParametroReporte"];

            //string comandosRS = "&rs:Command=Render&rs:Format=HTML4.0&rc:Parameters=false";

            return String.Format("<iframe id='ifReporte' width='100%' style='height: 500px' frameborder='0' src='{0}?{1}={2}&{3}={4}'>" +
                                 "</iframe>", servidor, nombrerep, nombreReporte, nombrepar, parametros);
        }

        public static void RenderizarReporte(ReportViewer reportViewer,
             string nombreReporte, List<ReportParameter> listaParametro)
        {
            reportViewer.ProcessingMode = ProcessingMode.Remote;
            reportViewer.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ServidorReporte"]);
            reportViewer.ServerReport.ReportPath = ConfigurationManager.AppSettings["CarpetaReporte"] + nombreReporte;
            reportViewer.ServerReport.ReportServerCredentials = new CustomReportCredentials(
                ConfigurationManager.AppSettings["UsuarioReporte"],
                ConfigurationManager.AppSettings["ClaveReporte"],
                ConfigurationManager.AppSettings["DominioReporte"]
                );
            reportViewer.ServerReport.SetParameters(listaParametro);

            //reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            //reportViewer.Refresh();
            reportViewer.ServerReport.Refresh();

        }

    }
}
