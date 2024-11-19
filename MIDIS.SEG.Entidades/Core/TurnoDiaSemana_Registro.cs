using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class TurnoDiaSemana_Registro
    {
        public int iCodTurnoDiaSemana { get; set; }
        public int iCodTurno { get; set; }
        public int iCodDiaSemana { get; set; }
		public DateTime tHoraEntrada { get; set; }
		public decimal dToleranciaEntrada { get; set; }
		public DateTime dtRangoMarcaEntradaInicio { get; set; }
		public DateTime dtRangoMarcaEntradaFin { get; set; }
		public DateTime tHoraSalida { get; set; }
		public decimal dToleranciaSalida { get; set; }
		public DateTime dtRangoMarcaSalidaInicio { get; set; }
		public DateTime dtRangoMarcaSalidaFin { get; set; }
		public bool bRefrigerioFlexible { get; set; }
		public decimal dTiempoRefrigerio { get; set; }
		public DateTime dtRangoMarcaRefrigerioInicio { get; set; }
		public DateTime dtRangoMarcaRefrigerioFin { get; set; }
        public bool bEstado { get; set; }
        public DateTime dtAuditCreacion { get; set; }
        public string vAuditCreacion { get; set; }
        public DateTime dtAuditModificacion { get; set; }
        public string vAuditModificacion { get; set; }
        public Grilla_Response Grilla { get; set; }
    }
}
