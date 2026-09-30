using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using NetCord.Hosting.Gateway;
using NetCord.Hosting.Services;
using NetCord.Hosting.Services.ApplicationCommands;
using Sharnaka_Dev.Data;
using Sharnaka_Dev.Services;

var builder = Host.CreateApplicationBuilder(args);



// Adds the user secrets configuration source to the application configuration to acces the token. 
builder.Configuration.AddUserSecrets<Program>();

// Registers the BotContext with a factory method to create instances of the context using SQLite as the database provider.
builder.Services.AddDbContextFactory<BotContext>(o => o.UseSqlite(DbPaths.ConnectionString));

// Registers bot inside the DI container
builder.Services.AddSingleton<DiscordBot>();

// Register an instance of the MarkdownServices
builder.Services.AddSingleton<MarkdownService>();

var host = builder.Build();

// Resolves and starts the bot
var bot = host.Services.GetRequiredService<DiscordBot>();

// Opens a DBContext scope to apply any pending migrations and set the SQLite journal mode to WAL (Write-Ahead Logging) for better performance and concurrency.
using (var scope = host.Services.CreateScope())
{
    var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<BotContext>>();
    await using var db = await factory.CreateDbContextAsync();
    await db.Database.MigrateAsync();
    await db.Database.ExecuteSqlRawAsync("PRAGMA journal_mode=WAL;");
}


// Bot runs indefinitely 
await host.RunAsync();

// Add the ApplicationCommandService to the DI container and populates the commands from the assembly containing the bot's commands.
// This allows the bot to register and handle application commands (slash commands) with Discord.
await bot.createAppCommands();