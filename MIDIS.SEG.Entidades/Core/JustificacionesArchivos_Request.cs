using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class JustificacionesArchivos_Request
    {
        public int iCodJustificacionesArchivos { get; set; }
        public int iCodJustificaciones { get; set; }
        public Grilla_Request Grilla { get; set; }
    }
}
