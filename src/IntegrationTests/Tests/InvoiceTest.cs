using BillerContracts;
using BillerContracts.Clients;
using BillerContracts.Clients.Interfaces;
using BillerContracts.Enums;
using BillerContracts.Requests.Customer;
using BillerContracts.Requests.Invoice;
using BillerContracts.Requests.Item;
using BillerContracts.Requests.Seller;
using BillerContracts.Requests.Series;
using BillerContracts.Requests.User;
using BillerContracts.Responses;
using BillerContracts.Responses.Customer;
using BillerContracts.Responses.Invoice;
using BillerContracts.Responses.Item;
using BillerContracts.Responses.Seller;
using BillerContracts.Responses.Series;
using BillerContracts.Responses.User;
using IntegrationTests.ApiClients;
using IntegrationTests.Models;
using System.Collections.Concurrent;
using System.Net;

namespace BillioIntegrationTest.Tests;

public static class InvoiceTestDataSources
{
    public static ConcurrentDictionary<string, UserResponse> SavedUsers = new();
    public static ConcurrentDictionary<string, SellerResponse> SavedSellers = new();
    public static ConcurrentDictionary<string, CustomerResponse> SavedCustomers = new();
    public static ConcurrentDictionary<string, ItemResponse> SavedItems = new();
    public static ConcurrentDictionary<string, InvoiceResponse> SavedInvoices = new();

    public static IEnumerable<string> InvoiceKeys()
    {
        yield return "Invoice 1";
        yield return "Invoice 2";
        yield return "Invoice 3";
        yield return "Invoice 4";
    }

    public static string User0 = "TheSpider@spymaster.com";
    public static string User1 = "ThreeEyedRaven@winterfell.com";

    private static string Seller0 = "Night_watch";
    private static string Seller1 = "Whispering_associates";

    private static string Customer0 = "White_walkers";
    private static string Customer1 = "Grove_druids";

    private static string Item0 = "Crossbow";
    private static string Item1 = "Halbert";

    public static string User0_Seller0 = Seller0 + User0;
    public static string User0_Seller1 = Seller1 + User0;
    public static string User1_Seller0 = Seller0 + User1;
    public static string User1_Seller1 = Seller1 + User1;

    public static string User0_Seller0_Customer0 = Customer0 + Seller0 + User0;
    public static string User0_Seller0_Customer1 = Customer1 + Seller0 + User0;
    public static string User0_Seller1_Customer0 = Customer0 + Seller1 + User0;
    public static string User0_Seller1_Customer1 = Customer1 + Seller1 + User0;
    public static string User1_Seller0_Customer0 = Customer0 + Seller0 + User1;
    public static string User1_Seller0_Customer1 = Customer1 + Seller0 + User1;
    public static string User1_Seller1_Customer0 = Customer0 + Seller1 + User1;
    public static string User1_Seller1_Customer1 = Customer1 + Seller1 + User1;

    public static string User0_Seller0_Customer0_Item0 = Item0 + Customer0 + Seller0 + User0;
    public static string User0_Seller0_Customer0_Item1 = Item1 + Customer0 + Seller0 + User0;
    public static string User0_Seller0_Customer1_Item0 = Item0 + Customer1 + Seller0 + User0;
    public static string User0_Seller0_Customer1_Item1 = Item1 + Customer1 + Seller0 + User0;
    public static string User0_Seller1_Customer0_Item0 = Item0 + Customer0 + Seller1 + User0;
    public static string User0_Seller1_Customer0_Item1 = Item1 + Customer0 + Seller1 + User0;
    public static string User0_Seller1_Customer1_Item0 = Item0 + Customer1 + Seller1 + User0;
    public static string User0_Seller1_Customer1_Item1 = Item1 + Customer1 + Seller1 + User0;
    public static string User1_Seller0_Customer0_Item0 = Item0 + Customer0 + Seller0 + User1;
    public static string User1_Seller0_Customer0_Item1 = Item1 + Customer0 + Seller0 + User1;
    public static string User1_Seller0_Customer1_Item0 = Item0 + Customer1 + Seller0 + User1;
    public static string User1_Seller0_Customer1_Item1 = Item1 + Customer1 + Seller0 + User1;
    public static string User1_Seller1_Customer0_Item0 = Item0 + Customer0 + Seller1 + User1;
    public static string User1_Seller1_Customer0_Item1 = Item1 + Customer0 + Seller1 + User1;
    public static string User1_Seller1_Customer1_Item0 = Item0 + Customer1 + Seller1 + User1;
    public static string User1_Seller1_Customer1_Item1 = Item1 + Customer1 + Seller1 + User1;

    public static IEnumerable<string> UserEmails()
    {
        yield return User0;
        yield return User1;
    }
    public static IEnumerable<string> SellerNames()
    {
        yield return Seller0;
        yield return Seller1;
    }
    public static IEnumerable<string> CustomerNames()
    {
        yield return Customer0;
        yield return Customer1;
    }
    public static IEnumerable<string> ItemNames()
    {
        yield return Item0;
        yield return Item1;
    }

