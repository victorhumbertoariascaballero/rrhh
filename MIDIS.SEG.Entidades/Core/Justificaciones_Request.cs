using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class Justificaciones_Request
    {
        public int iCodJustificaciones { get; set; }
        public int iCodTrabajador { get; set; }
        public int iCodigoDependencia { get; set; }
        public int iCodTipoJustificacion { get; set; }
        public int iCodTipGoce { get; set; }
        public int iCodEstadoProceso { get; set; }
        public Grilla_Request Grilla { get; set; }
    }
}
