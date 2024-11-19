using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class PlanillaProyeccionAnualRta5ta_Registro
    {
        public int iCodTrabajador { get; set; }
        public int iMes { get; set; }
        public int iAnio { get; set; }
        public decimal dcMontoTotalIng4ta { get; set; }
        public decimal dcRemuneracion { get; set; }
        public decimal dcAguinaldo { get; set; }
        public decimal dcReintegro { get; set; }
        public decimal dcTotal { get; set; }
        public decimal dcRetencion { get; set; }
        public decimal dcMontoTotalImpRenta4ta { get; set; }
        public decimal dcMontoTotalVacImpRenta4ta { get; set; }
        public decimal dcMontoTotalOtrosIng5ta { get; set; }
        public decimal dcMontoTotalAguinaldoTrunco { get; set; }
        public decimal dcMontoTotalCompensacionVacacional { get; set; }
        public decimal dcMontoTotalOtrosRenta5ta { get; set; }

        public Grilla_Response Grilla { get; set; }
    }
}
