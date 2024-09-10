using DDD.Core.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Caju.Authorizer.Application
{
    public static class BuildHandler
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddDefaultMessageHandler(typeof(BuildHandler).Assembly);

            return services;
        }
    }
}
