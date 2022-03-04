namespace DataAccess.Models;

public class orderApiResultModel
{
    public int Count { get; set; }
    public int TotalCount { get; set; }
    public int ItemsPerPage { get; set; }
    public int StatusCode { get; set; }
    public int? LogId { get; set; }
    public bool Success { get; set; }
    public string? Message { get; set; }
    public ValidationErrors? ValidationErrors { get; set; }
    public List<Order>? Content { get; set; }

}