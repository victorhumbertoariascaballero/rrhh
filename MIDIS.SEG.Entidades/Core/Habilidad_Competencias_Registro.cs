using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class Habilidad_Competencias_Registro
    {
        public int iCodPerfil { get; set; }
        public int iCodTipoCualidad { get; set; }
        public int iCodCualidad { get; set; }
        public int iCodCualidadAnt { get; set; }
        public Tipo_Cualidad_Response TipoCualidad { get; set; }
        public Cualidad_Response Cualidad { get; set; }

    }
}
