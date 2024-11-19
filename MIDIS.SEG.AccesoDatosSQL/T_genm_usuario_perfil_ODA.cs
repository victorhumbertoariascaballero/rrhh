/*----------------------------------------------------------------------------------------
ARCHIVO CLASE   : T_genm_usuario_perfil_ODA

Objetivo: Clase referida a los métodos de Acceso a datos de la clase T_genm_usuario_perfil
Autor: Miguel Angel Salvador Paucar (MASP)
Fecha Creacion: 2015-09-03
Metodos: 
        Insertar_T_genm_usuario_perfil
        Actualizar_T_genm_usuario_perfil
        Listar_T_genm_usuario_perfil
        Anular_T_genm_usuario_perfil_PorCodigo
        Recuperar_T_genm_usuario_perfil_PorCodigo
        ListarPaginado_T_genm_usuario_perfil

----------------------------------------------------------------------------------------*/
#region Espacio de Nombres
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using MIDIS.ORI.Entidades;
#endregion

namespace MIDIS.SEG.AccesoDatosSQL
{
    public partial class T_genm_usuario_perfil_ODA
    {

        #region Metodos Generales

        string vSqlString = new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnOracle"]].ConnectionString);

        public T_genm_usuario_perfil Insertar_T_genm_usuario_perfil(T_genm_usuario_perfil t_genm_usuario_perfil)
        {
            using (SqlConnection vCnn = new SqlConnection(vSqlString))
            {
                using (SqlCommand vCmd = new SqlCommand("[SEGURIDAD].[up_InsertarUsuarioPerfil]", vCnn))
                {
                    vCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    vCmd.Parameters.AddWithValue("@pId_usuario_perfil", t_genm_usuario_perfil.Id_usuario_perfil);
                    vCmd.Parameters.AddWithValue("@pId_usuario", t_genm_usuario_perfil.Id_usuario);
                    vCmd.Parameters.AddWithValue("@pId_perfil", t_genm_usuario_perfil.Id_perfil);
                    vCmd.Parameters.AddWithValue("@pIp_ingreso", t_genm_usuario_perfil.Ip_ingreso);
                    vCmd.Parameters.AddWithValue("@pId_usuario_ingresa", t_genm_usuario_perfil.Id_usuario_ingresa);
                    vCmd.Parameters.AddWithValue("@pUsu_ingresa", t_genm_usuario_perfil.Usu_ingresa);
                    vCmd.Parameters.AddWithValue("@pFec_ingreso", t_genm_usuario_perfil.Fec_ingreso);
                    vCmd.Parameters.AddWithValue("@pIp_modifica", t_genm_usuario_perfil.Ip_modifica);
                    vCmd.Parameters.AddWithValue("@pId_usuario_modifica", t_genm_usuario_perfil.Id_usuario_modifica);
                    vCmd.Parameters.AddWithValue("@pUsu_modifica", t_genm_usuario_perfil.Usu_modifica);
                    vCmd.Parameters.AddWithValue("@pFec_modifica", t_genm_usuario_perfil.Fec_modifica);
                    vCmd.Parameters.AddWithValue("@pFlg_estado", t_genm_usuario_perfil.Flg_estado);
                    vCnn.Open();
                    vCmd.ExecuteNonQuery();
                    vCnn.Close();
                }
            }
            return t_genm_usuario_perfil;
        }

