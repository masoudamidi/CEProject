using DataAccess.Models;

public interface IOrdersRepository
{
    //Requesting the orders with given Status from The Provided API and Store them in a List.
    public Task<orderApiResultModel> getOrdersByStatus(orderStatus Status);

    //Calculate the top 5 five products that are the most sales.
    List<Product> getTopFiveProduct(List<Order> orders);
}