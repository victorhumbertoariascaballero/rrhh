using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
	public partial class Persona_Request
	{
	    public Int32? IdPersona { get; set; }
		public Boolean? TieneDocumento { get; set; }
		public String TipoDeDocumento { get; set; }
		public String NumeroDeDocumento { get; set; }
		public Boolean? DniValidoEnReniec { get; set; }
		public String Nombres { get; set; }
		public String ApellidoPaterno { get; set; }
		public String ApellidoMaterno { get; set; }
		public String Sexo { get; set; }
		public DateTime? FechaDeNacimientoEntreDesde { get; set; }
        public DateTime? FechaDeNacimientoEntreHasta { get; set; }
		public Int32? IdUbigeo { get; set; }
		public Int32? IdCentroPoblado { get; set; }
		public Int32? IdUbigeoDomicilio { get; set; }
		public String DireccionDomicilio { get; set; }
		public Boolean? DomicilioEstaConfirmado { get; set; }
		public Int32? IdCentroPobladoDomicilio { get; set; }
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

        public Ubigeo_Request Ubigeo { get; set; }
        
        public Grilla_Request Grilla { get; set; }
	}
}