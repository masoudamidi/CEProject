using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Repositories;
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
        //GIVEN
        var appSettingsStub = new Dictionary<string, string> {
            {"apikey", "testkeyvalue"},
            {"apipath", "https://api-dev.channelengine.net/api/v2/"}
        };
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(appSettingsStub)
            .Build();

        IOrdersRepository orders = new OrdersRepository(configuration);

        //WHEN
        var result = await orders.getOrdersByStatus(orderStatus.IN_PROGRESS);

        //THEN
        Assert.Equal("401", result.StatusCode.ToString());
    }

    [Fact]
    public void getTopFiveProduct_Test()
    {
        //GIVEN
        var appSettingsStub = new Dictionary<string, string> {
            {"apikey", "541b989ef78ccb1bad630ea5b85c6ebff9ca3322"},
            {"apipath", "https://api-dev.channelengine.net/api/v2/"}
        };
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(appSettingsStub)
            .Build();
            
        IOrdersRepository orders = new OrdersRepository(configuration);

        var DummyData = GetDummyData();

        //WHEN
        var result = orders.getTopFiveProduct(DummyData);

        //THEN
        Assert.True(result.Count <= 5);
    }

    //Preparing Dummy data for the test
    //Because testing is about only products with their quantity we can only fill required data.
    public static List<Order> GetDummyData()
    {
        List<Order> myDummyData = new List<Order>();

        myDummyData.Add(new Order()
        {
            Lines = new List<Lines>() {
                    new Lines() {
                        Description = "T-Shirt ONE",
                        Gtin = "123456789",
                        Quantity = 1
                    }
            }
        });

        myDummyData.Add(new Order()
        {
            Lines = new List<Lines>() {
                    new Lines() {
                        Description = "T-Shirt ONE",
                        Gtin = "123456789",
                        Quantity = 1
                    },
                    new Lines() {
                        Description = "T-Shirt TWO",
                        Gtin = "987654321",
                        Quantity = 3
                    }
            }
        });

        myDummyData.Add(new Order()
        {
            Lines = new List<Lines>() {
                    new Lines() {
                        Description = "T-Shirt THREE",
                        Gtin = "111111111",
                        Quantity = 6
                    },
                    new Lines() {
                        Description = "T-Shirt FOUR",
                        Gtin = "222222222",
                        Quantity = 1
                    }
            }
        });

        myDummyData.Add(new Order()
        {
            Lines = new List<Lines>() {
                    new Lines() {
                        Description = "T-Shirt FIVE",
                        Gtin = "333333333",
                        Quantity = 2
                    },
                    new Lines() {
                        Description = "T-Shirt SIX",
                        Gtin = "444444444",
                        Quantity = 2
                    }
            }
        });

        return myDummyData;
    }
}