using MIDIS.ORI.Entidades;
using MIDIS.ORI.Entidades.Core;
using MIDIS.SEG.AccesoDatosSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.SEG.LogicaNegocio
{
    public class T_genm_controlasistencia_LN
    {
        private readonly T_genm_empleado_ODA _empleado_Repositorio = new T_genm_empleado_ODA();
        private readonly T_genm_controlasistencia_ODA _Repositorio = new T_genm_controlasistencia_ODA();
        public Empleado_Registro ObtenerParaEditar(Empleado_Request peticion)
        {
            //peticion.IdEmpleado = 38041;
            Empleado_Registro objEmpleado = _empleado_Repositorio.ObtenerParaEditar(peticion);

            return objEmpleado;
        }
        public List<ReporteHorizontal_Registro> GetReporteHorizontal(ReporteHorizontal_Request request)
        {
            return _Repositorio.GetReporteHorizontal(request);
        }

        public List<ReportePlanilla_Registro> GetReportePlanilla(ReportePlanilla_Request request)
        {
            return _Repositorio.GetReportePlanilla(request);
        }
        public List<ReporteSobretiempo_Registro> GetReporteSobretiempo(ReporteSobretiempo_Request request)
        {
            return _Repositorio.GetReporteSobretiempo(request);
        }
    }
}
