using System.Collections.Generic;
using BusinessLogic.Repositories;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace CEProject.Test;

public class productsTest
{
    private readonly IConfiguration _config;

    public productsTest() 
    {
        _config = new ConfigurationBuilder()
        .AddJsonFile(@"appsettings.json", false, false)
        .AddEnvironmentVariables()
        .Build();
    }
    //Checking If ProductMerchantNo has incorrect value
    [Fact]
    public void updateStock_Invalid_ProductMerchantNo_Value_Test()
    {
        //Arrange
        IProductRepository products = new ProductRepository(_config);

        //Providing Dummy data for the test
        var updateStockInput = new List<offerStockApiRequestModel>() {
            new offerStockApiRequestModel() {
                MerchantProductNo = "",
                Stock = 25,
                StockLocationId = 2
            }
        };

        //Act
        var result = products.updateStock(updateStockInput).Result;

        //Assert
        Assert.Equal("Merchant Product No cannot be null", result);
    }

    //Checking if Stock has invalid value
    [Fact]
    public void updateStock_Invalid_Stock_Value_Test()
    {
        //Arrange
        IProductRepository products = new ProductRepository(_config);

        var updateStockInput = new List<offerStockApiRequestModel>() {
            new offerStockApiRequestModel() {
                MerchantProductNo = "aa",
                Stock = -1,
                StockLocationId = 2
            }
        };

        //Act
        var result = products.updateStock(updateStockInput).Result;

        //Assert
        Assert.Equal("Stock quantity is not valid", result);
    }

    //Checking if Stock Location Id has invalid value
    [Fact]
    public void updateStock_Invalid_StockLocationId_Value_Test()
    {
        //Arrange
        IProductRepository products = new ProductRepository(_config);

        var updateStockInput = new List<offerStockApiRequestModel>() {
            new offerStockApiRequestModel() {
                MerchantProductNo = "aa",
                Stock = 5,
                StockLocationId = 0
            }
        };

        //Act
        var result = products.updateStock(updateStockInput).Result;

        //Assert
        Assert.Equal("Stock Location Id is not valid", result);
    }

    //Testing the Update Stock value if it works successfully
    [Fact]
    public void updateStock_Success_Test()
    {
        //Arrange
        IProductRepository products = new ProductRepository(_config);

        var updateStockInput = new List<offerStockApiRequestModel>() {
            new offerStockApiRequestModel() {
                MerchantProductNo = "001201-S",
                Stock = 25,
                StockLocationId = 2
            }
        };

        //Act
        var result = products.updateStock(updateStockInput).Result;

        //Assert
        Assert.True(result.Contains("Updates processed without warnings"));
    }
}