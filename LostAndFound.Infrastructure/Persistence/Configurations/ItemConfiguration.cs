using Intent.RoslynWeaver.Attributes;
using LostAndFound.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.EntityTypeConfiguration", Version = "1.0")]

namespace LostAndFound.Infrastructure.Persistence.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(x => x.ItemId);

            builder.Property(x => x.CreatedBy)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.CreatedDate)
                .IsRequired();

            builder.Property(x => x.LastModifiedBy)
                .HasMaxLength(150);

            builder.Property(x => x.LastModifiedDate);

            builder.Property(x => x.Disabled)
                .IsRequired();

            builder.Property(x => x.Deleted)
                .IsRequired();

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Category)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.Location)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.DateLost)
                .IsRequired();

            builder.Property(x => x.LostImage);

            builder.Property(x => x.LostDescription)
                .IsRequired()
                .HasMaxLength(250);

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}