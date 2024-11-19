using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class EmpleadoFamiliar_Registro
    {
        public Int32 IdEmpleadoFamiliar { get; set; }
        public Int32 IdEmpleado { get; set; }
        public Int32 IdFamiliar { get; set; }
        public Int32 IdParentesco { get; set; }
        public String Nombre { get; set; }
        public String NombreParentesco { get; set; }
        public String NroDocumento { get; set; }
        public Int32 Edad { get; set; }
        public String Ocupacion { get; set; }
        public String FechaNacimiento { get; set; }
        public String IdSexo { get; set; }

        public Int32? IdUsuarioRegistro { get; set; }
        public Int32? IdUsuarioModificacion { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public Estado_Response Parentesco { get; set; }
        public Estado_Response Sexo { get; set; }
        public Grilla_Response Grilla { get; set; }
    }
}
