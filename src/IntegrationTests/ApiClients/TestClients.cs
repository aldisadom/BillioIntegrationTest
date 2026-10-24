using BillerContracts.Clients;
using BillerContracts.Clients.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests.ApiClients;

/// <summary>
/// Builds the BillerContracts clients once. URLs come from the <c>ClientsOptions</c> section of
/// <c>appsettings.json</c>; environment variables such as <c>ClientsOptions__UserUrl</c> override them.
/// </summary>
public static class TestClients
{
    private static readonly ServiceProvider _provider = Build();

    public static IUserClient User => _provider.GetRequiredService<IUserClient>();
    public static ISellerClient Seller => _provider.GetRequiredService<ISellerClient>();
    public static ICustomerClient Customer => _provider.GetRequiredService<ICustomerClient>();
    public static IItemClient Item => _provider.GetRequiredService<IItemClient>();
    public static IInvoiceClient Invoice => _provider.GetRequiredService<IInvoiceClient>();

    private static ServiceProvider Build()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        ServiceCollection services = new();
        services.AddBillerClients(configuration.GetSection("ClientsOptions"));
        services.ConfigureHttpClientDefaults(builder => builder.AddHttpMessageHandler(() => new ErrorResponseRecorder()));

        return services.BuildServiceProvider();
    }
}
