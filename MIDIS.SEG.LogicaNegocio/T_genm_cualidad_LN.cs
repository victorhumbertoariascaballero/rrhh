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
    public class T_genm_cualidad_LN : BaseLN
    {
        private readonly T_genm_TipoCualidad_ODA _tipoCualidad_Repositorio = new T_genm_TipoCualidad_ODA();
        private readonly T_genm_Cualidad_ODA _cualidad_Repositorio = new T_genm_Cualidad_ODA();
        public IEnumerable<Tipo_Cualidad_Response> ListarTiposCualidad()
        {
            return _tipoCualidad_Repositorio.ListarTipoCualidad();
        }

        public IEnumerable<Cualidad_Response> ListarCualidadesPorTipo(int id)
        {
            return _cualidad_Repositorio.ListarTipoCualidad(id);
        }
    }
}
