using System.Threading.Tasks;
using Ca.Scta.Dal.Connection;
using Ca.Scta.Dal.Cqrs.Base;
using Dapper;

namespace Ca.Scta.Dal.Cqrs.Contacts
{
    public class UpdateContactByIdCommandHandler : DapperCommandHandler<UpdateContactByIdCommand, GenericResult<int>>
    {
        private readonly string _sql;

        public UpdateContactByIdCommandHandler(IDbConnectionAsyncFactory factory) : base(factory)
        {
            _sql = @"
                    UPDATE Contacts
                    SET
	                    City=@City,
	                    Name=@Name,
	                    [Description]=@Description,
	                    Position=@Position,
	                    Email=@Email
                    WHERE
	                    ContactId=@ContactId;";
        }

        public override async Task<GenericResult<int>> HandleAsync(UpdateContactByIdCommand command)
        {
            int count;
            using (var conn = await Factory.GetOpenConnectionAsync())
            {
                count = await conn.ExecuteAsync(_sql, command);
                conn.Close();
            }
            if(count==1)
                return GenericResult<int>.Success(command.ContactId);
            return GenericResult<int>.Failure(ErrorReason.NotFound);
        }
    }
}