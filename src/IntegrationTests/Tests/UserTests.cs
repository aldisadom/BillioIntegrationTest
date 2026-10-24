using BillerContracts;
using BillerContracts.Clients;
using BillerContracts.Clients.Interfaces;
using BillerContracts.Requests.User;
using BillerContracts.Responses;
using BillerContracts.Responses.User;
using IntegrationTests;
using IntegrationTests.ApiClients;
using IntegrationTests.Models;
using System.Collections.Concurrent;
using System.Net;

namespace BillioIntegrationTest.Tests;

public static class UserTestDataSources
{
    public static ConcurrentDictionary<string, UserModel> SavedUsers = new();

    public static IEnumerable<TestCaseModel<UserAddRequest>> AddData()
    {
        yield return new()
        {
            TestName = Emails().First(),
            Data = new()
            {
                Email = Emails().First(),
                Name = "Daenerys",
                LastName = "Targaryen",
                Password = "password"
            }
        };
        yield return new()
        {
            TestName = Emails().ToArray()[1],
            Data = new()
            {
                Email = Emails().ToArray()[1],
                Name = "Jon",
                LastName = "Snow",
                Password = "password"
            }
        };
    }

    public static IEnumerable<TestCaseError<UserAddRequest>> AddDataInvalid()
    {
        yield return new()
        {
            TestName = "MotherOfDragons@hotmail.com already exists",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Key is already used: email"
            },
            Data = new()
            {
                Email = Emails().First(),
                Name = "Daenerys",
                LastName = "Targaryen",
                Password = "password"
            }
        };
        yield return new()
        {
            TestName = "Password too short",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify a more complex password"
            },
            Data = new()
            {
                Email = Emails().First(),
                Name = "Daenerys",
                LastName = "Targaryen",
                Password = "passwor"
            }
        };
        yield return new()
        {
            TestName = "No email address",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please provide valid email address"
            },
            Data = new()
            {
                Name = "Daenerys",
                LastName = "Targaryen",
                Password = "password"
            }
        };
        yield return new()
        {
            TestName = "No name",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify name"
            },
            Data = new()
            {
                Email = Emails().First(),
                LastName = "Targaryen",
                Password = "password"
            }
        };
        yield return new()
        {
            TestName = "No lastname",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify last name"
            },
            Data = new()
            {
                Email = Emails().First(),
                Name = "Daenerys",
                Password = "password"
            }
        };
    }

    public static IEnumerable<TestCaseError<UserLoginRequest>> LoginDataInvalid()
    {
        yield return new()
        {
            TestName = "Incorrect password for MotherOfDragons@hotmail.com",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Message = "Unauthorized",
                ExtendedMessage = "Invalid login data"
            },
            Data = new()
            {
                Email = Emails().First(),
                Password = "incorectpassword"
            }
        };
        yield return new()
        {
            TestName = "Incorrect email address",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Message = "Unauthorized",
                ExtendedMessage = "Invalid login data"
            },
            Data = new()
            {
                Email = "MotherOfDragons@hotmail",
                Password = "password"
            }
        };
        yield return new()
        {
            TestName = "No email",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify email"
            },
            Data = new()
            {
                Password = "password"
            }
        };
        yield return new()
        {
            TestName = "No password",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify a password"
            },
            Data = new()
            {
                Email = Emails().First(),
            }
        };
    }

    public static IEnumerable<TestCaseError<UserModel>> UpdateDataInvalid()
    {
        yield return new()
        {
            TestName = "No name",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify a name"
            },
            Data = new()
            {
                Id = default,
                Email = Emails().First(),
                LastName = "Targaryen",
            }
        };
        yield return new()
        {
            TestName = "No last name",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify a last name"
            },
            Data = new()
            {
                Id = default,
                Email = Emails().First(),
                Name = "Daenerys"
            }
        };
        yield return new()
        {
            TestName = "Random Id",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "Entity not found",
                ExtendedMessage = "Change me"
            },
            Data = new()
            {
                Id = Guid.NewGuid(),
                Email = Emails().First(),
                Name = "Jon",
                LastName = "Snow"
            }
        };
    }

    public static IEnumerable<string> Emails()
    {
        yield return "MotherOfDragons@hotmail.com";
        yield return "WannabeStark@gmail.com";
    }
}

