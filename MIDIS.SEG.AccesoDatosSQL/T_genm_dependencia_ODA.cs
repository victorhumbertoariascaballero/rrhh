/*----------------------------------------------------------------------------------------
ARCHIVO CLASE   : T_genm_usuario_ODA

Objetivo: Clase referida a los métodos de Acceso a datos de la clase T_genm_usuario
Autor: Miguel Angel Salvador Paucar (MASP)
Fecha Creacion: 2015-09-03
Metodos: 
        Insertar_T_genm_usuario
        Actualizar_T_genm_usuario
        Listar_T_genm_usuario
        Anular_T_genm_usuario_PorCodigo
        Recuperar_T_genm_usuario_PorCodigo
        ListarPaginado_T_genm_usuario

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
    public partial class T_genm_dependencia_ODA
    {
        BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWork = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnOracleM"]].ConnectionString));
        BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWorkAnt = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnOracleAnt"]].ConnectionString));

        #region Metodos Generales

        string vSqlString = new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnOracleM"]].ConnectionString);

        public IEnumerable<Dependencia_Registro> ListarDependencias(Dependencia_Request peticion)
        {
            List<Dependencia_Registro> lista = new List<Dependencia_Registro>();
            Dependencia_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[Usp_Maedependencia_sel]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@ID_DEPENDENCIA", peticion.IdDependencia));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));
                
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Dependencia_Registro();

                            item.Grilla = new Grilla_Response();
                            //item.Grilla.NumeroDeFila = dr.GetInt64(dr.GetOrdinal("NUMERO_FILA"));
                            //item.Grilla.TotalDeRegistros = dr.GetInt32(dr.GetOrdinal("TOTAL_FILAS"));

                            item.IdDependencia = dr.GetInt32(dr.GetOrdinal("ICODIGODEPENDENCIA"));
                            item.Sigla = dr.GetString(dr.GetOrdinal("VSIGLAS"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("VDEPENDENCIA"));
                            //if (!Convert.IsDBNull(dr["boleta"]) && peticion.IdEmpleado > 0) { item.Boleta = (byte[])(dr["BOLETA"]); }
                            //if (!Convert.IsDBNull(dr["fecha_envio"])) { item.FechaEnvio = dr.GetDateTime(dr.GetOrdinal("FECHA_ENVIO")).ToString("dd/MM/yyyy HH:mm"); }
                            //if (!Convert.IsDBNull(dr["fecha_recepcion"])) { item.FechaRecepcion = dr.GetDateTime(dr.GetOrdinal("FECHA_RECEPCION")).ToString("dd/MM/yyyy HH:mm"); }

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<Dependencia_Registro> ListarDependenciasAntiguo(Dependencia_Request peticion)
        {
            List<Dependencia_Registro> lista = new List<Dependencia_Registro>();
            Dependencia_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWorkAnt.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "SELECT * FROM [GENERAL].[GEMaeDependencia] where bEstado = 1 and iCodMaeDependencia = " + peticion.IdDependencia.Value.ToString();
                cmd.CommandType = CommandType.Text;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Dependencia_Registro();

                            item.Grilla = new Grilla_Response();

                            item.IdDependencia = dr.GetInt32(dr.GetOrdinal("iCodMaeDependencia"));
                            item.Sigla = dr.GetString(dr.GetOrdinal("nvAbreviatura"));
                            item.Nombre = dr.GetString(dr.GetOrdinal("nvDenominacion"));
                            
                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        #endregion

    }
}
