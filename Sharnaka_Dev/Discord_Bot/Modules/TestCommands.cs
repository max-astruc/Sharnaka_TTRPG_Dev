using NetCord;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;

namespace Sharnaka_Dev.Discord_Bot.Modules
{

    public class TestCommandsModule : ApplicationCommandModule<ApplicationCommandContext>
    {
        [SlashCommand("ping", "Says back Pong !")]
        public static string Ping() => "Pong !";

        [SlashCommand("salute", "Salutes a user")]
        public string Salute(User user)
            => $"{Context.User} says hello to {user} !";
    }
}