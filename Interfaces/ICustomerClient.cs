using BillioIntegrationTest.Clients;
using BillioIntegrationTest.Contracts.Requests.Customer;
using BillioIntegrationTest.Contracts.Responses;
using BillioIntegrationTest.Contracts.Responses.Customer;

namespace BillioIntegrationTest.Interfaces;

public interface ICustomerClient
{
    Task<Result<AddResponse>> Add(CustomerAddRequest customer);
    Task<Result<bool>> Delete(Guid id);
    Task<Result<CustomerListResponse>> Get();
    Task<Result<CustomerListResponse>> Get(CustomerGetRequest request);
    Task<Result<CustomerResponse?>> Get(Guid id);
    Task<Result<bool>> Update(CustomerUpdateRequest customer);
}
