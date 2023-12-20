using Microsoft.Extensions.Hosting;
using Serilog;

namespace KitchenHell.Common.Web;

public static class LoggingRegistrations
{
    public static IHostBuilder AddLogging(
        this IHostBuilder hostBuilder
    )
    {
        hostBuilder.UseSerilog(
            (context, services, config) =>
            {
                config.MinimumLevel.Warning();

                config.WriteTo.Async(
                    wt =>
                    {
                        wt.Console(
                            outputTemplate:
                            "[{Timestamp:HH:mm:ss}] [{Level:u4}] <s:{SourceContext}> {Message:lj}{NewLine}{Exception}");
                    });

                config.Destructure.ToMaximumDepth(2);
                config.Destructure.ToMaximumStringLength(60);
                config.Destructure.ToMaximumCollectionCount(10);

                config
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services);
            });

        return hostBuilder;
    }
}
