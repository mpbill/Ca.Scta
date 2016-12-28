using System.Threading.Tasks;

namespace Ca.Scta.Dal.Connection
{
    public interface IDbConnectionAsyncFactory
    {
        Task<IDbConnectionAsync> GetOpenConnectionAsync();
        IDbConnectionAsync GetConnection();
    }
}