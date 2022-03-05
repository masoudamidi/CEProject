using System.ComponentModel.DataAnnotations;

public class offerStockApiRequestModel
{
    [Required]
    public string? MerchantProductNo { get; set; }

    [Required]
    public int Stock { get; set; }

    [Required]
    public int StockLocationId { get; set; }
}