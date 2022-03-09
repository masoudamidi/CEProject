public interface IProductRepository
{
    //Updating stock of a product with given model, Contains the quantity using the provided API
    public Task<string> updateStock(List<OfferStock_ApiRequestDTO> products);
}