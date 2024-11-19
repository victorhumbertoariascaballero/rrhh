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
#endregion

namespace MIDIS.ORI.LogicaNegocio
{
    public class T_genm_postulante_LN : BaseLN
    {
        private readonly T_genm_postulante_ODA _postulante_Repositorio = new T_genm_postulante_ODA();

        #region Métodos

        public IEnumerable<Empleado_Registro> ValidarPostulante(String strDocumento, String strTipoDocumento, String strVariable)
        {
            return _postulante_Repositorio.ValidarPostulante(strDocumento, strTipoDocumento, strVariable);
        }

        public IEnumerable<PostulanteInformacion_Registro> ListarPostulantes(Postulante_Request peticion)
        {
            return _postulante_Repositorio.ListarPostulantes(peticion);
        }
        public IEnumerable<PostulanteInformacion_Registro> ListarPostulantesGanadores(Postulante_Request peticion)
        {
            return _postulante_Repositorio.ListarPostulantesGanadores(peticion);
        }
        public IEnumerable<PostulanteFamiliar_Registro> ListarPostulanteFamiliares(Postulante_Request peticion)
        {
            return _postulante_Repositorio.ListarPostulanteFamiliares(peticion);
        }
        public IEnumerable<PostulanteDocumento_Registro> ListarPostulanteDocumento(Postulante_Request peticion)
        {
            return _postulante_Repositorio.ListarPostulanteDocumento(peticion);
        }
        public IEnumerable<PostulacionDocumento_Registro> ListarPostulacionDocumento(Postulante_Request peticion)
        {
            return _postulante_Repositorio.ListarPostulacionDocumento(peticion);
        }
        public IEnumerable<PostulacionDocumento_Registro> ListarPostulacionPracticaDocumento(Postulante_Request peticion)
        {
            return _postulante_Repositorio.ListarPostulacionPracticaDocumento(peticion);
        }
        public IEnumerable<PostulanteEstudio_Registro> ListarPostulanteEstudio(Postulante_Request peticion)
        {
            return _postulante_Repositorio.ListarPostulanteEstudio(peticion);
        }
        public IEnumerable<PostulacionEstudio_Registro> ListarPostulacionEstudio(Postulante_Request peticion)
        {
            return _postulante_Repositorio.ListarPostulacionEstudio(peticion);
        }
        public IEnumerable<PostulanteCapacitacion_Registro> ListarPostulanteCapacitacion(Postulante_Request peticion)
        {
            return _postulante_Repositorio.ListarPostulanteCapacitacion(peticion);
        }
        public IEnumerable<PostulacionCapacitacion_Registro> ListarPostulacionCapacitacion(Postulante_Request peticion)
        {
            return _postulante_Repositorio.ListarPostulacionCapacitacion(peticion);
        }
        public IEnumerable<PostulanteExperiencia_Registro> ListarPostulanteExperiencia(Postulante_Request peticion)
        {
            return _postulante_Repositorio.ListarPostulanteExperiencia(peticion);
        }
        public IEnumerable<PostulacionExperiencia_Registro> ListarPostulacionExperiencia(Postulante_Request peticion)
        {
            return _postulante_Repositorio.ListarPostulacionExperiencia(peticion);
        }

        public IEnumerable<PerfilFormacionAcademica_Registro> ListarPostulacionRequisitosEstudio(BasesPerfilPuesto_Request peticion)
        {
            return _postulante_Repositorio.ListarPostulacionRequisitosEstudio(peticion);
        }
        public IEnumerable<PerfilFormacionAcademica_Registro> ListarPostulacionRequisitosCapacitacion(BasesPerfilPuesto_Request peticion)
        {
            return _postulante_Repositorio.ListarPostulacionRequisitosCapacitacion(peticion);
        }
        
