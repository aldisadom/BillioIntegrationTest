using BillioIntegrationTest.Helpers;
using BillioIntegrationTest.Models;
using Contracts.Requests.Customer;
using Contracts.Responses;
using Contracts.Responses.Customer;
using Contracts.Responses.Invoice;
using IntegrationTests.Clients;
using IntegrationTests.Models;
using System.Net;
using System.Xml.Linq;
using TUnit.Assertions.Extensions.Generic;
using TUnit.Core.Extensions;

namespace BillioIntegrationTest.Tests;

public static class CustomerTestDataSources
{
    public static IEnumerable<TestCaseModel<CustomerModel>> AddData()
    {
        yield return new()
        {
            TestCase = Emails().First(),
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
                InvoiceName = "LL"
            }
        };
        yield return new()
        {
            TestCase = Emails().ToArray()[1],
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
                InvoiceName = "FF"
            }
        };
    }
    public static IEnumerable<TestCaseModel<CustomerModel>> AddDataInvalid()
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
                SellerEmail = SellerTestDataSources.Emails().First(),
                CompanyName = "Lannister Libations",
                CompanyNumber = "CN123456789",
                Street = "Lannister Lane 123",
                City = "Casterly Rock",
                State = "The Free Cities",
                Phone = "+123450679",
                InvoiceName = "LL"
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
                SellerEmail = SellerTestDataSources.Emails().First(),
                Email = Emails().First(),
                CompanyNumber = "CN123456789",
                Street = "Lannister Lane 123",
                City = "Casterly Rock",
                State = "The Free Cities",
                Phone = "+123450679",
                InvoiceName = "LL"
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
                SellerEmail = SellerTestDataSources.Emails().First(),
                Email = Emails().First(),
                CompanyName = "Lannister Libations",
                Street = "Lannister Lane 123",
                City = "Casterly Rock",
                State = "The Free Cities",
                Phone = "+123450679",
                InvoiceName = "LL"
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
                SellerEmail = SellerTestDataSources.Emails().First(),
                Email = Emails().First(),
                CompanyName = "Lannister Libations",
                CompanyNumber = "CN123456789",
                City = "Casterly Rock",
                State = "The Free Cities",
                Phone = "+123450679",
                InvoiceName = "LL"
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
                SellerEmail = SellerTestDataSources.Emails().First(),
                Email = Emails().First(),
                CompanyName = "Lannister Libations",
                CompanyNumber = "CN123456789",
                Street = "Lannister Lane 123",
                State = "The Free Cities",
                Phone = "+123450679",
                InvoiceName = "LL"
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
                SellerEmail = SellerTestDataSources.Emails().First(),
                Email = Emails().First(),
                CompanyName = "Lannister Libations",
                CompanyNumber = "CN123456789",
                Street = "Lannister Lane 123",
                City = "Casterly Rock",
                Phone = "+123450679",
                InvoiceName = "LL"
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
                SellerEmail = SellerTestDataSources.Emails().First(),
                Email = Emails().First(),
                CompanyName = "Lannister Libations",
                CompanyNumber = "CN123456789",
                Street = "Lannister Lane 123",
                City = "Casterly Rock",
                State = "The Free Cities",
                InvoiceName = "LL"
            }
        };
        yield return new()
        {
            TestCase = "No invoice name",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please provide invoice name"
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
            TestCase = "No seller",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Key does not exist: seller_id"
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
                InvoiceName = "LL"
            }
        };
    }

    public static IEnumerable<TestCaseModel<CustomerModel>> UpdateDataInvalid()
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
                CompanyName = "Lannister Libations",
                CompanyNumber = "CN123456789",
                Street = "Lannister Lane 123",
                City = "Casterly Rock",
                State = "The Free Cities",
                Phone = "+123450679",
                InvoiceName = "LL",
                InvoiceNumber = 99,
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
                CompanyName = "Lannister Libations",
                CompanyNumber = "CN123456789",
                Street = "Lannister Lane 123",
                City = "Casterly Rock",
                State = "The Free Cities",
                Phone = "+123450679",
                InvoiceName = "LL",
                InvoiceNumber = 99,
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
                CompanyNumber = "CN123456789",
                Street = "Lannister Lane 123",
                City = "Casterly Rock",
                State = "The Free Cities",
                Phone = "+123450679",
                InvoiceName = "LL",
                InvoiceNumber = 99
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
                CompanyName = "Lannister Libations",
                Street = "Lannister Lane 123",
                City = "Casterly Rock",
                State = "The Free Cities",
                Phone = "+123450679",
                InvoiceName = "LL",
                InvoiceNumber = 99
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
                CompanyName = "Lannister Libations",
                CompanyNumber = "CN123456789",
                City = "Casterly Rock",
                State = "The Free Cities",
                Phone = "+123450679",
                InvoiceName = "LL",
                InvoiceNumber = 99
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
                CompanyName = "Lannister Libations",
                CompanyNumber = "CN123456789",
                Street = "Lannister Lane 123",
                State = "The Free Cities",
                Phone = "+123450679",
                InvoiceName = "LL",
                InvoiceNumber = 99
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
                CompanyName = "Lannister Libations",
                CompanyNumber = "CN123456789",
                Street = "Lannister Lane 123",
                City = "Casterly Rock",
                Phone = "+123450679",
                InvoiceName = "LL",
                InvoiceNumber = 99
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
                CompanyName = "Lannister Libations",
                CompanyNumber = "CN123456789",
                Street = "Lannister Lane 123",
                City = "Casterly Rock",
                State = "The Free Cities",
                InvoiceName = "LL",
                InvoiceNumber = 99
            }
        };
        yield return new()
        {
            TestCase = "No invoice name",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please provide invoice name"
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
                Phone = "+123450679",
                InvoiceNumber = 99
            }
        };
        yield return new()
        {
            TestCase = "No invoice number",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please provide invoice number that should be more than 0"
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
                Phone = "+123450679",
                InvoiceName = "LL"
            }
        };
        yield return new()
        {
            TestCase = "Invoice number 0",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please provide invoice number that should be more than 0"
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
                Phone = "+123450679",
                InvoiceName = "LL",
                InvoiceNumber = 0
            }
        };
    }

    public static IEnumerable<string> Emails()
    {
        yield return "lannister_libations@lannister.com";
        yield return "faceless_freight@gmail.com";
    }

}

