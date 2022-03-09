public class OfferStock_ApiResultDTO
{
    public OfferStock_ApiContentDTO Content { get; set; }
    public int StatusCode { get; set; }
    public int? LogId { get; set; }
    public bool Success { get; set; }
    public string? Message { get; set; }
    public ValidationErrorsDTO? ValidationErrors { get; set; }
}
