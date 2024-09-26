using BillioIntegrationTest.Contracts.Requests.Invoice;
using BillioIntegrationTest.Contracts.Responses;
using BillioIntegrationTest.Contracts.Responses.Invoice;

namespace BillioIntegrationTest.Interfaces
{
    public interface IInvoiceClient
    {
        Task<AddResponse> Add(InvoiceAddRequest item);
        Task Delete(Guid id);
        Task<InvoiceListResponse> Get();
        Task<InvoiceResponse?> Get(Guid id);
        Task Update(InvoiceUpdateRequest item);
    }
}