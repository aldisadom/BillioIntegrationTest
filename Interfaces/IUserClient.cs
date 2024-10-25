using BillioIntegrationTest.Clients;
using BillioIntegrationTest.Contracts.Requests.User;
using BillioIntegrationTest.Contracts.Responses;
using BillioIntegrationTest.Contracts.Responses.User;

namespace BillioIntegrationTest.Interfaces;

public interface IUserClient
{
    Task<Result<UserLoginResponse>> Login(UserLoginRequest user);
    Task<Result<UserListResponse>> Get();
    Task<Result<UserResponse?>> Get(Guid id);
    Task<Result<AddResponse>> Add(UserAddRequest user);
    Task<Result<bool>> Update(UserUpdateRequest user);
    Task<Result<bool>> Delete(Guid id);
}