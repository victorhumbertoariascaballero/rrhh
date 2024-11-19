using System;
namespace MIDIS.ORI.Entidades.Auth
{
    public class BE_TAB_Usuario : BE_Generico
    {
        public int iCodUsuario { get; set; }
        public string vUsuario { get; set; }
        public string vPassword { get; set; }
        public string vToken { get; set; }
        public string vEmail { get; set; }
        public string vRutaInicio { get; set; }
        public string vUrlRedireccion { get; set; }
        public BE_TAB_Persona oPersona { get;set;}

    }

    public class BE_TAB_Personal : BE_Generico
    {
        public int iCodPersonal { get; set; }
        public BE_TAB_Persona oPersona { get;set; }            
    }

    public class BE_TAB_Persona : BE_Generico
    {
        public int iCodPersona { get; set; }
        public int iCodEntidad { get; set; }
        public string cCodDocPersona { get; set; }
        public string cCodTipoDocumento { get; set; }
        public string vNombres { get; set; }
        public string vApePaterno { get; set; }
        public string vApeMaterno { get; set; }
        public int iCodCargo { get; set; }
        public string vCargo { get; set; }
        public string vcCodigoSARH { get; set; }
        public string vcActivo { get; set; }
        public DateTime dtFecInicioLabores { get; set; }
        public DateTime dtFecFinLabores { get; set; }
    }


}
