using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class ArchivosTXT_Response
    {
        public string sCodPlanilla { get; set; }
        public string sCodTipoDocumento { get; set; }
        public string sNumeroDocumento { get; set; }
        public string sCodTipFuenteFinanciamiento { get; set; }
        public string sFuenteFinanciamiento { get; set; }
        public string sCodTipoConcepto { get; set; }
        public string sTipoConcepto { get; set; }
        public string sCodConcepto { get; set; }
        public string sConcepto { get; set; }
        public string sNombreConcepto { get; set; }
        public string sCodigoExterno { get; set; }
        public decimal dcMonto { get; set; }
        public decimal dcMontoTotalIngresos { get; set; }
        public decimal dcMontoTotalDescuentos { get; set; }
        public decimal dcMontoTotalAportes { get; set; }

        public string sHorasJOR { get; set; }
        public string sCodigoTOC { get; set; }
        public string sCodigoREM { get; set; }

        public string sCodigoAIRHSP { get; set; }
        public string sCodTipRegSISPER { get; set; }
        public string sCodMCPP { get; set; }

        public Int32 iVacaciones { get; set; }
        public Int32 iLicencia_sin_Goce { get; set; }
        public Int32 iSancion_Disciplinaria { get; set; }
        public Int32 iEnfermedad_o_Accidente { get; set; }
        public Int32 iLicencia_con_Goce { get; set; }
        public Int32 iLicencia_por_Paternidad { get; set; }
        public Int32 iLicencia_por_Fallecimiento_de_Padres { get; set; }
        public Int32 iLicencia_por_Enfermedad_Grave_o_Terminal_o_Accidente_Grave_de_Familiares_Directos { get; set; }

        public int iCodTipoCondicionTrabajador { get; set; }
        public IEnumerable<String> formatos { get; set; }

        public Grilla_Response Grilla { get; set; }
    }
}
