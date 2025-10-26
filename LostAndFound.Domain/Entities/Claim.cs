using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace LostAndFound.Domain.Entities
{
    public class Claim : BaseEntity
    {
        public Claim()
        {
            User = null!;
            Item = null!;
        }

        public int ClaimId { get; set; }

        public int ItemId { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual Item Item { get; set; }
    }
}