namespace IntegrationTests.Models;

public class CustomerModel
{
    public Guid Id { get; set; }
    public string SellerEmail { get; set; } = string.Empty;
    public string SeriesName { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyNumber { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}
