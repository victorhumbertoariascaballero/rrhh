using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
    public partial class EmpleadoCuenta_Registro
	{
	    public Int32 IdEmpleadoBanco { get; set; }
        public Int32 IdEmpleado { get; set; }
        public Int32 IdBanco { get; set; }
        public String CCI { get; set; }
        public Int32 IdEstado { get; set; }
        public String NombreBanco { get; set; }
        public String NroCuenta { get; set; }
        public Int32? IdUsuarioRegistro { get; set; }
        public Int32? IdUsuarioModificacion { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public Banco_Registro Banco { get; set; }
        public Estado_Response Estado { get; set; }
        
        public Grilla_Response Grilla { get; set; }
	}
}