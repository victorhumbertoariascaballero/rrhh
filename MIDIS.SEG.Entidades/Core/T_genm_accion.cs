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
#endregion

namespace MIDIS.ORI.Entidades
{
    [DataContract]
    public partial class T_genm_accion : BEPaginacion
    {
        #region Propiedades
        [DataMember]
        public decimal Id_accion { get; set; }
        [DataMember]
        public string Nombre_accion { get; set; }
        [DataMember]
        public decimal Id_aplicacion { get; set; }
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
        [DataMember]
        public string Flg_estado { get; set; }

        #endregion
    }
}