using Caju.Authorizer.Domain.Transactions;
using Caju.Authorizer.Domain.Transactions.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Caju.Authorizer.Infrastructure.DataPersistence.TypeConfigurators
{
    internal class TransactionTypeConfigurator : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => TransactionId.Create(value));

            builder.Property(t => t.AccountId);

            builder.Property(t => t.Amount);

            builder.Property(t => t.Merchant);

            builder.Property(t => t.MCC);
        }
    }
}
