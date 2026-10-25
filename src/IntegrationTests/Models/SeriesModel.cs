namespace IntegrationTests.Models;

public class SeriesModel
{
    public Guid Id { get; set; }
    public string SellerEmail { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int? NextNumber { get; set; }
}
