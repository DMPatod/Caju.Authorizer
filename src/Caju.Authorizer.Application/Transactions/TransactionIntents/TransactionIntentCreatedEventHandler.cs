using Caju.Authorizer.Application.Accounts;
using Caju.Authorizer.Domain.Accounts.Repositories;
using Caju.Authorizer.Domain.Accounts.ValueObjects;
using Caju.Authorizer.Domain.Transactions.Events;
using DDD.Core.Handlers;
using DDD.Core.Handlers.SHS.RD.CGC.Core.DomainEvents;

namespace Caju.Authorizer.Application.Transactions.TransactionIntents
{
    public class TransactionIntentCreatedEventHandler : IEventHandler<TransactionIntentCreatedEvent>
    {
        private readonly IMessageHandler _messageHandler;
        private readonly IAccountRepository _accountRepository;

        public TransactionIntentCreatedEventHandler(IMessageHandler messageHandler, IAccountRepository accountRepository)
        {
            _messageHandler = messageHandler;
            _accountRepository = accountRepository;
        }

        public async Task Handle(TransactionIntentCreatedEvent notification, CancellationToken cancellationToken)
        {
            if (notification.Intent is null)
            {
                throw new Exception("Unespected event trigged");
            }

            if (!notification.Intent.Authorized)
            {
                return;
            }

            var accountIdStr = notification.Intent.Transaction.AccountId;
            var accountId = Guid.TryParse(accountIdStr, out var value) ? AccountId.Create(value) : throw new Exception("Invalid Account");
            var account = await _accountRepository.FindAsync(accountId, cancellationToken) ?? throw new Exception("Account not found");

            var command = new AccountCommitTransactionCommand(account, notification.Intent.Transaction, notification.Intent.ConcurrencyStamp);
            await _messageHandler.SendAsync(command, cancellationToken);
        }
    }
}
