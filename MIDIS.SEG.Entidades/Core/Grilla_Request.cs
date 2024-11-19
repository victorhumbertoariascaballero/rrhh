using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public class Grilla_Request
    {
        public int RegistrosPorPagina { get; set; }
        public int PaginaActual { get; set; }
        public string OrdenarPor { get; set; }
        public string OrdenarDeForma { get; set; }
    }
}
