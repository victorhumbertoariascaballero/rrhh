using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using MIDIS.ORI.Entidades;

namespace MIDIS.SEG.AccesoDatosSQL
{
    public partial class T_genm_regimenpensionario_ODA
    {
        BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWork = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnOracle"]].ConnectionString));
        

        #region Metodos Generales

        string vSqlString = new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnOracleM"]].ConnectionString);

        //public IEnumerable<RegimenPensionario_Registro> ListarDependencias(Dependencia_Request peticion)
        //{
        //    List<Dependencia_Registro> lista = new List<Dependencia_Registro>();
        //    Dependencia_Registro item = null;

        //    using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
        //    {
        //        if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
        //        cmd.CommandText = "[dbo].[Usp_Maedependencia_sel]";
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        //cmd.CommandTimeout = cmd.CommandTimeout;

        //        cmd.Parameters.Add(new SqlParameter("@ID_DEPENDENCIA", peticion.IdDependencia));
        //        cmd.Parameters.Add(new SqlParameter("@NOMBRE", peticion.Nombre));

        //        using (SqlDataReader dr = cmd.ExecuteReader())
        //        {
        //            if (dr.HasRows)
        //            {
        //                while (dr.Read())
        //                {
        //                    item = new Dependencia_Registro();

        //                    item.Grilla = new Grilla_Response();
        //                    //item.Grilla.NumeroDeFila = dr.GetInt64(dr.GetOrdinal("NUMERO_FILA"));
        //                    //item.Grilla.TotalDeRegistros = dr.GetInt32(dr.GetOrdinal("TOTAL_FILAS"));

        //                    item.IdDependencia = dr.GetInt32(dr.GetOrdinal("ICODIGODEPENDENCIA"));
        //                    item.Sigla = dr.GetString(dr.GetOrdinal("VSIGLAS"));
        //                    item.Nombre = dr.GetString(dr.GetOrdinal("VDEPENDENCIA"));
        //                    //if (!Convert.IsDBNull(dr["boleta"]) && peticion.IdEmpleado > 0) { item.Boleta = (byte[])(dr["BOLETA"]); }
        //                    //if (!Convert.IsDBNull(dr["fecha_envio"])) { item.FechaEnvio = dr.GetDateTime(dr.GetOrdinal("FECHA_ENVIO")).ToString("dd/MM/yyyy HH:mm"); }
        //                    //if (!Convert.IsDBNull(dr["fecha_recepcion"])) { item.FechaRecepcion = dr.GetDateTime(dr.GetOrdinal("FECHA_RECEPCION")).ToString("dd/MM/yyyy HH:mm"); }

        //                    lista.Add(item);
        //                }
        //            }
        //        }

        //        if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

        //    }

        //    return lista;
        //}

