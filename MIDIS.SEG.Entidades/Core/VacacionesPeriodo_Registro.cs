using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades.Core
{
    public class VacacionesPeriodo_Registro
    {
        public int iCodVacacionesPeriodo { get; set; }
        public int iCodTrabajador { get; set; }
        public int iCodigoDependencia { get; set; }
        public int iPeriodo { get; set; }
        public DateTime dtFechaInicio { get; set; }
        public DateTime dtFechaFin { get; set; }
        public decimal iProgramados { get; set; }
        public decimal iAsignados { get; set; }
        public decimal iDisponibles { get; set; }
        public decimal iFraccionamiento { get; set; }
        public DateTime dtAuditCreacion { get; set; }
        public string vAuditCreacion { get; set; }
        public DateTime dtAuditModificacion { get; set; }
        public string vAuditModificacion { get; set; }
        public Grilla_Response Grilla { get; set; }

        public string vNombreTrabajador { get; set; }
        public string vNumeroDocumento { get; set; }
        public string vDependencia { get; set; }
    }
}
