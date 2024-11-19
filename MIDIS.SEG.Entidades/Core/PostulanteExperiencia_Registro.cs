using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
    public partial class PostulanteExperiencia_Registro
	{
	    public Int32 IdPostulante { get; set; }
        public Int32 IdLaboral { get; set; }
        public String Empresa { get; set; }
        public String Cargo { get; set; }
        public String Descripcion { get; set; }
        public String FechaInicio { get; set; }
        public String FechaFin { get; set; }
        public Int32 Estado { get; set; }
        
        public Int32? IdUsuarioRegistro { get; set; }
        public Int32? IdUsuarioModificacion { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public IEnumerable<object> formatos { get; set; }
        public Int32 IdTieneArchivo { get; set; }
        public byte[] FileArchivo { get; set; }
        public Int32 Sector { get; set; }
        public Int32 Regimen { get; set; }
        public String NombreJefeDirecto { get; set; }
        public String PuestoJefeDirecto { get; set; }
        public String MotivoCambio { get; set; }
        public Decimal Remuneracion { get; set; }
        public String ReferenciaNombre { get; set; }
        public String ReferenciaPuesto { get; set;}
        public String ReferenciaTelefono { get; set; }
        public String ReferenciaCorreo { get; set; }
        public Grilla_Response Grilla { get; set; }
	}
}