using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
	public partial class Convocatoria_Registro
	{
	    public Int32 IdConvocatoria { get; set; }
        public Int32 IdBase { get; set; }
        public Int32 IdPerfil { get; set; }
        public Int32 IdDependencia { get; set; }
        public Int32 IdOrgano { get; set; }
        public Int32 IdTipo { get; set; } //1: CAS  2: PRACTICAS  3: SERVIR
        //public Int32 IdTipoConvocatoria { get; set; } //1: CAS  2: SERVIR
        public Int32 IdTipoApertura { get; set; } //1: ABIERTO  2: CERRADO
        public Int32 CantidadVacantes { get; set; }
        public String Año { 
            get {
                String anio = String.Empty;
                if (!String.IsNullOrEmpty(this.NroConvocatoria)) {
                    anio = this.NroConvocatoria.Substring(this.NroConvocatoria.Length - 4);
                }
                return anio;
            } 
        }
        public String AñoServir
        {
            get
            {
                String anio = String.Empty;
                if (!String.IsNullOrEmpty(this.NroConvocatoria) && this.NroConvocatoria.Length > 11)
                {
                    anio = this.NroConvocatoria.Substring(this.NroConvocatoria.Length - 11, 4);
                }
                return anio;
            }
        }
        public String NroConvocatoria { get; set; }
        public String NombreCargo { get; set; }
        public String NroAIRHSP { get; set; }
        public String Meta { get; set; }
        public String Organo { get; set; }
        public String Dependencia { get; set; }
        public String Contrasena { get; set; }
        public Int32 Estado { get; set; }
        public Int32 IdMostrarPostular { get; set; }
        public Decimal Remuneracion { get; set; }
        public String EstadoNombre { get; set; } // { return ((Estado == 1) ? "PENDIENTE" : ((Estado == 2) ? "EN PROCESO" : ((Estado == 3) ? "CONCLUIDA" : ((Estado == 5) ? "CANCELADA" : String.Empty)))); } }
        public Int32 IdResponsableCurricular { get; set; }
        public Int32 IdTieneRequerimiento { get; set; }
        public Int32 IdTieneCertificacion { get; set; }
        public Int32 IdTieneComite { get; set; }
        public Int32 IdTieneActaCurri { get; set; }
        public Int32 IdTieneExamenConoc { get; set; }
        public Int32 IdTieneExamenPsico { get; set; }
        public String ResponsableCurricular { get; set; }
        public String FechaPublicacionDesde { get; set; }
        public String FechaPublicacionHasta { get; set; }
        public String FechaPostulacion { get; set; }
        public String FechaCurricularDesde { get; set; }
        public String FechaCurricularHasta { get; set; }
        public String FechaConocimientos { get; set; }
        public String FechaPsicologico { get; set; }
        public String FechaResultadosCurri { get; set; }
        public String FechaResultadosConoc { get; set; }
        public String FechaResultadosPsico { get; set; }
        public String FechaEntrevistaDesde { get; set; }
        public String FechaEntrevistaHasta { get; set; }
        public String FechaResultadoFinal { get; set; }
        public String FechaContratoDesde { get; set; }
        public String FechaContratoHasta { get; set; }

        public DateTime dFechaRegCVPostulante { get; set; }
        public DateTime dFechaDesdeEvaCV { get; set; }
        public DateTime dFechaHastaEvaCV { get; set; }
        public DateTime dFechaPubResultadoMIDIS { get; set; }
        public DateTime dFechaDesdeEntrevista { get; set; }
        public DateTime dFechaHastaEntrevista { get; set; }
        public DateTime dFechaPubResultadoFinalMIDIS { get; set; }
        public DateTime dFechaDesdeSuscripcionContrato { get; set; }
        public DateTime dFechaHastaSuscripcionContrato { get; set; }

        public String Fecha1 { get { return String.Format("{0} - {1}", FechaPublicacionDesde, FechaPublicacionHasta); } }
        public String Fecha2 { get { return String.Format("{0}", FechaPostulacion); } }
        public String Fecha3 { get { return String.Format("{0} - {1}", FechaCurricularDesde, FechaCurricularHasta); } }
        public String Fecha4 { get { return String.Format("{0}", FechaResultadosCurri); } }
        public String Fecha5 { get { return (IdTieneExamenConoc == 1 ? FechaConocimientos: "NO APLICA"); } }
        //public String Fecha6 { get { return (IdTieneExamenPsico == 1 ? FechaPsicologico : "NO APLICA"); } }
        public String Fecha6 { get { return (IdTieneExamenConoc == 1 ? FechaResultadosConoc : "NO APLICA"); } }
        public String Fecha7 { get { return String.Format("{0} - {1}", FechaEntrevistaDesde, FechaEntrevistaHasta); } }
        public String Fecha8 { get { return String.Format("{0}", FechaResultadoFinal); } }
        public String Fecha9 { get { return String.Format("{0} - {1}", FechaContratoDesde, FechaContratoHasta); } }

        public String TextoComunicados { get; set; }
        public String TextoCurricular { get; set; }
        public String TextoConocimientos { get; set; }
        public String TextoFinal { get; set; }

        public Int32 EsFecha1 { 
            get {
                Int32 existe = 0;
                if (!String.IsNullOrEmpty(FechaPublicacionDesde) && (!String.IsNullOrEmpty(FechaPublicacionHasta))) {
                    if (DateTime.Parse(FechaPublicacionDesde) < DateTime.Now && DateTime.Parse(FechaPublicacionHasta).AddDays(1).AddMinutes(-1) > DateTime.Now)
                        existe = 1;
                }
                
                return existe;
            } 
        }
        public Int32 EsFecha2
        {
            get
            {
                Int32 existe = 0;
                if (!String.IsNullOrEmpty(FechaPostulacion)) {
                    DateTime aux = DateTime.Parse(FechaPostulacion);
                    if (aux.Year == DateTime.Now.Year && aux.Month == DateTime.Now.Month && aux.Day == DateTime.Now.Day)
                        existe = 1;
                }

                return existe;
            }
        }
        public Int32 EsFecha3
        {
            get
            {
                Int32 existe = 0;
                if (!String.IsNullOrEmpty(FechaCurricularDesde) && (!String.IsNullOrEmpty(FechaCurricularHasta))) {
                    if (DateTime.Parse(FechaCurricularDesde) < DateTime.Now && DateTime.Parse(FechaCurricularHasta).AddDays(1).AddMinutes(-1) > DateTime.Now)
                        existe = 1;
                }
                
                return existe;
            }
        }
        public Int32 EsFecha4
        {
            get
            {
                Int32 existe = 0;
                if (!String.IsNullOrEmpty(FechaResultadosCurri)) {
                    DateTime aux = DateTime.Parse(FechaResultadosCurri);
                    if (aux.Year == DateTime.Now.Year && aux.Month == DateTime.Now.Month && aux.Day == DateTime.Now.Day)
                        existe = 1;
                }
                
                return existe;
            }
        }
        public Int32 EsFecha5
        {
            get
            {
                Int32 existe = 0;
                if (!String.IsNullOrEmpty(FechaConocimientos))
                {
                    DateTime aux = DateTime.Parse(FechaConocimientos);
                    if (aux.Year == DateTime.Now.Year && aux.Month == DateTime.Now.Month && aux.Day == DateTime.Now.Day)
                        existe = 1;
                }

                return existe;
            }
        }
        public Int32 EsFecha6
        {
            get
            {
                Int32 existe = 0;
                return existe;
            }
        }
        public Int32 EsFecha7
        {
            get
            {
                Int32 existe = 0;
                if (!String.IsNullOrEmpty(FechaEntrevistaDesde) && (!String.IsNullOrEmpty(FechaEntrevistaHasta))) {
                    if (DateTime.Parse(FechaEntrevistaDesde) < DateTime.Now && DateTime.Parse(FechaEntrevistaHasta).AddDays(1).AddMinutes(-1) > DateTime.Now)
                        existe = 1;
                }
                
                return existe;
            }
        }
        public Int32 EsFecha8
        {
            get
            {
                Int32 existe = 0;
                if (!String.IsNullOrEmpty(FechaResultadoFinal)) {
                    DateTime aux = DateTime.Parse(FechaResultadoFinal);
                    if (aux.Year == DateTime.Now.Year && aux.Month == DateTime.Now.Month && aux.Day == DateTime.Now.Day)
                        existe = 1;
                }
                
                return existe;
            }
        }
        public Int32 EsFecha9
        {
            get
            {
                Int32 existe = 0;
                if (!String.IsNullOrEmpty(FechaContratoDesde) && (!String.IsNullOrEmpty(FechaContratoHasta))) {
                    if (DateTime.Parse(FechaContratoDesde) < DateTime.Now && DateTime.Parse(FechaContratoHasta).AddDays(1).AddMinutes(-1) > DateTime.Now)
                        existe = 1;
                }
                
                return existe;
            }
        }

        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public Int32? IdUsuarioRegistro { get; set; }
        public Int32? IdUsuarioModificacion { get; set; }
        public Int32 IdComiteDependencia1 { get; set; }
        public Int32 IdComiteMiembro1T { get; set; }
        public Int32 IdComiteMiembro1S { get; set; }
        public Int32 IdComiteDependencia2 { get; set; }
        public Int32 IdComiteMiembro2T { get; set; }
        public Int32 IdComiteMiembro2S { get; set; }
        public Int32 IdComiteDependencia3 { get; set; }
        public Int32 IdComiteMiembro3T { get; set; }
        public Int32 IdComiteMiembro3S { get; set; }
        public Int32 IdTipoNotificacion { get; set; }

        public byte[] fileCertificacion { get; set; }
        public byte[] fileRequerimiento { get; set; }
        public byte[] fileComite { get; set; }
        public byte[] fileActaCurri { get; set; }
        public byte[] fileComunicado { get; set; }
        public byte[] fileEntrevista { get; set; }
        public IEnumerable<object> formatos { get; set; }
        public List<ConvocatoriaComite_Registro> comite { get; set; }
        
        public Grilla_Response Grilla { get; set; }
	}
}