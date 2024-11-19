using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class PerfilPuestoRegistro
    {
        public int iCodPerfil { get; set; }
        public int iCodPerfilAnexo { get; set; }
        public string strMision { get; set; }
        public int iTipoReq { get; set; }
        public int iAnioExpGeneral { get; set; }
        public int iAnioExpEspecifica { get; set; }
        public int iAnioExpSectorPublico { get; set; }
        public int iCodNivelMinimo { get; set; }
        public bool bEstado { get; set; }
        public int iCodTrabajador { get; set; }
        public int iCodOrgano { get; set; }
        public int iCodUnidadOrganica { get; set; }
        public int iConocimiento { get; set; }
        public int iPsicologico { get; set; }
        public int iPosiciones { get; set; }
        public int iTipoServicio { get; set; }
        public string strJustificacion { get; set; }
        public string strTrabajadorCese { get; set; }
        public string strPeriodo { get; set; }
        public string strRemuneracion { get; set; }
        public string strPuestoEstructural { get; set; }
        public string strNombrePuesto { get; set; }
        public string strMeta { get; set; }
        public string strDependenciaJerarquicaLineal { get; set; }
        public string strDependenciaFuncional { get; set; }
        public string strPuestos_a_su_Cargo { get; set; }
        public DateTime? datFechaCese { get; set; }
        public DateTime datFechaReg { get; set; }
        public DateTime datFechaMod { get; set; }
        public bool bEstadoCompletado { get; set; }

        public string strObservacion { get; set; }
        public string strEstadoDerivado { get; set; }
        public string strDesExpEspecifica { get; set; }
        public string strCondiciones { get; set; }
        public string strPeriodicidad { get; set; }
        public int iPeriodicidad { get; set; }
        public string strOrgano { get; set; }
        public string strUnidadOrganica { get; set; }
        public string strEstadoCompletado { get; set; }
        public int iTipoPerfil { get; set; }
        public Int32? IdUsuarioRegistro { get; set; }
        public Int32? IdUsuarioModificacion { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }


        public string strUnidadOrganica_Envia { get; set; }
        public string strUnidadOrganica_Recibe { get; set; }
        public string strEstadoAprobacion { get; set; }
        public string strFechaReg { get; set; }

        public Int32 IdTieneArchivo { get; set; }
        public Int32 IdUsuarioAsignado { get; set; }
        public byte[] archivo { get; set; }
        public IEnumerable<object> formatos { get; set; }
        public Grilla_Response Grilla { get; set; }
    }
}
