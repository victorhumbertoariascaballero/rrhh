using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades.Core
{
    public class JustificacionesTrabajador_Request
    {
        public int? iCodigoDependencia { get; set; }
        public int? iCodTrabajador { get; set; }
        public string fecha { get; set; }
        public int? iCodTipoJustificacion { get; set; }
        public int? iCodEstadoProceso { get; set; }
        //
        public Grilla_Request Grilla { get; set; }
    }
}