        public PostulanteInformacion_Registro ObtenerInformacionPostulante(Postulante_Request peticion)
        {
            PostulanteInformacion_Registro info = _postulante_Repositorio.ObtenerInformacionPostulante(peticion);
            //KMM
            //if (info.Sexo == "F") info.Sexo = "1";
            //if (info.Sexo == "M") info.Sexo = "2";

            if (!String.IsNullOrEmpty(info.FechaNacimiento)) { 
                DateTime aux;
                if (DateTime.TryParse(info.FechaNacimiento, out aux)) {
                    info.Edad = new TimeSpan(DateTime.Now.Ticks - aux.Ticks).Days / 365;
                }
            }
            return info;
        }
        public Postulante_Registro ObtenerPostulante(Postulante_Request peticion)
        {
            Postulante_Registro info = _postulante_Repositorio.ObtenerPostulante(peticion);
            if (info != null) {
                if (info.Sexo == "F") info.Sexo = "1";
                if (info.Sexo == "M") info.Sexo = "2";

                if (!String.IsNullOrEmpty(info.FechaNacimiento))
                {
                    DateTime aux;
                    if (DateTime.TryParse(info.FechaNacimiento, out aux))
                    {
                        info.Edad = new TimeSpan(DateTime.Now.Ticks - aux.Ticks).Days / 365;
                    }
                }
            }
            
            return info;
        }
        public Postulante_Registro ObtenerPostulantePersona(Postulante_Request peticion)
        {
            Postulante_Registro info = _postulante_Repositorio.ObtenerPostulantePersona(peticion);

            return info;
        }
        public PostulacionPostulante_Registro ObtenerPostulacionPostulante(Postulante_Request peticion)
        {
            PostulacionPostulante_Registro info = _postulante_Repositorio.ObtenerPostulacionPostulante(peticion);
            if (info != null)
            {
                if (info.Sexo == "F") info.Sexo = "1";
                if (info.Sexo == "M") info.Sexo = "2";

                if (!String.IsNullOrEmpty(info.FechaNacimiento))
                {
                    DateTime aux;
                    if (DateTime.TryParse(info.FechaNacimiento, out aux))
                    {
                        info.Edad = new TimeSpan(DateTime.Now.Ticks - aux.Ticks).Days / 365;
                    }
                }
            }

            return info;
        }
        public PostulacionPostulante_Registro ObtenerPostulacionPostulanteServir(Postulante_Request peticion)
        {
            PostulacionPostulante_Registro info = _postulante_Repositorio.ObtenerPostulacionPostulanteServir(peticion);
            if (info != null)
            {
                if (info.Sexo == "F") info.Sexo = "1";
                if (info.Sexo == "M") info.Sexo = "2";

                if (!String.IsNullOrEmpty(info.FechaNacimiento))
                {
                    DateTime aux;
                    if (DateTime.TryParse(info.FechaNacimiento, out aux))
                    {
                        info.Edad = new TimeSpan(DateTime.Now.Ticks - aux.Ticks).Days / 365;
                    }
                }
            }

            return info;
        }
        public PostulanteExperiencia_Registro ObtenerPostulanteExperiencia(Postulante_Registro peticion)
        {
            return _postulante_Repositorio.ObtenerPostulanteExperiencia(peticion);
        }
        public PostulanteCapacitacion_Registro ObtenerPostulanteCapacitacion(Postulante_Registro peticion)
        {
            return _postulante_Repositorio.ObtenerPostulanteCapacitacion(peticion);
        }
        public PostulanteEstudio_Registro ObtenerPostulanteEstudio(Postulante_Registro peticion)
        {
            return _postulante_Repositorio.ObtenerPostulanteEstudio(peticion);
        }
        //public PostulanteEstudio_Registro ObtenerPostulanteDocumento(Postulante_Registro peticion)
        //{
        //    return _postulante_Repositorio.ObtenerPostulanteDocumento(peticion);
        //}
        public IEnumerable<PostulantePostulacion_Registro> ListarPostulaciones(Postulante_Request peticion)
        {
            return _postulante_Repositorio.ListarPostulaciones(peticion);
        }
        public IEnumerable<PostulantePostulacion_Registro> ListarPostulacionesServir(Postulante_Request peticion)
        {
            return _postulante_Repositorio.ListarPostulacionesServir(peticion);
        }
        public IEnumerable<PostulanteNotificacion_Registro> ListarNotificaciones(Postulante_Request peticion)
        {
            return _postulante_Repositorio.ListarNotificaciones(peticion);
        }
        public Int32 GenerarCodigoPostulante()
        {
            return _postulante_Repositorio.GenerarCodigoPostulante();
        }
        public Int32 InsertarRegistroPostulante(PostulanteInformacion_Registro registro)
        {
            return _postulante_Repositorio.InsertarPostulanteFicha(registro);
        }
        public Int32 InsertarPostulante(Postulante_Registro registro)
        {
            return _postulante_Repositorio.InsertarPostulante(registro);
        }
        public Int32 ActualizarPostulante(Postulante_Registro registro)
        {
            return _postulante_Repositorio.ActualizarPostulante(registro);
        }
        public Int32 ActualizarRegistroPostulante(PostulanteInformacion_Registro registro)
        {
            return _postulante_Repositorio.ActualizarPostulanteFicha(registro);
        }
        public Int32 ActualizarPostulanteFichaDocumento(PostulanteInformacion_Registro registro)
        {
            return _postulante_Repositorio.ActualizarPostulanteFichaDocumento(registro);
        }
        public Int32 AprobarContratoPostulante(PostulanteInformacion_Registro registro)
        {
            return _postulante_Repositorio.AprobarContratoPostulante(registro);
        }
        public Int32 ValidarRegistroPostulante(PostulanteInformacion_Registro registro)
        {
            return _postulante_Repositorio.ValidarRegistroPostulante(registro);
        }
        public PostulanteInformacion_Registro ObtenerPostulanteFicha(PostulanteInformacion_Registro peticion)
        {
            return _postulante_Repositorio.ObtenerPostulanteFicha(peticion);
        }
        public PostulanteInformacion_Registro ObtenerPostulanteFichaDocumento(PostulanteInformacion_Registro peticion)
        {
            return _postulante_Repositorio.ObtenerPostulanteFichaDocumento(peticion);
        }
        public Postulante_Registro ObtenerPostulanteDocumento(Postulante_Registro peticion)
        {
            return _postulante_Repositorio.ObtenerPostulanteDocumento(peticion);
        }
        public Postulacion_Registro ObtenerPostulacionDocumento(Postulacion_Registro peticion)
        {
            return _postulante_Repositorio.ObtenerPostulacionDocumento(peticion);
        }
        public Int32 RegistrarPostulanteFamiliar(PostulanteFamiliar_Registro registro)
        {
            return _postulante_Repositorio.RegistrarPostulanteFamiliar(registro);
        }
        public Int32 RegistrarPostulanteDocumento(PostulanteDocumento_Registro registro)
        {
            if (registro.IdDocumento == 0)
                return _postulante_Repositorio.RegistrarPostulanteDocumento(registro);
            else
                return _postulante_Repositorio.ActualizarPostulanteDocumento(registro);
        }
        public Int32 RegistrarPostulanteEstudio(PostulanteEstudio_Registro registro)
        {
            if (registro.IdEstudio == 0)
                return _postulante_Repositorio.RegistrarPostulanteEstudio(registro);
            else
                return _postulante_Repositorio.ActualizarPostulanteEstudio(registro);
        }
        public Int32 RegistrarPostulanteCapacitacion(PostulanteCapacitacion_Registro registro)
        {
            if (registro.IdCapacitacion == 0)
                return _postulante_Repositorio.RegistrarPostulanteCapacitacion(registro);
            else
                return _postulante_Repositorio.ActualizarPostulanteCapacitacion(registro);
        }
        public Int32 RegistrarPostulanteExperiencia(PostulanteExperiencia_Registro registro)
        {
            if (registro.IdLaboral == 0)
                return _postulante_Repositorio.RegistrarPostulanteExperiencia(registro);
            else
                return _postulante_Repositorio.ActualizarPostulanteExperiencia(registro);
        }
        public Int32 RegistrarPostulanteNotificacion(PostulanteNotificacion_Registro registro)
        {
            return _postulante_Repositorio.RegistrarPostulanteNotificacion(registro);
        }
        public Int32 RegistrarPostulacionAnexo(PostulacionAnexo_Registro registro)
        {
            return _postulante_Repositorio.RegistrarPostulacionAnexo(registro);
        }
        
