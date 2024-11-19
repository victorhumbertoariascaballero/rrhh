using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using MIDIS.ORI.Entidades;

namespace MVCSisGesRRHH.Reportes.Planillas.ResumenGeneral
{
    public partial class ResumenGeneral : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Reporte_General"] != null)
                {
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.SizeToReportContent = true;
                    ReportViewer1.Width = Unit.Percentage(100);
                    ReportViewer1.Height = Unit.Percentage(100);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reportes/Planillas/ResumenGeneral/Report_1.rdlc");
                    ////NorthwindEntities entities = new NorthwindEntities();
                    List<PlanillaRpteResumenGeneraResponse> lstResumenGeneral = (List<PlanillaRpteResumenGeneraResponse>)Session["Reporte_General"];

                    List<PlanillaRpteResumenGeneraResponse> lstResumenGeneralIng = lstResumenGeneral.Where(x => x.iCodTipoConcepto == 1).ToList();
                    //PlanillaRpteResumenGeneraResponse objPlanillaRpteResumenGeneraResponseIng = new PlanillaRpteResumenGeneraResponse()
                    //{
                    //    dcTotalIng = lstResumenGeneral.FirstOrDefault(x => x.dcTotalIng > 0).dcTotalIng
                    //};
                    //lstResumenGeneralIng.Add(objPlanillaRpteResumenGeneraResponseIng);
                    ReportDataSource datasourceIng = new ReportDataSource();
                    datasourceIng.Name = "dsReporte1Ing";
                    datasourceIng.Value = lstResumenGeneralIng;

                    List<PlanillaRpteResumenGeneraResponse> lstResumenGeneralDscto = lstResumenGeneral.Where(x => x.iCodTipoConcepto == 2).ToList();
                    ReportDataSource datasourceDscto = new ReportDataSource();
                    if (lstResumenGeneralDscto!=null && lstResumenGeneralDscto.Count>0)
                    {
                        lstResumenGeneralDscto[0].sClasificadorGasto = lstResumenGeneral.Where(x => x.iCodTipoConcepto == 1 && x.sClasificadorGasto != "").FirstOrDefault().sClasificadorGasto;
                        //PlanillaRpteResumenGeneraResponse objPlanillaRpteResumenGeneraResponseDscto = new PlanillaRpteResumenGeneraResponse()
                        //{
                        //    dcTotalDscto = lstResumenGeneral.FirstOrDefault(x => x.dcTotalDscto > 0).dcTotalDscto
                        //};
                        //lstResumenGeneralDscto.Add(objPlanillaRpteResumenGeneraResponseDscto);                        
                        datasourceDscto.Name = "dsReporte1Dscto";
                        datasourceDscto.Value = lstResumenGeneralDscto;
                    }
                    else
                    {                        
                        datasourceDscto.Name = "dsReporte1Dscto";
                        datasourceDscto.Value = lstResumenGeneralDscto;
                    }
                    


                    List<PlanillaRpteResumenGeneraResponse> lstResumenGeneralAportes = lstResumenGeneral.Where(x => x.iCodTipoConcepto == 3).ToList();
                    //PlanillaRpteResumenGeneraResponse objPlanillaRpteResumenGeneraResponseAportes = new PlanillaRpteResumenGeneraResponse()
                    //{
                    //    dcTotalAportes = lstResumenGeneral.FirstOrDefault(x => x.dcTotalAportes > 0).dcTotalAportes
                    //};
                    //lstResumenGeneralAportes.Add(objPlanillaRpteResumenGeneraResponseAportes);
                    ReportDataSource datasourceAportes = new ReportDataSource();
                    if (lstResumenGeneralAportes != null && lstResumenGeneralAportes.Count > 0)
                    {                        
                        datasourceAportes.Name = "dsReporte1Aportes";
                        datasourceAportes.Value = lstResumenGeneralAportes;
                    }
                    else
                    {
                        datasourceAportes.Name = "dsReporte1Aportes";
                        datasourceAportes.Value = lstResumenGeneralAportes;
                    }
                    

