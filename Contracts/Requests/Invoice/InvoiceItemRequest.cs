using System.ComponentModel.DataAnnotations;

namespace BillioIntegrationTest.Contracts.Requests.Invoice;

public class InvoiceItemRequest
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public decimal Quantity { get; set; }
    public string Comments { get; set; } = string.Empty;
}
