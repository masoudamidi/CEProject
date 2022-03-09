public class ProductService : IProductService
{
    private readonly IProductRepository _product;

    public ProductService(IProductRepository product)
    {
        _product = product;
    }
    public async Task<string> updateStock(List<OfferStock_ApiRequestDTO> products)
    {
        //Check argument validation

        //Checking that every Merchant Product Numbers is valid
        if (products.Any(t => t.MerchantProductNo == ""))
        {
            return "Merchant Product No cannot be null";
        }
        //Checking All Stock quantities are valid
        if (products.Any(t => t.StockLocations.Any(t1 => t1.Stock < 0)))
        {
            return "Stock quantity is not valid";
        }
        //Checking All stock location Ids are valid
        if (products.Any(t => t.StockLocations.Any(t1 => t1.StockLocationId == 0)))
        {
            return "Stock Location Id is not valid";
        }

        var result = await _product.updateStock(products);

        return result;
    }
}