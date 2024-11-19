/*----------------------------------------------------------------------------------------
ARCHIVO MODELO  : Entidad

Objetivo: Clase Parcial entidad que identifica a la tabla de la base de datos
Autor: Miguel Angel Salvador Paucar (MASP)
Fecha Creacion: 2015-09-03
----------------------------------------------------------------------------------------*/
#region Espacio de Nombres
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Data;
#endregion

namespace MIDIS.ORI.Entidades
{
    public partial class T_genm_aplicacion 
    {
        #region generales

        public T_genm_aplicacion() { }

        public T_genm_aplicacion(IDataReader vReader)
        {
            
			this.Id_aplicacion = Convert.ToDecimal(vReader["Id_aplicacion"]);  
			this.Id_grupo = Convert.ToDecimal(vReader["Id_grupo"]);  
			this.Tipo_seguridad = Convert.ToDecimal(vReader["Tipo_seguridad"]);  
			this.Flg_tienesobre = Convert.ToString(vReader["Flg_tienesobre"]);  
			this.Nombre_aplicacion = Convert.ToString(vReader["Nombre_aplicacion"]);  
			this.Direccion_aplicacion = Convert.ToString(vReader["Direccion_aplicacion"]);  
			this.Direccion_imagen = Convert.ToString(vReader["Direccion_imagen"]);  
			this.Flg_eliminado = Convert.ToString(vReader["Flg_eliminado"]);  
			this.Ip_ingreso = Convert.ToString(vReader["Ip_ingreso"]);  
			this.Id_usuario_ingresa = Convert.ToDecimal(vReader["Id_usuario_ingresa"]);  
			this.Usu_ingresa = Convert.ToString(vReader["Usu_ingresa"]);  
			this.Fec_ingreso = Convert.ToDateTime(vReader["Fec_ingreso"]);  
			this.Ip_modifica = Convert.ToString(vReader["Ip_modifica"]);  
			this.Id_usuario_modifica = Convert.ToDecimal(vReader["Id_usuario_modifica"]);  
			this.Usu_modifica = Convert.ToString(vReader["Usu_modifica"]);  
			this.Fec_modifica = Convert.ToDateTime(vReader["Fec_modifica"]);
            this.Flag_tienesobre = Convert.ToString(vReader["Flg_tienesobre"]).Equals("1");
        }

        #endregion

        #region Propiedades Adicionales
        [DataMember]
        public Boolean Flag_tienesobre { get; set; }
        [DataMember]
        public List<T_genm_opcion_permiso> OpcionPermisos { get; set; }      
        #endregion

        #region Lista de Entidades

        
        #endregion
    }
}