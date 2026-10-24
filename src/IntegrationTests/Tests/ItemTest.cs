using BillerContracts;
using BillerContracts.Clients.Interfaces;
using BillerContracts.Requests.Item;
using BillerContracts.Responses;
using BillerContracts.Responses.Item;
using IntegrationTests.ApiClients;
using IntegrationTests.Models;
using System.Collections.Concurrent;
using System.Net;

namespace BillioIntegrationTest.Tests;

public static class ItemTestDataSources
{

    public static ConcurrentDictionary<string, ItemModel> SavedItems = new();
    public static IEnumerable<TestCaseModel<ItemModel>> AddData()
    {
        yield return new()
        {
            TestName = Names().First(),
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
            TestName = Names().ToArray()[1],
            Data = new()
            {
                CustomerEmail = CustomerTestDataSources.Emails().ToArray()[1],
                Name = Names().ToArray()[1],
                Price = 666M,
                Quantity = 1234,
            }
        };
    }
    public static IEnumerable<TestCaseError<ItemModel>> AddDataInvalid()
    {
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
                CustomerEmail = CustomerTestDataSources.Emails().First(),
                Price = 111,
                Quantity = 9999
            }
        };
        yield return new()
        {
            TestName = "No price",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please provide price that must be > 0"
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
            TestName = "Price is 0",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please provide price that must be > 0"
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
            TestName = "No quantity",
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
            TestName = "Quantity -2",
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
            TestName = "No customer",
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

    public static IEnumerable<TestCaseError<ItemModel>> UpdateDataInvalid()
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
                Name = Names().First(),
                Price = 123.1M,
                Quantity = 9999
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
                Id = default,
                Price = 123.1M,
                Quantity = 9999
            }
        };
        yield return new()
        {
            TestName = "No price",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please provide price that must be > 0"
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
            TestName = "Price = 0",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please provide price that must be > 0"
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
            TestName = "No quantity",
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
            TestName = "Quantity = -2",
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
    private static readonly IItemClient _itemClient = TestClients.Item;
    public static ItemModel GetItemFromTest(string name)
    {
        return ItemTestDataSources.SavedItems[name];
    }

    [Test]
    [DependsOn(nameof(CustomerDelete_Valid_Success))]
    public async Task Item_BeforeTests_DBEmpty()
    {
        ItemListResponse response = await _itemClient.Get();

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
            CustomerId = CustomerTestDataSources.SavedCustomers[itemModel.CustomerEmail].Id,
            Name = itemModel.Name,
            Quantity = itemModel.Quantity,
            Price = itemModel.Price
        };

        AddResponse addResponse = await _itemClient.Add(addRequest);

        await Assert.That(addResponse.Id).IsNotDefault();

        ItemResponse getResponse = await _itemClient.Get(addResponse.Id);

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

        if (!ItemTestDataSources.SavedItems.TryAdd(addRequest.Name, item))
            throw new Exception($"Item with name:{addRequest.Name} can not add to test data");
    }

    [Test]
    [MethodDataSource(typeof(ItemTestDataSources), nameof(ItemTestDataSources.AddDataInvalid))]
    [DependsOn(nameof(ItemAdd_Valid_Success), [typeof(TestCaseModel<ItemModel>)])]
    [DisplayName("Item add, invalid data: $testCase")]
    public async Task ItemAdd_InValid_Fail(TestCaseError<ItemModel> testCase)
    {
        ItemModel itemModel = testCase.Data;
        ItemAddRequest addRequest = new()
        {
            CustomerId = string.IsNullOrEmpty(itemModel.CustomerEmail)
                ? Guid.NewGuid()
                : CustomerTestDataSources.SavedCustomers[itemModel.CustomerEmail].Id,
            Name = itemModel.Name,
            Price = itemModel.Price,
            Quantity = itemModel.Quantity
        };

        ErrorModel error = (await Assert.ThrowsAsync<ApiErrorException>(() => _itemClient.Add(addRequest)))!.Error;

        await testCase.Error!.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(ItemAdd_Valid_Success), [typeof(TestCaseModel<ItemModel>)])]
    public async Task ItemDelete_Valid_Success()
    {
        ItemAddRequest addRequest = new()
        {
            CustomerId = CustomerTestDataSources.SavedCustomers[CustomerTestDataSources.Emails().First()].Id,
            Name = "delete@me.com",
            Price = 999999999999999,
            Quantity = 1
        };

        AddResponse addResponse = await _itemClient.Add(addRequest);

        await _itemClient.Delete(addResponse.Id);

        ErrorModel error = (await Assert.ThrowsAsync<ApiErrorException>(() => _itemClient.Get(addResponse.Id)))!.Error;
        ErrorModel expectedError = new("Entity not found", $"Invoice item:{addResponse.Id} not found", HttpStatusCode.NotFound);
        await expectedError.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(ItemDelete_Valid_Success))]
    public async Task ItemDelete_InValid_Fail()
    {
        Guid id = Guid.NewGuid();

        ErrorModel error = (await Assert.ThrowsAsync<ApiErrorException>(() => _itemClient.Delete(id)))!.Error;

        ErrorModel expectedError = new("Entity not found", $"Invoice item:{id} not found", HttpStatusCode.NotFound);
        await expectedError.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(ItemDelete_Valid_Success))]
    public async Task ItemGetAll_Valid_Success()
    {
        ItemListResponse listResponse = await _itemClient.Get();

        await Assert.That(listResponse.Items)
            .IsNotNull()
            .And.HasCount().EqualTo(2);
    }

    [Test]
    [DependsOn(nameof(ItemDelete_Valid_Success))]
    public async Task ItemGetAllForCustomer_Valid_Success()
    {
        var request = new ItemGetRequest()
        {
            CustomerId = CustomerTestDataSources.SavedCustomers[CustomerTestDataSources.Emails().First()].Id
        };

        ItemListResponse listResponse = await _itemClient.Get(request);

        await Assert.That(listResponse.Items)
            .IsNotNull()
            .And.HasCount().EqualTo(1);

        await Assert.That(listResponse.Items[0].CustomerId).IsEquivalentTo(request.CustomerId!.Value);
    }

    [Test]
    [MethodDataSource(typeof(ItemTestDataSources), nameof(ItemTestDataSources.Names))]
    [DependsOn(nameof(ItemAdd_Valid_Success), [typeof(TestCaseModel<ItemModel>)])]
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

        await _itemClient.Update(updateRequest);

        ItemResponse getResponse = await _itemClient.Get(updateRequest.Id);

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
    public async Task ItemUpdate_InValid_Fail(TestCaseError<ItemModel> testCase)
    {
        ItemModel item = testCase.Data;
        ItemUpdateRequest updateRequest = new()
        {
            Id = item.Id == default ? GetItemFromTest(ItemTestDataSources.Names().First()).Id : item.Id,
            Name = item.Name,
            Quantity = item.Quantity,
            Price = item.Price
        };

        ErrorModel error = (await Assert.ThrowsAsync<ApiErrorException>(() => _itemClient.Update(updateRequest)))!.Error;

        if (item.Id != default)
            testCase.Error!.ExtendedMessage = $"Invoice item:{updateRequest.Id} not found";

        await testCase.CheckErrors(error);
    }

    public static async Task Item_Delete_All()
    {
        await DeleteAll.Run("Item", async () => (await _itemClient.Get()).Items.Select(item => item.Id), _itemClient.Delete);
    }
}
