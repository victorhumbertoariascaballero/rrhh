using MIDIS.ORI.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.SEG.AccesoDatosSQL
{
    public class T_genm_TipoJustificacion_ODA
    {
        BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWork = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings["cnnSQL"].ConnectionString));

        public IEnumerable<TipoJustificacion_Registro> ListarTipoJustificacion()
        {
            List<TipoJustificacion_Registro> lista = new List<TipoJustificacion_Registro>();
            TipoJustificacion_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paTipoJustificacionConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@bEstado", true));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new TipoJustificacion_Registro();

                            item.Grilla = new Grilla_Response();

                            item.iCodTipoJustificacion = dr.GetInt32(dr.GetOrdinal("iCodTipoJustificacion"));
                            item.vDescripcion = dr.GetString(dr.GetOrdinal("vDescripcion"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
    }
}
