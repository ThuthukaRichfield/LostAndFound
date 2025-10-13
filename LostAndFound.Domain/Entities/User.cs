using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace LostAndFound.Domain.Entities
{
    public class User : BaseNamedEntity
    {
        public User()
        {
            Email = null!;
            Pasword = null!;
        }

        public int UserId { get; set; }

        public string Email { get; set; }

        public string Pasword { get; set; }

        public UserRole Role { get; set; }
    }
}