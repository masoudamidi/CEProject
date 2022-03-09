using Microsoft.Extensions.DependencyInjection;

namespace cApp
{
    public class productsUtilities
    {
        //Updating stock of a product using the provided API with given Quantity 
        public static async Task<string> updateQuantity(IServiceProvider services)
        {
            //Creating a new service scope
            using var serviceScope = services.CreateScope();
            var provider = serviceScope.ServiceProvider;

            //Using interface that provided for getting the orders and products
            var productservice = provider.GetRequiredService<IProductService>();
            var orderservice = provider.GetRequiredService<IOrdersService>();

            //Using the method inside Orders Interface for retrieving the Orders in "IN_PROGRESS" status
            var ordersInProgress = await orderservice.getOrdersByStatus(orderStatus.IN_PROGRESS);

            //Providing the model needed for product repository in order to updating the stock
            var _product = new OfferStock_ApiRequestDTO()
            {
                MerchantProductNo = ordersInProgress[0].Lines[0].MerchantProductNo,
                StockLocations = new List<OfferStock_StockLocationsDTO>() {
                    new OfferStock_StockLocationsDTO() {
                        Stock = 25,
                        StockLocationId = ordersInProgress[0].Lines[0].StockLocation.Id
                    }
                }
            };

            //Adding the model to the list. Getting data ready for the api to use
            var _products = new List<OfferStock_ApiRequestDTO>();
            _products.Add(_product);

            //Returning the result from the api
            return await productservice.updateStock(_products);

        }
    }
}