public partial class Tests
{
    private static readonly CustomerClient _customerClient = new();
    public static CustomerModel GetCustomerFromTest(string email)
    {
        return TestDataHelper.GetData<CustomerModel>(email, nameof(CustomerAdd_Valid_Success));
    }

    [Test]
    [DependsOn(nameof(SellerDelete_Valid_Success))]
    public async Task Customer_BeforeTests_DBEmpty()
    {
        var requestResult = await _customerClient.Get();
        CustomerListResponse response = requestResult.Match(
            customer => { return customer; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(response.Customers)
            .IsNotNull()
            .And.IsEmpty();
    }

    [Test]
    [DependsOn(nameof(Customer_BeforeTests_DBEmpty))]
    [MethodDataSource(typeof(CustomerTestDataSources), nameof(CustomerTestDataSources.AddData))]
    [DisplayName("Customer add, valid data: $testCase")]
    public async Task CustomerAdd_Valid_Success(TestCaseModel<CustomerModel> testCase)
    {
        CustomerModel customerModel = testCase.Data;
        CustomerAddRequest addRequest = new()
        {
            SellerId = GetSellerFromTest(customerModel.SellerEmail).Id,
            Email = customerModel.Email,
            CompanyName = customerModel.CompanyName,
            CompanyNumber = customerModel.CompanyNumber,
            Street = customerModel.Street,
            City = customerModel.City,
            State = customerModel.State,
            Phone = customerModel.Phone,
            InvoiceName = customerModel.InvoiceName
        };

        var addResponseResult = await _customerClient.Add(addRequest);
        AddResponse addResponse = addResponseResult.Match(
            customer => { return customer; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(addResponse.Id).IsNotNull();

        var getResponseResult = await _customerClient.Get(addResponse.Id);
        CustomerResponse? getResponse = getResponseResult.Match(
            customer => { return customer; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(getResponse).IsNotNull();
        await Assert.That(getResponse!.Id).IsEqualTo(addResponse.Id);
        await Assert.That(getResponse!.SellerId).IsEqualTo(addRequest.SellerId);
        await Assert.That(getResponse!.Email).IsEqualTo(addRequest.Email);
        await Assert.That(getResponse!.CompanyName).IsEqualTo(addRequest.CompanyName);
        await Assert.That(getResponse!.Street).IsEqualTo(addRequest.Street);
        await Assert.That(getResponse!.City).IsEqualTo(addRequest.City);
        await Assert.That(getResponse!.State).IsEqualTo(addRequest.State);
        await Assert.That(getResponse!.Phone).IsEqualTo(addRequest.Phone);
        await Assert.That(getResponse!.InvoiceName).IsEqualTo(addRequest.InvoiceName);

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
            InvoiceName = getResponse.InvoiceName,
            InvoiceNumber = getResponse.InvoiceNumber
        };
        TestContext.Current!.ObjectBag.Add(customer!.Email, customer);
    }

    [Test]
    [MethodDataSource(typeof(CustomerTestDataSources), nameof(CustomerTestDataSources.AddDataInvalid))]
    [DependsOn(nameof(CustomerAdd_Valid_Success), [typeof(TestCaseModel<CustomerModel>)])]
    [DisplayName("Customer add, invalid data: $testCase")]
    public async Task CustomerAdd_InValid_Fail(TestCaseModel<CustomerModel> testCase)
    {
        CustomerModel customerModel = testCase.Data;
        CustomerAddRequest addRequest = new()
        {
            SellerId = string.IsNullOrEmpty(customerModel.SellerEmail) ? Guid.NewGuid() : GetSellerFromTest(customerModel.SellerEmail).Id,
            Email = customerModel.Email,
            CompanyName = customerModel.CompanyName,
            CompanyNumber = customerModel.CompanyNumber,
            Street = customerModel.Street,
            City = customerModel.City,
            State = customerModel.State,
            Phone = customerModel.Phone,
            InvoiceName = customerModel.InvoiceName
        };

        var requestResult = await _customerClient.Add(addRequest);
        ErrorModel error = requestResult.Match(
            customer => { throw new Exception(customer.ToString()); },
            error => { return error; }
        );

        await testCase.Error!.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(CustomerAdd_Valid_Success), [typeof(TestCaseModel<CustomerModel>)])]
    public async Task CustomerDelete_Valid_Success()
    {
        CustomerAddRequest addRequest = new()
        {
            SellerId = GetSellerFromTest(SellerTestDataSources.Emails().First()).Id,
            Email = "delete@me.com",
            CompanyName = "Super deleters",
            CompanyNumber = "CN000000",
            Street = "Unknown",
            City = "Empty",
            State = "Void",
            Phone = "+000000000",
            InvoiceName = "Hole"
        };

        var addResponseResult = await _customerClient.Add(addRequest);
        AddResponse addResponse = addResponseResult.Match(
            customer => { return customer; },
            error => { throw new Exception(error.ToString()); }
        );

        var deleteResponseResult = await _customerClient.Delete(addResponse.Id);
        bool deleteResponse = deleteResponseResult.Match(
            customer => { return customer; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(deleteResponse).IsTrue();

        var getResponseResult = await _customerClient.Get(addResponse.Id);
        ErrorModel error = getResponseResult.Match(
            seller => { throw new Exception(seller!.ToString()); },
            error => { return error; }
        );
        ErrorModel expectedError = new("Entity not found", $"Invoice customer:{addResponse.Id} not found", HttpStatusCode.NotFound);
        await expectedError.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(CustomerDelete_Valid_Success))]
    public async Task CustomerDelete_InValid_Fail()
    {
        Guid id = Guid.NewGuid();

        var deleteResponseResult = await _customerClient.Delete(id);
        ErrorModel error = deleteResponseResult.Match(
            customer => { throw new Exception(customer.ToString()); },
            error => { return error; }
        );

        ErrorModel expectedError = new("Entity not found", $"Invoice customer:{id} not found", HttpStatusCode.NotFound);
        await expectedError.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(ItemDelete_Valid_Success))]
    [DependsOn(nameof(ItemAdd_Valid_Success))]
    public async Task CustomerDelete_WhenHaveSeller_Fail()
    {
        Guid id = GetCustomerFromTest(CustomerTestDataSources.Emails().First()).Id;

        var deleteResponseResult = await _customerClient.Delete(id);
        ErrorModel error = deleteResponseResult.Match(
            customer => { throw new Exception(customer.ToString()); },
            error => { return error; }
        );

        ErrorModel expectedError = new("Validation failure", $"Can not delete (please clear all dependants) or update (item not found): invoice_customers_id", HttpStatusCode.BadRequest);
        await expectedError.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(CustomerDelete_Valid_Success))]
    public async Task CustomerGetAll_Valid_Success()
    {
        var listResponseResult = await _customerClient.Get();
        CustomerListResponse listResponse = listResponseResult.Match(
            customer => { return customer; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(listResponse.Customers)
            .IsNotNull()
            .And.HasCount().EqualTo(2);
    }

    [Test]
    [MethodDataSource(typeof(CustomerTestDataSources), nameof(CustomerTestDataSources.Emails))]
    [DependsOn(nameof(CustomerAdd_Valid_Success), [typeof(CustomerAddRequest)])]
    [DisplayName("Customer update, valid data: $email")]
    public async Task CustomerUpdate_Valid_Success(string email)
    {
        CustomerModel customerModel = GetCustomerFromTest(email);

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
            InvoiceName = customerModel.InvoiceName + "_new",
            InvoiceNumber = customerModel.InvoiceNumber + 99
        };

        var updateResponseResult = await _customerClient.Update(updateRequest);
        bool updateResponse = updateResponseResult.Match(
            customer => { return customer; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(updateResponse).IsTrue();

        var getResponseResult = await _customerClient.Get(updateRequest.Id);
        CustomerResponse? getResponse = getResponseResult.Match(
            customer => { return customer; },
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
        await Assert.That(getResponse!.InvoiceName).IsEqualTo(updateRequest.InvoiceName);
        await Assert.That(getResponse!.InvoiceNumber).IsEquivalentTo(updateRequest.InvoiceNumber);
    }

    [Test]
    [MethodDataSource(typeof(CustomerTestDataSources), nameof(CustomerTestDataSources.UpdateDataInvalid))]
    [DependsOn(nameof(CustomerAdd_Valid_Success), [typeof(TestCaseModel<CustomerModel>)])]
    [DisplayName("Customer update, invalid data: $testCase")]
    public async Task CustomerUpdate_InValid_Fail(TestCaseModel<CustomerModel> testCase)
    {
        CustomerModel customer = testCase.Data;
        CustomerUpdateRequest updateRequest = new()
        {
            Id = customer.Id == default ? GetCustomerFromTest(CustomerTestDataSources.Emails().First()).Id : customer.Id,
            Email = customer.Email,
            CompanyName = customer.CompanyName,
            CompanyNumber = customer.CompanyNumber,
            Street = customer.Street,
            City = customer.City,
            State = customer.State,
            Phone = customer.Phone,
            InvoiceName = customer.InvoiceName,
            InvoiceNumber = customer.InvoiceNumber
        };

        var updateResponseResult = await _customerClient.Update(updateRequest);
        ErrorModel error = updateResponseResult.Match(
            customer => { throw new Exception(customer.ToString()); },
            error => { return error; }
        );

        if (customer.Id != default)
            testCase.Error!.ExtendedMessage = $"Invoice customer:{updateRequest.Id} not found";

        await testCase.CheckErrors(error);
    }

    public static async Task Customer_Delete_All()
    {
        var listResponseResult = await _customerClient.Get();
        CustomerListResponse listResponse = listResponseResult.Match(
            customer => { return customer; },
            error => { throw new Exception(error.ToString()); }
        );

        foreach (var invoice in listResponse.Customers)
        {
            var deleteResponseResult = await _customerClient.Delete(invoice.Id);
            bool deleteResponse = deleteResponseResult.Match(
                customer => { return customer; },
                error => { throw new Exception(error.ToString()); }
            );

            await Assert.That(deleteResponse).IsTrue();
        }
    }

    [Test]
    [DependsOn(nameof(ItemDelete_AfterAll_Success))]
    public static async Task CustomerDelete_AfterAll_Success()
    {
        await Customer_Delete_All();
    }
}
