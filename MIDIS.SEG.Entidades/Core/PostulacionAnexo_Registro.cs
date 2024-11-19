using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
    public partial class PostulacionAnexo_Registro
	{
        public Int32 IdPostulacionAnexo { get; set; }
	    public Int32 IdPostulacion { get; set; }
        public Int32 IdPostulante { get; set; }
        public Int32 IdAcepta { get; set; }
        public Int32 TipoDocumento { get; set; }
        public Int32 IdTipoConvocatoria { get; set; }
        public String NroDocumento { get; set; }
        public String Nombre { get; set; }
        public String Paterno { get; set; }
        public String Materno { get; set; }
        public String NepotismoRel1 { get; set; }
        public String NepotismoRel2 { get; set; }
        public String NepotismoRel3 { get; set; }
        public String NepotismoApe1 { get; set; }
        public String NepotismoApe2 { get; set; }
        public String NepotismoApe3 { get; set; }
        public String NepotismoNom1 { get; set; }
        public String NepotismoNom2 { get; set; }
        public String NepotismoNom3 { get; set; }
        public String NepotismoAre1 { get; set; }
        public String NepotismoAre2 { get; set; }
        public String NepotismoAre3 { get; set; }
        public String Direccion { get; set; }
        public String NroCAS { get; set; }
        public String Puesto { get; set; }
        
        public Int32? IdUsuarioRegistro { get; set; }
        public Int32? IdUsuarioModificacion { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public IEnumerable<object> formatos { get; set; }
        public Int32 IdTieneArchivo { get; set; }
        public byte[] FileArchivo { get; set; }
        //public Estado_Response Requisito { get; set; }
        public Grilla_Response Grilla { get; set; }
	}
}