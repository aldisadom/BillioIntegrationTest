using BillioIntegrationTest.Clients;
using BillioIntegrationTest.Contracts.Requests.Seller;
using BillioIntegrationTest.Contracts.Responses;
using BillioIntegrationTest.Contracts.Responses.Seller;

namespace BillioIntegrationTest.Interfaces;

public interface ISellerClient
{
    Task<Result<AddResponse>> Add(SellerAddRequest seller);
    Task<Result<bool>> Delete(Guid id);
    Task<Result<SellerListResponse>> Get();
    Task<Result<SellerListResponse>> Get(SellerGetRequest request);
    Task<Result<SellerResponse?>> Get(Guid id);
    Task<Result<bool>> Update(SellerUpdateRequest seller);
}