using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
	public partial class Persona_Response
	{
	    public Int32 IdPersona { get; set; }
		public Boolean? TieneDocumento { get; set; }
		public String TipoDeDocumento { get; set; }
		public String DscTipoDeDocumento { get; set; }
		public String NumeroDeDocumento { get; set; }
		public Boolean? DniValidoEnReniec { get; set; }
		public String Nombres { get; set; }
		public String ApellidoPaterno { get; set; }
		public String ApellidoMaterno { get; set; }
		public String Sexo { get; set; }
        public String TelefonoFijo { get; set; }
        public String TelefonoCelular { get; set; }
        public String CorreoElectronico { get; set; }
        public String Sisfoh { get; set; }
        public String DscSexo { get; set; }
		public DateTime? FechaDeNacimiento { get; set; }
		public Int32? IdUbigeo { get; set; }
		public Int32? IdCentroPoblado { get; set; }
		public Int32? IdUbigeoDomicilio { get; set; }
		public String DireccionDomicilio { get; set; }
		public Boolean? DomicilioEstaConfirmado { get; set; }
		public Int32? IdCentroPobladoDomicilio { get; set; }
	    public Boolean RegistroEstaActivo { get; set; }

        public Ubigeo_Response Ubigeo { get; set; }
        
        public Grilla_Response Grilla { get; set; }
	}
}