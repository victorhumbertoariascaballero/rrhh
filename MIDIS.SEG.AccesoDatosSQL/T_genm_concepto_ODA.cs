using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public partial class T_genm_concepto_ODA
    {
        BaseSqlAdoUnitOfWork _iBasesSqlAdoUnitOfWork = new BaseSqlAdoUnitOfWork(new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnOracle"]].ConnectionString));

        #region Metodos Generales

        string vSqlString = new MIDIS.Utiles.Crypto().Desencriptar(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnOracleM"]].ConnectionString);

        public IEnumerable<Concepto_Registro> ListarConcepto(Concepto_Request peticion)
        {
            List<Concepto_Registro> lista = new List<Concepto_Registro>();
            Concepto_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaConcepto_Listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;
                cmd.Parameters.Add(new SqlParameter("@iCodTipoConcepto", peticion.iCodTipoConcepto));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Concepto_Registro();

                            item.Grilla = new Grilla_Response();
                            item.iCodConcepto = dr.GetInt32(dr.GetOrdinal("iCodConcepto"));
                            item.vConcepto = dr.GetString(dr.GetOrdinal("vNombreConcepto"));
                            item.iCodTipoConcepto = dr.GetInt32(dr.GetOrdinal("iCodTipoConcepto"));
                            item.vTipoConcepto = dr.GetString(dr.GetOrdinal("vNombreTipoConcepto"));
                            item.iCodSubTipoConcepto = dr.GetInt32(dr.GetOrdinal("iCodSubTipoConcepto"));
                            item.vSubTipoConcepto = dr.GetString(dr.GetOrdinal("vNombreSubTipoConcepto"));
                            item.vRegCAS= dr.GetString(dr.GetOrdinal("vRegCAS"));
                            item.vRegFunc = dr.GetString(dr.GetOrdinal("vRegFunc"));
                            item.vRegSeci = dr.GetString(dr.GetOrdinal("vRegSeci"));
                            item.vCodigoExterno = dr.GetString(dr.GetOrdinal("vCodigoExterno"));
                            item.vCodigoMCPP = dr.GetString(dr.GetOrdinal("vCodigoMCPP"));
                            item.vCodigoMEF = dr.GetString(dr.GetOrdinal("vCodigoMEF"));
                            item.vClasificadorGasto = dr.GetString(dr.GetOrdinal("vClasificadorGasto"));
                            item.vRegConceptoBaseImponible = dr.GetString(dr.GetOrdinal("vRegConceptoBaseImponible"));
                            item.vRegCalculoAutomatico = dr.GetString(dr.GetOrdinal("vRegCalculoAutomatico"));
                            lista.Add(item);

                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        
        public IEnumerable<TipoConcepto_Response> ListarTipoConcepto(TipoConcepto_Request peticion)
        {
            List<TipoConcepto_Response> lista = new List<TipoConcepto_Response>();
            TipoConcepto_Response item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaTipoConcepto_Listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;
                //cmd.Parameters.Add(new SqlParameter("@iCodConcepto", peticion.iCodConcepto));
                //cmd.Parameters.Add(new SqlParameter("@vConcepto", peticion.vConcepto));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new TipoConcepto_Response();

                            item.Grilla = new Grilla_Response();
                            item.iCodTipoConcepto = dr.GetInt32(dr.GetOrdinal("iCodTipoConcepto"));
                            item.vNombre = dr.GetString(dr.GetOrdinal("vNombreTipoConcepto"));

                            lista.Add(item);

                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<TipoConcepto_Response> ListarTipoConceptoNoBaseImponible(TipoConcepto_Request peticion)
        {
            List<TipoConcepto_Response> lista = new List<TipoConcepto_Response>();
            TipoConcepto_Response item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaTipoConceptoNoBaseImp_Listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;
                //cmd.Parameters.Add(new SqlParameter("@iCodConcepto", peticion.iCodConcepto));
                //cmd.Parameters.Add(new SqlParameter("@vConcepto", peticion.vConcepto));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new TipoConcepto_Response();

                            item.Grilla = new Grilla_Response();
                            item.iCodTipoConcepto = dr.GetInt32(dr.GetOrdinal("iCodTipoConcepto"));
                            item.vNombre = dr.GetString(dr.GetOrdinal("vNombreTipoConcepto"));

                            lista.Add(item);

                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
 
        public IEnumerable<SubTipoConcepto_Response> ListarSubTipoConcepto(SubTipoConcepto_Request peticion)
        {
            List<SubTipoConcepto_Response> lista = new List<SubTipoConcepto_Response>();
            SubTipoConcepto_Response item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaSubTipoConcepto_Listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodTipoConcepto", peticion.iCodTipoConcepto));
                //cmd.Parameters.Add(new SqlParameter("@vConcepto", peticion.vConcepto));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new SubTipoConcepto_Response();

                            item.Grilla = new Grilla_Response();
                            item.iCodSubTipoConcepto = dr.GetInt32(dr.GetOrdinal("iCodSubTipoConcepto"));
                            item.vNombre = dr.GetString(dr.GetOrdinal("vNombre"));
                            item.iCodTipoConcepto = dr.GetInt32(dr.GetOrdinal("iCodTipoConcepto"));

                            lista.Add(item);

                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<SubTipoConcepto_Response> ListarSubTipoConceptoNoBaseImponible(SubTipoConcepto_Request peticion)
        {
            List<SubTipoConcepto_Response> lista = new List<SubTipoConcepto_Response>();
            SubTipoConcepto_Response item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaSubTipoConceptoNoBaseImp_Listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodTipoConcepto", peticion.iCodTipoConcepto));
                //cmd.Parameters.Add(new SqlParameter("@vConcepto", peticion.vConcepto));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new SubTipoConcepto_Response();

                            item.Grilla = new Grilla_Response();
                            item.iCodSubTipoConcepto = dr.GetInt32(dr.GetOrdinal("iCodSubTipoConcepto"));
                            item.vNombre = dr.GetString(dr.GetOrdinal("vNombre"));
                            item.iCodTipoConcepto = dr.GetInt32(dr.GetOrdinal("iCodTipoConcepto"));

                            lista.Add(item);

                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        
        public IEnumerable<Concepto_Response> ListarConceptoPorTipo(Concepto_Request peticion)
        {
            List<Concepto_Response> lista = new List<Concepto_Response>();
            Concepto_Response item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaConceptoPorTipo_Listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodTipoConcepto", peticion.iCodTipoConcepto));
                cmd.Parameters.Add(new SqlParameter("@iCodSubTipoConcepto", peticion.iCodSubTipoConcepto));
                cmd.Parameters.Add(new SqlParameter("@bRegCAS", peticion.bRegCAS));
                cmd.Parameters.Add(new SqlParameter("@bRegFun", peticion.bRegFunc));
                cmd.Parameters.Add(new SqlParameter("@bRegSeci", peticion.bRegSeci));
                cmd.Parameters.Add(new SqlParameter("@bRegPrac", peticion.bRegPracticantes));
                //cmd.Parameters.Add(new SqlParameter("@vConcepto", peticion.vConcepto));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Concepto_Response();

                            item.Grilla = new Grilla_Response();
                            item.iCodConcepto = dr.GetInt32(dr.GetOrdinal("iCodConcepto"));
                            item.vConcepto = dr.GetString(dr.GetOrdinal("vNombreConcepto"));
                            
                            lista.Add(item);

                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<Concepto_Response> ListarConceptoPorTipoNoBaseImponible(Concepto_Request peticion)
        {
            List<Concepto_Response> lista = new List<Concepto_Response>();
            Concepto_Response item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaConceptoPorTipoNoBaseImp_Listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodTipoConcepto", peticion.iCodTipoConcepto));
                cmd.Parameters.Add(new SqlParameter("@iCodSubTipoConcepto", peticion.iCodSubTipoConcepto));
                //cmd.Parameters.Add(new SqlParameter("@vConcepto", peticion.vConcepto));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Concepto_Response();

                            item.Grilla = new Grilla_Response();
                            item.iCodConcepto = dr.GetInt32(dr.GetOrdinal("iCodConcepto"));
                            item.vConcepto = dr.GetString(dr.GetOrdinal("vNombreConcepto"));

                            lista.Add(item);

                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public IEnumerable<Concepto_Response> ListarConceptoPorTipoConcepto(Concepto_Request peticion)
        {
            List<Concepto_Response> lista = new List<Concepto_Response>();
            Concepto_Response item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaConceptoPorTipoConcepto_Listar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodTipoConcepto", peticion.iCodTipoConcepto));                
                //cmd.Parameters.Add(new SqlParameter("@vConcepto", peticion.vConcepto));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Concepto_Response();

                            item.Grilla = new Grilla_Response();
                            item.iCodConcepto = dr.GetInt32(dr.GetOrdinal("iCodConcepto"));
                            item.vConcepto = dr.GetString(dr.GetOrdinal("vNombreConcepto"));
                            item.bRegCAS = dr.GetBoolean(dr.GetOrdinal("bRegCAS"));
                            item.bRegFunc = dr.GetBoolean(dr.GetOrdinal("bRegFun"));
                            item.bRegSeci = dr.GetBoolean(dr.GetOrdinal("bRegSeci"));
                            item.bRegConceptoBaseImponible = dr.GetBoolean(dr.GetOrdinal("bRegConceptoBaseImponible"));
                            item.bRegCalculoAutomatico = dr.GetBoolean(dr.GetOrdinal("bRegCalculoAutomatico"));

                            lista.Add(item);

                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }
        public IEnumerable<Concepto_Registro> BuscarConcepto(Concepto_Request peticion)
        {
            List<Concepto_Registro> lista = new List<Concepto_Registro>();
            Concepto_Registro item = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaConcepto_Buscar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodConcepto", peticion.iCodConcepto));
                cmd.Parameters.Add(new SqlParameter("@vNombre", peticion.vConcepto));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new Concepto_Registro();

                            item.Grilla = new Grilla_Response();
                      
                            item.iCodConcepto = dr.GetInt32(dr.GetOrdinal("iCodConcepto"));
                            item.iCodTipoConcepto = dr.GetInt32(dr.GetOrdinal("iCodTipoConcepto"));
                            item.iCodSubTipoConcepto = dr.GetInt32(dr.GetOrdinal("iCodSubTipoConcepto"));
                            item.vConcepto = dr.GetString(dr.GetOrdinal("vNombreConcepto"));
                            item.vAbreviatura = dr.GetString(dr.GetOrdinal("vAbreviatura"));
                            item.bRegCAS = dr.GetBoolean(dr.GetOrdinal("bRegCAS"));
                            item.bRegFunc = dr.GetBoolean(dr.GetOrdinal("bRegFun"));
                            item.bRegSeci = dr.GetBoolean(dr.GetOrdinal("bRegSeci"));
                            item.bEstado = dr.GetBoolean(dr.GetOrdinal("bEstado"));
                            item.vCodigoExterno = dr.GetString(dr.GetOrdinal("vCodigoExterno"));
                            item.vCodigoMCPP = dr.GetString(dr.GetOrdinal("vCodigoMCPP"));
                            item.vCodigoMEF = dr.GetString(dr.GetOrdinal("vCodigoMEF"));
                            item.vClasificadorGasto = dr.GetString(dr.GetOrdinal("vClasificadorGasto"));
                            item.bRegConceptoBaseImponible = dr.GetBoolean(dr.GetOrdinal("bRegConceptoBaseImponible"));
                            item.bRegCalculoAutomatico = dr.GetBoolean(dr.GetOrdinal("bRegCalculoAutomatico"));
                            lista.Add(item);

                        }
                    }
                }

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();

            }

            return lista;
        }

        public Concepto_Registro ObtenerConceptoParaEditar(Concepto_Request peticion)
        {
            Concepto_Registro registro = null;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[paPlanillaConcepto_BuscarPorID]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodConcepto", peticion.iCodConcepto));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            registro = new Concepto_Registro();
                            registro.Grilla = new Grilla_Response();

                            registro.iCodConcepto = dr.GetInt32(dr.GetOrdinal("iCodConcepto"));
                            registro.iCodTipoConcepto = dr.GetInt32(dr.GetOrdinal("iCodTipoConcepto"));
                            registro.iCodSubTipoConcepto = dr.GetInt32(dr.GetOrdinal("iCodSubTipoConcepto"));
                            registro.vConcepto = dr.GetString(dr.GetOrdinal("vNombreConcepto"));
                            registro.vAbreviatura = dr.GetString(dr.GetOrdinal("vAbreviatura"));
                            //
                            registro.bRegCAS = dr.GetBoolean(dr.GetOrdinal("bRegCAS"));
                            registro.bRegFunc = dr.GetBoolean(dr.GetOrdinal("bRegFun"));
                            registro.bRegSeci = dr.GetBoolean(dr.GetOrdinal("bRegSeci"));
                            registro.bEstado = dr.GetBoolean(dr.GetOrdinal("bEstado"));
                            registro.vCodigoExterno = dr.GetString(dr.GetOrdinal("vCodigoExterno"));
                            registro.vCodigoMCPP = dr.GetString(dr.GetOrdinal("vCodigoMCPP"));
                            registro.vCodigoMEF = dr.GetString(dr.GetOrdinal("vCodigoMEF"));
                            registro.vCodigoMEF = dr.GetString(dr.GetOrdinal("vCodigoMEF"));
                            registro.vClasificadorGasto = dr.GetString(dr.GetOrdinal("vClasificadorGasto"));
                            registro.bRegConceptoBaseImponible = dr.GetBoolean(dr.GetOrdinal("bConceptoBaseImp"));
                            registro.bRegCalculoAutomatico = dr.GetBoolean(dr.GetOrdinal("bCalculoAutomatico"));
                        }
                    }
                }
            }

            return registro;
        }

        public Int32 RegistrarConcepto(Concepto_Registro registro)
        {
            Int32 resultado;

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaConcepto_Registrar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                SqlParameter parmOUT = new SqlParameter("@iCodConcepto", SqlDbType.Int);
                parmOUT.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(parmOUT);

                //cmd.Parameters.Add(new SqlParameter("@iCodConcepto", registro.iCodConcepto));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoConcepto", registro.iCodTipoConcepto));
                cmd.Parameters.Add(new SqlParameter("@iCodSubTipoConcepto", registro.iCodSubTipoConcepto));
                cmd.Parameters.Add(new SqlParameter("@vConcepto", registro.vConcepto));
                cmd.Parameters.Add(new SqlParameter("@vAbreviatura", registro.vAbreviatura));
                cmd.Parameters.Add(new SqlParameter("@bRegCAS", registro.bRegCAS));
                cmd.Parameters.Add(new SqlParameter("@bRegFunc", registro.bRegFunc));
                cmd.Parameters.Add(new SqlParameter("@bRegSeci", registro.bRegSeci));
                cmd.Parameters.Add(new SqlParameter("@bRegConceptoBaseImponible", registro.bRegConceptoBaseImponible));
                cmd.Parameters.Add(new SqlParameter("@bRegCalculoAutomatico", registro.bRegCalculoAutomatico));
                if (registro.vCodigoExterno != null && registro.vCodigoExterno != string.Empty)
                {
                    cmd.Parameters.Add(new SqlParameter("@vCodigoExterno", registro.vCodigoExterno));
                }
                if (registro.vCodigoMCPP != null && registro.vCodigoMCPP != string.Empty)
                {
                    cmd.Parameters.Add(new SqlParameter("@vCodigoMCPP", registro.vCodigoMCPP));
                }
                if (registro.vCodigoMEF != null && registro.vCodigoMEF != string.Empty)
                {
                    cmd.Parameters.Add(new SqlParameter("@vCodigoMEF", registro.vCodigoMEF));
                }
                if (registro.vClasificadorGasto != null && registro.vClasificadorGasto != string.Empty)
                {
                    cmd.Parameters.Add(new SqlParameter("@vClasificadorGasto", registro.vClasificadorGasto));
                }
                //cmd.Parameters.Add(new SqlParameter("@vUsuario", registro.IdUsuarioRegistro));

                cmd.ExecuteNonQuery();
                //registro.iCodRegistroRegimenPen = Int32.Parse(IdPropuestaParameter.Value.ToString());
                registro.iCodConcepto = Convert.ToInt32(parmOUT.Value);

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.iCodConcepto;
            //return resultado;
            
        }

        public Int32 ActualizarConcepto(Concepto_Registro registro)
        {

            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                cmd.CommandText = "[dbo].[paPlanillaConcepto_Actualizar]";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@iCodConcepto", registro.iCodConcepto));
                cmd.Parameters.Add(new SqlParameter("@iCodTipoConcepto", registro.iCodTipoConcepto));
                cmd.Parameters.Add(new SqlParameter("@iCodSubTipoConcepto", registro.iCodSubTipoConcepto));
                cmd.Parameters.Add(new SqlParameter("@vConcepto", registro.vConcepto));
                cmd.Parameters.Add(new SqlParameter("@vAbreviatura", registro.vAbreviatura));
                cmd.Parameters.Add(new SqlParameter("@bRegCAS", registro.bRegCAS));
                cmd.Parameters.Add(new SqlParameter("@bRegFunc", registro.bRegFunc));
                cmd.Parameters.Add(new SqlParameter("@bRegSeci", registro.bRegSeci));
                cmd.Parameters.Add(new SqlParameter("@bRegConceptoBaseImponible", registro.bRegConceptoBaseImponible));
                cmd.Parameters.Add(new SqlParameter("@bRegCalculoAutomatico", registro.bRegCalculoAutomatico));
                if (registro.vCodigoExterno != null && registro.vCodigoExterno != string.Empty)
                {
                    cmd.Parameters.Add(new SqlParameter("@vCodigoExterno", registro.vCodigoExterno));
                }
                if (registro.vCodigoMCPP != null && registro.vCodigoMCPP != string.Empty)
                {
                    cmd.Parameters.Add(new SqlParameter("@vCodigoMCPP", registro.vCodigoMCPP));
                }
                if (registro.vCodigoMEF != null && registro.vCodigoMEF != string.Empty)
                {
                    cmd.Parameters.Add(new SqlParameter("@vCodigoMEF", registro.vCodigoMEF));
                }
                if (registro.vClasificadorGasto != null && registro.vClasificadorGasto != string.Empty)
                {
                    cmd.Parameters.Add(new SqlParameter("@vClasificadorGasto", registro.vClasificadorGasto));
                }
                //cmd.Parameters.Add(new SqlParameter("@vUsuario", registro.IdUsuarioModificacion));
                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.iCodConcepto;
        }

        public Int32 EliminarConcepto(Concepto_Registro registro)
        {
            using (SqlCommand cmd = _iBasesSqlAdoUnitOfWork.ObtenerComandoDeConexion())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                cmd.CommandText = "[dbo].[paPlanillaConcepto_Eliminar]";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = cmd.CommandTimeout;

                cmd.Parameters.Add(new SqlParameter("@iCodConcepto", registro.iCodConcepto));

                cmd.ExecuteNonQuery();

                if (!_iBasesSqlAdoUnitOfWork.TieneTransaccion()) cmd.Connection.Close();
            }

            return registro.iCodConcepto;
        }
        #endregion

    }
}
