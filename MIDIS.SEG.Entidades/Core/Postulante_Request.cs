using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
	public partial class Postulante_Request
	{
	    public Int32 IdPostulante { get; set; }
        public Int32 IdPostulacion { get; set; }
        public Int32 IdConvocatoria { get; set; }
        public Int32 IdTipoConvocatoria { get; set; }
        public Int32 IdTipoDocumento { get; set; }
        public String Nombre { get; set; }
        public String NroDocumento { get; set; }
        public String NombreProceso { get; set; }
        public Int32 Estado { get; set; }
        public Int32 Tipo { get; set; }
        public Int32 IdLegajo { get; set; }
        //public Persona_Response Persona { get; set; }

        //public Grilla_Response Grilla { get; set; }
    }
}