using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public class PlanillaCalculada_Request
    {
        public int iCodTrabajador { get; set; }
        public int iCodPlanilla { get; set; }
        public int iCodTipoPlanilla { get; set; }
        public int iCodDetPlanilla { get; set; }
        public int iCodConcepto { get; set; }
        public string sNombreConcepto { get; set; }
        public int iCodTipoConcepto { get; set; }
        public string sNombreTipoConcepto { get; set; }
        public int iCodSubTipoConcepto { get; set; }
        public string sNombreSubTipoConcepto { get; set; }
        public int iCodTipoDocumento { get; set; }
        public decimal dcMonto { get; set; }
        public string sNombreTipoDocumento { get; set; }
        public string sNroDocumento { get; set; }
        public string sNombres { get; set; }
        public string sApepPaterno { get; set; }
        public string sApepMaterno { get; set; }
        public string sNombreCompleto { get; set; }
        public string sCargo { get; set; }
        public int iCodPensionario { get; set; }
        public string sNombreRegimenPensionario { get; set; }
        public int iCodTipoPensionario { get; set; }
        public string sNombreTipoRegimenPensionario { get; set; }
        public string sRegimenPensionario { get; set; }
        public bool bExoneracionRenta4ta { get; set; }
        public string sFechaInicio { get; set; }
        public string sFechaFin { get; set; }
        public int iDiasLaborados { get; set; }
        public int iDiasVacaciones { get; set; }
        public int iDiasLicencias { get; set; }
        public int iDiasDescansos_Subsidios { get; set; }
        public decimal dcMontoRemuneracionBasica { get; set; }
        public decimal dcMontoTotalIngresos { get; set; }
        public decimal dcMontoTotalDescuentos { get; set; }
        public decimal dcMontoTotalNeto { get; set; }
        public decimal dcMontoAporteEsSalud { get; set; }
        public decimal dcMontoAporteEsSalud_675 { get; set; }
        public decimal dcMontoAporteEsSaludEPS { get; set; }
        public int iMes { get; set; }
        public int iAnio { get; set; }
        public PlanillaCalculadaIngresos_Request objPlanillaCalculadaIngresos_Request { get; set; }
        public PlanillaCalculadaDescuentos_Request objPlanillaCalculadaDescuentos_Request { get; set; }
        public Grilla_Response Grilla { get; set; }
        public string sNombreFuenteFinanciamiento { get; set; }
        public string sNombreMeta { get; set; }
        public string sBanco { get; set; }
        public string sCuenta { get; set; }
        public string sCCI { get; set; }
    }

    public class PlanillaCalculadaIngresos_Request
    {
        public decimal dcMontoContraprestacion { get; set; }
        public decimal dcMontoContraprescionVacaional { get; set; }
        public decimal dcMontoReintegros { get; set; }
        public decimal dcMontoCopagoSubsidio { get; set; }
        public decimal dcMontoAguinaldos { get; set; }
        public decimal dcMontoPagoContratoAnterior { get; set; }
        public decimal dcMontoCTS { get; set; }
        public decimal dcMontoReintegroCoPagoSubsidio { get; set; }
        public decimal dcMontoTotalIngresos { get; set; }
        public Grilla_Response Grilla { get; set; }
    }

    public class PlanillaCalculadaDescuentos_Request
    {
        public decimal dcMontoTardanzas { get; set; }
        public decimal dcMontoInasistencias { get; set; }
        public decimal dcMontoPermisos { get; set; }
        public decimal dcMontoLicenciaSinGoce { get; set; }
        public decimal dcMontoSalidasAnticipadas { get; set; }
        public decimal dcMontoImpuestoRenta4ta { get; set; }
        public decimal dcMontoImpuestoRenta4taAguinaldo { get; set; }
        public decimal dcMontoImpuestoRenta4taDsctoJudicialAguinaldo { get; set; }
        public decimal dcMontoImpuestoRenta5ta { get; set; }
        public decimal dcMontoONP { get; set; }
        public decimal dcMontoAporteOblAFP { get; set; }
        public decimal dcMontoComisionAFP { get; set; }
        public decimal dcMontoPrimaSegAFP { get; set; }
        public decimal dcMontoDescuentoJudicial { get; set; }
        public decimal dcMontoEsSaludMasVida { get; set; }
        public decimal dcMontoRimacSeguro { get; set; }
        public decimal dcMontoDsctoPagoExceso { get; set; }
        public decimal dcMontoEPS { get; set; }
        public decimal dcMontoCampOftalmologica { get; set; }
        public decimal dcMontoCapacitaciones { get; set; }
        public decimal dcMontoTotalDescuento { get; set; }
        public Grilla_Response Grilla { get; set; }
    }
}

