using BillerContracts;
using BillerContracts.Clients.Interfaces;
using BillerContracts.Requests.Seller;
using BillerContracts.Responses;
using BillerContracts.Responses.Seller;
using IntegrationTests.ApiClients;
using IntegrationTests.Models;
using System.Collections.Concurrent;
using System.Net;

namespace BillioIntegrationTest.Tests;

public static class SellerTestDataSources
{

    public static ConcurrentDictionary<string, SellerModel> SavedSellers = new();
    public static IEnumerable<TestCaseModel<SellerModel>> AddData()
    {
        yield return new()
        {
            TestName = Emails().First(),
            Data = new()
            {
                UserEmail = UserTestDataSources.Emails().First(),
                Email = Emails().First(),
                CompanyName = "Dragons Delight",
                CompanyNumber = "CN1234569",
                Street = "Main road 5",
                City = "Braavos",
                State = "The Free Cities",
                Phone = "+123450679",
                BankName = "Draconiko",
                BankNumber = "DC99968877453453211"
            }
        };
        yield return new()
        {
            TestName = Emails().ToArray()[1],
            Data = new()
            {
                UserEmail = UserTestDataSources.Emails().ToArray()[1],
                Email = Emails().ToArray()[1],
                CompanyName = "Stark Supply Co",
                CompanyNumber = "CN1234599",
                Street = "Northern Road 48",
                City = "Winterfell",
                State = "The North",
                Phone = "+123456789",
                BankName = "Frost",
                BankNumber = "FR99968877453453211"
            }
        };
    }
    public static IEnumerable<TestCaseError<SellerModel>> AddDataInvalid()
    {
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
                UserEmail = UserTestDataSources.Emails().First(),
                CompanyName = "Dragons Delight",
                CompanyNumber = "CN1234569",
                Street = "Main road 5",
                City = "Braavos",
                State = "The Free Cities",
                Phone = "+123450679",
                BankName = "Draconiko",
                BankNumber = "DC99968877453453211"
            }
        };
        yield return new()
        {
            TestName = "No company name",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify company name"
            },
            Data = new()
            {
                UserEmail = UserTestDataSources.Emails().First(),
                Email = Emails().First(),
                CompanyNumber = "CN1234569",
                Street = "Main road 5",
                City = "Braavos",
                State = "The Free Cities",
                Phone = "+123450679",
                BankName = "Draconiko",
                BankNumber = "DC99968877453453211"
            }
        };
        yield return new()
        {
            TestName = "No company number",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify company number"
            },
            Data = new()
            {
                UserEmail = UserTestDataSources.Emails().First(),
                Email = Emails().First(),
                CompanyName = "Dragons Delight",
                Street = "Main road 5",
                City = "Braavos",
                State = "The Free Cities",
                Phone = "+123450679",
                BankName = "Draconiko",
                BankNumber = "DC99968877453453211"
            }
        };
        yield return new()
        {
            TestName = "No street",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify street"
            },
            Data = new()
            {
                UserEmail = UserTestDataSources.Emails().First(),
                Email = Emails().First(),
                CompanyName = "Dragons Delight",
                CompanyNumber = "CN1234569",
                City = "Braavos",
                State = "The Free Cities",
                Phone = "+123450679",
                BankName = "Draconiko",
                BankNumber = "DC99968877453453211"
            }
        };
        yield return new()
        {
            TestName = "No city",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify city"
            },
            Data = new()
            {
                UserEmail = UserTestDataSources.Emails().First(),
                Email = Emails().First(),
                CompanyName = "Dragons Delight",
                CompanyNumber = "CN1234569",
                Street = "Main road 5",
                State = "The Free Cities",
                Phone = "+123450679",
                BankName = "Draconiko",
                BankNumber = "DC99968877453453211"
            }
        };
        yield return new()
        {
            TestName = "No state",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify state"
            },
            Data = new()
            {
                UserEmail = UserTestDataSources.Emails().First(),
                Email = Emails().First(),
                CompanyName = "Dragons Delight",
                CompanyNumber = "CN1234569",
                Street = "Main road 5",
                City = "Braavos",
                Phone = "+123450679",
                BankName = "Draconiko",
                BankNumber = "DC99968877453453211"
            }
        };
        yield return new()
        {
            TestName = "No phone",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please provide phone number"
            },
            Data = new()
            {
                UserEmail = UserTestDataSources.Emails().First(),
                Email = Emails().First(),
                CompanyName = "Dragons Delight",
                CompanyNumber = "CN1234569",
                Street = "Main road 5",
                City = "Braavos",
                State = "The Free Cities",
                BankName = "Draconiko",
                BankNumber = "DC99968877453453211"
            }
        };
        yield return new()
        {
            TestName = "No bank name",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please provide bank name"
            },
            Data = new()
            {
                UserEmail = UserTestDataSources.Emails().First(),
                Email = Emails().First(),
                CompanyName = "Dragons Delight",
                CompanyNumber = "CN1234569",
                Street = "Main road 5",
                City = "Braavos",
                State = "The Free Cities",
                Phone = "+123450679",
                BankNumber = "DC99968877453453211"
            }
        };
        yield return new()
        {
            TestName = "No bank number",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please provide bank account number"
            },
            Data = new()
            {
                UserEmail = UserTestDataSources.Emails().First(),
                Email = Emails().First(),
                CompanyName = "Dragons Delight",
                CompanyNumber = "CN1234569",
                Street = "Main road 5",
                City = "Braavos",
                State = "The Free Cities",
                Phone = "+123450679",
                BankName = "Draconiko"
            }
        };
        yield return new()
        {
            TestName = "No user",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Key does not exist: user_id"
            },
            Data = new()
            {
                Email = Emails().First(),
                CompanyName = "Dragons Delight",
                CompanyNumber = "CN1234569",
                Street = "Main road 5",
                City = "Braavos",
                State = "The Free Cities",
                Phone = "+123450679",
                BankName = "Draconiko",
                BankNumber = "DC99968877453453211"
            }
        };
    }
    public static IEnumerable<TestCaseError<SellerModel>> UpdateDataInvalid()
    {
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
                CompanyName = "Dragons Delight",
                CompanyNumber = "CN1234569",
                Street = "Main road 5",
                City = "Braavos",
                State = "The Free Cities",
                Phone = "+123450679",
                BankName = "Draconiko",
                BankNumber = "DC99968877453453211"
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
                Id = default,
                CompanyName = "Dragons Delight",
                CompanyNumber = "CN1234569",
                Street = "Main road 5",
                City = "Braavos",
                State = "The Free Cities",
                Phone = "+123450679",
                BankName = "Draconiko",
                BankNumber = "DC99968877453453211"
            }
        };
        yield return new()
        {
            TestName = "No company name",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify company name"
            },
            Data = new()
            {
                Id = default,
                Email = Emails().First(),
                CompanyNumber = "CN1234569",
                Street = "Main road 5",
                City = "Braavos",
                State = "The Free Cities",
                Phone = "+123450679",
                BankName = "Draconiko",
                BankNumber = "DC99968877453453211"
            }
        };
        yield return new()
        {
            TestName = "No company number",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify company number"
            },
            Data = new()
            {
                Id = default,
                Email = Emails().First(),
                CompanyName = "Dragons Delight",
                Street = "Main road 5",
                City = "Braavos",
                State = "The Free Cities",
                Phone = "+123450679",
                BankName = "Draconiko",
                BankNumber = "DC99968877453453211"
            }
        };
        yield return new()
        {
            TestName = "No street",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify street"
            },
            Data = new()
            {
                Id = default,
                Email = Emails().First(),
                CompanyName = "Dragons Delight",
                CompanyNumber = "CN1234569",
                City = "Braavos",
                State = "The Free Cities",
                Phone = "+123450679",
                BankName = "Draconiko",
                BankNumber = "DC99968877453453211"
            }
        };
        yield return new()
        {
            TestName = "No city",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify city"
            },
            Data = new()
            {
                Id = default,
                Email = Emails().First(),
                CompanyName = "Dragons Delight",
                CompanyNumber = "CN1234569",
                Street = "Main road 5",
                State = "The Free Cities",
                Phone = "+123450679",
                BankName = "Draconiko",
                BankNumber = "DC99968877453453211"
            }
        };
        yield return new()
        {
            TestName = "No state",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify state"
            },
            Data = new()
            {
                Id = default,
                Email = Emails().First(),
                CompanyName = "Dragons Delight",
                CompanyNumber = "CN1234569",
                Street = "Main road 5",
                City = "Braavos",
                Phone = "+123450679",
                BankName = "Draconiko",
                BankNumber = "DC99968877453453211"
            }
        };
        yield return new()
        {
            TestName = "No phone",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please provide phone number"
            },
            Data = new()
            {
                Id = default,
                Email = Emails().First(),
                CompanyName = "Dragons Delight",
                CompanyNumber = "CN1234569",
                Street = "Main road 5",
                City = "Braavos",
                State = "The Free Cities",
                BankName = "Draconiko",
                BankNumber = "DC99968877453453211"
            }
        };
        yield return new()
        {
            TestName = "No bank name",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please provide bank name"
            },
            Data = new()
            {
                Id = default,
                Email = Emails().First(),
                CompanyName = "Dragons Delight",
                CompanyNumber = "CN1234569",
                Street = "Main road 5",
                City = "Braavos",
                State = "The Free Cities",
                Phone = "+123450679",
                BankNumber = "DC99968877453453211"
            }
        };
        yield return new()
        {
            TestName = "No bank number",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please provide bank account number"
            },
            Data = new()
            {
                Id = default,
                Email = Emails().First(),
                CompanyName = "Dragons Delight",
                CompanyNumber = "CN1234569",
                Street = "Main road 5",
                City = "Braavos",
                State = "The Free Cities",
                Phone = "+123450679",
                BankName = "Draconiko"
            }
        };
    }
    public static IEnumerable<string> Emails()
    {
        yield return "dragon_delight@winter.com";
        yield return "stark_supply_co@winterfell.com";
    }
}

