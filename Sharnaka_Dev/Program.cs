using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);
var path = "C:\\Users\\ZEL\\source\\repos\\max-astruc\\Sharnaka_TTRPG_Dev\\Sharnaka_Dev\\Discord_Bot\\appsettings.json";
// Charge appsettings.json from folder dossier Discord_Bot
builder.Configuration.AddJsonFile(path , optional: false, reloadOnChange: true);

// Registers bot inside the DI container
builder.Services.AddSingleton<DiscordBot>();

var host = builder.Build();

// Resolves and starts the bot
var bot = host.Services.GetRequiredService<DiscordBot>();

// Bot runs indefinitely 
await host.RunAsync();  