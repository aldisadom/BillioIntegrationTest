namespace IntegrationTests.Models;

public record ItemModel
{
    public Guid Id { get; set; }
    public string CustomerEmail { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }
}
