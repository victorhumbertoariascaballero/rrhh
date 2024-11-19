using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
    public partial class PostulanteDocumento_Registro
	{
	    public Int32 IdPostulante { get; set; }
        public Int32 IdDocumento { get; set; }
        public Int32 IdTipoDocumento { get; set; }
        public String NombreTipoDocumento { get; set; }
        public Int32 Estado { get; set; }
        
        public Int32? IdUsuarioRegistro { get; set; }
        public Int32? IdUsuarioModificacion { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public IEnumerable<object> formatos { get; set; }
        public Int32 IdTieneArchivo { get; set; }
        public byte[] FileArchivo { get; set; }
        
        public Grilla_Response Grilla { get; set; }
	}
}