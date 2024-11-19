using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
    public partial class PostulanteInformacion_Registro
	{
	    public Int32 IdPostulante { get; set; }
        public Int32 IdPostulacion { get; set; }
        public Int32 IdConvocatoria { get; set; }
        public Int32 TipoDocumento { get; set; }
        public String NroDocumento { get; set; }
        public String IdRegistro { get { return String.Format("{0}|{1}|{2}", IdPostulante, IdPostulacion, IdConvocatoria); } }
        public String NombreCompleto { get { return String.Format("{0} {1}, {2}", Paterno, Materno, Nombre); } }
        public String NombreCompletoProceso { get { return (String.IsNullOrEmpty(NombreProceso) ? String.Format("{0} {1}, {2}", Paterno, Materno, Nombre) : String.Format("{0} {1}, {2} - {3}", Paterno, Materno, Nombre, NombreProceso)); } }
        public String Contrasena { get; set; }
        public String Sexo { get; set; }
        public String Nombre { get; set; }
        public String Paterno { get; set; }
        public String Materno { get; set; }
        public String Nacionalidad { get; set; }
        public Int32 IdEstadoCivil { get; set; }
        public String FechaNacimiento { get; set; }
        public String LugarNacimiento { get; set; }
        public Int32 Edad { get; set; }
        public Int32 IdTipoVivienda { get; set; }
        public Int32 IdTipoBrevete { get; set; }
        public String NroLicencia { get; set; }
        public String Ubigeo { get; set; }
        public String DescripcionUbigeo { get; set; }
        public String Domicilio { get; set; }
        public String Telefono { get; set; }
        public String Celular { get; set; }
        public String CorreoElectronico { get; set; }
        public String RUC { get; set; }
        public String TelefonoEmergencia1 { get; set; }
        public String ContactoEmergencia1 { get; set; }
        public String TelefonoEmergencia2 { get; set; }
        public String ContactoEmergencia2 { get; set; }
        public Int32 IdEstaAfiliadoPensiones { get; set; }
        public Int32 IdAFPAfiliada { get; set; }
        public String NombreAFPAfiliada { 
            get {
                String strNombre = String.Empty;
                if (IdTipoAFP == 1) strNombre = "PROFUTURO";
                if (IdTipoAFP == 2) strNombre = "INTEGRA";
                if (IdTipoAFP == 3) strNombre = "PRIMA";
                if (IdTipoAFP == 4) strNombre = "HABITAT";

                return strNombre;
            } 
        }
        public String CodigoAFP { get; set; }
        public Int32 IdTipoAFP { get; set; }
        public Int32 IdTipoComisionAFP { get; set; }
        public Int32 IdTipoPensionDeseaAfiliar { get; set; }
        public Int32 IdEstaAfiliadoBanco { get; set; }
        public Int32 IdBancoAfiliado { get; set; }
        public String CuentaBancoAfiliado { get; set; }
        public String CuentaBancoCCIAfiliado { get; set; }
        public Int32 IdBancoDeseaAfiliar { get; set; }
        public Int32 IdPresentaAlergias { get; set; }
        public Int32 IdPresentaAlergias1 { get; set; }
        public Int32 IdPresentaAlergias2 { get; set; }
        public String PresentaAlergiasOtro { get; set; }
        public Int32 IdPresentaEnfermedades { get; set; }
        public Int32 IdPresentaEnfermedadesD { get; set; }
        public Int32 IdPresentaEnfermedadesH { get; set; }
        public Int32 IdPresentaEnfermedadesA { get; set; }
        public Int32 IdPresentaEnfermedadesE { get; set; }
        public String PresentaEnfermedadesOtro { get; set; }
        public Int32 IdConsumeMedicamentos { get; set; }
        public String ConsumeMedicamentosOtro { get; set; }
        public Int32 IdPresentaDiscapacidad { get; set; }
        public Int32 IdPresentaDiscapacidadA { get; set; }
        public Int32 IdPresentaDiscapacidadV { get; set; }
        public Int32 IdPresentaDiscapacidadH { get; set; }
        public Int32 IdPresentaDiscapacidadC { get; set; }
        public Int32 IdPresentaDiscapacidadF { get; set; }
        public String PresentaDiscapacidadC { get; set; }
        public String PresentaDiscapacidadF { get; set; }
        public Int32 IdCertificadoDiscapacidad { get; set; }
        public Int32 IdGrupoSanguineo { get; set; }
        public String InformacionAdicionalSalud { get; set; }
        
        public Int32 IdSituacionAcademicaS { get; set; }
	    public Int32 IdSituacionAcademicaT { get; set; }
	    public Int32 IdSituacionAcademicaU { get; set; }
	    public Int32 IdSituacionAcademicaO { get; set; }
	    public Int32 IdCentroEstudiosPU {get; set; }
	    public Int32 IdCentroEstudiosPR {get; set; }
	    public Int32 IdCentroEstudiosEX {get; set; }
	    public String SituacionAcademicaT {get; set; }
	    public String SituacionAcademicaU {get; set; }
	    public String SituacionAcademicaO {get; set; }
	    public String CentroEstudiosPU { get; set; }
	    public String CentroEstudiosPR { get; set; }
	    public String CentroEstudiosEX { get; set; }
	    public Int32 IdGradoAcademicoES {get; set; }
	    public Int32 IdGradoAcademicoEG {get; set; }
	    public Int32 IdGradoAcademicoBA {get; set; }
	    public Int32 IdGradoAcademicoTI {get; set; }
	    public Int32 IdPostgradoM { get; set; }
	    public Int32 IdPostgradoD { get; set; }
	    public Int32 IdPostgradoO { get; set; }
	    public String PostgradoO { get; set; }
	    public String PostgradoCE { get; set; }
        public String PostgradoGrado { get; set; }

        public Int32 Estado { get; set; }
        public String EstadoNombre { get { return ((Estado == 0) ? "INACTIVO" : ((Estado == 1) ? "ACTIVO" : ((Estado == 2) ? "NO REGISTRADO" : ""))); } }
        public String Foto { get; set; }

        public String NroPlanilla { get; set; }
        public Int32 IdUnidadOrganica { get; set; }
        public String NombreCargo { get; set; }
        public String CodigoProceso { get; set; }
        public String NombreProceso { get; set; }
        public String NombreUnidadOrganica { get; set; }
        public Decimal Remuneracion { get; set; }
        public String Meta { get; set; }

        public Int32 IdDeclaraIncompatibilidad { get; set; }
        public Int32 IdDeclaraNepotismo { get; set; }
        public Int32 IdDeclaraNormas { get; set; }
        public Int32 IdDeclaraInteres { get; set; }
        public String NepotismoRel1 { get; set; }
        public String NepotismoRel2 { get; set; }
        public String NepotismoRel3 { get; set; }
        public String NepotismoApe1 { get; set; }
        public String NepotismoApe2 { get; set; }
        public String NepotismoApe3 { get; set; }
        public String NepotismoNom1 { get; set; }
        public String NepotismoNom2 { get; set; }
        public String NepotismoNom3 { get; set; }
        public String NepotismoAre1 { get; set; }
        public String NepotismoAre2 { get; set; }
        public String NepotismoAre3 { get; set; }
        public String NombreBanco
        {
            get
            {
                String strNombre = String.Empty;
                switch (IdBancoAfiliado)
                {
                    case 1: strNombre = "MIBANCO"; break;
                    case 2: strNombre = "BANCO DE CREDITO"; break;
                    case 3: strNombre = "SCOTIABANK PERU"; break;
                    case 4: strNombre = "BANCO CONTINENTAL"; break;
                    case 5: strNombre = "BANCO DE COMERCIO"; break;
                    case 6: strNombre = "CITIBANK DEL PERU"; break;
                    case 7: strNombre = "BANBIF"; break;
                    case 8: strNombre = "NACION"; break;
                    case 9: strNombre = "BANCO FINANCIERO"; break;
                    case 10: strNombre = "COFIDE"; break;
                    case 11: strNombre = "INTERBANK"; break;
                    case 12: strNombre = "BANCO RIPLEY"; break;
                    case 13: strNombre = "BANCO CENTRAL PERU"; break;
                    case 14: strNombre = "BANCO FALABELLA"; break;
                    case 15: strNombre = "AGROBANCO"; break;
                    case 16: strNombre = "BANCO GNB"; break;
                    case 17: strNombre = "SANTANDER PERU"; break;
                    case 18: strNombre = "BANCO AZTECA"; break;
                    case 19: strNombre = "BANCO CENCOSUD"; break;
                    case 20: strNombre = "ICBC BANK"; break;
                    case 21: strNombre = "BANCO PICHINCHA"; break;
                }

                return strNombre;
            }
        }
        public Int32? IdUsuarioRegistro { get; set; }
        public Int32? IdUsuarioModificacion { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public Int32 IdEstadoRegistro { get; set; }
        public String FechaMaximaContrato { get; set; }
        public Grilla_Response Grilla { get; set; }
        public IEnumerable<object> formatos { get; set; }
        public Int32 IdTieneHojaVida { get; set; }
        public Int32 IdTieneSustento { get; set; }
        public Int32 IdTieneDDJJ { get; set; }
        public Int32 IdTieneFormato { get; set; }
        public Int32 TotalAlertas { get; set; }
        
        public byte[] FileHojaVida { get; set; }
        public byte[] FileSustento { get; set; }
        public byte[] FileDDJJ { get; set; }
        public byte[] FileFormato { get; set; }

	}
}