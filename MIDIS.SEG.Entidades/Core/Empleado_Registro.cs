using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
	public partial class Empleado_Registro
	{
	    public Int32 IdEmpleado { get; set; }
        public String CodigoSisper { get; set; }
        public Int32 IdDependencia { get; set; }
        public String NombreCompleto { get { return String.Format("{0} {1}, {2}", Paterno, Materno, Nombre); } }
        public String Contrasena { get; set; }
        public String Nombre { get; set; }
		public String Paterno { get; set; }
		public String Materno { get; set; }
		public String TipoDocumento { get; set; }
        public String NroDocumento { get; set; }
		//public String Correo { get; set; }
		public String Sigla { get; set; }
        public String FechaNacimiento { get; set; }
        public String RUC { get; set; }
        public String NombreEntidad { get; set; }
        public String NombreOficina { get; set; }
        public String NombreArchivo { get; set; }
        public String NombreCargo { get; set; }
        public String Anio { get; set; }
        public String Mes { get; set; }
        public Int32 Estado { get; set; }
        public String EstadoNombre { get { return ((Estado == 0) ? "INACTIVO" : ((Estado == 1) ? "ACTIVO" : ((Estado == 2) ? "NO REGISTRADO" : ""))); } }
        public Int32 EstadoEnvio { get; set; }
        public String EstadoEnvioNombre { get { return ((EstadoEnvio == 0) ? "NO ENVIADO" : ((EstadoEnvio == 1) ? "ENVIADO" : "")); } }
        public String AnioMes { get { return String.Format("{0} {1}", Anio, NombreMes); } }
        public Int32 IdGenero { get; set; }
        public Int32 IdCondicion { get; set; }
        public Int32 IdContrato { get; set; }
        public Int32 IdSede { get; set; }
        public Int32 IdGrupoSanguineo { get; set; }
        public Int32 IdEstadoCivil { get; set; }
        public Int32 IdTipoPension { get; set; }
        public Int32 IdTipoAfp { get; set; }
        public Int32 TieneJefatura { get; set; }
        public Int32 TieneEncargatura { get; set; }
        public Int32 IdTipoIngreso { get; set; }
        public Int32 IdTipoContrato { get; set; }
        public Int32 IdTipoModalidad { get; set; }
        public String DocIngreso { get; set; }
        public String DocCese { get; set; }
        public String TipoIngreso { get; set; }

        public String CodigoAfp { get; set; }
        public Int32 IdTipoComisionAfp { get; set; }
        public String Genero { get { return (IdGenero == 0) ? "" : ((IdGenero == 1) ? "FEMENINO" : "MASCULINO"); } }
        public String Domicilio { get; set; }
        public String CondicionLaboral { get; set; }
        public String Sede { get; set; }
        public String GrupoSanguineo { get; set; }
        public String Telefono { get; set; }
        public String Celular { get; set; }
        public String CorreoElectronico { get; set; }
        public String TelefonoLaboral { get; set; }
        public String AnexoLaboral { get; set; }
        public String CelularLaboral { get; set; }
        public String CorreoElectronicoLaboral { get; set; }
        public String NombreMes
        { 
            get {
                String strMes = String.Empty;
                if (Mes == "01") strMes = "ENERO";
                if (Mes == "02") strMes = "FEBRERO";
                if (Mes == "03") strMes = "MARZO";
                if (Mes == "04") strMes = "ABRIL";
                if (Mes == "05") strMes = "MAYO";
                if (Mes == "06") strMes = "JUNIO";
                if (Mes == "07") strMes = "JULIO";
                if (Mes == "08") strMes = "AGOSTO";
                if (Mes == "09") strMes = "SETIEMBRE";
                if (Mes == "10") strMes = "OCTUBRE";
                if (Mes == "11") strMes = "NOVIEMBRE";
                if (Mes == "12") strMes = "DICIEMBRE";
                                
                return strMes;
            } 
        }
        public String Guid { get; set; }
        public String Planilla { get; set; }
        public String TipoPlanilla { get; set; }
        public String FechaEnvio { get; set; }
        public String FechaRecepcion { get; set; }
        public String DescripcionUbigeo { get; set; }
        public String Foto { get; set; }
        public byte[] Boleta { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaCese { get; set; }
        public String FechaInicioCadena { 
            get {
                String valor = String.Empty;
                if (FechaInicio.HasValue) valor = FechaInicio.Value.ToString("dd/MM/yyyy");

                return valor;
            } }
        public String FechaCeseCadena {
            get {
                String valor = String.Empty;
                if (FechaCese.HasValue) valor = FechaCese.Value.ToString("dd/MM/yyyy");

                return valor;
            }
        }

        //public Persona_Response Persona { get; set; }
        public String Ubigeo { get; set; }
        
        public Int32? IdUsuarioRegistro { get; set; }
        public Int32? IdUsuarioModificacion { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public String NroOrden { get; set; }
        public String NombreOrden { get; set; }
        public Int32 DuracionOrden { get; set; }
        public String InicioOrden { get; set; }
        public String FinOrden { get; set; }
        public Decimal MontoOrden { get; set; }
        
        public Grilla_Response Grilla { get; set; }

        public int IdMaestro { get; set; }
        public int IdMaestroDetalle { get; set; }
        public int DiasDescansoFisico { get; set; }
        public int MesesDescansoFisico { get; set; }
        public int DiasVacacionesTruncas { get; set; }
        public int MesesVacacionesTruncas { get; set; }
        public int IdTipoPlanillaAdicional { get; set; }
        public string NombreTipoPlanillaAdicional { get; set; }
        public decimal MontoPlanillaAdicional { get; set; }
        public bool FED { get; set; }
        public bool Sindicato { get; set; }
        public bool DDJJ { get; set; }
        public bool FFAA { get; set; }
        public bool Deportista { get; set; }
        public bool Discapacidad { get; set; }

        public int TieneContrato { get; set; }
        public String NroAIRHSP { get; set; }
        public String Meta { get; set; }
        public String NroContrato { get; set; }
        public String NombreProceso { get; set; }
        public Decimal Remuneracion { get; set; }

        public bool EPSEsSalud { get; set; }
        public bool SupsRet4taCat { get; set; }
        public string sNroAutorizacionExoneracion { get; set; }
        public bool DsctoJudicial { get; set; }
        public int iCodJudicial { get; set; }
        public Int32 Director { get; set; }
        public bool Encargatura { get; set; }
        public bool ExisteDirector { get; set; }        
        public List<DescuentoJudicialBeneficiario_Registro> lstDescuentoJudicialBeneficiario_Registro { get; set; }
    }
}