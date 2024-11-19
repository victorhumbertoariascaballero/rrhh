using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class TipoMarcaciones_Registro
    {
        public int iCodTipoMarcacion { get; set; }
        public string vDescripcion { get; set; }
        public bool bEstado { get; set; }
        public DateTime? dtAuditCreacion { get; set; }
        public string vAuditCreacion { get; set; }
        public DateTime dtAuditModificacion { get; set; }
        public string vAuditModificacion { get; set; }

    }
}
