using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class EmpleadoEncargatura_Registro
    {
        public Int32 IdMaestro { get; set; }
        public Int32 IdMaestroDetalle { get; set; }
        public Int32 IdEmpleadoEncargatura { get; set; }
        public Int32 IdEmpleado { get; set; }
        public Int32 IdDependenciaEncargatura { get; set; }        
        public Int32 IdEstado { get; set; }
        public Int32 IdDDJJ { get; set; }
        public Int32? IdUsuarioRegistro { get; set; }
        public Int32? IdUsuarioModificacion { get; set; }
        public String DocEncargatura { get; set; }
        public String DocEncargaturaFin { get; set; }
        public DateTime? FechaIni { get; set; }
        public DateTime? FechaFin { get; set; }
        public Dependencia_Registro Dependencia { get; set; }
        public Estado_Response Estado { get; set; }
        public Estado_Response DDJJ { get; set; }
        public Grilla_Response Grilla { get; set; }
    }
}
