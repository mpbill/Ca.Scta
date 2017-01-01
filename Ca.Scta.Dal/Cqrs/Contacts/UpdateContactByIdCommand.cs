using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ca.Scta.Dal.Cqrs.Base;

namespace Ca.Scta.Dal.Cqrs.Contacts
{
    public class UpdateContactByIdCommand : CreateContactCommand, ICommand
    {
        public int ContactId { get; private set; }
        public UpdateContactByIdCommand(string city, string name, string description, string position, string email, int contactId) : base(city, name, description, position, email)
        {
            ContactId = contactId;
        }
    }
}
