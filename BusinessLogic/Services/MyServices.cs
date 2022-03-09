using DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic.Interfaces
{
    public static class MyServices
    {
        //Gathering all Repositories and Registering for DI in one file for better read and usability.
        //Finally after declaring all of the interfaces and classes here. This Class going to use in WebApp Startup for final registration.
        public static IServiceCollection AddMyServices(this IServiceCollection services)
        {
            services.AddTransient<IOrdersRepository, OrdersRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IOrdersService, OrdersService>();
            services.AddTransient<IProductService, ProductService>();
            return services;
        }
    }

}
