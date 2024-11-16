using Contracts.Requests.User;
using IntegrationTests.Models;
using TUnit.Core.Interfaces;

namespace IntegrationTests;

internal class Program
{
    public record SingleLimiter : IParallelLimit
    {
        public int Limit => 1;
    }
    public record DoubleLimiter : IParallelLimit
    {
        public int Limit => 2;
    }
}

public class ArgumentFormatter : ArgumentDisplayFormatter
{
    public override bool CanHandle(object? value)
    {
        if (value is TestCaseBase)
            return true;

        return false;
    }

    public override string FormatValue(object? value)
    {
        var fixture = value as TestCaseBase;
        return fixture?.TestName ?? "TestName not provided";
    }
}