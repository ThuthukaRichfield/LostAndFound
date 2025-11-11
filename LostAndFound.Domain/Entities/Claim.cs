using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace LostAndFound.Domain.Entities
{
    public class Claim : BaseEntity
    {
        public Claim()
        {
            FoundDescription = null!;
            User = null!;
            Item = null!;
        }

        public int ClaimId { get; set; }

        public int ItemId { get; set; }

        public int UserId { get; set; }

        public IList<byte>? FoundImage { get; set; } = [];

        public string FoundDescription { get; set; }

        public ClaimStatus Status { get; set; }

        public virtual User User { get; set; }

        public virtual Item Item { get; set; }
    }
}
