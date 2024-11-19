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
using MIDIS.ORI.Entidades.Core;
#endregion

namespace MIDIS.ORI.LogicaNegocio
{
    public class T_genm_empleado_LN : BaseLN
    {
        private readonly T_genm_empleado_ODA _empleado_Repositorio = new T_genm_empleado_ODA();

        #region Métodos

        public IEnumerable<Empleado_Registro> ListarEmpleados(Empleado_Request peticion)
        {
            return _empleado_Repositorio.ListarEmpleados(peticion);
        }
        public IEnumerable<Empleado_Registro> ListarEmpleadosMaestro(Empleado_Request peticion)
        {
            return _empleado_Repositorio.ListarEmpleadosMaestro(peticion);
        }
        public IEnumerable<EmpleadoCuenta_Registro> ListarCuentasEmpleado(Empleado_Request peticion)
        {
            return _empleado_Repositorio.ListarCuentasEmpleado(peticion);
        }
        public IEnumerable<EmpleadoOrden_Registro> ListarOrdenesEmpleado(Empleado_Request peticion)
        {
            return _empleado_Repositorio.ListarOrdenesEmpleado(peticion);
        }
        public IEnumerable<Empleado_Registro> ListarEmpleadosBoleta(Empleado_Registro peticion)
        {
            return _empleado_Repositorio.ListarEmpleadosBoleta(peticion);
        }
        public IEnumerable<Empleado_Registro> ListarEmpleadosValidacionBoleta(Empleado_Registro peticion)
        {
            return _empleado_Repositorio.ListarEmpleadosValidacionBoleta(peticion);
        }
        public IEnumerable<EmpleadoSisper_Registro> ListarEmpleadosSisper(Empleado_Registro peticion)
        {
            return _empleado_Repositorio.ListarEmpleadosSisper(peticion);
        }
        public IEnumerable<EmpleadoSisper_Registro> ListarEmpleadosSispla(Empleado_Registro peticion)
        {
            return _empleado_Repositorio.ListarEmpleadosSispla(peticion);
        }
        public IEnumerable<EmpleadoBoletaResumen_Registro> ListarResumenBoletas(Empleado_Registro peticion)
        {
            List<EmpleadoBoletaResumen_Registro> listaAux = new List<EmpleadoBoletaResumen_Registro>();
            List <EmpleadoBoletaResumen_Registro> lista = _empleado_Repositorio.ListarResumenBoletas(peticion).ToList();
            foreach (EmpleadoBoletaResumen_Registro obj in lista) {
                peticion.Anio = obj.Anio.ToString();
                peticion.Mes = obj.Mes.ToString().PadLeft(2, '0');
                peticion.Planilla = obj.IdPlanilla;
                peticion.TipoPlanilla = obj.IdTipoPlanilla;
                obj.TotalValidado = _empleado_Repositorio.TotalBoletasValidas(peticion);
                obj.TotalEnviado = _empleado_Repositorio.TotalBoletasEnviadas(peticion);
                if (obj.TotalValidado > 0)
                    listaAux.Add(obj);
            }

            return listaAux;
        }
        public Int32 TotalBoletasValidas(Empleado_Registro peticion)
        {
            return _empleado_Repositorio.TotalBoletasValidas(peticion);
        }
        public IEnumerable<EmpleadoConceptoSisper_Registro> ListarEmpleadoConceptoSisper(Empleado_Registro peticion)
        {
            return _empleado_Repositorio.ListarEmpleadoConceptoSisper(peticion);
        }
        public IEnumerable<EmpleadoConceptoSisper_Registro> ListarEmpleadoConceptoSispla(Empleado_Registro peticion)
        {
            return _empleado_Repositorio.ListarEmpleadoConceptoSispla(peticion);
        }
        public String Validar(BoletaCarga_Registro registro)
        {
            return _empleado_Repositorio.Validar(registro);
        }
        public Int32 Insertar(BoletaCarga_Registro registro)
        {
            return _empleado_Repositorio.Insertar(registro);
        }
        public Int32 InsertarBoletaValida(BoletaCarga_Registro registro)
        {
            return _empleado_Repositorio.InsertarBoletaValida(registro);
        }
        public Int32 InsertarNotificacion(BoletaCarga_Registro registro)
        {
            return _empleado_Repositorio.InsertarNotificacion(registro);
        }
        public Int32 ValidarExisteBoletaValida(BoletaCarga_Registro registro)
        {
            return _empleado_Repositorio.ValidarExisteBoletaValida(registro);
        }
        public Int32 ActualizarRecepcion(Empleado_Registro registro)
        {
            return _empleado_Repositorio.ActualizarRecepcion(registro);
        }
        public Int32 ActualizarRecepcionNotificacion(Empleado_Registro registro)
        {
            return _empleado_Repositorio.ActualizarRecepcionNotificacion(registro);
        }
        public Empleado_Registro RegistrarEmpleado(Empleado_Registro registro)
        {
            return _empleado_Repositorio.RegistrarEmpleado(registro);
        }
        public Empleado_Registro ActualizarEmpleado(Empleado_Registro registro)
        {
            return _empleado_Repositorio.ActualizarEmpleado(registro);
        }
        public Int32 RegistrarCese(Empleado_Registro registro)
        {
            return _empleado_Repositorio.RegistrarCese(registro);
        }
        public String ObtenerIndicadores()
        {
            return _empleado_Repositorio.ObtenerIndicadores();
        }
        public Int32 ActualizarEmpleadoContacto(Empleado_Registro registro)
        {
            return _empleado_Repositorio.ActualizarEmpleadoContacto(registro);
        }
        public Empleado_Registro ObtenerParaEditar(Empleado_Request peticion)
        {
            Empleado_Registro objEmpleado = _empleado_Repositorio.ObtenerParaEditar(peticion);

            //KMM: POR AHORA NO PINTAMOS EL CONTRATO EN CASO QUE EXISTA
            //if (objEmpleado != null) {
            //    objEmpleado.TieneContrato = 0;
            //    List<EmpleadoContrato_Registro> lista = new T_genm_contrato_ODA().ListarContratos(new Contrato_Request() { NroDocumento = objEmpleado.NroDocumento, Nombre = "", Estado = 1 }).ToList();
            //    if (lista != null) {
            //        if (lista.Count > 0) {
            //            objEmpleado.TieneContrato = 1;
            //            objEmpleado.IdContrato = lista[0].IdContrato;
            //            objEmpleado.NroContrato = (String.IsNullOrEmpty(objEmpleado.NroContrato) ? lista[0].NombreContrato : objEmpleado.NroContrato);
            //            objEmpleado.NroAIRHSP = (String.IsNullOrEmpty(objEmpleado.NroAIRHSP) ? lista[0].NroAIRHSP : objEmpleado.NroAIRHSP);
            //            objEmpleado.Meta = (String.IsNullOrEmpty(objEmpleado.Meta) ? lista[0].Meta : objEmpleado.Meta);
            //            objEmpleado.DocIngreso = (String.IsNullOrEmpty(objEmpleado.DocIngreso) ? lista[0].NombreProceso : objEmpleado.DocIngreso);
            //            objEmpleado.Remuneracion = (objEmpleado.Remuneracion == Decimal.Zero ? lista[0].Remuneracion : objEmpleado.Remuneracion);
            //        }
            //    }
            //}
             

            return objEmpleado;
        }

