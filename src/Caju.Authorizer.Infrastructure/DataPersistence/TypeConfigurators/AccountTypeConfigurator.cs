using Caju.Authorizer.Domain.Accounts;
using Caju.Authorizer.Domain.Accounts.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Caju.Authorizer.Infrastructure.DataPersistence.TypeConfigurators
{
    internal class AccountTypeConfigurator : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => AccountId.Create(value));

            builder.Property(a => a.AmountFood);

            builder.Property(a => a.AmountMeal);

            builder.Property(a => a.AmountCash);

            builder.Ignore(a => a.AmountTotal);

            builder.Property(a => a.ConcurrencyStamp)
                .IsConcurrencyToken();
        }
    }
}
