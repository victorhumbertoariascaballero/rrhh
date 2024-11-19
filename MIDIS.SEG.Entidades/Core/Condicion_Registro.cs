using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
	public partial class Condicion_Registro
	{
        public Int32 IdCondicion { get; set; }
        public String Nombre { get; set; }
        
        public Grilla_Response Grilla { get; set; }
	}
}