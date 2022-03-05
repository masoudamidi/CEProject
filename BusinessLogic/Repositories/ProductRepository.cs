using System.Net.Http.Headers;
using System.Text;
using DataAccess.Models;
using System.Text.Json;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace BusinessLogic.Repositories
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
        public async Task<string> updateStock(List<offerStockApiRequestModel> products)
        {
            //Check argument validation

            //Checking that every Merchant Product Numbers is valid
            if (products.Any(t => t.MerchantProductNo == ""))
            {
                return "Merchant Product No cannot be null";
            }
            //Checking All Stock quantities are valid
            if (products.Any(t => t.Stock < 0))
            {
                return "Stock quantity is not valid.";
            }
            //Checking All stock location Ids are valid
            if (products.Any(t => t.StockLocationId == 0))
            {
                return "Stock Location Id is not valid.";
            }

            //Reading the api key from appsetting.json
            var apikey = _config["apikey"];
            string _r = "";
            offerStockApiResultModel result = null;

            //Combining the Api path url and the apikey for authorization
            string path = $"https://api-dev.channelengine.net/api/v2/offer/stock?apikey={apikey}";

            //Using PUT version of the request because it's going to update a record
            HttpResponseMessage response = await client.PutAsJsonAsync(path, products);

            //Checking If the response is in a good shape with no exception
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                //Reading and binding the response of api to the model
                result = await response.Content.ReadAsAsync<offerStockApiResultModel>();

                //Checking if there is and error returned from the API
                //If there is no error then combining the products that sent to api for stock update.
                //If there is errors then combining the errors and sending them for showing in the view
                if (result.Content?.Count == 0)
                {
                    _r = result.Message;
                    _r += " | Updated Products: ";
                    foreach (var item in products)
                    {
                        _r += item.MerchantProductNo + $" Stock:({item.Stock}) - ";
                    }
                }
                else
                {
                    foreach (var item in result.Content)
                    {
                        _r = item.Key + ": ";
                        foreach (var item2 in item.Value)
                        {
                            _r += item2 + " ";
                        }
                        _r = " |";
                    }
                }
            }
            return _r;
        }
    }
}
