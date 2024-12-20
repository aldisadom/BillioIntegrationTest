﻿using Common;
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
using IntegrationTests.Helpers;
using IntegrationTests.Interfaces;
using IntegrationTests.Models;
using LanguageExt.Pipes;
using Microsoft.VisualBasic;
using System.Net;
using System.Xml.Linq;
using static IntegrationTests.Program;

namespace BillioIntegrationTest.Tests;

public static class InvoiceTestDataSources
{
    public static IEnumerable<TestCaseModel<InvoiceModel>> AddData()
    {
        yield return new()
        {
            TestName = InvoiceKeys().First().ToString(),
            Data = new()
            {
                BucketKey = InvoiceKeys().First(),
                UserEmail = UserEmails().First(),
                SellerEmail = SellerNames().First() + UserEmails().First(),
                CustomerEmail = CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                Items =
                [
                    new (){
                        Name = ItemNames().First() + CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                        Quantity = 1,
                        Comments = "New"
                    },
                    new (){
                        Name = ItemNames().ToArray()[1] + CustomerNames().First() + SellerNames().First() + UserEmails().First(),
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
                UserEmail = UserEmails().First(),
                SellerEmail = SellerNames().First() + UserEmails().First(),
                CustomerEmail = CustomerNames().ToArray()[1] + SellerNames().First() + UserEmails().First(),
                Items =
                [
                    new (){
                        Name = ItemNames().First() + CustomerNames().ToArray()[1] + SellerNames().First() + UserEmails().First(),
                        Quantity = 3,
                    },
                    new (){
                        Name = ItemNames().First() + CustomerNames().ToArray()[1] + SellerNames().First() + UserEmails().First(),
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
                UserEmail = UserEmails().ToArray()[1],
                SellerEmail = SellerNames().First() + UserEmails().ToArray()[1],
                CustomerEmail = CustomerNames().First() + SellerNames().First() + UserEmails().ToArray()[1],
                Items =
                [
                    new (){
                        Name = ItemNames().First() + CustomerNames().First() + SellerNames().First() + UserEmails().ToArray()[1],
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
                UserEmail = UserEmails().ToArray()[1],
                SellerEmail = SellerNames().ToArray()[1] + UserEmails().ToArray()[1],
                CustomerEmail = CustomerNames().First() + SellerNames().ToArray()[1] + UserEmails().ToArray()[1],
                Items =
                [
                    new (){
                        Name = ItemNames().First() + CustomerNames().First() + SellerNames().ToArray()[1] + UserEmails().ToArray()[1],
                        Quantity = 7,
                    },
                    new (){
                        Name = ItemNames().ToArray()[1] + CustomerNames().First() + SellerNames().ToArray()[1] + UserEmails().ToArray()[1],
                        Quantity = 8,
                    },
                ],
                DueDate = new DateOnly(2024, 1, 1).AddDays(5),
                CreatedDate = new DateOnly(2024, 1, 1),
            }
        };
    }
    public static IEnumerable<TestCaseError<InvoiceModel>> AddDataInvalid()
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
                SellerEmail = SellerNames().First() + UserEmails().First(),
                CustomerEmail = CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                Items =
                [
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
                UserEmail = UserEmails().First(),
                CustomerEmail = CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                Items =
                [
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
                UserEmail = UserEmails().First(),
                SellerEmail = SellerNames().First() + UserEmails().ToArray()[1],
                CustomerEmail = CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                Items =
                [
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
                UserEmail = UserEmails().First(),
                SellerEmail = SellerNames().First() + UserEmails().First(),
                Items =
                [
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
                UserEmail = UserEmails().First(),
                SellerEmail = SellerNames().First() + UserEmails().First(),
                CustomerEmail = CustomerNames().First() + SellerNames().First() + UserEmails().ToArray()[1],
                Items =
                [
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
                UserEmail = UserEmails().First(),
                SellerEmail = SellerNames().First() + UserEmails().First(),
                CustomerEmail = CustomerNames().First() + SellerNames().First() + UserEmails().First(),
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
                UserEmail = UserEmails().First(),
                SellerEmail = SellerNames().First() + UserEmails().First(),
                CustomerEmail = CustomerNames().First() + SellerNames().First() + UserEmails().First(),
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
                UserEmail = UserEmails().First(),
                SellerEmail = SellerNames().First() + UserEmails().First(),
                CustomerEmail = CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                Items =
                [
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
                    new (){
                        Name = ItemNames().ToArray()[1] + CustomerNames().ToArray()[1] + SellerNames().First() + UserEmails().ToArray()[1],
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
                UserEmail = UserEmails().First(),
                SellerEmail = SellerNames().First() + UserEmails().First(),
                CustomerEmail = CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                Items =
                [
                    new (){
                        Name = ItemNames().First() + CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                        Quantity = 10,
                        Comments = "New"
                    },
                    new (){
                        Name = ItemNames().ToArray()[1] + CustomerNames().First() + SellerNames().First() + UserEmails().First(),
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
                UserEmail = UserEmails().First(),
                SellerEmail = SellerNames().First() + UserEmails().First(),
                CustomerEmail = CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                Items =
                [
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
                UserEmail = UserEmails().First(),
                SellerEmail = SellerNames().First() + UserEmails().First(),
                CustomerEmail = CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                Items =
                [
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
                ],
                Comments = "Invoice com",
                DueDate = new DateOnly(2024, 1, 1).AddDays(-1),
                CreatedDate = new DateOnly(2024, 1, 1)
            }
        };
    }
    public static IEnumerable<TestCaseModel<InvoiceDetaisUpdateModel>> UpdateDetailsData()
    {
        yield return new()
        {
            TestName = "Update all details",
            Data = new()
            {
                BucketKey = InvoiceKeys().First(),
                Comments = "Updated invoice com",
                DueDate = new DateOnly(2026, 1, 1).AddDays(33),
                CreatedDate = new DateOnly(2024, 1, 1),
                InvoiceNumber = 99
            }
        };
        yield return new()
        {
            TestName = "Update all details and remove comment",
            Data = new()
            {
                BucketKey = InvoiceKeys().ToArray()[1],
                DueDate = new DateOnly(2027, 1, 1).AddDays(33),
                CreatedDate = new DateOnly(2027, 1, 1),
                InvoiceNumber = 699
            }
        };
    }
    public static IEnumerable<TestCaseError<InvoiceDetaisUpdateModel>> UpdateDataInvalid()
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
                CreatedDate = new DateOnly(2027, 1, 1),
                InvoiceNumber = 699
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
                DueDate = new DateOnly(2027, 1, 1).AddDays(33),
                InvoiceNumber = 699
            }
        };
        yield return new()
        {
            TestName = "No invoice number",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = "Please specify invoice number >= 0"
            },
            Data = new()
            {
                BucketKey = InvoiceKeys().First(),
                DueDate = new DateOnly(2027, 1, 1).AddDays(33),
                CreatedDate = new DateOnly(2027, 1, 1)
            }
        };
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
    public static IEnumerable<string> InvoiceKeys()
    {
        yield return "Invoice 1";
        yield return "Invoice 2";
        yield return "Invoice 3";
        yield return "Invoice 4";
    }
}

public partial class Tests
{
    private static readonly InvoiceClient _invoiceClient = new();
    public static T GetInvoicePrepFromTest<T>(string name)
    {
        return TestDataHelper.GetData<T>(name, nameof(Invoice_Prepare_Data));
    }
    public static InvoiceResponse GetInvoiceFromTest(string createDate)
    {
        return TestDataHelper.GetData<InvoiceResponse>(createDate, nameof(InvoiceAdd_Valid_Success));
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
            InvoiceName = invoice.Customer!.InvoiceName,
            InvoiceNumber = invoice.Customer!.InvoiceNumber
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
                    CompanyNumber = "Seller-12122",
                    Street = "Sel_Street",
                    City = "Sel_City",
                    State = "Sel_State",
                    Phone = "Sel_Phone",
                    BankName = "Sel_BankName",
                    BankNumber = "Sel_BankNumber"
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
                        CompanyNumber = "CUSTOMER-12122",
                        Street = "CUS_Street",
                        City = "CUS_City",
                        State = "CUS_State",
                        Phone = "CUS_Phone",
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
    //[ParallelLimiter<SingleLimiter>]
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

        TestContext.Current!.ObjectBag.Add(invoiceModel.BucketKey, getResponse);
    }

    [Test]
    [DependsOn(nameof(InvoiceAdd_Valid_Success))]
    [DisplayName("Invoice add multiple invoices to same customer")]
    public async Task InvoiceAddMultipleToSameCustomer_Valid_Success()
    {
        string userEmail = InvoiceTestDataSources.UserEmails().ToArray()[1];
        string sellerEmail = InvoiceTestDataSources.SellerNames().ToArray()[1] + InvoiceTestDataSources.UserEmails().ToArray()[1];
        string customerEmail = InvoiceTestDataSources.CustomerNames().ToArray()[1] + InvoiceTestDataSources.SellerNames().ToArray()[1] + InvoiceTestDataSources.UserEmails().ToArray()[1];
        string itemName = InvoiceTestDataSources.ItemNames().First() + InvoiceTestDataSources.CustomerNames().ToArray()[1] + InvoiceTestDataSources.SellerNames().ToArray()[1] + InvoiceTestDataSources.UserEmails().ToArray()[1];

        var user = GetInvoicePrepFromTest<UserResponse>(userEmail);
        var seller = GetInvoicePrepFromTest<SellerResponse>(sellerEmail);
        var customer = GetInvoicePrepFromTest<CustomerResponse>(customerEmail);
        var itemList = new List<InvoiceItemRequest>()
        {
            new()
            { 
                Id = GetInvoicePrepFromTest<ItemResponse>(itemName).Id,
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
        await Assert.That(getResponse!.InvoiceNumber).IsEquivalentTo(1);

        addRequest.Comments = "Invoice number should be 2";
        addResponseResult = await _invoiceClient.Add(addRequest);
        addResponse = addResponseResult.Match(
            invoice => { return invoice; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(addResponse.Id).IsNotNull();

        getResponseResult = await _invoiceClient.Get(addResponse.Id);
        getResponse = getResponseResult.Match(
            invoice => { return invoice; },
            error => { throw new Exception(error.ToString()); }
        );
        await Assert.That(getResponse).IsNotNull();
        await Assert.That(getResponse!.InvoiceNumber).IsEquivalentTo(2);
    }

    [Test]
    [DependsOn(nameof(InvoiceAddMultipleToSameCustomer_Valid_Success))]
    [MethodDataSource(typeof(InvoiceTestDataSources), nameof(InvoiceTestDataSources.AddDataInvalid))]
    [DisplayName("Invoice add, invalid data: $testCase")]
    public async Task InvoiceAdd_InValid_Fail(TestCaseError<InvoiceModel> testCase)
    {
        InvoiceModel invoiceModel = testCase.Data;

        var itemList = invoiceModel.Items?.Select(i => new InvoiceItemRequest()
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
            Items = itemList!,
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
        string userEmail = InvoiceTestDataSources.UserEmails().ToArray()[1];
        string sellerEmail = InvoiceTestDataSources.SellerNames().ToArray()[1] + InvoiceTestDataSources.UserEmails().ToArray()[1];
        string customerEmail = InvoiceTestDataSources.CustomerNames().ToArray()[1] + InvoiceTestDataSources.SellerNames().ToArray()[1] + InvoiceTestDataSources.UserEmails().ToArray()[1];
        string itemName = InvoiceTestDataSources.ItemNames().First() + InvoiceTestDataSources.CustomerNames().ToArray()[1] + InvoiceTestDataSources.SellerNames().ToArray()[1] + InvoiceTestDataSources.UserEmails().ToArray()[1];

        var user = GetInvoicePrepFromTest<UserResponse>(userEmail);
        var seller = GetInvoicePrepFromTest<SellerResponse>(sellerEmail);
        var customer = GetInvoicePrepFromTest<CustomerResponse>(customerEmail);
        var itemList = new List<InvoiceItemRequest>()
        {
            new()
            {
                Id = GetInvoicePrepFromTest<ItemResponse>(itemName).Id,
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

        var addResponseResult = await _invoiceClient.Add(addRequest);
        AddResponse addResponse = addResponseResult.Match(
            invoice => { return invoice; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(addResponse.Id).IsNotNull();

        var deleteResponseResult = await _invoiceClient.Delete(addResponse.Id);
        bool deleteResponse = deleteResponseResult.Match(
            invoice => { return invoice; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(deleteResponse).IsTrue();

        var getResponseResult = await _invoiceClient.Get(addResponse.Id);
        ErrorModel error = getResponseResult.Match(
            invoice => { throw new Exception(invoice!.ToString()); },
            error => { return error; }
        );
        ErrorModel expectedError = new("Entity not found", $"Invoice:{addResponse.Id} not found", HttpStatusCode.NotFound);
        await expectedError.CheckErrors(error);
    }

    [Test]
    //[ParallelLimiter<SingleLimiter>] // brakes all tests
    [DependsOn(nameof(InvoiceAddMultipleToSameCustomer_Valid_Success))]
    [MethodDataSource(typeof(InvoiceTestDataSources), nameof(InvoiceTestDataSources.UpdateDetailsData))]
    [DisplayName("Invoice update, details valid data: $testCase")]
    public async Task InvoiceDetailsUpdate_Valid_Success(TestCaseModel<InvoiceDetaisUpdateModel> testCase)
    {
        InvoiceDetaisUpdateModel invoiceModel = testCase.Data;

        var invoiceFromBucket = GetInvoiceFromTest(invoiceModel.BucketKey);

        (SellerUpdateRequest seller, CustomerUpdateRequest customer, List<InvoiceItemUpdateRequest> items) = ExtractUpdateFromInvoice(invoiceFromBucket);

        InvoiceUpdateRequest updateRequest = new()
        {
            Id = invoiceFromBucket.Id,
            Comments = invoiceModel.Comments,
            DueDate = invoiceModel.DueDate,
            CreatedDate = invoiceModel.CreatedDate,
            InvoiceNumber = invoiceModel.InvoiceNumber,
            Seller = seller,
            Customer = customer,
            Items = items,
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
        await Assert.That(getResponse!.CreatedDate).IsEqualTo(updateRequest.CreatedDate);
        await Assert.That(getResponse!.DueDate).IsEqualTo(updateRequest.DueDate);
        await Assert.That(getResponse!.Comments).IsEqualTo(updateRequest.Comments);
        await Assert.That(getResponse!.InvoiceNumber).IsEquivalentTo(updateRequest.InvoiceNumber);
    }

    [Test]
    [MethodDataSource(typeof(InvoiceTestDataSources), nameof(InvoiceTestDataSources.UpdateDataInvalid))]
    [DependsOn(nameof(InvoiceDetailsUpdate_Valid_Success))]
    [DisplayName("Invoice update, details invalid data: $testCase")]
    public async Task InvoiceDetailsUpdate_Invalid_Fail(TestCaseError<InvoiceDetaisUpdateModel> testCase)
    {
        InvoiceDetaisUpdateModel invoiceModel = testCase.Data;

        var invoiceFromBucket = GetInvoiceFromTest(invoiceModel.BucketKey);

        (SellerUpdateRequest seller, CustomerUpdateRequest customer, List<InvoiceItemUpdateRequest> items) = ExtractUpdateFromInvoice(invoiceFromBucket);

        InvoiceUpdateRequest updateRequest = new()
        {
            Id = invoiceFromBucket.Id,
            Comments = invoiceModel.Comments,
            DueDate = invoiceModel.DueDate,
            CreatedDate = invoiceModel.CreatedDate,
            InvoiceNumber = invoiceModel.InvoiceNumber,
            Seller = seller,
            Customer = customer,
            Items = items,
        };

        var updateResponseResult = await _invoiceClient.Update(updateRequest);
        ErrorModel error = updateResponseResult.Match(
            seller => { throw new Exception(seller.ToString()); },
            error => { return error; }
        );

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
            InvoiceNumber = invoiceFromBucket.InvoiceNumber,
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
            InvoiceNumber = invoiceFromBucket.InvoiceNumber,
            Seller = sellerUpdate,
            Customer = customer,
            Items = items,
        };

        var updateResponseResult = await _invoiceClient.Update(updateRequest);
        ErrorModel error = updateResponseResult.Match(
            seller => { throw new Exception(seller.ToString()); },
            error => { return error; }
        );

        if (sellerModel.Id != default)
            testCase.Error = new("Validation failure", $"Seller id {updateRequest.Seller.Id} is invalid for user id {invoiceFromBucket.UserId}", HttpStatusCode.BadRequest);

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
            InvoiceNumber = invoiceFromBucket.InvoiceNumber,
            Seller = seller,
            Customer = new ()
            {
                Id = customer.Id,
                Email = customer.Email + "_new",
                CompanyName = customer.CompanyName + "_new",
                CompanyNumber = customer.CompanyNumber + "_new",
                Street = customer.Street + "_new",
                City = customer.City + "_new",
                State = customer.State + "_new",
                Phone = customer.Phone + "_new",
                InvoiceName = customer.InvoiceName + "_new",
                InvoiceNumber = customer.InvoiceNumber + 99
            },
            Items = items,
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
        await Assert.That(getResponse!.Customer).IsNotNull();
        await Assert.That(getResponse.Customer!.Id).IsEqualTo(updateRequest.Customer.Id);
        await Assert.That(getResponse.Customer!.Email).IsEqualTo(updateRequest.Customer.Email);
        await Assert.That(getResponse.Customer!.CompanyName).IsEqualTo(updateRequest.Customer.CompanyName);
        await Assert.That(getResponse.Customer!.Street).IsEqualTo(updateRequest.Customer.Street);
        await Assert.That(getResponse.Customer!.City).IsEqualTo(updateRequest.Customer.City);
        await Assert.That(getResponse.Customer!.State).IsEqualTo(updateRequest.Customer.State);
        await Assert.That(getResponse.Customer!.Phone).IsEqualTo(updateRequest.Customer.Phone);
        await Assert.That(getResponse.Customer!.InvoiceName).IsEqualTo(updateRequest.Customer.InvoiceName);
        await Assert.That(getResponse.Customer!.InvoiceNumber).IsEqualTo(updateRequest.Customer.InvoiceNumber);
    }

    [Test]
    [MethodDataSource(typeof(CustomerTestDataSources), nameof(CustomerTestDataSources.UpdateDataInvalid))]
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
            InvoiceName = customerModel.InvoiceName,
            InvoiceNumber = customerModel.InvoiceNumber
        };

        InvoiceUpdateRequest updateRequest = new()
        {
            Id = invoiceFromBucket.Id,
            Comments = invoiceFromBucket.Comments,
            DueDate = invoiceFromBucket.DueDate,
            CreatedDate = invoiceFromBucket.CreatedDate,
            InvoiceNumber = invoiceFromBucket.InvoiceNumber,
            Seller = seller,
            Customer = customerUpdate,
            Items = items,
        };

        var updateResponseResult = await _invoiceClient.Update(updateRequest);
        ErrorModel error = updateResponseResult.Match(
            seller => { throw new Exception(seller.ToString()); },
            error => { return error; }
        );

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

        List<InvoiceItemUpdateRequest> updatedItems = items.Select(i=> new InvoiceItemUpdateRequest()
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
            InvoiceNumber = invoiceFromBucket.InvoiceNumber,
            Seller = seller,
            Customer = customer,
            Items = updatedItems,
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
                Id = itemModel.Id == default ? seller.Id : itemModel.Id,
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
            InvoiceNumber = invoiceFromBucket.InvoiceNumber,
            Seller = seller,
            Customer = customer,
            Items = updatedItems,
        };

        var updateResponseResult = await _invoiceClient.Update(updateRequest);
        ErrorModel error = updateResponseResult.Match(
            seller => { throw new Exception(seller.ToString()); },
            error => { return error; }
        );
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

        var deleteResponseResult = await _invoiceClient.Delete(id);
        ErrorModel error = deleteResponseResult.Match(
            invoice => { throw new Exception(invoice.ToString()); },
            error => { return error; }
        );

        ErrorModel expectedError = new("Entity not found", $"Invoice:{id} not found", HttpStatusCode.NotFound);
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

        var listResponseResult = await _invoiceClient.Get(request);
        InvoiceListResponse listResponse = listResponseResult.Match(
            invoice => { return invoice; },
            error => { throw new Exception(error.ToString()); }
        );

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

        var listResponseResult = await _invoiceClient.Get(request);
        InvoiceListResponse listResponse = listResponseResult.Match(
            invoice => { return invoice; },
            error => { throw new Exception(error.ToString()); }
        );

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

        var listResponseResult = await _invoiceClient.Get(request);
        InvoiceListResponse listResponse = listResponseResult.Match(
            invoice => { return invoice; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(listResponse.Invoices)
            .IsNotNull()
            .And.HasCount().EqualTo(2);

        foreach (var invoice in listResponse.Invoices)
            await Assert.That(invoice.UserId).IsEquivalentTo(request.UserId!.Value);
    }

    /*
        UserEmail = UserEmails().First(),
                SellerEmail = SellerNames().First() + UserEmails().First(),
                CustomerEmail = CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                Items =
                [
                    new (){
                        Name = ItemNames().First() + CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                        Quantity = 1,
                        Comments = "New"
                    },
                    new (){
                        Name = ItemNames().ToArray()[1] + CustomerNames().First() + SellerNames().First() + UserEmails().First(),
                        Quantity = 2,
                        Comments = "Old"
                    },
                ],
                Comments = "Invoice com",
                DueDate = DateOnly.FromDateTime(DateTime.Now),
                CreatedDate = DateOnly.FromDateTime(DateTime.Now),
            */
    [Test]
    [DependsOn(nameof(InvoiceSellerUpdate_Valid_Success))]
    [DependsOn(nameof(InvoiceCustomerUpdate_Valid_Success))]
    [DependsOn(nameof(InvoiceItemUpdate_Valid_Success))]
    public async Task InvoiceUpdateChangeItems_Valid_Success()
    {
        var invoiceFromBucket = GetInvoiceFromTest(InvoiceTestDataSources.InvoiceKeys().First());

        (SellerUpdateRequest seller, CustomerUpdateRequest customer, List<InvoiceItemUpdateRequest> items) = ExtractUpdateFromInvoice(invoiceFromBucket);
                
        var getResponseResult = await _invoiceClient.Get(invoiceFromBucket.Id);
        InvoiceResponse? getResponse = getResponseResult.Match(
            invoice => { return invoice; },
            error => { throw new Exception(error.ToString()); }
        );

        ItemResponse item1 = GetInvoicePrepFromTest<ItemResponse>(InvoiceTestDataSources.ItemNames().First() + InvoiceTestDataSources.CustomerNames().First() + InvoiceTestDataSources.SellerNames().First() + InvoiceTestDataSources.UserEmails().First());
        ItemResponse item2 = GetInvoicePrepFromTest<ItemResponse>(InvoiceTestDataSources.ItemNames().ToArray()[1] + InvoiceTestDataSources.CustomerNames().First() + InvoiceTestDataSources.SellerNames().First() + InvoiceTestDataSources.UserEmails().First());

        await Assert.That(getResponse!.Items![0].Id).IsEquivalentTo(item1.Id);
        await Assert.That(getResponse!.Items![1].Id).IsEquivalentTo(item2.Id);

        ItemResponse itemNew = item2;
        List<InvoiceItemUpdateRequest> updatedItems = new ()
        {
            new InvoiceItemUpdateRequest()
            {
                Id = itemNew.Id,
                Name = itemNew.Name,
                Quantity = itemNew.Quantity,
                Price = itemNew.Price
            }
        };

        InvoiceUpdateRequest updateRequest = new()
        {
            Id = invoiceFromBucket.Id,
            Comments = invoiceFromBucket.Comments,
            DueDate = invoiceFromBucket.DueDate,
            CreatedDate = invoiceFromBucket.CreatedDate,
            InvoiceNumber = invoiceFromBucket.InvoiceNumber,
            Seller = seller,
            Customer = customer,
            Items = updatedItems,
        };

        var updateResponseResult = await _invoiceClient.Update(updateRequest);
        bool updateResponse = updateResponseResult.Match(
            invoice => { return invoice; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(updateResponse).IsTrue();

        getResponseResult = await _invoiceClient.Get(updateRequest.Id);
        getResponse = getResponseResult.Match(
            invoice => { return invoice; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(getResponse).IsNotNull();
        await Assert.That(getResponse!.Items)
            .IsNotNull()
            .And.HasCount().EqualTo(1);

        await Assert.That(getResponse!.Items![0].Id).IsEquivalentTo(itemNew.Id);
    }

    [Test]
    [DependsOn(nameof(InvoiceUpdateChangeItems_Valid_Success))]
    public async Task InvoiceUpdateChangeCustomer_Valid_Success()
    {
        var invoiceFromBucket = GetInvoiceFromTest(InvoiceTestDataSources.InvoiceKeys().First());

        (SellerUpdateRequest seller, CustomerUpdateRequest customer, List<InvoiceItemUpdateRequest> items) = ExtractUpdateFromInvoice(invoiceFromBucket);

        var getResponseResult = await _invoiceClient.Get(invoiceFromBucket.Id);
        InvoiceResponse? getResponse = getResponseResult.Match(
            invoice => { return invoice; },
            error => { throw new Exception(error.ToString()); }
        );

        CustomerResponse customerFromTest = GetInvoicePrepFromTest<CustomerResponse>(InvoiceTestDataSources.CustomerNames().First() + InvoiceTestDataSources.SellerNames().First() + InvoiceTestDataSources.UserEmails().First());
        
        await Assert.That(getResponse!.Customer!.Id).IsEquivalentTo(customerFromTest.Id);

        CustomerResponse customerNew = GetInvoicePrepFromTest<CustomerResponse>(InvoiceTestDataSources.CustomerNames().ToArray()[1] + InvoiceTestDataSources.SellerNames().First() + InvoiceTestDataSources.UserEmails().First());
        ItemResponse itemNew = GetInvoicePrepFromTest<ItemResponse>(InvoiceTestDataSources.ItemNames().First() + InvoiceTestDataSources.CustomerNames().ToArray()[1] + InvoiceTestDataSources.SellerNames().First() + InvoiceTestDataSources.UserEmails().First());

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
            InvoiceName = customerNew.InvoiceName,
            InvoiceNumber = customerNew.InvoiceNumber
        };

        InvoiceUpdateRequest updateRequest = new()
        {
            Id = invoiceFromBucket.Id,
            Comments = invoiceFromBucket.Comments,
            DueDate = invoiceFromBucket.DueDate,
            CreatedDate = invoiceFromBucket.CreatedDate,
            InvoiceNumber = invoiceFromBucket.InvoiceNumber,
            Seller = seller,
            Customer = updatedCustomer,
            Items = updatedItems,
        };

        var updateResponseResult = await _invoiceClient.Update(updateRequest);
        bool updateResponse = updateResponseResult.Match(
            invoice => { return invoice; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(updateResponse).IsTrue();

        getResponseResult = await _invoiceClient.Get(updateRequest.Id);
        getResponse = getResponseResult.Match(
            invoice => { return invoice; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(getResponse).IsNotNull();
        await Assert.That(getResponse!.Customer!.Id).IsEquivalentTo(customerNew.Id);
        await Assert.That(getResponse!.Items)
            .IsNotNull()
            .And.HasCount().EqualTo(1);

        await Assert.That(getResponse!.Items![0].Id).IsEquivalentTo(itemNew.Id);
    }
    
    [Test]
    [DependsOn(nameof(InvoiceUpdateChangeCustomer_Valid_Success))]
    public async Task InvoiceUpdateChangeSeller_Valid_Success()
    {
        var invoiceFromBucket = GetInvoiceFromTest(InvoiceTestDataSources.InvoiceKeys().First());

        (SellerUpdateRequest seller, CustomerUpdateRequest customer, List<InvoiceItemUpdateRequest> items) = ExtractUpdateFromInvoice(invoiceFromBucket);

        var getResponseResult = await _invoiceClient.Get(invoiceFromBucket.Id);
        InvoiceResponse? getResponse = getResponseResult.Match(
            invoice => { return invoice; },
            error => { throw new Exception(error.ToString()); }
        );

        SellerResponse sellerFromTest = GetInvoicePrepFromTest<SellerResponse>(InvoiceTestDataSources.SellerNames().First() + InvoiceTestDataSources.UserEmails().First());

        await Assert.That(getResponse!.Seller!.Id).IsEquivalentTo(sellerFromTest.Id);

        SellerResponse sellerNew = GetInvoicePrepFromTest<SellerResponse>(InvoiceTestDataSources.SellerNames().ToArray()[1] + InvoiceTestDataSources.UserEmails().First());
        CustomerResponse customerNew = GetInvoicePrepFromTest<CustomerResponse>(InvoiceTestDataSources.CustomerNames().First() + InvoiceTestDataSources.SellerNames().ToArray()[1] + InvoiceTestDataSources.UserEmails().First());
        ItemResponse itemNew = GetInvoicePrepFromTest<ItemResponse>(InvoiceTestDataSources.ItemNames().First() + InvoiceTestDataSources.CustomerNames().First() + InvoiceTestDataSources.SellerNames().ToArray()[1] + InvoiceTestDataSources.UserEmails().First());

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
            InvoiceName = customerNew.InvoiceName,
            InvoiceNumber = customerNew.InvoiceNumber
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
            InvoiceNumber = invoiceFromBucket.InvoiceNumber,
            Seller = updatedSeller,
            Customer = updatedCustomer,
            Items = updatedItems,
        };

        var updateResponseResult = await _invoiceClient.Update(updateRequest);
        bool updateResponse = updateResponseResult.Match(
            invoice => { return invoice; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(updateResponse).IsTrue();

        getResponseResult = await _invoiceClient.Get(updateRequest.Id);
        getResponse = getResponseResult.Match(
            invoice => { return invoice; },
            error => { throw new Exception(error.ToString()); }
        );

        await Assert.That(getResponse).IsNotNull();
        await Assert.That(getResponse!.Seller!.Id).IsEquivalentTo(sellerNew.Id);
        await Assert.That(getResponse!.Customer!.Id).IsEquivalentTo(customerNew.Id);
        await Assert.That(getResponse!.Items)
            .IsNotNull()
            .And.HasCount().EqualTo(1);

        await Assert.That(getResponse!.Items![0].Id).IsEquivalentTo(itemNew.Id);
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
