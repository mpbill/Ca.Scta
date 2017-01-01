using System.Collections.Generic;

namespace Ca.Scta.Api.Controllers.Contacts
{
    public class GetAllContactsResponseViewModel
    {
        public List<ContactResponseViewModel> AllContacts { get; set; }

        public GetAllContactsResponseViewModel(List<ContactResponseViewModel> contacts)
        {
            AllContacts = contacts;
        }
    }
}