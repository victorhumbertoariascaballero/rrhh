using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class EmpleadoReporteBanco_Registro
    {
        public Int32 IdEmpleado { get; set; }
        public String NombreCompleto { get { return String.Format("{0} {1}, {2}", Paterno, Materno, Nombre); } }
        public String Nombre { get; set; }
        public String Paterno { get; set; }
        public String Materno { get; set; }
        public String TipoDocumento { get; set; }
        public String NroDocumento { get; set; }
        public String Banco { get; set; }
        public String NroCuenta { get; set; }
        public String NroCCI { get; set; }
        public decimal ImporteAbonar { get; set; }

        public Grilla_Response Grilla { get; set; }
    }
}
