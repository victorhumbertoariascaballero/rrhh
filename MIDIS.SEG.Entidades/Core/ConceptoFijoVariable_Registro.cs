using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class ConceptoFijoVariable_Registro
    {
        public int iCodTrabajador {get; set;}
        public int iCodPlanilla {get; set;}
        public int iCodTipoPlanilla {get; set;}
        public int iCodDetPlanilla { get; set; }
        public int iMes {get; set;}
        public int iAnio {get; set;}
        public int iCodConcepto {get; set;}
        public int iCodTipoConcepto {get; set;}
        public int iCodSubTipoConcepto {get; set;}
        public decimal dMonto {get; set;}
        public string dFechaReg { get; set; }
        public int iDiaSubsidio { get; set; }
        // Atributos relacionados
        public Int64 iNro { get; set; }
        public Int32 iCodTipoDocumento { get; set; }
        public string vTrabajador { get; set; }
        public string vNroDocumento { get; set; }
        public string vTipoDocumento { get; set; }
        public string vNombrePlanilla { get; set; }
        public string vTipoPlanilla { get; set; }
        public string vConcepto { get; set; }
        public string vTipoConcepto { get; set; }
        public string vSubTipoConcepto { get; set; }
        public string vCodigoPlanilla { get; set; }
        public IEnumerable<String> formatos { get; set; }
        public Grilla_Response Grilla { get; set; }
    }
}
