using BillerContracts;
using BillerContracts.Responses;
using Newtonsoft.Json;

namespace IntegrationTests.ApiClients;

/// <summary>
/// Holds the error response of one call made through <see cref="ApiError.Capture"/>.
/// </summary>
public class ErrorCapture
{
    public ErrorModel? Error { get; set; }
}

/// <summary>
/// Sits in every BillerContracts client's HTTP pipeline and turns a non-success response into an
/// <see cref="ErrorModel"/> (status code, message, extended message) for the capture of the current call.
/// The clients' <c>ClientAPIException</c> only carries a text message.
/// </summary>
public class ErrorResponseRecorder : DelegatingHandler
{
    public static readonly AsyncLocal<ErrorCapture?> Current = new();

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

        ErrorCapture? capture = Current.Value;
        if (capture is null || response.IsSuccessStatusCode)
            return response;

        await response.Content.LoadIntoBufferAsync();
        string body = await response.Content.ReadAsStringAsync(cancellationToken);
        capture.Error = DecodeError(body, response);

        return response;
    }

    private static ErrorModel DecodeError(string body, HttpResponseMessage response)
    {
        ErrorModel error = new()
        {
            StatusCode = response.StatusCode
        };

        try
        {
            ErrorResponse? errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(body);

            if (errorResponse is null)
                error.Message = body;
            else
            {
                error.Message = errorResponse.Message;
                error.ExtendedMessage = errorResponse.ExtendedMessage;
            }
        }
        catch (Exception)
        {
            error.Message = body;
        }

        return error;
    }
}
