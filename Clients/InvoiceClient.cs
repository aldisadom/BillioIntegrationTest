using BillioIntegrationTest.Contracts.Requests.Invoice;
using BillioIntegrationTest.Contracts.Responses;
using BillioIntegrationTest.Contracts.Responses.Invoice;
using BillioIntegrationTest.Interfaces;

namespace BillioIntegrationTest.Clients;

public class InvoiceClient : IInvoiceClient
{
    private readonly BaseHttpClient _userHttpClient;
    private readonly string _controller = "invoices";

    public InvoiceClient()
    {
        string billioUrl = "https://localhost:8091";

        _userHttpClient = new(billioUrl);
    }

    public async Task<InvoiceListResponse> Get()
    {
        return await _userHttpClient.GetAsync<InvoiceListResponse>($"{_controller}");
    }
    /*
    public async Task<InvoiceDataListResponse> Get(InvoiceDataGetRequest request)
    {
        Dictionary<string, string> headers = new()
        {
            { "SellerId", request.SellerId.ToString()! }
        };
        return await _userHttpClient.GetAsync<InvoiceDataListResponse>("{_controller}", headers);
    }
    */
    public async Task<InvoiceResponse?> Get(Guid id)
    {
        return await _userHttpClient.GetAsync<InvoiceResponse>($"{_controller}/{id}");
    }

    public async Task<AddResponse> Add(InvoiceAddRequest invoice)
    {
        return await _userHttpClient.PostAsync<InvoiceAddRequest, AddResponse>($"{_controller}", invoice);
    }

    public async Task Update(InvoiceUpdateRequest invoice)
    {
        await _userHttpClient.PutAsync($"{_controller}", invoice);
    }

    public async Task Delete(Guid id)
    {
        await _userHttpClient.DeleteAsync($"{_controller}/{id}");
    }
}
