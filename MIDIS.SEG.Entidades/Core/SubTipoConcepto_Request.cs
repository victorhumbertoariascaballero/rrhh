using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class SubTipoConcepto_Request
    {
        public Int32 iCodSubTipoConcepto { get; set; }
        public Int32 iCodTipoConcepto { get; set; }
        public String vNombre { get; set; }
        public Boolean bEstado { get; set; }

        public Grilla_Response Grilla { get; set; }
    }
}
