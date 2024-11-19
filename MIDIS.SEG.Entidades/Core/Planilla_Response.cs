using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class Planilla_Response
    {
        public Int32 iCodPlanilla { get; set; }
        public String vNombre { get; set; }

        public string vCodigo { get; set; }

        public Grilla_Response Grilla { get; set; }

    }
}
