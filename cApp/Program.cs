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
            //Creating host for using DI in this console app
            using var host = CreateHostBuilder(args).Build();

            //Adding configuration builder for accessing the data in appsettings.json
            var builder = new ConfigurationBuilder()
               .AddJsonFile($"appsettings.json", true, true);

            var config = builder.Build();

            //Getting the top 5 products from the API
            List<Product> products = getTopFiveProducts(host.Services);

            //Preparing the data for ConsoleTable package for showing the data in Readable Table in the Console Output
            var rows = Enumerable.Repeat(products, products.Count());

            //Printing the Data in Table version in Output from ConsoleTable package
            ConsoleTable
                .From<Product>(products.AsEnumerable())
                .Configure(o => o.NumberAlignment = Alignment.Right)
                .Write(Format.Alternative);

            //Updating The Stock of the first product retrieved from api to 25. Using 25 is for this Assessment
            //the quantity can come from the user.
            Console.WriteLine(updateQuantity(host.Services));            
        }

        //With this approach Using DI in Console Applications are reachable
        //Registering the two interfaces and implementations to the services for the use            
        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddTransient<IOrdersRepository, OrdersRepository>()
                            .AddTransient<IProductRepository, ProductRepository>());
        }

        //Getting the orders that are in "IN_PROGRESS" status and calculating 
        //top five products that sold in those orders
        public static List<Product> getTopFiveProducts(IServiceProvider services)
        {
            //Creating a new service scope
            using var serviceScope = services.CreateScope();
            var provider = serviceScope.ServiceProvider;

            //Using interface that provided for getting the orders
            var ordersservice = provider.GetRequiredService<IOrdersRepository>();

            //Using the method inside Orders Interface for retrieving the Orders in "IN_PROGRESS" status
            //The status can come from the end user but for the purpose of this assessment it just declared here.
            var _Orders = ordersservice.getOrdersByStatus(orderStatus.IN_PROGRESS).Result;

            //Returning the Products in the correct model.
            return ordersservice.getTopFiveProduct(_Orders.Content);
        }

        //Updating stock of a product using the provided API with given Quantity 
        public static string updateQuantity(IServiceProvider services)
        {
            //Creating a new service scope
            using var serviceScope = services.CreateScope();
            var provider = serviceScope.ServiceProvider;

            //Using interface that provided for getting the orders and products
            var productservice = provider.GetRequiredService<IProductRepository>();
            var orderservice = provider.GetRequiredService<IOrdersRepository>();

            //Using the method inside Orders Interface for retrieving the Orders in "IN_PROGRESS" status
            var ordersInProgress = orderservice.getOrdersByStatus(orderStatus.IN_PROGRESS).Result;

            //Providing the model needed for product repository in order to updating the stock
            var _product = new offerStockApiRequestModel() {
                MerchantProductNo = ordersInProgress.Content[0].Lines[0].MerchantProductNo,
                Stock = 25,
                StockLocationId = ordersInProgress.Content[0].Lines[0].StockLocation.Id
            };

            //Adding the model to the list. Getting data ready for the api to use
            var _products = new List<offerStockApiRequestModel>();
            _products.Add(_product);

            //Returning the result from the api
            return productservice.updateStock(_products).Result;
        }
    }
}