[ArgumentDisplayFormatter<ArgumentFormatter>]
public partial class Tests
{
    private static readonly IUserClient _userClient = TestClients.User;

    [Test]
    public async Task User_BeforeTests_DBEmpty()
    {
        UserListResponse listResponse = await _userClient.Get();

        await Assert.That(listResponse.Users)
            .IsNotNull()
            .And.IsEmpty();
    }

    [Test]
    [MethodDataSource(typeof(UserTestDataSources), nameof(UserTestDataSources.AddData))]
    [DependsOn(nameof(User_BeforeTests_DBEmpty))]
    [DisplayName("User add, valid data: $testCase")]
    public async Task UserAdd_Valid_Success(TestCaseModel<UserAddRequest> testCase)
    {
        UserAddRequest addRequest = testCase.Data;

        AddResponse addResponse = await _userClient.Add(addRequest);

        await Assert.That(addResponse.Id).IsNotDefault();

        UserResponse getResponse = await _userClient.Get(addResponse.Id);

        await Assert.That(getResponse).IsNotNull();
        await Assert.That(getResponse!.Id).IsEqualTo(addResponse.Id);
        await Assert.That(getResponse!.Name).IsEqualTo(addRequest.Name);
        await Assert.That(getResponse!.LastName).IsEqualTo(addRequest.LastName);
        await Assert.That(getResponse!.Email).IsEqualTo(addRequest.Email);

        UserModel user = new()
        {
            Id = addResponse.Id,
            Email = addRequest.Email,
            Name = addRequest.Name,
            LastName = addRequest.LastName,
            Password = addRequest.Password
        };

        if (!UserTestDataSources.SavedUsers.TryAdd(addRequest.Email, user))
            throw new Exception($"User with email:{addRequest.Email} can not add to test data");
    }

