using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades.Core
{
    public class CompensacionesTrabajador_Request
    {
        public int? iCodigoDependencia { get; set; }
        public int? iCodTrabajador { get; set; }
        public DateTime dFecha { get; set; }
        public string fecha { get; set; }
        public int? iCodEstadoProceso { get; set; }

        public int? iMes { get; set; }
        public int? iAnio { get; set; }
        //
        public List<DateTime> dFechas { get; set; }
        public Grilla_Request Grilla { get; set; }
    }
}
