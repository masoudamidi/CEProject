using DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class DI
{
    //With this approach Using DI in Console Applications are reachable
        //Registering the two interfaces and implementations to the services for the use            
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddTransient<IOrdersService, OrdersService>()
                            .AddTransient<IProductService, ProductService>()
                            .AddTransient<IOrdersRepository, OrdersRepository>()
                            .AddTransient<IProductRepository, ProductRepository>());
        }
}