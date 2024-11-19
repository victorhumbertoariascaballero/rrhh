#region Espacio de Nombres
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using MIDIS.ORI.Entidades;
using MIDIS.ORI.LogicaNegocio.Base;
using MIDIS.SEG.AccesoDatosSQL;
using MIDIS.Utiles;
using MIDIS.Autenticacion;
#endregion

namespace MIDIS.SEG.LogicaNegocio
{
    public partial class T_genm_descuentojudicial_LN : BaseLN
    {

        private readonly T_genm_descuentojudicial_ODA _descuentojudicial_Repositorio = new T_genm_descuentojudicial_ODA();

        #region Métodos

        public IEnumerable<Banco_Registro> ListarBancos(Banco_Request peticion)
        {
            return _descuentojudicial_Repositorio.ListarBancos(peticion);
        }

        public IEnumerable<DescuentoJudicial_Registro> ListarDescuentoJudicial(DescuentoJudicial_Request peticion)
        {
            return _descuentojudicial_Repositorio.ListarDescuentoJudicial(peticion);
        }

        public IEnumerable<DescuentoJudicial_Registro> ListarDescuentoJudicialTrabajadores()
        {
            return _descuentojudicial_Repositorio.ListarDescuentoJudicialTrabajadores();
        }

        public DescuentoJudicial_Registro ObtenerDescuentoJudicialParaEditar(DescuentoJudicial_Request peticion)
        {
            return _descuentojudicial_Repositorio.ObtenerDescuentoJudicialParaEditar(peticion);
        }

        public DescuentoJudicial_Registro ObtenerDescuentoJudicialTrabajadorParaEditar(DescuentoJudicial_Request peticion)
        {
            return _descuentojudicial_Repositorio.ObtenerDescuentoJudicialTrabajadorParaEditar(peticion);
        }

        public Int32 RegistrarDescuentoJudicialTrabajador(DescuentoJudicial_Registro registro)
        {
            return _descuentojudicial_Repositorio.RegistrarDescuentoJudicial(registro);
        }

        public Int32 RegistrarDescuentoJudicialTrabajador_Nuevo(DescuentoJudicial_Registro registro)
        {
            return _descuentojudicial_Repositorio.RegistrarDescuentoJudicial_Nuevo(registro);
        }

        public Int32 ActualizarDescuentoJudicialTrabajador(DescuentoJudicial_Registro registro)
        {
            return _descuentojudicial_Repositorio.ActualizarDescuentoJudicial(registro);
        }

        public Int32 ActualizarDescuentoJudicialTrabajador_Nuevo(DescuentoJudicial_Registro registro)
        {
            return _descuentojudicial_Repositorio.ActualizarDescuentoJudicial_Nuevo(registro);
        }

        public Int32 EliminarDescuentoJudicialTrabajador(DescuentoJudicial_Registro registro)
        {
            return _descuentojudicial_Repositorio.EliminarDescuentoJudicial(registro);
        }

        public Int32 EliminarDescuentoJudicialBeneficiario(DescuentoJudicial_Registro registro)
        {
            return _descuentojudicial_Repositorio.EliminarDescuentoJudicialBeneficiario(registro);
        }

        public Int32 CargarDescuentoJudicial(DescuentoJudicial_Registro registro)
        {
            return _descuentojudicial_Repositorio.CargarDescuentoJudicial(registro);
        }

        public bool GenerarPlanillaJudicial(int iMes, int iAnio, int iCodPlanilla, int iCodTipoPlanilla)
        {
            return _descuentojudicial_Repositorio.GenerarPlanillaJudicial(iMes, iAnio, iCodPlanilla, iCodTipoPlanilla);
        }


        #endregion

        #region Métodos - Judicial Detalle
        public IEnumerable<DescuentoJudicialBeneficiario_Registro> ListarDescuentoJudicial_Beneficiario(DescuentoJudicialBeneficiario_Request peticion)
        {
            return _descuentojudicial_Repositorio.ListarDescuentoJudicial_Beneficiario(peticion);
        }

        public IEnumerable<DescuentoJudicialBeneficiario_Registro> ListarDescuentoJudicial_BeneficiarioTrabajadores(DescuentoJudicialBeneficiario_Request peticion)
        {
            return _descuentojudicial_Repositorio.ListarDescuentoJudicial_BeneficiarioTrabajadores(peticion);
        }
        #endregion
        public Int32 Registrar_Beneficiario(DescuentoJudicialBeneficiario_Registro registro)
        {
            return _descuentojudicial_Repositorio.Registrar_Beneficiario(registro);
        }
        public bool Quitar_Beneficiario(DescuentoJudicialBeneficiario_Registro registro)
        {
            return _descuentojudicial_Repositorio.Quitar_Beneficiario(registro);
        }

    }
}
