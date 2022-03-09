using System.Threading.Tasks;
using CEProject.Test.Fixtures;
using Moq;
using Xunit;

namespace CEProject.Test;

public class ordersTest
{
    private readonly OrdersService _sut;
    private readonly Mock<IOrdersRepository> _orderMock = new Mock<IOrdersRepository>();

    public ordersTest() 
    {
        _sut = new OrdersService(_orderMock.Object);
    }

    [Fact]
    public async Task getTopFiveProduct_Test()
    {
        //Arrange           
        var DummyData = OrdersFixture.GetDummyData();
        _orderMock.Setup(x => x.getOrdersByStatus(orderStatus.IN_PROGRESS)).ReturnsAsync(DummyData);

        //Act
        var result = await _sut.getTopFiveProduct();

        //Assert
        Assert.True(result.Count <= 5);
    }
}