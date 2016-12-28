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
    public class GetAppUserByIdQuery : IQuery
    {
        public GetAppUserByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }

    }

    public class GetAppUserByIdQueryHandler : DapperQueryHandler<GetAppUserByIdQuery, AppUserModel>
    {
        private readonly string _sql;
        public GetAppUserByIdQueryHandler(IDbConnectionAsyncFactory factory) : base(factory)
        {
            _sql = @"SELECT TOP 1 * FROM AppUsers WHERE Id=@Id";
        }

        public override async Task<AppUserModel> HandleAsync(GetAppUserByIdQuery query)
        {
            AppUserModel appUser;
            using (var conn = await Factory.GetOpenConnectionAsync())
            {
                appUser = await conn.QuerySingleOrDefaultAsync<AppUserModel>(_sql, query);
                conn.Close();
            }
            return appUser;
        }
    }
}
