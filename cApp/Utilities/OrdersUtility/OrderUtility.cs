
using Microsoft.Extensions.DependencyInjection;

public class OrderUtility
{
       //Getting the orders that are in "IN_PROGRESS" status and calculating 
        //top five products that sold in those orders
        public static IndexViewModel getTopFiveProducts(IServiceProvider services)
        {
            var model = new IndexViewModel();

            //Creating a new service scope
            using var serviceScope = services.CreateScope();
            var provider = serviceScope.ServiceProvider;

            //Using interface that provided for getting the orders
            var ordersservice = provider.GetRequiredService<IOrdersRepository>();

            //Using the method inside Orders Interface for retrieving the Orders in "IN_PROGRESS" status
            //The status can come from the end user but for the purpose of this assessment it just declared here.
            var _Orders = ordersservice.getOrdersByStatus(orderStatus.IN_PROGRESS).Result;

            if (_Orders.StatusCode == 200)
            {
                //Returning the Products in the correct model.
                model.products = ordersservice.getTopFiveProduct(_Orders.Content);
            }
            else
            {
                model.Message = _Orders.StatusCode.ToString() + ' ' + _Orders.Message;
            }

            //Returning the Products in the correct model.
            return model;
        }
}