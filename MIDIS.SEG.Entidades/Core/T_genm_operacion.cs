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
    public partial class T_genm_operacion : BEPaginacion
    {
        #region Propiedades
        [DataMember]  
		public long Id_operacion { get; set; }  
		[DataMember]  
		public long Id_opcion { get; set; }  
		[DataMember]
        [Required(ErrorMessage = "Debe ingresar el código de la operación")]
        public string Codigo { get; set; }  
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
		
        #endregion
    }
}