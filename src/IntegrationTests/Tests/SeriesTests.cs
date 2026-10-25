using BillerContracts;
using BillerContracts.Clients;
using BillerContracts.Clients.Interfaces;
using BillerContracts.Requests.Series;
using BillerContracts.Responses;
using BillerContracts.Responses.Series;
using IntegrationTests.ApiClients;
using IntegrationTests.Models;
using System.Collections.Concurrent;
using System.Net;

namespace BillioIntegrationTest.Tests;

public static class SeriesTestDataSources
{
    public static ConcurrentDictionary<string, SeriesModel> SavedSeries = new();

    public static IEnumerable<TestCaseModel<SeriesModel>> AddData()
    {
        yield return new()
        {
            TestName = "LL",
            Data = new()
            {
                SellerEmail = SellerTestDataSources.Emails().First(),
                Name = "LL"
            }
        };
        yield return new()
        {
            TestName = "LX",
            Data = new()
            {
                SellerEmail = SellerTestDataSources.Emails().First(),
                Name = "LX"
            }
        };
        yield return new()
        {
            TestName = "FF",
            Data = new()
            {
                SellerEmail = SellerTestDataSources.Emails().ToArray()[1],
                Name = "FF"
            }
        };
        yield return new()
        {
            TestName = "FX",
            Data = new()
            {
                SellerEmail = SellerTestDataSources.Emails().ToArray()[1],
                Name = "FX"
            }
        };
    }

    // ExtendedMessage "Change me" is filled in by the test, because it holds ids made during the run.
    public static IEnumerable<TestCaseError<SeriesModel>> AddDataInvalid()
    {
        yield return new()
        {
            TestName = "No seller id",
            Error = new("Validation failure", "Please specify seller Id", HttpStatusCode.BadRequest),
            Data = new() { Name = "NS" }
        };
        yield return new()
        {
            TestName = "Unknown seller",
            Error = new("Entity not found", "Change me", HttpStatusCode.NotFound),
            Data = new() { SellerEmail = "unknown", Name = "US" }
        };
        yield return new()
        {
            TestName = "No name",
            Error = new("Validation failure", "Please specify name", HttpStatusCode.BadRequest),
            Data = new() { SellerEmail = SellerTestDataSources.Emails().First() }
        };
        yield return new()
        {
            TestName = "Name longer than 10 characters",
            Error = new("Validation failure", "Please specify name of at most 10 characters", HttpStatusCode.BadRequest),
            Data = new() { SellerEmail = SellerTestDataSources.Emails().First(), Name = "ABCDEFGHIJK" }
        };
        yield return new()
        {
            TestName = "Name already used by the seller",
            Error = new("Validation failure", "Change me", HttpStatusCode.BadRequest),
            Data = new() { SellerEmail = SellerTestDataSources.Emails().First(), Name = "LL" }
        };
    }

    public static IEnumerable<TestCaseError<SeriesModel>> UpdateDataInvalid()
    {
        yield return new()
        {
            TestName = "Random id",
            Error = new("Entity not found", "Change me", HttpStatusCode.NotFound),
            Data = new() { Id = Guid.NewGuid(), Name = "RI", NextNumber = 1 }
        };
        yield return new()
        {
            TestName = "No name",
            Error = new("Validation failure", "Please specify name", HttpStatusCode.BadRequest),
            Data = new() { NextNumber = 1 }
        };
        yield return new()
        {
            TestName = "Name longer than 10 characters",
            Error = new("Validation failure", "Please specify name of at most 10 characters", HttpStatusCode.BadRequest),
            Data = new() { Name = "ABCDEFGHIJK", NextNumber = 1 }
        };
        yield return new()
        {
            TestName = "Next number 0",
            Error = new("Validation failure", "Please specify next number that must be > 0", HttpStatusCode.BadRequest),
            Data = new() { Name = "LX", NextNumber = 0 }
        };
        yield return new()
        {
            TestName = "Name used by another series of the seller",
            Error = new("Validation failure", "Change me", HttpStatusCode.BadRequest),
            Data = new() { Name = "LL", NextNumber = 1 }
        };
    }
}

public partial class Tests
{
    private static readonly ISeriesClient _seriesClient = TestClients.Series;

    [Test]
    [DependsOn(nameof(SellerDelete_Valid_Success))]
    public async Task Series_BeforeTests_DBEmpty()
    {
        SeriesListResponse response = await _seriesClient.Get();

        await Assert.That(response.Series)
            .IsNotNull()
            .And.IsEmpty();
        await Assert.That(response.TotalCount).IsEqualTo(0);
    }

