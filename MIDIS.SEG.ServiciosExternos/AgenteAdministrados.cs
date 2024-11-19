using MIDIS.Entidades;
using MIDIS.Utiles.REST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.SEG.ServiciosExternos
{
    public class AgenteAdministrados
    {

        protected log4net.ILog Log
        {
            get
            {
                return log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().GetType());
            }
        }

        public T_genm_persona Recuperar_T_genm_persona_Por_NroDocumento(int tipo, string nroDocumento)
        {
            T_genm_persona persona = new T_genm_persona();
            try
            {
                Dictionary<string, string> parametros = new Dictionary<string, string>();
                parametros.Add("tipo", tipo.ToString());
                parametros.Add("nroDocumento", nroDocumento);

                persona =
                    RESTClienteHelper.RecuperarREST<T_genm_persona>(
                 "Recuperar_T_genm_persona_Por_NroDocumento",
                 parametros,
                 VariablesServicioExternos.UrlServicioAdministrado,
                 true,
                 VariablesServicioExternos.UsuarioServicioAdministrado,
                 VariablesServicioExternos.ClaveServicioAdministrado);
            }
            catch (Exception ex)
            {
                persona.Id_persona = -1;
                persona.Nombre_completo = ex.Message;
                Log.Error(ex.Message, ex);
            }


            return persona;
        }

        public T_genm_persona RecuperarUsuarioPorCodigo(long Id_usuario)
        {

            T_genm_persona persona = new T_genm_persona();
            try
            {
                Dictionary<string, string> parametros = new Dictionary<string, string>();
                parametros.Add("Id_persona", Id_usuario.ToString());
                Log.Info("Ruta Servidor: " + VariablesServicioExternos.UrlServicioAdministrado);
                persona =
                    MIDIS.Utiles.REST.RESTClienteHelper.RecuperarREST
                    <MIDIS.Entidades.T_genm_persona>("Recuperar_T_genm_persona_PorCodigo", parametros,
                    VariablesServicioExternos.UrlServicioAdministrado,
                     true,
                 VariablesServicioExternos.UsuarioServicioAdministrado,
                 VariablesServicioExternos.ClaveServicioAdministrado);

            }
            catch (Exception ex)
            {
                persona.Id_persona = -1;
                persona.Nombre_completo = ex.Message;
                Log.Error(ex.Message, ex);
            }
            return persona;
        }
    }
}
