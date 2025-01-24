using MIDIS.ORI.Entidades.Core;
using MIDIS.SEG.AccesoDatosSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.SEG.LogicaNegocio
{
    public class T_genm_vacaciones_LN
    {
        private readonly T_genm_vacaciones_ODA _Repositorio = new T_genm_vacaciones_ODA();
        public IEnumerable<Vacaciones_Registro> ListarVacacionesTrabajador(VacacionesTrabajador_Request request)
        {
            return _Repositorio.ListaVacacionesTrabajador(request);
        }
        
        public VacacionesTrabajador_Registro ObtenerVacacionesPorId(int iCodVacaciones)
        {
            return _Repositorio.ObtenerVacacionesPorId(iCodVacaciones);
        }
        public VacacionesTrabajador_Registro Insertar(VacacionesTrabajador_Registro request)
        {
            return _Repositorio.InsertarVacaciones(request);
        }
        public VacacionesTrabajador_Registro Actualizar(VacacionesTrabajador_Registro request)
        {
            return _Repositorio.ActualizarVacaciones(request);
        }
        public IEnumerable<VacacionesPeriodo_Registro> ListarVacacionesPeriodo(VacacionesPeriodo_Registro request)
        {
            return _Repositorio.ListarVacacionesPeriodo(request);
        }
        public VacacionesTrabajador_Registro Aprobar(VacacionesTrabajador_Registro request)
        {
            return _Repositorio.AprobarVacaciones(request);
        }

        public VacacionesTrabajador_Registro Denegar(VacacionesTrabajador_Registro request)
        {
            return _Repositorio.DenegarVacaciones(request);
        }

        public VacacionesTrabajador_Registro AprobarMas(VacacionesTrabajador_Registro request)
        {
            return _Repositorio.AprobarVacacionesMas(request);
        }

        public VacacionesTrabajador_Registro DenegarMas(VacacionesTrabajador_Registro request)
        {
            return _Repositorio.DenegarVacacionesMas(request);
        }

        public IEnumerable<VacacionesProceso> ListarVacacionesProcesoHistorial(int iCodVacaciones)
        {
            return _Repositorio.ListarVacacionesProcesoHistorial(iCodVacaciones);
        }
        //public VacacionesTrabajador_Registro ObtenerJustificacionPorId(int iCodVacaciones)
        //{
        //    return _Repositorio.ObtenerJustificacionPorId(iCodVacaciones);
        //}
    }
}
