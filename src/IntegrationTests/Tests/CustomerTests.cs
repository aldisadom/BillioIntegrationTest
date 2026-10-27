using BillerContracts;
using BillerContracts.Clients;
using BillerContracts.Clients.Interfaces;
using BillerContracts.Requests.Customer;
using BillerContracts.Responses;
using BillerContracts.Responses.Customer;
using IntegrationTests.ApiClients;
using IntegrationTests.Models;
using System.Collections.Concurrent;
using System.Net;

namespace BillioIntegrationTest.Tests;

public static class CustomerTestDataSources
{
    public static ConcurrentDictionary<string, CustomerModel> SavedCustomers = new();
    public static IEnumerable<TestCaseModel<CustomerModel>> AddData()
    {
        yield return new()
        {
            TestName = Emails().First(),
            Data = new()
            {
                SellerEmail = SellerTestDataSources.Emails().First(),
                Email = Emails().First(),
                CompanyName = "Lannister Libations",
                CompanyNumber = "CN123456789",
                Street = "Lannister Lane 123",
                City = "Casterly Rock",
                State = "The Free Cities",
                Phone = "+123450679",
                SeriesName = "LL"
            }
        };
        yield return new()
        {
            TestName = Emails().ToArray()[1],
            Data = new()
            {
                SellerEmail = SellerTestDataSources.Emails().ToArray()[1],
                Email = Emails().ToArray()[1],
                CompanyName = "Faceless Freight",
                CompanyNumber = "FF",
                Street = "No Ones Road 66",
                City = "Bravoos",
                State = "The Free Cities",
                Phone = "+123450679",
                SeriesName = "FF"
            }
        };
    }
    public static IEnumerable<TestCaseError<CustomerModel>> AddDataInvalid()
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
                SellerEmail = SellerTestDataSources.Emails().First(),
                CompanyName = "Lannister Libations",
                CompanyNumber = "CN123456789",
                Street = "Lannister Lane 123",
                City = "Casterly Rock",
                State = "The Free Cities",
                Phone = "+123450679",
                SeriesName = "LL"
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
                SellerEmail = SellerTestDataSources.Emails().First(),
                Email = Emails().First(),
                CompanyNumber = "CN123456789",
                Street = "Lannister Lane 123",
                City = "Casterly Rock",
                State = "The Free Cities",
                Phone = "+123450679",
                SeriesName = "LL"
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
                SellerEmail = SellerTestDataSources.Emails().First(),
                Email = Emails().First(),
                CompanyName = "Lannister Libations",
                Street = "Lannister Lane 123",
                City = "Casterly Rock",
                State = "The Free Cities",
                Phone = "+123450679",
                SeriesName = "LL"
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
                SellerEmail = SellerTestDataSources.Emails().First(),
                Email = Emails().First(),
                CompanyName = "Lannister Libations",
                CompanyNumber = "CN123456789",
                City = "Casterly Rock",
                State = "The Free Cities",
                Phone = "+123450679",
                SeriesName = "LL"
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
                SellerEmail = SellerTestDataSources.Emails().First(),
                Email = Emails().First(),
                CompanyName = "Lannister Libations",
                CompanyNumber = "CN123456789",
                Street = "Lannister Lane 123",
                State = "The Free Cities",
                Phone = "+123450679",
                SeriesName = "LL"
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
                SellerEmail = SellerTestDataSources.Emails().First(),
                Email = Emails().First(),
                CompanyName = "Lannister Libations",
                CompanyNumber = "CN123456789",
                Street = "Lannister Lane 123",
                City = "Casterly Rock",
                Phone = "+123450679",
                SeriesName = "LL"
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
                SellerEmail = SellerTestDataSources.Emails().First(),
                Email = Emails().First(),
                CompanyName = "Lannister Libations",
                CompanyNumber = "CN123456789",
                Street = "Lannister Lane 123",
                City = "Casterly Rock",
                State = "The Free Cities",
                SeriesName = "LL"
            }
        };
        yield return new()
        {
            TestName = "No series",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify series Id"
            },
            Data = new()
            {
                SellerEmail = SellerTestDataSources.Emails().First(),
                Email = Emails().First(),
                CompanyName = "Lannister Libations",
                CompanyNumber = "CN123456789",
                Street = "Lannister Lane 123",
                City = "Casterly Rock",
                State = "The Free Cities",
                Phone = "+123450679"
            }
        };
        yield return new()
        {
            TestName = "Seller the series doesn't belong to",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Change me"
            },
            Data = new()
            {
                Email = Emails().First(),
                CompanyName = "Lannister Libations",
                CompanyNumber = "CN123456789",
                Street = "Lannister Lane 123",
                City = "Casterly Rock",
                State = "The Free Cities",
                Phone = "+123450679",
                SeriesName = "LL"
            }
        };
    }

    public static IEnumerable<TestCaseError<CustomerModel>> UpdateDataInvalid()
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
                CompanyName = "Lannister Libations",
                CompanyNumber = "CN123456789",
                Street = "Lannister Lane 123",
                City = "Casterly Rock",
                State = "The Free Cities",
                Phone = "+123450679",
                SeriesName = "LL"
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
                CompanyName = "Lannister Libations",
                CompanyNumber = "CN123456789",
                Street = "Lannister Lane 123",
                City = "Casterly Rock",
                State = "The Free Cities",
                Phone = "+123450679",
                SeriesName = "LL"
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
                CompanyNumber = "CN123456789",
                Street = "Lannister Lane 123",
                City = "Casterly Rock",
                State = "The Free Cities",
                Phone = "+123450679",
                SeriesName = "LL"
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
                CompanyName = "Lannister Libations",
                Street = "Lannister Lane 123",
                City = "Casterly Rock",
                State = "The Free Cities",
                Phone = "+123450679",
                SeriesName = "LL"
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
                CompanyName = "Lannister Libations",
                CompanyNumber = "CN123456789",
                City = "Casterly Rock",
                State = "The Free Cities",
                Phone = "+123450679",
                SeriesName = "LL"
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
                CompanyName = "Lannister Libations",
                CompanyNumber = "CN123456789",
                Street = "Lannister Lane 123",
                State = "The Free Cities",
                Phone = "+123450679",
                SeriesName = "LL"
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
                CompanyName = "Lannister Libations",
                CompanyNumber = "CN123456789",
                Street = "Lannister Lane 123",
                City = "Casterly Rock",
                Phone = "+123450679",
                SeriesName = "LL"
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
                CompanyName = "Lannister Libations",
                CompanyNumber = "CN123456789",
                Street = "Lannister Lane 123",
                City = "Casterly Rock",
                State = "The Free Cities",
                SeriesName = "LL"
            }
        };
        yield return new()
        {
            TestName = "No series",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify series Id"
            },
            Data = new()
            {
                Id = default,
                Email = Emails().First(),
                CompanyName = "Lannister Libations",
                CompanyNumber = "CN123456789",
                Street = "Lannister Lane 123",
                City = "Casterly Rock",
                State = "The Free Cities",
                Phone = "+123450679"
            }
        };
    }

    // An invoice keeps its own series, so the customer copy in an invoice update needs no series.
    public static IEnumerable<TestCaseError<CustomerModel>> InvoiceUpdateDataInvalid()
    {
        return UpdateDataInvalid().Where(testCase => testCase.TestName != "No series");
    }

    public static IEnumerable<string> Emails()
    {
        yield return "lannister_libations@lannister.com";
        yield return "faceless_freight@gmail.com";
    }

}

