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
    public partial class T_genm_opcion 
    {
        #region generales

        public T_genm_opcion() { }

        public T_genm_opcion(IDataReader vReader)
        {
            
			this.Id_opcion = Convert.ToDecimal(vReader["Id_opcion"]);  
			this.Id_aplicacion = Convert.ToDecimal(vReader["Id_aplicacion"]);  
			this.Nombre_opcion = Convert.ToString(vReader["Nombre_opcion"]);  
			this.Url = Convert.ToString(vReader["Url"]);  
			this.Titulo_opcion = Convert.ToString(vReader["Titulo_opcion"]);  
			this.Direccion_opcion = Convert.ToString(vReader["Direccion_opcion"]);  
			this.Id_grupo_opcion = Convert.ToDecimal(vReader["Id_grupo_opcion"]);  
			this.Flg_eliminado = Convert.ToString(vReader["Flg_eliminado"]);  
			this.Ip_ingreso = Convert.ToString(vReader["Ip_ingreso"]);  
			this.Id_usuario_ingresa = Convert.ToDecimal(vReader["Id_usuario_ingresa"]);  
			this.Usu_ingresa = Convert.ToString(vReader["Usu_ingresa"]);  
			this.Fec_ingreso = Convert.ToDateTime(vReader["Fec_ingreso"]);  
			this.Ip_modifica = Convert.ToString(vReader["Ip_modifica"]);  
			this.Id_usuario_modifica = Convert.ToDecimal(vReader["Id_usuario_modifica"]);  
			this.Usu_modifica = Convert.ToString(vReader["Usu_modifica"]);  
			this.Fec_modifica = Convert.ToDateTime(vReader["Fec_modifica"]);  
			if (!Convert.IsDBNull(vReader["Nroorden"])) { this.Nroorden = Convert.ToDecimal(vReader["Nroorden"]); }
			this.Flg_estado = Convert.ToString(vReader["Flg_estado"]);  
        }

        #endregion

        #region Propiedades Adicionales

        public string Area { get; set; }
        public string Controladora { get; set; }
        public string Accion { get; set; }
        
        #endregion

        #region Lista de Entidades

        
        #endregion

        public override string ToString()
        {
            return this.Controladora + " - " + this.Accion + " - " + this.Nombre_opcion;
            //return base.ToString();
        }
    }
}