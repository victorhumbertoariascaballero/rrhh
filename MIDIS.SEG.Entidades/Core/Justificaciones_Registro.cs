using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class Justificaciones_Registro
    {	
		public int iCodJustificaciones { get; set; }
        public int iCodTrabajador { get; set; }
        public int iCodigoDependencia { get; set; }		
        public int iCodTipoJustificacion { get; set; }		
        public int iCodTipGoce { get; set; }		
        public int iCodEstadoProceso { get; set; }		
        public bool bVigente { get; set; }
        public bool bMadrugada { get; set; }
        public DateTime dtFechaHoraInicio { get; set; }
        public DateTime dtFechaHoraFin { get; set; }
        public string vDescripcion { get; set; }
        public bool bEstado { get; set; }
        public DateTime dtAuditCreacion { get; set; }
        public string vAuditCreacion { get; set; }
        public DateTime dtAuditModificacion { get; set; }
        public string vAuditModificacion { get; set; }
        public Grilla_Response Grilla { get; set; }
    }
}
