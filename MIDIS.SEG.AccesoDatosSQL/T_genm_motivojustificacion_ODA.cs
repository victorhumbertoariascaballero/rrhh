using MIDIS.ORI.Entidades;
using MIDIS.ORI.Entidades.Core;
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
    public class T_genm_motivojustificacion_ODA
    {
        BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWork = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings["cnnSQL"].ConnectionString));

        public IEnumerable<MotivoJustificacion_Registro> ListarMotivoJustificacion(MotivoJustificacion_Request request)
        {
            List<MotivoJustificacion_Registro> lista = new List<MotivoJustificacion_Registro>();
            MotivoJustificacion_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMotivoJustificacionConsultar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;
                cmd.Parameters.Add(new SqlParameter("@bEstado", request.bEstado));


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            //iCodMotivoJustificacion, vDescripcion, bConGoce, bEstado, dtAuditCreacion, vAuditCreacion, dtAuditModificacion, vAuditModificacion

                            item = new MotivoJustificacion_Registro();

                            item.Grilla = new Grilla_Response();
                            item.iCodMotivoJustificacion = Types.CheckDefaultValue<Int32>(dr["iCodMotivoJustificacion"]);
                            item.iCodTipoJustificacion = Types.CheckDefaultValue<Int32>(dr["iCodTipoJustificacion"]);
                            item.vDescripcion = Types.CheckDefaultValue<string>(dr["vDescripcion"]);                            
                            item.bConGoce = Types.CheckDefaultValue<bool>(dr["bConGoce"]);
                            item.bBloquearGoce = Types.CheckDefaultValue<bool>(dr["bBloquearGoce"]);
                            item.bBloquearTipoJustificacion = Types.CheckDefaultValue<bool>(dr["bBloquearTipoJustificacion"]);
                            item.bEstado = Types.CheckDefaultValue<bool>(dr["bEstado"]);

                            item.dtAuditCreacion = Types.CheckDefaultValue<DateTime>(dr["dtAuditCreacion"]);
                            item.vAuditCreacion = Types.CheckDefaultValue<String>(dr["vAuditCreacion"]);

                            item.dtAuditModificacion = Types.CheckDefaultValue<DateTime>(dr["dtAuditModificacion"]);
                            item.vAuditModificacion = Types.CheckDefaultValue<String>(dr["vAuditModificacion"]);

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
