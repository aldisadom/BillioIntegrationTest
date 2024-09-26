using BillioIntegrationTest.Contracts.Requests.Customer;
using BillioIntegrationTest.Contracts.Responses;
using BillioIntegrationTest.Contracts.Responses.Customer;

namespace BillioIntegrationTest.Interfaces;

public interface ICustomerClient
{
    Task<AddResponse> Add(CustomerAddRequest customer);
    Task Delete(Guid id);
    Task<CustomerListResponse> Get();
    Task<CustomerListResponse> Get(CustomerGetRequest request);
    Task<CustomerResponse?> Get(Guid id);
    Task Update(CustomerUpdateRequest customer);
}
