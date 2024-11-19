using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class TipoConcepto_Request
    {
        public Int32 iCodTipoConcepto { get; set; }
        public String vNombre { get; set; }
        
        public Boolean bEstado { get; set; }

        public Grilla_Request Grilla { get; set; }
    }
}
