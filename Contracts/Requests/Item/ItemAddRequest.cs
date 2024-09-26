﻿using System.ComponentModel.DataAnnotations;

namespace BillioIntegrationTest.Contracts.Requests.Item;

public record ItemAddRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public decimal Price { get; set; }
    [Required]
    public Guid CustomerId { get; set; }
    [Required]
    public decimal Quantity { get; set; }
}
