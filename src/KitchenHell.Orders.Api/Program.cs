// This switch must be set before creating the GrpcChannel/HttpClient.

AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

await WebApplication.CreateBuilder(args)
    .Configure()
    .Build()
    .Configure()
    .MigrateAndRunAsync();