using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class PlanillaRpteResumenGeneralResponse2
    {
        public int iCodTipFuenteFinanciamiento { get; set; }
        public string sSiglas { get; set; }
        public string sFuenteFinanciamiento { get; set; }
        public string sSec_Func { get; set; }
        public string sMetaPresupuestal { get; set; }
        public int iCodDependencia { get; set; }
        public string sDependencia { get; set; }
        public int iCodTipoConcepto { get; set; }
        public string sTipoConcepto { get; set; }
        public int iCodConcepto { get; set; }
        public string sConcepto { get; set; }
        public string sClasificadorGasto { get; set; }
        public string sAFP { get; set; }
        public string sNumeroDocumento { get; set; }
        public string sNombre { get; set; }
        public string sApePaterno { get; set; }
        public string sApeMaterno { get; set; }
        public string sApellidos { get { return String.Format("{0} {1},", sApePaterno, sApeMaterno); } }
        public string sNombreCompleto { get { return String.Format("{0} {1}, {2}", sApePaterno, sApeMaterno, sNombre); } }
        public string sEntidadBancaria { get; set; }
        public int iCantTrabEntBancaria { get; set; }
        public string sCuenta { get; set; }
        public string sCCI { get; set; }
        public decimal dcMontoIng { get; set; }
        public decimal dcMontoEgre { get; set; }
        public decimal dcMontoSaldo { get; set; }
        //public List<PlanillaRpteResumenGeneralPorFTE_FTO_Partida_Meta> lstObj { get; set; }
        public decimal dcMontoTotalIng { get; set; }
        public decimal dcMontoTotalEgre { get; set; }
        public decimal dcMontoTotalSaldo { get; set; }
        public string sMes { get; set; }
        public int iAnio { get; set; }
        public string sPlanilla { get; set; }
        public string sNombreReporte { get; set; }
        public string sCargo { get; set; }
        public string sFechaInicioLab { get; set; }
        public int iDiasLaborados { get; set; }
        public decimal dcMontoTotal { get; set; }
        public string sCondTrabajador { get; set; }
        public string sRegLaboral { get; set; }
        public Grilla_Response Grilla { get; set; }
    }
}
