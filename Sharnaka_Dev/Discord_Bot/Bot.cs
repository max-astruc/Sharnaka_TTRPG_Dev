using NetCord;
using NetCord.Logging;
using NetCord.Gateway;
using Microsoft.Extensions.Configuration;

//<summary>
// This class represents a Discord bot that connects to the Discord API using the NetCord library.
// The bot reads its token from a file (appsettings.json) and initializes a GatewayClient to handle the connection and interactions with Discord.
// The goal is to have a separated running environement for the bot than the TTRPG structure, so that we can have a more modular design and potentially run the bot independently of the TTRPG structure if needed in the future.
// The bot should seek every data it needs from the TTRPG structure, but for now, we will focus on getting the bot up and running and then we can implement the interactions with the TTRPG structure as needed.
// </summary>
public class DiscordBot
{
    private GatewayClient client;
    private string token;   
    public DiscordBot _instance { get; }
    public DiscordBot(IConfiguration configuration)
    {

        // Read the token from user secrets and check if it is null or empty, if it is, throw an exception
        this.token = configuration["Discord:Token"]     
            ?? throw new InvalidOperationException("Token not found in configuration. Please check the .NET user secrets.");

        if (!token.Equals(null))
        {
            Console.WriteLine("Token found in configuration.");
        }

        // Get the token from the string and create a new GatewayClient with the token
        this.client = new(new BotToken(token), new GatewayClientConfiguration
        {
            Logger = new ConsoleLogger(),
        });

        // could be removed since we already have a singleton instance of the bot, but we can keep it for now in case we want to have multiple instances of the bot in the future
        _instance = this;
        _ = StartBot();
    }

    // Async method to start the bot and keep it running indefinitely
    private async Task StartBot()
    {
        if (this.client != null)
        {
            await client.StartAsync();
            await Task.Delay(-1); // Keep the bot running indefinitely
            
        }
        else
        {
            Console.WriteLine("Client is not initialized.");
            throw new Exception("Client is not initialized. Some parameters are missing.");
        }
    }   
}
