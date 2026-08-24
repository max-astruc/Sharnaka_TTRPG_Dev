using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);



// Adds the user secrets configuration source to the application configuration. This allows you to store sensitive information, such as API keys 
builder.Configuration.AddUserSecrets<Program>();

// Registers bot inside the DI container
builder.Services.AddSingleton<DiscordBot>();

var host = builder.Build();

// Resolves and starts the bot
var bot = host.Services.GetRequiredService<DiscordBot>();

// Bot runs indefinitely 
await host.RunAsync();

// Add the ApplicationCommandService to the DI container and populates the commands from the assembly containing the bot's commands.
// This allows the bot to register and handle application commands (slash commands) with Discord.
await bot.createAppCommands();