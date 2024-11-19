using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class Concepto_Request
    {
        public int iCodConcepto { get; set; }
        public int iCodTipoConcepto { get; set; }
        public int iCodSubTipoConcepto { get; set; }

        public string vConcepto { get; set; }
        public string vTipoConcepto { get; set; }
        public string vSubTipoConcepto { get; set; }
        public bool bRegCAS { get; set; }
        public bool bRegFunc { get; set; }
        public bool bRegSeci { get; set; }
        public bool bRegPracticantes { get; set; }
        public bool bRegConceptoBaseImponible { get; set; }
        public bool bRegCalculoAutomatico { get; set; }

        public Grilla_Request Grilla { get; set; }
    }
}
