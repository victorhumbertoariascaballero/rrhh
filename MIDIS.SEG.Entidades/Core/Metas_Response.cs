using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public class Metas_Response
    {
        public int iCodMeta { get; set; }
        public string sSec_Func { get; set; }
        public string sFinal_COD { get; set; }
        public string sMeta { get; set; }
        public bool bEstado { get; set; }
        public int iAnnoMeta { get; set; }
        public decimal dMonto { get; set; }
        //public Grilla_Response Grilla { get; set; }
    }
}
