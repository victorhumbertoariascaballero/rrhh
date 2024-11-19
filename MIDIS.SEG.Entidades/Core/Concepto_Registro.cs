using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class Concepto_Registro
    {
        public int iCodConcepto { get; set; }
        public int iCodTipoConcepto { get; set; }
        public int iCodSubTipoConcepto { get; set; }
        public string vConcepto { get; set; }
        public string vAbreviatura { get; set; }
        public bool bRegCAS { get; set; }
        public bool bRegFunc { get; set; }
        public bool bRegSeci { get; set; }
        public bool bRegConceptoBaseImponible { get; set; }
        public bool bRegCalculoAutomatico { get; set; }
        public bool bEstado { get; set; }
        //Atributos Relacionados
        public string vTipoConcepto { get; set; }
        public string vSubTipoConcepto { get; set; }
        public string vRegCAS { get; set; }
        public string vRegFunc { get; set; }
        public string vRegSeci { get; set; }
        public string vCodigoExterno { get; set; }
        public string vCodigoMCPP { get; set; }
        public string vCodigoMEF { get; set; }
        public string vRegConceptoBaseImponible { get; set; }
        public string vRegCalculoAutomatico { get; set; }
        public string vClasificadorGasto { get; set; }
        public Grilla_Response Grilla { get; set; }
        
        
    }
}
