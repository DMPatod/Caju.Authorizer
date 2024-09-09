using Caju.Authorizer.Domain.Transactions.Entities;

namespace Caju.Authorizer.Domain.Transactions.Repositories
{
    public interface ITransactionIntentRepository
    {
        Task<TransactionIntent> CreateAsync(TransactionIntent intent, CancellationToken cancellationToken = default);
        Task<ICollection<TransactionIntent>> FindAsync(CancellationToken cancellationToken = default);
    }
}
