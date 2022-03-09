namespace DataAccess.DTOs;

public class OrderLinesDTO
{
    public string? Status { get; set; }    
    public bool IsFulfillmentByMarketplace { get; set; }    
    public string? Gtin { get; set; }    
    public string? Description { get; set; }    
    public double UnitVat { get; set; }    
    public double LineTotalInclVat { get; set; }    
    public double LineVat { get; set; }    
    public double OriginalUnitPriceInclVat { get; set; }    
    public double OriginalUnitVat { get; set; }    
    public double OriginalLineTotalInclVat { get; set; }    
    public double OriginalLineVat { get; set; }    
    public double OriginalFeeFixed { get; set; }    
    public string? BundleProductMerchantProductNo { get; set; }    
    public string? JurisCode { get; set; }    
    public string? JurisName { get; set; }    
    public double VatRate { get; set; }    
    public string? ChannelProductNo { get; set; }    
    public string? MerchantProductNo { get; set; }    
    public double Quantity { get; set; }    
    public double CancellationRequestedQuantity { get; set; }    
    public double UnitPriceInclVat { get; set; }    
    public double FeeFixed { get; set; }    
    public double FeeRate { get; set; }    
    public string? Condition { get; set; }    
    public DateTime ExpectedDeliveryDate { get; set; }    
    public StockLocation? StockLocation { get; set; }    
    public List<ExtraData>? ExtraData { get; set; }    
}