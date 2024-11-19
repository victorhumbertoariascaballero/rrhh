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
    public class T_genm_conceptofijovariable_LN : BaseLN
    {
        private readonly T_genm_conceptofijovariable_ODA _concepto_Repositorio = new T_genm_conceptofijovariable_ODA();

        #region Métodos

        public IEnumerable<ConceptoFijoVariable_Registro> ListarConceptoVariable(ConceptoFijoVariable_Request peticion)
        {
            return _concepto_Repositorio.ListarConceptoVariable(peticion);
        }

        public IEnumerable<ConceptoFijoVariable_Registro> ListarConceptoVariableNoBaseImponible(ConceptoFijoVariable_Request peticion)
        {
            return _concepto_Repositorio.ListarConceptoVariableNoBaseImponible(peticion);
        }

        public IEnumerable<PlanillaEjecucion_Response> ListarPlanillaEjecucionAperturada(PlanillaEjecucion_Request peticion)
        {
            return _concepto_Repositorio.ListarPlanillaEjecucionAperturada(peticion);
        }

        public ConceptoFijoVariable_Registro ObtenerConceptoTrabajadorParaEditar(ConceptoFijoVariable_Request peticion)
        {
            return _concepto_Repositorio.ObtenerConceptoTrabajadorParaEditar(peticion);
        }


        //public Int32 RegistrarConceptoTrabajador(ConceptoFijoVariable_Registro registro)
        //{
        //    return _concepto_Repositorio.RegistrarConcepto(registro);
        //}
        public string RegistrarConceptoTrabajador(ConceptoFijoVariable_Registro registro)
        {
            return _concepto_Repositorio.RegistrarConcepto(registro);
        }
        public Int32 ActualizarConceptoTrabajador(ConceptoFijoVariable_Registro registro)
        {
            return _concepto_Repositorio.ActualizarConcepto(registro);
        }

        public bool EliminarConceptoTrabajador(ConceptoFijoVariable_Registro registro)
        {
            return _concepto_Repositorio.EliminarConcepto(registro);
        }

        //public Int32 EliminarConcepto(Concepto_Registro registro)
        //{
        //    return _concepto_Repositorio.EliminarConcepto(registro);
        //}
        #endregion

    }
}
