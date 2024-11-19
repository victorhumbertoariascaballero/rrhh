using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using MIDIS.ORI.Entidades.Auth;
using MIDIS.Utiles;

namespace MIDIS.SEG.AccesoDatosSQL
{
    public class DA_Authentication
    {
        public string verificarTokenUso(string token)
        {
            String status_code = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(new Crypto().Desencriptar(ConfigurationManager.ConnectionStrings["AppContext"].ConnectionString)))
                {
                    cn.Open();
                    using (SqlCommand cm = new SqlCommand("eqgeneral.pa_Verificar_Token_Uso", cn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.Parameters.AddWithValue("@pvToken", token);

                        using (SqlDataReader dr = cm.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                               
                                status_code = dr.IsDBNull(dr.GetOrdinal("vMessageCode")) ? "" : dr.GetString(dr.GetOrdinal("vMessageCode"));

                            }
                        }
                    }
                    cn.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return status_code;
        }

        //metodo copiado de servicio de proveedores e iiees,
        public bool verificarPermisosUsuario(string operation, string username)
        {
            bool status_code = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(new Crypto().Desencriptar(ConfigurationManager.ConnectionStrings["AppContext"].ConnectionString)))
                {
                    cn.Open();
                    using (SqlCommand cm = new SqlCommand("eqgeneral.pa_Verificar_permiso_usuario", cn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.Parameters.AddWithValue("@pusername", username);
                        cm.Parameters.AddWithValue("@poperation", operation);

                        using (SqlDataReader dr = cm.ExecuteReader())
                        {
                            while (dr.Read())
                            {

                                status_code = dr.IsDBNull(dr.GetOrdinal("habilitado")) ?false : dr.GetBoolean(dr.GetOrdinal("habilitado"));

                            }
                        }
                    }
                    cn.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return status_code;
        }

        public BE_EncryptionAttribs obtenerDatosTipoEncriptacion(BE_EncryptionAttribs encriptionReq)
        {
            BE_EncryptionAttribs responseObj = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(new Crypto().Desencriptar(ConfigurationManager.ConnectionStrings["AppContext"].ConnectionString)))
                {
                    cn.Open();
                    using (SqlCommand cm = new SqlCommand("EQUSUARIO.pa_Obtener_Datos_TipoEncriptacion", cn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.Parameters.AddWithValue("@pvTipoEncriptacion", encriptionReq.vTipoEncriptacion);

                        using (SqlDataReader dr = cm.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                responseObj = new BE_EncryptionAttribs();

                                responseObj.vTipoEncriptacion = dr.IsDBNull(dr.GetOrdinal("vTipoEncriptacion")) ? "" : dr.GetString(dr.GetOrdinal("vTipoEncriptacion"));

                                responseObj.vClaveEncriptacion = dr.IsDBNull(dr.GetOrdinal("vClaveEncriptacion")) ? "" : dr.GetString(dr.GetOrdinal("vClaveEncriptacion"));
                               
                                responseObj.vbSalt = dr.IsDBNull(dr.GetOrdinal("vbSaltSize")) ? null :  (byte[])dr.GetValue(dr.GetOrdinal("vbSalt"));
                                responseObj.vbSaltSize = dr.IsDBNull(dr.GetOrdinal("vbSaltSize")) ? 0 : dr.GetInt32(dr.GetOrdinal("vbSaltSize"));
                                responseObj.iKeygenIterations = dr.IsDBNull(dr.GetOrdinal("iKeygenIterations")) ? 0 : dr.GetInt32(dr.GetOrdinal("iKeygenIterations"));
                                responseObj.iKeySizeInBits = dr.IsDBNull(dr.GetOrdinal("iKeySizeInBits")) ? 0 : dr.GetInt32(dr.GetOrdinal("iKeySizeInBits"));

                            }
                        }
                    }
                    cn.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return responseObj;
        }

    }
}