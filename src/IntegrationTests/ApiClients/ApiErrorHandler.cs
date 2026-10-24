using BillerContracts;
using BillerContracts.Clients;
using BillerContracts.Responses;
using Newtonsoft.Json;

namespace IntegrationTests.ApiClients;

/// <summary>
/// A failed API call, with the whole error the API sent back. The clients' own
/// <see cref="ClientAPIException"/> only carries the status code and message as text.
/// </summary>
public class ApiErrorException : ClientAPIException
{
    public ErrorModel Error { get; }

    public ApiErrorException(string message, ErrorModel error) : base(message)
    {
        Error = error;
    }
}

/// <summary>
/// Sits in every BillerContracts client's HTTP pipeline and turns a non-success response into an
/// <see cref="ApiErrorException"/> with the status code, message and extended message.
/// </summary>
public class ApiErrorHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

        if (response.IsSuccessStatusCode)
            return response;

        using (response)
        {
            string body = await response.Content.ReadAsStringAsync(cancellationToken);
            ErrorModel error = DecodeError(body, response);

            throw new ApiErrorException($"{request.Method} {request.RequestUri} failed with {error.StatusCode}, "
                + $"message: {error.Message}, extended message: {error.ExtendedMessage}", error);
        }
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
