using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
	public partial class Ubigeo_Registro
	{
    	    public Int32 IdUbigeo { get; set; }
		public String CodigoInei { get; set; }
		public String CodigoReniec { get; set; }
		public String Departamento { get; set; }
		public String Provincia { get; set; }
		public String Distrito { get; set; }
	    public Int32 RegistroUsuarioCreacion { get; set; }
	    public Int32? RegistroUsuarioModificacion { get; set; }
	    public DateTime RegistroFechaCreacion { get; set; }
        public DateTime? RegistroFechaModificacion { get; set; }
	    public String RegistroIpCreacion { get; set; }
	    public String RegistroIpModificacion { get; set; }
	    public Boolean RegistroEstaActivo { get; set; }
	    public Boolean RegistroEstaEliminado { get; set; }
	}
}