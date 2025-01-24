using MIDIS.ORI.Entidades;
using MIDIS.SEG.AccesoDatosSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.SEG.LogicaNegocio
{
    public class T_genm_justificacionesproceso_LN
    {
        private readonly T_genm_justificacionesproceso_ODA _Repositorio = new T_genm_justificacionesproceso_ODA();
        public IEnumerable<JustificacionesProceso_Registro> ListarJustificacionProcesoHistorial(int iCodJustificaciones)
        {
            return _Repositorio.ListarJustificacionProcesoHistorial(iCodJustificaciones);
        }
    }
}
