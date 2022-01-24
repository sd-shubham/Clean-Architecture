

using App.Domain.Enities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EntityConfigurations
{
    internal class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(x => x.Id);
            builder.OwnsOne(account => account.Currency,
                NavigationBuilder =>
                {
                    NavigationBuilder.Property(currency => currency.CurrencyCode)
                                     .IsRequired()
                                     .HasColumnName("CurrencyCode")
                                     .HasMaxLength(3);
                });
            //we need to add the IsRequired on the prop of the OwnEntity and on the navigation
            //Property, it required to do that if the project use Nullable reference
            builder.Navigation(b => b.Currency).IsRequired(); 
        }
    }
}
