using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
    public partial class TrabajadorFirma_Registro
	{
	    public String NroDocumento { get; set; }
		public String Nombre { get; set; }
        public String Correo { get; set; }
        public IEnumerable<object> formatos { get; set; }
        public byte[] formato1 { get; set; }

	}
}