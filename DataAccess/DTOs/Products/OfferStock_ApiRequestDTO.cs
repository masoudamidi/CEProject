using System.ComponentModel.DataAnnotations;
using DataAccess.DTOs;

public class OfferStock_ApiRequestDTO
{
    [Required]
    public string? MerchantProductNo { get; set; }

    [Required]
    public List<OfferStock_StockLocationsDTO> StockLocations { get; set; }
}