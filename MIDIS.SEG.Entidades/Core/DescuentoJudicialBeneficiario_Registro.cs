using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class DescuentoJudicialBeneficiario_Registro
    {
        public int iCodJudicialDetalle { get; set; }
        public int iCodJudicial { get; set; }
        public int iCodTrabajador { get; set; }
        public string vDniBeneficiario { get; set; }
        public string vNombreBeneficiario { get; set; }
        public int iCodigoBanco { get; set; }
        public string vNumeroCuenta { get; set; }
        public int iCodTipoRetencion { get; set; }
        public decimal dValorPorcentaje { get; set; }
        public decimal dMontoRetencion { get; set; }
        public string vObservacion { get; set; }
        public int iCodFormaPago { get; set; }
        public string sFechaLlegadaDoc { get; set; }

        /* Atributos Secundarios */
        public string vTrabajador { get; set; }
        public string vCodigoPlanilla { get; set; }
        public string vNombreBanco { get; set; }
        public string vNombreRetencion { get; set; }
        public string vNombreFormaPago { get; set; }
        //
        public Grilla_Response Grilla { get; set; }

    }
}
