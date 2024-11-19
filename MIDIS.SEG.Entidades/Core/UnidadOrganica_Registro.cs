using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class UnidadOrganica_Registro
    {
        public int iCodTrabajador { get; set; }
        public int iCodigoDependencia { get; set; }
        public int iCodOrgano { get; set; }
        public string strOrgano { get; set; }
        public int iCodDependencia { get; set; }
        public string strUnidad_Organica { get; set; }
        public string strDependencia_Jerarquica_Lineal { get; set; }
        public string strDependencia_Funcional { get; set; }
    }
}