        public T_genm_usuario_perfil Actualizar_T_genm_usuario_perfil(T_genm_usuario_perfil t_genm_usuario_perfil)
        {
            using (SqlConnection vCnn = new SqlConnection(vSqlString))
            {
                using (SqlCommand vCmd = new SqlCommand("[SEGURIDAD].[up_ActualizarUsuarioPerfil]", vCnn))
                {
                    vCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    vCmd.Parameters.AddWithValue("@pId_usuario_perfil", t_genm_usuario_perfil.Id_usuario_perfil);
                    vCmd.Parameters.AddWithValue("@pId_usuario", t_genm_usuario_perfil.Id_usuario);
                    vCmd.Parameters.AddWithValue("@pId_perfil", t_genm_usuario_perfil.Id_perfil);
                    vCmd.Parameters.AddWithValue("@pIp_ingreso", t_genm_usuario_perfil.Ip_ingreso);
                    vCmd.Parameters.AddWithValue("@pId_usuario_ingresa", t_genm_usuario_perfil.Id_usuario_ingresa);
                    vCmd.Parameters.AddWithValue("@pUsu_ingresa", t_genm_usuario_perfil.Usu_ingresa);
                    vCmd.Parameters.AddWithValue("@pFec_ingreso", t_genm_usuario_perfil.Fec_ingreso);
                    vCmd.Parameters.AddWithValue("@pIp_modifica", t_genm_usuario_perfil.Ip_modifica);
                    vCmd.Parameters.AddWithValue("@pId_usuario_modifica", t_genm_usuario_perfil.Id_usuario_modifica);
                    vCmd.Parameters.AddWithValue("@pUsu_modifica", t_genm_usuario_perfil.Usu_modifica);
                    vCmd.Parameters.AddWithValue("@pFec_modifica", t_genm_usuario_perfil.Fec_modifica);
                    vCmd.Parameters.AddWithValue("@pFlg_estado", t_genm_usuario_perfil.Flg_estado);
                    vCnn.Open();
                    vCmd.ExecuteNonQuery();

                    vCnn.Close();
                }
            }
            return t_genm_usuario_perfil;
        }

        public List<T_genm_usuario_perfil> Listar_T_genm_usuario_perfil()
        {
            List<T_genm_usuario_perfil> listaEntidad = new List<T_genm_usuario_perfil>();
            T_genm_usuario_perfil entidad = null;

            using (SqlConnection vCnn = new SqlConnection(vSqlString))
            {
                using (SqlCommand vCmd = new SqlCommand("[SEGURIDAD].[up_ListarUsuarioPerfil]", vCnn))
                {
                    vCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //vCmd.Parameters.Add("c_Cursor", SqlDbType.RefCursor).Direction = ParameterDirection.Output;
                    vCnn.Open();
                    SqlDataReader reader = vCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        entidad = new T_genm_usuario_perfil(reader);
                        listaEntidad.Add(entidad);
                    }
                }
                vCnn.Close();
            }
            return listaEntidad;
        }

        public int Anular_T_genm_usuario_perfil_PorCodigo(string Id_usuario_perfil)
        {
            int resp = 0;
            using (SqlConnection vCnn = new SqlConnection(vSqlString))
            {
                using (SqlCommand vCmd = new SqlCommand("[SEGURIDAD].[up_EliminarUsuarioPerfil]", vCnn))
                {
                    vCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    vCmd.Parameters.AddWithValue("@pId_usuario_perfil", Id_usuario_perfil);
                    vCnn.Open();
                    resp = vCmd.ExecuteNonQuery();
                    vCnn.Close();
                }
            }
            return resp;
        }