    [Test]
    [DependsOn(nameof(Series_BeforeTests_DBEmpty))]
    [MethodDataSource(typeof(SeriesTestDataSources), nameof(SeriesTestDataSources.AddData))]
    [DisplayName("Series add, valid data: $testCase")]
    public async Task SeriesAdd_Valid_Success(TestCaseModel<SeriesModel> testCase)
    {
        SeriesModel seriesModel = testCase.Data;
        SeriesAddRequest addRequest = new()
        {
            SellerId = SellerTestDataSources.SavedSellers[seriesModel.SellerEmail].Id,
            Name = seriesModel.Name
        };

        AddResponse addResponse = await _seriesClient.Add(addRequest);

        await Assert.That(addResponse.Id).IsNotDefault();

        SeriesResponse getResponse = await _seriesClient.Get(addResponse.Id);

        await Assert.That(getResponse).IsNotNull();
        await Assert.That(getResponse!.Id).IsEqualTo(addResponse.Id);
        await Assert.That(getResponse!.SellerId).IsEqualTo(addRequest.SellerId);
        await Assert.That(getResponse!.Name).IsEqualTo(addRequest.Name);
        await Assert.That(getResponse!.NextNumber).IsEqualTo(1);

        SeriesModel series = new()
        {
            Id = getResponse.Id,
            SellerEmail = seriesModel.SellerEmail,
            Name = getResponse.Name,
            NextNumber = getResponse.NextNumber
        };

        if (!SeriesTestDataSources.SavedSeries.TryAdd(series.Name, series))
            throw new Exception($"Series with name:{series.Name} can not add to test data");
    }

