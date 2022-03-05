using DataAccess.Models;
using Microsoft.Extensions.Configuration;

namespace BusinessLogic.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        //Injecting the Configuration for accessing the Api key from appsettings.json
        private readonly IConfiguration _config;
        public OrdersRepository(IConfiguration config)
        {
            _config = config;
        }
        static HttpClient client = new HttpClient();

        //Getting the Orders with given status from the API and storing them in the model
        public async Task<orderApiResultModel> getOrdersByStatus(string Status)
        {
            //Retrieving the Api Key from appsettings.json
            var apikey = _config["apikey"];
            orderApiResultModel result = null;

            //Combining the Path URL and data is needed for the api. apikey is needed for authorization
            //and status is order status type for filtering the data coming from Api.
            string path = $"https://api-dev.channelengine.net/api/v2/orders/?apikey={apikey}&status={Status}";

            //Creating a response for api request. using Async version is better for perfomance
            HttpResponseMessage response = await client.GetAsync(path);

            //Checking if the request responded correctly with no exception
            if (response.IsSuccessStatusCode)
            {
                //Reading the api result and binding the result with model provided for the response
                result = await response.Content.ReadAsAsync<orderApiResultModel>();
            }
            return result;
        }

        //Calculating the top five products implementation.
        public List<Product> getTopFiveProduct(List<Order> orders)
        {
            //Getting all order lines and Grouping by the name and gtin and summing the Quantity they have in orders
            //Then Ordering them by the summation and Taking the Top of all products.
            var tmp = orders.SelectMany(t => t.Lines).GroupBy(x => new { x.Description, x.Gtin })
                        .Select(y => new Product
                        {
                            productName = y.Key.Description,
                            Gtin = y.Key.Gtin,
                            Quantity = y.Sum(x => x.Quantity)
                        })
                        .OrderByDescending(x => x.Quantity)
                        .Take(5)
                        .ToList();

            return tmp;
        }
    }
}
