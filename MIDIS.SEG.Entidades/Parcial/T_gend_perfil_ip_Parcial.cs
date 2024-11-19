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
    public partial class T_gend_perfil_ip
    {
        #region generales

        public T_gend_perfil_ip() { }

        public T_gend_perfil_ip(IDataReader vReader)
        {
            this.Id_perfil_ip = Convert.ToInt64(vReader["Id_perfil_ip"]);
            if (!Convert.IsDBNull(vReader["Id_perfil"])) { this.Id_perfil = Convert.ToInt64(vReader["Id_perfil"]); }
            if (!Convert.IsDBNull(vReader["Item"])) { this.Item = Convert.ToInt64(vReader["Item"]); }
            if (!Convert.IsDBNull(vReader["Nro_ip"])) { this.Nro_ip = Convert.ToString(vReader["Nro_ip"]); }
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
        [DataMember]
        public int Id_aplicacion { get; set; }
        #endregion

        #region Lista de Entidades


        #endregion
    }
}