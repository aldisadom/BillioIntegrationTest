﻿using BillioIntegrationTest.Contracts.Responses.Customer;
using BillioIntegrationTest.Contracts.Responses.Seller;

namespace BillioIntegrationTest.Contracts.Responses.Invoice;

public class InvoiceResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int InvoiceNumber { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime DueDate { get; set; }
    public SellerResponse? Seller { get; set; }
    public CustomerResponse? Customer { get; set; }
    public List<InvoiceItemResponse>? Items { get; set; }
    public string? Comments { get; set; }
    public decimal TotalPrice { get; set; }
}
