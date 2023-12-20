var app = WebApplication.CreateBuilder(args)
    .Configure()
    .Build()
    .Configure();

await app.MigrateDatabasesAsync();
await app.RunWithConsoleCancellationAsync();