public partial class Tests
{
    private static readonly ICustomerClient _customerClient = TestClients.Customer;

    private static string SeriesNameAfterUpdate(CustomerModel customer) => customer.SeriesName == "LL" ? "LX" : "FX";

    private static async Task AssertListedCustomerSeries(CustomerResponse listed)
    {
        CustomerModel saved = CustomerTestDataSources.SavedCustomers.Values.Single(c => c.Id == listed.Id);
        string expectedSeries = SeriesNameAfterUpdate(saved);

        await Assert.That(listed.SeriesId).IsEqualTo(SeriesIdByName(expectedSeries));
        await Assert.That(listed.SeriesName).IsEqualTo(expectedSeries);
    }

    private static Guid SeriesIdByName(string seriesName)
    {
        return string.IsNullOrEmpty(seriesName) ? Guid.Empty : SeriesTestDataSources.SavedSeries[seriesName].Id;
    }

    [Test]
    [DependsOn(nameof(SellerDelete_Valid_Success))]
    public async Task Customer_BeforeTests_DBEmpty()
    {
        CustomerListResponse response = await _customerClient.Get();

        await Assert.That(response.Customers)
            .IsNotNull()
            .And.IsEmpty();
    }

    [Test]
    [DependsOn(nameof(Customer_BeforeTests_DBEmpty))]
    [DependsOn(nameof(SeriesAdd_Valid_Success), [typeof(TestCaseModel<SeriesModel>)])]
    [MethodDataSource(typeof(CustomerTestDataSources), nameof(CustomerTestDataSources.AddData))]
    [DisplayName("Customer add, valid data: $testCase")]
    public async Task CustomerAdd_Valid_Success(TestCaseModel<CustomerModel> testCase)
    {
        CustomerModel customerModel = testCase.Data;
        CustomerAddRequest addRequest = new()
        {
            SellerId = SellerTestDataSources.SavedSellers[customerModel.SellerEmail].Id,
            Email = customerModel.Email,
            CompanyName = customerModel.CompanyName,
            CompanyNumber = customerModel.CompanyNumber,
            Street = customerModel.Street,
            City = customerModel.City,
            State = customerModel.State,
            Phone = customerModel.Phone,
            SeriesId = SeriesIdByName(customerModel.SeriesName)
        };

        AddResponse addResponse = await _customerClient.Add(addRequest);

        await Assert.That(addResponse.Id).IsNotDefault();

        CustomerResponse getResponse = await _customerClient.Get(addResponse.Id);

        await Assert.That(getResponse).IsNotNull();
        await Assert.That(getResponse!.Id).IsEqualTo(addResponse.Id);
        await Assert.That(getResponse!.SellerId).IsEqualTo(addRequest.SellerId);
        await Assert.That(getResponse!.Email).IsEqualTo(addRequest.Email);
        await Assert.That(getResponse!.CompanyName).IsEqualTo(addRequest.CompanyName);
        await Assert.That(getResponse!.Street).IsEqualTo(addRequest.Street);
        await Assert.That(getResponse!.City).IsEqualTo(addRequest.City);
        await Assert.That(getResponse!.State).IsEqualTo(addRequest.State);
        await Assert.That(getResponse!.Phone).IsEqualTo(addRequest.Phone);
        await Assert.That(getResponse!.SeriesId).IsEqualTo(addRequest.SeriesId);
        await Assert.That(getResponse!.SeriesName).IsEqualTo(customerModel.SeriesName);

        CustomerModel customer = new()
        {
            SellerEmail = customerModel.SellerEmail,
            Id = getResponse.Id,
            Email = getResponse.Email,
            CompanyName = getResponse.CompanyName,
            CompanyNumber = getResponse.CompanyNumber,
            Street = getResponse.Street,
            City = getResponse.City,
            State = getResponse.State,
            Phone = getResponse.Phone,
            SeriesName = getResponse.SeriesName
        };

        if (!CustomerTestDataSources.SavedCustomers.TryAdd(addRequest.Email, customer))
            throw new Exception($"Customer with email:{addRequest.Email} can not add to test data");
    }

