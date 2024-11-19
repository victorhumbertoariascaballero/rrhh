using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
    public partial class PostulanteNotificacion_Registro
	{
	    public Int32 IdNotificacion { get; set; }
        public Int32 IdPostulacion { get; set; }
        public Int32 IdPostulante { get; set; }
        public Int32 IdConvocatoria { get; set; }
        public String Descripcion { get; set; }
        public Int32 IdEstado { get; set; }
        public Int32 IdOrigen { get; set; }
        public String IdUsuarioRegistro { get; set; }
        public String FechaRegistro { get; set; }
        
        public Grilla_Response Grilla { get; set; }
	}
}