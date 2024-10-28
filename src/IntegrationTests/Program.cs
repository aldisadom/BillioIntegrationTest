using TUnit.Core.Interfaces;

namespace IntegrationTests;

internal class Program
{
    public class LoadTestParallelLimit : IParallelLimit
    {
        public int Limit => 10;
    }
}
