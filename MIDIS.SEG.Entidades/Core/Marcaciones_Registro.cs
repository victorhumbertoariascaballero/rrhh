using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class Marcaciones_Registro
    {
        public int iCodMarcaciones { get; set; }
        public int iCodTrabajador { get; set; }
        public int iCodigoDependencia { get; set; }
        public int iCodTipoMarcacion { get; set; }
        public DateTime dtFechaMarcacion { get; set; }
        public string vLongitud { get; set; }
        public string vLatitud { get; set; }
        public string vIpCliente { get; set; }
        public bool bEstado { get; set; }
        public DateTime dtAuditCreacion { get; set; }
        public string vAuditCreacion { get; set; }
        public DateTime dtAuditModificacion { get; set; }
        public string vAuditModificacion { get; set; }
        public string vDependencia { get; set; }
        public string vNombreTrabajador { get; set; }
        public string vTipoMarcacion { get; set; }
        public string vNumeroDocumento { get; set; }
        public Grilla_Response Grilla { get; set; }
    }
}
