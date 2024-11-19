using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
	public partial class Convocatoria_Request
	{
	    public Int32 IdConvocatoria { get; set; }
        public Int32 IdConvocatoriaDocumento { get; set; }
        public Int32 IdDependencia { get; set; }
        public Int32 IdTipo { get; set; } //1: CAS  2: PRACTICAS
        public Int32 IdOrgano { get; set; }
        public String NroCAS { get; set; }
        public String NombreCargo { get; set; }
        public Int32 Estado { get; set; }
        public Int32 IdUsuarioRegistro { get; set; }
        public DateTime? RegistroFechaCreacionEntreDesde { get; set; }
        public DateTime? RegistroFechaCreacionEntreHasta { get; set; }
        public String Anio { get; set; }
        public String Mes { get; set; }
        //public Persona_Response Persona { get; set; }

        //public Grilla_Response Grilla { get; set; }
	}
}