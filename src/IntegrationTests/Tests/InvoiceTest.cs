using BillioIntegrationTest.Helpers;
using BillioIntegrationTest.Models;
using Contracts.Requests.Customer;
using Contracts.Requests.Invoice;
using Contracts.Requests.Item;
using Contracts.Requests.Seller;
using Contracts.Requests.User;
using Contracts.Responses;
using Contracts.Responses.Customer;
using Contracts.Responses.Invoice;
using Contracts.Responses.Item;
using Contracts.Responses.Seller;
using Contracts.Responses.User;
using IntegrationTests.Clients;
using IntegrationTests.Models;
using System.Net;
using TUnit.Assertions.Extensions.Generic;
using TUnit.Core.Extensions;
using TUnit.Core.Interfaces;
using static IntegrationTests.Program;

namespace BillioIntegrationTest.Tests;
public record InvoiceModelf
{
    public string SellerEmail { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
    public List<string> ItemsName { get; set; } = [];
    public string Comments { get; set; } = string.Empty;
    public DateOnly DueDate { get; set; }
    public DateOnly CreatedDate { get; set; }
}

public static class InvoiceTestDataSources
{
    public static IEnumerable<TestCaseModel<InvoiceModel>> AddData()
    {
        yield return new()
        {
            TestCase = InvoiceCreateDate().First().ToString(),
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify invoice name"
            },
            Data = new()
            {
                UserEmail = UserEmails().First(),
                SellerEmail = SellerNames().First() + UserEmails().First(),
                CustomerEmail = CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                Items = new List<InvoiceItemModel>()
                {
                    new (){
                        Name = ItemNames().First() + CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                        Quantity = 10,
                        Comments = "New"
                    },
                    new (){
                        Name = ItemNames().ToArray()[1] + CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                        Quantity = 15,
                        Comments = "Old"
                    },
                },
                Comments = "Invoice com",
                DueDate = InvoiceCreateDate().First().AddDays(5),
                CreatedDate = InvoiceCreateDate().First(),
            }
        };
        yield return new()
        {
            TestCase = InvoiceCreateDate().ToArray()[1].ToString() + " without comment",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify invoice name"
            },
            Data = new()
            {
                UserEmail = UserEmails().ToArray()[1],
                SellerEmail = SellerNames().First() + UserEmails().ToArray()[1],
                CustomerEmail = CustomerNames().First() + SellerNames().First() + UserEmails().ToArray()[1],
                Items = new List<InvoiceItemModel>()
                {
                    new (){
                        Name = ItemNames().First() + CustomerNames().First() + SellerNames().First() + UserEmails().ToArray()[1],
                        Quantity = 10,
                        Comments = "New"
                    },
                    new (){
                        Name = ItemNames().ToArray()[1] + CustomerNames().First() + SellerNames().First() + UserEmails().ToArray()[1],
                        Quantity = 15,
                        Comments = "Old"
                    },
                },
                DueDate = InvoiceCreateDate().ToArray()[1].AddDays(5),
                CreatedDate = InvoiceCreateDate().ToArray()[1],
            }
        };
        yield return new()
        {
            TestCase = InvoiceCreateDate().ToArray()[1].ToString() + " without comments",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify invoice name"
            },
            Data = new()
            {
                UserEmail = UserEmails().ToArray()[1],
                SellerEmail = SellerNames().First() + UserEmails().ToArray()[1],
                CustomerEmail = CustomerNames().ToArray()[1] + SellerNames().First() + UserEmails().ToArray()[1],
                Items = new List<InvoiceItemModel>()
                {
                    new (){
                        Name = ItemNames().First() + CustomerNames().ToArray()[1] + SellerNames().First() + UserEmails().ToArray()[1],
                        Quantity = 10,
                    },
                    new (){
                        Name = ItemNames().ToArray()[1] + CustomerNames().ToArray()[1] + SellerNames().First() + UserEmails().ToArray()[1],
                        Quantity = 15,
                    },
                },
                DueDate = InvoiceCreateDate().ToArray()[2].AddDays(5),
                CreatedDate = InvoiceCreateDate().ToArray()[2],
            }
        };
    }
    public static IEnumerable<TestCaseModel<InvoiceModel>> AddDataInvalid()
    {        
        yield return new()
        {
            TestCase = "No user id",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify user id"
            },
            Data = new()
            {
                SellerEmail = SellerNames().First() + UserEmails().First(),
                CustomerEmail = CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                Items = new List<InvoiceItemModel>()
                {
                    new (){
                        Name = ItemNames().First() + CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                        Quantity = 10,
                        Comments = "New"
                    },
                    new (){
                        Name = ItemNames().ToArray()[1] + CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                        Quantity = 15,
                        Comments = "Old"
                    },
                },
                Comments = "Invoice com",
                DueDate = InvoiceCreateDate().First().AddDays(5),
                CreatedDate = InvoiceCreateDate().First(),
            }
        };
        yield return new()
        {
            TestCase = "No seller id",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify seller Id"
            },
            Data = new()
            {
                UserEmail = UserEmails().First(),
                CustomerEmail = CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                Items = new List<InvoiceItemModel>()
                {
                    new (){
                        Name = ItemNames().First() + CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                        Quantity = 10,
                        Comments = "New"
                    },
                    new (){
                        Name = ItemNames().ToArray()[1] + CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                        Quantity = 15,
                        Comments = "Old"
                    },
                },
                Comments = "Invoice com",
                DueDate = InvoiceCreateDate().First().AddDays(5),
                CreatedDate = InvoiceCreateDate().First(),
            }
        };
        yield return new()
        {
            TestCase = "Invalid seller id",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify valid seller Id"
            },
            Data = new()
            {
                UserEmail = UserEmails().First(),
                SellerEmail = SellerNames().First() + UserEmails().ToArray()[1],
                CustomerEmail = CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                Items = new List<InvoiceItemModel>()
                {
                    new (){
                        Name = ItemNames().First() + CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                        Quantity = 10,
                        Comments = "New"
                    },
                    new (){
                        Name = ItemNames().ToArray()[1] + CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                        Quantity = 15,
                        Comments = "Old"
                    },
                },
                Comments = "Invoice com",
                DueDate = InvoiceCreateDate().First().AddDays(5),
                CreatedDate = InvoiceCreateDate().First(),
            }
        };
        yield return new()
        {
            TestCase = "No customer id",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify customer id"
            },
            Data = new()
            {
                UserEmail = UserEmails().First(),
                SellerEmail = SellerNames().First() + UserEmails().First(),
                Items = new List<InvoiceItemModel>()
                {
                    new (){
                        Name = ItemNames().First() + CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                        Quantity = 10,
                        Comments = "New"
                    },
                    new (){
                        Name = ItemNames().ToArray()[1] + CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                        Quantity = 15,
                        Comments = "Old"
                    },
                },
                Comments = "Invoice com",
                DueDate = InvoiceCreateDate().First().AddDays(5),
                CreatedDate = InvoiceCreateDate().First(),
            }
        };
        yield return new()
        {
            TestCase = "Invalid customer id",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify valid customer id"
            },
            Data = new()
            {
                UserEmail = UserEmails().First(),                
                SellerEmail = SellerNames().First() + UserEmails().First(),
                CustomerEmail = CustomerNames().First() + SellerNames().First() + UserEmails().ToArray()[1],
                Items = new List<InvoiceItemModel>()
                {
                    new (){
                        Name = ItemNames().First() + CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                        Quantity = 10,
                        Comments = "New"
                    },
                    new (){
                        Name = ItemNames().ToArray()[1] + CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                        Quantity = 15,
                        Comments = "Old"
                    },
                },
                Comments = "Invoice com",
                DueDate = InvoiceCreateDate().First().AddDays(5),
                CreatedDate = InvoiceCreateDate().First(),
            }
        };
        yield return new()
        {
            TestCase = "No items",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please provide at least one item"
            },
            Data = new()
            {
                UserEmail = UserEmails().First(),
                SellerEmail = SellerNames().First() + UserEmails().First(),
                CustomerEmail = CustomerNames().First() + SellerNames().First() + UserEmails().First(),                
                Comments = "Invoice com",
                DueDate = InvoiceCreateDate().First().AddDays(5),
                CreatedDate = InvoiceCreateDate().First(),
            }
        };
        yield return new()
        {
            TestCase = "Empty item list",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please provide at least one item"
            },
            Data = new()
            {
                UserEmail = UserEmails().First(),
                SellerEmail = SellerNames().First() + UserEmails().First(),
                CustomerEmail = CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                Items = new List<InvoiceItemModel>(),
                Comments = "Invoice com",
                DueDate = InvoiceCreateDate().First().AddDays(5),
                CreatedDate = InvoiceCreateDate().First(),
            }
        };
        yield return new()
        {
            TestCase = "Invalid item",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please provide valid item"
            },
            Data = new()
            {
                UserEmail = UserEmails().First(),
                SellerEmail = SellerNames().First() + UserEmails().First(),
                CustomerEmail = CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                Items = new List<InvoiceItemModel>()
                {
                    new (){
                        Name = ItemNames().First() + CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                        Quantity = 10,
                        Comments = "New"
                    },
                    new (){
                        Name = ItemNames().ToArray()[1] + CustomerNames().First() + SellerNames().First() + UserEmails().ToArray()[1],
                        Quantity = 15,
                        Comments = "Old"
                    },
                },
                Comments = "Invoice com",
                DueDate = InvoiceCreateDate().First().AddDays(5),
                CreatedDate = InvoiceCreateDate().First(),
            }
        };
        yield return new()
        {
            TestCase = "No due date",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify due date >= create date"
            },
            Data = new()
            {
                UserEmail = UserEmails().First(),
                SellerEmail = SellerNames().First() + UserEmails().First(),
                CustomerEmail = CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                Items = new List<InvoiceItemModel>()
                {
                    new (){
                        Name = ItemNames().First() + CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                        Quantity = 10,
                        Comments = "New"
                    },
                    new (){
                        Name = ItemNames().ToArray()[1] + CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                        Quantity = 15,
                        Comments = "Old"
                    },
                },
                Comments = "Invoice com",
                CreatedDate = InvoiceCreateDate().First(),
            }
        };
        yield return new()
        {
            TestCase = "Due date < create date",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify due date >= create date"
            },
            Data = new()
            {
                UserEmail = UserEmails().First(),
                SellerEmail = SellerNames().First() + UserEmails().First(),
                CustomerEmail = CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                Items = new List<InvoiceItemModel>()
                {
                    new (){
                        Name = ItemNames().First() + CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                        Quantity = 10,
                        Comments = "New"
                    },
                    new (){
                        Name = ItemNames().ToArray()[1] + CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                        Quantity = 15,
                        Comments = "Old"
                    },
                },
                Comments = "Invoice com",
                DueDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-1),
                CreatedDate = DateOnly.FromDateTime(DateTime.Now),
            }
        };
        /*
        yield return new()
        {
            TestCase = "No user id",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify invoice name"
            },
            Data = new()
            {
                UserEmail = UserEmails().First(),
                SellerEmail = SellerNames().First() + UserEmails().First(),
                CustomerEmail = CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                Items = new List<InvoiceItemModel>()
                {
                    new (){
                        Name = ItemNames().First() + CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                        Quantity = 10,
                        Comments = "New"
                    },
                    new (){
                        Name = ItemNames().ToArray()[1] + CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                        Quantity = 15,
                        Comments = "Old"
                    },
                },
                Comments = "Invoice com",
                DueDate = InvoiceCreateDate().First().AddDays(5),
                CreatedDate = InvoiceCreateDate().First(),
            }
        };*/
    }

    public static IEnumerable<TestCaseModel<InvoiceModel>> UpdateDataInvalid()
    {
        yield return new()
        {
            TestCase = "No name",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify invoice name"
            },
            Data = new()
        };
        /*
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
                ExtendedMessage = "Please specify invoice name"
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
                ExtendedMessage = "Please specify invoice quantity"
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
        };*/
    }

    public static IEnumerable<string> UserEmails()
    {
        yield return "TheSpider@spymaster.com";
        yield return "ThreeEyedRaven@winterfell.com";
    }
    public static IEnumerable<string> SellerNames()
    {
        yield return "Night_watch";
        yield return "Whispering_associates";
    }
    public static IEnumerable<string> CustomerNames()
    {
        yield return "White_walkers";
        yield return "Grove_druids";
    }
    public static IEnumerable<string> ItemNames()
    {
        yield return "Crossbow";
        yield return "Halbert";
    }
    public static IEnumerable<DateOnly> InvoiceCreateDate()
    {
        yield return new DateOnly(2024,1,1);
        yield return DateOnly.FromDateTime(DateTime.Now);
        yield return DateOnly.FromDateTime(DateTime.Now.AddDays(365));
    }
}

