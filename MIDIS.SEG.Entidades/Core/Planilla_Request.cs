using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class Planilla_Request
    {
        public Int32 iCodPlanilla { get; set; }
        public String vNombrePlanilla { get; set; }
        
        public Grilla_Request Grilla { get; set; }

    }
}
