using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
	public partial class EnumeradoCabecera_Response
	{
	    public Int32 IdEnumeradoCabecera { get; set; }
		public String Nombre { get; set; }
		public String Descripcion { get; set; }
	    public Boolean RegistroEstaActivo { get; set; }


        public Grilla_Response Grilla { get; set; }
	}
}