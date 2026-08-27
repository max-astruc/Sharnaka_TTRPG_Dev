using NetCord;
using NetCord.Rest;
using NetCord.Logging;
using NetCord.Gateway;
using NetCord.Services;
using NetCord.Services.ApplicationCommands;

using Microsoft.Extensions.Configuration;

//<summary>
// This class represents a Discord bot that connects to the Discord API using the NetCord library.
// The bot reads its token from the user secrets stored on the host machine and initializes a GatewayClient to handle the connection and interactions with Discord.
// The goal is to have a separated running environement for the bot than the TTRPG structure, so that we can have a more modular design and potentially run the bot independently of the TTRPG structure if needed in the future.
// The bot should seek every data it needs from the TTRPG structure, but for now, we will focus on getting the bot up and running and then we can implement the interactions with the TTRPG structure as needed.
/**************************************************************/
// As of right now, the bot only wakes up and connects while launching the executable, but a control panel should be developped in the future alongside an admin panel with the logger from the bot
// </summary>



// Constructor for the DiscordBot class that takes an IConfiguration object as a parameter. 
// The IConfiguration object is used to read the bot's token from the user secrets stored on the host machine. The constructor initializes a GatewayClient with the token and starts the bot asynchronously.
public class DiscordBot
{
    private GatewayClient client;
    private string token;   
    public DiscordBot _instance { get; }
    public required ApplicationCommandService<ApplicationCommandContext> applicationCommandService;
    public DiscordBot(IConfiguration configuration)
    {

        // Read the token from user secrets and check if it is null or empty, if it is, throw an exception
        this.token = configuration["Discord:Token"]     
            ?? throw new InvalidOperationException("Token not found in configuration. Please check the .NET user secrets.");

        // Log into the console that the token was found in the configuration
        if (!token.Equals(null))
        {
            Console.WriteLine($"{DateTime.Now}: Token found in configuration.");
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
        // Create the commands for the bot
        await this.createAppCommands();

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

    // Adding slash commands to the bot
    public async Task createAppCommands()
    {
        // Create the application command service
        applicationCommandService = new();

        // Add commands from modules
        applicationCommandService.AddModules(typeof(Program).Assembly);

        // Add the handler to handle interactions
        client.InteractionCreate += async interaction =>
        {
            // Check if the interaction is an application command interaction
            if (interaction is not ApplicationCommandInteraction applicationCommandInteraction)
                return;

            // Execute the command
            var result = await applicationCommandService.ExecuteAsync(new ApplicationCommandContext(applicationCommandInteraction, client));

            // Check if the execution failed
            if (result is not IFailResult failResult)
                return;

            // Return the error message to the user if the execution failed
            try
            {
                await interaction.SendResponseAsync(InteractionCallback.Message(failResult.Message));
            }
            catch
            {
            }
        };

        // Register the commands withint the Discord client 
        await applicationCommandService.RegisterCommandsAsync(client.Rest, client.Id);
    }
}
