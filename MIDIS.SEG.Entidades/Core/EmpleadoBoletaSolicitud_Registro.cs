using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
    public partial class EmpleadoBoletaSolicitud_Registro
    {
	    public Int32 IdSolicitud { get; set; }
        public String Documento { get; set; }
        public String Clave { get; set; }
        public Int32 Estado { get; set; }
        public String EstadoNombre { get; set; }
        public String FechaRegistro { get; set; }
        //public byte[] Boleta { get; set; }
		
        public Grilla_Response Grilla { get; set; }
	}
}