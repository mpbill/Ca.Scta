using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ca.Scta.Dal.Connection
{
    public class DbConnectionAsyncFactory : IDbConnectionAsyncFactory
    {
        public async Task<IDbConnectionAsync> GetOpenConnectionAsync()
        {
             
            var dbConn = GetConnection();
            await dbConn.OpenAsync();
            return dbConn;
        }

        public IDbConnectionAsync GetConnection()
        {
            var conn = new SqlConnection("Data Source=localhost;Initial Catalog=Ca.Scta.Db;Integrated Security=True");
            var dbConn = new DbConnectionAsync(conn);
            return dbConn;
            return 
        }
    }
}
