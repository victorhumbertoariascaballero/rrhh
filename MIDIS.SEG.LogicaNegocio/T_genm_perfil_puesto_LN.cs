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
    public class T_genm_perfil_puesto_LN
    {
        private readonly T_genm_perfil_puesto_ODA _perfilPuesto_Repositorio = new T_genm_perfil_puesto_ODA();
        public int InsertarCab(PerfilPuestoRegistro registro)
        {
            return _perfilPuesto_Repositorio.InsertarCab(registro);
        }
        public int ActualizarCab(PerfilPuestoRegistro registro)
        {
            return _perfilPuesto_Repositorio.ActualizarCab(registro);
        }
        public int ActualizarAnexo1(PerfilPuestoRegistro registro)
        {
            return _perfilPuesto_Repositorio.ActualizarAnexo1(registro);
        }
        public int InsertarMision(PerfilPuestoRegistro registro)
        {
            return _perfilPuesto_Repositorio.InsertarMision(registro);
        }

        public int InsertarExperiencia(PerfilPuestoRegistro registro)
        {
            return _perfilPuesto_Repositorio.InsertarExperiencia(registro);
        }

        public IEnumerable<PerfilPuesto_Request> ObtenerPerfilesPuesto(string id, string fechaIni, string fechaFin)
        {
            return _perfilPuesto_Repositorio.ObtenerPerfilesPuesto(id, fechaIni, fechaFin);
        }
        public IEnumerable<PerfilPuestoRegistro> ObtenerPerfilesPuestoUserRRHH(String strOrgano, String strUO, String strEstado, String strNombre)
        {
            return _perfilPuesto_Repositorio.ObtenerPerfilesPuestoUserRRHH(strOrgano, strUO, strEstado, strNombre);
        }
        public IEnumerable<PerfilPuesto_Request> ObtenerPerfilesPuestoJefe(string IdDependencia, string iCodUnidadOrganica, string fechaIni, string fechaFin)
        {
            return _perfilPuesto_Repositorio.ObtenerPerfilesPuestoJefe(IdDependencia, iCodUnidadOrganica, fechaIni, fechaFin);
        }
        public IEnumerable<PerfilPuesto_Request> ObtenerPerfilesPuestoJefeRRHH(string id, string fechaIni, string fechaFin)
        {
            return _perfilPuesto_Repositorio.ObtenerPerfilesPuestoJefeRRHH(id, fechaIni, fechaFin);
        }
        public IEnumerable<PerfilPuesto_Request> ObtenerPerfilesPuestoUser(string id, string fechaIni, string fechaFin)
        {
            return _perfilPuesto_Repositorio.ObtenerPerfilesPuestoUser(id, fechaIni, fechaFin);
        }
        public IEnumerable<Dependencia_Request> ObtenerDependenciasPorUUOO(string id)
        {
            return _perfilPuesto_Repositorio.ObtenerDependenciasPorUUOO(id);
        }

        public IEnumerable<PerfilPuestoRegistro> ObtenerPerfilesPuestoPorID(string id)
        {
            return _perfilPuesto_Repositorio.ObtenerPerfilesPuestoPorID(id);
        }
        public IEnumerable<PerfilPuestoRegistro> ObtenerPerfilesAnexo1PorID(string id)
        {
            return _perfilPuesto_Repositorio.ObtenerPerfilesAnexo1PorID(id);
        }
        public IEnumerable<PerfilFunciones_Request> ListarPerfilDetFunciones(PerfilFunciones_Request peticion)
        {
            return _perfilPuesto_Repositorio.ListarPerfilDetFunciones(peticion);
        }

        public bool InsertarFunciones(PerfilFunciones_Request peticion)
        {
            return _perfilPuesto_Repositorio.InsertarFunciones(peticion);
        }

        public bool ActualizarFunciones(PerfilFunciones_Request peticion)
        {
            return _perfilPuesto_Repositorio.ActualizarFunciones(peticion);
        }

        public bool EliminarFunciones(PerfilFunciones_Request peticion)
        {
            return _perfilPuesto_Repositorio.EliminarFunciones(peticion);
        }

        public IEnumerable<RequisitosAdicionales_Registro> ListarPerfilDetRequisitosAdicionales(RequisitosAdicionales_Registro peticion)
        {
            return _perfilPuesto_Repositorio.ListarPerfilDetRequisitosAdicionales(peticion);
        }

        public bool InsertarDetRequisitosAdicionales(RequisitosAdicionales_Registro peticion)
        {
            return _perfilPuesto_Repositorio.InsertarDetRequisitosAdicionales(peticion);
        }

        public bool ActualizarDetRequisitosAdicionales(RequisitosAdicionales_Registro peticion)
        {
            return _perfilPuesto_Repositorio.ActualizarDetRequisitosAdicionales(peticion);
        }

        public bool EliminarDetRequisitosAdicionales(RequisitosAdicionales_Registro peticion)
        {
            return _perfilPuesto_Repositorio.EliminarDetRequisitosAdicionales(peticion);
        }

        public IEnumerable<Habilidad_Competencias_Registro> ListarPerfilDetHabilidades_Competencias(Habilidad_Competencias_Registro peticion)
        {
            return _perfilPuesto_Repositorio.ListarPerfilDetHabilidades_Competencias(peticion);
        }

        public IEnumerable<Habilidad_Competencias_Registro> ListarPerfilDetHabilidades(Habilidad_Competencias_Registro peticion)
        {
            return _perfilPuesto_Repositorio.ListarPerfilDetHabilidades(peticion);
        }

        public IEnumerable<Habilidad_Competencias_Registro> ListarPerfilDetCompetencias(Habilidad_Competencias_Registro peticion)
        {
            return _perfilPuesto_Repositorio.ListarPerfilDetCompetencias(peticion);
        }

        public bool InsertarDetHabilidades_Competencias(Habilidad_Competencias_Registro peticion)
        {
            return _perfilPuesto_Repositorio.InsertarDetHabilidades_Competencias(peticion);
        }

        public bool ActualizarDetHabilidades_Competencias(Habilidad_Competencias_Registro peticion)
        {
            return _perfilPuesto_Repositorio.ActualizarDetHabilidades_Competencias(peticion);
        }

        public bool EliminarDetHabilidades_Competencias(Habilidad_Competencias_Registro peticion)
        {
            return _perfilPuesto_Repositorio.EliminarDetHabilidades_Competencias(peticion);
        }

        public IEnumerable<NivelMinimo_Registro> ListarNivelMimino()
        {
            return _perfilPuesto_Repositorio.ListarNivelMimino();
        }
        public IEnumerable<PerfilNivelMateria_Response> ListarMaePerfilNivelMateria()
        {
            return _perfilPuesto_Repositorio.ListarMaePerfilNivelMateria();
        }
        public IEnumerable<PerfilTipoMateria_Response> ListarMaePerfilTipoMateria()
        {
            return _perfilPuesto_Repositorio.ListarMaePerfilTipoMateria();
        }

        public IEnumerable<PerfilTipoMateriaOtros_Response> ListarMaePerfilTipoMateriaOtros()
        {
            return _perfilPuesto_Repositorio.ListarMaePerfilTipoMateriaOtros();
        }

        public IEnumerable<PerfillNivelEducativo_Response> ListarNivelEducativo()
        {
            return _perfilPuesto_Repositorio.ListarNivelEducativo();
        }

        public IEnumerable<PerfilGrados_Response> ListarGradosBasico()
        {
            return _perfilPuesto_Repositorio.ListarGradosBasico();
        }

        public IEnumerable<PerfilGrados_Response> ListarGrados()
        {
            return _perfilPuesto_Repositorio.ListarGrados();
        }
        public IEnumerable<PerfilGrados_Response> ListarGradosTodos()
        {
            return _perfilPuesto_Repositorio.ListarGradosTodos();
        }
        public IEnumerable<PerfilCarrera_Response> ListarCarreraNivel1(int iCodTipoCarrera)
        {
            return _perfilPuesto_Repositorio.ListarCarreraNivel1(iCodTipoCarrera);
        }

        public IEnumerable<PerfilCarrera_Response> ListarCarreraNivel2(string vCodCarrera)
        {
            return _perfilPuesto_Repositorio.ListarCarreraNivel2(vCodCarrera);
        }

        public IEnumerable<PerfilCarrera_Response> ListarCarreraNivel3(string vCodCarrera)
        {
            return _perfilPuesto_Repositorio.ListarCarreraNivel3(vCodCarrera);
        }

        public IEnumerable<PerfilCarrera_Response> ListarCarreraNivel4(string vCodCarrera, string vDescripcion)
        {
            return _perfilPuesto_Repositorio.ListarCarreraNivel4(vCodCarrera, vDescripcion);
        }

        public IEnumerable<PerfilCarrera_Response> ListarCarreraNivel1_Mae(int iCodTipoCarrera)
        {
            return _perfilPuesto_Repositorio.ListarCarreraNivel1_Mae(iCodTipoCarrera);
        }

        public IEnumerable<PerfilCarrera_Response> ListarCarreraNivel2_Mae(string vCodCarrera)
        {
            return _perfilPuesto_Repositorio.ListarCarreraNivel2_Mae(vCodCarrera);
        }

        public IEnumerable<PerfilCarrera_Response> ListarCarreraNivel3_Mae(string vCodCarrera)
        {
            return _perfilPuesto_Repositorio.ListarCarreraNivel3_Mae(vCodCarrera);
        }

        public IEnumerable<PerfilCarrera_Response> ListarCarreraNivel4_Mae(string vCodCarrera, string vDescripcion)
        {
            return _perfilPuesto_Repositorio.ListarCarreraNivel4_Mae(vCodCarrera, vDescripcion);
        }

        public IEnumerable<PerfilCarrera_Response> ListarCarreraNivel1_Doc(int iCodTipoCarrera)
        {
            return _perfilPuesto_Repositorio.ListarCarreraNivel1_Doc(iCodTipoCarrera);
        }

        public IEnumerable<PerfilCarrera_Response> ListarCarreraNivel2_Doc(string vCodCarrera)
        {
            return _perfilPuesto_Repositorio.ListarCarreraNivel2_Doc(vCodCarrera);
        }

        public IEnumerable<PerfilCarrera_Response> ListarCarreraNivel3_Doc(string vCodCarrera)
        {
            return _perfilPuesto_Repositorio.ListarCarreraNivel3_Doc(vCodCarrera);
        }

        public IEnumerable<PerfilCarrera_Response> ListarCarreraNivel4_Doc(string vCodCarrera, string vDescripcion)
        {
            return _perfilPuesto_Repositorio.ListarCarreraNivel4_Doc(vCodCarrera, vDescripcion);
        }






        public IEnumerable<PerfilTipoSubMateriaOtros_Response> ListarMaePerfilTipoSubMateriaOtros(int iCodTipoMateriaOtros)
        {
            return _perfilPuesto_Repositorio.ListarMaePerfilTipoSubMateriaOtros(iCodTipoMateriaOtros);
        }

        public IEnumerable<PerfilCoordinaciones_Registro> ListarPerfilDetCoordinacionInterna(PerfilCoordinaciones_Registro peticion)
        {
            return _perfilPuesto_Repositorio.ListarPerfilDetCoordinacionInterna(peticion);
        }

        public IEnumerable<PerfilCoordinaciones_Registro> ListarPerfilDetCoordinacionExterna(PerfilCoordinaciones_Registro peticion)
        {
            return _perfilPuesto_Repositorio.ListarPerfilDetCoordinacionExterna(peticion);
        }

        public bool InsertarDetCoordinacionesInt_Ext(PerfilCoordinaciones_Registro peticion)
        {
            return _perfilPuesto_Repositorio.InsertarDetCoordinacionesInt_Ext(peticion);
        }

        public bool ActualizarDetCoordinacionesInt_Ext(PerfilCoordinaciones_Registro peticion)
        {
            return _perfilPuesto_Repositorio.ActualizarDetCoordinacionesInt_Ext(peticion);
        }

        public bool EliminarDetCoordinacionesInt_Ext(PerfilCoordinaciones_Registro peticion)
        {
            return _perfilPuesto_Repositorio.EliminarDetCoordinacionesInt_Ext(peticion);
        }

        public IEnumerable<PerfilConocimientos_Registro> ListarPerfilDetConocimientosTecnicos(PerfilConocimientos_Registro peticion)
        {
            return _perfilPuesto_Repositorio.ListarPerfilDetConocimientosTecnicos(peticion);
        }

        public IEnumerable<PerfilConocimientos_Registro> ListarPerfilDetConocimientosCursosProgramas(PerfilConocimientos_Registro peticion)
        {
            return _perfilPuesto_Repositorio.ListarPerfilDetConocimientosCursosProgramas(peticion);
        }

        public IEnumerable<PerfilConocimientos_Registro> ListarPerfilDetConocimientosOfficeIdiomas(PerfilConocimientos_Registro peticion)
        {
            return _perfilPuesto_Repositorio.ListarPerfilDetConocimientosOfficeIdiomas(peticion);
        }

        public bool InsertarDetConocimientos(PerfilConocimientos_Registro peticion)
        {
            return _perfilPuesto_Repositorio.InsertarDetConocimientos(peticion);
        }

        public bool ActualizarDetConocimientos(PerfilConocimientos_Registro peticion)
        {
            return _perfilPuesto_Repositorio.ActualizarDetConocimientos(peticion);
        }

        public bool EliminarDetConocimientos(PerfilConocimientos_Registro peticion)
        {
            return _perfilPuesto_Repositorio.EliminarDetConocimientos(peticion);
        }

        public IEnumerable<PerfilFormacionAcademica_Registro> ListarPerfilDetFormAcaNivelBasico(PerfilFormacionAcademica_Registro peticion)
        {
            return _perfilPuesto_Repositorio.ListarPerfilDetFormAcaNivelBasico(peticion);
        }

        public IEnumerable<PerfilFormacionAcademica_Registro> ListarPerfilDetFormAcaNivelEducativo(PerfilFormacionAcademica_Registro peticion)
        {
            return _perfilPuesto_Repositorio.ListarPerfilDetFormAcaNivelEducativo(peticion);
        }

        public IEnumerable<PerfilFormacionAcademica_Registro> ListarPerfilDetFormAcaMaestria(PerfilFormacionAcademica_Registro peticion)
        {
            return _perfilPuesto_Repositorio.ListarPerfilDetFormAcaMaestria(peticion);
        }

        public IEnumerable<PerfilFormacionAcademica_Registro> ListarPerfilDetFormAcaDoctorado(PerfilFormacionAcademica_Registro peticion)
        {
            return _perfilPuesto_Repositorio.ListarPerfilDetFormAcaDoctorado(peticion);
        }

        public bool InsertarDetFormacionAcademica(PerfilFormacionAcademica_Registro peticion)
        {
            return _perfilPuesto_Repositorio.InsertarDetFormacionAcademica(peticion);
        }
        public bool InsertarCarreraProfesional(PerfilFormacionAcademica_Registro peticion)
        {
            return _perfilPuesto_Repositorio.InsertarCarreraProfesional(peticion);
        }
        public bool InsertarMaestria(PerfilFormacionAcademica_Registro peticion)
        {
            return _perfilPuesto_Repositorio.InsertarMaestria(peticion);
        }
        public bool InsertarDoctorado(PerfilFormacionAcademica_Registro peticion)
        {
            return _perfilPuesto_Repositorio.InsertarDoctorado(peticion);
        }
        public bool ActualizarDetFormacionAcademica(PerfilFormacionAcademica_Registro peticion)
        {
            return _perfilPuesto_Repositorio.ActualizarDetFormacionAcademica(peticion);
        }

        public bool EliminarDetFormacionAcademica(PerfilFormacionAcademica_Registro peticion)
        {
            return _perfilPuesto_Repositorio.EliminarDetFormacionAcademica(peticion);
        }
        public bool PerfilEliminar(PerfilPuestoRegistro peticion)
        {
            return _perfilPuesto_Repositorio.PerfilEliminar(peticion);
        }

        public bool PerfilFinalizar(PerfilPuestoRegistro peticion)
        {
            return _perfilPuesto_Repositorio.PerfilFinalizar(peticion);
        }

        public bool PerfilDerivarUser(PerfilPuestoRegistro peticion)
        {
            return _perfilPuesto_Repositorio.PerfilDerivarUser(peticion);
        }

        public bool PerfilDerivarJefe(PerfilPuestoRegistro peticion)
        {
            return _perfilPuesto_Repositorio.PerfilDerivarJefe(peticion);
        }

        public bool PerfilDerivarJefeRRHH(PerfilPuestoRegistro peticion)
        {
            return _perfilPuesto_Repositorio.PerfilDerivarJefeRRHH(peticion);
        }

        public bool PerfilDerivarUserRRHH(PerfilPuestoRegistro peticion)
        {
            return _perfilPuesto_Repositorio.PerfilDerivarUserRRHH(peticion);
        }

        public bool PerfilDesaprobar(PerfilPuestoRegistro peticion)
        {
            return _perfilPuesto_Repositorio.PerfilDesaprobar(peticion);
        }

        public IEnumerable<PerfilPuestoRegistro> ListarPerfilHistorico(string id)
        {
            return _perfilPuesto_Repositorio.ListarPerfilHistorico(id);
        }

        public Int32 RegistrarPerfilArchivo(PerfilPuestoRegistro registro)
        {
            return _perfilPuesto_Repositorio.RegistrarPerfilArchivo(registro);
        }
    }
}
