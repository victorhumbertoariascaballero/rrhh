using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class TipoJustificacion_Request
    {
        public int iCodTipoJustificacion { get; set; }
        public string vDescripcion { get; set; }
        public bool bEstado { get; set; }
        public Grilla_Request Grilla { get; set; }
    }
}
