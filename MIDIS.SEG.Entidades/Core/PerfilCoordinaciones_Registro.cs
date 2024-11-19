using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class PerfilCoordinaciones_Registro
    {
        public int iCodPerfil { get; set; }
        public int iSecuencia { get; set; }       
        public string Coordinacion { get; set; }
        public int iTipoCoordinacion { get; set; }
    }
}
