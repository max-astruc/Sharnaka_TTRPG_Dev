using NetCord.Logging;
using NetCord.Gateway;
using NetCord;

//<summary>
// This class represents a Discord bot that connects to the Discord API using the NetCord library.
// The bot reads its token from a file (appsettings.json) and initializes a GatewayClient to handle the connection and interactions with Discord.
// The goal is to have a separated running environement for the bot than the TTRPG structure, so that we can have a more modular design and potentially run the bot independently of the TTRPG structure if needed in the future.
// The bot should seek every data it needs from the TTRPG structure, but for now, we will focus on getting the bot up and running and then we can implement the interactions with the TTRPG structure as needed.
// </summary>
public class DiscordBot
{
    GatewayClient client;
    string token;   
    DiscordBot()
    {
        this.token =
        (            
            // Read the token from a file or environment variable
            // For example, you can read it from a file like this:
            File.ReadAllText("appsettings.json").Trim()
        );

        // Get the token from the string and create a new GatewayClient with the token
        this.client = new(new BotToken(token), new GatewayClientConfiguration
        {
            Logger = new ConsoleLogger(),
        });

        StartBot();
    }

    // Async method to start the bot and keep it running indefinitely
    async Task StartBot()
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
