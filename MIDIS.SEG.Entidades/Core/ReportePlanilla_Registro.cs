using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades.Core
{
    public class ReportePlanilla_Registro
    {      

        public string vNombreTrabajador { get; set; }
        public string vNumeroDocumento { get; set; }
        public string vDependencia { get; set; }
        public string vRegimen { get; set; }
        public string vHorario { get; set; }
        //TOTAL DE MINUTOS DE TARDANZA
        public string vMinTardanza { get; set; }
        //TOTAL DE MINUTOS SALIDA ANTES DEL FIN DE LA JORNADA	
        public string vMinSalidaAntes { get; set; }
        //TOTAL DE MINUTOS DE PERMISO PERSONAL
        public string vMinPermisoPer { get; set; }
        //TOTAL DE MINUTOS POR EL PERIODO	
        public string vMinPeriodos { get; set; }
        //TOTAL DE FALTAS
        public string vNroFaltas { get; set; }
        //TOTAL DE LICENCIA SIN GOCE	
        public string vLicSinGoce { get; set; }
        //TOTAL DE DIAS POR EL PERIODO
        public string vDiasPorPeriodo { get; set; }
        public Grilla_Response Grilla { get; set; }
    }
}
