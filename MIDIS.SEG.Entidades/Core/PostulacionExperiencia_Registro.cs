using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
    public partial class PostulacionExperiencia_Registro
	{
	    public Int32 IdPostulacionExperiencia { get; set; }
        public Int32 IdPostulacion { get; set; }
        public Int32 IdPostulante { get; set; }
        public Int32 IdLaboral { get; set; }
        public String Empresa { get; set; }
        public String Cargo { get; set; }
        public String Descripcion { get; set; }
        public String FechaInicio { get; set; }
        public String FechaFin { get; set; }
        public String ExperienciaPerfil { 
            get {
                if (Requisito.Codigo == "0")
                    return "GENERAL";
                else if (Requisito.Codigo == "1")
                    return "GENERAL Y ESPECÍFICA";
                else
                    return String.Empty;
            } 
        }
        public Double RangoFecha
        { 
            get {
                if (!String.IsNullOrEmpty(FechaInicio) && !String.IsNullOrEmpty(FechaFin))
                    return (DateTime.Parse(FechaFin) - DateTime.Parse(FechaInicio)).TotalDays;
                else
                    return 0;
            } 
        }
        public String RangoFechaCadena
        { 
            get {
                String rango = String.Empty;
                if (!String.IsNullOrEmpty(FechaInicio) && !String.IsNullOrEmpty(FechaFin)) {
                    TimeSpan diferencia = DateTime.Parse(FechaFin) - DateTime.Parse(FechaInicio);
                    
                    int years = (int)(diferencia.TotalDays / 365.25);
                    int months = (int)(((diferencia.TotalDays / 365.25) - years) * 12);
                    int days = (int)(((((diferencia.TotalDays / 365.25) - years) * 12) - months) * 365.25 / 12);

                    rango = String.Format("{0} años, {1} meses, {2} días", years, months, days);
                }

                return rango;
            } 
        }
        public Int32 Estado { get; set; }
        
        public Int32? IdUsuarioRegistro { get; set; }
        public Int32? IdUsuarioModificacion { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public IEnumerable<object> formatos { get; set; }
        public Int32 IdTieneArchivo { get; set; }
        public Int32 IdExperienciaPerfil { get; set; }
        public Int32 IdExperienciaAuditoria { get; set; }
        public Int32 IdTipoConvocatoria { get; set; }
        public Int32 IdTipoActualizacion { get; set; }
        public Estado_Response Requisito { get; set; }
        public Estado_Response Auditoria { get; set; }
        public byte[] FileArchivo { get; set; }
        
        public Grilla_Response Grilla { get; set; }
        public Int32 iSector { get; set; }
        public String Sector
        {
            get
            {
                switch (iSector)
                {
                    case 1:
                        return "Privado";
                    case 2:
                        return "Público";
                    default:
                        return "No encontrado";
                }
            }
        }
        public Int32 iRegimen { get; set; }
        public String Regimen
        {
            get
            {
                switch (iRegimen)
                {
                    case 1:
                        return "N° 276";
                    case 2:
                        return "N° 728";
                    case 3:
                        return "N° 1057";
                    case 4:
                        return "N° 1024";
                    case 5:
                        return "PAC";
                    case 6:
                        return "FAG";
                    case 7:
                        return "Ley N° 30057";
                    case 8:
                        return "N° 1401";
                    case 9:
                        return "Otro";
                    default:
                        return "No encontrado";
                }
            }
        }
        public String NombreJefeDirecto { get; set; }
        public String PuestoJefeDirecto { get; set; }
        public String MotivoCambio { get; set; }
        public decimal Remuneracion { get; set; }
        public String RefLaboralNombre { get; set; }
        public String RefLaboralPuesto { get; set; }
        public String RefLaboralTelefono { get; set; }
        public String RefLaboralCorreo { get; set; }
    }
}