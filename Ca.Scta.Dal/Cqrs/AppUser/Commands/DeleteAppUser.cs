using System.Threading.Tasks;
using Ca.Scta.Dal.Connection;
using Ca.Scta.Dal.Cqrs.Base;
using Dapper;

namespace Ca.Scta.Dal.Cqrs.AppUser.Commands
{
    public class DeleteAppUserCommand : ICommand
    {
        public DeleteAppUserCommand(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }

    }

    public class DeleteAppUserCommandHandler : DapperCommandHandler<DeleteAppUserCommand, bool>
    {
        private readonly string _sql;
        public DeleteAppUserCommandHandler(IDbConnectionAsyncFactory factory) : base(factory)
        {
            _sql = @"DELETE FROM AppUsers WHERE Id=@Id";
        }

        public override async Task<bool> HandleAsync(DeleteAppUserCommand command)
        {
            bool successfull;
            using (var conn = await Factory.GetOpenConnectionAsync())
            {
                var count = await conn.ExecuteAsync(_sql, command);
                successfull = count == 1;
                conn.Close();
            }
            return successfull;
        }
    }
}
