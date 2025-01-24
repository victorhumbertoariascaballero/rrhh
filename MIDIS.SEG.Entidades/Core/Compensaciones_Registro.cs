using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades.Core
{
    public class Compensaciones_Registro
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


        public Grilla_Response Grilla { get; set; }

        public string vNombreTrabajador { get; set; }
        public string vNumeroDocumento { get; set; }
        public string vDependencia { get; set; }
        public string vTipoJustificacion { get; set; }
        public string vEstadoProceso { get; set; }




        public string vAprovadoJeje { get; set; }
        public string vFechaAprobadoJefe { get; set; }
        public string vAprobadoAdmin { get; set; }
        public string vFechaAprobadoAdmin { get; set; }
        
        public bool aprobadoAdmin { get; set; }


        public DateTime? dFechaAprobadoJefe { get; set; }
        public DateTime? dFechaAprobadoAdmin { get; set; }
        public DateTime? dFechaDenegadoAdmin { get; set; }
    }
}
