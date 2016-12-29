using System.Threading.Tasks;
using Ca.Scta.Dal.Connection;
using Ca.Scta.Dal.Cqrs.Base;
using Dapper;

namespace Ca.Scta.Dal.Cqrs.AppUser.Commands
{
    public class AddAppUserCommand : ICommand
    {
        public AddAppUserCommand(string userName, string passwordHash, string securityStamp, string email, bool emailConfirmed)
        {
            UserName = userName;
            PasswordHash = passwordHash;
            SecurityStamp = securityStamp;
            Email = email;
            EmailConfirmed = emailConfirmed;
        }
        
        public string UserName { get; private set; }
        public string PasswordHash { get; private set; }
        public string SecurityStamp { get; private set; }
        public string Email { get; private set; }
        public bool EmailConfirmed { get; private set; }
    }

    public class AddAppUserCommandHandler : DapperCommandHandler<AddAppUserCommand, int>
    {
        private readonly string _sql;
        public AddAppUserCommandHandler(IDbConnectionAsyncFactory factory) : base(factory)
        {
            _sql = @"
                    INSERT INTO AppUsers
                    (
	                    UserName,
	                    Email,
	                    EmailConfirmed,
	                    PasswordHash,
	                    SecurityStamp
                    )
                    VALUES
                    (
	                    @UserName,
	                    @Email,
	                    @EmailConfirmed,
	                    @PasswordHash,
	                    @SecurityStamp
                    );
                    SELECT SCOPE_IDENTITY();
                    ";
        }

        public override async Task<int> HandleAsync(AddAppUserCommand command)
        {
            int scopeIdentity;
            using (var conn = await Factory.GetOpenConnectionAsync())
            {
                scopeIdentity = await conn.ExecuteScalarAsync<int>(_sql, command);
                conn.Close();
            }
            return scopeIdentity;
        }
    }
}
