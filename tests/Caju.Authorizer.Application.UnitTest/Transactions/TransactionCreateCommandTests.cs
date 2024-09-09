using Caju.Authorizer.Application.Transactions;
using Caju.Authorizer.Domain.Accounts;
using Caju.Authorizer.Domain.Accounts.Repositories;
using Caju.Authorizer.Domain.Accounts.ValueObjects;
using Caju.Authorizer.Domain.Transactions.Repositories;
using Moq;

namespace Caju.Authorizer.Application.UnitTest.Transactions
{
    public class TransactionCreateCommandTests
    {
        [Theory]
        [InlineData(1000, 1000, 1000, 100, "5411")]
        [InlineData(1000, 1000, 1000, 100, "5412")]
        [InlineData(1000, 1000, 1000, 100, "5811")]
        [InlineData(1000, 1000, 1000, 100, "5812")]
        [InlineData(1000, 1000, 1000, 100, "X")]
        [InlineData(0, 1000, 1000, 100, "5411")]
        [InlineData(1000, 0, 1000, 100, "5811")]
        public async Task Handle_WhenValidRequest_ShouldCreateValidTransactionAndIntent(double foodFunds,
                                                                                        double mealFunds,
                                                                                        double cashFunds,
                                                                                        double transactionAmount,
                                                                                        string mcc)
        {
            var accountId = AccountId.Create();
            var account = new Account(accountId, foodFunds, mealFunds, cashFunds);

            var transactionRepository = new Mock<ITransactionRepository>();
            var intentRepository = new Mock<ITransactionIntentRepository>();
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(r => r.FindAsync(accountId, default)).ReturnsAsync(account);

            var handler = new TransactionCreateCommandHandler(transactionRepository.Object, intentRepository.Object, accountRepository.Object);
            var command = new TransactionCreateCommand(accountId.ToString(), transactionAmount, "Padaria do Ze", mcc);

            var result = await handler.Handle(command, default);

            Assert.True(result.Authorized);
            Assert.Equal(result.ConcurrencyStamp, account.ConcurrencyStamp);
            Assert.NotEmpty(result.DomainEvents);
        }

        [Fact]
        public async Task Handle_WhenInvalidAccount_ShouldThrowException()
        {
            var transactionRepository = new Mock<ITransactionRepository>();
            var intentRepository = new Mock<ITransactionIntentRepository>();
            var accountRepository = new Mock<IAccountRepository>();

            var handler = new TransactionCreateCommandHandler(transactionRepository.Object, intentRepository.Object, accountRepository.Object);
            var command = new TransactionCreateCommand("invalid", 100, "Padaria do Ze", "5811");

            await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, default));
        }

        [Fact]
        public async Task Handle_WhenAccountNotFound_ShouldThrowException()
        {
            var accountId = AccountId.Create();

            var transactionRepository = new Mock<ITransactionRepository>();
            var intentRepository = new Mock<ITransactionIntentRepository>();
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(r => r.FindAsync(accountId, default)).ReturnsAsync((Account)null!);

            var handler = new TransactionCreateCommandHandler(transactionRepository.Object, intentRepository.Object, accountRepository.Object);
            var command = new TransactionCreateCommand(accountId.ToString(), 100, "Padaria do Ze", "5811");

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
        public async Task Handle_WhenAccountHasInsufficientFunds_ShouldCreateInvalidIntent(double foodFunds,
                                                                                        double mealFunds,
                                                                                        double cashFunds,
                                                                                        double transactionAmount,
                                                                                        string mcc)
        {
            var accountId = AccountId.Create();
            var account = new Account(accountId, foodFunds, mealFunds, cashFunds);

            var transactionRepository = new Mock<ITransactionRepository>();
            var intentRepository = new Mock<ITransactionIntentRepository>();
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(r => r.FindAsync(accountId, default)).ReturnsAsync(account);

            var handler = new TransactionCreateCommandHandler(transactionRepository.Object, intentRepository.Object, accountRepository.Object);
            var command = new TransactionCreateCommand(accountId.ToString(), transactionAmount, "Padaria do Ze", mcc);

            var result = await handler.Handle(command, default);

            Assert.False(result.Authorized);
            Assert.Equal(result.ConcurrencyStamp, account.ConcurrencyStamp);
            Assert.NotEmpty(result.DomainEvents);
        }
    }
}
