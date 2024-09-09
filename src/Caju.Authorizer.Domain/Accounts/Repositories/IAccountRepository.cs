using Caju.Authorizer.Domain.Accounts.ValueObjects;
using DDD.Core.Repositories;

namespace Caju.Authorizer.Domain.Accounts.Repositories
{
    public interface IAccountRepository : IBaseRepository<Account, AccountId>
    {
    }
}
