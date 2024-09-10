using Caju.Authorizer.Application.Transactions.TransactionIntents;
using Caju.Authorizer.Domain.Accounts.Repositories;
using Caju.Authorizer.Domain.Transactions.Events;
using DDD.Core.Handlers.SHS.RD.CGC.Core.DomainEvents;
using Moq;

namespace Caju.Authorizer.Application.UnitTest.Transactions.TransactionIntents
{
    public class TransactionIntentCreatedEventHandlerTests
    {
        [Fact]
        public async Task Handle_WhenIntentIsNull_ShouldThrowException()
        {
            var messageHandler = new Mock<IMessageHandler>();
            var accountRepository = new Mock<IAccountRepository>();

            var handler = new TransactionIntentCreatedEventHandler(messageHandler.Object, accountRepository.Object);

            var notification = new TransactionIntentCreatedEvent(null!);

            await Assert.ThrowsAsync<Exception>(() => handler.Handle(notification, CancellationToken.None));
        }
    }
}
