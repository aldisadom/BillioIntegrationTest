using BillioIntegrationTest.Helpers;
using Contracts.Requests.Item;
using Contracts.Responses;
using Contracts.Responses.Item;
using IntegrationTests.Clients;
using IntegrationTests.Models;
using System.Net;
using TUnit.Assertions.Extensions.Generic;
using TUnit.Core.Extensions;

namespace BillioIntegrationTest.Tests;

public static class ItemTestDataSources
{
    public static IEnumerable<TestCaseModel<ItemModel>> AddData()
    {
        yield return new()
        {
            TestCase = Names().First(),
            Data = new()
            {
                CustomerEmail = CustomerTestDataSources.Emails().First(),
                Name = Names().First(),
                Price = 123.1M,
                Quantity = 9999
            }
        };
        yield return new()
        {
            TestCase = Names().ToArray()[1],
            Data = new()
            {
                CustomerEmail = CustomerTestDataSources.Emails().ToArray()[1],
                Name = Names().ToArray()[1],
                Price = 666M,
                Quantity = 1234,
            }
        };
    }
    public static IEnumerable<TestCaseModel<ItemModel>> AddDataInvalid()
    {
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
                CustomerEmail = CustomerTestDataSources.Emails().First(),
                Price = 111,
                Quantity = 9999
            }
        };
        yield return new()
        {
            TestCase = "No price",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Price should be more than zero"
            },
            Data = new()
            {
                CustomerEmail = CustomerTestDataSources.Emails().First(),
                Name = Names().First(),
                Quantity = 9999
            }
        };
        yield return new()
        {
            TestCase = "Price is 0",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Price should be more than zero"
            },
            Data = new()
            {
                CustomerEmail = CustomerTestDataSources.Emails().First(),
                Name = Names().First(),
                Quantity = 9999,
                Price = 0
            }
        };
        yield return new()
        {
            TestCase = "No quantity",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify quantity"
            },
            Data = new()
            {
                CustomerEmail = CustomerTestDataSources.Emails().First(),
                Name = Names().First(),
                Price = 123.1M
            }
        };
        yield return new()
        {
            TestCase = "Quantity -2",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Quantity can not be negative, except -1 quantity not used"
            },
            Data = new()
            {
                CustomerEmail = CustomerTestDataSources.Emails().First(),
                Name = Names().First(),
                Price = 123.1M,
                Quantity = -2,
            }
        };
        yield return new()
        {
            TestCase = "No customer",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Key does not exist: invoice_customers_id"
            },
            Data = new()
            {
                Name = Names().First(),
                Price = 123.1M,
                Quantity = 9999
            }
        };
    }

    public static IEnumerable<TestCaseModel<ItemModel>> UpdateDataInvalid()
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
                Name = Names().First(),
                Price = 123.1M,
                Quantity = 9999
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
                Id = default,
                Price = 123.1M,
                Quantity = 9999
            }
        };
        yield return new()
        {
            TestCase = "No price",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Price should be more than zero"
            },
            Data = new()
            {
                Id = default,
                Name = Names().First(),
                Quantity = 9999
            }
        };
        yield return new()
        {
            TestCase = "Price = 0",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Price should be more than zero"
            },
            Data = new()
            {
                Id = default,
                Name = Names().First(),
                Quantity = 9999,
                Price = 0
            }
        };
        yield return new()
        {
            TestCase = "No quantity",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify quantity"
            },
            Data = new()
            {
                Id = default,
                Name = Names().First(),
                Price = 123.1M
            }
        };
        yield return new()
        {
            TestCase = "Quantity = -2",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Quantity can not be negative, except -1 quantity not used"
            },
            Data = new()
            {
                Id = default,
                Name = Names().First(),
                Price = 123.1M,
                Quantity = -2
            }
        };
    }

    public static IEnumerable<string> Names()
    {
        yield return "Back stab";
        yield return "Remove identity";
    }

}

public partial class Tests
{
    private static readonly ItemClient _itemClient = new();
    public static ItemModel GetItemFromTest(string email)
    {
        return TestDataHelper.GetData<ItemModel>(email, nameof(ItemAdd_Valid_Success));
    }

