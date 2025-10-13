using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace LostAndFound.Domain.Entities
{
    public class Dispute : BaseEntity
    {
        public Dispute()
        {
            Reason = null!;
            Claim = null!;
        }

        public int DisputeId { get; set; }

        public string Reason { get; set; }

        public ClaimStatus Status { get; set; }

        public int ClaimId { get; set; }

        public virtual Claim Claim { get; set; }
    }
}