        public Int32 RegistrarCuentaEmpleado(EmpleadoCuenta_Registro registro)
        {
            return _empleado_Repositorio.RegistrarCuentaEmpleado(registro);
        }
        public Int32 RegistrarOrdenEmpleado(EmpleadoOrden_Registro registro)
        {
            return _empleado_Repositorio.RegistrarOrdenEmpleado(registro);
        }
        public Int32 RegistrarSolicitudBoleta(EmpleadoBoletaSolicitud_Registro registro)
        {
            return _empleado_Repositorio.RegistrarSolicitudBoleta(registro);
        }
        public String ValidarSolicitudBoleta(EmpleadoBoletaSolicitud_Registro registro)
        {
            return _empleado_Repositorio.ValidarSolicitudBoleta(registro);
        }
        public Int32 ActualizarCuentaEmpleado(EmpleadoCuenta_Registro registro)
        {
            return _empleado_Repositorio.ActualizarCuentaEmpleado(registro);
        }
        public Int32 ActualizarOrdenEmpleado(EmpleadoOrden_Registro registro)
        {
            return _empleado_Repositorio.ActualizarOrdenEmpleado(registro);
        }
        public Int32 EliminarCuentaEmpleado(EmpleadoCuenta_Registro registro)
        {
            return _empleado_Repositorio.EliminarCuentaEmpleado(registro);
        }
        public Int32 EliminarHistorialEmpleado(EmpleadoCuenta_Registro registro)
        {
            return _empleado_Repositorio.EliminarHistorialEmpleado(registro);
        }
        public Int32 EliminarOrdenEmpleado(EmpleadoOrden_Registro registro)
        {
            return _empleado_Repositorio.EliminarOrdenEmpleado(registro);
        }
        public List<DescuentoJudicialBeneficiario_Registro> ObtenerBeneficiarios(Empleado_Request peticion)
        {
            return _empleado_Repositorio.ObtenerBeneficiarios(peticion);
        }
        public IEnumerable<EmpleadoEncargatura_Registro> ListarEncargaturasEmpleado(Empleado_Request peticion)
        {
            return _empleado_Repositorio.ListarEncargaturasEmpleado(peticion);
        }
        public IEnumerable<EmpleadoFamiliar_Registro> ListarFamiliaresEmpleado(Empleado_Request peticion)
        {
            return _empleado_Repositorio.ListarFamiliaresEmpleado(peticion);
        }
        public IEnumerable<Empleado_Registro> ListarDesplazamientoEmpleado(Empleado_Request peticion)
        {
            return _empleado_Repositorio.ListarDesplazamientoEmpleado(peticion);
        }
        public String RegistrarEncargaturasEmpleado(EmpleadoEncargatura_Registro registro)
        {
            return _empleado_Repositorio.RegistrarEncargaturasEmpleado(registro);
        }
        public String ActualizarEncargaturasEmpleado(EmpleadoEncargatura_Registro registro)
        {
            return _empleado_Repositorio.ActualizarEncargaturasEmpleado(registro);
        }
        #endregion

