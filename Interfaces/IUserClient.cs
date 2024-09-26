using BillioIntegrationTest.Contracts.Requests.User;
using BillioIntegrationTest.Contracts.Responses;
using BillioIntegrationTest.Contracts.Responses.User;

namespace BillioIntegrationTest.Interfaces;

public interface IUserClient
{
    Task<UserLoginResponse> Login(UserLoginRequest user);
    Task<UserListResponse> Get();
    Task<UserResponse?> Get(Guid id);
    Task<AddResponse> Add(UserAddRequest user);
    Task Update(UserUpdateRequest user);
    Task Delete(Guid id);
}