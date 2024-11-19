using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class BasesPerfilPuesto_Request
    {
        public int iCodBasePerfil { get; set; }
        public string strNroCAS { get; set; }
        public DateTime dFechaReg { get; set; }
        public DateTime dFechaMod { get; set; }
        public int iCodTrabajadorReg { get; set; }
        public int iCodTrabajadorMod { get; set; }
        public int iCantPersonalRequerido { get; set; }
        public string strDuracionContrato { get; set; }
        public bool bDuracionContrato31Diciembre { get; set; }
        public decimal decRemuneracion { get; set; }
        public DateTime dFechaAprobConv { get; set; }
        public DateTime dFechaDesdePubSNE_MTPE { get; set; }
        public DateTime dFechaHastaPubSNE_MTPE { get; set; }
        public DateTime dFechaDesdePubMIDIS { get; set; }
        public DateTime dFechaHastaPubMIDIS { get; set; }
        public DateTime dFechaRegCVPostulante { get; set; }
        public DateTime dFechaDesdeEvaCV { get; set; }
        public DateTime dFechaHastaEvaCV { get; set; }
        public DateTime dFechaPubResultadoMIDIS { get; set; }
        public DateTime dFechaDesdeEntrevista { get; set; }
        public DateTime dFechaHastaEntrevista { get; set; }
        public DateTime dFechaPubResultadoFinalMIDIS { get; set; }
        public DateTime dFechaDesdeSuscripcionContrato { get; set; }
        public DateTime dFechaHastaSuscripcionContrato { get; set; }
        public int iCodPerfil { get; set; }
        public bool bEstado { get; set; }
        public int iCodigoDependencia { get; set; }
        public string strOrgano { get; set; }
        public string strUnidadOrganica { get; set; }
        public string strNombrePuesto { get; set; }
        public string cEstadoAprobado { get; set; }
        public string strEstadoAprobado { get; set; }
        public string cEstadoPublicacion { get; set; }
        public string strEstadoPublicacion { get; set; }
        public string strMeta { get; set; }

        public string strNombreArchivo { get; set; }
        public Grilla_Response Grilla { get; set; }
    }
}
