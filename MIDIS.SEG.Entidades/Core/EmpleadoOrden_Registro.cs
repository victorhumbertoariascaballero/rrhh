using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
    public partial class EmpleadoOrden_Registro
	{
	    public Int32 IdEmpleadoOrden { get; set; }
        public Int32 IdEmpleado { get; set; }
        public String NroOrden { get; set; }
        public String NroSIAF { get; set; }
        public Int32 IdEstado { get; set; }
        public Int32 Duracion { get; set; }
        public String Nombre { get; set; }
        public Decimal Monto { get; set; }
        public String FechaInicio { get; set; }
        public String FechaFin { get; set; }
        public Int32? IdUsuarioRegistro { get; set; }
        public Int32? IdUsuarioModificacion { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public Estado_Response Estado { get; set; }
        
        public Grilla_Response Grilla { get; set; }
	}
}