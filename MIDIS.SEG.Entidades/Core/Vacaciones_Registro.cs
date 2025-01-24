using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades.Core
{
    public class Vacaciones_Registro
    {
        public int iCodVacaciones { get; set; }
        public int iCodTipoVacaciones { get; set; }
        public int iCodTrabajador { get; set; }
        public int iCodigoDependencia { get; set; }
        public int iCodEstadoProceso { get; set; }
        public int iDiasRestantes { get; set; }
        public int iDiasAsignados { get; set; }
        public int iAsignados { get; set; }
        public int iDisponibles { get; set; }
        public int iPeriodo { get; set; }
        public DateTime dtFechaInicio { get; set; }
        public DateTime dtFechaFin { get; set; }
        public string vDescripcion { get; set; }
        public bool bEstado { get; set; }
        public bool bFraccionamientoVacacionalMediaJornada { get; set; }
        public DateTime dtAuditCreacion { get; set; }
        public string vAuditCreacion { get; set; }
        public DateTime dtAuditModificacion { get; set; }
        public string vAuditModificacion { get; set; }
        public Grilla_Response Grilla { get; set; }

        public string vNombreTrabajador { get; set; }
        public string vNumeroDocumento { get; set; }
        public string vDependencia { get; set; }
        public string vEstadoProceso { get; set; }
        public string vTipoVacaciones { get; set; }
        public string vUrlArchivo { get; set; }

        public DateTime dFechaAprobadoJefe { get; set; }
        public DateTime dFechaAprobadoAdmin { get; set; }
        public DateTime dFechaDenegadoAdmin { get; set; }

        public string vAprovadoJeje { get; set; }
        public string vFechaAprobadoJefe { get; set; }
        public string vAprobadoAdmin { get; set; }
        public string vFechaAprobadoAdmin { get; set; }
    }
}
