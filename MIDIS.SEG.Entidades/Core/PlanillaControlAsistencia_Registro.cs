using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
	public partial class PlanillaControlAsistencia_Registro
	{
	    public Int32 IdPlanilla { get; set; }
        public Int32 IdTipoPlanilla { get; set; }
        public Int32 IdDetPlanilla { get; set; }
        public Int32 IdAnio { get; set; }
        public Int32 IdMes { get; set; }
        public Int32 IdTrabajador { get; set; }
        //public String NroDocumento { get; set; }
        public String NombreTrabajador { get; set; }
        public String NombreDependencia { get; set; }
        public String NombrePlanilla { get; set; }
        public String NombreTipoPlanilla { get; set; }
        public String NombreCondicion { get; set; }
        public Int32 DiasLaborados { get; set; }
        public Int32 Faltas { get; set; }
        public Int32 Salidas { get; set; }
        public Int32 Licencia { get; set; }
        public Int32 Tardanza { get; set; }
        public Int32 Permisos { get; set; }
        public Int32 Vacaciones { get; set; }
        public Decimal ImporteVacaciones { get; set; }
        public Decimal ImporteLicencias { get; set; }
        public Decimal ImporteTardanzas { get; set; }
        public Decimal ImportePermisos { get; set; }
        public Decimal ImporteFaltas { get; set; }
        public Decimal ImporteDescuento { get; set; }
        public Decimal ImporteSalidas { get; set; }

        public String IdRegistroPlanilla { 
            get {
                return IdPlanilla + "|" + IdTipoPlanilla + "|" + IdAnio + "|" + IdMes;
                } 
        }
        public String IdNombrePlanilla
        {
            get
            {
                return NombrePlanilla + " - " + NombreTipoPlanilla;
            }
        }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public Int32? IdUsuarioRegistro { get; set; }
        public Int32? IdUsuarioModificacion { get; set; }
        
        public IEnumerable<String> formatos { get; set; }
        //public List<ConvocatoriaComite_Registro> comite { get; set; }


        public Int32 Sancion_Disciplinaria { get; set; }
        public Int32 Enfermedad_o_Accidente { get; set; }
        public Int32 Licencia_con_Goce { get; set; }
        public Int32 Licencia_por_Paternidad { get; set; }
        public Int32 Licencia_por_Fallecimiento_de_Padres { get; set; }
        public Int32 Licencia_por_Enfermedad_Grave_o_Terminal_o_Accidente_Grave_de_Familiares_Directos { get; set; }
        
        public Grilla_Response Grilla { get; set; }
	}
}