namespace MVCSisRRHH.Models
{
    public class mUsuarioPerfil
    {
        public long iCodPerfil { get; set; }
        public string vNombrePerfil { get; set; }
        public int iCodAplicacion { get; set; }
        public string dFechaRegistro { get; set; }
        public long iCodUsuarioRegistro { get; set; }
        public string dFechaModifica { get; set; }
        public string iCodUsuarioModifica { get; set; }
        public bool bFlgEliminado { get; set; }
    }
}
