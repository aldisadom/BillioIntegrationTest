﻿using Common;
using Contracts.Requests.Invoice;
using Contracts.Responses;
using Contracts.Responses.Invoice;

namespace IntegrationTests.Interfaces;

public interface IInvoiceClient
{
    Task<Result<AddResponse>> Add(InvoiceAddRequest invoice);
    Task<Result<bool>> Delete(Guid id);
    Task<Result<InvoiceListResponse>> Get();
    Task<Result<InvoiceResponse?>> Get(Guid id);
    Task<Result<InvoiceListResponse>> Get(InvoiceGetRequest request);
    Task<Result<bool>> Update(InvoiceUpdateRequest invoice);
}