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
        private readonly IConfiguration _config;
        public ProductRepository(IConfiguration config)
        {
            _config = config;
        }
        static HttpClient client = new HttpClient();
        public async Task<string> updateStock(List<offerStockApiRequestModel> products)
        {
            var apikey = _config["apikey"];
            string _r = "";
            offerStockApiResultModel result = null;
            string path = $"https://api-dev.channelengine.net/api/v2/offer/stock?apikey={apikey}";
            HttpResponseMessage response = await client.PutAsJsonAsync(path, products);

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<offerStockApiResultModel>();
                
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
