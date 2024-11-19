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
    public partial class T_gend_empresa_aplicacion
    {
        #region generales

        public T_gend_empresa_aplicacion() { }

        public T_gend_empresa_aplicacion(IDataReader vReader)
        {

            this.Id_empresa_aplicacion = Convert.ToInt64(vReader["Id_empresa_aplicacion"]);
            if (!Convert.IsDBNull(vReader["Id_empresa"])) { this.Id_empresa = Convert.ToInt64(vReader["Id_empresa"]); }
            if (!Convert.IsDBNull(vReader["Id_aplicacion"])) { this.Id_aplicacion = Convert.ToInt64(vReader["Id_aplicacion"]); }
            if (!Convert.IsDBNull(vReader["Id_situacion"])) { this.Id_situacion = Convert.ToInt64(vReader["Id_situacion"]); }
            if (!Convert.IsDBNull(vReader["Flg_eliminado"])) { this.Flg_eliminado = Convert.ToString(vReader["Flg_eliminado"]) == "1"; }
            if (!Convert.IsDBNull(vReader["Id_sesion_ingreso"])) { this.Id_sesion_ingreso = Convert.ToInt64(vReader["Id_sesion_ingreso"]); }
            if (!Convert.IsDBNull(vReader["Fec_ingreso"])) { this.Fec_ingreso = Convert.ToDateTime(vReader["Fec_ingreso"]); }
            if (!Convert.IsDBNull(vReader["Id_sesion_modifica"])) { this.Id_sesion_modifica = Convert.ToInt64(vReader["Id_sesion_modifica"]); }
            if (!Convert.IsDBNull(vReader["Fec_modifica"])) { this.Fec_modifica = Convert.ToDateTime(vReader["Fec_modifica"]); }
        }

        #endregion

        #region Propiedades Adicionales
        [DataMember]
        public string NroRuc { get; set; }
        [DataMember]
        public string RazonSocial { get; set; }
        [DataMember]
        public string NombreTipoDocumento { get; set; }
        [DataMember]
        public string NroDocumento { get; set; }
        [DataMember]
        public string NombreCompleto { get; set; }
        [DataMember]
        public string NombreAplicacion { get; set; }

        #endregion

        #region Lista de Entidades


        #endregion
    }
}