                    List<PlanillaRpteResumenGeneraResponse> lstResumenGeneralTotales = new List<PlanillaRpteResumenGeneraResponse>();
                    PlanillaRpteResumenGeneraResponse objPlanillaRpteResumenGeneraResponseTotales = new PlanillaRpteResumenGeneraResponse()
                    {
                        //dcTotalIng = lstResumenGeneral.FirstOrDefault(x => x.dcTotalIng > 0).dcTotalIng,
                        //dcTotalDscto = lstResumenGeneral.FirstOrDefault(x => x.dcTotalDscto > 0).dcTotalDscto,
                        //dcTotalAportes = lstResumenGeneral.FirstOrDefault(x => x.dcTotalAportes > 0).dcTotalAportes,
                        //dcTotalLiquido = lstResumenGeneral.FirstOrDefault(x => x.dcTotalLiquido > 0).dcTotalLiquido
                        dcTotalIng = lstResumenGeneral.Where(x => x.iCodTipoConcepto == 1).Select(x => x.dcMonto).Sum(),
                        dcTotalDscto = lstResumenGeneral.Where(x => x.iCodTipoConcepto == 2).Select(x => x.dcMonto).Sum(),
                        dcTotalAportes = lstResumenGeneral.Where(x => x.iCodTipoConcepto == 3).Select(x => x.dcMonto).Sum(),
                        dcTotalLiquido = lstResumenGeneral.Where(x => x.iCodTipoConcepto == 1).Select(x => x.dcMonto).Sum() - lstResumenGeneral.Where(x => x.iCodTipoConcepto == 2).Select(x => x.dcMonto).Sum()

                    };
                    lstResumenGeneralTotales.Add(objPlanillaRpteResumenGeneraResponseTotales);
                    ReportDataSource datasourceTotales = new ReportDataSource();
                    datasourceTotales.Name = "dsReporte1Totales";
                    datasourceTotales.Value = lstResumenGeneralTotales;
                    
