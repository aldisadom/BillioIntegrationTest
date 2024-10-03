using BillioIntegrationTest;
using BillioIntegrationTest.Clients;
using BillioIntegrationTest.Contracts.Requests.User;
using BillioIntegrationTest.Contracts.Responses;
using BillioIntegrationTest.Contracts.Responses.User;
using BillioIntegrationTest.Exceptions;
using BillioIntegrationTest.Models;
using TUnit.Assertions.Extensions.Throws;
using TUnit.Engine.Extensions;
using static IntegrationTests.UserTestDataSources;

namespace IntegrationTests;

public static class UserTestDataSources
{
    public static IEnumerable<UserAddRequest> AddData()
    {
        yield return new UserAddRequest()
        {
            Email = "MotherOfDragons@hotmail.com",
            Name = "Daenerys",
            LastName = "Targaryen",
            Password = "password" 
        };
        yield return new UserAddRequest()
        {
            Email = "WannabeStark@gmail.com",
            Name = "Jon",
            LastName = "Snow",
            Password = "password"
        };
    }

    public static IEnumerable<(Exception, UserAddRequest)> AddDataInvalid()
    {
        yield return (new ClientAPIException(), new UserAddRequest() //already exists
        {
            Email = "MotherOfDragons@hotmail.com",
            Name = "Daenerys",
            LastName = "Targaryen",
            Password = "password"
        });
        yield return (new ClientAPIException(), new UserAddRequest() //password too short
        {
            Email = "MotherOfDragons@hotmail.com",
            Name = "Daenerys",
            LastName = "Targaryen",
            Password = "passwor"
        });
        yield return (new ClientAPIException(), new UserAddRequest() // no email
        {
            Name = "Daenerys",
            LastName = "Targaryen",
            Password = "password"
        });
        yield return (new ClientAPIException(), new UserAddRequest() // no name
        {
            Email = "MotherOfDragons@hotmail.com",
            LastName = "Targaryen",
            Password = "password"
        });
        yield return (new ClientAPIException(), new UserAddRequest() // no last name
        {
            Email = "MotherOfDragons@hotmail.com",
            Name = "Daenerys",
            Password = "password"
        });
    }
    public static IEnumerable<UserLoginRequest> LoginDataInvalid()
    {
        yield return new UserLoginRequest()
        {
            Email = "MotherOfDragons@hotmail.com",
            Password = "incorectpassword"
        };
        yield return new UserLoginRequest()
        {
            Email = "MotherOfDragons@hotmail",
            Password = "password"
        };
        yield return new UserLoginRequest()
        {
            Password = "password"
        };
        yield return new UserLoginRequest()
        {
            Email = "MotherOfDragons@hotmail.com",
        };
    }

    public static IEnumerable<string> Emails()
    {
        yield return "MotherOfDragons@hotmail.com";
        yield return "WannabeStark@gmail.com";
    }

    public static IEnumerable<UserModel> UpdateDataInvalid()
    {
        yield return new UserModel() // no name
        {
            Email = Emails().First(),
            LastName = "Targaryen",
        };
        yield return new UserModel() // no last name
        {
            Email = Emails().First(),
            Name = "Daenerys"
        };
        yield return new UserModel() // use random Id, when Email is null
        {
            Name = "Jon",
            LastName = "Snow"
        };
    }
}


public class UsersIntegrationTest
{
    private static UserClient _client = new();

    [Test]
    [Before(Class)]
    public static async Task User_BeforeTests_DBEmpty()
    {
        UserListResponse result = await _client.Get();

        await Assert.That(result.Users)
            .IsNotNull()
            .IsEmpty();
    }

    public static UserModel GetUserFromTest(string email)
    {
        var a = TestContext.Parameters;
        var addToBagTestContext = TestContext.Current!.GetTests(nameof(UserAdd_Valid_Success));

        foreach (var bag in addToBagTestContext)
        {
            try
            {
                var item = bag.ObjectBag[email];
                if (item is not null)
                    return (UserModel)item;
            }
            catch
            {

            }            
        }

        throw new Exception($"User email not found in test data{email}");
    }

    [Test]
    [MethodDataSource(typeof(UserTestDataSources), nameof(AddData))]
    [DisplayName("User add, valid data: $userAddRequest")]
    public async Task UserAdd_Valid_Success(UserAddRequest userAddRequest)
    {
        AddResponse addResponse = await _client.Add(userAddRequest);

        await Assert.That(addResponse.Id).IsNotNull();

        UserResponse? getResponse = await _client.Get(addResponse.Id);

        await Assert.That(getResponse).IsNotNull();
        await Assert.That(getResponse!.Id).IsEqualTo(addResponse.Id);
        await Assert.That(getResponse!.Name).IsEqualTo(userAddRequest.Name);
        await Assert.That(getResponse!.LastName).IsEqualTo(userAddRequest.LastName);
        await Assert.That(getResponse!.Email).IsEqualTo(userAddRequest.Email);

        UserModel user = new()
        {
            Id = addResponse.Id,
            Email = userAddRequest.Email,
            Name = userAddRequest.Name,
            LastName = userAddRequest.LastName,
            Password = userAddRequest.Password
        };

        TestContext.Current!.ObjectBag.Add(getResponse!.Email, user);
    }

