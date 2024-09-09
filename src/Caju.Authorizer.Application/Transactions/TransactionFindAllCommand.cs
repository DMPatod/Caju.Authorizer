using Caju.Authorizer.Domain.Transactions;
using Caju.Authorizer.Domain.Transactions.Repositories;
using DDD.Core.Handlers;
using DDD.Core.Messages;

namespace Caju.Authorizer.Application.Transactions
{
    public record TransactionFindAllCommand() : ICommand<ICollection<Transaction>>;

    public class TransactionFindAllCommandHandler : ICommandHandler<TransactionFindAllCommand, ICollection<Transaction>>
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionFindAllCommandHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<ICollection<Transaction>> Handle(TransactionFindAllCommand command, CancellationToken cancellationToken)
        {
            return await _transactionRepository.FindAsync(cancellationToken);
        }
    }
}
