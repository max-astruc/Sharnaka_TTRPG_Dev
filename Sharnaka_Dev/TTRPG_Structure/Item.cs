using System;
using System.Collections.Generic;
using System.Text;

namespace Sharnaka_Dev.TTRPG_Structure
{
    internal class Item
    {
        int item_id; // Unique identifier for the item

        string item_name; // Name of the item



        public Item(string name) 
        {
            this.item_id = 1;
            this.item_name = name;
        }
    }

}
