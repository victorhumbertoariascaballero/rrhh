using MIDIS.ORI.Entidades;
using MIDIS.SEG.AccesoDatosSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.SEG.LogicaNegocio
{
    public class T_genm_TipoJustificacion_LN
    {
        private readonly T_genm_TipoJustificacion_ODA _Repositorio = new T_genm_TipoJustificacion_ODA();
        public IEnumerable<TipoJustificacion_Registro> ListarTipoJustificacion()
        {
            return _Repositorio.ListarTipoJustificacion();
        }
    }
}
