/*----------------------------------------------------------------------------------------
ARCHIVO CLASE   : T_genm_usuarioLN

Objetivo: Clase referida a los métodos de Lógica de Negocio de la clase T_Sold_Solicitud_Archivo
Autor: Miguel Angel Salvador Paucar (MASP)
Fecha Creacion: 2015-09-03
Métodos: 
        Insertar_T_genm_usuario
        Actualizar_T_genm_usuario
        Anular_T_genm_usuario_PorCodigo
        Listar_T_genm_usuario()
        Recuperar_T_genm_usuario_PorCodigo
        ListarPaginado_T_genm_usuario

----------------------------------------------------------------------------------------*/
#region Espacio de Nombres
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIDIS.ORI.Entidades;
using MIDIS.SEG.AccesoDatosSQL;
using MIDIS.Utiles;
using MIDIS.ORI.LogicaNegocio.Base;
using System.Configuration;
using MIDIS.Autenticacion;
using MIDIS.ORI.Entidades.Core;
#endregion

namespace MIDIS.ORI.LogicaNegocio
{
    public class T_genm_convocatoria_LN : BaseLN
    {
        private readonly T_genm_convocatoria_ODA _convocatoria_Repositorio = new T_genm_convocatoria_ODA();

        #region Métodos

        public IEnumerable<Convocatoria_Registro> ListarConvocatoria(Convocatoria_Request peticion)
        {
            return _convocatoria_Repositorio.ListarConvocatoria(peticion);
        }
        public IEnumerable<PostulacionPostulante_Registro> ListarPostulantes(Convocatoria_Request peticion)
        {
            return _convocatoria_Repositorio.ListarPostulantes(peticion);
        }
        public IEnumerable<PostulacionPostulante_Registro> ListarPostulantesServir(Convocatoria_Request peticion)
        {
            return _convocatoria_Repositorio.ListarPostulantesServir(peticion);
        }
        public IEnumerable<PostulacionPostulante_Registro> ListarPostulantesPractica(Convocatoria_Request peticion)
        {
            return _convocatoria_Repositorio.ListarPostulantesPractica(peticion);
        }
        public IEnumerable<PostulacionEvaluacionCurricular_Registro> ListarPostulantesEvaluacionCurri(Convocatoria_Request peticion)
        {
            return _convocatoria_Repositorio.ListarPostulantesEvaluacionCurri(peticion);
        }
        public IEnumerable<PostulacionEvaluacionCurricular_Registro> ListarPostulantesServirEvaluacionCurri(Convocatoria_Request peticion)
        {
            return _convocatoria_Repositorio.ListarPostulantesServirEvaluacionCurri(peticion);
        }
        public IEnumerable<PostulacionEvaluacionCurricular_Registro> ListarPostulantesPracticaEvaluacionCurri(Convocatoria_Request peticion)
        {
            return _convocatoria_Repositorio.ListarPostulantesPracticaEvaluacionCurri(peticion);
        }
        public IEnumerable<PostulacionEvaluacionConocimiento_Registro> ListarPostulantesEvaluacionConocimiento(Convocatoria_Request peticion)
        {
            return _convocatoria_Repositorio.ListarPostulantesEvaluacionConocimiento(peticion);
        }
        public IEnumerable<PostulacionEvaluacionConocimiento_Registro> ListarPostulantesServirEvaluacionConocimiento(Convocatoria_Request peticion)
        {
            return _convocatoria_Repositorio.ListarPostulantesServirEvaluacionConocimiento(peticion);
        }
        public IEnumerable<PostulacionEntrevistaPersonal_Registro> ListarPostulantesEntrevistaPersonal(Convocatoria_Request peticion)
        {
            return _convocatoria_Repositorio.ListarPostulantesEntrevistaPersonal(peticion);
        }
        public IEnumerable<PostulacionEntrevistaPersonal_Registro> ListarPostulantesPracticaEntrevistaPersonal(Convocatoria_Request peticion)
        {
            return _convocatoria_Repositorio.ListarPostulantesPracticaEntrevistaPersonal(peticion);
        }
        public IEnumerable<PostulacionResultadoTotal_Registro> ListarPostulantesResultadosTotales(Convocatoria_Request peticion)
        {
            List<PostulacionResultadoTotal_Registro> lista = _convocatoria_Repositorio.ListarPostulantesResultadosTotales(peticion).ToList();
            Convocatoria_Registro obj = _convocatoria_Repositorio.ObtenerParaEditar(peticion);
            foreach (PostulacionResultadoTotal_Registro item in lista) {
                if (obj.IdTieneExamenConoc == 1)
                {
                    if (item.PuntajeEntrevista >= 30)
                        item.AptoEntrevista = 1;
                    else
                        item.AptoEntrevista = 0;
                }
                else
                {
                    if (item.PuntajeEntrevista >= 35)
                        item.AptoEntrevista = 1;
                    else
                        item.AptoEntrevista = 0;
                }


                if (item.BonifFFAA == 1 && item.AptoEntrevista == 1) {
                    item.PuntajeBonificacion = Math.Round(item.PuntajeTotal * (Decimal)0.1, 0);
                    item.PuntajeTotal += item.PuntajeBonificacion;
                }
                if (item.BonifDiscapacidad == 1 && item.AptoEntrevista == 1) {
                    item.PuntajeBonificacion = Math.Round(item.PuntajeTotal * (Decimal)0.15, 0);
                    item.PuntajeTotal += Math.Round(item.PuntajeTotal * (Decimal)0.15, 0);
                }
                    

                if (obj.IdTieneExamenConoc == 1)
                {
                    if (item.PuntajeTotal >= 75 && item.AptoCurricular == 1 && item.AptoConocimiento == 1 && item.AptoEntrevista == 1)
                        item.AptoGanador = 1;
                    else
                        item.AptoGanador = 0;
                }
                else {
                    if (item.PuntajeTotal >= 80 && item.AptoCurricular == 1 && item.AptoEntrevista == 1)
                        item.AptoGanador = 1;
                    else
                        item.AptoGanador = 0;
                }
            }
            return lista;
        }
        public IEnumerable<PostulacionResultadoTotal_Registro> ListarPostulantesPracticaResultadosTotales(Convocatoria_Request peticion)
        {
            List<PostulacionResultadoTotal_Registro> lista = _convocatoria_Repositorio.ListarPostulantesPracticaResultadosTotales(peticion).ToList();
            Convocatoria_Registro obj = _convocatoria_Repositorio.ObtenerPracticaParaEditar(peticion);
            foreach (PostulacionResultadoTotal_Registro item in lista)
            {
                
                if (item.PuntajeEntrevista >= 24)
                    item.AptoEntrevista = 1;
                else
                    item.AptoEntrevista = 0;
                
                if (item.BonifFFAA == 1 && item.AptoEntrevista == 1)
                {
                    item.PuntajeBonificacion = Math.Round(item.PuntajeTotal * (Decimal)0.1, 0);
                    item.PuntajeTotal += item.PuntajeBonificacion;
                }
                if (item.BonifDiscapacidad == 1 && item.AptoEntrevista == 1)
                {
                    item.PuntajeBonificacion = Math.Round(item.PuntajeTotal * (Decimal)0.15, 0);
                    item.PuntajeTotal += Math.Round(item.PuntajeTotal * (Decimal)0.15, 0);
                }

                if (item.PuntajeTotal >= 84 && item.AptoCurricular == 1 && item.AptoEntrevista == 1)
                    item.AptoGanador = 1;
                else
                    item.AptoGanador = 0;
                
            }
            return lista;
        }
        public IEnumerable<PostulacionEntrevistaPersonalPregunta_Registro> ListarEntrevistaPersonalPreguntas(PostulacionEvaluacionEntrevista_Registro peticion)
        {
            return _convocatoria_Repositorio.ListarEntrevistaPersonalPreguntas(peticion);
        }
        public IEnumerable<PostulacionEntrevistaPersonalPregunta_Registro> ListarEntrevistaPersonalPreguntasPractica(PostulacionEvaluacionEntrevista_Registro peticion)
        {
            return _convocatoria_Repositorio.ListarEntrevistaPersonalPreguntasPractica(peticion);
        }
        public IEnumerable<PostulacionEntrevistaPersonalPregunta_Registro> ListarEntrevistaPracticaPreguntasMaestras(PostulacionEvaluacionEntrevista_Registro peticion)
        {
            return _convocatoria_Repositorio.ListarEntrevistaPracticaPreguntasMaestras(peticion);
        }
        public IEnumerable<PostulacionEntrevistaPersonalPregunta_Registro> ListarEntrevistaPreguntasMaestras(PostulacionEvaluacionEntrevista_Registro peticion)
        {
            return _convocatoria_Repositorio.ListarEntrevistaPreguntasMaestras(peticion);
        }
        public Int32 IniciarEvaluacionCurri(Convocatoria_Request peticion)
        {
            return _convocatoria_Repositorio.IniciarEvaluacionCurri(peticion);
        }
        public Int32 IniciarEvaluacionCurriPractica(Convocatoria_Request peticion)
        {
            return _convocatoria_Repositorio.IniciarEvaluacionCurriPractica(peticion);
        }
        public Int32 IniciarEvaluacionConocimiento(Convocatoria_Request peticion)
        {
            return _convocatoria_Repositorio.IniciarEvaluacionConocimiento(peticion);
        }
        public String ActualizarEvaluacionCurri(PostulacionEvaluacionCurricular_Registro registro)
        {
            return _convocatoria_Repositorio.ActualizarEvaluacionCurri(registro);
        }
        public String ActualizarServirEvaluacionCurri(PostulacionEvaluacionCurricular_Registro registro)
        {
            return _convocatoria_Repositorio.ActualizarServirEvaluacionCurri(registro);
        }
        public String ActualizarPracticaEvaluacionCurri(PostulacionEvaluacionCurricular_Registro registro)
        {
            return _convocatoria_Repositorio.ActualizarPracticaEvaluacionCurri(registro);
        }
        public Int32 ActualizarEvaluacionConocimiento(PostulacionEvaluacionConocimiento_Registro registro)
        {
            return _convocatoria_Repositorio.ActualizarEvaluacionConocimiento(registro);
        }
        public Int32 ActualizarEvaluacionCurriObs(PostulacionEvaluacionCurricular_Registro registro)
        {
            return _convocatoria_Repositorio.ActualizarEvaluacionCurriObs(registro);
        }
        public Int32 ActualizarPracticaEvaluacionCurriObs(PostulacionEvaluacionCurricular_Registro registro)
        {
            return _convocatoria_Repositorio.ActualizarPracticaEvaluacionCurriObs(registro);
        }
        public Int32 ActualizarEvaluacionNSP(PostulacionEvaluacionEntrevista_Registro registro)
        {
            return _convocatoria_Repositorio.ActualizarEvaluacionNSP(registro);
        }
        public Int32 ActualizarEvaluacionPracticaNSP(PostulacionEvaluacionEntrevista_Registro registro)
        {
            return _convocatoria_Repositorio.ActualizarEvaluacionPracticaNSP(registro);
        }
        public Int32 ActualizarEvaluacionEntrevistaPersonal(PostulacionEvaluacionEntrevista_Registro registro)
        {
            return _convocatoria_Repositorio.ActualizarEvaluacionEntrevistaPersonal(registro);
        }
        public Int32 ActualizarEvaluacionPracticaEntrevistaPersonal(PostulacionEvaluacionEntrevista_Registro registro)
        {
            return _convocatoria_Repositorio.ActualizarEvaluacionPracticaEntrevistaPersonal(registro);
        }
        public Int32 ActualizarActaEntrevistaPersonal(PostulacionEvaluacionEntrevista_Registro registro)
        {
            return _convocatoria_Repositorio.ActualizarActaEntrevistaPersonal(registro);
        }
        public Int32 ActualizarActaEntrevistaPersonalPractica(PostulacionEvaluacionEntrevista_Registro registro)
        {
            return _convocatoria_Repositorio.ActualizarActaEntrevistaPersonalPractica(registro);
        }
        public Int32 RegistrarConvocatoria(Convocatoria_Registro registro)
        {
            Int32 iNuevo = 0;
            if (registro.IdTipo == 1)
                iNuevo = _convocatoria_Repositorio.RegistrarConvocatoria(registro);
            if (registro.IdTipo == 2)
                iNuevo = _convocatoria_Repositorio.RegistrarConvocatoriaPractica(registro);
            if (registro.IdTipo == 3)
                iNuevo = _convocatoria_Repositorio.RegistrarConvocatoriaServir(registro);

            return iNuevo;
        }
        public Int32 RegistrarConvocatoriaDocumentoArchivo(ConvocatoriaDocumento_Registro registro)
        {
            return _convocatoria_Repositorio.RegistrarConvocatoriaDocumentoArchivo(registro);
        }
        public Int32 RegistrarConvocatoriaPracticaDocumentoArchivo(ConvocatoriaDocumento_Registro registro)
        {
            return _convocatoria_Repositorio.RegistrarConvocatoriaPracticaDocumentoArchivo(registro);
        }
        public Int32 RegistrarEntrevistaPersonalPregunta(PostulacionEntrevistaPersonalPregunta_Registro registro)
        {
            return _convocatoria_Repositorio.RegistrarEntrevistaPersonalPregunta(registro);
        }
        public Int32 RegistrarEntrevistaPersonalPreguntaPractica(PostulacionEntrevistaPersonalPregunta_Registro registro)
        {
            return _convocatoria_Repositorio.RegistrarEntrevistaPersonalPreguntaPractica(registro);
        }
        public Int32 RegistrarPreguntaMaestraPractica(PostulacionEntrevistaPersonalPregunta_Registro registro)
        {
            return _convocatoria_Repositorio.RegistrarPreguntaMaestraPractica(registro);
        }
        public Int32 RegistrarPreguntaMaestra(PostulacionEntrevistaPersonalPregunta_Registro registro)
        {
            return _convocatoria_Repositorio.RegistrarPreguntaMaestra(registro);
        }
        public Int32 ActualizarConvocatoria(Convocatoria_Registro registro)
        {
            return _convocatoria_Repositorio.ActualizarConvocatoria(registro);
        }
        public Int32 ActualizarConvocatoriaPractica(Convocatoria_Registro registro)
        {
            return _convocatoria_Repositorio.ActualizarConvocatoriaPractica(registro);
        }
        public Int32 ActualizarEntrevistaPersonalPregunta(PostulacionEntrevistaPersonalPregunta_Registro registro)
        {
            return _convocatoria_Repositorio.ActualizarEntrevistaPersonalPregunta(registro);
        }
        public Int32 ActualizarEntrevistaPersonalPreguntaPractica(PostulacionEntrevistaPersonalPregunta_Registro registro)
        {
            return _convocatoria_Repositorio.ActualizarEntrevistaPersonalPreguntaPractica(registro);
        }
        public Int32 ActualizarPreguntaMaestraPractica(PostulacionEntrevistaPersonalPregunta_Registro registro)
        {
            return _convocatoria_Repositorio.ActualizarPreguntaMaestraPractica(registro);
        }
        public Int32 ActualizarPreguntaMaestra(PostulacionEntrevistaPersonalPregunta_Registro registro)
        {
            return _convocatoria_Repositorio.ActualizarPreguntaMaestra(registro);
        }
        public Int32 RegistrarConvocatoriaComiteEntrevista(List<ConvocatoriaComite_Registro> registro)
        {
            return _convocatoria_Repositorio.RegistrarConvocatoriaComiteEntrevista(registro);
        }
        public Int32 LimpiarEvaluacionEntrevista(PostulacionEvaluacionEntrevista_Registro registro)
        {
            return _convocatoria_Repositorio.LimpiarEvaluacionEntrevista(registro);
        }
        public Int32 DeclararAccesitarioGanador(PostulacionEvaluacionEntrevista_Registro registro)
        {
            return _convocatoria_Repositorio.DeclararAccesitarioGanador(registro);
        }
        public Int32 RegistrarConvocatoriaPracticaComiteEntrevista(List<ConvocatoriaComite_Registro> registro)
        {
            return _convocatoria_Repositorio.RegistrarConvocatoriaPracticaComiteEntrevista(registro);
        }
        public Convocatoria_Registro ObtenerParaEditar(Convocatoria_Request peticion)
        {
            return _convocatoria_Repositorio.ObtenerParaEditar(peticion);
        }
        public Convocatoria_Registro ObtenerServirParaEditar(Convocatoria_Request peticion)
        {
            return _convocatoria_Repositorio.ObtenerServirParaEditar(peticion);
        }
        public Convocatoria_Registro ObtenerPracticaParaEditar(Convocatoria_Request peticion)
        {
            return _convocatoria_Repositorio.ObtenerPracticaParaEditar(peticion);
        }
        public Convocatoria_Registro ObtenerConvocatoriaDocumento(Convocatoria_Request peticion)
        {
            return _convocatoria_Repositorio.ObtenerConvocatoriaDocumento(peticion);
        }
        public Convocatoria_Registro ObtenerConvocatoriaServirDocumento(Convocatoria_Request peticion)
        {
            return _convocatoria_Repositorio.ObtenerConvocatoriaServirDocumento(peticion);
        }
        public Convocatoria_Registro ObtenerConvocatoriaPracticaDocumento(Convocatoria_Request peticion)
        {
            return _convocatoria_Repositorio.ObtenerConvocatoriaPracticaDocumento(peticion);
        }
        public Convocatoria_Registro ObtenerConvocatoriaDocumentoPorId(Convocatoria_Request peticion)
        {
            return _convocatoria_Repositorio.ObtenerConvocatoriaDocumentoPorId(peticion);
        }
        public Convocatoria_Registro ObtenerConvocatoriaServirDocumentoPorId(Convocatoria_Request peticion)
        {
            return _convocatoria_Repositorio.ObtenerConvocatoriaServirDocumentoPorId(peticion);
        }
        public Convocatoria_Registro ObtenerConvocatoriaPracticaDocumentoPorId(Convocatoria_Request peticion)
        {
            return _convocatoria_Repositorio.ObtenerConvocatoriaPracticaDocumentoPorId(peticion);
        }
        public Convocatoria_Registro ObtenerConvocatoriaDocumentoEntrevista(Convocatoria_Request peticion)
        {
            return _convocatoria_Repositorio.ObtenerConvocatoriaDocumentoEntrevista(peticion);
        }
        public Convocatoria_Registro ObtenerConvocatoriaPracticaDocumentoEntrevista(Convocatoria_Request peticion)
        {
            return _convocatoria_Repositorio.ObtenerConvocatoriaPracticaDocumentoEntrevista(peticion);
        }
        public Postulacion_Registro ObtenerInformacionPostulacion(Postulante_Request peticion)
        {
            return _convocatoria_Repositorio.ObtenerInformacionPostulacion(peticion);
        }
        public Postulacion_Registro ObtenerInformacionPostulacionServir(Postulante_Request peticion)
        {
            return _convocatoria_Repositorio.ObtenerInformacionPostulacionServir(peticion);
        }
        public PostulacionEvaluacionEntrevista_Registro ObtenerInformacionEntrevistaPersonal(PostulacionEvaluacionEntrevista_Registro peticion)
        {
            return _convocatoria_Repositorio.ObtenerInformacionEntrevistaPersonal(peticion);
        }
        public PostulacionEvaluacionEntrevista_Registro ObtenerInformacionEntrevistaPersonalPractica(PostulacionEvaluacionEntrevista_Registro peticion)
        {
            return _convocatoria_Repositorio.ObtenerInformacionEntrevistaPersonalPractica(peticion);
        }
        public Postulacion_Registro InsertarRegistroPostulacion(Postulante_Request registro)
        {
            return _convocatoria_Repositorio.InsertarRegistroPostulacion(registro);
        }
        public Postulacion_Registro InsertarRegistroPostulacionServir(Postulante_Request registro)
        {
            return _convocatoria_Repositorio.InsertarRegistroPostulacionServir(registro);
        }
        public PostulacionAnexo_Registro ObtenerPostulacionAnexo(Postulacion_Registro peticion)
        {
            return _convocatoria_Repositorio.ObtenerPostulacionAnexo(peticion);
        }
        public PostulacionAnexo_Registro ObtenerPostulacionAnexoServir(Postulacion_Registro peticion)
        {
            return _convocatoria_Repositorio.ObtenerPostulacionAnexoServir(peticion);
        }
        public Postulacion_Registro ListarPostulacionRequisitos(Postulacion_Registro peticion)
        {
            return _convocatoria_Repositorio.ListarPostulacionRequisitos(peticion);
        }

