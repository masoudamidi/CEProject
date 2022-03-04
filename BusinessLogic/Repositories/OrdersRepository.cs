using DataAccess.Models;

namespace BusinessLogic.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        static HttpClient client = new HttpClient();
        public async Task<orderApiResultModel> getOrdersByStatus(string Status)
        {
            orderApiResultModel result = null;
            string path = "https://api-dev.channelengine.net/api/v2/orders/?apikey=541b989ef78ccb1bad630ea5b85c6ebff9ca3322" + "&status=" + Status;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<orderApiResultModel>();
            }
            return result;
        }

        public List<Product> getTopFiveProduct(List<Order> orders)
        {
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
