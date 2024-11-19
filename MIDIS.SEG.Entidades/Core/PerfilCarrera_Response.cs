using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class PerfilCarrera_Response
    {
        public string strCodCarrera { get; set; }
        public string strDescripcion { get; set; }
        public int iCodTipoCarrera { get; set; }
        public bool bEstado { get; set; }
    }
}
