using MIDIS.ORI.Entidades;
using MIDIS.SEG.AccesoDatosSQL.Helpers;
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
    public class T_genm_justificacionesproceso_ODA
    {
        BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWork = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings["cnnSQL"].ConnectionString));

        public IEnumerable<JustificacionesProceso_Registro> ListarJustificacionProcesoHistorial(int iCodJustificaciones)
        {
            List<JustificacionesProceso_Registro> lista = new List<JustificacionesProceso_Registro>();
            JustificacionesProceso_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[pa_JustificacionProcesoHistorialporIdJustificacion]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodJustificaciones", iCodJustificaciones));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new JustificacionesProceso_Registro();

                            item.Grilla = new Grilla_Response();
                            item.vComentario = Types.CheckDefaultValue<string>(dr["vComentario"]);
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
