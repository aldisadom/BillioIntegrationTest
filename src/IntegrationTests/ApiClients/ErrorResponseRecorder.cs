using BillerContracts;
using BillerContracts.Clients;
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
/// <see cref="ErrorModel"/> (status code, message, extended message). Inside <see cref="ApiError.Capture"/>
/// it hands that to the capture; outside it fails the call with it.
/// The clients' <c>ClientAPIException</c> only carries a text message.
/// </summary>
public class ErrorResponseRecorder : DelegatingHandler
{
    public static readonly AsyncLocal<ErrorCapture?> Current = new();

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

        if (response.IsSuccessStatusCode)
            return response;

        await response.Content.LoadIntoBufferAsync();
        string body = await response.Content.ReadAsStringAsync(cancellationToken);
        ErrorModel error = DecodeError(body, response);

        // A call outside ApiError.Capture must succeed. Fail it here with the whole error, because
        // the clients' exception text has no extended message.
        ErrorCapture? capture = Current.Value;
        if (capture is null)
        {
            response.Dispose();
            throw new ClientAPIException($"{request.Method} {request.RequestUri} failed with {error.StatusCode}, "
                + $"message: {error.Message}, extended message: {error.ExtendedMessage}");
        }

        capture.Error = error;
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
