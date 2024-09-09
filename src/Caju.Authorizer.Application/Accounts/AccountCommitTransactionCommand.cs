using Caju.Authorizer.Domain.Accounts;
using Caju.Authorizer.Domain.Accounts.Repositories;
using Caju.Authorizer.Domain.Transactions;
using DDD.Core.Handlers;
using DDD.Core.Messages;

namespace Caju.Authorizer.Application.Accounts
{
    public record AccountCommitTransactionCommand(Account Account, Transaction Transaction, Guid Stamp)
        : ICommand;

    public class AccountCommitTransactionCommandHandler : ICommandHandler<AccountCommitTransactionCommand>
    {
        private readonly IAccountRepository _accountRepository;

        public AccountCommitTransactionCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<bool> Handle(AccountCommitTransactionCommand request, CancellationToken cancellationToken)
        {
            request.Account.CommitTransaction(request.Transaction, request.Stamp);
            await _accountRepository.UpdateAsync(request.Account, cancellationToken);

            return true;
        }
    }
}
