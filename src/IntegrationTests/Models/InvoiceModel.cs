namespace BillioIntegrationTest.Models;

public record InvoiceModel
{
    public string SellerEmail { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
    public List<InvoiceItemModel> Items { get; set; } = [];
    public string Comments { get; set; } = string.Empty;
    public DateOnly DueDate { get; set; }
    public DateOnly CreatedDate { get; set; }
}

public record InvoiceItemModel
{
    public string Name = string.Empty;
    public decimal Quantity;
    public string Comments = string.Empty;
}
