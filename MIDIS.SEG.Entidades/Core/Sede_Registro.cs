using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
	public partial class Sede_Registro
	{
        public Int32 IdSede { get; set; }
        public String Nombre { get; set; }
        
        public Grilla_Response Grilla { get; set; }
	}
}