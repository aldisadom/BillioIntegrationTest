using BillioIntegrationTest.Contracts.Requests.Seller;
using BillioIntegrationTest.Contracts.Responses;
using BillioIntegrationTest.Contracts.Responses.Seller;

namespace BillioIntegrationTest.Interfaces;

public interface ISellerClient
{
    Task<AddResponse> Add(SellerAddRequest seller);
    Task Delete(Guid id);
    Task<SellerListResponse> Get();
    Task<SellerListResponse> Get(SellerGetRequest request);
    Task<SellerResponse?> Get(Guid id);
    Task Update(SellerUpdateRequest seller);
}