using NetCord;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;

namespace Sharnaka_Dev.Discord_Bot.Modules
{

    public class TestCommandsModule : ApplicationCommandModule<ApplicationCommandContext>
    {
        [SlashCommand("ping", "Says back Pong !")]
        public static string Ping() => "Pong !";

        [SlashCommand("salut", "Salutes a user")]
        public string Salut(User user)
            => $"{Context.User} says hello to {user} !";
    }
}