    [Test]
    [MethodDataSource(typeof(UserTestDataSources), nameof(UserTestDataSources.AddDataInvalid))]
    [DependsOn(nameof(UserAdd_Valid_Success), [typeof(TestCaseModel<UserAddRequest>)])]
    [DisplayName("User add, invalid data: $testCase")]
    public async Task UserAdd_InValid_Fail(TestCaseError<UserAddRequest> testCase)
    {
        UserAddRequest addRequest = testCase.Data;

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _userClient.Add(addRequest)))!.Error!;

        await testCase.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(UserAdd_Valid_Success), [typeof(TestCaseModel<UserAddRequest>)])]
    public async Task UserDelete_Valid_Success()
    {
        UserAddRequest addRequest = new()
        {
            Email = "ToBeDeleted@deleteMe.com",
            Name = "Ready",
            LastName = "To",
            Password = "DeleteMe123"
        };

        AddResponse addResponse = await _userClient.Add(addRequest);

        await _userClient.Delete(addResponse.Id);

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _userClient.Get(addResponse.Id)))!.Error!;
        ErrorModel expectedError = new("Entity not found", $"User:{addResponse.Id} not found", HttpStatusCode.NotFound);
        await expectedError.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(UserDelete_Valid_Success))]
    public async Task UserDelete_InValid_Fail()
    {
        Guid id = Guid.NewGuid();

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _userClient.Delete(id)))!.Error!;

        ErrorModel expectedError = new("Entity not found", $"User:{id} not found", HttpStatusCode.NotFound);
        await expectedError.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(UserDelete_Valid_Success))]
    [DependsOn("SellerAdd_Valid_Success")]
    public async Task UserDelete_WhenHaveSeller_Fail()
    {
        Guid id = UserTestDataSources.SavedUsers[UserTestDataSources.Emails().First()].Id;

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _userClient.Delete(id)))!.Error!;

        ErrorModel expectedError = new("Validation failure", $"Can not delete (please clear all dependants) or update (item not found): user_id", HttpStatusCode.BadRequest);
        await expectedError.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(UserDelete_Valid_Success))]
    public async Task UserGetAll_Valid_Success()
    {
        UserListResponse listResponse = await _userClient.Get();

        await Assert.That(listResponse.Users)
            .IsNotNull()
            .And.HasCount().EqualTo(2);
    }

    [Test]
    [MethodDataSource(typeof(UserTestDataSources), nameof(UserTestDataSources.Emails))]
    [DependsOn(nameof(UserAdd_Valid_Success), [typeof(TestCaseModel<UserAddRequest>)])]
    [DisplayName("User login: $email")]
    public async Task UserLogin_Valid_Success(string email)
    {
        UserModel user = UserTestDataSources.SavedUsers[email];

        UserLoginRequest loginRequest = new()
        {
            Email = user.Email,
            Password = user.Password,
        };

        UserLoginResponse loginResponse = await _userClient.Login(loginRequest);

        await Assert.That(loginResponse.Token)
            .IsNotNull()
            .And.IsEqualTo("fakeToken");
    }

    [Test]
    [MethodDataSource(typeof(UserTestDataSources), nameof(UserTestDataSources.LoginDataInvalid))]
    [DependsOn(nameof(UserAdd_Valid_Success), [typeof(TestCaseModel<UserAddRequest>)])]
    [DisplayName("User login with invalid: $testCase")]
    public async Task UserLogin_InValid_Fail(TestCaseError<UserLoginRequest> testCase)
    {
        UserLoginRequest loginRequest = testCase.Data;

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _userClient.Login(loginRequest)))!.Error!;

        await testCase.CheckErrors(error);
    }

    [Test]
    [MethodDataSource(typeof(UserTestDataSources), nameof(UserTestDataSources.Emails))]
    [DependsOn(nameof(UserAdd_Valid_Success), [typeof(TestCaseModel<UserAddRequest>)])]
    [DisplayName("User update, valid data: $email")]
    public async Task UserUpdate_Valid_Success(string email)
    {
        UserModel user = UserTestDataSources.SavedUsers[email];

        UserUpdateRequest updateRequest = new()
        {
            Id = user.Id,
            Name = user.Name + "_new",
            LastName = user.LastName + "_new",
        };

        await _userClient.Update(updateRequest);

        UserResponse getResponse = await _userClient.Get(user.Id);

        await Assert.That(getResponse).IsNotNull();
        await Assert.That(getResponse!.Id).IsEqualTo(updateRequest.Id);
        await Assert.That(getResponse!.Name).IsEqualTo(updateRequest.Name);
        await Assert.That(getResponse!.LastName).IsEqualTo(updateRequest.LastName);
    }

    [Test]
    [MethodDataSource(typeof(UserTestDataSources), nameof(UserTestDataSources.UpdateDataInvalid))]
    [DependsOn(nameof(UserAdd_Valid_Success), [typeof(TestCaseModel<UserAddRequest>)])]
    [DisplayName("User update, invalid data: $testCase")]
    public async Task UserUpdate_InValid_Fail(TestCaseError<UserModel> testCase)
    {
        UserModel user = testCase.Data;
        UserUpdateRequest updateRequest = new()
        {
            Id = user.Id == default
                ? UserTestDataSources.SavedUsers[UserTestDataSources.Emails().First()].Id
                : user.Id,
            Name = user.Name,
            LastName = user.LastName,
        };

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _userClient.Update(updateRequest)))!.Error!;

        if (user.Id != default)
            testCase.Error!.ExtendedMessage = $"User:{updateRequest.Id} not found";

        await testCase.CheckErrors(error);
    }

    public static async Task User_Delete_All()
    {
        await DeleteAll.Run("User", async () => (await _userClient.Get()).Users.Select(user => user.Id), _userClient.Delete);
    }
}
