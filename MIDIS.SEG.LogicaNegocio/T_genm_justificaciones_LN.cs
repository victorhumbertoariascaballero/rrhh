using MIDIS.ORI.Entidades;
using MIDIS.ORI.Entidades.Core;
using MIDIS.SEG.AccesoDatosSQL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.SEG.LogicaNegocio
{
    public class T_genm_justificaciones_LN
    {
        private readonly T_genm_justificaciones_ODA _Repositorio = new T_genm_justificaciones_ODA();
        public IEnumerable<Justificaciones_Registro> ListarJustificacionesTrabajador(JustificacionesTrabajador_Request request)
        {
            return _Repositorio.ListarJustificacionTrabajador(request);
        }
        public JustificacionesTrabajador_Registro Insertar(JustificacionesTrabajador_Registro request)
        {
            return _Repositorio.Insertar(request);
        }
        public JustificacionesTrabajador_Registro Actualizar(JustificacionesTrabajador_Registro request)
        {
            return _Repositorio.ActualizarJustificacion(request);
        }

        public JustificacionesTrabajador_Registro Aprobar(JustificacionesTrabajador_Registro request)
        {
            return _Repositorio.AprobarJustificacion(request);
        }

        public JustificacionesTrabajador_Registro Denegar(JustificacionesTrabajador_Registro request)
        {
            return _Repositorio.DenegarJustificacion(request);
        }
        public JustificacionesTrabajador_Registro ObtenerJustificacionPorId(int iCodJustificaciones)
        {
            return _Repositorio.ObtenerJustificacionPorId(iCodJustificaciones);
        }

        public JustificacionesTrabajador_Registro AprobarMas(JustificacionesTrabajador_Registro request)
        {
            return _Repositorio.AprobarJustificacionMas(request);
        }

        public JustificacionesTrabajador_Registro DenegarMas(JustificacionesTrabajador_Registro request)
        {
            return _Repositorio.DenegarJustificacionMas(request);
        }


    }
}