    [Test]
    [DependsOn(nameof(CustomerDelete_Valid_Success))]
    public async Task Item_BeforeTests_DBEmpty()
    {
        var requestResult = await _itemClient.Get();
        ItemListResponse response = requestResult.Match(
            item => { return item; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(response.Items)
            .IsNotNull()
            .And.IsEmpty();
    }

    [Test]
    [DependsOn(nameof(Item_BeforeTests_DBEmpty))]
    [MethodDataSource(typeof(ItemTestDataSources), nameof(ItemTestDataSources.AddData))]
    [DisplayName("Item add, valid data: $testCase")]
    public async Task ItemAdd_Valid_Success(TestCaseModel<ItemModel> testCase)
    {
        ItemModel itemModel = testCase.Data;
        ItemAddRequest addRequest = new()
        {
            CustomerId = GetCustomerFromTest(itemModel.CustomerEmail).Id,
            Name = itemModel.Name,
            Quantity = itemModel.Quantity,
            Price = itemModel.Price
        };

        var addResponseResult = await _itemClient.Add(addRequest);
        AddResponse addResponse = addResponseResult.Match(
            item => { return item; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(addResponse.Id).IsNotNull();

        var getResponseResult = await _itemClient.Get(addResponse.Id);
        ItemResponse? getResponse = getResponseResult.Match(
            item => { return item; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(getResponse).IsNotNull();
        await Assert.That(getResponse!.Id).IsEqualTo(addResponse.Id);
        await Assert.That(getResponse!.CustomerId).IsEqualTo(addRequest.CustomerId);
        await Assert.That(getResponse!.Name).IsEqualTo(addRequest.Name);
        await Assert.That(getResponse!.Price).IsEquivalentTo(addRequest.Price);
        await Assert.That(getResponse!.Quantity).IsEquivalentTo(addRequest.Quantity);

        ItemModel item = new()
        {
            CustomerEmail = itemModel.CustomerEmail,
            Id = getResponse.Id,
            Name = getResponse.Name,
            Price = getResponse.Price,
            Quantity = getResponse.Quantity
        };
        TestContext.Current!.ObjectBag.Add(item!.Name, item);
    }

    [Test]
    [MethodDataSource(typeof(ItemTestDataSources), nameof(ItemTestDataSources.AddDataInvalid))]
    [DependsOn(nameof(ItemAdd_Valid_Success), [typeof(TestCaseModel<ItemModel>)])]
    [DisplayName("Item add, invalid data: $testCase")]
    public async Task ItemAdd_InValid_Fail(TestCaseModel<ItemModel> testCase)
    {
        ItemModel itemModel = testCase.Data;
        ItemAddRequest addRequest = new()
        {
            CustomerId = string.IsNullOrEmpty(itemModel.CustomerEmail) ? Guid.NewGuid() : GetCustomerFromTest(itemModel.CustomerEmail).Id,
            Name = itemModel.Name,
            Price = itemModel.Price,
            Quantity = itemModel.Quantity
        };

        var requestResult = await _itemClient.Add(addRequest);
        ErrorModel error = requestResult.Match(
            item => { throw new Exception(item.ToString()); },
            error => { return error; }
        );

        await testCase.Error!.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(ItemAdd_Valid_Success), [typeof(TestCaseModel<ItemModel>)])]
    public async Task ItemDelete_Valid_Success()
    {
        ItemAddRequest addRequest = new()
        {
            CustomerId = GetCustomerFromTest(CustomerTestDataSources.Emails().First()).Id,
            Name = "delete@me.com",
            Price = 999999999999999,
            Quantity = 1
        };

        var addResponseResult = await _itemClient.Add(addRequest);
        AddResponse addResponse = addResponseResult.Match(
            item => { return item; },
            error => { throw new Exception(error.ToString()); }
        );

        var deleteResponseResult = await _itemClient.Delete(addResponse.Id);
        bool deleteResponse = deleteResponseResult.Match(
            item => { return item; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(deleteResponse).IsTrue();

        var getResponseResult = await _itemClient.Get(addResponse.Id);
        ErrorModel error = getResponseResult.Match(
            customer => { throw new Exception(customer!.ToString()); },
            error => { return error; }
        );
        ErrorModel expectedError = new("Entity not found", $"Invoice item:{addResponse.Id} not found", HttpStatusCode.NotFound);
        await expectedError.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(ItemDelete_Valid_Success))]
    public async Task ItemDelete_InValid_Fail()
    {
        Guid id = Guid.NewGuid();

        var deleteResponseResult = await _itemClient.Delete(id);
        ErrorModel error = deleteResponseResult.Match(
            item => { throw new Exception(item.ToString()); },
            error => { return error; }
        );

        ErrorModel expectedError = new("Entity not found", $"Invoice item:{id} not found", HttpStatusCode.NotFound);
        await expectedError.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(ItemDelete_Valid_Success))]
    public async Task ItemGetAll_Valid_Success()
    {
        var listResponseResult = await _itemClient.Get();
        ItemListResponse listResponse = listResponseResult.Match(
            item => { return item; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(listResponse.Items)
            .IsNotNull()
            .And.HasCount().EqualTo(2);
    }

    [Test]
    [MethodDataSource(typeof(ItemTestDataSources), nameof(ItemTestDataSources.Names))]
    [DependsOn(nameof(ItemAdd_Valid_Success), [typeof(ItemAddRequest)])]
    [DisplayName("Item update, valid data: $name")]
    public async Task ItemUpdate_Valid_Success(string name)
    {
        ItemModel itemModel = GetItemFromTest(name);

        ItemUpdateRequest updateRequest = new()
        {
            Id = itemModel.Id,
            Name = itemModel.Name + "_new",
            Quantity = itemModel.Quantity + 999,
            Price = itemModel.Price + 666
        };

        var updateResponseResult = await _itemClient.Update(updateRequest);
        bool updateResponse = updateResponseResult.Match(
            item => { return item; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(updateResponse).IsTrue();

        var getResponseResult = await _itemClient.Get(updateRequest.Id);
        ItemResponse? getResponse = getResponseResult.Match(
            item => { return item; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(getResponse).IsNotNull();
        await Assert.That(getResponse!.Id).IsEqualTo(updateRequest.Id);
        await Assert.That(getResponse!.Name).IsEqualTo(updateRequest.Name);
        await Assert.That(getResponse!.Quantity).IsEquivalentTo(updateRequest.Quantity);
        await Assert.That(getResponse!.Price).IsEquivalentTo(updateRequest.Price);
    }

    [Test]
    [MethodDataSource(typeof(ItemTestDataSources), nameof(ItemTestDataSources.UpdateDataInvalid))]
    [DependsOn(nameof(ItemAdd_Valid_Success), [typeof(TestCaseModel<ItemModel>)])]
    [DisplayName("Item update, invalid data: $testCase")]
    public async Task ItemUpdate_InValid_Fail(TestCaseModel<ItemModel> testCase)
    {
        ItemModel item = testCase.Data;
        ItemUpdateRequest updateRequest = new()
        {
            Id = item.Id == default ? GetItemFromTest(ItemTestDataSources.Names().First()).Id : item.Id,
            Name = item.Name,
            Quantity = item.Quantity,
            Price = item.Price
        };

        var updateResponseResult = await _itemClient.Update(updateRequest);
        ErrorModel error = updateResponseResult.Match(
            item => { throw new Exception(item.ToString()); },
            error => { return error; }
        );

        if (item.Id != default)
            testCase.Error!.ExtendedMessage = $"Invoice item:{updateRequest.Id} not found";

        await testCase.CheckErrors(error);
    }

    public static async Task Item_Delete_All()
    {
        var listResponseResult = await _itemClient.Get();
        ItemListResponse listResponse = listResponseResult.Match(
            item => { return item; },
            error => { throw new Exception(error.ToString()); }
        );

        foreach (var invoice in listResponse.Items)
        {
            var deleteResponseResult = await _itemClient.Delete(invoice.Id);
            bool deleteResponse = deleteResponseResult.Match(
                item => { return item; },
                error => { throw new Exception(error.ToString()); }
            );

            await Assert.That(deleteResponse).IsTrue();
        }
    }

    [Test]
    [DependsOn(nameof(InvoiceDelete_AfterAll_Success))]
    public static async Task ItemDelete_AfterAll_Success()
    {
        await Item_Delete_All();
    }
}
