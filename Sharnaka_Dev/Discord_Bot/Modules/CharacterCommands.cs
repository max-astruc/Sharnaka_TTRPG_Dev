using NetCord.Rest;
using NetCord.Services.ApplicationCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sharnaka_Dev.Discord_Bot.Modules
{
    public class CharacterCommandsModule : ApplicationCommandModule<ApplicationCommandContext>
    {
        [SlashCommand("char", "Outputs your charcarter page")]
        public static string Char() => "Feature still in building !";

        [SlashCommand("create", "Create your character")]
        public static string CreateChararcter() => "Feature still in WIP !";
    }
}
