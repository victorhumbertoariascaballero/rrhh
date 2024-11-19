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
    public class T_genm_bases_perfil_puesto_LN
    {
        private readonly T_genm_bases_perfil_puesto_ODA _basesPerfilPuesto_Repositorio = new T_genm_bases_perfil_puesto_ODA();       

        public int InsertarBasesPerfilPuesto(BasesPerfilPuestoRegistro registro)
        {
            return _basesPerfilPuesto_Repositorio.InsertarBasesPerfilPuesto(registro);
        }

        public int ActualizarBasesPerfilPuesto(BasesPerfilPuestoRegistro registro)
        {
            return _basesPerfilPuesto_Repositorio.ActualizarBasesPerfilPuesto(registro);
        }

        public int AprobarBasesPerfilPuesto(BasesPerfilPuestoRegistro registro)
        {
            return _basesPerfilPuesto_Repositorio.AprobarBasesPerfilPuesto(registro);
        }

        public IEnumerable<BasesPerfilPuesto_Request> ObtenerBasesPerfilesPuestoConvocatoria(Int32 idDependencia, Int32 iTipo)
        {
            return _basesPerfilPuesto_Repositorio.ObtenerBasesPerfilesPuestoConvocatoria(idDependencia, iTipo);
        }
        public IEnumerable<BasesPerfilPuestoRegistro> ObtenerBasesPerfilesPuesto(String strOrgano, String strUO, String strEstado, string fechaIni, string fechaFin)
        {
            return _basesPerfilPuesto_Repositorio.ObtenerBasesPerfilesPuesto(strOrgano, strUO, strEstado, fechaIni, fechaFin);
        }

        public IEnumerable<BasesPerfilPuestoRegistro> ObtenerBasesPerfilesPuestoPorID(string id)
        {
            return _basesPerfilPuesto_Repositorio.ObtenerBasesPerfilesPuestoPorID(id);
        }

        public IEnumerable<PerfilPuesto_Request> ListarPerfilPuesto(string strPerfilPuesto, int tipo)
        {
            return _basesPerfilPuesto_Repositorio.ListarPerfilPuesto(strPerfilPuesto, tipo);
        }

        public string ObtenerRutaBasesConvocatorias(string strParametro)
        {
            return _basesPerfilPuesto_Repositorio.ObtenerRutaBasesConvocatorias(strParametro);
        }

        public bool LiberarBases(BasesPerfilPuestoObservacionRegistro peticion)
        {
            return _basesPerfilPuesto_Repositorio.LiberarBases(peticion);
        }

        public IEnumerable<BasesPerfilPuestoObservacionRequest> ListarBasesPerfilPuestoObservacion(string id)
        {
            return _basesPerfilPuesto_Repositorio.ListarBasesPerfilPuestoObservacion(id);
        }

        public int PublicarBasesPerfilPuesto(BasesPerfilPuestoRegistro registro)
        {
            return _basesPerfilPuesto_Repositorio.PublicarBasesPerfilPuesto(registro);
        }
        public Int32 RegistrarBasesArchivo(BasesPerfilPuestoRegistro registro)
        {
            return _basesPerfilPuesto_Repositorio.RegistrarBasesArchivo(registro);
        }
    }
}
