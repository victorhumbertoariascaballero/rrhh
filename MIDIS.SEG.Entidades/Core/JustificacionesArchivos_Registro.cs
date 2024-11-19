using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class JustificacionesArchivos_Registro
    {
		public int iCodJustificacionesArchivos { get; set; }
		public int iCodJustificaciones { get; set; }
        public string vObservaciones { get; set; }
        public string vUrlArchivo { get; set; }
        public bool bEstado { get; set; }
        public DateTime dtAuditCreacion { get; set; }
        public string vAuditCreacion { get; set; }
        public DateTime dtAuditModificacion { get; set; }
        public string vAuditModificacion { get; set; }
        public Grilla_Response Grilla { get; set; }
    }
}
