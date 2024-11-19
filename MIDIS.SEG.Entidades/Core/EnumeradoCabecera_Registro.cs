using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
	public partial class EnumeradoCabecera_Registro
	{
    	    public Int32 IdEnumeradoCabecera { get; set; }
		public String Nombre { get; set; }
		public String Descripcion { get; set; }
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