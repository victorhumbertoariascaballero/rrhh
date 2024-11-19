using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
    public partial class Legajo_Registro
	{
	    public Int32 IdPostulante { get; set; }
        public Int32 IdPostulacion { get; set; }
        public Int32 IdConvocatoria { get; set; }
        public Int32 TipoDocumento { get; set; }
        public String NroDocumento { get; set; }
        public String IdRegistro { get { return String.Format("{0}|{1}|{2}", IdPostulante, IdPostulacion, IdConvocatoria); } }
        public String NombreCompleto { get { return String.Format("{0} {1}, {2}", Paterno, Materno, Nombre); } }
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
        public String Ubigeo { get; set; }
        public String DescripcionUbigeo { get; set; }
        public String Domicilio { get; set; }
        public String Telefono { get; set; }
        public String Celular { get; set; }
        public String CorreoElectronico { get; set; }
        public String RUC { get; set; }
        public String IdDrive { get; set; }
        public String IdDrive1 { get; set; }
        public String IdDrive2 { get; set; }
        public String IdDrive3 { get; set; }
        public String IdDrive4 { get; set; }
        public String IdDrive5 { get; set; }
        public String IdDrive6 { get; set; }
        public String IdDrive7 { get; set; }
        public Int32 Estado { get; set; }
        public String EstadoNombre { get { return ((Estado == 0) ? "INACTIVO" : ((Estado == 1) ? "ACTIVO" : ((Estado == 2) ? "NO REGISTRADO" : ""))); } }
        public String Foto { get; set; }

        public Int32? IdUsuarioRegistro { get; set; }
        public Int32? IdUsuarioModificacion { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public Int32 IdEstadoRegistro { get; set; }
        public Grilla_Response Grilla { get; set; }
        //public IEnumerable<object> formatos { get; set; }
 //       public byte[] FileHojaVida { get; set; }
 //       public byte[] FileSustento { get; set; }
 //       public byte[] FileDDJJ { get; set; }
 //       public byte[] FileFormato { get; set; }

	}
}