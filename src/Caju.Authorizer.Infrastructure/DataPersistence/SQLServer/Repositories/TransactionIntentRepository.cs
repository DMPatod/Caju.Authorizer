using Caju.Authorizer.Domain.Transactions.Entities;
using Caju.Authorizer.Domain.Transactions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Caju.Authorizer.Infrastructure.DataPersistence.SQLServer.Repositories
{
    internal class TransactionIntentRepository : ITransactionIntentRepository
    {
        private readonly SQLServerContext _context;

        public TransactionIntentRepository(SQLServerContext context)
        {
            _context = context;
        }

        public async Task<TransactionIntent> CreateAsync(TransactionIntent entity, CancellationToken cancellationToken = default)
        {
            var ct = await _context.AddAsync(entity, cancellationToken);
            await _context.SaveAsync(cancellationToken);
            return ct.Entity;
        }

        public async Task<ICollection<TransactionIntent>> FindAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<TransactionIntent>().ToListAsync(cancellationToken);
        }
    }
}