    [Test]
    [MethodDataSource(typeof(SeriesTestDataSources), nameof(SeriesTestDataSources.AddDataInvalid))]
    [DependsOn(nameof(SeriesAdd_Valid_Success), [typeof(TestCaseModel<SeriesModel>)])]
    [DisplayName("Series add, invalid data: $testCase")]
    public async Task SeriesAdd_InValid_Fail(TestCaseError<SeriesModel> testCase)
    {
        SeriesModel seriesModel = testCase.Data;
        SeriesAddRequest addRequest = new()
        {
            SellerId = seriesModel.SellerEmail switch
            {
                "" => Guid.Empty,
                "unknown" => Guid.NewGuid(),
                _ => SellerTestDataSources.SavedSellers[seriesModel.SellerEmail].Id
            },
            Name = seriesModel.Name
        };

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _seriesClient.Add(addRequest)))!.Error!;

        if (testCase.Error.ExtendedMessage == "Change me")
            testCase.Error.ExtendedMessage = testCase.Error.StatusCode == HttpStatusCode.NotFound
                ? $"Invoice seller:{addRequest.SellerId} not found"
                : $"Series {addRequest.Name} already exists for seller id {addRequest.SellerId}";

        await testCase.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(SeriesAdd_Valid_Success), [typeof(TestCaseModel<SeriesModel>)])]
    public async Task SeriesUpdate_Valid_Success()
    {
        SeriesAddRequest addRequest = new()
        {
            SellerId = SellerTestDataSources.SavedSellers[SellerTestDataSources.Emails().ToArray()[1]].Id,
            Name = "UPD"
        };
        AddResponse addResponse = await _seriesClient.Add(addRequest);

        SeriesUpdateRequest updateRequest = new()
        {
            Id = addResponse.Id,
            Name = "UPD_new",
            NextNumber = 7
        };

        await _seriesClient.Update(updateRequest);

        SeriesResponse getResponse = await _seriesClient.Get(addResponse.Id);

        await Assert.That(getResponse).IsNotNull();
        await Assert.That(getResponse!.Id).IsEqualTo(updateRequest.Id);
        await Assert.That(getResponse!.SellerId).IsEqualTo(addRequest.SellerId);
        await Assert.That(getResponse!.Name).IsEqualTo(updateRequest.Name);
        await Assert.That(getResponse!.NextNumber).IsEqualTo(updateRequest.NextNumber);

        await _seriesClient.Delete(addResponse.Id);
    }

    [Test]
    [MethodDataSource(typeof(SeriesTestDataSources), nameof(SeriesTestDataSources.UpdateDataInvalid))]
    [DependsOn(nameof(SeriesAdd_Valid_Success), [typeof(TestCaseModel<SeriesModel>)])]
    [DisplayName("Series update, invalid data: $testCase")]
    public async Task SeriesUpdate_InValid_Fail(TestCaseError<SeriesModel> testCase)
    {
        SeriesModel seriesModel = testCase.Data;
        SeriesModel stored = SeriesTestDataSources.SavedSeries["LX"];
        SeriesUpdateRequest updateRequest = new()
        {
            Id = seriesModel.Id == default ? stored.Id : seriesModel.Id,
            Name = seriesModel.Name,
            NextNumber = seriesModel.NextNumber!.Value
        };

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _seriesClient.Update(updateRequest)))!.Error!;

        if (testCase.Error.ExtendedMessage == "Change me")
            testCase.Error.ExtendedMessage = testCase.Error.StatusCode == HttpStatusCode.NotFound
                ? $"Invoice series:{updateRequest.Id} not found"
                : $"Series {updateRequest.Name} already exists for seller id {SellerTestDataSources.SavedSellers[stored.SellerEmail].Id}";

        await testCase.CheckErrors(error);

        SeriesResponse getResponse = await _seriesClient.Get(stored.Id);
        await Assert.That(getResponse.Name).IsEqualTo(stored.Name);
        await Assert.That(getResponse.NextNumber).IsEqualTo(stored.NextNumber!.Value);
    }

    [Test]
    [DependsOn(nameof(SeriesAdd_Valid_Success), [typeof(TestCaseModel<SeriesModel>)])]
    public async Task SeriesDelete_Valid_Success()
    {
        SeriesAddRequest addRequest = new()
        {
            SellerId = SellerTestDataSources.SavedSellers[SellerTestDataSources.Emails().ToArray()[1]].Id,
            Name = "DEL"
        };

        AddResponse addResponse = await _seriesClient.Add(addRequest);

        await _seriesClient.Delete(addResponse.Id);

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _seriesClient.Get(addResponse.Id)))!.Error!;
        ErrorModel expectedError = new("Entity not found", $"Invoice series:{addResponse.Id} not found", HttpStatusCode.NotFound);
        await expectedError.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(SeriesDelete_Valid_Success))]
    public async Task SeriesDelete_InValid_Fail()
    {
        Guid id = Guid.NewGuid();

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _seriesClient.Delete(id)))!.Error!;

        ErrorModel expectedError = new("Entity not found", $"Invoice series:{id} not found", HttpStatusCode.NotFound);
        await expectedError.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(CustomerUpdate_Valid_Success), [typeof(string)])]
    public async Task SeriesDelete_WhenUsedByCustomer_Fail()
    {
        Guid customerId = CustomerTestDataSources.SavedCustomers[CustomerTestDataSources.Emails().First()].Id;
        Guid seriesId = (await _customerClient.Get(customerId)).SeriesId;

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _seriesClient.Delete(seriesId)))!.Error!;

        ErrorModel expectedError = new("Validation failure", $"Series id {seriesId} is used by customers or invoices", HttpStatusCode.BadRequest);
        await expectedError.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(SeriesDelete_Valid_Success))]
    [DependsOn(nameof(SeriesUpdate_Valid_Success))]
    public async Task SeriesGetAllForSeller_Valid_Success()
    {
        SeriesGetRequest request = new()
        {
            SellerId = SellerTestDataSources.SavedSellers[SellerTestDataSources.Emails().First()].Id
        };

        SeriesListResponse listResponse = await _seriesClient.Get(request);

        await Assert.That(listResponse.Series)
            .IsNotNull()
            .And.HasCount().EqualTo(2);
        await Assert.That(listResponse.TotalCount).IsEqualTo(2);
        await Assert.That(listResponse.Series[0].Name).IsEqualTo("LL");
        await Assert.That(listResponse.Series[1].Name).IsEqualTo("LX");
        await Assert.That(listResponse.Series.All(s => s.SellerId == request.SellerId)).IsTrue();
    }

    [Test]
    [DependsOn(nameof(Series_BeforeTests_DBEmpty))]
    public async Task SeriesGet_PageZero_Fail()
    {
        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _seriesClient.Get(new SeriesGetRequest { Page = 0 })))!.Error!;

        ErrorModel expectedError = new("Validation failure", "Please specify page >= 1", HttpStatusCode.BadRequest);
        await expectedError.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(SeriesAdd_Valid_Success), [typeof(TestCaseModel<SeriesModel>)])]
    public async Task SeriesAdd_NameWithSpaces_IsTrimmed()
    {
        SeriesAddRequest addRequest = new()
        {
            SellerId = SellerTestDataSources.SavedSellers[SellerTestDataSources.Emails().ToArray()[1]].Id,
            Name = " TR "
        };

        AddResponse addResponse = await _seriesClient.Add(addRequest);
        SeriesResponse getResponse = await _seriesClient.Get(addResponse.Id);

        await Assert.That(getResponse.Name).IsEqualTo("TR");

        await _seriesClient.Delete(addResponse.Id);
    }

    public static async Task Series_Delete_All()
    {
        await DeleteAll.Run("Series", async () => (await _seriesClient.Get()).Series.Select(series => series.Id), _seriesClient.Delete);
    }
}
