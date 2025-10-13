using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace LostAndFound.Domain.Entities
{
    public class Item : BaseEntity
    {
        public Item()
        {
            Title = null!;
            Category = null!;
            User = null!;
        }

        public int ItemId { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public ItemStatus Status { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}