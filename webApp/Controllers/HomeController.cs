using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Interfaces;

namespace webApp.Controllers;

public class HomeController : Controller
{
    private readonly IOrdersRepository ordersRepository;
    private readonly IProductRepository productRepository;

    public HomeController(IOrdersRepository _ordersRepository,
                          IProductRepository _productRepository)
    {
        ordersRepository = _ordersRepository;
        productRepository = _productRepository;
    }

    public IActionResult Index()
    {

        var _Orders = ordersRepository.getOrdersByStatus("IN_PROGRESS").Result;
        var _topFiveProducts = ordersRepository.getTopFiveProduct(_Orders.Content);

        var _product = new offerStockApiRequestModel()
        {
            MerchantProductNo = _Orders.Content[0].Lines[0].MerchantProductNo,
            Stock = 25,
            StockLocationId = _Orders.Content[0].Lines[0].StockLocation.Id
        };

        var _products = new List<offerStockApiRequestModel>();
        _products.Add(_product);
        var updateStockResult = productRepository.updateStock(_products).Result;

        var model = new IndexViewModel() {
            products = _topFiveProducts,
            UpdateStockResult = updateStockResult
        };
        return View(model);
    }
}
