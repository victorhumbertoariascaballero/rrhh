using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class PerfilPuesto_Request
    {
        public int iCodPerfil { get; set; }

        public int iCodTrabajador { get; set; }

        public int iCodOrgano { get; set; }
        public string strOrgano { get; set; }

        public int iCodUnidadOrganica { get; set; }
        public string strUnidadOrganica { get; set; }
        public string strPuestoEstructural { get; set; }
        public string strNombrePuesto { get; set; }
        public string strDependenciaJerarquicaLineal { get; set; }
        public string strDependenciaFuncional { get; set; }
        public string strPuestos_a_su_Cargo { get; set; }
        public string strMision { get; set; }
        public int iAnioExpGeneral { get; set; }
        public int iAnioExpEspecifica { get; set; }
        public string strDesExpEspecifica { get; set; }
        public int iAnioExpSectorPublico { get; set; }
        public int iCodNivelMinimo { get; set; }

        public DateTime datFechaReg { get; set; }
        public bool bEstadoCompletado { get; set; }
        public string strEstadoCompletado { get; set; }

        public Grilla_Response Grilla { get; set; }

        public string strEstadoDerivado { get; set; }

        public string strEstadoMovimiento { get; set; }


    }
}
