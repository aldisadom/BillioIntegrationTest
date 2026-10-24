using BillerContracts;
using BillerContracts.Requests.Customer;
using BillerContracts.Responses;
using BillerContracts.Responses.Customer;

namespace IntegrationTests.Interfaces;

public interface ICustomerClient
{
    Task<Result<AddResponse>> Add(CustomerAddRequest customer);
    Task<Result<bool>> Delete(Guid id);
    Task<Result<CustomerListResponse>> Get();
    Task<Result<CustomerListResponse>> Get(CustomerGetRequest request);
    Task<Result<CustomerResponse?>> Get(Guid id);
    Task<Result<bool>> Update(CustomerUpdateRequest customer);
}
