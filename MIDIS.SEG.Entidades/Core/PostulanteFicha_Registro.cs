using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
    public partial class PostulanteFicha_Registro
	{
	    public Int32 IdPostulante { get; set; }
        public String TipoDocumento { get; set; }
        public String NroDocumento { get; set; }
        public String NombreCompleto { get { return String.Format("{0} {1}, {2}", Paterno, Materno, Nombre); } }
        public String Contrasena { get; set; }
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
        public String NombreAFPAfiliada { get; set; }
        public Int32 IdTipoPensionDeseaAfiliar { get; set; }
        public Int32 IdEstaAfiliadoBanco { get; set; }
        public Int32 IdBancoAfiliado { get; set; }
        public String CuentaBancoAfiliado { get; set; }
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
	    public Int32 iSituacionAcademicaT { get; set; }
	    public Int32 iSituacionAcademicaU { get; set; }
	    public Int32 iSituacionAcademicaO { get; set; }
	    public Int32 iCentroEstudiosPU {get; set; }
	    public Int32 iCentroEstudiosPR {get; set; }
	    public Int32 iCentroEstudiosEX {get; set; }
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
        

        public Int32? IdUsuarioRegistro { get; set; }
        public Int32? IdUsuarioModificacion { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public Grilla_Response Grilla { get; set; }
        
	}
}