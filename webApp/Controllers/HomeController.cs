using Microsoft.AspNetCore.Mvc;

namespace webApp.Controllers;

public class HomeController : Controller
{
    //Creating Objects from our Repositories for Dependency Injection use
    private readonly IOrdersService ordersService;
    private readonly IProductService productService;

    //Injecting the repositories
    public HomeController(IOrdersService _ordersService,
                          IProductService _productService)
    {
        ordersService = _ordersService;
        productService = _productService;
    }

    public async Task<IActionResult> Index()
    {
        //Using the method inside Orders Interface for retrieving the Orders in "IN_PROGRESS" status
        //The status can come from the end user but for the purpose of this assessment it just declared here.
        var _Orders = await ordersService.getOrdersByStatus(orderStatus.IN_PROGRESS);

        //Calculating Top 5 Products based on recieved orders
        var _topFiveProducts = await ordersService.getTopFiveProduct();

        //Creating Model equals to the model accepted by Update Stock API
        //Getting First product of first order that recieved from orders that are IN_PROGRESS
        //Setting Stock quantity equal to 25. This is for this Test Purpose. We Can Get this field from end user as a form too.
        var _product = new OfferStock_ApiRequestDTO()
        {
            MerchantProductNo = _Orders[0].Lines[0].MerchantProductNo,
            StockLocations = new List<OfferStock_StockLocationsDTO>() {
                new OfferStock_StockLocationsDTO() {
                    Stock = 30,
                    StockLocationId = _Orders[0].Lines[0].StockLocation.Id
                }
            }
        };

        //Adding Product model to a list. As API requires it.
        var _products = new List<OfferStock_ApiRequestDTO>();
        _products.Add(_product);

        //Calling The Method that Updates the stock of the product
        var updateStockResult = await productService.updateStock(_products);

        //Combine the two models to one model for showing in the VIEW.
        var model = new IndexViewDTO()
        {
            products = _topFiveProducts,
            UpdateStockResult = updateStockResult
        };
        return View(model);
    }
}
