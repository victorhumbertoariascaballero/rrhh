using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MIDIS.SEG.AccesoDatosSQL
{
    public class BaseSqlAdoUnitOfWork 
    {
        private readonly string CadenaDeConexion;
        private SqlTransaction SqlTransaccion;
        public int Timeout { get; set; }

        public BaseSqlAdoUnitOfWork(string _cadenaDeConexion)
        {
            CadenaDeConexion = _cadenaDeConexion; 
        }

        /*
        public string ObtenerCadenaDeConexion()
        {
            return CadenaDeConexion;
        }
        */

        public SqlCommand ObtenerComandoDeConexion()
        {
            SqlConnection cnn = new SqlConnection(CadenaDeConexion);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;

            if (SqlTransaccion != null)
            {
                cmd.Connection = SqlTransaccion.Connection;
                cmd.Transaction = SqlTransaccion;
            }

            return cmd;
        }

        public void IniciarTransaccion(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            var cn = new SqlConnection(CadenaDeConexion);
            if (cn.State != ConnectionState.Open) cn.Open();
            SqlTransaccion = cn.BeginTransaction(isolationLevel);
        }

        public bool ConfirmarTransaccion()
        {
            SqlTransaccion.Commit();
            SqlTransaccion.Dispose();
            SqlTransaccion = null;
            return true;
        }

        public void RetrocederTransaccion()
        {
            SqlTransaccion.Rollback();
            SqlTransaccion.Dispose();
            SqlTransaccion = null;
        }

        public bool TieneTransaccion()
        {
            return (SqlTransaccion != null);
        }
    }
}
