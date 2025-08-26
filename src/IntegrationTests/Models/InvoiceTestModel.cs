namespace IntegrationTests.Models;

public record InvoiceTestModel
{
    public string BucketKey { get; set; } = string.Empty;
    public string SellerEmail { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
    public List<InvoiceItemTestModel> Items { get; set; } = [];
    public string Comments { get; set; } = string.Empty;
    public DateOnly DueDate { get; set; }
    public DateOnly CreatedDate { get; set; }
}

public record InvoiceItemTestModel
{
    public string Name = string.Empty;
    public decimal Quantity;
    public string Comments = string.Empty;
}

public record InvoiceDetaisUpdateTestModel
{
    public string BucketKey { get; set; } = string.Empty;
    public string? Comments { get; set; }
    public DateOnly DueDate { get; set; }
    public DateOnly CreatedDate { get; set; }
    public int InvoiceNumber { get; set; }
}

