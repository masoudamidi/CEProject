using DataAccess.DTOs;

public interface IOrdersService
{
    //Requesting the orders with given Status from The Provided API and Store them in a List.
    Task<List<DataAccess.DTOs.OrderDTO>> getOrdersByStatus(orderStatus Status);

    //Calculate the top 5 five products that are the most sales.
    Task<List<ProductDTO>> getTopFiveProduct();
}