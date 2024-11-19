using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class Marcaciones_Request
    {
        public int iCodMarcaciones { get; set; }
        public int iCodTrabajador { get; set; }
        public int iCodigoDependencia { get; set; }
        public int iCodTipoMarcacion { get; set; }
        public DateTime dtFechaMarcacionIni { get; set; }
        public DateTime dtFechaMarcacionFin { get; set; }
        public Grilla_Request Grilla { get; set; }
    }
}
