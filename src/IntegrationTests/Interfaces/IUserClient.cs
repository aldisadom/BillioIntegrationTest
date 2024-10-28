using Contracts.Requests.User;
using Contracts.Responses;
using Contracts.Responses.User;
using IntegrationTests.Clients;

namespace IntegrationTests.Interfaces;

public interface IUserClient
{
    Task<Result<UserLoginResponse>> Login(UserLoginRequest user);
    Task<Result<UserListResponse>> Get();
    Task<Result<UserResponse?>> Get(Guid id);
    Task<Result<AddResponse>> Add(UserAddRequest user);
    Task<Result<bool>> Update(UserUpdateRequest user);
    Task<Result<bool>> Delete(Guid id);
}