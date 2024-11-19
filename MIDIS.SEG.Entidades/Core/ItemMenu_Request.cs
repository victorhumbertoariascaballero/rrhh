using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
	public partial class ItemMenu_Request
	{
	    public Int32? IdItemMenu { get; set; }
		public String Codigo { get; set; }
		public String Nombre { get; set; }
		public String Descripcion { get; set; }
		public Int32? Nivel { get; set; }
		public Int32? IdItemMenuPadre { get; set; }
		public String Url { get; set; }
		public String Icono { get; set; }
		public Int32? OrdenMenu { get; set; }
		public String RutaMenu { get; set; }
		public Boolean? EsVisible { get; set; }
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