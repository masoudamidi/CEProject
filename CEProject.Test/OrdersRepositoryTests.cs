using System.Collections.Generic;
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
    [Fact]
    public async Task getTopFiveProduct_invalid_Api_Value_Test()
    {
        //Arrange
        var appSettingsStub = new Dictionary<string, string> {
            {"apikey", "testkeyvalue"},
            {"apipath", "https://api-dev.channelengine.net/api/v2/"}
        };
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(appSettingsStub)
            .Build();

        IOrdersRepository orders = new OrdersRepository(configuration);

        //Act
        var result = await orders.getOrdersByStatus(orderStatus.IN_PROGRESS);

        //Assert
        Assert.Equal("401", result.StatusCode.ToString());
    }

    [Fact]
    public void getTopFiveProduct_Test()
    {
        //Arrange
        var appSettingsStub = new Dictionary<string, string> {
            {"apikey", "541b989ef78ccb1bad630ea5b85c6ebff9ca3322"},
            {"apipath", "https://api-dev.channelengine.net/api/v2/"}
        };
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(appSettingsStub)
            .Build();
            
        IOrdersRepository orders = new OrdersRepository(configuration);

        var DummyData = OrdersFixture.GetDummyData();

        //Act
        var result = orders.getTopFiveProduct(DummyData);

        //Assert
        Assert.True(result.Count <= 5);
    }
}