using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class Conceptos_Response
    {
        public int iCodConcepto { get; set; }

        public string vConcepto { get; set; }
        public string vTipoConcepto { get; set; }
        public string vSubTipoConcepto { get; set; }
        public string vRegCAS { get; set; }
        public string vRegFunc { get; set; }
        public string vRegSeci { get; set; }

        public Conceptos_Response(string _vConcepto, string _vTipoConcepto, string _vSubTipoConcepto, string _vRegCAS, string _vRegFunc, string _vRegSeci)
        {
            vConcepto = _vConcepto;
            vTipoConcepto = _vTipoConcepto;
            vSubTipoConcepto = _vSubTipoConcepto;
            vRegCAS = _vRegCAS;
            vRegFunc = _vRegFunc;
            vRegSeci = _vRegSeci;            
        }
    }
}
