using BillioIntegrationTest.Contracts.Requests.Item;
using BillioIntegrationTest.Contracts.Responses;
using BillioIntegrationTest.Contracts.Responses.Item;

namespace BillioIntegrationTest.Interfaces;

public interface IItemClient
{
    Task<AddResponse> Add(ItemAddRequest item);
    Task Delete(Guid id);
    Task<ItemListResponse> Get();
    Task<ItemResponse?> Get(Guid id);
    Task<ItemListResponse> Get(ItemGetRequest request);
    Task Update(ItemUpdateRequest item);
}