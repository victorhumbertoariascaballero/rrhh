using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
	public partial class EnumeradoDetalle_Request
	{
	    public Int32? IdEnumeradoDetalle { get; set; }
		public Int32? IdEnumeradoCabecera { get; set; }
        public Int32? SoloLocalesAtencion { get; set; }
        public Int32? SoloPorIdServicioSocial { get; set; }
        public String ValorDetalle { get; set; }
		public String TextoDetalle { get; set; }
		public String AbreviaturaDetalle { get; set; }
		public String OrdenDetalle { get; set; }
		public Boolean? EsValorPorDefecto { get; set; }
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

        public EnumeradoCabecera_Request EnumeradoCabecera { get; set; }

        public Grilla_Request Grilla { get; set; }
	}
}