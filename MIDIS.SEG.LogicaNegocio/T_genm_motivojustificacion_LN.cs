using MIDIS.ORI.Entidades;
using MIDIS.ORI.Entidades.Core;
using MIDIS.SEG.AccesoDatosSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.SEG.LogicaNegocio
{
    public class T_genm_motivojustificacion_LN
    {
        private readonly T_genm_motivojustificacion_ODA _Repositorio = new T_genm_motivojustificacion_ODA();

        public IEnumerable<MotivoJustificacion_Registro> ListarMotivoJustificacion(MotivoJustificacion_Request request)
        {
            return _Repositorio.ListarMotivoJustificacion(request);
        }

      
    }
}
