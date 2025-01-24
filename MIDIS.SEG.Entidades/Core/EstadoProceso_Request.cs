using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades.Core
{
    public  class EstadoProceso_Request
    {
        public int iCodEstadoProceso { get; set; }
        public string vDescripcion { get; set; }
        public bool bEstado { get; set; }
        public Grilla_Request Grilla { get; set; }
    }
}
