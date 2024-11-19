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
    public partial class T_genm_usuario_perfil 
    {
        #region generales

        public T_genm_usuario_perfil() { }

        public T_genm_usuario_perfil(IDataReader vReader)
        {
            
			this.Id_usuario_perfil = Convert.ToDecimal(vReader["Id_usuario_perfil"]);  
			if (!Convert.IsDBNull(vReader["Id_usuario"])) { this.Id_usuario = Convert.ToDecimal(vReader["Id_usuario"]); }
			if (!Convert.IsDBNull(vReader["Id_perfil"])) { this.Id_perfil = Convert.ToDecimal(vReader["Id_perfil"]); }
			if (!Convert.IsDBNull(vReader["Ip_ingreso"])) { this.Ip_ingreso = Convert.ToString(vReader["Ip_ingreso"]); }
			if (!Convert.IsDBNull(vReader["Id_usuario_ingresa"])) { this.Id_usuario_ingresa = Convert.ToDecimal(vReader["Id_usuario_ingresa"]); }
			if (!Convert.IsDBNull(vReader["Usu_ingresa"])) { this.Usu_ingresa = Convert.ToString(vReader["Usu_ingresa"]); }
			if (!Convert.IsDBNull(vReader["Fec_ingreso"])) { this.Fec_ingreso = Convert.ToDateTime(vReader["Fec_ingreso"]); }
			if (!Convert.IsDBNull(vReader["Ip_modifica"])) { this.Ip_modifica = Convert.ToString(vReader["Ip_modifica"]); }
			if (!Convert.IsDBNull(vReader["Id_usuario_modifica"])) { this.Id_usuario_modifica = Convert.ToDecimal(vReader["Id_usuario_modifica"]); }
			if (!Convert.IsDBNull(vReader["Usu_modifica"])) { this.Usu_modifica = Convert.ToString(vReader["Usu_modifica"]); }
			if (!Convert.IsDBNull(vReader["Fec_modifica"])) { this.Fec_modifica = Convert.ToDateTime(vReader["Fec_modifica"]); }
			if (!Convert.IsDBNull(vReader["Flg_estado"])) { this.Flg_estado = Convert.ToString(vReader["Flg_estado"]); }
        }

        #endregion

        #region Propiedades Adicionales
        [DataMember]
        public string cod_usuario { get; set; }  
        
        #endregion

        #region Lista de Entidades

        
        #endregion
    }
}