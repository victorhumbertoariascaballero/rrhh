<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResumenAlta.aspx.cs" Inherits="MVCSisGesRRHH.Reportes.Nomina.ReporteAlta.ResumenAlta" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script runat="server">  

        void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String Anio = Request.QueryString["Anio"];
                String Mes = Request.QueryString["Mes"];
                Int32 auxInt = 0;
                DateTime auxDate;

                MIDIS.ORI.Entidades.EmpleadoReporte_Request peticion = new MIDIS.ORI.Entidades.EmpleadoReporte_Request();
                peticion.Anio = (Int32.TryParse(Anio, out auxInt) ? auxInt : 0);
                peticion.Mes = (Int32.TryParse(Mes, out auxInt) ? auxInt : 0);
                
                object respuesta = new MIDIS.ORI.LogicaNegocio.T_genm_empleado_LN().ListarEmpleadoAltas(peticion);

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reportes/Nomina/ReporteAlta/ResumenAlta.rdlc");
                ReportDataSource RDS = new ReportDataSource("dsAltas", respuesta);

                //ReportViewer1.LocalReport.SetParameters(new ReportParameter("pTotal", ((IEnumerable<MIDIS.ORI.Entidades.Empleado_Registro>)respuesta).ToList<MIDIS.ORI.Entidades.Empleado_Registro>().Count.ToString()));
                ReportViewer1.LocalReport.DataSources.Add(RDS);
                ReportViewer1.ShowExportControls = false;
                ReportViewer1.LocalReport.Refresh();

            }
        }


   </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>    
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server">
        </rsweb:ReportViewer>    
    </div>
    </form>
</body>
</html>
