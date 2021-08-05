using System;
using System.Collections.Generic;
using System.Text;

namespace HappiestDungeon
{
    enum Phasetype {Encounter, Looting, Setup, Transit}; //type of Phase, setup = changing spells etc., details about encounter is read from node
    //there is currently no looting
    class Phase
    {
        static Dictionary<Phasetype, Phasetype> Transitions = new Dictionary<Phasetype, Phasetype> 
        { 
            { Phasetype.Encounter, Phasetype.Setup },  { Phasetype.Setup, Phasetype.Transit},  { Phasetype.Transit, Phasetype.Encounter },
            { Phasetype.Encounter, Phasetype.Setup },  { Phasetype.Encounter, Phasetype.Encounter },  
        };
        Phasetype Phasetype;
        
        public Phase(Phasetype phasetype)
        {
            Phasetype = phasetype;
        }
    }
}
