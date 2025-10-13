using Intent.RoslynWeaver.Attributes;
using LostAndFound.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.EntityTypeConfiguration", Version = "1.0")]

namespace LostAndFound.Infrastructure.Persistence.Configurations
{
    public class ClaimConfiguration : IEntityTypeConfiguration<Claim>
    {
        public void Configure(EntityTypeBuilder<Claim> builder)
        {
            builder.HasKey(x => x.ClaimId);

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

            builder.Property(x => x.ItemId)
                .IsRequired();

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Item)
                .WithMany()
                .HasForeignKey(x => x.ItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}