    public static IEnumerable<TestCaseModel<InvoiceTestModel>> AddData()
    {
        yield return new()
        {
            TestName = InvoiceKeys().First().ToString(),
            Data = new()
            {
                BucketKey = InvoiceKeys().First(),
                UserEmail = User0,
                SellerEmail = User0_Seller0,
                CustomerEmail = User0_Seller0_Customer0,
                Items =
                [
                    new (){
                        Name = User0_Seller0_Customer0_Item0,
                        Quantity = 1,
                        Comments = "New"
                    },
                    new (){
                        Name = User0_Seller0_Customer0_Item1,
                        Quantity = 2,
                        Comments = "Old"
                    },
                ],
                Comments = "Invoice com",
                DueDate = DateOnly.FromDateTime(DateTime.Now),
                CreatedDate = DateOnly.FromDateTime(DateTime.Now),
            }
        };
        yield return new()
        {
            TestName = InvoiceKeys().ToArray()[1].ToString() + " same 2 items",
            Data = new()
            {
                BucketKey = InvoiceKeys().ToArray()[1],
                UserEmail = User0,
                SellerEmail = User0_Seller0,
                CustomerEmail = User0_Seller0_Customer1,
                Items =
                [
                    new (){
                        Name = User0_Seller0_Customer1_Item0,
                        Quantity = 3,
                    },
                    new (){
                        Name = User0_Seller0_Customer1_Item0,
                        Quantity = 4,
                    },
                ],
                DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(365)).AddDays(5),
                CreatedDate = DateOnly.FromDateTime(DateTime.Now.AddDays(365))
            }
        };
        yield return new()
        {
            TestName = InvoiceKeys().ToArray()[2].ToString() + " without comment",
            Data = new()
            {
                BucketKey = InvoiceKeys().ToArray()[2],
                UserEmail = User1,
                SellerEmail = User1_Seller0,
                CustomerEmail = User1_Seller0_Customer0,
                Items =
                [
                    new (){
                        Name = User1_Seller0_Customer0_Item0,
                        Quantity = 5,
                        Comments = "New"
                    }
                ],
                DueDate = DateOnly.FromDateTime(DateTime.Now).AddDays(5),
                CreatedDate = DateOnly.FromDateTime(DateTime.Now),
            }
        };
        yield return new()
        {
            TestName = InvoiceKeys().ToArray()[3].ToString() + " without items comments",
            Data = new()
            {
                BucketKey = InvoiceKeys().ToArray()[3],
                UserEmail = User1,
                SellerEmail = User1_Seller1,
                CustomerEmail = User1_Seller1_Customer0,
                Items =
                [
                    new (){
                        Name = User1_Seller1_Customer0_Item0,
                        Quantity = 7,
                    },
                    new (){
                        Name = User1_Seller1_Customer0_Item1,
                        Quantity = 8,
                    },
                ],
                DueDate = new DateOnly(2024, 1, 1).AddDays(5),
                CreatedDate = new DateOnly(2024, 1, 1),
            }
        };
    }
    public static IEnumerable<TestCaseError<InvoiceTestModel>> AddDataInvalid()
    {
        yield return new()
        {
            TestName = "No user id",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify user id"
            },
            Data = new()
            {
                SellerEmail = User0_Seller0,
                CustomerEmail = User0_Seller0_Customer0,
                Items =
                [
                    new (){
                        Name = User0_Seller0_Customer0_Item0,
                        Quantity = 10,
                        Comments = "New"
                    },
                    new (){
                        Name = User0_Seller0_Customer0_Item1,
                        Quantity = 15,
                        Comments = "Old"
                    },
                ],
                Comments = "Invoice com",
                DueDate = new DateOnly(2024, 1, 1).AddDays(5),
                CreatedDate = new DateOnly(2024, 1, 1),
            }
        };
        yield return new()
        {
            TestName = "No seller id",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify seller Id"
            },
            Data = new()
            {
                UserEmail = User0,
                CustomerEmail = User0_Seller0_Customer0,
                Items =
                [
                    new (){
                        Name = User0_Seller0_Customer0_Item0,
                        Quantity = 10,
                        Comments = "New"
                    },
                    new (){
                        Name = User0_Seller0_Customer0_Item1,
                        Quantity = 15,
                        Comments = "Old"
                    },
                ],
                Comments = "Invoice com",
                DueDate = new DateOnly(2024, 1, 1).AddDays(5),
                CreatedDate = new DateOnly(2024, 1, 1)
            }
        };
        yield return new()
        {
            TestName = "Invalid seller id",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Change me"
            },
            Data = new()
            {
                UserEmail = User0,
                SellerEmail = User1_Seller0,
                CustomerEmail = User0_Seller0_Customer0,
                Items =
                [
                    new (){
                        Name = User0_Seller0_Customer0_Item0,
                        Quantity = 10,
                        Comments = "New"
                    },
                    new (){
                        Name = User0_Seller0_Customer0_Item1,
                        Quantity = 15,
                        Comments = "Old"
                    },
                ],
                Comments = "Invoice com",
                DueDate = new DateOnly(2024, 1, 1).AddDays(5),
                CreatedDate = new DateOnly(2024, 1, 1)
            }
        };
        yield return new()
        {
            TestName = "No customer id",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify customer id"
            },
            Data = new()
            {
                UserEmail = User0,
                SellerEmail = User0_Seller0,
                Items =
                [
                    new (){
                        Name = User0_Seller0_Customer0_Item0,
                        Quantity = 10,
                        Comments = "New"
                    },
                    new (){
                        Name = User0_Seller0_Customer0_Item1,
                        Quantity = 15,
                        Comments = "Old"
                    },
                ],
                Comments = "Invoice com",
                DueDate = new DateOnly(2024, 1, 1).AddDays(5),
                CreatedDate = new DateOnly(2024, 1, 1)
            }
        };
        yield return new()
        {
            TestName = "Invalid customer id",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Change me"
            },
            Data = new()
            {
                UserEmail = User0,
                SellerEmail = User0_Seller0,
                CustomerEmail = User1_Seller0_Customer0,
                Items =
                [
                    new (){
                        Name = User0_Seller0_Customer0_Item0,
                        Quantity = 10,
                        Comments = "New"
                    },
                    new (){
                        Name = User0_Seller0_Customer0_Item1,
                        Quantity = 15,
                        Comments = "Old"
                    },
                ],
                Comments = "Invoice com",
                DueDate = new DateOnly(2024, 1, 1).AddDays(5),
                CreatedDate = new DateOnly(2024, 1, 1)
            }
        };
        yield return new()
        {
            TestName = "No items",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please provide at least one item"
            },
            Data = new()
            {
                UserEmail = User0,
                SellerEmail = User0_Seller0,
                CustomerEmail = User0_Seller0_Customer0,
                Comments = "Invoice com",
                DueDate = new DateOnly(2024, 1, 1).AddDays(5),
                CreatedDate = new DateOnly(2024, 1, 1)
            }
        };
        yield return new()
        {
            TestName = "Empty item list",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please provide at least one item"
            },
            Data = new()
            {
                UserEmail = User0,
                SellerEmail = User0_Seller0,
                CustomerEmail = User0_Seller0_Customer0,
                Items = [],
                Comments = "Invoice com",
                DueDate = new DateOnly(2024, 1, 1).AddDays(5),
                CreatedDate = new DateOnly(2024, 1, 1)
            }
        };
        yield return new()
        {
            TestName = "Invalid items id",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Change me"
            },
            Data = new()
            {
                UserEmail = User0,
                SellerEmail = User0_Seller0,
                CustomerEmail = User0_Seller0_Customer0,
                Items =
                [
                    new (){
                        Name = User0_Seller0_Customer0_Item0,
                        Quantity = 10,
                        Comments = "New"
                    },
                    new (){
                        Name = User1_Seller0_Customer0_Item1,
                        Quantity = 15,
                        Comments = "Old"
                    },
                    new (){
                        Name = User1_Seller0_Customer1_Item1,
                        Quantity = 15,
                        Comments = "Old"
                    },
                ],
                Comments = "Invoice com",
                DueDate = new DateOnly(2024, 1, 1).AddDays(5),
                CreatedDate = new DateOnly(2024, 1, 1)
            }
        };
        yield return new()
        {
            TestName = "Invalid item quantity",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Change me"
            },
            Data = new()
            {
                UserEmail = User0,
                SellerEmail = User0_Seller0,
                CustomerEmail = User0_Seller0_Customer0,
                Items =
                [
                    new (){
                        Name = User0_Seller0_Customer0_Item0,
                        Quantity = 10,
                        Comments = "New"
                    },
                    new (){
                        Name = User0_Seller0_Customer0_Item1,
                        Comments = "Old"
                    },
                ],
                Comments = "Invoice com",
                DueDate = new DateOnly(2024, 1, 1).AddDays(5),
                CreatedDate = new DateOnly(2024, 1, 1)
            }
        };
        yield return new()
        {
            TestName = "No due date",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify due date >= create date"
            },
            Data = new()
            {
                UserEmail = User0,
                SellerEmail = User0_Seller0,
                CustomerEmail = User0_Seller0_Customer0,
                Items =
                [
                    new (){
                        Name = User0_Seller0_Customer0_Item0,
                        Quantity = 10,
                        Comments = "New"
                    },
                    new (){
                        Name = User0_Seller0_Customer0_Item1,
                        Quantity = 15,
                        Comments = "Old"
                    },
                ],
                Comments = "Invoice com",
                CreatedDate = new DateOnly(2024, 1, 1)
            }
        };
        yield return new()
        {
            TestName = "Due date < create date",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify due date >= create date"
            },
            Data = new()
            {
                UserEmail = User0,
                SellerEmail = User0_Seller0,
                CustomerEmail = User0_Seller0_Customer0,
                Items =
                [
                    new (){
                        Name = User0_Seller0_Customer0_Item0,
                        Quantity = 10,
                        Comments = "New"
                    },
                    new (){
                        Name = User0_Seller0_Customer0_Item1,
                        Quantity = 15,
                        Comments = "Old"
                    },
                ],
                Comments = "Invoice com",
                DueDate = new DateOnly(2024, 1, 1).AddDays(-1),
                CreatedDate = new DateOnly(2024, 1, 1)
            }
        };
    }
    public static IEnumerable<TestCaseModel<InvoiceDetaisUpdateTestModel>> UpdateDetailsData()
    {
        yield return new()
        {
            TestName = "Update all details",
            Data = new()
            {
                BucketKey = InvoiceKeys().First(),
                Comments = "Updated invoice com",
                DueDate = new DateOnly(2026, 1, 1).AddDays(33),
                CreatedDate = new DateOnly(2024, 1, 1)
            }
        };
        yield return new()
        {
            TestName = "Update all details and remove comment",
            Data = new()
            {
                BucketKey = InvoiceKeys().ToArray()[1],
                DueDate = new DateOnly(2027, 1, 1).AddDays(33),
                CreatedDate = new DateOnly(2027, 1, 1)
            }
        };
    }
    public static IEnumerable<TestCaseError<InvoiceDetaisUpdateTestModel>> UpdateDataInvalid()
    {
        yield return new()
        {
            TestName = "No due date",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify due date >= create date"
            },
            Data = new()
            {
                BucketKey = InvoiceKeys().First(),
                CreatedDate = new DateOnly(2027, 1, 1)
            }
        };
        yield return new()
        {
            TestName = "No create date",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify create date"
            },
            Data = new()
            {
                BucketKey = InvoiceKeys().First(),
                DueDate = new DateOnly(2027, 1, 1).AddDays(33)
            }
        };
    }

    public static IEnumerable<TestCaseModel<InvoiceStatusChangeTestModel>> UpdateStatusData()
    {
        yield return new() { TestName = "Status payed to send", Data = new() { StartStatus = InvoiceStatus.Payed, Status = InvoiceStatus.Send } };
        yield return new() { TestName = "Status send to payed", Data = new() { StartStatus = InvoiceStatus.Send, Status = InvoiceStatus.Payed } };
        yield return new() { TestName = "Status send to created", Data = new() { StartStatus = InvoiceStatus.Send, Status = InvoiceStatus.Created } };
    }

    public static IEnumerable<TestCaseError<InvoiceStatus>> UpdateStatusDataInvalid()
    {
        yield return new()
        {
            TestName = "Status over due can not be set by hand",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Overdue status cannot be set manually"
            },
            Data = InvoiceStatus.OverDue
        };
        yield return new()
        {
            TestName = "Unknown status",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Invalid invoice status: 99"
            },
            Data = (InvoiceStatus)99
        };
    }
}

public partial class Tests
{
    private static readonly IInvoiceClient _invoiceClient = TestClients.Invoice;

    public static InvoiceResponse GetInvoiceFromTest(string bucketKey)
    {
        return InvoiceTestDataSources.SavedInvoices[bucketKey];
    }
    public static (SellerUpdateRequest seller, CustomerUpdateRequest customer, List<InvoiceItemUpdateRequest> items) ExtractUpdateFromInvoice(InvoiceResponse invoice)
    {
        SellerUpdateRequest seller = new()
        {
            Id = invoice.Seller!.Id,
            Email = invoice.Seller!.Email,
            CompanyName = invoice.Seller!.CompanyName,
            CompanyNumber = invoice.Seller!.CompanyNumber,
            Street = invoice.Seller!.Street,
            City = invoice.Seller!.City,
            State = invoice.Seller!.State,
            Phone = invoice.Seller!.Phone,
            BankName = invoice.Seller!.BankName,
            BankNumber = invoice.Seller!.BankNumber
        };
        CustomerUpdateRequest customer = new()
        {
            Id = invoice.Customer!.Id,
            Email = invoice.Customer!.Email,
            CompanyName = invoice.Customer!.CompanyName,
            CompanyNumber = invoice.Customer!.CompanyNumber,
            Street = invoice.Customer!.Street,
            City = invoice.Customer!.City,
            State = invoice.Customer!.State,
            Phone = invoice.Customer!.Phone,
            SeriesId = invoice.Customer!.SeriesId
        };
        List<InvoiceItemUpdateRequest> items = invoice.Items!.Select(i => new InvoiceItemUpdateRequest()
        {
            Id = i.Id,
            Name = i.Name,
            Quantity = i.Quantity,
            Price = i.Price,
            Comments = i.Comments
        }).ToList();
        return (seller, customer, items);
    }

