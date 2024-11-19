using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
	public partial class ConvocatoriaDocumento_Registro
	{
	    public Int32 IdConvocatoria { get; set; }
        public Int32 IdConvocatoriaDocumento { get; set; }
        public Int32 IdTipoDocumento { get; set; }
        public Int32 Estado { get; set; }
        public String NombreDocumento { get; set; } // { return ((Estado == 1) ? "PENDIENTE" : ((Estado == 2) ? "EN PROCESO" : ((Estado == 3) ? "CONCLUIDA" : ((Estado == 5) ? "CANCELADA" : String.Empty)))); } }
        public Int32 IdResponsableEvaluacion { get; set; }
        public String ResponsableEvaluacion { get; set; }
        public String TipoDocumento { get; set; }
        public String FechaEvaluacion { get; set; }
        
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public Int32? IdUsuarioRegistro { get; set; }
        public Int32? IdUsuarioModificacion { get; set; }
        
        public byte[] archivo { get; set; }
        public IEnumerable<object> formatos { get; set; }
        
        public Grilla_Response Grilla { get; set; }
	}
}