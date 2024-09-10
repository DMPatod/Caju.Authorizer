using Caju.Authorizer.Application.Accounts;
using Caju.Authorizer.Domain.Accounts;
using Caju.Authorizer.Domain.Accounts.Repositories;
using Moq;

namespace Caju.Authorizer.Application.UnitTest.Accounts
{
    public class AccountCreateCommandTests
    {
        [Fact]
        public async Task Handler_ShouldCreateAccount()
        {
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(r => r.CreateAsync(It.IsAny<Account>(), CancellationToken.None));

            var handler = new AccountCreateCommandHandler(accountRepository.Object);

            var result = await handler.Handle(new AccountCreateCommand(100, 100, 100), CancellationToken.None);

            Assert.Equal(100, result.AmountFood);
            Assert.Equal(100, result.AmountMeal);
            Assert.Equal(100, result.AmountCash);
            accountRepository.Verify(r => r.CreateAsync(It.IsAny<Account>(), CancellationToken.None), Times.Once);
        }
    }
}
