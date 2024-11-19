namespace MIDIS.ORI.Entidades.Auth
{
    public class BE_Generico
    {
        public string vColumnaArchivo { get; set; }
        public string vColumnaNombreArchivo { get; set; }
        public string Message { get; set; }
        public string MessageCode { get; set; }
        public int iCodUsuario_Aud { get; set; }
        public string vcIPTerminal_Aud { get; set; }
        public string vcNomTerminal_Aud { get; set; }
        public string vcNomSistema_Aud { get; set; }
        public string vcModuloSistema_Aud { get; set; }
        public int iPageSize { get; set; }
        public int iCurrentPage { get; set; }
        public string sortOrder { get; set; }
        public string sortColumn { get; set; }
    }
}
