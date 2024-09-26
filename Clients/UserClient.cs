using BillioIntegrationTest.Contracts.Requests.User;
using BillioIntegrationTest.Contracts.Responses;
using BillioIntegrationTest.Contracts.Responses.User;
using BillioIntegrationTest.Interfaces;

namespace BillioIntegrationTest.Clients;

public class UserClient : IUserClient
{
    private readonly BaseHttpClient _userHttpClient;
    private readonly string _controller = "users";

    public UserClient()
    {
        string billioUrl = "https://localhost:8091";

        _userHttpClient = new(billioUrl);
    }

    public async Task<UserLoginResponse> Login(UserLoginRequest user)
    {
        Dictionary<string, string> headers = new()
        {
            { "Email", user.Email },
            { "Password", user.Password }
        };

        return await _userHttpClient.GetAsync<UserLoginResponse>($"{_controller}/Login", headers);
    }

    public async Task<UserListResponse> Get()
    {
        return await _userHttpClient.GetAsync<UserListResponse>($"{_controller}");
    }

    public async Task<UserResponse?> Get(Guid id)
    {
        return await _userHttpClient.GetAsync<UserResponse>($"{_controller}/{id}");
    }

    public async Task<AddResponse> Add(UserAddRequest user)
    {
        return await _userHttpClient.PostAsync<UserAddRequest, AddResponse>($"{_controller}", user);
    }

    public async Task Update(UserUpdateRequest user)
    {
        await _userHttpClient.PutAsync($"{_controller}", user);
    }

    public async Task Delete(Guid id)
    {
        await _userHttpClient.DeleteAsync($"{_controller}/{id}");
    }
}
