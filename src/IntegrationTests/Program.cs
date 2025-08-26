using IntegrationTests.Models;

namespace IntegrationTests;

internal class Program { }

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
        var testCase = value as TestCaseBase;
        return testCase?.TestName ?? "TestName not provided";
    }
}