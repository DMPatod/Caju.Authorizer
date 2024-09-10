using Caju.Authorizer.Domain.Transactions;
using Caju.Authorizer.Domain.Transactions.Entities;

namespace Caju.Authorizer.Domain.UnitTest.Transactions
{
    public class TransactionIntentTests
    {
        [Fact]
        public void Create_ShouldBe_Valid()
        {
            var transaction = Transaction.Create("accountId", 100, "Padaria do Ze", "X");

            var intent = TransactionIntent.Create(transaction, Guid.NewGuid(), true, "message", "metadata");

            Assert.True(intent.Authorized);
            Assert.Equal("message", intent.Message);
            Assert.NotEmpty(intent.DomainEvents);
        }
    }
}
