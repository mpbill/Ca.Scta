using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ca.Scta.Dal.Connection
{
    public interface IDbConnectionAsync : IDbConnection
    {
        Task OpenAsync();
        Task OpenAsync(CancellationToken token);
        SqlConnection GetInnerSqlConnection();
    }
    public class DbConnectionAsync : IDbConnectionAsync
    {
        private readonly SqlConnection _conn;

        public DbConnectionAsync(SqlConnection conn)
        {
            _conn = conn;
        }

        public async Task OpenAsync() => await _conn.OpenAsync();
        public async Task OpenAsync(CancellationToken token) => await _conn.OpenAsync(token);
        public SqlConnection GetInnerSqlConnection()
        {
            return _conn;
        }

        public void Dispose() => _conn.Dispose();

        public IDbTransaction BeginTransaction() => _conn.BeginTransaction();

        public IDbTransaction BeginTransaction(IsolationLevel il) => _conn.BeginTransaction(il);

        public void Close() => _conn.Close();

        public void ChangeDatabase(string databaseName) => _conn.ChangeDatabase(databaseName);

        public IDbCommand CreateCommand() => _conn.CreateCommand();

        public void Open() => _conn.Open();

        public string ConnectionString { get { return _conn.ConnectionString; } set { _conn.ConnectionString = value; } }
        public int ConnectionTimeout => _conn.ConnectionTimeout;
        public string Database => _conn.Database;
        public ConnectionState State => _conn.State;
    }
}
