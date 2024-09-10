using Caju.Authorizer.Infrastructure.DataPersistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Caju.Authorizer.Infrastructure
{
    public static class BuildHandler
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataPersistence(configuration);

            return services;
        }
    }
}
