namespace Sharnaka_Dev.TTRPG_Structure
{
    internal class Character
    {
        public string Char_name { get; set; }
        public int Char_level { get; set; }

        public Species? Char_specie { get; set; }
        public Races? Char_race {  get; set; }

        public Wallet? Char_wallet;

        public bool IsAlive { get; set; }

        public Character() // Default constructor with default values, can be used for testing or non-interactible NPCs 
        {
            Char_name = "Default Name";
            Char_level = 1;
            Char_specie = null;
            Char_race = null;
            Char_wallet = null;
            IsAlive = true;
        }


        public Character(string name, Species? specie,  Races? race) // Constructor with minimal parameters, for creating characters with basic information ( ex: non interactible NPCs)
        {
            this.Char_name = name;
            this.Char_level = 1; // Default level for new characters is set to 1, can be adjusted as needed.
            this.Char_specie = specie;
            this.Char_race = race;
            this.Char_wallet = null; // Wallet is set to null by default, since it doesn't have to be implemented
                                     // NTS : could be implemented for looting or robery 
        }


        public Character(string name, int level, Species? specie, Races? race, Wallet? wallet) // Constructor that allows for setting all properties, including the wallet. This can be used for creating characters with specific starting money or for NPCs that have a wallet (eg : merchants/party members)
        {
            this.Char_name = name;
            this.Char_level = level;
            this.Char_specie = specie;
            this.Char_race = race;
            this.Char_wallet = new Wallet();  
        }
    }
}
