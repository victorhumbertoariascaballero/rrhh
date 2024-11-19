using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class PerfilFunciones_Request
    {
        public int iCodPerfil { get; set; }
        public int iSecuencia { get; set; }
        public int iCodVerbo { get; set; }
        public string Objetivo { get; set; }
        public string Funcion { get; set; }

        public Verbo_Registro Verbo { get; set; }
    }
}
