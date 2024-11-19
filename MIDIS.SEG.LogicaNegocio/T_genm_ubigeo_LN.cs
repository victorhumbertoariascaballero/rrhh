/*----------------------------------------------------------------------------------------
ARCHIVO CLASE   : T_genm_aplicacionLN

Objetivo: Clase referida a los métodos de Lógica de Negocio de la clase T_Sold_Solicitud_Archivo
Autor: Miguel Angel Salvador Paucar (MASP)
Fecha Creacion: 2015-09-03
Métodos: 
        Insertar_T_genm_aplicacion
        Actualizar_T_genm_aplicacion
        Anular_T_genm_aplicacion_PorCodigo
        Listar_T_genm_aplicacion()
        Recuperar_T_genm_aplicacion_PorCodigo
        ListarPaginado_T_genm_aplicacion

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
#endregion

namespace MIDIS.ORI.LogicaNegocio
{
    public class T_genm_ubigeo_LN : BaseLN
    {
        private readonly T_genm_ubigeo_ODA _ubigeo_Repositorio = new T_genm_ubigeo_ODA();

        public IEnumerable<Ubigeo_Response> Listar(Ubigeo_Request peticion)
        {
            return _ubigeo_Repositorio.Listar(peticion);
        }

        public IEnumerable<string> Validar(Ubigeo_Registro registro)
        {
            return _ubigeo_Repositorio.Validar(registro);
        }

        public Int32 Insertar(Ubigeo_Registro registro)
        {
            return _ubigeo_Repositorio.Insertar(registro);
        }

        public void Actualizar(Ubigeo_Registro registro)
        {
            _ubigeo_Repositorio.Actualizar(registro);
        }

        public Ubigeo_Registro Obtener(Ubigeo_Registro registro)
        {
            return _ubigeo_Repositorio.Obtener(registro);
        }

        public void Eliminar(Ubigeo_Registro registro)
        {
            _ubigeo_Repositorio.Eliminar(registro);
        }

        public void ActualizarEstaActivo(Ubigeo_Registro registro)
        {
            _ubigeo_Repositorio.ActualizarEstaActivo(registro);
        }

        public void ActualizarEstaEliminado(Ubigeo_Registro registro)
        {
            _ubigeo_Repositorio.ActualizarEstaEliminado(registro);
        }
    }
}
