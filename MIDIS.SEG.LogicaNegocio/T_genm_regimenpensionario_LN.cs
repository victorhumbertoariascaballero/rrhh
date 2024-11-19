using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using MIDIS.ORI.LogicaNegocio.Base;
using MIDIS.ORI.Entidades;
using MIDIS.SEG.AccesoDatosSQL;



namespace MIDIS.SEG.LogicaNegocio
{
    public class T_genm_regimenpensionario_LN : BaseLN
    {
        private readonly T_genm_regimenpensionario_ODA _regimenpensionario_Repositorio = new T_genm_regimenpensionario_ODA();

        #region Métodos

        //public IEnumerable<RegimenPensionario_Registro> ListarRegimenPensionario(RegimenPensionario_Request peticion)
        //{
        //    return _regimenpensionario_Repositorio.ListarRegimenPensionario(peticion);
        //}

        public IEnumerable<RegimenPensionario_Response> ListarRegimenPensionario(RegimenPensionario_Request peticion)
        {
            return _regimenpensionario_Repositorio.ListarRegimenPensionario(peticion);
        }

        public IEnumerable<RegimenAfp_Response> ListarRegimenAfp(RegimenAfp_Request peticion)
        {
            return _regimenpensionario_Repositorio.ListarRegimenAfp(peticion);
        }

        //public IEnumerable<RegimenPensionario_Response> ListarRegimenPensionario(RegimenAfp_Request peticion)
        //{
        //    return _regimenpensionario_Repositorio.ListarRegimenAfp(peticion);
        //}

        public IEnumerable<TipoRegimenPensionario_Response> ListarTipoRegimenPensionario(TipoRegimenPensionario_Request peticion)
        {
            return _regimenpensionario_Repositorio.ListarTipoRegimenPensionario(peticion);
        }

        public RegistroRegimenPensionario_Registro ObtenerRegimenParaEditar(RegistroRegimenPensionario_Request peticion)
        {
            return _regimenpensionario_Repositorio.ObtenerRegimenParaEditar(peticion);
        }

        public Int32 InsertarRegistroRegimenPensionario(RegistroRegimenPensionario_Registro registro)
        {
            return _regimenpensionario_Repositorio.InsertarRegistroRegimenPensionario(registro);
        }
        public Int32 ActualizarRegistroRegimenPensionario(RegistroRegimenPensionario_Registro registro)
        {
            return _regimenpensionario_Repositorio.ActualizarRegistroRegimenPensionario(registro);
        }

        //public IEnumerable<RegistroRegimenPensionario_Registro> ListarRegistroRegimenPensionario(RegistroRegimenPensionario_Request peticion)
        //{
        //    return _regimenpensionario_Repositorio.ListarRegistroRegimenPensionario(iMes, iAnio);
        //}

        public IEnumerable<RegistroRegimenPensionario_Registro> ListarRegistroRegimenPensionario(int iMes, int iAnio)
        {
            return _regimenpensionario_Repositorio.ListarRegistroRegimenPensionario(iMes, iAnio);
        }

        public Int32 CopiarRegimenPensionario(RegistroRegimenPensionario_Registro registro)
        {
            return _regimenpensionario_Repositorio.CopiarRegimenPensionario(registro);
        }
            
        #endregion


        //public object ListarTipoRegimenPensionario(TipoRegimenPensionario_Response peticion)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