    [Test]
    [MethodDataSource(typeof(CustomerTestDataSources), nameof(CustomerTestDataSources.AddDataInvalid))]
    [DependsOn(nameof(CustomerAdd_Valid_Success), [typeof(TestCaseModel<CustomerModel>)])]
    [DisplayName("Customer add, invalid data: $testCase")]
    public async Task CustomerAdd_InValid_Fail(TestCaseError<CustomerModel> testCase)
    {
        CustomerModel customerModel = testCase.Data;
        CustomerAddRequest addRequest = new()
        {
            SellerId = string.IsNullOrEmpty(customerModel.SellerEmail)
                ? Guid.NewGuid()
                : SellerTestDataSources.SavedSellers[customerModel.SellerEmail].Id,
            Email = customerModel.Email,
            CompanyName = customerModel.CompanyName,
            CompanyNumber = customerModel.CompanyNumber,
            Street = customerModel.Street,
            City = customerModel.City,
            State = customerModel.State,
            Phone = customerModel.Phone,
            SeriesId = SeriesIdByName(customerModel.SeriesName)
        };

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _customerClient.Add(addRequest)))!.Error!;

        if (testCase.Error.ExtendedMessage == "Change me")
            testCase.Error.ExtendedMessage = $"Series id {addRequest.SeriesId} is invalid for seller id {addRequest.SellerId}";

        await testCase.Error!.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(CustomerAdd_Valid_Success), [typeof(TestCaseModel<CustomerModel>)])]
    public async Task CustomerDelete_Valid_Success()
    {
        CustomerAddRequest addRequest = new()
        {
            SellerId = SellerTestDataSources.SavedSellers[SellerTestDataSources.Emails().First()].Id,
            Email = "delete@me.com",
            CompanyName = "Super deleters",
            CompanyNumber = "CN000000",
            Street = "Unknown",
            City = "Empty",
            State = "Void",
            Phone = "+000000000",
            SeriesId = SeriesIdByName("LL")
        };

        AddResponse addResponse = await _customerClient.Add(addRequest);

        await _customerClient.Delete(addResponse.Id);

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _customerClient.Get(addResponse.Id)))!.Error!;
        ErrorModel expectedError = new("Entity not found", $"Invoice customer:{addResponse.Id} not found", HttpStatusCode.NotFound);
        await expectedError.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(CustomerDelete_Valid_Success))]
    public async Task CustomerDelete_InValid_Fail()
    {
        Guid id = Guid.NewGuid();

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _customerClient.Delete(id)))!.Error!;

        ErrorModel expectedError = new("Entity not found", $"Invoice customer:{id} not found", HttpStatusCode.NotFound);
        await expectedError.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(ItemDelete_Valid_Success))]
    [DependsOn(nameof(ItemAdd_Valid_Success))]
    public async Task CustomerDelete_WhenHaveSeller_Fail()
    {
        Guid id = CustomerTestDataSources.SavedCustomers[CustomerTestDataSources.Emails().First()].Id;

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _customerClient.Delete(id)))!.Error!;

        ErrorModel expectedError = new("Validation failure", $"Can not delete (please clear all dependants) or update (item not found): invoice_customers_id", HttpStatusCode.BadRequest);
        await expectedError.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(CustomerDelete_Valid_Success))]
    [DependsOn(nameof(CustomerUpdate_Valid_Success), [typeof(string)])]
    public async Task CustomerGetAll_Valid_Success()
    {
        CustomerListResponse listResponse = await _customerClient.Get();

        await Assert.That(listResponse.Customers)
            .IsNotNull()
            .And.HasCount().EqualTo(2);

        foreach (var customer in listResponse.Customers)
            await AssertListedCustomerSeries(customer);
    }

    [Test]
    [DependsOn(nameof(CustomerDelete_Valid_Success))]
    [DependsOn(nameof(CustomerUpdate_Valid_Success), [typeof(string)])]
    public async Task CustomerGetAllForSeller_Valid_Success()
    {
        var request = new CustomerGetRequest()
        {
            SellerId = SellerTestDataSources.SavedSellers[SellerTestDataSources.Emails().First()].Id
        };

        CustomerListResponse listResponse = await _customerClient.Get(request);

        await Assert.That(listResponse.Customers)
            .IsNotNull()
            .And.HasCount().EqualTo(1);

        await Assert.That(listResponse.Customers[0].SellerId).IsEquivalentTo(request.SellerId!.Value);
        await AssertListedCustomerSeries(listResponse.Customers[0]);
    }

    [Test]
    [MethodDataSource(typeof(CustomerTestDataSources), nameof(CustomerTestDataSources.Emails))]
    [DependsOn(nameof(CustomerAdd_Valid_Success), [typeof(TestCaseModel<CustomerModel>)])]
    [DisplayName("Customer update, valid data: $email")]
    public async Task CustomerUpdate_Valid_Success(string email)
    {
        CustomerModel customerModel = CustomerTestDataSources.SavedCustomers[email];

        CustomerUpdateRequest updateRequest = new()
        {
            Id = customerModel.Id,
            Email = customerModel.Email + "_new",
            CompanyName = customerModel.CompanyName + "_new",
            CompanyNumber = customerModel.CompanyNumber + "_new",
            Street = customerModel.Street + "_new",
            City = customerModel.City + "_new",
            State = customerModel.State + "_new",
            Phone = customerModel.Phone + "_new",
            SeriesId = SeriesIdByName(SeriesNameAfterUpdate(customerModel))
        };

        await _customerClient.Update(updateRequest);

        CustomerResponse getResponse = await _customerClient.Get(updateRequest.Id);

        await Assert.That(getResponse).IsNotNull();
        await Assert.That(getResponse!.Id).IsEqualTo(updateRequest.Id);
        await Assert.That(getResponse!.Email).IsEqualTo(updateRequest.Email);
        await Assert.That(getResponse!.CompanyName).IsEqualTo(updateRequest.CompanyName);
        await Assert.That(getResponse!.Street).IsEqualTo(updateRequest.Street);
        await Assert.That(getResponse!.City).IsEqualTo(updateRequest.City);
        await Assert.That(getResponse!.State).IsEqualTo(updateRequest.State);
        await Assert.That(getResponse!.Phone).IsEqualTo(updateRequest.Phone);
        await Assert.That(getResponse!.SeriesId).IsEqualTo(updateRequest.SeriesId);
        await Assert.That(getResponse!.SeriesName).IsEqualTo(SeriesNameAfterUpdate(customerModel));
    }

    [Test]
    [DependsOn(nameof(CustomerAdd_Valid_Success), [typeof(TestCaseModel<CustomerModel>)])]
    public async Task CustomerUpdate_SeriesOfOtherSeller_Fail()
    {
        CustomerModel customerModel = CustomerTestDataSources.SavedCustomers[CustomerTestDataSources.Emails().First()];
        SeriesModel otherSellersSeries = SeriesTestDataSources.SavedSeries["FF"];

        CustomerUpdateRequest updateRequest = new()
        {
            Id = customerModel.Id,
            Email = customerModel.Email,
            CompanyName = customerModel.CompanyName,
            CompanyNumber = customerModel.CompanyNumber,
            Street = customerModel.Street,
            City = customerModel.City,
            State = customerModel.State,
            Phone = customerModel.Phone,
            SeriesId = otherSellersSeries.Id
        };

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _customerClient.Update(updateRequest)))!.Error!;

        Guid sellerId = SellerTestDataSources.SavedSellers[customerModel.SellerEmail].Id;
        ErrorModel expectedError = new("Validation failure", $"Series id {otherSellersSeries.Id} is invalid for seller id {sellerId}", HttpStatusCode.BadRequest);
        await expectedError.CheckErrors(error);
    }

    [Test]
    [MethodDataSource(typeof(CustomerTestDataSources), nameof(CustomerTestDataSources.UpdateDataInvalid))]
    [DependsOn(nameof(CustomerAdd_Valid_Success), [typeof(TestCaseModel<CustomerModel>)])]
    [DisplayName("Customer update, invalid data: $testCase")]
    public async Task CustomerUpdate_InValid_Fail(TestCaseError<CustomerModel> testCase)
    {
        CustomerModel customer = testCase.Data;
        CustomerUpdateRequest updateRequest = new()
        {
            Id = customer.Id == default
                ? CustomerTestDataSources.SavedCustomers[CustomerTestDataSources.Emails().First()].Id
                : customer.Id,
            Email = customer.Email,
            CompanyName = customer.CompanyName,
            CompanyNumber = customer.CompanyNumber,
            Street = customer.Street,
            City = customer.City,
            State = customer.State,
            Phone = customer.Phone,
            SeriesId = SeriesIdByName(customer.SeriesName)
        };

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _customerClient.Update(updateRequest)))!.Error!;

        if (customer.Id != default)
            testCase.Error!.ExtendedMessage = $"Invoice customer:{updateRequest.Id} not found";

        await testCase.CheckErrors(error);
    }

    public static async Task Customer_Delete_All()
    {
        await DeleteAll.Run("Customer", async () => (await _customerClient.Get()).Customers.Select(customer => customer.Id), _customerClient.Delete);
    }
}
