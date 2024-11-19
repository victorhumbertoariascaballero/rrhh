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
    public class T_genm_Turnos_LN : BaseLN
    {
        private readonly T_genm_Turnos_ODA _Repositorio = new T_genm_Turnos_ODA();

        public IEnumerable<Turno_Registro> ListarTurnos()
        {
            return _Repositorio.ListarTurnos();
        }

        public IEnumerable<TurnoTrabajador_Registro> ListarTurnosTrabajador(TurnoTrabajador_Request request)
        {
            return _Repositorio.ListarTurnosTrabajador(request);
        }

        public TurnoTrabajador_Registro Insertar(TurnoTrabajador_Registro request)
        {
            return _Repositorio.Insertar(request);
        }
    }

}
