using App.Domain.Enities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EntityConfigurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(u => u.UserName).HasMaxLength(200)
                   .IsRequired();
            // optional according to you requirment
            builder.HasIndex(u => new
            {
                u.Id,
                u.UserName
            });
        }
    }
}
