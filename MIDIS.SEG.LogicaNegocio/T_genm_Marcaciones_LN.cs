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
    public class T_genm_Marcaciones_LN : BaseLN
    {
        private readonly T_genm_Marcaciones_ODA _Repositorio = new T_genm_Marcaciones_ODA();

        public IEnumerable<TipoMarcaciones_Registro> ListarTipoMarcaciones()
        {
            return _Repositorio.ListarTipoMarcaciones();
        }

        public IEnumerable<Marcaciones_Registro> ListarMarcaciones(Marcaciones_Request request)
        {
            return _Repositorio.ListarMarcaciones(request);
        }

        public Marcaciones_Registro Insertar(Marcaciones_Registro request)
        {
            return _Repositorio.Insertar(request);
        }
    }

}
