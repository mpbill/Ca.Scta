using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ca.Scta.Dal.Connection;
using Ca.Scta.Dal.Cqrs.Base;
using Dapper;

namespace Ca.Scta.Dal.Cqrs.Contacts
{
    public abstract class GenericId
    {
        protected GenericId(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }

    }
    public class DeleteContactByIdCommand : GenericId, ICommand
    {
        public DeleteContactByIdCommand(int id) : base(id)
        {
        }
    }

    
    public class DeleteContactByIdCommandHandler : DapperCommandHandler<DeleteContactByIdCommand, GenericResult<int>>
    {
        private readonly string _sql;
        public DeleteContactByIdCommandHandler(IDbConnectionAsyncFactory factory) : base(factory)
        {
            _sql = "DELETE FROM Contacts WHERE ContactId=@Id";
        }

        public override async Task<GenericResult<int>> HandleAsync(DeleteContactByIdCommand command)
        {
            int count;
            using (var conn = await Factory.GetOpenConnectionAsync())
            {
                count = await conn.ExecuteAsync(_sql, command);
                conn.Close();

            }
            if (count == 1)
                return GenericIntResult.Success(command.Id);
            return GenericResult<int>.Failure(ErrorReason.NotFound);
        }
    }
}
