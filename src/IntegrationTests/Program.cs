using TUnit.Core.Interfaces;

namespace IntegrationTests;

internal class Program
{
    public record SingleLimiter : IParallelLimit
    {
        public int Limit => 1;
    }
}
