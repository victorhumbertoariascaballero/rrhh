using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
    public partial class PostulantePostulacion_Registro
	{
	    public Int32 Nro { get; set; }
        public Int32 IdPostulacion { get; set; }
        public Int32 IdPostulante { get; set; }
        public Int32 IdPostulantePostulacion { get; set; }
        public Int32 IdConvocatoria { get; set; }
        public String TipoDocumento { get; set; }
        public String NroDocumento { get; set; }
        public String NombreCompleto { get { return String.Format("{0} {1}, {2}", Paterno, Materno, Nombre); } }
        public String Nombre { get; set; }
        public String Paterno { get; set; }
        public String Materno { get; set; }
        public String NombreProceso { get; set; }
        public String NombreCargo { get; set; }
        public String FechaPostulacion { get; set; }
        public String FechaMaximaContrato { get; set; }
        
        public Int32 AptoCurricular { get; set; }
        public Int32 AptoEvaluacion { get; set; }
        public Int32 AptoResultado { get; set; }
        public Int32 AptoContrato { get; set; }
        public String Contrasena { get; set; }
        //public String EstadoNombre { get { return ((Estado == 0) ? "INACTIVO" : ((Estado == 1) ? "ACTIVO" : ((Estado == 2) ? "NO REGISTRADO" : ""))); } }
        

        public Int32? IdUsuarioRegistro { get; set; }
        public Int32? IdUsuarioModificacion { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
        
        public Grilla_Response Grilla { get; set; }
	}
}