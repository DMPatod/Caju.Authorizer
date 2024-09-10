using Caju.Authorizer.Domain.Accounts;
using Caju.Authorizer.Domain.Accounts.Repositories;
using Caju.Authorizer.Domain.Accounts.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Caju.Authorizer.Infrastructure.DataPersistence.SQLServer.Repositories
{
    internal class AccountRepository : IAccountRepository
    {
        private readonly SQLServerContext _context;

        public AccountRepository(SQLServerContext context)
        {
            _context = context;
        }

        public async Task<Account> CreateAsync(Account entity, CancellationToken cancellationToken = default)
        {
            var ct = await _context.AddAsync(entity, cancellationToken);
            await _context.SaveAsync(cancellationToken);
            return ct.Entity;
        }

        public Task DeleteAsync(Account entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Account?> FindAsync(AccountId id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Account>().FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        public async Task<ICollection<Account>> FindAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<Account>().ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(Account entity, CancellationToken cancellationToken = default)
        {
            _context.Update(entity);
            await _context.SaveAsync(cancellationToken);
        }
    }
}