    private static async Task<InvoiceResponse> AddInvoiceForUser1Customer1(string comments)
    {
        var user = InvoiceTestDataSources.SavedUsers[InvoiceTestDataSources.User1];
        var seller = InvoiceTestDataSources.SavedSellers[InvoiceTestDataSources.User1_Seller1];
        var customer = InvoiceTestDataSources.SavedCustomers[InvoiceTestDataSources.User1_Seller1_Customer1];
        var itemList = new List<InvoiceItemRequest>()
        {
            new()
            {
                Id = InvoiceTestDataSources.SavedItems[InvoiceTestDataSources.User1_Seller1_Customer1_Item0].Id,
                Quantity = 1
            }
        };

        InvoiceAddRequest addRequest = new()
        {
            UserId = user.Id,
            SellerId = seller.Id,
            CustomerId = customer.Id,
            Items = itemList,
            DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(6)),
            CreatedDate = DateOnly.FromDateTime(DateTime.Now),
            Comments = comments
        };

        AddResponse addResponse = await _invoiceClient.Add(addRequest);

        await Assert.That(addResponse.Id).IsNotDefault();

        return await GetInvoice(addResponse.Id);
    }

    private static async Task<InvoiceResponse> GetInvoice(Guid id)
    {
        InvoiceResponse getResponse = await _invoiceClient.Get(id);

        await Assert.That(getResponse).IsNotNull();

        return getResponse!;
    }

    private static async Task UpdateInvoiceStatus(Guid id, InvoiceStatus status)
    {
        InvoiceUpdateStatusRequest statusRequest = new()
        {
            Id = id,
            Status = status
        };

        await _invoiceClient.UpdateStatus(statusRequest);
    }

    // Asserts every field except Status and Comments, which the caller checks.
    private static async Task AssertInvoiceDataUnchanged(InvoiceResponse actual, InvoiceResponse expected)
    {
        await Assert.That(actual.Id).IsEqualTo(expected.Id);
        await Assert.That(actual.UserId).IsEqualTo(expected.UserId);
        await Assert.That(actual.SeriesId).IsEqualTo(expected.SeriesId);
        await Assert.That(actual.SeriesName).IsEqualTo(expected.SeriesName);
        await Assert.That(actual.InvoiceNumber).IsEqualTo(expected.InvoiceNumber);
        await Assert.That(actual.CreatedDate).IsEqualTo(expected.CreatedDate);
        await Assert.That(actual.DueDate).IsEqualTo(expected.DueDate);
        await Assert.That(actual.TotalPrice).IsEquivalentTo(expected.TotalPrice);

        await Assert.That(actual.Seller).IsNotNull();
        await Assert.That(actual.Seller!.Id).IsEqualTo(expected.Seller!.Id);
        await Assert.That(actual.Seller!.UserId).IsEqualTo(expected.Seller!.UserId);
        await Assert.That(actual.Seller!.Email).IsEqualTo(expected.Seller!.Email);
        await Assert.That(actual.Seller!.CompanyName).IsEqualTo(expected.Seller!.CompanyName);
        await Assert.That(actual.Seller!.CompanyNumber).IsEqualTo(expected.Seller!.CompanyNumber);
        await Assert.That(actual.Seller!.Street).IsEqualTo(expected.Seller!.Street);
        await Assert.That(actual.Seller!.City).IsEqualTo(expected.Seller!.City);
        await Assert.That(actual.Seller!.State).IsEqualTo(expected.Seller!.State);
        await Assert.That(actual.Seller!.Phone).IsEqualTo(expected.Seller!.Phone);
        await Assert.That(actual.Seller!.BankName).IsEqualTo(expected.Seller!.BankName);
        await Assert.That(actual.Seller!.BankNumber).IsEqualTo(expected.Seller!.BankNumber);

        await Assert.That(actual.Customer).IsNotNull();
        await Assert.That(actual.Customer!.Id).IsEqualTo(expected.Customer!.Id);
        await Assert.That(actual.Customer!.SellerId).IsEqualTo(expected.Customer!.SellerId);
        await Assert.That(actual.Customer!.Email).IsEqualTo(expected.Customer!.Email);
        await Assert.That(actual.Customer!.CompanyName).IsEqualTo(expected.Customer!.CompanyName);
        await Assert.That(actual.Customer!.CompanyNumber).IsEqualTo(expected.Customer!.CompanyNumber);
        await Assert.That(actual.Customer!.Street).IsEqualTo(expected.Customer!.Street);
        await Assert.That(actual.Customer!.City).IsEqualTo(expected.Customer!.City);
        await Assert.That(actual.Customer!.State).IsEqualTo(expected.Customer!.State);
        await Assert.That(actual.Customer!.Phone).IsEqualTo(expected.Customer!.Phone);
        await Assert.That(actual.Customer!.SeriesId).IsEqualTo(expected.Customer!.SeriesId);
        await Assert.That(actual.Customer!.SeriesName).IsEqualTo(expected.Customer!.SeriesName);

        await Assert.That(actual.Items)
            .IsNotNull()
            .And.HasCount().EqualTo(expected.Items!.Count);

        for (int i = 0; i < actual.Items!.Count; i++)
        {
            await Assert.That(actual.Items[i].Id).IsEqualTo(expected.Items[i].Id);
            await Assert.That(actual.Items[i].Name).IsEqualTo(expected.Items[i].Name);
            await Assert.That(actual.Items[i].Price).IsEquivalentTo(expected.Items[i].Price);
            await Assert.That(actual.Items[i].Quantity).IsEquivalentTo(expected.Items[i].Quantity);
            await Assert.That(actual.Items[i].Comments).IsEqualTo(expected.Items[i].Comments);
        }
    }

    [Test]
    [DependsOn(nameof(ItemGetAll_Valid_Success))]
    public async Task Invoice_BeforeTests_DBEmpty()
    {
        InvoiceListResponse response = await _invoiceClient.Get();

        await Assert.That(response.Invoices)
            .IsNotNull()
            .And.IsEmpty();
    }

    [Test]
    [DependsOn(nameof(Invoice_BeforeTests_DBEmpty))]
    public async Task Invoice_Prepare_Data()
    {
        foreach (var userEmail in InvoiceTestDataSources.UserEmails())
        {
            UserAddRequest user = new()
            {
                Email = userEmail,
                Name = "Name_" + userEmail,
                LastName = "LastName_" + userEmail,
                Password = "StrongPassword"
            };

            AddResponse userAdd = await _userClient.Add(user);

            foreach (var sellerName in InvoiceTestDataSources.SellerNames())
            {
                SellerAddRequest seller = new()
                {
                    UserId = userAdd.Id,
                    Email = sellerName + userEmail,
                    CompanyName = sellerName + user.Name,
                    CompanyNumber = "Sel-12122",
                    Street = "Sel_Street",
                    City = "Sel_City",
                    State = "Sel_State",
                    Phone = "Sel_Phone",
                    BankName = "Sel_BankName",
                    BankNumber = "Sel_BankNumber"
                };

                AddResponse sellerAdd = await _sellerClient.Add(seller);

                foreach (var customerName in InvoiceTestDataSources.CustomerNames())
                {
                    SeriesAddRequest series = new()
                    {
                        SellerId = sellerAdd.Id,
                        Name = string.Concat(userEmail.AsSpan(0, 1), sellerName.AsSpan(0, 1), customerName.AsSpan(0, 1))
                    };

                    AddResponse seriesAdd = await _seriesClient.Add(series);

                    CustomerAddRequest customer = new()
                    {
                        SellerId = sellerAdd.Id,
                        Email = customerName + sellerName + userEmail,
                        CompanyName = customerName + seller.CompanyName,
                        CompanyNumber = "CUS-12122",
                        Street = "CUS_Street",
                        City = "CUS_City",
                        State = "CUS_State",
                        Phone = "CUS_Phone",
                        SeriesId = seriesAdd.Id
                    };

                    AddResponse customerAdd = await _customerClient.Add(customer);

                    bool firstItem = true;
                    foreach (var itemName in InvoiceTestDataSources.ItemNames())
                    {
                        ItemAddRequest item = new()
                        {
                            CustomerId = customerAdd.Id,
                            Name = itemName + customerName + sellerName + userEmail,
                            Quantity = 9999,
                            Price = firstItem ? 10 : 20,
                        };

                        AddResponse itemAdd = await _itemClient.Add(item);

                        ItemResponse itemGet = await _itemClient.Get(itemAdd.Id);

                        firstItem = false;
                        if (!InvoiceTestDataSources.SavedItems.TryAdd(itemGet!.Name, itemGet))
                            throw new Exception($"Item with name:{itemGet.Name} can not add to test data");
                    }

                    CustomerResponse customerGet = await _customerClient.Get(customerAdd.Id);
                    if (!InvoiceTestDataSources.SavedCustomers.TryAdd(customerGet!.Email, customerGet))
                        throw new Exception($"Customer with email:{customerGet.Email} can not add to test data");
                }

                SellerResponse sellerGet = await _sellerClient.Get(sellerAdd.Id);
                if (!InvoiceTestDataSources.SavedSellers.TryAdd(sellerGet!.Email, sellerGet))
                    throw new Exception($"Seller with email:{sellerGet.Email} can not add to test data");
            }

            UserResponse userGet = await _userClient.Get(userAdd.Id);
            if (!InvoiceTestDataSources.SavedUsers.TryAdd(userGet!.Email, userGet))
                throw new Exception($"User with email:{userGet.Email} can not add to test data");
        }
    }
    [Test]
    [DependsOn(nameof(Invoice_Prepare_Data))]
    [MethodDataSource(typeof(InvoiceTestDataSources), nameof(InvoiceTestDataSources.AddData))]
    [DisplayName("Invoice add, valid data: $testCase")]
    public async Task InvoiceAdd_Valid_Success(TestCaseModel<InvoiceTestModel> testCase)
    {
        InvoiceTestModel invoiceModel = testCase.Data;

        var user = InvoiceTestDataSources.SavedUsers[invoiceModel.UserEmail];
        var seller = InvoiceTestDataSources.SavedSellers[invoiceModel.SellerEmail];
        var customer = InvoiceTestDataSources.SavedCustomers[invoiceModel.CustomerEmail];
        var itemList = invoiceModel.Items.Select(i => new InvoiceItemRequest()
        {
            Id = InvoiceTestDataSources.SavedItems[i.Name].Id,
            Quantity = i.Quantity,
            Comments = i.Comments
        }
        ).ToList();

        InvoiceAddRequest addRequest = new()
        {
            UserId = user.Id,
            SellerId = seller.Id,
            CustomerId = customer.Id,
            Items = itemList,
            Comments = invoiceModel.Comments,
            DueDate = invoiceModel.DueDate,
            CreatedDate = invoiceModel.CreatedDate,
        };

        AddResponse addResponse = await _invoiceClient.Add(addRequest);

        await Assert.That(addResponse.Id).IsNotDefault();

        InvoiceResponse getResponse = await _invoiceClient.Get(addResponse.Id);

        await Assert.That(getResponse).IsNotNull();
        await Assert.That(getResponse!.Id).IsEqualTo(addResponse.Id);
        await Assert.That(getResponse!.CreatedDate).IsEqualTo(addRequest.CreatedDate);
        await Assert.That(getResponse!.DueDate).IsEqualTo(addRequest.DueDate);
        await Assert.That(getResponse!.UserId).IsEqualTo(addRequest.UserId);
        await Assert.That(getResponse!.Comments).IsEqualTo(addRequest.Comments);
        await Assert.That(getResponse!.SeriesId).IsEqualTo(customer.SeriesId);
        await Assert.That(getResponse!.SeriesName).IsEqualTo(customer.SeriesName);
        await Assert.That(getResponse!.InvoiceNumber).IsEquivalentTo(1);
        await Assert.That(getResponse!.Status).IsEqualTo(InvoiceStatus.Created);

        await Assert.That(getResponse.Seller).IsNotNull();
        await Assert.That(getResponse.Seller!.Id).IsEqualTo(seller.Id);
        await Assert.That(getResponse.Seller!.Email).IsEqualTo(seller.Email);
        await Assert.That(getResponse.Seller!.CompanyName).IsEqualTo(seller.CompanyName);
        await Assert.That(getResponse.Seller!.Street).IsEqualTo(seller.Street);
        await Assert.That(getResponse.Seller!.City).IsEqualTo(seller.City);
        await Assert.That(getResponse.Seller!.State).IsEqualTo(seller.State);
        await Assert.That(getResponse.Seller!.Phone).IsEqualTo(seller.Phone);
        await Assert.That(getResponse.Seller!.BankName).IsEqualTo(seller.BankName);
        await Assert.That(getResponse.Seller!.BankNumber).IsEqualTo(seller.BankNumber);

        await Assert.That(getResponse.Customer).IsNotNull();
        await Assert.That(getResponse.Customer!.Id).IsEqualTo(customer.Id);
        await Assert.That(getResponse.Customer!.SellerId).IsEqualTo(seller.Id);
        await Assert.That(getResponse.Customer!.Email).IsEqualTo(customer.Email);
        await Assert.That(getResponse.Customer!.CompanyName).IsEqualTo(customer.CompanyName);
        await Assert.That(getResponse.Customer!.Street).IsEqualTo(customer.Street);
        await Assert.That(getResponse.Customer!.City).IsEqualTo(customer.City);
        await Assert.That(getResponse.Customer!.State).IsEqualTo(customer.State);
        await Assert.That(getResponse.Customer!.Phone).IsEqualTo(customer.Phone);
        await Assert.That(getResponse.Customer!.SeriesId).IsEqualTo(customer.SeriesId);
        await Assert.That(getResponse.Customer!.SeriesName).IsEqualTo(customer.SeriesName);

        await Assert.That(getResponse.Items)
            .IsNotNull()
            .And.HasCount().EqualTo(invoiceModel.Items.Count);

        decimal totalPrice = 0;
        for (int i = 0; i < getResponse.Items!.Count; i++)
        {
            var item = InvoiceTestDataSources.SavedItems[invoiceModel.Items[i].Name];

            await Assert.That(getResponse.Items[i].Id).IsEqualTo(item.Id);
            await Assert.That(getResponse.Items[i].Name).IsEqualTo(item.Name);
            await Assert.That(getResponse.Items[i].Price).IsEquivalentTo(item.Price);

            await Assert.That(getResponse.Items[i].Comments).IsEqualTo(invoiceModel.Items[i].Comments);
            await Assert.That(getResponse.Items[i].Quantity).IsEquivalentTo(invoiceModel.Items[i].Quantity);

            totalPrice += invoiceModel.Items[i].Quantity * item.Price;
        }
        await Assert.That(getResponse!.TotalPrice).IsEquivalentTo(totalPrice);

        if (!InvoiceTestDataSources.SavedInvoices.TryAdd(invoiceModel.BucketKey, getResponse))
            throw new Exception($"Invoice named:{invoiceModel.BucketKey} can not add to test data");
    }

    [Test]
    [DependsOn(nameof(InvoiceAdd_Valid_Success))]
    [DisplayName("Invoice add multiple invoices to same customer")]
    public async Task InvoiceAddMultipleToSameCustomer_Valid_Success()
    {
        string userEmail = InvoiceTestDataSources.User1;
        string sellerEmail = InvoiceTestDataSources.User1_Seller1;
        string customerEmail = InvoiceTestDataSources.User1_Seller1_Customer1;
        string itemName = InvoiceTestDataSources.User1_Seller1_Customer1_Item0;

        var user = InvoiceTestDataSources.SavedUsers[userEmail];
        var seller = InvoiceTestDataSources.SavedSellers[sellerEmail];
        var customer = InvoiceTestDataSources.SavedCustomers[customerEmail];
        var itemList = new List<InvoiceItemRequest>()
        {
            new()
            {
                Id = InvoiceTestDataSources.SavedItems[itemName].Id,
                Quantity = 1
            }
        };

        InvoiceAddRequest addRequest = new()
        {
            UserId = user.Id,
            SellerId = seller.Id,
            CustomerId = customer.Id,
            Items = itemList,
            DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(6)),
            CreatedDate = DateOnly.FromDateTime(DateTime.Now),
            Comments = "Invoice number should be 1"
        };

        AddResponse addResponse = await _invoiceClient.Add(addRequest);

        await Assert.That(addResponse.Id).IsNotDefault();

        InvoiceResponse getResponse = await _invoiceClient.Get(addResponse.Id);
        await Assert.That(getResponse).IsNotNull();
        await Assert.That(getResponse!.InvoiceNumber).IsEquivalentTo(1);
        await Assert.That(getResponse!.Status).IsEqualTo(InvoiceStatus.Created);

        addRequest.Comments = "Invoice number should be 2";
        addResponse = await _invoiceClient.Add(addRequest);

        await Assert.That(addResponse.Id).IsNotDefault();

        getResponse = await _invoiceClient.Get(addResponse.Id);
        await Assert.That(getResponse).IsNotNull();
        await Assert.That(getResponse!.InvoiceNumber).IsEquivalentTo(2);
        await Assert.That(getResponse!.Status).IsEqualTo(InvoiceStatus.Created);
    }

    [Test]
    [DependsOn(nameof(InvoiceAddMultipleToSameCustomer_Valid_Success))]
    [MethodDataSource(typeof(InvoiceTestDataSources), nameof(InvoiceTestDataSources.AddDataInvalid))]
    [DisplayName("Invoice add, invalid data: $testCase")]
    public async Task InvoiceAdd_InValid_Fail(TestCaseError<InvoiceTestModel> testCase)
    {
        InvoiceTestModel invoiceModel = testCase.Data;

        var itemList = invoiceModel.Items?.Select(i => new InvoiceItemRequest()
        {
            Id = InvoiceTestDataSources.SavedItems[i.Name].Id,
            Quantity = i.Quantity,
            Comments = i.Comments
        }
        ).ToList();

        InvoiceAddRequest addRequest = new()
        {
            UserId = string.IsNullOrEmpty(invoiceModel.UserEmail)
                ? Guid.Empty
                : InvoiceTestDataSources.SavedUsers[invoiceModel.UserEmail].Id,
            SellerId = string.IsNullOrEmpty(invoiceModel.SellerEmail)
                ? Guid.Empty
                : InvoiceTestDataSources.SavedSellers[invoiceModel.SellerEmail].Id,
            CustomerId = string.IsNullOrEmpty(invoiceModel.CustomerEmail)
                ? Guid.Empty
                : InvoiceTestDataSources.SavedCustomers[invoiceModel.CustomerEmail].Id,
            Items = itemList!,
            Comments = invoiceModel.Comments,
            DueDate = invoiceModel.DueDate,
            CreatedDate = invoiceModel.CreatedDate,
        };

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _invoiceClient.Add(addRequest)))!.Error!;

        ErrorModel testError = new ErrorModel()
        {
            StatusCode = error.StatusCode,
            Message = error.Message,

        };

        if (testCase.TestName == "Invalid seller id")
            testError.ExtendedMessage = $"Seller id {addRequest.SellerId} is invalid for user id {addRequest.UserId}";
        else if (testCase.TestName == "Invalid customer id")
            testError.ExtendedMessage = $"Customer id {addRequest.CustomerId} is invalid for seller id {addRequest.SellerId}";
        else if (testCase.TestName == "Invalid items id")
            testError.ExtendedMessage = $"Items id {addRequest.Items[1].Id} {addRequest.Items[2].Id} is invalid for customer id {addRequest.CustomerId}";
        else if (testCase.TestName == "Invalid item quantity")
            testError.ExtendedMessage = $"Please provide quantity that should > 0 for {addRequest.Items[1].Id}";
        else
            testError = testCase.Error!;

        await testError.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(InvoiceAddMultipleToSameCustomer_Valid_Success))]
    public async Task InvoiceDelete_Valid_Success()
    {
        InvoiceResponse addResponse = await AddInvoiceForUser1Customer1("Invoice number should be 1");

        await _invoiceClient.Delete(addResponse.Id);

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _invoiceClient.Get(addResponse.Id)))!.Error!;
        ErrorModel expectedError = new("Entity not found", $"Invoice:{addResponse.Id} not found", HttpStatusCode.NotFound);
        await expectedError.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(InvoiceAddMultipleToSameCustomer_Valid_Success))]
    [MethodDataSource(typeof(InvoiceTestDataSources), nameof(InvoiceTestDataSources.UpdateDetailsData))]
    [DisplayName("Invoice update, details valid data: $testCase")]
    public async Task InvoiceDetailsUpdate_Valid_Success(TestCaseModel<InvoiceDetaisUpdateTestModel> testCase)
    {
        InvoiceDetaisUpdateTestModel invoiceModel = testCase.Data;

        var invoiceFromBucket = GetInvoiceFromTest(invoiceModel.BucketKey);

        (SellerUpdateRequest seller, CustomerUpdateRequest customer, List<InvoiceItemUpdateRequest> items) = ExtractUpdateFromInvoice(invoiceFromBucket);

        InvoiceUpdateRequest updateRequest = new()
        {
            Id = invoiceFromBucket.Id,
            Comments = invoiceModel.Comments,
            DueDate = invoiceModel.DueDate,
            CreatedDate = invoiceModel.CreatedDate,
            Seller = seller,
            Customer = customer,
            Items = items,
        };

        await _invoiceClient.Update(updateRequest);

        InvoiceResponse getResponse = await _invoiceClient.Get(updateRequest.Id);

        await Assert.That(getResponse).IsNotNull();
        await Assert.That(getResponse!.Id).IsEqualTo(updateRequest.Id);
        await Assert.That(getResponse!.CreatedDate).IsEqualTo(updateRequest.CreatedDate);
        await Assert.That(getResponse!.DueDate).IsEqualTo(updateRequest.DueDate);
        await Assert.That(getResponse!.Comments).IsEqualTo(updateRequest.Comments);
        await Assert.That(getResponse!.InvoiceNumber).IsEquivalentTo(invoiceFromBucket.InvoiceNumber);
        await Assert.That(getResponse!.SeriesId).IsEqualTo(invoiceFromBucket.SeriesId);
        await Assert.That(getResponse!.SeriesName).IsEqualTo(invoiceFromBucket.SeriesName);
    }

    [Test]
    [MethodDataSource(typeof(InvoiceTestDataSources), nameof(InvoiceTestDataSources.UpdateDataInvalid))]
    [DependsOn(nameof(InvoiceDetailsUpdate_Valid_Success))]
    [DisplayName("Invoice update, details invalid data: $testCase")]
    public async Task InvoiceDetailsUpdate_Invalid_Fail(TestCaseError<InvoiceDetaisUpdateTestModel> testCase)
    {
        InvoiceDetaisUpdateTestModel invoiceModel = testCase.Data;

        var invoiceFromBucket = GetInvoiceFromTest(invoiceModel.BucketKey);

        (SellerUpdateRequest seller, CustomerUpdateRequest customer, List<InvoiceItemUpdateRequest> items) = ExtractUpdateFromInvoice(invoiceFromBucket);

        InvoiceUpdateRequest updateRequest = new()
        {
            Id = invoiceFromBucket.Id,
            Comments = invoiceModel.Comments,
            DueDate = invoiceModel.DueDate,
            CreatedDate = invoiceModel.CreatedDate,
            Seller = seller,
            Customer = customer,
            Items = items,
        };

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _invoiceClient.Update(updateRequest)))!.Error!;

        await testCase.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(InvoiceDetailsUpdate_Valid_Success))]
    [DisplayName("Invoice update, seller valid data")]
    public async Task InvoiceSellerUpdate_Valid_Success()
    {
        var invoiceFromBucket = GetInvoiceFromTest(InvoiceTestDataSources.InvoiceKeys().First());

        (SellerUpdateRequest seller, CustomerUpdateRequest customer, List<InvoiceItemUpdateRequest> items) = ExtractUpdateFromInvoice(invoiceFromBucket);

        InvoiceUpdateRequest updateRequest = new()
        {
            Id = invoiceFromBucket.Id,
            Comments = invoiceFromBucket.Comments,
            DueDate = invoiceFromBucket.DueDate,
            CreatedDate = invoiceFromBucket.CreatedDate,
            Seller = new SellerUpdateRequest()
            {
                Id = seller.Id,
                Email = seller.Email + "_new",
                CompanyName = seller.CompanyName + "_new",
                CompanyNumber = seller.CompanyNumber + "_new",
                Street = seller.Street + "_new",
                City = seller.City + "_new",
                State = seller.State + "_new",
                Phone = seller.Phone + "_new",
                BankName = seller.BankName + "_new",
                BankNumber = seller.BankNumber + "_new"
            },
            Customer = customer,
            Items = items,
        };

        await _invoiceClient.Update(updateRequest);

        InvoiceResponse getResponse = await _invoiceClient.Get(updateRequest.Id);

        await Assert.That(getResponse).IsNotNull();
        await Assert.That(getResponse!.Seller).IsNotNull();
        await Assert.That(getResponse.Seller!.Id).IsEqualTo(updateRequest.Seller.Id);
        await Assert.That(getResponse.Seller!.Email).IsEqualTo(updateRequest.Seller.Email);
        await Assert.That(getResponse.Seller!.CompanyName).IsEqualTo(updateRequest.Seller.CompanyName);
        await Assert.That(getResponse.Seller!.Street).IsEqualTo(updateRequest.Seller.Street);
        await Assert.That(getResponse.Seller!.City).IsEqualTo(updateRequest.Seller.City);
        await Assert.That(getResponse.Seller!.State).IsEqualTo(updateRequest.Seller.State);
        await Assert.That(getResponse.Seller!.Phone).IsEqualTo(updateRequest.Seller.Phone);
        await Assert.That(getResponse.Seller!.BankName).IsEqualTo(updateRequest.Seller.BankName);
        await Assert.That(getResponse.Seller!.BankNumber).IsEqualTo(updateRequest.Seller.BankNumber);
    }

    [Test]
    [MethodDataSource(typeof(SellerTestDataSources), nameof(SellerTestDataSources.UpdateDataInvalid))]
    [DependsOn(nameof(InvoiceSellerUpdate_Valid_Success))]
    [DisplayName("Invoice update, seller invalid data: $testCase")]
    public async Task InvoiceSellerUpdate_Invalid_Fail(TestCaseError<SellerModel> testCase)
    {
        SellerModel sellerModel = testCase.Data;

        var invoiceFromBucket = GetInvoiceFromTest(InvoiceTestDataSources.InvoiceKeys().First());

        (SellerUpdateRequest seller, CustomerUpdateRequest customer, List<InvoiceItemUpdateRequest> items) = ExtractUpdateFromInvoice(invoiceFromBucket);

        SellerUpdateRequest sellerUpdate = new()
        {
            Id = sellerModel.Id == default ? seller.Id : sellerModel.Id,
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

        InvoiceUpdateRequest updateRequest = new()
        {
            Id = invoiceFromBucket.Id,
            Comments = invoiceFromBucket.Comments,
            DueDate = invoiceFromBucket.DueDate,
            CreatedDate = invoiceFromBucket.CreatedDate,
            Seller = sellerUpdate,
            Customer = customer,
            Items = items,
        };

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _invoiceClient.Update(updateRequest)))!.Error!;

        // Any other seller id is rejected before the seller is looked up: an invoice keeps its seller.
        if (sellerModel.Id != default)
            testCase.Error = new("Validation failure", $"Invoice id {updateRequest.Id} can't be moved to another seller", HttpStatusCode.BadRequest);

        await testCase.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(InvoiceSellerUpdate_Valid_Success))]
    [DisplayName("Invoice update, seller valid data")]
    public async Task InvoiceCustomerUpdate_Valid_Success()
    {
        var invoiceFromBucket = GetInvoiceFromTest(InvoiceTestDataSources.InvoiceKeys().First());

        (SellerUpdateRequest seller, CustomerUpdateRequest customer, List<InvoiceItemUpdateRequest> items) = ExtractUpdateFromInvoice(invoiceFromBucket);

        InvoiceUpdateRequest updateRequest = new()
        {
            Id = invoiceFromBucket.Id,
            Comments = invoiceFromBucket.Comments,
            DueDate = invoiceFromBucket.DueDate,
            CreatedDate = invoiceFromBucket.CreatedDate,
            Seller = seller,
            Customer = new()
            {
                Id = customer.Id,
                Email = customer.Email + "_new",
                CompanyName = customer.CompanyName + "_new",
                CompanyNumber = customer.CompanyNumber + "_new",
                Street = customer.Street + "_new",
                City = customer.City + "_new",
                State = customer.State + "_new",
                Phone = customer.Phone + "_new",
                SeriesId = Guid.NewGuid()
            },
            Items = items,
        };

        await _invoiceClient.Update(updateRequest);

        InvoiceResponse getResponse = await _invoiceClient.Get(updateRequest.Id);

        await Assert.That(getResponse).IsNotNull();
        await Assert.That(getResponse!.Customer).IsNotNull();
        await Assert.That(getResponse.Customer!.Id).IsEqualTo(updateRequest.Customer.Id);
        await Assert.That(getResponse.Customer!.Email).IsEqualTo(updateRequest.Customer.Email);
        await Assert.That(getResponse.Customer!.CompanyName).IsEqualTo(updateRequest.Customer.CompanyName);
        await Assert.That(getResponse.Customer!.Street).IsEqualTo(updateRequest.Customer.Street);
        await Assert.That(getResponse.Customer!.City).IsEqualTo(updateRequest.Customer.City);
        await Assert.That(getResponse.Customer!.State).IsEqualTo(updateRequest.Customer.State);
        await Assert.That(getResponse.Customer!.Phone).IsEqualTo(updateRequest.Customer.Phone);
        await Assert.That(getResponse.Customer!.SeriesId).IsEqualTo(invoiceFromBucket.SeriesId);
        await Assert.That(getResponse.Customer!.SeriesName).IsEqualTo(invoiceFromBucket.SeriesName);
        await Assert.That(getResponse.SeriesId).IsEqualTo(invoiceFromBucket.SeriesId);
        await Assert.That(getResponse.InvoiceNumber).IsEqualTo(invoiceFromBucket.InvoiceNumber);
    }

    [Test]
    [MethodDataSource(typeof(CustomerTestDataSources), nameof(CustomerTestDataSources.InvoiceUpdateDataInvalid))]
    [DependsOn(nameof(InvoiceCustomerUpdate_Valid_Success))]
    [DisplayName("Invoice update, customer invalid data: $testCase")]
    public async Task InvoiceCustomerUpdate_Invalid_Fail(TestCaseError<CustomerModel> testCase)
    {
        CustomerModel customerModel = testCase.Data;

        var invoiceFromBucket = GetInvoiceFromTest(InvoiceTestDataSources.InvoiceKeys().First());

        (SellerUpdateRequest seller, CustomerUpdateRequest customer, List<InvoiceItemUpdateRequest> items) = ExtractUpdateFromInvoice(invoiceFromBucket);

        CustomerUpdateRequest customerUpdate = new()
        {
            Id = customerModel.Id == default ? seller.Id : customerModel.Id,
            Email = customerModel.Email,
            CompanyName = customerModel.CompanyName,
            CompanyNumber = customerModel.CompanyNumber,
            Street = customerModel.Street,
            City = customerModel.City,
            State = customerModel.State,
            Phone = customerModel.Phone,
            SeriesId = SeriesIdByName(customerModel.SeriesName)
        };

        InvoiceUpdateRequest updateRequest = new()
        {
            Id = invoiceFromBucket.Id,
            Comments = invoiceFromBucket.Comments,
            DueDate = invoiceFromBucket.DueDate,
            CreatedDate = invoiceFromBucket.CreatedDate,
            Seller = seller,
            Customer = customerUpdate,
            Items = items,
        };

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _invoiceClient.Update(updateRequest)))!.Error!;

        if (customerModel.Id != default)
            testCase.Error = new("Validation failure", $"Customer id {updateRequest.Customer.Id} is invalid for seller id {seller.Id}", HttpStatusCode.BadRequest);

        await testCase.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(InvoiceCustomerUpdate_Valid_Success))]
    [DisplayName("Invoice update, item valid data")]
    public async Task InvoiceItemUpdate_Valid_Success()
    {
        var invoiceFromBucket = GetInvoiceFromTest(InvoiceTestDataSources.InvoiceKeys().First());

        (SellerUpdateRequest seller, CustomerUpdateRequest customer, List<InvoiceItemUpdateRequest> items) = ExtractUpdateFromInvoice(invoiceFromBucket);

        List<InvoiceItemUpdateRequest> updatedItems = items.Select(i => new InvoiceItemUpdateRequest()
        {
            Id = i.Id,
            Name = i.Name + "_new",
            Quantity = i.Quantity + 999,
            Price = i.Price + 666
        }).ToList();

        InvoiceUpdateRequest updateRequest = new()
        {
            Id = invoiceFromBucket.Id,
            Comments = invoiceFromBucket.Comments,
            DueDate = invoiceFromBucket.DueDate,
            CreatedDate = invoiceFromBucket.CreatedDate,
            Seller = seller,
            Customer = customer,
            Items = updatedItems,
        };

        await _invoiceClient.Update(updateRequest);

        InvoiceResponse getResponse = await _invoiceClient.Get(updateRequest.Id);

        await Assert.That(getResponse!.Items)
            .IsNotNull()
            .And.HasCount().EqualTo(updateRequest.Items.Count);

        decimal totalPrice = 0;
        for (int i = 0; i < getResponse.Items!.Count; i++)
        {
            var item = updateRequest.Items[i];

            await Assert.That(getResponse.Items[i].Id).IsEqualTo(item.Id);
            await Assert.That(getResponse.Items[i].Name).IsEqualTo(item.Name);
            await Assert.That(getResponse.Items[i].Price).IsEquivalentTo(item.Price);

            await Assert.That(getResponse.Items[i].Comments).IsEqualTo(item.Comments);
            await Assert.That(getResponse.Items[i].Quantity).IsEquivalentTo(item.Quantity);

            totalPrice += item.Quantity * item.Price;
        }
        await Assert.That(getResponse!.TotalPrice).IsEquivalentTo(totalPrice);
    }

    [Test]
    [MethodDataSource(typeof(ItemTestDataSources), nameof(ItemTestDataSources.UpdateDataInvalid))]
    [DependsOn(nameof(InvoiceItemUpdate_Valid_Success))]
    [DisplayName("Invoice update, item invalid data: $testCase")]
    public async Task InvoiceItemUpdate_Invalid_Fail(TestCaseError<ItemModel> testCase)
    {
        ItemModel itemModel = testCase.Data;

        var invoiceFromBucket = GetInvoiceFromTest(InvoiceTestDataSources.InvoiceKeys().ToArray()[2]);

        (SellerUpdateRequest seller, CustomerUpdateRequest customer, List<InvoiceItemUpdateRequest> items) = ExtractUpdateFromInvoice(invoiceFromBucket);

        List<InvoiceItemUpdateRequest> updatedItems = new(){
            new InvoiceItemUpdateRequest()
            {
                Id = itemModel.Id == default
                    ? seller.Id
                    : itemModel.Id,
                Name = itemModel.Name,
                Quantity = itemModel.Quantity,
                Price = itemModel.Price
            }
        };

        InvoiceUpdateRequest updateRequest = new()
        {
            Id = invoiceFromBucket.Id,
            Comments = invoiceFromBucket.Comments,
            DueDate = invoiceFromBucket.DueDate,
            CreatedDate = invoiceFromBucket.CreatedDate,
            Seller = seller,
            Customer = customer,
            Items = updatedItems,
        };

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _invoiceClient.Update(updateRequest)))!.Error!;
        if (testCase.TestName == "Quantity = -2")
            testCase.Error.ExtendedMessage = "Please specify quantity";

        if (itemModel.Id != default)
            testCase.Error = new ErrorModel("Validation failure", $"Items id {updateRequest.Items[0].Id} is invalid for customer id {updateRequest.Customer.Id}", HttpStatusCode.BadRequest);

        await testCase.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(InvoiceDelete_Valid_Success))]
    public async Task InvoiceDelete_InValid_Fail()
    {
        Guid id = Guid.NewGuid();

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _invoiceClient.Delete(id)))!.Error!;

        ErrorModel expectedError = new("Entity not found", $"Invoice:{id} not found", HttpStatusCode.NotFound);
        await expectedError.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(InvoiceDelete_Valid_Success))]
    public async Task InvoiceGetAll_Valid_Success()
    {
        InvoiceListResponse listResponse = await _invoiceClient.Get();

        await Assert.That(listResponse.Invoices)
            .IsNotNull()
            .And.HasCount().EqualTo(6);
    }

    [Test]
    [DependsOn(nameof(InvoiceDelete_Valid_Success))]
    public async Task InvoiceGetAllForCustomer_Valid_Success()
    {
        var invoiceModel = GetInvoiceFromTest(InvoiceTestDataSources.InvoiceKeys().First());
        var request = new InvoiceGetRequest()
        {
            CustomerId = invoiceModel.Customer!.Id
        };

        InvoiceListResponse listResponse = await _invoiceClient.Get(request);

        await Assert.That(listResponse.Invoices)
            .IsNotNull()
            .And.HasCount().EqualTo(1);

        await Assert.That(listResponse.Invoices[0].Customer!.Id).IsEquivalentTo(request.CustomerId!.Value);
    }

    [Test]
    [DependsOn(nameof(InvoiceDelete_Valid_Success))]
    public async Task InvoiceGetAllForSeller_Valid_Success()
    {
        var invoiceModel = GetInvoiceFromTest(InvoiceTestDataSources.InvoiceKeys().First());
        var request = new InvoiceGetRequest()
        {
            SellerId = invoiceModel.Seller!.Id
        };

        InvoiceListResponse listResponse = await _invoiceClient.Get(request);

        await Assert.That(listResponse.Invoices)
            .IsNotNull()
            .And.HasCount().EqualTo(2);

        foreach (var invoice in listResponse.Invoices)
            await Assert.That(invoice.Seller!.Id).IsEquivalentTo(request.SellerId!.Value);
    }

    [Test]
    [DependsOn(nameof(InvoiceDelete_Valid_Success))]
    public async Task InvoiceGetAllForUser_Valid_Success()
    {
        var invoiceModel = GetInvoiceFromTest(InvoiceTestDataSources.InvoiceKeys().First());
        var request = new InvoiceGetRequest()
        {
            UserId = invoiceModel.UserId
        };

        InvoiceListResponse listResponse = await _invoiceClient.Get(request);

        await Assert.That(listResponse.Invoices)
            .IsNotNull()
            .And.HasCount().EqualTo(2);

        foreach (var invoice in listResponse.Invoices)
            await Assert.That(invoice.UserId).IsEquivalentTo(request.UserId!.Value);
    }

    [Test]
    [DependsOn(nameof(InvoiceSellerUpdate_Valid_Success))]
    [DependsOn(nameof(InvoiceCustomerUpdate_Valid_Success))]
    [DependsOn(nameof(InvoiceItemUpdate_Valid_Success))]
    public async Task InvoiceUpdateChangeItems_Valid_Success()
    {
        var invoiceFromBucket = GetInvoiceFromTest(InvoiceTestDataSources.InvoiceKeys().First());

        (SellerUpdateRequest seller, CustomerUpdateRequest customer, List<InvoiceItemUpdateRequest> items) = ExtractUpdateFromInvoice(invoiceFromBucket);

        InvoiceResponse getResponse = await _invoiceClient.Get(invoiceFromBucket.Id);

        ItemResponse item1 = InvoiceTestDataSources.SavedItems[InvoiceTestDataSources.User0_Seller0_Customer0_Item0];
        ItemResponse item2 = InvoiceTestDataSources.SavedItems[InvoiceTestDataSources.User0_Seller0_Customer0_Item1];

        await Assert.That(getResponse!.Items![0].Id).IsEquivalentTo(item1.Id);
        await Assert.That(getResponse!.Items![1].Id).IsEquivalentTo(item2.Id);

        List<InvoiceItemUpdateRequest> updatedItems = new()
        {
            new InvoiceItemUpdateRequest()
            {
                Id = item2.Id,
                Name = item2.Name,
                Quantity = item2.Quantity,
                Price = item2.Price
            }
        };

        InvoiceUpdateRequest updateRequest = new()
        {
            Id = invoiceFromBucket.Id,
            Comments = invoiceFromBucket.Comments,
            DueDate = invoiceFromBucket.DueDate,
            CreatedDate = invoiceFromBucket.CreatedDate,
            Seller = seller,
            Customer = customer,
            Items = updatedItems,
        };

        await _invoiceClient.Update(updateRequest);

        getResponse = await _invoiceClient.Get(updateRequest.Id);

        await Assert.That(getResponse).IsNotNull();
        await Assert.That(getResponse!.Items)
            .IsNotNull()
            .And.HasCount().EqualTo(1);

        await Assert.That(getResponse!.Items![0].Id).IsEquivalentTo(item2.Id);
    }

    [Test]
    [DependsOn(nameof(InvoiceUpdateChangeItems_Valid_Success))]
    public async Task InvoiceUpdateChangeCustomer_Valid_Success()
    {
        var invoiceFromBucket = GetInvoiceFromTest(InvoiceTestDataSources.InvoiceKeys().First());

        (SellerUpdateRequest seller, CustomerUpdateRequest customer, List<InvoiceItemUpdateRequest> items) = ExtractUpdateFromInvoice(invoiceFromBucket);

        InvoiceResponse getResponse = await _invoiceClient.Get(invoiceFromBucket.Id);
        CustomerResponse customerFromTest = InvoiceTestDataSources.SavedCustomers[InvoiceTestDataSources.User0_Seller0_Customer0];

        await Assert.That(getResponse!.Customer!.Id).IsEquivalentTo(customerFromTest.Id);

        CustomerResponse customerNew = InvoiceTestDataSources.SavedCustomers[InvoiceTestDataSources.User0_Seller0_Customer1];
        ItemResponse itemNew = InvoiceTestDataSources.SavedItems[InvoiceTestDataSources.User0_Seller0_Customer1_Item0];

        List<InvoiceItemUpdateRequest> updatedItems = new()
        {
            new InvoiceItemUpdateRequest()
            {
                Id = itemNew.Id,
                Name = itemNew.Name,
                Quantity = itemNew.Quantity,
                Price = itemNew.Price
            }
        };

        CustomerUpdateRequest updatedCustomer = new()
        {
            Id = customerNew.Id,
            Email = customerNew.Email,
            CompanyName = customerNew.CompanyName,
            CompanyNumber = customerNew.CompanyNumber,
            Street = customerNew.Street,
            City = customerNew.City,
            State = customerNew.State,
            Phone = customerNew.Phone,
            SeriesId = customerNew.SeriesId
        };

        InvoiceUpdateRequest updateRequest = new()
        {
            Id = invoiceFromBucket.Id,
            Comments = invoiceFromBucket.Comments,
            DueDate = invoiceFromBucket.DueDate,
            CreatedDate = invoiceFromBucket.CreatedDate,
            Seller = seller,
            Customer = updatedCustomer,
            Items = updatedItems,
        };

        await _invoiceClient.Update(updateRequest);

        getResponse = await _invoiceClient.Get(updateRequest.Id);

        await Assert.That(getResponse).IsNotNull();
        await Assert.That(getResponse!.Customer!.Id).IsEquivalentTo(customerNew.Id);
        await Assert.That(getResponse!.Items)
            .IsNotNull()
            .And.HasCount().EqualTo(1);

        await Assert.That(getResponse!.Items![0].Id).IsEquivalentTo(itemNew.Id);
    }

    [Test]
    [DependsOn(nameof(InvoiceUpdateChangeCustomer_Valid_Success))]
    public async Task InvoiceUpdateChangeSeller_Invalid_Fail()
    {
        var invoiceFromBucket = GetInvoiceFromTest(InvoiceTestDataSources.InvoiceKeys().First());

        InvoiceResponse getResponse = await _invoiceClient.Get(invoiceFromBucket.Id);

        SellerResponse sellerFromTest = InvoiceTestDataSources.SavedSellers[InvoiceTestDataSources.User0_Seller0];

        await Assert.That(getResponse!.Seller!.Id).IsEquivalentTo(sellerFromTest.Id);

        SellerResponse sellerNew = InvoiceTestDataSources.SavedSellers[InvoiceTestDataSources.User0_Seller1];
        CustomerResponse customerNew = InvoiceTestDataSources.SavedCustomers[InvoiceTestDataSources.User0_Seller1_Customer0];
        ItemResponse itemNew = InvoiceTestDataSources.SavedItems[InvoiceTestDataSources.User0_Seller1_Customer0_Item0];

        List<InvoiceItemUpdateRequest> updatedItems = new()
        {
            new InvoiceItemUpdateRequest()
            {
                Id = itemNew.Id,
                Name = itemNew.Name,
                Quantity = itemNew.Quantity,
                Price = itemNew.Price
            }
        };

        CustomerUpdateRequest updatedCustomer = new()
        {
            Id = customerNew.Id,
            Email = customerNew.Email,
            CompanyName = customerNew.CompanyName,
            CompanyNumber = customerNew.CompanyNumber,
            Street = customerNew.Street,
            City = customerNew.City,
            State = customerNew.State,
            Phone = customerNew.Phone,
            SeriesId = customerNew.SeriesId
        };

        SellerUpdateRequest updatedSeller = new()
        {
            Id = sellerNew.Id,
            Email = sellerNew.Email,
            CompanyName = sellerNew.CompanyName,
            CompanyNumber = sellerNew.CompanyNumber,
            Street = sellerNew.Street,
            City = sellerNew.City,
            State = sellerNew.State,
            Phone = sellerNew.Phone,
            BankName = sellerNew.BankName,
            BankNumber = sellerNew.BankNumber
        };

        InvoiceUpdateRequest updateRequest = new()
        {
            Id = invoiceFromBucket.Id,
            Comments = invoiceFromBucket.Comments,
            DueDate = invoiceFromBucket.DueDate,
            CreatedDate = invoiceFromBucket.CreatedDate,
            Seller = updatedSeller,
            Customer = updatedCustomer,
            Items = updatedItems,
        };

        // A series belongs to one seller, and the invoice keeps its series and number.
        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _invoiceClient.Update(updateRequest)))!.Error!;
        ErrorModel expectedError = new("Validation failure", $"Invoice id {updateRequest.Id} can't be moved to another seller", HttpStatusCode.BadRequest);
        await expectedError.CheckErrors(error);

        InvoiceResponse afterResponse = await _invoiceClient.Get(updateRequest.Id);

        await Assert.That(afterResponse).IsNotNull();
        await Assert.That(afterResponse!.Seller!.Id).IsEquivalentTo(sellerFromTest.Id);
        await Assert.That(afterResponse!.Customer!.Id).IsEquivalentTo(getResponse!.Customer!.Id);
        await Assert.That(afterResponse!.Items!.Select(i => i.Id).ToList())
            .IsEquivalentTo(getResponse!.Items!.Select(i => i.Id).ToList());
        await Assert.That(afterResponse!.SeriesId).IsEquivalentTo(getResponse!.SeriesId);
        await Assert.That(afterResponse!.InvoiceNumber).IsEqualTo(getResponse!.InvoiceNumber);
    }

    public static async Task Invoice_Delete_All()
    {
        await DeleteAll.Run("Invoice", async () => (await _invoiceClient.Get()).Invoices.Select(invoice => invoice.Id), _invoiceClient.Delete);
    }

    [Test]
    [DependsOn(nameof(InvoiceGetAll_Valid_Success))]
    [DependsOn(nameof(InvoiceGetAllForCustomer_Valid_Success))]
    [DependsOn(nameof(InvoiceGetAllForSeller_Valid_Success))]
    [DependsOn(nameof(InvoiceGetAllForUser_Valid_Success))]
    [MethodDataSource(typeof(InvoiceTestDataSources), nameof(InvoiceTestDataSources.UpdateStatusData))]
    [DisplayName("Invoice update status, valid data: $testCase")]
    public async Task InvoiceStatusUpdate_Valid_Success(TestCaseModel<InvoiceStatusChangeTestModel> testCase)
    {
        InvoiceStatusChangeTestModel statusChange = testCase.Data;

        InvoiceResponse added = await AddInvoiceForUser1Customer1("Status test");
        await Assert.That(added.Status).IsEqualTo(InvoiceStatus.Created);

        await UpdateInvoiceStatus(added.Id, statusChange.StartStatus);

        InvoiceResponse getResponse = await GetInvoice(added.Id);
        await Assert.That(getResponse.Status).IsEqualTo(statusChange.StartStatus);
        await Assert.That(getResponse.Comments).IsEqualTo(added.Comments);
        await AssertInvoiceDataUnchanged(getResponse, added);

        await UpdateInvoiceStatus(added.Id, statusChange.Status);

        getResponse = await GetInvoice(added.Id);
        await Assert.That(getResponse.Status).IsEqualTo(statusChange.Status);
        await Assert.That(getResponse.Comments).IsEqualTo(added.Comments);
        await AssertInvoiceDataUnchanged(getResponse, added);

        (SellerUpdateRequest seller, CustomerUpdateRequest customer, List<InvoiceItemUpdateRequest> items) = ExtractUpdateFromInvoice(getResponse);

        InvoiceUpdateRequest updateRequest = new()
        {
            Id = added.Id,
            Comments = "Editing keeps the status",
            DueDate = added.DueDate,
            CreatedDate = added.CreatedDate,
            Seller = seller,
            Customer = customer,
            Items = items,
        };

        await _invoiceClient.Update(updateRequest);

        getResponse = await GetInvoice(added.Id);
        await Assert.That(getResponse.Status).IsEqualTo(statusChange.Status);
        await Assert.That(getResponse.Comments).IsEqualTo(updateRequest.Comments);
        await AssertInvoiceDataUnchanged(getResponse, added);

        await _invoiceClient.Delete(added.Id);
    }

    [Test]
    [DependsOn(nameof(InvoiceGetAll_Valid_Success))]
    [DependsOn(nameof(InvoiceGetAllForCustomer_Valid_Success))]
    [DependsOn(nameof(InvoiceGetAllForSeller_Valid_Success))]
    [DependsOn(nameof(InvoiceGetAllForUser_Valid_Success))]
    [MethodDataSource(typeof(InvoiceTestDataSources), nameof(InvoiceTestDataSources.UpdateStatusDataInvalid))]
    [DisplayName("Invoice update status, invalid data: $testCase")]
    public async Task InvoiceStatusUpdate_Invalid_Fail(TestCaseError<InvoiceStatus> testCase)
    {
        InvoiceResponse added = await AddInvoiceForUser1Customer1("Status test, invalid");
        await UpdateInvoiceStatus(added.Id, InvoiceStatus.Send);

        InvoiceUpdateStatusRequest statusRequest = new()
        {
            Id = added.Id,
            Status = testCase.Data
        };

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _invoiceClient.UpdateStatus(statusRequest)))!.Error!;

        await testCase.CheckErrors(error);

        InvoiceResponse getResponse = await GetInvoice(added.Id);
        await Assert.That(getResponse.Status).IsEqualTo(InvoiceStatus.Send);
        await Assert.That(getResponse.Comments).IsEqualTo(added.Comments);
        await AssertInvoiceDataUnchanged(getResponse, added);

        await _invoiceClient.Delete(added.Id);
    }

    [Test]
    [DependsOn(nameof(InvoiceAddMultipleToSameCustomer_Valid_Success))]
    public async Task InvoiceStatusUpdate_NotFound_Fail()
    {
        InvoiceUpdateStatusRequest statusRequest = new()
        {
            Id = Guid.NewGuid(),
            Status = InvoiceStatus.Send
        };

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _invoiceClient.UpdateStatus(statusRequest)))!.Error!;

        ErrorModel expectedError = new("Entity not found", $"Invoice:{statusRequest.Id} not found", HttpStatusCode.NotFound);
        await expectedError.CheckErrors(error);
    }

    private static async Task<InvoiceResponse> AddNumberingInvoice(Guid userId, Guid sellerId, Guid customerId, Guid itemId)
    {
        InvoiceAddRequest addRequest = new()
        {
            UserId = userId,
            SellerId = sellerId,
            CustomerId = customerId,
            Items = [new() { Id = itemId, Quantity = 1 }],
            DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(6)),
            CreatedDate = DateOnly.FromDateTime(DateTime.Now),
            Comments = "Numbering"
        };

        AddResponse addResponse = await _invoiceClient.Add(addRequest);

        return await GetInvoice(addResponse.Id);
    }

    [Test]
    [DependsOn(nameof(InvoiceGetAll_Valid_Success))]
    [DependsOn(nameof(InvoiceGetAllForCustomer_Valid_Success))]
    [DependsOn(nameof(InvoiceGetAllForSeller_Valid_Success))]
    [DependsOn(nameof(InvoiceGetAllForUser_Valid_Success))]
    public async Task InvoiceNumbering_SeriesRules_Success()
    {
        AddResponse user = await _userClient.Add(new UserAddRequest
        {
            Email = "numbering@citadel.com",
            Name = "Maester",
            LastName = "Aemon",
            Password = "StrongPassword"
        });
        AddResponse seller = await _sellerClient.Add(new SellerAddRequest
        {
            UserId = user.Id,
            Email = "ravens@citadel.com",
            CompanyName = "Citadel",
            CompanyNumber = "CIT-1",
            Street = "Archmaester road 1",
            City = "Oldtown",
            State = "The Reach",
            Phone = "+100000001",
            BankName = "Iron bank",
            BankNumber = "IB-1"
        });
        AddResponse series = await _seriesClient.Add(new SeriesAddRequest { SellerId = seller.Id, Name = "NUM" });
        AddResponse customer = await _customerClient.Add(new CustomerAddRequest
        {
            SellerId = seller.Id,
            SeriesId = series.Id,
            Email = "novice@citadel.com",
            CompanyName = "Novices",
            CompanyNumber = "NOV-1",
            Street = "Library 2",
            City = "Oldtown",
            State = "The Reach",
            Phone = "+100000002"
        });
        AddResponse item = await _itemClient.Add(new ItemAddRequest { CustomerId = customer.Id, Name = "Chain link", Quantity = 9999, Price = 5 });

        // Invoices 1, 2, 3 in a new series.
        List<InvoiceResponse> invoices = [];
        for (int i = 1; i <= 3; i++)
        {
            InvoiceResponse invoice = await AddNumberingInvoice(user.Id, seller.Id, customer.Id, item.Id);
            await Assert.That(invoice.InvoiceNumber).IsEqualTo(i);
            await Assert.That(invoice.SeriesId).IsEqualTo(series.Id);
            await Assert.That(invoice.SeriesName).IsEqualTo("NUM");
            invoices.Add(invoice);
        }
        await Assert.That((await _seriesClient.Get(series.Id)).NextNumber).IsEqualTo(4);

        // Deleting the latest invoice gives its number back.
        await _invoiceClient.Delete(invoices[2].Id);
        await Assert.That((await _seriesClient.Get(series.Id)).NextNumber).IsEqualTo(3);

        InvoiceResponse again = await AddNumberingInvoice(user.Id, seller.Id, customer.Id, item.Id);
        await Assert.That(again.InvoiceNumber).IsEqualTo(3);

        // Deleting an invoice that isn't the latest leaves a gap.
        await _invoiceClient.Delete(invoices[1].Id);
        await Assert.That((await _seriesClient.Get(series.Id)).NextNumber).IsEqualTo(4);

        // The next number can't go back to or below an existing invoice number.
        SeriesUpdateRequest lowerNumber = new() { Id = series.Id, Name = "NUM", NextNumber = 3 };
        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _seriesClient.Update(lowerNumber)))!.Error!;
        ErrorModel expectedError = new("Validation failure", "Next number must be greater than 3, the highest invoice number in series NUM", HttpStatusCode.BadRequest);
        await expectedError.CheckErrors(error);

        // A series with invoices can't be renamed, so its invoices keep their series name.
        SeriesUpdateRequest rename = new() { Id = series.Id, Name = "NUM2", NextNumber = 4 };
        error = (await Assert.ThrowsAsync<ClientAPIException>(() => _seriesClient.Update(rename)))!.Error!;
        expectedError = new("Validation failure", "Series NUM has invoices, so its name can't be changed", HttpStatusCode.BadRequest);
        await expectedError.CheckErrors(error);

        InvoiceResponse oldInvoice = await GetInvoice(invoices[0].Id);
        await Assert.That(oldInvoice.SeriesName).IsEqualTo("NUM");
        await Assert.That(oldInvoice.InvoiceNumber).IsEqualTo(1);

        InvoiceResponse afterRename = await AddNumberingInvoice(user.Id, seller.Id, customer.Id, item.Id);
        await Assert.That(afterRename.SeriesName).IsEqualTo("NUM");
        await Assert.That(afterRename.InvoiceNumber).IsEqualTo(4);

        // A series with invoices can't be deleted.
        error = (await Assert.ThrowsAsync<ClientAPIException>(() => _seriesClient.Delete(series.Id)))!.Error!;
        expectedError = new("Validation failure", $"Series id {series.Id} is used by customers or invoices", HttpStatusCode.BadRequest);
        await expectedError.CheckErrors(error);

        // Invoices created at the same time get different, consecutive numbers.
        InvoiceResponse[] parallel = await Task.WhenAll(Enumerable.Range(0, 10)
            .Select(_ => AddNumberingInvoice(user.Id, seller.Id, customer.Id, item.Id)));
        await Assert.That(parallel.Select(i => i.InvoiceNumber).OrderBy(n => n).ToList())
            .IsEquivalentTo(Enumerable.Range(5, 10).ToList());
        await Assert.That((await _seriesClient.Get(series.Id)).NextNumber).IsEqualTo(15);

        List<Guid> created = [invoices[0].Id, again.Id, afterRename.Id, .. parallel.Select(i => i.Id)];
        foreach (Guid id in created)
            await _invoiceClient.Delete(id);
    }
}
