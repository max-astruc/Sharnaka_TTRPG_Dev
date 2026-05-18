using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharnaka_Dev.TTRPG_Structure
{
    internal class Character
    {
        public string Name { get; set; }
        public int Level { get; set; }

        public Species specie { get; set; }
        public Races race {  get; set; }

        public Wallet? Char_wallet;

        public Stats? Char_stats; 
        public bool IsAlive { get; set; }

        public Character() 
        // Default constructor with default values, can be used for testing or non-interactible NPCs 
        {
            Char_name = "Default Name";
            Char_level = 1;
            Char_specie = null;
            Char_race = null;
            Char_wallet = null;
            IsAlive = true;
        }


        public Character(string name, Species? specie,  Races? race)
        // Constructor with minimal parameters, for creating characters with basic information ( ex: non interactible NPCs)
        {
            this.Char_name = name;
            this.Char_level = 1; // Default level for new characters is set to 1, can be adjusted as needed.
            this.Char_specie = specie;
            this.Char_race = race;
            this.Char_wallet = null; // Wallet is set to null by default, since it doesn't have to be implemented
                                     // NTS : could be implemented for looting or 
            this.IsAlive = true; // Characters are alive by default, can be set to false for certain NPCs or as a result of gameplay events.
        }


        public Character(string name, int level, Species? specie, Races? race, Wallet? wallet)
        // Constructor that allows for setting all properties, including the wallet. This can be used for creating characters with specific starting money or for NPCs that have a wallet (eg : merchants/party members)
        {
            this.Char_name = name;
            this.Char_level = level;
            this.Char_specie = specie;
            this.Char_race = race;
            this.Char_wallet = new Wallet();  
            
            this.IsAlive = true; // Characters are alive by default
        }

        public void SetCharacterStats(Character _char)
        {
            _char.Char_stats = new Stats(); // Initialize stats for the character, can be adjusted to allow for custom stat values if needed.
            switch(_char.Char_specie)
            {
                case Species.Astrel:
                    _char.Char_stats.Agility += 1;
                    _char.Char_stats.Intelligence += 2;
                    break;
                case Species.Cycléides:
                    _char.Char_stats.Constitution += 1;
                    _char.Char_stats.Mental += 1;
                    _char.Char_stats.Strength += 1;
                    _char.Char_stats.Intelligence -= 1; 
                    break;

                case Species.Joricien:
                    _char.Char_stats.Charisma += 1;
                    _char.Char_stats.Intelligence += 1;
                    break;
            }
        }
    }

    internal class Stats
    {
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Mental { get; set; }
        public int Charisma { get; set; }

        public Stats()
        {
            this.Strength = 10;
            this.Agility = 10;
            this.Dexterity = 10;
            this.Constitution = 10;
            this.Intelligence = 10;
            this.Mental = 10;
            this.Charisma = 10;
        }
    }
}
