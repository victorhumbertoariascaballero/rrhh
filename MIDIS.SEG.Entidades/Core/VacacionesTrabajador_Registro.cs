using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MIDIS.ORI.Entidades.Core
{
    public class VacacionesTrabajador_Registro
    {
        public int iCodVacaciones { get; set; }
        public int iCodTipoVacaciones { get; set; }
        public int? iCodTrabajador { get; set; }
        public int? iCodigoDependencia { get; set; }
        public int? iCodEstadoProceso { get; set; }
        public bool? bFraccionamientoVacacionalMediaJornada { get; set; }
        public DateTime? dtFechaInicio { get; set; }
        public DateTime? dtFechaFin { get; set; }
        public string vDescripcion { get; set; }
        public int? iPeriodo { get; set; }
        public int? iDisponibles { get; set; }
        public int iDiasRestantes { get; set; }
        public int iDiasAsignados { get; set; }
        public int iAsignados { get; set; }
        public bool bEstado { get; set; }
        public DateTime? dtAuditCreacion { get; set; }
        public string vAuditCreacion { get; set; }
        public DateTime? dtAuditModificacion { get; set; }
        public string vAuditModificacion { get; set; }

        // Relación con archivos de vacaciones
        public List<VacacionesArchivo> Archivos { get; set; } = new List<VacacionesArchivo>();

        // Relación con procesos de vacaciones
        public List<VacacionesProceso> Procesos { get; set; } = new List<VacacionesProceso>();

        public List<HttpPostedFileBase> filesUpload { get; set; }

        public HttpPostedFileBase filesUploadFormato { get; set; }
        public bool aprobarDenegar { get; set; }
        public bool adminJefe { get; set; }
        public string vIds { get; set; }
        public bool resMasivo { get; set; }
    }

    public class VacacionesArchivo
    {
        public int iCodVacacionesArchivos { get; set; }
        public int iCodVacaciones { get; set; }  // Relación con Vacaciones
        public int iCodTipoVacacionesFormato { get; set; }  // Relación con Vacaciones
        public string vObservaciones { get; set; }
        public string vUrlArchivo { get; set; }
        public bool bEstado { get; set; }
        public DateTime? dtAuditCreacion { get; set; }
        public string vAuditCreacion { get; set; }
        public DateTime? dtAuditModificacion { get; set; }
        public string vAuditModificacion { get; set; }

        // Relación con la vacaciones
        public VacacionesTrabajador_Registro Vacaciones { get; set; }

        public string vNombre
        {
            get
            {
                // Encontrar la posición del último guion bajo
                int lastIndex = vUrlArchivo.LastIndexOf('_');

                // Extraer todo después del último guion bajo
                string resultado = vUrlArchivo.Substring(lastIndex + 1);
                return resultado;
            }
        }
    }

    public class VacacionesProceso
    {
        public int iCodVacacionesProceso { get; set; }
        public int iCodVacaciones { get; set; }  // Relación con Vacaciones
        public int iCodEstadoProceso { get; set; }
        public string vComentario { get; set; }
        public bool bEstado { get; set; }
        public DateTime? dtAuditCreacion { get; set; }
        public string vAuditCreacion { get; set; }
        public DateTime? dtAuditModificacion { get; set; }
        public string vAuditModificacion { get; set; }

        // Relación con la vacaciones
        public VacacionesTrabajador_Registro Vacaciones { get; set; }
    }
}
