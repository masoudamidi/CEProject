using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace CEProject.Test;

public class productsTest
{
    private readonly ProductService _sut;
    private readonly Mock<IProductRepository> _mockProduct = new Mock<IProductRepository>();

    public productsTest() 
    {
        _sut = new ProductService(_mockProduct.Object);
    }

    //Checking If ProductMerchantNo has incorrect value
    [Fact]
    public async Task updateStock_Invalid_ProductMerchantNo_Value_Test()
    {
        //Arrange
        //Providing Dummy data for the test
        var updateStockInput = new List<OfferStock_ApiRequestDTO>() {
            new OfferStock_ApiRequestDTO() {
                MerchantProductNo = "",
                StockLocations = new List<OfferStock_StockLocationsDTO>() {
                    new OfferStock_StockLocationsDTO() {
                        Stock = 25,
                        StockLocationId = 2
                    }
                }
            }
        };
        _mockProduct.Setup(x => x.updateStock(updateStockInput)).ReturnsAsync("Merchant Product No cannot be null");

        //Act
        var result = await _sut.updateStock(updateStockInput);

        //Assert
        Assert.Equal("Merchant Product No cannot be null", result);
    }

    //Checking if Stock has invalid value
    [Fact]
    public void updateStock_Invalid_Stock_Value_Test()
    {
        //Arrange
        //Providing Dummy data for the test
        var updateStockInput = new List<OfferStock_ApiRequestDTO>() {
            new OfferStock_ApiRequestDTO() {
                MerchantProductNo = "aa",
                StockLocations = new List<OfferStock_StockLocationsDTO>() {
                    new OfferStock_StockLocationsDTO() {
                        Stock = -1,
                        StockLocationId = 2
                    }
                }
            }
        };
        _mockProduct.Setup(x => x.updateStock(updateStockInput)).ReturnsAsync("Stock quantity is not valid");

        //Act
        var result = _sut.updateStock(updateStockInput).Result;

        //Assert
        Assert.Equal("Stock quantity is not valid", result);
    }

    //Checking if Stock Location Id has invalid value
    [Fact]
    public void updateStock_Invalid_StockLocationId_Value_Test()
    {
        //Arrange
        var updateStockInput = new List<OfferStock_ApiRequestDTO>() {
            new OfferStock_ApiRequestDTO() {
                MerchantProductNo = "aa",
                StockLocations = new List<OfferStock_StockLocationsDTO>() {
                    new OfferStock_StockLocationsDTO() {
                        Stock = 25,
                        StockLocationId = 0
                    }
                }
            }
        };
        _mockProduct.Setup(x => x.updateStock(updateStockInput)).ReturnsAsync("Stock Location Id is not valid");

        //Act
        var result = _sut.updateStock(updateStockInput).Result;

        //Assert
        Assert.Equal("Stock Location Id is not valid", result);
    }

    //Testing the Update Stock value if it works successfully
    [Fact]
    public void updateStock_Success_Test()
    {
        //Arrange
        var updateStockInput = new List<OfferStock_ApiRequestDTO>() {
            new OfferStock_ApiRequestDTO() {
                MerchantProductNo = "001201-S",
                StockLocations = new List<OfferStock_StockLocationsDTO>() {
                    new OfferStock_StockLocationsDTO() {
                        Stock = 25,
                        StockLocationId = 2
                    }
                }
            }
        };
        _mockProduct.Setup(x => x.updateStock(updateStockInput)).ReturnsAsync("Updates processed without warnings");

        //Act
        var result = _sut.updateStock(updateStockInput).Result;

        //Assert
        Assert.True(result.Contains("Updates processed without warnings"));
    }
}