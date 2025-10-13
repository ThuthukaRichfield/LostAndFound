using Intent.RoslynWeaver.Attributes;
using LostAndFound.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.EntityTypeConfiguration", Version = "1.0")]

namespace LostAndFound.Infrastructure.Persistence.Configurations
{
    public class DisputeConfiguration : IEntityTypeConfiguration<Dispute>
    {
        public void Configure(EntityTypeBuilder<Dispute> builder)
        {
            builder.HasKey(x => x.DisputeId);

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

            builder.Property(x => x.Reason)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.ClaimId)
                .IsRequired();

            builder.HasOne(x => x.Claim)
                .WithMany()
                .HasForeignKey(x => x.ClaimId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}