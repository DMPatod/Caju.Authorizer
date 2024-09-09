using Caju.Authorizer.Domain.Accounts;
using Caju.Authorizer.Domain.Accounts.Repositories;
using Caju.Authorizer.Domain.Accounts.ValueObjects;

namespace Caju.Authorizer.Infrastructure.DataPersistence.Pgsql.Repositories
{
    internal class AccountRepository : IAccountRepository
    {
        public Task<Account> CreateAsync(Account entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Account entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Account?> FindAsync(AccountId id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Account>> FindAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Account entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
