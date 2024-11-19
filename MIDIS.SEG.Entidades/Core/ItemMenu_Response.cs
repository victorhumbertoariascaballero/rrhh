using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
	public partial class ItemMenu_Response
	{
	    public Int32 IdItemMenu { get; set; }
		public String Codigo { get; set; }
		public String Nombre { get; set; }
		public String Descripcion { get; set; }
		public Int32? Nivel { get; set; }
		public Int32? IdItemMenuPadre { get; set; }
		public String Url { get; set; }
		public String Icono { get; set; }
		public Int32 OrdenMenu { get; set; }
		public String RutaMenu { get; set; }
		public Boolean EsVisible { get; set; }
	    public Boolean RegistroEstaActivo { get; set; }


        public Grilla_Response Grilla { get; set; }
	}
}