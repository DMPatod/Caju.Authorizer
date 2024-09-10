using Caju.Authorizer.Domain.Accounts.Repositories;
using Caju.Authorizer.Domain.Transactions.Repositories;
using Caju.Authorizer.Infrastructure.DataPersistence.SQLServer;
using Caju.Authorizer.Infrastructure.DataPersistence.SQLServer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Caju.Authorizer.Infrastructure.DataPersistence
{
    public static class BuildHandler
    {
        public static IServiceCollection AddDataPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SQLServerContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ITransactionIntentRepository, TransactionIntentRepository>();

            return services;
        }
    }
}
