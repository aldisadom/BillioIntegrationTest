using BillerContracts;
using BillerContracts.Requests.User;
using BillerContracts.Responses;
using BillerContracts.Responses.User;

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