using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        //Injecting the Configuration for accessing the appsettings.json
        private readonly IConfiguration _config;
        public ProductRepository(IConfiguration config)
        {
            _config = config;
        }
        static HttpClient client = new HttpClient();

        //Updating the Stock of product using provided Api.
        public async Task<string> updateStock(List<OfferStock_ApiRequestDTO> products)
        {
            //Reading the api key from appsetting.json
            var apikey = _config["apikey"];
            var apipath = _config["apipath"];
            string _result = "";
            OfferStock_ApiResultDTO result = null;

            //Combining the Api path url and the apikey for authorization
            string path = $"{apipath}offer/stock?apikey={apikey}";

            //Using PUT version of the request because it's going to update a record
            HttpResponseMessage response = await client.PutAsJsonAsync(path, products);

            //Reading and binding the response of api to the model
            //var d = await response.Content.ReadAsStringAsync();
            result = await response.Content.ReadAsAsync<OfferStock_ApiResultDTO>();

            if (response.IsSuccessStatusCode)
            {
                //Checking if there is and error returned from the API
                //If there is no error then combining the products that sent to api for stock update.
                //If there is errors then combining the errors and sending them for showing in the view
                if (result.Content.Results.Products == null)
                {
                    _result = result.Message;
                    _result += " | Updated Products: ";
                    foreach (var item in products)
                    {
                        _result += item.MerchantProductNo;
                        foreach (var item2 in item.StockLocations)
                        {
                           _result += $" Stock:({item2.Stock}) - ";
                        }                         
                    }
                }
                else
                {
                    foreach (var item in result.Content.Results.Products)
                    {
                        _result += item.Key + ": ";
                        foreach (var item2 in item.Value)
                        {
                            _result += item2 + " ";
                        }
                        _result = " | ";
                    }
                }
            }
            return _result;
        }
    }
}
