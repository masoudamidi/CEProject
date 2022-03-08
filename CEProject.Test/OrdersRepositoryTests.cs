using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BusinessLogic.Repositories;
using CEProject.Test.Fixtures;
using DataAccess.Models;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace CEProject.Test;

public class ordersTest
{
    private readonly IConfiguration _config;

    public ordersTest() 
    {
        _config = new ConfigurationBuilder()
        .AddJsonFile(@"appsettings.json", false, false)
        .AddEnvironmentVariables()
        .Build();
    }

    [Fact]
    public async Task getTopFiveProduct_invalid_Api_Value_Test()
    {
        //Arrange
        _config["apikey"] = "testapikey";
        IOrdersRepository orders = new OrdersRepository(_config);

        //Act
        var result = await orders.getOrdersByStatus(orderStatus.IN_PROGRESS);

        //Assert
        Assert.Equal("401", result.StatusCode.ToString());
    }

    [Fact]
    public void getTopFiveProduct_Test()
    {
        //Arrange           
        IOrdersRepository orders = new OrdersRepository(_config);

        var DummyData = OrdersFixture.GetDummyData();

        //Act
        var result = orders.getTopFiveProduct(DummyData);

        //Assert
        Assert.True(result.Count <= 5);
    }
}