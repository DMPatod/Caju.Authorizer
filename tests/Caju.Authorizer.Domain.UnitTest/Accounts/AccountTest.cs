using Caju.Authorizer.Domain.Accounts;
using Caju.Authorizer.Domain.Transactions;

namespace Caju.Authorizer.Domain.UnitTest.Accounts
{
    public class AccountTest
    {
        [Fact]
        public void Commit_ShouldBe_Valid()
        {
            var account = Account.Create(1000, 1000, 1000);

            var transaction = Transaction.Create("accountId", 100, "Padaria do Ze", "X");

            account.CommitTransaction(transaction, account.ConcurrencyStamp);

            Assert.Equal(900, account.AmountCash);
        }

        [Fact]
        public void Authorize_InsufficientFunds_ShouldReturnFalse()
        {
            var account = Account.Create(100, 100, 100);

            var transaction = Transaction.Create("accountId", 500, "Restaurant", "5411");

            var intent = account.Authorize(transaction);

            Assert.False(intent.Authorized);
            Assert.Equal("Insufficient funds", intent.Message);
        }

        [Fact]
        public void Authorize_InsufficientFoodFunds_ShouldFallbackToCash()
        {
            var account = Account.Create(100, 100, 1000);

            var transaction = Transaction.Create("accountId", 200, "Restaurant", "5411");

            var intent = account.Authorize(transaction);

            Assert.True(intent.Authorized);
            Assert.Equal("Insufficient Food funds, transaction processed with cash", intent.Message);
        }

        [Fact]
        public void Authorize_InsufficientMealFunds_ShouldFallbackToCash()
        {
            var account = Account.Create(100, 100, 1000);

            var transaction = Transaction.Create("accountId", 200, "Restaurant", "5811");

            var intent = account.Authorize(transaction);

            Assert.True(intent.Authorized);
            Assert.Equal("Insufficient Meal funds, transaction processed with cash", intent.Message);
        }

        [Fact]
        public void Authorize_ValidTransaction_ShouldReturnTrue()
        {
            var account = Account.Create(1000, 1000, 1000);

            var transaction = Transaction.Create("accountId", 100, "Restaurant", "5411");

            var intent = account.Authorize(transaction);

            Assert.True(intent.Authorized);
            Assert.Equal("Transaction processed successful", intent.Message);
        }

        [Fact]
        public void Commit_InvalidConcurrencyStamp_ShouldThrowException()
        {
            var account = Account.Create(1000, 1000, 1000);

            var transaction = Transaction.Create("accountId", 100, "Restaurant", "5411");

            Assert.Throws<Exception>(() => account.CommitTransaction(transaction, Guid.NewGuid()));
        }

        [Fact]
        public void Commit_ConcurrencyStamp_ShouldChangeAfterCommit()
        {
            var account = Account.Create(1000, 1000, 1000);
            var stampStr = account.ConcurrencyStamp.ToString();

            var transaction = Transaction.Create("accountId", 100, "Restaurant", "5411");

            account.CommitTransaction(transaction, account.ConcurrencyStamp);

            Assert.NotEqual(Guid.Parse(stampStr), account.ConcurrencyStamp);
        }
    }
}
