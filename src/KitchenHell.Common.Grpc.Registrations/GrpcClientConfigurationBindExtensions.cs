using Microsoft.Extensions.Configuration;

namespace KitchenHell.Common.Grpc.Registrations;

/// <summary>
///   Helper method to bind configuration to grpc client options object.
/// </summary>
public static class GrpcClientConfigurationBindExtensions
{
  /// <summary>
  ///   Binds <see cref="IConfiguration" /> section onto <paramref name="options" /> object.
  /// </summary>
  /// <param name="configuration">Configuration source.</param>
  /// <param name="grpcClientName">Name of a Grpc client</param>
  /// <param name="options">Options object to bind onto</param>
  public static void BindGrpcClientOptions(
    this IConfiguration configuration,
    string grpcClientName,
    object options
  )
  {
    var name = grpcClientName.TrimEnd("Client");
    configuration
      .GetSection($"Clients:{name}")
      .Bind(options);
  }
}
