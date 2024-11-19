using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class TurnoDiaSemana_Request
    {
        public int iCodTurnoDiaSemana { get; set; }
        public int iCodTurno { get; set; }
        public int iCodDiaSemana { get; set; }
        public Grilla_Request Grilla { get; set; }
    }
}
