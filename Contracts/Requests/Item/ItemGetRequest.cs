﻿namespace BillioIntegrationTest.Contracts.Requests.Item;

public record ItemGetRequest
{
    public Guid? CustomerId { get; set; }
}
