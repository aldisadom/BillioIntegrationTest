﻿namespace BillioIntegrationTest.Contracts.Requests.Seller;

public record SellerGetRequest
{
    public Guid? UserId { get; set; }
}
