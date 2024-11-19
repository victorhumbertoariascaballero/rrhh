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
    public class T_genm_metas_LN
    {
        private readonly T_genm_metas_ODA _metas_Repositorio = new T_genm_metas_ODA();

        public IEnumerable<Metas_Response> ListarMetas()
        {
            return _metas_Repositorio.ListarMetas();
        }

    }
}
