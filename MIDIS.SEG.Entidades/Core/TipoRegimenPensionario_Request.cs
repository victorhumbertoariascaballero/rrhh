using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class TipoRegimenPensionario_Request
    {
        public Int32 iCodTipoRegimenPen { get; set; }
        public String vNombre { get; set; }

        public Grilla_Request Grilla { get; set; }

    }
}
