using System;
using Microsoft.Extensions.Hosting;

using NetCord.Hosting.Gateway;

public class DiscordBot
{
    DiscordBot()
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddDiscordGateway();

        var host = builder.Build();

        host.RunAsync().GetAwaiter().GetResult();

        ///await host.RunAsync();
    }
}
