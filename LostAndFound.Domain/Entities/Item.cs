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
            Location = null!;
            LostDescription = null!;
            User = null!;
        }

        public int ItemId { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public ItemStatus Status { get; set; }

        public int UserId { get; set; }

        public string Location { get; set; }

        public DateTime DateLost { get; set; }

        public IList<byte>? LostImage { get; set; } = [];

        public string LostDescription { get; set; }

        public virtual User User { get; set; }
    }
}