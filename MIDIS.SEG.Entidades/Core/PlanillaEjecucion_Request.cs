using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class PlanillaEjecucion_Request
    {
        public Int32 iCodPlanilla { get; set; }
        public Int32 iCodTipoPlanilla { get; set; }
        public Int32 iCodDetPlanilla { get; set; }
        public Int32 iMes { get; set; }
        public Int32 iAnio { get; set; }

        public bool bEstadoCierre { get; set; }
        public bool bEstadoEjecutado { get; set; }
        //                
        public Grilla_Request Grilla { get; set; }
    }
}
