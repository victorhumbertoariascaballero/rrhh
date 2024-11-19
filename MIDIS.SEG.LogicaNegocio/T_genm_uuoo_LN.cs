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
    public class T_genm_uuoo_LN : BaseLN
    {
        private readonly T_genm_uuoo_ODA _uuoo_Repositorio = new T_genm_uuoo_ODA();

        public UnidadOrganica_Registro ObtenerUnidadOrganica(string id)
        {
            return _uuoo_Repositorio.ObtenerUnidadOrganica(id);
        }
        public IEnumerable<UnidadOrganica_Registro> ListarOrganos()
        {
            return _uuoo_Repositorio.ListarOrganos();
        }
        public IEnumerable<UnidadOrganica_Registro> ListarUnidadesOrganicas(string id)
        {
            return _uuoo_Repositorio.ListarUnidadesOrganicas(id);
        }
    }
}
