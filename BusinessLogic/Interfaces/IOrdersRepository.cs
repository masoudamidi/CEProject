using BusinessLogic.Repositories;
using BusinessLogic.Interfaces;
using DataAccess.Models;

public interface IOrdersRepository
{
    public Task<orderApiResultModel> getOrdersByStatus(string Status);
    List<Product> getTopFiveProduct(List<Order> orders);
}