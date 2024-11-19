using System;
using System.Collections.Generic;
using MIDIS.ORI.Entidades.Auth;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using MIDIS.Utiles;

namespace MIDIS.SEG.AccesoDatosSQL
{
    public class DA_TAB_Usuario
    {
        public BE_TAB_Usuario Autenticar(BE_TAB_Usuario oParam)
        {
            BE_TAB_Usuario oIterador= null;
            try
            {
                using (SqlConnection cn = new SqlConnection(new Crypto().Desencriptar(ConfigurationManager.ConnectionStrings["AppContext"].ConnectionString)))
                {
                    cn.Open();
                    using (SqlCommand cm = new SqlCommand("EQUSUARIO.usp_Autenticar_Usuario", cn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 0;
                        cm.Parameters.AddWithValue("@pvUsuario", oParam.vUsuario);
                        cm.Parameters.AddWithValue("@pvPassword", oParam.vPassword);

                        using (SqlDataReader dr = cm.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                oIterador = new BE_TAB_Usuario();

                                oIterador.iCodUsuario = dr.IsDBNull(dr.GetOrdinal("iCodUsuario")) ?
                                                                    0 : dr.GetInt32(dr.GetOrdinal("iCodUsuario"));

                                oIterador.MessageCode = dr.IsDBNull(dr.GetOrdinal("vMessageCode")) ?
                                                                    "" : dr.GetString(dr.GetOrdinal("vMessageCode"));
                                oIterador.Message = dr.IsDBNull(dr.GetOrdinal("vMessage")) ?
                                                                    "" : dr.GetString(dr.GetOrdinal("vMessage"));
                                oIterador.oPersona = new BE_TAB_Persona();

                                oIterador.oPersona.iCodPersona = dr.IsDBNull(dr.GetOrdinal("iCodPersona")) ?
                                                                    0 : dr.GetInt32(dr.GetOrdinal("iCodPersona"));

                                oIterador.oPersona.vApePaterno = dr.IsDBNull(dr.GetOrdinal("vcApePaterno")) ?
                                                                    "" : dr.GetString(dr.GetOrdinal("vcApePaterno"));
                                oIterador.oPersona.vApeMaterno = dr.IsDBNull(dr.GetOrdinal("vcApeMaterno")) ?
                                                                    "" : dr.GetString(dr.GetOrdinal("vcApeMaterno"));
                                oIterador.oPersona.vNombres = dr.IsDBNull(dr.GetOrdinal("vcNombres")) ?
                                                                    "" : dr.GetString(dr.GetOrdinal("vcNombres"));
                                oIterador.oPersona.iCodCargo = dr.IsDBNull(dr.GetOrdinal("iCodCargo")) ?
                                                                    0 : dr.GetInt32(dr.GetOrdinal("iCodCargo"));
                                oIterador.oPersona.vCargo = dr.IsDBNull(dr.GetOrdinal("vcCargo")) ?
                                                                    "" : dr.GetString(dr.GetOrdinal("vcCargo"));

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oIterador;
        }

        public List<BE_TAB_Persona> ListarAbogados(BE_TAB_Persona oParam)
        {
            List<BE_TAB_Persona> lstPersona = new List<BE_TAB_Persona>();
            BE_TAB_Persona oIterador = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(new Crypto().Desencriptar(ConfigurationManager.ConnectionStrings["AppContext"].ConnectionString)))
                {
                    cn.Open();
                    using (SqlCommand cm = new SqlCommand("eqsispad.usp_Listar_Abogados", cn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.Parameters.AddWithValue("@iCodEntidad", oParam.iCodEntidad);
                        cm.CommandTimeout = 0;

                        using (SqlDataReader dr = cm.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                oIterador = new BE_TAB_Persona();

                                oIterador.iCodPersona = dr.IsDBNull(dr.GetOrdinal("iCodPersona")) ?
                                                                    0 : dr.GetInt32(dr.GetOrdinal("iCodPersona"));
                                oIterador.vNombres = dr.IsDBNull(dr.GetOrdinal("vNombreCompleto")) ?
                                                                    "" : dr.GetString(dr.GetOrdinal("vNombreCompleto"));
                                oIterador.iCodCargo = dr.IsDBNull(dr.GetOrdinal("iCodCargo")) ?
                                                                    0 : dr.GetInt32(dr.GetOrdinal("iCodCargo"));

                                oIterador.dtFecInicioLabores = dr.IsDBNull(dr.GetOrdinal("dtFecInicioLabores")) ?
                                                                   DateTime.MinValue : dr.GetDateTime(dr.GetOrdinal("dtFecInicioLabores"));

                                oIterador.dtFecFinLabores = dr.IsDBNull(dr.GetOrdinal("dtFecFinLabores")) ?
                                                                   DateTime.MinValue : dr.GetDateTime(dr.GetOrdinal("dtFecFinLabores"));

                                lstPersona.Add(oIterador);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstPersona;
        }

        public List<BE_TAB_Personal> ListarPersonal(BE_TAB_Persona oParam)
        {
            List<BE_TAB_Personal> lstPersona = new List<BE_TAB_Personal>();
            BE_TAB_Personal oIterador = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(new Crypto().Desencriptar(ConfigurationManager.ConnectionStrings["AppContext"].ConnectionString)))
                {
                    cn.Open();
                    using (SqlCommand cm = new SqlCommand("eqsispad.usp_Buscar_Trabajadores", cn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.Parameters.AddWithValue("@pvNombres", oParam.vNombres);

                        using (SqlDataReader dr = cm.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                oIterador = new BE_TAB_Personal();

                                oIterador.iCodPersonal = dr.IsDBNull(dr.GetOrdinal("iCodPersonal")) ?
                                                                    0 : dr.GetInt32(dr.GetOrdinal("iCodPersonal"));
                                oIterador.oPersona = new BE_TAB_Persona();

                                oIterador.oPersona.iCodPersona = dr.IsDBNull(dr.GetOrdinal("iCodPersona")) ?
                                                                    0 : dr.GetInt32(dr.GetOrdinal("iCodPersona"));
                                oIterador.oPersona.vNombres = dr.IsDBNull(dr.GetOrdinal("vNombreCompleto")) ?
                                                                    "" : dr.GetString(dr.GetOrdinal("vNombreCompleto"));
                                oIterador.oPersona.iCodCargo = dr.IsDBNull(dr.GetOrdinal("iCodCargo")) ?
                                                                    0 : dr.GetInt32(dr.GetOrdinal("iCodCargo"));

                                oIterador.oPersona.vCargo = dr.IsDBNull(dr.GetOrdinal("vCargo")) ?
                                                                    "" : dr.GetString(dr.GetOrdinal("vCargo"));

                                oIterador.oPersona.dtFecInicioLabores = dr.IsDBNull(dr.GetOrdinal("dtFecInicioLabores")) ?
                                                                   DateTime.MinValue : dr.GetDateTime(dr.GetOrdinal("dtFecInicioLabores"));

                                oIterador.oPersona.dtFecFinLabores = dr.IsDBNull(dr.GetOrdinal("dtFecFinLabores")) ?
                                                                   DateTime.MinValue : dr.GetDateTime(dr.GetOrdinal("dtFecFinLabores"));


                                lstPersona.Add(oIterador);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstPersona;
        }

        public BE_TAB_Usuario ObtenerDatosUsuario(BE_TAB_Usuario oParam)
        {
            BE_TAB_Usuario oIterador = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(new Crypto().Desencriptar(ConfigurationManager.ConnectionStrings["AppContext"].ConnectionString)))
                {
                    cn.Open();
                    using (SqlCommand cm = new SqlCommand("EQUSUARIO.usp_Obtener_Datos_Generales_Usuario", cn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 0;
                        cm.Parameters.AddWithValue("@pvUsuario", oParam.vUsuario);

                        using (SqlDataReader dr = cm.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                oIterador = new BE_TAB_Usuario();

                                oIterador.vUsuario = dr.IsDBNull(dr.GetOrdinal("vUsuario")) ?
                                                                    "" : dr.GetString(dr.GetOrdinal("vUsuario"));
                                oIterador.vEmail = dr.IsDBNull(dr.GetOrdinal("vEmail")) ?
                                                                    "" : dr.GetString(dr.GetOrdinal("vEmail"));
                                oIterador.oPersona = new BE_TAB_Persona();

                                oIterador.oPersona.cCodDocPersona = dr.IsDBNull(dr.GetOrdinal("cCodDocPersona")) ?
                                                                    "" : dr.GetString(dr.GetOrdinal("cCodDocPersona"));

                                oIterador.oPersona.vNombres = dr.IsDBNull(dr.GetOrdinal("vcNombres")) ?
                                                                    "" : dr.GetString(dr.GetOrdinal("vcNombres"));

                                oIterador.oPersona.vApePaterno = dr.IsDBNull(dr.GetOrdinal("vcApePaterno")) ?
                                                                    "" : dr.GetString(dr.GetOrdinal("vcApePaterno"));
                                oIterador.oPersona.vApeMaterno = dr.IsDBNull(dr.GetOrdinal("vcApeMaterno")) ?
                                                                    "" : dr.GetString(dr.GetOrdinal("vcApeMaterno"));

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oIterador;
        }
        public int GenerarToken(BE_TAB_Usuario oParam)
        {
            int regafectados = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(new Crypto().Desencriptar(ConfigurationManager.ConnectionStrings["AppContext"].ConnectionString)))
                {
                    cn.Open();
                    using (SqlCommand cm = new SqlCommand("EQUSUARIO.usp_Generar_Token", cn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.Parameters.AddWithValue("@pvUsuario", oParam.vUsuario);
                        cm.Parameters.AddWithValue("@pvToken", oParam.vToken);
                        regafectados = cm.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return regafectados;
        }

        public int ActualizarContrasena(BE_TAB_Usuario oParam)
        {
            int regafectados = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(new Crypto().Desencriptar(ConfigurationManager.ConnectionStrings["AppContext"].ConnectionString)))
                {
                    cn.Open();
                    using (SqlCommand cm = new SqlCommand("EQUSUARIO.usp_Cambiar_Contrasena", cn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 0;
                        cm.Parameters.AddWithValue("@pvUsuario", oParam.vUsuario);
                        cm.Parameters.AddWithValue("@pvToken", oParam.vToken);
                        cm.Parameters.AddWithValue("@pvPassword", oParam.vPassword);

                        regafectados = cm.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return regafectados;
        }
    }
}