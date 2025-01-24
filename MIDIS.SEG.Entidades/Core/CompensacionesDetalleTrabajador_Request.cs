using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades.Core
{
    public class CompensacionesDetalleTrabajador_Request
    {
        public int ICodCompensacionesDetalle { get; set; }
        public int? iCodEstadoProceso { get; set; }
        public int? iCodigoDependencia { get; set; }
        public int? iCodTrabajador { get; set; }
        public int? ICodCompensaciones { get; set; }
        public int? INroDia { get; set; }
        public bool? BDiaCompleto { get; set; }
        public int? IMinutos { get; set; }
        public decimal? IHoras { get; set; }
        public DateTime? DtFecha { get; set; }
        public DateTime? DtFechaHoraIni { get; set; }
        public DateTime? DtFechaHoraFin { get; set; }
        public string VComentario { get; set; }
        public bool? BEstado { get; set; }
        public DateTime? DtAuditCreacion { get; set; }
        public string VAuditCreacion { get; set; }
        public DateTime? DtAuditModificacion { get; set; }
        public string VAuditModificacion { get; set; }

        public string vNombreTrabajador { get; set; }
        public string vNumeroDocumento { get; set; }
        public string vDependencia { get; set; }
        public string vEstadoProceso { get; set; }
        public DateTime? dFechaAprobadoJefe { get; set; }
        public DateTime? dFechaAprobadoAdmin { get; set; }
        public DateTime? dFechaDenegadoAdmin { get; set; }
        public String DtFechaHorasExtra { get; set; }

        public bool adminJefe { get; set; }
        public bool aprobarDenegar { get; set; }
        public string vIds { get; set; }
        public bool resMasivo { get; set; }
        public List<CompensacionesDetalleFechasTrabajador_Registro> DetalleFechas { get; set; }

        // Constructor por defecto
        public CompensacionesDetalleTrabajador_Request() { }

        // Constructor con parámetros (opcional)
        public CompensacionesDetalleTrabajador_Request(int iCodCompensaciones, int? iNroDia, bool? bDiaCompleto,
            int? iMinutos, decimal? iHoras, DateTime? dtFechaHoraIni, DateTime? dtFechaHoraFin,
            string vComentario, bool? bEstado, DateTime? dtAuditCreacion, string vAuditCreacion,
            DateTime? dtAuditModificacion, string vAuditModificacion)
        {
            ICodCompensaciones = iCodCompensaciones;
            INroDia = iNroDia;
            BDiaCompleto = bDiaCompleto;
            IMinutos = iMinutos;
            IHoras = iHoras;
            DtFechaHoraIni = dtFechaHoraIni;
            DtFechaHoraFin = dtFechaHoraFin;
            VComentario = vComentario;
            BEstado = bEstado;
            DtAuditCreacion = dtAuditCreacion;
            VAuditCreacion = vAuditCreacion;
            DtAuditModificacion = dtAuditModificacion;
            VAuditModificacion = vAuditModificacion;
        }
    }

    public class CompensacionesDetalleProceso
    {
        public int iCodCompensacionesDetalleProceso { get; set; }
        public int iCodCompensacionesDetalle { get; set; }
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