        public IEnumerable<ConvocatoriaComite_Registro> ListarConvocatoriaComite(Convocatoria_Request peticion)
        {
            return _convocatoria_Repositorio.ListarConvocatoriaComite(peticion);
        }
        public IEnumerable<ConvocatoriaComite_Registro> ListarConvocatoriaServirComite(Convocatoria_Request peticion)
        {
            return _convocatoria_Repositorio.ListarConvocatoriaServirComite(peticion);
        }
        public IEnumerable<ConvocatoriaComite_Registro> ListarConvocatoriaPracComite(Convocatoria_Request peticion)
        {
            return _convocatoria_Repositorio.ListarConvocatoriaPracComite(peticion);
        }
        public IEnumerable<ConvocatoriaDocumento_Registro> ListarConvocatoriaDocumento(Convocatoria_Request peticion)
        {
            return _convocatoria_Repositorio.ListarConvocatoriaDocumento(peticion);
        }
        public IEnumerable<ConvocatoriaComite_Registro> ListarConvocatoriaTipoDocumento(Convocatoria_Request peticion)
        {
            return _convocatoria_Repositorio.ListarConvocatoriaTipoDocumento(peticion);
        }
        public Int32 EliminarComunicado(ConvocatoriaDocumento_Registro peticion)
        {
            return _convocatoria_Repositorio.EliminarComunicado(peticion);
        }
        public Int32 EliminarComunicadoServir(ConvocatoriaDocumento_Registro peticion)
        {
            return _convocatoria_Repositorio.EliminarComunicadoServir(peticion);
        }
        public Int32 EliminarComunicadoPractica(ConvocatoriaDocumento_Registro peticion)
        {
            return _convocatoria_Repositorio.EliminarComunicadoPractica(peticion);
        }

        public Int32 IniciarEntrevistaPracticaPreguntasMaestras(PostulacionEntrevistaPersonal_Registro peticion)
        {
            return _convocatoria_Repositorio.IniciarEntrevistaPracticaPreguntasMaestras(peticion);
        }
        public Int32 IniciarEntrevistaPreguntasMaestras(PostulacionEntrevistaPersonal_Registro peticion)
        {
            return _convocatoria_Repositorio.IniciarEntrevistaPreguntasMaestras(peticion);
        }

        public IEnumerable<Convocatoria_Historica> ListarConvocatoria_Historica(Convocatoria_Request peticion)
        {
            return _convocatoria_Repositorio.ListarConvocatoria_Historica(peticion);
        }

        public IEnumerable<ConvocatoriaDocumento_Registro> ListarConvocatoriaDocumento_Historica(Convocatoria_Request peticion)
        {
            return _convocatoria_Repositorio.ListarConvocatoriaDocumento_Historica(peticion);
        }
        #endregion

    }
}
