using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MIDIS.ORI.Entidades.Core
{
    public class JustificacionesTrabajador_Registro
    {
        public int iCodJustificaciones { get; set; }
        public int? iCodTrabajador { get; set; }
        public int? iCodigoDependencia { get; set; }
        public int? iCodMotivoJustificacion { get; set; }
        public int? iCodTipoJustificacion { get; set; }
        public int? iCodTipGoce { get; set; }
        public int? iCodEstadoProceso { get; set; }
        public bool? bMadrugada { get; set; }
        public DateTime? dtFechaHoraInicio { get; set; }
        public DateTime? dtFechaHoraFin { get; set; }
        public string vDescripcion { get; set; }
        public bool bEstado { get; set; }
        public DateTime? dtAuditCreacion { get; set; }
        public string vAuditCreacion { get; set; }
        public DateTime? dtAuditModificacion { get; set; }
        public string vAuditModificacion { get; set; }

        // Relación con archivos de la justificación
        public List<JustificacionArchivo> Archivos { get; set; } = new List<JustificacionArchivo>();

        // Relación con procesos de la justificación
        public List<JustificacionProceso> Procesos { get; set; } = new List<JustificacionProceso>();

        public List<HttpPostedFileBase> filesUpload { get; set; }

        public bool aprobarDenegar { get; set; }
        public bool adminJefe { get; set; }
        public string vIds { get; set; }
        public bool resMasivo { get; set; }
    }



    public class JustificacionArchivo
    {
        public int iCodJustificacionesArchivos { get; set; }
        public int iCodJustificaciones { get; set; }  // Relación con Justificación
        public string vObservaciones { get; set; }
        public string vUrlArchivo { get; set; }
        public bool bEstado { get; set; }
        public DateTime? dtAuditCreacion { get; set; }
        public string vAuditCreacion { get; set; }
        public DateTime? dtAuditModificacion { get; set; }
        public string vAuditModificacion { get; set; }

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

        // Relación con la justificación
        public JustificacionesTrabajador_Registro Justificacion { get; set; }
    }

    public class JustificacionProceso
    {
        public int iCodJustificacionesProceso { get; set; }
        public int iCodJustificaciones { get; set; }  // Relación con Justificación
        public int iCodEstadoProceso { get; set; }
        public bool bEstado { get; set; }
        public string vComentario { get; set; }
        public DateTime? dtAuditCreacion { get; set; }
        public string vAuditCreacion { get; set; }
        public DateTime? dtAuditModificacion { get; set; }
        public string vAuditModificacion { get; set; }

        // Relación con la justificación
        public JustificacionesTrabajador_Registro Justificacion { get; set; }
    }
}
