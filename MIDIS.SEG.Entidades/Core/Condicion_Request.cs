using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
	public partial class Condicion_Request
	{
	    public Int32? IdCondicion { get; set; }
		public String Nombre { get; set; }
		public Int32? RegistroUsuarioCreacion { get; set; }
	    public Int32? RegistroUsuarioModificacion { get; set; }
	    public DateTime? RegistroFechaCreacionEntreDesde { get; set; }
        public DateTime? RegistroFechaCreacionEntreHasta { get; set; }
        public DateTime? RegistroFechaModificacionEntreDesde { get; set; }
        public DateTime? RegistroFechaModificacionEntreHasta { get; set; }
	    public String RegistroIpCreacion { get; set; }
	    public String RegistroIpModificacion { get; set; }
	    public Boolean? RegistroEstaActivo { get; set; }
	    public Boolean? RegistroEstaEliminado { get; set; }


        public Grilla_Request Grilla { get; set; }
	}
}