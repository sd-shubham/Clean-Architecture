using App.Domain.Enities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EntityConfigurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Ignore(x => x.DomainEvents);
            builder.HasKey(x => x.Id);
            builder.Property(u => u.UserName).HasMaxLength(200)
                   .IsRequired();
            builder.HasIndex(u => u.UserName).IsUnique();
        }
    }
}
