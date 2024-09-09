using Caju.Authorizer.Domain.Transactions.ValueObjects;

namespace Caju.Authorizer.Domain.Transactions.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction?> FindAsync(TransactionId id, CancellationToken cancellationToken = default);
        Task<ICollection<Transaction>> FindAsync(CancellationToken cancellationToken = default);
        Task<Transaction> CreateAsync(Transaction transaction, CancellationToken cancellationToken = default);
    }
}
