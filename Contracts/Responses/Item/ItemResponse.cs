﻿namespace BillioIntegrationTest.Contracts.Responses.Item;

public record ItemResponse
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }
}
