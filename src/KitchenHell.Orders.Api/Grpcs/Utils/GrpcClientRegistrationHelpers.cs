namespace KitchenHell.Orders.Api.Grpcs.Utils;

/// <summary>
/// Helper methods for grpc client registration.
/// </summary>
public static class GrpcClientRegistrationHelpers
{
    /// <summary>
    /// Registers named Grpc client and applies configuration on it.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="clientName"></param>
    /// <typeparam name="TClient"></typeparam>
    /// <returns></returns>
    public static IHttpClientBuilder AddConfiguredGrpcClient<TClient>(
        this IServiceCollection services,
        string clientName
    )
        where TClient : class
    {
        return services
            .AddGrpcClient<TClient>(
                (sp, o) =>
                {
                    sp.GetRequiredService<IConfiguration>()
                        .BindGrpcClientOptions(clientName, o);
                })
            .ConfigureChannel(
                (sp, o) =>
                {
                    sp.GetRequiredService<IConfiguration>()
                        .BindGrpcClientOptions(clientName, o);
                });
    }
}