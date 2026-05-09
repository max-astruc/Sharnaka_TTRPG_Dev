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

        public bool IsAlive { get; set; }
    }
}
