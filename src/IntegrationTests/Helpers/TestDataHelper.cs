using TUnit.Core.Extensions;

namespace IntegrationTests.Helpers;

public class TestDataHelper
{
    public static T GetData<T>(string key, string className)
    {
        var addToBagTestContext = TestContext.Current!.GetTests(className);

        foreach (var bag in addToBagTestContext)
        {
            try
            {
                var item = bag.ObjectBag[key];
                if (item is null || item is not T)
                    continue;

                return (T)item;
            }
            catch (Exception ex) 
            {
                throw new Exception($"{typeof(T)} get/parse fail with message: {ex.Message}");
            }
        }

        throw new Exception($"{typeof(T)} not found in test data: {key}");
    }
}