using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Interfaces;

namespace webApp.Controllers;

public class HomeController : Controller
{
    //Creating Objects from our Repositories for Dependency Injection use
    private readonly IOrdersRepository ordersRepository;
    private readonly IProductRepository productRepository;

    //Injecting the repositories
    public HomeController(IOrdersRepository _ordersRepository,
                          IProductRepository _productRepository)
    {
        ordersRepository = _ordersRepository;
        productRepository = _productRepository;
    }

    public IActionResult Index()
    {
        //Calling method for getting Orders that are in "IN_PROGRESS" status
        var _Orders = ordersRepository.getOrdersByStatus("IN_PROGRESS").Result;

        //Calculating Top 5 Products based on recieved orders
        var _topFiveProducts = ordersRepository.getTopFiveProduct(_Orders.Content);

        //Creating Model equals to the model accepted by Update Stock API
        //Getting First product of first order that recieved from orders that are IN_PROGRESS
        //Setting Stock quantity equal to 25. This is for this Test Purpose. We Can Get this field from end user as a form too.
        var _product = new offerStockApiRequestModel()
        {
            MerchantProductNo = _Orders.Content[0].Lines[0].MerchantProductNo,
            Stock = 25,
            StockLocationId = _Orders.Content[0].Lines[0].StockLocation.Id
        };

        //Adding Product model to a list. As API requires it.
        var _products = new List<offerStockApiRequestModel>();
        _products.Add(_product);

        //Calling The Method that Updates the Quantity
        var updateStockResult = productRepository.updateStock(_products).Result;

        //Combine the two models to one model for showing in the VIEW.
        var model = new IndexViewModel() {
            products = _topFiveProducts,
            UpdateStockResult = updateStockResult
        };
        return View(model);
    }
}