public partial class Tests
{
    private static readonly ISellerClient _sellerClient = TestClients.Seller;

    [Test]
    [DependsOn("UserDelete_Valid_Success")]
    public async Task Seller_BeforeTests_DBEmpty()
    {
        SellerListResponse response = await _sellerClient.Get();

        await Assert.That(response.Sellers)
            .IsNotNull()
            .And.IsEmpty();
    }

    [Test]
    [DependsOn(nameof(Seller_BeforeTests_DBEmpty))]
    [MethodDataSource(typeof(SellerTestDataSources), nameof(SellerTestDataSources.AddData))]
    [DisplayName("Seller add, valid data: $testCase")]
    public async Task SellerAdd_Valid_Success(TestCaseModel<SellerModel> testCase)
    {
        SellerModel sellerModel = testCase.Data;
        SellerAddRequest addRequest = new()
        {
            UserId = UserTestDataSources.SavedUsers[sellerModel.UserEmail].Id,
            Email = sellerModel.Email,
            CompanyName = sellerModel.CompanyName,
            CompanyNumber = sellerModel.CompanyNumber,
            Street = sellerModel.Street,
            City = sellerModel.City,
            State = sellerModel.State,
            Phone = sellerModel.Phone,
            BankName = sellerModel.BankName,
            BankNumber = sellerModel.BankNumber
        };

        AddResponse addResponse = await _sellerClient.Add(addRequest);

        await Assert.That(addResponse.Id).IsNotDefault();

        SellerResponse getResponse = await _sellerClient.Get(addResponse.Id);

        await Assert.That(getResponse).IsNotNull();
        await Assert.That(getResponse!.Id).IsEqualTo(addResponse.Id);
        await Assert.That(getResponse!.UserId).IsEqualTo(addRequest.UserId);
        await Assert.That(getResponse!.Email).IsEqualTo(addRequest.Email);
        await Assert.That(getResponse!.CompanyName).IsEqualTo(addRequest.CompanyName);
        await Assert.That(getResponse!.Street).IsEqualTo(addRequest.Street);
        await Assert.That(getResponse!.City).IsEqualTo(addRequest.City);
        await Assert.That(getResponse!.State).IsEqualTo(addRequest.State);
        await Assert.That(getResponse!.Phone).IsEqualTo(addRequest.Phone);
        await Assert.That(getResponse!.BankName).IsEqualTo(addRequest.BankName);
        await Assert.That(getResponse!.BankNumber).IsEqualTo(addRequest.BankNumber);

        SellerModel seller = new()
        {
            UserEmail = sellerModel.UserEmail,
            Id = getResponse.Id,
            Email = getResponse.Email,
            CompanyName = getResponse.CompanyName,
            CompanyNumber = getResponse.CompanyNumber,
            Street = getResponse.Street,
            City = getResponse.City,
            State = getResponse.State,
            Phone = getResponse.Phone,
            BankName = getResponse.BankName,
            BankNumber = getResponse.BankNumber
        };

        if (!SellerTestDataSources.SavedSellers.TryAdd(addRequest.Email, seller))
            throw new Exception($"Seller with email:{addRequest.Email} can not add to test data");
    }

