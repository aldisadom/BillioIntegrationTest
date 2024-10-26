using TUnit.Core.Interfaces;

namespace BillioIntegrationTest;

internal class Program
{
    public class LoadTestParallelLimit : IParallelLimit
    {
        public int Limit => 10;
    }
}
