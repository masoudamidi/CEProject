using DataAccess.DTOs;

public interface IOrdersRepository
{
    //Requesting the orders with given Status from The Provided API and Store them in a List.
    Task<List<OrderDTO>> getOrdersByStatus(orderStatus Status);
}