/*----------------------------------------------------------------------------------------
ARCHIVO MODELO: Entidad

Objetivo: Clase entidad que identifica a la tabla de la base de datos
Autor: Miguel Angel Salvador Paucar (MASP)
Fecha Creacion: 2015-09-03
----------------------------------------------------------------------------------------*/
#region Espacio de Nombres
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using MIDIS.ORI.Entidades.Base;
using System.ComponentModel.DataAnnotations;
#endregion

namespace MIDIS.ORI.Entidades
{
    [DataContract]
    [Serializable]
    public partial class T_genm_usuario : BEPaginacion
    {
        #region Propiedades
        [DataMember]
        public long Id_usuario { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Debe ingresar el codigo de Usuario")]
        public string Cod_usuario { get; set; }
        [DataMember]
        public byte[] Clave { get; set; }
        [DataMember]
        public bool Flg_tienehorario { get; set; }
        [DataMember]
        public bool Flg_tieneentidad { get; set; }
        [DataMember]
        public bool Flg_usuariointegrado { get; set; }
        [DataMember]
        public long Id_sobreactivo { get; set; }
        [DataMember]
        public bool Flg_eliminado { get; set; }
        [DataMember]
        public string Ip_ingreso { get; set; }
        [DataMember]
        public long Id_usuario_ingresa { get; set; }
        [DataMember]
        public string Usu_ingresa { get; set; }
        [DataMember]
        public DateTime Fec_ingreso { get; set; }
        [DataMember]
        public string Ip_modifica { get; set; }
        [DataMember]
        public long Id_usuario_modifica { get; set; }
        [DataMember]
        public string Usu_modifica { get; set; }
        [DataMember]
        public DateTime Fec_modifica { get; set; }
        [DataMember]
        public bool Flg_activo { get; set; }
        [DataMember]
        public bool Flg_reset { get; set; }

        #endregion
    }
}