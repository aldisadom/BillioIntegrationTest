using BillerContracts.Enums;

namespace BillioIntegrationTest.Tests;

public partial class Tests
{
    // Biller's PagingRules: the largest page whose offset still fits in an int at the largest page size, PageSize.p50
    private const int MaxPage = int.MaxValue / 50;
    private static readonly string PageOutOfRangeMessage = $"Please specify page up to {MaxPage}";

    // Biller answers a page below 1 with page 1, and a page past the last one with the last page (page 1 with no rows)
    private static int LastPage(int totalCount, PageSize pageSize) => Math.Max(1, (totalCount + (int)pageSize - 1) / (int)pageSize);

    // Rejected by model binding before Biller's validators run, so the message is ASP.NET's
    private const PageSize UndefinedPageSize = (PageSize)7;
    private const string UndefinedPageSizeMessage = "The value '7' is invalid.";
}
