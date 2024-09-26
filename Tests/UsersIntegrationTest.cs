using BillioIntegrationTest.Clients;
using BillioIntegrationTest.Contracts.Requests.User;
using BillioIntegrationTest.Contracts.Responses;
using BillioIntegrationTest.Contracts.Responses.User;
using TUnit.Core;
using TUnit.Core.Interfaces;

namespace IntegrationTests;

internal class UsersIntegrationTest
{
    UserClient _client;

    public UsersIntegrationTest()
    {
        _client = new UserClient();
    }

    [Test]
    [ParallelLimiter<LoadTestParallelLimit>]
    [Arguments("MotherOfDragons@hotmail.com",   "Daenerys",     "Targaryen",    "password123456")]
    [Arguments("WannabeStark@gmail.com",        "Jon",          "Snow",         "password123456")]
    [Arguments("RedPriestess@lordoflight.com",  "Melisandre",   "Of Asshai",    "password123456")]
    public async Task UserAdd_Valid_Success(string email, string name, string lastName, string password)
    {
        UserAddRequest userAddRequest = new UserAddRequest()
        {
            Email = email,
            Name = name,
            LastName = lastName,
            Password = password
        };

        AddResponse result = await _client.Add(userAddRequest);

        await Assert.That(result.Id).IsNotNull();

        UserResponse? user = await _client.Get(result.Id);

        await Assert.That(user).IsNotNull();
        await Assert.That(user?.Id).IsEqualTo(result.Id);
        await Assert.That(user?.Name).IsEqualTo(userAddRequest.Name);
        await Assert.That(user?.LastName).IsEqualTo(userAddRequest.LastName);
        await Assert.That(user?.Email).IsEqualTo(userAddRequest.Email);
        await _client.Delete(result.Id);
    }
    /*
    [Category("Downloads")]
    [Timeout(300_000)]
    [Test, NotInParallel(Order = 1)]
    public async Task DownloadFile1()
    {

    }*/

    public class LoadTestParallelLimit : IParallelLimit
    {
        public int Limit => 50;
    }
}
