using BusinessLogic.Repositories;
using BusinessLogic.Interfaces;
using DataAccess.Models;

public interface IProductRepository
{
    public Task<string> updateStock(List<offerStockApiRequestModel> products);
}