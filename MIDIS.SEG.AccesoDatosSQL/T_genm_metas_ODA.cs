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
    public class T_genm_metas_ODA
    {
        BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWork = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnOracle"]].ConnectionString));

        public IEnumerable<Metas_Response> ListarMetas()
        {
            List<Metas_Response> lista = new List<Metas_Response>();
            Metas_Response be = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaePlanillaMetaConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;                

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            be = new Metas_Response();
                            //item.Grilla = new Grilla_Response();
                            //be.Grilla = new Grilla_Response();


                            be.iCodMeta = dr.GetInt32(dr.GetOrdinal("iCodigoMeta"));
                            be.sSec_Func = dr.GetString(dr.GetOrdinal("vSec_Func"));
                            be.sFinal_COD = dr.GetString(dr.GetOrdinal("vFinal_COD"));
                            be.sMeta = dr.GetString(dr.GetOrdinal("vMeta"));
                            be.bEstado = dr.GetBoolean(dr.GetOrdinal("bEstado"));
                            be.iAnnoMeta = dr.GetInt32(dr.GetOrdinal("iAnnoMeta"));
                            be.dMonto = dr.GetDecimal(dr.GetOrdinal("dMontoMeta"));                           
                            lista.Add(be);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
    }
}
