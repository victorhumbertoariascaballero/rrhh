using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
    public partial class PostulacionPostulante_Registro
	{
	    public Int32 IdPostulacion { get; set; }
        public Int32 IdPostulante { get; set; }
        public Int32 TipoDocumento { get; set; }
        public String NroDocumento { get; set; }
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
        public String Colegio { get; set; }
        public String NroColegiatura { get; set; }
        public String DescripcionUbigeo { get; set; }
        public String Domicilio { get; set; }
        public String Telefono { get; set; }
        public String Celular { get; set; }
        public String CorreoElectronico { get; set; }
        public String RUC { get; set; }
        
        public Int32 Estado { get; set; }
        public Int32 FFAA { get; set; }
        public Int32 Discapacidad { get; set; }
        public Int32 Deportista { get; set; }
        public String EstadoNombre { get { return ((Estado == 0) ? "INACTIVO" : ((Estado == 1) ? "ACTIVO" : ((Estado == 2) ? "NO REGISTRADO" : ""))); } }
        public String Foto { get; set; }
        public Int32 IdDetalle { get; set; }
        public Int32 IdTipo { get; set; }
        public Int32? IdUsuarioRegistro { get; set; }
        public Int32? IdUsuarioModificacion { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public String FechaModificacion { get; set; }
        public Int32 IdEstadoRegistro { get; set; }
        public Grilla_Response Grilla { get; set; }
        //public IEnumerable<object> formatos { get; set; }
        //public Int32 IdTieneHojaVida { get; set; }
        //public Int32 IdTieneSustento { get; set; }
        //public Int32 IdTieneDDJJ { get; set; }
        //public Int32 IdTieneFormato { get; set; }
        //public Int32 TotalAlertas { get; set; }
        
        //public byte[] FileEstudio { get; set; }
        //public byte[] Filecapacitacion { get; set; }
        //public byte[] FileExperiencia { get; set; }
        //public byte[] FileDocumento { get; set; }
        public String DireccionActual { get; set; }
        public String Referencia { get; set; }
        public bool? bColigiatura { get; set; }
        public bool? bColegiatura_Habilitada { get; set; }
        public String CarnetConadis { get; set; }
        public String Acreditacion { get; set; }
        public bool? bTrabajarProvincia { get; set; }
        public String Trabajar_Provincia { get; set; }
        public String CarnetFuerzasArmadas { get; set; }
    }
}