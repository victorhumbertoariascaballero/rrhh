using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
	public partial class Mes_Response
	{
	    public String Codigo { get; set; }
        public String Nombre { get; set; }
		public Grilla_Response Grilla { get; set; }

        public Mes_Response(String codigo, String nombre) {
            Codigo = codigo;
            Nombre = nombre;
        }
	}
}