using Intent.RoslynWeaver.Attributes;
using LostAndFound.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.EntityTypeConfiguration", Version = "1.0")]

namespace LostAndFound.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.UserId);

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

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Email)
                .IsRequired();

            builder.Property(x => x.Pasword)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.Role)
                .IsRequired();
        }
    }
}