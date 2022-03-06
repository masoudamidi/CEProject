using BusinessLogic.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace cApp
{
    public static class productsUtilities
    {
        //With this approach Using DI in Console Applications are reachable
        //Registering the two interfaces and implementations to the services for the use            
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddTransient<IOrdersRepository, OrdersRepository>()
                            .AddTransient<IProductRepository, ProductRepository>());
        }

        //Getting the orders that are in "IN_PROGRESS" status and calculating 
        //top five products that sold in those orders
        public static IndexViewModel getTopFiveProducts(IServiceProvider services)
        {
            var model = new IndexViewModel();

            //Creating a new service scope
            using var serviceScope = services.CreateScope();
            var provider = serviceScope.ServiceProvider;

            //Using interface that provided for getting the orders
            var ordersservice = provider.GetRequiredService<IOrdersRepository>();

            //Using the method inside Orders Interface for retrieving the Orders in "IN_PROGRESS" status
            //The status can come from the end user but for the purpose of this assessment it just declared here.
            var _Orders = ordersservice.getOrdersByStatus(orderStatus.IN_PROGRESS).Result;

            if (_Orders.StatusCode == 200)
            {
                //Returning the Products in the correct model.
                model.products = ordersservice.getTopFiveProduct(_Orders.Content);
            }
            else
            {
                model.Message = _Orders.StatusCode.ToString() + ' ' + _Orders.Message;
            }

            //Returning the Products in the correct model.
            return model;
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

            if (ordersInProgress.StatusCode == 200)
            {
                //Checking if there is any orders in api result
                if (ordersInProgress.Content != null)
                {
                    //Providing the model needed for product repository in order to updating the stock
                    var _product = new offerStockApiRequestModel()
                    {
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
                else
                {
                    return "There is no product to update";
                }
            }
            else 
            {
                return ordersInProgress.StatusCode.ToString() + ' ' + ordersInProgress.Message;
            }
        }
    }
}