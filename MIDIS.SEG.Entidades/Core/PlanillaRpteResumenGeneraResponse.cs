using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class PlanillaRpteResumenGeneraResponse
    {
        //public string sTipoConcepto { get; set; }
        public int iCodTipoConcepto { get; set; }
        public string sClasificadorGastoIng { get; set; }
        public string sConceptoIng { get; set; }
        public decimal? dcMontoIng { get; set; }
        public int? iCantIng { get; set; }
        public decimal? dcTotalIng { get; set; }
        public string sClasificadorGastoDscto { get; set; }
        public string sConceptoDscto { get; set; }
        public decimal? dcMontoDscto { get; set; }
        public int? iCantDscto { get; set; }
        public decimal? dcTotalDscto { get; set; }
        public string sClasificadorGastoAporte { get; set; }
        public string sConceptoAporte { get; set; }
        public decimal? dcMontoAporte { get; set; }
        public int? iCantAporte { get; set; }
        public decimal? dcTotalAportes { get; set; }
        public decimal? dcTotalLiquido { get; set; }

        
        public string sClasificadorGasto { get; set; }
        public string sConcepto { get; set; }
        public decimal? dcMonto { get; set; }
        public int? iCant { get; set; }

        public string sMes { get; set; }
        public int iAnio { get; set; }
        public string sPlanilla { get; set; }
        public string sNombreReporte { get; set; }
        
        public Grilla_Response Grilla { get; set; }
    }
}
