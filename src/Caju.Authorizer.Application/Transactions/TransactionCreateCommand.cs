using Caju.Authorizer.Domain.Accounts.Repositories;
using Caju.Authorizer.Domain.Accounts.ValueObjects;
using Caju.Authorizer.Domain.Transactions;
using Caju.Authorizer.Domain.Transactions.Entities;
using Caju.Authorizer.Domain.Transactions.Repositories;
using DDD.Core.Handlers;
using DDD.Core.Messages;

namespace Caju.Authorizer.Application.Transactions
{
    public record TransactionCreateCommand(
        string Account,
        double Amount,
        string Merchant,
        string Mcc
        ) : ICommand<TransactionIntent>;

    public class TransactionCreateCommandHandler : ICommandHandler<TransactionCreateCommand, TransactionIntent>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransactionIntentRepository _transactionIntenteRepository;
        private readonly IAccountRepository _accountRepository;

        public TransactionCreateCommandHandler(ITransactionRepository transactionRepository, ITransactionIntentRepository transactionIntenteRepository, IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _transactionIntenteRepository = transactionIntenteRepository;
            _accountRepository = accountRepository;
        }

        public async Task<TransactionIntent> Handle(TransactionCreateCommand request, CancellationToken cancellationToken)
        {
            var transaction = Transaction.Create(request.Account, request.Amount, request.Merchant, request.Mcc);
            await _transactionRepository.CreateAsync(transaction, cancellationToken);

            var accountId = Guid.TryParse(request.Account, out var value) ? AccountId.Create(value) : throw new Exception("Invalid Account");
            var account = await _accountRepository.FindAsync(accountId, cancellationToken) ?? throw new Exception("Account not found");

            var intent = account.Authorize(transaction);
            await _transactionIntenteRepository.CreateAsync(intent, cancellationToken);

            return intent;
        }
    }
}
