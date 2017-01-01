using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ca.Scta.Dal.Connection;
using Ca.Scta.Dal.Cqrs.Base;
using Dapper;

namespace Ca.Scta.Dal.Cqrs.Contacts
{
    public class GetAllContactsQueryHandler : DapperQueryHandler<GetAllContactsQuery, List<Contact>>
    {
        private readonly string _sql;
        public GetAllContactsQueryHandler(IDbConnectionAsyncFactory factory) : base(factory)
        {
            _sql = "SELECT * FROM Contacts;";
        }

        public override async Task<List<Contact>> HandleAsync(GetAllContactsQuery query)
        {
            IEnumerable<Contact> contactsEnum;
            using (var conn = await Factory.GetOpenConnectionAsync())
            {
                contactsEnum = await conn.QueryAsync<Contact>(_sql);
                conn.Close();
            }
            return contactsEnum.ToList();
        }
    }
}