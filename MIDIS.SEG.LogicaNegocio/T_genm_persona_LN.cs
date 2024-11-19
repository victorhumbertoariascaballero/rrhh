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
    public class T_genm_persona_LN : BaseLN
    {
        private readonly T_genm_persona_ODA _persona_Repositorio = new T_genm_persona_ODA();

        public IEnumerable<Persona_Response> Listar(Persona_Request peticion)
        {
            return _persona_Repositorio.Listar(peticion);
        }

        public IEnumerable<string> Validar(Persona_Registro registro)
        {
            return _persona_Repositorio.Validar(registro);
        }

        public Int32 Insertar(Persona_Registro registro)
        {
            return _persona_Repositorio.Insertar(registro);
        }

        public void Actualizar(Persona_Registro registro)
        {
            _persona_Repositorio.Actualizar(registro);
        }

        public Persona_Registro Obtener(Persona_Registro registro)
        {
            return _persona_Repositorio.Obtener(registro);
        }

        public void Eliminar(Persona_Registro registro)
        {
            _persona_Repositorio.Eliminar(registro);
        }

        public void ActualizarEstaActivo(Persona_Registro registro)
        {
            _persona_Repositorio.ActualizarEstaActivo(registro);
        }

        public void ActualizarEstaEliminado(Persona_Registro registro)
        {
            _persona_Repositorio.ActualizarEstaEliminado(registro);
        }
        public IEnumerable<Banco_Registro> ListarBancos(Banco_Request termino)
        {
            return _persona_Repositorio.ListarBancos(termino);
        }
        public IEnumerable<Perfil_Nivel> ListarPerfilNivel()
        {
            return _persona_Repositorio.ListarPerfilNivel();
        }
    }
}
