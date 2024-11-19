using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
    public partial class PostulacionEntrevistaPersonalPregunta_Registro
    {
	    public Int32 Posicion { get; set; }
        public Int32 IdEvaluacion { get; set; }
        public Int32 IdPregunta { get; set; }
        public Int32 IdPreguntaMaestra { get; set; }
        public Int32 IdConvocatoria { get; set; }
        public Int32 IdTrabajador { get; set; }
        public Int32 TipoDocumento { get; set; }
        public String NroDocumento { get; set; }
        public String NombreCompleto { get { return String.Format("{0} {1}, {2}", Paterno, Materno, Nombre); } }
        public String Contrasena { get; set; }
        public String Sexo { get; set; }
        public String Nombre { get; set; }
        public String Paterno { get; set; }
        public String Materno { get; set; }
        public String Nacionalidad { get; set; }
        public Int32 IdEstadoCivil { get; set; }
        public String FechaNacimiento { get; set; }
        public String LugarNacimiento { get; set; }
        public Int32 Estado { get; set; }
        public String EstadoNombre { get { return ((Estado == 0) ? "INACTIVO" : ((Estado == 1) ? "ACTIVO" : ((Estado == 2) ? "NO REGISTRADO" : ""))); } }
        public String Descripcion { get; set; }

        public Int32? IdUsuarioRegistro { get; set; }
        public Int32? IdUsuarioModificacion { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public String FechaModificacion { get; set; }
        public Int32 IdEstadoRegistro { get; set; }
        public Grilla_Response Grilla { get; set; }

        public byte[] fileActa { get; set; }


    }
}