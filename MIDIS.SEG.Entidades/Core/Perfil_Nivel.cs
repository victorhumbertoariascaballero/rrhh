using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public class Perfil_Nivel
    {
        public Int32 IdNivel { get; set; }
        public String Perfil { get; set; }
        public Grilla_Response Grilla { get; set; }
    }
}