    [Test]
    [MethodDataSource(typeof(UserTestDataSources), nameof(AddDataInvalid))]
    [DependsOn(nameof(UserAdd_Valid_Success), [typeof(UserAddRequest)])]
    [DisplayName("User add, invalid data: $userAddRequest")]
    public async Task UserAdd_InValid_Fail(Exception ex, UserAddRequest userAddRequest)
    {
        await Assert.That(async () => { await _client.Add(userAddRequest);})
                .ThrowsException();
    }

    [Test]
    [DependsOn(nameof(UserAdd_Valid_Success), [typeof(UserAddRequest)])]
    public async Task UserDelete_Valid_Success()
    {
        UserAddRequest userAddRequest = new()
        {
            Email = "ToBeDeleted@deleteMe.com",
            Name = "Ready",
            LastName = "To",
            Password = "DeleteMe123"
        };

        AddResponse addResponse = await _client.Add(userAddRequest);

        await Assert.That(async () => { await _client.Delete(addResponse.Id); })
                .ThrowsNothing();

        await Assert.That(async () => { await _client.Get(addResponse.Id); })
                .ThrowsException();
    }

    [Test]
    [DependsOn(nameof(UserDelete_Valid_Success))]
    public async Task UserDelete_InValid_Fail()
    {
        Guid id = Guid.NewGuid();

        await Assert.That(async () => { await _client.Delete(id); })
                .ThrowsException();
    }

    [Test]
    [DependsOn(nameof(UserDelete_InValid_Fail), [typeof(UserAddRequest)])]
    public async Task UserGetAll_Valid_Success()
    {
        UserListResponse getResponse = await _client.Get();

        await Assert.That(getResponse.Users).IsNotNull();
        await Assert.That(getResponse.Users.Count).IsEqualTo(2);       
    }
    
    [Test]
    [MethodDataSource(typeof(UserTestDataSources), nameof(Emails))]
    [DependsOn(nameof(UserAdd_Valid_Success), [typeof(UserAddRequest)])]
    [DisplayName("User login: $email")]
    public async Task UserLogin_Valid_Success(string email)
    {
        UserModel user = GetUserFromTest(email);

        UserLoginRequest loginRequest = new UserLoginRequest()
        {
            Email = user.Email,
            Password = user.Password,
        };

        UserLoginResponse loginResponse = await _client.Login(loginRequest);

        await Assert.That(loginResponse.Token).IsNotNull()
            .IsNotEmpty()
            .IsEqualTo("fakeToken");
    }

    [Test]
    [MethodDataSource(typeof(UserTestDataSources), nameof(LoginDataInvalid))]
    [DependsOn(nameof(UserAdd_Valid_Success), [typeof(UserAddRequest)])]
    [DisplayName("User login with invalid: $loginRequest")]
    public async Task UserLogin_InValid_Fail(UserLoginRequest loginRequest)
    {
        await Assert.That(async () => { await _client.Login(loginRequest); })
                .ThrowsException();
    }        

    [Test]
    [MethodDataSource(typeof(UserTestDataSources), nameof(Emails))]
    [DependsOn(nameof(UserAdd_Valid_Success), [typeof(UserAddRequest)])]
    [DisplayName("User update, valid data: $email")]
    public async Task UserUpdate_Valid_Success(string email)
    {
        UserModel user = GetUserFromTest(email);

        UserUpdateRequest updateRequest = new()
        {
            Id = user.Id,
            Name = user.Name + "_new_" + "Name",
            LastName = user.LastName + "_new_" + "LastName",
        };

        await Assert.That(async () => { await _client.Update(updateRequest); })
                .ThrowsNothing();

        UserResponse? getResponse = await _client.Get(updateRequest.Id);

        await Assert.That(getResponse).IsNotNull();
        await Assert.That(getResponse!.Id).IsEqualTo(updateRequest.Id);
        await Assert.That(getResponse!.Name).IsEqualTo(updateRequest.Name);
        await Assert.That(getResponse!.LastName).IsEqualTo(updateRequest.LastName);
    }

    [Test]
    [MethodDataSource(typeof(UserTestDataSources), nameof(UpdateDataInvalid))]
    [DependsOn(nameof(UserAdd_Valid_Success), [typeof(UserAddRequest)])]
    [DisplayName("User update, invalid data: $user")]
    public async Task UserUpdate_InValid_Fail(UserModel user)
    {
        UserUpdateRequest updateRequest = new()
        {
            Id = string.IsNullOrEmpty(user.Email) ? Guid.NewGuid() : GetUserFromTest(user.Email).Id,
            Name = user.Name,
            LastName = user.LastName,
        };

        await Assert.That(async () => { await _client.Update(updateRequest); })
                .ThrowsException();
    }

    [Test]    
    [After(Assembly)]
    public static async Task UserDelete_AfterAll_Success()
    {
        UserListResponse getResponse = await _client.Get();

        foreach (var user in getResponse.Users)
        {
            await Assert.That(async () => { await _client.Delete(user.Id); })
                .ThrowsNothing();
        }
    }
    
    
}
