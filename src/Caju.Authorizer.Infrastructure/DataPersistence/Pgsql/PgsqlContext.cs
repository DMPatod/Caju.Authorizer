using DDD.Core.DataPersistence;

namespace Caju.Authorizer.Infrastructure.DataPersistence.Pgsql
{
    internal class PgsqlContext : IDomainContext
    {
        public Task SaveAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
