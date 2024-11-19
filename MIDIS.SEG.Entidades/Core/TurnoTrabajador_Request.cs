using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class TurnoTrabajador_Request
    {
        public int iCodTurnoTrabajador { get; set; }
        public int? iCodTurno { get; set; }
        public int? iCodTrabajador { get; set; }
        public int? iCodigoDependencia { get; set; }
        public bool bVigente { get; set; }
        public bool bEstado { get; set; }
        public DateTime dtAuditCreacion { get; set; }
        public string vAuditCreacion { get; set; }
        public DateTime dtAuditModificacion { get; set; }
        public string vAuditModificacion { get; set; }
        public Grilla_Request Grilla { get; set; }
    }
}
