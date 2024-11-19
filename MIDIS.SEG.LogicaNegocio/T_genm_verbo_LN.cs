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
    public class T_genm_verbo_LN : BaseLN
    {
        private readonly T_genm_verbo_ODA _verbo_Repositorio = new T_genm_verbo_ODA();

        public IEnumerable<Verbo_Registro> ListarVerbos()
        {
            return _verbo_Repositorio.ListarVerbos();
        }
    }
}
