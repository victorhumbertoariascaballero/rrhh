using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;

namespace MIDIS.SEG.AccesoDatosSQL
{
    public class BaseOracleAdoUnitOfWork 
    {
        private readonly string CadenaDeConexion;
        private OracleTransaction OracleTransaccion;
        public int Timeout { get; set; }

        public BaseOracleAdoUnitOfWork(string _cadenaDeConexion)
        {
            CadenaDeConexion = _cadenaDeConexion; 
        }

        public OracleCommand ObtenerComandoDeConexion()
        {
            OracleConnection cnn = new OracleConnection(CadenaDeConexion);
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = cnn;

            if (OracleTransaccion != null)
            {
                cmd.Connection = OracleTransaccion.Connection;
                cmd.Transaction = OracleTransaccion;
            }

            return cmd;
        }

        //public void IniciarTransaccion(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        //{
        //    var cn = new SqlConnection(CadenaDeConexion);
        //    if (cn.State != ConnectionState.Open) cn.Open();
        //    OracleTransaccion = cn.BeginTransaction(isolationLevel);
        //}

        //public bool ConfirmarTransaccion()
        //{
        //    OracleTransaccion.Commit();
        //    return true;
        //}

        //public void RetrocederTransaccion()
        //{
        //    OracleTransaccion.Rollback();
        //}

        //public bool TieneTransaccion()
        //{
        //    return (OracleTransaccion != null);
        //}
    }
}