        public Int32 RegistrarFamiliarEmpleado(EmpleadoFamiliar_Registro registro)
        {
            return _empleado_Repositorio.RegistrarFamiliarEmpleado(registro);
        }
        public Int32 ActualizarFamiliarEmpleado(EmpleadoFamiliar_Registro registro)
        {
            return _empleado_Repositorio.ActualizarFamiliarEmpleado(registro);
        }
        public Int32 EliminarFamiliarEmpleado(EmpleadoFamiliar_Registro registro)
        {
            return _empleado_Repositorio.EliminarFamiliarEmpleado(registro);
        }
        // PARA PRUEBAS DE ENVIO DE CERTIFICADO
        public IEnumerable<Empleado_Registro> ListarParticipantesCertificadoPrueba(Empleado_Request peticion)
        {
            return _empleado_Repositorio.ListarParticipantesCertificadoPrueba(peticion);
        }
        public IEnumerable<Empleado_Registro> ListarEmpleadoAltas(EmpleadoReporte_Request peticion)
        {
            return _empleado_Repositorio.ListarEmpleadoAltas(peticion);
        }
        public IEnumerable<Empleado_Registro> ListarEmpleadoBajas(EmpleadoReporte_Request peticion)
        {
            return _empleado_Repositorio.ListarEmpleadoAltas(peticion);
        }

        #region Estudios
        public IEnumerable<EmpleadoEstudio_Registro> ListarEmpleadoEstudios(Empleado_Request peticion)
        {
            return _empleado_Repositorio.ListarEmpleadoEstudios(peticion);
        }
        public Int32 RegistrarEstudioEmpleado(EmpleadoEstudio_Registro registro)
        {
            return _empleado_Repositorio.RegistrarEmpleadoEstudio(registro);
        }
        public Int32 ActualizarEstudioEmpleado(EmpleadoEstudio_Registro registro)
        {
            return _empleado_Repositorio.ActualizarEmpleadoEstudio(registro);
        }
        public Int32 EliminarEstudioEmpleado(EmpleadoEstudio_Registro registro)
        {
            return _empleado_Repositorio.EliminarEmpleadoEstudio(registro);
        }
        #endregion
    }
}
