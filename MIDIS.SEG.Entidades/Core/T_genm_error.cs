#region Espacio de Nombres
using MIDIS.ORI.Entidades.Base;
using System;
using System.Runtime.Serialization;
#endregion

namespace MIDIS.ORI.Entidades
{
    [DataContract]
    public partial class T_genm_error : BEPaginacion
    {
        #region Propiedades
        [DataMember]
        public decimal Id_error { get; set; }
        [DataMember]
        public decimal Id_aplicacion { get; set; }
        [DataMember]
        public string Desc_error { get; set; }
        [DataMember]
        public string Flg_eliminado { get; set; }
        [DataMember]
        public string Ip_ingreso { get; set; }
        [DataMember]
        public decimal Id_usuario_ingresa { get; set; }
        [DataMember]
        public string Usu_ingresa { get; set; }
        [DataMember]
        public DateTime Fec_ingreso { get; set; }
        [DataMember]
        public string Ip_modifica { get; set; }
        [DataMember]
        public decimal Id_usuario_modifica { get; set; }
        [DataMember]
        public string Usu_modifica { get; set; }
        [DataMember]
        public DateTime Fec_modifica { get; set; }

        #endregion
    }
}
