using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ca.Scta.Dal.Cqrs.Base;

namespace Ca.Scta.Dal.Cqrs.Contacts
{
    public class GetContactByIdQuery : IQuery
    {
        public GetContactByIdQuery(int contactId)
        {
            ContactId = contactId;
        }

        public int ContactId { get; private set; }
        
    }
}
