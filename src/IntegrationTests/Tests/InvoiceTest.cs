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
    public static IEnumerable<TestCaseError<InvoiceIdsUpdateTestModel>> UpdateIdsDataInvalid()
    {
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
                BucketKey = InvoiceKeys().ToArray()[2],
                CustomerEmail = User1_Seller0_Customer0,
                Items = [new() { Name = User1_Seller0_Customer0_Item0, Quantity = 1 }]
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
                BucketKey = InvoiceKeys().ToArray()[2],
                SellerEmail = User1_Seller0,
                Items = [new() { Name = User1_Seller0_Customer0_Item0, Quantity = 1 }]
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
                BucketKey = InvoiceKeys().ToArray()[2],
                SellerEmail = User1_Seller0,
                CustomerEmail = User1_Seller0_Customer0,
                Items = []
            }
        };
        yield return new()
        {
            TestName = "No item id",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify item Id"
            },
            Data = new()
            {
                BucketKey = InvoiceKeys().ToArray()[2],
                SellerEmail = User1_Seller0,
                CustomerEmail = User1_Seller0_Customer0,
                Items = [new() { Quantity = 1 }]
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
                BucketKey = InvoiceKeys().ToArray()[2],
                SellerEmail = User1_Seller0,
                CustomerEmail = User1_Seller0_Customer0,
                Items = [new() { Name = User1_Seller0_Customer0_Item0, Quantity = 0 }]
            }
        };
        yield return new()
        {
            TestName = "Another seller",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Change me"
            },
            Data = new()
            {
                BucketKey = InvoiceKeys().ToArray()[2],
                SellerEmail = User0_Seller0,
                CustomerEmail = User1_Seller0_Customer0,
                Items = [new() { Name = User1_Seller0_Customer0_Item0, Quantity = 1 }]
            }
        };
        yield return new()
        {
            TestName = "Another customer",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Change me"
            },
            Data = new()
            {
                BucketKey = InvoiceKeys().ToArray()[2],
                SellerEmail = User1_Seller0,
                CustomerEmail = User1_Seller1_Customer0,
                Items = [new() { Name = User1_Seller1_Customer0_Item0, Quantity = 1 }]
            }
        };
        yield return new()
        {
            TestName = "Item of another customer",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Change me"
            },
            Data = new()
            {
                BucketKey = InvoiceKeys().ToArray()[2],
                SellerEmail = User1_Seller0,
                CustomerEmail = User1_Seller0_Customer0,
                Items = [new() { Name = User1_Seller0_Customer1_Item0, Quantity = 1 }]
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
    public static InvoiceUpdateRequest UpdateRequestFromInvoice(InvoiceResponse invoice)
    {
        return new InvoiceUpdateRequest()
        {
            Id = invoice.Id,
            SellerId = invoice.SellerId,
            CustomerId = invoice.CustomerId,
            Items = invoice.Items!.Select(i => new InvoiceItemRequest()
            {
                Id = i.Id,
                Quantity = i.Quantity,
                Comments = i.Comments
            }).ToList(),
            Comments = invoice.Comments,
            DueDate = invoice.DueDate,
            CreatedDate = invoice.CreatedDate
        };
    }

    // A user, seller, customer and item that no other test reads, for tests that edit them.
    private static async Task<(SellerResponse seller, CustomerResponse customer, ItemResponse item, InvoiceAddRequest request, Guid invoiceId)> AddInvoiceWithOwnData(string name)
    {
        AddResponse userAdd = await _userClient.Add(new UserAddRequest()
        {
            Email = name + "@thewall.com",
            Name = "Name_" + name,
            LastName = "LastName_" + name,
            Password = "StrongPassword"
        });

        AddResponse sellerAdd = await _sellerClient.Add(new SellerAddRequest()
        {
            UserId = userAdd.Id,
            Email = "seller_" + name + "@thewall.com",
            CompanyName = "Sel_" + name,
            CompanyNumber = "Sel-12122",
            Street = "Sel_Street",
            City = "Sel_City",
            State = "Sel_State",
            Phone = "Sel_Phone",
            BankName = "Sel_BankName",
            BankNumber = "Sel_BankNumber"
        });

        AddResponse seriesAdd = await _seriesClient.Add(new SeriesAddRequest()
        {
            SellerId = sellerAdd.Id,
            Name = name.ToUpper()
        });

        AddResponse customerAdd = await _customerClient.Add(new CustomerAddRequest()
        {
            SellerId = sellerAdd.Id,
            Email = "customer_" + name + "@thewall.com",
            CompanyName = "CUS_" + name,
            CompanyNumber = "CUS-12122",
            Street = "CUS_Street",
            City = "CUS_City",
            State = "CUS_State",
            Phone = "CUS_Phone",
            SeriesId = seriesAdd.Id
        });

        AddResponse itemAdd = await _itemClient.Add(new ItemAddRequest()
        {
            CustomerId = customerAdd.Id,
            Name = "Longclaw_" + name,
            Quantity = 9999,
            Price = 10
        });

        InvoiceAddRequest request = new()
        {
            UserId = userAdd.Id,
            SellerId = sellerAdd.Id,
            CustomerId = customerAdd.Id,
            Items = [new() { Id = itemAdd.Id, Quantity = 3, Comments = "Valyrian steel" }],
            Comments = "Winter is coming",
            DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(6)),
            CreatedDate = DateOnly.FromDateTime(DateTime.Now)
        };
        AddResponse invoiceAdd = await _invoiceClient.Add(request);

        return (await _sellerClient.Get(sellerAdd.Id), await _customerClient.Get(customerAdd.Id),
            await _itemClient.Get(itemAdd.Id), request, invoiceAdd.Id);
    }

    // Asserts the fields an invoice from AddInvoiceWithOwnData keeps from its add request and its customer's series.
    private static async Task AssertOwnInvoice(InvoiceResponse actual, Guid invoiceId, InvoiceAddRequest request, CustomerResponse customer)
    {
        await Assert.That(actual.Id).IsEqualTo(invoiceId);
        await Assert.That(actual.UserId).IsEqualTo(request.UserId);
        await Assert.That(actual.SeriesId).IsEqualTo(customer.SeriesId);
        await Assert.That(actual.SeriesName).IsEqualTo(customer.SeriesName);
        await Assert.That(actual.InvoiceNumber).IsEqualTo(1);
        await Assert.That(actual.CreatedDate).IsEqualTo(request.CreatedDate);
        await Assert.That(actual.DueDate).IsEqualTo(request.DueDate);
        await Assert.That(actual.Comments).IsEqualTo(request.Comments);
        await Assert.That(actual.Status).IsEqualTo(InvoiceStatus.Created);
        await Assert.That(actual.SellerId).IsEqualTo(request.SellerId);
        await Assert.That(actual.CustomerId).IsEqualTo(request.CustomerId);
    }

    private static async Task AssertSingleLine(InvoiceResponse actual, Guid itemId, string name, decimal price, decimal quantity, string? comments)
    {
        await Assert.That(actual.Items)
            .IsNotNull()
            .And.HasCount().EqualTo(1);
        await Assert.That(actual.Items![0].Id).IsEqualTo(itemId);
        await Assert.That(actual.Items![0].Name).IsEqualTo(name);
        await Assert.That(actual.Items![0].Price).IsEquivalentTo(price);
        await Assert.That(actual.Items![0].Quantity).IsEquivalentTo(quantity);
        await Assert.That(actual.Items![0].Comments).IsEqualTo(comments);
        await Assert.That(actual.TotalPrice).IsEquivalentTo(price * quantity);
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

    private static async Task AssertListedInvoiceSeries(InvoiceResponse listed)
    {
        InvoiceResponse saved = InvoiceTestDataSources.SavedInvoices.Values.Single(i => i.Id == listed.Id);

        await Assert.That(listed.SeriesId).IsEqualTo(saved.SeriesId);
        await Assert.That(listed.SeriesName).IsEqualTo(saved.SeriesName);
        await Assert.That(listed.InvoiceNumber).IsEqualTo(saved.InvoiceNumber);
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

        await Assert.That(actual.SellerId).IsEqualTo(expected.SellerId);
        await Assert.That(actual.SellerName).IsEqualTo(expected.SellerName);
        await Assert.That(actual.CustomerId).IsEqualTo(expected.CustomerId);
        await Assert.That(actual.CustomerName).IsEqualTo(expected.CustomerName);

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
    [DependsOn(nameof(CustomerGetAll_Valid_Success))]
    [DependsOn(nameof(CustomerGetAllForSeller_Valid_Success))]
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

        await Assert.That(getResponse.SellerId).IsEqualTo(seller.Id);
        await Assert.That(getResponse.SellerName).IsEqualTo(seller.CompanyName);
        await Assert.That(getResponse.CustomerId).IsEqualTo(customer.Id);
        await Assert.That(getResponse.CustomerName).IsEqualTo(customer.CompanyName);

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
        Guid firstId = addResponse.Id;

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

        getResponse = await _invoiceClient.Get(firstId);
        await Assert.That(getResponse!.InvoiceNumber).IsEquivalentTo(1);
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

        InvoiceUpdateRequest updateRequest = UpdateRequestFromInvoice(invoiceFromBucket);
        updateRequest.Comments = invoiceModel.Comments;
        updateRequest.DueDate = invoiceModel.DueDate;
        updateRequest.CreatedDate = invoiceModel.CreatedDate;

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

        await Assert.That(getResponse.Items)
            .IsNotNull()
            .And.HasCount().EqualTo(updateRequest.Items.Count);

        decimal totalPrice = 0;
        for (int i = 0; i < getResponse.Items!.Count; i++)
        {
            ItemResponse item = InvoiceTestDataSources.SavedItems.Values.Single(s => s.Id == updateRequest.Items[i].Id);

            await Assert.That(getResponse.Items[i].Id).IsEqualTo(item.Id);
            await Assert.That(getResponse.Items[i].Name).IsEqualTo(item.Name);
            await Assert.That(getResponse.Items[i].Price).IsEquivalentTo(item.Price);
            await Assert.That(getResponse.Items[i].Quantity).IsEquivalentTo(updateRequest.Items[i].Quantity);
            await Assert.That(getResponse.Items[i].Comments).IsEqualTo(updateRequest.Items[i].Comments);

            totalPrice += updateRequest.Items[i].Quantity * item.Price;
        }
        await Assert.That(getResponse.TotalPrice).IsEquivalentTo(totalPrice);
    }

    [Test]
    [MethodDataSource(typeof(InvoiceTestDataSources), nameof(InvoiceTestDataSources.UpdateDataInvalid))]
    [DependsOn(nameof(InvoiceDetailsUpdate_Valid_Success))]
    [DisplayName("Invoice update, details invalid data: $testCase")]
    public async Task InvoiceDetailsUpdate_Invalid_Fail(TestCaseError<InvoiceDetaisUpdateTestModel> testCase)
    {
        InvoiceDetaisUpdateTestModel invoiceModel = testCase.Data;

        var invoiceFromBucket = GetInvoiceFromTest(invoiceModel.BucketKey);

        InvoiceUpdateRequest updateRequest = UpdateRequestFromInvoice(invoiceFromBucket);
        updateRequest.Comments = invoiceModel.Comments;
        updateRequest.DueDate = invoiceModel.DueDate;
        updateRequest.CreatedDate = invoiceModel.CreatedDate;

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _invoiceClient.Update(updateRequest)))!.Error!;

        await testCase.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(InvoiceAddMultipleToSameCustomer_Valid_Success))]
    [MethodDataSource(typeof(InvoiceTestDataSources), nameof(InvoiceTestDataSources.UpdateIdsDataInvalid))]
    [DisplayName("Invoice update, seller, customer or items invalid: $testCase")]
    public async Task InvoiceIdsUpdate_Invalid_Fail(TestCaseError<InvoiceIdsUpdateTestModel> testCase)
    {
        InvoiceIdsUpdateTestModel invoiceModel = testCase.Data;

        var invoiceFromBucket = GetInvoiceFromTest(invoiceModel.BucketKey);

        InvoiceUpdateRequest updateRequest = UpdateRequestFromInvoice(invoiceFromBucket);
        updateRequest.SellerId = string.IsNullOrEmpty(invoiceModel.SellerEmail)
            ? Guid.Empty
            : InvoiceTestDataSources.SavedSellers[invoiceModel.SellerEmail].Id;
        updateRequest.CustomerId = string.IsNullOrEmpty(invoiceModel.CustomerEmail)
            ? Guid.Empty
            : InvoiceTestDataSources.SavedCustomers[invoiceModel.CustomerEmail].Id;
        updateRequest.Items = invoiceModel.Items.Select(i => new InvoiceItemRequest()
        {
            Id = string.IsNullOrEmpty(i.Name)
                ? Guid.Empty
                : InvoiceTestDataSources.SavedItems[i.Name].Id,
            Quantity = i.Quantity
        }).ToList();

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _invoiceClient.Update(updateRequest)))!.Error!;

        if (testCase.TestName == "Invalid item quantity")
            testCase.Error.ExtendedMessage = $"Please provide quantity that should > 0 for {updateRequest.Items[0].Id}";
        // Another seller or customer is rejected before it is looked up: an invoice keeps both.
        else if (testCase.TestName == "Another seller")
            testCase.Error.ExtendedMessage = $"Invoice id {updateRequest.Id} can't be moved to another seller";
        else if (testCase.TestName == "Another customer")
            testCase.Error.ExtendedMessage = $"Invoice id {updateRequest.Id} can't be moved to another customer";
        else if (testCase.TestName == "Item of another customer")
            testCase.Error.ExtendedMessage = $"Items id {updateRequest.Items[0].Id} is invalid for customer id {updateRequest.CustomerId}";

        await testCase.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(InvoiceAddMultipleToSameCustomer_Valid_Success))]
    public async Task InvoiceUpdate_NotFound_Fail()
    {
        InvoiceUpdateRequest updateRequest = UpdateRequestFromInvoice(GetInvoiceFromTest(InvoiceTestDataSources.InvoiceKeys().ToArray()[2]));
        updateRequest.Id = Guid.NewGuid();

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _invoiceClient.Update(updateRequest)))!.Error!;

        ErrorModel expectedError = new("Entity not found", $"Invoice:{updateRequest.Id} not found", HttpStatusCode.NotFound);
        await expectedError.CheckErrors(error);
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
            CustomerId = invoiceModel.CustomerId
        };

        InvoiceListResponse listResponse = await _invoiceClient.Get(request);

        await Assert.That(listResponse.Invoices)
            .IsNotNull()
            .And.HasCount().EqualTo(1);

        await Assert.That(listResponse.Invoices[0].CustomerId).IsEquivalentTo(request.CustomerId!.Value);
        await AssertListedInvoiceSeries(listResponse.Invoices[0]);
    }

    [Test]
    [DependsOn(nameof(InvoiceDelete_Valid_Success))]
    public async Task InvoiceGetAllForSeller_Valid_Success()
    {
        var invoiceModel = GetInvoiceFromTest(InvoiceTestDataSources.InvoiceKeys().First());
        var request = new InvoiceGetRequest()
        {
            SellerId = invoiceModel.SellerId
        };

        InvoiceListResponse listResponse = await _invoiceClient.Get(request);

        await Assert.That(listResponse.Invoices)
            .IsNotNull()
            .And.HasCount().EqualTo(2);

        foreach (var invoice in listResponse.Invoices)
        {
            await Assert.That(invoice.SellerId).IsEquivalentTo(request.SellerId!.Value);
            await AssertListedInvoiceSeries(invoice);
        }
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
        {
            await Assert.That(invoice.UserId).IsEquivalentTo(request.UserId!.Value);
            await AssertListedInvoiceSeries(invoice);
        }
    }

    [Test]
    [DependsOn(nameof(InvoiceDetailsUpdate_Valid_Success))]
    [DependsOn(nameof(InvoiceGetAll_Valid_Success))]
    [DependsOn(nameof(InvoiceGetAllForCustomer_Valid_Success))]
    [DependsOn(nameof(InvoiceGetAllForSeller_Valid_Success))]
    [DependsOn(nameof(InvoiceGetAllForUser_Valid_Success))]
    public async Task InvoiceUpdateChangeItems_Valid_Success()
    {
        var invoiceFromBucket = GetInvoiceFromTest(InvoiceTestDataSources.InvoiceKeys().First());

        InvoiceResponse getResponse = await _invoiceClient.Get(invoiceFromBucket.Id);

        ItemResponse item1 = InvoiceTestDataSources.SavedItems[InvoiceTestDataSources.User0_Seller0_Customer0_Item0];
        ItemResponse item2 = InvoiceTestDataSources.SavedItems[InvoiceTestDataSources.User0_Seller0_Customer0_Item1];

        await Assert.That(getResponse!.Items![0].Id).IsEquivalentTo(item1.Id);
        await Assert.That(getResponse!.Items![1].Id).IsEquivalentTo(item2.Id);

        InvoiceUpdateRequest updateRequest = UpdateRequestFromInvoice(invoiceFromBucket);
        updateRequest.Items = [new() { Id = item2.Id, Quantity = item2.Quantity }];

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
    public async Task InvoiceUpdateChangeCustomer_Invalid_Fail()
    {
        var invoiceFromBucket = GetInvoiceFromTest(InvoiceTestDataSources.InvoiceKeys().First());

        InvoiceResponse getResponse = await _invoiceClient.Get(invoiceFromBucket.Id);
        CustomerResponse customerFromTest = InvoiceTestDataSources.SavedCustomers[InvoiceTestDataSources.User0_Seller0_Customer0];

        await Assert.That(getResponse!.CustomerId).IsEquivalentTo(customerFromTest.Id);

        CustomerResponse customerNew = InvoiceTestDataSources.SavedCustomers[InvoiceTestDataSources.User0_Seller0_Customer1];
        ItemResponse itemNew = InvoiceTestDataSources.SavedItems[InvoiceTestDataSources.User0_Seller0_Customer1_Item0];

        InvoiceUpdateRequest updateRequest = UpdateRequestFromInvoice(invoiceFromBucket);
        updateRequest.CustomerId = customerNew.Id;
        updateRequest.Items = [new() { Id = itemNew.Id, Quantity = itemNew.Quantity }];

        // The invoice keeps its customer.
        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _invoiceClient.Update(updateRequest)))!.Error!;
        ErrorModel expectedError = new("Validation failure", $"Invoice id {updateRequest.Id} can't be moved to another customer", HttpStatusCode.BadRequest);
        await expectedError.CheckErrors(error);

        InvoiceResponse afterResponse = await _invoiceClient.Get(updateRequest.Id);

        await Assert.That(afterResponse).IsNotNull();
        await AssertInvoiceDataUnchanged(afterResponse, getResponse);
        await Assert.That(afterResponse.Comments).IsEqualTo(getResponse.Comments);
    }

    [Test]
    [DependsOn(nameof(InvoiceUpdateChangeCustomer_Invalid_Fail))]
    public async Task InvoiceUpdateChangeSeller_Invalid_Fail()
    {
        var invoiceFromBucket = GetInvoiceFromTest(InvoiceTestDataSources.InvoiceKeys().First());

        InvoiceResponse getResponse = await _invoiceClient.Get(invoiceFromBucket.Id);

        SellerResponse sellerFromTest = InvoiceTestDataSources.SavedSellers[InvoiceTestDataSources.User0_Seller0];

        await Assert.That(getResponse!.SellerId).IsEquivalentTo(sellerFromTest.Id);

        SellerResponse sellerNew = InvoiceTestDataSources.SavedSellers[InvoiceTestDataSources.User0_Seller1];
        CustomerResponse customerNew = InvoiceTestDataSources.SavedCustomers[InvoiceTestDataSources.User0_Seller1_Customer0];
        ItemResponse itemNew = InvoiceTestDataSources.SavedItems[InvoiceTestDataSources.User0_Seller1_Customer0_Item0];

        InvoiceUpdateRequest updateRequest = UpdateRequestFromInvoice(invoiceFromBucket);
        updateRequest.SellerId = sellerNew.Id;
        updateRequest.CustomerId = customerNew.Id;
        updateRequest.Items = [new() { Id = itemNew.Id, Quantity = itemNew.Quantity }];

        // A series belongs to one seller, and the invoice keeps its series and number.
        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _invoiceClient.Update(updateRequest)))!.Error!;
        ErrorModel expectedError = new("Validation failure", $"Invoice id {updateRequest.Id} can't be moved to another seller", HttpStatusCode.BadRequest);
        await expectedError.CheckErrors(error);

        InvoiceResponse afterResponse = await _invoiceClient.Get(updateRequest.Id);

        await Assert.That(afterResponse).IsNotNull();
        await AssertInvoiceDataUnchanged(afterResponse, getResponse);
        await Assert.That(afterResponse.Comments).IsEqualTo(getResponse.Comments);
    }
    [Test]
    [DependsOn(nameof(InvoiceAdd_Valid_Success))]
    public async Task CustomerWithInvoicesUpdate_OtherSeries_Fail()
    {
        CustomerResponse customer = InvoiceTestDataSources.SavedCustomers[InvoiceTestDataSources.User0_Seller0_Customer0];
        CustomerResponse otherCustomerOfSeller = InvoiceTestDataSources.SavedCustomers[InvoiceTestDataSources.User0_Seller0_Customer1];

        CustomerUpdateRequest updateRequest = new()
        {
            Id = customer.Id,
            Email = customer.Email,
            CompanyName = customer.CompanyName,
            CompanyNumber = customer.CompanyNumber,
            Street = customer.Street,
            City = customer.City,
            State = customer.State,
            Phone = customer.Phone,
            SeriesId = otherCustomerOfSeller.SeriesId
        };

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _customerClient.Update(updateRequest)))!.Error!;
        ErrorModel expectedError = new("Validation failure", $"Customer id {customer.Id} has invoices, so its series can't be changed", HttpStatusCode.BadRequest);
        await expectedError.CheckErrors(error);

        CustomerResponse getResponse = await _customerClient.Get(customer.Id);

        await Assert.That(getResponse!.SeriesId).IsEqualTo(customer.SeriesId);
        await Assert.That(getResponse!.SeriesName).IsEqualTo(customer.SeriesName);
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

        InvoiceUpdateRequest updateRequest = UpdateRequestFromInvoice(getResponse);
        updateRequest.Comments = "Editing keeps the status";

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

    [Test]
    [DependsOn(nameof(InvoiceGetAll_Valid_Success))]
    [DependsOn(nameof(InvoiceGetAllForCustomer_Valid_Success))]
    [DependsOn(nameof(InvoiceGetAllForSeller_Valid_Success))]
    [DependsOn(nameof(InvoiceGetAllForUser_Valid_Success))]
    public async Task InvoiceShowsCurrentSellerAndCustomer_Valid_Success()
    {
        var (seller, customer, item, request, invoiceId) = await AddInvoiceWithOwnData("Jon");

        SellerUpdateRequest sellerUpdate = new()
        {
            Id = seller.Id,
            Email = "new_" + seller.Email,
            CompanyName = seller.CompanyName + "_new",
            CompanyNumber = seller.CompanyNumber + "_new",
            Street = seller.Street + "_new",
            City = seller.City + "_new",
            State = seller.State + "_new",
            Phone = seller.Phone + "_new",
            BankName = seller.BankName + "_new",
            BankNumber = seller.BankNumber + "_new"
        };
        await _sellerClient.Update(sellerUpdate);

        CustomerUpdateRequest customerUpdate = new()
        {
            Id = customer.Id,
            Email = "new_" + customer.Email,
            CompanyName = customer.CompanyName + "_new",
            CompanyNumber = customer.CompanyNumber + "_new",
            Street = customer.Street + "_new",
            City = customer.City + "_new",
            State = customer.State + "_new",
            Phone = customer.Phone + "_new",
            SeriesId = customer.SeriesId
        };
        await _customerClient.Update(customerUpdate);

        InvoiceResponse getResponse = await GetInvoice(invoiceId);

        await AssertOwnInvoice(getResponse, invoiceId, request, customer);
        await AssertSingleLine(getResponse, item.Id, item.Name, item.Price, request.Items[0].Quantity, request.Items[0].Comments);

        await Assert.That(getResponse.SellerId).IsEqualTo(sellerUpdate.Id);
        await Assert.That(getResponse.SellerName).IsEqualTo(sellerUpdate.CompanyName);
        await Assert.That(getResponse.CustomerId).IsEqualTo(customerUpdate.Id);
        await Assert.That(getResponse.CustomerName).IsEqualTo(customerUpdate.CompanyName);

        await _invoiceClient.Delete(invoiceId);
    }

    [Test]
    [DependsOn(nameof(InvoiceGetAll_Valid_Success))]
    [DependsOn(nameof(InvoiceGetAllForCustomer_Valid_Success))]
    [DependsOn(nameof(InvoiceGetAllForSeller_Valid_Success))]
    [DependsOn(nameof(InvoiceGetAllForUser_Valid_Success))]
    public async Task InvoiceKeepsItemPriceUntilUpdated_Valid_Success()
    {
        var (_, customer, item, request, invoiceId) = await AddInvoiceWithOwnData("Arya");

        ItemUpdateRequest itemUpdate = new()
        {
            Id = item.Id,
            Name = item.Name + "_new",
            Price = item.Price + 5,
            Quantity = item.Quantity
        };
        await _itemClient.Update(itemUpdate);

        InvoiceResponse getResponse = await GetInvoice(invoiceId);

        await AssertOwnInvoice(getResponse, invoiceId, request, customer);
        await AssertSingleLine(getResponse, item.Id, item.Name, item.Price, request.Items[0].Quantity, request.Items[0].Comments);

        InvoiceUpdateRequest updateRequest = UpdateRequestFromInvoice(getResponse);
        updateRequest.Items[0].Quantity = 4;
        updateRequest.Items[0].Comments = "Needle";

        await _invoiceClient.Update(updateRequest);

        getResponse = await GetInvoice(invoiceId);

        await AssertOwnInvoice(getResponse, invoiceId, request, customer);
        await AssertSingleLine(getResponse, item.Id, itemUpdate.Name, itemUpdate.Price, 4, "Needle");

        // One invoice was added in this series, and saving it again must not move the number on.
        await Assert.That((await _seriesClient.Get(customer.SeriesId)).NextNumber).IsEqualTo(2);

        await _invoiceClient.Delete(invoiceId);
    }

    [Test]
    [DependsOn(nameof(InvoiceGetAll_Valid_Success))]
    [DependsOn(nameof(InvoiceGetAllForCustomer_Valid_Success))]
    [DependsOn(nameof(InvoiceGetAllForSeller_Valid_Success))]
    [DependsOn(nameof(InvoiceGetAllForUser_Valid_Success))]
    public async Task InvoiceWithDeletedItem_Valid_Success()
    {
        var (_, customer, item, request, invoiceId) = await AddInvoiceWithOwnData("Sansa");

        await _itemClient.Delete(item.Id);

        InvoiceResponse getResponse = await GetInvoice(invoiceId);

        await AssertOwnInvoice(getResponse, invoiceId, request, customer);
        await AssertSingleLine(getResponse, item.Id, item.Name, item.Price, request.Items[0].Quantity, request.Items[0].Comments);

        await _invoiceClient.Delete(invoiceId);
    }

    [Test]
    [DependsOn(nameof(InvoiceGetAll_Valid_Success))]
    [DependsOn(nameof(InvoiceGetAllForCustomer_Valid_Success))]
    [DependsOn(nameof(InvoiceGetAllForSeller_Valid_Success))]
    [DependsOn(nameof(InvoiceGetAllForUser_Valid_Success))]
    public async Task CustomerDelete_WhenHasInvoice_Fail()
    {
        var (_, customer, item, _, invoiceId) = await AddInvoiceWithOwnData("Bran");

        // Without its item, only the invoice keeps the customer.
        await _itemClient.Delete(item.Id);

        ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => _customerClient.Delete(customer.Id)))!.Error!;

        ErrorModel expectedError = new("Validation failure", "Can not delete (please clear all dependants) or update (item not found): invoices_customer_id", HttpStatusCode.BadRequest);
        await expectedError.CheckErrors(error);

        InvoiceResponse getResponse = await GetInvoice(invoiceId);
        await Assert.That(getResponse.CustomerId).IsEqualTo(customer.Id);

        await _invoiceClient.Delete(invoiceId);
    }

    private static InvoiceAddRequest NewNumberingInvoice(Guid userId, Guid sellerId, Guid customerId, Guid itemId)
    {
        return new InvoiceAddRequest
        {
            UserId = userId,
            SellerId = sellerId,
            CustomerId = customerId,
            Items = [new() { Id = itemId, Quantity = 1 }],
            DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(6)),
            CreatedDate = DateOnly.FromDateTime(DateTime.Now),
            Comments = "Numbering"
        };
    }

    // Runs every clean-up step even when one fails, and returns the failures.
    private static async Task<List<Exception>> CleanUp(IEnumerable<Func<Task>> steps)
    {
        List<Exception> failures = [];
        foreach (Func<Task> step in steps)
        {
            try
            {
                await step();
            }
            catch (Exception e)
            {
                failures.Add(e);
            }
        }

        return failures;
    }

    [Test]
    [DependsOn(nameof(InvoiceGetAll_Valid_Success))]
    [DependsOn(nameof(InvoiceGetAllForCustomer_Valid_Success))]
    [DependsOn(nameof(InvoiceGetAllForSeller_Valid_Success))]
    [DependsOn(nameof(InvoiceGetAllForUser_Valid_Success))]
    public async Task InvoiceNumbering_SeriesRules_Success()
    {
        List<Guid> invoiceIds = [];
        List<Guid> itemIds = [];
        List<Guid> customerIds = [];
        Guid? seriesId = null;
        Guid? sellerId = null;
        Guid? userId = null;
        bool succeeded = false;

        try
        {
            AddResponse user = await _userClient.Add(new UserAddRequest
            {
                Email = "numbering@citadel.com",
                Name = "Maester",
                LastName = "Aemon",
                Password = "StrongPassword"
            });
            userId = user.Id;
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
            sellerId = seller.Id;
            AddResponse series = await _seriesClient.Add(new SeriesAddRequest { SellerId = seller.Id, Name = "NUM" });
            seriesId = series.Id;
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
            customerIds.Add(customer.Id);
            AddResponse otherCustomer = await _customerClient.Add(new CustomerAddRequest
            {
                SellerId = seller.Id,
                SeriesId = series.Id,
                Email = "acolyte@citadel.com",
                CompanyName = "Acolytes",
                CompanyNumber = "ACO-1",
                Street = "Library 3",
                City = "Oldtown",
                State = "The Reach",
                Phone = "+100000003"
            });
            customerIds.Add(otherCustomer.Id);
            AddResponse item = await _itemClient.Add(new ItemAddRequest { CustomerId = customer.Id, Name = "Chain link", Quantity = 9999, Price = 5 });
            itemIds.Add(item.Id);
            AddResponse otherItem = await _itemClient.Add(new ItemAddRequest { CustomerId = otherCustomer.Id, Name = "Candle", Quantity = 9999, Price = 2 });
            itemIds.Add(otherItem.Id);

            async Task<InvoiceResponse> AddInvoice(bool forOtherCustomer = false)
            {
                AddResponse added = forOtherCustomer
                    ? await _invoiceClient.Add(NewNumberingInvoice(user.Id, seller.Id, otherCustomer.Id, otherItem.Id))
                    : await _invoiceClient.Add(NewNumberingInvoice(user.Id, seller.Id, customer.Id, item.Id));
                lock (invoiceIds)
                    invoiceIds.Add(added.Id);

                return await GetInvoice(added.Id);
            }

            async Task DeleteInvoice(Guid id)
            {
                await _invoiceClient.Delete(id);
                lock (invoiceIds)
                    invoiceIds.Remove(id);
            }

            // Two customers share the series, so their invoices 1, 2, 3 come from one sequence.
            List<InvoiceResponse> invoices = [];
            for (int i = 1; i <= 3; i++)
            {
                InvoiceResponse invoice = await AddInvoice(forOtherCustomer: i == 2);
                await Assert.That(invoice.InvoiceNumber).IsEqualTo(i);
                await Assert.That(invoice.SeriesId).IsEqualTo(series.Id);
                await Assert.That(invoice.SeriesName).IsEqualTo("NUM");
                invoices.Add(invoice);
            }
            await Assert.That(invoices[1].CustomerId).IsEqualTo(otherCustomer.Id);
            await Assert.That((await _seriesClient.Get(series.Id)).NextNumber).IsEqualTo(4);

            // Deleting the latest invoice gives its number back.
            await DeleteInvoice(invoices[2].Id);
            await Assert.That((await _seriesClient.Get(series.Id)).NextNumber).IsEqualTo(3);

            InvoiceResponse again = await AddInvoice();
            await Assert.That(again.InvoiceNumber).IsEqualTo(3);

            // Deleting an invoice that isn't the latest leaves a gap.
            await DeleteInvoice(invoices[1].Id);
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

            InvoiceResponse afterRename = await AddInvoice();
            await Assert.That(afterRename.SeriesName).IsEqualTo("NUM");
            await Assert.That(afterRename.InvoiceNumber).IsEqualTo(4);

            // A series with invoices can get a higher next number when it keeps its name (trimmed), and the next
            // invoice uses it.
            await _seriesClient.Update(new SeriesUpdateRequest { Id = series.Id, Name = " NUM ", NextNumber = 10 });
            SeriesResponse raised = await _seriesClient.Get(series.Id);
            await Assert.That(raised.Id).IsEqualTo(series.Id);
            await Assert.That(raised.SellerId).IsEqualTo(seller.Id);
            await Assert.That(raised.Name).IsEqualTo("NUM");
            await Assert.That(raised.NextNumber).IsEqualTo(10);

            InvoiceResponse afterRaise = await AddInvoice(forOtherCustomer: true);
            await Assert.That(afterRaise.InvoiceNumber).IsEqualTo(10);

            // A raised next number can be lowered again, down to the highest invoice number + 1.
            await _seriesClient.Update(new SeriesUpdateRequest { Id = series.Id, Name = "NUM", NextNumber = 15 });
            await Assert.That((await _seriesClient.Get(series.Id)).NextNumber).IsEqualTo(15);
            await _seriesClient.Update(new SeriesUpdateRequest { Id = series.Id, Name = "NUM", NextNumber = 11 });
            await Assert.That((await _seriesClient.Get(series.Id)).NextNumber).IsEqualTo(11);

            // Editing an invoice keeps its series and number.
            InvoiceUpdateRequest edit = UpdateRequestFromInvoice(afterRaise);
            edit.Comments = "Edited";
            await _invoiceClient.Update(edit);
            InvoiceResponse afterEdit = await GetInvoice(afterRaise.Id);
            await AssertInvoiceDataUnchanged(afterEdit, afterRaise);
            await Assert.That(afterEdit.Comments).IsEqualTo("Edited");
            await Assert.That(afterEdit.Status).IsEqualTo(afterRaise.Status);

            // A customer with invoices can be edited while it keeps its series. Its old invoices show the edited
            // customer and keep their series, number and lines.
            CustomerUpdateRequest customerUpdate = new()
            {
                Id = customer.Id,
                SeriesId = series.Id,
                Email = "chained@citadel.com",
                CompanyName = "Novices of the Citadel",
                CompanyNumber = "NOV-2",
                Street = "Library 4",
                City = "Old Oldtown",
                State = "The Arbor",
                Phone = "+100000099"
            };
            await _customerClient.Update(customerUpdate);

            CustomerResponse updatedCustomer = await _customerClient.Get(customer.Id);
            await Assert.That(updatedCustomer.Id).IsEqualTo(customerUpdate.Id);
            await Assert.That(updatedCustomer.SellerId).IsEqualTo(seller.Id);
            await Assert.That(updatedCustomer.SeriesId).IsEqualTo(customerUpdate.SeriesId);
            await Assert.That(updatedCustomer.SeriesName).IsEqualTo("NUM");
            await Assert.That(updatedCustomer.Email).IsEqualTo(customerUpdate.Email);
            await Assert.That(updatedCustomer.CompanyName).IsEqualTo(customerUpdate.CompanyName);
            await Assert.That(updatedCustomer.CompanyNumber).IsEqualTo(customerUpdate.CompanyNumber);
            await Assert.That(updatedCustomer.Street).IsEqualTo(customerUpdate.Street);
            await Assert.That(updatedCustomer.City).IsEqualTo(customerUpdate.City);
            await Assert.That(updatedCustomer.State).IsEqualTo(customerUpdate.State);
            await Assert.That(updatedCustomer.Phone).IsEqualTo(customerUpdate.Phone);

            InvoiceResponse oldInvoiceAfterCustomerEdit = await GetInvoice(invoices[0].Id);
            invoices[0].CustomerName = updatedCustomer.CompanyName;
            await AssertInvoiceDataUnchanged(oldInvoiceAfterCustomerEdit, invoices[0]);
            await Assert.That(oldInvoiceAfterCustomerEdit.Comments).IsEqualTo(invoices[0].Comments);
            await Assert.That(oldInvoiceAfterCustomerEdit.Status).IsEqualTo(invoices[0].Status);

            // A series with invoices can't be deleted.
            error = (await Assert.ThrowsAsync<ClientAPIException>(() => _seriesClient.Delete(series.Id)))!.Error!;
            expectedError = new("Validation failure", $"Series id {series.Id} is used by customers or invoices", HttpStatusCode.BadRequest);
            await expectedError.CheckErrors(error);

            // Invoices created at the same time get different, consecutive numbers.
            Task<InvoiceResponse>[] adds = Enumerable.Range(0, 10).Select(i => AddInvoice(forOtherCustomer: i % 2 == 1)).ToArray();
            await Task.WhenAll(adds);

            await Assert.That(adds.Select(a => a.Result.InvoiceNumber).OrderBy(n => n).ToList())
                .IsEquivalentTo(Enumerable.Range(11, 10).ToList());
            await Assert.That((await _seriesClient.Get(series.Id)).NextNumber).IsEqualTo(21);

            // When the next number can't be reserved any more, the series is full.
            await _seriesClient.Update(new SeriesUpdateRequest { Id = series.Id, Name = "NUM", NextNumber = int.MaxValue - 1 });
            InvoiceResponse lastInvoice = await AddInvoice();
            await Assert.That(lastInvoice.InvoiceNumber).IsEqualTo(int.MaxValue - 1);

            error = (await Assert.ThrowsAsync<ClientAPIException>(() => AddInvoice()))!.Error!;
            expectedError = new("Validation failure", $"Invoice was not created: customer id {customer.Id} is no longer in series id {series.Id}, or the series has no numbers left", HttpStatusCode.BadRequest);
            await expectedError.CheckErrors(error);
            await Assert.That((await _seriesClient.Get(series.Id)).NextNumber).IsEqualTo(int.MaxValue);

            succeeded = true;
        }
        finally
        {
            List<Func<Task>> steps = [
                .. invoiceIds.ToList().Select<Guid, Func<Task>>(id => () => _invoiceClient.Delete(id)),
                .. itemIds.Select<Guid, Func<Task>>(id => () => _itemClient.Delete(id)),
                .. customerIds.Select<Guid, Func<Task>>(id => () => _customerClient.Delete(id))
            ];
            if (seriesId is not null)
                steps.Add(() => _seriesClient.Delete(seriesId.Value));
            if (sellerId is not null)
                steps.Add(() => _sellerClient.Delete(sellerId.Value));
            if (userId is not null)
                steps.Add(() => _userClient.Delete(userId.Value));

            List<Exception> failures = await CleanUp(steps);
            if (succeeded && failures.Count > 0)
                throw new AggregateException("Clean-up failed", failures);
        }
    }
}
