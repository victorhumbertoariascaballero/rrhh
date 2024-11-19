using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
	public partial class BoletaCarga_Registro
	{
    	public String Anio { get; set; }
		public String Mes { get; set; }
        public Int32 IdEmpleado { get; set; }
        public String NroDocumento { get; set; }
        public String NombreArchivo { get; set; }
        public String TipoPlanilla { get; set; }
        public String IdPlanilla { get; set; }
        public String Trabajador { get; set; }
        public Int32? IdUsuarioRegistro { get; set; }
        public Int32? IdUsuarioModificacion { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public Int32? Estado { get; set; }
        public byte[] archivo { get; set; }
        public IEnumerable<object> formatos { get; set; }
        
        
	}
}