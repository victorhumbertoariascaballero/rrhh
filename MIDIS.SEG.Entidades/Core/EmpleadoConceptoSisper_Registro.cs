using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
    public partial class EmpleadoConceptoSisper_Registro
	{
	    public String IdPlanilla { get; set; }
		public String TipoPlanilla { get; set; }
        public Int32 Anio { get; set; }
        public Int32 Mes { get; set; }
        public String Trabajador { get; set; }
        public String Concepto { get; set; }
        public Decimal MontoConcepto { get; set; }
        public String NombreConcepto { get; set; }
        public String Abreviatura { get; set; }
        public String TipoConcepto { get; set; }
        public String NombreTipoConcepto { get; set; }
        public String NombrePlanilla { get; set; }
        public String NombreTipoPlanilla { get; set; }
        public String AbreviaturaPlanilla { get; set; }
		public String RegimenLaboral { get; set; }
		
        public Grilla_Response Grilla { get; set; }
	}
}