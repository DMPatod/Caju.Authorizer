using Caju.Authorizer.Domain.Transactions;
using Caju.Authorizer.Domain.Transactions.Repositories;
using Caju.Authorizer.Domain.Transactions.ValueObjects;

namespace Caju.Authorizer.Infrastructure.DataPersistence.Pgsql.Repositories
{
    internal class TransactionRepository : ITransactionRepository
    {
        public Task<Transaction> CreateAsync(Transaction transaction, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Transaction?> FindAsync(TransactionId id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Transaction>> FindAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
