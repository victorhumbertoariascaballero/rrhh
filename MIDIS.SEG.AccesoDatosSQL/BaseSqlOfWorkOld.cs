using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace MIDIS.SEG.AccesoDatosSQL
{
    public class BaseSqlOfWorkOld
    {
        private readonly string CadenaDeConexion;
        private SqlTransaction SqlTransaccion;
        public int Timeout { get; set; }

        public BaseSqlOfWorkOld(string _cadenaDeConexion)
        {
            CadenaDeConexion = _cadenaDeConexion;
        }

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

        public bool TieneTransaccionOld()
        {
            return (SqlTransaccion != null);
        }

    }
}
