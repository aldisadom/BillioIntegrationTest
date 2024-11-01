using BillioIntegrationTest.Helpers;
using Contracts.Requests.Seller;
using Contracts.Responses;
using Contracts.Responses.Invoice;
using Contracts.Responses.Seller;
using IntegrationTests.Clients;
using IntegrationTests.Models;
using System.Net;
using TUnit.Assertions.Extensions.Generic;
using TUnit.Core.Extensions;

namespace BillioIntegrationTest.Tests;

public static class SellerTestDataSources
{
    public static IEnumerable<TestCaseModel<SellerModel>> AddData()
    {
        yield return new()
        {
            TestCase = Emails().First(),
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
            TestCase = Emails().ToArray()[1],
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
    public static IEnumerable<TestCaseModel<SellerModel>> AddDataInvalid()
    {
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
            TestCase = "No company name",
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
            TestCase = "No company number",
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
            TestCase = "No street",
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
            TestCase = "No city",
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
            TestCase = "No state",
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
            TestCase = "No phone",
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
            TestCase = "No bank name",
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
            TestCase = "No bank number",
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
            TestCase = "No user",
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

    public static IEnumerable<TestCaseModel<SellerModel>> UpdateDataInvalid()
    {
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
            TestCase = "No email address",
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
            TestCase = "No company name",
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
            TestCase = "No company number",
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
            TestCase = "No street",
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
            TestCase = "No city",
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
            TestCase = "No state",
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
            TestCase = "No phone",
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
            TestCase = "No bank name",
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
            TestCase = "No bank number",
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
    private static readonly SellerClient _sellerClient = new();
    public static SellerModel GetSellerFromTest(string email)
    {
        return TestDataHelper.GetData<SellerModel>(email, nameof(SellerAdd_Valid_Success));
    }

    [Test]
    [DependsOn(nameof(UserDelete_Valid_Success))]
    public async Task Seller_BeforeTests_DBEmpty()
    {
        var requestResult = await _sellerClient.Get();
        SellerListResponse response = requestResult.Match(
            seller => { return seller; },
            error => { throw new Exception(error.ToString()); }
        );

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
            UserId = GetUserFromTest(sellerModel.UserEmail).Id,
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

        var addResponseResult = await _sellerClient.Add(addRequest);
        AddResponse addResponse = addResponseResult.Match(
            seller => { return seller; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(addResponse.Id).IsNotNull();

        var getResponseResult = await _sellerClient.Get(addResponse.Id);
        SellerResponse? getResponse = getResponseResult.Match(
            seller => { return seller; },
            error => { throw new Exception(error.ToString()); }
        );

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
        TestContext.Current!.ObjectBag.Add(seller!.Email, seller);
    }

    [Test]
    [MethodDataSource(typeof(SellerTestDataSources), nameof(SellerTestDataSources.AddDataInvalid))]
    [DependsOn(nameof(SellerAdd_Valid_Success), [typeof(TestCaseModel<SellerModel>)])]
    [DisplayName("Seller add, invalid data: $testCase")]
    public async Task SellerAdd_InValid_Fail(TestCaseModel<SellerModel> testCase)
    {
        SellerModel sellerModel = testCase.Data;
        SellerAddRequest addRequest = new()
        {
            UserId = string.IsNullOrEmpty(sellerModel.UserEmail) ? Guid.NewGuid() : GetUserFromTest(sellerModel.UserEmail).Id,
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

        var requestResult = await _sellerClient.Add(addRequest);
        ErrorModel error = requestResult.Match(
            seller => { throw new Exception(seller.ToString()); },
            error => { return error; }
        );

        await testCase.Error!.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(SellerAdd_Valid_Success), [typeof(TestCaseModel<SellerModel>)])]
    public async Task SellerDelete_Valid_Success()
    {
        SellerAddRequest addRequest = new()
        {
            UserId = GetUserFromTest(UserTestDataSources.Emails().First()).Id,
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

        var addResponseResult = await _sellerClient.Add(addRequest);
        AddResponse addResponse = addResponseResult.Match(
            seller => { return seller; },
            error => { throw new Exception(error.ToString()); }
        );

        var deleteResponseResult = await _sellerClient.Delete(addResponse.Id);
        bool deleteResponse = deleteResponseResult.Match(
            seller => { return seller; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(deleteResponse).IsTrue();

        var getResponseResult = await _sellerClient.Get(addResponse.Id);
        ErrorModel error = getResponseResult.Match(
            user => { throw new Exception(user!.ToString()); },
            error => { return error; }
        );
        ErrorModel expectedError = new("Entity not found", $"Invoice seller:{addResponse.Id} not found", HttpStatusCode.NotFound);
        await expectedError.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(SellerDelete_Valid_Success))]
    public async Task SellerDelete_InValid_Fail()
    {
        Guid id = Guid.NewGuid();

        var deleteResponseResult = await _sellerClient.Delete(id);
        ErrorModel error = deleteResponseResult.Match(
            seller => { throw new Exception(seller.ToString()); },
            error => { return error; }
        );

        ErrorModel expectedError = new("Entity not found", $"Invoice seller:{id} not found", HttpStatusCode.NotFound);
        await expectedError.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(SellerDelete_Valid_Success))]
    [DependsOn(nameof(CustomerAdd_Valid_Success))]
    public async Task SellerDelete_WhenHaveSeller_Fail()
    {
        Guid id = GetSellerFromTest(SellerTestDataSources.Emails().First()).Id;

        var deleteResponseResult = await _sellerClient.Delete(id);
        ErrorModel error = deleteResponseResult.Match(
            user => { throw new Exception(user.ToString()); },
            error => { return error; }
        );

        ErrorModel expectedError = new("Validation failure", $"Can not delete (please clear all dependants) or update (item not found): seller_id", HttpStatusCode.BadRequest);
        await expectedError.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(SellerDelete_Valid_Success))]
    public async Task SellerGetAll_Valid_Success()
    {
        var listResponseResult = await _sellerClient.Get();
        SellerListResponse listResponse = listResponseResult.Match(
            seller => { return seller; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(listResponse.Sellers)
            .IsNotNull()
            .And.HasCount().EqualTo(2);
    }

    [Test]
    [MethodDataSource(typeof(SellerTestDataSources), nameof(SellerTestDataSources.Emails))]
    [DependsOn(nameof(SellerAdd_Valid_Success), [typeof(SellerAddRequest)])]
    [DisplayName("Seller update, valid data: $email")]
    public async Task SellerUpdate_Valid_Success(string email)
    {
        SellerModel sellerModel = GetSellerFromTest(email);

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

        var updateResponseResult = await _sellerClient.Update(updateRequest);
        bool updateResponse = updateResponseResult.Match(
            seller => { return seller; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(updateResponse).IsTrue();

        var getResponseResult = await _sellerClient.Get(updateRequest.Id);
        SellerResponse? getResponse = getResponseResult.Match(
            seller => { return seller; },
            error => { throw new Exception(error.ToString()); }
        );

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
    public async Task SellerUpdate_InValid_Fail(TestCaseModel<SellerModel> testCase)
    {
        SellerModel seller = testCase.Data;
        SellerUpdateRequest updateRequest = new()
        {
            Id = seller.Id == default ? GetSellerFromTest(SellerTestDataSources.Emails().First()).Id : seller.Id,
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

        var updateResponseResult = await _sellerClient.Update(updateRequest);
        ErrorModel error = updateResponseResult.Match(
            seller => { throw new Exception(seller.ToString()); },
            error => { return error; }
        );

        if (seller.Id != default)
            testCase.Error!.ExtendedMessage = $"Invoice seller:{updateRequest.Id} not found";

        await testCase.CheckErrors(error);
    }

    public static async Task Seller_Delete_All()
    {
        var listResponseResult = await _sellerClient.Get();
        SellerListResponse listResponse = listResponseResult.Match(
            seller => { return seller; },
            error => { throw new Exception(error.ToString()); }
        );

        foreach (var invoice in listResponse.Sellers)
        {
            var deleteResponseResult = await _sellerClient.Delete(invoice.Id);
            bool deleteResponse = deleteResponseResult.Match(
                seller => { return seller; },
                error => { throw new Exception(error.ToString()); }
            );

            await Assert.That(deleteResponse).IsTrue();
        }
    }

    [Test]
    [DependsOn(nameof(CustomerDelete_AfterAll_Success))]
    public static async Task SellerDelete_AfterAll_Success()
    {
        await Seller_Delete_All();
    }
}
