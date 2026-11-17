using BillerContracts;
using System.Net;

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

public static class PdfCharacterCases
{
    // An invalid case for one field Biller's PDF character rule (DrawableOnPdf) is on: newData() is
    // valid, addCharacter gives that one field 🐉. field is the name FluentValidation puts in the
    // message. Each call of the returned Func builds its own data, so each test gets its own copy.
    public static Func<TestCaseError<T>> For<T>(string field, Func<T> newData, Action<T> addCharacter) => () =>
    {
        TestCaseError<T> testCase = new()
        {
            TestName = $"{field} with a character the PDF can't show",
            Error = new ErrorModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Validation failure",
                ExtendedMessage = $"Please use only characters the invoice PDF can show in {field}. Can't show: 🐉 (U+1F409)"
            },
            Data = newData()
        };
        addCharacter(testCase.Data);
        return testCase;
    };
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