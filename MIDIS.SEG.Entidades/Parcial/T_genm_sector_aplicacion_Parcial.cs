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
    public partial class T_genm_sector_aplicacion 
    {
        #region generales

        public T_genm_sector_aplicacion() { }

        public T_genm_sector_aplicacion(IDataReader vReader)
        {
            
			this.Id_sector = Convert.ToDecimal(vReader["Id_sector"]);  
			this.Nombre_sector = Convert.ToString(vReader["Nombre_sector"]);  
			this.Nro_orden = Convert.ToDecimal(vReader["Nro_orden"]);  
			this.Flg_reqautenticacion = Convert.ToString(vReader["Flg_reqautenticacion"]);  
			this.Flg_eliminado = Convert.ToString(vReader["Flg_eliminado"]);  
			this.Ip_ingreso = Convert.ToString(vReader["Ip_ingreso"]);  
			this.Id_usuario_ingresa = Convert.ToDecimal(vReader["Id_usuario_ingresa"]);  
			this.Usu_ingresa = Convert.ToString(vReader["Usu_ingresa"]);  
			this.Fec_ingreso = Convert.ToDateTime(vReader["Fec_ingreso"]);  
			this.Ip_modifica = Convert.ToString(vReader["Ip_modifica"]);  
			this.Id_usuario_modifica = Convert.ToDecimal(vReader["Id_usuario_modifica"]);  
			this.Usu_modifica = Convert.ToString(vReader["Usu_modifica"]);  
			this.Fec_modifica = Convert.ToDateTime(vReader["Fec_modifica"]);
            this.Flg_ReqAutentificacion = Convert.ToString(vReader["Flg_reqautenticacion"]).Equals("1");
        }

        #endregion

        #region Propiedades Adicionales
        public Boolean Flg_ReqAutentificacion { get; set; } 
        
        #endregion

        #region Lista de Entidades

        
        #endregion
    }
}