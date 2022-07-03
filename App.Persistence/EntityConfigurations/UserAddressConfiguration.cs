using App.Domain.Enities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EntityConfigurations
{
    internal class UserAddressConfiguration : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(a => a.PinCode)
                   .HasMaxLength(6);
            builder.HasOne(x => x.User)
                  .WithMany(u => u.UserAddresses)
                  .HasForeignKey(x => x.UserID);
        }
    }
}
