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
    DiscordBot()
    {
        string token =
        (            
            // Read the token from a file or environment variable
            // For example, you can read it from a file like this:
            File.ReadAllText("appsettings.json").Trim()
        );

        GatewayClient client = new(new BotToken(token), new GatewayClientConfiguration
        {
            Logger = new ConsoleLogger(),
        });
    }
}
