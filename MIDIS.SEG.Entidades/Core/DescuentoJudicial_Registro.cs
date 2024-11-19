using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class DescuentoJudicial_Registro
    {
         public int iCodJudicial {get; set; }
         public int iCodTrabajador {get; set; }
         public int iCodPlanilla {get; set; }
         public int iCodTipoPlanilla {get; set; }
         public int iCodDetPlanilla { get; set; }
         public int iMes {get; set; }
         public int iAnio {get; set; }
         public int iCodConcepto {get; set; }
         public int iCodTipoConcepto {get; set; }
         public int iCodSubTipoConcepto {get; set; }

         public decimal dMontoRetencionTotal {get; set; }
         

         public bool bEstado {get; set; }
         public DateTime dFechaReg { get; set; }
         //
         public Int64 iNro { get; set; }
         public Int32 iCodTipoDocumento { get; set; }
         public string vTipoDocumento { get; set; }
         public string vNroDocumentoTrabajador { get; set; }
         public string vTrabajador { get; set; }
        
         public string vNombrePlanilla { get; set; }
         public string vTipoPlanilla { get; set; }
         public string vConcepto { get; set; }
         public string vCodigoPlanilla { get; set; }
         //
         public Int32 iResultado { get; set; }
         public int iValidarCambio { get; set; }
        
         public Grilla_Response Grilla { get; set; }
         public IEnumerable<String> detBeneficiarios { get; set; }
    }
}
