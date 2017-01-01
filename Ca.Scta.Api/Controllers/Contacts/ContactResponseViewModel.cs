using Ca.Scta.Dal.Cqrs.Contacts;

namespace Ca.Scta.Api.Controllers.Contacts
{
    public class ContactResponseViewModel : Contact
    {
        public ContactResponseViewModel(Contact contact)
        {
            ContactId = contact.ContactId;
            City = contact.City;
            Name = contact.Name;
            Description = contact.Description;
            Email = contact.Email;
            Position = contact.Position;

        }
    }
}