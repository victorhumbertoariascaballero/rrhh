using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class TipoRetencion_Response
    {
        public String CodTipoRetencion { get; set; }
        public String NomTipoRetencion { get; set; }

		public Grilla_Response Grilla { get; set; }

        public TipoRetencion_Response(String codigo, String nombre) {
            CodTipoRetencion = codigo;
            NomTipoRetencion = nombre;
        }
    }
}
