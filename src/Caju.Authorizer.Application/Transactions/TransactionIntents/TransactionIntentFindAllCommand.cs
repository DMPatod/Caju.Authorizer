using Caju.Authorizer.Domain.Transactions.Entities;
using Caju.Authorizer.Domain.Transactions.Repositories;
using DDD.Core.Handlers;
using DDD.Core.Messages;

namespace Caju.Authorizer.Application.Transactions.TransactionIntents
{
    public record TransactionIntentFindAllCommand() : ICommand<ICollection<TransactionIntent>>;

    public class TransactionIntentFindAllCommandHandler : ICommandHandler<TransactionIntentFindAllCommand, ICollection<TransactionIntent>>
    {
        private readonly ITransactionIntentRepository _transactionIntentRepository;

        public TransactionIntentFindAllCommandHandler(ITransactionIntentRepository transactionIntentRepository)
        {
            _transactionIntentRepository = transactionIntentRepository;
        }

        public async Task<ICollection<TransactionIntent>> Handle(TransactionIntentFindAllCommand command, CancellationToken cancellationToken)
        {
            return await _transactionIntentRepository.FindAsync(cancellationToken);
        }
    }
}
