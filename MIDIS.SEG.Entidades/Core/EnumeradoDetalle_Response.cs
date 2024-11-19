using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
	public partial class EnumeradoDetalle_Response
	{
	    public Int32 IdEnumeradoDetalle { get; set; }
		public Int32 IdEnumeradoCabecera { get; set; }
		public String ValorDetalle { get; set; }
		public String TextoDetalle { get; set; }
		public String AbreviaturaDetalle { get; set; }
		public String OrdenDetalle { get; set; }
		public Boolean? EsValorPorDefecto { get; set; }
	    public Boolean RegistroEstaActivo { get; set; }

        public EnumeradoCabecera_Response EnumeradoCabecera { get; set; }

        public Grilla_Response Grilla { get; set; }
	}
}