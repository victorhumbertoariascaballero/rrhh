using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
	public partial class Contrato_Request
	{
	    public Int32 IdContrato { get; set; }
		public Int32 IdDependencia { get; set; }
        public Int32 IdCondicion { get; set; }
        public Int32 IdSede { get; set; }
        public String Nombre { get; set; }
        public String NroDocumento { get; set; }
        public String NroContrato { get; set; }
        public Int32 Estado { get; set; }
		
        //public Persona_Response Persona { get; set; }

        //public Grilla_Response Grilla { get; set; }
	}
}