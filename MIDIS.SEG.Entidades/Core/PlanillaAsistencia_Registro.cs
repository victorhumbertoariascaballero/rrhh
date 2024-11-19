using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
	public partial class PlanillaAsistencia_Registro
	{
	    public Int32 IdPlanilla { get; set; }
        public Int32 IdTipoPlanilla { get; set; }
        public Int32 iCodDetPlanilla { get; set; }
        public Int32 IdAnio { get; set; }
        public Int32 IdMes { get; set; }
        public Int32 IdTrabajador { get; set; }
        public String NroDocumento { get; set; }
        public String Nombre { get; set; }
        public String NombrePlanilla { get; set; }
        public String NombreTipoPlanilla { get; set; }
        public Int32 Faltas { get; set; }
        public Int32 Licencia { get; set; }
        public Int32 Tardanza { get; set; }
        public Int32 Permisos { get; set; }
        public Int32 Vacaciones { get; set; }
        public Decimal ImporteVacaciones { get; set; }
        public Decimal ImporteLicencias { get; set; }
        public Decimal ImportePermisos { get; set; }
        public Decimal ImporteFaltas { get; set; }

        public String IdRegistroPlanilla { 
            get {
                return IdPlanilla + "|" + IdTipoPlanilla + "|" + IdAnio + "|" + IdMes + "|" + iCodDetPlanilla + "|" + Convert.ToInt16(bEstadoRegAsistencia) + "|" + Convert.ToInt16(bEstadoDsctoFijoVariable);
                } 
        }
        public String IdNombrePlanilla
        {
            get
            {
                return NombrePlanilla + " - " + NombreTipoPlanilla + " - " + ToRoman(iCodDetPlanilla);
            }
        }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public Int32? IdUsuarioRegistro { get; set; }
        public Int32? IdUsuarioModificacion { get; set; }
        public Boolean bEstadoRegAsistencia { get; set; }
        public Boolean bEstadoDsctoFijoVariable { get; set; }

        public IEnumerable<String> formatos { get; set; }
        //public List<ConvocatoriaComite_Registro> comite { get; set; }
        
        public Grilla_Response Grilla { get; set; }

        public static string ToRoman(int number)
        {
            if ((number < 0) || (number > 3999)) throw new ArgumentOutOfRangeException("Value must be between 1 and 3999");
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900); //EDIT: i've typed 400 instead 900
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            throw new ArgumentOutOfRangeException("Value must be between 1 and 3999");
        }
	}
}