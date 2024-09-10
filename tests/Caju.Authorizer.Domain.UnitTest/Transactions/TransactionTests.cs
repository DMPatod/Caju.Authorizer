using Caju.Authorizer.Domain.Transactions;

namespace Caju.Authorizer.Domain.UnitTest.Transactions
{
    public class TransactionTests
    {
        [Fact]
        public void Create_ShouldBe_Valid()
        {
            var transaction = Transaction.Create("accountId", 100, "Padaria do Ze", "X");

            Assert.Equal("accountId", transaction.AccountId);
            Assert.Equal(100, transaction.Amount);
            Assert.Equal("Padaria do Ze", transaction.Merchant);
            Assert.Equal("X", transaction.MCC);
        }
    }
}
