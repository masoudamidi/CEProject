using System.Collections.Generic;
using BusinessLogic.Repositories;
using DataAccess.Models;
using Moq;
using Xunit;

namespace CEProject.Test;

public class ordersTest
{
    [Fact]
    public void getTopFiveProduct_Test()
    {
        IOrdersRepository orders = new OrdersRepository();

        var DummyData = GetDummyData();
        var result = orders.getTopFiveProduct(DummyData);

        Assert.True(result.Count <= 5);
    }

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