using System.Net.Http.Headers;
using System.Text;
using DataAccess.Models;
using System.Text.Json;
using Newtonsoft.Json;

namespace BusinessLogic.Repositories
{
    public class ProductRepository : IProductRepository
    {
        static HttpClient client = new HttpClient();
        public async Task<string> updateStock(List<offerStockApiRequestModel> products)
        {
            string _r = "";
            offerStockApiResultModel result = null;
            string path = "https://api-dev.channelengine.net/api/v2/offer/stock?apikey=541b989ef78ccb1bad630ea5b85c6ebff9ca3322";
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