public partial class Tests
{
    private static readonly InvoiceClient _invoiceClient = new();
    public static T GetInvoicePrepFromTest<T>(string name)
    {
        return TestDataHelper.GetData<T>(name, nameof(Invoice_Prepare_Data));        
    }
    public static InvoiceModel GetInvoiceFromTest(string name)
    {
        return TestDataHelper.GetData<InvoiceModel>(name, nameof(InvoiceAdd_Valid_Success));
    }

    [Test]
    [DependsOn(nameof(ItemGetAll_Valid_Success))]
    public async Task Invoice_BeforeTests_DBEmpty()
    {
        var requestResult = await _invoiceClient.Get();
        InvoiceListResponse response = requestResult.Match(
            invoice => { return invoice; },
            error => { throw new Exception(error.ToString()); }
        );

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
            bool firstUser = userEmail == InvoiceTestDataSources.UserEmails().First();
            UserAddRequest user = new()
            {
                Email = userEmail,
                Name = firstUser ? "Varys" : "Bran",
                LastName = firstUser ? "." : "Stark",
                Password = "SecretWhisperer"
            };

            var userResult = await _userClient.Add(user);
            AddResponse userAdd = userResult.Match(
                user => { return user; },
                error => { throw new Exception("User add: " + error.ToString()); }
            );

            foreach (var sellerName in InvoiceTestDataSources.SellerNames())
            {
                SellerAddRequest seller = new()
                {
                    UserId = userAdd.Id,
                    Email = sellerName + userEmail,
                    CompanyName = sellerName + user.Name,
                    CompanyNumber = "CN12122",
                    Street = "Street",
                    City = "City",
                    State = "State",
                    Phone = "Phone",
                    BankName = "BankName",
                    BankNumber = "BankNumber"
                };

                var sellerResult = await _sellerClient.Add(seller);
                AddResponse sellerAdd = sellerResult.Match(
                    seller => { return seller; },
                    error => { throw new Exception("Seller add: " + error.ToString()); }
                );

                foreach (var customerName in InvoiceTestDataSources.CustomerNames())
                {
                    CustomerAddRequest customer = new()
                    {
                        SellerId = sellerAdd.Id,
                        Email = customerName + sellerName + userEmail,
                        CompanyName = customerName + seller.CompanyName,
                        CompanyNumber = "CN12122",
                        Street = "Street",
                        City = "City",
                        State = "State",
                        Phone = "Phone",
                        InvoiceName = string.Concat(userEmail.AsSpan(0, 1), sellerName.AsSpan(0, 1), customerName.AsSpan(0, 1))
                    };

                    var customerResult = await _customerClient.Add(customer);
                    AddResponse customerAdd = customerResult.Match(
                        customer => { return customer; },
                        error => { throw new Exception("Customer add: " + error.ToString()); }
                    );

                    foreach (var itemName in InvoiceTestDataSources.ItemNames())
                    {
                        bool firstItem = userEmail == InvoiceTestDataSources.ItemNames().First();
                        ItemAddRequest item = new()
                        {
                            CustomerId = customerAdd.Id,
                            Name = itemName + customerName + sellerName + userEmail,
                            Quantity = 9999,
                            Price = firstItem ? 10 : 20,
                        };

                        var itemResult = await _itemClient.Add(item);
                        AddResponse itemAdd = itemResult.Match(
                            item => { return item; },
                            error => { throw new Exception("Item add: " + error.ToString()); }
                        );

                        var itemGetResult = await _itemClient.Get(itemAdd.Id);
                        ItemResponse? itemGet = itemGetResult.Match(
                            item => { return item; },
                            error => { throw new Exception("Item get: " + error.ToString()); }
                        );
                        TestContext.Current!.ObjectBag.Add(itemGet!.Name, itemGet);
                    }

                    var customerGetResult = await _customerClient.Get(customerAdd.Id);
                    CustomerResponse? customerGet = customerGetResult.Match(
                        customer => { return customer; },
                        error => { throw new Exception("Customer get: " + error.ToString()); }
                    );
                    TestContext.Current!.ObjectBag.Add(customerGet!.Email, customerGet);
                }

                var sellerGetResult = await _sellerClient.Get(sellerAdd.Id);
                SellerResponse? sellerGet = sellerGetResult.Match(
                    seller => { return seller; },
                    error => { throw new Exception("Seller get: " + error.ToString()); }
                );
                TestContext.Current!.ObjectBag.Add(sellerGet!.Email, sellerGet);
            }

            var userGetResult = await _userClient.Get(userAdd.Id);
            UserResponse? userGet = userGetResult.Match(
                user => { return user; },
                error => { throw new Exception("User get: " + error.ToString()); }
            );
            TestContext.Current!.ObjectBag.Add(userGet!.Email, userGet);        
        }
    }

    [Test]
    [ParallelLimiter<SingleLimiter>]
    [DependsOn(nameof(Invoice_Prepare_Data))]
    [MethodDataSource(typeof(InvoiceTestDataSources), nameof(InvoiceTestDataSources.AddData))]
    [DisplayName("Invoice add, valid data: $testCase")]
    public async Task InvoiceAdd_Valid_Success(TestCaseModel<InvoiceModel> testCase)
    {
        InvoiceModel invoiceModel = testCase.Data;

        var user = GetInvoicePrepFromTest<UserResponse>(invoiceModel.UserEmail);
        var seller = GetInvoicePrepFromTest<SellerResponse>(invoiceModel.SellerEmail);
        var customer = GetInvoicePrepFromTest<CustomerResponse>(invoiceModel.CustomerEmail);
        var itemList = invoiceModel.Items.Select(i => new InvoiceItemRequest()
            {
                Id = GetInvoicePrepFromTest<ItemResponse>(i.Name).Id,
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
        
        var addResponseResult = await _invoiceClient.Add(addRequest);
        AddResponse addResponse = addResponseResult.Match(
            invoice => { return invoice; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(addResponse.Id).IsNotNull();

        var getResponseResult = await _invoiceClient.Get(addResponse.Id);
        InvoiceResponse? getResponse = getResponseResult.Match(
            invoice => { return invoice; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(getResponse).IsNotNull();
        await Assert.That(getResponse!.Id).IsEqualTo(addResponse.Id);
        await Assert.That(getResponse!.CreatedDate).IsEqualTo(addRequest.CreatedDate);
        await Assert.That(getResponse!.DueDate).IsEqualTo(addRequest.DueDate);
        await Assert.That(getResponse!.UserId).IsEqualTo(addRequest.UserId);
        await Assert.That(getResponse!.Comments).IsEqualTo(addRequest.Comments);
        await Assert.That(getResponse!.InvoiceNumber).IsEquivalentTo(customer.InvoiceNumber);

        await Assert.That(getResponse.Seller).IsNotNull();
        await Assert.That(getResponse.Seller!.Id).IsEqualTo(seller.Id);
        await Assert.That(getResponse.Seller!.UserId).IsEqualTo(seller.UserId);
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
        await Assert.That(getResponse.Customer!.SellerId).IsEqualTo(customer.SellerId);
        await Assert.That(getResponse.Customer!.SellerId).IsEqualTo(seller.Id);
        await Assert.That(getResponse.Customer!.Email).IsEqualTo(customer.Email);
        await Assert.That(getResponse.Customer!.CompanyName).IsEqualTo(customer.CompanyName);
        await Assert.That(getResponse.Customer!.Street).IsEqualTo(customer.Street);
        await Assert.That(getResponse.Customer!.City).IsEqualTo(customer.City);
        await Assert.That(getResponse.Customer!.State).IsEqualTo(customer.State);
        await Assert.That(getResponse.Customer!.Phone).IsEqualTo(customer.Phone);
        await Assert.That(getResponse.Customer!.InvoiceNumber).IsEquivalentTo(customer.InvoiceNumber);
        await Assert.That(getResponse.Customer!.InvoiceName).IsEqualTo(customer.InvoiceName);

        await Assert.That(getResponse.Items)
            .IsNotNull()
            .And.HasCount().EqualTo(invoiceModel.Items.Count);

        decimal totalPrice = 0;
        for (int i = 0; i < getResponse.Items!.Count; i++)
        {
            var item = GetInvoicePrepFromTest<ItemResponse>(invoiceModel.Items[i].Name);

            await Assert.That(getResponse.Items[i].Id).IsEqualTo(item.Id);
            await Assert.That(getResponse.Items[i].Name).IsEqualTo(item.Name);
            await Assert.That(getResponse.Items[i].Price).IsEquivalentTo(item.Price);

            await Assert.That(getResponse.Items[i].Comments).IsEqualTo(invoiceModel.Items[i].Comments);
            await Assert.That(getResponse.Items[i].Quantity).IsEquivalentTo(invoiceModel.Items[i].Quantity);

            totalPrice += invoiceModel.Items[i].Quantity * item.Price;
        }        
        await Assert.That(getResponse!.TotalPrice).IsEquivalentTo(totalPrice);        

        TestContext.Current!.ObjectBag.Add(getResponse.CreatedDate.ToString(), getResponse);
    }

    [Test]
    [MethodDataSource(typeof(InvoiceTestDataSources), nameof(InvoiceTestDataSources.AddDataInvalid))]
    [DependsOn(nameof(InvoiceAdd_Valid_Success), [typeof(TestCaseModel<InvoiceModel>)])]
    [DisplayName("Invoice add, invalid data: $testCase")]
    public async Task InvoiceAdd_InValid_Fail(TestCaseModel<InvoiceModel> testCase)
    {
        InvoiceModel invoiceModel = testCase.Data;

        var itemList = invoiceModel.Items is null ? null : invoiceModel.Items.Select(i => new InvoiceItemRequest()
        {
            Id = GetInvoicePrepFromTest<ItemResponse>(i.Name).Id,
            Quantity = i.Quantity,
            Comments = i.Comments
        }
        ).ToList();

        InvoiceAddRequest addRequest = new()
        {
            UserId = string.IsNullOrEmpty(invoiceModel.UserEmail) ? Guid.Empty : GetInvoicePrepFromTest<UserResponse>(invoiceModel.UserEmail).Id,
            SellerId = string.IsNullOrEmpty(invoiceModel.SellerEmail) ? Guid.Empty : GetInvoicePrepFromTest<SellerResponse>(invoiceModel.SellerEmail).Id,
            CustomerId = string.IsNullOrEmpty(invoiceModel.CustomerEmail) ? Guid.Empty : GetInvoicePrepFromTest<CustomerResponse>(invoiceModel.CustomerEmail).Id,
            Items = itemList,
            Comments = invoiceModel.Comments,
            DueDate = invoiceModel.DueDate,
            CreatedDate = invoiceModel.CreatedDate,
        };

        var addResponseResult = await _invoiceClient.Add(addRequest);
        var requestResult = await _invoiceClient.Add(addRequest);
        ErrorModel error = requestResult.Match(
            invoice => { throw new Exception(invoice.ToString()); },
            error => { return error; }
        );

        await testCase.Error!.CheckErrors(error);
    }

    [Test]
    [DependsOn(nameof(InvoiceAdd_Valid_Success), [typeof(TestCaseModel<InvoiceModel>)])]
    public async Task InvoiceDelete_Valid_Success()
    {/*
        InvoiceAddRequest addRequest = new()
        {
            ItemId = GetItemFromTest(ItemTestDataSources.Emails().First()).Id,
            Name = "delete@me.com",
            Price = 999999999999999,
            Quantity = 1
        };

        var addResponseResult = await _invoiceClient.Add(addRequest);
        AddResponse addResponse = addResponseResult.Match(
            invoice => { return invoice; },
            error => { throw new Exception(error.ToString()); }
        );

        var deleteResponseResult = await _invoiceClient.Delete(addResponse.Id);
        bool deleteResponse = deleteResponseResult.Match(
            invoice => { return invoice; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(deleteResponse).IsTrue();

        var getResponseResult = await _invoiceClient.Get(addResponse.Id);
        ErrorModel error = getResponseResult.Match(
            item => { throw new Exception(item!.ToString()); },
            error => { return error; }
        );
        ErrorModel expectedError = new("Entity not found", $"Invoice invoice:{addResponse.Id} not found", HttpStatusCode.NotFound);
        await expectedError.CheckErrors(error);*/
    }

    [Test]
    [DependsOn(nameof(InvoiceDelete_Valid_Success))]
    public async Task InvoiceDelete_InValid_Fail()
    {
        Guid id = Guid.NewGuid();

        var deleteResponseResult = await _invoiceClient.Delete(id);
        ErrorModel error = deleteResponseResult.Match(
            invoice => { throw new Exception(invoice.ToString()); },
            error => { return error; }
        );

        ErrorModel expectedError = new("Entity not found", $"Invoice data:{id} not found", HttpStatusCode.NotFound);
        await expectedError.CheckErrors(error);
    }   

    [Test]
    [DependsOn(nameof(InvoiceDelete_Valid_Success))]
    public async Task InvoiceGetAll_Valid_Success()
    {
        var listResponseResult = await _invoiceClient.Get();
        InvoiceListResponse listResponse = listResponseResult.Match(
            invoice => { return invoice; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(listResponse.Invoices)
            .IsNotNull()
            .And.HasCount().EqualTo(3);
    }

    [Test]
    [MethodDataSource(typeof(InvoiceTestDataSources), nameof(InvoiceTestDataSources.InvoiceCreateDate))]
    [DependsOn(nameof(InvoiceAdd_Valid_Success), [typeof(InvoiceAddRequest)])]
    [DisplayName("Invoice update, valid data: $name")]
    public async Task InvoiceUpdate_Valid_Success(DateOnly dueDate)
    {/*
        InvoiceModel invoiceModel = GetInvoiceFromTest(name);

        InvoiceUpdateRequest updateRequest = new()
        {
            Id = invoiceModel.Id,
            Name = invoiceModel.Name + "_new",
            Quantity = invoiceModel.Quantity + 999,
            Price = invoiceModel.Price + 666
        };

        var updateResponseResult = await _invoiceClient.Update(updateRequest);
        bool updateResponse = updateResponseResult.Match(
            invoice => { return invoice; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(updateResponse).IsTrue();

        var getResponseResult = await _invoiceClient.Get(updateRequest.Id);
        InvoiceResponse? getResponse = getResponseResult.Match(
            invoice => { return invoice; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(getResponse).IsNotNull();
        await Assert.That(getResponse!.Id).IsEqualTo(updateRequest.Id);
        await Assert.That(getResponse!.Name).IsEqualTo(updateRequest.Name);
        await Assert.That(getResponse!.Quantity).IsEquivalentTo(updateRequest.Quantity);
        await Assert.That(getResponse!.Price).IsEquivalentTo(updateRequest.Price);*/
    }

    [Test]
    [MethodDataSource(typeof(InvoiceTestDataSources), nameof(InvoiceTestDataSources.UpdateDataInvalid))]
    [DependsOn(nameof(InvoiceAdd_Valid_Success), [typeof(TestCaseModel<InvoiceModel>)])]
    [DisplayName("Invoice update, invalid data: $testCase")]
    public async Task InvoiceUpdate_InValid_Fail(TestCaseModel<InvoiceModel> testCase)
    {/*
        InvoiceModel invoice = testCase.Data;
        InvoiceUpdateRequest updateRequest = new()
        {
            Id = invoice.Id == default ? GetInvoiceFromTest(InvoiceTestDataSources.Names().First()).Id : invoice.Id,
            Name = invoice.Name,
            Quantity = invoice.Quantity,
            Price = invoice.Price
        };

        var updateResponseResult = await _invoiceClient.Update(updateRequest);
        ErrorModel error = updateResponseResult.Match(
            invoice => { throw new Exception(invoice.ToString()); },
            error => { return error; }
        );

        if (invoice.Id != default)
            testCase.Error!.ExtendedMessage = $"Invoice invoice:{updateRequest.Id} not found";

        await testCase.CheckErrors(error);*/
    }

    public static async Task Invoice_Delete_All()
    {
        var listResponseResult = await _invoiceClient.Get();
        InvoiceListResponse listResponse = listResponseResult.Match(
            invoice => { return invoice; },
            error => { throw new Exception(error.ToString()); }
        );

        foreach (var invoice in listResponse.Invoices)
        {
            var deleteResponseResult = await _invoiceClient.Delete(invoice.Id);
            bool deleteResponse = deleteResponseResult.Match(
                invoice => { return invoice; },
                error => { throw new Exception(error.ToString()); }
            );

            await Assert.That(deleteResponse).IsTrue();
        }
    }

    [Test]
    [After(Class)]
    public static async Task InvoiceDelete_AfterAll_Success()
    {
        await Invoice_Delete_All();
    }
}
