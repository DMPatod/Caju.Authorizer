using Caju.Authorizer.Domain.Transactions;
using Caju.Authorizer.Domain.Transactions.Repositories;
using Caju.Authorizer.Domain.Transactions.ValueObjects;

namespace Caju.Authorizer.Infrastructure.DataPersistence.SQLServer.Repositories
{
    internal class TransactionRepository : ITransactionRepository
    {
        private readonly SQLServerContext _context;

        public TransactionRepository(SQLServerContext context)
        {
            _context = context;
        }

        public async Task<Transaction> CreateAsync(Transaction entity, CancellationToken cancellationToken = default)
        {
            var ct = await _context.AddAsync(entity, cancellationToken);
            await _context.SaveAsync(cancellationToken);
            return ct.Entity;
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
