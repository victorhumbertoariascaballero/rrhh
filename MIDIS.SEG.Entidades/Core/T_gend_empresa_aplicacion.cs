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
    public partial class T_gend_empresa_aplicacion : BEPaginacion
    {
        #region Propiedades
        [DataMember]  
		public long Id_empresa_aplicacion { get; set; }  
		[DataMember]  
		public long? Id_empresa { get; set; }  
		[DataMember]  
		public long? Id_aplicacion { get; set; }  
		[DataMember]  
		public long? Id_situacion { get; set; }  
		[DataMember]  
		public bool Flg_eliminado { get; set; }  
		[DataMember]  
		public long? Id_sesion_ingreso { get; set; }  
		[DataMember]  
		public DateTime? Fec_ingreso { get; set; }  
		[DataMember]  
		public long? Id_sesion_modifica { get; set; }  
		[DataMember]  
		public DateTime? Fec_modifica { get; set; }  
		
        #endregion
    }
}