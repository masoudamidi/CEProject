using System.Collections.Generic;
using BusinessLogic.Repositories;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace CEProject.Test;

public class productsTest
{
    //Checking If ProductMerchantNo has incorrect value
    [Fact]
    public void updateStock_Invalid_ProductMerchantNo_Value_Test()
    {
        //Arrange

        //Creating a dictionary for mocking the configuration that needed in product repository
        var appSettingsStub = new Dictionary<string, string> {
            {"apikey", "541b989ef78ccb1bad630ea5b85c6ebff9ca3322"},
            {"apipath", "https://api-dev.channelengine.net/api/v2/"}
        };
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(appSettingsStub)
            .Build();

        IProductRepository products = new ProductRepository(configuration);

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
        var appSettingsStub = new Dictionary<string, string> {
            {"apikey", "541b989ef78ccb1bad630ea5b85c6ebff9ca3322"},
            {"apipath", "https://api-dev.channelengine.net/api/v2/"}
        };
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(appSettingsStub)
            .Build();

        IProductRepository products = new ProductRepository(configuration);

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
        var appSettingsStub = new Dictionary<string, string> {
            {"apikey", "541b989ef78ccb1bad630ea5b85c6ebff9ca3322"},
            {"apipath", "https://api-dev.channelengine.net/api/v2/"}
        };
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(appSettingsStub)
            .Build();

        IProductRepository products = new ProductRepository(configuration);

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
        var appSettingsStub = new Dictionary<string, string> {
            {"apikey", "541b989ef78ccb1bad630ea5b85c6ebff9ca3322"},
            {"apipath", "https://api-dev.channelengine.net/api/v2/"}
        };
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(appSettingsStub)
            .Build();

        IProductRepository products = new ProductRepository(configuration);

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