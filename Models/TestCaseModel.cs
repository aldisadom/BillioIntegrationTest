using BillioIntegrationTest.Contracts.Responses;
using LanguageExt.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUnit.Assertions.Extensions;

namespace BillioIntegrationTest.Models;

public class TestCaseModel<T>
{
    public string TestCase = string.Empty;
    public required T Data { get; set; }
    public ErrorModel? Error { get; set; }

    public async Task CheckErrors(ErrorModel inputError) => await Error!.CheckErrors(inputError);

    public override string ToString() => TestCase;
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