using System.Collections.Generic;
namespace MVCSisRRHH.Models
{
    public class mUsuarioResponse
    {
        public long iCodUsuario { get; set; }
        public string vNombreUsuario { get; set; }
        public string vUbigeo { get; set; }
        public bool bFlgUsuarioIntegrado { get; set; }
        public string vCodigoRespuesta { get; set; }
        public string sMensaje { get; set; }
        public mUsuarioPersona Persona { get; set; }
        public List<mUsuarioPerfil> Perfil { get; set; }
        public List<mUsuarioOpciones> Opciones { get; set; }
    }
}
