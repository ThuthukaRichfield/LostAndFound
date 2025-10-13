using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace LostAndFound.Domain.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreatedBy = null!;
        }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string? LastModifiedBy { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public bool Disabled { get; set; }

        public bool Deleted { get; set; }
    }
}