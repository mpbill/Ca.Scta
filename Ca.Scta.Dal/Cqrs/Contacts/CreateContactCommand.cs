using System.ComponentModel.DataAnnotations;
using Ca.Scta.Dal.Cqrs.Base;

namespace Ca.Scta.Dal.Cqrs.Contacts
{
    public class CreateContactCommand : ICommand
    {
        public CreateContactCommand(
            string city,
            string name,
            string description,
            string position,
            string email
        )
        {
            City = city;
            Name = name;
            Description = description;
            Position = position;
            Email = email;
        }
        [MaxLength(50)]
        public string City { get; private set; }
        [MaxLength(50)]
        public string Name { get; private set; }
        [MaxLength(50)]
        public string Description { get; private set; }
        [MaxLength(50)]
        public string Position { get; private set; }
        [MaxLength(254)]
        public string Email { get; private set; }
    }
}