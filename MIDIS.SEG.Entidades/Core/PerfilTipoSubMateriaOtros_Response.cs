using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class PerfilTipoSubMateriaOtros_Response
    {
        public int iCodTipoSubMateriaOtros { get; set; }
        public string strDescripcion { get; set; }

        public int iCodTipoMateriaOtros { get; set; }
        public bool bEstado { get; set; }
    }
}
