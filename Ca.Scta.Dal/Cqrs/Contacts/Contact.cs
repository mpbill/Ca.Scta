using System;
using System.Linq;
using System.Text;

namespace Ca.Scta.Dal.Cqrs.Contacts
{
    public class Contact
    {
        public int ContactId { get; set; }
        public string City { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
    }
}
