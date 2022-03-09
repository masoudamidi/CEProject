using Microsoft.Extensions.DependencyInjection;

public class OrderUtility
{
    //Getting the orders that are in "IN_PROGRESS" status and calculating 
    //top five products that sold in those orders
    public static async Task<IndexViewDTO> getTopFiveProducts(IServiceProvider services)
    {
        var model = new IndexViewDTO();

        //Creating a new service scope
        using var serviceScope = services.CreateScope();
        var provider = serviceScope.ServiceProvider;

        //Using interface that provided for getting the orders
        var ordersservice = provider.GetRequiredService<IOrdersService>();

        // //Using the method inside Orders Interface for retrieving the Orders in "IN_PROGRESS" status
        // //The status can come from the end user but for the purpose of this assessment it just declared here.
        // var _Orders = await ordersservice.getOrdersByStatus(orderStatus.IN_PROGRESS);

        //Returning the Products in the correct model.
        model.products = await ordersservice.getTopFiveProduct();

        //Returning the Products in the correct model.
        return model;
    }
}