                    ReportDataSource datasource = new ReportDataSource();
                    datasource.Name = "dsReporte1";
                    datasource.Value = (List<PlanillaRpteResumenGeneraResponse>)Session["Reporte_General"];
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasource);
                    ReportViewer1.LocalReport.DataSources.Add(datasourceIng);
                    ReportViewer1.LocalReport.DataSources.Add(datasourceDscto);
                    ReportViewer1.LocalReport.DataSources.Add(datasourceAportes);
                    ReportViewer1.LocalReport.DataSources.Add(datasourceTotales);
                    Session["Reporte_General"] = null;
                }
                if (Session["Reporte_GeneralPorMeta"] != null)
                {
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.SizeToReportContent = true;
                    ReportViewer1.Width = Unit.Percentage(100);
                    ReportViewer1.Height = Unit.Percentage(100);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reportes/Planillas/ResumenGeneral/Report_2.rdlc");
                    ////NorthwindEntities entities = new NorthwindEntities();                    
                    ReportDataSource datasourcePorMeta = new ReportDataSource();
                    datasourcePorMeta.Name = "dsReportePorMeta";
                    datasourcePorMeta.Value = (List<PlanillaRpteResumenGeneralResponse2>)Session["Reporte_GeneralPorMeta"];
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasourcePorMeta);
                    Session["Reporte_GeneralPorMeta"] = null;
                }
                if (Session["Reporte_GeneralPorMetaEsSalud"] != null)
                {
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.SizeToReportContent = true;
                    ReportViewer1.Width = Unit.Percentage(100);
                    ReportViewer1.Height = Unit.Percentage(100);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reportes/Planillas/ResumenGeneral/Report_3.rdlc");
                    ////NorthwindEntities entities = new NorthwindEntities();                    
                    ReportDataSource datasourcePorMeta = new ReportDataSource();
                    datasourcePorMeta.Name = "dsReportePorMeta";
                    datasourcePorMeta.Value = (List<PlanillaRpteResumenGeneralResponse2>)Session["Reporte_GeneralPorMetaEsSalud"];
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasourcePorMeta);
                    Session["Reporte_GeneralPorMetaEsSalud"] = null;
                }
                if (Session["Reporte_DetalladoEsSalud"] != null)
                {
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.SizeToReportContent = true;
                    ReportViewer1.Width = Unit.Percentage(100);
                    ReportViewer1.Height = Unit.Percentage(100);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reportes/Planillas/ReporteDetalladoEsSalud/Report_DetEsSalud.rdlc");
                    ////NorthwindEntities entities = new NorthwindEntities();                    
                    ReportDataSource datasourceReporteDetalladoEsSalud = new ReportDataSource();
                    datasourceReporteDetalladoEsSalud.Name = "dsReporteDetalladoEsSalud";
                    datasourceReporteDetalladoEsSalud.Value = (List<PlanillaRpteResumenGeneralResponse2>)Session["Reporte_DetalladoEsSalud"];
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasourceReporteDetalladoEsSalud);
                    Session["Reporte_DetalladoEsSalud"] = null;
                }
                if (Session["ReporteResumenRetencionesFteFto"] != null)
                {
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.SizeToReportContent = true;
                    ReportViewer1.Width = Unit.Percentage(100);
                    ReportViewer1.Height = Unit.Percentage(100);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reportes/Planillas/ReporteRetFuenteFinancMetPartConcepto/Report_RetFuenteFinMetaPartConcepto.rdlc");
                    ////NorthwindEntities entities = new NorthwindEntities();                    
                    ReportDataSource datasourceFuenteFinMetaPartConcepto = new ReportDataSource();
                    datasourceFuenteFinMetaPartConcepto.Name = "dsReporteRetFuenteFinMetaPartConcepto";
                    datasourceFuenteFinMetaPartConcepto.Value = (List<PlanillaRpteResumenGeneralResponse2>)Session["ReporteResumenRetencionesFteFto"];
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasourceFuenteFinMetaPartConcepto);
                    Session["ReporteResumenRetencionesFteFto"] = null;
                }
                if (Session["ReporteResumenIngEgreAporMetaPartidaConcepto"] != null)
                {
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.SizeToReportContent = true;
                    ReportViewer1.Width = Unit.Percentage(100);
                    ReportViewer1.Height = Unit.Percentage(100);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reportes/Planillas/ReporteResumenIngEgreMetaPartidaConcepto/Report_ResumenIngEgreMetaPartidaConcepto.rdlc");
                    ////NorthwindEntities entities = new NorthwindEntities();                    
                    ReportDataSource datasourceResumenIngEgreMetaPartidaConcepto = new ReportDataSource();
                    datasourceResumenIngEgreMetaPartidaConcepto.Name = "dsReporteResumenIngEgreAporMetaPartidaConcepto";
                    datasourceResumenIngEgreMetaPartidaConcepto.Value = (List<PlanillaRpteResumenGeneralResponse2>)Session["ReporteResumenIngEgreAporMetaPartidaConcepto"];
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasourceResumenIngEgreMetaPartidaConcepto);
                    Session["ReporteResumenIngEgreAporMetaPartidaConcepto"] = null;
                }
                if (Session["ReporteResumenGralAFPconNroAfiliados"] != null)
                {
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.SizeToReportContent = true;
                    ReportViewer1.Width = Unit.Percentage(100);
                    ReportViewer1.Height = Unit.Percentage(100);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reportes/Planillas/ReporteResumenGralAFP/Report_ReporteResumenGralAFPconNroAfiliados.rdlc");
                    ////NorthwindEntities entities = new NorthwindEntities();                    
                    ReportDataSource datasourceReportResumenGralAFPconNroAfiliados = new ReportDataSource();
                    datasourceReportResumenGralAFPconNroAfiliados.Name = "dsReporteResumenGralAFPconNroAfiliados";
                    datasourceReportResumenGralAFPconNroAfiliados.Value = (List<PlanillaRpteResumenGeneralResponse2>)Session["ReporteResumenGralAFPconNroAfiliados"];
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasourceReportResumenGralAFPconNroAfiliados);
                    Session["ReporteResumenGralAFPconNroAfiliados"] = null;
                }
                if (Session["ReporteResumenGralAFPconAFPDepMetaClasGasto"] != null)
                {
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.SizeToReportContent = true;
                    ReportViewer1.Width = Unit.Percentage(100);
                    ReportViewer1.Height = Unit.Percentage(100);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reportes/Planillas/ReporteResumenGralAFP/Report_ReporteResumenGralAFPconAFPDepMetaClasGasto.rdlc");
                    ////NorthwindEntities entities = new NorthwindEntities();                    
                    ReportDataSource datasourceReporteResumenGralAFPconAFPDepMetaClasGasto = new ReportDataSource();
                    datasourceReporteResumenGralAFPconAFPDepMetaClasGasto.Name = "dsReporteResumenGralAFPconAFPDepMetaClasGasto";
                    datasourceReporteResumenGralAFPconAFPDepMetaClasGasto.Value = (List<PlanillaRpteResumenGeneralResponse2>)Session["ReporteResumenGralAFPconAFPDepMetaClasGasto"];
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasourceReporteResumenGralAFPconAFPDepMetaClasGasto);
                    Session["ReporteResumenGralAFPconAFPDepMetaClasGasto"] = null;
                }
                if (Session["ReporteResumenGralBcosPorInstCantTotIngTotEgre"] != null)
                {
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.SizeToReportContent = true;
                    ReportViewer1.Width = Unit.Percentage(100);
                    ReportViewer1.Height = Unit.Percentage(100);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reportes/Planillas/ReporteResumenBancos/Report_ReporteResumenGralBcosPorInstCantTotIngTotEgre.rdlc");
                    ////NorthwindEntities entities = new NorthwindEntities();                    
                    ReportDataSource datasourceReporteResumenGralBcosPorInstCantTotIngTotEgre = new ReportDataSource();
                    datasourceReporteResumenGralBcosPorInstCantTotIngTotEgre.Name = "dsReporteResumenGralBcosPorInstCantTotIngTotEgre";
                    datasourceReporteResumenGralBcosPorInstCantTotIngTotEgre.Value = (List<PlanillaRpteResumenGeneralResponse2>)Session["ReporteResumenGralBcosPorInstCantTotIngTotEgre"];
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasourceReporteResumenGralBcosPorInstCantTotIngTotEgre);
                    Session["ReporteResumenGralBcosPorInstCantTotIngTotEgre"] = null;
                }
                if (Session["ReporteResumenGralBcosFteFtoMeta"] != null)
                {
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.SizeToReportContent = true;
                    ReportViewer1.Width = Unit.Percentage(100);
                    ReportViewer1.Height = Unit.Percentage(100);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reportes/Planillas/ReporteResumenBancos/Report_ReporteResumenGralBcosFteFtoMeta.rdlc");
                    ////NorthwindEntities entities = new NorthwindEntities();                    
                    ReportDataSource datasourceReporteResumenGralBcosFteFtoMeta = new ReportDataSource();
                    datasourceReporteResumenGralBcosFteFtoMeta.Name = "dsReporteResumenGralBcosFteFtoMeta";
                    datasourceReporteResumenGralBcosFteFtoMeta.Value = (List<PlanillaRpteResumenGeneralResponse2>)Session["ReporteResumenGralBcosFteFtoMeta"];
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasourceReporteResumenGralBcosFteFtoMeta);
                    Session["ReporteResumenGralBcosFteFtoMeta"] = null;
                }
                if (Session["ReporteResumenDetalladoBcos"] != null)
                {
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.SizeToReportContent = true;
                    ReportViewer1.Width = Unit.Percentage(100);
                    ReportViewer1.Height = Unit.Percentage(100);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reportes/Planillas/ReporteResumenBancos/Report_ReporteResumenDetalladoBcos.rdlc");
                    ////NorthwindEntities entities = new NorthwindEntities();                    
                    ReportDataSource datasourceReporteResumenDetalladoBcos = new ReportDataSource();
                    datasourceReporteResumenDetalladoBcos.Name = "dsReporteResumenDetalladoBcos";
                    datasourceReporteResumenDetalladoBcos.Value = (List<PlanillaRpteResumenGeneralResponse2>)Session["ReporteResumenDetalladoBcos"];
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasourceReporteResumenDetalladoBcos);
                    Session["ReporteResumenDetalladoBcos"] = null;
                }
                if (Session["ReportePlanillaUnicaPagos"] != null)
                {
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.SizeToReportContent = true;
                    ReportViewer1.Width = Unit.Percentage(100);
                    ReportViewer1.Height = Unit.Percentage(100);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reportes/Planillas/ReportePlanillaUnicaPagos/Report_ReportePlanillaUnicaPagos.rdlc");
                    ////NorthwindEntities entities = new NorthwindEntities();                    
                    ReportDataSource datasourceReportePlanillaUnicaPagos = new ReportDataSource();
                    datasourceReportePlanillaUnicaPagos.Name = "dsReportePlanillaUnicaPagos";
                    datasourceReportePlanillaUnicaPagos.Value = (List<PlanillaRpteResumenGeneralResponse2>)Session["ReportePlanillaUnicaPagos"];
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasourceReportePlanillaUnicaPagos);
                    Session["ReportePlanillaUnicaPagos"] = null;
                }
            }
        }
    }
}