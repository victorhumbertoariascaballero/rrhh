using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class RegistroRegimenPensionario_Request
    {
        public Int32 iCodRegistroRegimenPen { get; set; }
        public Int32 iCodRegimenPen { get; set; }
        public Int32 iCodTipoRegimenPen { get; set; }
        public Int32 iCodAPF { get; set; }
        public Int32 iMes { get; set; }
        public Int32 iAnio { get; set; }
        public String sNombre { get; set; }
        public string sEstado { get; set; }
        public bool bEstado { get; set; }

        public Grilla_Request Grilla { get; set; }
    }
}
