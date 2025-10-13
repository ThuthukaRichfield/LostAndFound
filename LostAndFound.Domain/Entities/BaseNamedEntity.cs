using System.ComponentModel.DataAnnotations.Schema;
using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace LostAndFound.Domain.Entities
{
    public abstract class BaseNamedEntity : BaseEntity
    {
        public BaseNamedEntity()
        {
            Name = null!;
        }

        public string Name { get; set; }
    }
}