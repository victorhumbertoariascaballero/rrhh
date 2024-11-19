namespace MVCSisRRHH.Models
{
    public class mUsuarioPersona
    {
        public long biIdPersona { get; set; }
        public string iIdTipDocumento { get; set; }
        public string iIdTipoGenero { get; set; }
        public string iIdTipPersona { get; set; }
        public int iCodTrabajador { get; set; }

        public int iCodEntidad { get; set; }
        public string vNombre_Entidad { get; set; }
        public string vDescripcion_Entidad { get; set; }
        public string vCodIdentificacion_Entidad { get; set; }

        public string vNombres { get; set; }
        public string vApePaterno { get; set; }
        public string vApeMaterno { get; set; }
        public string vRazonSocial { get; set; }
        public string vNroDocumento { get; set; }
        public string dtFecNacimiento { get; set; }
        public string dtFecCreacion { get; set; }
        public string dtFecModificacion { get; set; }
        public bool bEstado { get; set; }
        public bool bFlgRegManual { get; set; }

        public string vEmail { get; set; }
        public string vCelular { get; set; }
        
        public string vCodigoRespuesta { get; set; }
        public string sMensaje { get; set; }
    }
}
