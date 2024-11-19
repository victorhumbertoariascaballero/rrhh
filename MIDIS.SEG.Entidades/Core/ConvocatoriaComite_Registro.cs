using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
	public partial class ConvocatoriaComite_Registro
	{
	    public Int32 IdConvocatoria { get; set; }
        public Int32 IdPostulacion { get; set; }
        public Int32 IdPostulante { get; set; }
        public Int32 IdConvocatoriaComite { get; set; }
        public Int32 IdMiembro { get; set; }
        public Int32 IdDependencia { get; set; }
        public Int32 IdTrabajador { get; set; }
        public Int32 IdTitular { get; set; }
        public Int32 Estado { get; set; }
        public String NombreMiembro { get; set; }
        public String NombreDependencia { get; set; }
        public String DescripcionMiembro { 
            get {
                return String.Format("{0} - {1}", (IdTitular == 1 ? "[TITULAR]" : "[SUPLENTE]"), NombreMiembro);
            } 
        }
        public String FechaEntrevista { get; set; }
        public String HoraEntrevista { get; set; }

        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public Int32? IdUsuarioRegistro { get; set; }
        public Int32? IdUsuarioModificacion { get; set; }
        //public Grilla_Response Grilla { get; set; }
	}
}