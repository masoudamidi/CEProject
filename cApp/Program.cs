using System;
using System.Threading.Tasks;
using BusinessLogic.Repositories;
using ConsoleTables;
using DataAccess.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace cApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();
            var builder = new ConfigurationBuilder()
               .AddJsonFile($"appsettings.json", true, true);

            var config = builder.Build();

            List<Product> A = getTopFiveProducts(host.Services);


            var rows = Enumerable.Repeat(A, A.Count());

            ConsoleTable
                .From<Product>(A.AsEnumerable())
                .Configure(o => o.NumberAlignment = Alignment.Right)
                .Write(Format.Alternative);

            Console.WriteLine(updateQuantity(host.Services, 25));            
        }


        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddTransient<IOrdersRepository, OrdersRepository>()
                            .AddTransient<IProductRepository, ProductRepository>());
        }

        public static List<Product> getTopFiveProducts(IServiceProvider services)
        {
            using var serviceScope = services.CreateScope();
            var provider = serviceScope.ServiceProvider;

            var ordersservice = provider.GetRequiredService<IOrdersRepository>();
            var _Orders = ordersservice.getOrdersByStatus("IN_PROGRESS").Result;
            return ordersservice.getTopFiveProduct(_Orders.Content);
        }

        public static string updateQuantity(IServiceProvider services, int Quantity)
        {
            using var serviceScope = services.CreateScope();
            var provider = serviceScope.ServiceProvider;

            var productservice = provider.GetRequiredService<IProductRepository>();
            var orderservice = provider.GetRequiredService<IOrdersRepository>();

            var ordersInProgress = orderservice.getOrdersByStatus("IN_PROGRESS").Result;

            var _product = new offerStockApiRequestModel() {
                MerchantProductNo = ordersInProgress.Content[0].Lines[0].MerchantProductNo,
                Stock = Quantity,
                StockLocationId = ordersInProgress.Content[0].Lines[0].StockLocation.Id
            };

            var _products = new List<offerStockApiRequestModel>();
            _products.Add(_product);

            return productservice.updateStock(_products).Result;
        }
    }
}





