using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
	public partial class Ubigeo_Response
	{
	    public Int32 IdUbigeo { get; set; }
		public String CodigoInei { get; set; }
		public String CodigoReniec { get; set; }
		public String Departamento { get; set; }
		public String Provincia { get; set; }
		public String Distrito { get; set; }
        public String Ubigeo
        {
            get
            {
                return String.Format("{0} / {1} / {2}", Departamento, Provincia, Distrito);
            }
        }
	    public Boolean RegistroEstaActivo { get; set; }


        public Grilla_Response Grilla { get; set; }
	}
}