    [Test]
    [MethodDataSource(typeof(SellerTestDataSources), nameof(SellerTestDataSources.AddDataInvalid))]
    [DependsOn(nameof(SellerAdd_Valid_Success), [typeof(TestCaseModel<SellerModel>)])]
    [DisplayName("Seller add, invalid data: $testCase")]
    public async Task SellerAdd_InValid_Fail(TestCaseError<SellerModel> testCase)
    {
        SellerModel sellerModel = testCase.Data;
        SellerAddRequest addRequest = new()
        {
            UserId = string.IsNullOrEmpty(sellerModel.UserEmail)
                ? Guid.NewGuid()
                : UserTestDataSources.SavedUsers[sellerModel.UserEmail].Id,
            Email = sellerModel.Email,
            CompanyName = sellerModel.CompanyName,
            CompanyNumber = sellerModel.CompanyNumber,
            Street = sellerModel.Street,
            City = sellerModel.City,
            State = sellerModel.State,
            Phone = sellerModel.Phone,
            BankName = sellerModel.BankName,
            BankNumber = sellerModel.BankNumber
        };

        ErrorModel error = await ApiError.Capture(() => _sellerClient.Add(addRequest));

        await testCase.Error!.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(SellerAdd_Valid_Success), [typeof(TestCaseModel<SellerModel>)])]
    public async Task SellerDelete_Valid_Success()
    {
        SellerAddRequest addRequest = new()
        {
            UserId = UserTestDataSources.SavedUsers[UserTestDataSources.Emails().First()].Id,
            Email = "delete@me.com",
            CompanyName = "Super deleters",
            CompanyNumber = "CN000000",
            Street = "Unknown",
            City = "Empty",
            State = "Void",
            Phone = "+000000000",
            BankName = "Hole",
            BankNumber = "HO00000000000"
        };

        AddResponse addResponse = await _sellerClient.Add(addRequest);

        await _sellerClient.Delete(addResponse.Id);

        ErrorModel error = await ApiError.Capture(() => _sellerClient.Get(addResponse.Id));
        ErrorModel expectedError = new("Entity not found", $"Invoice seller:{addResponse.Id} not found", HttpStatusCode.NotFound);
        await expectedError.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(SellerDelete_Valid_Success))]
    public async Task SellerDelete_InValid_Fail()
    {
        Guid id = Guid.NewGuid();

        ErrorModel error = await ApiError.Capture(() => _sellerClient.Delete(id));

        ErrorModel expectedError = new("Entity not found", $"Invoice seller:{id} not found", HttpStatusCode.NotFound);
        await expectedError.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(SellerDelete_Valid_Success))]
    [DependsOn(nameof(CustomerAdd_Valid_Success))]
    public async Task SellerDelete_WhenHaveSeller_Fail()
    {
        Guid id = SellerTestDataSources.SavedSellers[SellerTestDataSources.Emails().First()].Id;

        ErrorModel error = await ApiError.Capture(() => _sellerClient.Delete(id));

        ErrorModel expectedError = new("Validation failure", $"Can not delete (please clear all dependants) or update (item not found): seller_id", HttpStatusCode.BadRequest);
        await expectedError.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(SellerDelete_Valid_Success))]
    public async Task SellerGetAll_Valid_Success()
    {
        SellerListResponse listResponse = await _sellerClient.Get();

        await Assert.That(listResponse.Sellers)
            .IsNotNull()
            .And.HasCount().EqualTo(2);
    }

    [Test]
    [DependsOn(nameof(SellerDelete_Valid_Success))]
    public async Task SellerGetAllForUser_Valid_Success()
    {
        var request = new SellerGetRequest()
        {
            UserId = UserTestDataSources.SavedUsers[UserTestDataSources.Emails().First()].Id
        };

        SellerListResponse listResponse = await _sellerClient.Get(request);

        await Assert.That(listResponse.Sellers)
            .IsNotNull()
            .And.HasCount().EqualTo(1);

        await Assert.That(listResponse.Sellers[0].UserId).IsEquivalentTo(request.UserId.Value);
    }

    [Test]
    [MethodDataSource(typeof(SellerTestDataSources), nameof(SellerTestDataSources.Emails))]
    [DependsOn(nameof(SellerAdd_Valid_Success), [typeof(TestCaseModel<SellerModel>)])]
    [DisplayName("Seller update, valid data: $email")]
    public async Task SellerUpdate_Valid_Success(string email)
    {
        SellerModel sellerModel = SellerTestDataSources.SavedSellers[email];

        SellerUpdateRequest updateRequest = new()
        {
            Id = sellerModel.Id,
            Email = sellerModel.Email + "_new",
            CompanyName = sellerModel.CompanyName + "_new",
            CompanyNumber = sellerModel.CompanyNumber + "_new",
            Street = sellerModel.Street + "_new",
            City = sellerModel.City + "_new",
            State = sellerModel.State + "_new",
            Phone = sellerModel.Phone + "_new",
            BankName = sellerModel.BankName + "_new",
            BankNumber = sellerModel.BankNumber + "_new"
        };

        await _sellerClient.Update(updateRequest);

        SellerResponse getResponse = await _sellerClient.Get(updateRequest.Id);

        await Assert.That(getResponse).IsNotNull();
        await Assert.That(getResponse!.Id).IsEqualTo(updateRequest.Id);
        await Assert.That(getResponse!.Email).IsEqualTo(updateRequest.Email);
        await Assert.That(getResponse!.CompanyName).IsEqualTo(updateRequest.CompanyName);
        await Assert.That(getResponse!.Street).IsEqualTo(updateRequest.Street);
        await Assert.That(getResponse!.City).IsEqualTo(updateRequest.City);
        await Assert.That(getResponse!.State).IsEqualTo(updateRequest.State);
        await Assert.That(getResponse!.Phone).IsEqualTo(updateRequest.Phone);
        await Assert.That(getResponse!.BankName).IsEqualTo(updateRequest.BankName);
        await Assert.That(getResponse!.BankNumber).IsEqualTo(updateRequest.BankNumber);
    }

    [Test]
    [MethodDataSource(typeof(SellerTestDataSources), nameof(SellerTestDataSources.UpdateDataInvalid))]
    [DependsOn(nameof(SellerAdd_Valid_Success), [typeof(TestCaseModel<SellerModel>)])]
    [DisplayName("Seller update, invalid data: $testCase")]
    public async Task SellerUpdate_InValid_Fail(TestCaseError<SellerModel> testCase)
    {
        SellerModel seller = testCase.Data;
        SellerUpdateRequest updateRequest = new()
        {
            Id = seller.Id == default
                ? SellerTestDataSources.SavedSellers[SellerTestDataSources.Emails().First()].Id
                : seller.Id,
            Email = seller.Email,
            CompanyName = seller.CompanyName,
            CompanyNumber = seller.CompanyNumber,
            Street = seller.Street,
            City = seller.City,
            State = seller.State,
            Phone = seller.Phone,
            BankName = seller.BankName,
            BankNumber = seller.BankNumber
        };

        ErrorModel error = await ApiError.Capture(() => _sellerClient.Update(updateRequest));

        if (seller.Id != default)
            testCase.Error!.ExtendedMessage = $"Invoice seller:{updateRequest.Id} not found";

        await testCase.CheckErrors(error);
    }

    public static async Task Seller_Delete_All()
    {
        // Get returns one page, so delete page by page until nothing is left
        SellerListResponse listResponse = await _sellerClient.Get();

        while (listResponse.Sellers.Count > 0)
        {
            foreach (var seller in listResponse.Sellers)
            {
                await _sellerClient.Delete(seller.Id);
            }

            listResponse = await _sellerClient.Get();
        }
    }
}
