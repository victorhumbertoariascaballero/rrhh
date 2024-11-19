using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using MIDIS.ORI.Entidades;

namespace MIDIS.SEG.AccesoDatosSQL
{
    public partial class T_genm_uuoo_ODA
    {
        //BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWork = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSQLAnt"]].ConnectionString));
        BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWork = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnOracle"]].ConnectionString));

        public UnidadOrganica_Registro ObtenerUnidadOrganica(string id)
        {
            //List<UnidadOrganica_Registro> lista = new List<UnidadOrganica_Registro>();
            UnidadOrganica_Registro be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paUnidadOrganicaPorUsuaarioConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", Convert.ToInt32(id)));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        if (dr.Read())
                        {
                        be = new UnidadOrganica_Registro();
                            //item.Grilla = new Grilla_Response();

                        be.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                        be.iCodOrgano = dr.GetInt32(dr.GetOrdinal("iCodOrgano"));
                        be.strOrgano = dr.GetString(dr.GetOrdinal("vOrgano"));
                        be.iCodDependencia = dr.GetInt32(dr.GetOrdinal("iCodDependencia"));
                        be.strUnidad_Organica = dr.GetString(dr.GetOrdinal("vUnidad_Organica"));
                        be.strDependencia_Jerarquica_Lineal = dr.GetString(dr.GetOrdinal("vDependencia_Jerarquica_Lineal"));
                        be.strDependencia_Funcional = dr.GetString(dr.GetOrdinal("vDependencia_Funcional"));

                        

                            //lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return be;
        }


        public IEnumerable<UnidadOrganica_Registro> ListarOrganos()
        {
            List<UnidadOrganica_Registro> lista = new List<UnidadOrganica_Registro>();
            UnidadOrganica_Registro be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paOrganoConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                //cmd.Parameters.Add(new SqlParameter("@iCodTrabajador", Convert.ToInt32(id)));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new UnidadOrganica_Registro();
                            //item.Grilla = new Grilla_Response();

                            //be.iCodTrabajador = dr.GetInt32(dr.GetOrdinal("iCodTrabajador"));
                            be.iCodigoDependencia = dr.GetInt32(dr.GetOrdinal("iCodigoDependencia"));
                            be.strUnidad_Organica = dr.GetString(dr.GetOrdinal("vDependencia"));
                            //be.strDependencia_Jerarquica_Lineal = dr.GetString(dr.GetOrdinal("vDependencia_Jerarquica_Lineal"));
                            //be.strDependencia_Funcional = dr.GetString(dr.GetOrdinal("vDependencia_Funcional"));



                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<UnidadOrganica_Registro> ListarUnidadesOrganicas(string id)
        {
            List<UnidadOrganica_Registro> lista = new List<UnidadOrganica_Registro>();
            UnidadOrganica_Registro be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paUnidadOrganicaConsultarPorCodigoPadre]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodigoDependeciaPadre", Convert.ToInt32(id)));
                //cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        Int32[] organos = { 25, 1, 10, 11, 77, 76, 74, 75, 13, 14, 21, 5, 6, 7, 38, 32, 37, 73, 4, 15, 9 };
                        while (dr.Read())
                        {
                            if (!organos.Contains(dr.GetInt32(dr.GetOrdinal("iCodigoDependencia")))) {
                                be = new UnidadOrganica_Registro();
                                
                                be.iCodigoDependencia = dr.GetInt32(dr.GetOrdinal("iCodigoDependencia"));
                                be.strUnidad_Organica = dr.GetString(dr.GetOrdinal("vDependencia"));
                                
                                lista.Add(be);
                            }
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
    }
}
