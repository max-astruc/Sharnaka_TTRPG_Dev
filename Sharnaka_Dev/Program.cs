using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);
//var path = "C:\\Users\\ZEL\\source\\repos\\max-astruc\\Sharnaka_TTRPG_Dev\\Sharnaka_Dev\\Discord_Bot\\appsettings.json";
//// Charge appsettings.json from folder  Discord_Bot

// Adds the user secrets configuration source to the application configuration. This allows you to store sensitive information, such as API keys 
builder.Configuration.AddUserSecrets<Program>();

// Registers bot inside the DI container
builder.Services.AddSingleton<DiscordBot>();

var host = builder.Build();

// Resolves and starts the bot
var bot = host.Services.GetRequiredService<DiscordBot>();

// Bot runs indefinitely 
await host.RunAsync();  