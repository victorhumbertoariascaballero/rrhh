using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class ConceptoFijoVariable_Request
    {
        public int iCodTrabajador { get; set; }
        public int iCodPlanilla { get; set; }
        public int iCodTipoPlanilla { get; set; }
        public int iCodDetPlanilla { get; set; }
        public int iMes { get; set; }
        public int iAnio { get; set; }
        public int iCodConcepto { get; set; }
        public int iCodTipoConcepto { get; set; }
        public int iCodSubTipoConcepto { get; set; }
        public int iNroDias { get; set; }

        public Grilla_Request Grilla { get; set; }

    }
}
