using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ca.Scta.Dal.Connection;
using Ca.Scta.Dal.Cqrs.Base;
using Ca.Scta.Dal.Models;
using Dapper;

namespace Ca.Scta.Dal.Cqrs
{
    public class GetAppUserByUserNameQuery : IQuery
    {
        public GetAppUserByUserNameQuery(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; private set; }
    }

    public class GetAppUserByUserNameQueryHandler : DapperQueryHandler<GetAppUserByUserNameQuery, AppUserModel>
    {
        private readonly string _sql;
        public GetAppUserByUserNameQueryHandler(IDbConnectionAsyncFactory factory) : base(factory)
        {
            _sql = @"SELECT TOP 1 FROM AppUsers WHERE UserName=@UserName";
        }

        private class Parameter
        {
            public DbString UserName { get; set; }

            public Parameter(GetAppUserByUserNameQuery query)
            {
                UserName = new DbString();
                UserName.IsAnsi = true;
                UserName.IsFixedLength = false;
                UserName.Value = query.UserName;

            }
        }
        public override async Task<AppUserModel> HandleAsync(GetAppUserByUserNameQuery query)
        {
            AppUserModel model;
            var param = new Parameter(query);
            using (var conn = await Factory.GetOpenConnectionAsync())
            {
                model = await conn.QuerySingleOrDefaultAsync<AppUserModel>(_sql, param);
                conn.Close();
            }
            return model;

        }
    }
}
