using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
    public partial class PostulacionCapacitacion_Registro
	{
        public Int32 IdPostulacionCapacitacion { get; set; }
	    public Int32 IdPostulacion { get; set; }
        public Int32 IdPostulante { get; set; }
        public Int32 IdCapacitacion { get; set; }
        public String Especialidad { get; set; }
        public String Institucion { get; set; }
        public String Ciudad { get; set; }
        public String FechaInicio { get; set; }
        public String FechaFin { get; set; }
        public Int32 Horas { get; set; }
        public Int32 Estado { get; set; }
        
        public Int32? IdUsuarioRegistro { get; set; }
        public Int32? IdUsuarioModificacion { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public IEnumerable<object> formatos { get; set; }
        public Int32 IdTieneArchivo { get; set; }
        public byte[] FileArchivo { get; set; }
        public Int32 IdCapacitacionPerfil { get; set; }
        public Int32 IdCapacitacionAuditoria { get; set; }
        public Int32 IdTipoConvocatoria { get; set; }
        public Int32 IdTipoActualizacion { get; set; }
        public Estado_Response Requisito { get; set; }
        public Estado_Response Auditoria { get; set; }
        public Grilla_Response Grilla { get; set; }
        public String MateriaDescripcion { get; set; }
        public String MateriaOtrosDescripcion { get; set; }
        public Int32? iTipoNivelMateria { get; set; }
        public Int32 bOtroTipoEstudio { get; set; }
	}
}