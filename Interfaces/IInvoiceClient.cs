using BillioIntegrationTest.Clients;
using BillioIntegrationTest.Contracts.Requests.Invoice;
using BillioIntegrationTest.Contracts.Responses;
using BillioIntegrationTest.Contracts.Responses.Invoice;

namespace BillioIntegrationTest.Interfaces
{
    public interface IInvoiceClient
    {
        Task<Result<AddResponse>> Add(InvoiceAddRequest item);
        Task<Result<bool>> Delete(Guid id);
        Task<Result<InvoiceListResponse>> Get();
        Task<Result<InvoiceResponse?>> Get(Guid id);
        Task<Result<bool>> Update(InvoiceUpdateRequest item);
    }
}