using KitchenHell.ActivityGenerators;
using KitchenHell.Common.Web;

var app = WebApplication.CreateBuilder(args)
  .Configure()
  .Build()
  .Configure();

await app.RunWithConsoleCancellationAsync();
