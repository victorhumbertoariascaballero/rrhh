#region Espacio de Nombres
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Data;
#endregion

namespace MIDIS.ORI.Entidades
{
    public partial class T_genm_error
    {
        #region generales

        public T_genm_error() { }

        public T_genm_error(IDataReader vReader)
        {

            this.Id_error = Convert.ToDecimal(vReader["Id_error"]);
            this.Id_aplicacion = Convert.ToDecimal(vReader["Id_aplicacion"]);
            this.Desc_error = Convert.ToString(vReader["Desc_error"]);
            this.Flg_eliminado = Convert.ToString(vReader["Flg_eliminado"]);
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
