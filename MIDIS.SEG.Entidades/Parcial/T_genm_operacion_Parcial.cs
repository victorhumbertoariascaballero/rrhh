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
    public partial class T_genm_operacion
    {
        #region generales

        public T_genm_operacion() { }

        public T_genm_operacion(IDataReader vReader)
        {

            this.Id_operacion = Convert.ToInt64(vReader["Id_operacion"]);
            this.Id_opcion = Convert.ToInt64(vReader["Id_opcion"]);
            if (!Convert.IsDBNull(vReader["Codigo"])) { this.Codigo = Convert.ToString(vReader["Codigo"]); }
            this.Flg_eliminado = Convert.ToString(vReader["Flg_eliminado"]) == "1";
            this.Ip_ingreso = Convert.ToString(vReader["Ip_ingreso"]);
            this.Id_usuario_ingresa = Convert.ToInt64(vReader["Id_usuario_ingresa"]);
            this.Usu_ingresa = Convert.ToString(vReader["Usu_ingresa"]);
            this.Fec_ingreso = Convert.ToDateTime(vReader["Fec_ingreso"]);
            this.Ip_modifica = Convert.ToString(vReader["Ip_modifica"]);
            this.Id_usuario_modifica = Convert.ToInt64(vReader["Id_usuario_modifica"]);
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