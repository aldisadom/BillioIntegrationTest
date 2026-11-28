using BillerContracts;
using BillerContracts.Clients;
using BillerContracts.Clients.Interfaces;
using BillerContracts.Enums;
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
    public static IEnumerable<Func<TestCaseModel<ItemModel>>> AddData()
    {
        yield return () => new()
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
        yield return () => new()
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
    public static IEnumerable<Func<TestCaseError<ItemModel>>> AddDataInvalid()
    {
        yield return () => new()
        {
            TestName = "Name with a character the PDF can't show",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please use only characters the invoice PDF can show in Name. Can't show: 🐉 (U+1F409)"
            },
            Data = new()
            {
                CustomerEmail = CustomerTestDataSources.Emails().First(),
                Name = "Dragon 🐉",
                Price = 111,
                Quantity = 9999
            }
        };
        yield return () => new()
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
        yield return () => new()
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
        yield return () => new()
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
        yield return () => new()
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
        yield return () => new()
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
        yield return () => new()
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

    public static IEnumerable<Func<TestCaseError<ItemModel>>> UpdateDataInvalid()
    {
        yield return () => new()
        {
            TestName = "Name with a character the PDF can't show",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please use only characters the invoice PDF can show in Name. Can't show: 🐉 (U+1F409)"
            },
            Data = new()
            {
                Id = default,
                Name = "Dragon 🐉",
                Price = 123.1M,
                Quantity = 9999
            }
        };
        yield return () => new()
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
        yield return () => new()
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
        yield return () => new()
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
        yield return () => new()
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
        yield return () => new()
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
        yield return () => new()
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
        await Assert.That(response.TotalCount).IsEqualTo(0);
    }


    [Test]
    [DependsOn(nameof(Item_BeforeTests_DBEmpty))]
    public async Task ItemGet_UndefinedPageSize_Fail()
    {
        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _itemClient.Get(new ItemGetRequest { PageSize = UndefinedPageSize })))!.Error!;

        ErrorModel expectedError = new("Validation failure", UndefinedPageSizeMessage, HttpStatusCode.BadRequest);
        await expectedError.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(Item_BeforeTests_DBEmpty))]
    public async Task ItemGet_EmptyCustomerId_Fail()
    {
        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _itemClient.Get(new ItemGetRequest { CustomerId = Guid.Empty })))!.Error!;

        ErrorModel expectedError = new("Validation failure", "Please specify valid customer Id", HttpStatusCode.BadRequest);
        await expectedError.CheckErrors(error);
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
        await Assert.That(getResponse!.Version).IsEqualTo(1);

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

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _itemClient.Add(addRequest)))!.Error!;

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

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _itemClient.Get(addResponse.Id)))!.Error!;
        ErrorModel expectedError = new("Entity not found", $"Item:{addResponse.Id} not found", HttpStatusCode.NotFound);
        await expectedError.CheckErrors(error);

        // Its versions go with it
        error = (await Assert.ThrowsAsync<ClientAPIException>(() => _itemClient.GetVersions(addResponse.Id)))!.Error!;
        await expectedError.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(ItemDelete_Valid_Success))]
    public async Task ItemDelete_InValid_Fail()
    {
        Guid id = Guid.NewGuid();

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _itemClient.Delete(id)))!.Error!;

        ErrorModel expectedError = new("Entity not found", $"Item:{id} not found", HttpStatusCode.NotFound);
        await expectedError.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(ItemDelete_Valid_Success))]
    public async Task ItemGetAll_Valid_Success()
    {
        ItemListResponse listResponse = await _itemClient.Get();

        await Assert.That(listResponse.Items)
            .IsNotNull()
            .And.Count().IsEqualTo(2);
        await Assert.That(listResponse.TotalCount).IsEqualTo(2);
        await Assert.That(listResponse.Page).IsEqualTo(1);
        await Assert.That(listResponse.PageSize).IsEqualTo(PageSize.p25);
    }

    [Test]
    [DependsOn(nameof(ItemDelete_Valid_Success))]
    [DependsOn(nameof(ItemUpdate_Valid_Success), [typeof(string)])]
    public async Task ItemGetAll_SecondPage_Success()
    {
        ItemListResponse listResponse = await _itemClient.Get(new ItemGetRequest { Page = 2, PageSize = PageSize.p1 });

        await AssertItemPageOfOne(listResponse, 2);
    }

    [Test]
    [DependsOn(nameof(ItemDelete_Valid_Success))]
    [DependsOn(nameof(ItemUpdate_Valid_Success), [typeof(string)])]
    public async Task ItemGetAll_PagePastTheLast_ReturnsLastPage()
    {
        ItemListResponse listResponse = await _itemClient.Get(new ItemGetRequest { Page = int.MaxValue, PageSize = PageSize.p1 });

        await AssertItemPageOfOne(listResponse, 2);
    }

    [Test]
    [DependsOn(nameof(ItemDelete_Valid_Success))]
    [DependsOn(nameof(ItemUpdate_Valid_Success), [typeof(string)])]
    public async Task ItemGetAll_NegativePage_ReturnsFirstPage()
    {
        ItemListResponse listResponse = await _itemClient.Get(new ItemGetRequest { Page = -1, PageSize = PageSize.p1 });

        await AssertItemPageOfOne(listResponse, 1);
    }

    // The two saved items, one per page, after ItemUpdate_Valid_Success added "_new" to the name,
    // 666 to the price and 999 to the quantity
    private static async Task AssertItemPageOfOne(ItemListResponse listResponse, int expectedPage)
    {
        // Items are ordered by name: Back stab, then Remove identity
        string name = expectedPage == 1 ? ItemTestDataSources.Names().First() : ItemTestDataSources.Names().ToArray()[1];
        ItemModel expected = ItemTestDataSources.SavedItems[name];

        await Assert.That(listResponse.Items)
            .IsNotNull()
            .And.Count().IsEqualTo(1);
        ItemResponse listed = listResponse.Items[0];
        await Assert.That(listed.Id).IsEqualTo(expected.Id);
        await Assert.That(listed.CustomerId).IsEqualTo(CustomerTestDataSources.SavedCustomers[expected.CustomerEmail].Id);
        await Assert.That(listed.Name).IsEqualTo(expected.Name + "_new");
        await Assert.That(listed.Price).IsEquivalentTo(expected.Price + 666);
        await Assert.That(listed.Quantity).IsEquivalentTo(expected.Quantity + 999);
        await Assert.That(listed.Version).IsEqualTo(2);
        await Assert.That(listResponse.TotalCount).IsEqualTo(2);
        await Assert.That(listResponse.Page).IsEqualTo(expectedPage);
        await Assert.That(listResponse.PageSize).IsEqualTo(PageSize.p1);
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
            .And.Count().IsEqualTo(1);
        await Assert.That(listResponse.TotalCount).IsEqualTo(1);

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
        await Assert.That(getResponse!.Version).IsEqualTo(2);
    }

    // Runs after the tests that count every item, because it adds one of its own
    [Test]
    [DependsOn(nameof(ItemGetAll_Valid_Success))]
    [DependsOn(nameof(ItemGetAll_SecondPage_Success))]
    [DependsOn(nameof(ItemGetAll_PagePastTheLast_ReturnsLastPage))]
    [DependsOn(nameof(ItemGetAll_NegativePage_ReturnsFirstPage))]
    [DependsOn(nameof(ItemGetAllForCustomer_Valid_Success))]
    public async Task ItemVersions_Valid_Success()
    {
        ItemAddRequest addRequest = new()
        {
            CustomerId = CustomerTestDataSources.SavedCustomers[CustomerTestDataSources.Emails().First()].Id,
            Name = "Needle",
            Price = 10,
            Quantity = 1
        };
        // The API's clock may differ a little from this machine's
        DateTime before = DateTime.UtcNow.AddMinutes(-5);
        Guid id = (await _itemClient.Add(addRequest)).Id;

        // A price-only change and a name-only change each make a version; saving the same name and
        // price again, with only another quantity, makes none
        await _itemClient.Update(new ItemUpdateRequest { Id = id, Name = "Needle", Price = 15, Quantity = 1 });
        await _itemClient.Update(new ItemUpdateRequest { Id = id, Name = "Needle, sharpened", Price = 15, Quantity = 1 });
        await _itemClient.Update(new ItemUpdateRequest { Id = id, Name = "Needle, sharpened", Price = 15, Quantity = 4 });
        DateTime after = DateTime.UtcNow.AddMinutes(5);

        ItemVersionListResponse versions = await _itemClient.GetVersions(id);

        (string Name, decimal Price)[] expected = [("Needle", 10m), ("Needle", 15m), ("Needle, sharpened", 15m)];

        await Assert.That(versions.ItemId).IsEqualTo(id);
        await Assert.That(versions.Versions).Count().IsEqualTo(expected.Length);
        for (int i = 0; i < expected.Length; i++)
        {
            // Oldest first
            await Assert.That(versions.Versions[i].Version).IsEqualTo(i + 1);
            await Assert.That(versions.Versions[i].Name).IsEqualTo(expected[i].Name);
            await Assert.That(versions.Versions[i].Price).IsEquivalentTo(expected[i].Price);
            await Assert.That(versions.Versions[i].ChangedDate.ToUniversalTime()).IsGreaterThanOrEqualTo(before)
                .And.IsLessThanOrEqualTo(after);
            if (i > 0)
                // Each update is its own call, so each version has its own, later time
                await Assert.That(versions.Versions[i].ChangedDate).IsGreaterThan(versions.Versions[i - 1].ChangedDate);
        }

        ItemResponse latest = await _itemClient.Get(id);

        await Assert.That(latest.Id).IsEqualTo(id);
        await Assert.That(latest.CustomerId).IsEqualTo(addRequest.CustomerId);
        await Assert.That(latest.Name).IsEqualTo("Needle, sharpened");
        await Assert.That(latest.Price).IsEquivalentTo(15m);
        await Assert.That(latest.Quantity).IsEquivalentTo(4m);
        await Assert.That(latest.Version).IsEqualTo(3);

        // An old version has that version's name and price, and the item's current quantity
        ItemResponse first = await _itemClient.Get(id, 1);

        await Assert.That(first.Id).IsEqualTo(id);
        await Assert.That(first.CustomerId).IsEqualTo(addRequest.CustomerId);
        await Assert.That(first.Name).IsEqualTo("Needle");
        await Assert.That(first.Price).IsEquivalentTo(10m);
        await Assert.That(first.Quantity).IsEquivalentTo(4m);
        await Assert.That(first.Version).IsEqualTo(1);

        await _itemClient.Delete(id);
    }

    [Test]
    [DependsOn(nameof(ItemAdd_Valid_Success), [typeof(TestCaseModel<ItemModel>)])]
    public async Task ItemGetVersion_NotFound_Fail()
    {
        Guid id = GetItemFromTest(ItemTestDataSources.Names().First()).Id;

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _itemClient.Get(id, 99)))!.Error!;

        ErrorModel expectedError = new("Entity not found", $"Item:{id} version 99 not found", HttpStatusCode.NotFound);
        await expectedError.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(Item_BeforeTests_DBEmpty))]
    public async Task ItemGetVersions_NotFound_Fail()
    {
        Guid id = Guid.NewGuid();

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _itemClient.GetVersions(id)))!.Error!;

        ErrorModel expectedError = new("Entity not found", $"Item:{id} not found", HttpStatusCode.NotFound);
        await expectedError.CheckErrors(error);
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

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _itemClient.Update(updateRequest)))!.Error!;

        if (item.Id != default)
            testCase.Error!.ExtendedMessage = $"Item:{updateRequest.Id} not found";

        await testCase.CheckErrors(error);
    }

    public static async Task Item_Delete_All()
    {
        await DeleteAll.Run("Item", async () => (await _itemClient.Get()).Items.Select(item => item.Id), _itemClient.Delete);
    }
}
