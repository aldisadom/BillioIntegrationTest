namespace BillioIntegrationTest.Tests;


public class TestPreparation
{
    private static async Task DeleteAllData()
    {
        await Tests.Invoice_Delete_All();
        await Tests.Item_Delete_All();
        await Tests.Customer_Delete_All();
        await Tests.Seller_Delete_All();
        await Tests.User_Delete_All();
    }

    [Before(Assembly)]
    public static async Task PrepareTestEnvironment()
    {
        await DeleteAllData();
    }

    [After(TestSession)]
    public static async Task DeleteAfterAll()
    {
        await DeleteAllData();
    }
}
