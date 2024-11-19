using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIDIS.ORI.Entidades;
using MIDIS.SEG.AccesoDatosSQL;
using MIDIS.Utiles;
using MIDIS.ORI.LogicaNegocio.Base;

namespace MIDIS.SEG.LogicaNegocio
{
    public class T_genm_planillas_LN
    {
        private readonly T_genm_planillas_ODA _planillas_Repositorio = new T_genm_planillas_ODA();

        #region Planilla CAS General
        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralCAS(int idCodFuenteFinanciamiento, string sMetas, int iMes, int iAnio)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaGeneralCAS(idCodFuenteFinanciamiento, sMetas, iMes, iAnio);
        }
        public IEnumerable<PlanillaCalculadaIngresos_Request> ListarPlanillaCalculadaIngresosCAS(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaIngresosCAS(iCodTrabajador, iMes, iAnio, iCodTipoPlanilla);
        }
        public IEnumerable<PlanillaCalculadaDescuentos_Request> ListarPlanillaCalculadaDescuentosCAS(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaDescuentosCAS(iCodTrabajador, iMes, iAnio, iCodTipoPlanilla);
        }

        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralCompletaCAS(int idCodFuenteFinanciamiento, string sMetas, int iMes, int iAnio)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaGeneralCompletaCAS(idCodFuenteFinanciamiento, sMetas, iMes, iAnio);
        }

        public bool GenerarPlanillaCAS(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla)
        {
            return _planillas_Repositorio.GenerarPlanillaCAS(iMes, iAnio, iCodPlanilla, iCodTipoPlanilla);
        }
        #endregion Planilla CAS General

        #region Planilla FED General
        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralFED(int idCodFuenteFinanciamiento, string sMetas, int iMes, int iAnio)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaGeneralFED(idCodFuenteFinanciamiento, sMetas, iMes, iAnio);
        }
        public IEnumerable<PlanillaCalculadaIngresos_Request> ListarPlanillaCalculadaIngresosFED(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaIngresosFED(iCodTrabajador, iMes, iAnio, iCodTipoPlanilla);
        }
        public IEnumerable<PlanillaCalculadaDescuentos_Request> ListarPlanillaCalculadaDescuentosFED(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaDescuentosFED(iCodTrabajador, iMes, iAnio, iCodTipoPlanilla);
        }

        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralCompletaFED(int idCodFuenteFinanciamiento, string sMetas, int iMes, int iAnio)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaGeneralCompletaFED(idCodFuenteFinanciamiento, sMetas, iMes, iAnio);
        }

        public bool GenerarPlanillaFED(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla)
        {
            return _planillas_Repositorio.GenerarPlanillaFED(iMes, iAnio, iCodPlanilla, iCodTipoPlanilla);
        }
        #endregion Planilla FED General

        /*------Planilla CAS Adicional------*/
        #region Planilla CAS Adicional
        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralCASAdicional(int idCodFuenteFinanciamiento, string sMetas, int iMes, int iAnio, int iCodPlanilla, int iCodDetPlanilla)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaGeneralCASAdicional(idCodFuenteFinanciamiento, sMetas, iMes, iAnio, iCodPlanilla, iCodDetPlanilla);
        }
        public IEnumerable<PlanillaCalculadaIngresos_Request> ListarPlanillaCalculadaIngresosCASAdicional(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla, int iCodPlanilla, int iCodDetPlanilla)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaIngresosCASAdicional(iCodTrabajador, iMes, iAnio, iCodTipoPlanilla, iCodPlanilla, iCodDetPlanilla);
        }
        public IEnumerable<PlanillaCalculadaDescuentos_Request> ListarPlanillaCalculadaDescuentosCASAdicional(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla, int iCodPlanilla, int iCodDetPlanilla)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaDescuentosCASAdicional(iCodTrabajador, iMes, iAnio, iCodTipoPlanilla, iCodPlanilla, iCodDetPlanilla);
        }

        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralCompletaCASAdicional(int iMes, int iAnio, int iCodPlanilla, int iCodDetPlanilla)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaGeneralCompletaCASAdicional(iMes, iAnio, iCodPlanilla, iCodDetPlanilla);
        }

        public bool GenerarPlanillaCASAdicional(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, IEnumerable<String> strCodTrabajadores)
        {
            return _planillas_Repositorio.GenerarPlanillaCASAdicional(iMes, iAnio, iCodPlanilla, iCodTipoPlanilla, iCodDetPlanilla, strCodTrabajadores);
        }
        #endregion Planilla CAS Adicional

        #region Planilla Funcionarios
        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralFUNC(int iMes, int iAnio)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaGeneralFUNC(iMes, iAnio);
        }
        public IEnumerable<PlanillaCalculadaIngresos_Request> ListarPlanillaCalculadaIngresosFUNC(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaIngresosFUNC(iCodTrabajador, iMes, iAnio, iCodTipoPlanilla);
        }
        public IEnumerable<PlanillaCalculadaDescuentos_Request> ListarPlanillaCalculadaDescuentosFUNC(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaDescuentosFUNC(iCodTrabajador, iMes, iAnio, iCodTipoPlanilla);
        }

        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralCompletaFUNC(int iMes, int iAnio)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaGeneralCompletaFUNC(iMes, iAnio);
        }

        public bool GenerarPlanillaFUNC(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla)
        {
            return _planillas_Repositorio.GenerarPlanillaFUNC(iMes, iAnio, iCodPlanilla, iCodTipoPlanilla);
        }
        #endregion Funcionarios

        public string ConsultarEjecucionPlanilla(int iCodPlanilla, int iMes, int iAnio, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            return _planillas_Repositorio.ConsultarEjecucionPlanilla(iCodPlanilla, iMes, iAnio, iCodTipoPlanilla, iCodDetPlanilla);
        }

        public int CerrarFase(int iCodPlanilla, int iMes, int iAnio, int iCodTipoPlanilla, int iCodDetPlanilla, int iCodFase, string sUsuario)
        {
            return _planillas_Repositorio.CerrarFase(iCodPlanilla, iMes, iAnio, iCodTipoPlanilla, iCodDetPlanilla, iCodFase, sUsuario);
        }

        public bool ConsultarGeneracionPlanilla(int iCodPlanilla, int iMes, int iAnio, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            return _planillas_Repositorio.ConsultarGeneracionPlanilla(iCodPlanilla, iMes, iAnio, iCodTipoPlanilla, iCodDetPlanilla);
        }

        public IEnumerable<Empleado_Registro> ListarEmpleadosPlanilla(string strCodTipoCondicionTrabajador)
        {
            return _planillas_Repositorio.ListarEmpleadosPlanilla(strCodTipoCondicionTrabajador);
        }

        public IEnumerable<TipoPlanillaAdicional_Response> ListarTipoPlanillaCASAdicional()
        {
            return _planillas_Repositorio.ListarTipoPlanillaCASAdicional();
        }

        public IEnumerable<Empleado_Registro> ListarTrabajadoresPlanillaCASAdicional(int iCodPlanilla, int iMes, int iAnio, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            return _planillas_Repositorio.ListarTrabajadoresPlanillaCASAdicional(iCodPlanilla, iMes, iAnio, iCodTipoPlanilla, iCodDetPlanilla);
        }

        #region Planilla SECIGRISTAS General
        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralSECIGRISTAS(int idCodFuenteFinanciamiento, string sMetas, int iMes, int iAnio)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaGeneralSECIGRISTAS(idCodFuenteFinanciamiento, sMetas, iMes, iAnio);
        }
        public IEnumerable<PlanillaCalculadaIngresos_Request> ListarPlanillaCalculadaIngresosSECIGRISTAS(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaIngresosSECIGRISTAS(iCodTrabajador, iMes, iAnio, iCodTipoPlanilla);
        }
        public IEnumerable<PlanillaCalculadaDescuentos_Request> ListarPlanillaCalculadaDescuentosSECIGRISTAS(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaDescuentosSECIGRISTAS(iCodTrabajador, iMes, iAnio, iCodTipoPlanilla);
        }

        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralCompletaSECIGRISTAS(int idCodFuenteFinanciamiento, string sMetas, int iMes, int iAnio)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaGeneralCompletaSECIGRISTAS(idCodFuenteFinanciamiento, sMetas, iMes, iAnio);
        }

        public bool GenerarPlanillaSECIGRISTAS(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla)
        {
            return _planillas_Repositorio.GenerarPlanillaSECIGRISTAS(iMes, iAnio, iCodPlanilla, iCodTipoPlanilla);
        }
        #endregion Planilla SECIGRISTAS General

        #region Planilla PRACTICANTES General
        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralPRACTICANTES(int idCodFuenteFinanciamiento, string sMetas, int iMes, int iAnio)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaGeneralPRACTICANTES(idCodFuenteFinanciamiento, sMetas, iMes, iAnio);
        }
        public IEnumerable<PlanillaCalculadaIngresos_Request> ListarPlanillaCalculadaIngresosPRACTICANTES(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaIngresosPRACTICANTES(iCodTrabajador, iMes, iAnio, iCodTipoPlanilla);
        }
        public IEnumerable<PlanillaCalculadaDescuentos_Request> ListarPlanillaCalculadaDescuentosPRACTICANTES(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaDescuentosPRACTICANTES(iCodTrabajador, iMes, iAnio, iCodTipoPlanilla);
        }

        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralCompletaPRACTICANTES(int idCodFuenteFinanciamiento, string sMetas, int iMes, int iAnio)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaGeneralCompletaSECIGRISTAS(idCodFuenteFinanciamiento, sMetas, iMes, iAnio);
        }

        public bool GenerarPlanillaPRACTICANTES(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla)
        {
            return _planillas_Repositorio.GenerarPlanillaPRACTICANTES(iMes, iAnio, iCodPlanilla, iCodTipoPlanilla);
        }
        #endregion Planilla PRACTICANTES General

        /*------Planilla Funcional Adicional------*/
        #region Planilla Funcional Adicional
        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralFUNCAdicional(int idCodFuenteFinanciamiento, string sMetas, int iMes, int iAnio, int iCodDetPlanilla)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaGeneralFUNCAdicional(idCodFuenteFinanciamiento, sMetas, iMes, iAnio, iCodDetPlanilla);
        }
        public IEnumerable<PlanillaCalculadaIngresos_Request> ListarPlanillaCalculadaIngresosFUNCAdicional(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaIngresosFUNCAdicional(iCodTrabajador, iMes, iAnio, iCodTipoPlanilla, iCodDetPlanilla);
        }
        public IEnumerable<PlanillaCalculadaDescuentos_Request> ListarPlanillaCalculadaDescuentosFUNCAdicional(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaDescuentosFUNCAdicional(iCodTrabajador, iMes, iAnio, iCodTipoPlanilla, iCodDetPlanilla);
        }

        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralCompletaFUNCAdicional(int iMes, int iAnio, int iCodDetPlanilla)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaGeneralCompletaFUNCAdicional(iMes, iAnio, iCodDetPlanilla);
        }

        public bool GenerarPlanillaFUNCAdicional(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, IEnumerable<String> strCodTrabajadores)
        {
            return _planillas_Repositorio.GenerarPlanillaFUNCAdicional(iMes, iAnio, iCodPlanilla, iCodTipoPlanilla, iCodDetPlanilla, strCodTrabajadores);
        }
        public IEnumerable<Empleado_Registro> ListarTrabajadoresPlanillaFUNCAdicional(int iCodPlanilla, int iMes, int iAnio, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            return _planillas_Repositorio.ListarTrabajadoresPlanillaFUNCAdicional(iCodPlanilla, iMes, iAnio, iCodTipoPlanilla, iCodDetPlanilla);
        }
        #endregion Planilla Funcional Adicional

        #region Planilla CAS Vacaciones Truncas
        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralCASVacTruncas(int idCodFuenteFinanciamiento, string sMetas, int iMes, int iAnio, int iCodDetPlanilla)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaGeneralCASVacTruncas(idCodFuenteFinanciamiento, sMetas, iMes, iAnio, iCodDetPlanilla);
        }
        public IEnumerable<PlanillaCalculadaIngresos_Request> ListarPlanillaCalculadaIngresosCASVacTruncas(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaIngresosCASVacTruncas(iCodTrabajador, iMes, iAnio, iCodTipoPlanilla, iCodDetPlanilla);
        }
        public IEnumerable<PlanillaCalculadaDescuentos_Request> ListarPlanillaCalculadaDescuentosCASVacTruncas(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaDescuentosCASVacTruncas(iCodTrabajador, iMes, iAnio, iCodTipoPlanilla, iCodDetPlanilla);
        }

        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralCompletaCASVacTruncas(int iMes, int iAnio, int iCodDetPlanilla)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaGeneralCompletaCASVacTruncas(iMes, iAnio, iCodDetPlanilla);
        }

        public bool GenerarPlanillaCASVacTruncas(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, List<Empleado_Registro> lstEmpleados)
        {
            return _planillas_Repositorio.GenerarPlanillaCASVacTruncas(iMes, iAnio, iCodPlanilla, iCodTipoPlanilla, iCodDetPlanilla, lstEmpleados);
        }
        public IEnumerable<Empleado_Registro> ListarTrabajadoresPlanillaVacTruncas(int iCodPlanilla, int iMes, int iAnio, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            return _planillas_Repositorio.ListarTrabajadoresPlanillaVacTruncas(iCodPlanilla, iMes, iAnio, iCodTipoPlanilla, iCodDetPlanilla);
        }
        #endregion Planilla CAS Vacaciones Truncas

        #region Planilla FED Vacaciones Truncas
        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralFEDVacTruncas(int idCodFuenteFinanciamiento, string sMetas, int iMes, int iAnio, int iCodDetPlanilla)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaGeneralFEDVacTruncas(idCodFuenteFinanciamiento, sMetas, iMes, iAnio, iCodDetPlanilla);
        }
        public IEnumerable<PlanillaCalculadaIngresos_Request> ListarPlanillaCalculadaIngresosFEDVacTruncas(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaIngresosFEDVacTruncas(iCodTrabajador, iMes, iAnio, iCodTipoPlanilla, iCodDetPlanilla);
        }
        public IEnumerable<PlanillaCalculadaDescuentos_Request> ListarPlanillaCalculadaDescuentosFEDVacTruncas(int iCodTrabajador, int iMes, int iAnio, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaDescuentosFEDVacTruncas(iCodTrabajador, iMes, iAnio, iCodTipoPlanilla, iCodDetPlanilla);
        }

        public IEnumerable<PlanillaCalculada_Request> ListarPlanillaCalculadaGeneralCompletaFEDVacTruncas(int iMes, int iAnio, int iCodDetPlanilla)
        {
            return _planillas_Repositorio.ListarPlanillaCalculadaGeneralCompletaFEDVacTruncas(iMes, iAnio, iCodDetPlanilla);
        }

        public bool GenerarPlanillaFEDVacTruncas(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, List<Empleado_Registro> lstEmpleados)
        {
            return _planillas_Repositorio.GenerarPlanillaFEDVacTruncas(iMes, iAnio, iCodPlanilla, iCodTipoPlanilla, iCodDetPlanilla, lstEmpleados);
        }
        #endregion Planilla FED Vacaciones Truncas




        /*Importacion de asistencia */
        #region Importacion de Asistencia
        public bool EjecutarImportarAsistencia(PlanillaAsistencia_Registro registro)
        {
            return _planillas_Repositorio.EjecutarImportarAsistencia(registro);
        }

        public IEnumerable<PlanillaAsistencia_Registro> ListarPlanillas(PlanillaAsistencia_Registro registro)
        {
            return _planillas_Repositorio.ListarPlanillas(registro);
        }

        public IEnumerable<PlanillaControlAsistencia_Registro> ListarControlAsistencia(PlanillaAsistencia_Registro registro)
        {
            return _planillas_Repositorio.ListarControlAsistencia(registro);
        }

        public bool EliminarControlAsistenciaPermisosPorTrabajador(PlanillaControlAsistencia_Registro registro)
        {
            return _planillas_Repositorio.EliminarControlAsistenciaPermisosPorTrabajador(registro);
        }
        #endregion

        #region Adminitrar Planilla

        public bool InsertarPlanilla(PlanillaEjecucion_Registro registro) 
        {
            return _planillas_Repositorio.InsertarPlanilla(registro);
        }
        public bool CerrarPlanilla(PlanillaEjecucion_Registro registro)
        {
            return _planillas_Repositorio.CerrarPlanilla(registro);
        }
        public bool ModFasePlanilla(PlanillaEjecucion_Registro registro)
        {
            return _planillas_Repositorio.ModFasePlanilla(registro);
        }
        public IEnumerable<PlanillaEjecucion_Registro> ListarPlanillasCreadas(PlanillaEjecucion_Registro objPlanillaEjecucion_Registro)
        {
            return _planillas_Repositorio.ListarPlanillasCreadas(objPlanillaEjecucion_Registro);
        }
        public IEnumerable<Planilla_Request> ListarPlanillasBase()
        {
            return _planillas_Repositorio.ListarPlanillasBase();
        }
        public IEnumerable<TipoPlanilla_Request> ListarTipoPlanillasBase()
        {
            return _planillas_Repositorio.ListarTipoPlanillasBase();
        }
        public IEnumerable<EmpleadoReporteBanco_Registro> ListarPlanillaCerrada(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            return _planillas_Repositorio.ListarPlanillaCerrada(iMes, iAnio, iCodPlanilla, iCodTipoPlanilla, iCodDetPlanilla);
        }
        public IEnumerable<PlanillaRpteResumenGeneraResponse> ReporteResumenGeneral(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            return _planillas_Repositorio.ReporteResumenGeneral(iMes, iAnio, iCodPlanilla, iCodTipoPlanilla, iCodDetPlanilla);
        }
        public IEnumerable<PlanillaRpteResumenGeneraResponse> ReporteResumenGeneral2(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            return _planillas_Repositorio.ReporteResumenGeneral2(iMes, iAnio, iCodPlanilla, iCodTipoPlanilla, iCodDetPlanilla, sMes, sPlanilla, sNombreReporte);
        }
        public IEnumerable<PlanillaRpteResumenGeneralResponse2> RRG2(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            return _planillas_Repositorio.RRG2(iMes, iAnio, iCodPlanilla, iCodTipoPlanilla, iCodDetPlanilla);
        }
        //public IEnumerable<PlanillaRpteResumenGeneralResponse2> RRG2_1(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, string sMes, string sPlanilla)
        //{
        //    return _planillas_Repositorio.RRG2_1(iMes, iAnio, iCodPlanilla, iCodTipoPlanilla, iCodDetPlanilla, sMes, sPlanilla);
        //}
        public IEnumerable<PlanillaRpteResumenGeneralResponse2> RRG3(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            return _planillas_Repositorio.RRG3(iMes, iAnio, iCodPlanilla, iCodTipoPlanilla, iCodDetPlanilla);
        }
        //public IEnumerable<PlanillaRpteResumenGeneralResponse2> RRG3_1(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, string sMes, string sPlanilla)
        //{
        //    return _planillas_Repositorio.RRG3_1(iMes, iAnio, iCodPlanilla, iCodTipoPlanilla, iCodDetPlanilla, sMes, sPlanilla);
        //}
        public IEnumerable<PlanillaRpteResumenGeneralResponse2> RRG4(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            return _planillas_Repositorio.RRG4(iMes, iAnio, iCodPlanilla, iCodTipoPlanilla, iCodDetPlanilla, sMes, sPlanilla, sNombreReporte);
        }
        public IEnumerable<PlanillaRpteResumenGeneralResponse2> RRGEsSalud(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            return _planillas_Repositorio.RRGEsSalud(iMes, iAnio, iCodPlanilla, iCodTipoPlanilla, iCodDetPlanilla, sMes, sPlanilla, sNombreReporte);
        }
        public IEnumerable<PlanillaRpteResumenGeneralResponse2> ReporteDetalladoEsSalud(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            return _planillas_Repositorio.ReporteDetalladoEsSalud(iMes, iAnio, iCodPlanilla, iCodTipoPlanilla, iCodDetPlanilla, sMes, sPlanilla, sNombreReporte);
        }
        public IEnumerable<PlanillaRpteResumenGeneralResponse2> ReporteResumenRetencionesFteFto(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            return _planillas_Repositorio.ReporteResumenRetencionesFteFto(iMes, iAnio, iCodPlanilla, iCodTipoPlanilla, iCodDetPlanilla, sMes, sPlanilla, sNombreReporte);
        }

        public IEnumerable<PlanillaRpteResumenGeneralResponse2> ReporteResumenIngresosMetaPartidaConcepto(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            return _planillas_Repositorio.ReporteResumenIngEgreAporMetaPartidaConcepto(iMes, iAnio, iCodPlanilla, iCodTipoPlanilla, iCodDetPlanilla, 1, false, sMes, sPlanilla, sNombreReporte);
        }
        public IEnumerable<PlanillaRpteResumenGeneralResponse2> ReporteResumenEgresosMetaPartidaConcepto(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            return _planillas_Repositorio.ReporteResumenIngEgreAporMetaPartidaConcepto(iMes, iAnio, iCodPlanilla, iCodTipoPlanilla, iCodDetPlanilla, 2, false, sMes, sPlanilla, sNombreReporte);
        }
        public IEnumerable<PlanillaRpteResumenGeneralResponse2> ReporteResumenAportacionesMetaPartidaConcepto(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            return _planillas_Repositorio.ReporteResumenIngEgreAporMetaPartidaConcepto(iMes, iAnio, iCodPlanilla, iCodTipoPlanilla, iCodDetPlanilla, 3, false, sMes, sPlanilla, sNombreReporte);
        }
        public IEnumerable<PlanillaRpteResumenGeneralResponse2> ReporteResumenIngresosMetaPartidaPorConcepto(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte, int iCodTipoConcepto, int iCodConcepto)
        {
            return _planillas_Repositorio.ReporteResumenIngEgreAporMetaPartidaPorConcepto(iMes, iAnio, iCodPlanilla, iCodTipoPlanilla, iCodDetPlanilla, iCodTipoConcepto, false, sMes, sPlanilla, sNombreReporte, iCodConcepto);
        }
        public IEnumerable<PlanillaRpteResumenGeneralResponse2> ReporteResumenDsctosJudicialesMetaPartidaConcepto(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            return _planillas_Repositorio.ReporteResumenIngEgreAporMetaPartidaConcepto(iMes, iAnio, iCodPlanilla, iCodTipoPlanilla, iCodDetPlanilla, 2, true, sMes, sPlanilla, sNombreReporte);
        }
        public IEnumerable<PlanillaRpteResumenGeneralResponse2> ReporteResumenGralAFPconNroAfiliados(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            return _planillas_Repositorio.ReporteResumenGralAFPconNroAfiliados(iMes, iAnio, iCodPlanilla, iCodTipoPlanilla, iCodDetPlanilla, sMes, sPlanilla, sNombreReporte);
        }
        public IEnumerable<PlanillaRpteResumenGeneralResponse2> ReporteResumenGralAFPconAFPDepMetaClasGasto(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            return _planillas_Repositorio.ReporteResumenGralAFPconAFPDepMetaClasGasto(iMes, iAnio, iCodPlanilla, iCodTipoPlanilla, iCodDetPlanilla, sMes, sPlanilla, sNombreReporte);
        }
        public IEnumerable<PlanillaRpteResumenGeneralResponse2> ReporteResumenGralBcosPorInstCantTotIngTotEgre(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            return _planillas_Repositorio.ReporteResumenGralBcosPorInstCantTotIngTotEgre(iMes, iAnio, iCodPlanilla, iCodTipoPlanilla, iCodDetPlanilla, sMes, sPlanilla, sNombreReporte);
        }
        public IEnumerable<PlanillaRpteResumenGeneralResponse2> ReporteResumenGralBcosFteFtoMeta(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            return _planillas_Repositorio.ReporteResumenGralBcosFteFtoMeta(iMes, iAnio, iCodPlanilla, iCodTipoPlanilla, iCodDetPlanilla, sMes, sPlanilla, sNombreReporte);
        }
        public IEnumerable<PlanillaRpteResumenGeneralResponse2> ReporteResumenDetalladoBcos(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            return _planillas_Repositorio.ReporteResumenDetalladoBcos(iMes, iAnio, iCodPlanilla, iCodTipoPlanilla, iCodDetPlanilla, sMes, sPlanilla, sNombreReporte);
        }
        public IEnumerable<PlanillaRpteResumenGeneralResponse2> ReportePlanillaUnicaPagos(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla, string sMes, string sPlanilla, string sNombreReporte)
        {
            return _planillas_Repositorio.ReportePlanillaUnicaPagos(iMes, iAnio, iCodPlanilla, iCodTipoPlanilla, iCodDetPlanilla, sMes, sPlanilla, sNombreReporte);
        }

        public IEnumerable<PlanillaRpteEstructuraInfoAFPNETResponse> ReporteEstructuraInfoAFPNET(int iMes, int iAnio, string sNombreReporte, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            return _planillas_Repositorio.ReporteEstructuraInfoAFPNET(iMes, iAnio, sNombreReporte, iCodPlanilla, iCodTipoPlanilla, iCodDetPlanilla);
        }
        public IEnumerable<PlanillaRpteEstructuraInfoAFPNETResponse> ReporteTrabajadoresAfiliadosONPE(int iMes, int iAnio, string sNombreReporte)
        {
            return _planillas_Repositorio.ReporteTrabajadoresAfiliadosONPE(iMes, iAnio, sNombreReporte);
        }
        public IEnumerable<PlanillaRpteResumenGeneralAnualResponse> ReporteInfoPortalTranspEstandar(int iMes, int iAnio, string sMes, string sNombreReporte)
        {
            return _planillas_Repositorio.ReporteInfoPortalTranspEstandar(iMes, iAnio, sMes, sNombreReporte);
        }
        public IEnumerable<PlanillaRpteResumenGeneralAnualResponse> ReporteResumenPlanillaMensIngEgreEsSalud(int iAnio, string sNombreReporte, int iCodTipoConcepto)
        {
            return _planillas_Repositorio.ReporteResumenPlanillaMensIngEgreEsSalud(iAnio, sNombreReporte, iCodTipoConcepto);
        }
        public IEnumerable<PlanillaRpteResumenGeneralAnualResponse> ReporteResumenAnuaPorTrab(int iAnio, string sNombreReporte, string sDNI)
        {
            return _planillas_Repositorio.ReporteResumenAnuaPorTrab(iAnio, sNombreReporte, sDNI);
        }
        public IEnumerable<PlanillaRpteResumenGeneralAnualResponse> ReporteResumenIngRent4ta(int iAnio, string sNombreReporte)
        {
            return _planillas_Repositorio.ReporteResumenIngRent4ta(iAnio, sNombreReporte);
        }

        public IEnumerable<ArchivosTXT_Response> GenerarArchivosTXT(ArchivosTXT_Response objArchivosTXT_Response)
        {
            return _planillas_Repositorio.GenerarArchivosTXT(objArchivosTXT_Response);
        }
        public IEnumerable<ArchivosTXT_Response> GenerarArchivosTXTMCPPWEB(ArchivosTXT_Response objArchivosTXT_Response)
        {
            return _planillas_Repositorio.GenerarArchivosTXTMCPPWEB(objArchivosTXT_Response);
        }
        public IEnumerable<ArchivosTXT_Response> GenerarArchivosTXTMCPPWEBJUDICIAL(ArchivosTXT_Response objArchivosTXT_Response)
        {
            return _planillas_Repositorio.GenerarArchivosTXTMCPPWEBJUDICIAL(objArchivosTXT_Response);
        }
        public IEnumerable<PlanillaRpteResumenGeneralAnualResponse> ReporteEjecucionMensualMetaEspGasto(int iAnio, string sNombreReporte, int iTipo)
        {
            return _planillas_Repositorio.ReporteEjecucionMensualMetaEspGasto(iAnio, sNombreReporte, iTipo);
        }
        public bool GenerarPlanillaPorTrabajdor(int iCodTrabajador, int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla)
        {
            return _planillas_Repositorio.GenerarPlanillaPorTrabajdor(iCodTrabajador, iMes, iAnio, iCodPlanilla, iCodTipoPlanilla);
        }
        #endregion

        #region Suspencion Retencion de 4ta Categoria
        public IEnumerable<SuspensionRetencionCuartaCat_Registro> ListarTrabajadoresSuspRet4Ta(SuspensionRetencionCuartaCat_Registro peticion)
        {
            return _planillas_Repositorio.ListarTrabajadoresSuspRet4Ta(peticion);
        }

        public bool InsertarTrabajadoresSuspRet4Ta(SuspensionRetencionCuartaCat_Registro peticion)
        {
            return _planillas_Repositorio.InsertarTrabajadoresSuspRet4Ta(peticion);
        }
        public bool EliminarTrabajadoresSuspRet4Ta(SuspensionRetencionCuartaCat_Registro peticion)
        {
            return _planillas_Repositorio.EliminarTrabajadoresSuspRet4Ta(peticion);
        }
        
        #endregion

        #region PagosTrabajador
        public IEnumerable<PlanillaCalculada_Request> ListarConceptosPagosTrabajador(int iCodTrabajador, int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla, int iCodDetPlanilla)
        {
            return _planillas_Repositorio.ListarConceptosPagosTrabajador(iCodTrabajador, iMes, iAnio, iCodPlanilla, iCodTipoPlanilla, iCodDetPlanilla);
        }

        public bool InsertarConceptoPagosTrabajador(PlanillaCalculada_Request peticion)
        {
            return _planillas_Repositorio.InsertarConceptoPagosTrabajador(peticion);
        }
        public bool ActualizarConceptoPagosTrabajador(PlanillaCalculada_Request peticion)
        {
            return _planillas_Repositorio.ActualizarConceptoPagosTrabajador(peticion);
        }
        public bool EliminarConceptoPagosTrabajador(PlanillaCalculada_Request peticion)
        {
            return _planillas_Repositorio.EliminarConceptoPagosTrabajador(peticion);
        }
        #endregion

        #region ProyecionAnualRta5ta

        public IEnumerable<PlanillaProyeccionAnualRta5ta_Registro> ListarProyeccionAnualRta5ta_RegistroTrabajador(int iCodTrabajador, int iAnio)
        {
            return _planillas_Repositorio.ListarProyeccionAnualRta5ta_RegistroTrabajador(iCodTrabajador, iAnio);
        }
        public bool ActualizarProyeccionAnualRta5taTrabajador(PlanillaProyeccionAnualRta5ta_Registro peticion)
        {
            return _planillas_Repositorio.ActualizarProyeccionAnualRta5taTrabajador(peticion);
        }
        #endregion
    }
}
