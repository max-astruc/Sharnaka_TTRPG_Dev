using NetCord;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;

// <summary>
// This class represents a module for handling dice rolling commands in a Discord bot using slash commands.
// </summary>

namespace Sharnaka_Dev.Discord_Bot.Modules
{
    public class DiceCommandsModule : ApplicationCommandModule<ApplicationCommandContext>
    {
        [SlashCommand("roll", "Rolls a die with the specified number of faces.")]
        public static string Roll(int faces)
        {
            Random random = new();
            return random.Next(1, faces + 1).ToString();
        }

        [SlashCommand("roll-d4", "Rolls a 4-sided die")]
        public static string RollD4() => Roll(4);

        [SlashCommand("roll-d6", "Rolls a 6-sided die")]
        public static string RollD6() => Roll(6);

        [SlashCommand("roll-d8", "Rolls a 8-sided die")]
        public static string RollD8() => Roll(8);

        [SlashCommand("roll-d10", "Rolls a 10-sided die")]
        public static string RollD10() => Roll(10);

        [SlashCommand("roll-d12", "Rolls a 12-sided die")]
        public static string RollD12() => Roll(12);

        [SlashCommand("roll-d20", "Rolls a 20-sided die")]
        public static string RollD20() => Roll(20);

        [SlashCommand("roll-d100", "Rolls a 100-sided die")]
        public static string RollD100() => Roll(100);

        [SlashCommand("roll-d-m10", "Rolls a 10-sided die with multiples of ten (00, 10, 20, ...)")]
        public static string RollDM10()
        {
            Random random = new();
            return (random.Next(0, 9) * 10).ToString();
        }
    }
}
