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
    public partial class T_gend_perfil_horario
    {
        #region generales

        public T_gend_perfil_horario() { }

        public T_gend_perfil_horario(IDataReader vReader)
        {

            this.Id_perfil_horario = Convert.ToInt64(vReader["Id_perfil_horario"]);
            if (!Convert.IsDBNull(vReader["Id_perfil"])) { this.Id_perfil = Convert.ToInt64(vReader["Id_perfil"]); }
            if (!Convert.IsDBNull(vReader["Item"])) { this.Item = Convert.ToInt64(vReader["Item"]); }
            if (!Convert.IsDBNull(vReader["Nro_dia"])) { this.Nro_dia = Convert.ToInt64(vReader["Nro_dia"]); }
            this.Flg_eliminado = Convert.ToString(vReader["Flg_eliminado"]) == "1";
            this.Ip_ingreso = Convert.ToString(vReader["Ip_ingreso"]);
            this.Id_usuario_ingresa = Convert.ToInt64(vReader["Id_usuario_ingresa"]);
            this.Usu_ingresa = Convert.ToString(vReader["Usu_ingresa"]);
            this.Fec_ingreso = Convert.ToDateTime(vReader["Fec_ingreso"]);
            this.Ip_modifica = Convert.ToString(vReader["Ip_modifica"]);
            this.Id_usuario_modifica = Convert.ToInt64(vReader["Id_usuario_modifica"]);
            this.Usu_modifica = Convert.ToString(vReader["Usu_modifica"]);
            this.Fec_modifica = Convert.ToDateTime(vReader["Fec_modifica"]);
            if (!Convert.IsDBNull(vReader["Hora_inicio"])) { this.Hora_inicio = Convert.ToDateTime(vReader["Hora_inicio"]); }
            if (!Convert.IsDBNull(vReader["Hora_fin"])) { this.Hora_fin = Convert.ToDateTime(vReader["Hora_fin"]); }
        }

        #endregion

        #region Propiedades Adicionales
        [DataMember]
        public string Hora_inicio_texto { get; set; }
        [DataMember]
        public string Hora_fin_texto { get; set; }

        [DataMember]
        public int Id_aplicacion { get; set; }
        #endregion

        #region Lista de Entidades


        #endregion
    }
}