await WebApplication.CreateBuilder(args)
    .Configure()
    .Build()
    .Configure()
    .MigrateAndRunAsync();