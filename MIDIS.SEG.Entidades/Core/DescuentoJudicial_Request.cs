using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class DescuentoJudicial_Request
    {
        public int iCodJudicial { get; set; }
        public int iCodTrabajador { get; set; }
        public int iCodPlanilla { get; set; }
        public int iCodTipoPlanilla { get; set; }
        public int iCodDetPlanilla { get; set; }
        public int iMes { get; set; }
        public int iAnio { get; set; }
        //
        public string vDniTrabajador { get; set; }
        public string vNombreTrabajador { get; set; }
        public string vBeneficiario { get; set; }

        public Grilla_Request Grilla { get; set; }

    }
}
