using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ca.Scta.Dal.Cqrs.Base;
using Ca.Scta.Dal.Connection;
using Dapper;

namespace Ca.Scta.Dal.Cqrs
{
    public class UpdateAppUserCommand : ICommand
    {
        public UpdateAppUserCommand(
            int id,
            string userName,
            string passwordHash,
            string securityStamp, 
            string email,
            bool emailConfirmed)
        {
            Id = id;
            UserName = userName;
            PasswordHash = passwordHash;
            SecurityStamp = securityStamp;
            Email = email;
            EmailConfirmed = emailConfirmed;
        }

        public int Id { get; private set; }
        public string UserName { get; private set; }
        public string PasswordHash { get; private set; }
        public string SecurityStamp { get; private set; }
        public string Email { get; private set; }
        public bool EmailConfirmed { get; private set; }
    }

    public class UpdateAppUserCommandHandler : DapperCommandHandler<UpdateAppUserCommand, bool>
    {
        private readonly string _sql;
        public UpdateAppUserCommandHandler(IDbConnectionAsyncFactory factory) : base(factory)
        {
            _sql = @"
                    UPDATE 
	                    AppUsers
                    SET
	                    UserName=@UserName,
	                    Email=@Email,
	                    EmailConfirmed=@EmailConfirmed,
	                    PasswordHash=@PasswordHash,
	                    SecurityStamp=@SecurityStamp
                    WHERE
	                    Id=@Id
                    ";
        }

        public override async Task<bool> HandleAsync(UpdateAppUserCommand command)
        {
            bool successfull;
            int updateCount;
            using (var conn = await Factory.GetOpenConnectionAsync())
            {
                updateCount = await conn.ExecuteAsync(_sql, command);
                conn.Close();
            }
            successfull = updateCount == 1;
            return successfull;
        }
    }
}
