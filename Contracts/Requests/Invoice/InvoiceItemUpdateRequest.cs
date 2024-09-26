﻿using System.ComponentModel.DataAnnotations;

namespace BillioIntegrationTest.Contracts.Requests.Invoice;

public class InvoiceItemUpdateRequest
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public decimal Quantity { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public decimal Price { get; set; }
    public string Comments { get; set; } = string.Empty;
}
