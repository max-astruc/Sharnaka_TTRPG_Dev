using System;
using System.Collections.Generic;
using System.Text;

namespace Sharnaka_Dev.TTRPG_Structure
{
    internal class Bodypart
    {
        string Bodypart_name { get; set; } // Name of the body part 

        string Bodypart_state { get; set; } // State of the body part (eg : healthy, injured, broken, etc.)

        Bodypart(string p_name, string? p_state)
        {
            Bodypart_name = p_name;
            Bodypart_state = p_state ?? "Healthy"; // Default state if none is provided
        }
    }
}
