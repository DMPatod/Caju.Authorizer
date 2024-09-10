using Caju.Authorizer.Application.Accounts;
using Caju.Authorizer.Domain.Accounts;
using Caju.Authorizer.Domain.Accounts.Repositories;
using Caju.Authorizer.Domain.Accounts.ValueObjects;
using Caju.Authorizer.Domain.Transactions;
using Caju.Authorizer.Domain.Transactions.ValueObjects;
using Moq;

namespace Caju.Authorizer.Application.UnitTest.Accounts
{
    public class AccountCommitTransactionCommandTests
    {
        [Theory]
        [InlineData(1000, 1000, 1000, 100, "5411")]
        [InlineData(1000, 1000, 1000, 100, "5412")]
        [InlineData(1000, 1000, 1000, 100, "5811")]
        [InlineData(1000, 1000, 1000, 100, "5812")]
        [InlineData(1000, 1000, 1000, 100, "X")]
        [InlineData(0, 1000, 1000, 100, "5411")]
        [InlineData(1000, 0, 1000, 100, "5811")]
        public async Task Handle_WhenValidRequest_ShouldUpdateAccount(double foodFunds,
                                                                      double mealFunds,
                                                                      double cashFunds,
                                                                      double transactionAmount,
                                                                      string mcc)
        {
            var accountId = AccountId.Create();
            var account = new Account(accountId, foodFunds, mealFunds, cashFunds);
            var transaction = new Transaction(TransactionId.Create(), accountId.ToString(), transactionAmount, "Padaria do Ze", mcc);

            var accountRepository = new Mock<IAccountRepository>();

            var command = new AccountCommitTransactionCommand(account, transaction, account.ConcurrencyStamp);
            var handler = new AccountCommitTransactionCommandHandler(accountRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            accountRepository.Verify(r => r.UpdateAsync(account, CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenConcurrencyStampDiffer_ShouldThrowException()
        {
            var accountId = AccountId.Create();
            var account = new Account(accountId, 1000, 1000, 1000);
            var transaction = new Transaction(TransactionId.Create(), accountId.ToString(), 100, "Padaria do Ze", "5411");

            var accountRepository = new Mock<IAccountRepository>();

            var command = new AccountCommitTransactionCommand(account, transaction, Guid.NewGuid());
            var handler = new AccountCommitTransactionCommandHandler(accountRepository.Object);

            await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, default));
        }

        [Fact]
        public async Task Handle_WhenAccountIdDiffer_ShouldThrowException()
        {
            var accountId = AccountId.Create();
            var account = new Account(accountId, 1000, 1000, 1000);
            var transaction = new Transaction(TransactionId.Create(), "invalid", 100, "Padaria do Ze", "5411");

            var accountRepository = new Mock<IAccountRepository>();

            var command = new AccountCommitTransactionCommand(account, transaction, account.ConcurrencyStamp);
            var handler = new AccountCommitTransactionCommandHandler(accountRepository.Object);

            await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, default));
        }

        [Theory]
        [InlineData(0, 1000, 0, 100, "5411")]
        [InlineData(0, 1000, 0, 100, "5412")]
        [InlineData(1000, 0, 0, 100, "5811")]
        [InlineData(1000, 0, 0, 100, "5812")]
        [InlineData(1000, 1000, 0, 100, "X")]
        [InlineData(0, 1000, 100, 1000, "5411")]
        [InlineData(1000, 0, 100, 1000, "5811")]
        public async Task Handle_WhenInvalidRequest_ShouldThrowException(double foodFunds,
                                                                      double mealFunds,
                                                                      double cashFunds,
                                                                      double transactionAmount,
                                                                      string mcc)
        {
            var accountId = AccountId.Create();
            var account = new Account(accountId, foodFunds, mealFunds, cashFunds);
            var transaction = new Transaction(TransactionId.Create(), accountId.ToString(), transactionAmount, "Padaria do Ze", mcc);

            var accountRepository = new Mock<IAccountRepository>();

            var command = new AccountCommitTransactionCommand(account, transaction, account.ConcurrencyStamp);
            var handler = new AccountCommitTransactionCommandHandler(accountRepository.Object);

            await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, default));
        }
    }
}
