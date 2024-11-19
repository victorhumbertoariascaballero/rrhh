using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using MIDIS.ORI.Entidades;

namespace MVCSisGesRRHH.Reportes.Planillas.ResumenGeneralAnual
{
    public partial class ResumenGeneralAnual : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ReporteEstructuraInfoAFPNET"] != null)
                {
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.SizeToReportContent = true;
                    ReportViewer1.Width = Unit.Percentage(100);
                    ReportViewer1.Height = Unit.Percentage(100);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reportes/Planillas/ReporteEstructuraInfoAFPNET/Report_ReporteEstructuraInfoAFPNET.rdlc");
                    ////NorthwindEntities entities = new NorthwindEntities();                    
                    ReportDataSource datasourceReporteEstructuraInfoAFPNET = new ReportDataSource();
                    datasourceReporteEstructuraInfoAFPNET.Name = "dsReporteEstructuraInfoAFPNET";
                    datasourceReporteEstructuraInfoAFPNET.Value = (List<PlanillaRpteEstructuraInfoAFPNETResponse>)Session["ReporteEstructuraInfoAFPNET"];
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasourceReporteEstructuraInfoAFPNET);
                    Session["ReporteEstructuraInfoAFPNET"] = null;
                }
                if (Session["ReporteTrabajadoresAfiliadosONPE"] != null)
                {
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.SizeToReportContent = true;
                    ReportViewer1.Width = Unit.Percentage(100);
                    ReportViewer1.Height = Unit.Percentage(100);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reportes/Planillas/ReporteEstructuraInfoAFPNET/Report_ReporteTrabajadoresAfiliadosONPE.rdlc");
                    ////NorthwindEntities entities = new NorthwindEntities();                    
                    ReportDataSource datasourceReporteTrabajadoresAfiliadosONPE = new ReportDataSource();
                    datasourceReporteTrabajadoresAfiliadosONPE.Name = "dsReporteTrabajadoresAfiliadosONPE";
                    datasourceReporteTrabajadoresAfiliadosONPE.Value = (List<PlanillaRpteEstructuraInfoAFPNETResponse>)Session["ReporteTrabajadoresAfiliadosONPE"];
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasourceReporteTrabajadoresAfiliadosONPE);
                    Session["ReporteTrabajadoresAfiliadosONPE"] = null;
                }
                if (Session["ReporteInfoPortalTranspEstandar"] != null)
                {
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.SizeToReportContent = true;
                    ReportViewer1.Width = Unit.Percentage(100);
                    ReportViewer1.Height = Unit.Percentage(100);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reportes/Planillas/ReporteInfoPortalTranspEstandar/Report_ReporteInfoPortalTranspEstandar.rdlc");
                    ////NorthwindEntities entities = new NorthwindEntities();                    
                    ReportDataSource datasourceReporteInfoPortalTranspEstandar = new ReportDataSource();
                    datasourceReporteInfoPortalTranspEstandar.Name = "dsReporteInfoPortalTranspEstandar";
                    datasourceReporteInfoPortalTranspEstandar.Value = (List<PlanillaRpteResumenGeneralAnualResponse>)Session["ReporteInfoPortalTranspEstandar"];
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasourceReporteInfoPortalTranspEstandar);
                    Session["ReporteInfoPortalTranspEstandar"] = null;
                }
                if (Session["ReporteResumenPlanillaMensIngEgreEsSalud"] != null)
                {
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.SizeToReportContent = true;
                    ReportViewer1.Width = Unit.Percentage(100);
                    ReportViewer1.Height = Unit.Percentage(100);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reportes/Planillas/ReporteResumenPlanillaMensIngEgreEsSalud/Report_ReporteResumenPlanillaMensIngEgreEsSalud.rdlc");
                    ////NorthwindEntities entities = new NorthwindEntities();                    
                    ReportDataSource datasourceReporteResumenPlanillaMensIngEgreEsSalud = new ReportDataSource();
                    datasourceReporteResumenPlanillaMensIngEgreEsSalud.Name = "dsReporteResumenPlanillaMensIngEgreEsSalud";
                    datasourceReporteResumenPlanillaMensIngEgreEsSalud.Value = (List<PlanillaRpteResumenGeneralAnualResponse>)Session["ReporteResumenPlanillaMensIngEgreEsSalud"];
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasourceReporteResumenPlanillaMensIngEgreEsSalud);
                    Session["ReporteResumenPlanillaMensIngEgreEsSalud"] = null;
                }
                if (Session["ReporteResumenAnuaPorTrab"] != null)
                {
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.SizeToReportContent = true;
                    ReportViewer1.Width = Unit.Percentage(100);
                    ReportViewer1.Height = Unit.Percentage(100);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reportes/Planillas/ReporteResumenAnuaPorTrab/Report_ReporteResumenAnuaPorTrab.rdlc");
                    ////NorthwindEntities entities = new NorthwindEntities();                    
                    ReportDataSource datasourceReporteResumenAnuaPorTrab = new ReportDataSource();
                    datasourceReporteResumenAnuaPorTrab.Name = "dsReporteResumenAnuaPorTrab";
                    datasourceReporteResumenAnuaPorTrab.Value = (List<PlanillaRpteResumenGeneralAnualResponse>)Session["ReporteResumenAnuaPorTrab"];

                    ReportDataSource datasourceReporteResumenAnuaPorTrab_Encabezado = new ReportDataSource();
                    datasourceReporteResumenAnuaPorTrab_Encabezado.Name = "dsReporteResumenAnuaPorTrab_Encabezado";
                    datasourceReporteResumenAnuaPorTrab_Encabezado.Value = (List<PlanillaRpteResumenGeneralAnualResponse>)Session["ReporteResumenAnuaPorTrab"];

                    ReportDataSource datasourceReporteResumenAnuaPorTrab_Detalle = new ReportDataSource();
                    datasourceReporteResumenAnuaPorTrab_Detalle.Name = "dsReporteResumenAnuaPorTrab_Detalle";
                    datasourceReporteResumenAnuaPorTrab_Detalle.Value = (List<PlanillaRpteResumenGeneralAnualResponse>)Session["ReporteResumenAnuaPorTrab"];
                    //ReportParameter[] parameters = new ReportParameter[1];
                    //parameters[0] = new ReportParameter("p_Dni", "DNI");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasourceReporteResumenAnuaPorTrab);
                    ReportViewer1.LocalReport.DataSources.Add(datasourceReporteResumenAnuaPorTrab_Encabezado);
                    ReportViewer1.LocalReport.DataSources.Add(datasourceReporteResumenAnuaPorTrab_Detalle);
                    Session["ReporteResumenAnuaPorTrab"] = null;
                }
                if (Session["ReporteResumenIngRent4ta"] != null)
                {
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.SizeToReportContent = true;
                    ReportViewer1.Width = Unit.Percentage(100);
                    ReportViewer1.Height = Unit.Percentage(100);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reportes/Planillas/ReporteResumenIngRent4ta/Report_ReporteResumenIngRent4ta.rdlc");
                    ////NorthwindEntities entities = new NorthwindEntities();                    
                    ReportDataSource datasourceReporteResumenIngRent4ta = new ReportDataSource();
                    datasourceReporteResumenIngRent4ta.Name = "dsReporteResumenIngRent4ta";
                    datasourceReporteResumenIngRent4ta.Value = (List<PlanillaRpteResumenGeneralAnualResponse>)Session["ReporteResumenIngRent4ta"];
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasourceReporteResumenIngRent4ta);
                    Session["ReporteResumenIngRent4ta"] = null;
                }
                if (Session["ReporteEjecucionMensualMetaEspGasto"] != null)
                {
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.SizeToReportContent = true;
                    ReportViewer1.Width = Unit.Percentage(100);
                    ReportViewer1.Height = Unit.Percentage(100);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reportes/Planillas/ReporteEjecucionMensualMetaEspGasto/Report_ReporteEjecucionMensualMetaEspGasto.rdlc");
                    ////NorthwindEntities entities = new NorthwindEntities();                    
                    ReportDataSource datasourceReporteEjecucionMensualMetaEspGasto = new ReportDataSource();
                    datasourceReporteEjecucionMensualMetaEspGasto.Name = "dsReporteEjecucionMensualMetaEspGasto";
                    datasourceReporteEjecucionMensualMetaEspGasto.Value = (List<PlanillaRpteResumenGeneralAnualResponse>)Session["ReporteEjecucionMensualMetaEspGasto"];
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasourceReporteEjecucionMensualMetaEspGasto);
                    Session["ReporteEjecucionMensualMetaEspGasto"] = null;
                }
            }
        }
    }
}