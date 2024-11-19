using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class EmpleadoDesplazamiento_Registro
    {
        public Int32 IdEmpleado { get; set; }
        public Int32 IdDependencia { get; set; }        
        public Int32 IdEstado { get; set; }        
        public Int32? IdUsuarioRegistro { get; set; }
        public Int32? IdUsuarioModificacion { get; set; }
        public DateTime? FechaIni { get; set; }
        public DateTime? FechaFin { get; set; }
        public String Contrato { get; set; }
        public String Cargo { get; set; }
        public Dependencia_Registro Dependencia { get; set; }
        public Estado_Response Estado { get; set; }
        public Grilla_Response Grilla { get; set; }
    }
}
