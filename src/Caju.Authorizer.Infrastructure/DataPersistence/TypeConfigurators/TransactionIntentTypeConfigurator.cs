using Caju.Authorizer.Domain.Transactions.Entities;
using Caju.Authorizer.Domain.Transactions.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Caju.Authorizer.Infrastructure.DataPersistence.TypeConfigurators
{
    internal class TransactionIntentTypeConfigurator : IEntityTypeConfiguration<TransactionIntent>
    {
        public void Configure(EntityTypeBuilder<TransactionIntent> builder)
        {
            builder.ToTable("TransactionIntents");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => TransactionIntentId.Create(value));

            builder.Property(t => t.ConcurrencyStamp);

            builder.Property(t => t.Authorized);

            builder.Property(t => t.Message);

            builder.Property(t => t.MetaData);
        }
    }
}
