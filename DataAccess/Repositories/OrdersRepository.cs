using DataAccess.DTOs;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Repositories
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
        public async Task<List<OrderDTO>> getOrdersByStatus(orderStatus Status)
        {
            //Retrieving the Api Key from appsettings.json
            var apikey = _config["apikey"];
            var apipath = _config["apipath"];
            Order_ApiResultDTO result = null;

            //Combining the Path URL and data is needed for the api. apikey is needed for authorization
            //and status is order status type for filtering the data coming from Api.
            string path = $"{apipath}orders/?apikey={apikey}&status={Status}";

            //Creating a response for api request. using Async version is better for perfomance
            HttpResponseMessage response = await client.GetAsync(path);

            //Reading the api result and binding the result with model provided for the response
            result = await response.Content.ReadAsAsync<Order_ApiResultDTO>();
            return result.Content;
        }       
    }
}
