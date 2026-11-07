using BillerContracts.Enums;

namespace BillioIntegrationTest.Tests;

public partial class Tests
{
    // Rejected by model binding before Biller's validators run, so the message is ASP.NET's
    private const PageSize UndefinedPageSize = (PageSize)7;
    private const string UndefinedPageSizeMessage = "The value '7' is invalid.";
}
