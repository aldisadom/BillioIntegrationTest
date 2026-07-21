using BillerContracts;
using BillerContracts.Requests.Invoice;
using BillerContracts.Responses;
using BillerContracts.Responses.Invoice;

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