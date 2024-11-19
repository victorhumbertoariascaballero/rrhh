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
    public partial class T_genm_grupo_aplicacion
    {
        #region generales

        public T_genm_grupo_aplicacion() { }

        public T_genm_grupo_aplicacion(IDataReader vReader)
        {

            this.Id_grupo = Convert.ToDecimal(vReader["Id_grupo"]);
            this.Nombre_grupo = Convert.ToString(vReader["Nombre_grupo"]);
            this.Id_sector = Convert.ToDecimal(vReader["Id_sector"]);
            this.Flg_eliminado = Convert.ToString(vReader["Flg_eliminado"]);
            if (!Convert.IsDBNull(vReader["Nro_orden"])) { this.Nro_orden = Convert.ToDecimal(vReader["Nro_orden"]); }
            this.Ip_ingreso = Convert.ToString(vReader["Ip_ingreso"]);
            this.Id_usuario_ingresa = Convert.ToDecimal(vReader["Id_usuario_ingresa"]);
            this.Usu_ingresa = Convert.ToString(vReader["Usu_ingresa"]);
            this.Fec_ingreso = Convert.ToDateTime(vReader["Fec_ingreso"]);
            this.Ip_modifica = Convert.ToString(vReader["Ip_modifica"]);
            this.Id_usuario_modifica = Convert.ToDecimal(vReader["Id_usuario_modifica"]);
            this.Usu_modifica = Convert.ToString(vReader["Usu_modifica"]);
            this.Fec_modifica = Convert.ToDateTime(vReader["Fec_modifica"]);
        }

        #endregion

        #region Propiedades Adicionales


        #endregion

        #region Lista de Entidades


        #endregion
    }
}