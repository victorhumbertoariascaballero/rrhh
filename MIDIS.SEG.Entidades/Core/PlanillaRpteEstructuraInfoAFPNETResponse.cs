using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class PlanillaRpteEstructuraInfoAFPNETResponse
    {
        public string sCuspp { get; set; }
        public int iCodigoTipoDocumento { get; set; }
        public string sNumeroDocumento { get; set; }
        public string sNombre { get; set; }
        public string sApePaterno { get; set; }
        public string sApeMaterno { get; set; }
        public string sNombreCompleto { get { return String.Format("{0} {1}, {2}", sApePaterno, sApeMaterno, sNombre); } }
        public string sRelacionLaboral { get; set; }
        public string sInicioRL { get; set; }
        public string sCeseRL { get; set; }
        public string sExcepcionAportar { get; set; }
        public string sTipoTrabajo { get; set; }       
        public decimal dcRemuneracion { get; set; }
        public decimal dcAporteVoluntario { get; set; }
        public int iMes { get; set; }
        public string sMes { get; set; }
        public int iAnio { get; set; }
        public string sPlanilla { get; set; }
        public string sNombreReporte { get; set; }
        public Grilla_Response Grilla { get; set; }
    }
}
