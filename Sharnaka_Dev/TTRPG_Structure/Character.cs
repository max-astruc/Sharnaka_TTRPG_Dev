namespace Sharnaka_Dev.TTRPG_Structure
{
    internal class Character
    {
        public string Char_name { get; set; } // Name of the character
        public int Char_level { get; set; } // Level of experience of the character

        public Species? Char_specie { get; set; } // Specie of the character
        public Races? Char_race { get; set; } // Race of the character within it's species

        public Item[] Char_inv; // Character's inventory

        public Stats? Char_stats; // Character's stats

        public bool IsAlive { get; set; } // Living status of the character, true if alive, false if dead

        public bool IsNPC { get; set; } // Indicates if the character is an NPC (non-player character)

        public short VoluntyPoints { get; set; }

        public short Fortune { get; set; }



        public Character(string name, int level, Species? specie, Races? race, Item[]? inv)
        // Constructor that allows for setting all properties, including the wallet. Used for creating characters with specific starting money or for NPCs that have a wallet (eg : merchants/party members)
        // Default values are included to avoid errors
        {
            this.Char_name = name ?? "John NPC";
            this.Char_level = level;
            this.Char_specie = specie;
            this.Char_race = race;
            this.Char_inv = inv ?? new Item[2048]; // Use provided inventory or create a new one if none exists
            this.IsAlive = true; // Characters are alive by default

            this.Char_inv[(Char_inv.Length - 1)] = new Wallet(); // Adding a wallet as the last slot of the inventory (for simplicity) 



            
            GiveCharacterStats(this); // Set the character's base stats
        }


        
        public void GiveCharacterStats(Character _char)
        // Sets the character's base stats while taking in account species/race bonuses/maluses
        {
            _char.Char_stats = new Stats(); // Initialize stats for the character depending on race, can be adjusted to allow for custom stat values if needed.

            // By defaiult, all species bonus/malus are increments/decrements of 1 point to the base stats, can be adjusted to allow for more complex stat modifications if needed.
            switch (_char.Char_specie)
            {
                case Species.Astrel:
                    _char.Char_stats.Agility += 1;
                    _char.Char_stats.Dexterity += 1;
                    _char.Char_stats.Mental += 1;
                    _char.Char_stats.Constitution -=1;
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
                    _char.Char_stats.Strength += 1;
                    _char.Char_stats.Dexterity -= 1;
                    break;

                case Species.Murci:
                    switch(_char.Char_race)
                    {
                        case Races.Murci_Lavia:
                            _char.Char_stats.Intelligence += 1;
                            _char.Char_stats.Dexterity += 1;
                            _char.Char_stats.Perception += 1;
                            _char.Char_stats.Strength -= 1;
                            break;

                        case Races.Murci_Hipsy:
                            _char.Char_stats.Strength += 1;
                            _char.Char_stats.Agility += 1;
                            _char.Char_stats.Perception += 1;
                            _char.Char_stats.Intelligence -= 1;
                            break;
                    } 
                    break;

                    case Species.Tiefflin:
                        switch(_char.Char_race)
                        {
                            case Races.Tiefflin_Aeris:
                                _char.Char_stats.Agility += 1;
                                _char.Char_stats.Perception += 1;
                                break;
                            case Races.Tiefflin_Akwo:
                                _char.Char_stats.Mental += 1;
                                _char.Char_stats.Intelligence += 1;
                                break;
                            case Races.Tiefflin_Litho:
                                _char.Char_stats.Constitution += 1;
                                _char.Char_stats.Dexterity += 1;
                            break;
                            case Races.Tiefflin_Pyris:
                                _char.Char_stats.Strength += 1;
                                _char.Char_stats.Charisma += 1;
                                break;
                    }
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

        public int Perception { get; set; }
        public int Mental { get; set; }
        public int Charisma { get; set; }

        public Stats()
        {
            // By default, stats are set to 10 then modified by species/race bonuses/maluses in the GiveCharacterStats method
            this.Strength = 10;
            this.Agility = 10;
            this.Dexterity = 10;
            this.Constitution = 10;
            this.Intelligence = 10;
            this.Mental = 10;
            this.Charisma = 10;
            this.Perception = 10;
        }
    }

}
