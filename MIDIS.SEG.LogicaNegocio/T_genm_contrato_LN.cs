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
    public class T_genm_contrato_LN : BaseLN
    {
        private readonly T_genm_contrato_ODA _contrato_Repositorio = new T_genm_contrato_ODA();

        #region Métodos

        public IEnumerable<EmpleadoContrato_Registro> ListarContratos(Contrato_Request peticion)
        {
            return _contrato_Repositorio.ListarContratos(peticion);
        }
        
        public Int32 RegistrarContrato(EmpleadoContrato_Registro registro, PostulanteInformacion_Registro postulante)
        {
            return _contrato_Repositorio.RegistrarContrato(registro, postulante);
        }
        public Int32 RegistrarContratoArchivo(EmpleadoContrato_Registro registro)
        {
            return _contrato_Repositorio.RegistrarContratoArchivo(registro);
        }
        public Int32 ActualizarContratoNominaTrabajador(EmpleadoContrato_Registro registro)
        {
            return _contrato_Repositorio.ActualizarContratoNominaTrabajador(registro);
        }
        public EmpleadoContrato_Registro ObtenerParaEditar(Contrato_Request peticion)
        {
            return _contrato_Repositorio.ListarContratos(peticion).FirstOrDefault();
        }
        //public Int32 ActualizarEmpleado(Empleado_Registro registro)
        //{
        //    return _empleado_Repositorio.ActualizarEmpleado(registro);
        //}
        //public EmpleadoContrato_Registro ObtenerParaEditar(Empleado_Request peticion)
        //{
        //    return _empleado_Repositorio.ObtenerParaEditar(peticion);
        //}

        
        //public Int32 ActualizarOrdenEmpleado(EmpleadoOrden_Registro registro)
        //{
        //    return _empleado_Repositorio.ActualizarOrdenEmpleado(registro);
        //}
        //public Int32 EliminarCuentaEmpleado(EmpleadoCuenta_Registro registro)
        //{
        //    return _empleado_Repositorio.EliminarCuentaEmpleado(registro);
        //}
        //public Int32 EliminarOrdenEmpleado(EmpleadoOrden_Registro registro)
        //{
        //    return _empleado_Repositorio.EliminarOrdenEmpleado(registro);
        //}
        #endregion
               
    }
}