        public T_genm_usuario_perfil Recuperar_T_genm_usuario_perfil_PorCodigo(string Id_usuario_perfil)
        {
            T_genm_usuario_perfil entidad = null;
            using (SqlConnection vCnn = new SqlConnection(vSqlString))
            {
                using (SqlCommand vCmd = new SqlCommand("[SEGURIDAD].[up_ObtenerUsuarioPerfil]", vCnn))
                {
                    vCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    vCmd.Parameters.AddWithValue("@pId_usuario_perfil", Id_usuario_perfil);
                    //vCmd.Parameters.Add("c_Cursor", SqlDbType.RefCursor).Direction = ParameterDirection.Output;

                    vCnn.Open();
                    SqlDataReader reader = vCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        entidad = new T_genm_usuario_perfil(reader);
                    }
                }
                vCnn.Close();
            }
            return entidad;
        }

        public List<T_genm_usuario_perfil> ListarPaginado_T_genm_usuario_perfil(string Filtro, int NumPag, int CantRegxPag)
        {
            List<T_genm_usuario_perfil> listaEntidad = new List<T_genm_usuario_perfil>();
            T_genm_usuario_perfil entidad = null;

            using (SqlConnection vCnn = new SqlConnection(vSqlString))
            {
                using (SqlCommand vCmd = new SqlCommand("PKG_T_genm_usuario_perfil.sp_ListarPaginado", vCnn))
                {
                    vCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    vCmd.Parameters.AddWithValue("@pFiltro", Filtro);
                    vCmd.Parameters.AddWithValue("@pNumPag", NumPag);
                    vCmd.Parameters.AddWithValue("@pCantRegxPag", CantRegxPag);
                    //vCmd.Parameters.Add("c_Cursor", SqlDbType.RefCursor).Direction = ParameterDirection.Output;
                    vCnn.Open();
                    SqlDataReader reader = vCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        entidad = new T_genm_usuario_perfil(reader);
                        entidad.TotalVirtual = System.Convert.ToInt32(reader["TotalVirtual"]);
                        listaEntidad.Add(entidad);
                    }
                }
                vCnn.Close();
            }
            return listaEntidad;
        }

        public List<T_genm_usuario_perfil> ListarUsuarioPorIdPerfil(int idPerfil)
        {
            List<T_genm_usuario_perfil> listaEntidad = new List<T_genm_usuario_perfil>();
            T_genm_usuario_perfil entidad = null;

            using (SqlConnection vCnn = new SqlConnection(vSqlString))
            {
                using (SqlCommand vCmd = new SqlCommand("[SEGURIDAD].[up_ListUsuarioPerfil]", vCnn))
                {
                    vCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    vCmd.Parameters.AddWithValue("@pId_perfil", idPerfil);
                    //vCmd.Parameters.Add("c_Cursor", SqlDbType.RefCursor).Direction = ParameterDirection.Output;
                    vCnn.Open();
                    SqlDataReader reader = vCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        entidad = new T_genm_usuario_perfil(reader);
                        if (!Convert.IsDBNull(reader["cod_usuario"])) { entidad.cod_usuario = Convert.ToString(reader["cod_usuario"]); }
                        
                        listaEntidad.Add(entidad);
                    }
                }
                vCnn.Close();
            }
            return listaEntidad;
        }
        public List<T_genm_usuario_perfil> ListarUsuarioPorIdUsuarioIdPerfil(int idPerfil, int idUsuario)
        {
            List<T_genm_usuario_perfil> listaEntidad = new List<T_genm_usuario_perfil>();
            T_genm_usuario_perfil entidad = null;

            using (SqlConnection vCnn = new SqlConnection(vSqlString))
            {
                using (SqlCommand vCmd = new SqlCommand("[SEGURIDAD].[up_ListIdUsuarioIdPerfil]", vCnn))
                {
                    vCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    vCmd.Parameters.AddWithValue("@pId_perfil", idPerfil);
                    vCmd.Parameters.AddWithValue("@pId_usuario", idUsuario);
                    //vCmd.Parameters.Add("c_Cursor", SqlDbType.RefCursor).Direction = ParameterDirection.Output;
                    vCnn.Open();
                    SqlDataReader reader = vCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        entidad = new T_genm_usuario_perfil(reader);
                        listaEntidad.Add(entidad);
                    }
                }
                vCnn.Close();
            }
            return listaEntidad;
        }

        public List<T_genm_usuario_perfil> ListarUsuarioPorIdUSuarioAndIdAplicacion(int idUsuario, int idAplicacion)
        {
            List<T_genm_usuario_perfil> listaEntidad = new List<T_genm_usuario_perfil>();
            T_genm_usuario_perfil entidad = null;

            using (SqlConnection vCnn = new SqlConnection(vSqlString))
            {
                using (SqlCommand vCmd = new SqlCommand("[SEGURIDAD].[up_ListarUsuPerfilxIdAplicacion]", vCnn))
                {
                    vCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    vCmd.Parameters.AddWithValue("@pId_usuario", idUsuario);
                    vCmd.Parameters.AddWithValue("@pId_aplicacion", idAplicacion);
                    //vCmd.Parameters.Add("c_Cursor", SqlDbType.RefCursor).Direction = ParameterDirection.Output;
                    vCnn.Open();
                    SqlDataReader reader = vCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        entidad = new T_genm_usuario_perfil(reader);
                        listaEntidad.Add(entidad);
                    }
                }
                vCnn.Close();
            }
            return listaEntidad;
        }
        #endregion

        #region Extensiones


        #endregion

    }
}
