using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades.Core
{
    public class ReporteHorizontal_Request
    {
        public int? iCodigoDependencia { get; set; }
        public int? iCodTrabajador { get; set; }
        public int? iMes { get; set; }
            //
        public Grilla_Request Grilla { get; set; }

    }
}
