using BusinessLogic.Repositories;
using BusinessLogic.Interfaces;
using DataAccess.Models;

public interface IProductRepository
{
    //Updating stock of a product with given model, Contains the quantity using the provided API
    public Task<string> updateStock(List<offerStockApiRequestModel> products);
}