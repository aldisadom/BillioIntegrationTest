using BillioIntegrationTest.Clients;
using BillioIntegrationTest.Contracts.Requests.User;
using BillioIntegrationTest.Contracts.Responses;
using BillioIntegrationTest.Contracts.Responses.User;
using BillioIntegrationTest.Models;
using LanguageExt;
using System.Net;
using TUnit.Assertions.Extensions.Generic;
using TUnit.Core.Extensions;

namespace BillioIntegrationTest.Tests;

public static class UserTestDataSources
{
    public static IEnumerable<TestCaseModel<UserAddRequest>> AddData()
    {
        yield return new()
        {
            TestCase = Emails().First(),
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
            TestCase = Emails().ToArray()[1],
            Data = new()
            {
                Email = Emails().ToArray()[1],
                Name = "Jon",
                LastName = "Snow",
                Password = "password"
            }
        };
    }

    public static IEnumerable<TestCaseModel<UserAddRequest>> AddDataInvalid()
    {
        yield return new()
        {
            TestCase = "MotherOfDragons@hotmail.com already exists",
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
            TestCase = "Password too short",
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
            TestCase = "No email address",
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
            TestCase = "No name",
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
            TestCase = "No lastname",
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

    public static IEnumerable<TestCaseModel<UserLoginRequest>> LoginDataInvalid()
    {
        yield return new()
        {
            TestCase = "Incorrect password for MotherOfDragons@hotmail.com",
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
            TestCase = "Incorrect email address",
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
            TestCase = "No email",
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
            TestCase = "No password",
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

    public static IEnumerable<TestCaseModel<UserModel>> UpdateDataInvalid()
    {
        yield return new()
        {
            TestCase = "No name",
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
            TestCase = "No last name",
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
            TestCase = "Random Id",
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

public partial class Tests
{
    private static readonly UserClient _userClient = new();

    public static UserModel GetUserFromTest(string email)
    {
        var addToBagTestContext = TestContext.Current!.GetTests(nameof(UserAdd_Valid_Success));

        foreach (var bag in addToBagTestContext)
        {
            try
            {
                var item = bag.ObjectBag[email];
                if (item is null || item is not UserModel)
                    continue;

                return (UserModel)item;
            }
            catch
            {
            }
        }

        throw new Exception($"User email not found in test data: {email}");
    }

    [Test]
    [Before(Class)]
    public static async Task PrepareTestEnvironment()
    {
        await ItemDelete_AfterAll_Success();
        await CustomerDelete_AfterAll_Success();
        await SellerDelete_AfterAll_Success();
        await UserDelete_AfterAll_Success();
    }

    [Test]
    public static async Task User_BeforeTests_DBEmpty()
    {
        var getResponseResult = await _userClient.Get();
        UserListResponse listResponse = getResponseResult.Match(
            user => { return user; },
            error => { throw new Exception(error.ToString()); }
        );

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

        var addResponseResult = await _userClient.Add(addRequest);
        AddResponse addResponse = addResponseResult.Match(
            user => { return user; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(addResponse.Id).IsNotNull();

        var getResponseResult = await _userClient.Get(addResponse.Id);
        UserResponse? getResponse = getResponseResult.Match(
            user => { return user; },
            error => { throw new Exception(error.ToString()); }
        );

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

        TestContext.Current!.ObjectBag.Add(getResponse!.Email, user);
    }

    [Test]
    [MethodDataSource(typeof(UserTestDataSources), nameof(UserTestDataSources.AddDataInvalid))]
    [DependsOn(nameof(UserAdd_Valid_Success), [typeof(TestCaseModel<UserAddRequest>)])]
    [DisplayName("User add, invalid data: $testCase")]
    public async Task UserAdd_InValid_Fail(TestCaseModel<UserAddRequest> testCase)
    {
        UserAddRequest addRequest = testCase.Data;

        var addResponseResult = await _userClient.Add(addRequest);
        ErrorModel error = addResponseResult.Match(
            user => { throw new Exception(user.ToString()); },
            error => { return error; }
        );

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

        var addResponseResult = await _userClient.Add(addRequest);
        AddResponse addResponse = addResponseResult.Match(
            user => { return user; },
            error => { throw new Exception(error.ToString()); }
        );

        var deleteResponseResult = await _userClient.Delete(addResponse.Id);
        bool deleteResponse = deleteResponseResult.Match(
            user => { return user; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(deleteResponse).IsTrue();

        var getResponseResult = await _userClient.Get(addResponse.Id);
        ErrorModel error = getResponseResult.Match(
            user => { throw new Exception(user!.ToString()); },
            error => { return error; }
        );
        ErrorModel expectedError = new("Entity not found", $"User:{addResponse.Id} not found", HttpStatusCode.NotFound);
        await expectedError.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(UserDelete_Valid_Success))]
    public async Task UserDelete_InValid_Fail()
    {
        Guid id = Guid.NewGuid();

        var deleteResponseResult = await _userClient.Delete(id);
        ErrorModel error = deleteResponseResult.Match(
            user => { throw new Exception(user.ToString()); },
            error => { return error; }
        );

        ErrorModel expectedError = new("Entity not found", $"User:{id} not found", HttpStatusCode.NotFound);
        await expectedError.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(UserDelete_Valid_Success))]
    [DependsOn(nameof(SellerAdd_Valid_Success))]
    public async Task UserDelete_WhenHaveSeller_Fail()
    {
        Guid id = GetUserFromTest(UserTestDataSources.Emails().First()).Id;

        var deleteResponseResult = await _userClient.Delete(id);
        ErrorModel error = deleteResponseResult.Match(
            user => { throw new Exception(user.ToString()); },
            error => { return error; }
        );

        ErrorModel expectedError = new("Validation failure", $"Can not delete (please clear all dependants) or update (item not found): user_id", HttpStatusCode.BadRequest);
        await expectedError.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(UserDelete_Valid_Success))]
    public async Task UserGetAll_Valid_Success()
    {
        var listResponseResult = await _userClient.Get();
        UserListResponse listResponse = listResponseResult.Match(
            user => { return user; },
            error => { throw new Exception(error.ToString()); }
        );

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
        UserModel user = GetUserFromTest(email);

        UserLoginRequest loginRequest = new()
        {
            Email = user.Email,
            Password = user.Password,
        };

        var loginResponseResult = await _userClient.Login(loginRequest);
        UserLoginResponse loginResponse = loginResponseResult.Match(
            user => { return user; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(loginResponse.Token)
            .IsNotNull()
            .And.IsEqualTo("fakeToken");
    }

    [Test]
    [MethodDataSource(typeof(UserTestDataSources), nameof(UserTestDataSources.LoginDataInvalid))]
    [DependsOn(nameof(UserAdd_Valid_Success), [typeof(TestCaseModel<UserAddRequest>)])]
    [DisplayName("User login with invalid: $testCase")]
    public async Task UserLogin_InValid_Fail(TestCaseModel<UserLoginRequest> testCase)
    {
        UserLoginRequest loginRequest = testCase.Data;

        var loginResponseResult = await _userClient.Login(loginRequest);
        ErrorModel error = loginResponseResult.Match(
            user => { throw new Exception(user.ToString()); },
            error => { return error; }
        );

        await testCase.CheckErrors(error);
    }

    [Test]
    [MethodDataSource(typeof(UserTestDataSources), nameof(UserTestDataSources.Emails))]
    [DependsOn(nameof(UserAdd_Valid_Success), [typeof(TestCaseModel<UserAddRequest>)])]
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

        var updateResponseResult = await _userClient.Update(updateRequest);
        bool updateResponse = updateResponseResult.Match(
            user => { return user; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(updateResponse).IsTrue();

        var getResponseResult = await _userClient.Get(user.Id);
        UserResponse? getResponse = getResponseResult.Match(
            user => { return user; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(getResponse).IsNotNull();
        await Assert.That(getResponse!.Id).IsEqualTo(updateRequest.Id);
        await Assert.That(getResponse!.Name).IsEqualTo(updateRequest.Name);
        await Assert.That(getResponse!.LastName).IsEqualTo(updateRequest.LastName);
    }

    [Test]
    [MethodDataSource(typeof(UserTestDataSources), nameof(UserTestDataSources.UpdateDataInvalid))]
    [DependsOn(nameof(UserAdd_Valid_Success), [typeof(TestCaseModel<UserAddRequest>)])]
    [DisplayName("User update, invalid data: $testCase")]
    public async Task UserUpdate_InValid_Fail(TestCaseModel<UserModel> testCase)
    {
        UserModel user = testCase.Data;
        UserUpdateRequest updateRequest = new()
        {
            Id = user.Id == default ? GetUserFromTest(UserTestDataSources.Emails().First()).Id : user.Id,
            Name = user.Name,
            LastName = user.LastName,
        };

        var updateRequestResult = await _userClient.Update(updateRequest);
        ErrorModel error = updateRequestResult.Match(
            user => { throw new Exception(user.ToString()); },
            error => { return error; }
        );

        if (user.Id != default)
            testCase.Error!.ExtendedMessage = $"User:{updateRequest.Id} not found";

        await testCase.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(SellerDelete_AfterAll_Success))]
    public static async Task UserDelete_AfterAll_Success()
    {
        var listResponseResult = await _userClient.Get();
        UserListResponse listResponse = listResponseResult.Match(
            user => { return user; },
            error => { throw new Exception(error.ToString()); }
        );

        foreach (var user in listResponse.Users)
        {
            var deleteResponseResult = await _userClient.Delete(user.Id);
            bool deleteResponse = deleteResponseResult.Match(
                user => { return user; },
                error => { throw new Exception(error.ToString()); }
            );

            await Assert.That(deleteResponse).IsTrue();
        }
    }
}
