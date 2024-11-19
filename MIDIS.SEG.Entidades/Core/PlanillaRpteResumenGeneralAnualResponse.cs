using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class PlanillaRpteResumenGeneralAnualResponse
    {
        public int iCodPlanilla { get; set; }
        public int iCodDetPlanilla { get; set; }
        public string sRUC { get; set; }
        public string sNumeroDocumento { get; set; }
        public int iCodTipoCondicionTrabajador { get; set; }
        public int iCodTrabajador { get; set; }
        public string sNombre { get; set; }
        public string sApePaterno { get; set; }
        public string sApeMaterno { get; set; }
        public string sNombreCompleto { get { return String.Format("{0} {1}, {2}", sApePaterno, sApeMaterno, sNombre); } }
        public string sDependencia { get; set; }
        public string sSiglas { get; set; }
        public string sSec_Func { get; set; }
        public string sCargo { get; set; }
        public decimal dcRemuneracion { get; set; }
        public string sNroCta { get; set; }
        public decimal dcTotalIng { get; set; }
        public decimal dcRet4ta { get; set; }
        public int iMes { get; set; }
        public string sMes { get; set; }
        public int iAnio { get; set; }
        public string sPlanilla { get; set; }
        public string sTipoPlanilla { get; set; }
        public int iCodTipoConcepto { get; set; }
        public string sTipoConcepto { get; set; }
        public string sConcepto { get; set; }
        public string sNombreReporte { get; set; }
        public string sClasificadorGasto { get; set; }
        public Grilla_Response Grilla { get; set; }
    }
}