        public IEnumerable<RegimenPensionario_Response> ListarRegimenPensionario(RegimenPensionario_Request peticion)
        {
            List<RegimenPensionario_Response> lista = new List<RegimenPensionario_Response>();
            RegimenPensionario_Response item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaeRegimenPensionario_Lista]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodRegimenPen", peticion.iCodRegimenPen));
                cmd.Parameters.Add(new SqlParameter("@vNombre", peticion.vNombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new RegimenPensionario_Response();

                            item.Grilla = new Grilla_Response();

                            item.iCodRegimenPen = dr.GetInt32(dr.GetOrdinal("iCodRegimenPen"));
                            item.vNombre = dr.GetString(dr.GetOrdinal("vNombre"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }
            return lista;
        }
        
        public IEnumerable<RegimenAfp_Response> ListarRegimenAfp(RegimenAfp_Request peticion)
        {
            List<RegimenAfp_Response> lista = new List<RegimenAfp_Response>();
            RegimenAfp_Response item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaeAFP_Lista]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodAfp", peticion.iCodAFP));
                cmd.Parameters.Add(new SqlParameter("@vNombre", peticion.vNombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new RegimenAfp_Response();

                            item.Grilla = new Grilla_Response();

                            item.iCodAFP = dr.GetInt32(dr.GetOrdinal("iCodAfp"));
                            item.vNombre = dr.GetString(dr.GetOrdinal("vNombre"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }
            return lista;
        }

        public IEnumerable<TipoRegimenPensionario_Response> ListarTipoRegimenPensionario(TipoRegimenPensionario_Request peticion)
        {
            List<TipoRegimenPensionario_Response> lista = new List<TipoRegimenPensionario_Response>();
            TipoRegimenPensionario_Response item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paMaeTipoRegimenPensionario_Lista]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodTipoRegimen", peticion.iCodTipoRegimenPen));
                cmd.Parameters.Add(new SqlParameter("@vNombre", peticion.vNombre));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new TipoRegimenPensionario_Response();

                            item.Grilla = new Grilla_Response();

                            item.iCodTipoRegimenPen= dr.GetInt32(dr.GetOrdinal("iCodTipoRegimenPen"));
                            //item.Sigla = dr.GetString(dr.GetOrdinal("VSIGLAS"));
                            item.vNombre = dr.GetString(dr.GetOrdinal("vNombre"));
                            
                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return lista;
        }

        public IEnumerable<RegistroRegimenPensionario_Registro> ListarRegistroRegimenPensionario(int iMes, int iAnio)
        {
            List<RegistroRegimenPensionario_Registro> lista = new List<RegistroRegimenPensionario_Registro>();
            RegistroRegimenPensionario_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaRegistroRegimenPensionario_Lista]";
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter("@iMes", iMes));
                cmd.Parameters.Add(new SqlParameter("@iAnio", iAnio));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new RegistroRegimenPensionario_Registro();

                            item.Grilla = new Grilla_Response();

                            item.iCodRegistroRegimenPen = dr.GetInt32(dr.GetOrdinal("iCodRegistroRegimenPen"));
                            item.iCodRegimenPen = dr.GetInt32(dr.GetOrdinal("iCodRegimenPen"));
                            item.sNombreRegimenPen = dr.GetString(dr.GetOrdinal("vNombreRegimenPensionario"));

                            item.iCodTipoRegimenPen = dr.GetInt32(dr.GetOrdinal("iCodTipoRegimenPen"));
                            item.sNombreTipoRegimenPen = dr.GetString(dr.GetOrdinal("vNombreTipoRegimenPen"));

                            item.iCodAFP = dr.GetInt32(dr.GetOrdinal("iCodAFP"));
                            item.sNombre = dr.GetString(dr.GetOrdinal("vNombreAfp"));
                            item.sTipo = dr.GetString(dr.GetOrdinal("vTipo"));

                            item.dcComision = dr.GetDecimal(dr.GetOrdinal("dComision"));
                            item.dcPrimaSeguro = dr.GetDecimal(dr.GetOrdinal("dPrimaSeguro"));
                            item.dcAporte = dr.GetDecimal(dr.GetOrdinal("dAporte"));
                            item.dcTope = dr.GetDecimal(dr.GetOrdinal("dTope"));
                            item.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            item.iAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            item.sEstado = dr.GetString(dr.GetOrdinal("sEstado"));

                            lista.Add(item);
                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;

        }

        public RegistroRegimenPensionario_Registro ObtenerRegimenParaEditar(RegistroRegimenPensionario_Request peticion)
        {
            RegistroRegimenPensionario_Registro registro = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[paPlanillaRegistroRegimenPensionario_BuscarPorID]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodRegistroRegimenPen", peticion.iCodRegistroRegimenPen));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            registro = new RegistroRegimenPensionario_Registro();
                            registro.Grilla = new Grilla_Response();

                            registro.iCodRegistroRegimenPen = dr.GetInt32(dr.GetOrdinal("iCodRegistroRegimenPen"));
                            registro.iCodRegimenPen = dr.GetInt32(dr.GetOrdinal("iCodRegimenPen"));
                            registro.iCodAFP = dr.GetInt32(dr.GetOrdinal("iCodAFP"));
                            registro.iCodTipoRegimenPen = dr.GetInt32(dr.GetOrdinal("iCodTipoRegimenPen"));
                            registro.sNombre = dr.GetString(dr.GetOrdinal("vNombreAfp"));
                            //registro.sNombreRegimenPen = dr.GetString(dr.GetOrdinal("vNombreRegimenPen"));
                            registro.sNombreTipoRegimenPen = dr.GetString(dr.GetOrdinal("vNombreTipoRegimenPen"));
                            //
                            registro.dcComision = dr.GetDecimal(dr.GetOrdinal("dComision"));
                            registro.dcPrimaSeguro = dr.GetDecimal(dr.GetOrdinal("dPrimaSeguro"));
                            registro.dcAporte = dr.GetDecimal(dr.GetOrdinal("dAporte"));
                            registro.dcTope = dr.GetDecimal(dr.GetOrdinal("dTope"));

                            registro.iAnio = dr.GetInt32(dr.GetOrdinal("iAnio"));
                            registro.iMes = dr.GetInt32(dr.GetOrdinal("iMes"));
                            
                            registro.bEstado = dr.GetBoolean(dr.GetOrdinal("bEstado"));

                        }
                    }
                }
            }

            return registro;
        }

        public Int32 InsertarRegistroRegimenPensionario(RegistroRegimenPensionario_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaRegistroRegimenPensionario_Registrar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;
                cmd.Parameters.Add(new SqlParameter("@iCodRegistroRegimenPen", registro.iCodRegistroRegimenPen));
                cmd.Parameters.Add(new SqlParameter("@iCodRegimenPen", registro.iCodRegimenPen));
                cmd.Parameters.Add(new SqlParameter("@iCodAFP", registro.iCodAFP));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoRegimenPen", registro.iCodTipoRegimenPen));
                cmd.Parameters.Add(new SqlParameter("@dcComision", registro.dcComision));
                cmd.Parameters.Add(new SqlParameter("@dcPrimaSeguro", registro.dcPrimaSeguro));
                cmd.Parameters.Add(new SqlParameter("@dcAporte", registro.dcAporte));
                cmd.Parameters.Add(new SqlParameter("@dcTope", registro.dcTope));
                cmd.Parameters.Add(new SqlParameter("@iAnio", registro.iAnio));
                cmd.Parameters.Add(new SqlParameter("@iMes", registro.iMes));
                //cmd.Parameters.Add(new SqlParameter("@bEstado", registro.Estado));
                //cmd.Parameters.Add(new SqlParameter("@vUsuario", registro.IdUsuarioRegistro));
            
                //SqlParameter IdPropuestaParameter = new SqlParameter("@resultado", SqlDbType.Int)
                //{
                //    Direction = ParameterDirection.Output
                //};
                //cmd.Parameters.Add(IdPropuestaParameter);

                cmd.ExecuteNonQuery();
                //registro.iCodRegistroRegimenPen = Int32.Parse(IdPropuestaParameter.Value.ToString());

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.iCodRegistroRegimenPen;
        }

        public Int32 ActualizarRegistroRegimenPensionario(RegistroRegimenPensionario_Registro registro)
        {
            
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                cmd.CommandText = "[dbo].[paPlanillaRegistroRegimenPensionario_Actualizar]";
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter("@iCodRegistroRegimenPen", registro.iCodRegistroRegimenPen));
                cmd.Parameters.Add(new SqlParameter("@iCodRegimenPen", registro.iCodRegimenPen));
                cmd.Parameters.Add(new SqlParameter("@iCodAFP", registro.iCodAFP));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoRegimenPen", registro.iCodTipoRegimenPen));
                cmd.Parameters.Add(new SqlParameter("@dcComision", registro.dcComision));
                cmd.Parameters.Add(new SqlParameter("@dcPrimaSeguro", registro.dcPrimaSeguro));
                cmd.Parameters.Add(new SqlParameter("@dcAporte", registro.dcAporte));
                cmd.Parameters.Add(new SqlParameter("@dcTope", registro.dcTope));
                //cmd.Parameters.Add(new SqlParameter("@iAnio", registro.iAnio));
                //cmd.Parameters.Add(new SqlParameter("@iMes", registro.iMes));
                //cmd.Parameters.Add(new SqlParameter("@bEstado", registro.Estado));
                //cmd.Parameters.Add(new SqlParameter("@vUsuario", registro.IdUsuarioModificacion));
                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.iCodRegistroRegimenPen;
        }

        public Int32 CopiarRegimenPensionario(RegistroRegimenPensionario_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaRegistroRegimenPensionario_CopiarPeriodo]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;
                cmd.Parameters.Add(new SqlParameter("@iAnio", registro.iAnio));
                cmd.Parameters.Add(new SqlParameter("@iMes", registro.iMes));
                //cmd.Parameters.Add(new SqlParameter("@bEstado", registro.Estado));
                //cmd.Parameters.Add(new SqlParameter("@vUsuario", registro.IdUsuarioRegistro));
                Int32 iReg = Convert.ToInt32(cmd.ExecuteNonQuery());
                //registro.iCodRegistroRegimenPen = Int32.Parse(IdPropuestaParameter.Value.ToString());

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.iResultado;
        }

        #endregion


    }
}
