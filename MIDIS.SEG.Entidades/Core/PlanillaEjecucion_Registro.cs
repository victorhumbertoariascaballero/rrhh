using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class PlanillaEjecucion_Registro
    {
        public int? iCodPlanilla { get; set; }
        public int? iCodTipoPlanilla { get; set; }
        public int? iCodDetPlanilla { get; set; }
        public int? iMes { get; set; }
        public int? iAnio { get; set; }
        public string sNombrePlanilla { get; set; }
        public string sNombreTipoPlanilla { get; set; }
        public string sFechaReg { get; set; }
        public string sEstadoApertura { get; set; }
        public string sFechaApertura { get; set; }
        public string sUsuarioApertura { get; set; }
        public bool bEstadoApertura { get; set; }

        public string sEstadoRegAsistencia { get; set; }
        public string sFechaRegAsistencia { get; set; }
        public bool bEstadoRegAsistencia { get; set; }
        public string sUsuarioRegAsistencia { get; set; }

        public string sEstadoDsctoFijoVariable { get; set; }
        public string sFechaDsctoFijoVariable { get; set; }
        public bool bEstadoDsctoFijoVariable { get; set; }
        public string sUsuarioDsctoFijoVariable { get; set; }

        public bool bEstadoEjecutado { get; set; }
        public string sEstadoEjecutado { get; set; }
        public string sFechaGeneracion { get; set; }
        public string sUsuarioEjecutado { get; set; }

        public string sEstadoCierre { get; set; }
        public string sFechaCierre { get; set; }
        public bool bEstadoCierre { get; set; }
        public string sUsuarioCierre { get; set; }
        public int iCodFase { get; set; }
        public string sFase { get; set; }
        public string sObservacion { get; set; }
        public Decimal dIngresos { get; set; }
        public Decimal dDescuentos { get; set; }
        public Decimal dAportes { get; set; }
        public Grilla_Response Grilla { get; set; }
    }
}
