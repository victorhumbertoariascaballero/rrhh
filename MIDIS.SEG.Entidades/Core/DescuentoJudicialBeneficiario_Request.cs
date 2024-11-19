using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class DescuentoJudicialBeneficiario_Request
    {
        public int iCodJudicialDetalle { get; set; }
        public int iCodJudicial { get; set; }
        public int iCodTrabajador { get; set; }
        public string vDniTrabajador { get; set; }
        public string vDniBeneficiario { get; set; }
        public string vBeneficiario { get; set; }
        //
        public Grilla_Request Grilla { get; set; }

    }
}
