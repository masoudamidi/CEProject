public class OrdersService : IOrdersService
{
    private readonly IOrdersRepository _orders;

    public OrdersService(IOrdersRepository orders)
    {
        _orders = orders;
    }

    public async Task<List<DataAccess.DTOs.OrderDTO>> getOrdersByStatus(orderStatus Status)
    {
        return await _orders.getOrdersByStatus(orderStatus.IN_PROGRESS);
    }

    //Calculating the top five products implementation.
    public async Task<List<ProductDTO>> getTopFiveProduct()
    {
        var od = await _orders.getOrdersByStatus(orderStatus.IN_PROGRESS);
        //Getting all order lines and Grouping by the name and gtin and summing the Quantity they have in orders
        //Then Ordering them by the summation and Taking the Top of all products.
        var topFiveProductList = od.SelectMany(t => t.Lines).GroupBy(x => new { x.Description, x.Gtin })
                    .Select(y => new ProductDTO
                    {
                        productName = y.Key.Description,
                        Gtin = y.Key.Gtin,
                        Quantity = y.Sum(x => x.Quantity)
                    })
                    .OrderByDescending(x => x.Quantity)
                    .Take(5)
                    .ToList();
        return topFiveProductList;
    }
}