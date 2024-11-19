using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
	public partial class Postulacion_Registro
	{
	    public Int32 IdPostulacion { get; set; }
        public Int32 IdPostulante { get; set; }
        public Int32 IdConvocatoria { get; set; }
        public Int32 Estado { get; set; }
        public String EstadoNombre { get; set; }
        public String FechaPostulacion { get; set; }

        public Int32 ExisteEstudio { get; set; }
        public Int32 ExisteCapacitacion { get; set; }
        public Int32 ExisteExperiencia { get; set; }
        public Int32 ExisteAnexo { get; set; }
        
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public Int32 IdDetalle { get; set; }
        public Int32 IdTipo { get; set; }
        public Int32 IdTipoConvocatoria { get; set; }
        public byte[] FileEstudio { get; set; }
        public byte[] Filecapacitacion { get; set; }
        public byte[] FileExperiencia { get; set; }
        public byte[] FileDocumento { get; set; }
        public byte[] FileAnexo06 { get; set; }
        public byte[] FileHojaVida { get; set; }
        public byte[] FileCarta { get; set; }
        public byte[] FileAnexoPracticas { get; set; }
        public IEnumerable<object> formatos { get; set; }

        // para el tema de practicas 
        public String TipoDocumento { get; set; }
        public String NroDocumento { get; set; }
        public String TelefonoCelular { get; set; }
        public String CorreoElectronico { get; set; }

        public Grilla_Response Grilla { get; set; }
	}
}