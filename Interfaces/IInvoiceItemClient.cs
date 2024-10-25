using BillioIntegrationTest.Clients;
using BillioIntegrationTest.Contracts.Requests.Item;
using BillioIntegrationTest.Contracts.Responses;
using BillioIntegrationTest.Contracts.Responses.Item;

namespace BillioIntegrationTest.Interfaces;

public interface IItemClient
{
    Task<Result<AddResponse>> Add(ItemAddRequest item);
    Task<Result<bool>> Delete(Guid id);
    Task<Result<ItemListResponse>> Get();
    Task<Result<ItemResponse?>> Get(Guid id);
    Task<Result<ItemListResponse>> Get(ItemGetRequest request);
    Task<Result<bool>> Update(ItemUpdateRequest item);
}