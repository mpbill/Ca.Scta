using System.Threading.Tasks;
using Ca.Scta.Dal.Connection;
using Ca.Scta.Dal.Cqrs.Base;
using Ca.Scta.Dal.Models;
using Dapper;

namespace Ca.Scta.Dal.Cqrs.AppUser.Queries
{
    public class GetAppUserByEmailQuery : IQuery
    {
        public GetAppUserByEmailQuery(string email)
        {
            Email = email;
        }

        public string Email { get; private set; }
    }

    public class GetAppUserByEmailQueryHandler : DapperQueryHandler<GetAppUserByEmailQuery, AppUserModel>
    {
        private readonly string _sql;

        public GetAppUserByEmailQueryHandler(IDbConnectionAsyncFactory factory) : base(factory)
        {
            _sql = @"SELECT * FROM AppUsers WHERE Email=@Email";
        }

        private class Parameter
        {
            public DbString Email { get; set; }

            public Parameter(GetAppUserByEmailQuery query)
            {
                Email = new DbString();
                Email.Value = query.Email;
                Email.IsAnsi = true;
                Email.IsFixedLength = false;
                
            }
        }
        public override async Task<AppUserModel> HandleAsync(GetAppUserByEmailQuery query)
        {
            
            var param = new Parameter(query);
            AppUserModel appUserModel;
            using (var conn = await Factory.GetOpenConnectionAsync())
            {
                appUserModel = await conn.QuerySingleAsync<AppUserModel>(_sql, param);
                conn.Close();
            }
            return appUserModel;

        }
    }
}
