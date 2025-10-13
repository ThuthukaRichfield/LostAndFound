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
            builder.HasBaseType<BaseNamedEntity>();

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