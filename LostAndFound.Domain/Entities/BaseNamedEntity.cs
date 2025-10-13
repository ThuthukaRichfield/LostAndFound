using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace LostAndFound.Domain.Entities
{
    public class BaseNamedEntity : BaseEntity
    {
        public BaseNamedEntity()
        {
            Name = null!;
        }

        public string Name { get; set; }
    }
}