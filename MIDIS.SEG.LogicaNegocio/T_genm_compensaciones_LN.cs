using MIDIS.ORI.Entidades.Core;
using MIDIS.SEG.AccesoDatosSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.SEG.LogicaNegocio
{
    public class T_genm_compensaciones_LN
    {
        private readonly T_genm_compensaciones_ODA _Repositorio = new T_genm_compensaciones_ODA();
        public List<Compensaciones_Registro> ListarCompensaciones(CompensacionesTrabajador_Request request)
        {
            return _Repositorio.ListarCompensaciones(request);
        }
        public List<Compensaciones_Registro> ListarCompensacionesAptas(CompensacionesTrabajador_Request request)
        {
            return _Repositorio.ListarCompensacionesAptas(request);
        }
        public List<CompensacionesCalendarioTrabajador_Registro> ListarCompensacionesCalendarAptas(CompensacionesTrabajador_Request request)
        {
            return _Repositorio.ListarCompensacionesCalendarAptas(request);
        }
        public CompensacionesTrabajador_Registro ObtenerCompensacionPorId(int iCodCompensaciones)
        {
            return _Repositorio.ObtenerCompensacionPorId(iCodCompensaciones);
        }

        public CompensacionesTrabajador_Registro InsertarCompensacion(CompensacionesTrabajador_Registro request)
        {
            return _Repositorio.InsertarCompensacion(request);
        }

        public CompensacionesTrabajador_Registro ActualizarCompensacion(CompensacionesTrabajador_Registro request)
        {
            return _Repositorio.ActualizarCompensacion(request);
        }

        public CompensacionesTrabajador_Registro Aprobar(CompensacionesTrabajador_Registro request)
        {
            return _Repositorio.AprobarCompensacion(request);
        }

        public CompensacionesTrabajador_Registro Denegar(CompensacionesTrabajador_Registro request)
        {
            return _Repositorio.DenegarCompensacion(request);
        }
        public CompensacionesTrabajador_Registro AprobarMas(CompensacionesTrabajador_Registro request)
        {
            return _Repositorio.AprobarCompensacionMas(request);
        }

        public CompensacionesTrabajador_Registro DenegarMas(CompensacionesTrabajador_Registro request)
        {
            return _Repositorio.DenegarCompensacionMas(request);
        }
        public bool InsertarCompensacionDetalle(List<CompensacionesDetalleTrabajador_Request> request)
        {
            return _Repositorio.InsertarCompensacionDetalle(request);
        }

        public decimal ObtenerTotalHorasPorCompensacion(int iCodCompensaciones)
        {
            return _Repositorio.ObtenerTotalHorasPorCompensacion(iCodCompensaciones);
        }

        public IEnumerable<CompensacionesProceso> ListarCompensacionesProcesoHistorial(int iCodCompensaciones)
        {
            return _Repositorio.ListarCompensacionesProcesoHistorial(iCodCompensaciones);
        }

        public decimal GetMinutosXFecha(CompensacionesTrabajador_Request request)
        {
            return _Repositorio.GetMinutosXFecha(request);
        }

        public IEnumerable<CompensacionesDetalleTrabajador_Request> ListarCompensacionesDetalle(CompensacionesDetalleTrabajador_Request request)
        {
            return _Repositorio.ListarCompensacionesDetalle(request);
        }

        public IEnumerable<CompensacionesDetalleProceso> ListarCompensacionesDetalleProcesoHistorial(int iCodCompensacionesDetalle)
        {
            return _Repositorio.ListarCompensacionesDetalleProcesoHistorial(iCodCompensacionesDetalle);
        }

        public CompensacionesDetalleTrabajador_Request AprobarDetalle(CompensacionesDetalleTrabajador_Request request)
        {
            return _Repositorio.AprobarCompensacionDetalle(request);
        }

        public CompensacionesDetalleTrabajador_Request DenegarDetalle(CompensacionesDetalleTrabajador_Request request)
        {
            return _Repositorio.DenegarCompensacionDetalle(request);
        }

        public CompensacionesDetalleTrabajador_Request AprobarDetalleMas(CompensacionesDetalleTrabajador_Request request)
        {
            return _Repositorio.AprobarCompensacionDetalleMas(request);
        }

        public CompensacionesDetalleTrabajador_Request DenegarDetalleMas(CompensacionesDetalleTrabajador_Request request)
        {
            return _Repositorio.DenegarCompensacionDetalleMas(request);
        }
    }
}
