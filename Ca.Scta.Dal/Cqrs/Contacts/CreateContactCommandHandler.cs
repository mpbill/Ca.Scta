using System.Threading.Tasks;
using Ca.Scta.Dal.Connection;
using Ca.Scta.Dal.Cqrs.Base;
using Dapper;

namespace Ca.Scta.Dal.Cqrs.Contacts
{
    public class CreateContactCommandHandler : DapperCommandHandler<CreateContactCommand,GenericResult<int>>
    {
        private readonly string 
            _checkIfEmailExistsSql,
            _checkIfNameExistsSql,
            _checkIfPositionExistsSql,
            _createContactSql;
        
        public CreateContactCommandHandler(IDbConnectionAsyncFactory factory) : base(factory)
        {
            _createContactSql =
                @"
                    INSERT INTO Contacts 
	                    (
		                    City,
		                    [Description],
		                    Email,
		                    Name,
		                    Position
	                    )
                    VALUES
	                    (
		                    @City,
		                    @Description,
		                    @Email,
		                    @Name,
		                    @Position
	                    );
                    SELECT SCOPE_IDENTITY();";
            _checkIfEmailExistsSql
                = @"
                    SELECT TOP 1 
	                    c.ContactId
                    FROM
	                    Contacts c
                    WHERE
	                    c.Email=@Email;";
            _checkIfNameExistsSql
                = @"
                    SELECT TOP 1 
	                    c.ContactId
                    FROM
	                    Contacts c
                    WHERE
	                    c.Name=@Name;";
            _checkIfPositionExistsSql
                = @"
                    SELECT TOP 1 
	                    c.ContactId
                    FROM
	                    Contacts c
                    WHERE
	                    c.Position=@Position;";
            

        }

        public override async Task<GenericResult<int>> HandleAsync(CreateContactCommand command)
        {
            //todo:implament data annotations check here.
            GenericResult<int> result;
            using (var conn = await Factory.GetOpenConnectionAsync())
            {
                //todo: implement email, name, description check here.
                int id = await conn.ExecuteScalarAsync<int>(_createContactSql, command);
                conn.Close();
                result = GenericResult<int>.Success(id);
            }
            return result;
        }
    }
}