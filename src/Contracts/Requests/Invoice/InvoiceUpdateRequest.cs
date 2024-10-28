﻿using Contracts.Requests.Customer;
using Contracts.Requests.Seller;

namespace Contracts.Requests.Invoice;

public class InvoiceUpdateRequest
{
    public Guid Id { get; set; }
    public int InvoiceNumber { get; set; }
    public required SellerUpdateRequest Seller { get; set; }
    public required CustomerUpdateRequest Customer { get; set; }
    public List<InvoiceItemUpdateRequest> Items { get; set; } = [];
    public string? Comments { get; set; }
    public DateOnly DueDate { get; set; }
    public DateOnly CreatedDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
}
