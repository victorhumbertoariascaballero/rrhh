using MIDIS.ORI.Entidades.Core;
using MIDIS.SEG.AccesoDatosSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.SEG.LogicaNegocio
{
    public class T_genm_estadoproceso_LN
    {

        private readonly T_genm_estadoproceso_ODA _Repositorio = new T_genm_estadoproceso_ODA();
        public IEnumerable<EstadoProceso_Registro> ListarEstadoProceso()
        {
            return _Repositorio.ListarEstadoProceso();
        }
    }
}
