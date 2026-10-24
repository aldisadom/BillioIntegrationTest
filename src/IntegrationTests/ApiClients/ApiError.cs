using BillerContracts;
using BillerContracts.Clients;

namespace IntegrationTests.ApiClients;

public static class ApiError
{
    /// <summary>
    /// Runs a client call that must fail and returns the error the API sent back.
    /// Throws when the call succeeds or fails without an error response.
    /// </summary>
    public static async Task<ErrorModel> Capture(Func<Task> call)
    {
        ErrorCapture capture = new();
        ErrorResponseRecorder.Current.Value = capture;

        try
        {
            await call();
        }
        catch (ClientAPIException exception)
        {
            return capture.Error
                ?? throw new Exception($"The call failed without an error response: {exception.Message}", exception);
        }
        finally
        {
            ErrorResponseRecorder.Current.Value = null;
        }

        throw new Exception("The call succeeded, but an error was expected");
    }
}
