using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades.Core
{
    public class CompensacionesTrabajador_Registro
    {
        public int ICodCompensaciones { get; set; }
        public int? ICodTrabajador { get; set; }
        public int? ICodigoDependencia { get; set; }
        public int? ICodEstadoProceso { get; set; }
        public int? Horas { get; set; }
        public bool? Exacto { get; set; }
        public DateTime? DtFechaCompensacion { get; set; }
        public string VDescripcion { get; set; }
        public bool? BEstado { get; set; }
        public DateTime? DtAuditCreacion { get; set; }
        public string VAuditCreacion { get; set; }
        public DateTime? DtAuditModificacion { get; set; }
        public string VAuditModificacion { get; set; }

        // Relación con procesos de la compensacion
        public List<CompensacionesProceso> Procesos { get; set; } = new List<CompensacionesProceso>();

        public bool aprobarDenegar { get; set; }
        public bool adminJefe { get; set; }
        public string vIds { get; set; }
        public bool resMasivo { get; set; }
        public double? IHoras { get; set; }
    }

    public class CompensacionesProceso
    {
        public int ICodCompensacionesProceso { get; set; }
        public int ICodCompensaciones { get; set; }
        public int? ICodEstadoProceso { get; set; }
        public string VComentario { get; set; }
        public bool? BEstado { get; set; }
        public DateTime? DtAuditCreacion { get; set; }
        public string VAuditCreacion { get; set; }
        public DateTime? DtAuditModificacion { get; set; }
        public string VAuditModificacion { get; set; }

        public Grilla_Response Grilla { get; set; }
    }
}
