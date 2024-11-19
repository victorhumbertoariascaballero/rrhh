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
    public partial class T_genm_usuario
    {
        #region generales

        public T_genm_usuario()
        {

        }

        public T_genm_usuario(IDataReader vReader)
        {

            this.Id_usuario = Convert.ToInt64(vReader["Id_usuario"]);
            this.Cod_usuario = Convert.ToString(vReader["Cod_usuario"]);
            //this.Clave = Convert.ToString(vReader["Clave"]);
            if (!Convert.IsDBNull(vReader["Flg_tienehorario"]))
            {
                this.Flg_tienehorario = Convert.ToString(vReader["Flg_tienehorario"]) == "1" ? true : false;
            }

            if (!Convert.IsDBNull(vReader["Flg_tieneentidad"]))
            {
                this.Flg_tieneentidad = Convert.ToString(vReader["Flg_tieneentidad"]) == "1" ? true : false;
            }

            if (!Convert.IsDBNull(vReader["Flg_usuariointegrado"]))
            {
                this.Flg_usuariointegrado = Convert.ToString(vReader["Flg_usuariointegrado"]) == "1" ? true : false;
            }

            if (!Convert.IsDBNull(vReader["Id_sobreactivo"]))
            {
                this.Id_sobreactivo = Convert.ToInt64(vReader["Id_sobreactivo"]);
            }

            if (!Convert.IsDBNull(vReader["Flg_eliminado"]))
            {
                this.Flg_eliminado = Convert.ToString(vReader["Flg_eliminado"]) == "1" ? true : false;
            }

            if (!Convert.IsDBNull(vReader["Ip_ingreso"]))
            {
                this.Ip_ingreso = Convert.ToString(vReader["Ip_ingreso"]);
            }

            if (!Convert.IsDBNull(vReader["Id_usuario_ingresa"]))
            {
                this.Id_usuario_ingresa = Convert.ToInt64(vReader["Id_usuario_ingresa"]);
            }

            if (!Convert.IsDBNull(vReader["Usu_ingresa"]))
            {
                this.Usu_ingresa = Convert.ToString(vReader["Usu_ingresa"]);
            }

            if (!Convert.IsDBNull(vReader["Fec_ingreso"]))
            {
                this.Fec_ingreso = Convert.ToDateTime(vReader["Fec_ingreso"]);
            }

            if (!Convert.IsDBNull(vReader["Ip_modifica"]))
            {
                this.Ip_modifica = Convert.ToString(vReader["Ip_modifica"]);
            }

            if (!Convert.IsDBNull(vReader["Id_usuario_modifica"]))
            {
                this.Id_usuario_modifica = Convert.ToInt64(vReader["Id_usuario_modifica"]);
            }

            if (!Convert.IsDBNull(vReader["Usu_modifica"]))
            {
                this.Usu_modifica = Convert.ToString(vReader["Usu_modifica"]);
            }

            if (!Convert.IsDBNull(vReader["Fec_modifica"]))
            {
                this.Fec_modifica = Convert.ToDateTime(vReader["Fec_modifica"]);
            }
            if (!Convert.IsDBNull(vReader["Flg_activo"]))
            {
                this.Flg_activo = Convert.ToString(vReader["Flg_activo"]) == "1" ? true : false;
            }
            if (!Convert.IsDBNull(vReader["Flg_reset"]))
            {
                this.Flg_reset = Convert.ToString(vReader["Flg_reset"]) == "1" ? true : false;
            }

            
        }

        #endregion

        #region Propiedades Adicionales

        [DataMember]
        public string Clave_texto { get; set; }
        [DataMember]
        public string Correo_electronico { get; set; }
        [DataMember]
        public string Nombre_apellido { get; set; }
        [DataMember]
        public bool Flg_interno { get; set; }
        [DataMember]
        public string Domicilio { get; set; }
        [DataMember]
        public long Id_Domicilio { get; set; }
        [DataMember]
        public string Ruc { get; set; }
        [DataMember]
        public string Tipo_documento { get; set; }
        [DataMember]
        public string Nro_documento { get; set; }
        #endregion

        #region Lista de Entidades

        [DataMember]
        public List<T_genm_opcion> Lista_opciones { get; set; }
        [DataMember]
        public List<T_genm_perfil> Lista_perfiles { get; set; }

        //public SENACE.Entidades.T_genm_persona T_genm_persona { get; set; }


        #endregion
    }
}