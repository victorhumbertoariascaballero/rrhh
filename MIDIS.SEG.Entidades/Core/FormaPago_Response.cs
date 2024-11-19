using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class FormaPago_Response
    {
        public String CodFormaPago { get; set; }
        public String NomFormaPago { get; set; }

		public Grilla_Response Grilla { get; set; }

        public FormaPago_Response(String codigo, String nombre)
        {
            CodFormaPago = codigo;
            NomFormaPago = nombre;
        }
    }
}