        public Int32 ActualizarPostulanteFamiliar(PostulanteFamiliar_Registro registro)
        {
            return _postulante_Repositorio.ActualizarPostulanteFamiliar(registro);
        }
        public Int32 EliminarPostulanteFamiliar(PostulanteFamiliar_Registro registro)
        {
            return _postulante_Repositorio.EliminarPostulanteFamiliar(registro);
        }

        public Int32 ActualizarPostulacionEstudio(PostulacionEstudio_Registro registro)
        {
            return _postulante_Repositorio.ActualizarPostulacionEstudio(registro);
        }
        public Int32 ActualizarPostulacionCapacitacion(PostulacionCapacitacion_Registro registro)
        {
            return _postulante_Repositorio.ActualizarPostulacionCapacitacion(registro);
        }
        public Int32 ActualizarPostulacionExperiencia(PostulacionExperiencia_Registro registro)
        {
            return _postulante_Repositorio.ActualizarPostulacionExperiencia(registro);
        }
        public Int32 EliminarPostulanteEstudio(PostulanteEstudio_Registro registro)
        {
            return _postulante_Repositorio.EliminarPostulanteEstudio(registro);
        }
        public Int32 EliminarPostulanteCapacitacion(PostulanteCapacitacion_Registro registro)
        {
            return _postulante_Repositorio.EliminarPostulanteCapacitacion(registro);
        }
        public Int32 EliminarPostulanteExperiencia(PostulanteExperiencia_Registro registro)
        {
            return _postulante_Repositorio.EliminarPostulanteExperiencia(registro);
        }
        public Int32 EliminarPostulanteDocumento(PostulanteDocumento_Registro registro)
        {
            return _postulante_Repositorio.EliminarPostulanteDocumento(registro);
        }

        public Int32 ActualizarRegistroPostulacion(Postulacion_Registro registro)
        {
            return _postulante_Repositorio.ActualizarRegistroPostulacion(registro);
        }
        public Int32 ActualizarRegistroPostulacionServir(Postulacion_Registro registro)
        {
            return _postulante_Repositorio.ActualizarRegistroPostulacionServir(registro);
        }
        public Int32 RegistrarPostulacionPractica(Postulacion_Registro registro)
        {
            return _postulante_Repositorio.RegistrarPostulacionPractica(registro);
        }

        #endregion

    }
}
