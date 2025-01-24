using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class MotivoJustificacion_Registro
    {
        
        public int iCodMotivoJustificacion { get; set; }
        public int iCodTipoJustificacion { get; set; }
        public string vDescripcion { get; set; }
        public bool bConGoce { get; set; }
        public bool bEstado { get; set; }
        public bool bBloquearGoce { get; set; }
        public bool bBloquearTipoJustificacion { get; set; }
        public DateTime dtAuditCreacion { get; set; }
        public string vAuditCreacion { get; set; }
        public DateTime dtAuditModificacion { get; set; }
        public string vAuditModificacion { get; set; }
        public Grilla_Response Grilla { get; set; }
    }
}
