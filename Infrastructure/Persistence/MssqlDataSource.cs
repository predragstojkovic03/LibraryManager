using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class MssqlDataSource
    {
        private SqlConnection connection;
        public SqlConnection Connection => connection;
        public SqlTransaction transaction;

        public MssqlDataSource()
        {
            connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SeminarskiPS;Integrated Security=True;");
        }

        public void OpenConnection()
        {
            connection.Open();
        }

        public SqlCommand CreateCommand(SqlTransaction? transaction)
        {
            SqlCommand cmd = new SqlCommand("", connection, transaction);
            return cmd;
        }

        public void CloseConnection()
        {
            if (connection != null && connection.State != System.Data.ConnectionState.Closed)
            {
                connection.Close();
            }
        }

        public void BeginTransaction()
        {
            transaction = connection.BeginTransaction();
        }

        public void Commit()
        {
            if (transaction != null)
            {
                transaction.Commit();
            }
        }

        public void Rollback()
        {
            if (transaction != null)
            {
                transaction.Rollback();
            }
        }
    }
}
