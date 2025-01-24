using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades.Core
{
    public class CompensacionesDetalleTrabajador_Registro
    {
        public int ICodCompensacionesDetalle { get; set; }
        public int? ICodCompensaciones { get; set; }
        public int? INroDia { get; set; }
        public bool? BDiaCompleto { get; set; }
        public int? IMinutos { get; set; }
        public decimal? IHoras { get; set; }
        public DateTime? DtFechaHoraIni { get; set; }
        public DateTime? DtFechaHoraFin { get; set; }
        public string VComentario { get; set; }
        public bool? BEstado { get; set; }
        public DateTime? DtAuditCreacion { get; set; }
        public string VAuditCreacion { get; set; }
        public DateTime? DtAuditModificacion { get; set; }
        public string VAuditModificacion { get; set; }

        // Constructor por defecto
        public CompensacionesDetalleTrabajador_Registro() { }

        // Constructor con parámetros (opcional)
        public CompensacionesDetalleTrabajador_Registro(int iCodCompensaciones, int? iNroDia, bool? bDiaCompleto,
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

    public class CompensacionesDetalleFechasTrabajador_Registro
    {
        public int iCodCompensacionesDetalleFecha { get; set; }
        public int iCodCompensacionesDetalle { get; set; }
        public int iCodCompensacionesCalendario { get; set; }
        public int iCodTrabajador { get; set; }
        public int iCodigoDependencia { get; set; }
        public DateTime? dtFecha { get; set; }
        public decimal? dMinConsumidos { get; set; }
        public decimal? dMinRestantes { get; set; }
        public bool? bEstado { get; set; }
        public DateTime? dtAuditCreacion { get; set; }
        public string vAuditCreacion { get; set; }
        public DateTime? dtAuditModificacion { get; set; }
        public string vAuditModificacion { get; set; }
    }

    public class CompensacionesCalendarioTrabajador_Registro
    {
        public int iCodCompensacionesCalendario { get; set; }
        public int iCodTrabajador { get; set; }
        public int iCodigoDependencia { get; set; }
        public DateTime? dtFecha { get; set; }
        public decimal? dMinCompDisponibles { get; set; }
        public decimal? dMinConsumidos { get; set; } = 0;
        public bool? bEstado { get; set; }
        public DateTime? dtAuditCreacion { get; set; }
        public string vAuditCreacion { get; set; }
        public DateTime? dtAuditModificacion { get; set; }
        public string vAuditModificacion { get; set; }
    }
}
