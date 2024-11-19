using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Espacio de Nombres
using System.Configuration;
using MIDIS.ORI.Entidades;
using MIDIS.ORI.LogicaNegocio.Base;
using MIDIS.SEG.AccesoDatosSQL;
using MIDIS.Utiles;
using MIDIS.Autenticacion;
#endregion


namespace MIDIS.SEG.LogicaNegocio
{

    public class T_genm_concepto_LN : BaseLN
    {
        private readonly T_genm_concepto_ODA _concepto_Repositorio = new T_genm_concepto_ODA();

        #region Métodos

        public IEnumerable<Concepto_Registro> ListarConcepto(Concepto_Request peticion)
        {
            return _concepto_Repositorio.ListarConcepto(peticion);
        }

        public IEnumerable<TipoConcepto_Response> ListarTipoConcepto(TipoConcepto_Request peticion)
        {
            return _concepto_Repositorio.ListarTipoConcepto(peticion);
        }        

        public IEnumerable<SubTipoConcepto_Response> ListarSubTipoConcepto(SubTipoConcepto_Request peticion)
        {
            return _concepto_Repositorio.ListarSubTipoConcepto(peticion);
        }

        public IEnumerable<SubTipoConcepto_Response> ListarSubTipoConceptoNoBaseImponible(SubTipoConcepto_Request peticion)
        {
            return _concepto_Repositorio.ListarSubTipoConceptoNoBaseImponible(peticion);
        }

        public IEnumerable<Concepto_Response> ListarConceptoPorTipo(Concepto_Request peticion)
        {
            return _concepto_Repositorio.ListarConceptoPorTipo(peticion);
        }

        public IEnumerable<Concepto_Response> ListarConceptoPorTipoNoBaseImponible(Concepto_Request peticion)
        {
            return _concepto_Repositorio.ListarConceptoPorTipoNoBaseImponible(peticion);
        }

        public IEnumerable<Concepto_Response> ListarConceptoPorTipoConcepto(Concepto_Request peticion)
        {
            return _concepto_Repositorio.ListarConceptoPorTipoConcepto(peticion);
        }

        public Concepto_Registro ObtenerConceptoParaEditar(Concepto_Request peticion)
        {
            return _concepto_Repositorio.ObtenerConceptoParaEditar(peticion);
        }

        public Int32 RegistrarConcepto(Concepto_Registro registro)
        {
            return _concepto_Repositorio.RegistrarConcepto(registro);
        }
        public Int32 ActualizarConcepto(Concepto_Registro registro)
        {
            return _concepto_Repositorio.ActualizarConcepto(registro);
        }
        public Int32 EliminarConcepto(Concepto_Registro registro)
        {
            return _concepto_Repositorio.EliminarConcepto(registro);
        }
        #endregion
    }
}
