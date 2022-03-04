using BusinessLogic.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic.Interfaces
{
    public static class MyServices
    {
        public static IServiceCollection AddMyServices(this IServiceCollection services)
        {
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }
    }

}
