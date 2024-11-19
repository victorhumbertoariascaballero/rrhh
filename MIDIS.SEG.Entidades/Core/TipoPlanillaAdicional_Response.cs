using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class TipoPlanillaAdicional_Response
    {
        public int iCodTipoPlanillaAdicional { get; set; }
        public string sNombreTipoPlanillaAdicional { get; set; }
        public bool bEstado { get; set; }

        public Grilla_Response Grilla { get; set; }
    }
}
