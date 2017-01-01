using System.Threading.Tasks;
using Ca.Scta.Dal.Connection;
using Ca.Scta.Dal.Cqrs.Base;
using Dapper;

namespace Ca.Scta.Dal.Cqrs.Contacts
{
    public class GetContactByIdQueryHandler : DapperQueryHandler<GetContactByIdQuery,Contact>
    {
        private readonly string _sql;
        public GetContactByIdQueryHandler(IDbConnectionAsyncFactory factory) : base(factory)
        {
            _sql = "SELECT TOP 1 * FROM Contacts WHERE ContactId=@ContactId;";

        }

        public override async Task<Contact> HandleAsync(GetContactByIdQuery query)
        {
            Contact contact;
            using (var conn = await Factory.GetOpenConnectionAsync())
            {
                contact = await conn.QueryFirstAsync<Contact>(_sql, query);
                conn.Close();
            }
            return contact;
        }
    }
}