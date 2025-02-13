/*----------------------------------------------------------------------------------------
ARCHIVO CLASE   : T_genm_usuarioLN

Objetivo: Clase referida a los métodos de Lógica de Negocio de la clase T_Sold_Solicitud_Archivo
Autor: Miguel Angel Salvador Paucar (MASP)
Fecha Creacion: 2015-09-03
Métodos: 
        Insertar_T_genm_usuario
        Actualizar_T_genm_usuario
        Anular_T_genm_usuario_PorCodigo
        Listar_T_genm_usuario()
        Recuperar_T_genm_usuario_PorCodigo
        ListarPaginado_T_genm_usuario

----------------------------------------------------------------------------------------*/
#region Espacio de Nombres
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIDIS.ORI.Entidades;
using MIDIS.SEG.AccesoDatosSQL;
using MIDIS.Utiles;
using MIDIS.ORI.LogicaNegocio.Base;
using System.Configuration;
using MIDIS.Autenticacion;
#endregion

namespace MIDIS.ORI.LogicaNegocio
{
    public class T_genm_dependencia_LN : BaseLN
    {
        private readonly T_genm_dependencia_ODA _dependencia_Repositorio = new T_genm_dependencia_ODA();

        #region Métodos

        public IEnumerable<Dependencia_Registro> ListarDependencias(Dependencia_Request peticion)
        {
            return _dependencia_Repositorio.ListarDependencias(peticion);
        }
        public IEnumerable<Dependencia_Registro> ListarDependenciasAntiguo(Dependencia_Request peticion)
        {
            return _dependencia_Repositorio.ListarDependenciasAntiguo(peticion);
        }
        #endregion

    }
}
