

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
                                     .HasColumnName("CurrencyCode");
                });
        }
    }
}
