﻿using Common;
using Contracts.Requests.Invoice;
using Contracts.Responses;
using Contracts.Responses.Invoice;
using IntegrationTests.Interfaces;

namespace IntegrationTests.Clients;

public class InvoiceClient : IInvoiceClient
{
    private readonly BaseHttpClient _userHttpClient;
    private readonly string _controller = "invoices";

    public InvoiceClient()
    {
        string billioUrl = "https://localhost:8091";

        _userHttpClient = new(billioUrl);
    }

    public async Task<Result<InvoiceListResponse>> Get()
    {
        return await _userHttpClient.GetAsync<InvoiceListResponse>($"{_controller}");
    }

    public async Task<Result<InvoiceListResponse>> Get(InvoiceGetRequest request)
    {
        Dictionary<string, string> headers = new() { };
        if (request.CustomerId is not null)
            headers.Add("CustomerId", request.CustomerId.Value.ToString());
        else if (request.SellerId is not null)
            headers.Add("SellerId", request.SellerId.Value.ToString());
        else if (request.UserId is not null)
            headers.Add("UserId", request.UserId.Value.ToString());

        return await _userHttpClient.GetAsync<InvoiceListResponse>($"{_controller}", headers);
    }

    public async Task<Result<InvoiceResponse?>> Get(Guid id)
    {
        return await _userHttpClient.GetAsync<InvoiceResponse?>($"{_controller}/{id}");
    }

    public async Task<Result<AddResponse>> Add(InvoiceAddRequest invoice)
    {
        return await _userHttpClient.PostAsync<InvoiceAddRequest, AddResponse>($"{_controller}", invoice);
    }

    public async Task<Result<bool>> Update(InvoiceUpdateRequest invoice)
    {
        return await _userHttpClient.PutAsync($"{_controller}", invoice);
    }

    public async Task<Result<bool>> Delete(Guid id)
    {
        return await _userHttpClient.DeleteAsync($"{_controller}/{id}");
    }
}
