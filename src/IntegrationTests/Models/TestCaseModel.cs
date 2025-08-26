using Common;

namespace IntegrationTests.Models;

public class TestCaseBase
{
    public string TestName = string.Empty;
    public override string ToString() => TestName;
}

public class TestCaseModel<T> : TestCaseBase
{
    public required T Data { get; set; }
}

public class TestCaseError<T> : TestCaseModel<T>
{
    public required ErrorModel Error { get; set; }

    public async Task CheckErrors(ErrorModel inputError) => await Error!.CheckErrors(inputError);
}

public static class ErrorModelExtension
{
    public static async Task CheckErrors(this ErrorModel error, ErrorModel inputError)
    {
        await Assert.That(error).IsNotNull();
        await Assert.That(inputError).IsNotNull();

        await Assert.That(inputError.StatusCode).IsEquivalentTo(error!.StatusCode);
        await Assert.That(inputError.Message).IsEqualTo(error!.Message);
        await Assert.That(inputError.ExtendedMessage).IsEqualTo(error!.ExtendedMessage);
    }
}